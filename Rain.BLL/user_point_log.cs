// Decompiled with JetBrains decompiler
// Type: Rain.BLL.user_point_log
// Assembly: Rain.BLL, Version=4.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 8924FCAE-37FA-4301-A188-DA020D33C8EB
// Assembly location: C:\Users\Felix.Ho\Desktop\page\bin\Rain.BLL.dll

using System.Data;

namespace Rain.BLL
{
  public class user_point_log
  {
    private readonly Rain.Model.siteconfig siteConfig = new siteconfig().loadConfig();
    private readonly Rain.DAL.user_point_log dal;

    public user_point_log()
    {
      this.dal = new Rain.DAL.user_point_log(this.siteConfig.sysdatabaseprefix);
    }

    public bool Exists(int id)
    {
      return this.dal.Exists(id);
    }

    public int Add(int user_id, string user_name, int value, string remark, bool is_upgrade)
    {
      int num = this.dal.Add(new Rain.Model.user_point_log()
      {
        user_id = user_id,
        user_name = user_name,
        value = value,
        remark = remark
      }, is_upgrade);
      if (is_upgrade && value > 0 && num > 0)
        new users().Upgrade(user_id);
      return num;
    }

    public Rain.Model.user_point_log GetModel(int id)
    {
      return this.dal.GetModel(id);
    }

    public bool Delete(int id)
    {
      return this.dal.Delete(id);
    }

    public bool Delete(int id, string user_name)
    {
      return this.dal.Delete(id, user_name);
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
