// Decompiled with JetBrains decompiler
// Type: Rain.Web.api.payment.alipaypc.index
// Assembly: Rain.Web, Version=4.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 7C74D3EC-D223-4A13-9304-5BE7CB5803E5
// Assembly location: C:\Users\Felix.Ho\Desktop\page\bin\Rain.Web.dll

using System;
using System.Collections.Generic;
using System.Web.UI;
using Rain.API.Payment.alipaypc;
using Rain.Common;
using Rain.Web.UI;

namespace Rain.Web.api.payment.alipaypc
{
  public class index : Page
  {
    protected void Page_Load(object sender, EventArgs e)
    {
      Rain.Model.siteconfig siteconfig = new Rain.BLL.siteconfig().loadConfig();
      string upper = DTRequest.GetFormString("pay_order_no").ToUpper();
      Decimal formDecimal = DTRequest.GetFormDecimal("pay_order_amount", new Decimal(0));
      string formString1 = DTRequest.GetFormString("pay_user_name");
      string formString2 = DTRequest.GetFormString("pay_subject");
      string empty1 = string.Empty;
      string empty2 = string.Empty;
      string str1 = string.Empty;
      string empty3 = string.Empty;
      string empty4 = string.Empty;
      if (string.IsNullOrEmpty(upper) || formDecimal == new Decimal(0))
      {
        this.Response.Redirect(new BasePage().linkurl("error", (object) ("?msg=" + Utils.UrlEncode("对不起，您提交的参数有误！"))));
      }
      else
      {
        string str2;
        string address;
        string telphone;
        string mobile;
        if (upper.StartsWith("R"))
        {
          Rain.Model.user_recharge model1 = new Rain.BLL.user_recharge().GetModel(upper);
          if (model1 == null)
          {
            this.Response.Redirect(new BasePage().linkurl("error", (object) ("?msg=" + Utils.UrlEncode("对不起，您充值的订单号不存在或已删除！"))));
            return;
          }
          if (model1.amount != formDecimal)
          {
            this.Response.Redirect(new BasePage().linkurl("error", (object) ("?msg=" + Utils.UrlEncode("对不起，您充值的订单金额与实际金额不一致！"))));
            return;
          }
          Rain.Model.users model2 = new Rain.BLL.users().GetModel(model1.user_id);
          if (model2 == null)
          {
            this.Response.Redirect(new BasePage().linkurl("error", (object) ("?msg=" + Utils.UrlEncode("对不起，用户账户不存在或已删除！"))));
            return;
          }
          str2 = model2.nick_name;
          address = model2.address;
          telphone = model2.telphone;
          mobile = model2.mobile;
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
          str2 = model.accept_name;
          address = model.address;
          str1 = model.post_code;
          telphone = model.telphone;
          mobile = model.mobile;
        }
        string str3 = !(formString1 != "") ? "匿名用户" : "支付会员：" + formString1;
        if (Config.Type == "1")
          this.Response.Write(new Service().Create_direct_pay_by_user(new SortedDictionary<string, string>()
          {
            {
              "payment_type",
              "1"
            },
            {
              "show_url",
              siteconfig.weburl
            },
            {
              "out_trade_no",
              upper
            },
            {
              "subject",
              siteconfig.webname + "-" + formString2
            },
            {
              "body",
              str3
            },
            {
              "total_fee",
              formDecimal.ToString()
            },
            {
              "paymethod",
              ""
            },
            {
              "defaultbank",
              ""
            },
            {
              "anti_phishing_key",
              ""
            },
            {
              "exter_invoke_ip",
              DTRequest.GetIP()
            },
            {
              "buyer_email",
              ""
            },
            {
              "royalty_type",
              ""
            },
            {
              "royalty_parameters",
              ""
            }
          }));
        else
          this.Response.Write(new Service().Create_partner_trade_by_buyer(new SortedDictionary<string, string>()
          {
            {
              "payment_type",
              "1"
            },
            {
              "out_trade_no",
              upper
            },
            {
              "subject",
              siteconfig.webname + "-" + formString2
            },
            {
              "price",
              formDecimal.ToString()
            },
            {
              "quantity",
              "1"
            },
            {
              "logistics_fee",
              "0.00"
            },
            {
              "logistics_type",
              "EXPRESS"
            },
            {
              "logistics_payment",
              "SELLER_PAY"
            },
            {
              "body",
              str3
            },
            {
              "show_url",
              siteconfig.weburl
            },
            {
              "receive_name",
              str2
            },
            {
              "receive_address",
              address
            },
            {
              "receive_zip",
              str1
            },
            {
              "receive_phone",
              telphone
            },
            {
              "receive_mobile",
              mobile
            }
          }));
      }
    }
  }
}
