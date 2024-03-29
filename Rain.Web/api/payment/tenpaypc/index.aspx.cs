﻿// Decompiled with JetBrains decompiler
// Type: Rain.Web.api.payment.tenpaypc.index
// Assembly: Rain.Web, Version=4.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 7C74D3EC-D223-4A13-9304-5BE7CB5803E5
// Assembly location: C:\Users\Felix.Ho\Desktop\page\bin\Rain.Web.dll

using System;
using System.Collections;
using System.Text;
using System.Web.UI;
using Rain.API.Payment.tenpaypc;
using Rain.Common;
using Rain.Web.UI;

namespace Rain.Web.api.payment.tenpaypc
{
  public class index : Page
  {
    protected void Page_Load(object sender, EventArgs e)
    {
      Rain.Model.siteconfig siteconfig = new Rain.BLL.siteconfig().loadConfig();
      TenpayUtil tenpayUtil = new TenpayUtil();
      string upper = DTRequest.GetFormString("pay_order_no").ToUpper();
      Decimal formDecimal = DTRequest.GetFormDecimal("pay_order_amount", new Decimal(0));
      string formString1 = DTRequest.GetFormString("pay_user_name");
      string formString2 = DTRequest.GetFormString("pay_subject");
      string str = string.Empty;
      if (upper == "" || formDecimal == new Decimal(0))
      {
        this.Response.Redirect(new BasePage().linkurl("error", (object) ("?msg=" + Utils.UrlEncode("对不起，您提交的参数有误！"))));
      }
      else
      {
        if (upper.StartsWith("R"))
        {
          Rain.Model.user_recharge model = new Rain.BLL.user_recharge().GetModel(upper);
          if (model == null)
          {
            this.Response.Redirect(new BasePage().linkurl("error", (object) ("?msg=" + Utils.UrlEncode("对不起，您充值的订单号不存在或已删除！"))));
            return;
          }
          if (model.amount != formDecimal)
          {
            this.Response.Redirect(new BasePage().linkurl("error", (object) ("?msg=" + Utils.UrlEncode("对不起，您充值的订单金额与实际金额不一致！"))));
            return;
          }
          str = "2";
        }
        else
        {
          Rain.Model.orders model = new Rain.BLL.orders().GetModel(upper);
          if (model == null)
          {
            this.Response.Redirect(new BasePage().linkurl("error", (object) ("?msg=" + Utils.UrlEncode("对不起，您支付的订单号不存在或已删除！"))));
            return;
          }
          if (model.order_amount != formDecimal)
          {
            this.Response.Redirect(new BasePage().linkurl("error", (object) ("?msg=" + Utils.UrlEncode("对不起，您支付的订单金额与实际金额不一致！"))));
            return;
          }
          str = "1";
        }
        string parameterValue = string.IsNullOrEmpty(formString1) ? "匿名用户" : "支付会员：" + formString1;
        RequestHandler requestHandler = new RequestHandler(this.Context);
        requestHandler.init();
        requestHandler.setKey(tenpayUtil.key);
        requestHandler.setGateUrl("https://gw.tenpay.com/gateway/pay.htm");
        requestHandler.setParameter("partner", tenpayUtil.partner);
        requestHandler.setParameter("out_trade_no", upper);
        requestHandler.setParameter("total_fee", (Convert.ToDouble(formDecimal) * 100.0).ToString());
        requestHandler.setParameter("return_url", tenpayUtil.return_url);
        requestHandler.setParameter("notify_url", tenpayUtil.notify_url);
        requestHandler.setParameter("body", parameterValue);
        requestHandler.setParameter("bank_type", "DEFAULT");
        requestHandler.setParameter("spbill_create_ip", this.Page.Request.UserHostAddress);
        requestHandler.setParameter("fee_type", "1");
        requestHandler.setParameter("subject", siteconfig.webname + "-" + formString2);
        requestHandler.setParameter("sign_type", "MD5");
        requestHandler.setParameter("service_version", "1.0");
        requestHandler.setParameter("input_charset", "UTF-8");
        requestHandler.setParameter("sign_key_index", "1");
        requestHandler.setParameter("product_fee", "0");
        requestHandler.setParameter("transport_fee", "0");
        requestHandler.setParameter("time_start", DateTime.Now.ToString("yyyyMMddHHmmss"));
        requestHandler.setParameter("time_expire", "");
        requestHandler.setParameter("buyer_id", "");
        requestHandler.setParameter("goods_tag", "");
        requestHandler.setParameter("trade_mode", tenpayUtil.type);
        requestHandler.setParameter("transport_desc", "");
        requestHandler.setParameter("trans_type", "1");
        requestHandler.setParameter("agentid", "");
        requestHandler.setParameter("agent_type", "");
        requestHandler.setParameter("seller_id", "");
        requestHandler.getRequestURL();
        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.Append("<form id='tenpaysubmit' name='tenpaysubmit' action='" + requestHandler.getGateUrl() + "' method='get'>");
        foreach (DictionaryEntry allParameter in requestHandler.getAllParameters())
          stringBuilder.Append("<input type=\"hidden\" name=\"" + allParameter.Key + "\" value=\"" + allParameter.Value + "\" >\n");
        stringBuilder.Append("<input type='submit' value='确认' style='display:none;'></form>");
        stringBuilder.Append("<script>document.forms['tenpaysubmit'].submit();</script>");
        this.Response.Write(stringBuilder.ToString());
      }
    }
  }
}
