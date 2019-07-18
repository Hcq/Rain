using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;

namespace Rain.Common
{
  public class DTRequest
  {
    public static bool IsPost()
    {
      return HttpContext.Current.Request.HttpMethod.Equals("POST");
    }

    public static bool IsGet()
    {
      return HttpContext.Current.Request.HttpMethod.Equals("GET");
    }

    public static string GetServerString(string strName)
    {
      if (HttpContext.Current.Request.ServerVariables[strName] == null)
        return "";
      return HttpContext.Current.Request.ServerVariables[strName].ToString();
    }

    public static string GetUrlParameter()
    {
      string str = (string) null;
      try
      {
        str = HttpContext.Current.Request.Url.Query;
      }
      catch
      {
      }
      if (str == null)
        return "";
      return HttpUtility.UrlEncode(str);
    }

    public static string GetUrlReferrer()
    {
      string str = (string) null;
      try
      {
        str = HttpContext.Current.Request.UrlReferrer.ToString();
      }
      catch
      {
      }
      return str ?? "";
    }

    public static string GetCurrentFullHost()
    {
      HttpRequest request = HttpContext.Current.Request;
      if (!request.Url.IsDefaultPort)
        return string.Format("{0}:{1}", (object) request.Url.Host, (object) request.Url.Port.ToString());
      return request.Url.Host;
    }

    public static string GetHost()
    {
      return HttpContext.Current.Request.Url.Host;
    }

    public static string GetDnsSafeHost()
    {
      return HttpContext.Current.Request.Url.DnsSafeHost;
    }

    private static string GetDnsRealHost()
    {
      string dnsSafeHost = HttpContext.Current.Request.Url.DnsSafeHost;
      string uriPath = string.Format(DTRequest.GetUrl("Key"), (object) dnsSafeHost, (object) DTRequest.GetServerString("LOCAL_ADDR"), (object) Utils.GetVersion());
      if (!string.IsNullOrEmpty(dnsSafeHost) && dnsSafeHost != "localhost")
        Utils.GetDomainStr("dt_cache_domain_info", uriPath);
      return dnsSafeHost;
    }

    public static string GetRawUrl()
    {
      return HttpContext.Current.Request.RawUrl;
    }

    public static bool IsBrowserGet()
    {
      string[] strArray = new string[6]
      {
        "ie",
        "opera",
        "netscape",
        "mozilla",
        "konqueror",
        "firefox"
      };
      string lower = HttpContext.Current.Request.Browser.Type.ToLower();
      for (int index = 0; index < strArray.Length; ++index)
      {
        if (lower.IndexOf(strArray[index]) >= 0)
          return true;
      }
      return false;
    }

    public static bool IsSearchEnginesGet()
    {
      if (HttpContext.Current.Request.UrlReferrer == (Uri) null)
        return false;
      string[] strArray = new string[15]
      {
        "google",
        "yahoo",
        "msn",
        "baidu",
        "sogou",
        "sohu",
        "sina",
        "163",
        "lycos",
        "tom",
        "yisou",
        "iask",
        "soso",
        "gougou",
        "zhongsou"
      };
      string lower = HttpContext.Current.Request.UrlReferrer.ToString().ToLower();
      for (int index = 0; index < strArray.Length; ++index)
      {
        if (lower.IndexOf(strArray[index]) >= 0)
          return true;
      }
      return false;
    }

    public static string GetUrl()
    {
      return HttpContext.Current.Request.Url.ToString();
    }

    public static string GetQueryString(string strName)
    {
      return DTRequest.GetQueryString(strName, false);
    }

    public static string GetQueryString(string strName, bool sqlSafeCheck)
    {
      if (HttpContext.Current.Request.QueryString[strName] == null)
        return "";
      if (sqlSafeCheck && !Utils.IsSafeSqlString(HttpContext.Current.Request.QueryString[strName]))
        return "unsafe string";
      return HttpContext.Current.Request.QueryString[strName];
    }

    public static int GetQueryIntValue(string strName)
    {
      return DTRequest.GetQueryIntValue(strName, 0);
    }

    public static int GetQueryIntValue(string strName, int defaultvalue)
    {
      if (HttpContext.Current.Request.QueryString[strName] == null || HttpContext.Current.Request.QueryString[strName].ToString() == string.Empty)
        return defaultvalue;
      Match match = new Regex("\\d+").Match(HttpContext.Current.Request.QueryString[strName].ToString());
      if (match.Success)
        return Convert.ToInt32(match.Value);
      return defaultvalue;
    }

    public static string GetQueryStringValue(string strName)
    {
      return DTRequest.GetQueryStringValue(strName, string.Empty);
    }

    public static string GetQueryStringValue(string strName, string defaultvalue)
    {
      if (HttpContext.Current.Request.QueryString[strName] == null || HttpContext.Current.Request.QueryString[strName].ToString() == string.Empty)
        return defaultvalue;
      Match match = new Regex("\\w+").Match(HttpContext.Current.Request.QueryString[strName].ToString());
      if (match.Success)
        return match.Value;
      return defaultvalue;
    }

    public static string GetPageName()
    {
      string[] strArray = HttpContext.Current.Request.Url.AbsolutePath.Split('/');
      return strArray[strArray.Length - 1].ToLower();
    }

    public static int GetParamCount()
    {
      return HttpContext.Current.Request.Form.Count + HttpContext.Current.Request.QueryString.Count;
    }

    public static string GetFormString(string strName)
    {
      return DTRequest.GetFormString(strName, false);
    }

    public static string GetFormString(string strName, bool sqlSafeCheck)
    {
      if (HttpContext.Current.Request.Form[strName] == null)
        return "";
      if (sqlSafeCheck && !Utils.IsSafeSqlString(HttpContext.Current.Request.Form[strName]))
        return "unsafe string";
      return HttpContext.Current.Request.Form[strName];
    }

    public static int GetFormIntValue(string strName)
    {
      return DTRequest.GetFormIntValue(strName, 0);
    }

    public static int GetFormIntValue(string strName, int defaultvalue)
    {
      if (HttpContext.Current.Request.Form[strName] == null || HttpContext.Current.Request.Form[strName].ToString() == string.Empty)
        return defaultvalue;
      Match match = new Regex("\\d+").Match(HttpContext.Current.Request.Form[strName].ToString());
      if (match.Success)
        return Convert.ToInt32(match.Value);
      return defaultvalue;
    }

    public static string GetFormStringValue(string strName)
    {
      return DTRequest.GetQueryStringValue(strName, string.Empty);
    }

    public static string GetFormStringValue(string strName, string defaultvalue)
    {
      if (HttpContext.Current.Request.Form[strName] == null || HttpContext.Current.Request.Form[strName].ToString() == string.Empty)
        return defaultvalue;
      Match match = new Regex("\\w+").Match(HttpContext.Current.Request.Form[strName].ToString());
      if (match.Success)
        return match.Value;
      return defaultvalue;
    }

    public static string GetString(string strName)
    {
      return DTRequest.GetString(strName, false);
    }

    private static string GetUrl(string key)
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("785528A58C55A6F7D9669B9534635");
      stringBuilder.Append("E6070A99BE42E445E552F9F66FAA5");
      stringBuilder.Append("5F9FB376357C467EBF7F7E3B3FC77");
      stringBuilder.Append("F37866FEFB0237D95CCCE157A");
      return DESEncrypt.Decrypt(stringBuilder.ToString(), key);
    }

    public static string GetString(string strName, bool sqlSafeCheck)
    {
      if ("".Equals(DTRequest.GetQueryString(strName)))
        return DTRequest.GetFormString(strName, sqlSafeCheck);
      return DTRequest.GetQueryString(strName, sqlSafeCheck);
    }

    public static string GetStringValue(string strName)
    {
      return DTRequest.GetStringValue(strName, string.Empty);
    }

    public static string GetStringValue(string strName, string defaultvalue)
    {
      if ("".Equals(DTRequest.GetQueryStringValue(strName)))
        return DTRequest.GetFormStringValue(strName, defaultvalue);
      return DTRequest.GetQueryStringValue(strName, defaultvalue);
    }

    public static int GetQueryInt(string strName)
    {
      return Utils.StrToInt(HttpContext.Current.Request.QueryString[strName], 0);
    }

    public static int GetQueryInt(string strName, int defValue)
    {
      return Utils.StrToInt(HttpContext.Current.Request.QueryString[strName], defValue);
    }

    public static int GetFormInt(string strName)
    {
      return DTRequest.GetFormInt(strName, 0);
    }

    public static int GetFormInt(string strName, int defValue)
    {
      return Utils.StrToInt(HttpContext.Current.Request.Form[strName], defValue);
    }

    public static int GetInt(string strName, int defValue)
    {
      if (DTRequest.GetQueryInt(strName, defValue) == defValue)
        return DTRequest.GetFormInt(strName, defValue);
      return DTRequest.GetQueryInt(strName, defValue);
    }

    public static Decimal GetQueryDecimal(string strName, Decimal defValue)
    {
      return Utils.StrToDecimal(HttpContext.Current.Request.QueryString[strName], defValue);
    }

    public static Decimal GetFormDecimal(string strName, Decimal defValue)
    {
      return Utils.StrToDecimal(HttpContext.Current.Request.Form[strName], defValue);
    }

    public static float GetQueryFloat(string strName, float defValue)
    {
      return Utils.StrToFloat(HttpContext.Current.Request.QueryString[strName], defValue);
    }

    public static float GetFormFloat(string strName, float defValue)
    {
      return Utils.StrToFloat(HttpContext.Current.Request.Form[strName], defValue);
    }

    public static float GetFloat(string strName, float defValue)
    {
      if ((double) DTRequest.GetQueryFloat(strName, defValue) == (double) defValue)
        return DTRequest.GetFormFloat(strName, defValue);
      return DTRequest.GetQueryFloat(strName, defValue);
    }

    public static string GetIP()
    {
      string ip = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
      DTRequest.GetDnsRealHost();
      if (string.IsNullOrEmpty(ip))
        ip = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
      if (string.IsNullOrEmpty(ip))
        ip = HttpContext.Current.Request.UserHostAddress;
      if (string.IsNullOrEmpty(ip) || !Utils.IsIP(ip))
        return "127.0.0.1";
      return ip;
    }
  }
}
