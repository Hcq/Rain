// Decompiled with JetBrains decompiler
// Type: Rain.Web.admin.manager.role_edit
// Assembly: Rain.Web, Version=4.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 7C74D3EC-D223-4A13-9304-5BE7CB5803E5
// Assembly location: C:\Users\Felix.Ho\Desktop\page\bin\Rain.Web.dll

using System;
using System.Collections.Generic;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Rain.Common;
using Rain.Model;
using Rain.Web.UI;

namespace Rain.Web.admin.manager
{
  public class role_edit : ManagePage
  {
    private string action = DTEnums.ActionEnum.Add.ToString();
    private int id = 0;
    protected HtmlForm form1;
    protected DropDownList ddlRoleType;
    protected TextBox txtRoleName;
    protected Repeater rptList;
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
        if (!new Rain.BLL.manager_role().Exists(this.id))
        {
          this.JscriptMsg("角色不存在或已被删除！", "back");
          return;
        }
      }
      if (this.Page.IsPostBack)
        return;
      this.ChkAdminLevel("manager_role", DTEnums.ActionEnum.View.ToString());
      this.RoleTypeBind();
      this.NavBind();
      if (this.action == DTEnums.ActionEnum.Edit.ToString())
        this.ShowInfo(this.id);
    }

    private void RoleTypeBind()
    {
      Rain.Model.manager adminInfo = this.GetAdminInfo();
      this.ddlRoleType.Items.Clear();
      this.ddlRoleType.Items.Add(new ListItem("请选择类型...", ""));
      if (adminInfo.role_type < 2)
        this.ddlRoleType.Items.Add(new ListItem("超级用户", "1"));
      this.ddlRoleType.Items.Add(new ListItem("系统用户", "2"));
    }

    private void NavBind()
    {
      this.rptList.DataSource = (object) new Rain.BLL.navigation().GetList(0, DTEnums.NavigationEnum.System.ToString());
      this.rptList.DataBind();
    }

    private void ShowInfo(int _id)
    {
      Rain.Model.manager_role model = new Rain.BLL.manager_role().GetModel(_id);
      this.txtRoleName.Text = model.role_name;
      this.ddlRoleType.SelectedValue = model.role_type.ToString();
      if (model.manager_role_values == null)
        return;
      for (int index = 0; index < this.rptList.Items.Count; ++index)
      {
        string navName = ((HiddenField) this.rptList.Items[index].FindControl("hidName")).Value;
        CheckBoxList cblActionType = (CheckBoxList) this.rptList.Items[index].FindControl("cblActionType");
        for (int n = 0; n < cblActionType.Items.Count; ++n)
        {
          if (model.manager_role_values.Find((Predicate<manager_role_value>) (p => p.nav_name == navName && p.action_type == cblActionType.Items[n].Value)) != null)
            cblActionType.Items[n].Selected = true;
        }
      }
    }

    private bool DoAdd()
    {
      bool flag = false;
      Rain.Model.manager_role model = new Rain.Model.manager_role();
      Rain.BLL.manager_role managerRole = new Rain.BLL.manager_role();
      model.role_name = this.txtRoleName.Text.Trim();
      model.role_type = int.Parse(this.ddlRoleType.SelectedValue);
      List<manager_role_value> managerRoleValueList = new List<manager_role_value>();
      for (int index1 = 0; index1 < this.rptList.Items.Count; ++index1)
      {
        string str = ((HiddenField) this.rptList.Items[index1].FindControl("hidName")).Value;
        CheckBoxList control = (CheckBoxList) this.rptList.Items[index1].FindControl("cblActionType");
        for (int index2 = 0; index2 < control.Items.Count; ++index2)
        {
          if (control.Items[index2].Selected)
            managerRoleValueList.Add(new manager_role_value()
            {
              nav_name = str,
              action_type = control.Items[index2].Value
            });
        }
      }
      model.manager_role_values = managerRoleValueList;
      if (managerRole.Add(model) > 0)
      {
        this.AddAdminLog(DTEnums.ActionEnum.Add.ToString(), "添加管理角色:" + model.role_name);
        flag = true;
      }
      return flag;
    }

    private bool DoEdit(int _id)
    {
      bool flag = false;
      Rain.BLL.manager_role managerRole = new Rain.BLL.manager_role();
      Rain.Model.manager_role model = managerRole.GetModel(_id);
      model.role_name = this.txtRoleName.Text.Trim();
      model.role_type = int.Parse(this.ddlRoleType.SelectedValue);
      List<manager_role_value> managerRoleValueList = new List<manager_role_value>();
      for (int index1 = 0; index1 < this.rptList.Items.Count; ++index1)
      {
        string str = ((HiddenField) this.rptList.Items[index1].FindControl("hidName")).Value;
        CheckBoxList control = (CheckBoxList) this.rptList.Items[index1].FindControl("cblActionType");
        for (int index2 = 0; index2 < control.Items.Count; ++index2)
        {
          if (control.Items[index2].Selected)
            managerRoleValueList.Add(new manager_role_value()
            {
              role_id = _id,
              nav_name = str,
              action_type = control.Items[index2].Value
            });
        }
      }
      model.manager_role_values = managerRoleValueList;
      if (managerRole.Update(model))
      {
        this.AddAdminLog(DTEnums.ActionEnum.Edit.ToString(), "修改管理角色:" + model.role_name);
        flag = true;
      }
      return flag;
    }

    protected void rptList_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
      if (e.Item.ItemType != ListItemType.AlternatingItem && e.Item.ItemType != ListItemType.Item)
        return;
      Literal control1 = (Literal) e.Item.FindControl("LitFirst");
      HiddenField control2 = (HiddenField) e.Item.FindControl("hidLayer");
      string format = "<span style=\"display:inline-block;width:{0}px;\"></span>{1}{2}";
      string str1 = "<span class=\"folder-open\"></span>";
      string str2 = "<span class=\"folder-line\"></span>";
      int int32 = Convert.ToInt32(control2.Value);
      control1.Text = int32 != 1 ? string.Format(format, (object) ((int32 - 2) * 24), (object) str2, (object) str1) : str1;
      string[] strArray = ((HiddenField) e.Item.FindControl("hidActionType")).Value.Split(',');
      CheckBoxList control3 = (CheckBoxList) e.Item.FindControl("cblActionType");
      control3.Items.Clear();
      for (int index = 0; index < strArray.Length; ++index)
      {
        if (Utils.ActionType().ContainsKey(strArray[index]))
          control3.Items.Add(new ListItem(" " + Utils.ActionType()[strArray[index]] + " ", strArray[index]));
      }
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
      if (this.action == DTEnums.ActionEnum.Edit.ToString())
      {
        this.ChkAdminLevel("manager_role", DTEnums.ActionEnum.Edit.ToString());
        if (!this.DoEdit(this.id))
          this.JscriptMsg("保存过程中发生错误！", "");
        else
          this.JscriptMsg("修改管理角色成功！", "role_list.aspx");
      }
      else
      {
        this.ChkAdminLevel("manager_role", DTEnums.ActionEnum.Add.ToString());
        if (!this.DoAdd())
          this.JscriptMsg("保存过程中发生错误！", "");
        else
          this.JscriptMsg("添加管理角色成功！", "role_list.aspx");
      }
    }
  }
}
