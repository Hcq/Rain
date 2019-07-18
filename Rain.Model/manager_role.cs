// Decompiled with JetBrains decompiler
// Type: Rain.Model.manager_role
// Assembly: Rain.Model, Version=4.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 7F3024AC-7DBA-4351-8C52-F3DF08C93221
// Assembly location: C:\Users\Felix.Ho\Desktop\page\bin\Rain.Model.dll

using System;
using System.Collections.Generic;

namespace Rain.Model
{
  [Serializable]
  public class manager_role
  {
    private int _is_sys = 0;
    private int _id;
    private string _role_name;
    private int _role_type;
    private List<manager_role_value> _manager_role_values;

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

    public string role_name
    {
      set
      {
        this._role_name = value;
      }
      get
      {
        return this._role_name;
      }
    }

    public int role_type
    {
      set
      {
        this._role_type = value;
      }
      get
      {
        return this._role_type;
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

    public List<manager_role_value> manager_role_values
    {
      set
      {
        this._manager_role_values = value;
      }
      get
      {
        return this._manager_role_values;
      }
    }
  }
}
