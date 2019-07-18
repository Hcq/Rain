// Decompiled with JetBrains decompiler
// Type: Rain.Web.admin.settings.nav_edit
// Assembly: Rain.Web, Version=4.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 7C74D3EC-D223-4A13-9304-5BE7CB5803E5
// Assembly location: C:\Users\Felix.Ho\Desktop\page\bin\Rain.Web.dll

using System;
using System.Collections.Generic;
using System.Data;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Rain.Common;
using Rain.Web.UI;

namespace Rain.Web.admin.settings
{
  public class nav_edit : ManagePage
  {
    private string action = DTEnums.ActionEnum.Add.ToString();
    private int id = 0;
    protected HtmlForm form1;
    protected DropDownList ddlParentId;
    protected TextBox txtSortId;
    protected CheckBox cbIsLock;
    protected TextBox txtName;
    protected TextBox txtTitle;
    protected TextBox txtSubTitle;
    protected TextBox txtIconUrl;
    protected TextBox txtLinkUrl;
    protected TextBox txtRemark;
    protected CheckBoxList cblActionType;
    protected Button btnSubmit;

    protected void Page_Load(object sender, EventArgs e)
    {
      string queryString = DTRequest.GetQueryString("action");
      this.id = DTRequest.GetQueryInt("id");
      if (!string.IsNullOrEmpty(queryString) && queryString == DTEnums.ActionEnum.Edit.ToString())
      {
        this.action = DTEnums.ActionEnum.Edit.ToString();
        if (this.id == 0)
        {
          this.JscriptMsg("传输参数不正确！", "back");
          return;
        }
        if (!new Rain.BLL.navigation().Exists(this.id))
        {
          this.JscriptMsg("导航不存在或已被删除！", "back");
          return;
        }
      }
      if (this.Page.IsPostBack)
        return;
      this.ChkAdminLevel("sys_navigation", DTEnums.ActionEnum.View.ToString());
      this.TreeBind(DTEnums.NavigationEnum.System.ToString());
      this.ActionTypeBind();
      if (this.action == DTEnums.ActionEnum.Edit.ToString())
      {
        this.ShowInfo(this.id);
      }
      else
      {
        if (this.id > 0)
          this.ddlParentId.SelectedValue = this.id.ToString();
        this.txtName.Attributes.Add("ajaxurl", "../../tools/admin_ajax.ashx?action=navigation_validate");
      }
    }

    private void TreeBind(string nav_type)
    {
      DataTable list = new Rain.BLL.navigation().GetList(0, nav_type);
      this.ddlParentId.Items.Clear();
      this.ddlParentId.Items.Add(new ListItem("无父级导航", "0"));
      foreach (DataRow row in (InternalDataCollectionBase) list.Rows)
      {
        string str1 = row["id"].ToString();
        int num = int.Parse(row["class_layer"].ToString());
        string text = row["title"].ToString().Trim();
        if (num == 1)
        {
          this.ddlParentId.Items.Add(new ListItem(text, str1));
        }
        else
        {
          string str2 = "├ " + text;
          this.ddlParentId.Items.Add(new ListItem(Utils.StringOfChar(num - 1, "　") + str2, str1));
        }
      }
    }

    private void ActionTypeBind()
    {
      this.cblActionType.Items.Clear();
      foreach (KeyValuePair<string, string> keyValuePair in Utils.ActionType())
        this.cblActionType.Items.Add(new ListItem(keyValuePair.Value + "(" + keyValuePair.Key + ")", keyValuePair.Key));
    }

    private void ShowInfo(int _id)
    {
      Rain.Model.navigation model = new Rain.BLL.navigation().GetModel(_id);
      this.ddlParentId.SelectedValue = model.parent_id.ToString();
      this.txtSortId.Text = model.sort_id.ToString();
      if (model.is_lock == 1)
        this.cbIsLock.Checked = true;
      this.txtName.Text = model.name;
      this.txtName.Attributes.Add("ajaxurl", "../../tools/admin_ajax.ashx?action=navigation_validate&old_name=" + Utils.UrlEncode(model.name));
      this.txtName.Focus();
      if (model.is_sys == 1)
      {
        this.ddlParentId.Enabled = false;
        this.txtName.ReadOnly = true;
      }
      this.txtTitle.Text = model.title;
      this.txtSubTitle.Text = model.sub_title;
      this.txtIconUrl.Text = model.icon_url;
      this.txtLinkUrl.Text = model.link_url;
      this.txtRemark.Text = model.remark;
      string[] strArray = model.action_type.Split(',');
      for (int index1 = 0; index1 < this.cblActionType.Items.Count; ++index1)
      {
        for (int index2 = 0; index2 < strArray.Length; ++index2)
        {
          if (strArray[index2].ToLower() == this.cblActionType.Items[index1].Value.ToLower())
            this.cblActionType.Items[index1].Selected = true;
        }
      }
    }

    private bool DoAdd()
    {
      try
      {
        Rain.Model.navigation model = new Rain.Model.navigation();
        Rain.BLL.navigation navigation = new Rain.BLL.navigation();
        model.nav_type = DTEnums.NavigationEnum.System.ToString();
        model.name = this.txtName.Text.Trim();
        model.title = this.txtTitle.Text.Trim();
        model.sub_title = this.txtSubTitle.Text.Trim();
        model.icon_url = this.txtIconUrl.Text.Trim();
        model.link_url = this.txtLinkUrl.Text.Trim();
        model.sort_id = int.Parse(this.txtSortId.Text.Trim());
        model.is_lock = 0;
        if (this.cbIsLock.Checked)
          model.is_lock = 1;
        model.remark = this.txtRemark.Text.Trim();
        model.parent_id = int.Parse(this.ddlParentId.SelectedValue);
        string str = string.Empty;
        for (int index = 0; index < this.cblActionType.Items.Count; ++index)
        {
          if (this.cblActionType.Items[index].Selected && Utils.ActionType().ContainsKey(this.cblActionType.Items[index].Value))
            str = str + this.cblActionType.Items[index].Value + ",";
        }
        model.action_type = Utils.DelLastComma(str);
        if (navigation.Add(model) > 0)
        {
          this.AddAdminLog(DTEnums.ActionEnum.Add.ToString(), "添加导航菜单:" + model.title);
          return true;
        }
      }
      catch
      {
        return false;
      }
      return false;
    }

    private bool DoEdit(int _id)
    {
      try
      {
        Rain.BLL.navigation navigation = new Rain.BLL.navigation();
        Rain.Model.navigation model = navigation.GetModel(_id);
        model.name = this.txtName.Text.Trim();
        model.title = this.txtTitle.Text.Trim();
        model.sub_title = this.txtSubTitle.Text.Trim();
        model.icon_url = this.txtIconUrl.Text.Trim();
        model.link_url = this.txtLinkUrl.Text.Trim();
        model.sort_id = int.Parse(this.txtSortId.Text.Trim());
        model.is_lock = 0;
        if (this.cbIsLock.Checked)
          model.is_lock = 1;
        model.remark = this.txtRemark.Text.Trim();
        if (model.is_sys == 0)
        {
          int num = int.Parse(this.ddlParentId.SelectedValue);
          if (num != model.id)
            model.parent_id = num;
        }
        string str = string.Empty;
        for (int index = 0; index < this.cblActionType.Items.Count; ++index)
        {
          if (this.cblActionType.Items[index].Selected && Utils.ActionType().ContainsKey(this.cblActionType.Items[index].Value))
            str = str + this.cblActionType.Items[index].Value + ",";
        }
        model.action_type = Utils.DelLastComma(str);
        if (navigation.Update(model))
        {
          this.AddAdminLog(DTEnums.ActionEnum.Add.ToString(), "修改导航菜单:" + model.title);
          return true;
        }
      }
      catch
      {
        return false;
      }
      return false;
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
      if (this.action == DTEnums.ActionEnum.Edit.ToString())
      {
        this.ChkAdminLevel("sys_navigation", DTEnums.ActionEnum.Edit.ToString());
        if (!this.DoEdit(this.id))
          this.JscriptMsg("保存过程中发生错误！", "");
        else
          this.JscriptMsg("修改导航菜单成功！", "nav_list.aspx", "parent.loadMenuTree");
      }
      else
      {
        this.ChkAdminLevel("sys_navigation", DTEnums.ActionEnum.Add.ToString());
        if (!this.DoAdd())
          this.JscriptMsg("保存过程中发生错误！", "");
        else
          this.JscriptMsg("添加导航菜单成功！", "nav_list.aspx", "parent.loadMenuTree");
      }
    }
  }
}
