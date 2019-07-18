// Decompiled with JetBrains decompiler
// Type: Rain.Model.cart_items
// Assembly: Rain.Model, Version=4.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 7F3024AC-7DBA-4351-8C52-F3DF08C93221
// Assembly location: C:\Users\Felix.Ho\Desktop\page\bin\Rain.Model.dll

using System;

namespace Rain.Model
{
  [Serializable]
  public class cart_items
  {
    private string _goods_no = string.Empty;
    private string _title = string.Empty;
    private string _spec_text = string.Empty;
    private string _img_url = string.Empty;
    private Decimal _sell_price = new Decimal(0);
    private Decimal _user_price = new Decimal(0);
    private int _point = 0;
    private int _quantity = 1;
    private int _stock_quantity = 0;
    private int _article_id;

    public int article_id
    {
      set
      {
        this._article_id = value;
      }
      get
      {
        return this._article_id;
      }
    }

    public string goods_no
    {
      set
      {
        this._goods_no = value;
      }
      get
      {
        return this._goods_no;
      }
    }

    public string title
    {
      set
      {
        this._title = value;
      }
      get
      {
        return this._title;
      }
    }

    public string spec_text
    {
      set
      {
        this._spec_text = value;
      }
      get
      {
        return this._spec_text;
      }
    }

    public string img_url
    {
      set
      {
        this._img_url = value;
      }
      get
      {
        return this._img_url;
      }
    }

    public Decimal sell_price
    {
      set
      {
        this._sell_price = value;
      }
      get
      {
        return this._sell_price;
      }
    }

    public Decimal user_price
    {
      set
      {
        this._user_price = value;
      }
      get
      {
        return this._user_price;
      }
    }

    public int point
    {
      set
      {
        this._point = value;
      }
      get
      {
        return this._point;
      }
    }

    public int quantity
    {
      get
      {
        return this._quantity;
      }
      set
      {
        this._quantity = value;
      }
    }

    public int stock_quantity
    {
      set
      {
        this._stock_quantity = value;
      }
      get
      {
        return this._stock_quantity;
      }
    }
  }
}
