// Decompiled with JetBrains decompiler
// Type: Rain.Model.manager_log
// Assembly: Rain.Model, Version=4.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 7F3024AC-7DBA-4351-8C52-F3DF08C93221
// Assembly location: C:\Users\Felix.Ho\Desktop\page\bin\Rain.Model.dll

using System;

namespace Rain.Model
{
  [Serializable]
  public class manager_log
  {
    private DateTime _add_time = DateTime.Now;
    private int _id;
    private int _user_id;
    private string _user_name;
    private string _action_type;
    private string _remark;
    private string _user_ip;

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

    public string action_type
    {
      set
      {
        this._action_type = value;
      }
      get
      {
        return this._action_type;
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
