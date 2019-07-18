// Decompiled with JetBrains decompiler
// Type: Rain.Model.userconfig
// Assembly: Rain.Model, Version=4.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 7F3024AC-7DBA-4351-8C52-F3DF08C93221
// Assembly location: C:\Users\Felix.Ho\Desktop\page\bin\Rain.Model.dll

using System;

namespace Rain.Model
{
  [Serializable]
  public class userconfig
  {
    private int _regstatus = 0;
    private int _regverify = 0;
    private int _regmsgstatus = 0;
    private string _regmsgtxt = "";
    private string _regkeywords = "";
    private int _regctrl = 0;
    private int _regsmsexpired = 0;
    private int _regemailexpired = 0;
    private int _mobilelogin = 0;
    private int _emaillogin = 0;
    private int _regrules = 0;
    private string _regrulestxt = "";
    private int _invitecodeexpired = 0;
    private int _invitecodecount = 0;
    private int _invitecodenum = 10;
    private Decimal _pointcashrate = new Decimal(0);
    private int _pointinvitenum = 0;
    private int _pointloginnum = 0;

    public int regstatus
    {
      get
      {
        return this._regstatus;
      }
      set
      {
        this._regstatus = value;
      }
    }

    public int regverify
    {
      get
      {
        return this._regverify;
      }
      set
      {
        this._regverify = value;
      }
    }

    public int regmsgstatus
    {
      get
      {
        return this._regmsgstatus;
      }
      set
      {
        this._regmsgstatus = value;
      }
    }

    public string regmsgtxt
    {
      get
      {
        return this._regmsgtxt;
      }
      set
      {
        this._regmsgtxt = value;
      }
    }

    public string regkeywords
    {
      get
      {
        return this._regkeywords;
      }
      set
      {
        this._regkeywords = value;
      }
    }

    public int regctrl
    {
      get
      {
        return this._regctrl;
      }
      set
      {
        this._regctrl = value;
      }
    }

    public int regsmsexpired
    {
      get
      {
        return this._regsmsexpired;
      }
      set
      {
        this._regsmsexpired = value;
      }
    }

    public int regemailexpired
    {
      get
      {
        return this._regemailexpired;
      }
      set
      {
        this._regemailexpired = value;
      }
    }

    public int mobilelogin
    {
      get
      {
        return this._mobilelogin;
      }
      set
      {
        this._mobilelogin = value;
      }
    }

    public int emaillogin
    {
      get
      {
        return this._emaillogin;
      }
      set
      {
        this._emaillogin = value;
      }
    }

    public int regrules
    {
      get
      {
        return this._regrules;
      }
      set
      {
        this._regrules = value;
      }
    }

    public string regrulestxt
    {
      get
      {
        return this._regrulestxt;
      }
      set
      {
        this._regrulestxt = value;
      }
    }

    public int invitecodeexpired
    {
      get
      {
        return this._invitecodeexpired;
      }
      set
      {
        this._invitecodeexpired = value;
      }
    }

    public int invitecodecount
    {
      get
      {
        return this._invitecodecount;
      }
      set
      {
        this._invitecodecount = value;
      }
    }

    public int invitecodenum
    {
      get
      {
        return this._invitecodenum;
      }
      set
      {
        this._invitecodenum = value;
      }
    }

    public Decimal pointcashrate
    {
      get
      {
        return this._pointcashrate;
      }
      set
      {
        this._pointcashrate = value;
      }
    }

    public int pointinvitenum
    {
      get
      {
        return this._pointinvitenum;
      }
      set
      {
        this._pointinvitenum = value;
      }
    }

    public int pointloginnum
    {
      get
      {
        return this._pointloginnum;
      }
      set
      {
        this._pointloginnum = value;
      }
    }
  }
}
