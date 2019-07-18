// Decompiled with JetBrains decompiler
// Type: Rain.DAL.user_code
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
  public class user_code
  {
    private string databaseprefix;

    public user_code(string _databaseprefix)
    {
      this.databaseprefix = _databaseprefix;
    }

    private int GetMaxId(OleDbConnection conn, OleDbTransaction trans)
    {
      string SQLString = "select top 1 id from " + this.databaseprefix + "user_code order by id desc";
      object single = DbHelperOleDb.GetSingle(conn, trans, SQLString);
      if (single == null)
        return 0;
      return int.Parse(single.ToString());
    }

    public bool Exists(int id)
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("select count(1) from " + this.databaseprefix + nameof (user_code));
      stringBuilder.Append(" where id=@id");
      OleDbParameter[] oleDbParameterArray = new OleDbParameter[1]
      {
        new OleDbParameter("@id", OleDbType.Integer, 4)
      };
      oleDbParameterArray[0].Value = (object) id;
      return DbHelperOleDb.Exists(stringBuilder.ToString(), oleDbParameterArray);
    }

    public bool Exists(string type, string user_name)
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("select count(1) from " + this.databaseprefix + nameof (user_code));
      stringBuilder.Append(" where status=0 and datediff('d',eff_time,now())<=0 and [type]=@type and user_name=@user_name");
      OleDbParameter[] oleDbParameterArray = new OleDbParameter[2]
      {
        new OleDbParameter("@type", OleDbType.VarChar, 20),
        new OleDbParameter("@user_name", OleDbType.VarChar, 100)
      };
      oleDbParameterArray[0].Value = (object) type;
      oleDbParameterArray[1].Value = (object) user_name;
      return DbHelperOleDb.Exists(stringBuilder.ToString(), oleDbParameterArray);
    }

    public int Add(Rain.Model.user_code model)
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
            stringBuilder.Append("insert into " + this.databaseprefix + "user_code(");
            stringBuilder.Append("user_id,user_name,[type],str_code,[count],status,user_ip,eff_time,add_time)");
            stringBuilder.Append(" values (");
            stringBuilder.Append("@user_id,@user_name,@type,@str_code,@count,@status,@user_ip,@eff_time,@add_time)");
            OleDbParameter[] oleDbParameterArray = new OleDbParameter[9]
            {
              new OleDbParameter("@user_id", OleDbType.Integer, 4),
              new OleDbParameter("@user_name", OleDbType.VarChar, 100),
              new OleDbParameter("@type", OleDbType.VarChar, 20),
              new OleDbParameter("@str_code", OleDbType.VarChar, (int) byte.MaxValue),
              new OleDbParameter("@count", OleDbType.Integer, 4),
              new OleDbParameter("@status", OleDbType.Integer, 4),
              new OleDbParameter("@user_ip", OleDbType.VarChar, 20),
              new OleDbParameter("@eff_time", OleDbType.Date),
              new OleDbParameter("@add_time", OleDbType.Date)
            };
            oleDbParameterArray[0].Value = (object) model.user_id;
            oleDbParameterArray[1].Value = (object) model.user_name;
            oleDbParameterArray[2].Value = (object) model.type;
            oleDbParameterArray[3].Value = (object) model.str_code;
            oleDbParameterArray[4].Value = (object) model.count;
            oleDbParameterArray[5].Value = (object) model.status;
            oleDbParameterArray[6].Value = (object) model.user_ip;
            oleDbParameterArray[7].Value = (object) model.eff_time;
            oleDbParameterArray[8].Value = (object) model.add_time;
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

    public bool Update(Rain.Model.user_code model)
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("update " + this.databaseprefix + "user_code set ");
      stringBuilder.Append("user_id=@user_id,");
      stringBuilder.Append("user_name=@user_name,");
      stringBuilder.Append("[type]=@type,");
      stringBuilder.Append("str_code=@str_code,");
      stringBuilder.Append("[count]=@count,");
      stringBuilder.Append("status=@status,");
      stringBuilder.Append("user_ip=@user_ip,");
      stringBuilder.Append("eff_time=@eff_time,");
      stringBuilder.Append("add_time=@add_time");
      stringBuilder.Append(" where id=@id");
      OleDbParameter[] oleDbParameterArray = new OleDbParameter[10]
      {
        new OleDbParameter("@user_id", OleDbType.Integer, 4),
        new OleDbParameter("@user_name", OleDbType.VarChar, 100),
        new OleDbParameter("@type", OleDbType.VarChar, 20),
        new OleDbParameter("@str_code", OleDbType.VarChar, (int) byte.MaxValue),
        new OleDbParameter("@count", OleDbType.Integer, 4),
        new OleDbParameter("@status", OleDbType.Integer, 4),
        new OleDbParameter("@user_ip", OleDbType.VarChar, 20),
        new OleDbParameter("@eff_time", OleDbType.Date),
        new OleDbParameter("@add_time", OleDbType.Date),
        new OleDbParameter("@id", OleDbType.Integer, 4)
      };
      oleDbParameterArray[0].Value = (object) model.user_id;
      oleDbParameterArray[1].Value = (object) model.user_name;
      oleDbParameterArray[2].Value = (object) model.type;
      oleDbParameterArray[3].Value = (object) model.str_code;
      oleDbParameterArray[4].Value = (object) model.count;
      oleDbParameterArray[5].Value = (object) model.status;
      oleDbParameterArray[6].Value = (object) model.user_ip;
      oleDbParameterArray[7].Value = (object) model.eff_time;
      oleDbParameterArray[8].Value = (object) model.add_time;
      oleDbParameterArray[9].Value = (object) model.id;
      return DbHelperOleDb.ExecuteSql(stringBuilder.ToString(), oleDbParameterArray) > 0;
    }

    public bool Delete(int id)
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("delete from " + this.databaseprefix + "user_code ");
      stringBuilder.Append(" where id=@id");
      OleDbParameter[] oleDbParameterArray = new OleDbParameter[1]
      {
        new OleDbParameter("@id", OleDbType.Integer, 4)
      };
      oleDbParameterArray[0].Value = (object) id;
      return DbHelperOleDb.ExecuteSql(stringBuilder.ToString(), oleDbParameterArray) > 0;
    }

    public bool Delete(string strWhere)
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("delete from " + this.databaseprefix + "user_code ");
      stringBuilder.Append(" where " + strWhere);
      return DbHelperOleDb.ExecuteSql(stringBuilder.ToString()) > 0;
    }

    public Rain.Model.user_code GetModel(int id)
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("select top 1 id,user_id,user_name,[type],str_code,[count],status,user_ip,eff_time,add_time");
      stringBuilder.Append(" from " + this.databaseprefix + "user_code ");
      stringBuilder.Append(" where id=@id");
      OleDbParameter[] oleDbParameterArray = new OleDbParameter[1]
      {
        new OleDbParameter("@id", OleDbType.Integer, 4)
      };
      oleDbParameterArray[0].Value = (object) id;
      DataSet dataSet = DbHelperOleDb.Query(stringBuilder.ToString(), oleDbParameterArray);
      if (dataSet.Tables[0].Rows.Count > 0)
        return this.DataRowToModel(dataSet.Tables[0].Rows[0]);
      return (Rain.Model.user_code) null;
    }

    public Rain.Model.user_code GetModel(string str_code)
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("select top 1 id,user_id,user_name,[type],str_code,[count],status,user_ip,eff_time,add_time");
      stringBuilder.Append(" from " + this.databaseprefix + "user_code ");
      stringBuilder.Append(" where status=0 and datediff('d',eff_time,now())<=0 and str_code=@str_code");
      OleDbParameter[] oleDbParameterArray = new OleDbParameter[1]
      {
        new OleDbParameter("@str_code", OleDbType.VarChar, (int) byte.MaxValue)
      };
      oleDbParameterArray[0].Value = (object) str_code;
      DataSet dataSet = DbHelperOleDb.Query(stringBuilder.ToString(), oleDbParameterArray);
      if (dataSet.Tables[0].Rows.Count > 0)
        return this.DataRowToModel(dataSet.Tables[0].Rows[0]);
      return (Rain.Model.user_code) null;
    }

    public Rain.Model.user_code GetModel(
      string user_name,
      string code_type,
      string datepart)
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("select top 1 id,user_id,user_name,[type],str_code,[count],status,user_ip,eff_time,add_time");
      stringBuilder.Append(" from " + this.databaseprefix + "user_code ");
      stringBuilder.Append(" where status=0 and datediff('" + datepart + "',eff_time,now())<=0 and user_name=@user_name and type=@type");
      OleDbParameter[] oleDbParameterArray = new OleDbParameter[2]
      {
        new OleDbParameter("@user_name", OleDbType.VarChar, 100),
        new OleDbParameter("@type", OleDbType.VarChar, 20)
      };
      oleDbParameterArray[0].Value = (object) user_name;
      oleDbParameterArray[1].Value = (object) code_type;
      DataSet dataSet = DbHelperOleDb.Query(stringBuilder.ToString(), oleDbParameterArray);
      if (dataSet.Tables[0].Rows.Count > 0)
        return this.DataRowToModel(dataSet.Tables[0].Rows[0]);
      return (Rain.Model.user_code) null;
    }

    public DataSet GetList(int Top, string strWhere, string filedOrder)
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("select ");
      if (Top > 0)
        stringBuilder.Append(" top " + Top.ToString());
      stringBuilder.Append(" id,user_id,user_name,[type],str_code,[count],status,user_ip,eff_time,add_time ");
      stringBuilder.Append(" FROM " + this.databaseprefix + "user_code ");
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
      stringBuilder.Append("select * FROM " + this.databaseprefix + nameof (user_code));
      if (strWhere.Trim() != "")
        stringBuilder.Append(" where " + strWhere);
      recordCount = Convert.ToInt32(DbHelperOleDb.GetSingle(PagingHelper.CreateCountingSql(stringBuilder.ToString())));
      return DbHelperOleDb.Query(PagingHelper.CreatePagingSql(recordCount, pageSize, pageIndex, stringBuilder.ToString(), filedOrder));
    }

    public int GetCount(string strWhere)
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("select count(*) as H ");
      stringBuilder.Append(" from " + this.databaseprefix + "user_code ");
      if (strWhere.Trim() != "")
        stringBuilder.Append(" where " + strWhere);
      return Convert.ToInt32(DbHelperOleDb.GetSingle(stringBuilder.ToString()));
    }

    public Rain.Model.user_code DataRowToModel(DataRow row)
    {
      Rain.Model.user_code userCode = new Rain.Model.user_code();
      if (row != null)
      {
        if (row["id"] != null && row["id"].ToString() != "")
          userCode.id = int.Parse(row["id"].ToString());
        if (row["user_id"] != null && row["user_id"].ToString() != "")
          userCode.user_id = int.Parse(row["user_id"].ToString());
        if (row["user_name"] != null)
          userCode.user_name = row["user_name"].ToString();
        if (row["type"] != null)
          userCode.type = row["type"].ToString();
        if (row["str_code"] != null)
          userCode.str_code = row["str_code"].ToString();
        if (row["count"] != null && row["count"].ToString() != "")
          userCode.count = int.Parse(row["count"].ToString());
        if (row["status"] != null && row["status"].ToString() != "")
          userCode.status = int.Parse(row["status"].ToString());
        if (row["user_ip"] != null)
          userCode.user_ip = row["user_ip"].ToString();
        if (row["eff_time"] != null && row["eff_time"].ToString() != "")
          userCode.eff_time = DateTime.Parse(row["eff_time"].ToString());
        if (row["add_time"] != null && row["add_time"].ToString() != "")
          userCode.add_time = DateTime.Parse(row["add_time"].ToString());
      }
      return userCode;
    }
  }
}
