// Decompiled with JetBrains decompiler
// Type: Rain.BLL.users
// Assembly: Rain.BLL, Version=4.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 8924FCAE-37FA-4301-A188-DA020D33C8EB
// Assembly location: C:\Users\Felix.Ho\Desktop\page\bin\Rain.BLL.dll

using System;
using System.Data;
using Rain.Common;

namespace Rain.BLL
{
  public class users
  {
    private readonly Rain.Model.siteconfig siteConfig = new siteconfig().loadConfig();
    private readonly Rain.DAL.users dal;

    public users()
    {
      this.dal = new Rain.DAL.users(this.siteConfig.sysdatabaseprefix);
    }

    public bool Exists(int id)
    {
      return this.dal.Exists(id);
    }

    public bool Exists(string user_name)
    {
      return this.dal.Exists(user_name);
    }

    public bool Exists(string reg_ip, int regctrl)
    {
      return this.dal.Exists(reg_ip, regctrl);
    }

    public int Add(Rain.Model.users model)
    {
      return this.dal.Add(model);
    }

    public bool Update(Rain.Model.users model)
    {
      return this.dal.Update(model);
    }

    public bool Delete(int id)
    {
      return this.dal.Delete(id);
    }

    public Rain.Model.users GetModel(int id)
    {
      return this.dal.GetModel(id);
    }

    public Rain.Model.users GetModel(
      string user_name,
      string password,
      int emaillogin,
      int mobilelogin,
      bool is_encrypt)
    {
      if (is_encrypt)
      {
        string salt = this.dal.GetSalt(user_name);
        if (string.IsNullOrEmpty(salt))
          return (Rain.Model.users) null;
        password = DESEncrypt.Encrypt(password, salt);
      }
      return this.dal.GetModel(user_name, password, emaillogin, mobilelogin);
    }

    public Rain.Model.users GetModel(string user_name)
    {
      return this.dal.GetModel(user_name);
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

    public bool ExistsEmail(string email)
    {
      return this.dal.ExistsEmail(email);
    }

    public bool ExistsMobile(string mobile)
    {
      return this.dal.ExistsMobile(mobile);
    }

    public string GetRandomName(int length)
    {
      string user_name = Utils.Number(length, true);
      if (this.Exists(user_name))
        return this.GetRandomName(length);
      return user_name;
    }

    public string GetSalt(string user_name)
    {
      return this.dal.GetSalt(user_name);
    }

    public int UpdateField(int id, string strValue)
    {
      return this.dal.UpdateField(id, strValue);
    }

    public bool Upgrade(int id)
    {
      if (!this.Exists(id))
        return false;
      Rain.Model.users model = this.GetModel(id);
      Rain.Model.user_groups upgrade = new user_groups().GetUpgrade(model.group_id, model.exp);
      if (upgrade == null)
        return false;
      if (this.UpdateField(id, "group_id=" + (object) upgrade.id) > 0)
      {
        if (upgrade.point > 0)
          new user_point_log().Add(model.id, model.user_name, upgrade.point, "升级获得积分", true);
        if (upgrade.amount > new Decimal(0))
          new user_amount_log().Add(model.id, model.user_name, upgrade.amount, "升级赠送金额");
      }
      return true;
    }
  }
}
