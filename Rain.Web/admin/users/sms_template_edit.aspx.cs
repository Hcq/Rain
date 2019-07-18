// Decompiled with JetBrains decompiler
// Type: Rain.Web.admin.users.sms_template_edit
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
  public class sms_template_edit : ManagePage
  {
    private string action = DTEnums.ActionEnum.Add.ToString();
    private int id = 0;
    protected HtmlForm form1;
    protected TextBox txtTitle;
    protected TextBox txtCallIndex;
    protected TextBox txtContent;
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
        if (!new Rain.BLL.sms_template().Exists(this.id))
        {
          this.JscriptMsg("记录不存在或已被删除！", "back");
          return;
        }
      }
      if (this.Page.IsPostBack)
        return;
      this.ChkAdminLevel("user_sms_template", DTEnums.ActionEnum.View.ToString());
      if (this.action == DTEnums.ActionEnum.Edit.ToString())
        this.ShowInfo(this.id);
    }

    private void ShowInfo(int _id)
    {
      Rain.Model.sms_template model = new Rain.BLL.sms_template().GetModel(_id);
      this.txtTitle.Text = model.title;
      this.txtCallIndex.Text = model.call_index;
      this.txtContent.Text = model.content;
    }

    private bool DoAdd()
    {
      Rain.Model.sms_template model = new Rain.Model.sms_template();
      Rain.BLL.sms_template smsTemplate = new Rain.BLL.sms_template();
      model.title = this.txtTitle.Text.Trim();
      model.call_index = this.txtCallIndex.Text.Trim();
      model.content = this.txtContent.Text;
      if (smsTemplate.Add(model) <= 0)
        return false;
      this.AddAdminLog(DTEnums.ActionEnum.Add.ToString(), "添加短信模板:" + model.title);
      return true;
    }

    private bool DoEdit(int _id)
    {
      bool flag = false;
      Rain.BLL.sms_template smsTemplate = new Rain.BLL.sms_template();
      Rain.Model.sms_template model = smsTemplate.GetModel(_id);
      model.title = this.txtTitle.Text.Trim();
      model.call_index = this.txtCallIndex.Text.Trim();
      model.content = this.txtContent.Text;
      if (smsTemplate.Update(model))
      {
        this.AddAdminLog(DTEnums.ActionEnum.Edit.ToString(), "修改短信模板:" + model.title);
        flag = true;
      }
      return flag;
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
      if (this.action == DTEnums.ActionEnum.Edit.ToString())
      {
        this.ChkAdminLevel("user_sms_template", DTEnums.ActionEnum.Edit.ToString());
        if (!this.DoEdit(this.id))
          this.JscriptMsg("保存过程中发生错误！", string.Empty);
        else
          this.JscriptMsg("修改短信模板成功！", "sms_template_list.aspx");
      }
      else
      {
        this.ChkAdminLevel("user_sms_template", DTEnums.ActionEnum.Add.ToString());
        if (!this.DoAdd())
          this.JscriptMsg("保存过程中发生错误！", string.Empty);
        else
          this.JscriptMsg("添加短信模板成功！", "sms_template_list.aspx");
      }
    }
  }
}
