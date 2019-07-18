// Decompiled with JetBrains decompiler
// Type: Rain.BLL.user_groups
// Assembly: Rain.BLL, Version=4.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 8924FCAE-37FA-4301-A188-DA020D33C8EB
// Assembly location: C:\Users\Felix.Ho\Desktop\page\bin\Rain.BLL.dll

using System.Data;

namespace Rain.BLL
{
  public class user_groups
  {
    private readonly Rain.Model.siteconfig siteConfig = new siteconfig().loadConfig();
    private readonly Rain.DAL.user_groups dal;

    public user_groups()
    {
      this.dal = new Rain.DAL.user_groups(this.siteConfig.sysdatabaseprefix);
    }

    public bool Exists(int id)
    {
      return this.dal.Exists(id);
    }

    public int Add(Rain.Model.user_groups model)
    {
      return this.dal.Add(model);
    }

    public bool Update(Rain.Model.user_groups model)
    {
      return this.dal.Update(model);
    }

    public bool Delete(int id)
    {
      return this.dal.Delete(id);
    }

    public Rain.Model.user_groups GetModel(int id)
    {
      return this.dal.GetModel(id);
    }

    public DataSet GetList(int Top, string strWhere, string filedOrder)
    {
      return this.dal.GetList(Top, strWhere, filedOrder);
    }

    public string GetTitle(int id)
    {
      return this.dal.GetTitle(id);
    }

    public int GetDiscount(int id)
    {
      return this.dal.GetDiscount(id);
    }

    public Rain.Model.user_groups GetDefault()
    {
      return this.dal.GetDefault();
    }

    public Rain.Model.user_groups GetUpgrade(int group_id, int exp)
    {
      return this.dal.GetUpgrade(group_id, exp);
    }
  }
}
