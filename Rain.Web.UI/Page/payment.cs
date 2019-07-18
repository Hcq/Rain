// Decompiled with JetBrains decompiler
// Type: Rain.Web.UI.Page.payment
// Assembly: Rain.Web.UI, Version=4.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 3E78344A-857F-445F-804E-AF1B978FD5C7
// Assembly location: C:\Users\Felix.Ho\Desktop\page\bin\Rain.Web.UI.dll

using System;
using System.Web;
using Rain.Common;

namespace Rain.Web.UI.Page
{
  public class payment : BasePage
  {
    protected string action = string.Empty;
    protected string order_no = string.Empty;
    protected string order_type = string.Empty;
    protected Decimal order_amount = new Decimal(0);
    protected Rain.Model.orderconfig orderConfig = new Rain.BLL.orderconfig().loadConfig();
    protected Rain.Model.users userModel;
    protected Rain.Model.orders orderModel;
    protected Rain.Model.user_recharge rechargeModel;
    protected Rain.Model.payment payModel;

    protected override void ShowPage()
    {
      this.Init += new EventHandler(this.payment_Init);
    }

    protected void payment_Init(object sender, EventArgs e)
    {
      this.action = DTRequest.GetString("action");
      this.order_no = DTRequest.GetString("order_no");
      if (this.order_no.ToUpper().StartsWith("R"))
        this.order_type = DTEnums.AmountTypeEnum.Recharge.ToString().ToLower();
      else if (this.order_no.ToUpper().StartsWith("B"))
        this.order_type = DTEnums.AmountTypeEnum.BuyGoods.ToString().ToLower();
      switch (this.action)
      {
        case "confirm":
          if (string.IsNullOrEmpty(this.action) || string.IsNullOrEmpty(this.order_no))
          {
            HttpContext.Current.Response.Redirect(this.linkurl("error", (object) ("?msg=" + Utils.UrlEncode("出错啦，URL传输参数有误！"))));
            break;
          }
          this.userModel = new BasePage().GetUserInfo();
          if (this.orderConfig.anonymous == 0 || this.order_no.ToUpper().StartsWith("R"))
          {
            if (this.userModel == null)
            {
              HttpContext.Current.Response.Redirect(this.linkurl(nameof (payment), (object) "?action=login"));
              break;
            }
          }
          else if (this.userModel == null)
            this.userModel = new Rain.Model.users();
          if (this.order_no.ToUpper().StartsWith("R"))
          {
            this.rechargeModel = new Rain.BLL.user_recharge().GetModel(this.order_no);
            if (this.rechargeModel == null)
            {
              HttpContext.Current.Response.Redirect(this.linkurl("error", (object) ("?msg=" + Utils.UrlEncode("出错啦，订单号不存在或已删除！"))));
              break;
            }
            if (this.rechargeModel.status == 1)
            {
              HttpContext.Current.Response.Redirect(this.linkurl(nameof (payment), (object) ("?action=succeed&order_no=" + this.rechargeModel.recharge_no)));
              break;
            }
            this.payModel = new Rain.BLL.payment().GetModel(this.rechargeModel.payment_id);
            if (this.payModel == null)
            {
              HttpContext.Current.Response.Redirect(this.linkurl("error", (object) ("?msg=" + Utils.UrlEncode("出错啦，支付方式不存在或已删除！"))));
              break;
            }
            if (this.payModel.type == 2)
            {
              HttpContext.Current.Response.Redirect(this.linkurl("error", (object) ("?msg=" + Utils.UrlEncode("出错啦，账户充值不允许线下支付！"))));
              break;
            }
            this.order_amount = this.rechargeModel.amount;
            break;
          }
          if (this.order_no.ToUpper().StartsWith("B"))
          {
            this.orderModel = new Rain.BLL.orders().GetModel(this.order_no);
            if (this.orderModel == null)
            {
              HttpContext.Current.Response.Redirect(this.linkurl("error", (object) ("?msg=" + Utils.UrlEncode("出错啦，订单号不存在或已删除！"))));
              break;
            }
            if (this.orderModel.payment_status == 2)
            {
              HttpContext.Current.Response.Redirect(this.linkurl(nameof (payment), (object) ("?action=succeed&order_no=" + this.orderModel.order_no)));
              break;
            }
            this.payModel = new Rain.BLL.payment().GetModel(this.orderModel.payment_id);
            if (this.payModel == null)
            {
              HttpContext.Current.Response.Redirect(this.linkurl("error", (object) ("?msg=" + Utils.UrlEncode("出错啦，支付方式不存在或已删除！"))));
              break;
            }
            if (this.orderModel.payment_status == 0)
            {
              HttpContext.Current.Response.Redirect(this.linkurl(nameof (payment), (object) ("?action=succeed&order_no=" + this.orderModel.order_no)));
              break;
            }
            if (this.orderModel.order_amount == new Decimal(0))
            {
              if (!new Rain.BLL.orders().UpdateField(this.orderModel.order_no, "status=2,payment_status=2,payment_time='" + (object) DateTime.Now + "'"))
              {
                HttpContext.Current.Response.Redirect(this.linkurl(nameof (payment), (object) "?action=error"));
                break;
              }
              HttpContext.Current.Response.Redirect(this.linkurl(nameof (payment), (object) ("?action=succeed&order_no=" + this.orderModel.order_no)));
              break;
            }
            this.order_amount = this.orderModel.order_amount;
            break;
          }
          HttpContext.Current.Response.Redirect(this.linkurl("error", (object) ("?msg=" + Utils.UrlEncode("出错啦，找不到您要提交的订单类型！"))));
          break;
        case "succeed":
          if (this.order_no.ToUpper().StartsWith("R"))
          {
            this.rechargeModel = new Rain.BLL.user_recharge().GetModel(this.order_no);
            if (this.rechargeModel != null)
              break;
            HttpContext.Current.Response.Redirect(this.linkurl("error", (object) ("?msg=" + Utils.UrlEncode("出错啦，订单号不存在或已删除！"))));
            break;
          }
          if (this.order_no.ToUpper().StartsWith("B"))
          {
            this.orderModel = new Rain.BLL.orders().GetModel(this.order_no);
            if (this.orderModel != null)
              break;
            HttpContext.Current.Response.Redirect(this.linkurl("error", (object) ("?msg=" + Utils.UrlEncode("出错啦，订单号不存在或已删除！"))));
            break;
          }
          HttpContext.Current.Response.Redirect(this.linkurl("error", (object) ("?msg=" + Utils.UrlEncode("出错啦，找不到您要提交的订单类型！"))));
          break;
      }
    }
  }
}
