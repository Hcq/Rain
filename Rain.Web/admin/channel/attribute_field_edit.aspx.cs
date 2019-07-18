// Decompiled with JetBrains decompiler
// Type: Rain.Web.admin.channel.attribute_field_edit
// Assembly: Rain.Web, Version=4.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 7C74D3EC-D223-4A13-9304-5BE7CB5803E5
// Assembly location: C:\Users\Felix.Ho\Desktop\page\bin\Rain.Web.dll

using System;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Rain.Common;
using Rain.Web.UI;

namespace Rain.Web.admin.channel
{
  public class attribute_field_edit : ManagePage
  {
    private string action = DTEnums.ActionEnum.Add.ToString();
    private int id = 0;
    protected HtmlForm form1;
    protected DropDownList ddlControlType;
    protected TextBox txtSortId;
    protected TextBox txtName;
    protected TextBox txtTitle;
    protected CheckBox cbIsRequired;
    protected HtmlGenericControl dlIsPassWord;
    protected CheckBox cbIsPassword;
    protected HtmlGenericControl dlIsHtml;
    protected CheckBox cbIsHtml;
    protected HtmlGenericControl dlEditorType;
    protected RadioButtonList rblEditorType;
    protected HtmlGenericControl dlDataType;
    protected RadioButtonList rblDataType;
    protected HtmlGenericControl dlDataLength;
    protected TextBox txtDataLength;
    protected HtmlGenericControl dlDataPlace;
    protected DropDownList ddlDataPlace;
    protected HtmlGenericControl dlItemOption;
    protected TextBox txtItemOption;
    protected TextBox txtDefaultValue;
    protected HtmlGenericControl dlValidPattern;
    protected TextBox txtValidPattern;
    protected TextBox txtValidTipMsg;
    protected HtmlGenericControl dlValidErrorMsg;
    protected TextBox txtValidErrorMsg;
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
        if (!new Rain.BLL.article_attribute_field().Exists(this.id))
        {
          this.JscriptMsg("记录不存在或已被删除！", "back");
          return;
        }
      }
      if (this.Page.IsPostBack)
        return;
      this.ChkAdminLevel(" sys_channel_field", DTEnums.ActionEnum.View.ToString());
      this.dlIsPassWord.Visible = this.dlIsHtml.Visible = this.dlEditorType.Visible = this.dlDataType.Visible = this.dlDataLength.Visible = this.dlDataPlace.Visible = this.dlItemOption.Visible = false;
      if (this.action == DTEnums.ActionEnum.Edit.ToString())
        this.ShowInfo(this.id);
    }

    private void ShowInfo(int _id)
    {
      Rain.Model.article_attribute_field model = new Rain.BLL.article_attribute_field().GetModel(_id);
      this.txtName.Enabled = false;
      this.txtName.Attributes.Remove("ajaxurl");
      this.txtName.Attributes.Remove("datatype");
      this.ddlControlType.SelectedValue = model.control_type;
      this.showControlHtml(model.control_type);
      this.txtSortId.Text = model.sort_id.ToString();
      this.txtName.Text = model.name;
      this.txtTitle.Text = model.title;
      this.cbIsRequired.Checked = model.is_required == 1;
      this.cbIsPassword.Checked = model.is_password == 1;
      this.cbIsHtml.Checked = model.is_html == 1;
      this.rblEditorType.SelectedValue = model.editor_type.ToString();
      this.rblDataType.SelectedValue = model.data_type;
      this.txtDataLength.Text = model.data_length.ToString();
      this.ddlDataPlace.SelectedValue = model.data_place.ToString();
      this.txtItemOption.Text = model.item_option;
      this.txtDefaultValue.Text = model.default_value;
      this.txtValidPattern.Text = model.valid_pattern;
      this.txtValidTipMsg.Text = model.valid_tip_msg;
      this.txtValidErrorMsg.Text = model.valid_error_msg;
      if (model.is_sys != 1)
        return;
      this.ddlControlType.Enabled = false;
    }

    private void showControlHtml(string control_type)
    {
      this.dlIsPassWord.Visible = this.dlIsHtml.Visible = this.dlEditorType.Visible = this.dlDataType.Visible = this.dlDataLength.Visible = this.dlDataPlace.Visible = this.dlItemOption.Visible = this.dlValidPattern.Visible = this.dlValidErrorMsg.Visible = false;
      switch (control_type)
      {
        case "single-text":
          this.dlIsPassWord.Visible = this.dlDataLength.Visible = this.dlValidPattern.Visible = this.dlValidErrorMsg.Visible = true;
          break;
        case "multi-text":
          this.dlIsHtml.Visible = this.dlDataLength.Visible = this.dlValidPattern.Visible = this.dlValidErrorMsg.Visible = true;
          break;
        case "editor":
          this.dlEditorType.Visible = this.dlValidPattern.Visible = this.dlValidErrorMsg.Visible = true;
          break;
        case "images":
          this.dlValidPattern.Visible = this.dlValidErrorMsg.Visible = true;
          break;
        case "video":
          this.dlValidPattern.Visible = this.dlValidErrorMsg.Visible = true;
          break;
        case "number":
          this.dlDataPlace.Visible = this.dlValidPattern.Visible = this.dlValidErrorMsg.Visible = true;
          break;
        case "multi-radio":
          this.dlDataType.Visible = this.dlDataLength.Visible = this.dlItemOption.Visible = true;
          break;
        case "multi-checkbox":
          this.dlDataLength.Visible = this.dlItemOption.Visible = true;
          break;
      }
    }

    private bool DoAdd()
    {
      bool flag = false;
      Rain.Model.article_attribute_field model = new Rain.Model.article_attribute_field();
      Rain.BLL.article_attribute_field articleAttributeField = new Rain.BLL.article_attribute_field();
      model.control_type = this.ddlControlType.SelectedValue;
      model.sort_id = Utils.StrToInt(this.txtSortId.Text.Trim(), 99);
      model.name = this.txtName.Text.Trim();
      model.title = this.txtTitle.Text;
      model.is_required = !this.cbIsRequired.Checked ? 0 : 1;
      model.is_password = !this.cbIsPassword.Checked ? 0 : 1;
      model.is_html = !this.cbIsHtml.Checked ? 0 : 1;
      model.editor_type = Utils.StrToInt(this.rblEditorType.SelectedValue, 0);
      model.data_length = Utils.StrToInt(this.txtDataLength.Text.Trim(), 0);
      model.data_place = Utils.StrToInt(this.ddlDataPlace.SelectedValue, 0);
      model.data_type = this.rblDataType.SelectedValue;
      model.item_option = this.txtItemOption.Text.Trim();
      model.default_value = this.txtDefaultValue.Text.Trim();
      model.valid_pattern = this.txtValidPattern.Text.Trim();
      model.valid_tip_msg = this.txtValidTipMsg.Text.Trim();
      model.valid_error_msg = this.txtValidErrorMsg.Text.Trim();
      if (articleAttributeField.Add(model) > 0)
      {
        this.AddAdminLog(DTEnums.ActionEnum.Add.ToString(), "添加扩展字段:" + model.title);
        flag = true;
      }
      return flag;
    }

    private bool DoEdit(int _id)
    {
      bool flag = false;
      Rain.BLL.article_attribute_field articleAttributeField = new Rain.BLL.article_attribute_field();
      Rain.Model.article_attribute_field model = articleAttributeField.GetModel(_id);
      if (model.is_sys == 0)
      {
        model.control_type = this.ddlControlType.SelectedValue;
        model.data_length = Utils.StrToInt(this.txtDataLength.Text.Trim(), 0);
        model.data_place = Utils.StrToInt(this.ddlDataPlace.SelectedValue, 0);
        model.data_type = this.rblDataType.SelectedValue;
      }
      model.sort_id = Utils.StrToInt(this.txtSortId.Text.Trim(), 99);
      model.title = this.txtTitle.Text;
      model.is_required = !this.cbIsRequired.Checked ? 0 : 1;
      model.is_password = !this.cbIsPassword.Checked ? 0 : 1;
      model.is_html = !this.cbIsHtml.Checked ? 0 : 1;
      model.editor_type = Utils.StrToInt(this.rblEditorType.SelectedValue, 0);
      model.item_option = this.txtItemOption.Text.Trim();
      model.default_value = this.txtDefaultValue.Text.Trim();
      model.valid_pattern = this.txtValidPattern.Text.Trim();
      model.valid_tip_msg = this.txtValidTipMsg.Text.Trim();
      model.valid_error_msg = this.txtValidErrorMsg.Text.Trim();
      if (articleAttributeField.Update(model))
      {
        this.AddAdminLog(DTEnums.ActionEnum.Add.ToString(), "修改扩展字段:" + model.title);
        flag = true;
      }
      return flag;
    }

    protected void ddlControlType_SelectedIndexChanged(object sender, EventArgs e)
    {
      this.showControlHtml(this.ddlControlType.SelectedValue);
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
      if (this.action == DTEnums.ActionEnum.Edit.ToString())
      {
        this.ChkAdminLevel("sys_channel_field", DTEnums.ActionEnum.Edit.ToString());
        if (!this.DoEdit(this.id))
          this.JscriptMsg("保存过程中发生错误！", "");
        else
          this.JscriptMsg("修改扩展字段成功！", "attribute_field_list.aspx");
      }
      else
      {
        this.ChkAdminLevel("sys_channel_field", DTEnums.ActionEnum.Add.ToString());
        if (!this.DoAdd())
          this.JscriptMsg("保存过程中发生错误！", "");
        else
          this.JscriptMsg("添加扩展字段成功！", "attribute_field_list.aspx");
      }
    }
  }
}
