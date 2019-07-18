// Decompiled with JetBrains decompiler
// Type: Rain.Model.article_category
// Assembly: Rain.Model, Version=4.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 7F3024AC-7DBA-4351-8C52-F3DF08C93221
// Assembly location: C:\Users\Felix.Ho\Desktop\page\bin\Rain.Model.dll

using System;

namespace Rain.Model
{
  [Serializable]
  public class article_category
  {
    private string _call_index = "";
    private int _parent_id = 0;
    private string _class_list = "";
    private int _class_layer = 0;
    private int _sort_id = 99;
    private string _link_url = "";
    private string _img_url = "";
    private string _seo_title = "";
    private string _seo_keywords = "";
    private string _seo_description = "";
    private int _id;
    private int _channel_id;
    private string _title;
    private string _content;

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

    public string call_index
    {
      set
      {
        this._call_index = value;
      }
      get
      {
        return this._call_index;
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

    public string class_list
    {
      set
      {
        this._class_list = value;
      }
      get
      {
        return this._class_list;
      }
    }

    public int class_layer
    {
      set
      {
        this._class_layer = value;
      }
      get
      {
        return this._class_layer;
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

    public string seo_title
    {
      set
      {
        this._seo_title = value;
      }
      get
      {
        return this._seo_title;
      }
    }

    public string seo_keywords
    {
      set
      {
        this._seo_keywords = value;
      }
      get
      {
        return this._seo_keywords;
      }
    }

    public string seo_description
    {
      set
      {
        this._seo_description = value;
      }
      get
      {
        return this._seo_description;
      }
    }
  }
}
