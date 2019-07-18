// Decompiled with JetBrains decompiler
// Type: Rain.Web.admin.users.group_list
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

namespace Rain.Web.admin.users
{
  public class group_list : ManagePage
  {
    public string keywords = string.Empty;
    protected HtmlForm form1;
    protected LinkButton btnDelete;
    protected TextBox txtKeywords;
    protected LinkButton lbtnSearch;
    protected Repeater rptList;

    protected void Page_Load(object sender, EventArgs e)
    {
      this.keywords = DTRequest.GetQueryString("keywords");
      if (this.Page.IsPostBack)
        return;
      this.ChkAdminLevel("user_group", DTEnums.ActionEnum.View.ToString());
      this.RptBind("id>0" + this.CombSqlTxt(this.keywords));
    }

    private void RptBind(string _strWhere)
    {
      this.txtKeywords.Text = this.keywords;
      this.rptList.DataSource = (object) new user_groups().GetList(0, _strWhere, "grade asc,id asc");
      this.rptList.DataBind();
    }

    protected string CombSqlTxt(string _keywords)
    {
      StringBuilder stringBuilder = new StringBuilder();
      _keywords = _keywords.Replace("'", "");
      if (!string.IsNullOrEmpty(_keywords))
        stringBuilder.Append(" and title like '%" + _keywords + "%'");
      return stringBuilder.ToString();
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
      this.Response.Redirect(Utils.CombUrlTxt("group_list.aspx", "keywords={0}", this.txtKeywords.Text.Trim()));
    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {
      this.ChkAdminLevel("user_group", DTEnums.ActionEnum.Delete.ToString());
      int num1 = 0;
      int num2 = 0;
      user_groups userGroups = new user_groups();
      for (int index = 0; index < this.rptList.Items.Count; ++index)
      {
        int int32 = Convert.ToInt32(((HiddenField) this.rptList.Items[index].FindControl("hidId")).Value);
        if (((CheckBox) this.rptList.Items[index].FindControl("chkId")).Checked)
        {
          if (userGroups.Delete(int32))
            ++num1;
          else
            ++num2;
        }
      }
      this.AddAdminLog(DTEnums.ActionEnum.Delete.ToString(), "删除用户组成功" + (object) num1 + "条，失败" + (object) num2 + "条");
      this.JscriptMsg("删除成功" + (object) num1 + "条，失败" + (object) num2 + "条！", Utils.CombUrlTxt("group_list.aspx", "keywords={0}", this.txtKeywords.Text.Trim()));
    }
  }
}
