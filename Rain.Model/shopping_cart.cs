// Decompiled with JetBrains decompiler
// Type: Rain.Model.cart_keys
// Assembly: Rain.Model, Version=4.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 7F3024AC-7DBA-4351-8C52-F3DF08C93221
// Assembly location: C:\Users\Felix.Ho\Desktop\page\bin\Rain.Model.dll

using System;

namespace Rain.Model
{
  [Serializable]
  public class cart_keys
  {
    private int _quantity = 0;
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
  }
}
