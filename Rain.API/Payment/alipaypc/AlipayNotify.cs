// Decompiled with JetBrains decompiler
// Type: Rain.API.Payment.alipaypc.Notify
// Assembly: Rain.API, Version=4.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 114351D3-1A2F-4CC4-A6A6-43C59DB5A56B
// Assembly location: C:\Users\Felix.Ho\Desktop\page\bin\Rain.API.dll

using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;

namespace Rain.API.Payment.alipaypc
{
  public class Notify
  {
    private string _partner = string.Empty;
    private string _key = string.Empty;
    private string _input_charset = string.Empty;
    private string _sign_type = string.Empty;
    private string Https_veryfy_url = "https://mapi.alipay.com/gateway.do?service=notify_verify&";

    public Notify()
    {
      Config config = new Config();
      this._partner = Config.Partner.Trim();
      this._key = Config.Key.Trim().ToLower();
      this._input_charset = Config.Input_charset.Trim().ToLower();
      this._sign_type = Config.Sign_type.Trim().ToUpper();
    }

    public bool Verify(SortedDictionary<string, string> inputPara, string notify_id, string sign)
    {
      string responseMysign = this.GetResponseMysign(inputPara);
      string str = "true";
      if (notify_id != "")
        str = this.GetResponseTxt(notify_id);
      return str == "true" && sign == responseMysign;
    }

    private string GetPreSignStr(SortedDictionary<string, string> inputPara)
    {
      Dictionary<string, string> dictionary = new Dictionary<string, string>();
      return Core.CreateLinkString(Core.FilterPara(inputPara));
    }

    private string GetResponseMysign(SortedDictionary<string, string> inputPara)
    {
      Dictionary<string, string> dictionary = new Dictionary<string, string>();
      return Core.BuildMysign(Core.FilterPara(inputPara), this._key, this._sign_type, this._input_charset);
    }

    private string GetResponseTxt(string notify_id)
    {
      return this.Get_Http(this.Https_veryfy_url + "partner=" + this._partner + "&notify_id=" + notify_id, 120000);
    }

    private string Get_Http(string strUrl, int timeout)
    {
      string str;
      try
      {
        HttpWebRequest httpWebRequest = (HttpWebRequest) WebRequest.Create(strUrl);
        httpWebRequest.Timeout = timeout;
        StreamReader streamReader = new StreamReader(httpWebRequest.GetResponse().GetResponseStream(), Encoding.Default);
        StringBuilder stringBuilder = new StringBuilder();
        while (-1 != streamReader.Peek())
          stringBuilder.Append(streamReader.ReadLine());
        str = stringBuilder.ToString();
      }
      catch (Exception ex)
      {
        str = "错误：" + ex.Message;
      }
      return str;
    }
  }
}
