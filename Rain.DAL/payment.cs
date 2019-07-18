// Decompiled with JetBrains decompiler
// Type: Rain.DAL.payment
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
  public class payment
  {
    private string databaseprefix;

    public payment(string _databaseprefix)
    {
      this.databaseprefix = _databaseprefix;
    }

    private int GetMaxId(OleDbConnection conn, OleDbTransaction trans)
    {
      string SQLString = "select top 1 id from " + this.databaseprefix + "payment order by id desc";
      object single = DbHelperOleDb.GetSingle(conn, trans, SQLString);
      if (single == null)
        return 0;
      return int.Parse(single.ToString());
    }

    public bool Exists(int id)
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("select count(1) from " + this.databaseprefix + nameof (payment));
      stringBuilder.Append(" where id=@id");
      OleDbParameter[] oleDbParameterArray = new OleDbParameter[1]
      {
        new OleDbParameter("@id", OleDbType.Integer, 4)
      };
      oleDbParameterArray[0].Value = (object) id;
      return DbHelperOleDb.Exists(stringBuilder.ToString(), oleDbParameterArray);
    }

    public int Add(Rain.Model.payment model)
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
            stringBuilder.Append("insert into " + this.databaseprefix + "payment(");
            stringBuilder.Append("title,img_url,[remark],type,poundage_type,poundage_amount,sort_id,is_lock,api_path)");
            stringBuilder.Append(" values (");
            stringBuilder.Append("@title,@img_url,@remark,@type,@poundage_type,@poundage_amount,@sort_id,@is_lock,@api_path)");
            OleDbParameter[] oleDbParameterArray = new OleDbParameter[9]
            {
              new OleDbParameter("@title", OleDbType.VarChar, 100),
              new OleDbParameter("@img_url", OleDbType.VarChar, (int) byte.MaxValue),
              new OleDbParameter("@remark", OleDbType.VarChar, 500),
              new OleDbParameter("@type", OleDbType.Integer, 4),
              new OleDbParameter("@poundage_type", OleDbType.Integer, 4),
              new OleDbParameter("@poundage_amount", OleDbType.Decimal, 5),
              new OleDbParameter("@sort_id", OleDbType.Integer, 4),
              new OleDbParameter("@is_lock", OleDbType.Integer, 4),
              new OleDbParameter("@api_path", OleDbType.VarChar, 100)
            };
            oleDbParameterArray[0].Value = (object) model.title;
            oleDbParameterArray[1].Value = (object) model.img_url;
            oleDbParameterArray[2].Value = (object) model.remark;
            oleDbParameterArray[3].Value = (object) model.type;
            oleDbParameterArray[4].Value = (object) model.poundage_type;
            oleDbParameterArray[5].Value = (object) model.poundage_amount;
            oleDbParameterArray[6].Value = (object) model.sort_id;
            oleDbParameterArray[7].Value = (object) model.is_lock;
            oleDbParameterArray[8].Value = (object) model.api_path;
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

    public bool Update(Rain.Model.payment model)
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("update " + this.databaseprefix + "payment set ");
      stringBuilder.Append("title=@title,");
      stringBuilder.Append("img_url=@img_url,");
      stringBuilder.Append("[remark]=@remark,");
      stringBuilder.Append("type=@type,");
      stringBuilder.Append("poundage_type=@poundage_type,");
      stringBuilder.Append("poundage_amount=@poundage_amount,");
      stringBuilder.Append("sort_id=@sort_id,");
      stringBuilder.Append("is_lock=@is_lock,");
      stringBuilder.Append("api_path=@api_path");
      stringBuilder.Append(" where id=@id");
      OleDbParameter[] oleDbParameterArray = new OleDbParameter[10]
      {
        new OleDbParameter("@title", OleDbType.VarChar, 100),
        new OleDbParameter("@img_url", OleDbType.VarChar, (int) byte.MaxValue),
        new OleDbParameter("@remark", OleDbType.VarChar, 500),
        new OleDbParameter("@type", OleDbType.Integer, 4),
        new OleDbParameter("@poundage_type", OleDbType.Integer, 4),
        new OleDbParameter("@poundage_amount", OleDbType.Decimal, 5),
        new OleDbParameter("@sort_id", OleDbType.Integer, 4),
        new OleDbParameter("@is_lock", OleDbType.Integer, 4),
        new OleDbParameter("@api_path", OleDbType.VarChar, 100),
        new OleDbParameter("@id", OleDbType.Integer, 4)
      };
      oleDbParameterArray[0].Value = (object) model.title;
      oleDbParameterArray[1].Value = (object) model.img_url;
      oleDbParameterArray[2].Value = (object) model.remark;
      oleDbParameterArray[3].Value = (object) model.type;
      oleDbParameterArray[4].Value = (object) model.poundage_type;
      oleDbParameterArray[5].Value = (object) model.poundage_amount;
      oleDbParameterArray[6].Value = (object) model.sort_id;
      oleDbParameterArray[7].Value = (object) model.is_lock;
      oleDbParameterArray[8].Value = (object) model.api_path;
      oleDbParameterArray[9].Value = (object) model.id;
      return DbHelperOleDb.ExecuteSql(stringBuilder.ToString(), oleDbParameterArray) > 0;
    }

    public bool Delete(int id)
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("delete from " + this.databaseprefix + "payment ");
      stringBuilder.Append(" where id=@id");
      OleDbParameter[] oleDbParameterArray = new OleDbParameter[1]
      {
        new OleDbParameter("@id", OleDbType.Integer, 4)
      };
      oleDbParameterArray[0].Value = (object) id;
      return DbHelperOleDb.ExecuteSql(stringBuilder.ToString(), oleDbParameterArray) > 0;
    }

    public Rain.Model.payment GetModel(int id)
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("select top 1 id,title,img_url,[remark],type,poundage_type,poundage_amount,sort_id,is_lock,api_path");
      stringBuilder.Append(" from " + this.databaseprefix + nameof (Rain.Model.payment));
      stringBuilder.Append(" where id=@id");
      OleDbParameter[] oleDbParameterArray = new OleDbParameter[1]
      {
        new OleDbParameter("@id", OleDbType.Integer, 4)
      };
      oleDbParameterArray[0].Value = (object) id;
      Rain.Model.payment payment = new Rain.Model.payment();
      DataSet dataSet = DbHelperOleDb.Query(stringBuilder.ToString(), oleDbParameterArray);
      if (dataSet.Tables[0].Rows.Count > 0)
        return this.DataRowToModel(dataSet.Tables[0].Rows[0]);
      return (Rain.Model.payment) null;
    }

    public DataSet GetList(string strWhere)
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("select id,title,img_url,[remark],type,poundage_type,poundage_amount,sort_id,is_lock,api_path ");
      stringBuilder.Append(" FROM " + this.databaseprefix + "payment ");
      if (strWhere.Trim() != "")
        stringBuilder.Append(" where " + strWhere);
      stringBuilder.Append(" order by sort_id asc,id desc");
      return DbHelperOleDb.Query(stringBuilder.ToString());
    }

    public DataSet GetList(int Top, string strWhere, string filedOrder)
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("select ");
      if (Top > 0)
        stringBuilder.Append(" top " + Top.ToString());
      stringBuilder.Append(" id,title,img_url,[remark],type,poundage_type,poundage_amount,sort_id,is_lock,api_path ");
      stringBuilder.Append(" FROM " + this.databaseprefix + "payment ");
      if (strWhere.Trim() != "")
        stringBuilder.Append(" where " + strWhere);
      stringBuilder.Append(" order by " + filedOrder);
      return DbHelperOleDb.Query(stringBuilder.ToString());
    }

    public string GetTitle(int id)
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("select top 1 title from " + this.databaseprefix + nameof (payment));
      stringBuilder.Append(" where id=" + (object) id);
      string str = Convert.ToString(DbHelperOleDb.GetSingle(stringBuilder.ToString()));
      if (string.IsNullOrEmpty(str))
        return "-";
      return str;
    }

    public void UpdateField(int id, string strValue)
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("update " + this.databaseprefix + "payment set " + strValue);
      stringBuilder.Append(" where id=" + (object) id);
      DbHelperOleDb.ExecuteSql(stringBuilder.ToString());
    }

    public Rain.Model.payment DataRowToModel(DataRow row)
    {
      Rain.Model.payment payment = new Rain.Model.payment();
      if (row != null)
      {
        if (row["id"] != null && row["id"].ToString() != "")
          payment.id = int.Parse(row["id"].ToString());
        if (row["title"] != null)
          payment.title = row["title"].ToString();
        if (row["img_url"] != null)
          payment.img_url = row["img_url"].ToString();
        if (row["remark"] != null)
          payment.remark = row["remark"].ToString();
        if (row["type"] != null && row["type"].ToString() != "")
          payment.type = int.Parse(row["type"].ToString());
        if (row["poundage_type"] != null && row["poundage_type"].ToString() != "")
          payment.poundage_type = int.Parse(row["poundage_type"].ToString());
        if (row["poundage_amount"] != null && row["poundage_amount"].ToString() != "")
          payment.poundage_amount = Decimal.Parse(row["poundage_amount"].ToString());
        if (row["sort_id"] != null && row["sort_id"].ToString() != "")
          payment.sort_id = int.Parse(row["sort_id"].ToString());
        if (row["is_lock"] != null && row["is_lock"].ToString() != "")
          payment.is_lock = int.Parse(row["is_lock"].ToString());
        if (row["api_path"] != null)
          payment.api_path = row["api_path"].ToString();
      }
      return payment;
    }
  }
}
