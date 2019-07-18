// Decompiled with JetBrains decompiler
// Type: Rain.Model.channel_site
// Assembly: Rain.Model, Version=4.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 7F3024AC-7DBA-4351-8C52-F3DF08C93221
// Assembly location: C:\Users\Felix.Ho\Desktop\page\bin\Rain.Model.dll

using System;

namespace Rain.Model
{
  [Serializable]
  public class channel_site
  {
    private string _title = string.Empty;
    private string _build_path = string.Empty;
    private string _templet_path = string.Empty;
    private string _domain = "";
    private int _is_default = 0;
    private int _sort_id = 99;
    private int _id;
    private string _name;
    private string _logo;
    private string _company;
    private string _address;
    private string _tel;
    private string _fax;
    private string _email;
    private string _crod;
    private string _copyright;
    private string _seo_title;
    private string _seo_keyword;
    private string _seo_description;

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

    public string build_path
    {
      set
      {
        this._build_path = value;
      }
      get
      {
        return this._build_path;
      }
    }

    public string templet_path
    {
      set
      {
        this._templet_path = value;
      }
      get
      {
        return this._templet_path;
      }
    }

    public string domain
    {
      set
      {
        this._domain = value;
      }
      get
      {
        return this._domain;
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

    public string logo
    {
      set
      {
        this._logo = value;
      }
      get
      {
        return this._logo;
      }
    }

    public string company
    {
      set
      {
        this._company = value;
      }
      get
      {
        return this._company;
      }
    }

    public string address
    {
      set
      {
        this._address = value;
      }
      get
      {
        return this._address;
      }
    }

    public string tel
    {
      set
      {
        this._tel = value;
      }
      get
      {
        return this._tel;
      }
    }

    public string fax
    {
      set
      {
        this._fax = value;
      }
      get
      {
        return this._fax;
      }
    }

    public string email
    {
      set
      {
        this._email = value;
      }
      get
      {
        return this._email;
      }
    }

    public string crod
    {
      set
      {
        this._crod = value;
      }
      get
      {
        return this._crod;
      }
    }

    public string copyright
    {
      set
      {
        this._copyright = value;
      }
      get
      {
        return this._copyright;
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

    public string seo_keyword
    {
      set
      {
        this._seo_keyword = value;
      }
      get
      {
        return this._seo_keyword;
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

    public int is_default
    {
      set
      {
        this._is_default = value;
      }
      get
      {
        return this._is_default;
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
  }
}
