// Decompiled with JetBrains decompiler
// Type: Rain.DAL.article_comment
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
  public class article_comment
  {
    private string databaseprefix;

    public article_comment(string _databaseprefix)
    {
      this.databaseprefix = _databaseprefix;
    }

    private int GetMaxId(OleDbConnection conn, OleDbTransaction trans)
    {
      string SQLString = "select top 1 id from " + this.databaseprefix + "article_comment order by id desc";
      object single = DbHelperOleDb.GetSingle(conn, trans, SQLString);
      if (single == null)
        return 0;
      return int.Parse(single.ToString());
    }

    public bool Exists(int id)
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("select count(1) from " + this.databaseprefix + nameof (article_comment));
      stringBuilder.Append(" where id=@id ");
      OleDbParameter[] oleDbParameterArray = new OleDbParameter[1]
      {
        new OleDbParameter("@id", OleDbType.Integer, 4)
      };
      oleDbParameterArray[0].Value = (object) id;
      return DbHelperOleDb.Exists(stringBuilder.ToString(), oleDbParameterArray);
    }

    public int GetCount(string strWhere)
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("select count(*) as H ");
      stringBuilder.Append(" from " + this.databaseprefix + "article_comment ");
      if (strWhere.Trim() != "")
        stringBuilder.Append(" where " + strWhere);
      return Convert.ToInt32(DbHelperOleDb.GetSingle(stringBuilder.ToString()));
    }

    public int Add(Rain.Model.article_comment model)
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
            stringBuilder.Append("insert into " + this.databaseprefix + "article_comment(");
            stringBuilder.Append("channel_id,article_id,parent_id,user_id,user_name,user_ip,content,is_lock,add_time,is_reply,reply_content,reply_time)");
            stringBuilder.Append(" values (");
            stringBuilder.Append("@channel_id,@article_id,@parent_id,@user_id,@user_name,@user_ip,@content,@is_lock,@add_time,@is_reply,@reply_content,@reply_time)");
            OleDbParameter[] oleDbParameterArray = new OleDbParameter[12]
            {
              new OleDbParameter("@channel_id", OleDbType.Integer, 4),
              new OleDbParameter("@article_id", OleDbType.Integer, 4),
              new OleDbParameter("@parent_id", OleDbType.Integer, 4),
              new OleDbParameter("@user_id", OleDbType.Integer, 4),
              new OleDbParameter("@user_name", OleDbType.VarChar, 100),
              new OleDbParameter("@user_ip", OleDbType.VarChar, (int) byte.MaxValue),
              new OleDbParameter("@content", OleDbType.VarChar),
              new OleDbParameter("@is_lock", OleDbType.Integer, 4),
              new OleDbParameter("@add_time", OleDbType.Date),
              new OleDbParameter("@is_reply", OleDbType.Integer, 4),
              new OleDbParameter("@reply_content", OleDbType.VarChar),
              new OleDbParameter("@reply_time", OleDbType.Date)
            };
            oleDbParameterArray[0].Value = (object) model.channel_id;
            oleDbParameterArray[1].Value = (object) model.article_id;
            oleDbParameterArray[2].Value = (object) model.parent_id;
            oleDbParameterArray[3].Value = (object) model.user_id;
            oleDbParameterArray[4].Value = (object) model.user_name;
            oleDbParameterArray[5].Value = (object) model.user_ip;
            oleDbParameterArray[6].Value = (object) model.content;
            oleDbParameterArray[7].Value = (object) model.is_lock;
            oleDbParameterArray[8].Value = (object) model.add_time;
            oleDbParameterArray[9].Value = (object) model.is_reply;
            if (model.reply_content != null)
              oleDbParameterArray[10].Value = (object) model.reply_content;
            else
              oleDbParameterArray[10].Value = (object) DBNull.Value;
            if (model.reply_time.HasValue)
              oleDbParameterArray[11].Value = (object) model.reply_time;
            else
              oleDbParameterArray[11].Value = (object) DBNull.Value;
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

    public void UpdateField(int id, string strValue)
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("update " + this.databaseprefix + "article_comment set " + strValue);
      stringBuilder.Append(" where id=" + (object) id);
      DbHelperOleDb.ExecuteSql(stringBuilder.ToString());
    }

    public bool Update(Rain.Model.article_comment model)
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("update " + this.databaseprefix + "article_comment set ");
      stringBuilder.Append("channel_id=@channel_id,");
      stringBuilder.Append("article_id=@article_id,");
      stringBuilder.Append("parent_id=@parent_id,");
      stringBuilder.Append("user_id=@user_id,");
      stringBuilder.Append("user_name=@user_name,");
      stringBuilder.Append("user_ip=@user_ip,");
      stringBuilder.Append("content=@content,");
      stringBuilder.Append("is_lock=@is_lock,");
      stringBuilder.Append("add_time=@add_time,");
      stringBuilder.Append("is_reply=@is_reply,");
      stringBuilder.Append("reply_content=@reply_content,");
      stringBuilder.Append("reply_time=@reply_time");
      stringBuilder.Append(" where id=@id");
      OleDbParameter[] oleDbParameterArray = new OleDbParameter[13]
      {
        new OleDbParameter("@channel_id", OleDbType.Integer, 4),
        new OleDbParameter("@article_id", OleDbType.Integer, 4),
        new OleDbParameter("@parent_id", OleDbType.Integer, 4),
        new OleDbParameter("@user_id", OleDbType.Integer, 4),
        new OleDbParameter("@user_name", OleDbType.VarChar, 100),
        new OleDbParameter("@user_ip", OleDbType.VarChar, (int) byte.MaxValue),
        new OleDbParameter("@content", OleDbType.VarChar),
        new OleDbParameter("@is_lock", OleDbType.Integer, 4),
        new OleDbParameter("@add_time", OleDbType.Date),
        new OleDbParameter("@is_reply", OleDbType.Integer, 4),
        new OleDbParameter("@reply_content", OleDbType.VarChar),
        new OleDbParameter("@reply_time", OleDbType.Date),
        new OleDbParameter("@id", OleDbType.Integer, 4)
      };
      oleDbParameterArray[0].Value = (object) model.channel_id;
      oleDbParameterArray[1].Value = (object) model.article_id;
      oleDbParameterArray[2].Value = (object) model.parent_id;
      oleDbParameterArray[3].Value = (object) model.user_id;
      oleDbParameterArray[4].Value = (object) model.user_name;
      oleDbParameterArray[5].Value = (object) model.user_ip;
      oleDbParameterArray[6].Value = (object) model.content;
      oleDbParameterArray[7].Value = (object) model.is_lock;
      oleDbParameterArray[8].Value = (object) model.add_time;
      oleDbParameterArray[9].Value = (object) model.is_reply;
      if (model.reply_content != null)
        oleDbParameterArray[10].Value = (object) model.reply_content;
      else
        oleDbParameterArray[10].Value = (object) DBNull.Value;
      if (model.reply_time.HasValue)
        oleDbParameterArray[11].Value = (object) model.reply_time;
      else
        oleDbParameterArray[11].Value = (object) DBNull.Value;
      oleDbParameterArray[12].Value = (object) model.id;
      return DbHelperOleDb.ExecuteSql(stringBuilder.ToString(), oleDbParameterArray) > 0;
    }

    public bool Delete(int id)
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("delete from " + this.databaseprefix + "article_comment ");
      stringBuilder.Append(" where id=@id");
      OleDbParameter[] oleDbParameterArray = new OleDbParameter[1]
      {
        new OleDbParameter("@id", OleDbType.Integer, 4)
      };
      oleDbParameterArray[0].Value = (object) id;
      return DbHelperOleDb.ExecuteSql(stringBuilder.ToString(), oleDbParameterArray) > 0;
    }

    public Rain.Model.article_comment GetModel(int id)
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("select  top 1 id,channel_id,article_id,parent_id,user_id,user_name,user_ip,content,is_lock,add_time,is_reply,reply_content,reply_time");
      stringBuilder.Append(" from " + this.databaseprefix + "article_comment ");
      stringBuilder.Append(" where id=@id");
      OleDbParameter[] oleDbParameterArray = new OleDbParameter[1]
      {
        new OleDbParameter("@id", OleDbType.Integer, 4)
      };
      oleDbParameterArray[0].Value = (object) id;
      Rain.Model.article_comment articleComment = new Rain.Model.article_comment();
      DataSet dataSet = DbHelperOleDb.Query(stringBuilder.ToString(), oleDbParameterArray);
      if (dataSet.Tables[0].Rows.Count > 0)
        return this.DataRowToModel(dataSet.Tables[0].Rows[0]);
      return (Rain.Model.article_comment) null;
    }

    public Rain.Model.article_comment DataRowToModel(DataRow row)
    {
      Rain.Model.article_comment articleComment = new Rain.Model.article_comment();
      if (row != null)
      {
        if (row["id"] != null && row["id"].ToString() != "")
          articleComment.id = int.Parse(row["id"].ToString());
        if (row["channel_id"] != null && row["channel_id"].ToString() != "")
          articleComment.channel_id = int.Parse(row["channel_id"].ToString());
        if (row["article_id"] != null && row["article_id"].ToString() != "")
          articleComment.article_id = int.Parse(row["article_id"].ToString());
        if (row["parent_id"] != null && row["parent_id"].ToString() != "")
          articleComment.parent_id = int.Parse(row["parent_id"].ToString());
        if (row["user_id"] != null && row["user_id"].ToString() != "")
          articleComment.user_id = int.Parse(row["user_id"].ToString());
        if (row["user_name"] != null)
          articleComment.user_name = row["user_name"].ToString();
        if (row["user_ip"] != null)
          articleComment.user_ip = row["user_ip"].ToString();
        if (row["content"] != null)
          articleComment.content = row["content"].ToString();
        if (row["is_lock"] != null && row["is_lock"].ToString() != "")
          articleComment.is_lock = int.Parse(row["is_lock"].ToString());
        if (row["add_time"] != null && row["add_time"].ToString() != "")
          articleComment.add_time = DateTime.Parse(row["add_time"].ToString());
        if (row["is_reply"] != null && row["is_reply"].ToString() != "")
          articleComment.is_reply = int.Parse(row["is_reply"].ToString());
        if (row["reply_content"] != null)
          articleComment.reply_content = row["reply_content"].ToString();
        if (row["reply_time"] != null && row["reply_time"].ToString() != "")
          articleComment.reply_time = new DateTime?(DateTime.Parse(row["reply_time"].ToString()));
      }
      return articleComment;
    }

    public DataSet GetList(int Top, string strWhere, string filedOrder)
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("select ");
      if (Top > 0)
        stringBuilder.Append(" top " + Top.ToString());
      stringBuilder.Append(" id,channel_id,article_id,parent_id,user_id,user_name,user_ip,content,is_lock,add_time,is_reply,reply_content,reply_time ");
      stringBuilder.Append(" FROM " + this.databaseprefix + "article_comment ");
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
      stringBuilder.Append("select * FROM " + this.databaseprefix + nameof (article_comment));
      if (strWhere.Trim() != "")
        stringBuilder.Append(" where " + strWhere);
      recordCount = Convert.ToInt32(DbHelperOleDb.GetSingle(PagingHelper.CreateCountingSql(stringBuilder.ToString())));
      return DbHelperOleDb.Query(PagingHelper.CreatePagingSql(recordCount, pageSize, pageIndex, stringBuilder.ToString(), filedOrder));
    }
  }
}
