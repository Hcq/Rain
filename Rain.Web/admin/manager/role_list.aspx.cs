// Decompiled with JetBrains decompiler
// Type: Rain.Web.admin.manager.role_list
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

namespace Rain.Web.admin.manager
{
  public class role_list : ManagePage
  {
    protected string keywords = string.Empty;
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
      this.ChkAdminLevel("manager_role", DTEnums.ActionEnum.View.ToString());
      this.RptBind("role_type>=" + (object) this.GetAdminInfo().role_type + this.CombSqlTxt(this.keywords));
    }

    private void RptBind(string _strWhere)
    {
      this.txtKeywords.Text = this.keywords;
      this.rptList.DataSource = (object) new manager_role().GetList(_strWhere);
      this.rptList.DataBind();
    }

    protected string CombSqlTxt(string _keywords)
    {
      StringBuilder stringBuilder = new StringBuilder();
      _keywords = _keywords.Replace("'", "");
      if (!string.IsNullOrEmpty(_keywords))
        stringBuilder.Append(" and role_name like '%" + _keywords + "%'");
      return stringBuilder.ToString();
    }

    protected string GetTypeName(int role_type)
    {
      return role_type == 1 ? "超级用户" : "系统用户";
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
      this.Response.Redirect(Utils.CombUrlTxt("role_list.aspx", "keywords={0}", this.txtKeywords.Text.Trim()));
    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {
      this.ChkAdminLevel("manager_role", DTEnums.ActionEnum.Delete.ToString());
      int num1 = 0;
      int num2 = 0;
      manager_role managerRole = new manager_role();
      for (int index = 0; index < this.rptList.Items.Count; ++index)
      {
        int int32 = Convert.ToInt32(((HiddenField) this.rptList.Items[index].FindControl("hidId")).Value);
        if (((CheckBox) this.rptList.Items[index].FindControl("chkId")).Checked)
        {
          if (managerRole.Delete(int32))
            ++num1;
          else
            ++num2;
        }
      }
      this.AddAdminLog(DTEnums.ActionEnum.Delete.ToString(), "删除管理角色" + (object) num1 + "条，失败" + (object) num2 + "条");
      this.JscriptMsg("删除成功" + (object) num1 + "条，失败" + (object) num2 + "条！", Utils.CombUrlTxt("role_list.aspx", "keywords={0}", this.keywords));
    }
  }
}
