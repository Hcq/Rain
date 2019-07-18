// Decompiled with JetBrains decompiler
// Type: Rain.DAL.channel_site
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
  public class channel_site
  {
    private string databaseprefix;

    public channel_site(string _databaseprefix)
    {
      this.databaseprefix = _databaseprefix;
    }

    private int GetMaxId(OleDbConnection conn, OleDbTransaction trans)
    {
      string SQLString = "select top 1 id from " + this.databaseprefix + "channel_site order by id desc";
      object single = DbHelperOleDb.GetSingle(conn, trans, SQLString);
      if (single == null)
        return 0;
      return int.Parse(single.ToString());
    }

    public bool Exists(int id)
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("select count(1) from " + this.databaseprefix + nameof (channel_site));
      stringBuilder.Append(" where id=@id");
      OleDbParameter[] oleDbParameterArray = new OleDbParameter[1]
      {
        new OleDbParameter("@id", OleDbType.Integer, 4)
      };
      oleDbParameterArray[0].Value = (object) id;
      return DbHelperOleDb.Exists(stringBuilder.ToString(), oleDbParameterArray);
    }

    public bool Exists(string build_path)
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("select count(1) from " + this.databaseprefix + nameof (channel_site));
      stringBuilder.Append(" where build_path=@build_path ");
      OleDbParameter[] oleDbParameterArray = new OleDbParameter[1]
      {
        new OleDbParameter("@build_path", OleDbType.VarChar, 100)
      };
      oleDbParameterArray[0].Value = (object) build_path;
      return DbHelperOleDb.Exists(stringBuilder.ToString(), oleDbParameterArray);
    }

    public int Add(Rain.Model.channel_site model)
    {
      using (OleDbConnection oleDbConnection = new OleDbConnection(DbHelperOleDb.connectionString))
      {
        oleDbConnection.Open();
        using (OleDbTransaction trans = oleDbConnection.BeginTransaction())
        {
          try
          {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append("insert into " + this.databaseprefix + "channel_site(");
            stringBuilder.Append("title,build_path,templet_path,[domain],[name],logo,company,address,tel,fax,email,crod,copyright,seo_title,seo_keyword,seo_description,is_default,sort_id)");
            stringBuilder.Append(" values (");
            stringBuilder.Append("@title,@build_path,@templet_path,@domain,@name,@logo,@company,@address,@tel,@fax,@email,@crod,@copyright,@seo_title,@seo_keyword,@seo_description,@is_default,@sort_id)");
            OleDbParameter[] oleDbParameterArray = new OleDbParameter[18]
            {
              new OleDbParameter("@title", OleDbType.VarChar, 100),
              new OleDbParameter("@build_path", OleDbType.VarChar, 100),
              new OleDbParameter("@templet_path", OleDbType.VarChar, 100),
              new OleDbParameter("@domain", OleDbType.VarChar, (int) byte.MaxValue),
              new OleDbParameter("@name", OleDbType.VarChar, (int) byte.MaxValue),
              new OleDbParameter("@logo", OleDbType.VarChar, (int) byte.MaxValue),
              new OleDbParameter("@company", OleDbType.VarChar, (int) byte.MaxValue),
              new OleDbParameter("@address", OleDbType.VarChar, (int) byte.MaxValue),
              new OleDbParameter("@tel", OleDbType.VarChar, 30),
              new OleDbParameter("@fax", OleDbType.VarChar, 30),
              new OleDbParameter("@email", OleDbType.VarChar, 50),
              new OleDbParameter("@crod", OleDbType.VarChar, 100),
              new OleDbParameter("@copyright", OleDbType.VarChar),
              new OleDbParameter("@seo_title", OleDbType.VarChar, (int) byte.MaxValue),
              new OleDbParameter("@seo_keyword", OleDbType.VarChar, (int) byte.MaxValue),
              new OleDbParameter("@seo_description", OleDbType.VarChar, 500),
              new OleDbParameter("@is_default", OleDbType.Integer, 4),
              new OleDbParameter("@sort_id", OleDbType.Integer, 4)
            };
            oleDbParameterArray[0].Value = (object) model.title;
            oleDbParameterArray[1].Value = (object) model.build_path;
            oleDbParameterArray[2].Value = (object) model.templet_path;
            oleDbParameterArray[3].Value = (object) model.domain;
            oleDbParameterArray[4].Value = (object) model.name;
            oleDbParameterArray[5].Value = (object) model.logo;
            oleDbParameterArray[6].Value = (object) model.company;
            oleDbParameterArray[7].Value = (object) model.address;
            oleDbParameterArray[8].Value = (object) model.tel;
            oleDbParameterArray[9].Value = (object) model.fax;
            oleDbParameterArray[10].Value = (object) model.email;
            oleDbParameterArray[11].Value = (object) model.crod;
            oleDbParameterArray[12].Value = (object) model.copyright;
            oleDbParameterArray[13].Value = (object) model.seo_title;
            oleDbParameterArray[14].Value = (object) model.seo_keyword;
            oleDbParameterArray[15].Value = (object) model.seo_description;
            oleDbParameterArray[16].Value = (object) model.is_default;
            oleDbParameterArray[17].Value = (object) model.sort_id;
            DbHelperOleDb.ExecuteSql(oleDbConnection, trans, stringBuilder.ToString(), oleDbParameterArray);
            model.id = this.GetMaxId(oleDbConnection, trans);
            new navigation(this.databaseprefix).Add(oleDbConnection, trans, "sys_contents", "channel_" + model.build_path, model.title, "", model.sort_id, 0, "Show");
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

    public bool Update(Rain.Model.channel_site model, string old_build_path)
    {
      using (OleDbConnection oleDbConnection = new OleDbConnection(DbHelperOleDb.connectionString))
      {
        oleDbConnection.Open();
        using (OleDbTransaction trans = oleDbConnection.BeginTransaction())
        {
          try
          {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append("update " + this.databaseprefix + "channel_site set ");
            stringBuilder.Append("title=@title,");
            stringBuilder.Append("build_path=@build_path,");
            stringBuilder.Append("templet_path=@templet_path,");
            stringBuilder.Append("[domain]=@domain,");
            stringBuilder.Append("[name]=@name,");
            stringBuilder.Append("logo=@logo,");
            stringBuilder.Append("company=@company,");
            stringBuilder.Append("address=@address,");
            stringBuilder.Append("tel=@tel,");
            stringBuilder.Append("fax=@fax,");
            stringBuilder.Append("email=@email,");
            stringBuilder.Append("crod=@crod,");
            stringBuilder.Append("copyright=@copyright,");
            stringBuilder.Append("seo_title=@seo_title,");
            stringBuilder.Append("seo_keyword=@seo_keyword,");
            stringBuilder.Append("seo_description=@seo_description,");
            stringBuilder.Append("is_default=@is_default,");
            stringBuilder.Append("sort_id=@sort_id");
            stringBuilder.Append(" where id=@id");
            OleDbParameter[] oleDbParameterArray = new OleDbParameter[19]
            {
              new OleDbParameter("@title", OleDbType.VarChar, 100),
              new OleDbParameter("@build_path", OleDbType.VarChar, 100),
              new OleDbParameter("@templet_path", OleDbType.VarChar, 100),
              new OleDbParameter("@domain", OleDbType.VarChar, (int) byte.MaxValue),
              new OleDbParameter("@name", OleDbType.VarChar, (int) byte.MaxValue),
              new OleDbParameter("@logo", OleDbType.VarChar, (int) byte.MaxValue),
              new OleDbParameter("@company", OleDbType.VarChar, (int) byte.MaxValue),
              new OleDbParameter("@address", OleDbType.VarChar, (int) byte.MaxValue),
              new OleDbParameter("@tel", OleDbType.VarChar, 30),
              new OleDbParameter("@fax", OleDbType.VarChar, 30),
              new OleDbParameter("@email", OleDbType.VarChar, 50),
              new OleDbParameter("@crod", OleDbType.VarChar, 100),
              new OleDbParameter("@copyright", (object) SqlDbType.Text),
              new OleDbParameter("@seo_title", OleDbType.VarChar, (int) byte.MaxValue),
              new OleDbParameter("@seo_keyword", OleDbType.VarChar, (int) byte.MaxValue),
              new OleDbParameter("@seo_description", OleDbType.VarChar, 500),
              new OleDbParameter("@is_default", OleDbType.Integer, 4),
              new OleDbParameter("@sort_id", OleDbType.Integer, 4),
              new OleDbParameter("@id", OleDbType.Integer, 4)
            };
            oleDbParameterArray[0].Value = (object) model.title;
            oleDbParameterArray[1].Value = (object) model.build_path;
            oleDbParameterArray[2].Value = (object) model.templet_path;
            oleDbParameterArray[3].Value = (object) model.domain;
            oleDbParameterArray[4].Value = (object) model.name;
            oleDbParameterArray[5].Value = (object) model.logo;
            oleDbParameterArray[6].Value = (object) model.company;
            oleDbParameterArray[7].Value = (object) model.address;
            oleDbParameterArray[8].Value = (object) model.tel;
            oleDbParameterArray[9].Value = (object) model.fax;
            oleDbParameterArray[10].Value = (object) model.email;
            oleDbParameterArray[11].Value = (object) model.crod;
            oleDbParameterArray[12].Value = (object) model.copyright;
            oleDbParameterArray[13].Value = (object) model.seo_title;
            oleDbParameterArray[14].Value = (object) model.seo_keyword;
            oleDbParameterArray[15].Value = (object) model.seo_description;
            oleDbParameterArray[16].Value = (object) model.is_default;
            oleDbParameterArray[17].Value = (object) model.sort_id;
            oleDbParameterArray[18].Value = (object) model.id;
            DbHelperOleDb.ExecuteSql(oleDbConnection, trans, stringBuilder.ToString(), oleDbParameterArray);
            if (new navigation(this.databaseprefix).GetModel(oleDbConnection, trans, "channel_" + old_build_path) != null)
              new navigation(this.databaseprefix).Update(oleDbConnection, trans, "channel_" + old_build_path, "channel_" + model.build_path, model.title, model.sort_id);
            else
              new navigation(this.databaseprefix).Add(oleDbConnection, trans, "sys_contents", "channel_" + model.build_path, model.title, "", model.sort_id, 0, "Show");
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
      string buildPath = this.GetBuildPath(id);
      if (string.IsNullOrEmpty(buildPath))
        return false;
      string ids = new navigation(this.databaseprefix).GetIds("channel_" + buildPath);
      using (OleDbConnection connection = new OleDbConnection(DbHelperOleDb.connectionString))
      {
        connection.Open();
        using (OleDbTransaction trans = connection.BeginTransaction())
        {
          try
          {
            if (!string.IsNullOrEmpty(ids))
              DbHelperOleDb.ExecuteSql(connection, trans, "delete from " + this.databaseprefix + "navigation where id in(" + ids + ")");
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append("delete from " + this.databaseprefix + "channel_site ");
            stringBuilder.Append(" where id=@id");
            OleDbParameter[] oleDbParameterArray = new OleDbParameter[1]
            {
              new OleDbParameter("@id", OleDbType.Integer, 4)
            };
            oleDbParameterArray[0].Value = (object) id;
            DbHelperOleDb.ExecuteSql(connection, trans, stringBuilder.ToString(), oleDbParameterArray);
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

    public Rain.Model.channel_site GetModel(int id)
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("select top 1 id,title,build_path,templet_path,[domain],[name],logo,company,address,tel,fax,email,crod,copyright,seo_title,seo_keyword,seo_description,is_default,sort_id");
      stringBuilder.Append(" from " + this.databaseprefix + nameof (channel_site));
      stringBuilder.Append(" where id=@id");
      OleDbParameter[] oleDbParameterArray = new OleDbParameter[1]
      {
        new OleDbParameter("@id", OleDbType.Integer, 4)
      };
      oleDbParameterArray[0].Value = (object) id;
      Rain.Model.channel_site channelSite = new Rain.Model.channel_site();
      DataSet dataSet = DbHelperOleDb.Query(stringBuilder.ToString(), oleDbParameterArray);
      if (dataSet.Tables[0].Rows.Count > 0)
        return this.DataRowToModel(dataSet.Tables[0].Rows[0]);
      return (Rain.Model.channel_site) null;
    }

    public Rain.Model.channel_site GetModel(string build_path)
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("select top 1 id,title,build_path,templet_path,[domain],[name],logo,company,address,tel,fax,email,crod,copyright,seo_title,seo_keyword,seo_description,is_default,sort_id");
      stringBuilder.Append(" from " + this.databaseprefix + nameof (channel_site));
      stringBuilder.Append(" where build_path=@build_path");
      OleDbParameter[] oleDbParameterArray = new OleDbParameter[1]
      {
        new OleDbParameter("@build_path", OleDbType.VarChar, 50)
      };
      oleDbParameterArray[0].Value = (object) build_path;
      Rain.Model.channel_site channelSite = new Rain.Model.channel_site();
      DataSet dataSet = DbHelperOleDb.Query(stringBuilder.ToString(), oleDbParameterArray);
      if (dataSet.Tables[0].Rows.Count > 0)
        return this.DataRowToModel(dataSet.Tables[0].Rows[0]);
      return (Rain.Model.channel_site) null;
    }

    public DataSet GetList(int Top, string strWhere, string filedOrder)
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("select ");
      if (Top > 0)
        stringBuilder.Append(" top " + Top.ToString());
      stringBuilder.Append(" id,title,build_path,templet_path,[domain],[name],logo,company,address,tel,fax,email,crod,copyright,seo_title,seo_keyword,seo_description,is_default,sort_id");
      stringBuilder.Append(" from " + this.databaseprefix + nameof (channel_site));
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
      stringBuilder.Append("select * FROM " + this.databaseprefix + nameof (channel_site));
      if (strWhere.Trim() != "")
        stringBuilder.Append(" where " + strWhere);
      recordCount = Convert.ToInt32(DbHelperOleDb.GetSingle(PagingHelper.CreateCountingSql(stringBuilder.ToString())));
      return DbHelperOleDb.Query(PagingHelper.CreatePagingSql(recordCount, pageSize, pageIndex, stringBuilder.ToString(), filedOrder));
    }

    public string GetTitle(int id)
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("select top 1 title from " + this.databaseprefix + nameof (channel_site));
      stringBuilder.Append(" where id=" + (object) id);
      string str = Convert.ToString(DbHelperOleDb.GetSingle(stringBuilder.ToString()));
      if (string.IsNullOrEmpty(str))
        return "";
      return str;
    }

    public string GetTitle(string build_path)
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("select top 1 title from " + this.databaseprefix + nameof (channel_site));
      stringBuilder.Append(" where build_path=@build_path");
      OleDbParameter[] oleDbParameterArray = new OleDbParameter[1]
      {
        new OleDbParameter("@build_path", OleDbType.VarChar, 100)
      };
      oleDbParameterArray[0].Value = (object) build_path;
      string str = Convert.ToString(DbHelperOleDb.GetSingle(stringBuilder.ToString(), oleDbParameterArray));
      if (string.IsNullOrEmpty(str))
        return "";
      return str;
    }

    public string GetBuildPath(int id)
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("select top 1 build_path from " + this.databaseprefix + nameof (channel_site));
      stringBuilder.Append(" where id=" + (object) id);
      object single = DbHelperOleDb.GetSingle(stringBuilder.ToString());
      if (single != null)
        return Convert.ToString(single);
      return string.Empty;
    }

    public int GetSiteNavId(int id)
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("select N.id from " + this.databaseprefix + "navigation as N," + this.databaseprefix + "channel_site as S");
      stringBuilder.Append(" where 'channel_'+S.build_path=N.name and S.id=" + (object) id);
      object single = DbHelperOleDb.GetSingle(stringBuilder.ToString());
      if (single != null)
        return Convert.ToInt32(single);
      return 0;
    }

    public bool UpdateField(int id, string strValue)
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("update " + this.databaseprefix + "channel_site set " + strValue);
      stringBuilder.Append(" where id=" + (object) id);
      return DbHelperOleDb.ExecuteSql(stringBuilder.ToString()) > 0;
    }

    public bool UpdateField(string build_path, string strValue)
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("update " + this.databaseprefix + "channel_site set " + strValue);
      stringBuilder.Append(" where build_path=@build_path");
      OleDbParameter[] oleDbParameterArray = new OleDbParameter[1]
      {
        new OleDbParameter("@build_path", OleDbType.VarChar, 100)
      };
      oleDbParameterArray[0].Value = (object) build_path;
      return DbHelperOleDb.ExecuteSql(stringBuilder.ToString(), oleDbParameterArray) > 0;
    }

    public Rain.Model.channel_site DataRowToModel(DataRow row)
    {
      Rain.Model.channel_site channelSite = new Rain.Model.channel_site();
      if (row != null)
      {
        if (row["id"] != null && row["id"].ToString() != "")
          channelSite.id = int.Parse(row["id"].ToString());
        if (row["title"] != null)
          channelSite.title = row["title"].ToString();
        if (row["build_path"] != null)
          channelSite.build_path = row["build_path"].ToString();
        if (row["templet_path"] != null)
          channelSite.templet_path = row["templet_path"].ToString();
        if (row["domain"] != null)
          channelSite.domain = row["domain"].ToString();
        if (row["name"] != null)
          channelSite.name = row["name"].ToString();
        if (row["logo"] != null)
          channelSite.logo = row["logo"].ToString();
        if (row["company"] != null)
          channelSite.company = row["company"].ToString();
        if (row["address"] != null)
          channelSite.address = row["address"].ToString();
        if (row["tel"] != null)
          channelSite.tel = row["tel"].ToString();
        if (row["fax"] != null)
          channelSite.fax = row["fax"].ToString();
        if (row["email"] != null)
          channelSite.email = row["email"].ToString();
        if (row["crod"] != null)
          channelSite.crod = row["crod"].ToString();
        if (row["copyright"] != null)
          channelSite.copyright = row["copyright"].ToString();
        if (row["seo_title"] != null)
          channelSite.seo_title = row["seo_title"].ToString();
        if (row["seo_keyword"] != null)
          channelSite.seo_keyword = row["seo_keyword"].ToString();
        if (row["seo_description"] != null)
          channelSite.seo_description = row["seo_description"].ToString();
        if (row["is_default"] != null && row["is_default"].ToString() != "")
          channelSite.is_default = int.Parse(row["is_default"].ToString());
        if (row["sort_id"] != null && row["sort_id"].ToString() != "")
          channelSite.sort_id = int.Parse(row["sort_id"].ToString());
      }
      return channelSite;
    }
  }
}
