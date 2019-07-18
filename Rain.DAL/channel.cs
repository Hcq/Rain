// Decompiled with JetBrains decompiler
// Type: Rain.DAL.channel
// Assembly: Rain.DAL, Version=4.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 34D3CCEA-896B-46C7-93D3-7BA93E9004E2
// Assembly location: C:\Users\Felix.Ho\Desktop\page\bin\Rain.DAL.dll

using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Text;
using Rain.Common;
using Rain.DBUtility;
using Rain.Model;

namespace Rain.DAL
{
  public class channel
  {
    private string databaseprefix;

    public channel(string _databaseprefix)
    {
      this.databaseprefix = _databaseprefix;
    }

    private int GetMaxId(OleDbConnection conn, OleDbTransaction trans)
    {
      string SQLString = "select top 1 id from " + this.databaseprefix + "channel order by id desc";
      object single = DbHelperOleDb.GetSingle(conn, trans, SQLString);
      if (single == null)
        return 0;
      return int.Parse(single.ToString());
    }

    public bool Exists(int id)
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("select count(1) from " + this.databaseprefix + nameof (channel));
      stringBuilder.Append(" where id=@id ");
      OleDbParameter[] oleDbParameterArray = new OleDbParameter[1]
      {
        new OleDbParameter("@id", OleDbType.Integer, 4)
      };
      oleDbParameterArray[0].Value = (object) id;
      return DbHelperOleDb.Exists(stringBuilder.ToString(), oleDbParameterArray);
    }

    public bool Exists(string name)
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("select count(1) from " + this.databaseprefix + nameof (channel));
      stringBuilder.Append(" where name=@name ");
      OleDbParameter[] oleDbParameterArray = new OleDbParameter[1]
      {
        new OleDbParameter("@name", OleDbType.VarChar, 50)
      };
      oleDbParameterArray[0].Value = (object) name;
      return DbHelperOleDb.Exists(stringBuilder.ToString(), oleDbParameterArray);
    }

    public int GetCount(string strWhere)
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("select count(*) as H ");
      stringBuilder.Append(" from " + this.databaseprefix + nameof (channel));
      if (strWhere.Trim() != "")
        stringBuilder.Append(" where " + strWhere);
      return Convert.ToInt32(DbHelperOleDb.GetSingle(stringBuilder.ToString()));
    }

    public int Add(Rain.Model.channel model)
    {
      int siteNavId = new channel_site(this.databaseprefix).GetSiteNavId(model.site_id);
      if (siteNavId == 0)
        return 0;
      using (OleDbConnection oleDbConnection = new OleDbConnection(DbHelperOleDb.connectionString))
      {
        oleDbConnection.Open();
        using (OleDbTransaction trans = oleDbConnection.BeginTransaction())
        {
          try
          {
            StringBuilder stringBuilder1 = new StringBuilder();
            stringBuilder1.Append("insert into " + this.databaseprefix + "channel(");
            stringBuilder1.Append("site_id,[name],title,is_albums,is_attach,is_spec,sort_id)");
            stringBuilder1.Append(" values (");
            stringBuilder1.Append("@site_id,@name,@title,@is_albums,@is_attach,@is_spec,@sort_id)");
            OleDbParameter[] oleDbParameterArray1 = new OleDbParameter[7]
            {
              new OleDbParameter("@site_id", OleDbType.Integer, 4),
              new OleDbParameter("@name", OleDbType.VarChar, 50),
              new OleDbParameter("@title", OleDbType.VarChar, 100),
              new OleDbParameter("@is_albums", OleDbType.Integer, 4),
              new OleDbParameter("@is_attach", OleDbType.Integer, 4),
              new OleDbParameter("@is_spec", OleDbType.Integer, 4),
              new OleDbParameter("@sort_id", OleDbType.Integer, 4)
            };
            oleDbParameterArray1[0].Value = (object) model.site_id;
            oleDbParameterArray1[1].Value = (object) model.name;
            oleDbParameterArray1[2].Value = (object) model.title;
            oleDbParameterArray1[3].Value = (object) model.is_albums;
            oleDbParameterArray1[4].Value = (object) model.is_attach;
            oleDbParameterArray1[5].Value = (object) model.is_spec;
            oleDbParameterArray1[6].Value = (object) model.sort_id;
            DbHelperOleDb.ExecuteSql(oleDbConnection, trans, stringBuilder1.ToString(), oleDbParameterArray1);
            model.id = this.GetMaxId(oleDbConnection, trans);
            if (model.channel_fields != null)
            {
              foreach (channel_field channelField in model.channel_fields)
              {
                StringBuilder stringBuilder2 = new StringBuilder();
                stringBuilder2.Append("insert into " + this.databaseprefix + "channel_field(");
                stringBuilder2.Append("channel_id,field_id)");
                stringBuilder2.Append(" values (");
                stringBuilder2.Append("@channel_id,@field_id)");
                OleDbParameter[] oleDbParameterArray2 = new OleDbParameter[2]
                {
                  new OleDbParameter("@channel_id", OleDbType.Integer, 4),
                  new OleDbParameter("@field_id", OleDbType.Integer, 4)
                };
                oleDbParameterArray2[0].Value = (object) model.id;
                oleDbParameterArray2[1].Value = (object) channelField.field_id;
                DbHelperOleDb.ExecuteSql(oleDbConnection, trans, stringBuilder2.ToString(), oleDbParameterArray2);
              }
            }
            StringBuilder stringBuilder3 = new StringBuilder();
            stringBuilder3.Append("CREATE VIEW view_channel_" + model.name + " as");
            stringBuilder3.Append(" SELECT " + this.databaseprefix + "article.*");
            if (model.channel_fields != null)
            {
              foreach (channel_field channelField in model.channel_fields)
              {
                Rain.Model.article_attribute_field model1 = new article_attribute_field(this.databaseprefix).GetModel(channelField.field_id);
                if (model1 != null)
                  stringBuilder3.Append("," + this.databaseprefix + "article_attribute_value." + model1.name);
              }
            }
            stringBuilder3.Append(" FROM " + this.databaseprefix + "article_attribute_value INNER JOIN");
            stringBuilder3.Append(" " + this.databaseprefix + "article ON " + this.databaseprefix + "article_attribute_value.article_id = " + this.databaseprefix + "article.id");
            stringBuilder3.Append(" WHERE " + this.databaseprefix + "article.channel_id=" + (object) model.id);
            DbHelperOleDb.ExecuteSql(oleDbConnection, trans, stringBuilder3.ToString());
            int parent_id = new navigation(this.databaseprefix).Add(oleDbConnection, trans, siteNavId, "channel_" + model.name, model.title, "", model.sort_id, model.id, "Show");
            new navigation(this.databaseprefix).Add(oleDbConnection, trans, parent_id, "channel_" + model.name + "_list", "内容管理", "article/article_list.aspx", 99, model.id, "Show,View,Add,Edit,Delete,Audit");
            new navigation(this.databaseprefix).Add(oleDbConnection, trans, parent_id, "channel_" + model.name + "_category", "栏目类别", "article/category_list.aspx", 100, model.id, "Show,View,Add,Edit,Delete");
            new navigation(this.databaseprefix).Add(oleDbConnection, trans, parent_id, "channel_" + model.name + "_comment", "评论管理", "article/comment_list.aspx", 101, model.id, "Show,View,Delete,Reply");
            trans.Commit();
          }
          catch
          {
            trans.Rollback();
            return 0;
          }
        }
      }
      return model.id;
    }

    public bool Update(Rain.Model.channel model)
    {
      Rain.Model.channel model1 = this.GetModel(model.id);
      int siteNavId = new channel_site(this.databaseprefix).GetSiteNavId(model.site_id);
      if (siteNavId == 0)
        return false;
      using (OleDbConnection oleDbConnection = new OleDbConnection(DbHelperOleDb.connectionString))
      {
        oleDbConnection.Open();
        using (OleDbTransaction trans = oleDbConnection.BeginTransaction())
        {
          try
          {
            StringBuilder stringBuilder1 = new StringBuilder();
            stringBuilder1.Append("update " + this.databaseprefix + "channel set ");
            stringBuilder1.Append("site_id=@site_id,");
            stringBuilder1.Append("[name]=@name,");
            stringBuilder1.Append("title=@title,");
            stringBuilder1.Append("is_albums=@is_albums,");
            stringBuilder1.Append("is_attach=@is_attach,");
            stringBuilder1.Append("is_spec=@is_spec,");
            stringBuilder1.Append("sort_id=@sort_id");
            stringBuilder1.Append(" where id=@id ");
            OleDbParameter[] oleDbParameterArray1 = new OleDbParameter[8]
            {
              new OleDbParameter("@site_id", OleDbType.Integer, 4),
              new OleDbParameter("@name", OleDbType.VarChar, 50),
              new OleDbParameter("@title", OleDbType.VarChar, 100),
              new OleDbParameter("@is_albums", OleDbType.Integer, 4),
              new OleDbParameter("@is_attach", OleDbType.Integer, 4),
              new OleDbParameter("@is_spec", OleDbType.Integer, 4),
              new OleDbParameter("@sort_id", OleDbType.Integer, 4),
              new OleDbParameter("@id", OleDbType.Integer, 4)
            };
            oleDbParameterArray1[0].Value = (object) model.site_id;
            oleDbParameterArray1[1].Value = (object) model.name;
            oleDbParameterArray1[2].Value = (object) model.title;
            oleDbParameterArray1[3].Value = (object) model.is_albums;
            oleDbParameterArray1[4].Value = (object) model.is_attach;
            oleDbParameterArray1[5].Value = (object) model.is_spec;
            oleDbParameterArray1[6].Value = (object) model.sort_id;
            oleDbParameterArray1[7].Value = (object) model.id;
            DbHelperOleDb.ExecuteSql(oleDbConnection, trans, stringBuilder1.ToString(), oleDbParameterArray1);
            this.FieldDelete(oleDbConnection, trans, model.channel_fields, model.id);
            if (model.channel_fields != null)
            {
              using (List<channel_field>.Enumerator enumerator = model.channel_fields.GetEnumerator())
              {
                while (enumerator.MoveNext())
                {
                  channel_field modelt = enumerator.Current;
                  StringBuilder stringBuilder2 = new StringBuilder();
                  channel_field channelField = (channel_field) null;
                  if (model1.channel_fields != null)
                    channelField = model1.channel_fields.Find((Predicate<channel_field>) (p => p.field_id == modelt.field_id));
                  if (channelField == null)
                  {
                    stringBuilder2.Append("insert into " + this.databaseprefix + "channel_field(");
                    stringBuilder2.Append("channel_id,field_id)");
                    stringBuilder2.Append(" values (");
                    stringBuilder2.Append("@channel_id,@field_id)");
                    OleDbParameter[] oleDbParameterArray2 = new OleDbParameter[2]
                    {
                      new OleDbParameter("@channel_id", OleDbType.Integer, 4),
                      new OleDbParameter("@field_id", OleDbType.Integer, 4)
                    };
                    oleDbParameterArray2[0].Value = (object) modelt.channel_id;
                    oleDbParameterArray2[1].Value = (object) modelt.field_id;
                    DbHelperOleDb.ExecuteSql(oleDbConnection, trans, stringBuilder2.ToString(), oleDbParameterArray2);
                  }
                }
              }
            }
            this.RehabChannelViews(oleDbConnection, trans, model, model1.name);
            new navigation(this.databaseprefix).Update(oleDbConnection, trans, "channel_" + model1.name, siteNavId, "channel_" + model.name, model.title, model.sort_id);
            new navigation(this.databaseprefix).Update(oleDbConnection, trans, "channel_" + model1.name + "_list", "channel_" + model.name + "_list");
            new navigation(this.databaseprefix).Update(oleDbConnection, trans, "channel_" + model1.name + "_category", "channel_" + model.name + "_category");
            new navigation(this.databaseprefix).Update(oleDbConnection, trans, "channel_" + model1.name + "_comment", "channel_" + model.name + "_comment");
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
      string channelName = this.GetChannelName(id);
      if (string.IsNullOrEmpty(channelName))
        return false;
      string ids = new navigation(this.databaseprefix).GetIds("channel_" + channelName);
      using (OleDbConnection connection = new OleDbConnection(DbHelperOleDb.connectionString))
      {
        connection.Open();
        using (OleDbTransaction trans = connection.BeginTransaction())
        {
          try
          {
            if (!string.IsNullOrEmpty(ids))
              DbHelperOleDb.ExecuteSql(connection, trans, "delete from " + this.databaseprefix + "navigation where id in(" + ids + ")");
            StringBuilder stringBuilder1 = new StringBuilder();
            stringBuilder1.Append("drop view view_channel_" + channelName);
            DbHelperOleDb.ExecuteSql(connection, trans, stringBuilder1.ToString());
            StringBuilder stringBuilder2 = new StringBuilder();
            stringBuilder2.Append("delete from " + this.databaseprefix + "channel_field ");
            stringBuilder2.Append(" where channel_id=@channel_id ");
            OleDbParameter[] oleDbParameterArray1 = new OleDbParameter[1]
            {
              new OleDbParameter("@channel_id", OleDbType.Integer, 4)
            };
            oleDbParameterArray1[0].Value = (object) id;
            DbHelperOleDb.ExecuteSql(connection, trans, stringBuilder2.ToString(), oleDbParameterArray1);
            StringBuilder stringBuilder3 = new StringBuilder();
            stringBuilder3.Append("delete from " + this.databaseprefix + "channel ");
            stringBuilder3.Append(" where id=@id ");
            OleDbParameter[] oleDbParameterArray2 = new OleDbParameter[1]
            {
              new OleDbParameter("@id", OleDbType.Integer, 4)
            };
            oleDbParameterArray2[0].Value = (object) id;
            DbHelperOleDb.ExecuteSql(connection, trans, stringBuilder3.ToString(), oleDbParameterArray2);
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

    public Rain.Model.channel GetModel(int id)
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("select top 1 id,site_id,[name],title,is_albums,is_attach,is_spec,sort_id from " + this.databaseprefix + "channel ");
      stringBuilder.Append(" where id=@id ");
      OleDbParameter[] oleDbParameterArray = new OleDbParameter[1]
      {
        new OleDbParameter("@id", OleDbType.Integer, 4)
      };
      oleDbParameterArray[0].Value = (object) id;
      Rain.Model.channel channel = new Rain.Model.channel();
      DataSet dataSet = DbHelperOleDb.Query(stringBuilder.ToString(), oleDbParameterArray);
      if (dataSet.Tables[0].Rows.Count > 0)
        return this.DataRowToModel(dataSet.Tables[0].Rows[0]);
      return (Rain.Model.channel) null;
    }

    public Rain.Model.channel GetModel(string channel_name)
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("select top 1 id,site_id,[name],title,is_albums,is_attach,is_spec,sort_id from " + this.databaseprefix + "channel ");
      stringBuilder.Append(" where name=@channel_name ");
      OleDbParameter[] oleDbParameterArray = new OleDbParameter[1]
      {
        new OleDbParameter("@channel_name", OleDbType.VarChar, 50)
      };
      oleDbParameterArray[0].Value = (object) channel_name;
      Rain.Model.channel channel = new Rain.Model.channel();
      DataSet dataSet = DbHelperOleDb.Query(stringBuilder.ToString(), oleDbParameterArray);
      if (dataSet.Tables[0].Rows.Count > 0)
        return this.DataRowToModel(dataSet.Tables[0].Rows[0]);
      return (Rain.Model.channel) null;
    }

    public DataSet GetList(int Top, string strWhere, string filedOrder)
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("select ");
      if (Top > 0)
        stringBuilder.Append(" top " + Top.ToString());
      stringBuilder.Append(" id,site_id,[name],title,is_albums,is_attach,is_spec,sort_id ");
      stringBuilder.Append(" FROM " + this.databaseprefix + "channel ");
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
      stringBuilder.Append("select * FROM " + this.databaseprefix + nameof (channel));
      if (strWhere.Trim() != "")
        stringBuilder.Append(" where " + strWhere);
      recordCount = Convert.ToInt32(DbHelperOleDb.GetSingle(PagingHelper.CreateCountingSql(stringBuilder.ToString())));
      return DbHelperOleDb.Query(PagingHelper.CreatePagingSql(recordCount, pageSize, pageIndex, stringBuilder.ToString(), filedOrder));
    }

    public Rain.Model.channel DataRowToModel(DataRow row)
    {
      Rain.Model.channel channel = new Rain.Model.channel();
      if (row != null)
      {
        if (row["id"] != null && row["id"].ToString() != "")
          channel.id = int.Parse(row["id"].ToString());
        if (row["site_id"] != null && row["site_id"].ToString() != "")
          channel.site_id = int.Parse(row["site_id"].ToString());
        if (row["name"] != null)
          channel.name = row["name"].ToString();
        if (row["title"] != null)
          channel.title = row["title"].ToString();
        if (row["is_albums"] != null && row["is_albums"].ToString() != "")
          channel.is_albums = int.Parse(row["is_albums"].ToString());
        if (row["is_attach"] != null && row["is_attach"].ToString() != "")
          channel.is_attach = int.Parse(row["is_attach"].ToString());
        if (row["is_spec"] != null && row["is_spec"].ToString() != "")
          channel.is_spec = int.Parse(row["is_spec"].ToString());
        if (row["sort_id"] != null && row["sort_id"].ToString() != "")
          channel.sort_id = int.Parse(row["sort_id"].ToString());
        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.Append("select id,channel_id,field_id from " + this.databaseprefix + "channel_field ");
        stringBuilder.Append(" where channel_id=@channel_id ");
        OleDbParameter[] oleDbParameterArray = new OleDbParameter[1]
        {
          new OleDbParameter("@channel_id", OleDbType.Integer, 4)
        };
        oleDbParameterArray[0].Value = (object) channel.id;
        DataSet dataSet = DbHelperOleDb.Query(stringBuilder.ToString(), oleDbParameterArray);
        if (dataSet.Tables[0].Rows.Count > 0)
        {
          int count = dataSet.Tables[0].Rows.Count;
          List<channel_field> channelFieldList = new List<channel_field>();
          for (int index = 0; index < count; ++index)
          {
            channel_field channelField = new channel_field();
            if (dataSet.Tables[0].Rows[index]["id"].ToString() != "")
              channelField.id = int.Parse(dataSet.Tables[0].Rows[index]["id"].ToString());
            if (dataSet.Tables[0].Rows[index]["channel_id"].ToString() != "")
              channelField.channel_id = int.Parse(dataSet.Tables[0].Rows[index]["channel_id"].ToString());
            if (dataSet.Tables[0].Rows[index]["field_id"].ToString() != "")
              channelField.field_id = int.Parse(dataSet.Tables[0].Rows[index]["field_id"].ToString());
            channelFieldList.Add(channelField);
          }
          channel.channel_fields = channelFieldList;
        }
      }
      return channel;
    }

    public Rain.Model.channel DataRowToModel(
      OleDbConnection conn,
      OleDbTransaction trans,
      DataRow row)
    {
      Rain.Model.channel channel = new Rain.Model.channel();
      if (row != null)
      {
        if (row["id"] != null && row["id"].ToString() != "")
          channel.id = int.Parse(row["id"].ToString());
        if (row["site_id"] != null && row["site_id"].ToString() != "")
          channel.site_id = int.Parse(row["site_id"].ToString());
        if (row["name"] != null)
          channel.name = row["name"].ToString();
        if (row["title"] != null)
          channel.title = row["title"].ToString();
        if (row["is_albums"] != null && row["is_albums"].ToString() != "")
          channel.is_albums = int.Parse(row["is_albums"].ToString());
        if (row["is_attach"] != null && row["is_attach"].ToString() != "")
          channel.is_attach = int.Parse(row["is_attach"].ToString());
        if (row["is_spec"] != null && row["is_spec"].ToString() != "")
          channel.is_spec = int.Parse(row["is_spec"].ToString());
        if (row["sort_id"] != null && row["sort_id"].ToString() != "")
          channel.sort_id = int.Parse(row["sort_id"].ToString());
        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.Append("select top 1 id,channel_id,field_id from " + this.databaseprefix + "channel_field ");
        stringBuilder.Append(" where channel_id=@channel_id ");
        OleDbParameter[] oleDbParameterArray = new OleDbParameter[1]
        {
          new OleDbParameter("@channel_id", OleDbType.Integer, 4)
        };
        oleDbParameterArray[0].Value = (object) channel.id;
        DataSet dataSet = DbHelperOleDb.Query(conn, trans, stringBuilder.ToString(), oleDbParameterArray);
        if (dataSet.Tables[0].Rows.Count > 0)
        {
          int count = dataSet.Tables[0].Rows.Count;
          List<channel_field> channelFieldList = new List<channel_field>();
          for (int index = 0; index < count; ++index)
          {
            channel_field channelField = new channel_field();
            if (dataSet.Tables[0].Rows[index]["id"].ToString() != "")
              channelField.id = int.Parse(dataSet.Tables[0].Rows[index]["id"].ToString());
            if (dataSet.Tables[0].Rows[index]["channel_id"].ToString() != "")
              channelField.channel_id = int.Parse(dataSet.Tables[0].Rows[index]["channel_id"].ToString());
            if (dataSet.Tables[0].Rows[index]["field_id"].ToString() != "")
              channelField.field_id = int.Parse(dataSet.Tables[0].Rows[index]["field_id"].ToString());
            channelFieldList.Add(channelField);
          }
          channel.channel_fields = channelFieldList;
        }
      }
      return channel;
    }

    public Rain.Model.channel GetModel(
      OleDbConnection conn,
      OleDbTransaction trans,
      int id)
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("select top 1 id,site_id,[name],title,is_albums,is_attach,is_spec,sort_id from " + this.databaseprefix + "channel ");
      stringBuilder.Append(" where id=@id ");
      OleDbParameter[] oleDbParameterArray = new OleDbParameter[1]
      {
        new OleDbParameter("@id", OleDbType.Integer, 4)
      };
      oleDbParameterArray[0].Value = (object) id;
      Rain.Model.channel channel = new Rain.Model.channel();
      DataSet dataSet = DbHelperOleDb.Query(conn, trans, stringBuilder.ToString(), oleDbParameterArray);
      if (dataSet.Tables[0].Rows.Count > 0)
        return this.DataRowToModel(conn, trans, dataSet.Tables[0].Rows[0]);
      return (Rain.Model.channel) null;
    }

    public void UpdateField(int id, string strValue)
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("update " + this.databaseprefix + "channel set " + strValue);
      stringBuilder.Append(" where id=" + (object) id);
      DbHelperOleDb.ExecuteSql(stringBuilder.ToString());
    }

    public string GetChannelName(int id)
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("select top 1 name from " + this.databaseprefix + nameof (channel));
      stringBuilder.Append(" where id=" + (object) id);
      object single = DbHelperOleDb.GetSingle(stringBuilder.ToString());
      if (single != null)
        return Convert.ToString(single);
      return string.Empty;
    }

    public int GetChannelId(string name)
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("select top 1 id from " + this.databaseprefix + nameof (channel));
      stringBuilder.Append(" where name=@name ");
      OleDbParameter[] oleDbParameterArray = new OleDbParameter[1]
      {
        new OleDbParameter("@name", OleDbType.VarChar, 50)
      };
      oleDbParameterArray[0].Value = (object) name;
      object single = DbHelperOleDb.GetSingle(stringBuilder.ToString(), oleDbParameterArray);
      if (single != null)
        return Convert.ToInt32(single);
      return 0;
    }

    public void RehabChannelViews(
      OleDbConnection conn,
      OleDbTransaction trans,
      Rain.Model.channel model,
      string old_name)
    {
      StringBuilder stringBuilder1 = new StringBuilder();
      stringBuilder1.Append("drop view view_channel_" + old_name);
      DbHelperOleDb.ExecuteSql(conn, trans, stringBuilder1.ToString());
      StringBuilder stringBuilder2 = new StringBuilder();
      stringBuilder2.Append("CREATE VIEW view_channel_" + model.name + " as");
      stringBuilder2.Append(" SELECT " + this.databaseprefix + "article.*");
      if (model.channel_fields != null)
      {
        foreach (channel_field channelField in model.channel_fields)
        {
          Rain.Model.article_attribute_field model1 = new article_attribute_field(this.databaseprefix).GetModel(channelField.field_id);
          if (model1 != null)
            stringBuilder2.Append("," + this.databaseprefix + "article_attribute_value." + model1.name);
        }
      }
      stringBuilder2.Append(" FROM " + this.databaseprefix + "article_attribute_value INNER JOIN");
      stringBuilder2.Append(" " + this.databaseprefix + "article ON " + this.databaseprefix + "article_attribute_value.article_id = " + this.databaseprefix + "article.id");
      stringBuilder2.Append(" WHERE " + this.databaseprefix + "article.channel_id=" + (object) model.id);
      DbHelperOleDb.ExecuteSql(conn, trans, stringBuilder2.ToString());
    }

    private void FieldDelete(
      OleDbConnection conn,
      OleDbTransaction trans,
      List<channel_field> models,
      int channel_id)
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("select id,field_id from " + this.databaseprefix + "channel_field");
      stringBuilder.Append(" where channel_id=" + (object) channel_id);
      IEnumerator enumerator = DbHelperOleDb.Query(conn, trans, stringBuilder.ToString()).Tables[0].Rows.GetEnumerator();
      try
      {
        while (enumerator.MoveNext())
        {
          DataRow dr = (DataRow) enumerator.Current;
          if (models.Find((Predicate<channel_field>) (p => p.field_id == int.Parse(dr["field_id"].ToString()))) == null)
            DbHelperOleDb.ExecuteSql(conn, trans, "delete from " + this.databaseprefix + "channel_field where channel_id=" + (object) channel_id + " and field_id=" + dr["field_id"].ToString());
        }
      }
      finally
      {
        (enumerator as IDisposable)?.Dispose();
      }
    }
  }
}
