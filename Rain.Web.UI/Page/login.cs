// Decompiled with JetBrains decompiler
// Type: Rain.Web.UI.Page.login
// Assembly: Rain.Web.UI, Version=4.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 3E78344A-857F-445F-804E-AF1B978FD5C7
// Assembly location: C:\Users\Felix.Ho\Desktop\page\bin\Rain.Web.UI.dll

using System;
using System.Web;
using Rain.Common;

namespace Rain.Web.UI.Page
{
  public class login : BasePage
  {
    protected string turl = string.Empty;

    protected override void ShowPage()
    {
      this.Init += new EventHandler(this.UserPage_Init);
    }

    private void UserPage_Init(object sender, EventArgs e)
    {
      this.turl = this.linkurl("usercenter", (object) "index");
      if (HttpContext.Current.Request.Url != (Uri) null && HttpContext.Current.Request.UrlReferrer != (Uri) null)
      {
        string lower1 = HttpContext.Current.Request.Url.ToString().ToLower();
        string lower2 = HttpContext.Current.Request.UrlReferrer.ToString().ToLower();
        string lower3 = this.linkurl("register").ToLower();
        if (lower1 != lower2 && lower2.IndexOf(lower3) == -1)
          this.turl = HttpContext.Current.Request.UrlReferrer.ToString();
      }
      Utils.WriteCookie("dt_cookie_url_referrer", this.turl);
      if (this.GetUserInfo() == null)
        return;
      HttpContext.Current.Response.Redirect(this.turl);
    }
  }
}
