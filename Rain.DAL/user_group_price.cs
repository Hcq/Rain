// Decompiled with JetBrains decompiler
// Type: Rain.DAL.user_group_price
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
  public class user_group_price
  {
    private string databaseprefix;

    public user_group_price(string _databaseprefix)
    {
      this.databaseprefix = _databaseprefix;
    }

    public Rain.Model.user_group_price GetModel(int goods_id, int group_id)
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("select top 1 id,article_id,goods_id,group_id,price from " + this.databaseprefix + "user_group_price ");
      stringBuilder.Append(" where goods_id=@goods_id and group_id=@group_id");
      OleDbParameter[] oleDbParameterArray = new OleDbParameter[2]
      {
        new OleDbParameter("@goods_id", OleDbType.Integer, 4),
        new OleDbParameter("@group_id", OleDbType.Integer, 4)
      };
      oleDbParameterArray[0].Value = (object) goods_id;
      oleDbParameterArray[1].Value = (object) group_id;
      Rain.Model.user_group_price userGroupPrice = new Rain.Model.user_group_price();
      DataSet dataSet = DbHelperOleDb.Query(stringBuilder.ToString(), oleDbParameterArray);
      if (dataSet.Tables[0].Rows.Count > 0)
        return this.DataRowToModel(dataSet.Tables[0].Rows[0]);
      return (Rain.Model.user_group_price) null;
    }

    public Rain.Model.user_group_price DataRowToModel(DataRow row)
    {
      Rain.Model.user_group_price userGroupPrice = new Rain.Model.user_group_price();
      if (row != null)
      {
        if (row["id"] != null && row["id"].ToString() != "")
          userGroupPrice.id = int.Parse(row["id"].ToString());
        if (row["article_id"] != null && row["article_id"].ToString() != "")
          userGroupPrice.article_id = int.Parse(row["article_id"].ToString());
        if (row["group_id"] != null && row["group_id"].ToString() != "")
          userGroupPrice.group_id = int.Parse(row["group_id"].ToString());
        if (row["price"] != null && row["price"].ToString() != "")
          userGroupPrice.price = Decimal.Parse(row["price"].ToString());
      }
      return userGroupPrice;
    }
  }
}
