// Decompiled with JetBrains decompiler
// Type: Rain.Web.admin.manager.manager_log
// Assembly: Rain.Web, Version=4.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 7C74D3EC-D223-4A13-9304-5BE7CB5803E5
// Assembly location: C:\Users\Felix.Ho\Desktop\page\bin\Rain.Web.dll

using System;
using System.Text;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Rain.Common;
using Rain.Web.UI;

namespace Rain.Web.admin.manager
{
  public class manager_log : ManagePage
  {
    protected string keywords = string.Empty;
    private Rain.Model.manager model = new Rain.Model.manager();
    protected int totalCount;
    protected int page;
    protected int pageSize;
    protected HtmlForm form1;
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
      this.ChkAdminLevel(nameof (manager_log), DTEnums.ActionEnum.View.ToString());
      this.model = this.GetAdminInfo();
      this.RptBind("id>0" + this.CombSqlTxt(this.keywords), "add_time desc,id desc");
    }

    protected string CombSqlTxt(string _keywords)
    {
      StringBuilder stringBuilder = new StringBuilder();
      _keywords = _keywords.Replace("'", "");
      if (!string.IsNullOrEmpty(_keywords))
        stringBuilder.Append(" and (user_name like  '%" + _keywords + "%' or action_type like '%" + _keywords + "%')");
      return stringBuilder.ToString();
    }

    private void RptBind(string _strWhere, string _orderby)
    {
      this.page = DTRequest.GetQueryInt("page", 1);
      this.txtKeywords.Text = this.keywords;
      this.rptList.DataSource = (object) new Rain.BLL.manager_log().GetList(this.pageSize, this.page, _strWhere, _orderby, out this.totalCount);
      this.rptList.DataBind();
      this.txtPageNum.Text = this.pageSize.ToString();
      this.PageContent.InnerHtml = Utils.OutPageList(this.pageSize, this.page, this.totalCount, Utils.CombUrlTxt("manager_log.aspx", "keywords={0}&page={1}", this.keywords, "__id__"), 8);
    }

    private int GetPageSize(int _default_size)
    {
      int result;
      if (int.TryParse(Utils.GetCookie("manager_page_size", "UScmsPage"), out result) && result > 0)
        return result;
      return _default_size;
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
      this.Response.Redirect(Utils.CombUrlTxt("manager_log.aspx", "keywords={0}", this.txtKeywords.Text));
    }

    protected void txtPageNum_TextChanged(object sender, EventArgs e)
    {
      int result;
      if (int.TryParse(this.txtPageNum.Text.Trim(), out result) && result > 0)
        Utils.WriteCookie("manager_page_size", "UScmsPage", result.ToString(), 14400);
      this.Response.Redirect(Utils.CombUrlTxt("manager_log.aspx", "keywords={0}", this.keywords));
    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {
      this.ChkAdminLevel(nameof (manager_log), DTEnums.ActionEnum.Delete.ToString());
      int num = new Rain.BLL.manager_log().Delete(7);
      this.AddAdminLog(DTEnums.ActionEnum.Delete.ToString(), "删除管理日志" + (object) num + "条");
      this.JscriptMsg("删除日志" + (object) num + "条", Utils.CombUrlTxt("manager_log.aspx", "keywords={0}", this.keywords));
    }
  }
}
