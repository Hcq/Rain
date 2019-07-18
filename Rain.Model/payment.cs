// Decompiled with JetBrains decompiler
// Type: Rain.Model.payment
// Assembly: Rain.Model, Version=4.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 7F3024AC-7DBA-4351-8C52-F3DF08C93221
// Assembly location: C:\Users\Felix.Ho\Desktop\page\bin\Rain.Model.dll

using System;

namespace Rain.Model
{
  [Serializable]
  public class payment
  {
    private string _title = string.Empty;
    private string _img_url = "";
    private string _remark = string.Empty;
    private int _type = 1;
    private int _poundage_type = 1;
    private Decimal _poundage_amount = new Decimal(0);
    private int _sort_id = 99;
    private int _is_lock = 0;
    private string _api_path = string.Empty;
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

    public int type
    {
      set
      {
        this._type = value;
      }
      get
      {
        return this._type;
      }
    }

    public int poundage_type
    {
      set
      {
        this._poundage_type = value;
      }
      get
      {
        return this._poundage_type;
      }
    }

    public Decimal poundage_amount
    {
      set
      {
        this._poundage_amount = value;
      }
      get
      {
        return this._poundage_amount;
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

    public string api_path
    {
      set
      {
        this._api_path = value;
      }
      get
      {
        return this._api_path;
      }
    }
  }
}
