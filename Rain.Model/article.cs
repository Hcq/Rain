// Decompiled with JetBrains decompiler
// Type: Rain.Model.article
// Assembly: Rain.Model, Version=4.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 7F3024AC-7DBA-4351-8C52-F3DF08C93221
// Assembly location: C:\Users\Felix.Ho\Desktop\page\bin\Rain.Model.dll

using System;
using System.Collections.Generic;

namespace Rain.Model
{
  [Serializable]
  public class article
  {
    private int _channel_id = 0;
    private int _category_id = 0;
    private string _call_index = string.Empty;
    private string _link_url = string.Empty;
    private string _img_url = string.Empty;
    private string _seo_title = string.Empty;
    private string _seo_keywords = string.Empty;
    private string _seo_description = string.Empty;
    private string _zhaiyao = string.Empty;
    private int _sort_id = 99;
    private int _click = 0;
    private int _status = 0;
    private int _is_msg = 0;
    private int _is_top = 0;
    private int _is_red = 0;
    private int _is_hot = 0;
    private int _is_slide = 0;
    private int _is_sys = 0;
    private DateTime _add_time = DateTime.Now;
    private int _id;
    private string _title;
    private string _content;
    private string _user_name;
    private DateTime? _update_time;
    private Dictionary<string, string> _fields;
    private List<article_albums> _albums;
    private List<article_attach> _attach;
    private List<user_group_price> _group_price;

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

    public int category_id
    {
      set
      {
        this._category_id = value;
      }
      get
      {
        return this._category_id;
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

    public string zhaiyao
    {
      set
      {
        this._zhaiyao = value;
      }
      get
      {
        return this._zhaiyao;
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

    public int click
    {
      set
      {
        this._click = value;
      }
      get
      {
        return this._click;
      }
    }

    public int status
    {
      set
      {
        this._status = value;
      }
      get
      {
        return this._status;
      }
    }

    public int is_msg
    {
      set
      {
        this._is_msg = value;
      }
      get
      {
        return this._is_msg;
      }
    }

    public int is_top
    {
      set
      {
        this._is_top = value;
      }
      get
      {
        return this._is_top;
      }
    }

    public int is_red
    {
      set
      {
        this._is_red = value;
      }
      get
      {
        return this._is_red;
      }
    }

    public int is_hot
    {
      set
      {
        this._is_hot = value;
      }
      get
      {
        return this._is_hot;
      }
    }

    public int is_slide
    {
      set
      {
        this._is_slide = value;
      }
      get
      {
        return this._is_slide;
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

    public DateTime? update_time
    {
      set
      {
        this._update_time = value;
      }
      get
      {
        return this._update_time;
      }
    }

    public Dictionary<string, string> fields
    {
      get
      {
        return this._fields;
      }
      set
      {
        this._fields = value;
      }
    }

    public List<article_albums> albums
    {
      set
      {
        this._albums = value;
      }
      get
      {
        return this._albums;
      }
    }

    public List<article_attach> attach
    {
      set
      {
        this._attach = value;
      }
      get
      {
        return this._attach;
      }
    }

    public List<user_group_price> group_price
    {
      set
      {
        this._group_price = value;
      }
      get
      {
        return this._group_price;
      }
    }
  }
}
