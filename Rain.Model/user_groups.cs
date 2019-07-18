// Decompiled with JetBrains decompiler
// Type: Rain.Model.user_groups
// Assembly: Rain.Model, Version=4.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 7F3024AC-7DBA-4351-8C52-F3DF08C93221
// Assembly location: C:\Users\Felix.Ho\Desktop\page\bin\Rain.Model.dll

using System;

namespace Rain.Model
{
  [Serializable]
  public class user_groups
  {
    private string _title = "";
    private int _grade = 0;
    private int _upgrade_exp = 0;
    private Decimal _amount = new Decimal(0);
    private int _point = 0;
    private int _discount = 100;
    private int _is_default = 0;
    private int _is_upgrade = 1;
    private int _is_lock = 0;
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

    public int grade
    {
      set
      {
        this._grade = value;
      }
      get
      {
        return this._grade;
      }
    }

    public int upgrade_exp
    {
      set
      {
        this._upgrade_exp = value;
      }
      get
      {
        return this._upgrade_exp;
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

    public int discount
    {
      set
      {
        this._discount = value;
      }
      get
      {
        return this._discount;
      }
    }

    public int is_default
    {
      set
      {
        this._is_default = value;
      }
      get
      {
        return this._is_default;
      }
    }

    public int is_upgrade
    {
      set
      {
        this._is_upgrade = value;
      }
      get
      {
        return this._is_upgrade;
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
