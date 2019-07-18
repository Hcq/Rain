// Decompiled with JetBrains decompiler
// Type: Rain.BLL.user_oauth
// Assembly: Rain.BLL, Version=4.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 8924FCAE-37FA-4301-A188-DA020D33C8EB
// Assembly location: C:\Users\Felix.Ho\Desktop\page\bin\Rain.BLL.dll

using System.Data;

namespace Rain.BLL
{
  public class user_oauth
  {
    private readonly Rain.Model.siteconfig siteConfig = new siteconfig().loadConfig();
    private readonly Rain.DAL.user_oauth dal;

    public user_oauth()
    {
      this.dal = new Rain.DAL.user_oauth(this.siteConfig.sysdatabaseprefix);
    }

    public bool Exists(int id)
    {
      return this.dal.Exists(id);
    }

    public int Add(Rain.Model.user_oauth model)
    {
      return this.dal.Add(model);
    }

    public bool Update(Rain.Model.user_oauth model)
    {
      return this.dal.Update(model);
    }

    public bool Delete(int id)
    {
      return this.dal.Delete(id);
    }

    public Rain.Model.user_oauth GetModel(int id)
    {
      return this.dal.GetModel(id);
    }

    public Rain.Model.user_oauth GetModel(string oauth_name, string oauth_openid)
    {
      return this.dal.GetModel(oauth_name, oauth_openid);
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
