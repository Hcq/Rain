// Decompiled with JetBrains decompiler
// Type: Rain.BLL.user_login_log
// Assembly: Rain.BLL, Version=4.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 8924FCAE-37FA-4301-A188-DA020D33C8EB
// Assembly location: C:\Users\Felix.Ho\Desktop\page\bin\Rain.BLL.dll

using System;
using System.Data;
using Rain.Common;

namespace Rain.BLL
{
  public class user_login_log
  {
    private readonly Rain.Model.siteconfig siteConfig = new siteconfig().loadConfig();
    private readonly Rain.DAL.user_login_log dal;

    public user_login_log()
    {
      this.dal = new Rain.DAL.user_login_log(this.siteConfig.sysdatabaseprefix);
    }

    public bool Exists(int id)
    {
      return this.dal.Exists(id);
    }

    public int Add(int user_id, string user_name, string remark)
    {
      return this.dal.Add(new Rain.Model.user_login_log()
      {
        user_id = user_id,
        user_name = user_name,
        remark = remark,
        login_ip = DTRequest.GetIP(),
        login_time = DateTime.Now
      });
    }

    public int Add(Rain.Model.user_login_log model)
    {
      return this.dal.Add(model);
    }

    public bool Delete(int id)
    {
      return this.dal.Delete(id);
    }

    public Rain.Model.user_login_log GetModel(int id)
    {
      return this.dal.GetModel(id);
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

    public bool ExistsDay(string username)
    {
      return this.dal.ExistsDay(username);
    }

    public Rain.Model.user_login_log GetLastModel(string user_name)
    {
      return this.dal.GetLastModel(user_name);
    }
  }
}
