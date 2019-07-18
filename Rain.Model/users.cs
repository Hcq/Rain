// Decompiled with JetBrains decompiler
// Type: Rain.Model.users
// Assembly: Rain.Model, Version=4.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 7F3024AC-7DBA-4351-8C52-F3DF08C93221
// Assembly location: C:\Users\Felix.Ho\Desktop\page\bin\Rain.Model.dll

using System;

namespace Rain.Model
{
  [Serializable]
  public class users
  {
    private int _group_id = 0;
    private string _user_name = string.Empty;
    private string _mobile = string.Empty;
    private string _email = string.Empty;
    private string _avatar = string.Empty;
    private string _nick_name = string.Empty;
    private string _sex = string.Empty;
    private string _telphone = string.Empty;
    private string _area = string.Empty;
    private string _address = string.Empty;
    private string _qq = string.Empty;
    private string _msn = string.Empty;
    private Decimal _amount = new Decimal(0);
    private int _point = 0;
    private int _exp = 0;
    private int _status = 0;
    private DateTime _reg_time = DateTime.Now;
    private string _reg_ip = string.Empty;
    private int _id;
    private string _salt;
    private string _password;
    private DateTime? _birthday;

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

    public string salt
    {
      set
      {
        this._salt = value;
      }
      get
      {
        return this._salt;
      }
    }

    public string password
    {
      set
      {
        this._password = value;
      }
      get
      {
        return this._password;
      }
    }

    public string mobile
    {
      set
      {
        this._mobile = value;
      }
      get
      {
        return this._mobile;
      }
    }

    public string email
    {
      set
      {
        this._email = value;
      }
      get
      {
        return this._email;
      }
    }

    public string avatar
    {
      set
      {
        this._avatar = value;
      }
      get
      {
        return this._avatar;
      }
    }

    public string nick_name
    {
      set
      {
        this._nick_name = value;
      }
      get
      {
        return this._nick_name;
      }
    }

    public string sex
    {
      set
      {
        this._sex = value;
      }
      get
      {
        return this._sex;
      }
    }

    public DateTime? birthday
    {
      set
      {
        this._birthday = value;
      }
      get
      {
        return this._birthday;
      }
    }

    public string telphone
    {
      set
      {
        this._telphone = value;
      }
      get
      {
        return this._telphone;
      }
    }

    public string area
    {
      set
      {
        this._area = value;
      }
      get
      {
        return this._area;
      }
    }

    public string address
    {
      set
      {
        this._address = value;
      }
      get
      {
        return this._address;
      }
    }

    public string qq
    {
      set
      {
        this._qq = value;
      }
      get
      {
        return this._qq;
      }
    }

    public string msn
    {
      set
      {
        this._msn = value;
      }
      get
      {
        return this._msn;
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

    public int exp
    {
      set
      {
        this._exp = value;
      }
      get
      {
        return this._exp;
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

    public DateTime reg_time
    {
      set
      {
        this._reg_time = value;
      }
      get
      {
        return this._reg_time;
      }
    }

    public string reg_ip
    {
      set
      {
        this._reg_ip = value;
      }
      get
      {
        return this._reg_ip;
      }
    }
  }
}
