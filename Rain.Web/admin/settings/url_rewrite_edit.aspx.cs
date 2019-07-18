// Decompiled with JetBrains decompiler
// Type: Rain.Web.admin.settings.url_rewrite_edit
// Assembly: Rain.Web, Version=4.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 7C74D3EC-D223-4A13-9304-5BE7CB5803E5
// Assembly location: C:\Users\Felix.Ho\Desktop\page\bin\Rain.Web.dll

using System;
using System.Collections.Generic;
using System.Data;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Rain.Common;
using Rain.Model;
using Rain.Web.UI;

namespace Rain.Web.admin.settings
{
  public class url_rewrite_edit : ManagePage
  {
    private string action = DTEnums.ActionEnum.Add.ToString();
    private string urlName = string.Empty;
    protected HtmlForm form1;
    protected DropDownList ddlChannel;
    protected DropDownList ddlType;
    protected TextBox txtName;
    protected TextBox txtPage;
    protected TextBox txtInherit;
    protected TextBox txtTemplet;
    protected TextBox txtPageSize;
    protected Repeater rptList;
    protected Button btnSubmit;

    protected void Page_Load(object sender, EventArgs e)
    {
      string queryString = DTRequest.GetQueryString("action");
      if (!string.IsNullOrEmpty(queryString) && queryString == DTEnums.ActionEnum.Edit.ToString())
      {
        this.action = DTEnums.ActionEnum.Edit.ToString();
        this.urlName = DTRequest.GetQueryString("name");
        if (string.IsNullOrEmpty(this.urlName))
        {
          this.JscriptMsg("传输参数不正确！", "back");
          return;
        }
      }
      if (this.Page.IsPostBack)
        return;
      this.ChkAdminLevel("sys_url_rewrite", DTEnums.ActionEnum.View.ToString());
      this.TreeBind();
      if (this.action == DTEnums.ActionEnum.Edit.ToString())
        this.ShowInfo(this.urlName);
      else
        this.txtName.Attributes.Add("ajaxurl", "../../tools/admin_ajax.ashx?action=urlrewrite_name_validate");
    }

    private void TreeBind()
    {
      DataTable table = new Rain.BLL.channel().GetList(0, "", "sort_id asc,id desc").Tables[0];
      this.ddlChannel.Items.Clear();
      this.ddlChannel.Items.Add(new ListItem("不属于频道", ""));
      foreach (DataRow row in (InternalDataCollectionBase) table.Rows)
        this.ddlChannel.Items.Add(new ListItem(row["title"].ToString(), row["name"].ToString()));
    }

    private void ShowInfo(string _urlName)
    {
      Rain.Model.url_rewrite info = new Rain.BLL.url_rewrite().GetInfo(_urlName);
      this.txtName.Text = info.name;
      this.txtName.ReadOnly = true;
      this.ddlType.SelectedValue = info.type;
      this.ddlChannel.SelectedValue = info.channel;
      this.txtPage.Text = info.page;
      this.txtInherit.Text = info.inherit;
      this.txtTemplet.Text = info.templet;
      this.txtPageSize.Text = info.pagesize;
      this.rptList.DataSource = (object) info.url_rewrite_items;
      this.rptList.DataBind();
    }

    private bool DoAdd()
    {
      Rain.BLL.url_rewrite urlRewrite = new Rain.BLL.url_rewrite();
      Rain.Model.url_rewrite model = new Rain.Model.url_rewrite();
      model.name = this.txtName.Text.Trim();
      model.type = this.ddlType.SelectedValue;
      model.channel = this.ddlChannel.SelectedValue;
      model.page = this.txtPage.Text.Trim();
      model.inherit = this.txtInherit.Text.Trim();
      model.templet = this.txtTemplet.Text.Trim();
      if (!string.IsNullOrEmpty(this.txtPageSize.Text.Trim()))
        model.pagesize = this.txtPageSize.Text.Trim();
      List<url_rewrite_item> urlRewriteItemList = new List<url_rewrite_item>();
      string[] values1 = this.Request.Form.GetValues("itemPath");
      string[] values2 = this.Request.Form.GetValues("itemPattern");
      string[] values3 = this.Request.Form.GetValues("itemQuerystring");
      if (values1 != null && values2 != null && values3 != null)
      {
        for (int index = 0; index < values1.Length; ++index)
          urlRewriteItemList.Add(new url_rewrite_item()
          {
            path = values1[index],
            pattern = values2[index],
            querystring = values3[index]
          });
      }
      model.url_rewrite_items = urlRewriteItemList;
      if (!urlRewrite.Add(model))
        return false;
      this.AddAdminLog(DTEnums.ActionEnum.Add.ToString(), "添加URL配置信息:" + model.name);
      return true;
    }

    private bool DoEdit(string _urlName)
    {
      Rain.BLL.url_rewrite urlRewrite = new Rain.BLL.url_rewrite();
      Rain.Model.url_rewrite info = urlRewrite.GetInfo(_urlName);
      info.type = this.ddlType.SelectedValue;
      info.channel = this.ddlChannel.SelectedValue;
      info.page = this.txtPage.Text.Trim();
      info.inherit = this.txtInherit.Text.Trim();
      info.templet = this.txtTemplet.Text.Trim();
      if (!string.IsNullOrEmpty(this.txtPageSize.Text.Trim()))
        info.pagesize = this.txtPageSize.Text.Trim();
      List<url_rewrite_item> urlRewriteItemList = new List<url_rewrite_item>();
      string[] values1 = this.Request.Form.GetValues("itemPath");
      string[] values2 = this.Request.Form.GetValues("itemPattern");
      string[] values3 = this.Request.Form.GetValues("itemQuerystring");
      if (values1 != null && values2 != null && values3 != null)
      {
        for (int index = 0; index < values1.Length; ++index)
          urlRewriteItemList.Add(new url_rewrite_item()
          {
            path = values1[index],
            pattern = values2[index],
            querystring = values3[index]
          });
      }
      info.url_rewrite_items = urlRewriteItemList;
      if (!urlRewrite.Edit(info))
        return false;
      this.AddAdminLog(DTEnums.ActionEnum.Edit.ToString(), "修改URL配置信息:" + info.name);
      return true;
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
      if (this.action == DTEnums.ActionEnum.Edit.ToString())
      {
        this.ChkAdminLevel("sys_url_rewrite", DTEnums.ActionEnum.Edit.ToString());
        if (!this.DoEdit(this.urlName))
          this.JscriptMsg("保存过程中发生错误！", "");
        else
          this.JscriptMsg("修改配置成功！", "url_rewrite_list.aspx");
      }
      else
      {
        this.ChkAdminLevel("sys_url_rewrite", DTEnums.ActionEnum.Add.ToString());
        if (!this.DoAdd())
          this.JscriptMsg("保存过程中发生错误！", "");
        else
          this.JscriptMsg("添加配置成功！", "url_rewrite_list.aspx");
      }
    }
  }
}
