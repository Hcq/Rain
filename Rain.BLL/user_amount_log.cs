// Decompiled with JetBrains decompiler
// Type: Rain.BLL.user_amount_log
// Assembly: Rain.BLL, Version=4.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 8924FCAE-37FA-4301-A188-DA020D33C8EB
// Assembly location: C:\Users\Felix.Ho\Desktop\page\bin\Rain.BLL.dll

using System;
using System.Data;

namespace Rain.BLL
{
  public class user_amount_log
  {
    private readonly Rain.Model.siteconfig siteConfig = new siteconfig().loadConfig();
    private readonly Rain.DAL.user_amount_log dal;

    public user_amount_log()
    {
      this.dal = new Rain.DAL.user_amount_log(this.siteConfig.sysdatabaseprefix);
    }

    public bool Exists(int id)
    {
      return this.dal.Exists(id);
    }

    public int Add(int user_id, string user_name, Decimal value, string remark)
    {
      return this.dal.Add(new Rain.Model.user_amount_log()
      {
        user_id = user_id,
        user_name = user_name,
        value = value,
        remark = remark
      });
    }

    public int Add(Rain.Model.user_amount_log model)
    {
      return this.dal.Add(model);
    }

    public bool Update(Rain.Model.user_amount_log model)
    {
      return this.dal.Update(model);
    }

    public bool Delete(int id)
    {
      return this.dal.Delete(id);
    }

    public bool Delete(int id, string user_name)
    {
      return this.dal.Delete(id, user_name);
    }

    public Rain.Model.user_amount_log GetModel(int id)
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
  }
}
