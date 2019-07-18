// Decompiled with JetBrains decompiler
// Type: Rain.API.Payment.alipaypc.Submit
// Assembly: Rain.API, Version=4.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 114351D3-1A2F-4CC4-A6A6-43C59DB5A56B
// Assembly location: C:\Users\Felix.Ho\Desktop\page\bin\Rain.API.dll

using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Xml;

namespace Rain.API.Payment.alipaypc
{
  public class Submit
  {
    private static string _key = string.Empty;
    private static string _input_charset = string.Empty;
    private static string _sign_type = string.Empty;

    static Submit()
    {
      Submit._key = Config.Key.Trim().ToLower();
      Submit._input_charset = Config.Input_charset.Trim().ToLower();
      Submit._sign_type = Config.Sign_type.Trim().ToUpper();
    }

    private static Dictionary<string, string> BuildRequestPara(
      SortedDictionary<string, string> sParaTemp)
    {
      Dictionary<string, string> dictionary = new Dictionary<string, string>();
      Dictionary<string, string> dicArray = Core.FilterPara(sParaTemp);
      string str = Core.BuildMysign(dicArray, Submit._key, Submit._sign_type, Submit._input_charset);
      dicArray.Add("sign", str);
      dicArray.Add("sign_type", Submit._sign_type);
      return dicArray;
    }

    private static string BuildRequestParaToString(
      SortedDictionary<string, string> sParaTemp,
      Encoding code)
    {
      Dictionary<string, string> dictionary = new Dictionary<string, string>();
      return Core.CreateLinkStringUrlencode(Submit.BuildRequestPara(sParaTemp), code);
    }

    public static string BuildFormHtml(
      SortedDictionary<string, string> sParaTemp,
      string gateway,
      string strMethod,
      string strButtonValue)
    {
      Dictionary<string, string> dictionary1 = new Dictionary<string, string>();
      Dictionary<string, string> dictionary2 = Submit.BuildRequestPara(sParaTemp);
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("<form id='alipaysubmit' name='alipaysubmit' action='" + gateway + "_input_charset=" + Submit._input_charset + "' method='" + strMethod.ToLower().Trim() + "'>");
      foreach (KeyValuePair<string, string> keyValuePair in dictionary2)
        stringBuilder.Append("<input type='hidden' name='" + keyValuePair.Key + "' value='" + keyValuePair.Value + "'/>");
      stringBuilder.Append("<input type='submit' value='" + strButtonValue + "' style='display:none;'></form>");
      stringBuilder.Append("<script>document.forms['alipaysubmit'].submit();</script>");
      return stringBuilder.ToString();
    }

    public static XmlDocument SendPostInfo(
      SortedDictionary<string, string> sParaTemp,
      string gateway)
    {
      Encoding encoding = Encoding.GetEncoding(Submit._input_charset);
      string s = Submit.BuildRequestParaToString(sParaTemp, encoding);
      byte[] bytes = encoding.GetBytes(s);
      string requestUriString = gateway + "_input_charset=" + Submit._input_charset;
      XmlDocument xmlDocument = new XmlDocument();
      try
      {
        HttpWebRequest httpWebRequest = (HttpWebRequest) WebRequest.Create(requestUriString);
        httpWebRequest.Method = "post";
        httpWebRequest.ContentType = "application/x-www-form-urlencoded";
        httpWebRequest.ContentLength = (long) bytes.Length;
        Stream requestStream = httpWebRequest.GetRequestStream();
        requestStream.Write(bytes, 0, bytes.Length);
        requestStream.Close();
        XmlTextReader xmlTextReader = new XmlTextReader(httpWebRequest.GetResponse().GetResponseStream());
        xmlDocument.Load((XmlReader) xmlTextReader);
      }
      catch (Exception ex)
      {
        string xml = "<error>" + ex.Message + "</error>";
        xmlDocument.LoadXml(xml);
      }
      return xmlDocument;
    }

    public static XmlDocument SendGetInfo(
      SortedDictionary<string, string> sParaTemp,
      string gateway)
    {
      Encoding encoding = Encoding.GetEncoding(Submit._input_charset);
      string str = Submit.BuildRequestParaToString(sParaTemp, encoding);
      string requestUriString = gateway + str;
      XmlDocument xmlDocument = new XmlDocument();
      try
      {
        HttpWebRequest httpWebRequest = (HttpWebRequest) WebRequest.Create(requestUriString);
        httpWebRequest.Method = "get";
        XmlTextReader xmlTextReader = new XmlTextReader(httpWebRequest.GetResponse().GetResponseStream());
        xmlDocument.Load((XmlReader) xmlTextReader);
      }
      catch (Exception ex)
      {
        string xml = "<error>" + ex.Message + "</error>";
        xmlDocument.LoadXml(xml);
      }
      return xmlDocument;
    }
  }
}
