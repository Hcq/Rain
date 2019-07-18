// Decompiled with JetBrains decompiler
// Type: Rain.Web.UI.ShopCart
// Assembly: Rain.Web.UI, Version=4.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 3E78344A-857F-445F-804E-AF1B978FD5C7
// Assembly location: C:\Users\Felix.Ho\Desktop\page\bin\Rain.Web.UI.dll

using System;
using System.Collections.Generic;
using Rain.Common;
using Rain.Model;

namespace Rain.Web.UI
{
  public class ShopCart
  {
    public static List<cart_items> GetList(int group_id)
    {
      return ShopCart.ToList(ShopCart.GetCart(), group_id);
    }

    public static bool Add(int article_id, int goods_id, int quantity)
    {
      List<cart_keys> cartKeysList = ShopCart.GetCart();
      if (cartKeysList != null)
      {
        cart_keys cartKeys = cartKeysList.Find((Predicate<cart_keys>) (p => p.article_id == article_id));
        if (cartKeys != null)
        {
          int index = cartKeysList.FindIndex((Predicate<cart_keys>) (p => p.article_id == article_id));
          cartKeys.quantity += quantity;
          cartKeysList[index] = cartKeys;
          ShopCart.AddCookies(JsonHelper.ObjectToJSON((object) cartKeysList));
          return true;
        }
      }
      else
        cartKeysList = new List<cart_keys>();
      cartKeysList.Add(new cart_keys()
      {
        article_id = article_id,
        quantity = quantity
      });
      ShopCart.AddCookies(JsonHelper.ObjectToJSON((object) cartKeysList));
      return true;
    }

    public static cart_keys Update(int article_id, int quantity)
    {
      if (quantity < 1)
        return (cart_keys) null;
      List<cart_keys> cart = ShopCart.GetCart();
      if (cart != null)
      {
        cart_keys cartKeys = cart.Find((Predicate<cart_keys>) (p => p.article_id == article_id));
        if (cartKeys != null)
        {
          int index = cart.FindIndex((Predicate<cart_keys>) (p => p.article_id == article_id));
          cartKeys.quantity = quantity;
          cart[index] = cartKeys;
          ShopCart.AddCookies(JsonHelper.ObjectToJSON((object) cart));
          return cartKeys;
        }
      }
      return (cart_keys) null;
    }

    public static void Clear()
    {
      Utils.WriteCookie("dt_cookie_shopping_cart", "", -43200);
    }

    public static void Clear(int article_id, int goods_id)
    {
      if (article_id > 0)
      {
        List<cart_keys> cart = ShopCart.GetCart();
        if (cart == null)
          return;
        cart_keys cartKeys = cart.Find((Predicate<cart_keys>) (p => p.article_id == article_id));
        if (cartKeys != null)
        {
          cart.Remove(cartKeys);
          ShopCart.AddCookies(JsonHelper.ObjectToJSON((object) cart));
        }
      }
    }

    public static void Clear(List<cart_keys> ls)
    {
      if (ls == null)
        return;
      List<cart_keys> cart = ShopCart.GetCart();
      if (cart == null)
        return;
      using (List<cart_keys>.Enumerator enumerator = ls.GetEnumerator())
      {
        while (enumerator.MoveNext())
        {
          cart_keys modelt = enumerator.Current;
          cart_keys cartKeys = cart.Find((Predicate<cart_keys>) (p => p.article_id == modelt.article_id));
          if (cartKeys != null)
            cart.Remove(cartKeys);
        }
      }
      ShopCart.AddCookies(JsonHelper.ObjectToJSON((object) cart));
    }

    public static List<cart_items> ToList(List<cart_keys> ls, int group_id)
    {
      if (ls == null)
        return (List<cart_items>) null;
      List<cart_items> cartItemsList = new List<cart_items>();
      foreach (cart_keys l in ls)
      {
        Rain.Model.article model = new Rain.BLL.article().GetModel(l.article_id);
        if (model != null && model.fields.ContainsKey("sell_price"))
        {
          cart_items cartItems = new cart_items();
          cartItems.article_id = model.id;
          if (model.fields.ContainsKey("goods_no"))
            cartItems.goods_no = model.fields["goods_no"];
          cartItems.title = model.title;
          cartItems.img_url = model.img_url;
          cartItems.sell_price = Utils.StrToDecimal(model.fields["sell_price"], new Decimal(0));
          cartItems.user_price = Utils.StrToDecimal(model.fields["sell_price"], new Decimal(0));
          if (model.fields.ContainsKey("point"))
            cartItems.point = Utils.StrToInt(model.fields["point"], 0);
          if (model.fields.ContainsKey("stock_quantity"))
            cartItems.stock_quantity = Utils.StrToInt(model.fields["stock_quantity"], 0);
          bool flag = false;
          if (group_id > 0 && model.group_price != null)
          {
            user_group_price userGroupPrice = model.group_price.Find((Predicate<user_group_price>) (p => p.group_id == group_id));
            if (userGroupPrice != null)
            {
              flag = true;
              cartItems.user_price = userGroupPrice.price;
            }
          }
          if (group_id > 0 && !flag)
          {
            int discount = new Rain.BLL.user_groups().GetDiscount(group_id);
            if (discount > 0)
              cartItems.user_price = cartItems.sell_price * (Decimal) discount / new Decimal(100);
          }
          cartItems.quantity = l.quantity;
          cartItemsList.Add(cartItems);
        }
      }
      return cartItemsList;
    }

    public static cart_total GetTotal(int group_id)
    {
      return ShopCart.GetTotal(ShopCart.GetList(group_id));
    }

    public static cart_total GetTotal(List<cart_items> ls)
    {
      cart_total cartTotal = new cart_total();
      if (ls != null)
      {
        foreach (cart_items l in ls)
        {
          ++cartTotal.total_num;
          cartTotal.total_quantity += l.quantity;
          cartTotal.payable_amount += l.sell_price * (Decimal) l.quantity;
          cartTotal.real_amount += l.user_price * (Decimal) l.quantity;
          cartTotal.total_point += l.point * l.quantity;
        }
      }
      return cartTotal;
    }

    public static int GetQuantityCount()
    {
      string cookies = ShopCart.GetCookies();
      int num = 0;
      if (!string.IsNullOrEmpty(cookies))
      {
        List<cart_keys> cartKeysList = JsonHelper.JSONToObject<List<cart_keys>>(cookies);
        if (cartKeysList != null)
        {
          foreach (cart_keys cartKeys in cartKeysList)
            num += cartKeys.quantity;
        }
      }
      return num;
    }

    private static List<cart_keys> GetCart()
    {
      List<cart_keys> cartKeysList = new List<cart_keys>();
      string cookies = ShopCart.GetCookies();
      if (!string.IsNullOrEmpty(cookies))
        return JsonHelper.JSONToObject<List<cart_keys>>(cookies);
      return (List<cart_keys>) null;
    }

    private static void AddCookies(string strValue)
    {
      Utils.WriteCookie("dt_cookie_shopping_cart", strValue, 43200);
    }

    private static string GetCookies()
    {
      return Utils.GetCookie("dt_cookie_shopping_cart");
    }
  }
}
