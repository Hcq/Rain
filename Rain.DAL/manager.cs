// Decompiled with JetBrains decompiler
// Type: Rain.DAL.manager
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
  public class manager
  {
    private string databaseprefix;

    public manager(string _databaseprefix)
    {
      this.databaseprefix = _databaseprefix;
    }

    private int GetMaxId(OleDbConnection conn, OleDbTransaction trans)
    {
      string SQLString = "select top 1 id from " + this.databaseprefix + "manager order by id desc";
      object single = DbHelperOleDb.GetSingle(conn, trans, SQLString);
      if (single == null)
        return 0;
      return int.Parse(single.ToString());
    }

    public bool Exists(int id)
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("select count(1) from " + this.databaseprefix + nameof (manager));
      stringBuilder.Append(" where id=@id ");
      OleDbParameter[] oleDbParameterArray = new OleDbParameter[1]
      {
        new OleDbParameter("@id", OleDbType.Integer, 4)
      };
      oleDbParameterArray[0].Value = (object) id;
      return DbHelperOleDb.Exists(stringBuilder.ToString(), oleDbParameterArray);
    }

    public bool Exists(string user_name)
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("select count(1) from " + this.databaseprefix + nameof (manager));
      stringBuilder.Append(" where user_name=@user_name ");
      OleDbParameter[] oleDbParameterArray = new OleDbParameter[1]
      {
        new OleDbParameter("@user_name", OleDbType.VarChar, 100)
      };
      oleDbParameterArray[0].Value = (object) user_name;
      return DbHelperOleDb.Exists(stringBuilder.ToString(), oleDbParameterArray);
    }

    public string GetSalt(string user_name)
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("select top 1 salt from " + this.databaseprefix + nameof (manager));
      stringBuilder.Append(" where user_name=@user_name");
      OleDbParameter[] oleDbParameterArray = new OleDbParameter[1]
      {
        new OleDbParameter("@user_name", OleDbType.VarChar, 100)
      };
      oleDbParameterArray[0].Value = (object) user_name;
      string str = Convert.ToString(DbHelperOleDb.GetSingle(stringBuilder.ToString(), oleDbParameterArray));
      if (string.IsNullOrEmpty(str))
        return "";
      return str;
    }

    public int Add(Rain.Model.manager model)
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
            stringBuilder.Append("insert into " + this.databaseprefix + "manager(");
            stringBuilder.Append("role_id,role_type,user_name,[password],salt,real_name,telephone,email,is_lock,add_time)");
            stringBuilder.Append(" values (");
            stringBuilder.Append("@role_id,@role_type,@user_name,@password,@salt,@real_name,@telephone,@email,@is_lock,@add_time)");
            OleDbParameter[] oleDbParameterArray = new OleDbParameter[10]
            {
              new OleDbParameter("@role_id", OleDbType.Integer, 4),
              new OleDbParameter("@role_type", OleDbType.Integer, 4),
              new OleDbParameter("@user_name", OleDbType.VarChar, 100),
              new OleDbParameter("@password", OleDbType.VarChar, 100),
              new OleDbParameter("@salt", OleDbType.VarChar, 20),
              new OleDbParameter("@real_name", OleDbType.VarChar, 50),
              new OleDbParameter("@telephone", OleDbType.VarChar, 30),
              new OleDbParameter("@email", OleDbType.VarChar, 30),
              new OleDbParameter("@is_lock", OleDbType.Integer, 4),
              new OleDbParameter("@add_time", OleDbType.Date)
            };
            oleDbParameterArray[0].Value = (object) model.role_id;
            oleDbParameterArray[1].Value = (object) model.role_type;
            oleDbParameterArray[2].Value = (object) model.user_name;
            oleDbParameterArray[3].Value = (object) model.password;
            oleDbParameterArray[4].Value = (object) model.salt;
            oleDbParameterArray[5].Value = (object) model.real_name;
            oleDbParameterArray[6].Value = (object) model.telephone;
            oleDbParameterArray[7].Value = (object) model.email;
            oleDbParameterArray[8].Value = (object) model.is_lock;
            oleDbParameterArray[9].Value = (object) model.add_time;
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

    public bool Update(Rain.Model.manager model)
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("update " + this.databaseprefix + "manager set ");
      stringBuilder.Append("role_id=@role_id,");
      stringBuilder.Append("role_type=@role_type,");
      stringBuilder.Append("user_name=@user_name,");
      stringBuilder.Append("[password]=@password,");
      stringBuilder.Append("real_name=@real_name,");
      stringBuilder.Append("telephone=@telephone,");
      stringBuilder.Append("email=@email,");
      stringBuilder.Append("is_lock=@is_lock,");
      stringBuilder.Append("add_time=@add_time");
      stringBuilder.Append(" where id=@id");
      OleDbParameter[] oleDbParameterArray = new OleDbParameter[10]
      {
        new OleDbParameter("@role_id", OleDbType.Integer, 4),
        new OleDbParameter("@role_type", OleDbType.Integer, 4),
        new OleDbParameter("@user_name", OleDbType.VarChar, 100),
        new OleDbParameter("@password", OleDbType.VarChar, 100),
        new OleDbParameter("@real_name", OleDbType.VarChar, 50),
        new OleDbParameter("@telephone", OleDbType.VarChar, 30),
        new OleDbParameter("@email", OleDbType.VarChar, 30),
        new OleDbParameter("@is_lock", OleDbType.Integer, 4),
        new OleDbParameter("@add_time", OleDbType.Date),
        new OleDbParameter("@id", OleDbType.Integer, 4)
      };
      oleDbParameterArray[0].Value = (object) model.role_id;
      oleDbParameterArray[1].Value = (object) model.role_type;
      oleDbParameterArray[2].Value = (object) model.user_name;
      oleDbParameterArray[3].Value = (object) model.password;
      oleDbParameterArray[4].Value = (object) model.real_name;
      oleDbParameterArray[5].Value = (object) model.telephone;
      oleDbParameterArray[6].Value = (object) model.email;
      oleDbParameterArray[7].Value = (object) model.is_lock;
      oleDbParameterArray[8].Value = (object) model.add_time;
      oleDbParameterArray[9].Value = (object) model.id;
      return DbHelperOleDb.ExecuteSql(stringBuilder.ToString(), oleDbParameterArray) > 0;
    }

    public bool Delete(int id)
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("delete from " + this.databaseprefix + "manager ");
      stringBuilder.Append(" where id=@id");
      OleDbParameter[] oleDbParameterArray = new OleDbParameter[1]
      {
        new OleDbParameter("@id", OleDbType.Integer, 4)
      };
      oleDbParameterArray[0].Value = (object) id;
      return DbHelperOleDb.ExecuteSql(stringBuilder.ToString(), oleDbParameterArray) > 0;
    }

    public Rain.Model.manager GetModel(int id)
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("select  top 1 id,role_id,role_type,user_name,[password],salt,real_name,telephone,email,is_lock,add_time from " + this.databaseprefix + "manager ");
      stringBuilder.Append(" where id=@id");
      OleDbParameter[] oleDbParameterArray = new OleDbParameter[1]
      {
        new OleDbParameter("@id", OleDbType.Integer, 4)
      };
      oleDbParameterArray[0].Value = (object) id;
      Rain.Model.manager manager = new Rain.Model.manager();
      DataSet dataSet = DbHelperOleDb.Query(stringBuilder.ToString(), oleDbParameterArray);
      if (dataSet.Tables[0].Rows.Count <= 0)
        return (Rain.Model.manager) null;
      if (dataSet.Tables[0].Rows[0][nameof (id)].ToString() != "")
        manager.id = int.Parse(dataSet.Tables[0].Rows[0][nameof (id)].ToString());
      if (dataSet.Tables[0].Rows[0]["role_id"].ToString() != "")
        manager.role_id = int.Parse(dataSet.Tables[0].Rows[0]["role_id"].ToString());
      if (dataSet.Tables[0].Rows[0]["role_type"].ToString() != "")
        manager.role_type = int.Parse(dataSet.Tables[0].Rows[0]["role_type"].ToString());
      manager.user_name = dataSet.Tables[0].Rows[0]["user_name"].ToString();
      manager.password = dataSet.Tables[0].Rows[0]["password"].ToString();
      manager.salt = dataSet.Tables[0].Rows[0]["salt"].ToString();
      manager.real_name = dataSet.Tables[0].Rows[0]["real_name"].ToString();
      manager.telephone = dataSet.Tables[0].Rows[0]["telephone"].ToString();
      manager.email = dataSet.Tables[0].Rows[0]["email"].ToString();
      if (dataSet.Tables[0].Rows[0]["is_lock"].ToString() != "")
        manager.is_lock = int.Parse(dataSet.Tables[0].Rows[0]["is_lock"].ToString());
      if (dataSet.Tables[0].Rows[0]["add_time"].ToString() != "")
        manager.add_time = DateTime.Parse(dataSet.Tables[0].Rows[0]["add_time"].ToString());
      return manager;
    }

    public Rain.Model.manager GetModel(string user_name, string password)
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("select id from " + this.databaseprefix + nameof (manager));
      stringBuilder.Append(" where user_name=@user_name and [password]=@password and is_lock=0");
      OleDbParameter[] oleDbParameterArray = new OleDbParameter[2]
      {
        new OleDbParameter("@user_name", OleDbType.VarChar, 100),
        new OleDbParameter("@password", OleDbType.VarChar, 100)
      };
      oleDbParameterArray[0].Value = (object) user_name;
      oleDbParameterArray[1].Value = (object) password;
      object single = DbHelperOleDb.GetSingle(stringBuilder.ToString(), oleDbParameterArray);
      if (single != null)
        return this.GetModel(Convert.ToInt32(single));
      return (Rain.Model.manager) null;
    }

    public DataSet GetList(int Top, string strWhere, string filedOrder)
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("select ");
      if (Top > 0)
        stringBuilder.Append(" top " + Top.ToString());
      stringBuilder.Append(" id,role_id,role_type,user_name,[password],salt,real_name,telephone,email,is_lock,add_time ");
      stringBuilder.Append(" FROM " + this.databaseprefix + "manager ");
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
      stringBuilder.Append("select * FROM " + this.databaseprefix + nameof (manager));
      if (strWhere.Trim() != "")
        stringBuilder.Append(" where " + strWhere);
      recordCount = Convert.ToInt32(DbHelperOleDb.GetSingle(PagingHelper.CreateCountingSql(stringBuilder.ToString())));
      return DbHelperOleDb.Query(PagingHelper.CreatePagingSql(recordCount, pageSize, pageIndex, stringBuilder.ToString(), filedOrder));
    }
  }
}
