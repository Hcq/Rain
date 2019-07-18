// Decompiled with JetBrains decompiler
// Type: Rain.Web.admin.users.group_edit
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
  public class group_edit : ManagePage
  {
    private string action = DTEnums.ActionEnum.Add.ToString();
    private int id = 0;
    protected HtmlForm form1;
    protected TextBox txtTitle;
    protected CheckBox rblIsLock;
    protected CheckBox rblIsDefault;
    protected CheckBox rblIsUpgrade;
    protected TextBox txtGrade;
    protected TextBox txtUpgradeExp;
    protected TextBox txtAmount;
    protected TextBox txtPoint;
    protected TextBox txtDiscount;
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
        if (!new Rain.BLL.user_groups().Exists(this.id))
        {
          this.JscriptMsg("用户组不存在或已被删除！", "back");
          return;
        }
      }
      if (this.Page.IsPostBack)
        return;
      this.ChkAdminLevel("user_group", DTEnums.ActionEnum.View.ToString());
      if (this.action == DTEnums.ActionEnum.Edit.ToString())
        this.ShowInfo(this.id);
    }

    private void ShowInfo(int _id)
    {
      Rain.Model.user_groups model = new Rain.BLL.user_groups().GetModel(_id);
      this.txtTitle.Text = model.title;
      if (model.is_lock == 1)
        this.rblIsLock.Checked = true;
      if (model.is_default == 1)
        this.rblIsDefault.Checked = true;
      if (model.is_upgrade == 1)
        this.rblIsUpgrade.Checked = true;
      this.txtGrade.Text = model.grade.ToString();
      this.txtUpgradeExp.Text = model.upgrade_exp.ToString();
      this.txtAmount.Text = model.amount.ToString();
      this.txtPoint.Text = model.point.ToString();
      this.txtDiscount.Text = model.discount.ToString();
    }

    private bool DoAdd()
    {
      bool flag = false;
      Rain.Model.user_groups model = new Rain.Model.user_groups();
      Rain.BLL.user_groups userGroups = new Rain.BLL.user_groups();
      model.title = this.txtTitle.Text.Trim();
      model.is_lock = 0;
      if (this.rblIsLock.Checked)
        model.is_lock = 1;
      model.is_default = 0;
      if (this.rblIsDefault.Checked)
        model.is_default = 1;
      model.is_upgrade = 0;
      if (this.rblIsUpgrade.Checked)
        model.is_upgrade = 1;
      model.grade = int.Parse(this.txtGrade.Text.Trim());
      model.upgrade_exp = int.Parse(this.txtUpgradeExp.Text.Trim());
      model.amount = Decimal.Parse(this.txtAmount.Text.Trim());
      model.point = int.Parse(this.txtPoint.Text.Trim());
      model.discount = int.Parse(this.txtDiscount.Text.Trim());
      if (userGroups.Add(model) > 0)
      {
        this.AddAdminLog(DTEnums.ActionEnum.Add.ToString(), "添加用户组:" + model.title);
        flag = true;
      }
      return flag;
    }

    private bool DoEdit(int _id)
    {
      bool flag = false;
      Rain.BLL.user_groups userGroups = new Rain.BLL.user_groups();
      Rain.Model.user_groups model = userGroups.GetModel(_id);
      model.title = this.txtTitle.Text.Trim();
      model.is_lock = 0;
      if (this.rblIsLock.Checked)
        model.is_lock = 1;
      model.is_default = 0;
      if (this.rblIsDefault.Checked)
        model.is_default = 1;
      model.is_upgrade = 0;
      if (this.rblIsUpgrade.Checked)
        model.is_upgrade = 1;
      model.grade = int.Parse(this.txtGrade.Text.Trim());
      model.upgrade_exp = int.Parse(this.txtUpgradeExp.Text.Trim());
      model.amount = Decimal.Parse(this.txtAmount.Text.Trim());
      model.point = int.Parse(this.txtPoint.Text.Trim());
      model.discount = int.Parse(this.txtDiscount.Text.Trim());
      if (userGroups.Update(model))
      {
        this.AddAdminLog(DTEnums.ActionEnum.Edit.ToString(), "修改用户组:" + model.title);
        flag = true;
      }
      return flag;
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
      if (this.action == DTEnums.ActionEnum.Edit.ToString())
      {
        this.ChkAdminLevel("user_group", DTEnums.ActionEnum.Edit.ToString());
        if (!this.DoEdit(this.id))
          this.JscriptMsg("保存过程中发生错误！", "");
        else
          this.JscriptMsg("修改用户组成功！", "group_list.aspx");
      }
      else
      {
        this.ChkAdminLevel("user_group", DTEnums.ActionEnum.Add.ToString());
        if (!this.DoAdd())
          this.JscriptMsg("保存过程中发生错误！", "");
        else
          this.JscriptMsg("添加会员组成功！", "group_list.aspx");
      }
    }
  }
}
