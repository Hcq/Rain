// Decompiled with JetBrains decompiler
// Type: Rain.Model.user_code
// Assembly: Rain.Model, Version=4.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 7F3024AC-7DBA-4351-8C52-F3DF08C93221
// Assembly location: C:\Users\Felix.Ho\Desktop\page\bin\Rain.Model.dll

using System;

namespace Rain.Model
{
  [Serializable]
  public class user_code
  {
    private int _count = 0;
    private int _status = 0;
    private string _user_ip = string.Empty;
    private DateTime _add_time = DateTime.Now;
    private int _id;
    private int _user_id;
    private string _user_name;
    private string _type;
    private string _str_code;
    private DateTime _eff_time;

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

    public string type
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

    public string str_code
    {
      set
      {
        this._str_code = value;
      }
      get
      {
        return this._str_code;
      }
    }

    public int count
    {
      set
      {
        this._count = value;
      }
      get
      {
        return this._count;
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

    public string user_ip
    {
      set
      {
        this._user_ip = value;
      }
      get
      {
        return this._user_ip;
      }
    }

    public DateTime eff_time
    {
      set
      {
        this._eff_time = value;
      }
      get
      {
        return this._eff_time;
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
  }
}
