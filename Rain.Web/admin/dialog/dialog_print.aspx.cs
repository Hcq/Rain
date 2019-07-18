// Decompiled with JetBrains decompiler
// Type: Rain.Web.admin.dialog.dialog_print
// Assembly: Rain.Web, Version=4.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 7C74D3EC-D223-4A13-9304-5BE7CB5803E5
// Assembly location: C:\Users\Felix.Ho\Desktop\page\bin\Rain.Web.dll

using System;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Rain.Common;
using Rain.Web.UI;

namespace Rain.Web.admin.dialog
{
  public class dialog_print : ManagePage
  {
    private string order_no = string.Empty;
    protected Rain.Model.orders model = new Rain.Model.orders();
    protected Rain.Model.manager adminModel = new Rain.Model.manager();
    protected HtmlForm form1;
    protected Repeater rptList;

    protected void Page_Load(object sender, EventArgs e)
    {
      this.order_no = DTRequest.GetQueryString("order_no");
      if (this.order_no == "")
        this.JscriptMsg("传输参数不正确！", "back");
      else if (!new Rain.BLL.orders().Exists(this.order_no))
      {
        this.JscriptMsg("订单不存在或已被删除！", "back");
      }
      else
      {
        if (this.Page.IsPostBack)
          return;
        this.ShowInfo(this.order_no);
      }
    }

    private void ShowInfo(string _order_no)
    {
      this.model = new Rain.BLL.orders().GetModel(_order_no);
      this.adminModel = this.GetAdminInfo();
      this.rptList.DataSource = (object) this.model.order_goods;
      this.rptList.DataBind();
    }
  }
}
