// Decompiled with JetBrains decompiler
// Type: Rain.Web.admin.channel.channel_list
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

namespace Rain.Web.admin.channel
{
  public class channel_list : ManagePage
  {
    protected string keywords = string.Empty;
    protected int totalCount;
    protected int page;
    protected int pageSize;
    protected int site_id;
    protected HtmlForm form1;
    protected LinkButton btnSave;
    protected LinkButton btnDelete;
    protected DropDownList ddlSiteId;
    protected TextBox txtKeywords;
    protected LinkButton lbtnSearch;
    protected Repeater rptList;
    protected TextBox txtPageNum;
    protected HtmlGenericControl PageContent;

    protected void Page_Load(object sender, EventArgs e)
    {
      this.site_id = DTRequest.GetQueryInt("site_id");
      this.keywords = DTRequest.GetQueryString("keywords");
      this.pageSize = this.GetPageSize(10);
      if (this.Page.IsPostBack)
        return;
      this.ChkAdminLevel("sys_channel_manage", DTEnums.ActionEnum.View.ToString());
      this.TreeBind();
      this.RptBind("id>0" + this.CombSqlTxt(this.site_id, this.keywords), "sort_id asc,id desc");
    }

    private void TreeBind()
    {
      DataTable table = new Rain.BLL.channel_site().GetList(0, "", "sort_id asc,id desc").Tables[0];
      this.ddlSiteId.Items.Clear();
      this.ddlSiteId.Items.Add(new ListItem("所有站点", ""));
      foreach (DataRow row in (InternalDataCollectionBase) table.Rows)
        this.ddlSiteId.Items.Add(new ListItem(row["title"].ToString(), row["id"].ToString()));
    }

    private void RptBind(string _strWhere, string _orderby)
    {
      this.page = DTRequest.GetQueryInt("page", 1);
      this.ddlSiteId.SelectedValue = this.site_id.ToString();
      this.txtKeywords.Text = this.keywords;
      this.rptList.DataSource = (object) new Rain.BLL.channel().GetList(this.pageSize, this.page, _strWhere, _orderby, out this.totalCount);
      this.rptList.DataBind();
      this.txtPageNum.Text = this.pageSize.ToString();
      this.PageContent.InnerHtml = Utils.OutPageList(this.pageSize, this.page, this.totalCount, Utils.CombUrlTxt("channel_list.aspx", "site_id={0}&keywords={1}&page={2}", this.site_id.ToString(), this.keywords, "__id__"), 8);
    }

    protected string CombSqlTxt(int _site_id, string _keywords)
    {
      StringBuilder stringBuilder = new StringBuilder();
      if (_site_id > 0)
        stringBuilder.Append(" and site_id=" + (object) _site_id);
      _keywords = _keywords.Replace("'", "");
      if (!string.IsNullOrEmpty(_keywords))
        stringBuilder.Append(" and (name like  '%" + _keywords + "%' or title like '%" + _keywords + "%')");
      return stringBuilder.ToString();
    }

    private int GetPageSize(int _default_size)
    {
      int result;
      if (int.TryParse(Utils.GetCookie("channel_page_size", "UScmsPage"), out result) && result > 0)
        return result;
      return _default_size;
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
      this.Response.Redirect(Utils.CombUrlTxt("channel_list.aspx", "site_id={0}&keywords={1}", this.site_id.ToString(), this.txtKeywords.Text));
    }

    protected void ddlSiteId_SelectedIndexChanged(object sender, EventArgs e)
    {
      this.Response.Redirect(Utils.CombUrlTxt("channel_list.aspx", "site_id={0}&keywords={1}", this.ddlSiteId.SelectedValue, this.keywords));
    }

    protected void txtPageNum_TextChanged(object sender, EventArgs e)
    {
      int result;
      if (int.TryParse(this.txtPageNum.Text.Trim(), out result) && result > 0)
        Utils.WriteCookie("channel_page_size", "UScmsPage", result.ToString(), 14400);
      this.Response.Redirect(Utils.CombUrlTxt("channel_list.aspx", "site_id={0}&keywords={1}", this.site_id.ToString(), this.keywords));
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
      this.ChkAdminLevel("sys_channel_manage", DTEnums.ActionEnum.Edit.ToString());
      Rain.BLL.channel channel = new Rain.BLL.channel();
      for (int index = 0; index < this.rptList.Items.Count; ++index)
      {
        int int32 = Convert.ToInt32(((HiddenField) this.rptList.Items[index].FindControl("hidId")).Value);
        int result;
        if (!int.TryParse(((TextBox) this.rptList.Items[index].FindControl("txtSortId")).Text.Trim(), out result))
          result = 99;
        channel.UpdateSort(int32, result);
      }
      this.AddAdminLog(DTEnums.ActionEnum.Edit.ToString(), "保存频道排序");
      this.JscriptMsg("保存排序成功！", Utils.CombUrlTxt("channel_list.aspx", "site_id={0}&keywords={1}", this.site_id.ToString(), this.keywords), "parent.loadMenuTree");
    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {
      this.ChkAdminLevel("sys_channel_manage", DTEnums.ActionEnum.Delete.ToString());
      int num1 = 0;
      int num2 = 0;
      Rain.BLL.channel channel = new Rain.BLL.channel();
      for (int index = 0; index < this.rptList.Items.Count; ++index)
      {
        int int32 = Convert.ToInt32(((HiddenField) this.rptList.Items[index].FindControl("hidId")).Value);
        if (((CheckBox) this.rptList.Items[index].FindControl("chkId")).Checked)
        {
          if (new Rain.BLL.article().GetCount("channel_id=" + (object) int32) > 0)
          {
            ++num2;
          }
          else
          {
            Rain.Model.channel model = channel.GetModel(int32);
            if (channel.Delete(int32))
            {
              ++num1;
              new Rain.BLL.url_rewrite().Remove("channel", model.name);
            }
            else
              ++num2;
          }
        }
      }
      this.AddAdminLog(DTEnums.ActionEnum.Delete.ToString(), "删除频道成功" + (object) num1 + "条，失败" + (object) num2 + "条");
      this.JscriptMsg("删除成功" + (object) num1 + "条，失败" + (object) num2 + "条！", Utils.CombUrlTxt("channel_list.aspx", "site_id={0}&keywords={1}", this.site_id.ToString(), this.keywords), "parent.loadMenuTree");
    }
  }
}
