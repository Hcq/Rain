// Decompiled with JetBrains decompiler
// Type: Rain.Model.plugin
// Assembly: Rain.Model, Version=4.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 7F3024AC-7DBA-4351-8C52-F3DF08C93221
// Assembly location: C:\Users\Felix.Ho\Desktop\page\bin\Rain.Model.dll

using System;

namespace Rain.Model
{
  [Serializable]
  public class plugin
  {
    private string _directory;
    private string _name;
    private string _author;
    private string _version;
    private string _description;
    private int _isload;

    public string directory
    {
      get
      {
        return this._directory;
      }
      set
      {
        this._directory = value;
      }
    }

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

    public string description
    {
      get
      {
        return this._description;
      }
      set
      {
        this._description = value;
      }
    }

    public int isload
    {
      get
      {
        return this._isload;
      }
      set
      {
        this._isload = value;
      }
    }
  }
}
