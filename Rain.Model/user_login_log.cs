// Decompiled with JetBrains decompiler
// Type: Rain.Model.user_login_log
// Assembly: Rain.Model, Version=4.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 7F3024AC-7DBA-4351-8C52-F3DF08C93221
// Assembly location: C:\Users\Felix.Ho\Desktop\page\bin\Rain.Model.dll

using System;

namespace Rain.Model
{
  [Serializable]
  public class user_login_log
  {
    private string _user_name = "";
    private string _remark = "";
    private DateTime _login_time = DateTime.Now;
    private string _login_ip = "";
    private int _id;
    private int _user_id;

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

    public DateTime login_time
    {
      set
      {
        this._login_time = value;
      }
      get
      {
        return this._login_time;
      }
    }

    public string login_ip
    {
      set
      {
        this._login_ip = value;
      }
      get
      {
        return this._login_ip;
      }
    }
  }
}
