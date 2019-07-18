// Decompiled with JetBrains decompiler
// Type: Rain.BLL.channel_site
// Assembly: Rain.BLL, Version=4.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 8924FCAE-37FA-4301-A188-DA020D33C8EB
// Assembly location: C:\Users\Felix.Ho\Desktop\page\bin\Rain.BLL.dll

using System.Collections.Generic;
using System.Data;
using System.IO;
using Rain.Common;

namespace Rain.BLL
{
  public class channel_site
  {
    private readonly Rain.Model.siteconfig siteConfig = new siteconfig().loadConfig();
    private readonly Rain.DAL.channel_site dal;

    public channel_site()
    {
      this.dal = new Rain.DAL.channel_site(this.siteConfig.sysdatabaseprefix);
    }

    public bool Exists(int id)
    {
      return this.dal.Exists(id);
    }

    public bool Exists(string build_path)
    {
      return this.DirPathExists(this.siteConfig.webpath, build_path) || this.DirPathExists(this.siteConfig.webpath + "/aspx/", build_path) || (new Rain.DAL.channel(this.siteConfig.sysdatabaseprefix).Exists(build_path) || this.dal.Exists(build_path));
    }

    public int Add(Rain.Model.channel_site model)
    {
      return this.dal.Add(model);
    }

    public bool Update(Rain.Model.channel_site model)
    {
      string buildPath = this.dal.GetBuildPath(model.id);
      if (string.IsNullOrEmpty(buildPath) || !this.dal.Update(model, buildPath))
        return false;
      if (buildPath.ToLower() != model.build_path.ToLower())
      {
        Utils.MoveDirectory(this.siteConfig.webpath + "aspx/" + buildPath, this.siteConfig.webpath + "aspx/" + model.build_path);
        Utils.MoveDirectory(this.siteConfig.webpath + "html/" + buildPath, this.siteConfig.webpath + "html/" + model.build_path);
      }
      return true;
    }

    public bool Delete(int id)
    {
      return this.dal.Delete(id);
    }

    public Rain.Model.channel_site GetModel(int id)
    {
      return this.dal.GetModel(id);
    }

    public Rain.Model.channel_site GetModel(string build_path)
    {
      return this.dal.GetModel(build_path);
    }

    public DataSet GetList(int Top, string strWhere, string filedOrder)
    {
      return this.dal.GetList(Top, strWhere, filedOrder);
    }

    public DataSet GetList(
      int pageSize,
      int pageIndex,
      string strWhere,
      string filedOrder,
      out int recordCount)
    {
      return this.dal.GetList(pageSize, pageIndex, strWhere, filedOrder, out recordCount);
    }

    public string GetTitle(int id)
    {
      return this.dal.GetTitle(id);
    }

    public string GetTitle(string build_path)
    {
      return this.dal.GetTitle(build_path);
    }

    public bool UpdateField(int id, string strValue)
    {
      return this.dal.UpdateField(id, strValue);
    }

    public bool UpdateField(string build_path, string strValue)
    {
      return this.dal.UpdateField(build_path, strValue);
    }

    public bool UpdateSort(int id, int sort_id)
    {
      string buildPath = this.dal.GetBuildPath(id);
      if (buildPath == string.Empty)
        return false;
      new navigation().UpdateField("channel_" + buildPath, "sort_id=" + (object) sort_id);
      this.dal.UpdateField(id, "sort_id=" + (object) sort_id);
      return true;
    }

    public List<Rain.Model.channel_site> GetModelList()
    {
      return this.DataTableToList(this.dal.GetList(0, string.Empty, "sort_id asc,id desc").Tables[0]);
    }

    private List<Rain.Model.channel_site> DataTableToList(DataTable dt)
    {
      List<Rain.Model.channel_site> channelSiteList = new List<Rain.Model.channel_site>();
      int count = dt.Rows.Count;
      if (count > 0)
      {
        for (int index = 0; index < count; ++index)
          channelSiteList.Add(this.dal.DataRowToModel(dt.Rows[index]));
      }
      return channelSiteList;
    }

    private bool DirPathExists(string dirPath, string build_path)
    {
      foreach (DirectoryInfo directory in new DirectoryInfo(Utils.GetMapPath(dirPath)).GetDirectories())
      {
        if (build_path.ToLower() == directory.Name.ToLower())
          return true;
      }
      return false;
    }
  }
}
