// Decompiled with JetBrains decompiler
// Type: Rain.Web.api.oauth.kaixin.result_json
// Assembly: Rain.Web, Version=4.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 7C74D3EC-D223-4A13-9304-5BE7CB5803E5
// Assembly location: C:\Users\Felix.Ho\Desktop\page\bin\Rain.Web.dll

using System;
using System.Collections.Generic;
using System.Text;
using System.Web.UI;
using Rain.API.OAuth;

namespace Rain.Web.api.oauth.kaixin
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
        empty2 = this.Session["oauth_openid"].ToString();
        Dictionary<string, object> info = kaixin_helper.get_info(access_token, "uid,name,logo120,gender,birthday");
        if (info == null)
        {
          this.Response.Write("{\"ret\":\"1\", \"msg\":\"出错啦，无法获取授权用户信息！\"}");
        }
        else
        {
          StringBuilder stringBuilder = new StringBuilder();
          stringBuilder.Append("{");
          stringBuilder.Append("\"ret\": \"0\", ");
          stringBuilder.Append("\"msg\": \"获得用户信息成功！\", ");
          stringBuilder.Append("\"oauth_name\": \"" + str + "\", ");
          stringBuilder.Append("\"oauth_access_token\": \"" + access_token + "\", ");
          stringBuilder.Append("\"oauth_openid\": \"" + info["uid"].ToString() + "\", ");
          stringBuilder.Append("\"nick\": \"" + info["name"].ToString() + "\", ");
          stringBuilder.Append("\"avatar\": \"" + info["logo120"].ToString() + "\", ");
          if (info["gender"].ToString() == "0")
            stringBuilder.Append("\"sex\": \"男\", ");
          else if (info["gender"].ToString() == "1")
            stringBuilder.Append("\"sex\": \"女\", ");
          else
            stringBuilder.Append("\"sex\": \"保密\", ");
          stringBuilder.Append("\"birthday\": \"" + info["birthday"].ToString() + "\"");
          stringBuilder.Append("}");
          this.Response.Write(stringBuilder.ToString());
        }
      }
    }
  }
}
