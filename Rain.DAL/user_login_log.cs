// Decompiled with JetBrains decompiler
// Type: Rain.DAL.user_login_log
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
  public class user_login_log
  {
    private string databaseprefix;

    public user_login_log(string _databaseprefix)
    {
      this.databaseprefix = _databaseprefix;
    }

    private int GetMaxId(OleDbConnection conn, OleDbTransaction trans)
    {
      string SQLString = "select top 1 id from " + this.databaseprefix + "user_login_log order by id desc";
      object single = DbHelperOleDb.GetSingle(conn, trans, SQLString);
      if (single == null)
        return 0;
      return int.Parse(single.ToString());
    }

    public bool Exists(int id)
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("select count(1) from " + this.databaseprefix + nameof (user_login_log));
      stringBuilder.Append(" where id=@id");
      OleDbParameter[] oleDbParameterArray = new OleDbParameter[1]
      {
        new OleDbParameter("@id", OleDbType.Integer, 4)
      };
      oleDbParameterArray[0].Value = (object) id;
      return DbHelperOleDb.Exists(stringBuilder.ToString(), oleDbParameterArray);
    }

    public int Add(Rain.Model.user_login_log model)
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
            stringBuilder.Append("insert into " + this.databaseprefix + "user_login_log(");
            stringBuilder.Append("user_id,user_name,[remark],login_time,login_ip)");
            stringBuilder.Append(" values (");
            stringBuilder.Append("@user_id,@user_name,@remark,@login_time,@login_ip)");
            stringBuilder.Append(";select @@IDENTITY");
            OleDbParameter[] oleDbParameterArray = new OleDbParameter[5]
            {
              new OleDbParameter("@user_id", OleDbType.Integer, 4),
              new OleDbParameter("@user_name", OleDbType.VarChar, 100),
              new OleDbParameter("@remark", OleDbType.VarChar, (int) byte.MaxValue),
              new OleDbParameter("@login_time", OleDbType.Date),
              new OleDbParameter("@login_ip", OleDbType.VarChar, 50)
            };
            oleDbParameterArray[0].Value = (object) model.user_id;
            oleDbParameterArray[1].Value = (object) model.user_name;
            oleDbParameterArray[2].Value = (object) model.remark;
            oleDbParameterArray[3].Value = (object) model.login_time;
            oleDbParameterArray[4].Value = (object) model.login_ip;
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

    public bool Delete(int id)
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("delete from " + this.databaseprefix + "user_login_log ");
      stringBuilder.Append(" where id=@id");
      OleDbParameter[] oleDbParameterArray = new OleDbParameter[1]
      {
        new OleDbParameter("@id", OleDbType.Integer, 4)
      };
      oleDbParameterArray[0].Value = (object) id;
      return DbHelperOleDb.ExecuteSql(stringBuilder.ToString(), oleDbParameterArray) > 0;
    }

    public Rain.Model.user_login_log GetModel(int id)
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("select top 1 id,user_id,user_name,[remark],login_time,login_ip from " + this.databaseprefix + "user_login_log ");
      stringBuilder.Append(" where id=@id");
      OleDbParameter[] oleDbParameterArray = new OleDbParameter[1]
      {
        new OleDbParameter("@id", OleDbType.Integer, 4)
      };
      oleDbParameterArray[0].Value = (object) id;
      DataSet dataSet = DbHelperOleDb.Query(stringBuilder.ToString(), oleDbParameterArray);
      if (dataSet.Tables[0].Rows.Count > 0)
        return this.DataRowToModel(dataSet.Tables[0].Rows[0]);
      return (Rain.Model.user_login_log) null;
    }

    public DataSet GetList(int Top, string strWhere, string filedOrder)
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("select ");
      if (Top > 0)
        stringBuilder.Append(" top " + Top.ToString());
      stringBuilder.Append(" id,user_id,user_name,[remark],login_time,login_ip ");
      stringBuilder.Append(" FROM " + this.databaseprefix + "user_login_log ");
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
      stringBuilder.Append("select * FROM " + this.databaseprefix + nameof (user_login_log));
      if (strWhere.Trim() != "")
        stringBuilder.Append(" where " + strWhere);
      recordCount = Convert.ToInt32(DbHelperOleDb.GetSingle(PagingHelper.CreateCountingSql(stringBuilder.ToString())));
      return DbHelperOleDb.Query(PagingHelper.CreatePagingSql(recordCount, pageSize, pageIndex, stringBuilder.ToString(), filedOrder));
    }

    public bool ExistsDay(string username)
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("select count(1) from " + this.databaseprefix + nameof (user_login_log));
      stringBuilder.Append(" where user_name=@user_name and DATEDIFF('d',login_time,date())=0");
      OleDbParameter[] oleDbParameterArray = new OleDbParameter[1]
      {
        new OleDbParameter("@user_name", OleDbType.VarChar, 100)
      };
      oleDbParameterArray[0].Value = (object) username;
      return DbHelperOleDb.Exists(stringBuilder.ToString(), oleDbParameterArray);
    }

    public Rain.Model.user_login_log GetLastModel(string user_name)
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("select top 1 id,user_id,user_name,[remark],login_time,login_ip from " + this.databaseprefix + nameof (user_login_log));
      stringBuilder.Append(" where user_name=@user_name order by id desc");
      OleDbParameter[] oleDbParameterArray = new OleDbParameter[1]
      {
        new OleDbParameter("@user_name", OleDbType.VarChar, 100)
      };
      oleDbParameterArray[0].Value = (object) user_name;
      DataSet dataSet = DbHelperOleDb.Query(stringBuilder.ToString(), oleDbParameterArray);
      if (dataSet.Tables[0].Rows.Count > 0)
        return this.DataRowToModel(dataSet.Tables[0].Rows[0]);
      return (Rain.Model.user_login_log) null;
    }

    public Rain.Model.user_login_log DataRowToModel(DataRow row)
    {
      Rain.Model.user_login_log userLoginLog = new Rain.Model.user_login_log();
      if (row != null)
      {
        if (row["id"] != null && row["id"].ToString() != "")
          userLoginLog.id = int.Parse(row["id"].ToString());
        if (row["user_id"] != null && row["user_id"].ToString() != "")
          userLoginLog.user_id = int.Parse(row["user_id"].ToString());
        if (row["user_name"] != null)
          userLoginLog.user_name = row["user_name"].ToString();
        if (row["remark"] != null)
          userLoginLog.remark = row["remark"].ToString();
        if (row["login_time"] != null && row["login_time"].ToString() != "")
          userLoginLog.login_time = DateTime.Parse(row["login_time"].ToString());
        if (row["login_ip"] != null)
          userLoginLog.login_ip = row["login_ip"].ToString();
      }
      return userLoginLog;
    }
  }
}
