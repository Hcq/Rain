// Decompiled with JetBrains decompiler
// Type: Rain.BLL.manager_log
// Assembly: Rain.BLL, Version=4.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 8924FCAE-37FA-4301-A188-DA020D33C8EB
// Assembly location: C:\Users\Felix.Ho\Desktop\page\bin\Rain.BLL.dll

using System.Data;
using Rain.Common;

namespace Rain.BLL
{
  public class manager_log
  {
    private readonly Rain.Model.siteconfig siteConfig = new siteconfig().loadConfig();
    private readonly Rain.DAL.manager_log dal;

    public manager_log()
    {
      this.dal = new Rain.DAL.manager_log(this.siteConfig.sysdatabaseprefix);
    }

    public bool Exists(int id)
    {
      return this.dal.Exists(id);
    }

    public int Add(int user_id, string user_name, string action_type, string remark)
    {
      return this.dal.Add(new Rain.Model.manager_log()
      {
        user_id = user_id,
        user_name = user_name,
        action_type = action_type,
        remark = remark,
        user_ip = DTRequest.GetIP()
      });
    }

    public int Add(Rain.Model.manager_log model)
    {
      return this.dal.Add(model);
    }

    public Rain.Model.manager_log GetModel(int id)
    {
      return this.dal.GetModel(id);
    }

    public Rain.Model.manager_log GetModel(
      string user_name,
      int top_num,
      string action_type)
    {
      return this.dal.GetModel(user_name, top_num, action_type);
    }

    public int Delete(int dayCount)
    {
      return this.dal.Delete(dayCount);
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
