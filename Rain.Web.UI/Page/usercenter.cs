// Decompiled with JetBrains decompiler
// Type: Rain.Web.UI.Page.usercenter
// Assembly: Rain.Web.UI, Version=4.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 3E78344A-857F-445F-804E-AF1B978FD5C7
// Assembly location: C:\Users\Felix.Ho\Desktop\page\bin\Rain.Web.UI.dll

using System.Data;
using System.Web;
using Rain.BLL;
using Rain.Common;

namespace Rain.Web.UI.Page
{
  public class usercenter : UserPage
  {
    protected string action = string.Empty;
    protected string curr_login_ip = string.Empty;
    protected string pre_login_ip = string.Empty;
    protected string pre_login_time = string.Empty;
    protected int total_order;
    protected int total_msg;

    protected override void InitPage()
    {
      this.action = DTRequest.GetQueryString("action");
      DataTable table = new user_login_log().GetList(2, "user_name='" + this.userModel.user_name + "'", "id desc").Tables[0];
      if (table.Rows.Count == 2)
      {
        this.curr_login_ip = table.Rows[0]["login_ip"].ToString();
        this.pre_login_ip = table.Rows[1]["login_ip"].ToString();
        this.pre_login_time = table.Rows[1]["login_time"].ToString();
      }
      else if (table.Rows.Count == 1)
        this.curr_login_ip = table.Rows[0]["login_ip"].ToString();
      this.total_order = new orders().GetCount("user_name='" + this.userModel.user_name + "' and status<3");
      this.total_msg = new user_message().GetCount("accept_user_name='" + this.userModel.user_name + "' and is_read=0");
      if (!(this.action == "exit"))
        return;
      HttpContext.Current.Session["dt_session_user_info"] = (object) null;
      Utils.WriteCookie("dt_cookie_user_name_remember", "Rain", -43200);
      Utils.WriteCookie("dt_cookie_user_pwd_remember", "Rain", -43200);
      Utils.WriteCookie("UserName", "Rain", -1);
      Utils.WriteCookie("Password", "Rain", -1);
      HttpContext.Current.Response.Redirect(this.linkurl("login"));
    }
  }
}
