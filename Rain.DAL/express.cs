// Decompiled with JetBrains decompiler
// Type: Rain.DAL.express
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
  public class express
  {
    private string databaseprefix;

    public express(string _databaseprefix)
    {
      this.databaseprefix = _databaseprefix;
    }

    private int GetMaxId(OleDbConnection conn, OleDbTransaction trans)
    {
      string SQLString = "select top 1 id from " + this.databaseprefix + "express order by id desc";
      object single = DbHelperOleDb.GetSingle(conn, trans, SQLString);
      if (single == null)
        return 0;
      return int.Parse(single.ToString());
    }

    public bool Exists(int id)
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("select count(1) from " + this.databaseprefix + nameof (express));
      stringBuilder.Append(" where id=@id");
      OleDbParameter[] oleDbParameterArray = new OleDbParameter[1]
      {
        new OleDbParameter("@id", OleDbType.Integer, 4)
      };
      oleDbParameterArray[0].Value = (object) id;
      return DbHelperOleDb.Exists(stringBuilder.ToString(), oleDbParameterArray);
    }

    public int Add(Rain.Model.express model)
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
            stringBuilder.Append("insert into " + this.databaseprefix + "express(");
            stringBuilder.Append("title,express_code,express_fee,website,remark,sort_id,is_lock)");
            stringBuilder.Append(" values (");
            stringBuilder.Append("@title,@express_code,@express_fee,@website,@remark,@sort_id,@is_lock)");
            OleDbParameter[] oleDbParameterArray = new OleDbParameter[7]
            {
              new OleDbParameter("@title", OleDbType.VarChar, 100),
              new OleDbParameter("@express_code", OleDbType.VarChar, 100),
              new OleDbParameter("@express_fee", OleDbType.Decimal, 5),
              new OleDbParameter("@website", OleDbType.VarChar, (int) byte.MaxValue),
              new OleDbParameter("@remark", OleDbType.VarChar),
              new OleDbParameter("@sort_id", OleDbType.Integer, 4),
              new OleDbParameter("@is_lock", OleDbType.Integer, 4)
            };
            oleDbParameterArray[0].Value = (object) model.title;
            oleDbParameterArray[1].Value = (object) model.express_code;
            oleDbParameterArray[2].Value = (object) model.express_fee;
            oleDbParameterArray[3].Value = (object) model.website;
            oleDbParameterArray[4].Value = (object) model.remark;
            oleDbParameterArray[5].Value = (object) model.sort_id;
            oleDbParameterArray[6].Value = (object) model.is_lock;
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

    public bool Update(Rain.Model.express model)
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("update " + this.databaseprefix + "express set ");
      stringBuilder.Append("title=@title,");
      stringBuilder.Append("express_code=@express_code,");
      stringBuilder.Append("express_fee=@express_fee,");
      stringBuilder.Append("website=@website,");
      stringBuilder.Append("remark=@remark,");
      stringBuilder.Append("sort_id=@sort_id,");
      stringBuilder.Append("is_lock=@is_lock");
      stringBuilder.Append(" where id=@id");
      OleDbParameter[] oleDbParameterArray = new OleDbParameter[8]
      {
        new OleDbParameter("@title", OleDbType.VarChar, 100),
        new OleDbParameter("@express_code", OleDbType.VarChar, 100),
        new OleDbParameter("@express_fee", OleDbType.Decimal, 5),
        new OleDbParameter("@website", OleDbType.VarChar, (int) byte.MaxValue),
        new OleDbParameter("@remark", OleDbType.VarChar),
        new OleDbParameter("@sort_id", OleDbType.Integer, 4),
        new OleDbParameter("@is_lock", OleDbType.Integer, 4),
        new OleDbParameter("@id", OleDbType.Integer, 4)
      };
      oleDbParameterArray[0].Value = (object) model.title;
      oleDbParameterArray[1].Value = (object) model.express_code;
      oleDbParameterArray[2].Value = (object) model.express_fee;
      oleDbParameterArray[3].Value = (object) model.website;
      oleDbParameterArray[4].Value = (object) model.remark;
      oleDbParameterArray[5].Value = (object) model.sort_id;
      oleDbParameterArray[6].Value = (object) model.is_lock;
      oleDbParameterArray[7].Value = (object) model.id;
      return DbHelperOleDb.ExecuteSql(stringBuilder.ToString(), oleDbParameterArray) > 0;
    }

    public bool Delete(int id)
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("delete from " + this.databaseprefix + "express ");
      stringBuilder.Append(" where id=@id");
      OleDbParameter[] oleDbParameterArray = new OleDbParameter[1]
      {
        new OleDbParameter("@id", OleDbType.Integer, 4)
      };
      oleDbParameterArray[0].Value = (object) id;
      return DbHelperOleDb.ExecuteSql(stringBuilder.ToString(), oleDbParameterArray) > 0;
    }

    public Rain.Model.express GetModel(int id)
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("select top 1 id,title,express_code,express_fee,website,[remark],sort_id,is_lock");
      stringBuilder.Append(" from " + this.databaseprefix + nameof (Rain.Model.express));
      stringBuilder.Append(" where id=@id");
      OleDbParameter[] oleDbParameterArray = new OleDbParameter[1]
      {
        new OleDbParameter("@id", OleDbType.Integer, 4)
      };
      oleDbParameterArray[0].Value = (object) id;
      Rain.Model.express express = new Rain.Model.express();
      DataSet dataSet = DbHelperOleDb.Query(stringBuilder.ToString(), oleDbParameterArray);
      if (dataSet.Tables[0].Rows.Count > 0)
        return this.DataRowToModel(dataSet.Tables[0].Rows[0]);
      return (Rain.Model.express) null;
    }

    public DataSet GetList(string strWhere)
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("select id,title,express_code,express_fee,website,[remark],sort_id,is_lock ");
      stringBuilder.Append(" FROM " + this.databaseprefix + "express ");
      if (strWhere.Trim() != "")
        stringBuilder.Append(" where " + strWhere);
      stringBuilder.Append(" order by sort_id asc,id desc");
      return DbHelperOleDb.Query(stringBuilder.ToString());
    }

    public DataSet GetList(int Top, string strWhere, string filedOrder)
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("select ");
      if (Top > 0)
        stringBuilder.Append(" top " + Top.ToString());
      stringBuilder.Append(" id,title,express_code,express_fee,website,[remark],sort_id,is_lock ");
      stringBuilder.Append(" FROM " + this.databaseprefix + "express ");
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
      stringBuilder.Append("select * FROM " + this.databaseprefix + nameof (express));
      if (strWhere.Trim() != "")
        stringBuilder.Append(" where " + strWhere);
      recordCount = Convert.ToInt32(DbHelperOleDb.GetSingle(PagingHelper.CreateCountingSql(stringBuilder.ToString())));
      return DbHelperOleDb.Query(PagingHelper.CreatePagingSql(recordCount, pageSize, pageIndex, stringBuilder.ToString(), filedOrder));
    }

    public string GetTitle(int id)
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("select top 1 title from " + this.databaseprefix + nameof (express));
      stringBuilder.Append(" where id=" + (object) id);
      string str = Convert.ToString(DbHelperOleDb.GetSingle(stringBuilder.ToString()));
      if (string.IsNullOrEmpty(str))
        return "-";
      return str;
    }

    public void UpdateField(int id, string strValue)
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("update " + this.databaseprefix + "express set " + strValue);
      stringBuilder.Append(" where id=" + (object) id);
      DbHelperOleDb.ExecuteSql(stringBuilder.ToString());
    }

    public Rain.Model.express DataRowToModel(DataRow row)
    {
      Rain.Model.express express = new Rain.Model.express();
      if (row != null)
      {
        if (row["id"] != null && row["id"].ToString() != "")
          express.id = int.Parse(row["id"].ToString());
        if (row["title"] != null)
          express.title = row["title"].ToString();
        if (row["express_code"] != null)
          express.express_code = row["express_code"].ToString();
        if (row["express_fee"] != null && row["express_fee"].ToString() != "")
          express.express_fee = Decimal.Parse(row["express_fee"].ToString());
        if (row["website"] != null)
          express.website = row["website"].ToString();
        if (row["remark"] != null)
          express.remark = row["remark"].ToString();
        if (row["sort_id"] != null && row["sort_id"].ToString() != "")
          express.sort_id = int.Parse(row["sort_id"].ToString());
        if (row["is_lock"] != null && row["is_lock"].ToString() != "")
          express.is_lock = int.Parse(row["is_lock"].ToString());
      }
      return express;
    }
  }
}
