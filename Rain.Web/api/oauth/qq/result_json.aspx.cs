// Decompiled with JetBrains decompiler
// Type: Rain.Web.api.oauth.qq.result_json
// Assembly: Rain.Web, Version=4.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 7C74D3EC-D223-4A13-9304-5BE7CB5803E5
// Assembly location: C:\Users\Felix.Ho\Desktop\page\bin\Rain.Web.dll

using System;
using System.Collections.Generic;
using System.Text;
using System.Web.UI;
using Rain.API.OAuth;

namespace Rain.Web.api.oauth.qq
{
  public class result_json : Page
  {
    protected void Page_Load(object sender, EventArgs e)
    {
      string empty1 = string.Empty;
      string empty2 = string.Empty;
      string empty3 = string.Empty;
      if (this.Session["oauth_name"] == null || this.Session["oauth_access_token"] == null || this.Session["oauth_openid"] == null)
      {
        this.Response.Write("{\"ret\":\"1\", \"msg\":\"出错啦，Access Token已过期或不存在！\"}");
      }
      else
      {
        string str = this.Session["oauth_name"].ToString();
        string access_token = this.Session["oauth_access_token"].ToString();
        string open_id = this.Session["oauth_openid"].ToString();
        Dictionary<string, object> userInfo = qq_helper.get_user_info(access_token, open_id);
        if (userInfo == null)
        {
          this.Response.Write("{\"ret\":\"1\", \"msg\":\"出错啦，无法获取授权用户信息！\"}");
        }
        else
        {
          try
          {
            if (userInfo["ret"].ToString() != "0")
            {
              this.Response.Write("{\"ret\":\"" + userInfo["ret"].ToString() + "\", \"msg\":\"出错信息:" + userInfo["msg"].ToString() + "！\"}");
            }
            else
            {
              StringBuilder stringBuilder = new StringBuilder();
              stringBuilder.Append("{");
              stringBuilder.Append("\"ret\": \"" + userInfo["ret"].ToString() + "\", ");
              stringBuilder.Append("\"msg\": \"" + userInfo["msg"].ToString() + "\", ");
              stringBuilder.Append("\"oauth_name\": \"" + str + "\", ");
              stringBuilder.Append("\"oauth_access_token\": \"" + access_token + "\", ");
              stringBuilder.Append("\"oauth_openid\": \"" + open_id + "\", ");
              stringBuilder.Append("\"nick\": \"" + userInfo["nickname"].ToString() + "\", ");
              stringBuilder.Append("\"avatar\": \"" + userInfo["figureurl_qq_2"].ToString() + "\", ");
              stringBuilder.Append("\"sex\": \"" + userInfo["gender"].ToString() + "\", ");
              stringBuilder.Append("\"birthday\": \"\"");
              stringBuilder.Append("}");
              this.Response.Write(stringBuilder.ToString());
            }
          }
          catch
          {
            this.Response.Write("{\"ret\":\"1\", \"msg\":\"出错啦，无法获取授权用户信息！\"}");
          }
        }
      }
    }
  }
}
