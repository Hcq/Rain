// Decompiled with JetBrains decompiler
// Type: Rain.Web.admin.channel.channel_edit
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

namespace Rain.Web.admin.channel
{
  public class channel_edit : ManagePage
  {
    private string action = DTEnums.ActionEnum.Add.ToString();
    private int id = 0;
    protected HtmlForm form1;
    protected TextBox txtName;
    protected TextBox txtTitle;
    protected DropDownList ddlSiteId;
    protected CheckBox cbIsAlbums;
    protected CheckBox cbIsAttach;
    protected CheckBox cbIsSpec;
    protected TextBox txtSortId;
    protected CheckBoxList cblAttributeField;
    protected Repeater rptList;
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
        if (!new Rain.BLL.channel().Exists(this.id))
        {
          this.JscriptMsg("记录不存在或已删除！", "back");
          return;
        }
      }
      if (this.Page.IsPostBack)
        return;
      this.ChkAdminLevel("sys_channel_manage", DTEnums.ActionEnum.View.ToString());
      this.TreeBind();
      this.FieldBind();
      if (this.action == DTEnums.ActionEnum.Edit.ToString())
        this.ShowInfo(this.id);
      else
        this.txtName.Attributes.Add("ajaxurl", "../../tools/admin_ajax.ashx?action=channel_name_validate");
    }

    protected string GetPageTypeTxt(string type_name)
    {
      string str = "";
      switch (type_name)
      {
        case "index":
          str = "首页";
          break;
        case "category":
          str = "栏目页";
          break;
        case "list":
          str = "列表页";
          break;
        case "detail":
          str = "详细页";
          break;
      }
      return str;
    }

    private string GetInherit(string page_type)
    {
      string str = "";
      switch (page_type)
      {
        case "index":
          str = "Rain.Web.UI.Page.article";
          break;
        case "category":
          str = "Rain.Web.UI.Page.category";
          break;
        case "list":
          str = "Rain.Web.UI.Page.article_list";
          break;
        case "detail":
          str = "Rain.Web.UI.Page.article_show";
          break;
      }
      return str;
    }

    private void TreeBind()
    {
      DataTable table = new Rain.BLL.channel_site().GetList(0, "", "sort_id asc,id desc").Tables[0];
      this.ddlSiteId.Items.Clear();
      this.ddlSiteId.Items.Add(new ListItem("请选择站点...", ""));
      foreach (DataRow row in (InternalDataCollectionBase) table.Rows)
        this.ddlSiteId.Items.Add(new ListItem(row["title"].ToString(), row["id"].ToString()));
    }

    private void FieldBind()
    {
      DataTable table = new Rain.BLL.article_attribute_field().GetList(0, "", "sort_id asc,id desc").Tables[0];
      this.cblAttributeField.Items.Clear();
      foreach (DataRow row in (InternalDataCollectionBase) table.Rows)
        this.cblAttributeField.Items.Add(new ListItem(row["title"].ToString(), row["name"].ToString() + "," + row["id"].ToString()));
    }

    private void ShowInfo(int _id)
    {
      Rain.Model.channel model = new Rain.BLL.channel().GetModel(_id);
      this.txtTitle.Text = model.title;
      this.txtName.Text = model.name;
      this.txtName.Focus();
      this.txtName.Attributes.Add("ajaxurl", "../../tools/admin_ajax.ashx?action=channel_name_validate&old_channel_name=" + Utils.UrlEncode(model.name));
      this.ddlSiteId.SelectedValue = model.site_id.ToString();
      if (model.is_albums == 1)
        this.cbIsAlbums.Checked = true;
      if (model.is_attach == 1)
        this.cbIsAttach.Checked = true;
      if (model.is_spec == 1)
        this.cbIsSpec.Checked = true;
      this.txtSortId.Text = model.sort_id.ToString();
      if (model.channel_fields != null)
      {
        for (int index = 0; index < this.cblAttributeField.Items.Count; ++index)
        {
          string[] fieldIdArr = this.cblAttributeField.Items[index].Value.Split(',');
          if (model.channel_fields.Find((Predicate<channel_field>) (p => p.field_id == int.Parse(fieldIdArr[1]))) != null)
            this.cblAttributeField.Items[index].Selected = true;
        }
      }
      this.rptList.DataSource = (object) new Rain.BLL.url_rewrite().GetList(model.name);
      this.rptList.DataBind();
    }

    private bool DoAdd()
    {
      Rain.Model.channel model1 = new Rain.Model.channel();
      Rain.BLL.channel channel = new Rain.BLL.channel();
      model1.site_id = Utils.StrToInt(this.ddlSiteId.SelectedValue, 0);
      model1.name = this.txtName.Text.Trim();
      model1.title = this.txtTitle.Text.Trim();
      if (this.cbIsAlbums.Checked)
        model1.is_albums = 1;
      if (this.cbIsAttach.Checked)
        model1.is_attach = 1;
      if (this.cbIsSpec.Checked)
        model1.is_spec = 1;
      model1.sort_id = Utils.StrToInt(this.txtSortId.Text.Trim(), 99);
      List<channel_field> channelFieldList = new List<channel_field>();
      for (int index = 0; index < this.cblAttributeField.Items.Count; ++index)
      {
        if (this.cblAttributeField.Items[index].Selected)
        {
          string[] strArray = this.cblAttributeField.Items[index].Value.Split(',');
          channelFieldList.Add(new channel_field()
          {
            field_id = Utils.StrToInt(strArray[1], 0)
          });
        }
      }
      model1.channel_fields = channelFieldList;
      if (channel.Add(model1) < 1)
        return false;
      Rain.BLL.url_rewrite urlRewrite = new Rain.BLL.url_rewrite();
      urlRewrite.Remove("channel", model1.name);
      string[] values1 = this.Request.Form.GetValues("item_type");
      string[] values2 = this.Request.Form.GetValues("item_name");
      string[] values3 = this.Request.Form.GetValues("item_page");
      string[] values4 = this.Request.Form.GetValues("item_templet");
      string[] values5 = this.Request.Form.GetValues("item_pagesize");
      string[] values6 = this.Request.Form.GetValues("item_rewrite");
      if (values1 != null && values2 != null && (values3 != null && values4 != null) && values5 != null && values6 != null && (values1.Length == values2.Length && values2.Length == values3.Length && (values3.Length == values4.Length && values4.Length == values5.Length) && values5.Length == values6.Length))
      {
        for (int index = 0; index < values1.Length; ++index)
        {
          Rain.Model.url_rewrite model2 = new Rain.Model.url_rewrite()
          {
            name = values2[index].Trim(),
            type = values1[index].Trim(),
            page = values3[index].Trim()
          };
          model2.inherit = this.GetInherit(model2.type);
          model2.templet = values4[index].Trim();
          if (Utils.StrToInt(values5[index].Trim(), 0) > 0)
            model2.pagesize = values5[index].Trim();
          model2.channel = model1.name;
          List<url_rewrite_item> urlRewriteItemList = new List<url_rewrite_item>();
          string str1 = values6[index];
          char[] chArray1 = new char[1]{ '&' };
          foreach (string str2 in str1.Split(chArray1))
          {
            char[] chArray2 = new char[1]{ ',' };
            string[] strArray = str2.Split(chArray2);
            if (strArray.Length == 3)
              urlRewriteItemList.Add(new url_rewrite_item()
              {
                path = strArray[0],
                pattern = strArray[1],
                querystring = strArray[2]
              });
          }
          model2.url_rewrite_items = urlRewriteItemList;
          urlRewrite.Add(model2);
        }
      }
      this.AddAdminLog(DTEnums.ActionEnum.Add.ToString(), "添加频道" + model1.title);
      return true;
    }

    private bool DoEdit(int _id)
    {
      Rain.BLL.channel channel = new Rain.BLL.channel();
      Rain.Model.channel model1 = channel.GetModel(_id);
      string name = model1.name;
      model1.site_id = Utils.StrToInt(this.ddlSiteId.SelectedValue, 0);
      model1.name = this.txtName.Text.Trim();
      model1.title = this.txtTitle.Text.Trim();
      model1.is_albums = 0;
      model1.is_attach = 0;
      model1.is_spec = 0;
      if (this.cbIsAlbums.Checked)
        model1.is_albums = 1;
      if (this.cbIsAttach.Checked)
        model1.is_attach = 1;
      if (this.cbIsSpec.Checked)
        model1.is_spec = 1;
      model1.sort_id = Utils.StrToInt(this.txtSortId.Text.Trim(), 99);
      List<channel_field> channelFieldList = new List<channel_field>();
      for (int index = 0; index < this.cblAttributeField.Items.Count; ++index)
      {
        if (this.cblAttributeField.Items[index].Selected)
        {
          string[] strArray = this.cblAttributeField.Items[index].Value.Split(',');
          channelFieldList.Add(new channel_field()
          {
            channel_id = model1.id,
            field_id = Utils.StrToInt(strArray[1], 0)
          });
        }
      }
      model1.channel_fields = channelFieldList;
      if (!channel.Update(model1))
        return false;
      Rain.BLL.url_rewrite urlRewrite = new Rain.BLL.url_rewrite();
      urlRewrite.Remove("channel", name);
      string[] values1 = this.Request.Form.GetValues("item_type");
      string[] values2 = this.Request.Form.GetValues("item_name");
      string[] values3 = this.Request.Form.GetValues("item_page");
      string[] values4 = this.Request.Form.GetValues("item_templet");
      string[] values5 = this.Request.Form.GetValues("item_pagesize");
      string[] values6 = this.Request.Form.GetValues("item_rewrite");
      if (values1 != null && values2 != null && (values3 != null && values4 != null) && values5 != null && values6 != null && (values1.Length == values2.Length && values2.Length == values3.Length && (values3.Length == values4.Length && values4.Length == values5.Length) && values5.Length == values6.Length))
      {
        for (int index = 0; index < values1.Length; ++index)
        {
          Rain.Model.url_rewrite model2 = new Rain.Model.url_rewrite()
          {
            name = values2[index].Trim(),
            type = values1[index].Trim(),
            page = values3[index].Trim()
          };
          model2.inherit = this.GetInherit(model2.type);
          model2.templet = values4[index].Trim();
          if (Utils.StrToInt(values5[index].Trim(), 0) > 0)
            model2.pagesize = values5[index].Trim();
          model2.channel = model1.name;
          List<url_rewrite_item> urlRewriteItemList = new List<url_rewrite_item>();
          string str1 = values6[index];
          char[] chArray1 = new char[1]{ '&' };
          foreach (string str2 in str1.Split(chArray1))
          {
            char[] chArray2 = new char[1]{ ',' };
            string[] strArray = str2.Split(chArray2);
            if (strArray.Length == 3)
              urlRewriteItemList.Add(new url_rewrite_item()
              {
                path = strArray[0],
                pattern = strArray[1],
                querystring = strArray[2]
              });
          }
          model2.url_rewrite_items = urlRewriteItemList;
          urlRewrite.Add(model2);
        }
      }
      this.AddAdminLog(DTEnums.ActionEnum.Edit.ToString(), "修改频道" + model1.title);
      return true;
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
      if (this.action == DTEnums.ActionEnum.Edit.ToString())
      {
        this.ChkAdminLevel("sys_channel_manage", DTEnums.ActionEnum.Edit.ToString());
        if (!this.DoEdit(this.id))
          this.JscriptMsg("保存过程中发生错误！", "");
        else
          this.JscriptMsg("修改频道成功！", "channel_list.aspx", "parent.loadMenuTree");
      }
      else
      {
        this.ChkAdminLevel("sys_channel_manage", DTEnums.ActionEnum.Add.ToString());
        if (!this.DoAdd())
          this.JscriptMsg("保存过程中发生错误！", "");
        else
          this.JscriptMsg("添加频道成功！", "channel_list.aspx", "parent.loadMenuTree");
      }
    }
  }
}
