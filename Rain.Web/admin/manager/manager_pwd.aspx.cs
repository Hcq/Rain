// Decompiled with JetBrains decompiler
// Type: Rain.Web.admin.manager.manager_pwd
// Assembly: Rain.Web, Version=4.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 7C74D3EC-D223-4A13-9304-5BE7CB5803E5
// Assembly location: C:\Users\Felix.Ho\Desktop\page\bin\Rain.Web.dll

using System;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Rain.Common;
using Rain.Web.UI;

namespace Rain.Web.admin.manager
{
  public class manager_pwd : ManagePage
  {
    protected HtmlForm form1;
    protected TextBox txtUserName;
    protected TextBox txtOldPassword;
    protected TextBox txtPassword;
    protected TextBox txtPassword1;
    protected TextBox txtRealName;
    protected TextBox txtTelephone;
    protected TextBox txtEmail;
    protected Button btnSubmit;

    protected void Page_Load(object sender, EventArgs e)
    {
      if (this.Page.IsPostBack)
        return;
      this.ShowInfo(this.GetAdminInfo().id);
    }

    private void ShowInfo(int _id)
    {
      Rain.Model.manager model = new Rain.BLL.manager().GetModel(_id);
      this.txtUserName.Text = model.user_name;
      this.txtRealName.Text = model.real_name;
      this.txtTelephone.Text = model.telephone;
      this.txtEmail.Text = model.email;
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
      Rain.BLL.manager manager = new Rain.BLL.manager();
      Rain.Model.manager adminInfo = this.GetAdminInfo();
      if (DESEncrypt.Encrypt(this.txtOldPassword.Text.Trim(), adminInfo.salt) != adminInfo.password)
        this.JscriptMsg("旧密码不正确！", "");
      else if (this.txtPassword.Text.Trim() != this.txtPassword1.Text.Trim())
      {
        this.JscriptMsg("两次密码不一致！", "");
      }
      else
      {
        adminInfo.password = DESEncrypt.Encrypt(this.txtPassword.Text.Trim(), adminInfo.salt);
        adminInfo.real_name = this.txtRealName.Text.Trim();
        adminInfo.telephone = this.txtTelephone.Text.Trim();
        adminInfo.email = this.txtEmail.Text.Trim();
        if (!manager.Update(adminInfo))
        {
          this.JscriptMsg("保存过程中发生错误！", "");
        }
        else
        {
          this.Session["dt_session_admin_info"] = (object) null;
          this.JscriptMsg("密码修改成功！", "manager_pwd.aspx");
        }
      }
    }
  }
}
