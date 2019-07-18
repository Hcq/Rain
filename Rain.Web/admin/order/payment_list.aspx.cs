// Decompiled with JetBrains decompiler
// Type: Rain.Web.admin.order.payment_list
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
  public class payment_list : ManagePage
  {
    protected string keywords = string.Empty;
    protected HtmlForm form1;
    protected LinkButton btnSave;
    protected TextBox txtKeywords;
    protected LinkButton lbtnSearch;
    protected Repeater rptList;

    protected void Page_Load(object sender, EventArgs e)
    {
      if (this.Page.IsPostBack)
        return;
      this.ChkAdminLevel("order_payment", DTEnums.ActionEnum.View.ToString());
      this.RptBind("id>0" + this.CombSqlTxt(this.keywords), "sort_id asc,id desc");
    }

    private void RptBind(string _strWhere, string _orderby)
    {
      this.txtKeywords.Text = this.keywords;
      this.rptList.DataSource = (object) new payment().GetList(0, _strWhere, _orderby);
      this.rptList.DataBind();
    }

    protected string CombSqlTxt(string _keywords)
    {
      StringBuilder stringBuilder = new StringBuilder();
      _keywords = _keywords.Replace("'", "");
      if (!string.IsNullOrEmpty(_keywords))
        stringBuilder.Append(" and title like  '%" + _keywords + "%'");
      return stringBuilder.ToString();
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
      this.Response.Redirect(Utils.CombUrlTxt("payment_list.aspx", "keywords={0}", this.txtKeywords.Text));
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
      this.ChkAdminLevel("order_payment", DTEnums.ActionEnum.Edit.ToString());
      payment payment = new payment();
      for (int index = 0; index < this.rptList.Items.Count; ++index)
      {
        int int32 = Convert.ToInt32(((HiddenField) this.rptList.Items[index].FindControl("hidId")).Value);
        int result;
        if (!int.TryParse(((TextBox) this.rptList.Items[index].FindControl("txtSortId")).Text.Trim(), out result))
          result = 99;
        payment.UpdateField(int32, "sort_id=" + result.ToString());
      }
      this.AddAdminLog(DTEnums.ActionEnum.Edit.ToString(), "保存支付方式排序");
      this.JscriptMsg("保存排序成功！", Utils.CombUrlTxt("payment_list.aspx", "keywords={0}", this.keywords));
    }
  }
}
