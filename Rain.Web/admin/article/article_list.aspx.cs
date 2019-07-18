// Decompiled with JetBrains decompiler
// Type: Rain.Web.admin.article.article_list
// Assembly: Rain.Web, Version=4.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 7C74D3EC-D223-4A13-9304-5BE7CB5803E5
// Assembly location: C:\Users\Felix.Ho\Desktop\page\bin\Rain.Web.dll

using System;
using System.Data;
using System.Text;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Rain.Common;
using Rain.Web.UI;

namespace Rain.Web.admin.article
{
  public class article_list : ManagePage
  {
    protected string channel_name = string.Empty;
    protected string property = string.Empty;
    protected string keywords = string.Empty;
    protected string prolistview = string.Empty;
    protected int channel_id;
    protected int totalCount;
    protected int page;
    protected int pageSize;
    protected int category_id;
    protected HtmlForm form1;
    protected LinkButton btnSave;
    protected LinkButton btnAudit;
    protected LinkButton btnDelete;
    protected DropDownList ddlCategoryId;
    protected DropDownList ddlProperty;
    protected TextBox txtKeywords;
    protected LinkButton lbtnSearch;
    protected LinkButton lbtnViewImg;
    protected LinkButton lbtnViewTxt;
    protected Repeater rptList1;
    protected Repeater rptList2;
    protected TextBox txtPageNum;
    protected HtmlGenericControl PageContent;

    protected void Page_Load(object sender, EventArgs e)
    {
      this.channel_id = DTRequest.GetQueryInt("channel_id");
      this.category_id = DTRequest.GetQueryInt("category_id");
      this.keywords = DTRequest.GetQueryString("keywords");
      this.property = DTRequest.GetQueryString("property");
      if (this.channel_id == 0)
      {
        this.JscriptMsg("频道参数不正确！", "back");
      }
      else
      {
        this.channel_name = new Rain.BLL.channel().GetChannelName(this.channel_id);
        this.pageSize = this.GetPageSize(10);
        this.prolistview = Utils.GetCookie("article_list_view");
        if (this.Page.IsPostBack)
          return;
        this.ChkAdminLevel("channel_" + this.channel_name + "_list", DTEnums.ActionEnum.View.ToString());
        this.TreeBind(this.channel_id);
        this.RptBind(this.channel_id, this.category_id, "id>0" + this.CombSqlTxt(this.keywords, this.property), "sort_id asc,add_time desc,id desc");
      }
    }

    private void TreeBind(int _channel_id)
    {
      DataTable list = new Rain.BLL.article_category().GetList(0, _channel_id);
      this.ddlCategoryId.Items.Clear();
      this.ddlCategoryId.Items.Add(new ListItem("所有类别", ""));
      foreach (DataRow row in (InternalDataCollectionBase) list.Rows)
      {
        string str1 = row["id"].ToString();
        int num = int.Parse(row["class_layer"].ToString());
        string text = row["title"].ToString().Trim();
        if (num == 1)
        {
          this.ddlCategoryId.Items.Add(new ListItem(text, str1));
        }
        else
        {
          string str2 = "├ " + text;
          this.ddlCategoryId.Items.Add(new ListItem(Utils.StringOfChar(num - 1, "　") + str2, str1));
        }
      }
    }

    private void RptBind(int _channel_id, int _category_id, string _strWhere, string _orderby)
    {
      this.page = DTRequest.GetQueryInt("page", 1);
      if (this.category_id > 0)
        this.ddlCategoryId.SelectedValue = _category_id.ToString();
      this.ddlProperty.SelectedValue = this.property;
      this.txtKeywords.Text = this.keywords;
      Rain.BLL.article article = new Rain.BLL.article();
      switch (this.prolistview)
      {
        case "Txt":
          this.rptList2.Visible = false;
          this.rptList1.DataSource = (object) article.GetList(_channel_id, _category_id, this.pageSize, this.page, _strWhere, _orderby, out this.totalCount);
          this.rptList1.DataBind();
          break;
        default:
          this.rptList1.Visible = false;
          this.rptList2.DataSource = (object) article.GetList(_channel_id, _category_id, this.pageSize, this.page, _strWhere, _orderby, out this.totalCount);
          this.rptList2.DataBind();
          break;
      }
      this.txtPageNum.Text = this.pageSize.ToString();
      this.PageContent.InnerHtml = Utils.OutPageList(this.pageSize, this.page, this.totalCount, Utils.CombUrlTxt("article_list.aspx", "channel_id={0}&category_id={1}&keywords={2}&property={3}&page={4}", _channel_id.ToString(), _category_id.ToString(), this.keywords, this.property, "__id__"), 8);
    }

    protected string CombSqlTxt(string _keywords, string _property)
    {
      StringBuilder stringBuilder = new StringBuilder();
      _keywords = _keywords.Replace("'", "");
      if (!string.IsNullOrEmpty(_keywords))
        stringBuilder.Append(" and title like '%" + _keywords + "%'");
      if (!string.IsNullOrEmpty(_property))
      {
        switch (_property)
        {
          case "isLock":
            stringBuilder.Append(" and status=1");
            break;
          case "unIsLock":
            stringBuilder.Append(" and status=0");
            break;
          case "isMsg":
            stringBuilder.Append(" and is_msg=1");
            break;
          case "isTop":
            stringBuilder.Append(" and is_top=1");
            break;
          case "isRed":
            stringBuilder.Append(" and is_red=1");
            break;
          case "isHot":
            stringBuilder.Append(" and is_hot=1");
            break;
          case "isSlide":
            stringBuilder.Append(" and is_slide=1");
            break;
        }
      }
      return stringBuilder.ToString();
    }

    private int GetPageSize(int _default_size)
    {
      int result;
      if (int.TryParse(Utils.GetCookie("article_page_size", "UScmsPage"), out result) && result > 0)
        return result;
      return _default_size;
    }

    protected void rptList_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
      this.ChkAdminLevel("channel_" + this.channel_name + "_list", DTEnums.ActionEnum.Edit.ToString());
      int int32 = Convert.ToInt32(((HiddenField) e.Item.FindControl("hidId")).Value);
      Rain.BLL.article article = new Rain.BLL.article();
      Rain.Model.article model = article.GetModel(int32);
      switch (e.CommandName)
      {
        case "lbtnIsMsg":
          if (model.is_msg == 1)
          {
            article.UpdateField(int32, "is_msg=0");
            break;
          }
          article.UpdateField(int32, "is_msg=1");
          break;
        case "lbtnIsTop":
          if (model.is_top == 1)
          {
            article.UpdateField(int32, "is_top=0");
            break;
          }
          article.UpdateField(int32, "is_top=1");
          break;
        case "lbtnIsRed":
          if (model.is_red == 1)
          {
            article.UpdateField(int32, "is_red=0");
            break;
          }
          article.UpdateField(int32, "is_red=1");
          break;
        case "lbtnIsHot":
          if (model.is_hot == 1)
          {
            article.UpdateField(int32, "is_hot=0");
            break;
          }
          article.UpdateField(int32, "is_hot=1");
          break;
        case "lbtnIsSlide":
          if (model.is_slide == 1)
          {
            article.UpdateField(int32, "is_slide=0");
            break;
          }
          article.UpdateField(int32, "is_slide=1");
          break;
      }
      this.RptBind(this.channel_id, this.category_id, "id>0" + this.CombSqlTxt(this.keywords, this.property), "sort_id asc,add_time desc,id desc");
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
      this.Response.Redirect(Utils.CombUrlTxt("article_list.aspx", "channel_id={0}&category_id={1}&keywords={2}&property={3}", this.channel_id.ToString(), this.category_id.ToString(), this.txtKeywords.Text, this.property));
    }

    protected void ddlCategoryId_SelectedIndexChanged(object sender, EventArgs e)
    {
      this.Response.Redirect(Utils.CombUrlTxt("article_list.aspx", "channel_id={0}&category_id={1}&keywords={2}&property={3}", this.channel_id.ToString(), this.ddlCategoryId.SelectedValue, this.keywords, this.property));
    }

    protected void ddlProperty_SelectedIndexChanged(object sender, EventArgs e)
    {
      this.Response.Redirect(Utils.CombUrlTxt("article_list.aspx", "channel_id={0}&category_id={1}&keywords={2}&property={3}", this.channel_id.ToString(), this.category_id.ToString(), this.keywords, this.ddlProperty.SelectedValue));
    }

    protected void lbtnViewTxt_Click(object sender, EventArgs e)
    {
      Utils.WriteCookie("article_list_view", "Txt", 14400);
      this.Response.Redirect(Utils.CombUrlTxt("article_list.aspx", "channel_id={0}&category_id={1}&keywords={2}&property={3}&page={4}", this.channel_id.ToString(), this.category_id.ToString(), this.keywords, this.property, this.page.ToString()));
    }

    protected void lbtnViewImg_Click(object sender, EventArgs e)
    {
      Utils.WriteCookie("article_list_view", "Img", 14400);
      this.Response.Redirect(Utils.CombUrlTxt("article_list.aspx", "channel_id={0}&category_id={1}&keywords={2}&property={3}&page={4}", this.channel_id.ToString(), this.category_id.ToString(), this.keywords, this.property, this.page.ToString()));
    }

    protected void txtPageNum_TextChanged(object sender, EventArgs e)
    {
      int result;
      if (int.TryParse(this.txtPageNum.Text.Trim(), out result) && result > 0)
        Utils.WriteCookie("article_page_size", "UScmsPage", result.ToString(), 43200);
      this.Response.Redirect(Utils.CombUrlTxt("article_list.aspx", "channel_id={0}&category_id={1}&keywords={2}&property={3}", this.channel_id.ToString(), this.category_id.ToString(), this.keywords, this.property));
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
      this.ChkAdminLevel("channel_" + this.channel_name + "_list", DTEnums.ActionEnum.Edit.ToString());
      Rain.BLL.article article = new Rain.BLL.article();
      Repeater repeater1 = new Repeater();
      Repeater repeater2;
      switch (this.prolistview)
      {
        case "Txt":
          repeater2 = this.rptList1;
          break;
        default:
          repeater2 = this.rptList2;
          break;
      }
      for (int index = 0; index < repeater2.Items.Count; ++index)
      {
        int int32 = Convert.ToInt32(((HiddenField) repeater2.Items[index].FindControl("hidId")).Value);
        int result;
        if (!int.TryParse(((TextBox) repeater2.Items[index].FindControl("txtSortId")).Text.Trim(), out result))
          result = 99;
        article.UpdateField(int32, "sort_id=" + result.ToString());
      }
      this.AddAdminLog(DTEnums.ActionEnum.Edit.ToString(), "保存" + this.channel_name + "频道内容排序");
      this.JscriptMsg("保存排序成功！", Utils.CombUrlTxt("article_list.aspx", "channel_id={0}&category_id={1}&keywords={2}&property={3}", this.channel_id.ToString(), this.category_id.ToString(), this.keywords, this.property));
    }

    protected void btnAudit_Click(object sender, EventArgs e)
    {
      this.ChkAdminLevel("channel_" + this.channel_name + "_list", DTEnums.ActionEnum.Audit.ToString());
      Rain.BLL.article article = new Rain.BLL.article();
      Repeater repeater1 = new Repeater();
      Repeater repeater2;
      switch (this.prolistview)
      {
        case "Txt":
          repeater2 = this.rptList1;
          break;
        default:
          repeater2 = this.rptList2;
          break;
      }
      for (int index = 0; index < repeater2.Items.Count; ++index)
      {
        int int32 = Convert.ToInt32(((HiddenField) repeater2.Items[index].FindControl("hidId")).Value);
        if (((CheckBox) repeater2.Items[index].FindControl("chkId")).Checked)
          article.UpdateField(int32, "status=0");
      }
      this.AddAdminLog(DTEnums.ActionEnum.Audit.ToString(), "审核" + this.channel_name + "频道内容信息");
      this.JscriptMsg("批量审核成功！", Utils.CombUrlTxt("article_list.aspx", "channel_id={0}&category_id={1}&keywords={2}&property={3}", this.channel_id.ToString(), this.category_id.ToString(), this.keywords, this.property));
    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {
      this.ChkAdminLevel("channel_" + this.channel_name + "_list", DTEnums.ActionEnum.Delete.ToString());
      int num1 = 0;
      int num2 = 0;
      Rain.BLL.article article = new Rain.BLL.article();
      Repeater repeater1 = new Repeater();
      Repeater repeater2;
      switch (this.prolistview)
      {
        case "Txt":
          repeater2 = this.rptList1;
          break;
        default:
          repeater2 = this.rptList2;
          break;
      }
      for (int index = 0; index < repeater2.Items.Count; ++index)
      {
        int int32 = Convert.ToInt32(((HiddenField) repeater2.Items[index].FindControl("hidId")).Value);
        if (((CheckBox) repeater2.Items[index].FindControl("chkId")).Checked)
        {
          if (article.Delete(int32))
            ++num1;
          else
            ++num2;
        }
      }
      this.AddAdminLog(DTEnums.ActionEnum.Edit.ToString(), "删除" + this.channel_name + "频道内容成功" + (object) num1 + "条，失败" + (object) num2 + "条");
      this.JscriptMsg("删除成功" + (object) num1 + "条，失败" + (object) num2 + "条！", Utils.CombUrlTxt("article_list.aspx", "channel_id={0}&category_id={1}&keywords={2}&property={3}", this.channel_id.ToString(), this.category_id.ToString(), this.keywords, this.property));
    }
  }
}
