// Decompiled with JetBrains decompiler
// Type: Rain.Web.admin.order.order_edit
// Assembly: Rain.Web, Version=4.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 7C74D3EC-D223-4A13-9304-5BE7CB5803E5
// Assembly location: C:\Users\Felix.Ho\Desktop\page\bin\Rain.Web.dll

using System;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Rain.Common;
using Rain.Web.UI;

namespace Rain.Web.admin.order
{
  public class order_edit : ManagePage
  {
    private int id = 0;
    protected Rain.Model.orders model = new Rain.Model.orders();
    protected HtmlForm form1;
    protected Repeater rptList;
    protected HtmlInputButton btnEditAcceptInfo;
    protected HtmlGenericControl dlUserInfo;
    protected Label lbUserName;
    protected Label lbUserGroup;
    protected Label lbUserDiscount;
    protected Label lbUserAmount;
    protected Label lbUserPoint;
    protected HtmlInputButton btnEditRemark;
    protected HtmlInputButton btnEditRealAmount;
    protected HtmlInputButton btnEditExpressFee;
    protected HtmlInputButton btnEditPaymentFee;
    protected HtmlInputButton btnEditInvoiceTaxes;
    protected HtmlInputButton btnConfirm;
    protected HtmlInputButton btnPayment;
    protected HtmlInputButton btnExpress;
    protected HtmlInputButton btnComplete;
    protected HtmlInputButton btnCancel;
    protected HtmlInputButton btnInvalid;

    protected void Page_Load(object sender, EventArgs e)
    {
      this.id = DTRequest.GetQueryInt("id");
      if (this.id == 0)
        this.JscriptMsg("传输参数不正确！", "back");
      else if (!new Rain.BLL.orders().Exists(this.id))
      {
        this.JscriptMsg("记录不存在或已被删除！", "back");
      }
      else
      {
        if (this.Page.IsPostBack)
          return;
        this.ChkAdminLevel("order_list", DTEnums.ActionEnum.View.ToString());
        this.ShowInfo(this.id);
      }
    }

    private void ShowInfo(int _id)
    {
      this.model = new Rain.BLL.orders().GetModel(_id);
      this.rptList.DataSource = (object) this.model.order_goods;
      this.rptList.DataBind();
      if (this.model.user_id > 0)
      {
        Rain.Model.users model1 = new Rain.BLL.users().GetModel(this.model.user_id);
        if (model1 != null)
        {
          Rain.Model.user_groups model2 = new Rain.BLL.user_groups().GetModel(model1.group_id);
          if (model2 != null)
          {
            this.dlUserInfo.Visible = true;
            this.lbUserName.Text = model1.user_name;
            this.lbUserGroup.Text = model2.title;
            this.lbUserDiscount.Text = model2.discount.ToString() + " %";
            this.lbUserAmount.Text = model1.amount.ToString();
            this.lbUserPoint.Text = model1.point.ToString();
          }
        }
      }
      switch (this.model.status)
      {
        case 1:
          if (this.model.payment_status > 0)
            this.btnPayment.Visible = this.btnCancel.Visible = this.btnEditAcceptInfo.Visible = true;
          else
            this.btnConfirm.Visible = this.btnCancel.Visible = this.btnEditAcceptInfo.Visible = true;
          this.btnEditRemark.Visible = this.btnEditRealAmount.Visible = this.btnEditExpressFee.Visible = this.btnEditPaymentFee.Visible = this.btnEditInvoiceTaxes.Visible = true;
          break;
        case 2:
          if (this.model.express_status == 1)
            this.btnExpress.Visible = this.btnCancel.Visible = this.btnEditAcceptInfo.Visible = true;
          else if (this.model.express_status == 2)
            this.btnComplete.Visible = this.btnCancel.Visible = true;
          this.btnEditRemark.Visible = true;
          break;
        case 3:
          this.btnInvalid.Visible = this.btnEditRemark.Visible = true;
          break;
      }
      if (this.model.express_status != 2 || this.model.express_no.Trim().Length <= 0)
        return;
      new Rain.BLL.express().GetModel(this.model.express_id);
      new Rain.BLL.orderconfig().loadConfig();
    }
  }
}
