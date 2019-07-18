// Decompiled with JetBrains decompiler
// Type: Rain.DAL.user_groups
// Assembly: Rain.DAL, Version=4.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 34D3CCEA-896B-46C7-93D3-7BA93E9004E2
// Assembly location: C:\Users\Felix.Ho\Desktop\page\bin\Rain.DAL.dll

using System;
using System.Collections;
using System.Data;
using System.Data.OleDb;
using System.Text;
using Rain.DBUtility;

namespace Rain.DAL
{
  public class user_groups
  {
    private string databaseprefix;

    public user_groups(string _databaseprefix)
    {
      this.databaseprefix = _databaseprefix;
    }

    private int GetMaxId(OleDbConnection conn, OleDbTransaction trans)
    {
      string SQLString = "select top 1 id from " + this.databaseprefix + "user_groups order by id desc";
      object single = DbHelperOleDb.GetSingle(conn, trans, SQLString);
      if (single == null)
        return 0;
      return int.Parse(single.ToString());
    }

    public bool Exists(int id)
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("select count(1) from " + this.databaseprefix + nameof (user_groups));
      stringBuilder.Append(" where id=@id");
      OleDbParameter[] oleDbParameterArray = new OleDbParameter[1]
      {
        new OleDbParameter("@id", OleDbType.Integer, 4)
      };
      oleDbParameterArray[0].Value = (object) id;
      return DbHelperOleDb.Exists(stringBuilder.ToString(), oleDbParameterArray);
    }

    public int Add(Rain.Model.user_groups model)
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
            stringBuilder.Append("insert into " + this.databaseprefix + "user_groups(");
            stringBuilder.Append("title,grade,upgrade_exp,amount,point,discount,is_default,is_upgrade,is_lock)");
            stringBuilder.Append(" values (");
            stringBuilder.Append("@title,@grade,@upgrade_exp,@amount,@point,@discount,@is_default,@is_upgrade,@is_lock)");
            OleDbParameter[] oleDbParameterArray = new OleDbParameter[9]
            {
              new OleDbParameter("@title", OleDbType.VarChar, 100),
              new OleDbParameter("@grade", OleDbType.Integer, 4),
              new OleDbParameter("@upgrade_exp", OleDbType.Integer, 4),
              new OleDbParameter("@amount", OleDbType.Decimal, 5),
              new OleDbParameter("@point", OleDbType.Integer, 4),
              new OleDbParameter("@discount", OleDbType.Integer, 4),
              new OleDbParameter("@is_default", OleDbType.Integer, 4),
              new OleDbParameter("@is_upgrade", OleDbType.Integer, 4),
              new OleDbParameter("@is_lock", OleDbType.Integer, 4)
            };
            oleDbParameterArray[0].Value = (object) model.title;
            oleDbParameterArray[1].Value = (object) model.grade;
            oleDbParameterArray[2].Value = (object) model.upgrade_exp;
            oleDbParameterArray[3].Value = (object) model.amount;
            oleDbParameterArray[4].Value = (object) model.point;
            oleDbParameterArray[5].Value = (object) model.discount;
            oleDbParameterArray[6].Value = (object) model.is_default;
            oleDbParameterArray[7].Value = (object) model.is_upgrade;
            oleDbParameterArray[8].Value = (object) model.is_lock;
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

    public bool Update(Rain.Model.user_groups model)
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("update " + this.databaseprefix + "user_groups set ");
      stringBuilder.Append("title=@title,");
      stringBuilder.Append("grade=@grade,");
      stringBuilder.Append("upgrade_exp=@upgrade_exp,");
      stringBuilder.Append("amount=@amount,");
      stringBuilder.Append("point=@point,");
      stringBuilder.Append("discount=@discount,");
      stringBuilder.Append("is_default=@is_default,");
      stringBuilder.Append("is_upgrade=@is_upgrade,");
      stringBuilder.Append("is_lock=@is_lock");
      stringBuilder.Append(" where id=@id");
      OleDbParameter[] oleDbParameterArray = new OleDbParameter[10]
      {
        new OleDbParameter("@title", OleDbType.VarChar, 100),
        new OleDbParameter("@grade", OleDbType.Integer, 4),
        new OleDbParameter("@upgrade_exp", OleDbType.Integer, 4),
        new OleDbParameter("@amount", OleDbType.Decimal, 5),
        new OleDbParameter("@point", OleDbType.Integer, 4),
        new OleDbParameter("@discount", OleDbType.Integer, 4),
        new OleDbParameter("@is_default", OleDbType.Integer, 4),
        new OleDbParameter("@is_upgrade", OleDbType.Integer, 4),
        new OleDbParameter("@is_lock", OleDbType.Integer, 4),
        new OleDbParameter("@id", OleDbType.Integer, 4)
      };
      oleDbParameterArray[0].Value = (object) model.title;
      oleDbParameterArray[1].Value = (object) model.grade;
      oleDbParameterArray[2].Value = (object) model.upgrade_exp;
      oleDbParameterArray[3].Value = (object) model.amount;
      oleDbParameterArray[4].Value = (object) model.point;
      oleDbParameterArray[5].Value = (object) model.discount;
      oleDbParameterArray[6].Value = (object) model.is_default;
      oleDbParameterArray[7].Value = (object) model.is_upgrade;
      oleDbParameterArray[8].Value = (object) model.is_lock;
      oleDbParameterArray[9].Value = (object) model.id;
      return DbHelperOleDb.ExecuteSql(stringBuilder.ToString(), oleDbParameterArray) > 0;
    }

    public bool Delete(int id)
    {
      Hashtable SQLStringList = new Hashtable();
      StringBuilder stringBuilder1 = new StringBuilder();
      stringBuilder1.Append("delete from " + this.databaseprefix + "user_group_price ");
      stringBuilder1.Append(" where group_id=@group_id ");
      OleDbParameter[] oleDbParameterArray1 = new OleDbParameter[1]
      {
        new OleDbParameter("@group_id", OleDbType.Integer, 4)
      };
      oleDbParameterArray1[0].Value = (object) id;
      SQLStringList.Add((object) stringBuilder1.ToString(), (object) oleDbParameterArray1);
      StringBuilder stringBuilder2 = new StringBuilder();
      stringBuilder2.Append("delete from " + this.databaseprefix + "user_groups ");
      stringBuilder2.Append(" where id=@id");
      OleDbParameter[] oleDbParameterArray2 = new OleDbParameter[1]
      {
        new OleDbParameter("@id", OleDbType.Integer, 4)
      };
      oleDbParameterArray2[0].Value = (object) id;
      SQLStringList.Add((object) stringBuilder2.ToString(), (object) oleDbParameterArray2);
      return DbHelperOleDb.ExecuteSqlTran(SQLStringList);
    }

    public Rain.Model.user_groups GetModel(int id)
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("select top 1 id,title,grade,upgrade_exp,amount,point,discount,is_default,is_upgrade,is_lock");
      stringBuilder.Append(" from " + this.databaseprefix + nameof (user_groups));
      stringBuilder.Append(" where id=@id");
      OleDbParameter[] oleDbParameterArray = new OleDbParameter[1]
      {
        new OleDbParameter("@id", OleDbType.Integer, 4)
      };
      oleDbParameterArray[0].Value = (object) id;
      DataSet dataSet = DbHelperOleDb.Query(stringBuilder.ToString(), oleDbParameterArray);
      if (dataSet.Tables[0].Rows.Count > 0)
        return this.DataRowToModel(dataSet.Tables[0].Rows[0]);
      return (Rain.Model.user_groups) null;
    }

    public DataSet GetList(int Top, string strWhere, string filedOrder)
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("select ");
      if (Top > 0)
        stringBuilder.Append(" top " + Top.ToString());
      stringBuilder.Append(" id,title,grade,upgrade_exp,amount,point,discount,is_default,is_upgrade,is_lock ");
      stringBuilder.Append(" FROM " + this.databaseprefix + "user_groups ");
      if (strWhere.Trim() != "")
        stringBuilder.Append(" where " + strWhere);
      stringBuilder.Append(" order by " + filedOrder);
      return DbHelperOleDb.Query(stringBuilder.ToString());
    }

    public string GetTitle(int id)
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("select top 1 title from " + this.databaseprefix + nameof (user_groups));
      stringBuilder.Append(" where id=" + (object) id);
      string str = Convert.ToString(DbHelperOleDb.GetSingle(stringBuilder.ToString()));
      if (string.IsNullOrEmpty(str))
        return "";
      return str;
    }

    public int GetDiscount(int id)
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("select top 1 discount from " + this.databaseprefix + nameof (user_groups));
      stringBuilder.Append(" where id=" + (object) id);
      string str = Convert.ToString(DbHelperOleDb.GetSingle(stringBuilder.ToString()));
      if (string.IsNullOrEmpty(str))
        return 0;
      return Convert.ToInt32(str);
    }

    public Rain.Model.user_groups GetDefault()
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("select top 1 id,title,grade,upgrade_exp,amount,point,discount,is_default,is_upgrade,is_lock");
      stringBuilder.Append(" from " + this.databaseprefix + nameof (user_groups));
      stringBuilder.Append(" where is_lock=0 order by is_default desc,id asc");
      DataSet dataSet = DbHelperOleDb.Query(stringBuilder.ToString());
      if (dataSet.Tables[0].Rows.Count > 0)
        return this.DataRowToModel(dataSet.Tables[0].Rows[0]);
      return (Rain.Model.user_groups) null;
    }

    public Rain.Model.user_groups GetUpgrade(int group_id, int exp)
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("select top 1 id,title,grade,upgrade_exp,amount,point,discount,is_default,is_upgrade,is_lock");
      stringBuilder.Append(" from " + this.databaseprefix + nameof (user_groups));
      stringBuilder.Append(" where is_lock=0 and is_upgrade=1 and grade>(select grade from " + this.databaseprefix + "user_groups where id=" + (object) group_id + ") and upgrade_exp<=" + (object) exp);
      stringBuilder.Append(" order by grade asc");
      DataSet dataSet = DbHelperOleDb.Query(stringBuilder.ToString());
      if (dataSet.Tables[0].Rows.Count > 0)
        return this.DataRowToModel(dataSet.Tables[0].Rows[0]);
      return (Rain.Model.user_groups) null;
    }

    public Rain.Model.user_groups DataRowToModel(DataRow row)
    {
      Rain.Model.user_groups userGroups = new Rain.Model.user_groups();
      if (row != null)
      {
        if (row["id"] != null && row["id"].ToString() != "")
          userGroups.id = int.Parse(row["id"].ToString());
        if (row["title"] != null)
          userGroups.title = row["title"].ToString();
        if (row["grade"] != null && row["grade"].ToString() != "")
          userGroups.grade = int.Parse(row["grade"].ToString());
        if (row["upgrade_exp"] != null && row["upgrade_exp"].ToString() != "")
          userGroups.upgrade_exp = int.Parse(row["upgrade_exp"].ToString());
        if (row["amount"] != null && row["amount"].ToString() != "")
          userGroups.amount = Decimal.Parse(row["amount"].ToString());
        if (row["point"] != null && row["point"].ToString() != "")
          userGroups.point = int.Parse(row["point"].ToString());
        if (row["discount"] != null && row["discount"].ToString() != "")
          userGroups.discount = int.Parse(row["discount"].ToString());
        if (row["is_default"] != null && row["is_default"].ToString() != "")
          userGroups.is_default = int.Parse(row["is_default"].ToString());
        if (row["is_upgrade"] != null && row["is_upgrade"].ToString() != "")
          userGroups.is_upgrade = int.Parse(row["is_upgrade"].ToString());
        if (row["is_lock"] != null && row["is_lock"].ToString() != "")
          userGroups.is_lock = int.Parse(row["is_lock"].ToString());
      }
      return userGroups;
    }
  }
}
