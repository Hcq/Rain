// Decompiled with JetBrains decompiler
// Type: Rain.Web.admin.users.recharge_edit
// Assembly: Rain.Web, Version=4.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 7C74D3EC-D223-4A13-9304-5BE7CB5803E5
// Assembly location: C:\Users\Felix.Ho\Desktop\page\bin\Rain.Web.dll

using System;
using System.Data;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Rain.Common;
using Rain.Web.UI;

namespace Rain.Web.admin.users
{
  public class recharge_edit : ManagePage
  {
    private string username = string.Empty;
    protected HtmlForm form1;
    protected TextBox txtUserName;
    protected DropDownList ddlPaymentId;
    protected TextBox txtRechargeNo;
    protected TextBox txtAmount;
    protected Button btnSubmit;

    protected void Page_Load(object sender, EventArgs e)
    {
      this.username = DTRequest.GetQueryString("username");
      if (this.Page.IsPostBack)
        return;
      this.ChkAdminLevel("user_recharge_log", DTEnums.ActionEnum.View.ToString());
      if (!string.IsNullOrEmpty(this.username))
        this.txtUserName.Text = this.username;
      this.TreeBind("type=1");
      this.txtRechargeNo.Text = Utils.GetOrderNumber();
    }

    private void TreeBind(string strWhere)
    {
      DataTable table = new Rain.BLL.payment().GetList(0, strWhere, "sort_id asc,id asc").Tables[0];
      this.ddlPaymentId.Items.Clear();
      this.ddlPaymentId.Items.Add(new ListItem("请选择支付方式", ""));
      foreach (DataRow row in (InternalDataCollectionBase) table.Rows)
        this.ddlPaymentId.Items.Add(new ListItem(row["title"].ToString(), row["id"].ToString()));
    }

    private bool DoAdd()
    {
      Rain.Model.users model1 = new Rain.BLL.users().GetModel(this.txtUserName.Text.Trim());
      if (model1 == null)
        return false;
      bool flag = false;
      Rain.Model.user_recharge model2 = new Rain.Model.user_recharge();
      Rain.BLL.user_recharge userRecharge = new Rain.BLL.user_recharge();
      model2.user_id = model1.id;
      model2.user_name = model1.user_name;
      model2.recharge_no = "R" + this.txtRechargeNo.Text.Trim();
      model2.payment_id = Utils.StrToInt(this.ddlPaymentId.SelectedValue, 0);
      model2.amount = Utils.StrToDecimal(this.txtAmount.Text.Trim(), new Decimal(0));
      model2.status = 1;
      model2.add_time = DateTime.Now;
      model2.complete_time = new DateTime?(DateTime.Now);
      if (userRecharge.Recharge(model2))
      {
        this.AddAdminLog(DTEnums.ActionEnum.Add.ToString(), "给会员：" + model2.user_name + "，充值:" + (object) model2.amount + "元");
        flag = true;
      }
      return flag;
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
      this.ChkAdminLevel("user_recharge_log", DTEnums.ActionEnum.Add.ToString());
      if (!this.DoAdd())
        this.JscriptMsg("保存过程中发生错误！", "");
      else
        this.JscriptMsg("会员充值成功！", "recharge_list.aspx");
    }
  }
}
