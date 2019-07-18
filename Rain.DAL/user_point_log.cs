// Decompiled with JetBrains decompiler
// Type: Rain.DAL.user_point_log
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
  public class user_point_log
  {
    private string databaseprefix;

    public user_point_log(string _databaseprefix)
    {
      this.databaseprefix = _databaseprefix;
    }

    private int GetMaxId(OleDbConnection conn, OleDbTransaction trans)
    {
      string SQLString = "select top 1 id from " + this.databaseprefix + "user_point_log order by id desc";
      object single = DbHelperOleDb.GetSingle(conn, trans, SQLString);
      if (single == null)
        return 0;
      return int.Parse(single.ToString());
    }

    public bool Exists(int id)
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("select count(1) from " + this.databaseprefix + nameof (user_point_log));
      stringBuilder.Append(" where id=@id");
      OleDbParameter[] oleDbParameterArray = new OleDbParameter[1]
      {
        new OleDbParameter("@id", OleDbType.Integer, 4)
      };
      oleDbParameterArray[0].Value = (object) id;
      return DbHelperOleDb.Exists(stringBuilder.ToString(), oleDbParameterArray);
    }

    public int Add(Rain.Model.user_point_log model, bool is_upgrade)
    {
      using (OleDbConnection oleDbConnection = new OleDbConnection(DbHelperOleDb.connectionString))
      {
        oleDbConnection.Open();
        using (OleDbTransaction trans = oleDbConnection.BeginTransaction())
        {
          try
          {
            StringBuilder stringBuilder1 = new StringBuilder();
            stringBuilder1.Append("insert into " + this.databaseprefix + "user_point_log(");
            stringBuilder1.Append("user_id,user_name,[value],[remark],add_time)");
            stringBuilder1.Append(" values (");
            stringBuilder1.Append("@user_id,@user_name,@value,@remark,@add_time)");
            OleDbParameter[] oleDbParameterArray1 = new OleDbParameter[5]
            {
              new OleDbParameter("@user_id", OleDbType.Integer, 4),
              new OleDbParameter("@user_name", OleDbType.VarChar, 100),
              new OleDbParameter("@value", OleDbType.Integer, 4),
              new OleDbParameter("@remark", OleDbType.VarChar, 500),
              new OleDbParameter("@add_time", OleDbType.Date)
            };
            oleDbParameterArray1[0].Value = (object) model.user_id;
            oleDbParameterArray1[1].Value = (object) model.user_name;
            oleDbParameterArray1[2].Value = (object) model.value;
            oleDbParameterArray1[3].Value = (object) model.remark;
            oleDbParameterArray1[4].Value = (object) model.add_time;
            DbHelperOleDb.ExecuteSql(oleDbConnection, trans, stringBuilder1.ToString(), oleDbParameterArray1);
            model.id = this.GetMaxId(oleDbConnection, trans);
            StringBuilder stringBuilder2 = new StringBuilder();
            stringBuilder2.Append("update " + this.databaseprefix + "users set point=point+" + (object) model.value);
            if (model.value > 0 && is_upgrade)
              stringBuilder2.Append(",exp=exp+" + (object) model.value);
            stringBuilder2.Append(" where id=@id");
            OleDbParameter[] oleDbParameterArray2 = new OleDbParameter[1]
            {
              new OleDbParameter("@id", OleDbType.Integer, 4)
            };
            oleDbParameterArray2[0].Value = (object) model.user_id;
            DbHelperOleDb.ExecuteSql(oleDbConnection, trans, stringBuilder2.ToString(), oleDbParameterArray2);
            trans.Commit();
          }
          catch
          {
            trans.Rollback();
            return -1;
          }
        }
      }
      return model.id;
    }

    public bool Delete(int id)
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("delete from " + this.databaseprefix + "user_point_log ");
      stringBuilder.Append(" where id=@id");
      OleDbParameter[] oleDbParameterArray = new OleDbParameter[1]
      {
        new OleDbParameter("@id", OleDbType.Integer, 4)
      };
      oleDbParameterArray[0].Value = (object) id;
      return DbHelperOleDb.ExecuteSql(stringBuilder.ToString(), oleDbParameterArray) > 0;
    }

    public bool Delete(int id, string user_name)
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("delete from " + this.databaseprefix + "user_point_log ");
      stringBuilder.Append(" where id=@id and user_name=@user_name");
      OleDbParameter[] oleDbParameterArray = new OleDbParameter[2]
      {
        new OleDbParameter("@id", OleDbType.Integer, 4),
        new OleDbParameter("@user_name", OleDbType.VarChar, 100)
      };
      oleDbParameterArray[0].Value = (object) id;
      oleDbParameterArray[1].Value = (object) user_name;
      return DbHelperOleDb.ExecuteSql(stringBuilder.ToString(), oleDbParameterArray) > 0;
    }

    public Rain.Model.user_point_log GetModel(int id)
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("select  top 1 id,user_id,user_name,[value],[remark],add_time");
      stringBuilder.Append(" from " + this.databaseprefix + "user_point_log ");
      stringBuilder.Append(" where id=@id");
      OleDbParameter[] oleDbParameterArray = new OleDbParameter[1]
      {
        new OleDbParameter("@id", OleDbType.Integer, 4)
      };
      oleDbParameterArray[0].Value = (object) id;
      DataSet dataSet = DbHelperOleDb.Query(stringBuilder.ToString(), oleDbParameterArray);
      if (dataSet.Tables[0].Rows.Count > 0)
        return this.DataRowToModel(dataSet.Tables[0].Rows[0]);
      return (Rain.Model.user_point_log) null;
    }

    public DataSet GetList(int Top, string strWhere, string filedOrder)
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("select ");
      if (Top > 0)
        stringBuilder.Append(" top " + Top.ToString());
      stringBuilder.Append(" id,user_id,user_name,[value],[remark],add_time ");
      stringBuilder.Append(" FROM " + this.databaseprefix + "user_point_log ");
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
      stringBuilder.Append("select * FROM " + this.databaseprefix + nameof (user_point_log));
      if (strWhere.Trim() != "")
        stringBuilder.Append(" where " + strWhere);
      recordCount = Convert.ToInt32(DbHelperOleDb.GetSingle(PagingHelper.CreateCountingSql(stringBuilder.ToString())));
      return DbHelperOleDb.Query(PagingHelper.CreatePagingSql(recordCount, pageSize, pageIndex, stringBuilder.ToString(), filedOrder));
    }

    public Rain.Model.user_point_log DataRowToModel(DataRow row)
    {
      Rain.Model.user_point_log userPointLog = new Rain.Model.user_point_log();
      if (row != null)
      {
        if (row["id"] != null && row["id"].ToString() != "")
          userPointLog.id = int.Parse(row["id"].ToString());
        if (row["user_id"] != null && row["user_id"].ToString() != "")
          userPointLog.user_id = int.Parse(row["user_id"].ToString());
        if (row["user_name"] != null)
          userPointLog.user_name = row["user_name"].ToString();
        if (row["value"] != null && row["value"].ToString() != "")
          userPointLog.value = int.Parse(row["value"].ToString());
        if (row["remark"] != null)
          userPointLog.remark = row["remark"].ToString();
        if (row["add_time"] != null && row["add_time"].ToString() != "")
          userPointLog.add_time = DateTime.Parse(row["add_time"].ToString());
      }
      return userPointLog;
    }
  }
}
