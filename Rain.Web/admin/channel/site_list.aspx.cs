// Decompiled with JetBrains decompiler
// Type: Rain.Web.admin.channel.site_list
// Assembly: Rain.Web, Version=4.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 7C74D3EC-D223-4A13-9304-5BE7CB5803E5
// Assembly location: C:\Users\Felix.Ho\Desktop\page\bin\Rain.Web.dll

using System;
using System.Text;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Rain.Common;
using Rain.Web.UI;

namespace Rain.Web.admin.channel
{
  public class site_list : ManagePage
  {
    protected string keywords = string.Empty;
    protected int totalCount;
    protected int page;
    protected int pageSize;
    protected HtmlForm form1;
    protected LinkButton btnSave;
    protected LinkButton btnDelete;
    protected TextBox txtKeywords;
    protected LinkButton lbtnSearch;
    protected Repeater rptList;
    protected TextBox txtPageNum;
    protected HtmlGenericControl PageContent;

    protected void Page_Load(object sender, EventArgs e)
    {
      this.keywords = DTRequest.GetQueryString("keywords");
      this.pageSize = this.GetPageSize(10);
      if (this.Page.IsPostBack)
        return;
      this.ChkAdminLevel("sys_site_manage", DTEnums.ActionEnum.View.ToString());
      this.RptBind("id>0" + this.CombSqlTxt(this.keywords), "sort_id asc,id desc");
    }

    private void RptBind(string _strWhere, string _orderby)
    {
      this.page = DTRequest.GetQueryInt("page", 1);
      this.txtKeywords.Text = this.keywords;
      this.rptList.DataSource = (object) new Rain.BLL.channel_site().GetList(this.pageSize, this.page, _strWhere, _orderby, out this.totalCount);
      this.rptList.DataBind();
      this.txtPageNum.Text = this.pageSize.ToString();
      this.PageContent.InnerHtml = Utils.OutPageList(this.pageSize, this.page, this.totalCount, Utils.CombUrlTxt("site_list.aspx", "keywords={0}&page={1}", this.keywords, "__id__"), 8);
    }

    protected string CombSqlTxt(string _keywords)
    {
      StringBuilder stringBuilder = new StringBuilder();
      _keywords = _keywords.Replace("'", "");
      if (!string.IsNullOrEmpty(_keywords))
        stringBuilder.Append(" and (title like  '%" + _keywords + "%' or name like  '%" + _keywords + "%' or build_path like '%" + _keywords + "%' or domain like '%" + _keywords + "%')");
      return stringBuilder.ToString();
    }

    private int GetPageSize(int _default_size)
    {
      int result;
      if (int.TryParse(Utils.GetCookie("channel_site_page_size", "UScmsPage"), out result) && result > 0)
        return result;
      return _default_size;
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
      this.Response.Redirect(Utils.CombUrlTxt("site_list.aspx", "keywords={0}", this.txtKeywords.Text));
    }

    protected void txtPageNum_TextChanged(object sender, EventArgs e)
    {
      int result;
      if (int.TryParse(this.txtPageNum.Text.Trim(), out result) && result > 0)
        Utils.WriteCookie("channel_site_page_size", "UScmsPage", result.ToString(), 14400);
      this.Response.Redirect(Utils.CombUrlTxt("site_list.aspx", "keywords={0}", this.keywords));
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
      this.ChkAdminLevel("sys_site_manage", DTEnums.ActionEnum.Edit.ToString());
      Rain.BLL.channel_site channelSite = new Rain.BLL.channel_site();
      for (int index = 0; index < this.rptList.Items.Count; ++index)
      {
        int int32 = Convert.ToInt32(((HiddenField) this.rptList.Items[index].FindControl("hidId")).Value);
        int result;
        if (!int.TryParse(((TextBox) this.rptList.Items[index].FindControl("txtSortId")).Text.Trim(), out result))
          result = 99;
        channelSite.UpdateSort(int32, result);
      }
      this.AddAdminLog(DTEnums.ActionEnum.Edit.ToString(), "保存站点排序");
      this.JscriptMsg("保存排序成功！", Utils.CombUrlTxt("site_list.aspx", "keywords={0}", this.keywords), "parent.loadMenuTree");
    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {
      this.ChkAdminLevel("sys_site_manage", DTEnums.ActionEnum.Delete.ToString());
      int num1 = 0;
      int num2 = 0;
      Rain.BLL.channel_site channelSite = new Rain.BLL.channel_site();
      for (int index = 0; index < this.rptList.Items.Count; ++index)
      {
        int int32 = Convert.ToInt32(((HiddenField) this.rptList.Items[index].FindControl("hidId")).Value);
        if (((CheckBox) this.rptList.Items[index].FindControl("chkId")).Checked)
        {
          if (new Rain.BLL.channel().GetCount("site_id=" + (object) int32) > 0)
          {
            ++num2;
          }
          else
          {
            Rain.Model.channel_site model = channelSite.GetModel(int32);
            if (channelSite.Delete(int32))
            {
              ++num1;
              Utils.DeleteDirectory(this.siteConfig.webpath + "aspx/" + model.build_path);
              Utils.DeleteDirectory(this.siteConfig.webpath + "html/" + model.build_path);
            }
            else
              ++num2;
          }
        }
      }
      this.AddAdminLog(DTEnums.ActionEnum.Delete.ToString(), "删除站点成功" + (object) num1 + "条，失败" + (object) num2 + "条");
      this.JscriptMsg("删除成功" + (object) num1 + "条，失败" + (object) num2 + "条！", Utils.CombUrlTxt("site_list.aspx", "keywords={0}", this.keywords), "parent.loadMenuTree");
    }
  }
}
