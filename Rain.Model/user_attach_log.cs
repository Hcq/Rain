﻿// Decompiled with JetBrains decompiler
// Type: Rain.Model.user_attach_log
// Assembly: Rain.Model, Version=4.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 7F3024AC-7DBA-4351-8C52-F3DF08C93221
// Assembly location: C:\Users\Felix.Ho\Desktop\page\bin\Rain.Model.dll

using System;

namespace Rain.Model
{
  [Serializable]
  public class user_attach_log
  {
    private DateTime _add_time = DateTime.Now;
    private int _id;
    private int _user_id;
    private string _user_name;
    private int _attach_id;
    private string _file_name;

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

    public int attach_id
    {
      set
      {
        this._attach_id = value;
      }
      get
      {
        return this._attach_id;
      }
    }

    public string file_name
    {
      set
      {
        this._file_name = value;
      }
      get
      {
        return this._file_name;
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
