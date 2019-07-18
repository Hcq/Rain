// Decompiled with JetBrains decompiler
// Type: Rain.API.OAuth.feixin_helper
// Assembly: Rain.API, Version=4.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 114351D3-1A2F-4CC4-A6A6-43C59DB5A56B
// Assembly location: C:\Users\Felix.Ho\Desktop\page\bin\Rain.API.dll

using System.Collections.Generic;
using Rain.Common;

namespace Rain.API.OAuth
{
  public class feixin_helper
  {
    public static Dictionary<string, object> get_access_token(string code)
    {
      oauth_config config = oauth_helper.get_config("feixin");
      string jsonText = Utils.HttpGet("https://i.feixin.10086.cn/oauth2/access_token?grant_type=authorization_code&code=" + code + "&client_id=" + config.oauth_app_id + "&client_secret=" + config.oauth_app_key + "&redirect_uri=" + Utils.UrlEncode(config.return_uri));
      if (jsonText.Contains("error"))
        return (Dictionary<string, object>) null;
      try
      {
        return JsonHelper.DataRowFromJSON(jsonText);
      }
      catch
      {
        return (Dictionary<string, object>) null;
      }
    }

    public static Dictionary<string, object> get_info(string access_token)
    {
      string jsonText = Utils.HttpGet("https://i.feixin.10086.cn/api/user.json?access_token=" + access_token);
      if (jsonText.Contains("error"))
        return (Dictionary<string, object>) null;
      try
      {
        return JsonHelper.DataRowFromJSON(jsonText);
      }
      catch
      {
        return (Dictionary<string, object>) null;
      }
    }
  }
}
