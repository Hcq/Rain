// Decompiled with JetBrains decompiler
// Type: Rain.DAL.mail_template
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
  public class mail_template
  {
    private string databaseprefix;

    public mail_template(string _databaseprefix)
    {
      this.databaseprefix = _databaseprefix;
    }

    private int GetMaxId(OleDbConnection conn, OleDbTransaction trans)
    {
      string SQLString = "select top 1 id from " + this.databaseprefix + "mail_template order by id desc";
      object single = DbHelperOleDb.GetSingle(conn, trans, SQLString);
      if (single == null)
        return 0;
      return int.Parse(single.ToString());
    }

    public bool Exists(int id)
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("select count(1) from " + this.databaseprefix + nameof (mail_template));
      stringBuilder.Append(" where id=@id");
      OleDbParameter[] oleDbParameterArray = new OleDbParameter[1]
      {
        new OleDbParameter("@id", OleDbType.Integer, 4)
      };
      oleDbParameterArray[0].Value = (object) id;
      return DbHelperOleDb.Exists(stringBuilder.ToString(), oleDbParameterArray);
    }

    public bool Exists(string call_index)
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("select count(0) from " + this.databaseprefix + nameof (mail_template));
      stringBuilder.Append(" where call_index=@call_index ");
      OleDbParameter[] oleDbParameterArray = new OleDbParameter[1]
      {
        new OleDbParameter("@call_index", OleDbType.VarChar, 50)
      };
      oleDbParameterArray[0].Value = (object) call_index;
      return DbHelperOleDb.Exists(stringBuilder.ToString(), oleDbParameterArray);
    }

    public int Add(Rain.Model.mail_template model)
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
            stringBuilder.Append("insert into " + this.databaseprefix + "mail_template(");
            stringBuilder.Append("title,call_index,maill_title,content)");
            stringBuilder.Append(" values (");
            stringBuilder.Append("@title,@call_index,@maill_title,@content)");
            OleDbParameter[] oleDbParameterArray = new OleDbParameter[4]
            {
              new OleDbParameter("@title", OleDbType.VarChar, 100),
              new OleDbParameter("@call_index", OleDbType.VarChar, 50),
              new OleDbParameter("@maill_title", OleDbType.VarChar, 100),
              new OleDbParameter("@content", OleDbType.VarChar)
            };
            oleDbParameterArray[0].Value = (object) model.title;
            oleDbParameterArray[1].Value = (object) model.call_index;
            oleDbParameterArray[2].Value = (object) model.maill_title;
            oleDbParameterArray[3].Value = (object) model.content;
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

    public bool Update(Rain.Model.mail_template model)
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("update " + this.databaseprefix + "mail_template set ");
      stringBuilder.Append("title=@title,");
      stringBuilder.Append("call_index=@call_index,");
      stringBuilder.Append("maill_title=@maill_title,");
      stringBuilder.Append("content=@content");
      stringBuilder.Append(" where id=@id");
      OleDbParameter[] oleDbParameterArray = new OleDbParameter[5]
      {
        new OleDbParameter("@title", OleDbType.VarChar, 100),
        new OleDbParameter("@call_index", OleDbType.VarChar, 50),
        new OleDbParameter("@maill_title", OleDbType.VarChar, 100),
        new OleDbParameter("@content", OleDbType.VarChar),
        new OleDbParameter("@id", OleDbType.Integer, 4)
      };
      oleDbParameterArray[0].Value = (object) model.title;
      oleDbParameterArray[1].Value = (object) model.call_index;
      oleDbParameterArray[2].Value = (object) model.maill_title;
      oleDbParameterArray[3].Value = (object) model.content;
      oleDbParameterArray[4].Value = (object) model.id;
      return DbHelperOleDb.ExecuteSql(stringBuilder.ToString(), oleDbParameterArray) > 0;
    }

    public bool Delete(int id)
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("delete from " + this.databaseprefix + "mail_template ");
      stringBuilder.Append(" where id=@id");
      OleDbParameter[] oleDbParameterArray = new OleDbParameter[1]
      {
        new OleDbParameter("@id", OleDbType.Integer, 4)
      };
      oleDbParameterArray[0].Value = (object) id;
      return DbHelperOleDb.ExecuteSql(stringBuilder.ToString(), oleDbParameterArray) > 0;
    }

    public Rain.Model.mail_template GetModel(int id)
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("select top 1 id,title,call_index,maill_title,content,is_sys");
      stringBuilder.Append(" from " + this.databaseprefix + nameof (mail_template));
      stringBuilder.Append(" where id=@id");
      OleDbParameter[] oleDbParameterArray = new OleDbParameter[1]
      {
        new OleDbParameter("@id", OleDbType.Integer, 4)
      };
      oleDbParameterArray[0].Value = (object) id;
      DataSet dataSet = DbHelperOleDb.Query(stringBuilder.ToString(), oleDbParameterArray);
      if (dataSet.Tables[0].Rows.Count > 0)
        return this.DataRowToModel(dataSet.Tables[0].Rows[0]);
      return (Rain.Model.mail_template) null;
    }

    public Rain.Model.mail_template GetModel(string call_index)
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("select top 1 id,title,call_index,maill_title,content,is_sys");
      stringBuilder.Append(" from " + this.databaseprefix + nameof (mail_template));
      stringBuilder.Append(" where call_index=@call_index");
      OleDbParameter[] oleDbParameterArray = new OleDbParameter[1]
      {
        new OleDbParameter("@call_index", OleDbType.VarChar, 50)
      };
      oleDbParameterArray[0].Value = (object) call_index;
      DataSet dataSet = DbHelperOleDb.Query(stringBuilder.ToString(), oleDbParameterArray);
      if (dataSet.Tables[0].Rows.Count > 0)
        return this.DataRowToModel(dataSet.Tables[0].Rows[0]);
      return (Rain.Model.mail_template) null;
    }

    public DataSet GetList(int Top, string strWhere, string filedOrder)
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("select ");
      if (Top > 0)
        stringBuilder.Append(" top " + Top.ToString());
      stringBuilder.Append(" id,title,call_index,maill_title,content,is_sys ");
      stringBuilder.Append(" FROM " + this.databaseprefix + "mail_template ");
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
      stringBuilder.Append("select * FROM " + this.databaseprefix + nameof (mail_template));
      if (strWhere.Trim() != "")
        stringBuilder.Append(" where " + strWhere);
      recordCount = Convert.ToInt32(DbHelperOleDb.GetSingle(PagingHelper.CreateCountingSql(stringBuilder.ToString())));
      return DbHelperOleDb.Query(PagingHelper.CreatePagingSql(recordCount, pageSize, pageIndex, stringBuilder.ToString(), filedOrder));
    }

    public Rain.Model.mail_template DataRowToModel(DataRow row)
    {
      Rain.Model.mail_template mailTemplate = new Rain.Model.mail_template();
      if (row != null)
      {
        if (row["id"] != null && row["id"].ToString() != "")
          mailTemplate.id = int.Parse(row["id"].ToString());
        if (row["title"] != null)
          mailTemplate.title = row["title"].ToString();
        if (row["call_index"] != null)
          mailTemplate.call_index = row["call_index"].ToString();
        if (row["maill_title"] != null)
          mailTemplate.maill_title = row["maill_title"].ToString();
        if (row["content"] != null)
          mailTemplate.content = row["content"].ToString();
        if (row["is_sys"] != null && row["is_sys"].ToString() != "")
          mailTemplate.is_sys = int.Parse(row["is_sys"].ToString());
      }
      return mailTemplate;
    }
  }
}
