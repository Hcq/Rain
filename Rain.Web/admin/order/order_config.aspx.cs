// Decompiled with JetBrains decompiler
// Type: Rain.Web.admin.order.order_config
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
  public class order_config : ManagePage
  {
    protected HtmlForm form1;
    protected CheckBox anonymous;
    protected RadioButtonList taxtype;
    protected TextBox taxamount;
    protected RadioButtonList confirmmsg;
    protected TextBox confirmcallindex;
    protected RadioButtonList expressmsg;
    protected TextBox expresscallindex;
    protected RadioButtonList completemsg;
    protected TextBox completecallindex;
    protected Button btnSubmit;

    protected void Page_Load(object sender, EventArgs e)
    {
      if (this.Page.IsPostBack)
        return;
      this.ChkAdminLevel(nameof (order_config), DTEnums.ActionEnum.View.ToString());
      this.ShowInfo();
    }

    private void ShowInfo()
    {
      Rain.Model.orderconfig orderconfig = new Rain.BLL.orderconfig().loadConfig();
      this.anonymous.Checked = orderconfig.anonymous == 1;
      RadioButtonList taxtype = this.taxtype;
      int num = orderconfig.taxtype;
      string str1 = num.ToString();
      taxtype.SelectedValue = str1;
      this.taxamount.Text = orderconfig.taxamount.ToString();
      RadioButtonList confirmmsg = this.confirmmsg;
      num = orderconfig.confirmmsg;
      string str2 = num.ToString();
      confirmmsg.SelectedValue = str2;
      this.confirmcallindex.Text = orderconfig.confirmcallindex;
      RadioButtonList expressmsg = this.expressmsg;
      num = orderconfig.expressmsg;
      string str3 = num.ToString();
      expressmsg.SelectedValue = str3;
      this.expresscallindex.Text = orderconfig.expresscallindex;
      RadioButtonList completemsg = this.completemsg;
      num = orderconfig.completemsg;
      string str4 = num.ToString();
      completemsg.SelectedValue = str4;
      this.completecallindex.Text = orderconfig.completecallindex;
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
      this.ChkAdminLevel(nameof (order_config), DTEnums.ActionEnum.Edit.ToString());
      Rain.BLL.orderconfig orderconfig = new Rain.BLL.orderconfig();
      Rain.Model.orderconfig model = orderconfig.loadConfig();
      try
      {
        model.anonymous = !this.anonymous.Checked ? 0 : 1;
        model.taxtype = Utils.StrToInt(this.taxtype.SelectedValue, 1);
        model.taxamount = Utils.StrToDecimal(this.taxamount.Text.Trim(), new Decimal(0));
        model.confirmmsg = Utils.StrToInt(this.confirmmsg.SelectedValue, 0);
        model.confirmcallindex = this.confirmcallindex.Text;
        model.expressmsg = Utils.StrToInt(this.expressmsg.SelectedValue, 0);
        model.expresscallindex = this.expresscallindex.Text;
        model.completemsg = Utils.StrToInt(this.completemsg.SelectedValue, 0);
        model.completecallindex = this.completecallindex.Text;
        orderconfig.saveConifg(model);
        this.AddAdminLog(DTEnums.ActionEnum.Edit.ToString(), "修改订单配置信息");
        this.JscriptMsg("修改订单配置成功！", "order_config.aspx");
      }
      catch
      {
        this.JscriptMsg("文件写入失败，请检查是否有权限！", string.Empty);
      }
    }
  }
}
