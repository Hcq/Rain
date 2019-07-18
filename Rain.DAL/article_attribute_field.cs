// Decompiled with JetBrains decompiler
// Type: Rain.DAL.article_attribute_field
// Assembly: Rain.DAL, Version=4.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 34D3CCEA-896B-46C7-93D3-7BA93E9004E2
// Assembly location: C:\Users\Felix.Ho\Desktop\page\bin\Rain.DAL.dll

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Text;
using Rain.Common;
using Rain.DBUtility;

namespace Rain.DAL
{
  public class article_attribute_field
  {
    private string databaseprefix;

    public article_attribute_field(string _databaseprefix)
    {
      this.databaseprefix = _databaseprefix;
    }

    private int GetMaxId(OleDbConnection conn, OleDbTransaction trans)
    {
      string SQLString = "select top 1 id from " + this.databaseprefix + "article_attribute_field order by id desc";
      object single = DbHelperOleDb.GetSingle(conn, trans, SQLString);
      if (single == null)
        return 0;
      return int.Parse(single.ToString());
    }

    public bool Exists(int id)
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("select count(1) from " + this.databaseprefix + nameof (article_attribute_field));
      stringBuilder.Append(" where id=@id ");
      OleDbParameter[] oleDbParameterArray = new OleDbParameter[1]
      {
        new OleDbParameter("@id", OleDbType.Integer, 4)
      };
      oleDbParameterArray[0].Value = (object) id;
      return DbHelperOleDb.Exists(stringBuilder.ToString(), oleDbParameterArray);
    }

    public bool Exists(string column_name)
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("select count(1) from " + this.databaseprefix + nameof (article_attribute_field));
      stringBuilder.Append(" where name=@name ");
      OleDbParameter[] oleDbParameterArray = new OleDbParameter[1]
      {
        new OleDbParameter("@name", OleDbType.VarChar, 100)
      };
      oleDbParameterArray[0].Value = (object) column_name;
      return DbHelperOleDb.Exists(stringBuilder.ToString(), oleDbParameterArray) || DbHelperOleDb.ExitColumnName(this.databaseprefix + "article", column_name);
    }

    public int Add(Rain.Model.article_attribute_field model)
    {
      int maxId;
      using (OleDbConnection oleDbConnection = new OleDbConnection(DbHelperOleDb.connectionString))
      {
        oleDbConnection.Open();
        using (OleDbTransaction trans = oleDbConnection.BeginTransaction())
        {
          try
          {
            StringBuilder stringBuilder1 = new StringBuilder();
            stringBuilder1.Append("insert into " + this.databaseprefix + "article_attribute_field(");
            stringBuilder1.Append("[name],title,control_type,data_type,data_length,data_place,item_option,default_value,is_required,is_password,is_html,editor_type,valid_tip_msg,valid_error_msg,valid_pattern,sort_id,is_sys)");
            stringBuilder1.Append(" values (");
            stringBuilder1.Append("@name,@title,@control_type,@data_type,@data_length,@data_place,@item_option,@default_value,@is_required,@is_password,@is_html,@editor_type,@valid_tip_msg,@valid_error_msg,@valid_pattern,@sort_id,@is_sys)");
            OleDbParameter[] oleDbParameterArray1 = new OleDbParameter[17]
            {
              new OleDbParameter("@name", OleDbType.VarChar, 100),
              new OleDbParameter("@title", OleDbType.VarChar, 100),
              new OleDbParameter("@control_type", OleDbType.VarChar, 50),
              new OleDbParameter("@data_type", OleDbType.VarChar, 50),
              new OleDbParameter("@data_length", OleDbType.Integer, 4),
              new OleDbParameter("@data_place", OleDbType.Integer, 4),
              new OleDbParameter("@item_option", OleDbType.VarChar),
              new OleDbParameter("@default_value", OleDbType.VarChar),
              new OleDbParameter("@is_required", OleDbType.Integer, 4),
              new OleDbParameter("@is_password", OleDbType.Integer, 4),
              new OleDbParameter("@is_html", OleDbType.Integer, 4),
              new OleDbParameter("@editor_type", OleDbType.Integer, 4),
              new OleDbParameter("@valid_tip_msg", OleDbType.VarChar, (int) byte.MaxValue),
              new OleDbParameter("@valid_error_msg", OleDbType.VarChar, (int) byte.MaxValue),
              new OleDbParameter("@valid_pattern", OleDbType.VarChar, 500),
              new OleDbParameter("@sort_id", OleDbType.Integer, 4),
              new OleDbParameter("@is_sys", OleDbType.Integer, 4)
            };
            oleDbParameterArray1[0].Value = (object) model.name;
            oleDbParameterArray1[1].Value = (object) model.title;
            oleDbParameterArray1[2].Value = (object) model.control_type;
            oleDbParameterArray1[3].Value = (object) model.data_type;
            oleDbParameterArray1[4].Value = (object) model.data_length;
            oleDbParameterArray1[5].Value = (object) model.data_place;
            oleDbParameterArray1[6].Value = (object) model.item_option;
            oleDbParameterArray1[7].Value = (object) model.default_value;
            oleDbParameterArray1[8].Value = (object) model.is_required;
            oleDbParameterArray1[9].Value = (object) model.is_password;
            oleDbParameterArray1[10].Value = (object) model.is_html;
            oleDbParameterArray1[11].Value = (object) model.editor_type;
            oleDbParameterArray1[12].Value = (object) model.valid_tip_msg;
            oleDbParameterArray1[13].Value = (object) model.valid_error_msg;
            oleDbParameterArray1[14].Value = (object) model.valid_pattern;
            oleDbParameterArray1[15].Value = (object) model.sort_id;
            oleDbParameterArray1[16].Value = (object) model.is_sys;
            DbHelperOleDb.ExecuteSql(oleDbConnection, trans, stringBuilder1.ToString(), oleDbParameterArray1);
            maxId = this.GetMaxId(oleDbConnection, trans);
            StringBuilder stringBuilder2 = new StringBuilder();
            stringBuilder2.Append("alter table " + this.databaseprefix + "article_attribute_value add " + model.name + " " + model.data_type);
            OleDbParameter[] oleDbParameterArray2 = new OleDbParameter[0];
            DbHelperOleDb.ExecuteSql(oleDbConnection, trans, stringBuilder2.ToString(), oleDbParameterArray2);
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
      stringBuilder.Append("update " + this.databaseprefix + "article_attribute_field set " + strValue);
      stringBuilder.Append(" where id=" + (object) id);
      DbHelperOleDb.ExecuteSql(stringBuilder.ToString());
    }

    public bool Update(Rain.Model.article_attribute_field model)
    {
      using (OleDbConnection connection = new OleDbConnection(DbHelperOleDb.connectionString))
      {
        connection.Open();
        using (OleDbTransaction trans = connection.BeginTransaction())
        {
          try
          {
            StringBuilder stringBuilder1 = new StringBuilder();
            stringBuilder1.Append("update " + this.databaseprefix + "article_attribute_field set ");
            stringBuilder1.Append("[name]=@name,");
            stringBuilder1.Append("title=@title,");
            stringBuilder1.Append("control_type=@control_type,");
            stringBuilder1.Append("data_type=@data_type,");
            stringBuilder1.Append("data_length=@data_length,");
            stringBuilder1.Append("data_place=@data_place,");
            stringBuilder1.Append("item_option=@item_option,");
            stringBuilder1.Append("default_value=@default_value,");
            stringBuilder1.Append("is_required=@is_required,");
            stringBuilder1.Append("is_password=@is_password,");
            stringBuilder1.Append("is_html=@is_html,");
            stringBuilder1.Append("editor_type=@editor_type,");
            stringBuilder1.Append("valid_tip_msg=@valid_tip_msg,");
            stringBuilder1.Append("valid_error_msg=@valid_error_msg,");
            stringBuilder1.Append("valid_pattern=@valid_pattern,");
            stringBuilder1.Append("sort_id=@sort_id,");
            stringBuilder1.Append("is_sys=@is_sys");
            stringBuilder1.Append(" where id=@id");
            OleDbParameter[] oleDbParameterArray = new OleDbParameter[18]
            {
              new OleDbParameter("@name", OleDbType.VarChar, 100),
              new OleDbParameter("@title", OleDbType.VarChar, 100),
              new OleDbParameter("@control_type", OleDbType.VarChar, 50),
              new OleDbParameter("@data_type", OleDbType.VarChar, 50),
              new OleDbParameter("@data_length", OleDbType.Integer, 4),
              new OleDbParameter("@data_place", OleDbType.Integer, 4),
              new OleDbParameter("@item_option", OleDbType.VarChar),
              new OleDbParameter("@default_value", OleDbType.VarChar),
              new OleDbParameter("@is_required", OleDbType.Integer, 4),
              new OleDbParameter("@is_password", OleDbType.Integer, 4),
              new OleDbParameter("@is_html", OleDbType.Integer, 4),
              new OleDbParameter("@editor_type", OleDbType.Integer, 4),
              new OleDbParameter("@valid_tip_msg", OleDbType.VarChar, (int) byte.MaxValue),
              new OleDbParameter("@valid_error_msg", OleDbType.VarChar, (int) byte.MaxValue),
              new OleDbParameter("@valid_pattern", OleDbType.VarChar, 500),
              new OleDbParameter("@sort_id", OleDbType.Integer, 4),
              new OleDbParameter("@is_sys", OleDbType.Integer, 4),
              new OleDbParameter("@id", OleDbType.Integer, 4)
            };
            oleDbParameterArray[0].Value = (object) model.name;
            oleDbParameterArray[1].Value = (object) model.title;
            oleDbParameterArray[2].Value = (object) model.control_type;
            oleDbParameterArray[3].Value = (object) model.data_type;
            oleDbParameterArray[4].Value = (object) model.data_length;
            oleDbParameterArray[5].Value = (object) model.data_place;
            oleDbParameterArray[6].Value = (object) model.item_option;
            oleDbParameterArray[7].Value = (object) model.default_value;
            oleDbParameterArray[8].Value = (object) model.is_required;
            oleDbParameterArray[9].Value = (object) model.is_password;
            oleDbParameterArray[10].Value = (object) model.is_html;
            oleDbParameterArray[11].Value = (object) model.editor_type;
            oleDbParameterArray[12].Value = (object) model.valid_tip_msg;
            oleDbParameterArray[13].Value = (object) model.valid_error_msg;
            oleDbParameterArray[14].Value = (object) model.valid_pattern;
            oleDbParameterArray[15].Value = (object) model.sort_id;
            oleDbParameterArray[16].Value = (object) model.is_sys;
            oleDbParameterArray[17].Value = (object) model.id;
            DbHelperOleDb.ExecuteSql(connection, trans, stringBuilder1.ToString(), oleDbParameterArray);
            StringBuilder stringBuilder2 = new StringBuilder();
            stringBuilder2.Append("alter table " + this.databaseprefix + "article_attribute_value alter column " + model.name + " " + model.data_type);
            DbHelperOleDb.ExecuteSql(connection, trans, stringBuilder2.ToString());
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
      Rain.Model.article_attribute_field model1 = this.GetModel(id);
      using (OleDbConnection oleDbConnection = new OleDbConnection(DbHelperOleDb.connectionString))
      {
        oleDbConnection.Open();
        using (OleDbTransaction trans = oleDbConnection.BeginTransaction())
        {
          try
          {
            StringBuilder stringBuilder1 = new StringBuilder();
            stringBuilder1.Append("select channel_id,field_id from " + this.databaseprefix + "channel_field ");
            stringBuilder1.Append(" where field_id=@field_id");
            OleDbParameter[] oleDbParameterArray1 = new OleDbParameter[1]
            {
              new OleDbParameter("@field_id", OleDbType.Integer, 4)
            };
            oleDbParameterArray1[0].Value = (object) id;
            DataTable table = DbHelperOleDb.Query(oleDbConnection, trans, stringBuilder1.ToString(), oleDbParameterArray1).Tables[0];
            StringBuilder stringBuilder2 = new StringBuilder();
            stringBuilder2.Append("delete from " + this.databaseprefix + "channel_field");
            stringBuilder2.Append(" where field_id=@field_id");
            OleDbParameter[] oleDbParameterArray2 = new OleDbParameter[1]
            {
              new OleDbParameter("@field_id", OleDbType.Integer, 4)
            };
            oleDbParameterArray2[0].Value = (object) id;
            DbHelperOleDb.ExecuteSql(oleDbConnection, trans, stringBuilder2.ToString(), oleDbParameterArray2);
            if (table.Rows.Count > 0)
            {
              foreach (DataRow row in (InternalDataCollectionBase) table.Rows)
              {
                Rain.Model.channel model2 = new channel(this.databaseprefix).GetModel(oleDbConnection, trans, int.Parse(row["channel_id"].ToString()));
                if (model2 != null)
                  new channel(this.databaseprefix).RehabChannelViews(oleDbConnection, trans, model2, model2.name);
              }
            }
            StringBuilder stringBuilder3 = new StringBuilder();
            stringBuilder3.Append("delete from " + this.databaseprefix + "article_attribute_field ");
            stringBuilder3.Append(" where id=@id");
            OleDbParameter[] oleDbParameterArray3 = new OleDbParameter[1]
            {
              new OleDbParameter("@id", OleDbType.Integer, 4)
            };
            oleDbParameterArray3[0].Value = (object) id;
            DbHelperOleDb.ExecuteSql(oleDbConnection, trans, stringBuilder3.ToString(), oleDbParameterArray3);
            DbHelperOleDb.ExecuteSql(oleDbConnection, trans, "alter table " + this.databaseprefix + "article_attribute_value drop column " + model1.name);
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

    public Rain.Model.article_attribute_field GetModel(int id)
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("select top 1 id,[name],title,control_type,data_type,data_length,data_place,item_option,default_value,is_required,is_password,is_html,editor_type,valid_tip_msg,valid_error_msg,valid_pattern,sort_id,is_sys");
      stringBuilder.Append(" from " + this.databaseprefix + "article_attribute_field ");
      stringBuilder.Append(" where id=@id");
      OleDbParameter[] oleDbParameterArray = new OleDbParameter[1]
      {
        new OleDbParameter("@id", OleDbType.Integer, 4)
      };
      oleDbParameterArray[0].Value = (object) id;
      Rain.Model.article_attribute_field articleAttributeField = new Rain.Model.article_attribute_field();
      DataSet dataSet = DbHelperOleDb.Query(stringBuilder.ToString(), oleDbParameterArray);
      if (dataSet.Tables[0].Rows.Count <= 0)
        return (Rain.Model.article_attribute_field) null;
      if (dataSet.Tables[0].Rows[0][nameof (id)].ToString() != "")
        articleAttributeField.id = int.Parse(dataSet.Tables[0].Rows[0][nameof (id)].ToString());
      articleAttributeField.name = dataSet.Tables[0].Rows[0]["name"].ToString();
      articleAttributeField.title = dataSet.Tables[0].Rows[0]["title"].ToString();
      articleAttributeField.control_type = dataSet.Tables[0].Rows[0]["control_type"].ToString();
      articleAttributeField.data_type = dataSet.Tables[0].Rows[0]["data_type"].ToString();
      if (dataSet.Tables[0].Rows[0]["data_length"].ToString() != "")
        articleAttributeField.data_length = int.Parse(dataSet.Tables[0].Rows[0]["data_length"].ToString());
      if (dataSet.Tables[0].Rows[0]["data_place"].ToString() != "")
        articleAttributeField.data_place = int.Parse(dataSet.Tables[0].Rows[0]["data_place"].ToString());
      articleAttributeField.item_option = dataSet.Tables[0].Rows[0]["item_option"].ToString();
      articleAttributeField.default_value = dataSet.Tables[0].Rows[0]["default_value"].ToString();
      if (dataSet.Tables[0].Rows[0]["is_required"].ToString() != "")
        articleAttributeField.is_required = int.Parse(dataSet.Tables[0].Rows[0]["is_required"].ToString());
      if (dataSet.Tables[0].Rows[0]["is_password"].ToString() != "")
        articleAttributeField.is_password = int.Parse(dataSet.Tables[0].Rows[0]["is_password"].ToString());
      if (dataSet.Tables[0].Rows[0]["is_html"].ToString() != "")
        articleAttributeField.is_html = int.Parse(dataSet.Tables[0].Rows[0]["is_html"].ToString());
      if (dataSet.Tables[0].Rows[0]["editor_type"].ToString() != "")
        articleAttributeField.editor_type = int.Parse(dataSet.Tables[0].Rows[0]["editor_type"].ToString());
      articleAttributeField.valid_tip_msg = dataSet.Tables[0].Rows[0]["valid_tip_msg"].ToString();
      articleAttributeField.valid_error_msg = dataSet.Tables[0].Rows[0]["valid_error_msg"].ToString();
      articleAttributeField.valid_pattern = dataSet.Tables[0].Rows[0]["valid_pattern"].ToString();
      if (dataSet.Tables[0].Rows[0]["sort_id"].ToString() != "")
        articleAttributeField.sort_id = int.Parse(dataSet.Tables[0].Rows[0]["sort_id"].ToString());
      if (dataSet.Tables[0].Rows[0]["is_sys"].ToString() != "")
        articleAttributeField.is_sys = int.Parse(dataSet.Tables[0].Rows[0]["is_sys"].ToString());
      return articleAttributeField;
    }

    public DataSet GetList(int channel_id, string strWhere)
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("select dt_article_attribute_field.* ");
      stringBuilder.Append(" FROM " + this.databaseprefix + "article_attribute_field INNER JOIN " + this.databaseprefix + "channel_field ON " + this.databaseprefix + "article_attribute_field.id = " + this.databaseprefix + "channel_field.field_id");
      stringBuilder.Append(" where channel_id=" + (object) channel_id);
      if (strWhere.Trim() != "")
        stringBuilder.Append(" and " + strWhere);
      stringBuilder.Append(" order by sort_id asc," + this.databaseprefix + "article_attribute_field.id desc");
      return DbHelperOleDb.Query(stringBuilder.ToString());
    }

    public DataSet GetList(int Top, string strWhere, string filedOrder)
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("select ");
      if (Top > 0)
        stringBuilder.Append(" top " + Top.ToString());
      stringBuilder.Append(" id,[name],title,control_type,data_type,data_length,data_place,item_option,default_value,is_required,is_password,is_html,editor_type,valid_tip_msg,valid_error_msg,valid_pattern,sort_id,is_sys ");
      stringBuilder.Append(" FROM " + this.databaseprefix + "article_attribute_field ");
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
      stringBuilder.Append("select * FROM " + this.databaseprefix + nameof (article_attribute_field));
      if (strWhere.Trim() != "")
        stringBuilder.Append(" where " + strWhere);
      recordCount = Convert.ToInt32(DbHelperOleDb.GetSingle(PagingHelper.CreateCountingSql(stringBuilder.ToString())));
      return DbHelperOleDb.Query(PagingHelper.CreatePagingSql(recordCount, pageSize, pageIndex, stringBuilder.ToString(), filedOrder));
    }

    public Dictionary<string, string> GetFields(
      int channel_id,
      int article_id,
      string strWhere)
    {
      Dictionary<string, string> dictionary = new Dictionary<string, string>();
      DataTable table = this.GetList(channel_id, strWhere).Tables[0];
      if (table.Rows.Count > 0)
      {
        StringBuilder stringBuilder1 = new StringBuilder();
        foreach (DataRow row in (InternalDataCollectionBase) table.Rows)
          stringBuilder1.Append(row["name"].ToString() + ",");
        StringBuilder stringBuilder2 = new StringBuilder();
        stringBuilder2.Append("select top 1 " + Utils.DelLastComma(stringBuilder1.ToString()) + " from " + this.databaseprefix + "article_attribute_value ");
        stringBuilder2.Append(" where article_id=@article_id ");
        OleDbParameter[] oleDbParameterArray = new OleDbParameter[1]
        {
          new OleDbParameter("@article_id", OleDbType.Integer, 4)
        };
        oleDbParameterArray[0].Value = (object) article_id;
        DataSet dataSet = DbHelperOleDb.Query(stringBuilder2.ToString(), oleDbParameterArray);
        if (dataSet.Tables[0].Rows.Count > 0)
        {
          foreach (DataRow row in (InternalDataCollectionBase) table.Rows)
          {
            if (dataSet.Tables[0].Rows[0][row["name"].ToString()] != null)
              dictionary.Add(row["name"].ToString(), dataSet.Tables[0].Rows[0][row["name"].ToString()].ToString());
            else
              dictionary.Add(row["name"].ToString(), "");
          }
        }
      }
      return dictionary;
    }
  }
}
