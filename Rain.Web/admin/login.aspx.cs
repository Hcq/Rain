// Decompiled with JetBrains decompiler
// Type: Rain.Web.admin.login
// Assembly: Rain.Web, Version=4.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 7C74D3EC-D223-4A13-9304-5BE7CB5803E5
// Assembly location: C:\Users\Felix.Ho\Desktop\page\bin\Rain.Web.dll

using System;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Rain.Common;

namespace Rain.Web.admin
{
  public class login : Page
  {
    protected HtmlForm form1;
    protected TextBox txtUserName;
    protected TextBox txtPassword;
    protected Button btnSubmit;
    protected HtmlGenericControl msgtip;

    protected void Page_Load(object sender, EventArgs e)
    {
      if (this.Page.IsPostBack)
        return;
      this.txtUserName.Text = Utils.GetCookie("DTRememberName");
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
      string user_name = this.txtUserName.Text.Trim();
      string password = this.txtPassword.Text.Trim();
      if (user_name.Equals("") || password.Equals(""))
      {
        this.msgtip.InnerHtml = "请输入用户名或密码";
      }
      else
      {
        if (this.Session["AdminLoginSun"] == null)
          this.Session["AdminLoginSun"] = (object) 1;
        else
          this.Session["AdminLoginSun"] = (object) (Convert.ToInt32(this.Session["AdminLoginSun"]) + 1);
        if (this.Session["AdminLoginSun"] != null && Convert.ToInt32(this.Session["AdminLoginSun"]) > 5)
        {
          this.msgtip.InnerHtml = "错误超过5次，关闭浏览器重新登录！";
        }
        else
        {
          Rain.Model.manager model = new Rain.BLL.manager().GetModel(user_name, password, true);
          if (model == null)
          {
            this.msgtip.InnerHtml = "用户名或密码有误，请重试！";
          }
          else
          {
            this.Session["dt_session_admin_info"] = (object) model;
            this.Session.Timeout = 45;
            if (new Rain.BLL.siteconfig().loadConfig().logstatus > 0)
              new Rain.BLL.manager_log().Add(model.id, model.user_name, DTEnums.ActionEnum.Login.ToString(), "用户登录");
            Utils.WriteCookie("DTRememberName", model.user_name, 14400);
            Utils.WriteCookie("AdminName", "Rain", model.user_name);
            Utils.WriteCookie("AdminPwd", "Rain", model.password);
            this.Response.Redirect("index.aspx");
          }
        }
      }
    }
  }
}
