using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Web;

namespace Rain.Common
{
  public class Utils
  {
    public static string GetVersion()
    {
      return "4.0.0";
    }

    public static string MD5(string pwd)
    {
      MD5 md5 = (MD5) new MD5CryptoServiceProvider();
      byte[] bytes = Encoding.Default.GetBytes(pwd);
      byte[] hash = md5.ComputeHash(bytes);
      md5.Clear();
      string str = "";
      for (int index = 0; index < hash.Length; ++index)
        str += hash[index].ToString("x").PadLeft(2, '0');
      return str;
    }

    public static bool IsNumeric(object expression)
    {
      if (expression != null)
        return Utils.IsNumeric(expression.ToString());
      return false;
    }

    public static bool IsNumeric(string expression)
    {
      if (expression != null)
      {
        string input = expression;
        if (input.Length > 0 && input.Length <= 11 && Regex.IsMatch(input, "^[-]?[0-9]*[.]?[0-9]*$") && (input.Length < 10 || input.Length == 10 && input[0] == '1' || input.Length == 11 && input[0] == '-' && input[1] == '1'))
          return true;
      }
      return false;
    }

    public static string SubStrAddSuffix(string orginStr, int length, string suffix)
    {
      string str = orginStr;
      if (orginStr.Length > length)
        str = orginStr.Substring(0, length) + suffix;
      return str;
    }

    public static bool IsDouble(object expression)
    {
      if (expression != null)
        return Regex.IsMatch(expression.ToString(), "^([0-9])[0-9]*(\\.\\w*)?$");
      return false;
    }

    public static bool IsValidEmail(string strEmail)
    {
      return Regex.IsMatch(strEmail, "^[\\w\\.]+([-]\\w+)*@[A-Za-z0-9-_]+[\\.][A-Za-z0-9-_]");
    }

    public static bool IsValidDoEmail(string strEmail)
    {
      return Regex.IsMatch(strEmail, "^@((\\[[0-9]{1,3}\\.[0-9]{1,3}\\.[0-9]{1,3}\\.)|(([\\w-]+\\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\\]?)$");
    }

    public static bool IsURL(string strUrl)
    {
      return Regex.IsMatch(strUrl, "^(http|https)\\://([a-zA-Z0-9\\.\\-]+(\\:[a-zA-Z0-9\\.&%\\$\\-]+)*@)*((25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[1-9])\\.(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[1-9]|0)\\.(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[1-9]|0)\\.(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[0-9])|localhost|([a-zA-Z0-9\\-]+\\.)*[a-zA-Z0-9\\-]+\\.(com|edu|gov|int|mil|net|org|biz|arpa|info|name|pro|aero|coop|museum|[a-zA-Z]{1,10}))(\\:[0-9]+)*(/($|[a-zA-Z0-9\\.\\,\\?\\'\\\\\\+&%\\$#\\=~_\\-]+))*$");
    }

    public static string[] GetStrArray(string str)
    {
      return str.Split(new char[44]);
    }

    public static string GetArrayStr(List<string> list, string speater)
    {
      StringBuilder stringBuilder = new StringBuilder();
      for (int index = 0; index < list.Count; ++index)
      {
        if (index == list.Count - 1)
        {
          stringBuilder.Append(list[index]);
        }
        else
        {
          stringBuilder.Append(list[index]);
          stringBuilder.Append(speater);
        }
      }
      return stringBuilder.ToString();
    }

    public static bool StrToBool(object expression, bool defValue)
    {
      if (expression != null)
        return Utils.StrToBool(expression, defValue);
      return defValue;
    }

    public static bool StrToBool(string expression, bool defValue)
    {
      if (expression != null)
      {
        if (string.Compare(expression, "true", true) == 0)
          return true;
        if (string.Compare(expression, "false", true) == 0)
          return false;
      }
      return defValue;
    }

    public static int ObjToInt(object expression, int defValue)
    {
      if (expression != null)
        return Utils.StrToInt(expression.ToString(), defValue);
      return defValue;
    }

    public static int StrToInt(string expression, int defValue)
    {
      if (string.IsNullOrEmpty(expression) || expression.Trim().Length >= 11 || !Regex.IsMatch(expression.Trim(), "^([-]|[0-9])[0-9]*(\\.\\w*)?$"))
        return defValue;
      int result;
      if (int.TryParse(expression, out result))
        return result;
      return Convert.ToInt32(Utils.StrToFloat(expression, (float) defValue));
    }

    public static Decimal ObjToDecimal(object expression, Decimal defValue)
    {
      if (expression != null)
        return Utils.StrToDecimal(expression.ToString(), defValue);
      return defValue;
    }

    public static Decimal StrToDecimal(string expression, Decimal defValue)
    {
      if (expression == null || expression.Length > 10)
        return defValue;
      Decimal result = defValue;
      if (expression != null && Regex.IsMatch(expression, "^([-]|[0-9])[0-9]*(\\.\\w*)?$"))
        Decimal.TryParse(expression, out result);
      return result;
    }

    public static float ObjToFloat(object expression, float defValue)
    {
      if (expression != null)
        return Utils.StrToFloat(expression.ToString(), defValue);
      return defValue;
    }

    public static float StrToFloat(string expression, float defValue)
    {
      if (expression == null || expression.Length > 10)
        return defValue;
      float result = defValue;
      if (expression != null && Regex.IsMatch(expression, "^([-]|[0-9])[0-9]*(\\.\\w*)?$"))
        float.TryParse(expression, out result);
      return result;
    }

    public static DateTime StrToDateTime(string str, DateTime defValue)
    {
      DateTime result;
      if (!string.IsNullOrEmpty(str) && DateTime.TryParse(str, out result))
        return result;
      return defValue;
    }

    public static DateTime StrToDateTime(string str)
    {
      return Utils.StrToDateTime(str, DateTime.Now);
    }

    public static DateTime ObjectToDateTime(object obj)
    {
      return Utils.StrToDateTime(obj.ToString());
    }

    public static DateTime ObjectToDateTime(object obj, DateTime defValue)
    {
      return Utils.StrToDateTime(obj.ToString(), defValue);
    }

    public static string ObjectToStr(object obj)
    {
      if (obj == null)
        return "";
      return obj.ToString().Trim();
    }

    public static int ObjToInt(object obj)
    {
      if (Utils.isNumber(obj))
        return int.Parse(obj.ToString());
      return 0;
    }

    public static bool isNumber(object o)
    {
      int result;
      return o != null && o.ToString().Trim().Length != 0 && int.TryParse(o.ToString(), out result);
    }

    public static string[] SplitString(string strContent, string strSplit)
    {
      if (string.IsNullOrEmpty(strContent))
        return new string[0];
      if (strContent.IndexOf(strSplit) >= 0)
        return Regex.Split(strContent, Regex.Escape(strSplit), RegexOptions.IgnoreCase);
      return new string[1]{ strContent };
    }

    public static string[] SplitString(string strContent, string strSplit, int count)
    {
      string[] strArray1 = new string[count];
      string[] strArray2 = Utils.SplitString(strContent, strSplit);
      for (int index = 0; index < count; ++index)
        strArray1[index] = index >= strArray2.Length ? string.Empty : strArray2[index];
      return strArray1;
    }

    public static string DelLastComma(string str)
    {
      if (str.Length < 1)
        return "";
      return str.Substring(0, str.LastIndexOf(","));
    }

    public static string DelLastChar(string str, string strchar)
    {
      if (string.IsNullOrEmpty(str))
        return "";
      if (str.LastIndexOf(strchar) >= 0 && str.LastIndexOf(strchar) == str.Length - 1)
        return str.Substring(0, str.LastIndexOf(strchar));
      return str;
    }

    public static string StringOfChar(int strLong, string str)
    {
      string str1 = "";
      for (int index = 0; index < strLong; ++index)
        str1 += str;
      return str1;
    }

    public static string GetRamCode()
    {
      return DateTime.Now.ToString("yyyyMMddHHmmssffff");
    }

    public static string Number(int Length)
    {
      return Utils.Number(Length, false);
    }

    public static string Number(int Length, bool Sleep)
    {
      if (Sleep)
        Thread.Sleep(3);
      string str = "";
      Random random = new Random();
      for (int index = 0; index < Length; ++index)
        str += random.Next(10).ToString();
      return str;
    }

    public static string GetCheckCode(int codeCount)
    {
      string empty = string.Empty;
      int num1 = 0;
      long num2 = DateTime.Now.Ticks + (long) num1;
      int num3 = num1 + 1;
      Random random = new Random((int) (num2 & (long) uint.MaxValue) | (int) (num2 >> num3));
      for (int index = 0; index < codeCount; ++index)
      {
        int num4 = random.Next();
        char ch = num4 % 2 != 0 ? (char) (65U + (uint) (ushort) (num4 % 26)) : (char) (48U + (uint) (ushort) (num4 % 10));
        empty += ch.ToString();
      }
      return empty;
    }

    public static string GetOrderNumber()
    {
      return DateTime.Now.ToString("yyMMddHHmmss") + Utils.Number(2, true).ToString();
    }

    private static int Next(int numSeeds, int length)
    {
      byte[] data = new byte[length];
      new RNGCryptoServiceProvider().GetBytes(data);
      uint num = 0;
      for (int index = 0; index < length; ++index)
        num |= (uint) data[index] << (length - 1 - index) * 8;
      return (int) ((long) num % (long) numSeeds);
    }

    public static string CutString(string inputString, int len)
    {
      if (string.IsNullOrEmpty(inputString))
        return "";
      inputString = Utils.DropHTML(inputString);
      ASCIIEncoding asciiEncoding = new ASCIIEncoding();
      int num = 0;
      string str = "";
      byte[] bytes = asciiEncoding.GetBytes(inputString);
      for (int startIndex = 0; startIndex < bytes.Length; ++startIndex)
      {
        if (bytes[startIndex] == (byte) 63)
          num += 2;
        else
          ++num;
        try
        {
          str += inputString.Substring(startIndex, 1);
        }
        catch
        {
          break;
        }
        if (num > len)
          break;
      }
      if (Encoding.Default.GetBytes(inputString).Length > len)
        str += "…";
      return str;
    }

    public static string DropHTML(string Htmlstring)
    {
      if (string.IsNullOrEmpty(Htmlstring))
        return "";
      Htmlstring = Regex.Replace(Htmlstring, "<script[^>]*?>.*?</script>", "", RegexOptions.IgnoreCase);
      Htmlstring = Regex.Replace(Htmlstring, "<(.[^>]*)>", "", RegexOptions.IgnoreCase);
      Htmlstring = Regex.Replace(Htmlstring, "([\\r\\n])[\\s]+", "", RegexOptions.IgnoreCase);
      Htmlstring = Regex.Replace(Htmlstring, "-->", "", RegexOptions.IgnoreCase);
      Htmlstring = Regex.Replace(Htmlstring, "<!--.*", "", RegexOptions.IgnoreCase);
      Htmlstring = Regex.Replace(Htmlstring, "&(quot|#34);", "\"", RegexOptions.IgnoreCase);
      Htmlstring = Regex.Replace(Htmlstring, "&(amp|#38);", "&", RegexOptions.IgnoreCase);
      Htmlstring = Regex.Replace(Htmlstring, "&(lt|#60);", "<", RegexOptions.IgnoreCase);
      Htmlstring = Regex.Replace(Htmlstring, "&(gt|#62);", ">", RegexOptions.IgnoreCase);
      Htmlstring = Regex.Replace(Htmlstring, "&(nbsp|#160);", " ", RegexOptions.IgnoreCase);
      Htmlstring = Regex.Replace(Htmlstring, "&(iexcl|#161);", "¡", RegexOptions.IgnoreCase);
      Htmlstring = Regex.Replace(Htmlstring, "&(cent|#162);", "¢", RegexOptions.IgnoreCase);
      Htmlstring = Regex.Replace(Htmlstring, "&(pound|#163);", "£", RegexOptions.IgnoreCase);
      Htmlstring = Regex.Replace(Htmlstring, "&(copy|#169);", "©", RegexOptions.IgnoreCase);
      Htmlstring = Regex.Replace(Htmlstring, "&#(\\d+);", "", RegexOptions.IgnoreCase);
      Htmlstring.Replace("<", "");
      Htmlstring.Replace(">", "");
      Htmlstring.Replace("\r\n", "");
      Htmlstring.Replace("&emsp;", "");
      Htmlstring = HttpContext.Current.Server.HtmlEncode(Htmlstring).Trim();
      return Htmlstring;
    }

    public static string DropHTML(string Htmlstring, int strLen)
    {
      return Utils.CutString(Utils.DropHTML(Htmlstring), strLen);
    }

    public static string ToHtml(string Input)
    {
      StringBuilder stringBuilder = new StringBuilder(Input);
      stringBuilder.Replace("'", "&apos;");
      stringBuilder.Replace("&", "&amp;");
      stringBuilder.Replace("<", "&lt;");
      stringBuilder.Replace(">", "&gt;");
      stringBuilder.Replace("\r\n", "<br />");
      stringBuilder.Replace("\n", "<br />");
      stringBuilder.Replace("\t", " ");
      return stringBuilder.ToString();
    }

    public static string ToTxt(string Input)
    {
      StringBuilder stringBuilder = new StringBuilder(Input);
      stringBuilder.Replace("&nbsp;", " ");
      stringBuilder.Replace("<br>", "\r\n");
      stringBuilder.Replace("<br>", "\n");
      stringBuilder.Replace("<br />", "\n");
      stringBuilder.Replace("<br />", "\r\n");
      stringBuilder.Replace("&lt;", "<");
      stringBuilder.Replace("&gt;", ">");
      stringBuilder.Replace("&amp;", "&");
      return stringBuilder.ToString();
    }

    public static bool IsSafeSqlString(string str)
    {
      return !Regex.IsMatch(str, "[-|;|,|\\/|\\(|\\)|\\[|\\]|\\}|\\{|%|@|\\*|!|\\']");
    }

    public static string Filter(string sInput)
    {
      if (sInput == null || sInput == "")
        return (string) null;
      string lower = sInput.ToLower();
      string str1 = sInput;
      string str2 = "*|and|exec|insert|select|delete|update|count|master|truncate|declare|char(|mid(|chr(|'";
      if (Regex.Match(lower, Regex.Escape(str2), RegexOptions.IgnoreCase | RegexOptions.Compiled).Success)
        throw new Exception("字符串中含有非法字符!");
      return str1.Replace("'", "''");
    }

    public static bool SqlFilter(string word, string InText)
    {
      if (InText == null)
        return false;
      string str1 = word;
      char[] chArray = new char[1]{ '|' };
      foreach (string str2 in str1.Split(chArray))
      {
        if (InText.ToLower().IndexOf(str2 + " ") > -1 || InText.ToLower().IndexOf(" " + str2) > -1)
          return true;
      }
      return false;
    }

    public static string Htmls(string Input)
    {
      if (Input != string.Empty && Input != null)
        return Input.ToLower().Replace("<script", "&lt;script").Replace("script>", "script&gt;").Replace("<%", "&lt;%").Replace("%>", "%&gt;").Replace("<$", "&lt;$").Replace("$>", "$&gt;");
      return string.Empty;
    }

    public static bool IsIP(string ip)
    {
      return Regex.IsMatch(ip, "^((2[0-4]\\d|25[0-5]|[01]?\\d\\d?)\\.){3}(2[0-4]\\d|25[0-5]|[01]?\\d\\d?)$");
    }

    public static string GetXmlMapPath(string xmlName)
    {
      return Utils.GetMapPath(ConfigurationManager.AppSettings[xmlName].ToString());
    }

    public static string GetMapPath(string strPath)
    {
      if (strPath.ToLower().StartsWith("http://"))
        return strPath;
      if (HttpContext.Current != null)
        return HttpContext.Current.Server.MapPath(strPath);
      strPath = strPath.Replace("/", "\\");
      if (strPath.StartsWith("\\"))
        strPath = strPath.Substring(strPath.IndexOf('\\', 1)).TrimStart('\\');
      return Path.Combine(AppDomain.CurrentDomain.BaseDirectory, strPath);
    }

    public static bool DeleteFile(string _filepath)
    {
      if (string.IsNullOrEmpty(_filepath))
        return false;
      string mapPath = Utils.GetMapPath(_filepath);
      if (!System.IO.File.Exists(mapPath))
        return false;
      System.IO.File.Delete(mapPath);
      return true;
    }

    public static void DeleteUpFile(string _filepath)
    {
      if (string.IsNullOrEmpty(_filepath))
        return;
      string mapPath1 = Utils.GetMapPath(_filepath);
      if (System.IO.File.Exists(mapPath1))
        System.IO.File.Delete(mapPath1);
      if (_filepath.LastIndexOf("/") < 0)
        return;
      string mapPath2 = Utils.GetMapPath(_filepath.Substring(0, _filepath.LastIndexOf("/")) + "mall_" + _filepath.Substring(_filepath.LastIndexOf("/") + 1));
      if (System.IO.File.Exists(mapPath2))
        System.IO.File.Delete(mapPath2);
    }

    public static void DeleteContentPic(string content, string startstr)
    {
      if (string.IsNullOrEmpty(content))
        return;
      foreach (Match match in new Regex("IMG[^>]*?src\\s*=\\s*(?:\"(?<1>[^\"]*)\"|'(?<1>[^']*)')", RegexOptions.IgnoreCase).Matches(content))
      {
        string strPath = match.Groups[1].Value;
        string mapPath = Utils.GetMapPath(strPath);
        try
        {
          if (strPath.ToLower().StartsWith(startstr.ToLower()) && System.IO.File.Exists(mapPath))
            System.IO.File.Delete(mapPath);
        }
        catch
        {
        }
      }
    }

    public static bool DeleteDirectory(string _dirpath)
    {
      if (string.IsNullOrEmpty(_dirpath))
        return false;
      string mapPath = Utils.GetMapPath(_dirpath);
      if (!Directory.Exists(mapPath))
        return false;
      Directory.Delete(mapPath, true);
      return true;
    }

    public static bool MoveDirectory(string old_dirpath, string new_dirpath)
    {
      if (string.IsNullOrEmpty(old_dirpath))
        return false;
      string mapPath1 = Utils.GetMapPath(old_dirpath);
      string mapPath2 = Utils.GetMapPath(new_dirpath);
      if (!Directory.Exists(mapPath1))
        return false;
      Directory.Move(mapPath1, mapPath2);
      return true;
    }

    public static int GetFileSize(string _filepath)
    {
      if (string.IsNullOrEmpty(_filepath))
        return 0;
      string mapPath = Utils.GetMapPath(_filepath);
      if (System.IO.File.Exists(mapPath))
        return (int) new FileInfo(mapPath).Length / 1024;
      return 0;
    }

    public static string GetFileExt(string _filepath)
    {
      if (string.IsNullOrEmpty(_filepath) || _filepath.LastIndexOf(".") <= 0)
        return "";
      return _filepath.Substring(_filepath.LastIndexOf(".") + 1);
    }

    public static string GetFileName(string _filepath)
    {
      return _filepath.Substring(_filepath.LastIndexOf("/") + 1);
    }

    public static bool FileExists(string _filepath)
    {
      return System.IO.File.Exists(Utils.GetMapPath(_filepath));
    }

    public static string GetDomainStr(string key, string uriPath)
    {
      string str = CacheHelper.Get(key) as string;
      if (str == null)
      {
        WebClient webClient = new WebClient();
        try
        {
          webClient.Encoding = Encoding.UTF8;
          str = webClient.DownloadString(uriPath);
        }
        catch
        {
          str = "暂时无法连接!";
        }
        CacheHelper.Insert(key, (object) str, 60);
      }
      return str;
    }

    public static void WriteCookie(string strName, string strValue)
    {
      HttpCookie cookie = HttpContext.Current.Request.Cookies[strName] ?? new HttpCookie(strName);
      cookie.Value = Utils.UrlEncode(strValue);
      HttpContext.Current.Response.AppendCookie(cookie);
    }

    public static void WriteCookie(string strName, string key, string strValue)
    {
      HttpCookie cookie = HttpContext.Current.Request.Cookies[strName] ?? new HttpCookie(strName);
      cookie[key] = Utils.UrlEncode(strValue);
      HttpContext.Current.Response.AppendCookie(cookie);
    }

    public static void WriteCookie(string strName, string key, string strValue, int expires)
    {
      HttpCookie cookie = HttpContext.Current.Request.Cookies[strName] ?? new HttpCookie(strName);
      cookie[key] = Utils.UrlEncode(strValue);
      cookie.Expires = DateTime.Now.AddMinutes((double) expires);
      HttpContext.Current.Response.AppendCookie(cookie);
    }

    public static void WriteCookie(string strName, string strValue, int expires)
    {
      HttpCookie cookie = HttpContext.Current.Request.Cookies[strName] ?? new HttpCookie(strName);
      cookie.Value = Utils.UrlEncode(strValue);
      cookie.Expires = DateTime.Now.AddMinutes((double) expires);
      HttpContext.Current.Response.AppendCookie(cookie);
    }

    public static string GetCookie(string strName)
    {
      if (HttpContext.Current.Request.Cookies != null && HttpContext.Current.Request.Cookies[strName] != null)
        return Utils.UrlDecode(HttpContext.Current.Request.Cookies[strName].Value.ToString());
      return "";
    }

    public static string GetCookie(string strName, string key)
    {
      if (HttpContext.Current.Request.Cookies != null && HttpContext.Current.Request.Cookies[strName] != null && HttpContext.Current.Request.Cookies[strName][key] != null)
        return Utils.UrlDecode(HttpContext.Current.Request.Cookies[strName][key].ToString());
      return "";
    }

    public static string ReplaceStr(string originalStr, string oldStr, string newStr)
    {
      if (string.IsNullOrEmpty(oldStr))
        return "";
      return originalStr.Replace(oldStr, newStr);
    }

    public static string OutPageList(
      int pageSize,
      int pageIndex,
      int totalCount,
      string linkUrl,
      int centSize)
    {
      if (totalCount < 1 || pageSize < 1)
        return "";
      int num1 = totalCount / pageSize;
      if (num1 < 1)
        return "";
      if (totalCount % pageSize > 0)
        ++num1;
      if (num1 <= 1)
        return "";
      StringBuilder stringBuilder = new StringBuilder();
      string oldStr1 = "__id__";
      string originalStr1 = linkUrl;
      string oldStr2 = oldStr1;
      int num2 = pageIndex - 1;
      string newStr1 = num2.ToString();
      string str1 = "<a href=\"" + Utils.ReplaceStr(originalStr1, oldStr2, newStr1) + "\">«上一页</a>";
      string originalStr2 = linkUrl;
      string oldStr3 = oldStr1;
      num2 = pageIndex + 1;
      string newStr2 = num2.ToString();
      string str2 = "<a href=\"" + Utils.ReplaceStr(originalStr2, oldStr3, newStr2) + "\">下一页»</a>";
      string str3 = "<a href=\"" + Utils.ReplaceStr(linkUrl, oldStr1, "1") + "\">1</a>";
      string str4 = "<a href=\"" + Utils.ReplaceStr(linkUrl, oldStr1, num1.ToString()) + "\">" + num1.ToString() + "</a>";
      if (pageIndex <= 1)
        str1 = "<span class=\"disabled\">«上一页</span>";
      if (pageIndex >= num1)
        str2 = "<span class=\"disabled\">下一页»</span>";
      if (pageIndex == 1)
        str3 = "<span class=\"current\">1</span>";
      if (pageIndex == num1)
        str4 = "<span class=\"current\">" + num1.ToString() + "</span>";
      int num3 = pageIndex - centSize / 2;
      if (pageIndex < centSize)
        num3 = 2;
      int num4 = pageIndex + centSize - (centSize / 2 + 1);
      if (num4 >= num1)
        num4 = num1 - 1;
      stringBuilder.Append("<span>共" + (object) totalCount + "记录</span>");
      stringBuilder.Append(str1 + str3);
      if (pageIndex >= centSize)
        stringBuilder.Append("<span>...</span>\n");
      for (int index = num3; index <= num4; ++index)
      {
        if (index == pageIndex)
          stringBuilder.Append("<span class=\"current\">" + (object) index + "</span>");
        else
          stringBuilder.Append("<a href=\"" + Utils.ReplaceStr(linkUrl, oldStr1, index.ToString()) + "\">" + (object) index + "</a>");
      }
      if (num1 - pageIndex > centSize - centSize / 2)
        stringBuilder.Append("<span>...</span>");
      stringBuilder.Append(str4 + str2);
      return stringBuilder.ToString();
    }

    public static string UrlEncode(string str)
    {
      if (string.IsNullOrEmpty(str))
        return "";
      str = str.Replace("'", "");
      return HttpContext.Current.Server.UrlEncode(str);
    }

    public static string UrlDecode(string str)
    {
      if (string.IsNullOrEmpty(str))
        return "";
      return HttpContext.Current.Server.UrlDecode(str);
    }

    public static string CombUrlTxt(string _url, string _keys, params string[] _values)
    {
      StringBuilder stringBuilder = new StringBuilder();
      try
      {
        string[] strArray = _keys.Split('&');
        for (int index = 0; index < strArray.Length; ++index)
        {
          if (!string.IsNullOrEmpty(_values[index]) && _values[index] != "0")
          {
            _values[index] = Utils.UrlEncode(_values[index]);
            stringBuilder.Append(string.Format(strArray[index], (object[]) _values) + "&");
          }
        }
        if (!string.IsNullOrEmpty(stringBuilder.ToString()) && _url.IndexOf("?") == -1)
          stringBuilder.Insert(0, "?");
      }
      catch
      {
        return _url;
      }
      return _url + Utils.DelLastChar(stringBuilder.ToString(), "&");
    }

    public static string HttpPost(string url, string param)
    {
      HttpWebRequest httpWebRequest = (HttpWebRequest) WebRequest.Create(url);
      httpWebRequest.Method = "POST";
      httpWebRequest.ContentType = "application/x-www-form-urlencoded";
      httpWebRequest.Accept = "*/*";
      httpWebRequest.Timeout = 15000;
      httpWebRequest.AllowAutoRedirect = false;
      StreamWriter streamWriter1 = (StreamWriter) null;
      WebResponse webResponse = (WebResponse) null;
      string str = (string) null;
      try
      {
        StreamWriter streamWriter2 = new StreamWriter(httpWebRequest.GetRequestStream());
        streamWriter2.Write(param);
        streamWriter2.Close();
        WebResponse response = httpWebRequest.GetResponse();
        if (response != null)
        {
          StreamReader streamReader = new StreamReader(response.GetResponseStream(), Encoding.UTF8);
          str = streamReader.ReadToEnd();
          streamReader.Close();
        }
      }
      catch (Exception ex)
      {
        throw;
      }
      finally
      {
        streamWriter1 = (StreamWriter) null;
        webResponse = (WebResponse) null;
      }
      return str;
    }

    public static string HttpGet(string url)
    {
      HttpWebRequest httpWebRequest = (HttpWebRequest) WebRequest.Create(url);
      httpWebRequest.Method = "GET";
      httpWebRequest.Accept = "*/*";
      httpWebRequest.Timeout = 15000;
      httpWebRequest.AllowAutoRedirect = false;
      WebResponse webResponse = (WebResponse) null;
      string str = (string) null;
      try
      {
        WebResponse response = httpWebRequest.GetResponse();
        if (response != null)
        {
          StreamReader streamReader = new StreamReader(response.GetResponseStream(), Encoding.UTF8);
          str = streamReader.ReadToEnd();
          streamReader.Close();
        }
      }
      catch (Exception ex)
      {
        throw;
      }
      finally
      {
        webResponse = (WebResponse) null;
      }
      return str;
    }

    public static string UrlExecute(string urlPath)
    {
      if (string.IsNullOrEmpty(urlPath))
        return "error";
      StringWriter stringWriter = new StringWriter();
      try
      {
        HttpContext.Current.Server.Execute(urlPath, (TextWriter) stringWriter);
        return stringWriter.ToString();
      }
      catch (Exception ex)
      {
        return "error";
      }
      finally
      {
        stringWriter.Close();
        stringWriter.Dispose();
      }
    }

    public static Dictionary<string, string> ActionType()
    {
      return new Dictionary<string, string>()
      {
        {
          "Show",
          "显示"
        },
        {
          "View",
          "查看"
        },
        {
          "Add",
          "添加"
        },
        {
          "Edit",
          "修改"
        },
        {
          "Delete",
          "删除"
        },
        {
          "Audit",
          "审核"
        },
        {
          "Reply",
          "回复"
        },
        {
          "Confirm",
          "确认"
        },
        {
          "Cancel",
          "取消"
        },
        {
          "Invalid",
          "作废"
        },
        {
          "Build",
          "生成"
        },
        {
          "Instal",
          "安装"
        },
        {
          "Unload",
          "卸载"
        },
        {
          "Back",
          "备份"
        },
        {
          "Restore",
          "还原"
        },
        {
          "Replace",
          "替换"
        }
      };
    }

    public static string GetUrlExtension(string urlPage, string staticExtension)
    {
      int startIndex = urlPage.LastIndexOf('.');
      if (startIndex > 0)
        return urlPage.Replace(urlPage.Substring(startIndex), "." + staticExtension);
      return urlPage;
    }

    public static string GetUrlExtension(string urlPage, string staticExtension, bool defaultVal)
    {
      int startIndex = urlPage.LastIndexOf('.');
      if (startIndex > 0)
        return urlPage.Replace(urlPage.Substring(startIndex), "." + staticExtension);
      if (!defaultVal)
        return urlPage;
      if (urlPage.EndsWith("/"))
        return urlPage + "index." + staticExtension;
      return urlPage + "/index." + staticExtension;
    }

    public class VersionInfo
    {
      public int FileMajorPart
      {
        get
        {
          return 4;
        }
      }

      public int FileMinorPart
      {
        get
        {
          return 0;
        }
      }

      public int FileBuildPart
      {
        get
        {
          return 0;
        }
      }

      public string ProductName
      {
        get
        {
          return "UScms";
        }
      }

      public int ProductType
      {
        get
        {
          return 1;
        }
      }
    }
  }
}
