// Decompiled with JetBrains decompiler
// Type: Rain.API.OAuth.oauth_config
// Assembly: Rain.API, Version=4.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 114351D3-1A2F-4CC4-A6A6-43C59DB5A56B
// Assembly location: C:\Users\Felix.Ho\Desktop\page\bin\Rain.API.dll

using System;

namespace Rain.API.OAuth
{
  [Serializable]
  public class oauth_config
  {
    private string _oauth_name = string.Empty;
    private string _oauth_app_id = string.Empty;
    private string _oauth_app_key = string.Empty;
    private string _return_uri = string.Empty;

    public string oauth_name
    {
      set
      {
        this._oauth_name = value;
      }
      get
      {
        return this._oauth_name;
      }
    }

    public string oauth_app_id
    {
      set
      {
        this._oauth_app_id = value;
      }
      get
      {
        return this._oauth_app_id;
      }
    }

    public string oauth_app_key
    {
      set
      {
        this._oauth_app_key = value;
      }
      get
      {
        return this._oauth_app_key;
      }
    }

    public string return_uri
    {
      set
      {
        this._return_uri = value;
      }
      get
      {
        return this._return_uri;
      }
    }
  }
}
