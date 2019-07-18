// Decompiled with JetBrains decompiler
// Type: Rain.BLL.sms_template
// Assembly: Rain.BLL, Version=4.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 8924FCAE-37FA-4301-A188-DA020D33C8EB
// Assembly location: C:\Users\Felix.Ho\Desktop\page\bin\Rain.BLL.dll

using System.Data;

namespace Rain.BLL
{
  public class sms_template
  {
    private readonly Rain.Model.siteconfig siteConfig = new siteconfig().loadConfig();
    private readonly Rain.DAL.sms_template dal;

    public sms_template()
    {
      this.dal = new Rain.DAL.sms_template(this.siteConfig.sysdatabaseprefix);
    }

    public bool Exists(int id)
    {
      return this.dal.Exists(id);
    }

    public bool Exists(string call_index)
    {
      return this.dal.Exists(call_index);
    }

    public int Add(Rain.Model.sms_template model)
    {
      return this.dal.Add(model);
    }

    public bool Update(Rain.Model.sms_template model)
    {
      return this.dal.Update(model);
    }

    public bool Delete(int id)
    {
      return this.dal.Delete(id);
    }

    public Rain.Model.sms_template GetModel(int id)
    {
      return this.dal.GetModel(id);
    }

    public Rain.Model.sms_template GetModel(string call_index)
    {
      return this.dal.GetModel(call_index);
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
