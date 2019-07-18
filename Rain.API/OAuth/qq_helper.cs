// Decompiled with JetBrains decompiler
// Type: Rain.API.OAuth.qq_helper
// Assembly: Rain.API, Version=4.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 114351D3-1A2F-4CC4-A6A6-43C59DB5A56B
// Assembly location: C:\Users\Felix.Ho\Desktop\page\bin\Rain.API.dll

using System.Collections.Generic;
using Rain.Common;

namespace Rain.API.OAuth
{
  public class qq_helper
  {
    public static Dictionary<string, object> get_access_token(string code, string state)
    {
      oauth_config config = oauth_helper.get_config("qq");
      string str = Utils.HttpGet("https://graph.qq.com/oauth2.0/token?grant_type=authorization_code&client_id=" + config.oauth_app_id + "&client_secret=" + config.oauth_app_key + "&code=" + code + "&state=" + state + "&redirect_uri=" + Utils.UrlEncode(config.return_uri));
      if (str.Contains("error"))
        return (Dictionary<string, object>) null;
      try
      {
        string[] strArray = str.Split('&');
        return new Dictionary<string, object>()
        {
          {
            "access_token",
            (object) strArray[0].Split('=')[1]
          },
          {
            "expires_in",
            (object) strArray[1].Split('=')[1]
          }
        };
      }
      catch
      {
        return (Dictionary<string, object>) null;
      }
    }

    public static Dictionary<string, object> get_open_id(string access_token)
    {
      string str = Utils.HttpGet("https://graph.qq.com/oauth2.0/me?access_token=" + access_token);
      if (str.Contains("error"))
        return (Dictionary<string, object>) null;
      int startIndex = str.IndexOf('(') + 1;
      int num = str.LastIndexOf(')') - 1;
      return JsonHelper.DataRowFromJSON(str.Substring(startIndex, num - startIndex));
    }

    public static Dictionary<string, object> get_user_info(
      string access_token,
      string open_id)
    {
      oauth_config config = oauth_helper.get_config("qq");
      string jsonText = Utils.HttpGet("https://graph.qq.com/user/get_user_info?access_token=" + access_token + "&oauth_consumer_key=" + config.oauth_app_id + "&openid=" + open_id);
      if (jsonText.Contains("error"))
        return (Dictionary<string, object>) null;
      return JsonHelper.DataRowFromJSON(jsonText);
    }

    public static Dictionary<string, object> get_info(string access_token, string open_id)
    {
      oauth_config config = oauth_helper.get_config("qq");
      string jsonText = Utils.HttpGet("https://graph.qq.com/user/get_info?access_token=" + access_token + "&oauth_consumer_key=" + config.oauth_app_id + "&openid=" + open_id);
      if (jsonText.Contains("error"))
        return (Dictionary<string, object>) null;
      try
      {
        Dictionary<string, object> dictionary = JsonHelper.DataRowFromJSON(jsonText);
        if (dictionary.Count > 0)
          return dictionary;
      }
      catch
      {
        return (Dictionary<string, object>) null;
      }
      return (Dictionary<string, object>) null;
    }
  }
}
