// Decompiled with JetBrains decompiler
// Type: Rain.DAL.users
// Assembly: Rain.DAL, Version=4.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 34D3CCEA-896B-46C7-93D3-7BA93E9004E2
// Assembly location: C:\Users\Felix.Ho\Desktop\page\bin\Rain.DAL.dll

using System;
using System.Collections;
using System.Data;
using System.Data.OleDb;
using System.Text;
using Rain.Common;
using Rain.DBUtility;

namespace Rain.DAL
{
  public class users
  {
    private string databaseprefix;

    public users(string _databaseprefix)
    {
      this.databaseprefix = _databaseprefix;
    }

    private int GetMaxId(OleDbConnection conn, OleDbTransaction trans)
    {
      string SQLString = "select top 1 id from " + this.databaseprefix + "users order by id desc";
      object single = DbHelperOleDb.GetSingle(conn, trans, SQLString);
      if (single == null)
        return 0;
      return int.Parse(single.ToString());
    }

    public bool Exists(int id)
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("select count(1) from " + this.databaseprefix + nameof (users));
      stringBuilder.Append(" where id=@id ");
      OleDbParameter[] oleDbParameterArray = new OleDbParameter[1]
      {
        new OleDbParameter("@id", OleDbType.Integer, 4)
      };
      oleDbParameterArray[0].Value = (object) id;
      return DbHelperOleDb.Exists(stringBuilder.ToString(), oleDbParameterArray);
    }

    public bool Exists(string user_name)
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("select count(1) from " + this.databaseprefix + nameof (users));
      stringBuilder.Append(" where user_name=@user_name ");
      OleDbParameter[] oleDbParameterArray = new OleDbParameter[1]
      {
        new OleDbParameter("@user_name", OleDbType.VarChar, 100)
      };
      oleDbParameterArray[0].Value = (object) user_name;
      return DbHelperOleDb.Exists(stringBuilder.ToString(), oleDbParameterArray);
    }

    public bool Exists(string reg_ip, int regctrl)
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("select count(1) from " + this.databaseprefix + nameof (users));
      stringBuilder.Append(" where reg_ip=@reg_ip and DATEDIFF(hh,reg_time,date())<@regctrl ");
      OleDbParameter[] oleDbParameterArray = new OleDbParameter[2]
      {
        new OleDbParameter("@reg_ip", OleDbType.VarChar, 30),
        new OleDbParameter("@regctrl", OleDbType.Integer, 4)
      };
      oleDbParameterArray[0].Value = (object) reg_ip;
      oleDbParameterArray[1].Value = (object) regctrl;
      return DbHelperOleDb.Exists(stringBuilder.ToString(), oleDbParameterArray);
    }

    public int Add(Rain.Model.users model)
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
            stringBuilder.Append("insert into " + this.databaseprefix + "users(");
            stringBuilder.Append("group_id,user_name,salt,[password],mobile,email,avatar,nick_name,sex,birthday,telphone,area,address,qq,msn,amount,point,exp,status,reg_time,reg_ip)");
            stringBuilder.Append(" values (");
            stringBuilder.Append("@group_id,@user_name,@salt,@password,@mobile,@email,@avatar,@nick_name,@sex,@birthday,@telphone,@area,@address,@qq,@msn,@amount,@point,@exp,@status,@reg_time,@reg_ip)");
            OleDbParameter[] oleDbParameterArray = new OleDbParameter[21]
            {
              new OleDbParameter("@group_id", OleDbType.Integer, 4),
              new OleDbParameter("@user_name", OleDbType.VarChar, 100),
              new OleDbParameter("@salt", OleDbType.VarChar, 20),
              new OleDbParameter("@password", OleDbType.VarChar, 100),
              new OleDbParameter("@mobile", OleDbType.VarChar, 20),
              new OleDbParameter("@email", OleDbType.VarChar, 50),
              new OleDbParameter("@avatar", OleDbType.VarChar, (int) byte.MaxValue),
              new OleDbParameter("@nick_name", OleDbType.VarChar, 100),
              new OleDbParameter("@sex", OleDbType.VarChar, 20),
              new OleDbParameter("@birthday", OleDbType.Date),
              new OleDbParameter("@telphone", OleDbType.VarChar, 50),
              new OleDbParameter("@area", OleDbType.VarChar, (int) byte.MaxValue),
              new OleDbParameter("@address", OleDbType.VarChar, (int) byte.MaxValue),
              new OleDbParameter("@qq", OleDbType.VarChar, 20),
              new OleDbParameter("@msn", OleDbType.VarChar, 100),
              new OleDbParameter("@amount", OleDbType.Decimal, 5),
              new OleDbParameter("@point", OleDbType.Integer, 4),
              new OleDbParameter("@exp", OleDbType.Integer, 4),
              new OleDbParameter("@status", OleDbType.Integer, 4),
              new OleDbParameter("@reg_time", OleDbType.Date),
              new OleDbParameter("@reg_ip", OleDbType.VarChar, 20)
            };
            oleDbParameterArray[0].Value = (object) model.group_id;
            oleDbParameterArray[1].Value = (object) model.user_name;
            oleDbParameterArray[2].Value = (object) model.salt;
            oleDbParameterArray[3].Value = (object) model.password;
            oleDbParameterArray[4].Value = (object) model.mobile;
            oleDbParameterArray[5].Value = (object) model.email;
            oleDbParameterArray[6].Value = (object) model.avatar;
            oleDbParameterArray[7].Value = (object) model.nick_name;
            oleDbParameterArray[8].Value = (object) model.sex;
            if (model.birthday.HasValue)
              oleDbParameterArray[9].Value = (object) model.birthday;
            else
              oleDbParameterArray[9].Value = (object) DBNull.Value;
            oleDbParameterArray[10].Value = (object) model.telphone;
            oleDbParameterArray[11].Value = (object) model.area;
            oleDbParameterArray[12].Value = (object) model.address;
            oleDbParameterArray[13].Value = (object) model.qq;
            oleDbParameterArray[14].Value = (object) model.msn;
            oleDbParameterArray[15].Value = (object) model.amount;
            oleDbParameterArray[16].Value = (object) model.point;
            oleDbParameterArray[17].Value = (object) model.exp;
            oleDbParameterArray[18].Value = (object) model.status;
            oleDbParameterArray[19].Value = (object) model.reg_time;
            oleDbParameterArray[20].Value = (object) model.reg_ip;
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

    public bool Update(Rain.Model.users model)
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("update " + this.databaseprefix + "users set ");
      stringBuilder.Append("group_id=@group_id,");
      stringBuilder.Append("user_name=@user_name,");
      stringBuilder.Append("salt=@salt,");
      stringBuilder.Append("[password]=@password,");
      stringBuilder.Append("mobile=@mobile,");
      stringBuilder.Append("email=@email,");
      stringBuilder.Append("avatar=@avatar,");
      stringBuilder.Append("nick_name=@nick_name,");
      stringBuilder.Append("sex=@sex,");
      stringBuilder.Append("birthday=@birthday,");
      stringBuilder.Append("telphone=@telphone,");
      stringBuilder.Append("area=@area,");
      stringBuilder.Append("address=@address,");
      stringBuilder.Append("qq=@qq,");
      stringBuilder.Append("msn=@msn,");
      stringBuilder.Append("amount=@amount,");
      stringBuilder.Append("point=@point,");
      stringBuilder.Append("exp=@exp,");
      stringBuilder.Append("status=@status,");
      stringBuilder.Append("reg_time=@reg_time,");
      stringBuilder.Append("reg_ip=@reg_ip");
      stringBuilder.Append(" where id=@id");
      OleDbParameter[] oleDbParameterArray = new OleDbParameter[22]
      {
        new OleDbParameter("@group_id", OleDbType.Integer, 4),
        new OleDbParameter("@user_name", OleDbType.VarChar, 100),
        new OleDbParameter("@salt", OleDbType.VarChar, 20),
        new OleDbParameter("@password", OleDbType.VarChar, 100),
        new OleDbParameter("@mobile", OleDbType.VarChar, 20),
        new OleDbParameter("@email", OleDbType.VarChar, 50),
        new OleDbParameter("@avatar", OleDbType.VarChar, (int) byte.MaxValue),
        new OleDbParameter("@nick_name", OleDbType.VarChar, 100),
        new OleDbParameter("@sex", OleDbType.VarChar, 20),
        new OleDbParameter("@birthday", OleDbType.Date),
        new OleDbParameter("@telphone", OleDbType.VarChar, 50),
        new OleDbParameter("@area", OleDbType.VarChar, (int) byte.MaxValue),
        new OleDbParameter("@address", OleDbType.VarChar, (int) byte.MaxValue),
        new OleDbParameter("@qq", OleDbType.VarChar, 20),
        new OleDbParameter("@msn", OleDbType.VarChar, 100),
        new OleDbParameter("@amount", OleDbType.Decimal, 5),
        new OleDbParameter("@point", OleDbType.Integer, 4),
        new OleDbParameter("@exp", OleDbType.Integer, 4),
        new OleDbParameter("@status", OleDbType.Integer, 4),
        new OleDbParameter("@reg_time", OleDbType.Date),
        new OleDbParameter("@reg_ip", OleDbType.VarChar, 20),
        new OleDbParameter("@id", OleDbType.Integer, 4)
      };
      oleDbParameterArray[0].Value = (object) model.group_id;
      oleDbParameterArray[1].Value = (object) model.user_name;
      oleDbParameterArray[2].Value = (object) model.salt;
      oleDbParameterArray[3].Value = (object) model.password;
      oleDbParameterArray[4].Value = (object) model.mobile;
      oleDbParameterArray[5].Value = (object) model.email;
      oleDbParameterArray[6].Value = (object) model.avatar;
      oleDbParameterArray[7].Value = (object) model.nick_name;
      oleDbParameterArray[8].Value = (object) model.sex;
      if (model.birthday.HasValue)
        oleDbParameterArray[9].Value = (object) model.birthday;
      else
        oleDbParameterArray[9].Value = (object) DBNull.Value;
      oleDbParameterArray[10].Value = (object) model.telphone;
      oleDbParameterArray[11].Value = (object) model.area;
      oleDbParameterArray[12].Value = (object) model.address;
      oleDbParameterArray[13].Value = (object) model.qq;
      oleDbParameterArray[14].Value = (object) model.msn;
      oleDbParameterArray[15].Value = (object) model.amount;
      oleDbParameterArray[16].Value = (object) model.point;
      oleDbParameterArray[17].Value = (object) model.exp;
      oleDbParameterArray[18].Value = (object) model.status;
      oleDbParameterArray[19].Value = (object) model.reg_time;
      oleDbParameterArray[20].Value = (object) model.reg_ip;
      oleDbParameterArray[21].Value = (object) model.id;
      return DbHelperOleDb.ExecuteSql(stringBuilder.ToString(), oleDbParameterArray) > 0;
    }

    public bool Delete(int id)
    {
      Rain.Model.users model = this.GetModel(id);
      if (model == null)
        return false;
      Hashtable SQLStringList = new Hashtable();
      StringBuilder stringBuilder1 = new StringBuilder();
      stringBuilder1.Append("delete from " + this.databaseprefix + "user_point_log ");
      stringBuilder1.Append(" where user_id=@id");
      OleDbParameter[] oleDbParameterArray1 = new OleDbParameter[1]
      {
        new OleDbParameter("@id", OleDbType.Integer, 4)
      };
      oleDbParameterArray1[0].Value = (object) id;
      SQLStringList.Add((object) stringBuilder1.ToString(), (object) oleDbParameterArray1);
      StringBuilder stringBuilder2 = new StringBuilder();
      stringBuilder2.Append("delete from " + this.databaseprefix + "user_amount_log ");
      stringBuilder2.Append(" where user_id=@id");
      OleDbParameter[] oleDbParameterArray2 = new OleDbParameter[1]
      {
        new OleDbParameter("@id", OleDbType.Integer, 4)
      };
      oleDbParameterArray2[0].Value = (object) id;
      SQLStringList.Add((object) stringBuilder2.ToString(), (object) oleDbParameterArray2);
      StringBuilder stringBuilder3 = new StringBuilder();
      stringBuilder3.Append("delete from " + this.databaseprefix + "user_attach_log");
      stringBuilder3.Append(" where user_id=@id");
      OleDbParameter[] oleDbParameterArray3 = new OleDbParameter[1]
      {
        new OleDbParameter("@id", OleDbType.Integer, 4)
      };
      oleDbParameterArray3[0].Value = (object) id;
      SQLStringList.Add((object) stringBuilder3.ToString(), (object) oleDbParameterArray3);
      StringBuilder stringBuilder4 = new StringBuilder();
      stringBuilder4.Append("delete from " + this.databaseprefix + "user_message ");
      stringBuilder4.Append(" where post_user_name=@post_user_name or accept_user_name=@accept_user_name");
      OleDbParameter[] oleDbParameterArray4 = new OleDbParameter[2]
      {
        new OleDbParameter("@post_user_name", OleDbType.VarChar, 100),
        new OleDbParameter("@accept_user_name", OleDbType.VarChar, 100)
      };
      oleDbParameterArray4[0].Value = (object) model.user_name;
      oleDbParameterArray4[1].Value = (object) model.user_name;
      SQLStringList.Add((object) stringBuilder4.ToString(), (object) oleDbParameterArray4);
      StringBuilder stringBuilder5 = new StringBuilder();
      stringBuilder5.Append("delete from " + this.databaseprefix + "user_code ");
      stringBuilder5.Append(" where user_id=@id");
      OleDbParameter[] oleDbParameterArray5 = new OleDbParameter[1]
      {
        new OleDbParameter("@id", OleDbType.Integer, 4)
      };
      oleDbParameterArray5[0].Value = (object) id;
      SQLStringList.Add((object) stringBuilder5.ToString(), (object) oleDbParameterArray5);
      StringBuilder stringBuilder6 = new StringBuilder();
      stringBuilder6.Append("delete from " + this.databaseprefix + "user_login_log ");
      stringBuilder6.Append(" where user_id=@id");
      OleDbParameter[] oleDbParameterArray6 = new OleDbParameter[1]
      {
        new OleDbParameter("@id", OleDbType.Integer, 4)
      };
      oleDbParameterArray6[0].Value = (object) id;
      SQLStringList.Add((object) stringBuilder6.ToString(), (object) oleDbParameterArray6);
      StringBuilder stringBuilder7 = new StringBuilder();
      stringBuilder7.Append("delete from " + this.databaseprefix + "user_oauth ");
      stringBuilder7.Append(" where user_id=@id");
      OleDbParameter[] oleDbParameterArray7 = new OleDbParameter[1]
      {
        new OleDbParameter("@id", OleDbType.Integer, 4)
      };
      oleDbParameterArray7[0].Value = (object) id;
      SQLStringList.Add((object) stringBuilder7.ToString(), (object) oleDbParameterArray7);
      StringBuilder stringBuilder8 = new StringBuilder();
      stringBuilder8.Append("delete from " + this.databaseprefix + "user_recharge ");
      stringBuilder8.Append(" where user_id=@id");
      OleDbParameter[] oleDbParameterArray8 = new OleDbParameter[1]
      {
        new OleDbParameter("@id", OleDbType.Integer, 4)
      };
      oleDbParameterArray8[0].Value = (object) id;
      SQLStringList.Add((object) stringBuilder8.ToString(), (object) oleDbParameterArray8);
      StringBuilder stringBuilder9 = new StringBuilder();
      stringBuilder9.Append("delete from " + this.databaseprefix + "users ");
      stringBuilder9.Append(" where id=@id");
      OleDbParameter[] oleDbParameterArray9 = new OleDbParameter[1]
      {
        new OleDbParameter("@id", OleDbType.Integer, 4)
      };
      oleDbParameterArray9[0].Value = (object) id;
      SQLStringList.Add((object) stringBuilder9.ToString(), (object) oleDbParameterArray9);
      return DbHelperOleDb.ExecuteSqlTran(SQLStringList);
    }

    public Rain.Model.users GetModel(int id)
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("select top 1 id,group_id,user_name,salt,[password],mobile,email,avatar,nick_name,sex,birthday,telphone,area,address,qq,msn,amount,point,exp,status,reg_time,reg_ip");
      stringBuilder.Append(" from " + this.databaseprefix + nameof (users));
      stringBuilder.Append(" where id=@id");
      OleDbParameter[] oleDbParameterArray = new OleDbParameter[1]
      {
        new OleDbParameter("@id", OleDbType.Integer, 4)
      };
      oleDbParameterArray[0].Value = (object) id;
      DataSet dataSet = DbHelperOleDb.Query(stringBuilder.ToString(), oleDbParameterArray);
      if (dataSet.Tables[0].Rows.Count > 0)
        return this.DataRowToModel(dataSet.Tables[0].Rows[0]);
      return (Rain.Model.users) null;
    }

    public Rain.Model.users GetModel(
      string user_name,
      string password,
      int emaillogin,
      int mobilelogin)
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("select top 1 id,group_id,user_name,salt,[password],mobile,email,avatar,nick_name,sex,birthday,telphone,area,address,qq,msn,amount,point,exp,status,reg_time,reg_ip");
      stringBuilder.Append(" from " + this.databaseprefix + nameof (users));
      stringBuilder.Append(" where (user_name=@user_name");
      if (emaillogin == 1)
        stringBuilder.Append(" or email=@user_name");
      if (mobilelogin == 1)
        stringBuilder.Append(" or mobile=@user_name");
      stringBuilder.Append(") and [password]=@password and status<3");
      OleDbParameter[] oleDbParameterArray = new OleDbParameter[2]
      {
        new OleDbParameter("@user_name", OleDbType.VarChar, 100),
        new OleDbParameter("@password", OleDbType.VarChar, 100)
      };
      oleDbParameterArray[0].Value = (object) user_name;
      oleDbParameterArray[1].Value = (object) password;
      DataSet dataSet = DbHelperOleDb.Query(stringBuilder.ToString(), oleDbParameterArray);
      if (dataSet.Tables[0].Rows.Count > 0)
        return this.DataRowToModel(dataSet.Tables[0].Rows[0]);
      return (Rain.Model.users) null;
    }

    public Rain.Model.users GetModel(string user_name)
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("select top 1 id,group_id,user_name,salt,[password],mobile,email,avatar,nick_name,sex,birthday,telphone,area,address,qq,msn,amount,point,exp,status,reg_time,reg_ip");
      stringBuilder.Append(" from " + this.databaseprefix + nameof (users));
      stringBuilder.Append(" where user_name=@user_name and status<3");
      OleDbParameter[] oleDbParameterArray = new OleDbParameter[1]
      {
        new OleDbParameter("@user_name", OleDbType.VarChar, 100)
      };
      oleDbParameterArray[0].Value = (object) user_name;
      DataSet dataSet = DbHelperOleDb.Query(stringBuilder.ToString(), oleDbParameterArray);
      if (dataSet.Tables[0].Rows.Count > 0)
        return this.DataRowToModel(dataSet.Tables[0].Rows[0]);
      return (Rain.Model.users) null;
    }

    public DataSet GetList(int Top, string strWhere, string filedOrder)
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("select ");
      if (Top > 0)
        stringBuilder.Append(" top " + Top.ToString());
      stringBuilder.Append(" id,group_id,user_name,salt,[password],mobile,email,avatar,nick_name,sex,birthday,telphone,area,address,qq,msn,amount,point,exp,status,reg_time,reg_ip");
      stringBuilder.Append(" FROM " + this.databaseprefix + "users ");
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
      stringBuilder.Append("select * FROM " + this.databaseprefix + nameof (users));
      if (strWhere.Trim() != "")
        stringBuilder.Append(" where " + strWhere);
      recordCount = Convert.ToInt32(DbHelperOleDb.GetSingle(PagingHelper.CreateCountingSql(stringBuilder.ToString())));
      return DbHelperOleDb.Query(PagingHelper.CreatePagingSql(recordCount, pageSize, pageIndex, stringBuilder.ToString(), filedOrder));
    }

    public bool ExistsEmail(string email)
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("select count(1) from " + this.databaseprefix + nameof (users));
      stringBuilder.Append(" where email=@email ");
      OleDbParameter[] oleDbParameterArray = new OleDbParameter[1]
      {
        new OleDbParameter("@email", OleDbType.VarChar, 100)
      };
      oleDbParameterArray[0].Value = (object) email;
      return DbHelperOleDb.Exists(stringBuilder.ToString(), oleDbParameterArray);
    }

    public bool ExistsMobile(string mobile)
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("select count(1) from " + this.databaseprefix + nameof (users));
      stringBuilder.Append(" where mobile=@mobile ");
      OleDbParameter[] oleDbParameterArray = new OleDbParameter[1]
      {
        new OleDbParameter("@mobile", OleDbType.VarChar, 20)
      };
      oleDbParameterArray[0].Value = (object) mobile;
      return DbHelperOleDb.Exists(stringBuilder.ToString(), oleDbParameterArray);
    }

    public string GetSalt(string user_name)
    {
      StringBuilder stringBuilder1 = new StringBuilder();
      stringBuilder1.Append("select top 1 salt from " + this.databaseprefix + nameof (users));
      stringBuilder1.Append(" where user_name=@user_name");
      OleDbParameter[] oleDbParameterArray1 = new OleDbParameter[1]
      {
        new OleDbParameter("@user_name", OleDbType.VarChar, 100)
      };
      oleDbParameterArray1[0].Value = (object) user_name;
      string str1 = Convert.ToString(DbHelperOleDb.GetSingle(stringBuilder1.ToString(), oleDbParameterArray1));
      if (!string.IsNullOrEmpty(str1))
        return str1;
      StringBuilder stringBuilder2 = new StringBuilder();
      stringBuilder2.Append("select top 1 salt from " + this.databaseprefix + nameof (users));
      stringBuilder2.Append(" where mobile=@mobile");
      OleDbParameter[] oleDbParameterArray2 = new OleDbParameter[1]
      {
        new OleDbParameter("@mobile", OleDbType.VarChar, 20)
      };
      oleDbParameterArray2[0].Value = (object) user_name;
      string str2 = Convert.ToString(DbHelperOleDb.GetSingle(stringBuilder2.ToString(), oleDbParameterArray2));
      if (!string.IsNullOrEmpty(str2))
        return str2;
      StringBuilder stringBuilder3 = new StringBuilder();
      stringBuilder3.Append("select top 1 salt from " + this.databaseprefix + nameof (users));
      stringBuilder3.Append(" where email=@email");
      OleDbParameter[] oleDbParameterArray3 = new OleDbParameter[1]
      {
        new OleDbParameter("@email", OleDbType.VarChar, 50)
      };
      oleDbParameterArray3[0].Value = (object) user_name;
      string str3 = Convert.ToString(DbHelperOleDb.GetSingle(stringBuilder3.ToString(), oleDbParameterArray3));
      if (!string.IsNullOrEmpty(str3))
        return str3;
      return string.Empty;
    }

    public int UpdateField(int id, string strValue)
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("update " + this.databaseprefix + "users set " + strValue);
      stringBuilder.Append(" where id=" + (object) id);
      return DbHelperOleDb.ExecuteSql(stringBuilder.ToString());
    }

    public Rain.Model.users DataRowToModel(DataRow row)
    {
      Rain.Model.users users = new Rain.Model.users();
      if (row != null)
      {
        if (row["id"] != null && row["id"].ToString() != "")
          users.id = int.Parse(row["id"].ToString());
        if (row["group_id"] != null && row["group_id"].ToString() != "")
          users.group_id = int.Parse(row["group_id"].ToString());
        if (row["user_name"] != null)
          users.user_name = row["user_name"].ToString();
        if (row["salt"] != null)
          users.salt = row["salt"].ToString();
        if (row["password"] != null)
          users.password = row["password"].ToString();
        if (row["mobile"] != null)
          users.mobile = row["mobile"].ToString();
        if (row["email"] != null)
          users.email = row["email"].ToString();
        if (row["avatar"] != null)
          users.avatar = row["avatar"].ToString();
        if (row["nick_name"] != null)
          users.nick_name = row["nick_name"].ToString();
        if (row["sex"] != null)
          users.sex = row["sex"].ToString();
        if (row["birthday"] != null && row["birthday"].ToString() != "")
          users.birthday = new DateTime?(DateTime.Parse(row["birthday"].ToString()));
        if (row["telphone"] != null)
          users.telphone = row["telphone"].ToString();
        if (row["area"] != null)
          users.area = row["area"].ToString();
        if (row["address"] != null)
          users.address = row["address"].ToString();
        if (row["qq"] != null)
          users.qq = row["qq"].ToString();
        if (row["msn"] != null)
          users.msn = row["msn"].ToString();
        if (row["amount"] != null && row["amount"].ToString() != "")
          users.amount = Decimal.Parse(row["amount"].ToString());
        if (row["point"] != null && row["point"].ToString() != "")
          users.point = int.Parse(row["point"].ToString());
        if (row["exp"] != null && row["exp"].ToString() != "")
          users.exp = int.Parse(row["exp"].ToString());
        if (row["status"] != null && row["status"].ToString() != "")
          users.status = int.Parse(row["status"].ToString());
        if (row["reg_time"] != null && row["reg_time"].ToString() != "")
          users.reg_time = DateTime.Parse(row["reg_time"].ToString());
        if (row["reg_ip"] != null)
          users.reg_ip = row["reg_ip"].ToString();
      }
      return users;
    }
  }
}
