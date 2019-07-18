// Decompiled with JetBrains decompiler
// Type: Rain.BLL.channel
// Assembly: Rain.BLL, Version=4.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 8924FCAE-37FA-4301-A188-DA020D33C8EB
// Assembly location: C:\Users\Felix.Ho\Desktop\page\bin\Rain.BLL.dll

using System.Data;
using System.IO;
using Rain.Common;

namespace Rain.BLL
{
  public class channel
  {
    private readonly Rain.Model.siteconfig siteConfig = new siteconfig().loadConfig();
    private readonly Rain.DAL.channel dal;

    public channel()
    {
      this.dal = new Rain.DAL.channel(this.siteConfig.sysdatabaseprefix);
    }

    public bool Exists(int id)
    {
      return this.dal.Exists(id);
    }

    public bool Exists(string name)
    {
      return this.DirPathExists(this.siteConfig.webpath, name) || this.DirPathExists(this.siteConfig.webpath + "/aspx/", name) || this.dal.Exists(name);
    }

    public int GetCount(string strWhere)
    {
      return this.dal.GetCount(strWhere);
    }

    public int Add(Rain.Model.channel model)
    {
      return this.dal.Add(model);
    }

    public bool Update(Rain.Model.channel model)
    {
      return this.dal.Update(model);
    }

    public bool Delete(int id)
    {
      return this.dal.Delete(id);
    }

    public Rain.Model.channel GetModel(int id)
    {
      return this.dal.GetModel(id);
    }

    public Rain.Model.channel GetModel(string channel_name)
    {
      return this.dal.GetModel(channel_name);
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

    public void UpdateField(int id, string strValue)
    {
      this.dal.UpdateField(id, strValue);
    }

    public string GetChannelName(int id)
    {
      return this.dal.GetChannelName(id);
    }

    public int GetChannelId(string name)
    {
      return this.dal.GetChannelId(name);
    }

    public bool UpdateSort(int id, int sort_id)
    {
      string channelName = this.dal.GetChannelName(id);
      if (channelName == string.Empty || !new navigation().UpdateField("channel_" + channelName, "sort_id=" + (object) sort_id))
        return false;
      this.dal.UpdateField(id, "sort_id=" + (object) sort_id);
      return true;
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
