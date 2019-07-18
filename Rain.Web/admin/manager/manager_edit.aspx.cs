// Decompiled with JetBrains decompiler
// Type: Rain.Web.admin.manager.manager_edit
// Assembly: Rain.Web, Version=4.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 7C74D3EC-D223-4A13-9304-5BE7CB5803E5
// Assembly location: C:\Users\Felix.Ho\Desktop\page\bin\Rain.Web.dll

using System;
using System.Data;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Rain.Common;
using Rain.Web.UI;

namespace Rain.Web.admin.manager
{
  public class manager_edit : ManagePage
  {
    private string defaultpassword = "0|0|0|0";
    private string action = DTEnums.ActionEnum.Add.ToString();
    private int id = 0;
    protected HtmlForm form1;
    protected DropDownList ddlRoleId;
    protected CheckBox cbIsLock;
    protected TextBox txtUserName;
    protected TextBox txtPassword;
    protected TextBox txtPassword1;
    protected TextBox txtRealName;
    protected TextBox txtTelephone;
    protected TextBox txtEmail;
    protected Button btnSubmit;

    protected void Page_Load(object sender, EventArgs e)
    {
      string queryString = DTRequest.GetQueryString("action");
      if (!string.IsNullOrEmpty(queryString) && queryString == DTEnums.ActionEnum.Edit.ToString())
      {
        this.action = DTEnums.ActionEnum.Edit.ToString();
        if (!int.TryParse(this.Request.QueryString["id"], out this.id))
        {
          this.JscriptMsg("传输参数不正确！", "back");
          return;
        }
        if (!new Rain.BLL.manager().Exists(this.id))
        {
          this.JscriptMsg("记录不存在或已被删除！", "back");
          return;
        }
      }
      if (this.Page.IsPostBack)
        return;
      this.ChkAdminLevel("manager_list", DTEnums.ActionEnum.View.ToString());
      this.RoleBind(this.ddlRoleId, this.GetAdminInfo().role_type);
      if (this.action == DTEnums.ActionEnum.Edit.ToString())
        this.ShowInfo(this.id);
    }

    private void RoleBind(DropDownList ddl, int role_type)
    {
      DataTable table = new Rain.BLL.manager_role().GetList("").Tables[0];
      ddl.Items.Clear();
      ddl.Items.Add(new ListItem("请选择角色...", ""));
      foreach (DataRow row in (InternalDataCollectionBase) table.Rows)
      {
        if (Convert.ToInt32(row[nameof (role_type)]) >= role_type)
          ddl.Items.Add(new ListItem(row["role_name"].ToString(), row["id"].ToString()));
      }
    }

    private void ShowInfo(int _id)
    {
      Rain.Model.manager model = new Rain.BLL.manager().GetModel(_id);
      this.ddlRoleId.SelectedValue = model.role_id.ToString();
      this.cbIsLock.Checked = model.is_lock == 0;
      this.txtUserName.Text = model.user_name;
      this.txtUserName.ReadOnly = true;
      this.txtUserName.Attributes.Remove("ajaxurl");
      if (!string.IsNullOrEmpty(model.password))
        this.txtPassword.Attributes["value"] = this.txtPassword1.Attributes["value"] = this.defaultpassword;
      this.txtRealName.Text = model.real_name;
      this.txtTelephone.Text = model.telephone;
      this.txtEmail.Text = model.email;
    }

    private bool DoAdd()
    {
      Rain.Model.manager model = new Rain.Model.manager();
      Rain.BLL.manager manager = new Rain.BLL.manager();
      model.role_id = int.Parse(this.ddlRoleId.SelectedValue);
      model.role_type = new Rain.BLL.manager_role().GetModel(model.role_id).role_type;
      model.is_lock = !this.cbIsLock.Checked ? 1 : 0;
      if (manager.Exists(this.txtUserName.Text.Trim()))
        return false;
      model.user_name = this.txtUserName.Text.Trim();
      model.salt = Utils.GetCheckCode(6);
      model.password = DESEncrypt.Encrypt(this.txtPassword.Text.Trim(), model.salt);
      model.real_name = this.txtRealName.Text.Trim();
      model.telephone = this.txtTelephone.Text.Trim();
      model.email = this.txtEmail.Text.Trim();
      model.add_time = DateTime.Now;
      if (manager.Add(model) <= 0)
        return false;
      this.AddAdminLog(DTEnums.ActionEnum.Add.ToString(), "添加管理员:" + model.user_name);
      return true;
    }

    private bool DoEdit(int _id)
    {
      bool flag = false;
      Rain.BLL.manager manager = new Rain.BLL.manager();
      Rain.Model.manager model = manager.GetModel(_id);
      model.role_id = int.Parse(this.ddlRoleId.SelectedValue);
      model.role_type = new Rain.BLL.manager_role().GetModel(model.role_id).role_type;
      model.is_lock = !this.cbIsLock.Checked ? 1 : 0;
      if (this.txtPassword.Text.Trim() != this.defaultpassword)
        model.password = DESEncrypt.Encrypt(this.txtPassword.Text.Trim(), model.salt);
      model.real_name = this.txtRealName.Text.Trim();
      model.telephone = this.txtTelephone.Text.Trim();
      model.email = this.txtEmail.Text.Trim();
      if (manager.Update(model))
      {
        this.AddAdminLog(DTEnums.ActionEnum.Edit.ToString(), "修改管理员:" + model.user_name);
        flag = true;
      }
      return flag;
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
      if (this.action == DTEnums.ActionEnum.Edit.ToString())
      {
        this.ChkAdminLevel("manager_list", DTEnums.ActionEnum.Edit.ToString());
        if (!this.DoEdit(this.id))
          this.JscriptMsg("保存过程中发生错误！", "");
        else
          this.JscriptMsg("修改管理员信息成功！", "manager_list.aspx");
      }
      else
      {
        this.ChkAdminLevel("manager_list", DTEnums.ActionEnum.Add.ToString());
        if (!this.DoAdd())
          this.JscriptMsg("保存过程中发生错误！", "");
        else
          this.JscriptMsg("添加管理员信息成功！", "manager_list.aspx");
      }
    }
  }
}
