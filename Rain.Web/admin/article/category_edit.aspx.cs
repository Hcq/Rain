// Decompiled with JetBrains decompiler
// Type: Rain.Web.admin.article.category_edit
// Assembly: Rain.Web, Version=4.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 7C74D3EC-D223-4A13-9304-5BE7CB5803E5
// Assembly location: C:\Users\Felix.Ho\Desktop\page\bin\Rain.Web.dll

using System;
using System.Data;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Rain.Common;
using Rain.Web.UI;

namespace Rain.Web.admin.article
{
  public class category_edit : ManagePage
  {
    private string action = DTEnums.ActionEnum.Add.ToString();
    protected string channel_name = string.Empty;
    private int id = 0;
    protected HtmlForm form1;
    protected DropDownList ddlParentId;
    protected TextBox txtSortId;
    protected TextBox txtTitle;
    protected TextBox txtCallIndex;
    protected TextBox txtSeoTitle;
    protected TextBox txtSeoKeywords;
    protected TextBox txtSeoDescription;
    protected TextBox txtLinkUrl;
    protected TextBox txtImgUrl;
    protected HtmlTextArea txtContent;
    protected Button btnSubmit;
    protected int channel_id;

    protected void Page_Load(object sender, EventArgs e)
    {
      string queryString = DTRequest.GetQueryString("action");
      this.channel_id = DTRequest.GetQueryInt("channel_id");
      this.id = DTRequest.GetQueryInt("id");
      if (this.channel_id == 0)
      {
        this.JscriptMsg("频道参数不正确！", "back");
      }
      else
      {
        this.channel_name = new Rain.BLL.channel().GetChannelName(this.channel_id);
        if (!string.IsNullOrEmpty(queryString) && queryString == DTEnums.ActionEnum.Edit.ToString())
        {
          this.action = DTEnums.ActionEnum.Edit.ToString();
          if (this.id == 0)
          {
            this.JscriptMsg("传输参数不正确！", "back");
            return;
          }
          if (!new Rain.BLL.article_category().Exists(this.id))
          {
            this.JscriptMsg("类别不存在或已被删除！", "back");
            return;
          }
        }
        if (this.Page.IsPostBack)
          return;
        this.ChkAdminLevel("channel_" + this.channel_name + "_category", DTEnums.ActionEnum.View.ToString());
        this.TreeBind(this.channel_id);
        if (this.action == DTEnums.ActionEnum.Edit.ToString())
          this.ShowInfo(this.id);
        else if (this.id > 0)
          this.ddlParentId.SelectedValue = this.id.ToString();
      }
    }

    private void TreeBind(int _channel_id)
    {
      DataTable list = new Rain.BLL.article_category().GetList(0, _channel_id);
      this.ddlParentId.Items.Clear();
      this.ddlParentId.Items.Add(new ListItem("无父级分类", "0"));
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

    private void ShowInfo(int _id)
    {
      Rain.Model.article_category model = new Rain.BLL.article_category().GetModel(_id);
      this.ddlParentId.SelectedValue = model.parent_id.ToString();
      this.txtCallIndex.Text = model.call_index;
      this.txtTitle.Text = model.title;
      this.txtSortId.Text = model.sort_id.ToString();
      this.txtSeoTitle.Text = model.seo_title;
      this.txtSeoKeywords.Text = model.seo_keywords;
      this.txtSeoDescription.Text = model.seo_description;
      this.txtLinkUrl.Text = model.link_url;
      this.txtImgUrl.Text = model.img_url;
      this.txtContent.Value = model.content;
    }

    private bool DoAdd()
    {
      try
      {
        Rain.Model.article_category model = new Rain.Model.article_category();
        Rain.BLL.article_category articleCategory = new Rain.BLL.article_category();
        model.channel_id = this.channel_id;
        model.call_index = this.txtCallIndex.Text.Trim();
        model.title = this.txtTitle.Text.Trim();
        model.parent_id = int.Parse(this.ddlParentId.SelectedValue);
        model.sort_id = int.Parse(this.txtSortId.Text.Trim());
        model.seo_title = this.txtSeoTitle.Text;
        model.seo_keywords = this.txtSeoKeywords.Text;
        model.seo_description = this.txtSeoDescription.Text;
        model.link_url = this.txtLinkUrl.Text.Trim();
        model.img_url = this.txtImgUrl.Text.Trim();
        model.content = this.txtContent.Value;
        if (articleCategory.Add(model) > 0)
        {
          this.AddAdminLog(DTEnums.ActionEnum.Add.ToString(), "添加" + this.channel_name + "频道栏目分类:" + model.title);
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
        Rain.BLL.article_category articleCategory = new Rain.BLL.article_category();
        Rain.Model.article_category model = articleCategory.GetModel(_id);
        int num = int.Parse(this.ddlParentId.SelectedValue);
        model.channel_id = this.channel_id;
        model.call_index = this.txtCallIndex.Text.Trim();
        model.title = this.txtTitle.Text.Trim();
        if (num != model.id)
          model.parent_id = num;
        model.sort_id = int.Parse(this.txtSortId.Text.Trim());
        model.seo_title = this.txtSeoTitle.Text;
        model.seo_keywords = this.txtSeoKeywords.Text;
        model.seo_description = this.txtSeoDescription.Text;
        model.link_url = this.txtLinkUrl.Text.Trim();
        model.img_url = this.txtImgUrl.Text.Trim();
        model.content = this.txtContent.Value;
        if (articleCategory.Update(model))
        {
          this.AddAdminLog(DTEnums.ActionEnum.Edit.ToString(), "修改" + this.channel_name + "频道栏目分类:" + model.title);
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
        this.ChkAdminLevel("channel_" + this.channel_name + "_category", DTEnums.ActionEnum.Edit.ToString());
        if (!this.DoEdit(this.id))
          this.JscriptMsg("保存过程中发生错误！", "");
        else
          this.JscriptMsg("修改类别成功！", "category_list.aspx?channel_id=" + (object) this.channel_id);
      }
      else
      {
        this.ChkAdminLevel("channel_" + this.channel_name + "_category", DTEnums.ActionEnum.Add.ToString());
        if (!this.DoAdd())
          this.JscriptMsg("保存过程中发生错误！", "");
        else
          this.JscriptMsg("添加类别成功！", "category_list.aspx?channel_id=" + (object) this.channel_id);
      }
    }
  }
}
