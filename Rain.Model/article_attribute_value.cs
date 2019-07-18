// Decompiled with JetBrains decompiler
// Type: Rain.Model.article_attribute_value
// Assembly: Rain.Model, Version=4.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 7F3024AC-7DBA-4351-8C52-F3DF08C93221
// Assembly location: C:\Users\Felix.Ho\Desktop\page\bin\Rain.Model.dll

using System;

namespace Rain.Model
{
  [Serializable]
  public class article_attribute_value
  {
    private string _source = "";
    private string _author = "";
    private int _stock_quantity = 0;
    private Decimal _market_price = new Decimal(0);
    private Decimal _sell_price = new Decimal(0);
    private int _point = 0;
    private int _article_id;
    private string _sub_title;
    private string _goods_no;

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

    public string sub_title
    {
      set
      {
        this._sub_title = value;
      }
      get
      {
        return this._sub_title;
      }
    }

    public string source
    {
      set
      {
        this._source = value;
      }
      get
      {
        return this._source;
      }
    }

    public string author
    {
      set
      {
        this._author = value;
      }
      get
      {
        return this._author;
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

    public Decimal market_price
    {
      set
      {
        this._market_price = value;
      }
      get
      {
        return this._market_price;
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
