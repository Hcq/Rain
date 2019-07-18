// Decompiled with JetBrains decompiler
// Type: Rain.DAL.article_category
// Assembly: Rain.DAL, Version=4.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 34D3CCEA-896B-46C7-93D3-7BA93E9004E2
// Assembly location: C:\Users\Felix.Ho\Desktop\page\bin\Rain.DAL.dll

using System;
using System.Data;
using System.Data.OleDb;
using System.Text;
using Rain.DBUtility;

namespace Rain.DAL
{
  public class article_category
  {
    private string databaseprefix;

    public article_category(string _databaseprefix)
    {
      this.databaseprefix = _databaseprefix;
    }

    private int GetMaxId(OleDbConnection conn, OleDbTransaction trans)
    {
      string SQLString = "select top 1 id from " + this.databaseprefix + "article_category order by id desc";
      object single = DbHelperOleDb.GetSingle(conn, trans, SQLString);
      if (single == null)
        return 0;
      return int.Parse(single.ToString());
    }

    public bool Exists(int id)
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("select count(1) from " + this.databaseprefix + nameof (article_category));
      stringBuilder.Append(" where id=@id");
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
      stringBuilder.Append("select top 1 title from " + this.databaseprefix + nameof (article_category));
      stringBuilder.Append(" where id=" + (object) id);
      string str = Convert.ToString(DbHelperOleDb.GetSingle(stringBuilder.ToString()));
      if (string.IsNullOrEmpty(str))
        return "";
      return str;
    }

    public int Add(Rain.Model.article_category model)
    {
      using (OleDbConnection oleDbConnection = new OleDbConnection(DbHelperOleDb.connectionString))
      {
        oleDbConnection.Open();
        using (OleDbTransaction trans = oleDbConnection.BeginTransaction())
        {
          try
          {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append("insert into " + this.databaseprefix + "article_category(");
            stringBuilder.Append("channel_id,title,call_index,parent_id,class_list,class_layer,sort_id,link_url,img_url,content,seo_title,seo_keywords,seo_description)");
            stringBuilder.Append(" values (");
            stringBuilder.Append("@channel_id,@title,@call_index,@parent_id,@class_list,@class_layer,@sort_id,@link_url,@img_url,@content,@seo_title,@seo_keywords,@seo_description)");
            OleDbParameter[] oleDbParameterArray = new OleDbParameter[13]
            {
              new OleDbParameter("@channel_id", OleDbType.Integer, 4),
              new OleDbParameter("@title", OleDbType.VarChar, 100),
              new OleDbParameter("@call_index", OleDbType.VarChar, 50),
              new OleDbParameter("@parent_id", OleDbType.Integer, 4),
              new OleDbParameter("@class_list", OleDbType.VarChar, 500),
              new OleDbParameter("@class_layer", OleDbType.Integer, 4),
              new OleDbParameter("@sort_id", OleDbType.Integer, 4),
              new OleDbParameter("@link_url", OleDbType.VarChar, (int) byte.MaxValue),
              new OleDbParameter("@img_url", OleDbType.VarChar, (int) byte.MaxValue),
              new OleDbParameter("@content", OleDbType.VarChar),
              new OleDbParameter("@seo_title", OleDbType.VarChar, (int) byte.MaxValue),
              new OleDbParameter("@seo_keywords", OleDbType.VarChar, (int) byte.MaxValue),
              new OleDbParameter("@seo_description", OleDbType.VarChar, (int) byte.MaxValue)
            };
            oleDbParameterArray[0].Value = (object) model.channel_id;
            oleDbParameterArray[1].Value = (object) model.title;
            oleDbParameterArray[2].Value = (object) model.call_index;
            oleDbParameterArray[3].Value = (object) model.parent_id;
            oleDbParameterArray[4].Value = (object) model.class_list;
            oleDbParameterArray[5].Value = (object) model.class_layer;
            oleDbParameterArray[6].Value = (object) model.sort_id;
            oleDbParameterArray[7].Value = (object) model.link_url;
            oleDbParameterArray[8].Value = (object) model.img_url;
            oleDbParameterArray[9].Value = (object) model.content;
            oleDbParameterArray[10].Value = (object) model.seo_title;
            oleDbParameterArray[11].Value = (object) model.seo_keywords;
            oleDbParameterArray[12].Value = (object) model.seo_description;
            DbHelperOleDb.ExecuteSql(oleDbConnection, trans, stringBuilder.ToString(), oleDbParameterArray);
            model.id = this.GetMaxId(oleDbConnection, trans);
            if (model.parent_id > 0)
            {
              Rain.Model.article_category model1 = this.GetModel(oleDbConnection, trans, model.parent_id);
              model.class_list = model1.class_list + (object) model.id + ",";
              model.class_layer = model1.class_layer + 1;
            }
            else
            {
              model.class_list = "," + (object) model.id + ",";
              model.class_layer = 1;
            }
            DbHelperOleDb.ExecuteSql(oleDbConnection, trans, "update " + this.databaseprefix + "article_category set class_list='" + model.class_list + "', class_layer=" + (object) model.class_layer + " where id=" + (object) model.id);
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

    public void UpdateField(int id, string strValue)
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("update " + this.databaseprefix + "article_category set " + strValue);
      stringBuilder.Append(" where id=" + (object) id);
      DbHelperOleDb.ExecuteSql(stringBuilder.ToString());
    }

    public bool Update(Rain.Model.article_category model)
    {
      using (OleDbConnection oleDbConnection = new OleDbConnection(DbHelperOleDb.connectionString))
      {
        oleDbConnection.Open();
        using (OleDbTransaction trans = oleDbConnection.BeginTransaction())
        {
          try
          {
            if (this.IsContainNode(model.id, model.parent_id))
            {
              Rain.Model.article_category model1 = this.GetModel(model.id);
              string str = "," + (object) model.parent_id + ",";
              int num = 1;
              if (model1.parent_id > 0)
              {
                Rain.Model.article_category model2 = this.GetModel(oleDbConnection, trans, model1.parent_id);
                str = model2.class_list + (object) model.parent_id + ",";
                num = model2.class_layer + 1;
              }
              DbHelperOleDb.ExecuteSql(oleDbConnection, trans, "update " + this.databaseprefix + "article_category set parent_id=" + (object) model1.parent_id + ",class_list='" + str + "', class_layer=" + (object) num + " where id=" + (object) model.parent_id);
              this.UpdateChilds(oleDbConnection, trans, model.parent_id);
            }
            if (model.parent_id > 0)
            {
              Rain.Model.article_category model1 = this.GetModel(oleDbConnection, trans, model.parent_id);
              model.class_list = model1.class_list + (object) model.id + ",";
              model.class_layer = model1.class_layer + 1;
            }
            else
            {
              model.class_list = "," + (object) model.id + ",";
              model.class_layer = 1;
            }
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append("update " + this.databaseprefix + "article_category set ");
            stringBuilder.Append("channel_id=@channel_id,");
            stringBuilder.Append("title=@title,");
            stringBuilder.Append("call_index=@call_index,");
            stringBuilder.Append("parent_id=@parent_id,");
            stringBuilder.Append("class_list=@class_list,");
            stringBuilder.Append("class_layer=@class_layer,");
            stringBuilder.Append("sort_id=@sort_id,");
            stringBuilder.Append("link_url=@link_url,");
            stringBuilder.Append("img_url=@img_url,");
            stringBuilder.Append("content=@content,");
            stringBuilder.Append("seo_title=@seo_title,");
            stringBuilder.Append("seo_keywords=@seo_keywords,");
            stringBuilder.Append("seo_description=@seo_description");
            stringBuilder.Append(" where id=@id");
            OleDbParameter[] oleDbParameterArray = new OleDbParameter[14]
            {
              new OleDbParameter("@channel_id", OleDbType.Integer, 4),
              new OleDbParameter("@title", OleDbType.VarChar, 100),
              new OleDbParameter("@call_index", OleDbType.VarChar, 50),
              new OleDbParameter("@parent_id", OleDbType.Integer, 4),
              new OleDbParameter("@class_list", OleDbType.VarChar, 500),
              new OleDbParameter("@class_layer", OleDbType.Integer, 4),
              new OleDbParameter("@sort_id", OleDbType.Integer, 4),
              new OleDbParameter("@link_url", OleDbType.VarChar, (int) byte.MaxValue),
              new OleDbParameter("@img_url", OleDbType.VarChar, (int) byte.MaxValue),
              new OleDbParameter("@content", OleDbType.VarChar),
              new OleDbParameter("@seo_title", OleDbType.VarChar, (int) byte.MaxValue),
              new OleDbParameter("@seo_keywords", OleDbType.VarChar, (int) byte.MaxValue),
              new OleDbParameter("@seo_description", OleDbType.VarChar, (int) byte.MaxValue),
              new OleDbParameter("@id", OleDbType.Integer, 4)
            };
            oleDbParameterArray[0].Value = (object) model.channel_id;
            oleDbParameterArray[1].Value = (object) model.title;
            oleDbParameterArray[2].Value = (object) model.call_index;
            oleDbParameterArray[3].Value = (object) model.parent_id;
            oleDbParameterArray[4].Value = (object) model.class_list;
            oleDbParameterArray[5].Value = (object) model.class_layer;
            oleDbParameterArray[6].Value = (object) model.sort_id;
            oleDbParameterArray[7].Value = (object) model.link_url;
            oleDbParameterArray[8].Value = (object) model.img_url;
            oleDbParameterArray[9].Value = (object) model.content;
            oleDbParameterArray[10].Value = (object) model.seo_title;
            oleDbParameterArray[11].Value = (object) model.seo_keywords;
            oleDbParameterArray[12].Value = (object) model.seo_description;
            oleDbParameterArray[13].Value = (object) model.id;
            DbHelperOleDb.ExecuteSql(oleDbConnection, trans, stringBuilder.ToString(), oleDbParameterArray);
            this.UpdateChilds(oleDbConnection, trans, model.id);
            trans.Commit();
          }
          catch (Exception ex)
          {
            trans.Rollback();
            return false;
          }
        }
      }
      return true;
    }

    public void Delete(int id)
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("delete from " + this.databaseprefix + "article_category ");
      stringBuilder.Append(" where class_list like '%," + (object) id + ",%' ");
      DbHelperOleDb.Query(stringBuilder.ToString());
    }

    public Rain.Model.article_category GetModel(int id)
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("select  top 1 id,channel_id,title,call_index,parent_id,class_list,class_layer,sort_id,link_url,img_url,content,seo_title,seo_keywords,seo_description");
      stringBuilder.Append(" from " + this.databaseprefix + "article_category ");
      stringBuilder.Append(" where id=@id");
      OleDbParameter[] oleDbParameterArray = new OleDbParameter[1]
      {
        new OleDbParameter("@id", OleDbType.Integer, 4)
      };
      oleDbParameterArray[0].Value = (object) id;
      Rain.Model.article_category articleCategory = new Rain.Model.article_category();
      DataSet dataSet = DbHelperOleDb.Query(stringBuilder.ToString(), oleDbParameterArray);
      if (dataSet.Tables[0].Rows.Count > 0)
        return this.DataRowToModel(dataSet.Tables[0].Rows[0]);
      return (Rain.Model.article_category) null;
    }

    public Rain.Model.article_category GetModel(
      OleDbConnection conn,
      OleDbTransaction trans,
      int id)
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("select top 1 id,channel_id,title,call_index,parent_id,class_list,class_layer,sort_id,link_url,img_url,content,seo_title,seo_keywords,seo_description");
      stringBuilder.Append(" from " + this.databaseprefix + "article_category ");
      stringBuilder.Append(" where id=@id");
      OleDbParameter[] oleDbParameterArray = new OleDbParameter[1]
      {
        new OleDbParameter("@id", OleDbType.Integer, 4)
      };
      oleDbParameterArray[0].Value = (object) id;
      Rain.Model.article_category articleCategory = new Rain.Model.article_category();
      DataSet dataSet = DbHelperOleDb.Query(conn, trans, stringBuilder.ToString(), oleDbParameterArray);
      if (dataSet.Tables[0].Rows.Count > 0)
        return this.DataRowToModel(dataSet.Tables[0].Rows[0]);
      return (Rain.Model.article_category) null;
    }

    public DataTable GetChildList(int parent_id, int channel_id)
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("select id,channel_id,title,call_index,parent_id,class_list,class_layer,sort_id,link_url,img_url,content,seo_title,seo_keywords,seo_description");
      stringBuilder.Append(" from " + this.databaseprefix + nameof (article_category));
      stringBuilder.Append(" where channel_id=" + (object) channel_id + " and parent_id=" + (object) parent_id + " order by sort_id asc,id desc");
      return DbHelperOleDb.Query(stringBuilder.ToString()).Tables[0];
    }

    public DataTable GetList(int parent_id, int channel_id)
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("select id,channel_id,title,call_index,parent_id,class_list,class_layer,sort_id,link_url,img_url,content,seo_title,seo_keywords,seo_description");
      stringBuilder.Append(" from " + this.databaseprefix + nameof (article_category));
      stringBuilder.Append(" where channel_id=" + (object) channel_id + " order by sort_id asc,id desc");
      DataTable table = DbHelperOleDb.Query(stringBuilder.ToString()).Tables[0];
      if (table == null)
        return (DataTable) null;
      DataTable newData = table.Clone();
      this.GetChilds(table, newData, parent_id, channel_id);
      return newData;
    }

    public int GetParentId(int id)
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("select top 1 parent_id from " + this.databaseprefix + nameof (article_category));
      stringBuilder.Append(" where id=" + (object) id);
      return Convert.ToInt32(DbHelperOleDb.GetSingle(stringBuilder.ToString()));
    }

    public Rain.Model.article_category DataRowToModel(DataRow row)
    {
      Rain.Model.article_category articleCategory = new Rain.Model.article_category();
      if (row != null)
      {
        if (row["id"] != null && row["id"].ToString() != "")
          articleCategory.id = int.Parse(row["id"].ToString());
        if (row["channel_id"] != null && row["channel_id"].ToString() != "")
          articleCategory.channel_id = int.Parse(row["channel_id"].ToString());
        if (row["title"] != null)
          articleCategory.title = row["title"].ToString();
        if (row["call_index"] != null)
          articleCategory.call_index = row["call_index"].ToString();
        if (row["parent_id"] != null && row["parent_id"].ToString() != "")
          articleCategory.parent_id = int.Parse(row["parent_id"].ToString());
        if (row["class_list"] != null)
          articleCategory.class_list = row["class_list"].ToString();
        if (row["class_layer"] != null && row["class_layer"].ToString() != "")
          articleCategory.class_layer = int.Parse(row["class_layer"].ToString());
        if (row["sort_id"] != null && row["sort_id"].ToString() != "")
          articleCategory.sort_id = int.Parse(row["sort_id"].ToString());
        if (row["link_url"] != null)
          articleCategory.link_url = row["link_url"].ToString();
        if (row["img_url"] != null)
          articleCategory.img_url = row["img_url"].ToString();
        if (row["content"] != null)
          articleCategory.content = row["content"].ToString();
        if (row["seo_title"] != null)
          articleCategory.seo_title = row["seo_title"].ToString();
        if (row["seo_keywords"] != null)
          articleCategory.seo_keywords = row["seo_keywords"].ToString();
        if (row["seo_description"] != null)
          articleCategory.seo_description = row["seo_description"].ToString();
      }
      return articleCategory;
    }

    private void GetChilds(DataTable oldData, DataTable newData, int parent_id, int channel_id)
    {
      DataRow[] dataRowArray = oldData.Select("parent_id=" + (object) parent_id);
      for (int index = 0; index < dataRowArray.Length; ++index)
      {
        DataRow row = newData.NewRow();
        row["id"] = (object) int.Parse(dataRowArray[index]["id"].ToString());
        row[nameof (channel_id)] = (object) int.Parse(dataRowArray[index][nameof (channel_id)].ToString());
        row["title"] = (object) dataRowArray[index]["title"].ToString();
        row["call_index"] = (object) dataRowArray[index]["call_index"].ToString();
        row[nameof (parent_id)] = (object) int.Parse(dataRowArray[index][nameof (parent_id)].ToString());
        row["class_list"] = (object) dataRowArray[index]["class_list"].ToString();
        row["class_layer"] = (object) int.Parse(dataRowArray[index]["class_layer"].ToString());
        row["sort_id"] = (object) int.Parse(dataRowArray[index]["sort_id"].ToString());
        row["link_url"] = (object) dataRowArray[index]["link_url"].ToString();
        row["img_url"] = (object) dataRowArray[index]["img_url"].ToString();
        row["content"] = (object) dataRowArray[index]["content"].ToString();
        row["seo_title"] = (object) dataRowArray[index]["seo_title"].ToString();
        row["seo_keywords"] = (object) dataRowArray[index]["seo_keywords"].ToString();
        row["seo_description"] = (object) dataRowArray[index]["seo_description"].ToString();
        newData.Rows.Add(row);
        this.GetChilds(oldData, newData, int.Parse(dataRowArray[index]["id"].ToString()), channel_id);
      }
    }

    private void UpdateChilds(OleDbConnection conn, OleDbTransaction trans, int parent_id)
    {
      Rain.Model.article_category model = this.GetModel(conn, trans, parent_id);
      if (model == null)
        return;
      string SQLString = "select id from " + this.databaseprefix + "article_category where parent_id=" + (object) parent_id;
      foreach (DataRow row in (InternalDataCollectionBase) DbHelperOleDb.Query(conn, trans, SQLString).Tables[0].Rows)
      {
        int parent_id1 = int.Parse(row["id"].ToString());
        string str = model.class_list + (object) parent_id1 + ",";
        int num = model.class_layer + 1;
        DbHelperOleDb.ExecuteSql(conn, trans, "update " + this.databaseprefix + "article_category set class_list='" + str + "', class_layer=" + (object) num + " where id=" + (object) parent_id1);
        this.UpdateChilds(conn, trans, parent_id1);
      }
    }

    private bool IsContainNode(int id, int parent_id)
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("select count(1) from " + this.databaseprefix + "article_category ");
      stringBuilder.Append(" where class_list like '%," + (object) id + ",%' and id=" + (object) parent_id);
      return DbHelperOleDb.Exists(stringBuilder.ToString());
    }
  }
}
