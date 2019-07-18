// Decompiled with JetBrains decompiler
// Type: Rain.Web.api.payment.tenpaypc.return_url
// Assembly: Rain.Web, Version=4.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 7C74D3EC-D223-4A13-9304-5BE7CB5803E5
// Assembly location: C:\Users\Felix.Ho\Desktop\page\bin\Rain.Web.dll

using System;
using System.Web.UI;
using Rain.API.Payment.tenpaypc;
using Rain.Web.UI;

namespace Rain.Web.api.payment.tenpaypc
{
  public class return_url : Page
  {
    protected void Page_Load(object sender, EventArgs e)
    {
      TenpayUtil tenpayUtil = new TenpayUtil();
      ResponseHandler responseHandler = new ResponseHandler(this.Context);
      responseHandler.setKey(tenpayUtil.key);
      if (responseHandler.isTenpaySign())
      {
        responseHandler.getParameter("notify_id");
        string upper = responseHandler.getParameter("out_trade_no").ToUpper();
        responseHandler.getParameter("transaction_id");
        responseHandler.getParameter("total_fee");
        responseHandler.getParameter("discount");
        string parameter = responseHandler.getParameter("trade_state");
        responseHandler.getParameter("trade_mode");
        if ("0".Equals(parameter))
          this.Response.Redirect(new BasePage().linkurl("payment", (object) ("?action=succeed&order_no=" + upper)));
        else
          this.Response.Redirect(new BasePage().linkurl("payment", (object) "?action=error"));
      }
      else
        this.Response.Redirect(new BasePage().linkurl("payment", (object) "?action=error"));
    }
  }
}
