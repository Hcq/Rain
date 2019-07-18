// Decompiled with JetBrains decompiler
// Type: Rain.Model.mail_template
// Assembly: Rain.Model, Version=4.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 7F3024AC-7DBA-4351-8C52-F3DF08C93221
// Assembly location: C:\Users\Felix.Ho\Desktop\page\bin\Rain.Model.dll

using System;

namespace Rain.Model
{
  [Serializable]
  public class mail_template
  {
    private int _is_sys = 0;
    private int _id;
    private string _title;
    private string _call_index;
    private string _maill_title;
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

    public string maill_title
    {
      set
      {
        this._maill_title = value;
      }
      get
      {
        return this._maill_title;
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
