// Decompiled with JetBrains decompiler
// Type: Rain.API.OAuth.renren_helper
// Assembly: Rain.API, Version=4.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 114351D3-1A2F-4CC4-A6A6-43C59DB5A56B
// Assembly location: C:\Users\Felix.Ho\Desktop\page\bin\Rain.API.dll

using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using Rain.Common;

namespace Rain.API.OAuth
{
  public class renren_helper
  {
    public static Dictionary<string, object> get_access_token(string code)
    {
      oauth_config config = oauth_helper.get_config("renren");
      string jsonText = Rain.Common.Utils.HttpGet("https://graph.renren.com/oauth/token?grant_type=authorization_code&client_id=" + config.oauth_app_id + "&client_secret=" + config.oauth_app_key + "&code=" + code + "&redirect_uri=" + Rain.Common.Utils.UrlEncode(config.return_uri));
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
      oauth_config config = oauth_helper.get_config("renren");
      string url = "http://api.renren.com/restserver.do";
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("method=users.getInfo&");
      stringBuilder.Append("access_token=" + access_token + "&");
      stringBuilder.Append("fields=" + fields + "&");
      stringBuilder.Append("format=json&");
      stringBuilder.Append("v=1.0&");
      stringBuilder.Append("sig=" + renren_helper.MD5Encrpt("access_token=" + access_token + "fields=" + fields + "format=jsonmethod=users.getInfov=1.0" + config.oauth_app_key));
      string jsonText = Rain.Common.Utils.HttpPost(url, stringBuilder.ToString());
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

    public static string MD5Encrpt(string plainText)
    {
      byte[] hash = MD5.Create().ComputeHash(Encoding.UTF8.GetBytes(plainText));
      StringBuilder stringBuilder = new StringBuilder();
      foreach (byte num in hash)
        stringBuilder.Append(num.ToString("x2"));
      return stringBuilder.ToString();
    }
  }
}
