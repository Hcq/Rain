// Decompiled with JetBrains decompiler
// Type: Rain.Web.api.oauth.kaixin.return_url
// Assembly: Rain.Web, Version=4.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 7C74D3EC-D223-4A13-9304-5BE7CB5803E5
// Assembly location: C:\Users\Felix.Ho\Desktop\page\bin\Rain.Web.dll

using System;
using System.Collections.Generic;
using System.Web.UI;
using Rain.API.OAuth;
using Rain.Common;
using Rain.Web.UI;

namespace Rain.Web.api.oauth.kaixin
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
      string empty4 = string.Empty;
      if (this.Session["oauth_state"] == null || this.Session["oauth_state"].ToString() == "" || queryString1 != this.Session["oauth_state"].ToString())
      {
        this.Response.Write("出错啦，state未初始化！");
      }
      else
      {
        Dictionary<string, object> accessToken = kaixin_helper.get_access_token(queryString2, queryString1);
        if (accessToken == null)
        {
          this.Response.Write("出错了，无法获取Access Token，请检查App Key是否正确！");
        }
        else
        {
          string access_token = accessToken["access_token"].ToString();
          empty2 = accessToken["expires_in"].ToString();
          Dictionary<string, object> info = kaixin_helper.get_info(access_token, "uid");
          if (info == null)
          {
            this.Response.Write("出错啦，无法获取用户授权uid！");
          }
          else
          {
            string str = info["uid"].ToString();
            this.Session["oauth_name"] = (object) "kaixin";
            this.Session["oauth_access_token"] = (object) access_token;
            this.Session["oauth_openid"] = (object) str;
            this.Response.Redirect(new BasePage().linkurl("oauth_login"));
          }
        }
      }
    }
  }
}
