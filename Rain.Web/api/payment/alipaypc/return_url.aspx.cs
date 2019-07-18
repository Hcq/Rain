// Decompiled with JetBrains decompiler
// Type: Rain.Web.api.payment.alipaypc.return_url
// Assembly: Rain.Web, Version=4.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 7C74D3EC-D223-4A13-9304-5BE7CB5803E5
// Assembly location: C:\Users\Felix.Ho\Desktop\page\bin\Rain.Web.dll

using System;
using System.Collections.Generic;
using System.Web.UI;
using Rain.API.Payment.alipaypc;
using Rain.BLL;
using Rain.Common;
using Rain.Web.UI;

namespace Rain.Web.api.payment.alipaypc
{
  public class return_url : Page
  {
    protected void Page_Load(object sender, EventArgs e)
    {
      new siteconfig().loadConfig();
      SortedDictionary<string, string> requestGet = this.GetRequestGet();
      if (requestGet.Count > 0 && new Notify().Verify(requestGet, DTRequest.GetString("notify_id"), DTRequest.GetString("sign")))
      {
        DTRequest.GetString("trade_no");
        string str1 = DTRequest.GetString("out_trade_no");
        string str2 = DTRequest.GetString("trade_status");
        if (str2 == "WAIT_SELLER_SEND_GOODS" || str2 == "TRADE_FINISHED" || str2 == "TRADE_SUCCESS")
        {
          this.Response.Redirect(new BasePage().linkurl("payment", (object) ("?action=succeed&order_no=" + str1)));
          return;
        }
      }
      this.Response.Redirect(new BasePage().linkurl("payment", (object) "?action=error"));
    }

    public SortedDictionary<string, string> GetRequestGet()
    {
      SortedDictionary<string, string> sortedDictionary = new SortedDictionary<string, string>();
      string[] allKeys = this.Request.QueryString.AllKeys;
      for (int index = 0; index < allKeys.Length; ++index)
        sortedDictionary.Add(allKeys[index], this.Request.QueryString[allKeys[index]]);
      return sortedDictionary;
    }
  }
}
