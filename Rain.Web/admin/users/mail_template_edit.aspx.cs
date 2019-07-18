// Decompiled with JetBrains decompiler
// Type: Rain.Web.admin.users.mail_template_edit
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
  public class mail_template_edit : ManagePage
  {
    private string action = DTEnums.ActionEnum.Add.ToString();
    private int id = 0;
    protected HtmlForm form1;
    protected TextBox txtTitle;
    protected TextBox txtCallIndex;
    protected TextBox txtMailTitle;
    protected HtmlTextArea txtContent;
    protected Button btnSubmit;

    protected void Page_Load(object sender, EventArgs e)
    {
      string queryString = DTRequest.GetQueryString("action");
      if (!string.IsNullOrEmpty(queryString) && queryString == DTEnums.ActionEnum.Edit.ToString())
      {
        this.action = DTEnums.ActionEnum.Edit.ToString();
        this.id = DTRequest.GetQueryInt("id");
        if (this.id == 0)
        {
          this.JscriptMsg("传输参数不正确！", "back");
          return;
        }
        if (!new Rain.BLL.mail_template().Exists(this.id))
        {
          this.JscriptMsg("记录不存在或已被删除！", "back");
          return;
        }
      }
      if (this.Page.IsPostBack)
        return;
      this.ChkAdminLevel("user_mail_template", DTEnums.ActionEnum.View.ToString());
      if (this.action == DTEnums.ActionEnum.Edit.ToString())
        this.ShowInfo(this.id);
    }

    private void ShowInfo(int _id)
    {
      Rain.Model.mail_template model = new Rain.BLL.mail_template().GetModel(_id);
      this.txtTitle.Text = model.title;
      this.txtCallIndex.Text = model.call_index;
      this.txtMailTitle.Text = model.maill_title;
      this.txtContent.Value = model.content;
    }

    private bool DoAdd()
    {
      Rain.Model.mail_template model = new Rain.Model.mail_template();
      Rain.BLL.mail_template mailTemplate = new Rain.BLL.mail_template();
      model.title = this.txtTitle.Text.Trim();
      model.call_index = this.txtCallIndex.Text.Trim();
      model.maill_title = this.txtMailTitle.Text.Trim();
      model.content = this.txtContent.Value;
      if (mailTemplate.Add(model) <= 0)
        return false;
      this.AddAdminLog(DTEnums.ActionEnum.Add.ToString(), "添加邮件模板:" + model.title);
      return true;
    }

    private bool DoEdit(int _id)
    {
      bool flag = false;
      Rain.BLL.mail_template mailTemplate = new Rain.BLL.mail_template();
      Rain.Model.mail_template model = mailTemplate.GetModel(_id);
      model.title = this.txtTitle.Text.Trim();
      model.call_index = this.txtCallIndex.Text.Trim();
      model.maill_title = this.txtMailTitle.Text.Trim();
      model.content = this.txtContent.Value;
      if (mailTemplate.Update(model))
      {
        this.AddAdminLog(DTEnums.ActionEnum.Edit.ToString(), "修改邮件模板:" + model.title);
        flag = true;
      }
      return flag;
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
      if (this.action == DTEnums.ActionEnum.Edit.ToString())
      {
        this.ChkAdminLevel("user_mail_template", DTEnums.ActionEnum.Edit.ToString());
        if (!this.DoEdit(this.id))
          this.JscriptMsg("保存过程中发生错误！", string.Empty);
        else
          this.JscriptMsg("修改邮件模板成功！", "mail_template_list.aspx");
      }
      else
      {
        this.ChkAdminLevel("user_mail_template", DTEnums.ActionEnum.Add.ToString());
        if (!this.DoAdd())
          this.JscriptMsg("保存过程中发生错误！", string.Empty);
        else
          this.JscriptMsg("添加邮件模板成功！", "mail_template_list.aspx");
      }
    }
  }
}
