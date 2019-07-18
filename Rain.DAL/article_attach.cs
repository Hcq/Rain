// Decompiled with JetBrains decompiler
// Type: Rain.DAL.article_attach
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
  public class article_attach
  {
    private string databaseprefix;

    public article_attach(string _databaseprefix)
    {
      this.databaseprefix = _databaseprefix;
    }

    private int GetMaxId(OleDbConnection conn, OleDbTransaction trans)
    {
      string SQLString = "select top 1 id from " + this.databaseprefix + "article_attach order by id desc";
      object single = DbHelperOleDb.GetSingle(conn, trans, SQLString);
      if (single == null)
        return 0;
      return int.Parse(single.ToString());
    }

    public bool Exists(int id)
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("select count(1) from " + this.databaseprefix + nameof (article_attach));
      stringBuilder.Append(" where id=@id ");
      OleDbParameter[] oleDbParameterArray = new OleDbParameter[1]
      {
        new OleDbParameter("@id", OleDbType.Integer, 4)
      };
      oleDbParameterArray[0].Value = (object) id;
      return DbHelperOleDb.Exists(stringBuilder.ToString(), oleDbParameterArray);
    }

    public bool ExistsLog(int attach_id, int user_id)
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("select count(1) from " + this.databaseprefix + "user_attach_log");
      stringBuilder.Append(" where attach_id=@attach_id and user_id=@user_id");
      OleDbParameter[] oleDbParameterArray = new OleDbParameter[2]
      {
        new OleDbParameter("@attach_id", OleDbType.Integer, 4),
        new OleDbParameter("@user_id", OleDbType.Integer, 4)
      };
      oleDbParameterArray[0].Value = (object) attach_id;
      oleDbParameterArray[1].Value = (object) user_id;
      return DbHelperOleDb.Exists(stringBuilder.ToString(), oleDbParameterArray);
    }

    public int GetDownNum(int id)
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("select top 1 down_num from " + this.databaseprefix + nameof (article_attach));
      stringBuilder.Append(" where id=" + (object) id);
      string str = Convert.ToString(DbHelperOleDb.GetSingle(stringBuilder.ToString()));
      if (string.IsNullOrEmpty(str))
        return 0;
      return Convert.ToInt32(str);
    }

    public int GetCountNum(int article_id)
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("select sum(down_num) from " + this.databaseprefix + nameof (article_attach));
      stringBuilder.Append(" where article_id=" + (object) article_id);
      string str = Convert.ToString(DbHelperOleDb.GetSingle(stringBuilder.ToString()));
      if (string.IsNullOrEmpty(str))
        return 0;
      return Convert.ToInt32(str);
    }

    public int AddLog(Rain.Model.user_attach_log model)
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
            stringBuilder.Append("insert into " + this.databaseprefix + "user_attach_log(");
            stringBuilder.Append("user_id,user_name,attach_id,file_name,add_time)");
            stringBuilder.Append(" values (");
            stringBuilder.Append("@user_id,@user_name,@attach_id,@file_name,@add_time)");
            OleDbParameter[] oleDbParameterArray = new OleDbParameter[5]
            {
              new OleDbParameter("@user_id", OleDbType.Integer, 4),
              new OleDbParameter("@user_name", OleDbType.VarChar, 100),
              new OleDbParameter("@attach_id", OleDbType.Integer, 4),
              new OleDbParameter("@file_name", OleDbType.VarChar, (int) byte.MaxValue),
              new OleDbParameter("@add_time", OleDbType.Date)
            };
            oleDbParameterArray[0].Value = (object) model.user_id;
            oleDbParameterArray[1].Value = (object) model.user_name;
            oleDbParameterArray[2].Value = (object) model.attach_id;
            oleDbParameterArray[3].Value = (object) model.file_name;
            oleDbParameterArray[4].Value = (object) model.add_time;
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
      stringBuilder.Append("update " + this.databaseprefix + "article_attach set " + strValue);
      stringBuilder.Append(" where id=" + (object) id);
      DbHelperOleDb.ExecuteSql(stringBuilder.ToString());
    }

    public void DeleteList(
      OleDbConnection conn,
      OleDbTransaction trans,
      List<Rain.Model.article_attach> models,
      int article_id)
    {
      StringBuilder stringBuilder1 = new StringBuilder();
      if (models != null)
      {
        foreach (Rain.Model.article_attach model in models)
        {
          if (model.id > 0)
            stringBuilder1.Append(model.id.ToString() + ",");
        }
      }
      string str = Utils.DelLastChar(stringBuilder1.ToString(), ",");
      StringBuilder stringBuilder2 = new StringBuilder();
      stringBuilder2.Append("select id,file_path from " + this.databaseprefix + "article_attach where article_id=" + (object) article_id);
      if (!string.IsNullOrEmpty(str))
        stringBuilder2.Append(" and id not in(" + str + ")");
      foreach (DataRow row in (InternalDataCollectionBase) DbHelperOleDb.Query(conn, trans, stringBuilder2.ToString()).Tables[0].Rows)
      {
        if (DbHelperOleDb.ExecuteSql(conn, trans, "delete from " + this.databaseprefix + "article_attach where id=" + row["id"].ToString()) > 0)
          Utils.DeleteFile(row["file_path"].ToString());
      }
    }

    public void DeleteFile(List<Rain.Model.article_attach> models)
    {
      if (models == null)
        return;
      foreach (Rain.Model.article_attach model in models)
        Utils.DeleteFile(model.file_path);
    }

    public Rain.Model.article_attach GetModel(int id)
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("select top 1 id,article_id,file_name,file_path,file_size,file_ext,down_num,point,add_time from " + this.databaseprefix + "article_attach ");
      stringBuilder.Append(" where id=@id");
      OleDbParameter[] oleDbParameterArray = new OleDbParameter[1]
      {
        new OleDbParameter("@id", OleDbType.Integer, 4)
      };
      oleDbParameterArray[0].Value = (object) id;
      Rain.Model.article_attach articleAttach = new Rain.Model.article_attach();
      DataSet dataSet = DbHelperOleDb.Query(stringBuilder.ToString(), oleDbParameterArray);
      if (dataSet.Tables[0].Rows.Count <= 0)
        return (Rain.Model.article_attach) null;
      if (dataSet.Tables[0].Rows[0][nameof (id)].ToString() != "")
        articleAttach.id = int.Parse(dataSet.Tables[0].Rows[0][nameof (id)].ToString());
      if (dataSet.Tables[0].Rows[0]["article_id"].ToString() != "")
        articleAttach.article_id = int.Parse(dataSet.Tables[0].Rows[0]["article_id"].ToString());
      articleAttach.file_name = dataSet.Tables[0].Rows[0]["file_name"].ToString();
      articleAttach.file_path = dataSet.Tables[0].Rows[0]["file_path"].ToString();
      if (dataSet.Tables[0].Rows[0]["file_size"].ToString() != "")
        articleAttach.file_size = int.Parse(dataSet.Tables[0].Rows[0]["file_size"].ToString());
      articleAttach.file_ext = dataSet.Tables[0].Rows[0]["file_ext"].ToString();
      if (dataSet.Tables[0].Rows[0]["down_num"].ToString() != "")
        articleAttach.down_num = int.Parse(dataSet.Tables[0].Rows[0]["down_num"].ToString());
      if (dataSet.Tables[0].Rows[0]["point"].ToString() != "")
        articleAttach.point = int.Parse(dataSet.Tables[0].Rows[0]["point"].ToString());
      if (dataSet.Tables[0].Rows[0]["add_time"].ToString() != "")
        articleAttach.add_time = DateTime.Parse(dataSet.Tables[0].Rows[0]["add_time"].ToString());
      return articleAttach;
    }

    public List<Rain.Model.article_attach> GetList(int article_id)
    {
      List<Rain.Model.article_attach> articleAttachList = new List<Rain.Model.article_attach>();
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("select id,article_id,file_name,file_path,file_size,file_ext,down_num,point,add_time ");
      stringBuilder.Append(" FROM " + this.databaseprefix + "article_attach ");
      stringBuilder.Append(" where article_id=" + (object) article_id);
      DataTable table = DbHelperOleDb.Query(stringBuilder.ToString()).Tables[0];
      int count = table.Rows.Count;
      if (count > 0)
      {
        for (int index = 0; index < count; ++index)
        {
          Rain.Model.article_attach articleAttach = new Rain.Model.article_attach();
          if (table.Rows[index]["id"] != null && table.Rows[index]["id"].ToString() != "")
            articleAttach.id = int.Parse(table.Rows[index]["id"].ToString());
          if (table.Rows[index][nameof (article_id)] != null && table.Rows[index][nameof (article_id)].ToString() != "")
            articleAttach.article_id = int.Parse(table.Rows[index][nameof (article_id)].ToString());
          if (table.Rows[index]["file_name"] != null && table.Rows[index]["file_name"].ToString() != "")
            articleAttach.file_name = table.Rows[index]["file_name"].ToString();
          if (table.Rows[index]["file_path"] != null && table.Rows[index]["file_path"].ToString() != "")
            articleAttach.file_path = table.Rows[index]["file_path"].ToString();
          if (table.Rows[index]["file_ext"] != null && table.Rows[index]["file_ext"].ToString() != "")
            articleAttach.file_ext = table.Rows[index]["file_ext"].ToString();
          if (table.Rows[index]["file_size"] != null && table.Rows[index]["file_size"].ToString() != "")
            articleAttach.file_size = int.Parse(table.Rows[index]["file_size"].ToString());
          if (table.Rows[index]["down_num"] != null && table.Rows[index]["down_num"].ToString() != "")
            articleAttach.down_num = int.Parse(table.Rows[index]["down_num"].ToString());
          if (table.Rows[index]["point"] != null && table.Rows[index]["point"].ToString() != "")
            articleAttach.point = int.Parse(table.Rows[index]["point"].ToString());
          if (table.Rows[0]["add_time"].ToString() != "")
            articleAttach.add_time = DateTime.Parse(table.Rows[0]["add_time"].ToString());
          articleAttachList.Add(articleAttach);
        }
      }
      return articleAttachList;
    }
  }
}
