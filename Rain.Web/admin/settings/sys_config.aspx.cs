// Decompiled with JetBrains decompiler
// Type: Rain.Web.admin.settings.sys_config
// Assembly: Rain.Web, Version=4.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 7C74D3EC-D223-4A13-9304-5BE7CB5803E5
// Assembly location: C:\Users\Felix.Ho\Desktop\page\bin\Rain.Web.dll

using System;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Rain.BLL;
using Rain.Common;
using Rain.Web.UI;

namespace Rain.Web.admin.settings
{
  public class sys_config : ManagePage
  {
    private string defaultpassword = "0|0|0|0";
    protected HtmlForm form1;
    protected TextBox webname;
    protected TextBox weburl;
    protected TextBox webcompany;
    protected TextBox webaddress;
    protected TextBox webtel;
    protected TextBox webfax;
    protected TextBox webmail;
    protected TextBox webcrod;
    protected TextBox webpath;
    protected TextBox webmanagepath;
    protected RadioButtonList staticstatus;
    protected TextBox staticextension;
    protected CheckBox memberstatus;
    protected CheckBox commentstatus;
    protected CheckBox logstatus;
    protected CheckBox webstatus;
    protected TextBox webclosereason;
    protected TextBox webcountcode;
    protected Label labSmsCount;
    protected TextBox smsapiurl;
    protected TextBox smsusername;
    protected TextBox smspassword;
    protected TextBox emailsmtp;
    protected CheckBox emailssl;
    protected TextBox emailport;
    protected TextBox emailfrom;
    protected TextBox emailusername;
    protected TextBox emailpassword;
    protected TextBox emailnickname;
    protected TextBox filepath;
    protected DropDownList filesave;
    protected TextBox fileextension;
    protected TextBox videoextension;
    protected TextBox attachsize;
    protected TextBox videosize;
    protected TextBox imgsize;
    protected TextBox imgmaxheight;
    protected TextBox imgmaxwidth;
    protected TextBox thumbnailheight;
    protected TextBox thumbnailwidth;
    protected RadioButtonList watermarktype;
    protected RadioButtonList watermarkposition;
    protected TextBox watermarkimgquality;
    protected TextBox watermarkpic;
    protected TextBox watermarktransparency;
    protected TextBox watermarktext;
    protected DropDownList watermarkfont;
    protected TextBox watermarkfontsize;
    protected Button btnSubmit;

    protected void Page_Load(object sender, EventArgs e)
    {
      if (this.Page.IsPostBack)
        return;
      this.ChkAdminLevel(nameof (sys_config), DTEnums.ActionEnum.View.ToString());
      this.ShowInfo();
    }

    private void ShowInfo()
    {
      Rain.Model.siteconfig siteconfig = new Rain.BLL.siteconfig().loadConfig();
      this.webname.Text = siteconfig.webname;
      this.weburl.Text = siteconfig.weburl;
      this.webcompany.Text = siteconfig.webcompany;
      this.webaddress.Text = siteconfig.webaddress;
      this.webtel.Text = siteconfig.webtel;
      this.webfax.Text = siteconfig.webfax;
      this.webmail.Text = siteconfig.webmail;
      this.webcrod.Text = siteconfig.webcrod;
      this.webpath.Text = siteconfig.webpath;
      this.webmanagepath.Text = siteconfig.webmanagepath;
      this.staticstatus.SelectedValue = siteconfig.staticstatus.ToString();
      this.staticextension.Text = siteconfig.staticextension;
      this.memberstatus.Checked = siteconfig.memberstatus == 1;
      this.commentstatus.Checked = siteconfig.commentstatus == 1;
      this.logstatus.Checked = siteconfig.logstatus == 1;
      this.webstatus.Checked = siteconfig.webstatus == 1;
      this.webclosereason.Text = siteconfig.webclosereason;
      this.webcountcode.Text = siteconfig.webcountcode;
      this.smsapiurl.Text = siteconfig.smsapiurl;
      this.smsusername.Text = siteconfig.smsusername;
      if (!string.IsNullOrEmpty(siteconfig.smspassword))
        this.smspassword.Attributes["value"] = this.defaultpassword;
      this.labSmsCount.Text = this.GetSmsCount();
      this.emailsmtp.Text = siteconfig.emailsmtp;
      this.emailssl.Checked = siteconfig.emailssl == 1;
      TextBox emailport = this.emailport;
      int num = siteconfig.emailport;
      string str1 = num.ToString();
      emailport.Text = str1;
      this.emailfrom.Text = siteconfig.emailfrom;
      this.emailusername.Text = siteconfig.emailusername;
      if (!string.IsNullOrEmpty(siteconfig.emailpassword))
        this.emailpassword.Attributes["value"] = this.defaultpassword;
      this.emailnickname.Text = siteconfig.emailnickname;
      this.filepath.Text = siteconfig.filepath;
      DropDownList filesave = this.filesave;
      num = siteconfig.filesave;
      string str2 = num.ToString();
      filesave.SelectedValue = str2;
      this.fileextension.Text = siteconfig.fileextension;
      this.videoextension.Text = siteconfig.videoextension;
      TextBox attachsize = this.attachsize;
      num = siteconfig.attachsize;
      string str3 = num.ToString();
      attachsize.Text = str3;
      TextBox videosize = this.videosize;
      num = siteconfig.videosize;
      string str4 = num.ToString();
      videosize.Text = str4;
      TextBox imgsize = this.imgsize;
      num = siteconfig.imgsize;
      string str5 = num.ToString();
      imgsize.Text = str5;
      TextBox imgmaxheight = this.imgmaxheight;
      num = siteconfig.imgmaxheight;
      string str6 = num.ToString();
      imgmaxheight.Text = str6;
      TextBox imgmaxwidth = this.imgmaxwidth;
      num = siteconfig.imgmaxwidth;
      string str7 = num.ToString();
      imgmaxwidth.Text = str7;
      TextBox thumbnailheight = this.thumbnailheight;
      num = siteconfig.thumbnailheight;
      string str8 = num.ToString();
      thumbnailheight.Text = str8;
      TextBox thumbnailwidth = this.thumbnailwidth;
      num = siteconfig.thumbnailwidth;
      string str9 = num.ToString();
      thumbnailwidth.Text = str9;
      RadioButtonList watermarktype = this.watermarktype;
      num = siteconfig.watermarktype;
      string str10 = num.ToString();
      watermarktype.SelectedValue = str10;
      RadioButtonList watermarkposition = this.watermarkposition;
      num = siteconfig.watermarkposition;
      string str11 = num.ToString();
      watermarkposition.Text = str11;
      TextBox watermarkimgquality = this.watermarkimgquality;
      num = siteconfig.watermarkimgquality;
      string str12 = num.ToString();
      watermarkimgquality.Text = str12;
      this.watermarkpic.Text = siteconfig.watermarkpic;
      TextBox watermarktransparency = this.watermarktransparency;
      num = siteconfig.watermarktransparency;
      string str13 = num.ToString();
      watermarktransparency.Text = str13;
      this.watermarktext.Text = siteconfig.watermarktext;
      this.watermarkfont.SelectedValue = siteconfig.watermarkfont;
      TextBox watermarkfontsize = this.watermarkfontsize;
      num = siteconfig.watermarkfontsize;
      string str14 = num.ToString();
      watermarkfontsize.Text = str14;
    }

    private string GetSmsCount()
    {
      string code = string.Empty;
      int accountQuantity = new sms_message().GetAccountQuantity(out code);
      if (code == "115")
        return "查询出错：请完善账户信息";
      if (code != "100")
        return "错误代码：" + code;
      return accountQuantity.ToString() + " 条";
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
      this.ChkAdminLevel(nameof (sys_config), DTEnums.ActionEnum.Edit.ToString());
      Rain.BLL.siteconfig siteconfig = new Rain.BLL.siteconfig();
      Rain.Model.siteconfig model = siteconfig.loadConfig();
      try
      {
        model.webname = this.webname.Text;
        model.weburl = this.weburl.Text;
        model.webcompany = this.webcompany.Text;
        model.webaddress = this.webaddress.Text;
        model.webtel = this.webtel.Text;
        model.webfax = this.webfax.Text;
        model.webmail = this.webmail.Text;
        model.webcrod = this.webcrod.Text;
        model.webpath = this.webpath.Text;
        model.webmanagepath = this.webmanagepath.Text;
        model.staticstatus = Utils.StrToInt(this.staticstatus.SelectedValue, 0);
        model.staticextension = this.staticextension.Text;
        model.memberstatus = !this.memberstatus.Checked ? 0 : 1;
        model.commentstatus = !this.commentstatus.Checked ? 0 : 1;
        model.logstatus = !this.logstatus.Checked ? 0 : 1;
        model.webstatus = !this.webstatus.Checked ? 0 : 1;
        model.webclosereason = this.webclosereason.Text;
        model.webcountcode = this.webcountcode.Text;
        model.smsapiurl = this.smsapiurl.Text;
        model.smsusername = this.smsusername.Text;
        if (this.smspassword.Text.Trim() != "" && this.smspassword.Text.Trim() != this.defaultpassword)
          model.smspassword = Utils.MD5(this.smspassword.Text.Trim());
        model.emailsmtp = this.emailsmtp.Text;
        model.emailssl = !this.emailssl.Checked ? 0 : 1;
        model.emailport = Utils.StrToInt(this.emailport.Text.Trim(), 25);
        model.emailfrom = this.emailfrom.Text;
        model.emailusername = this.emailusername.Text;
        if (this.emailpassword.Text.Trim() != this.defaultpassword)
          model.emailpassword = DESEncrypt.Encrypt(this.emailpassword.Text, model.sysencryptstring);
        model.emailnickname = this.emailnickname.Text;
        model.filepath = this.filepath.Text;
        model.filesave = Utils.StrToInt(this.filesave.SelectedValue, 2);
        model.fileextension = this.fileextension.Text;
        model.videoextension = this.videoextension.Text;
        model.attachsize = Utils.StrToInt(this.attachsize.Text.Trim(), 0);
        model.videosize = Utils.StrToInt(this.videosize.Text.Trim(), 0);
        model.imgsize = Utils.StrToInt(this.imgsize.Text.Trim(), 0);
        model.imgmaxheight = Utils.StrToInt(this.imgmaxheight.Text.Trim(), 0);
        model.imgmaxwidth = Utils.StrToInt(this.imgmaxwidth.Text.Trim(), 0);
        model.thumbnailheight = Utils.StrToInt(this.thumbnailheight.Text.Trim(), 0);
        model.thumbnailwidth = Utils.StrToInt(this.thumbnailwidth.Text.Trim(), 0);
        model.watermarktype = Utils.StrToInt(this.watermarktype.SelectedValue, 0);
        model.watermarkposition = Utils.StrToInt(this.watermarkposition.Text.Trim(), 9);
        model.watermarkimgquality = Utils.StrToInt(this.watermarkimgquality.Text.Trim(), 80);
        model.watermarkpic = this.watermarkpic.Text;
        model.watermarktransparency = Utils.StrToInt(this.watermarktransparency.Text.Trim(), 5);
        model.watermarktext = this.watermarktext.Text;
        model.watermarkfont = this.watermarkfont.Text;
        model.watermarkfontsize = Utils.StrToInt(this.watermarkfontsize.Text.Trim(), 12);
        siteconfig.saveConifg(model);
        this.AddAdminLog(DTEnums.ActionEnum.Edit.ToString(), "修改系统配置信息");
        this.JscriptMsg("修改系统配置成功！", "sys_config.aspx");
      }
      catch
      {
        this.JscriptMsg("文件写入失败，请检查文件夹权限！", "");
      }
    }
  }
}
