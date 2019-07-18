// Decompiled with JetBrains decompiler
// Type: Rain.Model.user_recharge
// Assembly: Rain.Model, Version=4.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 7F3024AC-7DBA-4351-8C52-F3DF08C93221
// Assembly location: C:\Users\Felix.Ho\Desktop\page\bin\Rain.Model.dll

using System;

namespace Rain.Model
{
  [Serializable]
  public class user_recharge
  {
    private Decimal _amount = new Decimal(0);
    private int _status = 0;
    private DateTime _add_time = DateTime.Now;
    private int _id;
    private int _user_id;
    private string _user_name;
    private string _recharge_no;
    private int _payment_id;
    private DateTime? _complete_time;

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

    public int user_id
    {
      set
      {
        this._user_id = value;
      }
      get
      {
        return this._user_id;
      }
    }

    public string user_name
    {
      set
      {
        this._user_name = value;
      }
      get
      {
        return this._user_name;
      }
    }

    public string recharge_no
    {
      set
      {
        this._recharge_no = value;
      }
      get
      {
        return this._recharge_no;
      }
    }

    public int payment_id
    {
      set
      {
        this._payment_id = value;
      }
      get
      {
        return this._payment_id;
      }
    }

    public Decimal amount
    {
      set
      {
        this._amount = value;
      }
      get
      {
        return this._amount;
      }
    }

    public int status
    {
      set
      {
        this._status = value;
      }
      get
      {
        return this._status;
      }
    }

    public DateTime add_time
    {
      set
      {
        this._add_time = value;
      }
      get
      {
        return this._add_time;
      }
    }

    public DateTime? complete_time
    {
      set
      {
        this._complete_time = value;
      }
      get
      {
        return this._complete_time;
      }
    }
  }
}
