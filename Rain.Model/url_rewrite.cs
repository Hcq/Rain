// Decompiled with JetBrains decompiler
// Type: Rain.Model.url_rewrite
// Assembly: Rain.Model, Version=4.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 7F3024AC-7DBA-4351-8C52-F3DF08C93221
// Assembly location: C:\Users\Felix.Ho\Desktop\page\bin\Rain.Model.dll

using System;
using System.Collections.Generic;

namespace Rain.Model
{
  [Serializable]
  public class url_rewrite
  {
    private string _name = string.Empty;
    private string _type = string.Empty;
    private string _page = string.Empty;
    private string _inherit = string.Empty;
    private string _templet = string.Empty;
    private string _channel = string.Empty;
    private string _pagesize = string.Empty;
    private string _url_rewrite_str = string.Empty;
    private List<url_rewrite_item> _url_rewrite_items;

    public string name
    {
      get
      {
        return this._name;
      }
      set
      {
        this._name = value;
      }
    }

    public string type
    {
      get
      {
        return this._type;
      }
      set
      {
        this._type = value;
      }
    }

    public string page
    {
      get
      {
        return this._page;
      }
      set
      {
        this._page = value;
      }
    }

    public string inherit
    {
      get
      {
        return this._inherit;
      }
      set
      {
        this._inherit = value;
      }
    }

    public string templet
    {
      get
      {
        return this._templet;
      }
      set
      {
        this._templet = value;
      }
    }

    public string channel
    {
      get
      {
        return this._channel;
      }
      set
      {
        this._channel = value;
      }
    }

    public string pagesize
    {
      get
      {
        return this._pagesize;
      }
      set
      {
        this._pagesize = value;
      }
    }

    public string url_rewrite_str
    {
      get
      {
        return this._url_rewrite_str;
      }
      set
      {
        this._url_rewrite_str = value;
      }
    }

    public List<url_rewrite_item> url_rewrite_items
    {
      set
      {
        this._url_rewrite_items = value;
      }
      get
      {
        return this._url_rewrite_items;
      }
    }
  }
}
