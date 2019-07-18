// Decompiled with JetBrains decompiler
// Type: Rain.Model.navigation
// Assembly: Rain.Model, Version=4.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 7F3024AC-7DBA-4351-8C52-F3DF08C93221
// Assembly location: C:\Users\Felix.Ho\Desktop\page\bin\Rain.Model.dll

using System;

namespace Rain.Model
{
  [Serializable]
  public class navigation
  {
    private int _parent_id = 0;
    private int _channel_id = 0;
    private string _nav_type = "";
    private string _name = "";
    private string _title = "";
    private string _sub_title = "";
    private string _icon_url = "";
    private string _link_url = "";
    private int _sort_id = 99;
    private int _is_lock = 0;
    private string _remark = "";
    private string _action_type = "";
    private int _is_sys = 0;
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

    public string nav_type
    {
      set
      {
        this._nav_type = value;
      }
      get
      {
        return this._nav_type;
      }
    }

    public string name
    {
      set
      {
        this._name = value;
      }
      get
      {
        return this._name;
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

    public string sub_title
    {
      set
      {
        this._sub_title = value;
      }
      get
      {
        return this._sub_title;
      }
    }

    public string icon_url
    {
      set
      {
        this._icon_url = value;
      }
      get
      {
        return this._icon_url;
      }
    }

    public string link_url
    {
      set
      {
        this._link_url = value;
      }
      get
      {
        return this._link_url;
      }
    }

    public int sort_id
    {
      set
      {
        this._sort_id = value;
      }
      get
      {
        return this._sort_id;
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

    public int is_sys
    {
      set
      {
        this._is_sys = value;
      }
      get
      {
        return this._is_sys;
      }
    }
  }
}
