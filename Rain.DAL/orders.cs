// Decompiled with JetBrains decompiler
// Type: Rain.DAL.orders
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
using Rain.Model;

namespace Rain.DAL
{
  public class orders
  {
    private string databaseprefix;

    public orders(string _databaseprefix)
    {
      this.databaseprefix = _databaseprefix;
    }

    private int GetMaxId(OleDbConnection conn, OleDbTransaction trans)
    {
      string SQLString = "select top 1 id from " + this.databaseprefix + "orders order by id desc";
      object single = DbHelperOleDb.GetSingle(conn, trans, SQLString);
      if (single == null)
        return 0;
      return int.Parse(single.ToString());
    }

    public bool Exists(int id)
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("select count(1) from " + this.databaseprefix + nameof (orders));
      stringBuilder.Append(" where id=@id ");
      OleDbParameter[] oleDbParameterArray = new OleDbParameter[1]
      {
        new OleDbParameter("@id", OleDbType.Integer, 4)
      };
      oleDbParameterArray[0].Value = (object) id;
      return DbHelperOleDb.Exists(stringBuilder.ToString(), oleDbParameterArray);
    }

    public bool Exists(string order_no)
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("select count(1) from " + this.databaseprefix + nameof (orders));
      stringBuilder.Append(" where order_no=@order_no ");
      OleDbParameter[] oleDbParameterArray = new OleDbParameter[1]
      {
        new OleDbParameter("@order_no", OleDbType.VarChar, 100)
      };
      oleDbParameterArray[0].Value = (object) order_no;
      return DbHelperOleDb.Exists(stringBuilder.ToString(), oleDbParameterArray);
    }

    public int Add(Rain.Model.orders model)
    {
      using (OleDbConnection oleDbConnection = new OleDbConnection(DbHelperOleDb.connectionString))
      {
        oleDbConnection.Open();
        using (OleDbTransaction trans = oleDbConnection.BeginTransaction())
        {
          try
          {
            StringBuilder stringBuilder1 = new StringBuilder();
            stringBuilder1.Append("insert into " + this.databaseprefix + "orders(");
            stringBuilder1.Append("order_no,trade_no,user_id,user_name,payment_id,payment_fee,payment_status,payment_time,express_id,express_no,express_fee,express_status,express_time,accept_name,post_code,telphone,mobile,email,area,address,message,remark,is_invoice,invoice_title,invoice_taxes,payable_amount,real_amount,order_amount,point,status,add_time,confirm_time,complete_time)");
            stringBuilder1.Append(" values (");
            stringBuilder1.Append("@order_no,@trade_no,@user_id,@user_name,@payment_id,@payment_fee,@payment_status,@payment_time,@express_id,@express_no,@express_fee,@express_status,@express_time,@accept_name,@post_code,@telphone,@mobile,@email,@area,@address,@message,@remark,@is_invoice,@invoice_title,@invoice_taxes,@payable_amount,@real_amount,@order_amount,@point,@status,@add_time,@confirm_time,@complete_time)");
            OleDbParameter[] oleDbParameterArray1 = new OleDbParameter[33]
            {
              new OleDbParameter("@order_no", OleDbType.VarChar, 100),
              new OleDbParameter("@trade_no", OleDbType.VarChar, 100),
              new OleDbParameter("@user_id", OleDbType.Integer, 4),
              new OleDbParameter("@user_name", OleDbType.VarChar, 100),
              new OleDbParameter("@payment_id", OleDbType.Integer, 4),
              new OleDbParameter("@payment_fee", OleDbType.Decimal, 5),
              new OleDbParameter("@payment_status", OleDbType.Integer, 4),
              new OleDbParameter("@payment_time", OleDbType.Date),
              new OleDbParameter("@express_id", OleDbType.Integer, 4),
              new OleDbParameter("@express_no", OleDbType.VarChar, 100),
              new OleDbParameter("@express_fee", OleDbType.Decimal, 5),
              new OleDbParameter("@express_status", OleDbType.Integer, 4),
              new OleDbParameter("@express_time", OleDbType.Date),
              new OleDbParameter("@accept_name", OleDbType.VarChar, 50),
              new OleDbParameter("@post_code", OleDbType.VarChar, 20),
              new OleDbParameter("@telphone", OleDbType.VarChar, 30),
              new OleDbParameter("@mobile", OleDbType.VarChar, 20),
              new OleDbParameter("@email", OleDbType.VarChar, 30),
              new OleDbParameter("@area", OleDbType.VarChar, 100),
              new OleDbParameter("@address", OleDbType.VarChar, 500),
              new OleDbParameter("@message", OleDbType.VarChar, 500),
              new OleDbParameter("@remark", OleDbType.VarChar, 500),
              new OleDbParameter("@is_invoice", OleDbType.Integer, 4),
              new OleDbParameter("@invoice_title", OleDbType.VarChar, 100),
              new OleDbParameter("@invoice_taxes", OleDbType.Decimal, 5),
              new OleDbParameter("@payable_amount", OleDbType.Decimal, 5),
              new OleDbParameter("@real_amount", OleDbType.Decimal, 5),
              new OleDbParameter("@order_amount", OleDbType.Decimal, 5),
              new OleDbParameter("@point", OleDbType.Integer, 4),
              new OleDbParameter("@status", OleDbType.Integer, 4),
              new OleDbParameter("@add_time", OleDbType.Date),
              new OleDbParameter("@confirm_time", OleDbType.Date),
              new OleDbParameter("@complete_time", OleDbType.Date)
            };
            oleDbParameterArray1[0].Value = (object) model.order_no;
            oleDbParameterArray1[1].Value = (object) model.trade_no;
            oleDbParameterArray1[2].Value = (object) model.user_id;
            oleDbParameterArray1[3].Value = (object) model.user_name;
            oleDbParameterArray1[4].Value = (object) model.payment_id;
            oleDbParameterArray1[5].Value = (object) model.payment_fee;
            oleDbParameterArray1[6].Value = (object) model.payment_status;
            DateTime? nullable = model.payment_time;
            if (nullable.HasValue)
              oleDbParameterArray1[7].Value = (object) model.payment_time;
            else
              oleDbParameterArray1[7].Value = (object) DBNull.Value;
            oleDbParameterArray1[8].Value = (object) model.express_id;
            oleDbParameterArray1[9].Value = (object) model.express_no;
            oleDbParameterArray1[10].Value = (object) model.express_fee;
            oleDbParameterArray1[11].Value = (object) model.express_status;
            nullable = model.express_time;
            if (nullable.HasValue)
              oleDbParameterArray1[12].Value = (object) model.express_time;
            else
              oleDbParameterArray1[12].Value = (object) DBNull.Value;
            oleDbParameterArray1[13].Value = (object) model.accept_name;
            oleDbParameterArray1[14].Value = (object) model.post_code;
            oleDbParameterArray1[15].Value = (object) model.telphone;
            oleDbParameterArray1[16].Value = (object) model.mobile;
            oleDbParameterArray1[17].Value = (object) model.email;
            oleDbParameterArray1[18].Value = (object) model.area;
            oleDbParameterArray1[19].Value = (object) model.address;
            oleDbParameterArray1[20].Value = (object) model.message;
            oleDbParameterArray1[21].Value = (object) model.remark;
            oleDbParameterArray1[22].Value = (object) model.is_invoice;
            oleDbParameterArray1[23].Value = (object) model.invoice_title;
            oleDbParameterArray1[24].Value = (object) model.invoice_taxes;
            oleDbParameterArray1[25].Value = (object) model.payable_amount;
            oleDbParameterArray1[26].Value = (object) model.real_amount;
            oleDbParameterArray1[27].Value = (object) model.order_amount;
            oleDbParameterArray1[28].Value = (object) model.point;
            oleDbParameterArray1[29].Value = (object) model.status;
            oleDbParameterArray1[30].Value = (object) model.add_time;
            nullable = model.confirm_time;
            if (nullable.HasValue)
              oleDbParameterArray1[31].Value = (object) model.confirm_time;
            else
              oleDbParameterArray1[31].Value = (object) DBNull.Value;
            nullable = model.complete_time;
            if (nullable.HasValue)
              oleDbParameterArray1[32].Value = (object) model.complete_time;
            else
              oleDbParameterArray1[32].Value = (object) DBNull.Value;
            DbHelperOleDb.ExecuteSql(oleDbConnection, trans, stringBuilder1.ToString(), oleDbParameterArray1);
            model.id = this.GetMaxId(oleDbConnection, trans);
            if (model.order_goods != null)
            {
              foreach (order_goods orderGood in model.order_goods)
              {
                StringBuilder stringBuilder2 = new StringBuilder();
                stringBuilder2.Append("insert into " + this.databaseprefix + "order_goods(");
                stringBuilder2.Append("article_id,order_id,goods_no,goods_title,img_url,spec_text,goods_price,real_price,quantity,point)");
                stringBuilder2.Append(" values (");
                stringBuilder2.Append("@article_id,@order_id,@goods_no,@goods_title,@img_url,@spec_text,@goods_price,@real_price,@quantity,@point)");
                OleDbParameter[] oleDbParameterArray2 = new OleDbParameter[10]
                {
                  new OleDbParameter("@article_id", OleDbType.Integer, 4),
                  new OleDbParameter("@order_id", OleDbType.Integer, 4),
                  new OleDbParameter("@goods_no", OleDbType.VarChar, 50),
                  new OleDbParameter("@goods_title", OleDbType.VarChar, 100),
                  new OleDbParameter("@img_url", OleDbType.VarChar, (int) byte.MaxValue),
                  new OleDbParameter("@spec_text", (object) SqlDbType.Text),
                  new OleDbParameter("@goods_price", OleDbType.Decimal, 5),
                  new OleDbParameter("@real_price", OleDbType.Decimal, 5),
                  new OleDbParameter("@quantity", OleDbType.Integer, 4),
                  new OleDbParameter("@point", OleDbType.Integer, 4)
                };
                oleDbParameterArray2[0].Value = (object) orderGood.article_id;
                oleDbParameterArray2[1].Value = (object) model.id;
                oleDbParameterArray2[2].Value = (object) orderGood.goods_no;
                oleDbParameterArray2[3].Value = (object) orderGood.goods_title;
                oleDbParameterArray2[4].Value = (object) orderGood.img_url;
                oleDbParameterArray2[5].Value = (object) orderGood.spec_text;
                oleDbParameterArray2[6].Value = (object) orderGood.goods_price;
                oleDbParameterArray2[7].Value = (object) orderGood.real_price;
                oleDbParameterArray2[8].Value = (object) orderGood.quantity;
                oleDbParameterArray2[9].Value = (object) orderGood.point;
                DbHelperOleDb.ExecuteSql(oleDbConnection, trans, stringBuilder2.ToString(), oleDbParameterArray2);
                StringBuilder stringBuilder3 = new StringBuilder();
                stringBuilder3.Append("update " + this.databaseprefix + "article_attribute_value set ");
                stringBuilder3.Append("stock_quantity=stock_quantity-@stock_quantity where article_id=@article_id");
                OleDbParameter[] oleDbParameterArray3 = new OleDbParameter[2]
                {
                  new OleDbParameter("@stock_quantity", OleDbType.Integer, 4),
                  new OleDbParameter("@article_id", OleDbType.Integer, 4)
                };
                oleDbParameterArray3[0].Value = (object) orderGood.quantity;
                oleDbParameterArray3[1].Value = (object) orderGood.article_id;
                DbHelperOleDb.ExecuteSql(oleDbConnection, trans, stringBuilder3.ToString(), oleDbParameterArray3);
              }
            }
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

    public bool Update(Rain.Model.orders model)
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("update " + this.databaseprefix + "orders set ");
      stringBuilder.Append("order_no=@order_no,");
      stringBuilder.Append("trade_no=@trade_no,");
      stringBuilder.Append("user_id=@user_id,");
      stringBuilder.Append("user_name=@user_name,");
      stringBuilder.Append("payment_id=@payment_id,");
      stringBuilder.Append("payment_fee=@payment_fee,");
      stringBuilder.Append("payment_status=@payment_status,");
      stringBuilder.Append("payment_time=@payment_time,");
      stringBuilder.Append("express_id=@express_id,");
      stringBuilder.Append("express_no=@express_no,");
      stringBuilder.Append("express_fee=@express_fee,");
      stringBuilder.Append("express_status=@express_status,");
      stringBuilder.Append("express_time=@express_time,");
      stringBuilder.Append("accept_name=@accept_name,");
      stringBuilder.Append("post_code=@post_code,");
      stringBuilder.Append("telphone=@telphone,");
      stringBuilder.Append("mobile=@mobile,");
      stringBuilder.Append("email=@email,");
      stringBuilder.Append("area=@area,");
      stringBuilder.Append("address=@address,");
      stringBuilder.Append("message=@message,");
      stringBuilder.Append("remark=@remark,");
      stringBuilder.Append("is_invoice=@is_invoice,");
      stringBuilder.Append("invoice_title=@invoice_title,");
      stringBuilder.Append("invoice_taxes=@invoice_taxes,");
      stringBuilder.Append("payable_amount=@payable_amount,");
      stringBuilder.Append("real_amount=@real_amount,");
      stringBuilder.Append("order_amount=@order_amount,");
      stringBuilder.Append("point=@point,");
      stringBuilder.Append("status=@status,");
      stringBuilder.Append("add_time=@add_time,");
      stringBuilder.Append("confirm_time=@confirm_time,");
      stringBuilder.Append("complete_time=@complete_time");
      stringBuilder.Append(" where id=@id");
      OleDbParameter[] oleDbParameterArray = new OleDbParameter[34]
      {
        new OleDbParameter("@order_no", OleDbType.VarChar, 100),
        new OleDbParameter("@trade_no", OleDbType.VarChar, 100),
        new OleDbParameter("@user_id", OleDbType.Integer, 4),
        new OleDbParameter("@user_name", OleDbType.VarChar, 100),
        new OleDbParameter("@payment_id", OleDbType.Integer, 4),
        new OleDbParameter("@payment_fee", OleDbType.Decimal, 5),
        new OleDbParameter("@payment_status", OleDbType.Integer, 4),
        new OleDbParameter("@payment_time", OleDbType.Date),
        new OleDbParameter("@express_id", OleDbType.Integer, 4),
        new OleDbParameter("@express_no", OleDbType.VarChar, 100),
        new OleDbParameter("@express_fee", OleDbType.Decimal, 5),
        new OleDbParameter("@express_status", OleDbType.Integer, 4),
        new OleDbParameter("@express_time", OleDbType.Date),
        new OleDbParameter("@accept_name", OleDbType.VarChar, 50),
        new OleDbParameter("@post_code", OleDbType.VarChar, 20),
        new OleDbParameter("@telphone", OleDbType.VarChar, 30),
        new OleDbParameter("@mobile", OleDbType.VarChar, 20),
        new OleDbParameter("@email", OleDbType.VarChar, 30),
        new OleDbParameter("@area", OleDbType.VarChar, 100),
        new OleDbParameter("@address", OleDbType.VarChar, 500),
        new OleDbParameter("@message", OleDbType.VarChar, 500),
        new OleDbParameter("@remark", OleDbType.VarChar, 500),
        new OleDbParameter("@is_invoice", OleDbType.Integer, 4),
        new OleDbParameter("@invoice_title", OleDbType.VarChar, 100),
        new OleDbParameter("@invoice_taxes", OleDbType.Decimal, 5),
        new OleDbParameter("@payable_amount", OleDbType.Decimal, 5),
        new OleDbParameter("@real_amount", OleDbType.Decimal, 5),
        new OleDbParameter("@order_amount", OleDbType.Decimal, 5),
        new OleDbParameter("@point", OleDbType.Integer, 4),
        new OleDbParameter("@status", OleDbType.Integer, 4),
        new OleDbParameter("@add_time", OleDbType.Date),
        new OleDbParameter("@confirm_time", OleDbType.Date),
        new OleDbParameter("@complete_time", OleDbType.Date),
        new OleDbParameter("@id", OleDbType.Integer, 4)
      };
      oleDbParameterArray[0].Value = (object) model.order_no;
      oleDbParameterArray[1].Value = (object) model.trade_no;
      oleDbParameterArray[2].Value = (object) model.user_id;
      oleDbParameterArray[3].Value = (object) model.user_name;
      oleDbParameterArray[4].Value = (object) model.payment_id;
      oleDbParameterArray[5].Value = (object) model.payment_fee;
      oleDbParameterArray[6].Value = (object) model.payment_status;
      if (model.payment_time.HasValue)
        oleDbParameterArray[7].Value = (object) model.payment_time;
      else
        oleDbParameterArray[7].Value = (object) DBNull.Value;
      oleDbParameterArray[8].Value = (object) model.express_id;
      oleDbParameterArray[9].Value = (object) model.express_no;
      oleDbParameterArray[10].Value = (object) model.express_fee;
      oleDbParameterArray[11].Value = (object) model.express_status;
      DateTime? nullable = model.express_time;
      if (nullable.HasValue)
        oleDbParameterArray[12].Value = (object) model.express_time;
      else
        oleDbParameterArray[12].Value = (object) DBNull.Value;
      oleDbParameterArray[13].Value = (object) model.accept_name;
      oleDbParameterArray[14].Value = (object) model.post_code;
      oleDbParameterArray[15].Value = (object) model.telphone;
      oleDbParameterArray[16].Value = (object) model.mobile;
      oleDbParameterArray[17].Value = (object) model.email;
      oleDbParameterArray[18].Value = (object) model.area;
      oleDbParameterArray[19].Value = (object) model.address;
      oleDbParameterArray[20].Value = (object) model.message;
      oleDbParameterArray[21].Value = (object) model.remark;
      oleDbParameterArray[22].Value = (object) model.is_invoice;
      oleDbParameterArray[23].Value = (object) model.invoice_title;
      oleDbParameterArray[24].Value = (object) model.invoice_taxes;
      oleDbParameterArray[25].Value = (object) model.payable_amount;
      oleDbParameterArray[26].Value = (object) model.real_amount;
      oleDbParameterArray[27].Value = (object) model.order_amount;
      oleDbParameterArray[28].Value = (object) model.point;
      oleDbParameterArray[29].Value = (object) model.status;
      oleDbParameterArray[30].Value = (object) model.add_time;
      nullable = model.confirm_time;
      if (nullable.HasValue)
        oleDbParameterArray[31].Value = (object) model.confirm_time;
      else
        oleDbParameterArray[31].Value = (object) DBNull.Value;
      nullable = model.complete_time;
      if (nullable.HasValue)
        oleDbParameterArray[32].Value = (object) model.complete_time;
      else
        oleDbParameterArray[32].Value = (object) DBNull.Value;
      oleDbParameterArray[33].Value = (object) model.id;
      return DbHelperOleDb.ExecuteSql(stringBuilder.ToString(), oleDbParameterArray) > 0;
    }

    public bool Delete(int id)
    {
      Hashtable SQLStringList = new Hashtable();
      StringBuilder stringBuilder1 = new StringBuilder();
      stringBuilder1.Append("delete from " + this.databaseprefix + "order_goods ");
      stringBuilder1.Append(" where order_id=@order_id ");
      OleDbParameter[] oleDbParameterArray1 = new OleDbParameter[1]
      {
        new OleDbParameter("@order_id", OleDbType.Integer, 4)
      };
      oleDbParameterArray1[0].Value = (object) id;
      SQLStringList.Add((object) stringBuilder1.ToString(), (object) oleDbParameterArray1);
      StringBuilder stringBuilder2 = new StringBuilder();
      stringBuilder2.Append("delete from " + this.databaseprefix + "orders ");
      stringBuilder2.Append(" where id=@id");
      OleDbParameter[] oleDbParameterArray2 = new OleDbParameter[1]
      {
        new OleDbParameter("@id", OleDbType.Integer, 4)
      };
      oleDbParameterArray2[0].Value = (object) id;
      SQLStringList.Add((object) stringBuilder2.ToString(), (object) oleDbParameterArray2);
      return DbHelperOleDb.ExecuteSqlTran(SQLStringList);
    }

    public Rain.Model.orders GetModel(int id)
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("select top 1 id,order_no,trade_no,user_id,user_name,payment_id,payment_fee,payment_status,payment_time,express_id,express_no,express_fee,express_status,express_time,accept_name,post_code,telphone,mobile,email,area,address,message,remark,is_invoice,invoice_title,invoice_taxes,payable_amount,real_amount,order_amount,point,status,add_time,confirm_time,complete_time");
      stringBuilder.Append(" from " + this.databaseprefix + nameof (orders));
      stringBuilder.Append(" where id=@id");
      OleDbParameter[] oleDbParameterArray = new OleDbParameter[1]
      {
        new OleDbParameter("@id", OleDbType.Integer, 4)
      };
      oleDbParameterArray[0].Value = (object) id;
      DataSet dataSet = DbHelperOleDb.Query(stringBuilder.ToString(), oleDbParameterArray);
      if (dataSet.Tables[0].Rows.Count > 0)
        return this.DataRowToModel(dataSet.Tables[0].Rows[0]);
      return (Rain.Model.orders) null;
    }

    public Rain.Model.orders GetModel(string order_no)
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("select top 1 id,order_no,trade_no,user_id,user_name,payment_id,payment_fee,payment_status,payment_time,express_id,express_no,express_fee,express_status,express_time,accept_name,post_code,telphone,mobile,email,area,address,message,remark,is_invoice,invoice_title,invoice_taxes,payable_amount,real_amount,order_amount,point,status,add_time,confirm_time,complete_time");
      stringBuilder.Append(" from " + this.databaseprefix + nameof (orders));
      stringBuilder.Append(" where order_no=@order_no");
      OleDbParameter[] oleDbParameterArray = new OleDbParameter[1]
      {
        new OleDbParameter("@order_no", OleDbType.VarChar, 100)
      };
      oleDbParameterArray[0].Value = (object) order_no;
      DataSet dataSet = DbHelperOleDb.Query(stringBuilder.ToString(), oleDbParameterArray);
      if (dataSet.Tables[0].Rows.Count > 0)
        return this.DataRowToModel(dataSet.Tables[0].Rows[0]);
      return (Rain.Model.orders) null;
    }

    public DataSet GetList(int Top, string strWhere, string filedOrder)
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("select ");
      if (Top > 0)
        stringBuilder.Append(" top " + Top.ToString());
      stringBuilder.Append(" id,order_no,trade_no,user_id,user_name,payment_id,payment_fee,payment_status,payment_time,express_id,express_no,express_fee,express_status,express_time,accept_name,post_code,telphone,mobile,email,area,address,message,remark,is_invoice,invoice_title,invoice_taxes,payable_amount,real_amount,order_amount,point,status,add_time,confirm_time,complete_time ");
      stringBuilder.Append(" FROM " + this.databaseprefix + "orders ");
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
      stringBuilder.Append("select * FROM " + this.databaseprefix + nameof (orders));
      if (strWhere.Trim() != "")
        stringBuilder.Append(" where " + strWhere);
      recordCount = Convert.ToInt32(DbHelperOleDb.GetSingle(PagingHelper.CreateCountingSql(stringBuilder.ToString())));
      return DbHelperOleDb.Query(PagingHelper.CreatePagingSql(recordCount, pageSize, pageIndex, stringBuilder.ToString(), filedOrder));
    }

    public int GetCount(string strWhere)
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("select count(*) as H from " + this.databaseprefix + "orders ");
      if (strWhere.Trim() != "")
        stringBuilder.Append(" where " + strWhere);
      return Convert.ToInt32(DbHelperOleDb.GetSingle(stringBuilder.ToString()));
    }

    public void UpdateField(int id, string strValue)
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("update " + this.databaseprefix + "orders set " + strValue);
      stringBuilder.Append(" where id=" + (object) id);
      DbHelperOleDb.ExecuteSql(stringBuilder.ToString());
    }

    public bool UpdateField(string order_no, string strValue)
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("update " + this.databaseprefix + "orders set " + strValue);
      stringBuilder.Append(" where order_no='" + order_no + "'");
      return DbHelperOleDb.ExecuteSql(stringBuilder.ToString()) > 0;
    }

    public Rain.Model.orders DataRowToModel(DataRow row)
    {
      Rain.Model.orders orders = new Rain.Model.orders();
      if (row != null)
      {
        if (row["id"] != null && row["id"].ToString() != "")
          orders.id = int.Parse(row["id"].ToString());
        if (row["order_no"] != null)
          orders.order_no = row["order_no"].ToString();
        if (row["trade_no"] != null)
          orders.trade_no = row["trade_no"].ToString();
        if (row["user_id"] != null && row["user_id"].ToString() != "")
          orders.user_id = int.Parse(row["user_id"].ToString());
        if (row["user_name"] != null)
          orders.user_name = row["user_name"].ToString();
        if (row["payment_id"] != null && row["payment_id"].ToString() != "")
          orders.payment_id = int.Parse(row["payment_id"].ToString());
        if (row["payment_fee"] != null && row["payment_fee"].ToString() != "")
          orders.payment_fee = Decimal.Parse(row["payment_fee"].ToString());
        if (row["payment_status"] != null && row["payment_status"].ToString() != "")
          orders.payment_status = int.Parse(row["payment_status"].ToString());
        if (row["payment_time"] != null && row["payment_time"].ToString() != "")
          orders.payment_time = new DateTime?(DateTime.Parse(row["payment_time"].ToString()));
        if (row["express_id"] != null && row["express_id"].ToString() != "")
          orders.express_id = int.Parse(row["express_id"].ToString());
        if (row["express_no"] != null)
          orders.express_no = row["express_no"].ToString();
        if (row["express_fee"] != null && row["express_fee"].ToString() != "")
          orders.express_fee = Decimal.Parse(row["express_fee"].ToString());
        if (row["express_status"] != null && row["express_status"].ToString() != "")
          orders.express_status = int.Parse(row["express_status"].ToString());
        if (row["express_time"] != null && row["express_time"].ToString() != "")
          orders.express_time = new DateTime?(DateTime.Parse(row["express_time"].ToString()));
        if (row["accept_name"] != null)
          orders.accept_name = row["accept_name"].ToString();
        if (row["post_code"] != null)
          orders.post_code = row["post_code"].ToString();
        if (row["telphone"] != null)
          orders.telphone = row["telphone"].ToString();
        if (row["mobile"] != null)
          orders.mobile = row["mobile"].ToString();
        if (row["email"] != null)
          orders.email = row["email"].ToString();
        if (row["area"] != null)
          orders.area = row["area"].ToString();
        if (row["address"] != null)
          orders.address = row["address"].ToString();
        if (row["message"] != null)
          orders.message = row["message"].ToString();
        if (row["remark"] != null)
          orders.remark = row["remark"].ToString();
        if (row["is_invoice"] != null && row["is_invoice"].ToString() != "")
          orders.is_invoice = int.Parse(row["is_invoice"].ToString());
        if (row["invoice_title"] != null)
          orders.invoice_title = row["invoice_title"].ToString();
        if (row["invoice_taxes"] != null && row["invoice_taxes"].ToString() != "")
          orders.invoice_taxes = Decimal.Parse(row["invoice_taxes"].ToString());
        if (row["payable_amount"] != null && row["payable_amount"].ToString() != "")
          orders.payable_amount = Decimal.Parse(row["payable_amount"].ToString());
        if (row["real_amount"] != null && row["real_amount"].ToString() != "")
          orders.real_amount = Decimal.Parse(row["real_amount"].ToString());
        if (row["order_amount"] != null && row["order_amount"].ToString() != "")
          orders.order_amount = Decimal.Parse(row["order_amount"].ToString());
        if (row["point"] != null && row["point"].ToString() != "")
          orders.point = int.Parse(row["point"].ToString());
        if (row["status"] != null && row["status"].ToString() != "")
          orders.status = int.Parse(row["status"].ToString());
        if (row["add_time"] != null && row["add_time"].ToString() != "")
          orders.add_time = DateTime.Parse(row["add_time"].ToString());
        if (row["confirm_time"] != null && row["confirm_time"].ToString() != "")
          orders.confirm_time = new DateTime?(DateTime.Parse(row["confirm_time"].ToString()));
        if (row["complete_time"] != null && row["complete_time"].ToString() != "")
          orders.complete_time = new DateTime?(DateTime.Parse(row["complete_time"].ToString()));
        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.Append("select id,article_id,order_id,goods_no,goods_title,img_url,spec_text,goods_price,real_price,quantity,point");
        stringBuilder.Append(" from " + this.databaseprefix + "order_goods ");
        stringBuilder.Append(" where order_id=@id ");
        OleDbParameter[] oleDbParameterArray = new OleDbParameter[1]
        {
          new OleDbParameter("@id", OleDbType.Integer, 4)
        };
        oleDbParameterArray[0].Value = (object) orders.id;
        DataSet dataSet = DbHelperOleDb.Query(stringBuilder.ToString(), oleDbParameterArray);
        if (dataSet.Tables[0].Rows.Count > 0)
        {
          List<order_goods> orderGoodsList = new List<order_goods>();
          for (int index = 0; index < dataSet.Tables[0].Rows.Count; ++index)
          {
            order_goods orderGoods = new order_goods();
            if (dataSet.Tables[0].Rows[index]["id"] != null && dataSet.Tables[0].Rows[index]["id"].ToString() != "")
              orderGoods.id = int.Parse(dataSet.Tables[0].Rows[index]["id"].ToString());
            if (dataSet.Tables[0].Rows[index]["article_id"] != null && dataSet.Tables[0].Rows[index]["article_id"].ToString() != "")
              orderGoods.article_id = int.Parse(dataSet.Tables[0].Rows[index]["article_id"].ToString());
            if (dataSet.Tables[0].Rows[index]["order_id"] != null && dataSet.Tables[0].Rows[index]["order_id"].ToString() != "")
              orderGoods.order_id = int.Parse(dataSet.Tables[0].Rows[index]["order_id"].ToString());
            if (dataSet.Tables[0].Rows[index]["goods_no"] != null)
              orderGoods.goods_no = dataSet.Tables[0].Rows[index]["goods_no"].ToString();
            if (dataSet.Tables[0].Rows[index]["goods_title"] != null)
              orderGoods.goods_title = dataSet.Tables[0].Rows[index]["goods_title"].ToString();
            if (dataSet.Tables[0].Rows[index]["img_url"] != null)
              orderGoods.img_url = dataSet.Tables[0].Rows[index]["img_url"].ToString();
            if (dataSet.Tables[0].Rows[index]["spec_text"] != null)
              orderGoods.spec_text = dataSet.Tables[0].Rows[index]["spec_text"].ToString();
            if (dataSet.Tables[0].Rows[index]["goods_price"] != null && dataSet.Tables[0].Rows[index]["goods_price"].ToString() != "")
              orderGoods.goods_price = Decimal.Parse(dataSet.Tables[0].Rows[index]["goods_price"].ToString());
            if (dataSet.Tables[0].Rows[index]["real_price"] != null && dataSet.Tables[0].Rows[index]["real_price"].ToString() != "")
              orderGoods.real_price = Decimal.Parse(dataSet.Tables[0].Rows[index]["real_price"].ToString());
            if (dataSet.Tables[0].Rows[index]["quantity"] != null && dataSet.Tables[0].Rows[index]["quantity"].ToString() != "")
              orderGoods.quantity = int.Parse(dataSet.Tables[0].Rows[index]["quantity"].ToString());
            if (dataSet.Tables[0].Rows[index]["point"] != null && dataSet.Tables[0].Rows[index]["point"].ToString() != "")
              orderGoods.point = int.Parse(dataSet.Tables[0].Rows[index]["point"].ToString());
            orderGoodsList.Add(orderGoods);
          }
          orders.order_goods = orderGoodsList;
        }
      }
      return orders;
    }
  }
}
