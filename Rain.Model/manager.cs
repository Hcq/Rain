// Decompiled with JetBrains decompiler
// Type: Rain.Model.manager
// Assembly: Rain.Model, Version=4.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 7F3024AC-7DBA-4351-8C52-F3DF08C93221
// Assembly location: C:\Users\Felix.Ho\Desktop\page\bin\Rain.Model.dll

using System;

namespace Rain.Model
{
  [Serializable]
  public class manager
  {
    private int _role_type = 2;
    private string _real_name = "";
    private string _telephone = "";
    private string _email = "";
    private int _is_lock = 0;
    private DateTime _add_time = DateTime.Now;
    private int _id;
    private int _role_id;
    private string _user_name;
    private string _password;
    private string _salt;

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

    public int role_id
    {
      set
      {
        this._role_id = value;
      }
      get
      {
        return this._role_id;
      }
    }

    public int role_type
    {
      set
      {
        this._role_type = value;
      }
      get
      {
        return this._role_type;
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

    public string real_name
    {
      set
      {
        this._real_name = value;
      }
      get
      {
        return this._real_name;
      }
    }

    public string telephone
    {
      set
      {
        this._telephone = value;
      }
      get
      {
        return this._telephone;
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
