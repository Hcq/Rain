// Decompiled with JetBrains decompiler
// Type: Rain.Model.article_comment
// Assembly: Rain.Model, Version=4.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 7F3024AC-7DBA-4351-8C52-F3DF08C93221
// Assembly location: C:\Users\Felix.Ho\Desktop\page\bin\Rain.Model.dll

using System;

namespace Rain.Model
{
  [Serializable]
  public class article_comment
  {
    private int _channel_id = 0;
    private int _article_id = 0;
    private int _parent_id = 0;
    private int _user_id = 0;
    private string _user_name = "";
    private int _is_lock = 0;
    private DateTime _add_time = DateTime.Now;
    private int _is_reply = 0;
    private int _id;
    private string _user_ip;
    private string _content;
    private string _reply_content;
    private DateTime? _reply_time;

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

    public int channel_id
    {
      set
      {
        this._channel_id = value;
      }
      get
      {
        return this._channel_id;
      }
    }

    public int article_id
    {
      set
      {
        this._article_id = value;
      }
      get
      {
        return this._article_id;
      }
    }

    public int parent_id
    {
      set
      {
        this._parent_id = value;
      }
      get
      {
        return this._parent_id;
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

    public int is_reply
    {
      set
      {
        this._is_reply = value;
      }
      get
      {
        return this._is_reply;
      }
    }

    public string reply_content
    {
      set
      {
        this._reply_content = value;
      }
      get
      {
        return this._reply_content;
      }
    }

    public DateTime? reply_time
    {
      set
      {
        this._reply_time = value;
      }
      get
      {
        return this._reply_time;
      }
    }
  }
}
