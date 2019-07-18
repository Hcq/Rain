// Decompiled with JetBrains decompiler
// Type: Rain.Model.channel_field
// Assembly: Rain.Model, Version=4.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 7F3024AC-7DBA-4351-8C52-F3DF08C93221
// Assembly location: C:\Users\Felix.Ho\Desktop\page\bin\Rain.Model.dll

using System;

namespace Rain.Model
{
  [Serializable]
  public class channel_field
  {
    private int _id;
    private int _channel_id;
    private int _field_id;

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

    public int channel_id
    {
      set
      {
        this._channel_id = value;
      }
      get
      {
        return this._channel_id;
      }
    }

    public int field_id
    {
      set
      {
        this._field_id = value;
      }
      get
      {
        return this._field_id;
      }
    }
  }
}
