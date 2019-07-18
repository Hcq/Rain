// Decompiled with JetBrains decompiler
// Type: Rain.Web.api.payment.alipaypc.notify_url
// Assembly: Rain.Web, Version=4.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 7C74D3EC-D223-4A13-9304-5BE7CB5803E5
// Assembly location: C:\Users\Felix.Ho\Desktop\page\bin\Rain.Web.dll

using System;
using System.Collections.Generic;
using System.Web.UI;
using Rain.API.Payment.alipaypc;
using Rain.Common;

namespace Rain.Web.api.payment.alipaypc
{
  public class notify_url : Page
  {
    protected void Page_Load(object sender, EventArgs e)
    {
      SortedDictionary<string, string> requestPost = this.GetRequestPost();
      if (requestPost.Count > 0)
      {
        if (new Notify().Verify(requestPost, DTRequest.GetString("notify_id"), DTRequest.GetString("sign")))
        {
          string trade_no = DTRequest.GetString("trade_no");
          string upper = DTRequest.GetString("out_trade_no").ToUpper();
          string s = DTRequest.GetString("total_fee");
          string str = DTRequest.GetString("trade_status");
          if (Config.Type == "1")
          {
            if (str == "TRADE_FINISHED" || str == "TRADE_SUCCESS")
            {
              if (upper.StartsWith("R"))
              {
                Rain.BLL.user_recharge userRecharge = new Rain.BLL.user_recharge();
                Rain.Model.user_recharge model = userRecharge.GetModel(upper);
                if (model == null)
                {
                  this.Response.Write("该订单号不存在");
                  return;
                }
                if (model.status == 1)
                {
                  this.Response.Write("success");
                  return;
                }
                if (model.amount != Decimal.Parse(s))
                {
                  this.Response.Write("订单金额和支付金额不相符");
                  return;
                }
                if (!userRecharge.Confirm(upper))
                {
                  this.Response.Write("修改订单状态失败");
                  return;
                }
              }
              else if (upper.StartsWith("B"))
              {
                Rain.BLL.orders orders = new Rain.BLL.orders();
                Rain.Model.orders model = orders.GetModel(upper);
                if (model == null)
                {
                  this.Response.Write("该订单号不存在");
                  return;
                }
                if (model.payment_status == 2)
                {
                  this.Response.Write("success");
                  return;
                }
                if (model.order_amount != Decimal.Parse(s))
                {
                  this.Response.Write("订单金额和支付金额不相符");
                  return;
                }
                if (!orders.UpdateField(upper, "trade_no='" + trade_no + "',status=2,payment_status=2,payment_time='" + (object) DateTime.Now + "'"))
                {
                  this.Response.Write("修改订单状态失败");
                  return;
                }
                if (model.point < 0)
                  new Rain.BLL.user_point_log().Add(model.user_id, model.user_name, model.point, "换购扣除积分，订单号：" + model.order_no, false);
              }
            }
          }
          else if (str == "WAIT_SELLER_SEND_GOODS")
          {
            if (upper.StartsWith("R"))
            {
              Rain.BLL.user_recharge userRecharge = new Rain.BLL.user_recharge();
              Rain.Model.user_recharge model = userRecharge.GetModel(upper);
              if (model == null)
              {
                this.Response.Write("该订单号不存在");
                return;
              }
              if (model.status == 1)
              {
                this.Response.Write("success");
                return;
              }
              if (model.amount != Decimal.Parse(s))
              {
                this.Response.Write("订单金额和支付金额不相符");
                return;
              }
              if (!userRecharge.Confirm(upper))
              {
                this.Response.Write("修改订单状态失败");
                return;
              }
              if (!new Service().Send_goods_confirm_by_platform(trade_no, "EXPRESS", "", "DIRECT"))
              {
                this.Response.Write("自动发货失败");
                return;
              }
            }
            else if (upper.StartsWith("B"))
            {
              Rain.BLL.orders orders = new Rain.BLL.orders();
              Rain.Model.orders model = orders.GetModel(upper);
              if (model == null)
              {
                this.Response.Write("该订单号不存在");
                return;
              }
              if (model.payment_status == 2)
              {
                this.Response.Write("success");
                return;
              }
              if (model.order_amount != Decimal.Parse(s))
              {
                this.Response.Write("订单金额和支付金额不相符");
                return;
              }
              if (!orders.UpdateField(upper, "trade_no='" + trade_no + "',status=2,payment_status=2,payment_time='" + (object) DateTime.Now + "'"))
              {
                this.Response.Write("修改订单状态失败");
                return;
              }
              if (model.point < 0)
                new Rain.BLL.user_point_log().Add(model.user_id, model.user_name, model.point, "换购扣除积分，订单号：" + model.order_no, false);
            }
          }
          else if (str == "TRADE_FINISHED" && upper.StartsWith("B"))
          {
            Rain.BLL.orders orders = new Rain.BLL.orders();
            Rain.Model.orders model = orders.GetModel(upper);
            if (model == null)
            {
              this.Response.Write("该订单号不存在");
              return;
            }
            if (model.status > 2)
            {
              this.Response.Write("success");
              return;
            }
            if (model.order_amount != Decimal.Parse(s))
            {
              this.Response.Write("订单金额和支付金额不相符");
              return;
            }
            if (!orders.UpdateField(upper, "status=3,complete_time='" + (object) DateTime.Now + "'"))
            {
              this.Response.Write("修改订单状态失败");
              return;
            }
            if (model.user_id > 0 && model.point > 0)
              new Rain.BLL.user_point_log().Add(model.user_id, model.user_name, model.point, "购物获得积分，订单号：" + model.order_no, true);
          }
          this.Response.Write("success");
        }
        else
          this.Response.Write("fail");
      }
      else
        this.Response.Write("无通知参数");
    }

    public SortedDictionary<string, string> GetRequestPost()
    {
      SortedDictionary<string, string> sortedDictionary = new SortedDictionary<string, string>();
      string[] allKeys = this.Request.Form.AllKeys;
      for (int index = 0; index < allKeys.Length; ++index)
        sortedDictionary.Add(allKeys[index], this.Request.Form[allKeys[index]]);
      return sortedDictionary;
    }
  }
}
