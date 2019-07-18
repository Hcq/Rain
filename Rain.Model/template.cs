// Decompiled with JetBrains decompiler
// Type: Rain.Model.template
// Assembly: Rain.Model, Version=4.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 7F3024AC-7DBA-4351-8C52-F3DF08C93221
// Assembly location: C:\Users\Felix.Ho\Desktop\page\bin\Rain.Model.dll

using System;

namespace Rain.Model
{
  [Serializable]
  public class template
  {
    private string _name = "";
    private string _author = "";
    private string _createdate = "";
    private string _version = "";
    private string _fordntver = "";

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

    public string author
    {
      get
      {
        return this._author;
      }
      set
      {
        this._author = value;
      }
    }

    public string createdate
    {
      get
      {
        return this._createdate;
      }
      set
      {
        this._createdate = value;
      }
    }

    public string version
    {
      get
      {
        return this._version;
      }
      set
      {
        this._version = value;
      }
    }

    public string fordntver
    {
      get
      {
        return this._fordntver;
      }
      set
      {
        this._fordntver = value;
      }
    }
  }
}
