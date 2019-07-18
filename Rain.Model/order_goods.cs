// Decompiled with JetBrains decompiler
// Type: Rain.Model.order_goods
// Assembly: Rain.Model, Version=4.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 7F3024AC-7DBA-4351-8C52-F3DF08C93221
// Assembly location: C:\Users\Felix.Ho\Desktop\page\bin\Rain.Model.dll

using System;

namespace Rain.Model
{
  [Serializable]
  public class order_goods
  {
    private string _goods_no = string.Empty;
    private string _goods_title = string.Empty;
    private string _img_url = string.Empty;
    private string _spec_text = string.Empty;
    private Decimal _goods_price = new Decimal(0);
    private Decimal _real_price = new Decimal(0);
    private int _quantity = 0;
    private int _point = 0;
    private int _id;
    private int _article_id;
    private int _order_id;

    public int id
    {
      set
      {
        this._id = value;
      }
      get
      {
        return this._id;
      }
    }

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

    public int order_id
    {
      set
      {
        this._order_id = value;
      }
      get
      {
        return this._order_id;
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

    public string goods_title
    {
      set
      {
        this._goods_title = value;
      }
      get
      {
        return this._goods_title;
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

    public Decimal goods_price
    {
      set
      {
        this._goods_price = value;
      }
      get
      {
        return this._goods_price;
      }
    }

    public Decimal real_price
    {
      set
      {
        this._real_price = value;
      }
      get
      {
        return this._real_price;
      }
    }

    public int quantity
    {
      set
      {
        this._quantity = value;
      }
      get
      {
        return this._quantity;
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
  }
}
