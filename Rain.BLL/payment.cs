// Decompiled with JetBrains decompiler
// Type: Rain.BLL.payment
// Assembly: Rain.BLL, Version=4.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 8924FCAE-37FA-4301-A188-DA020D33C8EB
// Assembly location: C:\Users\Felix.Ho\Desktop\page\bin\Rain.BLL.dll

using System.Data;

namespace Rain.BLL
{
  public class payment
  {
    private readonly Rain.Model.siteconfig siteConfig = new siteconfig().loadConfig();
    private readonly Rain.DAL.payment dal;

    public payment()
    {
      this.dal = new Rain.DAL.payment(this.siteConfig.sysdatabaseprefix);
    }

    public bool Exists(int id)
    {
      return this.dal.Exists(id);
    }

    public int Add(Rain.Model.payment model)
    {
      return this.dal.Add(model);
    }

    public bool Update(Rain.Model.payment model)
    {
      return this.dal.Update(model);
    }

    public bool Delete(int id)
    {
      return this.dal.Delete(id);
    }

    public Rain.Model.payment GetModel(int id)
    {
      return this.dal.GetModel(id);
    }

    public DataSet GetList(string strWhere)
    {
      return this.dal.GetList(strWhere);
    }

    public DataSet GetList(int Top, string strWhere, string filedOrder)
    {
      return this.dal.GetList(Top, strWhere, filedOrder);
    }

    public string GetTitle(int id)
    {
      return this.dal.GetTitle(id);
    }

    public void UpdateField(int id, string strValue)
    {
      this.dal.UpdateField(id, strValue);
    }
  }
}
