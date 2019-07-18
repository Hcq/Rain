// Decompiled with JetBrains decompiler
// Type: Rain.Web.UI.Page.cart
// Assembly: Rain.Web.UI, Version=4.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 3E78344A-857F-445F-804E-AF1B978FD5C7
// Assembly location: C:\Users\Felix.Ho\Desktop\page\bin\Rain.Web.UI.dll

using System;
using System.Collections.Generic;
using Rain.Model;

namespace Rain.Web.UI.Page
{
  public class cart : BasePage
  {
    protected List<cart_items> goodsList = new List<cart_items>();
    protected cart_total goodsTotal = new cart_total();

    protected override void ShowPage()
    {
      this.Init += new EventHandler(this.cart_Init);
    }

    protected void cart_Init(object sender, EventArgs e)
    {
      int group_id = 0;
      users userInfo = this.GetUserInfo();
      if (userInfo != null)
        group_id = userInfo.group_id;
      this.goodsList = ShopCart.GetList(group_id);
      if (this.goodsList != null)
      {
        this.goodsTotal = ShopCart.GetTotal(this.goodsList);
      }
      else
      {
        this.goodsList = new List<cart_items>();
        this.goodsTotal = new cart_total();
      }
    }
  }
}
