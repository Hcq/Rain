// Decompiled with JetBrains decompiler
// Type: Rain.API.Payment.alipaypc.Config
// Assembly: Rain.API, Version=4.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 114351D3-1A2F-4CC4-A6A6-43C59DB5A56B
// Assembly location: C:\Users\Felix.Ho\Desktop\page\bin\Rain.API.dll

using System.Web;
using System.Xml;
using Rain.BLL;
using Rain.Common;

namespace Rain.API.Payment.alipaypc
{
  public class Config
  {
    private static string partner = "";
    private static string key = "";
    private static string email = "";
    private static string type = "1";
    private static string return_url = "";
    private static string notify_url = "";
    private static string input_charset = "";
    private static string sign_type = "";

    static Config()
    {
      string mapPath = Utils.GetMapPath("~/xmlconfig/alipaypc.config");
      XmlDocument xmlDocument = new XmlDocument();
      xmlDocument.Load(mapPath);
      XmlNode xmlNode1 = xmlDocument.SelectSingleNode("Root/partner");
      XmlNode xmlNode2 = xmlDocument.SelectSingleNode("Root/key");
      XmlNode xmlNode3 = xmlDocument.SelectSingleNode("Root/email");
      XmlNode xmlNode4 = xmlDocument.SelectSingleNode("Root/type");
      XmlNode xmlNode5 = xmlDocument.SelectSingleNode("Root/return_url");
      XmlNode xmlNode6 = xmlDocument.SelectSingleNode("Root/notify_url");
      new siteconfig().loadConfig();
      Config.partner = xmlNode1.InnerText;
      Config.key = xmlNode2.InnerText;
      Config.email = xmlNode3.InnerText;
      Config.type = xmlNode4.InnerText;
      Config.return_url = "http://" + HttpContext.Current.Request.Url.Authority.ToLower() + xmlNode5.InnerText;
      Config.notify_url = "http://" + HttpContext.Current.Request.Url.Authority.ToLower() + xmlNode6.InnerText;
      Config.input_charset = "utf-8";
      Config.sign_type = "MD5";
    }

    public static string Partner
    {
      get
      {
        return Config.partner;
      }
      set
      {
        Config.partner = value;
      }
    }

    public static string Key
    {
      get
      {
        return Config.key;
      }
      set
      {
        Config.key = value;
      }
    }

    public static string Type
    {
      get
      {
        return Config.type;
      }
      set
      {
        Config.type = value;
      }
    }

    public static string Email
    {
      get
      {
        return Config.email;
      }
      set
      {
        Config.email = value;
      }
    }

    public static string Return_url
    {
      get
      {
        return Config.return_url;
      }
    }

    public static string Notify_url
    {
      get
      {
        return Config.notify_url;
      }
    }

    public static string Input_charset
    {
      get
      {
        return Config.input_charset;
      }
    }

    public static string Sign_type
    {
      get
      {
        return Config.sign_type;
      }
    }
  }
}
