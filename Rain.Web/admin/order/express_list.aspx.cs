// Decompiled with JetBrains decompiler
// Type: Rain.Web.admin.order.express_list
// Assembly: Rain.Web, Version=4.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 7C74D3EC-D223-4A13-9304-5BE7CB5803E5
// Assembly location: C:\Users\Felix.Ho\Desktop\page\bin\Rain.Web.dll

using System;
using System.Text;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Rain.BLL;
using Rain.Common;
using Rain.Web.UI;

namespace Rain.Web.admin.order
{
  public class express_list : ManagePage
  {
    protected string keywords = string.Empty;
    protected HtmlForm form1;
    protected LinkButton btnSave;
    protected LinkButton btnDelete;
    protected TextBox txtKeywords;
    protected LinkButton lbtnSearch;
    protected Repeater rptList;
    protected TextBox txtPageNum;
    protected HtmlGenericControl PageContent;
    protected int totalCount;
    protected int page;
    protected int pageSize;

    protected void Page_Load(object sender, EventArgs e)
    {
      this.keywords = DTRequest.GetQueryString("keywords");
      this.pageSize = this.GetPageSize(10);
      if (this.Page.IsPostBack)
        return;
      this.ChkAdminLevel("order_express", DTEnums.ActionEnum.View.ToString());
      this.RptBind("id>0" + this.CombSqlTxt(this.keywords), "sort_id asc,id desc");
    }

    private void RptBind(string _strWhere, string _orderby)
    {
      this.page = DTRequest.GetQueryInt("page", 1);
      this.txtKeywords.Text = this.keywords;
      this.rptList.DataSource = (object) new express().GetList(this.pageSize, this.page, _strWhere, _orderby, out this.totalCount);
      this.rptList.DataBind();
      this.txtPageNum.Text = this.pageSize.ToString();
      this.PageContent.InnerHtml = Utils.OutPageList(this.pageSize, this.page, this.totalCount, Utils.CombUrlTxt("express_list.aspx", "keywords={0}&page={1}", this.keywords, "__id__"), 8);
    }

    protected string CombSqlTxt(string _keywords)
    {
      StringBuilder stringBuilder = new StringBuilder();
      _keywords = _keywords.Replace("'", "");
      if (!string.IsNullOrEmpty(_keywords))
        stringBuilder.Append(" and (title like  '%" + _keywords + "%' or express_code like '%" + _keywords + "%')");
      return stringBuilder.ToString();
    }

    private int GetPageSize(int _default_size)
    {
      int result;
      if (int.TryParse(Utils.GetCookie("express_page_size", "UScmsPage"), out result) && result > 0)
        return result;
      return _default_size;
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
      this.Response.Redirect(Utils.CombUrlTxt("express_list.aspx", "keywords={0}", this.txtKeywords.Text));
    }

    protected void txtPageNum_TextChanged(object sender, EventArgs e)
    {
      int result;
      if (int.TryParse(this.txtPageNum.Text.Trim(), out result) && result > 0)
        Utils.WriteCookie("express_page_size", "UScmsPage", result.ToString(), 14400);
      this.Response.Redirect(Utils.CombUrlTxt("express_list.aspx", "keywords={0}", this.keywords));
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
      this.ChkAdminLevel("order_express", DTEnums.ActionEnum.Edit.ToString());
      express express = new express();
      for (int index = 0; index < this.rptList.Items.Count; ++index)
      {
        int int32 = Convert.ToInt32(((HiddenField) this.rptList.Items[index].FindControl("hidId")).Value);
        int result;
        if (!int.TryParse(((TextBox) this.rptList.Items[index].FindControl("txtSortId")).Text.Trim(), out result))
          result = 99;
        express.UpdateField(int32, "sort_id=" + result.ToString());
      }
      this.AddAdminLog(DTEnums.ActionEnum.Edit.ToString(), "保存配送方式排序");
      this.JscriptMsg("保存排序成功！", Utils.CombUrlTxt("express_list.aspx", "keywords={0}", this.keywords));
    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {
      this.ChkAdminLevel("order_express", DTEnums.ActionEnum.Delete.ToString());
      int num1 = 0;
      int num2 = 0;
      express express = new express();
      for (int index = 0; index < this.rptList.Items.Count; ++index)
      {
        int int32 = Convert.ToInt32(((HiddenField) this.rptList.Items[index].FindControl("hidId")).Value);
        if (((CheckBox) this.rptList.Items[index].FindControl("chkId")).Checked)
        {
          if (express.Delete(int32))
            ++num1;
          else
            ++num2;
        }
      }
      this.AddAdminLog(DTEnums.ActionEnum.Delete.ToString(), "删除配送方式成功" + (object) num1 + "条，失败" + (object) num2 + "条");
      this.JscriptMsg("删除成功" + (object) num1 + "条，失败" + (object) num2 + "条！", Utils.CombUrlTxt("express_list.aspx", "keywords={0}", this.keywords));
    }
  }
}
