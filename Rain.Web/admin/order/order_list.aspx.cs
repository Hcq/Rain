// Decompiled with JetBrains decompiler
// Type: Rain.Web.admin.order.order_list
// Assembly: Rain.Web, Version=4.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 7C74D3EC-D223-4A13-9304-5BE7CB5803E5
// Assembly location: C:\Users\Felix.Ho\Desktop\page\bin\Rain.Web.dll

using System;
using System.Text;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Rain.Common;
using Rain.Web.UI;

namespace Rain.Web.admin.order
{
  public class order_list : ManagePage
  {
    protected string keywords = string.Empty;
    protected HtmlForm form1;
    protected LinkButton btnDelete;
    protected DropDownList ddlStatus;
    protected DropDownList ddlPaymentStatus;
    protected DropDownList ddlExpressStatus;
    protected TextBox txtKeywords;
    protected LinkButton lbtnSearch;
    protected Repeater rptList;
    protected TextBox txtPageNum;
    protected HtmlGenericControl PageContent;
    protected int totalCount;
    protected int page;
    protected int pageSize;
    protected int status;
    protected int payment_status;
    protected int express_status;

    protected void Page_Load(object sender, EventArgs e)
    {
      this.status = DTRequest.GetQueryInt("status");
      this.payment_status = DTRequest.GetQueryInt("payment_status");
      this.express_status = DTRequest.GetQueryInt("express_status");
      this.keywords = DTRequest.GetQueryString("keywords");
      this.pageSize = this.GetPageSize(10);
      if (this.Page.IsPostBack)
        return;
      this.ChkAdminLevel(nameof (order_list), DTEnums.ActionEnum.View.ToString());
      this.RptBind("id>0" + this.CombSqlTxt(this.status, this.payment_status, this.express_status, this.keywords), "status asc,add_time desc,id desc");
    }

    private void RptBind(string _strWhere, string _orderby)
    {
      this.page = DTRequest.GetQueryInt("page", 1);
      if (this.status > 0)
        this.ddlStatus.SelectedValue = this.status.ToString();
      if (this.payment_status > 0)
        this.ddlPaymentStatus.SelectedValue = this.payment_status.ToString();
      if (this.express_status > 0)
        this.ddlExpressStatus.SelectedValue = this.express_status.ToString();
      this.txtKeywords.Text = this.keywords;
      this.rptList.DataSource = (object) new Rain.BLL.orders().GetList(this.pageSize, this.page, _strWhere, _orderby, out this.totalCount);
      this.rptList.DataBind();
      this.txtPageNum.Text = this.pageSize.ToString();
      this.PageContent.InnerHtml = Utils.OutPageList(this.pageSize, this.page, this.totalCount, Utils.CombUrlTxt("order_list.aspx", "status={0}&payment_status={1}&express_status={2}&keywords={3}&page={4}", this.status.ToString(), this.payment_status.ToString(), this.express_status.ToString(), this.keywords, "__id__"), 8);
    }

    protected string CombSqlTxt(
      int _status,
      int _payment_status,
      int _express_status,
      string _keywords)
    {
      StringBuilder stringBuilder = new StringBuilder();
      if (_status > 0)
        stringBuilder.Append(" and status=" + (object) _status);
      if (_payment_status > 0)
        stringBuilder.Append(" and payment_status=" + (object) _payment_status);
      if (_express_status > 0)
        stringBuilder.Append(" and express_status=" + (object) _express_status);
      _keywords = _keywords.Replace("'", "");
      if (!string.IsNullOrEmpty(_keywords))
        stringBuilder.Append(" and (order_no like '%" + _keywords + "%' or user_name like '%" + _keywords + "%' or accept_name like '%" + _keywords + "%')");
      return stringBuilder.ToString();
    }

    private int GetPageSize(int _default_size)
    {
      int result;
      if (int.TryParse(Utils.GetCookie("order_list_page_size", "UScmsPage"), out result) && result > 0)
        return result;
      return _default_size;
    }

    protected string GetOrderStatus(int _id)
    {
      string str = string.Empty;
      Rain.Model.orders model = new Rain.BLL.orders().GetModel(_id);
      switch (model.status)
      {
        case 1:
          str = model.payment_status <= 0 ? "待确认" : "待付款";
          break;
        case 2:
          str = model.express_status <= 1 ? "待发货" : "已发货";
          break;
        case 3:
          str = "交易完成";
          break;
        case 4:
          str = "已取消";
          break;
        case 5:
          str = "已作废";
          break;
      }
      return str;
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
      this.Response.Redirect(Utils.CombUrlTxt("order_list.aspx", "status={0}&payment_status={1}&express_status={2}&keywords={3}", this.status.ToString(), this.payment_status.ToString(), this.express_status.ToString(), this.txtKeywords.Text));
    }

    protected void ddlStatus_SelectedIndexChanged(object sender, EventArgs e)
    {
      this.Response.Redirect(Utils.CombUrlTxt("order_list.aspx", "status={0}&payment_status={1}&express_status={2}&keywords={3}", this.ddlStatus.SelectedValue, this.payment_status.ToString(), this.express_status.ToString(), this.keywords));
    }

    protected void ddlPaymentStatus_SelectedIndexChanged(object sender, EventArgs e)
    {
      this.Response.Redirect(Utils.CombUrlTxt("order_list.aspx", "status={0}&payment_status={1}&express_status={2}&keywords={3}", this.status.ToString(), this.ddlPaymentStatus.SelectedValue, this.express_status.ToString(), this.keywords));
    }

    protected void ddlExpressStatus_SelectedIndexChanged(object sender, EventArgs e)
    {
      this.Response.Redirect(Utils.CombUrlTxt("order_list.aspx", "status={0}&payment_status={1}&express_status={2}&keywords={3}", this.status.ToString(), this.payment_status.ToString(), this.ddlExpressStatus.SelectedValue, this.keywords));
    }

    protected void txtPageNum_TextChanged(object sender, EventArgs e)
    {
      int result;
      if (int.TryParse(this.txtPageNum.Text.Trim(), out result) && result > 0)
        Utils.WriteCookie("order_list_page_size", "UScmsPage", result.ToString(), 14400);
      this.Response.Redirect(Utils.CombUrlTxt("user_list.aspx", "status={0}&payment_status={1}&express_status={2}&keywords={3}", this.status.ToString(), this.payment_status.ToString(), this.express_status.ToString(), this.keywords));
    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {
      this.ChkAdminLevel(nameof (order_list), DTEnums.ActionEnum.Delete.ToString());
      int num1 = 0;
      int num2 = 0;
      Rain.BLL.orders orders = new Rain.BLL.orders();
      for (int index = 0; index < this.rptList.Items.Count; ++index)
      {
        int int32 = Convert.ToInt32(((HiddenField) this.rptList.Items[index].FindControl("hidId")).Value);
        if (((CheckBox) this.rptList.Items[index].FindControl("chkId")).Checked)
        {
          if (orders.Delete(int32))
            ++num1;
          else
            ++num2;
        }
      }
      this.AddAdminLog(DTEnums.ActionEnum.Delete.ToString(), "删除订单成功" + (object) num1 + "条，失败" + (object) num2 + "条");
      this.JscriptMsg("删除成功" + (object) num1 + "条，失败" + (object) num2 + "条！", Utils.CombUrlTxt("order_list.aspx", "status={0}&payment_status={1}&express_status={2}&keywords={3}", this.status.ToString(), this.payment_status.ToString(), this.express_status.ToString(), this.keywords));
    }
  }
}
