// Decompiled with JetBrains decompiler
// Type: Rain.API.Payment.tenpaypc.TenpayUtil
// Assembly: Rain.API, Version=4.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 114351D3-1A2F-4CC4-A6A6-43C59DB5A56B
// Assembly location: C:\Users\Felix.Ho\Desktop\page\bin\Rain.API.dll

using System;
using System.Text;
using System.Web;
using System.Xml;
using Rain.BLL;
using Rain.Common;

namespace Rain.API.Payment.tenpaypc
{
  public class TenpayUtil
  {
    public string tenpay = "1";
    public string partner = "";
    public string key = "";
    public string type = "1";
    public string return_url = "";
    public string notify_url = "";

    public TenpayUtil()
    {
      string mapPath = Utils.GetMapPath("~/xmlconfig/tenpaypc.config");
      XmlDocument xmlDocument = new XmlDocument();
      xmlDocument.Load(mapPath);
      XmlNode xmlNode1 = xmlDocument.SelectSingleNode("Root/partner");
      XmlNode xmlNode2 = xmlDocument.SelectSingleNode("Root/key");
      XmlNode xmlNode3 = xmlDocument.SelectSingleNode("Root/type");
      XmlNode xmlNode4 = xmlDocument.SelectSingleNode("Root/return_url");
      XmlNode xmlNode5 = xmlDocument.SelectSingleNode("Root/notify_url");
      new siteconfig().loadConfig();
      this.partner = xmlNode1.InnerText;
      this.key = xmlNode2.InnerText;
      this.type = xmlNode3.InnerText;
      this.return_url = "http://" + HttpContext.Current.Request.Url.Authority.ToLower() + xmlNode4.InnerText;
      this.notify_url = "http://" + HttpContext.Current.Request.Url.Authority.ToLower() + xmlNode5.InnerText;
    }

    public string UrlEncode(string instr, string charset)
    {
      if (instr == null || instr.Trim() == "")
        return "";
      string str;
      try
      {
        str = HttpUtility.UrlEncode(instr, Encoding.GetEncoding(charset));
      }
      catch (Exception ex)
      {
        str = HttpUtility.UrlEncode(instr, Encoding.GetEncoding("GB2312"));
      }
      return str;
    }

    public string UrlDecode(string instr, string charset)
    {
      if (instr == null || instr.Trim() == "")
        return "";
      string str;
      try
      {
        str = HttpUtility.UrlDecode(instr, Encoding.GetEncoding(charset));
      }
      catch (Exception ex)
      {
        str = HttpUtility.UrlDecode(instr, Encoding.GetEncoding("GB2312"));
      }
      return str;
    }

    public uint UnixStamp()
    {
      return Convert.ToUInt32((DateTime.Now - TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1))).TotalSeconds);
    }

    public string BuildRandomStr(int length)
    {
      string str = new Random().Next().ToString();
      if (str.Length > length)
        str = str.Substring(0, length);
      else if (str.Length < length)
      {
        for (int index = length - str.Length; index > 0; --index)
          str.Insert(0, "0");
      }
      return str;
    }
  }
}
