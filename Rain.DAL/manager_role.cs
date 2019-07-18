// Decompiled with JetBrains decompiler
// Type: Rain.DAL.manager_role
// Assembly: Rain.DAL, Version=4.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 34D3CCEA-896B-46C7-93D3-7BA93E9004E2
// Assembly location: C:\Users\Felix.Ho\Desktop\page\bin\Rain.DAL.dll

using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Text;
using Rain.DBUtility;
using Rain.Model;

namespace Rain.DAL
{
  public class manager_role
  {
    private string databaseprefix;

    public manager_role(string _databaseprefix)
    {
      this.databaseprefix = _databaseprefix;
    }

    private int GetMaxId(OleDbConnection conn, OleDbTransaction trans)
    {
      string SQLString = "select top 1 id from " + this.databaseprefix + "manager_role order by id desc";
      object single = DbHelperOleDb.GetSingle(conn, trans, SQLString);
      if (single == null)
        return 0;
      return int.Parse(single.ToString());
    }

    public bool Exists(int id)
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("select count(1) from " + this.databaseprefix + nameof (manager_role));
      stringBuilder.Append(" where id=@id ");
      OleDbParameter[] oleDbParameterArray = new OleDbParameter[1]
      {
        new OleDbParameter("@id", OleDbType.Integer, 4)
      };
      oleDbParameterArray[0].Value = (object) id;
      return DbHelperOleDb.Exists(stringBuilder.ToString(), oleDbParameterArray);
    }

    public string GetTitle(int id)
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("select top 1 role_name from " + this.databaseprefix + nameof (manager_role));
      stringBuilder.Append(" where id=" + (object) id);
      string str = Convert.ToString(DbHelperOleDb.GetSingle(stringBuilder.ToString()));
      if (string.IsNullOrEmpty(str))
        return "";
      return str;
    }

    public int Add(Rain.Model.manager_role model)
    {
      using (OleDbConnection oleDbConnection = new OleDbConnection(DbHelperOleDb.connectionString))
      {
        oleDbConnection.Open();
        using (OleDbTransaction trans = oleDbConnection.BeginTransaction())
        {
          try
          {
            StringBuilder stringBuilder1 = new StringBuilder();
            stringBuilder1.Append("insert into " + this.databaseprefix + "manager_role(");
            stringBuilder1.Append("role_name,role_type,is_sys)");
            stringBuilder1.Append(" values (");
            stringBuilder1.Append("@role_name,@role_type,@is_sys)");
            OleDbParameter[] oleDbParameterArray1 = new OleDbParameter[3]
            {
              new OleDbParameter("@role_name", OleDbType.VarChar, 100),
              new OleDbParameter("@role_type", OleDbType.Integer, 4),
              new OleDbParameter("@is_sys", OleDbType.Integer, 4)
            };
            oleDbParameterArray1[0].Value = (object) model.role_name;
            oleDbParameterArray1[1].Value = (object) model.role_type;
            oleDbParameterArray1[2].Value = (object) model.is_sys;
            DbHelperOleDb.ExecuteSql(oleDbConnection, trans, stringBuilder1.ToString(), oleDbParameterArray1);
            model.id = this.GetMaxId(oleDbConnection, trans);
            if (model.manager_role_values != null)
            {
              foreach (manager_role_value managerRoleValue in model.manager_role_values)
              {
                StringBuilder stringBuilder2 = new StringBuilder();
                stringBuilder2.Append("insert into " + this.databaseprefix + "manager_role_value(");
                stringBuilder2.Append("role_id,nav_name,action_type)");
                stringBuilder2.Append(" values (");
                stringBuilder2.Append("@role_id,@nav_name,@action_type)");
                OleDbParameter[] oleDbParameterArray2 = new OleDbParameter[3]
                {
                  new OleDbParameter("@role_id", OleDbType.Integer, 4),
                  new OleDbParameter("@nav_name", OleDbType.VarChar, 100),
                  new OleDbParameter("@action_type", OleDbType.VarChar, 50)
                };
                oleDbParameterArray2[0].Value = (object) model.id;
                oleDbParameterArray2[1].Value = (object) managerRoleValue.nav_name;
                oleDbParameterArray2[2].Value = (object) managerRoleValue.action_type;
                DbHelperOleDb.ExecuteSql(oleDbConnection, trans, stringBuilder2.ToString(), oleDbParameterArray2);
              }
            }
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

    public bool Update(Rain.Model.manager_role model)
    {
      using (OleDbConnection connection = new OleDbConnection(DbHelperOleDb.connectionString))
      {
        connection.Open();
        using (OleDbTransaction trans = connection.BeginTransaction())
        {
          try
          {
            StringBuilder stringBuilder1 = new StringBuilder();
            stringBuilder1.Append("update " + this.databaseprefix + "manager_role set ");
            stringBuilder1.Append("role_name=@role_name,");
            stringBuilder1.Append("role_type=@role_type,");
            stringBuilder1.Append("is_sys=@is_sys");
            stringBuilder1.Append(" where id=@id");
            OleDbParameter[] oleDbParameterArray1 = new OleDbParameter[4]
            {
              new OleDbParameter("@role_name", OleDbType.VarChar, 100),
              new OleDbParameter("@role_type", OleDbType.Integer, 4),
              new OleDbParameter("@is_sys", OleDbType.Integer, 4),
              new OleDbParameter("@id", OleDbType.Integer, 4)
            };
            oleDbParameterArray1[0].Value = (object) model.role_name;
            oleDbParameterArray1[1].Value = (object) model.role_type;
            oleDbParameterArray1[2].Value = (object) model.is_sys;
            oleDbParameterArray1[3].Value = (object) model.id;
            DbHelperOleDb.ExecuteSql(connection, trans, stringBuilder1.ToString(), oleDbParameterArray1);
            StringBuilder stringBuilder2 = new StringBuilder();
            stringBuilder2.Append("delete from " + this.databaseprefix + "manager_role_value where role_id=@role_id ");
            OleDbParameter[] oleDbParameterArray2 = new OleDbParameter[1]
            {
              new OleDbParameter("@role_id", OleDbType.Integer, 4)
            };
            oleDbParameterArray2[0].Value = (object) model.id;
            DbHelperOleDb.ExecuteSql(connection, trans, stringBuilder2.ToString(), oleDbParameterArray2);
            if (model.manager_role_values != null)
            {
              foreach (manager_role_value managerRoleValue in model.manager_role_values)
              {
                StringBuilder stringBuilder3 = new StringBuilder();
                stringBuilder3.Append("insert into " + this.databaseprefix + "manager_role_value(");
                stringBuilder3.Append("role_id,nav_name,action_type)");
                stringBuilder3.Append(" values (");
                stringBuilder3.Append("@role_id,@nav_name,@action_type)");
                OleDbParameter[] oleDbParameterArray3 = new OleDbParameter[3]
                {
                  new OleDbParameter("@role_id", OleDbType.Integer, 4),
                  new OleDbParameter("@nav_name", OleDbType.VarChar, 100),
                  new OleDbParameter("@action_type", OleDbType.VarChar, 50)
                };
                oleDbParameterArray3[0].Value = (object) model.id;
                oleDbParameterArray3[1].Value = (object) managerRoleValue.nav_name;
                oleDbParameterArray3[2].Value = (object) managerRoleValue.action_type;
                DbHelperOleDb.ExecuteSql(connection, trans, stringBuilder3.ToString(), oleDbParameterArray3);
              }
            }
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

    public bool Delete(int id)
    {
      Hashtable SQLStringList = new Hashtable();
      StringBuilder stringBuilder1 = new StringBuilder();
      stringBuilder1.Append("delete from " + this.databaseprefix + "manager_role_value ");
      stringBuilder1.Append(" where role_id=@role_id");
      OleDbParameter[] oleDbParameterArray1 = new OleDbParameter[1]
      {
        new OleDbParameter("@role_id", OleDbType.Integer, 4)
      };
      oleDbParameterArray1[0].Value = (object) id;
      SQLStringList.Add((object) stringBuilder1.ToString(), (object) oleDbParameterArray1);
      StringBuilder stringBuilder2 = new StringBuilder();
      stringBuilder2.Append("delete from " + this.databaseprefix + "manager_role ");
      stringBuilder2.Append(" where id=@id");
      OleDbParameter[] oleDbParameterArray2 = new OleDbParameter[1]
      {
        new OleDbParameter("@id", OleDbType.Integer, 4)
      };
      oleDbParameterArray2[0].Value = (object) id;
      SQLStringList.Add((object) stringBuilder2.ToString(), (object) oleDbParameterArray2);
      return DbHelperOleDb.ExecuteSqlTran(SQLStringList);
    }

    public Rain.Model.manager_role GetModel(int id)
    {
      StringBuilder stringBuilder1 = new StringBuilder();
      stringBuilder1.Append("select  top 1 id,role_name,role_type,is_sys from " + this.databaseprefix + "manager_role ");
      stringBuilder1.Append(" where id=@id");
      OleDbParameter[] oleDbParameterArray1 = new OleDbParameter[1]
      {
        new OleDbParameter("@id", OleDbType.Integer, 4)
      };
      oleDbParameterArray1[0].Value = (object) id;
      Rain.Model.manager_role managerRole = new Rain.Model.manager_role();
      DataSet dataSet1 = DbHelperOleDb.Query(stringBuilder1.ToString(), oleDbParameterArray1);
      if (dataSet1.Tables[0].Rows.Count <= 0)
        return (Rain.Model.manager_role) null;
      if (dataSet1.Tables[0].Rows[0][nameof (id)].ToString() != "")
        managerRole.id = int.Parse(dataSet1.Tables[0].Rows[0][nameof (id)].ToString());
      managerRole.role_name = dataSet1.Tables[0].Rows[0]["role_name"].ToString();
      if (dataSet1.Tables[0].Rows[0]["role_type"].ToString() != "")
        managerRole.role_type = int.Parse(dataSet1.Tables[0].Rows[0]["role_type"].ToString());
      if (dataSet1.Tables[0].Rows[0]["is_sys"].ToString() != "")
        managerRole.is_sys = int.Parse(dataSet1.Tables[0].Rows[0]["is_sys"].ToString());
      StringBuilder stringBuilder2 = new StringBuilder();
      stringBuilder2.Append("select id,role_id,nav_name,action_type from " + this.databaseprefix + "manager_role_value ");
      stringBuilder2.Append(" where role_id=@role_id");
      OleDbParameter[] oleDbParameterArray2 = new OleDbParameter[1]
      {
        new OleDbParameter("@role_id", OleDbType.Integer, 4)
      };
      oleDbParameterArray2[0].Value = (object) id;
      DataSet dataSet2 = DbHelperOleDb.Query(stringBuilder2.ToString(), oleDbParameterArray2);
      if (dataSet2.Tables[0].Rows.Count > 0)
      {
        List<manager_role_value> managerRoleValueList = new List<manager_role_value>();
        foreach (DataRow row in (InternalDataCollectionBase) dataSet2.Tables[0].Rows)
        {
          manager_role_value managerRoleValue = new manager_role_value();
          if (row[nameof (id)].ToString() != "")
            managerRoleValue.id = int.Parse(row[nameof (id)].ToString());
          if (row["role_id"].ToString() != "")
            managerRoleValue.role_id = int.Parse(row["role_id"].ToString());
          managerRoleValue.nav_name = row["nav_name"].ToString();
          managerRoleValue.action_type = row["action_type"].ToString();
          managerRoleValueList.Add(managerRoleValue);
        }
        managerRole.manager_role_values = managerRoleValueList;
      }
      return managerRole;
    }

    public DataSet GetList(string strWhere)
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("select id,role_name,role_type,is_sys ");
      stringBuilder.Append(" FROM " + this.databaseprefix + "manager_role ");
      if (strWhere.Trim() != "")
        stringBuilder.Append(" where " + strWhere);
      return DbHelperOleDb.Query(stringBuilder.ToString());
    }
  }
}
