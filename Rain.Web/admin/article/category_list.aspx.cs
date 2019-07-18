// Decompiled with JetBrains decompiler
// Type: Rain.Web.admin.article.category_list
// Assembly: Rain.Web, Version=4.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 7C74D3EC-D223-4A13-9304-5BE7CB5803E5
// Assembly location: C:\Users\Felix.Ho\Desktop\page\bin\Rain.Web.dll

using System;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Rain.BLL;
using Rain.Common;
using Rain.Web.UI;

namespace Rain.Web.admin.article
{
  public class category_list : ManagePage
  {
    protected string channel_name = string.Empty;
    protected int channel_id;
    protected HtmlForm form1;
    protected LinkButton btnSave;
    protected LinkButton btnDelete;
    protected Repeater rptList;

    protected void Page_Load(object sender, EventArgs e)
    {
      this.channel_id = DTRequest.GetQueryInt("channel_id");
      this.channel_name = new Rain.BLL.channel().GetChannelName(this.channel_id);
      if (this.channel_id == 0)
      {
        this.JscriptMsg("频道参数不正确！", "back");
      }
      else
      {
        if (this.Page.IsPostBack)
          return;
        this.ChkAdminLevel("channel_" + this.channel_name + "_category", DTEnums.ActionEnum.View.ToString());
        this.RptBind();
      }
    }

    private void RptBind()
    {
      this.rptList.DataSource = (object) new article_category().GetList(0, this.channel_id);
      this.rptList.DataBind();
    }

    protected void rptList_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
      if (e.Item.ItemType != ListItemType.AlternatingItem && e.Item.ItemType != ListItemType.Item)
        return;
      Literal control1 = (Literal) e.Item.FindControl("LitFirst");
      HiddenField control2 = (HiddenField) e.Item.FindControl("hidLayer");
      string format = "<span style=\"display:inline-block;width:{0}px;\"></span>{1}{2}";
      string str1 = "<span class=\"folder-open\"></span>";
      string str2 = "<span class=\"folder-line\"></span>";
      int int32 = Convert.ToInt32(control2.Value);
      control1.Text = int32 != 1 ? string.Format(format, (object) ((int32 - 2) * 24), (object) str2, (object) str1) : str1;
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
      this.ChkAdminLevel("channel_" + this.channel_name + "_category", DTEnums.ActionEnum.Edit.ToString());
      article_category articleCategory = new article_category();
      for (int index = 0; index < this.rptList.Items.Count; ++index)
      {
        int int32 = Convert.ToInt32(((HiddenField) this.rptList.Items[index].FindControl("hidId")).Value);
        int result;
        if (!int.TryParse(((TextBox) this.rptList.Items[index].FindControl("txtSortId")).Text.Trim(), out result))
          result = 99;
        articleCategory.UpdateField(int32, "sort_id=" + result.ToString());
      }
      this.AddAdminLog(DTEnums.ActionEnum.Edit.ToString(), "保存" + this.channel_name + "频道栏目分类排序");
      this.JscriptMsg("保存排序成功！", Utils.CombUrlTxt("category_list.aspx", "channel_id={0}", this.channel_id.ToString()));
    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {
      this.ChkAdminLevel("channel_" + this.channel_name + "_category", DTEnums.ActionEnum.Delete.ToString());
      article_category articleCategory = new article_category();
      for (int index = 0; index < this.rptList.Items.Count; ++index)
      {
        int int32 = Convert.ToInt32(((HiddenField) this.rptList.Items[index].FindControl("hidId")).Value);
        if (((CheckBox) this.rptList.Items[index].FindControl("chkId")).Checked)
          articleCategory.Delete(int32);
      }
      this.AddAdminLog(DTEnums.ActionEnum.Edit.ToString(), "删除" + this.channel_name + "频道栏目分类数据");
      this.JscriptMsg("删除数据成功！", Utils.CombUrlTxt("category_list.aspx", "channel_id={0}", this.channel_id.ToString()));
    }
  }
}
