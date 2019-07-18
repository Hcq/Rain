// Decompiled with JetBrains decompiler
// Type: Rain.Web.api.payment.balance.index
// Assembly: Rain.Web, Version=4.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 7C74D3EC-D223-4A13-9304-5BE7CB5803E5
// Assembly location: C:\Users\Felix.Ho\Desktop\page\bin\Rain.Web.dll

using System;
using System.Web.UI;
using Rain.Common;
using Rain.Web.UI;

namespace Rain.Web.api.payment.balance
{
  public class index : Page
  {
    protected void Page_Load(object sender, EventArgs e)
    {
      new Rain.BLL.siteconfig().loadConfig();
      string upper = DTRequest.GetFormString("pay_order_no").ToUpper();
      Rain.Model.orders model1 = new Rain.BLL.orders().GetModel(upper);
      if (model1 == null)
      {
        this.Response.Redirect(new BasePage().linkurl("error", (object) ("?msg=" + Utils.UrlEncode("对不起，订单详情获取出错,请重试！"))));
      }
      else
      {
        Decimal orderAmount = model1.order_amount;
        string formString = DTRequest.GetFormString("pay_subject");
        if (upper == "" || orderAmount == new Decimal(0))
        {
          this.Response.Redirect(new BasePage().linkurl("error", (object) ("?msg=" + Utils.UrlEncode("对不起，您提交的参数有误！"))));
        }
        else
        {
          Rain.Model.users userInfo = new BasePage().GetUserInfo();
          if (userInfo == null)
            this.Response.Redirect(new BasePage().linkurl("payment", (object) "?action=login"));
          else if (userInfo.amount < orderAmount)
            this.Response.Redirect(new BasePage().linkurl("payment", (object) "?action=recharge"));
          else if (upper.StartsWith("B"))
          {
            Rain.BLL.orders orders = new Rain.BLL.orders();
            Rain.Model.orders model2 = orders.GetModel(upper);
            if (model2 == null)
            {
              this.Response.Redirect(new BasePage().linkurl("error", (object) ("?msg=" + Utils.UrlEncode("对不起，商品订单号不存在！"))));
            }
            else
            {
              if (model2.payment_status == 1)
              {
                if (new Rain.BLL.user_amount_log().Add(userInfo.id, userInfo.user_name, new Decimal(-1) * orderAmount, formString) > 0)
                {
                  if (!orders.UpdateField(upper, "status=2,payment_status=2,payment_time='" + (object) DateTime.Now + "'"))
                  {
                    this.Response.Redirect(new BasePage().linkurl("payment", (object) "?action=error"));
                    return;
                  }
                  if (model2.point < 0)
                    new Rain.BLL.user_point_log().Add(model2.user_id, model2.user_name, model2.point, "换购扣除积分，订单号：" + model2.order_no, false);
                }
                else
                {
                  this.Response.Redirect(new BasePage().linkurl("payment", (object) "?action=error"));
                  return;
                }
              }
              this.Response.Redirect(new BasePage().linkurl("payment", (object) ("?action=succeed&order_no=" + upper)));
            }
          }
          else
            this.Response.Redirect(new BasePage().linkurl("error", (object) ("?msg=" + Utils.UrlEncode("对不起，找不到需要支付的订单类型！"))));
        }
      }
    }
  }
}
