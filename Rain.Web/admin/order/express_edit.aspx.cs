// Decompiled with JetBrains decompiler
// Type: Rain.Web.admin.order.express_edit
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
  public class express_edit : ManagePage
  {
    private string action = DTEnums.ActionEnum.Add.ToString();
    private int id = 0;
    protected HtmlForm form1;
    protected TextBox txtTitle;
    protected TextBox txtExpressCode;
    protected TextBox txtWebSite;
    protected TextBox txtExpressFee;
    protected CheckBox cbIsLock;
    protected TextBox txtSortId;
    protected TextBox txtRemark;
    protected Button btnSubmit;

    protected void Page_Load(object sender, EventArgs e)
    {
      string queryString = DTRequest.GetQueryString("action");
      if (!string.IsNullOrEmpty(queryString) && queryString == DTEnums.ActionEnum.Edit.ToString())
      {
        this.action = DTEnums.ActionEnum.Edit.ToString();
        this.id = DTRequest.GetQueryInt("id");
        if (this.id == 0)
        {
          this.JscriptMsg("传输参数不正确！", "back");
          return;
        }
        if (!new Rain.BLL.express().Exists(this.id))
        {
          this.JscriptMsg("记录不存在或已被删除！", "back");
          return;
        }
      }
      if (this.Page.IsPostBack)
        return;
      this.ChkAdminLevel("order_express", DTEnums.ActionEnum.View.ToString());
      if (this.action == DTEnums.ActionEnum.Edit.ToString())
        this.ShowInfo(this.id);
    }

    private void ShowInfo(int _id)
    {
      Rain.Model.express model = new Rain.BLL.express().GetModel(_id);
      this.txtTitle.Text = model.title;
      this.txtExpressCode.Text = model.express_code;
      this.txtExpressFee.Text = model.express_fee.ToString();
      this.txtWebSite.Text = model.website;
      this.txtRemark.Text = Utils.ToTxt(model.remark);
      this.cbIsLock.Checked = model.is_lock == 0;
      this.txtSortId.Text = model.sort_id.ToString();
    }

    private bool DoAdd()
    {
      Rain.Model.express model = new Rain.Model.express();
      Rain.BLL.express express = new Rain.BLL.express();
      model.title = this.txtTitle.Text.Trim();
      model.express_code = this.txtExpressCode.Text.Trim();
      model.express_fee = Utils.StrToDecimal(this.txtExpressFee.Text.Trim(), new Decimal(0));
      model.website = this.txtWebSite.Text.Trim();
      model.remark = Utils.ToHtml(this.txtRemark.Text);
      model.is_lock = !this.cbIsLock.Checked ? 1 : 0;
      model.sort_id = Utils.StrToInt(this.txtSortId.Text.Trim(), 99);
      if (express.Add(model) <= 0)
        return false;
      this.AddAdminLog(DTEnums.ActionEnum.Add.ToString(), "添加配送方式:" + model.title);
      return true;
    }

    private bool DoEdit(int _id)
    {
      bool flag = false;
      Rain.BLL.express express = new Rain.BLL.express();
      Rain.Model.express model = express.GetModel(_id);
      model.title = this.txtTitle.Text.Trim();
      model.express_code = this.txtExpressCode.Text.Trim();
      model.express_fee = Utils.StrToDecimal(this.txtExpressFee.Text.Trim(), new Decimal(0));
      model.website = this.txtWebSite.Text.Trim();
      model.remark = Utils.ToHtml(this.txtRemark.Text);
      model.is_lock = !this.cbIsLock.Checked ? 1 : 0;
      model.sort_id = Utils.StrToInt(this.txtSortId.Text.Trim(), 99);
      if (express.Update(model))
      {
        this.AddAdminLog(DTEnums.ActionEnum.Edit.ToString(), "修改配送方式:" + model.title);
        flag = true;
      }
      return flag;
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
      if (this.action == DTEnums.ActionEnum.Edit.ToString())
      {
        this.ChkAdminLevel("order_express", DTEnums.ActionEnum.Edit.ToString());
        if (!this.DoEdit(this.id))
          this.JscriptMsg("保存过程中发生错误！", string.Empty);
        else
          this.JscriptMsg("修改物流配送成功！", "express_list.aspx");
      }
      else
      {
        this.ChkAdminLevel("order_express", DTEnums.ActionEnum.Add.ToString());
        if (!this.DoAdd())
          this.JscriptMsg("保存过程中发生错误！", string.Empty);
        else
          this.JscriptMsg("添加物流配送成功！", "express_list.aspx");
      }
    }
  }
}
