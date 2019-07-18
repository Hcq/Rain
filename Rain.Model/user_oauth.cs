// Decompiled with JetBrains decompiler
// Type: Rain.Model.user_oauth
// Assembly: Rain.Model, Version=4.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 7F3024AC-7DBA-4351-8C52-F3DF08C93221
// Assembly location: C:\Users\Felix.Ho\Desktop\page\bin\Rain.Model.dll

using System;

namespace Rain.Model
{
  [Serializable]
  public class user_oauth
  {
    private string _oauth_name = "0";
    private DateTime _add_time = DateTime.Now;
    private int _id;
    private int _user_id;
    private string _user_name;
    private string _oauth_access_token;
    private string _oauth_openid;

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

    public string oauth_name
    {
      set
      {
        this._oauth_name = value;
      }
      get
      {
        return this._oauth_name;
      }
    }

    public string oauth_access_token
    {
      set
      {
        this._oauth_access_token = value;
      }
      get
      {
        return this._oauth_access_token;
      }
    }

    public string oauth_openid
    {
      set
      {
        this._oauth_openid = value;
      }
      get
      {
        return this._oauth_openid;
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
