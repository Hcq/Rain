// Decompiled with JetBrains decompiler
// Type: Rain.BLL.user_message
// Assembly: Rain.BLL, Version=4.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 8924FCAE-37FA-4301-A188-DA020D33C8EB
// Assembly location: C:\Users\Felix.Ho\Desktop\page\bin\Rain.BLL.dll

using System.Data;

namespace Rain.BLL
{
  public class user_message
  {
    private readonly Rain.Model.siteconfig siteConfig = new siteconfig().loadConfig();
    private readonly Rain.DAL.user_message dal;

    public user_message()
    {
      this.dal = new Rain.DAL.user_message(this.siteConfig.sysdatabaseprefix);
    }

    public bool Exists(int id)
    {
      return this.dal.Exists(id);
    }

    public int Add(
      int type,
      string post_user_name,
      string accept_user_name,
      string title,
      string content)
    {
      return this.Add(new Rain.Model.user_message()
      {
        type = type,
        post_user_name = post_user_name,
        accept_user_name = accept_user_name,
        title = title,
        content = content
      });
    }

    public int Add(Rain.Model.user_message model)
    {
      return this.dal.Add(model);
    }

    public bool Update(Rain.Model.user_message model)
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

    public Rain.Model.user_message GetModel(int id)
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

    public int GetCount(string strWhere)
    {
      return this.dal.GetCount(strWhere);
    }

    public void UpdateField(int id, string strValue)
    {
      this.dal.UpdateField(id, strValue);
    }
  }
}
