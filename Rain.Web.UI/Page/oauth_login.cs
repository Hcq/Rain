// Decompiled with JetBrains decompiler
// Type: Rain.Web.UI.Page.oauth_login
// Assembly: Rain.Web.UI, Version=4.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 3E78344A-857F-445F-804E-AF1B978FD5C7
// Assembly location: C:\Users\Felix.Ho\Desktop\page\bin\Rain.Web.UI.dll

using System;
using System.Web;
using Rain.Common;

namespace Rain.Web.UI.Page
{
  public class oauth_login : BasePage
  {
    protected string turl = string.Empty;

    protected override void ShowPage()
    {
      this.Init += new EventHandler(this.UserPage_Init);
    }

    private void UserPage_Init(object sender, EventArgs e)
    {
      this.turl = Utils.GetCookie("dt_cookie_url_referrer");
      if (string.IsNullOrEmpty(this.turl) || this.turl == HttpContext.Current.Request.Url.ToString().ToLower())
        this.turl = this.linkurl("usercenter", (object) "index");
      if (this.IsUserLogin())
        HttpContext.Current.Response.Redirect(this.turl);
      else if (HttpContext.Current.Session["oauth_name"] == null || HttpContext.Current.Session["oauth_access_token"] == null || HttpContext.Current.Session["oauth_openid"] == null)
      {
        HttpContext.Current.Response.Redirect(this.linkurl("error", (object) ("?msg=" + Utils.UrlEncode("登录失败，用户授权已过期，请重新登录！"))));
      }
      else
      {
        Rain.Model.user_oauth model1 = new Rain.BLL.user_oauth().GetModel(HttpContext.Current.Session["oauth_name"].ToString(), HttpContext.Current.Session["oauth_openid"].ToString());
        if (model1 == null)
          return;
        Rain.Model.users model2 = new Rain.BLL.users().GetModel(model1.user_name);
        if (model2 == null)
        {
          HttpContext.Current.Response.Redirect(this.linkurl("error", (object) ("?msg=" + Utils.UrlEncode("登录失败，授权用户不存在或已被删除！"))));
        }
        else
        {
          HttpContext.Current.Session["dt_session_user_info"] = (object) model2;
          HttpContext.Current.Session.Timeout = 45;
          Utils.WriteCookie("dt_cookie_user_name_remember", "Rain", model2.user_name);
          Utils.WriteCookie("dt_cookie_user_pwd_remember", "Rain", model2.password);
          model1.oauth_access_token = HttpContext.Current.Session["oauth_access_token"].ToString();
          new Rain.BLL.user_oauth().Update(model1);
          HttpContext.Current.Response.Redirect(this.turl);
        }
      }
    }
  }
}
