// Decompiled with JetBrains decompiler
// Type: Rain.DAL.manager_log
// Assembly: Rain.DAL, Version=4.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 34D3CCEA-896B-46C7-93D3-7BA93E9004E2
// Assembly location: C:\Users\Felix.Ho\Desktop\page\bin\Rain.DAL.dll

using System;
using System.Data;
using System.Data.OleDb;
using System.Text;
using Rain.Common;
using Rain.DBUtility;

namespace Rain.DAL
{
  public class manager_log
  {
    private string databaseprefix;

    public manager_log(string _databaseprefix)
    {
      this.databaseprefix = _databaseprefix;
    }

    private int GetMaxId(OleDbConnection conn, OleDbTransaction trans)
    {
      string SQLString = "select top 1 id from " + this.databaseprefix + "manager_log order by id desc";
      object single = DbHelperOleDb.GetSingle(conn, trans, SQLString);
      if (single == null)
        return 0;
      return int.Parse(single.ToString());
    }

    public bool Exists(int id)
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("select count(1) from " + this.databaseprefix + nameof (manager_log));
      stringBuilder.Append(" where id=@id ");
      OleDbParameter[] oleDbParameterArray = new OleDbParameter[1]
      {
        new OleDbParameter("@id", OleDbType.Integer, 4)
      };
      oleDbParameterArray[0].Value = (object) id;
      return DbHelperOleDb.Exists(stringBuilder.ToString(), oleDbParameterArray);
    }

    public int GetCount(string strWhere)
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("select count(*) as H from " + this.databaseprefix + "manager_log ");
      if (strWhere.Trim() != "")
        stringBuilder.Append(" where " + strWhere);
      return Convert.ToInt32(DbHelperOleDb.GetSingle(stringBuilder.ToString()));
    }

    public int Add(Rain.Model.manager_log model)
    {
      int maxId;
      using (OleDbConnection oleDbConnection = new OleDbConnection(DbHelperOleDb.connectionString))
      {
        oleDbConnection.Open();
        using (OleDbTransaction trans = oleDbConnection.BeginTransaction())
        {
          try
          {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append("insert into " + this.databaseprefix + "manager_log(");
            stringBuilder.Append("user_id,user_name,action_type,[remark],user_ip,add_time)");
            stringBuilder.Append(" values (");
            stringBuilder.Append("@user_id,@user_name,@action_type,@remark,@user_ip,@add_time)");
            OleDbParameter[] oleDbParameterArray = new OleDbParameter[6]
            {
              new OleDbParameter("@user_id", OleDbType.Integer, 4),
              new OleDbParameter("@user_name", OleDbType.VarChar, 100),
              new OleDbParameter("@action_type", OleDbType.VarChar, 100),
              new OleDbParameter("@remark", OleDbType.VarChar, (int) byte.MaxValue),
              new OleDbParameter("@user_ip", OleDbType.VarChar, 30),
              new OleDbParameter("@add_time", OleDbType.Date)
            };
            oleDbParameterArray[0].Value = (object) model.user_id;
            oleDbParameterArray[1].Value = (object) model.user_name;
            oleDbParameterArray[2].Value = (object) model.action_type;
            oleDbParameterArray[3].Value = (object) model.remark;
            oleDbParameterArray[4].Value = (object) model.user_ip;
            oleDbParameterArray[5].Value = (object) model.add_time;
            DbHelperOleDb.ExecuteSql(oleDbConnection, trans, stringBuilder.ToString(), oleDbParameterArray);
            maxId = this.GetMaxId(oleDbConnection, trans);
            trans.Commit();
          }
          catch
          {
            trans.Rollback();
            return -1;
          }
        }
      }
      return maxId;
    }

    public Rain.Model.manager_log GetModel(int id)
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("select  top 1 id,user_id,user_name,action_type,[remark],user_ip,add_time");
      stringBuilder.Append(" from " + this.databaseprefix + "manager_log ");
      stringBuilder.Append(" where id=@id");
      OleDbParameter[] oleDbParameterArray = new OleDbParameter[1]
      {
        new OleDbParameter("@id", OleDbType.Integer, 4)
      };
      oleDbParameterArray[0].Value = (object) id;
      Rain.Model.manager_log managerLog = new Rain.Model.manager_log();
      DataSet dataSet = DbHelperOleDb.Query(stringBuilder.ToString(), oleDbParameterArray);
      if (dataSet.Tables[0].Rows.Count > 0)
        return this.DataRowToModel(dataSet.Tables[0].Rows[0]);
      return (Rain.Model.manager_log) null;
    }

    public Rain.Model.manager_log DataRowToModel(DataRow row)
    {
      Rain.Model.manager_log managerLog = new Rain.Model.manager_log();
      if (row != null)
      {
        if (row["id"] != null && row["id"].ToString() != "")
          managerLog.id = int.Parse(row["id"].ToString());
        if (row["user_id"] != null && row["user_id"].ToString() != "")
          managerLog.user_id = int.Parse(row["user_id"].ToString());
        if (row["user_name"] != null)
          managerLog.user_name = row["user_name"].ToString();
        if (row["action_type"] != null)
          managerLog.action_type = row["action_type"].ToString();
        if (row["remark"] != null)
          managerLog.remark = row["remark"].ToString();
        if (row["user_ip"] != null)
          managerLog.user_ip = row["user_ip"].ToString();
        if (row["add_time"] != null && row["add_time"].ToString() != "")
          managerLog.add_time = DateTime.Parse(row["add_time"].ToString());
      }
      return managerLog;
    }

    public Rain.Model.manager_log GetModel(
      string user_name,
      int top_num,
      string action_type)
    {
      int num = this.GetCount("user_name='" + user_name + "'");
      if (top_num == 1)
        num = 2;
      if (num > 1)
      {
        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.Append("select top 1 id from (select top " + (object) top_num + " id from " + this.databaseprefix + nameof (manager_log));
        stringBuilder.Append(" where user_name=@user_name and action_type=@action_type order by id desc) as T ");
        stringBuilder.Append(" order by id asc");
        OleDbParameter[] oleDbParameterArray = new OleDbParameter[2]
        {
          new OleDbParameter("@user_name", OleDbType.VarChar, 100),
          new OleDbParameter("@action_type", OleDbType.VarChar, 100)
        };
        oleDbParameterArray[0].Value = (object) user_name;
        oleDbParameterArray[1].Value = (object) action_type;
        object single = DbHelperOleDb.GetSingle(stringBuilder.ToString(), oleDbParameterArray);
        if (single != null)
          return this.GetModel(Convert.ToInt32(single));
      }
      return (Rain.Model.manager_log) null;
    }

    public int Delete(int dayCount)
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("delete from " + this.databaseprefix + "manager_log ");
      stringBuilder.Append(" where DATEDIFF('d', add_time, date()) > " + (object) dayCount);
      return DbHelperOleDb.ExecuteSql(stringBuilder.ToString());
    }

    public DataSet GetList(int Top, string strWhere, string filedOrder)
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("select ");
      if (Top > 0)
        stringBuilder.Append(" top " + Top.ToString());
      stringBuilder.Append(" id,user_id,user_name,action_type,[remark],user_ip,add_time ");
      stringBuilder.Append(" FROM " + this.databaseprefix + "manager_log ");
      if (strWhere.Trim() != "")
        stringBuilder.Append(" where " + strWhere);
      stringBuilder.Append(" order by " + filedOrder);
      return DbHelperOleDb.Query(stringBuilder.ToString());
    }

    public DataSet GetList(
      int pageSize,
      int pageIndex,
      string strWhere,
      string filedOrder,
      out int recordCount)
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("select * FROM " + this.databaseprefix + nameof (manager_log));
      if (strWhere.Trim() != "")
        stringBuilder.Append(" where " + strWhere);
      recordCount = Convert.ToInt32(DbHelperOleDb.GetSingle(PagingHelper.CreateCountingSql(stringBuilder.ToString())));
      return DbHelperOleDb.Query(PagingHelper.CreatePagingSql(recordCount, pageSize, pageIndex, stringBuilder.ToString(), filedOrder));
    }
  }
}
