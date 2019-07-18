// Decompiled with JetBrains decompiler
// Type: Rain.BLL.manager
// Assembly: Rain.BLL, Version=4.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 8924FCAE-37FA-4301-A188-DA020D33C8EB
// Assembly location: C:\Users\Felix.Ho\Desktop\page\bin\Rain.BLL.dll

using System.Data;
using Rain.Common;

namespace Rain.BLL
{
  public class manager
  {
    private readonly Rain.Model.siteconfig siteConfig = new siteconfig().loadConfig();
    private readonly Rain.DAL.manager dal;

    public manager()
    {
      this.dal = new Rain.DAL.manager(this.siteConfig.sysdatabaseprefix);
    }

    public bool Exists(int id)
    {
      return this.dal.Exists(id);
    }

    public bool Exists(string user_name)
    {
      return this.dal.Exists(user_name);
    }

    public string GetSalt(string user_name)
    {
      return this.dal.GetSalt(user_name);
    }

    public int Add(Rain.Model.manager model)
    {
      return this.dal.Add(model);
    }

    public bool Update(Rain.Model.manager model)
    {
      return this.dal.Update(model);
    }

    public bool Delete(int id)
    {
      return this.dal.Delete(id);
    }

    public Rain.Model.manager GetModel(int id)
    {
      return this.dal.GetModel(id);
    }

    public Rain.Model.manager GetModel(string user_name, string password)
    {
      return this.dal.GetModel(user_name, password);
    }

    public Rain.Model.manager GetModel(string user_name, string password, bool is_encrypt)
    {
      if (is_encrypt)
      {
        string salt = this.dal.GetSalt(user_name);
        if (string.IsNullOrEmpty(salt))
          return (Rain.Model.manager) null;
        password = DESEncrypt.Encrypt(password, salt);
      }
      return this.dal.GetModel(user_name, password);
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
