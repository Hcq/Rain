// Decompiled with JetBrains decompiler
// Type: Rain.Model.article_albums
// Assembly: Rain.Model, Version=4.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 7F3024AC-7DBA-4351-8C52-F3DF08C93221
// Assembly location: C:\Users\Felix.Ho\Desktop\page\bin\Rain.Model.dll

using System;

namespace Rain.Model
{
  [Serializable]
  public class article_albums
  {
    private int _article_id = 0;
    private string _thumb_path = "";
    private string _original_path = "";
    private string _remark = "";
    private DateTime _add_time = DateTime.Now;
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

    public string thumb_path
    {
      set
      {
        this._thumb_path = value;
      }
      get
      {
        return this._thumb_path;
      }
    }

    public string original_path
    {
      set
      {
        this._original_path = value;
      }
      get
      {
        return this._original_path;
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
  }
}
