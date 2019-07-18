// Decompiled with JetBrains decompiler
// Type: Rain.DAL.user_recharge
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
  public class user_recharge
  {
    private string databaseprefix;

    public user_recharge(string _databaseprefix)
    {
      this.databaseprefix = _databaseprefix;
    }

    private int GetMaxId(OleDbConnection conn, OleDbTransaction trans)
    {
      string SQLString = "select top 1 id from " + this.databaseprefix + "user_recharge order by id desc";
      object single = DbHelperOleDb.GetSingle(conn, trans, SQLString);
      if (single == null)
        return 0;
      return int.Parse(single.ToString());
    }

    public bool Exists(int id)
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("select count(1) from " + this.databaseprefix + nameof (user_recharge));
      stringBuilder.Append(" where id=@id");
      OleDbParameter[] oleDbParameterArray = new OleDbParameter[1]
      {
        new OleDbParameter("@id", OleDbType.Integer, 4)
      };
      oleDbParameterArray[0].Value = (object) id;
      return DbHelperOleDb.Exists(stringBuilder.ToString(), oleDbParameterArray);
    }

    public int Add(Rain.Model.user_recharge model)
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
            stringBuilder.Append("insert into " + this.databaseprefix + "user_recharge(");
            stringBuilder.Append("user_id,user_name,recharge_no,payment_id,amount,status,add_time,complete_time)");
            stringBuilder.Append(" values (");
            stringBuilder.Append("@user_id,@user_name,@recharge_no,@payment_id,@amount,@status,@add_time,@complete_time)");
            OleDbParameter[] oleDbParameterArray = new OleDbParameter[8]
            {
              new OleDbParameter("@user_id", OleDbType.Integer, 4),
              new OleDbParameter("@user_name", OleDbType.VarChar, 100),
              new OleDbParameter("@recharge_no", OleDbType.VarChar, 100),
              new OleDbParameter("@payment_id", OleDbType.Integer, 4),
              new OleDbParameter("@amount", OleDbType.Decimal, 5),
              new OleDbParameter("@status", OleDbType.Integer, 4),
              new OleDbParameter("@add_time", OleDbType.Date),
              new OleDbParameter("@complete_time", OleDbType.Date)
            };
            oleDbParameterArray[0].Value = (object) model.user_id;
            oleDbParameterArray[1].Value = (object) model.user_name;
            oleDbParameterArray[2].Value = (object) model.recharge_no;
            oleDbParameterArray[3].Value = (object) model.payment_id;
            oleDbParameterArray[4].Value = (object) model.amount;
            oleDbParameterArray[5].Value = (object) model.status;
            oleDbParameterArray[6].Value = (object) model.add_time;
            if (model.complete_time.HasValue)
              oleDbParameterArray[7].Value = (object) model.complete_time;
            else
              oleDbParameterArray[7].Value = (object) DBNull.Value;
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

    public bool Update(Rain.Model.user_recharge model)
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("update " + this.databaseprefix + "user_recharge set ");
      stringBuilder.Append("user_id=@user_id,");
      stringBuilder.Append("user_name=@user_name,");
      stringBuilder.Append("recharge_no=@recharge_no,");
      stringBuilder.Append("payment_id=@payment_id,");
      stringBuilder.Append("amount=@amount,");
      stringBuilder.Append("status=@status,");
      stringBuilder.Append("add_time=@add_time,");
      stringBuilder.Append("complete_time=@complete_time");
      stringBuilder.Append(" where id=@id");
      OleDbParameter[] oleDbParameterArray = new OleDbParameter[9]
      {
        new OleDbParameter("@user_id", OleDbType.Integer, 4),
        new OleDbParameter("@user_name", OleDbType.VarChar, 100),
        new OleDbParameter("@recharge_no", OleDbType.VarChar, 100),
        new OleDbParameter("@payment_id", OleDbType.Integer, 4),
        new OleDbParameter("@amount", OleDbType.Decimal, 5),
        new OleDbParameter("@status", OleDbType.Integer, 4),
        new OleDbParameter("@add_time", OleDbType.Date),
        new OleDbParameter("@complete_time", OleDbType.Date),
        new OleDbParameter("@id", OleDbType.Integer, 4)
      };
      oleDbParameterArray[0].Value = (object) model.user_id;
      oleDbParameterArray[1].Value = (object) model.user_name;
      oleDbParameterArray[2].Value = (object) model.recharge_no;
      oleDbParameterArray[3].Value = (object) model.payment_id;
      oleDbParameterArray[4].Value = (object) model.amount;
      oleDbParameterArray[5].Value = (object) model.status;
      oleDbParameterArray[6].Value = (object) model.add_time;
      if (model.complete_time.HasValue)
        oleDbParameterArray[7].Value = (object) model.complete_time;
      else
        oleDbParameterArray[7].Value = (object) DBNull.Value;
      oleDbParameterArray[8].Value = (object) model.id;
      return DbHelperOleDb.ExecuteSql(stringBuilder.ToString(), oleDbParameterArray) > 0;
    }

    public bool Delete(int id)
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("delete from " + this.databaseprefix + "user_recharge ");
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
      stringBuilder.Append("delete from " + this.databaseprefix + "user_recharge ");
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

    public Rain.Model.user_recharge GetModel(int id)
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("select top 1 id,user_id,user_name,recharge_no,payment_id,amount,status,add_time,complete_time");
      stringBuilder.Append(" from " + this.databaseprefix + "user_recharge ");
      stringBuilder.Append(" where id=@id");
      OleDbParameter[] oleDbParameterArray = new OleDbParameter[1]
      {
        new OleDbParameter("@id", OleDbType.Integer, 4)
      };
      oleDbParameterArray[0].Value = (object) id;
      DataSet dataSet = DbHelperOleDb.Query(stringBuilder.ToString(), oleDbParameterArray);
      if (dataSet.Tables[0].Rows.Count > 0)
        return this.DataRowToModel(dataSet.Tables[0].Rows[0]);
      return (Rain.Model.user_recharge) null;
    }

    public Rain.Model.user_recharge GetModel(string recharge_no)
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("select top 1 id,user_id,user_name,recharge_no,payment_id,amount,status,add_time,complete_time");
      stringBuilder.Append(" from " + this.databaseprefix + "user_recharge ");
      stringBuilder.Append(" where recharge_no=@recharge_no");
      OleDbParameter[] oleDbParameterArray = new OleDbParameter[1]
      {
        new OleDbParameter("@recharge_no", OleDbType.VarChar, 100)
      };
      oleDbParameterArray[0].Value = (object) recharge_no;
      DataSet dataSet = DbHelperOleDb.Query(stringBuilder.ToString(), oleDbParameterArray);
      if (dataSet.Tables[0].Rows.Count > 0)
        return this.DataRowToModel(dataSet.Tables[0].Rows[0]);
      return (Rain.Model.user_recharge) null;
    }

    public DataSet GetList(string strWhere)
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("select id,user_id,user_name,recharge_no,payment_id,amount,status,add_time,complete_time ");
      stringBuilder.Append(" FROM " + this.databaseprefix + "user_recharge ");
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
      stringBuilder.Append(" id,user_id,user_name,recharge_no,payment_id,amount,status,add_time,complete_time ");
      stringBuilder.Append(" FROM " + this.databaseprefix + "user_recharge ");
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
      stringBuilder.Append("select * FROM " + this.databaseprefix + nameof (user_recharge));
      if (strWhere.Trim() != "")
        stringBuilder.Append(" where " + strWhere);
      recordCount = Convert.ToInt32(DbHelperOleDb.GetSingle(PagingHelper.CreateCountingSql(stringBuilder.ToString())));
      return DbHelperOleDb.Query(PagingHelper.CreatePagingSql(recordCount, pageSize, pageIndex, stringBuilder.ToString(), filedOrder));
    }

    public bool Recharge(Rain.Model.user_recharge model)
    {
      using (OleDbConnection connection = new OleDbConnection(DbHelperOleDb.connectionString))
      {
        connection.Open();
        using (OleDbTransaction trans = connection.BeginTransaction())
        {
          try
          {
            StringBuilder stringBuilder1 = new StringBuilder();
            stringBuilder1.Append("insert into " + this.databaseprefix + "user_amount_log(");
            stringBuilder1.Append("user_id,user_name,[value],[remark],add_time)");
            stringBuilder1.Append(" values (");
            stringBuilder1.Append("@user_id,@user_name,@value,@remark,@add_time)");
            OleDbParameter[] oleDbParameterArray1 = new OleDbParameter[5]
            {
              new OleDbParameter("@user_id", OleDbType.Integer, 4),
              new OleDbParameter("@user_name", OleDbType.VarChar, 100),
              new OleDbParameter("@value", OleDbType.Decimal, 5),
              new OleDbParameter("@remark", OleDbType.VarChar, 500),
              new OleDbParameter("@add_time", OleDbType.Date)
            };
            oleDbParameterArray1[0].Value = (object) model.user_id;
            oleDbParameterArray1[1].Value = (object) model.user_name;
            oleDbParameterArray1[2].Value = (object) model.amount;
            oleDbParameterArray1[3].Value = (object) ("在线充值，单号：" + model.recharge_no);
            oleDbParameterArray1[4].Value = (object) DateTime.Now;
            DbHelperOleDb.ExecuteSql(connection, trans, stringBuilder1.ToString(), oleDbParameterArray1);
            StringBuilder stringBuilder2 = new StringBuilder();
            stringBuilder2.Append("update " + this.databaseprefix + "users set amount=amount+" + (object) model.amount);
            stringBuilder2.Append(" where id=@id");
            OleDbParameter[] oleDbParameterArray2 = new OleDbParameter[1]
            {
              new OleDbParameter("@id", OleDbType.Integer, 4)
            };
            oleDbParameterArray2[0].Value = (object) model.user_id;
            DbHelperOleDb.ExecuteSql(connection, trans, stringBuilder2.ToString(), oleDbParameterArray2);
            StringBuilder stringBuilder3 = new StringBuilder();
            stringBuilder3.Append("insert into " + this.databaseprefix + "user_recharge(");
            stringBuilder3.Append("user_id,user_name,recharge_no,payment_id,amount,status,add_time,complete_time)");
            stringBuilder3.Append(" values (");
            stringBuilder3.Append("@user_id,@user_name,@recharge_no,@payment_id,@amount,@status,@add_time,@complete_time)");
            OleDbParameter[] oleDbParameterArray3 = new OleDbParameter[8]
            {
              new OleDbParameter("@user_id", OleDbType.Integer, 4),
              new OleDbParameter("@user_name", OleDbType.VarChar, 100),
              new OleDbParameter("@recharge_no", OleDbType.VarChar, 100),
              new OleDbParameter("@payment_id", OleDbType.Integer, 4),
              new OleDbParameter("@amount", OleDbType.Decimal, 5),
              new OleDbParameter("@status", OleDbType.Integer, 4),
              new OleDbParameter("@add_time", OleDbType.Date),
              new OleDbParameter("@complete_time", OleDbType.Date)
            };
            oleDbParameterArray3[0].Value = (object) model.user_id;
            oleDbParameterArray3[1].Value = (object) model.user_name;
            oleDbParameterArray3[2].Value = (object) model.recharge_no;
            oleDbParameterArray3[3].Value = (object) model.payment_id;
            oleDbParameterArray3[4].Value = (object) model.amount;
            oleDbParameterArray3[5].Value = (object) model.status;
            oleDbParameterArray3[6].Value = (object) model.add_time;
            if (model.complete_time.HasValue)
              oleDbParameterArray3[7].Value = (object) model.complete_time;
            else
              oleDbParameterArray3[7].Value = (object) DBNull.Value;
            DbHelperOleDb.ExecuteSql(connection, trans, stringBuilder3.ToString(), oleDbParameterArray3);
            trans.Commit();
          }
          catch
          {
            trans.Rollback();
            return false;
          }
        }
      }
      return true;
    }

    public bool Confirm(string recharge_no)
    {
      Rain.Model.user_recharge model = this.GetModel(recharge_no);
      if (model == null)
        return false;
      using (OleDbConnection connection = new OleDbConnection(DbHelperOleDb.connectionString))
      {
        connection.Open();
        using (OleDbTransaction trans = connection.BeginTransaction())
        {
          try
          {
            StringBuilder stringBuilder1 = new StringBuilder();
            stringBuilder1.Append("insert into " + this.databaseprefix + "user_amount_log(");
            stringBuilder1.Append("user_id,user_name,[value],[remark],add_time)");
            stringBuilder1.Append(" values (");
            stringBuilder1.Append("@user_id,@user_name,@value,@remark,@add_time)");
            OleDbParameter[] oleDbParameterArray1 = new OleDbParameter[5]
            {
              new OleDbParameter("@user_id", OleDbType.Integer, 4),
              new OleDbParameter("@user_name", OleDbType.VarChar, 100),
              new OleDbParameter("@value", OleDbType.Decimal, 5),
              new OleDbParameter("@remark", OleDbType.VarChar, 500),
              new OleDbParameter("@add_time", OleDbType.Date)
            };
            oleDbParameterArray1[0].Value = (object) model.user_id;
            oleDbParameterArray1[1].Value = (object) model.user_name;
            oleDbParameterArray1[2].Value = (object) model.amount;
            oleDbParameterArray1[3].Value = (object) ("在线充值，单号：" + recharge_no);
            oleDbParameterArray1[4].Value = (object) DateTime.Now;
            DbHelperOleDb.ExecuteSql(connection, trans, stringBuilder1.ToString(), oleDbParameterArray1);
            StringBuilder stringBuilder2 = new StringBuilder();
            stringBuilder2.Append("update " + this.databaseprefix + "users set amount=amount+" + (object) model.amount);
            stringBuilder2.Append(" where id=@id");
            OleDbParameter[] oleDbParameterArray2 = new OleDbParameter[1]
            {
              new OleDbParameter("@id", OleDbType.Integer, 4)
            };
            oleDbParameterArray2[0].Value = (object) model.user_id;
            DbHelperOleDb.ExecuteSql(connection, trans, stringBuilder2.ToString(), oleDbParameterArray2);
            StringBuilder stringBuilder3 = new StringBuilder();
            stringBuilder3.Append("update " + this.databaseprefix + "user_recharge set ");
            stringBuilder3.Append("status=@status,");
            stringBuilder3.Append("complete_time=@complete_time");
            stringBuilder3.Append(" where recharge_no=@recharge_no");
            OleDbParameter[] oleDbParameterArray3 = new OleDbParameter[3]
            {
              new OleDbParameter("@status", OleDbType.Integer, 4),
              new OleDbParameter("@complete_time", OleDbType.Date),
              new OleDbParameter("@recharge_no", OleDbType.VarChar, 100)
            };
            oleDbParameterArray3[0].Value = (object) 1;
            oleDbParameterArray3[1].Value = (object) DateTime.Now;
            oleDbParameterArray3[2].Value = (object) recharge_no;
            DbHelperOleDb.ExecuteSql(connection, trans, stringBuilder3.ToString(), oleDbParameterArray3);
            trans.Commit();
          }
          catch
          {
            trans.Rollback();
            return false;
          }
        }
      }
      return true;
    }

    public Rain.Model.user_recharge DataRowToModel(DataRow row)
    {
      Rain.Model.user_recharge userRecharge = new Rain.Model.user_recharge();
      if (row != null)
      {
        if (row["id"] != null && row["id"].ToString() != "")
          userRecharge.id = int.Parse(row["id"].ToString());
        if (row["user_id"] != null && row["user_id"].ToString() != "")
          userRecharge.user_id = int.Parse(row["user_id"].ToString());
        if (row["user_name"] != null)
          userRecharge.user_name = row["user_name"].ToString();
        if (row["recharge_no"] != null)
          userRecharge.recharge_no = row["recharge_no"].ToString();
        if (row["payment_id"] != null && row["payment_id"].ToString() != "")
          userRecharge.payment_id = int.Parse(row["payment_id"].ToString());
        if (row["amount"] != null && row["amount"].ToString() != "")
          userRecharge.amount = Decimal.Parse(row["amount"].ToString());
        if (row["status"] != null && row["status"].ToString() != "")
          userRecharge.status = int.Parse(row["status"].ToString());
        if (row["add_time"] != null && row["add_time"].ToString() != "")
          userRecharge.add_time = DateTime.Parse(row["add_time"].ToString());
        if (row["complete_time"] != null && row["complete_time"].ToString() != "")
          userRecharge.complete_time = new DateTime?(DateTime.Parse(row["complete_time"].ToString()));
      }
      return userRecharge;
    }
  }
}
