// Decompiled with JetBrains decompiler
// Type: Rain.Model.url_rewrite_item
// Assembly: Rain.Model, Version=4.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 7F3024AC-7DBA-4351-8C52-F3DF08C93221
// Assembly location: C:\Users\Felix.Ho\Desktop\page\bin\Rain.Model.dll

using System;

namespace Rain.Model
{
  [Serializable]
  public class url_rewrite_item
  {
    private string _path = "";
    private string _pattern = "";
    private string _querystring = "";

    public string path
    {
      get
      {
        return this._path;
      }
      set
      {
        this._path = value;
      }
    }

    public string pattern
    {
      get
      {
        return this._pattern;
      }
      set
      {
        this._pattern = value;
      }
    }

    public string querystring
    {
      get
      {
        return this._querystring;
      }
      set
      {
        this._querystring = value;
      }
    }
  }
}
