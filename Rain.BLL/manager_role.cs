// Decompiled with JetBrains decompiler
// Type: Rain.BLL.manager_role
// Assembly: Rain.BLL, Version=4.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 8924FCAE-37FA-4301-A188-DA020D33C8EB
// Assembly location: C:\Users\Felix.Ho\Desktop\page\bin\Rain.BLL.dll

using System;
using System.Data;
using Rain.Model;

namespace Rain.BLL
{
  public class manager_role
  {
    private readonly Rain.Model.siteconfig siteConfig = new siteconfig().loadConfig();
    private readonly Rain.DAL.manager_role dal;

    public manager_role()
    {
      this.dal = new Rain.DAL.manager_role(this.siteConfig.sysdatabaseprefix);
    }

    public bool Exists(int id)
    {
      return this.dal.Exists(id);
    }

    public bool Exists(int role_id, string nav_name, string action_type)
    {
      Rain.Model.manager_role model = this.dal.GetModel(role_id);
      return model != null && (model.role_type == 1 || model.manager_role_values.Find((Predicate<manager_role_value>) (p => p.nav_name == nav_name && p.action_type == action_type)) != null);
    }

    public string GetTitle(int id)
    {
      return this.dal.GetTitle(id);
    }

    public int Add(Rain.Model.manager_role model)
    {
      return this.dal.Add(model);
    }

    public bool Update(Rain.Model.manager_role model)
    {
      return this.dal.Update(model);
    }

    public bool Delete(int id)
    {
      return this.dal.Delete(id);
    }

    public Rain.Model.manager_role GetModel(int id)
    {
      return this.dal.GetModel(id);
    }

    public DataSet GetList(string strWhere)
    {
      return this.dal.GetList(strWhere);
    }
  }
}
