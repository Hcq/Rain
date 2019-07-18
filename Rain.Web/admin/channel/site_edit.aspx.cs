// Decompiled with JetBrains decompiler
// Type: Rain.Web.admin.channel.site_edit
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
  public class site_edit : ManagePage
  {
    private string action = DTEnums.ActionEnum.Add.ToString();
    private int id = 0;
    protected HtmlForm form1;
    protected TextBox txtTitle;
    protected TextBox txtBuildPath;
    protected TextBox txtDomain;
    protected CheckBox cbIsDefault;
    protected TextBox txtSortId;
    protected TextBox txtName;
    protected TextBox txtLogo;
    protected TextBox txtCompany;
    protected TextBox txtAddress;
    protected TextBox txtTel;
    protected TextBox txtFax;
    protected TextBox txtEmail;
    protected TextBox txtCrod;
    protected TextBox txtSeoTitle;
    protected TextBox txtSeoKeyword;
    protected TextBox txtSeoDescription;
    protected TextBox txtCopyright;
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
        if (!new Rain.BLL.channel_site().Exists(this.id))
        {
          this.JscriptMsg("记录不存在或已被删除！", "back");
          return;
        }
      }
      if (this.Page.IsPostBack)
        return;
      this.ChkAdminLevel("sys_site_manage", DTEnums.ActionEnum.View.ToString());
      if (this.action == DTEnums.ActionEnum.Edit.ToString())
        this.ShowInfo(this.id);
      else
        this.txtBuildPath.Attributes.Add("ajaxurl", "../../tools/admin_ajax.ashx?action=channel_site_validate");
    }

    private void ShowInfo(int _id)
    {
      Rain.Model.channel_site model = new Rain.BLL.channel_site().GetModel(_id);
      this.txtTitle.Text = model.title;
      this.txtBuildPath.Text = model.build_path;
      this.txtBuildPath.Attributes.Add("ajaxurl", "../../tools/admin_ajax.ashx?action=channel_site_validate&old_build_path=" + Utils.UrlEncode(model.build_path));
      this.txtBuildPath.Focus();
      this.txtDomain.Text = model.domain;
      this.txtSortId.Text = model.sort_id.ToString();
      this.cbIsDefault.Checked = model.is_default == 1;
      this.txtName.Text = model.name;
      this.txtLogo.Text = model.logo;
      this.txtCompany.Text = model.company;
      this.txtAddress.Text = model.address;
      this.txtTel.Text = model.tel;
      this.txtFax.Text = model.fax;
      this.txtEmail.Text = model.email;
      this.txtCrod.Text = model.crod;
      this.txtSeoTitle.Text = model.seo_title;
      this.txtSeoKeyword.Text = model.seo_keyword;
      this.txtSeoDescription.Text = model.seo_description;
      this.txtCopyright.Text = model.copyright;
    }

    private bool DoAdd()
    {
      Rain.Model.channel_site model = new Rain.Model.channel_site();
      Rain.BLL.channel_site channelSite = new Rain.BLL.channel_site();
      model.title = this.txtTitle.Text.Trim();
      model.build_path = this.txtBuildPath.Text.Trim();
      model.domain = this.txtDomain.Text.Trim();
      model.sort_id = Utils.StrToInt(this.txtSortId.Text.Trim(), 99);
      model.is_default = !this.cbIsDefault.Checked ? 0 : 1;
      model.name = this.txtName.Text.Trim();
      model.logo = this.txtLogo.Text.Trim();
      model.company = this.txtCompany.Text.Trim();
      model.address = this.txtAddress.Text.Trim();
      model.tel = this.txtTel.Text.Trim();
      model.fax = this.txtFax.Text.Trim();
      model.email = this.txtEmail.Text.Trim();
      model.crod = this.txtCrod.Text.Trim();
      model.seo_title = this.txtSeoTitle.Text.Trim();
      model.seo_keyword = this.txtSeoKeyword.Text.Trim();
      model.seo_description = Utils.DropHTML(this.txtSeoDescription.Text);
      model.copyright = this.txtCopyright.Text.Trim();
      if (channelSite.Add(model) <= 0)
        return false;
      CacheHelper.Remove("dt_cache_http_domain");
      this.AddAdminLog(DTEnums.ActionEnum.Add.ToString(), "添加站点:" + model.title);
      return true;
    }

    private bool DoEdit(int _id)
    {
      bool flag = false;
      Rain.BLL.channel_site channelSite = new Rain.BLL.channel_site();
      Rain.Model.channel_site model = channelSite.GetModel(_id);
      model.title = this.txtTitle.Text.Trim();
      model.build_path = this.txtBuildPath.Text.Trim();
      model.domain = this.txtDomain.Text.Trim();
      model.sort_id = Utils.StrToInt(this.txtSortId.Text.Trim(), 99);
      model.is_default = !this.cbIsDefault.Checked ? 0 : 1;
      model.name = this.txtName.Text.Trim();
      model.logo = this.txtLogo.Text.Trim();
      model.company = this.txtCompany.Text.Trim();
      model.address = this.txtAddress.Text.Trim();
      model.tel = this.txtTel.Text.Trim();
      model.fax = this.txtFax.Text.Trim();
      model.email = this.txtEmail.Text.Trim();
      model.crod = this.txtCrod.Text.Trim();
      model.seo_title = this.txtSeoTitle.Text.Trim();
      model.seo_keyword = this.txtSeoKeyword.Text.Trim();
      model.seo_description = Utils.DropHTML(this.txtSeoDescription.Text);
      model.copyright = this.txtCopyright.Text.Trim();
      if (channelSite.Update(model))
      {
        CacheHelper.Remove("dt_cache_http_domain");
        this.AddAdminLog(DTEnums.ActionEnum.Edit.ToString(), "修改站点:" + model.title);
        flag = true;
      }
      return flag;
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
      if (this.action == DTEnums.ActionEnum.Edit.ToString())
      {
        this.ChkAdminLevel("sys_site_manage", DTEnums.ActionEnum.Edit.ToString());
        if (!this.DoEdit(this.id))
          this.JscriptMsg("保存过程中发生错误！", "");
        else
          this.JscriptMsg("修改站点信息成功！", "site_list.aspx", "parent.loadMenuTree");
      }
      else
      {
        this.ChkAdminLevel("sys_site_manage", DTEnums.ActionEnum.Add.ToString());
        if (!this.DoAdd())
          this.JscriptMsg("保存过程中发生错误！", "");
        else
          this.JscriptMsg("添加站点信息成功！", "site_list.aspx", "parent.loadMenuTree");
      }
    }
  }
}
