// Decompiled with JetBrains decompiler
// Type: Rain.Web.admin.users.user_edit
// Assembly: Rain.Web, Version=4.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 7C74D3EC-D223-4A13-9304-5BE7CB5803E5
// Assembly location: C:\Users\Felix.Ho\Desktop\page\bin\Rain.Web.dll

using System;
using System.Data;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Rain.Common;
using Rain.Web.UI;

namespace Rain.Web.admin.users
{
  public class user_edit : ManagePage
  {
    private string defaultpassword = "0|0|0|0";
    protected string action = DTEnums.ActionEnum.Add.ToString();
    private int id = 0;
    protected HtmlForm form1;
    protected DropDownList ddlGroupId;
    protected RadioButtonList rblStatus;
    protected TextBox txtUserName;
    protected TextBox txtPassword;
    protected TextBox txtPassword1;
    protected TextBox txtEmail;
    protected TextBox txtNickName;
    protected TextBox txtAvatar;
    protected RadioButtonList rblSex;
    protected TextBox txtBirthday;
    protected TextBox txtMobile;
    protected TextBox txtTelphone;
    protected TextBox txtQQ;
    protected TextBox txtMsn;
    protected TextBox txtAddress;
    protected TextBox txtAmount;
    protected TextBox txtPoint;
    protected TextBox txtExp;
    protected Label lblRegTime;
    protected Label lblRegIP;
    protected Label lblLastTime;
    protected Label lblLastIP;
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
        if (!new Rain.BLL.users().Exists(this.id))
        {
          this.JscriptMsg("信息不存在或已被删除！", "back");
          return;
        }
      }
      if (this.Page.IsPostBack)
        return;
      this.ChkAdminLevel("user_list", DTEnums.ActionEnum.View.ToString());
      this.TreeBind("is_lock=0");
      if (this.action == DTEnums.ActionEnum.Edit.ToString())
        this.ShowInfo(this.id);
    }

    private void TreeBind(string strWhere)
    {
      DataTable table = new Rain.BLL.user_groups().GetList(0, strWhere, "grade asc,id asc").Tables[0];
      this.ddlGroupId.Items.Clear();
      this.ddlGroupId.Items.Add(new ListItem("请选择组别...", ""));
      foreach (DataRow row in (InternalDataCollectionBase) table.Rows)
        this.ddlGroupId.Items.Add(new ListItem(row["title"].ToString(), row["id"].ToString()));
    }

    private void ShowInfo(int _id)
    {
      Rain.Model.users model = new Rain.BLL.users().GetModel(_id);
      this.ddlGroupId.SelectedValue = model.group_id.ToString();
      this.rblStatus.SelectedValue = model.status.ToString();
      this.txtUserName.Text = model.user_name;
      this.txtUserName.ReadOnly = true;
      this.txtUserName.Attributes.Remove("ajaxurl");
      if (!string.IsNullOrEmpty(model.password))
        this.txtPassword.Attributes["value"] = this.txtPassword1.Attributes["value"] = this.defaultpassword;
      this.txtEmail.Text = model.email;
      this.txtNickName.Text = model.nick_name;
      this.txtAvatar.Text = model.avatar;
      this.rblSex.SelectedValue = model.sex;
      if (model.birthday.HasValue)
        this.txtBirthday.Text = model.birthday.GetValueOrDefault().ToString("yyyy-M-d");
      this.txtTelphone.Text = model.telphone;
      this.txtMobile.Text = model.mobile;
      this.txtQQ.Text = model.qq;
      this.txtMsn.Text = model.msn;
      this.txtAddress.Text = model.address;
      this.txtAmount.Text = model.amount.ToString();
      this.txtPoint.Text = model.point.ToString();
      this.txtExp.Text = model.exp.ToString();
      this.lblRegTime.Text = model.reg_time.ToString();
      this.lblRegIP.Text = model.reg_ip.ToString();
      Rain.Model.user_login_log lastModel = new Rain.BLL.user_login_log().GetLastModel(model.user_name);
      if (lastModel == null)
        return;
      this.lblLastTime.Text = lastModel.login_time.ToString();
      this.lblLastIP.Text = lastModel.login_ip;
    }

    private bool DoAdd()
    {
      bool flag = false;
      Rain.Model.users model = new Rain.Model.users();
      Rain.BLL.users users = new Rain.BLL.users();
      model.group_id = int.Parse(this.ddlGroupId.SelectedValue);
      model.status = int.Parse(this.rblStatus.SelectedValue);
      if (users.Exists(this.txtUserName.Text.Trim()))
        return false;
      model.user_name = Utils.DropHTML(this.txtUserName.Text.Trim());
      model.salt = Utils.GetCheckCode(6);
      model.password = DESEncrypt.Encrypt(this.txtPassword.Text.Trim(), model.salt);
      model.email = Utils.DropHTML(this.txtEmail.Text);
      model.nick_name = Utils.DropHTML(this.txtNickName.Text);
      model.avatar = Utils.DropHTML(this.txtAvatar.Text);
      model.sex = this.rblSex.SelectedValue;
      DateTime result;
      if (DateTime.TryParse(this.txtBirthday.Text.Trim(), out result))
        model.birthday = new DateTime?(result);
      model.telphone = Utils.DropHTML(this.txtTelphone.Text.Trim());
      model.mobile = Utils.DropHTML(this.txtMobile.Text.Trim());
      model.qq = Utils.DropHTML(this.txtQQ.Text);
      model.msn = Utils.DropHTML(this.txtMsn.Text);
      model.address = Utils.DropHTML(this.txtAddress.Text.Trim());
      model.amount = Decimal.Parse(this.txtAmount.Text.Trim());
      model.point = int.Parse(this.txtPoint.Text.Trim());
      model.exp = int.Parse(this.txtExp.Text.Trim());
      model.reg_time = DateTime.Now;
      model.reg_ip = DTRequest.GetIP();
      if (users.Add(model) > 0)
      {
        this.AddAdminLog(DTEnums.ActionEnum.Add.ToString(), "添加用户:" + model.user_name);
        flag = true;
      }
      return flag;
    }

    private bool DoEdit(int _id)
    {
      bool flag = false;
      Rain.BLL.users users = new Rain.BLL.users();
      Rain.Model.users model = users.GetModel(_id);
      model.group_id = int.Parse(this.ddlGroupId.SelectedValue);
      model.status = int.Parse(this.rblStatus.SelectedValue);
      if (this.txtPassword.Text.Trim() != this.defaultpassword)
        model.password = DESEncrypt.Encrypt(this.txtPassword.Text.Trim(), model.salt);
      model.email = Utils.DropHTML(this.txtEmail.Text);
      model.nick_name = Utils.DropHTML(this.txtNickName.Text);
      model.avatar = Utils.DropHTML(this.txtAvatar.Text);
      model.sex = this.rblSex.SelectedValue;
      DateTime result;
      if (DateTime.TryParse(this.txtBirthday.Text.Trim(), out result))
        model.birthday = new DateTime?(result);
      model.telphone = Utils.DropHTML(this.txtTelphone.Text.Trim());
      model.mobile = Utils.DropHTML(this.txtMobile.Text.Trim());
      model.qq = Utils.DropHTML(this.txtQQ.Text);
      model.msn = Utils.DropHTML(this.txtMsn.Text);
      model.address = Utils.DropHTML(this.txtAddress.Text.Trim());
      model.amount = Utils.StrToDecimal(this.txtAmount.Text.Trim(), new Decimal(0));
      model.point = Utils.StrToInt(this.txtPoint.Text.Trim(), 0);
      model.exp = Utils.StrToInt(this.txtExp.Text.Trim(), 0);
      if (users.Update(model))
      {
        this.AddAdminLog(DTEnums.ActionEnum.Edit.ToString(), "修改用户信息:" + model.user_name);
        flag = true;
      }
      return flag;
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
      if (this.action == DTEnums.ActionEnum.Edit.ToString())
      {
        this.ChkAdminLevel("user_list", DTEnums.ActionEnum.Edit.ToString());
        if (!this.DoEdit(this.id))
          this.JscriptMsg("保存过程中发生错误！", "");
        else
          this.JscriptMsg("修改会员成功！", "user_list.aspx");
      }
      else
      {
        this.ChkAdminLevel("user_list", DTEnums.ActionEnum.Add.ToString());
        if (!this.DoAdd())
          this.JscriptMsg("保存过程中发生错误！", "");
        else
          this.JscriptMsg("添加会员成功！", "user_list.aspx");
      }
    }
  }
}
