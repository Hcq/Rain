// Decompiled with JetBrains decompiler
// Type: Rain.Web.api.payment.tenpaypc.notify_url
// Assembly: Rain.Web, Version=4.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 7C74D3EC-D223-4A13-9304-5BE7CB5803E5
// Assembly location: C:\Users\Felix.Ho\Desktop\page\bin\Rain.Web.dll

using System;
using System.Web.UI;
using Rain.API.Payment.tenpaypc;

namespace Rain.Web.api.payment.tenpaypc
{
  public class notify_url : Page
  {
    protected void Page_Load(object sender, EventArgs e)
    {
      TenpayUtil tenpayUtil = new TenpayUtil();
      ResponseHandler responseHandler = new ResponseHandler(this.Context);
      responseHandler.setKey(tenpayUtil.key);
      if (responseHandler.isTenpaySign())
      {
        string parameter1 = responseHandler.getParameter("notify_id");
        RequestHandler requestHandler = new RequestHandler(this.Context);
        requestHandler.init();
        requestHandler.setKey(tenpayUtil.key);
        requestHandler.setGateUrl("https://gw.tenpay.com/gateway/simpleverifynotifyid.xml");
        requestHandler.setParameter("partner", tenpayUtil.partner);
        requestHandler.setParameter("notify_id", parameter1);
        TenpayHttpClient tenpayHttpClient = new TenpayHttpClient();
        tenpayHttpClient.setTimeOut(5);
        tenpayHttpClient.setReqContent(requestHandler.getRequestURL());
        if (tenpayHttpClient.call())
        {
          ClientResponseHandler clientResponseHandler = new ClientResponseHandler();
          clientResponseHandler.setContent(tenpayHttpClient.getResContent());
          clientResponseHandler.setKey(tenpayUtil.key);
          if (clientResponseHandler.isTenpaySign())
          {
            string upper = responseHandler.getParameter("out_trade_no").ToUpper();
            string parameter2 = responseHandler.getParameter("transaction_id");
            string parameter3 = responseHandler.getParameter("total_fee");
            responseHandler.getParameter("discount");
            string parameter4 = responseHandler.getParameter("trade_state");
            string parameter5 = responseHandler.getParameter("trade_mode");
            if ("0".Equals(clientResponseHandler.getParameter("retcode")))
            {
              if ("1".Equals(parameter5))
              {
                if ("0".Equals(parameter4))
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
                    if (model.amount != Decimal.Parse(parameter3) / new Decimal(100))
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
                    if (model.order_amount != Decimal.Parse(parameter3) / new Decimal(100))
                    {
                      this.Response.Write("订单金额和支付金额不相符");
                      return;
                    }
                    if (!orders.UpdateField(upper, "trade_no='" + parameter2 + "',status=2,payment_status=2,payment_time='" + (object) DateTime.Now + "'"))
                    {
                      this.Response.Write("修改订单状态失败");
                      return;
                    }
                    if (model.point < 0)
                      new Rain.BLL.user_point_log().Add(model.user_id, model.user_name, model.point, "换购扣除积分，订单号：" + model.order_no, false);
                  }
                  this.Response.Write("success");
                }
                else
                  this.Response.Write("即时到账支付失败");
              }
              else if ("2".Equals(parameter5))
              {
                if ("0".Equals(parameter4))
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
                    if (model.amount != Decimal.Parse(parameter3) / new Decimal(100))
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
                    if (model.order_amount != Decimal.Parse(parameter3) / new Decimal(100))
                    {
                      this.Response.Write("订单金额和支付金额不相符");
                      return;
                    }
                    if (!orders.UpdateField(upper, "trade_no='" + parameter2 + "',status=2,payment_status=2,payment_time='" + (object) DateTime.Now + "'"))
                    {
                      this.Response.Write("修改订单状态失败");
                      return;
                    }
                    if (model.point < 0)
                      new Rain.BLL.user_point_log().Add(model.user_id, model.user_name, model.point, "换购扣除积分，订单号：" + model.order_no, false);
                  }
                }
                else if ("5".Equals(parameter4) && upper.StartsWith("B"))
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
                  if (model.order_amount != Decimal.Parse(parameter3))
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
            }
            else
              this.Response.Write("查询验证签名失败或id验证失败");
          }
          else
            this.Response.Write("通知ID查询签名验证失败");
        }
        else
          this.Response.Write("后台调用通信失败");
      }
      else
        this.Response.Write("签名验证失败");
      this.Response.End();
    }
  }
}
