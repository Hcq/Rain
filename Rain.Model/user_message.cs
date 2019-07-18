// Decompiled with JetBrains decompiler
// Type: Rain.Model.user_message
// Assembly: Rain.Model, Version=4.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 7F3024AC-7DBA-4351-8C52-F3DF08C93221
// Assembly location: C:\Users\Felix.Ho\Desktop\page\bin\Rain.Model.dll

using System;

namespace Rain.Model
{
  [Serializable]
  public class user_message
  {
    private int _type = 1;
    private int _is_read = 0;
    private DateTime _post_time = DateTime.Now;
    private int _id;
    private string _post_user_name;
    private string _accept_user_name;
    private string _title;
    private string _content;
    private DateTime? _read_time;

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

    public int type
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

    public string post_user_name
    {
      set
      {
        this._post_user_name = value;
      }
      get
      {
        return this._post_user_name;
      }
    }

    public string accept_user_name
    {
      set
      {
        this._accept_user_name = value;
      }
      get
      {
        return this._accept_user_name;
      }
    }

    public int is_read
    {
      set
      {
        this._is_read = value;
      }
      get
      {
        return this._is_read;
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

    public string content
    {
      set
      {
        this._content = value;
      }
      get
      {
        return this._content;
      }
    }

    public DateTime post_time
    {
      set
      {
        this._post_time = value;
      }
      get
      {
        return this._post_time;
      }
    }

    public DateTime? read_time
    {
      set
      {
        this._read_time = value;
      }
      get
      {
        return this._read_time;
      }
    }
  }
}
