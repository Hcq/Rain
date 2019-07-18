// Decompiled with JetBrains decompiler
// Type: Rain.BLL.user_recharge
// Assembly: Rain.BLL, Version=4.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 8924FCAE-37FA-4301-A188-DA020D33C8EB
// Assembly location: C:\Users\Felix.Ho\Desktop\page\bin\Rain.BLL.dll

using System;
using System.Data;

namespace Rain.BLL
{
  public class user_recharge
  {
    private readonly Rain.Model.siteconfig siteConfig = new siteconfig().loadConfig();
    private readonly Rain.DAL.user_recharge dal;

    public user_recharge()
    {
      this.dal = new Rain.DAL.user_recharge(this.siteConfig.sysdatabaseprefix);
    }

    public bool Exists(int id)
    {
      return this.dal.Exists(id);
    }

    public int Add(Rain.Model.user_recharge model)
    {
      return this.dal.Add(model);
    }

    public int Add(
      int user_id,
      string user_name,
      string recharge_no,
      int payment_id,
      Decimal amount)
    {
      return this.dal.Add(new Rain.Model.user_recharge()
      {
        user_id = user_id,
        user_name = user_name,
        recharge_no = recharge_no,
        payment_id = payment_id,
        amount = amount,
        status = 0,
        add_time = DateTime.Now
      });
    }

    public bool Update(Rain.Model.user_recharge model)
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

    public Rain.Model.user_recharge GetModel(int id)
    {
      return this.dal.GetModel(id);
    }

    public Rain.Model.user_recharge GetModel(string recharge_no)
    {
      return this.dal.GetModel(recharge_no);
    }

    public DataSet GetList(string strWhere)
    {
      return this.dal.GetList(strWhere);
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

    public bool Recharge(Rain.Model.user_recharge model)
    {
      return this.dal.Recharge(model);
    }

    public bool Confirm(string recharge_no)
    {
      return this.dal.Confirm(recharge_no);
    }
  }
}
