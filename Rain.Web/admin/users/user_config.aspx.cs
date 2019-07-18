// Decompiled with JetBrains decompiler
// Type: Rain.Web.admin.users.user_config
// Assembly: Rain.Web, Version=4.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 7C74D3EC-D223-4A13-9304-5BE7CB5803E5
// Assembly location: C:\Users\Felix.Ho\Desktop\page\bin\Rain.Web.dll

using System;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Rain.Common;
using Rain.Web.UI;

namespace Rain.Web.admin.users
{
  public class user_config : ManagePage
  {
    protected HtmlForm form1;
    protected RadioButtonList regstatus;
    protected CheckBox regverify;
    protected RadioButtonList regmsgstatus;
    protected TextBox regmsgtxt;
    protected TextBox regkeywords;
    protected TextBox regctrl;
    protected TextBox regsmsexpired;
    protected TextBox regemailexpired;
    protected CheckBox mobilelogin;
    protected CheckBox emaillogin;
    protected CheckBox regrules;
    protected TextBox regrulestxt;
    protected TextBox invitecodeexpired;
    protected TextBox invitecodecount;
    protected TextBox invitecodenum;
    protected TextBox pointcashrate;
    protected TextBox pointinvitenum;
    protected TextBox pointloginnum;
    protected Button btnSubmit;

    protected void Page_Load(object sender, EventArgs e)
    {
      if (this.Page.IsPostBack)
        return;
      this.ChkAdminLevel(nameof (user_config), DTEnums.ActionEnum.View.ToString());
      this.ShowInfo();
    }

    private void ShowInfo()
    {
      Rain.Model.userconfig userconfig = new Rain.BLL.userconfig().loadConfig();
      this.regstatus.SelectedValue = userconfig.regstatus.ToString();
      this.regmsgstatus.SelectedValue = userconfig.regmsgstatus.ToString();
      this.regmsgtxt.Text = userconfig.regmsgtxt;
      this.regkeywords.Text = userconfig.regkeywords;
      this.regctrl.Text = userconfig.regctrl.ToString();
      this.regsmsexpired.Text = userconfig.regsmsexpired.ToString();
      this.regemailexpired.Text = userconfig.regemailexpired.ToString();
      this.regverify.Checked = userconfig.regverify == 1;
      this.mobilelogin.Checked = userconfig.mobilelogin == 1;
      this.emaillogin.Checked = userconfig.emaillogin == 1;
      this.regrules.Checked = userconfig.regrules == 1;
      this.regrulestxt.Text = userconfig.regrulestxt;
      this.invitecodeexpired.Text = userconfig.invitecodeexpired.ToString();
      this.invitecodecount.Text = userconfig.invitecodecount.ToString();
      TextBox invitecodenum = this.invitecodenum;
      int num = userconfig.invitecodenum;
      string str1 = num.ToString();
      invitecodenum.Text = str1;
      this.pointcashrate.Text = userconfig.pointcashrate.ToString();
      TextBox pointinvitenum = this.pointinvitenum;
      num = userconfig.pointinvitenum;
      string str2 = num.ToString();
      pointinvitenum.Text = str2;
      TextBox pointloginnum = this.pointloginnum;
      num = userconfig.pointloginnum;
      string str3 = num.ToString();
      pointloginnum.Text = str3;
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
      this.ChkAdminLevel(nameof (user_config), DTEnums.ActionEnum.Edit.ToString());
      Rain.BLL.userconfig userconfig = new Rain.BLL.userconfig();
      Rain.Model.userconfig model = userconfig.loadConfig();
      try
      {
        model.regstatus = Utils.StrToInt(this.regstatus.SelectedValue, 0);
        model.regmsgstatus = Utils.StrToInt(this.regmsgstatus.SelectedValue, 0);
        model.regmsgtxt = this.regmsgtxt.Text;
        model.regkeywords = this.regkeywords.Text.Trim();
        model.regctrl = Utils.StrToInt(this.regctrl.Text.Trim(), 0);
        model.regsmsexpired = Utils.StrToInt(this.regsmsexpired.Text.Trim(), 0);
        model.regemailexpired = Utils.StrToInt(this.regemailexpired.Text.Trim(), 0);
        model.regverify = !this.regverify.Checked ? 0 : 1;
        model.mobilelogin = !this.mobilelogin.Checked ? 0 : 1;
        model.emaillogin = !this.emaillogin.Checked ? 0 : 1;
        model.regrules = !this.regrules.Checked ? 0 : 1;
        model.regrulestxt = this.regrulestxt.Text;
        model.invitecodeexpired = Utils.StrToInt(this.invitecodeexpired.Text.Trim(), 1);
        model.invitecodecount = Utils.StrToInt(this.invitecodecount.Text.Trim(), 0);
        model.invitecodenum = Utils.StrToInt(this.invitecodenum.Text.Trim(), 0);
        model.pointcashrate = Utils.StrToDecimal(this.pointcashrate.Text.Trim(), new Decimal(0));
        model.pointinvitenum = Utils.StrToInt(this.pointinvitenum.Text.Trim(), 0);
        model.pointloginnum = Utils.StrToInt(this.pointloginnum.Text.Trim(), 0);
        userconfig.saveConifg(model);
        this.AddAdminLog(DTEnums.ActionEnum.Edit.ToString(), "修改用户配置信息");
        this.JscriptMsg("修改用户配置成功！", "user_config.aspx");
      }
      catch
      {
        this.JscriptMsg("文件写入失败，请检查是否有权限！", string.Empty);
      }
    }
  }
}
