// Decompiled with JetBrains decompiler
// Type: Rain.BLL.article_attach
// Assembly: Rain.BLL, Version=4.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 8924FCAE-37FA-4301-A188-DA020D33C8EB
// Assembly location: C:\Users\Felix.Ho\Desktop\page\bin\Rain.BLL.dll

using System;
using Rain.Common;

namespace Rain.BLL
{
  public class article_attach
  {
    private readonly Rain.Model.siteconfig siteConfig = new siteconfig().loadConfig();
    private readonly Rain.DAL.article_attach dal;

    public article_attach()
    {
      this.dal = new Rain.DAL.article_attach(this.siteConfig.sysdatabaseprefix);
    }

    public bool Exists(int id)
    {
      return this.dal.Exists(id);
    }

    public bool ExistsLog(int attach_id, int user_id)
    {
      return this.dal.ExistsLog(attach_id, user_id);
    }

    public int GetDownNum(int id)
    {
      return this.dal.GetDownNum(id);
    }

    public int GetCountNum(int article_id)
    {
      return this.dal.GetCountNum(article_id);
    }

    public int AddLog(int user_id, string user_name, int attach_id, string file_name)
    {
      return this.dal.AddLog(new Rain.Model.user_attach_log()
      {
        user_id = user_id,
        user_name = user_name,
        attach_id = attach_id,
        file_name = file_name,
        add_time = DateTime.Now
      });
    }

    public void UpdateField(int id, string strValue)
    {
      this.dal.UpdateField(id, strValue);
    }

    public Rain.Model.article_attach GetModel(int id)
    {
      return this.dal.GetModel(id);
    }

    public void DeleteFile(int id, string filePath)
    {
      Rain.Model.article_attach model = this.GetModel(id);
      if (model == null || !(model.file_path != filePath))
        return;
      Utils.DeleteFile(model.file_path);
    }
  }
}
