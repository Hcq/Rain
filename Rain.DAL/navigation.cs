// Decompiled with JetBrains decompiler
// Type: Rain.DAL.navigation
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
  public class navigation
  {
    private string databaseprefix;

    public navigation(string _databaseprefix)
    {
      this.databaseprefix = _databaseprefix;
    }

    private int GetMaxId(OleDbConnection conn, OleDbTransaction trans)
    {
      string SQLString = "select top 1 id from " + this.databaseprefix + "navigation order by id desc";
      object single = DbHelperOleDb.GetSingle(conn, trans, SQLString);
      if (single == null)
        return 0;
      return int.Parse(single.ToString());
    }

    public bool Exists(int id)
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("select count(1) from " + this.databaseprefix + nameof (navigation));
      stringBuilder.Append(" where id=@id");
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
      stringBuilder.Append("select count(1) from " + this.databaseprefix + nameof (navigation));
      stringBuilder.Append(" where name=@name ");
      OleDbParameter[] oleDbParameterArray = new OleDbParameter[1]
      {
        new OleDbParameter("@name", OleDbType.VarChar, 50)
      };
      oleDbParameterArray[0].Value = (object) name;
      return DbHelperOleDb.Exists(stringBuilder.ToString(), oleDbParameterArray);
    }

    public int Add(Rain.Model.navigation model)
    {
      using (OleDbConnection oleDbConnection = new OleDbConnection(DbHelperOleDb.connectionString))
      {
        oleDbConnection.Open();
        using (OleDbTransaction trans = oleDbConnection.BeginTransaction())
        {
          try
          {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append("insert into " + this.databaseprefix + "navigation(");
            stringBuilder.Append("parent_id,channel_id,nav_type,[name],title,sub_title,icon_url,link_url,sort_id,is_lock,[remark],action_type,is_sys)");
            stringBuilder.Append(" values (");
            stringBuilder.Append("@parent_id,@channel_id,@nav_type,@name,@title,@sub_title,@icon_url,@link_url,@sort_id,@is_lock,@remark,@action_type,@is_sys)");
            OleDbParameter[] oleDbParameterArray = new OleDbParameter[13]
            {
              new OleDbParameter("@parent_id", OleDbType.Integer, 4),
              new OleDbParameter("@channel_id", OleDbType.Integer, 4),
              new OleDbParameter("@nav_type", OleDbType.VarChar, 50),
              new OleDbParameter("@name", OleDbType.VarChar, 50),
              new OleDbParameter("@title", OleDbType.VarChar, 100),
              new OleDbParameter("@sub_title", OleDbType.VarChar, 100),
              new OleDbParameter("@icon_url", OleDbType.VarChar, (int) byte.MaxValue),
              new OleDbParameter("@link_url", OleDbType.VarChar, (int) byte.MaxValue),
              new OleDbParameter("@sort_id", OleDbType.Integer, 4),
              new OleDbParameter("@is_lock", OleDbType.Integer, 4),
              new OleDbParameter("@remark", OleDbType.VarChar, 500),
              new OleDbParameter("@action_type", OleDbType.VarChar, 500),
              new OleDbParameter("@is_sys", OleDbType.Integer, 4)
            };
            oleDbParameterArray[0].Value = (object) model.parent_id;
            oleDbParameterArray[1].Value = (object) model.channel_id;
            oleDbParameterArray[2].Value = (object) model.nav_type;
            oleDbParameterArray[3].Value = (object) model.name;
            oleDbParameterArray[4].Value = (object) model.title;
            oleDbParameterArray[5].Value = (object) model.sub_title;
            oleDbParameterArray[6].Value = (object) model.icon_url;
            oleDbParameterArray[7].Value = (object) model.link_url;
            oleDbParameterArray[8].Value = (object) model.sort_id;
            oleDbParameterArray[9].Value = (object) model.is_lock;
            oleDbParameterArray[10].Value = (object) model.remark;
            oleDbParameterArray[11].Value = (object) model.action_type;
            oleDbParameterArray[12].Value = (object) model.is_sys;
            DbHelperOleDb.ExecuteSql(oleDbConnection, trans, stringBuilder.ToString(), oleDbParameterArray);
            model.id = this.GetMaxId(oleDbConnection, trans);
            trans.Commit();
          }
          catch
          {
            trans.Rollback();
            return -1;
          }
        }
      }
      return model.id;
    }

    public bool Update(Rain.Model.navigation model)
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("update " + this.databaseprefix + "navigation set ");
      stringBuilder.Append("parent_id=@parent_id,");
      stringBuilder.Append("channel_id=@channel_id,");
      stringBuilder.Append("nav_type=@nav_type,");
      stringBuilder.Append("[name]=@name,");
      stringBuilder.Append("title=@title,");
      stringBuilder.Append("sub_title=@sub_title,");
      stringBuilder.Append("icon_url=@icon_url,");
      stringBuilder.Append("link_url=@link_url,");
      stringBuilder.Append("sort_id=@sort_id,");
      stringBuilder.Append("is_lock=@is_lock,");
      stringBuilder.Append("[remark]=@remark,");
      stringBuilder.Append("action_type=@action_type,");
      stringBuilder.Append("is_sys=@is_sys");
      stringBuilder.Append(" where id=@id");
      OleDbParameter[] oleDbParameterArray = new OleDbParameter[14]
      {
        new OleDbParameter("@parent_id", OleDbType.Integer, 4),
        new OleDbParameter("@channel_id", OleDbType.Integer, 4),
        new OleDbParameter("@nav_type", OleDbType.VarChar, 50),
        new OleDbParameter("@name", OleDbType.VarChar, 50),
        new OleDbParameter("@title", OleDbType.VarChar, 100),
        new OleDbParameter("@sub_title", OleDbType.VarChar, 100),
        new OleDbParameter("@icon_url", OleDbType.VarChar, (int) byte.MaxValue),
        new OleDbParameter("@link_url", OleDbType.VarChar, (int) byte.MaxValue),
        new OleDbParameter("@sort_id", OleDbType.Integer, 4),
        new OleDbParameter("@is_lock", OleDbType.Integer, 4),
        new OleDbParameter("@remark", OleDbType.VarChar, 500),
        new OleDbParameter("@action_type", OleDbType.VarChar, 500),
        new OleDbParameter("@is_sys", OleDbType.Integer, 4),
        new OleDbParameter("@id", OleDbType.Integer, 4)
      };
      oleDbParameterArray[0].Value = (object) model.parent_id;
      oleDbParameterArray[1].Value = (object) model.channel_id;
      oleDbParameterArray[2].Value = (object) model.nav_type;
      oleDbParameterArray[3].Value = (object) model.name;
      oleDbParameterArray[4].Value = (object) model.title;
      oleDbParameterArray[5].Value = (object) model.sub_title;
      oleDbParameterArray[6].Value = (object) model.icon_url;
      oleDbParameterArray[7].Value = (object) model.link_url;
      oleDbParameterArray[8].Value = (object) model.sort_id;
      oleDbParameterArray[9].Value = (object) model.is_lock;
      oleDbParameterArray[10].Value = (object) model.remark;
      oleDbParameterArray[11].Value = (object) model.action_type;
      oleDbParameterArray[12].Value = (object) model.is_sys;
      oleDbParameterArray[13].Value = (object) model.id;
      return DbHelperOleDb.ExecuteSql(stringBuilder.ToString(), oleDbParameterArray) > 0;
    }

    public bool Delete(int id)
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("delete from " + this.databaseprefix + nameof (navigation));
      stringBuilder.Append(" where id in(" + this.GetIds(id) + ")");
      return DbHelperOleDb.ExecuteSql(stringBuilder.ToString()) > 0;
    }

    public Rain.Model.navigation GetModel(int id)
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("select  top 1 id,parent_id,channel_id,nav_type,[name],title,sub_title,icon_url,link_url,sort_id,is_lock,[remark],action_type,is_sys");
      stringBuilder.Append(" from " + this.databaseprefix + "navigation ");
      stringBuilder.Append(" where id=@id");
      OleDbParameter[] oleDbParameterArray = new OleDbParameter[1]
      {
        new OleDbParameter("@id", OleDbType.Integer, 4)
      };
      oleDbParameterArray[0].Value = (object) id;
      Rain.Model.navigation navigation = new Rain.Model.navigation();
      DataSet dataSet = DbHelperOleDb.Query(stringBuilder.ToString(), oleDbParameterArray);
      if (dataSet.Tables[0].Rows.Count > 0)
        return this.DataRowToModel(dataSet.Tables[0].Rows[0]);
      return (Rain.Model.navigation) null;
    }

    public Rain.Model.navigation GetModel(string nav_name)
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("select top 1 id,parent_id,channel_id,nav_type,[name],title,sub_title,icon_url,link_url,sort_id,is_lock,[remark],action_type,is_sys");
      stringBuilder.Append(" from " + this.databaseprefix + "navigation ");
      stringBuilder.Append(" where name=@nav_name");
      OleDbParameter[] oleDbParameterArray = new OleDbParameter[1]
      {
        new OleDbParameter("@nav_name", OleDbType.VarChar, 50)
      };
      oleDbParameterArray[0].Value = (object) nav_name;
      Rain.Model.navigation navigation = new Rain.Model.navigation();
      DataSet dataSet = DbHelperOleDb.Query(stringBuilder.ToString(), oleDbParameterArray);
      if (dataSet.Tables[0].Rows.Count > 0)
        return this.DataRowToModel(dataSet.Tables[0].Rows[0]);
      return (Rain.Model.navigation) null;
    }

    public DataTable GetList(int parent_id, string nav_type)
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("select id,parent_id,channel_id,nav_type,[name],title,sub_title,icon_url,link_url,sort_id,is_lock,[remark],action_type,is_sys");
      stringBuilder.Append(" FROM " + this.databaseprefix + nameof (navigation));
      stringBuilder.Append(" where nav_type='" + nav_type + "'");
      stringBuilder.Append(" order by sort_id asc,id desc");
      DataTable table = DbHelperOleDb.Query(stringBuilder.ToString()).Tables[0];
      if (table == null)
        return (DataTable) null;
      DataTable newData = new DataTable();
      newData.Columns.Add("id", typeof (int));
      newData.Columns.Add(nameof (parent_id), typeof (int));
      newData.Columns.Add("channel_id", typeof (int));
      newData.Columns.Add("class_layer", typeof (int));
      newData.Columns.Add(nameof (nav_type), typeof (string));
      newData.Columns.Add("name", typeof (string));
      newData.Columns.Add("title", typeof (string));
      newData.Columns.Add("sub_title", typeof (string));
      newData.Columns.Add("icon_url", typeof (string));
      newData.Columns.Add("link_url", typeof (string));
      newData.Columns.Add("sort_id", typeof (int));
      newData.Columns.Add("is_lock", typeof (int));
      newData.Columns.Add("remark", typeof (string));
      newData.Columns.Add("action_type", typeof (string));
      newData.Columns.Add("is_sys", typeof (int));
      this.GetChilds(table, newData, parent_id, 0);
      return newData;
    }

    public int GetNavId(string nav_name)
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("select top 1 id from " + this.databaseprefix + nameof (navigation));
      stringBuilder.Append(" where name=@nav_name");
      OleDbParameter[] oleDbParameterArray = new OleDbParameter[1]
      {
        new OleDbParameter("@nav_name", OleDbType.VarChar, 50)
      };
      oleDbParameterArray[0].Value = (object) nav_name;
      return Utils.StrToInt(Convert.ToString(DbHelperOleDb.GetSingle(stringBuilder.ToString(), oleDbParameterArray)), 0);
    }

    public string GetIds(int parent_id)
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("select id,parent_id");
      stringBuilder.Append(" FROM " + this.databaseprefix + nameof (navigation));
      DataSet dataSet = DbHelperOleDb.Query(stringBuilder.ToString());
      string ids = parent_id.ToString() + ",";
      this.GetChildIds(dataSet.Tables[0], parent_id, ref ids);
      return ids.TrimEnd(',');
    }

    public string GetIds(string nav_name)
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("select top 1 id from " + this.databaseprefix + nameof (navigation));
      stringBuilder.Append(" where name=@nav_name");
      OleDbParameter[] oleDbParameterArray = new OleDbParameter[1]
      {
        new OleDbParameter("@nav_name", OleDbType.VarChar, 50)
      };
      oleDbParameterArray[0].Value = (object) nav_name;
      object single = DbHelperOleDb.GetSingle(stringBuilder.ToString(), oleDbParameterArray);
      if (single != null)
        return this.GetIds(Convert.ToInt32(single));
      return string.Empty;
    }

    public bool UpdateField(int id, string strValue)
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("update " + this.databaseprefix + "navigation set " + strValue);
      stringBuilder.Append(" where id=" + (object) id);
      return DbHelperOleDb.ExecuteSql(stringBuilder.ToString()) > 0;
    }

    public bool UpdateField(string name, string strValue)
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("update " + this.databaseprefix + "navigation set " + strValue);
      stringBuilder.Append(" where name='" + name + "'");
      return DbHelperOleDb.ExecuteSql(stringBuilder.ToString()) > 0;
    }

    public bool Update(
      OleDbConnection conn,
      OleDbTransaction trans,
      string old_name,
      string new_name)
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("update " + this.databaseprefix + "navigation set name=@new_name");
      stringBuilder.Append(" where name=@old_name");
      OleDbParameter[] oleDbParameterArray = new OleDbParameter[2]
      {
        new OleDbParameter("@new_name", OleDbType.VarChar, 50),
        new OleDbParameter("@old_name", OleDbType.VarChar, 50)
      };
      oleDbParameterArray[0].Value = (object) new_name;
      oleDbParameterArray[1].Value = (object) old_name;
      return DbHelperOleDb.ExecuteSql(conn, trans, stringBuilder.ToString(), oleDbParameterArray) > 0;
    }

    public bool Update(
      OleDbConnection conn,
      OleDbTransaction trans,
      string old_name,
      string new_name,
      string title,
      int sort_id)
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("update " + this.databaseprefix + "navigation set");
      stringBuilder.Append(" [name]=@new_name,");
      stringBuilder.Append(" title=@title,");
      stringBuilder.Append(" sort_id=@sort_id");
      stringBuilder.Append(" where name=@old_name");
      OleDbParameter[] oleDbParameterArray = new OleDbParameter[4]
      {
        new OleDbParameter("@new_name", OleDbType.VarChar, 50),
        new OleDbParameter("@title", OleDbType.VarChar, 100),
        new OleDbParameter("@sort_id", OleDbType.Integer, 4),
        new OleDbParameter("@old_name", OleDbType.VarChar, 50)
      };
      oleDbParameterArray[0].Value = (object) new_name;
      oleDbParameterArray[1].Value = (object) title;
      oleDbParameterArray[2].Value = (object) sort_id;
      oleDbParameterArray[3].Value = (object) old_name;
      return DbHelperOleDb.ExecuteSql(conn, trans, stringBuilder.ToString(), oleDbParameterArray) > 0;
    }

    public bool Update(
      OleDbConnection conn,
      OleDbTransaction trans,
      string old_name,
      int parent_id,
      string nav_name,
      string title,
      int sort_id)
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("update " + this.databaseprefix + "navigation set");
      stringBuilder.Append(" parent_id=@parent_id,");
      stringBuilder.Append(" [name]=@name,");
      stringBuilder.Append(" title=@title,");
      stringBuilder.Append(" sort_id=@sort_id");
      stringBuilder.Append(" where name=@old_name");
      OleDbParameter[] oleDbParameterArray = new OleDbParameter[5]
      {
        new OleDbParameter("@parent_id", OleDbType.Integer, 4),
        new OleDbParameter("@name", OleDbType.VarChar, 50),
        new OleDbParameter("@title", OleDbType.VarChar, 100),
        new OleDbParameter("@sort_id", OleDbType.Integer, 4),
        new OleDbParameter("@old_name", OleDbType.VarChar, 50)
      };
      oleDbParameterArray[0].Value = (object) parent_id;
      oleDbParameterArray[1].Value = (object) nav_name;
      oleDbParameterArray[2].Value = (object) title;
      oleDbParameterArray[3].Value = (object) sort_id;
      oleDbParameterArray[4].Value = (object) old_name;
      return DbHelperOleDb.ExecuteSql(conn, trans, stringBuilder.ToString(), oleDbParameterArray) > 0;
    }

    public int Add(
      string parent_name,
      string nav_name,
      string title,
      string link_url,
      int sort_id,
      int channel_id,
      string action_type)
    {
      StringBuilder stringBuilder1 = new StringBuilder();
      stringBuilder1.Append("select top 1 id from " + this.databaseprefix + nameof (navigation));
      stringBuilder1.Append(" where name=@parent_name");
      OleDbParameter[] oleDbParameterArray1 = new OleDbParameter[1]
      {
        new OleDbParameter("@parent_name", OleDbType.VarChar, 50)
      };
      oleDbParameterArray1[0].Value = (object) parent_name;
      object single = DbHelperOleDb.GetSingle(stringBuilder1.ToString(), oleDbParameterArray1);
      if (single == null)
        return 0;
      int int32 = Convert.ToInt32(single);
      int maxId;
      using (OleDbConnection oleDbConnection = new OleDbConnection(DbHelperOleDb.connectionString))
      {
        oleDbConnection.Open();
        using (OleDbTransaction trans = oleDbConnection.BeginTransaction())
        {
          try
          {
            StringBuilder stringBuilder2 = new StringBuilder();
            stringBuilder2.Append("insert into " + this.databaseprefix + "navigation(");
            stringBuilder2.Append("parent_id,channel_id,nav_type,[name],title,link_url,sort_id,action_type,is_lock,is_sys)");
            stringBuilder2.Append(" values (");
            stringBuilder2.Append("@parent_id,@channel_id,@nav_type,@name,@title,@link_url,@sort_id,@action_type,@is_lock,@is_sys)");
            OleDbParameter[] oleDbParameterArray2 = new OleDbParameter[10]
            {
              new OleDbParameter("@parent_id", OleDbType.Integer, 4),
              new OleDbParameter("@channel_id", OleDbType.Integer, 4),
              new OleDbParameter("@nav_type", OleDbType.VarChar, 50),
              new OleDbParameter("@name", OleDbType.VarChar, 50),
              new OleDbParameter("@title", OleDbType.VarChar, 100),
              new OleDbParameter("@link_url", OleDbType.VarChar, (int) byte.MaxValue),
              new OleDbParameter("@sort_id", OleDbType.Integer, 4),
              new OleDbParameter("@action_type", OleDbType.VarChar, 500),
              new OleDbParameter("@is_lock", OleDbType.Integer, 4),
              new OleDbParameter("@is_sys", OleDbType.Integer, 4)
            };
            oleDbParameterArray2[0].Value = (object) int32;
            oleDbParameterArray2[1].Value = (object) channel_id;
            oleDbParameterArray2[2].Value = (object) DTEnums.NavigationEnum.System.ToString();
            oleDbParameterArray2[3].Value = (object) nav_name;
            oleDbParameterArray2[4].Value = (object) title;
            oleDbParameterArray2[5].Value = (object) link_url;
            oleDbParameterArray2[6].Value = (object) sort_id;
            oleDbParameterArray2[7].Value = (object) action_type;
            oleDbParameterArray2[8].Value = (object) 0;
            oleDbParameterArray2[9].Value = (object) 1;
            DbHelperOleDb.ExecuteSql(oleDbConnection, trans, stringBuilder2.ToString(), oleDbParameterArray2);
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

    public int Add(
      OleDbConnection conn,
      OleDbTransaction trans,
      string parent_name,
      string nav_name,
      string title,
      string link_url,
      int sort_id,
      int channel_id,
      string action_type)
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("select top 1 id from " + this.databaseprefix + nameof (navigation));
      stringBuilder.Append(" where name=@parent_name");
      OleDbParameter[] oleDbParameterArray = new OleDbParameter[1]
      {
        new OleDbParameter("@parent_name", OleDbType.VarChar, 50)
      };
      oleDbParameterArray[0].Value = (object) parent_name;
      object single = DbHelperOleDb.GetSingle(stringBuilder.ToString(), oleDbParameterArray);
      if (single == null)
        return 0;
      int int32 = Convert.ToInt32(single);
      return this.Add(conn, trans, int32, nav_name, title, link_url, sort_id, channel_id, action_type);
    }

    public int Add(
      OleDbConnection conn,
      OleDbTransaction trans,
      int parent_id,
      string nav_name,
      string title,
      string link_url,
      int sort_id,
      int channel_id,
      string action_type)
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("insert into " + this.databaseprefix + "navigation(");
      stringBuilder.Append("parent_id,channel_id,nav_type,[name],title,link_url,sort_id,action_type,is_lock,is_sys)");
      stringBuilder.Append(" values (");
      stringBuilder.Append("@parent_id,@channel_id,@nav_type,@name,@title,@link_url,@sort_id,@action_type,@is_lock,@is_sys)");
      OleDbParameter[] oleDbParameterArray = new OleDbParameter[10]
      {
        new OleDbParameter("@parent_id", OleDbType.Integer, 4),
        new OleDbParameter("@channel_id", OleDbType.Integer, 4),
        new OleDbParameter("@nav_type", OleDbType.VarChar, 50),
        new OleDbParameter("@name", OleDbType.VarChar, 50),
        new OleDbParameter("@title", OleDbType.VarChar, 100),
        new OleDbParameter("@link_url", OleDbType.VarChar, (int) byte.MaxValue),
        new OleDbParameter("@sort_id", OleDbType.Integer, 4),
        new OleDbParameter("@action_type", OleDbType.VarChar, 500),
        new OleDbParameter("@is_lock", OleDbType.Integer, 4),
        new OleDbParameter("@is_sys", OleDbType.Integer, 4)
      };
      oleDbParameterArray[0].Value = (object) parent_id;
      oleDbParameterArray[1].Value = (object) channel_id;
      oleDbParameterArray[2].Value = (object) DTEnums.NavigationEnum.System.ToString();
      oleDbParameterArray[3].Value = (object) nav_name;
      oleDbParameterArray[4].Value = (object) title;
      oleDbParameterArray[5].Value = (object) link_url;
      oleDbParameterArray[6].Value = (object) sort_id;
      oleDbParameterArray[7].Value = (object) action_type;
      oleDbParameterArray[8].Value = (object) 0;
      oleDbParameterArray[9].Value = (object) 1;
      DbHelperOleDb.ExecuteSql(conn, trans, stringBuilder.ToString(), oleDbParameterArray);
      return this.GetMaxId(conn, trans);
    }

    public Rain.Model.navigation GetModel(
      OleDbConnection conn,
      OleDbTransaction trans,
      string nav_name)
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("select top 1 id,parent_id,channel_id,nav_type,[name],title,sub_title,icon_url,link_url,sort_id,is_lock,remark,action_type,is_sys");
      stringBuilder.Append(" from " + this.databaseprefix + "navigation ");
      stringBuilder.Append(" where name=@nav_name");
      OleDbParameter[] oleDbParameterArray = new OleDbParameter[1]
      {
        new OleDbParameter("@nav_name", OleDbType.VarChar, 50)
      };
      oleDbParameterArray[0].Value = (object) nav_name;
      Rain.Model.navigation navigation = new Rain.Model.navigation();
      DataSet dataSet = DbHelperOleDb.Query(conn, trans, stringBuilder.ToString(), oleDbParameterArray);
      if (dataSet.Tables[0].Rows.Count > 0)
        return this.DataRowToModel(dataSet.Tables[0].Rows[0]);
      return (Rain.Model.navigation) null;
    }

    public bool Delete(OleDbConnection conn, OleDbTransaction trans, string nav_name)
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("delete from " + this.databaseprefix + nameof (navigation));
      stringBuilder.Append(" where name=@name");
      OleDbParameter[] oleDbParameterArray = new OleDbParameter[1]
      {
        new OleDbParameter("@name", OleDbType.VarChar, 50)
      };
      oleDbParameterArray[0].Value = (object) nav_name;
      return DbHelperOleDb.ExecuteSql(conn, trans, stringBuilder.ToString(), oleDbParameterArray) > 0;
    }

    private Rain.Model.navigation DataRowToModel(DataRow row)
    {
      Rain.Model.navigation navigation = new Rain.Model.navigation();
      if (row != null)
      {
        if (row["id"] != null && row["id"].ToString() != "")
          navigation.id = int.Parse(row["id"].ToString());
        if (row["parent_id"] != null && row["parent_id"].ToString() != "")
          navigation.parent_id = int.Parse(row["parent_id"].ToString());
        if (row["channel_id"] != null && row["channel_id"].ToString() != "")
          navigation.channel_id = int.Parse(row["channel_id"].ToString());
        if (row["nav_type"] != null)
          navigation.nav_type = row["nav_type"].ToString();
        if (row["name"] != null)
          navigation.name = row["name"].ToString();
        if (row["title"] != null)
          navigation.title = row["title"].ToString();
        if (row["sub_title"] != null)
          navigation.sub_title = row["sub_title"].ToString();
        if (row["icon_url"] != null)
          navigation.icon_url = row["icon_url"].ToString();
        if (row["link_url"] != null)
          navigation.link_url = row["link_url"].ToString();
        if (row["sort_id"] != null && row["sort_id"].ToString() != "")
          navigation.sort_id = int.Parse(row["sort_id"].ToString());
        if (row["is_lock"] != null && row["is_lock"].ToString() != "")
          navigation.is_lock = int.Parse(row["is_lock"].ToString());
        if (row["remark"] != null)
          navigation.remark = row["remark"].ToString();
        if (row["action_type"] != null)
          navigation.action_type = row["action_type"].ToString();
        if (row["is_sys"] != null && row["is_sys"].ToString() != "")
          navigation.is_sys = int.Parse(row["is_sys"].ToString());
      }
      return navigation;
    }

    private void GetChilds(DataTable oldData, DataTable newData, int parent_id, int class_layer)
    {
      ++class_layer;
      DataRow[] dataRowArray = oldData.Select("parent_id=" + (object) parent_id);
      for (int index = 0; index < dataRowArray.Length; ++index)
      {
        DataRow row = newData.NewRow();
        row["id"] = (object) int.Parse(dataRowArray[index]["id"].ToString());
        row[nameof (parent_id)] = (object) int.Parse(dataRowArray[index][nameof (parent_id)].ToString());
        row["channel_id"] = (object) int.Parse(dataRowArray[index]["channel_id"].ToString());
        row[nameof (class_layer)] = (object) class_layer;
        row["nav_type"] = (object) dataRowArray[index]["nav_type"].ToString();
        row["name"] = (object) dataRowArray[index]["name"].ToString();
        row["title"] = (object) dataRowArray[index]["title"].ToString();
        row["sub_title"] = (object) dataRowArray[index]["sub_title"].ToString();
        row["icon_url"] = (object) dataRowArray[index]["icon_url"].ToString();
        row["link_url"] = (object) dataRowArray[index]["link_url"].ToString();
        row["sort_id"] = (object) int.Parse(dataRowArray[index]["sort_id"].ToString());
        row["is_lock"] = (object) int.Parse(dataRowArray[index]["is_lock"].ToString());
        row["remark"] = (object) dataRowArray[index]["remark"].ToString();
        row["action_type"] = (object) dataRowArray[index]["action_type"].ToString();
        row["is_sys"] = (object) int.Parse(dataRowArray[index]["is_sys"].ToString());
        newData.Rows.Add(row);
        this.GetChilds(oldData, newData, int.Parse(dataRowArray[index]["id"].ToString()), class_layer);
      }
    }

    private void GetChildIds(DataTable dt, int parent_id, ref string ids)
    {
      DataRow[] dataRowArray = dt.Select("parent_id=" + (object) parent_id);
      for (int index = 0; index < dataRowArray.Length; ++index)
      {
        ref string local = ref ids;
        local = local + dataRowArray[index]["id"].ToString() + ",";
        this.GetChildIds(dt, int.Parse(dataRowArray[index]["id"].ToString()), ref ids);
      }
    }
  }
}
