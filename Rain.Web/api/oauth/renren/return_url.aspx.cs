// Decompiled with JetBrains decompiler
// Type: Rain.Web.api.oauth.renren.return_url
// Assembly: Rain.Web, Version=4.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 7C74D3EC-D223-4A13-9304-5BE7CB5803E5
// Assembly location: C:\Users\Felix.Ho\Desktop\page\bin\Rain.Web.dll

using System;
using System.Collections.Generic;
using System.Web.UI;
using Rain.API.OAuth;
using Rain.Common;
using Rain.Web.UI;

namespace Rain.Web.api.oauth.renren
{
  public class return_url : Page
  {
    protected void Page_Load(object sender, EventArgs e)
    {
      string queryString1 = DTRequest.GetQueryString("state");
      string queryString2 = DTRequest.GetQueryString("code");
      string empty1 = string.Empty;
      string empty2 = string.Empty;
      string empty3 = string.Empty;
      if (this.Session["oauth_state"] == null || this.Session["oauth_state"].ToString() == "" || queryString1 != this.Session["oauth_state"].ToString())
        this.Response.Write("出错啦，state未初始化！");
      else if (string.IsNullOrEmpty(queryString2))
      {
        this.Response.Write("授权被取消，相关信息：" + DTRequest.GetQueryString("error"));
      }
      else
      {
        Dictionary<string, object> accessToken = renren_helper.get_access_token(queryString2);
        if (accessToken == null)
          this.Response.Write("错误代码：，无法获取Access Token，请检查App Key是否正确！");
        string str1 = accessToken["access_token"].ToString();
        empty2 = accessToken["expires_in"].ToString();
        string str2 = (accessToken["user"] as Dictionary<string, object>)["id"].ToString();
        this.Session["oauth_name"] = (object) "renren";
        this.Session["oauth_access_token"] = (object) str1;
        this.Session["oauth_openid"] = (object) str2;
        this.Response.Redirect(new BasePage().linkurl("oauth_login"));
      }
    }
  }
}
