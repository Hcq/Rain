// Decompiled with JetBrains decompiler
// Type: Rain.Model.siteconfig
// Assembly: Rain.Model, Version=4.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 7F3024AC-7DBA-4351-8C52-F3DF08C93221
// Assembly location: C:\Users\Felix.Ho\Desktop\page\bin\Rain.Model.dll

using System;

namespace Rain.Model
{
  [Serializable]
  public class siteconfig
  {
    private string _webname = "";
    private string _weburl = "";
    private string _webcompany = "";
    private string _webaddress = "";
    private string _webtel = "";
    private string _webfax = "";
    private string _webmail = "";
    private string _webcrod = "";
    private string _webpath = "";
    private string _webmanagepath = "";
    private int _staticstatus = 0;
    private string _staticextension = "";
    private int _memberstatus = 1;
    private int _commentstatus = 0;
    private int _logstatus = 0;
    private int _webstatus = 1;
    private string _webclosereason = "";
    private string _webcountcode = "";
    private string _smsapiurl = "";
    private string _smsusername = "";
    private string _smspassword = "";
    private string _emailsmtp = "";
    private int _emailssl = 0;
    private int _emailport = 25;
    private string _emailfrom = "";
    private string _emailusername = "";
    private string _emailpassword = "";
    private string _emailnickname = "";
    private string _filepath = "";
    private int _filesave = 1;
    private string _fileextension = "";
    private string _videoextension = "";
    private int _attachsize = 0;
    private int _videosize = 0;
    private int _imgsize = 0;
    private int _imgmaxheight = 0;
    private int _imgmaxwidth = 0;
    private int _thumbnailheight = 0;
    private int _thumbnailwidth = 0;
    private int _watermarktype = 0;
    private int _watermarkposition = 9;
    private int _watermarkimgquality = 80;
    private string _watermarkpic = "";
    private int _watermarktransparency = 10;
    private string _watermarktext = "";
    private string _watermarkfont = "";
    private int _watermarkfontsize = 12;
    private string _sysdatabaseprefix = "dt_";
    private string _sysencryptstring = "Rain";

    public string webname
    {
      get
      {
        return this._webname;
      }
      set
      {
        this._webname = value;
      }
    }

    public string weburl
    {
      get
      {
        return this._weburl;
      }
      set
      {
        this._weburl = value;
      }
    }

    public string webcompany
    {
      get
      {
        return this._webcompany;
      }
      set
      {
        this._webcompany = value;
      }
    }

    public string webaddress
    {
      get
      {
        return this._webaddress;
      }
      set
      {
        this._webaddress = value;
      }
    }

    public string webtel
    {
      get
      {
        return this._webtel;
      }
      set
      {
        this._webtel = value;
      }
    }

    public string webfax
    {
      get
      {
        return this._webfax;
      }
      set
      {
        this._webfax = value;
      }
    }

    public string webmail
    {
      get
      {
        return this._webmail;
      }
      set
      {
        this._webmail = value;
      }
    }

    public string webcrod
    {
      get
      {
        return this._webcrod;
      }
      set
      {
        this._webcrod = value;
      }
    }

    public string webpath
    {
      get
      {
        return this._webpath;
      }
      set
      {
        this._webpath = value;
      }
    }

    public string webmanagepath
    {
      get
      {
        return this._webmanagepath;
      }
      set
      {
        this._webmanagepath = value;
      }
    }

    public int staticstatus
    {
      get
      {
        return this._staticstatus;
      }
      set
      {
        this._staticstatus = value;
      }
    }

    public string staticextension
    {
      get
      {
        return this._staticextension;
      }
      set
      {
        this._staticextension = value;
      }
    }

    public int memberstatus
    {
      get
      {
        return this._memberstatus;
      }
      set
      {
        this._memberstatus = value;
      }
    }

    public int commentstatus
    {
      get
      {
        return this._commentstatus;
      }
      set
      {
        this._commentstatus = value;
      }
    }

    public int logstatus
    {
      get
      {
        return this._logstatus;
      }
      set
      {
        this._logstatus = value;
      }
    }

    public int webstatus
    {
      get
      {
        return this._webstatus;
      }
      set
      {
        this._webstatus = value;
      }
    }

    public string webclosereason
    {
      get
      {
        return this._webclosereason;
      }
      set
      {
        this._webclosereason = value;
      }
    }

    public string webcountcode
    {
      get
      {
        return this._webcountcode;
      }
      set
      {
        this._webcountcode = value;
      }
    }

    public string smsapiurl
    {
      get
      {
        return this._smsapiurl;
      }
      set
      {
        this._smsapiurl = value;
      }
    }

    public string smsusername
    {
      get
      {
        return this._smsusername;
      }
      set
      {
        this._smsusername = value;
      }
    }

    public string smspassword
    {
      get
      {
        return this._smspassword;
      }
      set
      {
        this._smspassword = value;
      }
    }

    public string emailsmtp
    {
      get
      {
        return this._emailsmtp;
      }
      set
      {
        this._emailsmtp = value;
      }
    }

    public int emailssl
    {
      get
      {
        return this._emailssl;
      }
      set
      {
        this._emailssl = value;
      }
    }

    public int emailport
    {
      get
      {
        return this._emailport;
      }
      set
      {
        this._emailport = value;
      }
    }

    public string emailfrom
    {
      get
      {
        return this._emailfrom;
      }
      set
      {
        this._emailfrom = value;
      }
    }

    public string emailusername
    {
      get
      {
        return this._emailusername;
      }
      set
      {
        this._emailusername = value;
      }
    }

    public string emailpassword
    {
      get
      {
        return this._emailpassword;
      }
      set
      {
        this._emailpassword = value;
      }
    }

    public string emailnickname
    {
      get
      {
        return this._emailnickname;
      }
      set
      {
        this._emailnickname = value;
      }
    }

    public string filepath
    {
      get
      {
        return this._filepath;
      }
      set
      {
        this._filepath = value;
      }
    }

    public int filesave
    {
      get
      {
        return this._filesave;
      }
      set
      {
        this._filesave = value;
      }
    }

    public string fileextension
    {
      get
      {
        return this._fileextension;
      }
      set
      {
        this._fileextension = value;
      }
    }

    public string videoextension
    {
      get
      {
        return this._videoextension;
      }
      set
      {
        this._videoextension = value;
      }
    }

    public int attachsize
    {
      get
      {
        return this._attachsize;
      }
      set
      {
        this._attachsize = value;
      }
    }

    public int videosize
    {
      get
      {
        return this._videosize;
      }
      set
      {
        this._videosize = value;
      }
    }

    public int imgsize
    {
      get
      {
        return this._imgsize;
      }
      set
      {
        this._imgsize = value;
      }
    }

    public int imgmaxheight
    {
      get
      {
        return this._imgmaxheight;
      }
      set
      {
        this._imgmaxheight = value;
      }
    }

    public int imgmaxwidth
    {
      get
      {
        return this._imgmaxwidth;
      }
      set
      {
        this._imgmaxwidth = value;
      }
    }

    public int thumbnailheight
    {
      get
      {
        return this._thumbnailheight;
      }
      set
      {
        this._thumbnailheight = value;
      }
    }

    public int thumbnailwidth
    {
      get
      {
        return this._thumbnailwidth;
      }
      set
      {
        this._thumbnailwidth = value;
      }
    }

    public int watermarktype
    {
      get
      {
        return this._watermarktype;
      }
      set
      {
        this._watermarktype = value;
      }
    }

    public int watermarkposition
    {
      get
      {
        return this._watermarkposition;
      }
      set
      {
        this._watermarkposition = value;
      }
    }

    public int watermarkimgquality
    {
      get
      {
        return this._watermarkimgquality;
      }
      set
      {
        this._watermarkimgquality = value;
      }
    }

    public string watermarkpic
    {
      get
      {
        return this._watermarkpic;
      }
      set
      {
        this._watermarkpic = value;
      }
    }

    public int watermarktransparency
    {
      get
      {
        return this._watermarktransparency;
      }
      set
      {
        this._watermarktransparency = value;
      }
    }

    public string watermarktext
    {
      get
      {
        return this._watermarktext;
      }
      set
      {
        this._watermarktext = value;
      }
    }

    public string watermarkfont
    {
      get
      {
        return this._watermarkfont;
      }
      set
      {
        this._watermarkfont = value;
      }
    }

    public int watermarkfontsize
    {
      get
      {
        return this._watermarkfontsize;
      }
      set
      {
        this._watermarkfontsize = value;
      }
    }

    public string sysdatabaseprefix
    {
      get
      {
        return this._sysdatabaseprefix;
      }
      set
      {
        this._sysdatabaseprefix = value;
      }
    }

    public string sysencryptstring
    {
      get
      {
        return this._sysencryptstring;
      }
      set
      {
        this._sysencryptstring = value;
      }
    }
  }
}
