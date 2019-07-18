// Decompiled with JetBrains decompiler
// Type: Rain.DAL.article
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

namespace Rain.DAL
{
  public class article
  {
    private string databaseprefix;

    public article(string _databaseprefix)
    {
      this.databaseprefix = _databaseprefix;
    }

    private int GetMaxId(OleDbConnection conn, OleDbTransaction trans)
    {
      string SQLString = "select top 1 id from " + this.databaseprefix + "article order by id desc";
      object single = DbHelperOleDb.GetSingle(conn, trans, SQLString);
      if (single == null)
        return 0;
      return int.Parse(single.ToString());
    }

    public bool Exists(int id)
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("select count(1) from " + this.databaseprefix + nameof (article));
      stringBuilder.Append(" where id=@id ");
      OleDbParameter[] oleDbParameterArray = new OleDbParameter[1]
      {
        new OleDbParameter("@id", OleDbType.Integer, 4)
      };
      oleDbParameterArray[0].Value = (object) id;
      return DbHelperOleDb.Exists(stringBuilder.ToString(), oleDbParameterArray);
    }

    public bool Exists(string call_index)
    {
      if (string.IsNullOrEmpty(call_index))
        return false;
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("select count(1) from " + this.databaseprefix + nameof (article));
      stringBuilder.Append(" where call_index=@call_index ");
      OleDbParameter[] oleDbParameterArray = new OleDbParameter[1]
      {
        new OleDbParameter("@call_index", OleDbType.VarChar, 50)
      };
      oleDbParameterArray[0].Value = (object) call_index;
      return DbHelperOleDb.Exists(stringBuilder.ToString(), oleDbParameterArray);
    }

    public int Add(Rain.Model.article model)
    {
      using (OleDbConnection oleDbConnection = new OleDbConnection(DbHelperOleDb.connectionString))
      {
        oleDbConnection.Open();
        using (OleDbTransaction trans = oleDbConnection.BeginTransaction())
        {
          try
          {
            StringBuilder stringBuilder1 = new StringBuilder();
            stringBuilder1.Append("insert into " + this.databaseprefix + "article(");
            stringBuilder1.Append("channel_id,category_id,call_index,title,link_url,img_url,seo_title,seo_keywords,seo_description,zhaiyao,content,sort_id,click,status,is_msg,is_top,is_red,is_hot,is_slide,is_sys,user_name,add_time,update_time)");
            stringBuilder1.Append(" values (");
            stringBuilder1.Append("@channel_id,@category_id,@call_index,@title,@link_url,@img_url,@seo_title,@seo_keywords,@seo_description,@zhaiyao,@content,@sort_id,@click,@status,@is_msg,@is_top,@is_red,@is_hot,@is_slide,@is_sys,@user_name,@add_time,@update_time)");
            OleDbParameter[] oleDbParameterArray1 = new OleDbParameter[23]
            {
              new OleDbParameter("@channel_id", OleDbType.Integer, 4),
              new OleDbParameter("@category_id", OleDbType.Integer, 4),
              new OleDbParameter("@call_index", OleDbType.VarChar, 50),
              new OleDbParameter("@title", OleDbType.VarChar, 100),
              new OleDbParameter("@link_url", OleDbType.VarChar, (int) byte.MaxValue),
              new OleDbParameter("@img_url", OleDbType.VarChar, (int) byte.MaxValue),
              new OleDbParameter("@seo_title", OleDbType.VarChar, (int) byte.MaxValue),
              new OleDbParameter("@seo_keywords", OleDbType.VarChar, (int) byte.MaxValue),
              new OleDbParameter("@seo_description", OleDbType.VarChar, (int) byte.MaxValue),
              new OleDbParameter("@zhaiyao", OleDbType.VarChar, (int) byte.MaxValue),
              new OleDbParameter("@content", (object) SqlDbType.NText),
              new OleDbParameter("@sort_id", OleDbType.Integer, 4),
              new OleDbParameter("@click", OleDbType.Integer, 4),
              new OleDbParameter("@status", OleDbType.Integer, 4),
              new OleDbParameter("@is_msg", OleDbType.Integer, 4),
              new OleDbParameter("@is_top", OleDbType.Integer, 4),
              new OleDbParameter("@is_red", OleDbType.Integer, 4),
              new OleDbParameter("@is_hot", OleDbType.Integer, 4),
              new OleDbParameter("@is_slide", OleDbType.Integer, 4),
              new OleDbParameter("@is_sys", OleDbType.Integer, 4),
              new OleDbParameter("@user_name", OleDbType.VarChar, 100),
              new OleDbParameter("@add_time", OleDbType.Date),
              new OleDbParameter("@update_time", OleDbType.Date)
            };
            oleDbParameterArray1[0].Value = (object) model.channel_id;
            oleDbParameterArray1[1].Value = (object) model.category_id;
            oleDbParameterArray1[2].Value = (object) model.call_index;
            oleDbParameterArray1[3].Value = (object) model.title;
            oleDbParameterArray1[4].Value = (object) model.link_url;
            oleDbParameterArray1[5].Value = (object) model.img_url;
            oleDbParameterArray1[6].Value = (object) model.seo_title;
            oleDbParameterArray1[7].Value = (object) model.seo_keywords;
            oleDbParameterArray1[8].Value = (object) model.seo_description;
            oleDbParameterArray1[9].Value = (object) model.zhaiyao;
            oleDbParameterArray1[10].Value = (object) model.content;
            oleDbParameterArray1[11].Value = (object) model.sort_id;
            oleDbParameterArray1[12].Value = (object) model.click;
            oleDbParameterArray1[13].Value = (object) model.status;
            oleDbParameterArray1[14].Value = (object) model.is_msg;
            oleDbParameterArray1[15].Value = (object) model.is_top;
            oleDbParameterArray1[16].Value = (object) model.is_red;
            oleDbParameterArray1[17].Value = (object) model.is_hot;
            oleDbParameterArray1[18].Value = (object) model.is_slide;
            oleDbParameterArray1[19].Value = (object) model.is_sys;
            oleDbParameterArray1[20].Value = (object) model.user_name;
            oleDbParameterArray1[21].Value = (object) model.add_time;
            if (model.update_time.HasValue)
              oleDbParameterArray1[22].Value = (object) model.update_time;
            else
              oleDbParameterArray1[22].Value = (object) DBNull.Value;
            DbHelperOleDb.ExecuteSql(oleDbConnection, trans, stringBuilder1.ToString(), oleDbParameterArray1);
            model.id = this.GetMaxId(oleDbConnection, trans);
            StringBuilder stringBuilder2 = new StringBuilder();
            StringBuilder stringBuilder3 = new StringBuilder();
            StringBuilder stringBuilder4 = new StringBuilder();
            OleDbParameter[] oleDbParameterArray2 = new OleDbParameter[model.fields.Count + 1];
            int index = 1;
            stringBuilder3.Append("article_id");
            stringBuilder4.Append("@article_id");
            oleDbParameterArray2[0] = new OleDbParameter("@article_id", OleDbType.Integer, 4);
            oleDbParameterArray2[0].Value = (object) model.id;
            foreach (KeyValuePair<string, string> field in model.fields)
            {
              stringBuilder3.Append("," + field.Key);
              stringBuilder4.Append(",@" + field.Key);
              oleDbParameterArray2[index] = field.Value.Length > 4000 ? new OleDbParameter("@" + field.Key, (object) SqlDbType.NText) : new OleDbParameter("@" + field.Key, OleDbType.VarChar, field.Value.Length);
              oleDbParameterArray2[index].Value = (object) field.Value;
              ++index;
            }
            stringBuilder2.Append("insert into " + this.databaseprefix + "article_attribute_value(");
            stringBuilder2.Append(stringBuilder3.ToString() + ")");
            stringBuilder2.Append(" values (");
            stringBuilder2.Append(stringBuilder4.ToString() + ")");
            DbHelperOleDb.ExecuteSql(oleDbConnection, trans, stringBuilder2.ToString(), oleDbParameterArray2);
            if (model.albums != null)
            {
              foreach (Rain.Model.article_albums album in model.albums)
              {
                StringBuilder stringBuilder5 = new StringBuilder();
                stringBuilder5.Append("insert into " + this.databaseprefix + "article_albums(");
                stringBuilder5.Append("article_id,thumb_path,original_path,remark)");
                stringBuilder5.Append(" values (");
                stringBuilder5.Append("@article_id,@thumb_path,@original_path,@remark)");
                OleDbParameter[] oleDbParameterArray3 = new OleDbParameter[4]
                {
                  new OleDbParameter("@article_id", OleDbType.Integer, 4),
                  new OleDbParameter("@thumb_path", OleDbType.VarChar, (int) byte.MaxValue),
                  new OleDbParameter("@original_path", OleDbType.VarChar, (int) byte.MaxValue),
                  new OleDbParameter("@remark", OleDbType.VarChar, 500)
                };
                oleDbParameterArray3[0].Value = (object) model.id;
                oleDbParameterArray3[1].Value = (object) album.thumb_path;
                oleDbParameterArray3[2].Value = (object) album.original_path;
                oleDbParameterArray3[3].Value = (object) album.remark;
                DbHelperOleDb.ExecuteSql(oleDbConnection, trans, stringBuilder5.ToString(), oleDbParameterArray3);
              }
            }
            if (model.attach != null)
            {
              foreach (Rain.Model.article_attach articleAttach in model.attach)
              {
                StringBuilder stringBuilder5 = new StringBuilder();
                stringBuilder5.Append("insert into " + this.databaseprefix + "article_attach(");
                stringBuilder5.Append("article_id,file_name,file_path,file_size,file_ext,down_num,point)");
                stringBuilder5.Append(" values (");
                stringBuilder5.Append("@article_id,@file_name,@file_path,@file_size,@file_ext,@down_num,@point)");
                OleDbParameter[] oleDbParameterArray3 = new OleDbParameter[7]
                {
                  new OleDbParameter("@article_id", OleDbType.Integer, 4),
                  new OleDbParameter("@file_name", OleDbType.VarChar, 100),
                  new OleDbParameter("@file_path", OleDbType.VarChar, (int) byte.MaxValue),
                  new OleDbParameter("@file_size", OleDbType.Integer, 4),
                  new OleDbParameter("@file_ext", OleDbType.VarChar, 20),
                  new OleDbParameter("@down_num", OleDbType.Integer, 4),
                  new OleDbParameter("@point", OleDbType.Integer, 4)
                };
                oleDbParameterArray3[0].Value = (object) model.id;
                oleDbParameterArray3[1].Value = (object) articleAttach.file_name;
                oleDbParameterArray3[2].Value = (object) articleAttach.file_path;
                oleDbParameterArray3[3].Value = (object) articleAttach.file_size;
                oleDbParameterArray3[4].Value = (object) articleAttach.file_ext;
                oleDbParameterArray3[5].Value = (object) articleAttach.down_num;
                oleDbParameterArray3[6].Value = (object) articleAttach.point;
                DbHelperOleDb.ExecuteSql(oleDbConnection, trans, stringBuilder5.ToString(), oleDbParameterArray3);
              }
            }
            if (model.group_price != null)
            {
              foreach (Rain.Model.user_group_price userGroupPrice in model.group_price)
              {
                StringBuilder stringBuilder5 = new StringBuilder();
                stringBuilder5.Append("insert into " + this.databaseprefix + "user_group_price(");
                stringBuilder5.Append("article_id,group_id,price)");
                stringBuilder5.Append(" values (");
                stringBuilder5.Append("@article_id,@group_id,@price)");
                OleDbParameter[] oleDbParameterArray3 = new OleDbParameter[3]
                {
                  new OleDbParameter("@article_id", OleDbType.Integer, 4),
                  new OleDbParameter("@group_id", OleDbType.Integer, 4),
                  new OleDbParameter("@price", OleDbType.Decimal, 5)
                };
                oleDbParameterArray3[0].Value = (object) model.id;
                oleDbParameterArray3[1].Value = (object) userGroupPrice.group_id;
                oleDbParameterArray3[2].Value = (object) userGroupPrice.price;
                DbHelperOleDb.ExecuteSql(oleDbConnection, trans, stringBuilder5.ToString(), oleDbParameterArray3);
              }
            }
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

    public bool Update(Rain.Model.article model)
    {
      using (OleDbConnection oleDbConnection = new OleDbConnection(DbHelperOleDb.connectionString))
      {
        oleDbConnection.Open();
        using (OleDbTransaction trans = oleDbConnection.BeginTransaction())
        {
          try
          {
            StringBuilder stringBuilder1 = new StringBuilder();
            stringBuilder1.Append("update " + this.databaseprefix + "article set ");
            stringBuilder1.Append("channel_id=@channel_id,");
            stringBuilder1.Append("category_id=@category_id,");
            stringBuilder1.Append("call_index=@call_index,");
            stringBuilder1.Append("title=@title,");
            stringBuilder1.Append("link_url=@link_url,");
            stringBuilder1.Append("img_url=@img_url,");
            stringBuilder1.Append("seo_title=@seo_title,");
            stringBuilder1.Append("seo_keywords=@seo_keywords,");
            stringBuilder1.Append("seo_description=@seo_description,");
            stringBuilder1.Append("zhaiyao=@zhaiyao,");
            stringBuilder1.Append("content=@content,");
            stringBuilder1.Append("sort_id=@sort_id,");
            stringBuilder1.Append("click=@click,");
            stringBuilder1.Append("status=@status,");
            stringBuilder1.Append("is_msg=@is_msg,");
            stringBuilder1.Append("is_top=@is_top,");
            stringBuilder1.Append("is_red=@is_red,");
            stringBuilder1.Append("is_hot=@is_hot,");
            stringBuilder1.Append("is_slide=@is_slide,");
            stringBuilder1.Append("is_sys=@is_sys,");
            stringBuilder1.Append("user_name=@user_name,");
            stringBuilder1.Append("add_time=@add_time,");
            stringBuilder1.Append("update_time=@update_time");
            stringBuilder1.Append(" where id=@id");
            OleDbParameter[] oleDbParameterArray1 = new OleDbParameter[24]
            {
              new OleDbParameter("@channel_id", OleDbType.Integer, 4),
              new OleDbParameter("@category_id", OleDbType.Integer, 4),
              new OleDbParameter("@call_index", OleDbType.VarChar, 50),
              new OleDbParameter("@title", OleDbType.VarChar, 100),
              new OleDbParameter("@link_url", OleDbType.VarChar, (int) byte.MaxValue),
              new OleDbParameter("@img_url", OleDbType.VarChar, (int) byte.MaxValue),
              new OleDbParameter("@seo_title", OleDbType.VarChar, (int) byte.MaxValue),
              new OleDbParameter("@seo_keywords", OleDbType.VarChar, (int) byte.MaxValue),
              new OleDbParameter("@seo_description", OleDbType.VarChar, (int) byte.MaxValue),
              new OleDbParameter("@zhaiyao", OleDbType.VarChar, (int) byte.MaxValue),
              new OleDbParameter("@content", (object) SqlDbType.NText),
              new OleDbParameter("@sort_id", OleDbType.Integer, 4),
              new OleDbParameter("@click", OleDbType.Integer, 4),
              new OleDbParameter("@status", OleDbType.Integer, 4),
              new OleDbParameter("@is_msg", OleDbType.Integer, 4),
              new OleDbParameter("@is_top", OleDbType.Integer, 4),
              new OleDbParameter("@is_red", OleDbType.Integer, 4),
              new OleDbParameter("@is_hot", OleDbType.Integer, 4),
              new OleDbParameter("@is_slide", OleDbType.Integer, 4),
              new OleDbParameter("@is_sys", OleDbType.Integer, 4),
              new OleDbParameter("@user_name", OleDbType.VarChar, 100),
              new OleDbParameter("@add_time", OleDbType.Date),
              new OleDbParameter("@update_time", OleDbType.Date),
              new OleDbParameter("@id", OleDbType.Integer, 4)
            };
            oleDbParameterArray1[0].Value = (object) model.channel_id;
            oleDbParameterArray1[1].Value = (object) model.category_id;
            oleDbParameterArray1[2].Value = (object) model.call_index;
            oleDbParameterArray1[3].Value = (object) model.title;
            oleDbParameterArray1[4].Value = (object) model.link_url;
            oleDbParameterArray1[5].Value = (object) model.img_url;
            oleDbParameterArray1[6].Value = (object) model.seo_title;
            oleDbParameterArray1[7].Value = (object) model.seo_keywords;
            oleDbParameterArray1[8].Value = (object) model.seo_description;
            oleDbParameterArray1[9].Value = (object) model.zhaiyao;
            oleDbParameterArray1[10].Value = (object) model.content;
            oleDbParameterArray1[11].Value = (object) model.sort_id;
            oleDbParameterArray1[12].Value = (object) model.click;
            oleDbParameterArray1[13].Value = (object) model.status;
            oleDbParameterArray1[14].Value = (object) model.is_msg;
            oleDbParameterArray1[15].Value = (object) model.is_top;
            oleDbParameterArray1[16].Value = (object) model.is_red;
            oleDbParameterArray1[17].Value = (object) model.is_hot;
            oleDbParameterArray1[18].Value = (object) model.is_slide;
            oleDbParameterArray1[19].Value = (object) model.is_sys;
            oleDbParameterArray1[20].Value = (object) model.user_name;
            oleDbParameterArray1[21].Value = (object) model.add_time;
            oleDbParameterArray1[22].Value = (object) model.update_time;
            oleDbParameterArray1[23].Value = (object) model.id;
            DbHelperOleDb.ExecuteSql(oleDbConnection, trans, stringBuilder1.ToString(), oleDbParameterArray1);
            if (model.fields.Count > 0)
            {
              StringBuilder stringBuilder2 = new StringBuilder();
              StringBuilder stringBuilder3 = new StringBuilder();
              OleDbParameter[] oleDbParameterArray2 = new OleDbParameter[model.fields.Count + 1];
              int index = 0;
              foreach (KeyValuePair<string, string> field in model.fields)
              {
                stringBuilder3.Append(field.Key + "=@" + field.Key + ",");
                oleDbParameterArray2[index] = field.Value.Length > 4000 ? new OleDbParameter("@" + field.Key, (object) SqlDbType.NText) : new OleDbParameter("@" + field.Key, OleDbType.VarChar, field.Value.Length);
                oleDbParameterArray2[index].Value = (object) field.Value;
                ++index;
              }
              stringBuilder2.Append("update " + this.databaseprefix + "article_attribute_value set ");
              stringBuilder2.Append(Utils.DelLastComma(stringBuilder3.ToString()));
              stringBuilder2.Append(" where article_id=@article_id");
              oleDbParameterArray2[index] = new OleDbParameter("@article_id", OleDbType.Integer, 4);
              oleDbParameterArray2[index].Value = (object) model.id;
              DbHelperOleDb.ExecuteSql(oleDbConnection, trans, stringBuilder2.ToString(), oleDbParameterArray2);
            }
            new article_albums(this.databaseprefix).DeleteList(oleDbConnection, trans, model.albums, model.id);
            if (model.albums != null)
            {
              foreach (Rain.Model.article_albums album in model.albums)
              {
                StringBuilder stringBuilder2 = new StringBuilder();
                if (album.id > 0)
                {
                  stringBuilder2.Append("update " + this.databaseprefix + "article_albums set ");
                  stringBuilder2.Append("article_id=@article_id,");
                  stringBuilder2.Append("thumb_path=@thumb_path,");
                  stringBuilder2.Append("original_path=@original_path,");
                  stringBuilder2.Append("remark=@remark");
                  stringBuilder2.Append(" where id=@id");
                  OleDbParameter[] oleDbParameterArray2 = new OleDbParameter[5]
                  {
                    new OleDbParameter("@article_id", OleDbType.Integer, 4),
                    new OleDbParameter("@thumb_path", OleDbType.VarChar, (int) byte.MaxValue),
                    new OleDbParameter("@original_path", OleDbType.VarChar, (int) byte.MaxValue),
                    new OleDbParameter("@remark", OleDbType.VarChar, 500),
                    new OleDbParameter("@id", OleDbType.Integer, 4)
                  };
                  oleDbParameterArray2[0].Value = (object) album.article_id;
                  oleDbParameterArray2[1].Value = (object) album.thumb_path;
                  oleDbParameterArray2[2].Value = (object) album.original_path;
                  oleDbParameterArray2[3].Value = (object) album.remark;
                  oleDbParameterArray2[4].Value = (object) album.id;
                  DbHelperOleDb.ExecuteSql(oleDbConnection, trans, stringBuilder2.ToString(), oleDbParameterArray2);
                }
                else
                {
                  stringBuilder2.Append("insert into " + this.databaseprefix + "article_albums(");
                  stringBuilder2.Append("article_id,thumb_path,original_path,remark)");
                  stringBuilder2.Append(" values (");
                  stringBuilder2.Append("@article_id,@thumb_path,@original_path,@remark)");
                  OleDbParameter[] oleDbParameterArray2 = new OleDbParameter[4]
                  {
                    new OleDbParameter("@article_id", OleDbType.Integer, 4),
                    new OleDbParameter("@thumb_path", OleDbType.VarChar, (int) byte.MaxValue),
                    new OleDbParameter("@original_path", OleDbType.VarChar, (int) byte.MaxValue),
                    new OleDbParameter("@remark", OleDbType.VarChar, 500)
                  };
                  oleDbParameterArray2[0].Value = (object) album.article_id;
                  oleDbParameterArray2[1].Value = (object) album.thumb_path;
                  oleDbParameterArray2[2].Value = (object) album.original_path;
                  oleDbParameterArray2[3].Value = (object) album.remark;
                  DbHelperOleDb.ExecuteSql(oleDbConnection, trans, stringBuilder2.ToString(), oleDbParameterArray2);
                }
              }
            }
            new article_attach(this.databaseprefix).DeleteList(oleDbConnection, trans, model.attach, model.id);
            if (model.attach != null)
            {
              foreach (Rain.Model.article_attach articleAttach in model.attach)
              {
                StringBuilder stringBuilder2 = new StringBuilder();
                if (articleAttach.id > 0)
                {
                  stringBuilder2.Append("update " + this.databaseprefix + "article_attach set ");
                  stringBuilder2.Append("article_id=@article_id,");
                  stringBuilder2.Append("file_name=@file_name,");
                  stringBuilder2.Append("file_path=@file_path,");
                  stringBuilder2.Append("file_size=@file_size,");
                  stringBuilder2.Append("file_ext=@file_ext,");
                  stringBuilder2.Append("point=@point");
                  stringBuilder2.Append(" where id=@id");
                  OleDbParameter[] oleDbParameterArray2 = new OleDbParameter[7]
                  {
                    new OleDbParameter("@article_id", OleDbType.Integer, 4),
                    new OleDbParameter("@file_name", OleDbType.VarChar, 100),
                    new OleDbParameter("@file_path", OleDbType.VarChar, (int) byte.MaxValue),
                    new OleDbParameter("@file_size", OleDbType.Integer, 4),
                    new OleDbParameter("@file_ext", OleDbType.VarChar, 20),
                    new OleDbParameter("@point", OleDbType.Integer, 4),
                    new OleDbParameter("@id", OleDbType.Integer, 4)
                  };
                  oleDbParameterArray2[0].Value = (object) articleAttach.article_id;
                  oleDbParameterArray2[1].Value = (object) articleAttach.file_name;
                  oleDbParameterArray2[2].Value = (object) articleAttach.file_path;
                  oleDbParameterArray2[3].Value = (object) articleAttach.file_size;
                  oleDbParameterArray2[4].Value = (object) articleAttach.file_ext;
                  oleDbParameterArray2[5].Value = (object) articleAttach.point;
                  oleDbParameterArray2[6].Value = (object) articleAttach.id;
                  DbHelperOleDb.ExecuteSql(oleDbConnection, trans, stringBuilder2.ToString(), oleDbParameterArray2);
                }
                else
                {
                  stringBuilder2.Append("insert into " + this.databaseprefix + "article_attach(");
                  stringBuilder2.Append("article_id,file_name,file_path,file_size,file_ext,down_num,point)");
                  stringBuilder2.Append(" values (");
                  stringBuilder2.Append("@article_id,@file_name,@file_path,@file_size,@file_ext,@down_num,@point)");
                  OleDbParameter[] oleDbParameterArray2 = new OleDbParameter[7]
                  {
                    new OleDbParameter("@article_id", OleDbType.Integer, 4),
                    new OleDbParameter("@file_name", OleDbType.VarChar, 100),
                    new OleDbParameter("@file_path", OleDbType.VarChar, (int) byte.MaxValue),
                    new OleDbParameter("@file_size", OleDbType.Integer, 4),
                    new OleDbParameter("@file_ext", OleDbType.VarChar, 20),
                    new OleDbParameter("@down_num", OleDbType.Integer, 4),
                    new OleDbParameter("@point", OleDbType.Integer, 4)
                  };
                  oleDbParameterArray2[0].Value = (object) articleAttach.article_id;
                  oleDbParameterArray2[1].Value = (object) articleAttach.file_name;
                  oleDbParameterArray2[2].Value = (object) articleAttach.file_path;
                  oleDbParameterArray2[3].Value = (object) articleAttach.file_size;
                  oleDbParameterArray2[4].Value = (object) articleAttach.file_ext;
                  oleDbParameterArray2[5].Value = (object) articleAttach.down_num;
                  oleDbParameterArray2[6].Value = (object) articleAttach.point;
                  DbHelperOleDb.ExecuteSql(oleDbConnection, trans, stringBuilder2.ToString(), oleDbParameterArray2);
                }
              }
            }
            if (model.group_price != null)
            {
              foreach (Rain.Model.user_group_price userGroupPrice in model.group_price)
              {
                StringBuilder stringBuilder2 = new StringBuilder();
                if (userGroupPrice.id > 0)
                {
                  stringBuilder2.Append("update " + this.databaseprefix + "user_group_price set ");
                  stringBuilder2.Append("article_id=@article_id,");
                  stringBuilder2.Append("group_id=@group_id,");
                  stringBuilder2.Append("price=@price");
                  stringBuilder2.Append(" where id=@id");
                  OleDbParameter[] oleDbParameterArray2 = new OleDbParameter[4]
                  {
                    new OleDbParameter("@article_id", OleDbType.Integer, 4),
                    new OleDbParameter("@group_id", OleDbType.Integer, 4),
                    new OleDbParameter("@price", OleDbType.Decimal, 5),
                    new OleDbParameter("@id", OleDbType.Integer, 4)
                  };
                  oleDbParameterArray2[0].Value = (object) userGroupPrice.article_id;
                  oleDbParameterArray2[1].Value = (object) userGroupPrice.group_id;
                  oleDbParameterArray2[2].Value = (object) userGroupPrice.price;
                  oleDbParameterArray2[3].Value = (object) userGroupPrice.id;
                  DbHelperOleDb.ExecuteSql(oleDbConnection, trans, stringBuilder2.ToString(), oleDbParameterArray2);
                }
                else
                {
                  stringBuilder2.Append("insert into " + this.databaseprefix + "user_group_price(");
                  stringBuilder2.Append("article_id,group_id,price)");
                  stringBuilder2.Append(" values (");
                  stringBuilder2.Append("@article_id,@group_id,@price)");
                  OleDbParameter[] oleDbParameterArray2 = new OleDbParameter[3]
                  {
                    new OleDbParameter("@article_id", OleDbType.Integer, 4),
                    new OleDbParameter("@group_id", OleDbType.Integer, 4),
                    new OleDbParameter("@price", OleDbType.Decimal, 5)
                  };
                  oleDbParameterArray2[0].Value = (object) userGroupPrice.article_id;
                  oleDbParameterArray2[1].Value = (object) userGroupPrice.group_id;
                  oleDbParameterArray2[2].Value = (object) userGroupPrice.price;
                  DbHelperOleDb.ExecuteSql(oleDbConnection, trans, stringBuilder2.ToString(), oleDbParameterArray2);
                }
              }
            }
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
      List<Rain.Model.article_albums> list1 = new article_albums(this.databaseprefix).GetList(id);
      List<Rain.Model.article_attach> list2 = new article_attach(this.databaseprefix).GetList(id);
      Hashtable SQLStringList = new Hashtable();
      StringBuilder stringBuilder1 = new StringBuilder();
      stringBuilder1.Append("delete from " + this.databaseprefix + "article_attribute_value ");
      stringBuilder1.Append(" where article_id=@article_id ");
      OleDbParameter[] oleDbParameterArray1 = new OleDbParameter[1]
      {
        new OleDbParameter("@article_id", OleDbType.Integer, 4)
      };
      oleDbParameterArray1[0].Value = (object) id;
      SQLStringList.Add((object) stringBuilder1.ToString(), (object) oleDbParameterArray1);
      StringBuilder stringBuilder2 = new StringBuilder();
      stringBuilder2.Append("delete from " + this.databaseprefix + "article_albums ");
      stringBuilder2.Append(" where article_id=@article_id ");
      OleDbParameter[] oleDbParameterArray2 = new OleDbParameter[1]
      {
        new OleDbParameter("@article_id", OleDbType.Integer, 4)
      };
      oleDbParameterArray2[0].Value = (object) id;
      SQLStringList.Add((object) stringBuilder2.ToString(), (object) oleDbParameterArray2);
      StringBuilder stringBuilder3 = new StringBuilder();
      stringBuilder3.Append("delete from " + this.databaseprefix + "article_attach ");
      stringBuilder3.Append(" where article_id=@article_id ");
      OleDbParameter[] oleDbParameterArray3 = new OleDbParameter[1]
      {
        new OleDbParameter("@article_id", OleDbType.Integer, 4)
      };
      oleDbParameterArray3[0].Value = (object) id;
      SQLStringList.Add((object) stringBuilder3.ToString(), (object) oleDbParameterArray3);
      StringBuilder stringBuilder4 = new StringBuilder();
      stringBuilder4.Append("delete from " + this.databaseprefix + "user_group_price ");
      stringBuilder4.Append(" where article_id=@article_id ");
      OleDbParameter[] oleDbParameterArray4 = new OleDbParameter[1]
      {
        new OleDbParameter("@article_id", OleDbType.Integer, 4)
      };
      oleDbParameterArray4[0].Value = (object) id;
      SQLStringList.Add((object) stringBuilder4.ToString(), (object) oleDbParameterArray4);
      StringBuilder stringBuilder5 = new StringBuilder();
      stringBuilder5.Append("delete from " + this.databaseprefix + "article_comment ");
      stringBuilder5.Append(" where article_id=@article_id ");
      OleDbParameter[] oleDbParameterArray5 = new OleDbParameter[1]
      {
        new OleDbParameter("@article_id", OleDbType.Integer, 4)
      };
      oleDbParameterArray5[0].Value = (object) id;
      SQLStringList.Add((object) stringBuilder5.ToString(), (object) oleDbParameterArray5);
      StringBuilder stringBuilder6 = new StringBuilder();
      stringBuilder6.Append("delete from " + this.databaseprefix + "article ");
      stringBuilder6.Append(" where id=@id");
      OleDbParameter[] oleDbParameterArray6 = new OleDbParameter[1]
      {
        new OleDbParameter("@id", OleDbType.Integer, 4)
      };
      oleDbParameterArray6[0].Value = (object) id;
      SQLStringList.Add((object) stringBuilder6.ToString(), (object) oleDbParameterArray6);
      if (!DbHelperOleDb.ExecuteSqlTran(SQLStringList))
        return false;
      new article_albums(this.databaseprefix).DeleteFile(list1);
      new article_attach(this.databaseprefix).DeleteFile(list2);
      return true;
    }

    public Rain.Model.article GetModel(int id)
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("select top 1 id,channel_id,category_id,call_index,title,link_url,img_url,seo_title,seo_keywords,seo_description,zhaiyao,content,sort_id,click,status,is_msg,is_top,is_red,is_hot,is_slide,is_sys,user_name,add_time,update_time");
            _ = stringBuilder.Append(" from " + this.databaseprefix + nameof(Rain.Model.article));
      stringBuilder.Append(" where id=@id");
      OleDbParameter[] oleDbParameterArray = new OleDbParameter[1]
      {
        new OleDbParameter("@id", OleDbType.Integer, 4)
      };
      oleDbParameterArray[0].Value = (object) id;
      Rain.Model.article article = new Rain.Model.article();
      DataSet dataSet = DbHelperOleDb.Query(stringBuilder.ToString(), oleDbParameterArray);
      if (dataSet.Tables[0].Rows.Count > 0)
        return this.DataRowToModel(dataSet.Tables[0].Rows[0]);
      return (Rain.Model.article) null;
    }

    public Rain.Model.article GetModel(string call_index)
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("select id from " + this.databaseprefix + nameof (article));
      stringBuilder.Append(" where call_index=@call_index");
      OleDbParameter[] oleDbParameterArray = new OleDbParameter[1]
      {
        new OleDbParameter("@call_index", OleDbType.VarChar, 50)
      };
      oleDbParameterArray[0].Value = (object) call_index;
      object single = DbHelperOleDb.GetSingle(stringBuilder.ToString(), oleDbParameterArray);
      if (single != null)
        return this.GetModel(Convert.ToInt32(single));
      return (Rain.Model.article) null;
    }

    public DataSet GetList(int Top, string strWhere, string filedOrder)
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("select ");
      if (Top > 0)
        stringBuilder.Append(" top " + Top.ToString());
      stringBuilder.Append(" id,channel_id,category_id,call_index,title,link_url,img_url,seo_title,seo_keywords,seo_description,zhaiyao,content,sort_id,click,status,is_msg,is_top,is_red,is_hot,is_slide,is_sys,user_name,add_time,update_time ");
      stringBuilder.Append(" FROM " + this.databaseprefix + "article ");
      if (strWhere.Trim() != "")
        stringBuilder.Append(" where " + strWhere);
      stringBuilder.Append(" order by " + filedOrder);
      return DbHelperOleDb.Query(stringBuilder.ToString());
    }

    public DataSet GetList(
      int channel_id,
      int category_id,
      int pageSize,
      int pageIndex,
      string strWhere,
      string filedOrder,
      out int recordCount)
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("select * FROM " + this.databaseprefix + nameof (article));
      if (channel_id > 0)
        stringBuilder.Append(" where channel_id=" + (object) channel_id);
      if (category_id > 0)
      {
        if (channel_id > 0)
          stringBuilder.Append(" and category_id in(select id from " + this.databaseprefix + "article_category where class_list like '%," + (object) category_id + ",%')");
        else
          stringBuilder.Append(" where category_id in(select id from " + this.databaseprefix + "article_category where class_list like '%," + (object) category_id + ",%')");
      }
      if (strWhere.Trim() != "")
      {
        if (channel_id > 0 || category_id > 0)
          stringBuilder.Append(" and " + strWhere);
        else
          stringBuilder.Append(" where " + strWhere);
      }
      recordCount = Convert.ToInt32(DbHelperOleDb.GetSingle(PagingHelper.CreateCountingSql(stringBuilder.ToString())));
      return DbHelperOleDb.Query(PagingHelper.CreatePagingSql(recordCount, pageSize, pageIndex, stringBuilder.ToString(), filedOrder));
    }

    public bool ExistsTitle(string title)
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("select count(1) from " + this.databaseprefix + nameof (article));
      stringBuilder.Append(" where title=@title ");
      OleDbParameter[] oleDbParameterArray = new OleDbParameter[1]
      {
        new OleDbParameter("@title", OleDbType.VarChar, 200)
      };
      oleDbParameterArray[0].Value = (object) title;
      return DbHelperOleDb.Exists(stringBuilder.ToString(), oleDbParameterArray);
    }

    public bool ExistsTitle(string title, int category_id)
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("select count(1) from " + this.databaseprefix + nameof (article));
      stringBuilder.Append(" where title=@title and category_id=@category_id");
      OleDbParameter[] oleDbParameterArray = new OleDbParameter[2]
      {
        new OleDbParameter("@title", OleDbType.VarChar, 200),
        new OleDbParameter("@category_id", OleDbType.Integer, 4)
      };
      oleDbParameterArray[0].Value = (object) title;
      oleDbParameterArray[1].Value = (object) category_id;
      return DbHelperOleDb.Exists(stringBuilder.ToString(), oleDbParameterArray);
    }

    public string GetTitle(int id)
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("select top 1 title from " + this.databaseprefix + nameof (article));
      stringBuilder.Append(" where id=" + (object) id);
      string str = Convert.ToString(DbHelperOleDb.GetSingle(stringBuilder.ToString()));
      if (string.IsNullOrEmpty(str))
        return string.Empty;
      return str;
    }

    public string GetContent(int id)
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("select top 1 content from " + this.databaseprefix + nameof (article));
      stringBuilder.Append(" where id=" + (object) id);
      string str = Convert.ToString(DbHelperOleDb.GetSingle(stringBuilder.ToString()));
      if (string.IsNullOrEmpty(str))
        return "";
      return str;
    }

    public int GetClick(int id)
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("select top 1 click from " + this.databaseprefix + nameof (article));
      stringBuilder.Append(" where id=" + (object) id);
      string str = Convert.ToString(DbHelperOleDb.GetSingle(stringBuilder.ToString()));
      if (string.IsNullOrEmpty(str))
        return 0;
      return Convert.ToInt32(str);
    }

    public int GetCount(string strWhere)
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("select count(*) as H ");
      stringBuilder.Append(" from " + this.databaseprefix + nameof (article));
      if (strWhere.Trim() != "")
        stringBuilder.Append(" where " + strWhere);
      return Convert.ToInt32(DbHelperOleDb.GetSingle(stringBuilder.ToString()));
    }

    public int GetStockQuantity(int id)
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("select top 1 stock_quantity ");
      stringBuilder.Append(" from " + this.databaseprefix + "article_attribute_value");
      stringBuilder.Append(" where article_id=" + (object) id);
      return Convert.ToInt32(DbHelperOleDb.GetSingle(stringBuilder.ToString()));
    }

    public void UpdateField(int id, string strValue)
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("update " + this.databaseprefix + "article set " + strValue);
      stringBuilder.Append(" where id=" + (object) id);
      DbHelperOleDb.ExecuteSql(stringBuilder.ToString());
    }

    private List<Rain.Model.user_group_price> GetGroupPrice(int article_id)
    {
      List<Rain.Model.user_group_price> userGroupPriceList = new List<Rain.Model.user_group_price>();
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("select id,article_id,group_id,price from " + this.databaseprefix + "user_group_price ");
      stringBuilder.Append(" where article_id=" + (object) article_id);
      DataTable table = DbHelperOleDb.Query(stringBuilder.ToString()).Tables[0];
      int count = table.Rows.Count;
      if (count > 0)
      {
        for (int index = 0; index < count; ++index)
        {
          Rain.Model.user_group_price userGroupPrice = new Rain.Model.user_group_price();
          if (table.Rows[index]["id"] != null && table.Rows[index]["id"].ToString() != "")
            userGroupPrice.id = int.Parse(table.Rows[index]["id"].ToString());
          if (table.Rows[index][nameof (article_id)] != null && table.Rows[index][nameof (article_id)].ToString() != "")
            userGroupPrice.article_id = int.Parse(table.Rows[index][nameof (article_id)].ToString());
          if (table.Rows[index]["group_id"] != null && table.Rows[index]["group_id"].ToString() != "")
            userGroupPrice.group_id = int.Parse(table.Rows[index]["group_id"].ToString());
          if (table.Rows[index]["price"] != null && table.Rows[index]["price"].ToString() != "")
            userGroupPrice.price = Decimal.Parse(table.Rows[index]["price"].ToString());
          userGroupPriceList.Add(userGroupPrice);
        }
      }
      return userGroupPriceList;
    }

    private Rain.Model.article DataRowToModel(DataRow row)
    {
      Rain.Model.article article = new Rain.Model.article();
      if (row != null)
      {
        if (row["id"] != null && row["id"].ToString() != "")
          article.id = int.Parse(row["id"].ToString());
        if (row["channel_id"] != null && row["channel_id"].ToString() != "")
          article.channel_id = int.Parse(row["channel_id"].ToString());
        if (row["category_id"] != null && row["category_id"].ToString() != "")
          article.category_id = int.Parse(row["category_id"].ToString());
        if (row["call_index"] != null)
          article.call_index = row["call_index"].ToString();
        if (row["title"] != null)
          article.title = row["title"].ToString();
        if (row["link_url"] != null)
          article.link_url = row["link_url"].ToString();
        if (row["img_url"] != null)
          article.img_url = row["img_url"].ToString();
        if (row["seo_title"] != null)
          article.seo_title = row["seo_title"].ToString();
        if (row["seo_keywords"] != null)
          article.seo_keywords = row["seo_keywords"].ToString();
        if (row["seo_description"] != null)
          article.seo_description = row["seo_description"].ToString();
        if (row["zhaiyao"] != null)
          article.zhaiyao = row["zhaiyao"].ToString();
        if (row["content"] != null)
          article.content = row["content"].ToString();
        if (row["sort_id"] != null && row["sort_id"].ToString() != "")
          article.sort_id = int.Parse(row["sort_id"].ToString());
        if (row["click"] != null && row["click"].ToString() != "")
          article.click = int.Parse(row["click"].ToString());
        if (row["status"] != null && row["status"].ToString() != "")
          article.status = int.Parse(row["status"].ToString());
        if (row["is_msg"] != null && row["is_msg"].ToString() != "")
          article.is_msg = int.Parse(row["is_msg"].ToString());
        if (row["is_top"] != null && row["is_top"].ToString() != "")
          article.is_top = int.Parse(row["is_top"].ToString());
        if (row["is_red"] != null && row["is_red"].ToString() != "")
          article.is_red = int.Parse(row["is_red"].ToString());
        if (row["is_hot"] != null && row["is_hot"].ToString() != "")
          article.is_hot = int.Parse(row["is_hot"].ToString());
        if (row["is_slide"] != null && row["is_slide"].ToString() != "")
          article.is_slide = int.Parse(row["is_slide"].ToString());
        if (row["is_sys"] != null && row["is_sys"].ToString() != "")
          article.is_sys = int.Parse(row["is_sys"].ToString());
        if (row["user_name"] != null)
          article.user_name = row["user_name"].ToString();
        if (row["add_time"] != null && row["add_time"].ToString() != "")
          article.add_time = DateTime.Parse(row["add_time"].ToString());
        if (row["update_time"] != null && row["update_time"].ToString() != "")
          article.update_time = new DateTime?(DateTime.Parse(row["update_time"].ToString()));
        article.fields = new article_attribute_field(this.databaseprefix).GetFields(article.channel_id, article.id, string.Empty);
        article.albums = new article_albums(this.databaseprefix).GetList(article.id);
        article.attach = new article_attach(this.databaseprefix).GetList(article.id);
        article.group_price = this.GetGroupPrice(article.id);
      }
      return article;
    }

    public DataSet GetList(
      string channel_name,
      int Top,
      string strWhere,
      string filedOrder)
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("select ");
      if (Top > 0)
        stringBuilder.Append(" top " + Top.ToString());
      stringBuilder.Append(" * FROM view_channel_" + channel_name);
      stringBuilder.Append(" where datediff('d',add_time,date())>=0");
      if (strWhere.Trim() != "")
        stringBuilder.Append(" and " + strWhere);
      stringBuilder.Append(" order by " + filedOrder);
      return DbHelperOleDb.Query(stringBuilder.ToString());
    }

    public int GetCount(string channel_name, int category_id, string strWhere)
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("select ");
      stringBuilder.Append(" count(1) FROM view_channel_" + channel_name);
      stringBuilder.Append(" where datediff('d',add_time,date())>=0");
      if (category_id > 0)
        stringBuilder.Append(" and category_id in(select id from " + this.databaseprefix + "article_category where class_list like '%," + (object) category_id + ",%')");
      if (strWhere.Trim() != "")
        stringBuilder.Append(" and " + strWhere);
      return Convert.ToInt32(DbHelperOleDb.GetSingle(stringBuilder.ToString()));
    }

    public DataSet GetList(
      string channel_name,
      int category_id,
      int Top,
      string strWhere,
      string filedOrder)
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("select ");
      if (Top > 0)
        stringBuilder.Append(" top " + Top.ToString());
      stringBuilder.Append(" * FROM view_channel_" + channel_name);
      stringBuilder.Append(" where datediff('d',add_time,date())>=0");
      if (category_id > 0)
        stringBuilder.Append(" and category_id in(select id from " + this.databaseprefix + "article_category where class_list like '%," + (object) category_id + ",%')");
      if (strWhere.Trim() != "")
        stringBuilder.Append(" and " + strWhere);
      stringBuilder.Append(" order by " + filedOrder);
      return DbHelperOleDb.Query(stringBuilder.ToString());
    }

    public DataSet GetList(
      string channel_name,
      int category_id,
      int pageSize,
      int pageIndex,
      string strWhere,
      string filedOrder,
      out int recordCount)
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("select * FROM view_channel_" + channel_name);
      stringBuilder.Append(" where datediff('d',add_time,date())>=0");
      if (category_id > 0)
        stringBuilder.Append(" and category_id in(select id from " + this.databaseprefix + "article_category where class_list like '%," + (object) category_id + ",%')");
      if (strWhere.Trim() != "")
        stringBuilder.Append(" and " + strWhere);
      recordCount = Convert.ToInt32(DbHelperOleDb.GetSingle(PagingHelper.CreateCountingSql(stringBuilder.ToString())));
      return DbHelperOleDb.Query(PagingHelper.CreatePagingSql(recordCount, pageSize, pageIndex, stringBuilder.ToString(), filedOrder));
    }

    public DataSet GetSearch(
      string channel_name,
      int pageSize,
      int pageIndex,
      string strWhere,
      string filedOrder,
      out int recordCount)
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("select id,channel_id,call_index,title,zhaiyao,add_time,img_url from " + this.databaseprefix + nameof (article));
      stringBuilder.Append(" where id>0");
      if (!string.IsNullOrEmpty(channel_name))
        stringBuilder.Append(" and channel_id=(select id from " + this.databaseprefix + "channel where [name]='" + channel_name + "')");
      if (strWhere.Trim() != "")
        stringBuilder.Append(" and " + strWhere);
      recordCount = Convert.ToInt32(DbHelperOleDb.GetSingle(PagingHelper.CreateCountingSql(stringBuilder.ToString())));
      return DbHelperOleDb.Query(PagingHelper.CreatePagingSql(recordCount, pageSize, pageIndex, stringBuilder.ToString(), filedOrder));
    }
  }
}
