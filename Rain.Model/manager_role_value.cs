// Decompiled with JetBrains decompiler
// Type: Rain.Model.manager_role_value
// Assembly: Rain.Model, Version=4.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 7F3024AC-7DBA-4351-8C52-F3DF08C93221
// Assembly location: C:\Users\Felix.Ho\Desktop\page\bin\Rain.Model.dll

using System;

namespace Rain.Model
{
  [Serializable]
  public class manager_role_value
  {
    private int _id;
    private int _role_id;
    private string _nav_name;
    private string _action_type;

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

    public int role_id
    {
      set
      {
        this._role_id = value;
      }
      get
      {
        return this._role_id;
      }
    }

    public string nav_name
    {
      set
      {
        this._nav_name = value;
      }
      get
      {
        return this._nav_name;
      }
    }

    public string action_type
    {
      set
      {
        this._action_type = value;
      }
      get
      {
        return this._action_type;
      }
    }
  }
}
