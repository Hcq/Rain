// Decompiled with JetBrains decompiler
// Type: Rain.Web.admin.settings.url_rewrite_list
// Assembly: Rain.Web, Version=4.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 7C74D3EC-D223-4A13-9304-5BE7CB5803E5
// Assembly location: C:\Users\Felix.Ho\Desktop\page\bin\Rain.Web.dll

using System;
using System.Data;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Rain.BLL;
using Rain.Common;
using Rain.Web.UI;

namespace Rain.Web.admin.settings
{
  public class url_rewrite_list : ManagePage
  {
    protected string channel = string.Empty;
    protected string type = string.Empty;
    protected HtmlForm form1;
    protected LinkButton btnDelete;
    protected DropDownList ddlChannel;
    protected DropDownList ddlPageType;
    protected Repeater rptList;

    protected void Page_Load(object sender, EventArgs e)
    {
      this.channel = DTRequest.GetQueryString("channel");
      this.type = DTRequest.GetQueryString("type");
      if (this.Page.IsPostBack)
        return;
      this.ChkAdminLevel("sys_url_rewrite", DTEnums.ActionEnum.View.ToString());
      this.TreeBind();
      this.RptBind(this.channel, this.type);
    }

    private void TreeBind()
    {
      DataTable table = new Rain.BLL.channel().GetList(0, "", "sort_id asc,id desc").Tables[0];
      this.ddlChannel.Items.Clear();
      this.ddlChannel.Items.Add(new ListItem("所有频道", ""));
      foreach (DataRow row in (InternalDataCollectionBase) table.Rows)
        this.ddlChannel.Items.Add(new ListItem(row["title"].ToString(), row["name"].ToString()));
    }

    private void RptBind(string _channel, string _type)
    {
      if (this.channel != "")
        this.ddlChannel.SelectedValue = this.channel;
      if (this.type != "")
        this.ddlPageType.SelectedValue = this.type;
      this.rptList.DataSource = (object) new url_rewrite().GetList(_channel, _type);
      this.rptList.DataBind();
    }

    protected void ddlChannel_SelectedIndexChanged(object sender, EventArgs e)
    {
      this.Response.Redirect(Utils.CombUrlTxt("url_rewrite_list.aspx", "channel={0}&type={1}", this.ddlChannel.SelectedValue, this.type));
    }

    protected void ddlPageType_SelectedIndexChanged(object sender, EventArgs e)
    {
      this.Response.Redirect(Utils.CombUrlTxt("url_rewrite_list.aspx", "channel={0}&type={1}", this.channel, this.ddlPageType.SelectedValue));
    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {
      this.ChkAdminLevel("sys_url_rewrite", DTEnums.ActionEnum.Delete.ToString());
      url_rewrite urlRewrite = new url_rewrite();
      for (int index = 0; index < this.rptList.Items.Count; ++index)
      {
        string attrValue = ((HiddenField) this.rptList.Items[index].FindControl("hideName")).Value;
        if (((CheckBox) this.rptList.Items[index].FindControl("chkId")).Checked)
          urlRewrite.Remove("name", attrValue);
      }
      this.AddAdminLog(DTEnums.ActionEnum.Delete.ToString(), "删除URL配置信息");
      this.JscriptMsg("URL配置删除成功！", "url_rewrite_list.aspx");
    }
  }
}
