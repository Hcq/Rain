// Decompiled with JetBrains decompiler
// Type: Rain.BLL.user_code
// Assembly: Rain.BLL, Version=4.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 8924FCAE-37FA-4301-A188-DA020D33C8EB
// Assembly location: C:\Users\Felix.Ho\Desktop\page\bin\Rain.BLL.dll

using System.Data;

namespace Rain.BLL
{
  public class user_code
  {
    private readonly Rain.Model.siteconfig siteConfig = new siteconfig().loadConfig();
    private readonly Rain.DAL.user_code dal;

    public user_code()
    {
      this.dal = new Rain.DAL.user_code(this.siteConfig.sysdatabaseprefix);
    }

    public bool Exists(int id)
    {
      return this.dal.Exists(id);
    }

    public bool Exists(string type, string user_name)
    {
      return this.dal.Exists(type, user_name);
    }

    public int Add(Rain.Model.user_code model)
    {
      return this.dal.Add(model);
    }

    public bool Update(Rain.Model.user_code model)
    {
      return this.dal.Update(model);
    }

    public bool Delete(int id)
    {
      return this.dal.Delete(id);
    }

    public bool Delete(string strWhere)
    {
      return this.dal.Delete(strWhere);
    }

    public Rain.Model.user_code GetModel(int id)
    {
      return this.dal.GetModel(id);
    }

    public Rain.Model.user_code GetModel(string str_code)
    {
      return this.dal.GetModel(str_code);
    }

    public Rain.Model.user_code GetModel(
      string user_name,
      string code_type,
      string datepart)
    {
      return this.dal.GetModel(user_name, code_type, datepart);
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
  }
}
