// Decompiled with JetBrains decompiler
// Type: Rain.Web.admin.settings.nav_list
// Assembly: Rain.Web, Version=4.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 7C74D3EC-D223-4A13-9304-5BE7CB5803E5
// Assembly location: C:\Users\Felix.Ho\Desktop\page\bin\Rain.Web.dll

using System;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Rain.BLL;
using Rain.Common;
using Rain.Web.UI;

namespace Rain.Web.admin.settings
{
  public class nav_list : ManagePage
  {
    protected HtmlForm form1;
    protected LinkButton btnSave;
    protected LinkButton btnDelete;
    protected Repeater rptList;

    protected void Page_Load(object sender, EventArgs e)
    {
      if (this.Page.IsPostBack)
        return;
      this.ChkAdminLevel("sys_navigation", DTEnums.ActionEnum.View.ToString());
      this.RptBind();
    }

    private void RptBind()
    {
      this.rptList.DataSource = (object) new navigation().GetList(0, DTEnums.NavigationEnum.System.ToString());
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
      this.ChkAdminLevel("sys_navigation", DTEnums.ActionEnum.Edit.ToString());
      navigation navigation = new navigation();
      for (int index = 0; index < this.rptList.Items.Count; ++index)
      {
        int int32 = Convert.ToInt32(((HiddenField) this.rptList.Items[index].FindControl("hidId")).Value);
        int result;
        if (!int.TryParse(((TextBox) this.rptList.Items[index].FindControl("txtSortId")).Text.Trim(), out result))
          result = 99;
        navigation.UpdateField(int32, "sort_id=" + result.ToString());
      }
      this.AddAdminLog(DTEnums.ActionEnum.Edit.ToString(), "保存导航排序");
      this.JscriptMsg("保存排序成功！", "nav_list.aspx");
    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {
      this.ChkAdminLevel("sys_navigation", DTEnums.ActionEnum.Delete.ToString());
      navigation navigation = new navigation();
      for (int index = 0; index < this.rptList.Items.Count; ++index)
      {
        int int32 = Convert.ToInt32(((HiddenField) this.rptList.Items[index].FindControl("hidId")).Value);
        if (((CheckBox) this.rptList.Items[index].FindControl("chkId")).Checked)
          navigation.Delete(int32);
      }
      this.AddAdminLog(DTEnums.ActionEnum.Delete.ToString(), "删除导航菜单");
      this.JscriptMsg("删除数据成功！", "nav_list.aspx", "parent.loadMenuTree");
    }
  }
}
