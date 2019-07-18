// Decompiled with JetBrains decompiler
// Type: Rain.Model.user_oauth_app
// Assembly: Rain.Model, Version=4.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 7F3024AC-7DBA-4351-8C52-F3DF08C93221
// Assembly location: C:\Users\Felix.Ho\Desktop\page\bin\Rain.Model.dll

using System;

namespace Rain.Model
{
  [Serializable]
  public class user_oauth_app
  {
    private string _title = "";
    private string _img_url = "";
    private string _remark = "";
    private int _sort_id = 99;
    private int _is_lock = 0;
    private string _api_path = "";
    private int _id;
    private string _app_id;
    private string _app_key;

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

    public string img_url
    {
      set
      {
        this._img_url = value;
      }
      get
      {
        return this._img_url;
      }
    }

    public string app_id
    {
      set
      {
        this._app_id = value;
      }
      get
      {
        return this._app_id;
      }
    }

    public string app_key
    {
      set
      {
        this._app_key = value;
      }
      get
      {
        return this._app_key;
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

    public string api_path
    {
      set
      {
        this._api_path = value;
      }
      get
      {
        return this._api_path;
      }
    }
  }
}
