// Decompiled with JetBrains decompiler
// Type: Rain.API.OAuth.kaixin_helper
// Assembly: Rain.API, Version=4.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 114351D3-1A2F-4CC4-A6A6-43C59DB5A56B
// Assembly location: C:\Users\Felix.Ho\Desktop\page\bin\Rain.API.dll

using System.Collections.Generic;
using Rain.Common;

namespace Rain.API.OAuth
{
  public class kaixin_helper
  {
    public static Dictionary<string, object> get_access_token(string code, string state)
    {
      oauth_config config = oauth_helper.get_config("kaixin");
      string jsonText = Utils.HttpGet("https://api.kaixin001.com/oauth2/access_token?grant_type=authorization_code&code=" + code + "&client_id=" + config.oauth_app_id + "&client_secret=" + config.oauth_app_key + "&state=" + state + "&redirect_uri=" + Utils.UrlEncode(config.return_uri));
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

    public static Dictionary<string, object> get_info(string access_token, string fields)
    {
      string jsonText = Utils.HttpGet("https://api.kaixin001.com/users/me.json?fields=" + fields + "&access_token=" + access_token);
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
