// Decompiled with JetBrains decompiler
// Type: Rain.Model.user_group_price
// Assembly: Rain.Model, Version=4.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 7F3024AC-7DBA-4351-8C52-F3DF08C93221
// Assembly location: C:\Users\Felix.Ho\Desktop\page\bin\Rain.Model.dll

using System;

namespace Rain.Model
{
  [Serializable]
  public class user_group_price
  {
    private int _article_id = 0;
    private int _goods_id = 0;
    private int _group_id = 0;
    private Decimal _price = new Decimal(0);
    private int _id;

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

    public int group_id
    {
      set
      {
        this._group_id = value;
      }
      get
      {
        return this._group_id;
      }
    }

    public Decimal price
    {
      set
      {
        this._price = value;
      }
      get
      {
        return this._price;
      }
    }
  }
}
