// Decompiled with JetBrains decompiler
// Type: Rain.DAL.user_message
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
  public class user_message
  {
    private string databaseprefix;

    public user_message(string _databaseprefix)
    {
      this.databaseprefix = _databaseprefix;
    }

    private int GetMaxId(OleDbConnection conn, OleDbTransaction trans)
    {
      string SQLString = "select top 1 id from " + this.databaseprefix + "user_message order by id desc";
      object single = DbHelperOleDb.GetSingle(conn, trans, SQLString);
      if (single == null)
        return 0;
      return int.Parse(single.ToString());
    }

    public bool Exists(int id)
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("select count(1) from " + this.databaseprefix + nameof (user_message));
      stringBuilder.Append(" where id=@id");
      OleDbParameter[] oleDbParameterArray = new OleDbParameter[1]
      {
        new OleDbParameter("@id", OleDbType.Integer, 4)
      };
      oleDbParameterArray[0].Value = (object) id;
      return DbHelperOleDb.Exists(stringBuilder.ToString(), oleDbParameterArray);
    }

    public int Add(Rain.Model.user_message model)
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
            stringBuilder.Append("insert into " + this.databaseprefix + "user_message(");
            stringBuilder.Append("[type],post_user_name,accept_user_name,is_read,title,content,post_time,read_time)");
            stringBuilder.Append(" values (");
            stringBuilder.Append("@type,@post_user_name,@accept_user_name,@is_read,@title,@content,@post_time,@read_time)");
            OleDbParameter[] oleDbParameterArray = new OleDbParameter[8]
            {
              new OleDbParameter("@type", OleDbType.Integer, 4),
              new OleDbParameter("@post_user_name", OleDbType.VarChar, 100),
              new OleDbParameter("@accept_user_name", OleDbType.VarChar, 100),
              new OleDbParameter("@is_read", OleDbType.Integer, 4),
              new OleDbParameter("@title", OleDbType.VarChar, 100),
              new OleDbParameter("@content", OleDbType.VarChar),
              new OleDbParameter("@post_time", OleDbType.Date),
              new OleDbParameter("@read_time", OleDbType.Date)
            };
            oleDbParameterArray[0].Value = (object) model.type;
            oleDbParameterArray[1].Value = (object) model.post_user_name;
            oleDbParameterArray[2].Value = (object) model.accept_user_name;
            oleDbParameterArray[3].Value = (object) model.is_read;
            oleDbParameterArray[4].Value = (object) model.title;
            oleDbParameterArray[5].Value = (object) model.content;
            oleDbParameterArray[6].Value = (object) model.post_time;
            if (model.read_time.HasValue)
              oleDbParameterArray[7].Value = (object) model.read_time;
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

    public bool Update(Rain.Model.user_message model)
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("update " + this.databaseprefix + "user_message set ");
      stringBuilder.Append("[type]=@type,");
      stringBuilder.Append("post_user_name=@post_user_name,");
      stringBuilder.Append("accept_user_name=@accept_user_name,");
      stringBuilder.Append("is_read=@is_read,");
      stringBuilder.Append("title=@title,");
      stringBuilder.Append("content=@content,");
      stringBuilder.Append("post_time=@post_time,");
      stringBuilder.Append("read_time=@read_time");
      stringBuilder.Append(" where id=@id");
      OleDbParameter[] oleDbParameterArray = new OleDbParameter[9]
      {
        new OleDbParameter("@type", OleDbType.Integer, 4),
        new OleDbParameter("@post_user_name", OleDbType.VarChar, 100),
        new OleDbParameter("@accept_user_name", OleDbType.VarChar, 100),
        new OleDbParameter("@is_read", OleDbType.Integer, 4),
        new OleDbParameter("@title", OleDbType.VarChar, 100),
        new OleDbParameter("@content", (object) SqlDbType.NText),
        new OleDbParameter("@post_time", OleDbType.Date),
        new OleDbParameter("@read_time", OleDbType.Date),
        new OleDbParameter("@id", OleDbType.Integer, 4)
      };
      oleDbParameterArray[0].Value = (object) model.type;
      oleDbParameterArray[1].Value = (object) model.post_user_name;
      oleDbParameterArray[2].Value = (object) model.accept_user_name;
      oleDbParameterArray[3].Value = (object) model.is_read;
      oleDbParameterArray[4].Value = (object) model.title;
      oleDbParameterArray[5].Value = (object) model.content;
      oleDbParameterArray[6].Value = (object) model.post_time;
      if (model.read_time.HasValue)
        oleDbParameterArray[7].Value = (object) model.read_time;
      else
        oleDbParameterArray[7].Value = (object) DBNull.Value;
      oleDbParameterArray[8].Value = (object) model.id;
      return DbHelperOleDb.ExecuteSql(stringBuilder.ToString(), oleDbParameterArray) > 0;
    }

    public bool Delete(int id)
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("delete from " + this.databaseprefix + "user_message ");
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
      stringBuilder.Append("delete from " + this.databaseprefix + "user_message ");
      stringBuilder.Append(" where id=@id and (post_user_name=@post_user_name or accept_user_name=@accept_user_name)");
      OleDbParameter[] oleDbParameterArray = new OleDbParameter[3]
      {
        new OleDbParameter("@id", OleDbType.Integer, 4),
        new OleDbParameter("@post_user_name", OleDbType.VarChar, 100),
        new OleDbParameter("@accept_user_name", OleDbType.VarChar, 100)
      };
      oleDbParameterArray[0].Value = (object) id;
      oleDbParameterArray[1].Value = (object) user_name;
      oleDbParameterArray[2].Value = (object) user_name;
      return DbHelperOleDb.ExecuteSql(stringBuilder.ToString(), oleDbParameterArray) > 0;
    }

    public Rain.Model.user_message GetModel(int id)
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("select  top 1 id,[type],post_user_name,accept_user_name,is_read,title,content,post_time,read_time from " + this.databaseprefix + "user_message ");
      stringBuilder.Append(" where id=@id");
      OleDbParameter[] oleDbParameterArray = new OleDbParameter[1]
      {
        new OleDbParameter("@id", OleDbType.Integer, 4)
      };
      oleDbParameterArray[0].Value = (object) id;
      DataSet dataSet = DbHelperOleDb.Query(stringBuilder.ToString(), oleDbParameterArray);
      if (dataSet.Tables[0].Rows.Count > 0)
        return this.DataRowToModel(dataSet.Tables[0].Rows[0]);
      return (Rain.Model.user_message) null;
    }

    public DataSet GetList(int Top, string strWhere, string filedOrder)
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("select ");
      if (Top > 0)
        stringBuilder.Append(" top " + Top.ToString());
      stringBuilder.Append(" id,[type],post_user_name,accept_user_name,is_read,title,content,post_time,read_time ");
      stringBuilder.Append(" FROM " + this.databaseprefix + "user_message ");
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
      stringBuilder.Append("select * FROM " + this.databaseprefix + nameof (user_message));
      if (strWhere.Trim() != "")
        stringBuilder.Append(" where " + strWhere);
      recordCount = Convert.ToInt32(DbHelperOleDb.GetSingle(PagingHelper.CreateCountingSql(stringBuilder.ToString())));
      return DbHelperOleDb.Query(PagingHelper.CreatePagingSql(recordCount, pageSize, pageIndex, stringBuilder.ToString(), filedOrder));
    }

    public int GetCount(string strWhere)
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("select count(*) as H ");
      stringBuilder.Append(" from " + this.databaseprefix + "user_message ");
      if (strWhere.Trim() != "")
        stringBuilder.Append(" where " + strWhere);
      return Convert.ToInt32(DbHelperOleDb.GetSingle(stringBuilder.ToString()));
    }

    public void UpdateField(int id, string strValue)
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("update " + this.databaseprefix + "user_message set " + strValue);
      stringBuilder.Append(" where id=" + (object) id);
      DbHelperOleDb.ExecuteSql(stringBuilder.ToString());
    }

    public Rain.Model.user_message DataRowToModel(DataRow row)
    {
      Rain.Model.user_message userMessage = new Rain.Model.user_message();
      if (row != null)
      {
        if (row["id"] != null && row["id"].ToString() != "")
          userMessage.id = int.Parse(row["id"].ToString());
        if (row["type"] != null && row["type"].ToString() != "")
          userMessage.type = int.Parse(row["type"].ToString());
        if (row["post_user_name"] != null)
          userMessage.post_user_name = row["post_user_name"].ToString();
        if (row["accept_user_name"] != null)
          userMessage.accept_user_name = row["accept_user_name"].ToString();
        if (row["is_read"] != null && row["is_read"].ToString() != "")
          userMessage.is_read = int.Parse(row["is_read"].ToString());
        if (row["title"] != null)
          userMessage.title = row["title"].ToString();
        if (row["content"] != null)
          userMessage.content = row["content"].ToString();
        if (row["post_time"] != null && row["post_time"].ToString() != "")
          userMessage.post_time = DateTime.Parse(row["post_time"].ToString());
        if (row["read_time"] != null && row["read_time"].ToString() != "")
          userMessage.read_time = new DateTime?(DateTime.Parse(row["read_time"].ToString()));
      }
      return userMessage;
    }
  }
}
