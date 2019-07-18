// Decompiled with JetBrains decompiler
// Type: Rain.Model.article_attribute_field
// Assembly: Rain.Model, Version=4.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 7F3024AC-7DBA-4351-8C52-F3DF08C93221
// Assembly location: C:\Users\Felix.Ho\Desktop\page\bin\Rain.Model.dll

using System;

namespace Rain.Model
{
  [Serializable]
  public class article_attribute_field
  {
    private string _title = "";
    private int _data_length = 0;
    private int _data_place = 0;
    private string _item_option = "";
    private string _default_value = "";
    private int _is_required = 0;
    private int _is_password = 0;
    private int _is_html = 0;
    private int _editor_type = 0;
    private string _valid_tip_msg = "";
    private string _valid_error_msg = "";
    private string _valid_pattern = "";
    private int _sort_id = 99;
    private int _is_sys = 0;
    private int _id;
    private string _name;
    private string _control_type;
    private string _data_type;

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

    public string control_type
    {
      set
      {
        this._control_type = value;
      }
      get
      {
        return this._control_type;
      }
    }

    public string data_type
    {
      set
      {
        this._data_type = value;
      }
      get
      {
        return this._data_type;
      }
    }

    public int data_length
    {
      set
      {
        this._data_length = value;
      }
      get
      {
        return this._data_length;
      }
    }

    public int data_place
    {
      set
      {
        this._data_place = value;
      }
      get
      {
        return this._data_place;
      }
    }

    public string item_option
    {
      set
      {
        this._item_option = value;
      }
      get
      {
        return this._item_option;
      }
    }

    public string default_value
    {
      set
      {
        this._default_value = value;
      }
      get
      {
        return this._default_value;
      }
    }

    public int is_required
    {
      set
      {
        this._is_required = value;
      }
      get
      {
        return this._is_required;
      }
    }

    public int is_password
    {
      set
      {
        this._is_password = value;
      }
      get
      {
        return this._is_password;
      }
    }

    public int is_html
    {
      set
      {
        this._is_html = value;
      }
      get
      {
        return this._is_html;
      }
    }

    public int editor_type
    {
      set
      {
        this._editor_type = value;
      }
      get
      {
        return this._editor_type;
      }
    }

    public string valid_tip_msg
    {
      set
      {
        this._valid_tip_msg = value;
      }
      get
      {
        return this._valid_tip_msg;
      }
    }

    public string valid_error_msg
    {
      set
      {
        this._valid_error_msg = value;
      }
      get
      {
        return this._valid_error_msg;
      }
    }

    public string valid_pattern
    {
      set
      {
        this._valid_pattern = value;
      }
      get
      {
        return this._valid_pattern;
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
