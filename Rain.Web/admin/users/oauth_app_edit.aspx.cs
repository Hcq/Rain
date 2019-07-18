// Decompiled with JetBrains decompiler
// Type: Rain.Web.admin.users.oauth_app_edit
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
  public class oauth_app_edit : ManagePage
  {
    private string action = DTEnums.ActionEnum.Add.ToString();
    private int id = 0;
    protected HtmlForm form1;
    protected TextBox txtTitle;
    protected CheckBox cbIsLock;
    protected TextBox txtSortId;
    protected TextBox txtApiPath;
    protected TextBox txtAppId;
    protected TextBox txtAppKey;
    protected TextBox txtImgUrl;
    protected TextBox txtRemark;
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
        if (!new Rain.BLL.user_oauth_app().Exists(this.id))
        {
          this.JscriptMsg("记录不存在或已被删除！", "back");
          return;
        }
      }
      if (this.Page.IsPostBack)
        return;
      this.ChkAdminLevel("user_oauth", DTEnums.ActionEnum.View.ToString());
      if (this.action == DTEnums.ActionEnum.Edit.ToString())
        this.ShowInfo(this.id);
    }

    private void ShowInfo(int _id)
    {
      Rain.Model.user_oauth_app model = new Rain.BLL.user_oauth_app().GetModel(_id);
      this.txtTitle.Text = model.title;
      this.cbIsLock.Checked = model.is_lock == 0;
      this.txtSortId.Text = model.sort_id.ToString();
      this.txtApiPath.Text = model.api_path;
      this.txtAppId.Text = model.app_id;
      this.txtAppKey.Text = model.app_key;
      this.txtImgUrl.Text = model.img_url;
      this.txtRemark.Text = model.remark;
    }

    private bool DoAdd()
    {
      bool flag = false;
      Rain.Model.user_oauth_app model = new Rain.Model.user_oauth_app();
      Rain.BLL.user_oauth_app userOauthApp = new Rain.BLL.user_oauth_app();
      model.title = this.txtTitle.Text.Trim();
      model.is_lock = !this.cbIsLock.Checked ? 1 : 0;
      model.sort_id = Utils.StrToInt(this.txtSortId.Text.Trim(), 99);
      model.api_path = this.txtApiPath.Text.Trim();
      model.app_id = this.txtAppId.Text.Trim();
      model.app_key = this.txtAppKey.Text.Trim();
      model.img_url = this.txtImgUrl.Text.Trim();
      model.remark = this.txtRemark.Text;
      if (userOauthApp.Add(model) > 0)
      {
        this.AddAdminLog(DTEnums.ActionEnum.Add.ToString(), "添加OAuth信息:" + model.title);
        flag = true;
      }
      return flag;
    }

    private bool DoEdit(int _id)
    {
      bool flag = false;
      Rain.BLL.user_oauth_app userOauthApp = new Rain.BLL.user_oauth_app();
      Rain.Model.user_oauth_app model = userOauthApp.GetModel(_id);
      model.title = this.txtTitle.Text.Trim();
      model.is_lock = !this.cbIsLock.Checked ? 1 : 0;
      model.sort_id = Utils.StrToInt(this.txtSortId.Text.Trim(), 99);
      model.api_path = this.txtApiPath.Text.Trim();
      model.app_id = this.txtAppId.Text.Trim();
      model.app_key = this.txtAppKey.Text.Trim();
      model.img_url = this.txtImgUrl.Text.Trim();
      model.remark = this.txtRemark.Text;
      if (userOauthApp.Update(model))
      {
        this.AddAdminLog(DTEnums.ActionEnum.Edit.ToString(), "修改OAuth信息:" + model.title);
        flag = true;
      }
      return flag;
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
      if (this.action == DTEnums.ActionEnum.Edit.ToString())
      {
        this.ChkAdminLevel("user_oauth", DTEnums.ActionEnum.Edit.ToString());
        if (!this.DoEdit(this.id))
          this.JscriptMsg("保存过程中发生错误！", string.Empty);
        else
          this.JscriptMsg("修改OAuth应用成功！", "oauth_app_list.aspx");
      }
      else
      {
        this.ChkAdminLevel("user_oauth", DTEnums.ActionEnum.Add.ToString());
        if (!this.DoAdd())
          this.JscriptMsg("保存过程中发生错误！", string.Empty);
        else
          this.JscriptMsg("添加OAuth应用成功！", "oauth_app_list.aspx");
      }
    }
  }
}
