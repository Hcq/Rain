// Decompiled with JetBrains decompiler
// Type: Rain.BLL.sms_message
// Assembly: Rain.BLL, Version=4.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 8924FCAE-37FA-4301-A188-DA020D33C8EB
// Assembly location: C:\Users\Felix.Ho\Desktop\page\bin\Rain.BLL.dll

using System;
using System.Text;
using System.Text.RegularExpressions;
using Rain.Common;

namespace Rain.BLL
{
  public class sms_message
  {
    private readonly Rain.Model.siteconfig siteConfig = new siteconfig().loadConfig();

    public bool Exists()
    {
      return !string.IsNullOrEmpty(this.siteConfig.smsapiurl) && !string.IsNullOrEmpty(this.siteConfig.smsusername) && !string.IsNullOrEmpty(this.siteConfig.smspassword);
    }

    public bool Send(string mobiles, string content, int pass, out string msg)
    {
      if (!this.Exists())
      {
        msg = "短信配置参数有误，请完善后再提交！";
        return false;
      }
      int num1 = 0;
      string str = string.Empty;
      string[] strArray1 = mobiles.Split(',');
      int num2 = strArray1.Length / 2000 + 1;
      for (int index1 = 0; index1 < num2; ++index1)
      {
        StringBuilder stringBuilder = new StringBuilder();
        int num3 = 0;
        int num4 = (index1 + 1) * 2000;
        for (int index2 = 0; index2 < strArray1.Length && index2 < num4; ++index2)
        {
          int index3 = index2 + index1 * 2000;
          string pattern = "^1\\d{10}$";
          string input = strArray1[index3].Trim();
          if (new Regex(pattern, RegexOptions.IgnoreCase).Match(input) != null)
          {
            ++num3;
            stringBuilder.Append(input + ",");
          }
        }
        if (stringBuilder.ToString().Length > 0)
        {
          try
          {
            string[] strArray2 = Utils.HttpPost(this.siteConfig.smsapiurl, "cmd=tx&pass=" + (object) pass + "&uid=" + this.siteConfig.smsusername + "&pwd=" + this.siteConfig.smspassword + "&mobile=" + Utils.DelLastComma(stringBuilder.ToString()) + "&encode=utf8&content=" + Utils.UrlEncode(content)).Split(new string[1]
            {
              "||"
            }, StringSplitOptions.None);
            if (strArray2[0] != "100")
              str = "提交失败，错误提示：" + strArray2[1];
            else
              num1 += num3;
          }
          catch
          {
          }
        }
      }
      if (num1 > 0)
      {
        msg = "成功提交" + (object) num1 + "条，失败" + (object) (strArray1.Length - num1) + "条";
        return true;
      }
      msg = str;
      return false;
    }

    public int GetAccountQuantity(out string code)
    {
      if (!this.Exists())
      {
        code = "115";
        return 0;
      }
      try
      {
        string[] strArray = Utils.HttpPost(this.siteConfig.smsapiurl, "cmd=mm&uid=" + this.siteConfig.smsusername + "&pwd=" + this.siteConfig.smspassword).Split(new string[1]
        {
          "||"
        }, StringSplitOptions.None);
        if (strArray[0] != "100")
        {
          code = strArray[0];
          return 0;
        }
        code = strArray[0];
        return Utils.StrToInt(strArray[1], 0);
      }
      catch
      {
        code = "115";
        return 0;
      }
    }

    public int GetSendQuantity(out string code)
    {
      if (!this.Exists())
      {
        code = "115";
        return 0;
      }
      try
      {
        string[] strArray = Utils.HttpPost(this.siteConfig.smsapiurl, "cmd=se&uid=" + this.siteConfig.smsusername + "&pwd=" + this.siteConfig.smspassword).Split(new string[1]
        {
          "||"
        }, StringSplitOptions.None);
        if (strArray[0] != "100")
        {
          code = strArray[0];
          return 0;
        }
        code = strArray[0];
        return Utils.StrToInt(strArray[1], 0);
      }
      catch
      {
        code = "115";
        return 0;
      }
    }
  }
}
