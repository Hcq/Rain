// Decompiled with JetBrains decompiler
// Type: Rain.Model.channel
// Assembly: Rain.Model, Version=4.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 7F3024AC-7DBA-4351-8C52-F3DF08C93221
// Assembly location: C:\Users\Felix.Ho\Desktop\page\bin\Rain.Model.dll

using System;
using System.Collections.Generic;

namespace Rain.Model
{
  [Serializable]
  public class channel
  {
    private string _name = "";
    private string _title = "";
    private int _is_albums = 0;
    private int _is_attach = 0;
    private int _is_spec = 0;
    private int _sort_id = 99;
    private int _id;
    private int _site_id;
    private List<channel_field> _channel_fields;

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

    public int site_id
    {
      set
      {
        this._site_id = value;
      }
      get
      {
        return this._site_id;
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

    public int is_albums
    {
      set
      {
        this._is_albums = value;
      }
      get
      {
        return this._is_albums;
      }
    }

    public int is_attach
    {
      set
      {
        this._is_attach = value;
      }
      get
      {
        return this._is_attach;
      }
    }

    public int is_spec
    {
      set
      {
        this._is_spec = value;
      }
      get
      {
        return this._is_spec;
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

    public List<channel_field> channel_fields
    {
      set
      {
        this._channel_fields = value;
      }
      get
      {
        return this._channel_fields;
      }
    }
  }
}
