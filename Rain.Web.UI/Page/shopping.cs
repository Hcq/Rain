// Decompiled with JetBrains decompiler
// Type: Rain.Web.UI.Page.shopping
// Assembly: Rain.Web.UI, Version=4.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 3E78344A-857F-445F-804E-AF1B978FD5C7
// Assembly location: C:\Users\Felix.Ho\Desktop\page\bin\Rain.Web.UI.dll

using System;
using System.Collections.Generic;
using System.Web;
using Rain.Common;
using Rain.Model;

namespace Rain.Web.UI.Page
{
  public class shopping : BasePage
  {
    protected string goodsJsonValue = string.Empty;
    protected List<cart_items> goodsList = new List<cart_items>();
    protected cart_total goodsTotal = new cart_total();
    protected Rain.Model.orderconfig orderConfig = new Rain.BLL.orderconfig().loadConfig();
    protected Rain.Model.users userModel;

    protected override void ShowPage()
    {
      this.goodsJsonValue = Utils.GetCookie("dt_cookie_shopping_buy");
      this.Init += new EventHandler(this.shopping_Init);
    }

    protected void shopping_Init(object sender, EventArgs e)
    {
      int group_id = 0;
      this.userModel = this.GetUserInfo();
      if (this.userModel == null)
      {
        if (this.orderConfig.anonymous == 0)
          HttpContext.Current.Response.Redirect(this.linkurl("login"));
      }
      else
        group_id = this.userModel.group_id;
      if (string.IsNullOrEmpty(this.goodsJsonValue))
      {
        HttpContext.Current.Response.Redirect(this.linkurl("error", (object) ("?msg=" + Utils.UrlEncode("对不起，无法获取您要购买的商品！"))));
      }
      else
      {
        try
        {
          this.goodsList = ShopCart.ToList(JsonHelper.JSONToObject<List<cart_keys>>(this.goodsJsonValue), group_id);
          this.goodsTotal = ShopCart.GetTotal(this.goodsList);
        }
        catch
        {
          HttpContext.Current.Response.Redirect(this.linkurl("error", (object) ("?msg=" + Utils.UrlEncode("对不起，商品的传输参数有误！"))));
        }
      }
    }
  }
}
