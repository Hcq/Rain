// Decompiled with JetBrains decompiler
// Type: Rain.DAL.article_albums
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
  public class article_albums
  {
    private string databaseprefix;

    public article_albums(string _databaseprefix)
    {
      this.databaseprefix = _databaseprefix;
    }

    public List<Rain.Model.article_albums> GetList(int article_id)
    {
      List<Rain.Model.article_albums> articleAlbumsList = new List<Rain.Model.article_albums>();
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("select id,article_id,thumb_path,original_path,remark,add_time ");
      stringBuilder.Append(" FROM " + this.databaseprefix + "article_albums ");
      stringBuilder.Append(" where article_id=" + (object) article_id);
      DataTable table = DbHelperOleDb.Query(stringBuilder.ToString()).Tables[0];
      int count = table.Rows.Count;
      if (count > 0)
      {
        for (int index = 0; index < count; ++index)
        {
          Rain.Model.article_albums articleAlbums = new Rain.Model.article_albums();
          if (table.Rows[index]["id"] != null && table.Rows[index]["id"].ToString() != "")
            articleAlbums.id = int.Parse(table.Rows[index]["id"].ToString());
          if (table.Rows[index][nameof (article_id)] != null && table.Rows[index][nameof (article_id)].ToString() != "")
            articleAlbums.article_id = int.Parse(table.Rows[index][nameof (article_id)].ToString());
          if (table.Rows[index]["thumb_path"] != null && table.Rows[index]["thumb_path"].ToString() != "")
            articleAlbums.thumb_path = table.Rows[index]["thumb_path"].ToString();
          if (table.Rows[index]["original_path"] != null && table.Rows[index]["original_path"].ToString() != "")
            articleAlbums.original_path = table.Rows[index]["original_path"].ToString();
          if (table.Rows[index]["remark"] != null && table.Rows[index]["remark"].ToString() != "")
            articleAlbums.remark = table.Rows[index]["remark"].ToString();
          if (table.Rows[0]["add_time"].ToString() != "")
            articleAlbums.add_time = DateTime.Parse(table.Rows[0]["add_time"].ToString());
          articleAlbumsList.Add(articleAlbums);
        }
      }
      return articleAlbumsList;
    }

    public void DeleteList(
      OleDbConnection conn,
      OleDbTransaction trans,
      List<Rain.Model.article_albums> models,
      int article_id)
    {
      StringBuilder stringBuilder1 = new StringBuilder();
      if (models != null)
      {
        foreach (Rain.Model.article_albums model in models)
        {
          if (model.id > 0)
            stringBuilder1.Append(model.id.ToString() + ",");
        }
      }
      string str = Utils.DelLastChar(stringBuilder1.ToString(), ",");
      StringBuilder stringBuilder2 = new StringBuilder();
      stringBuilder2.Append("select id,thumb_path,original_path from " + this.databaseprefix + "article_albums where article_id=" + (object) article_id);
      if (!string.IsNullOrEmpty(str))
        stringBuilder2.Append(" and id not in(" + str + ")");
      foreach (DataRow row in (InternalDataCollectionBase) DbHelperOleDb.Query(conn, trans, stringBuilder2.ToString()).Tables[0].Rows)
      {
        if (DbHelperOleDb.ExecuteSql(conn, trans, "delete from " + this.databaseprefix + "article_albums where id=" + row["id"].ToString()) > 0)
        {
          Utils.DeleteFile(row["thumb_path"].ToString());
          Utils.DeleteFile(row["original_path"].ToString());
        }
      }
    }

    public void DeleteFile(List<Rain.Model.article_albums> models)
    {
      if (models == null)
        return;
      foreach (Rain.Model.article_albums model in models)
      {
        Utils.DeleteFile(model.thumb_path);
        Utils.DeleteFile(model.original_path);
      }
    }
  }
}
