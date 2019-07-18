// Decompiled with JetBrains decompiler
// Type: Rain.Model.express
// Assembly: Rain.Model, Version=4.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 7F3024AC-7DBA-4351-8C52-F3DF08C93221
// Assembly location: C:\Users\Felix.Ho\Desktop\page\bin\Rain.Model.dll

using System;

namespace Rain.Model
{
  [Serializable]
  public class express
  {
    private string _express_code = "";
    private Decimal _express_fee = new Decimal(0);
    private string _website = "";
    private string _remark = "";
    private int _sort_id = 99;
    private int _is_lock = 0;
    private int _id;
    private string _title;

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

    public string express_code
    {
      set
      {
        this._express_code = value;
      }
      get
      {
        return this._express_code;
      }
    }

    public Decimal express_fee
    {
      set
      {
        this._express_fee = value;
      }
      get
      {
        return this._express_fee;
      }
    }

    public string website
    {
      set
      {
        this._website = value;
      }
      get
      {
        return this._website;
      }
    }

    public string remark
    {
      set
      {
        this._remark = value;
      }
      get
      {
        return this._remark;
      }
    }

    public int sort_id
    {
      set
      {
        this._sort_id = value;
      }
      get
      {
        return this._sort_id;
      }
    }

    public int is_lock
    {
      set
      {
        this._is_lock = value;
      }
      get
      {
        return this._is_lock;
      }
    }
  }
}
