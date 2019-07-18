// Decompiled with JetBrains decompiler
// Type: Rain.API.Payment.alipaypc.Service
// Assembly: Rain.API, Version=4.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 114351D3-1A2F-4CC4-A6A6-43C59DB5A56B
// Assembly location: C:\Users\Felix.Ho\Desktop\page\bin\Rain.API.dll

using System;
using System.Collections.Generic;
using System.Xml;

namespace Rain.API.Payment.alipaypc
{
  public class Service
  {
    private string _partner = string.Empty;
    private string _key = string.Empty;
    private string _type = "1";
    private string _input_charset = string.Empty;
    private string _email = string.Empty;
    private string _return_url = string.Empty;
    private string _notify_url = string.Empty;
    private string GATEWAY_NEW = "https://mapi.alipay.com/gateway.do?";

    public Service()
    {
      this._partner = Config.Partner.Trim();
      this._key = Config.Key.Trim();
      this._type = Config.Type.Trim();
      this._input_charset = Config.Input_charset.Trim().ToLower();
      this._email = Config.Email.Trim();
      this._return_url = Config.Return_url.Trim();
      this._notify_url = Config.Notify_url.Trim();
    }

    public string Create_direct_pay_by_user(SortedDictionary<string, string> sParaTemp)
    {
      sParaTemp.Add("service", "create_direct_pay_by_user");
      sParaTemp.Add("partner", this._partner);
      sParaTemp.Add("_input_charset", this._input_charset);
      sParaTemp.Add("seller_email", this._email);
      sParaTemp.Add("return_url", this._return_url);
      sParaTemp.Add("notify_url", this._notify_url);
      string strButtonValue = "确认";
      return Submit.BuildFormHtml(sParaTemp, this.GATEWAY_NEW, "get", strButtonValue);
    }

    public string Create_partner_trade_by_buyer(SortedDictionary<string, string> sParaTemp)
    {
      sParaTemp.Add("service", "create_partner_trade_by_buyer");
      sParaTemp.Add("partner", this._partner);
      sParaTemp.Add("_input_charset", this._input_charset);
      sParaTemp.Add("seller_email", this._email);
      sParaTemp.Add("return_url", this._return_url);
      sParaTemp.Add("notify_url", this._notify_url);
      string strButtonValue = "确认";
      return Submit.BuildFormHtml(sParaTemp, this.GATEWAY_NEW, "get", strButtonValue);
    }

    public bool Send_goods_confirm_by_platform(
      string trade_no,
      string logistics_name,
      string invoice_no,
      string transport_type)
    {
      XmlDocument xmlDocument = Submit.SendPostInfo(new SortedDictionary<string, string>()
      {
        {
          "service",
          "send_goods_confirm_by_platform"
        },
        {
          "partner",
          this._partner
        },
        {
          "_input_charset",
          this._input_charset
        },
        {
          nameof (trade_no),
          trade_no
        },
        {
          nameof (logistics_name),
          logistics_name
        },
        {
          nameof (invoice_no),
          invoice_no
        },
        {
          nameof (transport_type),
          transport_type
        }
      }, this.GATEWAY_NEW);
      try
      {
        string innerText = xmlDocument.SelectSingleNode("/alipay/is_success").InnerText;
        if (!string.IsNullOrEmpty(innerText) && innerText == "T")
          return true;
      }
      catch (Exception ex)
      {
        return false;
      }
      return false;
    }

    public string Query_timestamp()
    {
      XmlTextReader xmlTextReader = new XmlTextReader(this.GATEWAY_NEW + "service=query_timestamp&partner=" + Config.Partner);
      XmlDocument xmlDocument = new XmlDocument();
      xmlDocument.Load((XmlReader) xmlTextReader);
      return xmlDocument.SelectSingleNode("/alipay/response/timestamp/encrypt_key").InnerText;
    }

    public string AlipayInterface(SortedDictionary<string, string> sParaTemp)
    {
      return "";
    }
  }
}
