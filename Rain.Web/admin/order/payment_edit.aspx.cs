// Decompiled with JetBrains decompiler
// Type: Rain.Web.admin.order.payment_edit
// Assembly: Rain.Web, Version=4.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 7C74D3EC-D223-4A13-9304-5BE7CB5803E5
// Assembly location: C:\Users\Felix.Ho\Desktop\page\bin\Rain.Web.dll

using System;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Xml;
using Rain.Common;
using Rain.Web.UI;

namespace Rain.Web.admin.order
{
  public class payment_edit : ManagePage
  {
    private int id = 0;
    protected Rain.Model.payment model = new Rain.Model.payment();
    protected HtmlForm form1;
    protected TextBox txtTitle;
    protected RadioButtonList rblType;
    protected CheckBox cbIsLock;
    protected TextBox txtSortId;
    protected RadioButtonList rblPoundageType;
    protected TextBox txtPoundageAmount;
    protected TextBox txtAlipaySellerEmail;
    protected TextBox txtAlipayPartner;
    protected TextBox txtAlipayKey;
    protected RadioButtonList rblAlipayType;
    protected TextBox txtTenpayBargainorId;
    protected TextBox txtTenpayKey;
    protected RadioButtonList rblTenpayType;
    protected TextBox txtImgUrl;
    protected TextBox txtRemark;
    protected Button btnSubmit;

    protected void Page_Load(object sender, EventArgs e)
    {
      this.id = DTRequest.GetQueryInt("id");
      if (this.id == 0)
        this.JscriptMsg("传输参数不正确！", "back");
      else if (!new Rain.BLL.payment().Exists(this.id))
      {
        this.JscriptMsg("支付方式不存在或已删除！", "back");
      }
      else
      {
        if (this.Page.IsPostBack)
          return;
        this.ChkAdminLevel("order_payment", DTEnums.ActionEnum.View.ToString());
        this.ShowInfo(this.id);
      }
    }

    private void ShowInfo(int _id)
    {
      this.model = new Rain.BLL.payment().GetModel(_id);
      this.txtTitle.Text = this.model.title;
      this.rblType.SelectedValue = this.model.type.ToString();
      this.rblType.Enabled = false;
      this.cbIsLock.Checked = this.model.is_lock == 0;
      this.txtSortId.Text = this.model.sort_id.ToString();
      this.rblPoundageType.SelectedValue = this.model.poundage_type.ToString();
      this.txtPoundageAmount.Text = this.model.poundage_amount.ToString();
      this.txtImgUrl.Text = this.model.img_url;
      this.txtRemark.Text = this.model.remark;
      if (this.model.api_path.ToLower() == "alipaypc")
      {
        XmlDocument xmlDocument = XmlHelper.LoadXmlDoc(Utils.GetMapPath(this.siteConfig.webpath + "xmlconfig/alipaypc.config"));
        this.txtAlipayPartner.Text = xmlDocument.SelectSingleNode("Root/partner").InnerText;
        this.txtAlipayKey.Text = xmlDocument.SelectSingleNode("Root/key").InnerText;
        this.txtAlipaySellerEmail.Text = xmlDocument.SelectSingleNode("Root/email").InnerText;
        this.rblAlipayType.SelectedValue = xmlDocument.SelectSingleNode("Root/type").InnerText;
      }
      else
      {
        if (!(this.model.api_path.ToLower() == "tenpaypc"))
          return;
        XmlDocument xmlDocument = XmlHelper.LoadXmlDoc(Utils.GetMapPath(this.siteConfig.webpath + "xmlconfig/tenpaypc.config"));
        this.txtTenpayBargainorId.Text = xmlDocument.SelectSingleNode("Root/partner").InnerText;
        this.txtTenpayKey.Text = xmlDocument.SelectSingleNode("Root/key").InnerText;
        this.rblTenpayType.SelectedValue = xmlDocument.SelectSingleNode("Root/type").InnerText;
      }
    }

    private bool DoEdit(int _id)
    {
      bool flag = false;
      Rain.BLL.payment payment = new Rain.BLL.payment();
      Rain.Model.payment model = payment.GetModel(_id);
      model.title = this.txtTitle.Text.Trim();
      model.is_lock = !this.cbIsLock.Checked ? 1 : 0;
      model.sort_id = int.Parse(this.txtSortId.Text.Trim());
      model.poundage_type = int.Parse(this.rblPoundageType.SelectedValue);
      model.poundage_amount = Decimal.Parse(this.txtPoundageAmount.Text.Trim());
      model.img_url = this.txtImgUrl.Text.Trim();
      model.remark = this.txtRemark.Text;
      if (model.api_path.ToLower() == "alipaypc")
      {
        string mapPath = Utils.GetMapPath(this.siteConfig.webpath + "xmlconfig/alipaypc.config");
        XmlHelper.UpdateNodeInnerText(mapPath, "Root/partner", this.txtAlipayPartner.Text);
        XmlHelper.UpdateNodeInnerText(mapPath, "Root/key", this.txtAlipayKey.Text);
        XmlHelper.UpdateNodeInnerText(mapPath, "Root/email", this.txtAlipaySellerEmail.Text);
        XmlHelper.UpdateNodeInnerText(mapPath, "Root/type", this.rblAlipayType.SelectedValue);
      }
      else if (model.api_path.ToLower() == "tenpaypc")
      {
        string mapPath = Utils.GetMapPath(this.siteConfig.webpath + "xmlconfig/tenpaypc.config");
        XmlHelper.UpdateNodeInnerText(mapPath, "Root/partner", this.txtTenpayBargainorId.Text);
        XmlHelper.UpdateNodeInnerText(mapPath, "Root/key", this.txtTenpayKey.Text);
        XmlHelper.UpdateNodeInnerText(mapPath, "Root/type", this.rblTenpayType.SelectedValue);
      }
      if (payment.Update(model))
      {
        this.AddAdminLog(DTEnums.ActionEnum.Edit.ToString(), "修改支付方式:" + model.title);
        flag = true;
      }
      return flag;
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
      this.ChkAdminLevel("order_payment", DTEnums.ActionEnum.Edit.ToString());
      if (!this.DoEdit(this.id))
        this.JscriptMsg("保存过程中发生错误！", string.Empty);
      else
        this.JscriptMsg("修改配置成功！", "payment_list.aspx");
    }
  }
}
