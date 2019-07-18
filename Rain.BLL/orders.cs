// Decompiled with JetBrains decompiler
// Type: Rain.BLL.orders
// Assembly: Rain.BLL, Version=4.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 8924FCAE-37FA-4301-A188-DA020D33C8EB
// Assembly location: C:\Users\Felix.Ho\Desktop\page\bin\Rain.BLL.dll

using System.Data;

namespace Rain.BLL
{
  public class orders
  {
    private readonly Rain.Model.siteconfig siteConfig = new siteconfig().loadConfig();
    private Rain.DAL.orders dal;

    public orders()
    {
      this.dal = new Rain.DAL.orders(this.siteConfig.sysdatabaseprefix);
    }

    public bool Exists(int id)
    {
      return this.dal.Exists(id);
    }

    public bool Exists(string order_no)
    {
      return this.dal.Exists(order_no);
    }

    public int Add(Rain.Model.orders model)
    {
      return this.dal.Add(model);
    }

    public bool Update(Rain.Model.orders model)
    {
      model.order_amount = model.real_amount + model.express_fee + model.payment_fee + model.invoice_taxes;
      return this.dal.Update(model);
    }

    public bool Delete(int id)
    {
      return this.dal.Delete(id);
    }

    public Rain.Model.orders GetModel(int id)
    {
      return this.dal.GetModel(id);
    }

    public Rain.Model.orders GetModel(string order_no)
    {
      return this.dal.GetModel(order_no);
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

    public int GetCount(string strWhere)
    {
      return this.dal.GetCount(strWhere);
    }

    public void UpdateField(int id, string strValue)
    {
      this.dal.UpdateField(id, strValue);
    }

    public bool UpdateField(string order_no, string strValue)
    {
      return this.dal.UpdateField(order_no, strValue);
    }
  }
}
