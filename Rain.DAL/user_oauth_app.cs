// Decompiled with JetBrains decompiler
// Type: Rain.DAL.user_oauth_app
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
  public class user_oauth_app
  {
    private string databaseprefix;

    public user_oauth_app(string _databaseprefix)
    {
      this.databaseprefix = _databaseprefix;
    }

    private int GetMaxId(OleDbConnection conn, OleDbTransaction trans)
    {
      string SQLString = "select top 1 id from " + this.databaseprefix + "user_oauth_app order by id desc";
      object single = DbHelperOleDb.GetSingle(conn, trans, SQLString);
      if (single == null)
        return 0;
      return int.Parse(single.ToString());
    }

    public bool Exists(int id)
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("select count(1) from " + this.databaseprefix + nameof (user_oauth_app));
      stringBuilder.Append(" where id=@id");
      OleDbParameter[] oleDbParameterArray = new OleDbParameter[1]
      {
        new OleDbParameter("@id", OleDbType.Integer, 4)
      };
      oleDbParameterArray[0].Value = (object) id;
      return DbHelperOleDb.Exists(stringBuilder.ToString(), oleDbParameterArray);
    }

    public int Add(Rain.Model.user_oauth_app model)
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
            stringBuilder.Append("insert into " + this.databaseprefix + "user_oauth_app(");
            stringBuilder.Append("title,img_url,app_id,app_key,[remark],sort_id,is_lock,api_path)");
            stringBuilder.Append(" values (");
            stringBuilder.Append("@title,@img_url,@app_id,@app_key,@remark,@sort_id,@is_lock,@api_path)");
            OleDbParameter[] oleDbParameterArray = new OleDbParameter[8]
            {
              new OleDbParameter("@title", OleDbType.VarChar, 100),
              new OleDbParameter("@img_url", OleDbType.VarChar, (int) byte.MaxValue),
              new OleDbParameter("@app_id", OleDbType.VarChar, 100),
              new OleDbParameter("@app_key", OleDbType.VarChar, 500),
              new OleDbParameter("@remark", OleDbType.VarChar, 500),
              new OleDbParameter("@sort_id", OleDbType.Integer, 4),
              new OleDbParameter("@is_lock", OleDbType.Integer, 4),
              new OleDbParameter("@api_path", OleDbType.VarChar, (int) byte.MaxValue)
            };
            oleDbParameterArray[0].Value = (object) model.title;
            oleDbParameterArray[1].Value = (object) model.img_url;
            oleDbParameterArray[2].Value = (object) model.app_id;
            oleDbParameterArray[3].Value = (object) model.app_key;
            oleDbParameterArray[4].Value = (object) model.remark;
            oleDbParameterArray[5].Value = (object) model.sort_id;
            oleDbParameterArray[6].Value = (object) model.is_lock;
            oleDbParameterArray[7].Value = (object) model.api_path;
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

    public bool Update(Rain.Model.user_oauth_app model)
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("update " + this.databaseprefix + "user_oauth_app set ");
      stringBuilder.Append("title=@title,");
      stringBuilder.Append("img_url=@img_url,");
      stringBuilder.Append("app_id=@app_id,");
      stringBuilder.Append("app_key=@app_key,");
      stringBuilder.Append("[remark]=@remark,");
      stringBuilder.Append("sort_id=@sort_id,");
      stringBuilder.Append("is_lock=@is_lock,");
      stringBuilder.Append("api_path=@api_path");
      stringBuilder.Append(" where id=@id");
      OleDbParameter[] oleDbParameterArray = new OleDbParameter[9]
      {
        new OleDbParameter("@title", OleDbType.VarChar, 100),
        new OleDbParameter("@img_url", OleDbType.VarChar, (int) byte.MaxValue),
        new OleDbParameter("@app_id", OleDbType.VarChar, 100),
        new OleDbParameter("@app_key", OleDbType.VarChar, 500),
        new OleDbParameter("@remark", OleDbType.VarChar, 500),
        new OleDbParameter("@sort_id", OleDbType.Integer, 4),
        new OleDbParameter("@is_lock", OleDbType.Integer, 4),
        new OleDbParameter("@api_path", OleDbType.VarChar, (int) byte.MaxValue),
        new OleDbParameter("@id", OleDbType.Integer, 4)
      };
      oleDbParameterArray[0].Value = (object) model.title;
      oleDbParameterArray[1].Value = (object) model.img_url;
      oleDbParameterArray[2].Value = (object) model.app_id;
      oleDbParameterArray[3].Value = (object) model.app_key;
      oleDbParameterArray[4].Value = (object) model.remark;
      oleDbParameterArray[5].Value = (object) model.sort_id;
      oleDbParameterArray[6].Value = (object) model.is_lock;
      oleDbParameterArray[7].Value = (object) model.api_path;
      oleDbParameterArray[8].Value = (object) model.id;
      return DbHelperOleDb.ExecuteSql(stringBuilder.ToString(), oleDbParameterArray) > 0;
    }

    public bool Delete(int id)
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("delete from " + this.databaseprefix + "user_oauth_app ");
      stringBuilder.Append(" where id=@id");
      OleDbParameter[] oleDbParameterArray = new OleDbParameter[1]
      {
        new OleDbParameter("@id", OleDbType.Integer, 4)
      };
      oleDbParameterArray[0].Value = (object) id;
      return DbHelperOleDb.ExecuteSql(stringBuilder.ToString(), oleDbParameterArray) > 0;
    }

    public Rain.Model.user_oauth_app GetModel(int id)
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("select top 1 id,title,img_url,app_id,app_key,[remark],sort_id,is_lock,api_path");
      stringBuilder.Append(" from " + this.databaseprefix + nameof (user_oauth_app));
      stringBuilder.Append(" where id=@id");
      OleDbParameter[] oleDbParameterArray = new OleDbParameter[1]
      {
        new OleDbParameter("@id", OleDbType.Integer, 4)
      };
      oleDbParameterArray[0].Value = (object) id;
      DataSet dataSet = DbHelperOleDb.Query(stringBuilder.ToString(), oleDbParameterArray);
      if (dataSet.Tables[0].Rows.Count > 0)
        return this.DataRowToModel(dataSet.Tables[0].Rows[0]);
      return (Rain.Model.user_oauth_app) null;
    }

    public Rain.Model.user_oauth_app GetModel(string api_path)
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("select top 1 id,title,img_url,app_id,app_key,[remark],sort_id,is_lock,api_path");
      stringBuilder.Append(" from " + this.databaseprefix + nameof (user_oauth_app));
      stringBuilder.Append(" where api_path=@api_path");
      OleDbParameter[] oleDbParameterArray = new OleDbParameter[1]
      {
        new OleDbParameter("@api_path", OleDbType.VarChar, 100)
      };
      oleDbParameterArray[0].Value = (object) api_path;
      DataSet dataSet = DbHelperOleDb.Query(stringBuilder.ToString(), oleDbParameterArray);
      if (dataSet.Tables[0].Rows.Count > 0)
        return this.DataRowToModel(dataSet.Tables[0].Rows[0]);
      return (Rain.Model.user_oauth_app) null;
    }

    public DataSet GetList(int Top, string strWhere, string filedOrder)
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("select ");
      if (Top > 0)
        stringBuilder.Append(" top " + Top.ToString());
      stringBuilder.Append(" id,title,img_url,app_id,app_key,[remark],sort_id,is_lock,api_path ");
      stringBuilder.Append(" FROM " + this.databaseprefix + "user_oauth_app ");
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
      stringBuilder.Append("select * FROM " + this.databaseprefix + nameof (user_oauth_app));
      if (strWhere.Trim() != "")
        stringBuilder.Append(" where " + strWhere);
      recordCount = Convert.ToInt32(DbHelperOleDb.GetSingle(PagingHelper.CreateCountingSql(stringBuilder.ToString())));
      return DbHelperOleDb.Query(PagingHelper.CreatePagingSql(recordCount, pageSize, pageIndex, stringBuilder.ToString(), filedOrder));
    }

    public void UpdateField(int id, string strValue)
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("update " + this.databaseprefix + "user_oauth_app set " + strValue);
      stringBuilder.Append(" where id=" + (object) id);
      DbHelperOleDb.ExecuteSql(stringBuilder.ToString());
    }

    public Rain.Model.user_oauth_app DataRowToModel(DataRow row)
    {
      Rain.Model.user_oauth_app userOauthApp = new Rain.Model.user_oauth_app();
      if (row != null)
      {
        if (row["id"] != null && row["id"].ToString() != "")
          userOauthApp.id = int.Parse(row["id"].ToString());
        if (row["title"] != null)
          userOauthApp.title = row["title"].ToString();
        if (row["img_url"] != null)
          userOauthApp.img_url = row["img_url"].ToString();
        if (row["app_id"] != null)
          userOauthApp.app_id = row["app_id"].ToString();
        if (row["app_key"] != null)
          userOauthApp.app_key = row["app_key"].ToString();
        if (row["remark"] != null)
          userOauthApp.remark = row["remark"].ToString();
        if (row["sort_id"] != null && row["sort_id"].ToString() != "")
          userOauthApp.sort_id = int.Parse(row["sort_id"].ToString());
        if (row["is_lock"] != null && row["is_lock"].ToString() != "")
          userOauthApp.is_lock = int.Parse(row["is_lock"].ToString());
        if (row["api_path"] != null)
          userOauthApp.api_path = row["api_path"].ToString();
      }
      return userOauthApp;
    }
  }
}
