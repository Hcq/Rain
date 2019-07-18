// Decompiled with JetBrains decompiler
// Type: Rain.API.Payment.alipaypc.Core
// Assembly: Rain.API, Version=4.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 114351D3-1A2F-4CC4-A6A6-43C59DB5A56B
// Assembly location: C:\Users\Felix.Ho\Desktop\page\bin\Rain.API.dll

using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace Rain.API.Payment.alipaypc
{
  public class Core
  {
    public static string BuildMysign(
      Dictionary<string, string> dicArray,
      string key,
      string sign_type,
      string _input_charset)
    {
      return Core.Sign(Core.CreateLinkString(dicArray) + key, sign_type, _input_charset);
    }

    public static Dictionary<string, string> FilterPara(
      SortedDictionary<string, string> dicArrayPre)
    {
      Dictionary<string, string> dictionary = new Dictionary<string, string>();
      foreach (KeyValuePair<string, string> keyValuePair in dicArrayPre)
      {
        if (keyValuePair.Key.ToLower() != "sign" && keyValuePair.Key.ToLower() != "sign_type" && keyValuePair.Value != "" && keyValuePair.Value != null)
          dictionary.Add(keyValuePair.Key.ToLower(), keyValuePair.Value);
      }
      return dictionary;
    }

    public static string CreateLinkString(Dictionary<string, string> dicArray)
    {
      StringBuilder stringBuilder = new StringBuilder();
      foreach (KeyValuePair<string, string> dic in dicArray)
        stringBuilder.Append(dic.Key + "=" + dic.Value + "&");
      int length = stringBuilder.Length;
      stringBuilder.Remove(length - 1, 1);
      return stringBuilder.ToString();
    }

    public static string CreateLinkStringUrlencode(
      Dictionary<string, string> dicArray,
      Encoding code)
    {
      StringBuilder stringBuilder = new StringBuilder();
      foreach (KeyValuePair<string, string> dic in dicArray)
        stringBuilder.Append(dic.Key + "=" + HttpUtility.UrlEncode(dic.Value, code) + "&");
      int length = stringBuilder.Length;
      stringBuilder.Remove(length - 1, 1);
      return stringBuilder.ToString();
    }

    public static string Sign(string prestr, string sign_type, string _input_charset)
    {
      StringBuilder stringBuilder = new StringBuilder(32);
      if (sign_type.ToUpper() == "MD5")
      {
        foreach (byte num in new MD5CryptoServiceProvider().ComputeHash(Encoding.GetEncoding(_input_charset).GetBytes(prestr)))
          stringBuilder.Append(num.ToString("x").PadLeft(2, '0'));
      }
      return stringBuilder.ToString();
    }

    public static void LogResult(string sWord)
    {
      StreamWriter streamWriter = new StreamWriter(HttpContext.Current.Server.MapPath("log") + "\\" + DateTime.Now.ToString().Replace(":", "") + ".txt", false, Encoding.Default);
      streamWriter.Write(sWord);
      streamWriter.Close();
    }
  }
}
