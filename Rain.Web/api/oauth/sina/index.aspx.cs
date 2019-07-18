﻿// Decompiled with JetBrains decompiler
// Type: Rain.Web.api.oauth.sina.index
// Assembly: Rain.Web, Version=4.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 7C74D3EC-D223-4A13-9304-5BE7CB5803E5
// Assembly location: C:\Users\Felix.Ho\Desktop\page\bin\Rain.Web.dll

using System;
using System.Web.UI;
using Rain.API.OAuth;
using Rain.Common;

namespace Rain.Web.api.oauth.sina
{
  public class index : Page
  {
    protected void Page_Load(object sender, EventArgs e)
    {
      oauth_config config = oauth_helper.get_config("sina");
      if (config == null)
      {
        this.Response.Write("出错了，您尚未配置新浪微博的API信息！");
      }
      else
      {
        string str = Guid.NewGuid().ToString().Replace("-", "");
        this.Session["oauth_state"] = (object) str;
        this.Response.Redirect("https://api.weibo.com/oauth2/authorize?response_type=code&client_id=" + config.oauth_app_id + "&state=" + str + "&redirect_uri=" + Utils.UrlEncode(config.return_uri));
      }
    }
  }
}
