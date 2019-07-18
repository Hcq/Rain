// Decompiled with JetBrains decompiler
// Type: Rain.Model.orderconfig
// Assembly: Rain.Model, Version=4.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 7F3024AC-7DBA-4351-8C52-F3DF08C93221
// Assembly location: C:\Users\Felix.Ho\Desktop\page\bin\Rain.Model.dll

using System;

namespace Rain.Model
{
  [Serializable]
  public class orderconfig
  {
    private int _anonymous = 0;
    private int _taxtype = 1;
    private Decimal _taxamount = new Decimal(0);
    private int _confirmmsg = 0;
    private string _confirmcallindex = "";
    private int _expressmsg = 0;
    private string _expresscallindex = "";
    private int _completemsg = 0;
    private string _completecallindex = "";

    public int anonymous
    {
      get
      {
        return this._anonymous;
      }
      set
      {
        this._anonymous = value;
      }
    }

    public int taxtype
    {
      get
      {
        return this._taxtype;
      }
      set
      {
        this._taxtype = value;
      }
    }

    public Decimal taxamount
    {
      get
      {
        return this._taxamount;
      }
      set
      {
        this._taxamount = value;
      }
    }

    public int confirmmsg
    {
      get
      {
        return this._confirmmsg;
      }
      set
      {
        this._confirmmsg = value;
      }
    }

    public string confirmcallindex
    {
      get
      {
        return this._confirmcallindex;
      }
      set
      {
        this._confirmcallindex = value;
      }
    }

    public int expressmsg
    {
      get
      {
        return this._expressmsg;
      }
      set
      {
        this._expressmsg = value;
      }
    }

    public string expresscallindex
    {
      get
      {
        return this._expresscallindex;
      }
      set
      {
        this._expresscallindex = value;
      }
    }

    public int completemsg
    {
      get
      {
        return this._completemsg;
      }
      set
      {
        this._completemsg = value;
      }
    }

    public string completecallindex
    {
      get
      {
        return this._completecallindex;
      }
      set
      {
        this._completecallindex = value;
      }
    }
  }
}
