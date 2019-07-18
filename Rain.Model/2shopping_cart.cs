// Decompiled with JetBrains decompiler
// Type: Rain.Model.cart_total
// Assembly: Rain.Model, Version=4.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 7F3024AC-7DBA-4351-8C52-F3DF08C93221
// Assembly location: C:\Users\Felix.Ho\Desktop\page\bin\Rain.Model.dll

using System;

namespace Rain.Model
{
  [Serializable]
  public class cart_total
  {
    private int _total_num = 0;
    private int _total_quantity = 0;
    private Decimal _payable_amount = new Decimal(0);
    private Decimal _real_amount = new Decimal(0);
    private int _total_point = 0;

    public int total_num
    {
      set
      {
        this._total_num = value;
      }
      get
      {
        return this._total_num;
      }
    }

    public int total_quantity
    {
      set
      {
        this._total_quantity = value;
      }
      get
      {
        return this._total_quantity;
      }
    }

    public Decimal payable_amount
    {
      set
      {
        this._payable_amount = value;
      }
      get
      {
        return this._payable_amount;
      }
    }

    public Decimal real_amount
    {
      set
      {
        this._real_amount = value;
      }
      get
      {
        return this._real_amount;
      }
    }

    public int total_point
    {
      set
      {
        this._total_point = value;
      }
      get
      {
        return this._total_point;
      }
    }
  }
}
