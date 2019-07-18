// Decompiled with JetBrains decompiler
// Type: Rain.DAL.user_attach_log
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
  public class user_attach_log
  {
    private string databaseprefix;

    public user_attach_log(string _databaseprefix)
    {
      this.databaseprefix = _databaseprefix;
    }

    private int GetMaxId(OleDbConnection conn, OleDbTransaction trans)
    {
      string SQLString = "select top 1 id from " + this.databaseprefix + "user_attach_log order by id desc";
      object single = DbHelperOleDb.GetSingle(conn, trans, SQLString);
      if (single == null)
        return 0;
      return int.Parse(single.ToString());
    }

    public bool Exists(int id)
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("select count(1) from " + this.databaseprefix + nameof (user_attach_log));
      stringBuilder.Append(" where id=@id");
      OleDbParameter[] oleDbParameterArray = new OleDbParameter[1]
      {
        new OleDbParameter("@id", OleDbType.Integer, 4)
      };
      oleDbParameterArray[0].Value = (object) id;
      return DbHelperOleDb.Exists(stringBuilder.ToString(), oleDbParameterArray);
    }

    public int Add(Rain.Model.user_attach_log model)
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
            stringBuilder.Append("insert into " + this.databaseprefix + "user_attach_log(");
            stringBuilder.Append("user_id,user_name,attach_id,file_name,add_time)");
            stringBuilder.Append(" values (");
            stringBuilder.Append("@user_id,@user_name,@attach_id,@file_name,@add_time)");
            OleDbParameter[] oleDbParameterArray = new OleDbParameter[5]
            {
              new OleDbParameter("@user_id", OleDbType.Integer, 4),
              new OleDbParameter("@user_name", OleDbType.VarChar, 100),
              new OleDbParameter("@attach_id", OleDbType.Integer, 4),
              new OleDbParameter("@file_name", OleDbType.VarChar, (int) byte.MaxValue),
              new OleDbParameter("@add_time", OleDbType.Date)
            };
            oleDbParameterArray[0].Value = (object) model.user_id;
            oleDbParameterArray[1].Value = (object) model.user_name;
            oleDbParameterArray[2].Value = (object) model.attach_id;
            oleDbParameterArray[3].Value = (object) model.file_name;
            oleDbParameterArray[4].Value = (object) model.add_time;
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

    public bool Update(Rain.Model.user_attach_log model)
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("update " + this.databaseprefix + "user_attach_log set ");
      stringBuilder.Append("user_id=@user_id,");
      stringBuilder.Append("user_name=@user_name,");
      stringBuilder.Append("attach_id=@attach_id,");
      stringBuilder.Append("file_name=@file_name,");
      stringBuilder.Append("add_time=@add_time");
      stringBuilder.Append(" where id=@id");
      OleDbParameter[] oleDbParameterArray = new OleDbParameter[6]
      {
        new OleDbParameter("@user_id", OleDbType.Integer, 4),
        new OleDbParameter("@user_name", OleDbType.VarChar, 100),
        new OleDbParameter("@attach_id", OleDbType.Integer, 4),
        new OleDbParameter("@file_name", OleDbType.VarChar, (int) byte.MaxValue),
        new OleDbParameter("@add_time", OleDbType.Date),
        new OleDbParameter("@id", OleDbType.Integer, 4)
      };
      oleDbParameterArray[0].Value = (object) model.user_id;
      oleDbParameterArray[1].Value = (object) model.user_name;
      oleDbParameterArray[2].Value = (object) model.attach_id;
      oleDbParameterArray[3].Value = (object) model.file_name;
      oleDbParameterArray[4].Value = (object) model.add_time;
      oleDbParameterArray[5].Value = (object) model.id;
      return DbHelperOleDb.ExecuteSql(stringBuilder.ToString(), oleDbParameterArray) > 0;
    }

    public bool Delete(int id)
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("delete from " + this.databaseprefix + "user_attach_log ");
      stringBuilder.Append(" where id=@id");
      OleDbParameter[] oleDbParameterArray = new OleDbParameter[1]
      {
        new OleDbParameter("@id", OleDbType.Integer, 4)
      };
      oleDbParameterArray[0].Value = (object) id;
      return DbHelperOleDb.ExecuteSql(stringBuilder.ToString(), oleDbParameterArray) > 0;
    }

    public Rain.Model.user_attach_log GetModel(int id)
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("select top 1 id,user_id,user_name,attach_id,file_name,add_time from " + this.databaseprefix + "user_attach_log ");
      stringBuilder.Append(" where id=@id");
      OleDbParameter[] oleDbParameterArray = new OleDbParameter[1]
      {
        new OleDbParameter("@id", OleDbType.Integer, 4)
      };
      oleDbParameterArray[0].Value = (object) id;
      DataSet dataSet = DbHelperOleDb.Query(stringBuilder.ToString(), oleDbParameterArray);
      if (dataSet.Tables[0].Rows.Count > 0)
        return this.DataRowToModel(dataSet.Tables[0].Rows[0]);
      return (Rain.Model.user_attach_log) null;
    }

    public DataSet GetList(string strWhere)
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("select id,user_id,user_name,attach_id,file_name,add_time ");
      stringBuilder.Append(" FROM " + this.databaseprefix + "user_attach_log ");
      if (strWhere.Trim() != "")
        stringBuilder.Append(" where " + strWhere);
      return DbHelperOleDb.Query(stringBuilder.ToString());
    }

    public DataSet GetList(int Top, string strWhere, string filedOrder)
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("select ");
      if (Top > 0)
        stringBuilder.Append(" top " + Top.ToString());
      stringBuilder.Append(" id,user_id,user_name,attach_id,file_name,add_time ");
      stringBuilder.Append(" FROM " + this.databaseprefix + "user_attach_log ");
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
      stringBuilder.Append("select * FROM " + this.databaseprefix + nameof (user_attach_log));
      if (strWhere.Trim() != "")
        stringBuilder.Append(" where " + strWhere);
      recordCount = Convert.ToInt32(DbHelperOleDb.GetSingle(PagingHelper.CreateCountingSql(stringBuilder.ToString())));
      return DbHelperOleDb.Query(PagingHelper.CreatePagingSql(recordCount, pageSize, pageIndex, stringBuilder.ToString(), filedOrder));
    }

    public Rain.Model.user_attach_log DataRowToModel(DataRow row)
    {
      Rain.Model.user_attach_log userAttachLog = new Rain.Model.user_attach_log();
      if (row != null)
      {
        if (row["id"] != null && row["id"].ToString() != "")
          userAttachLog.id = int.Parse(row["id"].ToString());
        if (row["user_id"] != null && row["user_id"].ToString() != "")
          userAttachLog.user_id = int.Parse(row["user_id"].ToString());
        if (row["user_name"] != null)
          userAttachLog.user_name = row["user_name"].ToString();
        if (row["attach_id"] != null && row["attach_id"].ToString() != "")
          userAttachLog.attach_id = int.Parse(row["attach_id"].ToString());
        if (row["file_name"] != null)
          userAttachLog.file_name = row["file_name"].ToString();
        if (row["add_time"] != null && row["add_time"].ToString() != "")
          userAttachLog.add_time = DateTime.Parse(row["add_time"].ToString());
      }
      return userAttachLog;
    }
  }
}
