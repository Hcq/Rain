// Decompiled with JetBrains decompiler
// Type: Rain.Web.admin.users.user_list
// Assembly: Rain.Web, Version=4.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 7C74D3EC-D223-4A13-9304-5BE7CB5803E5
// Assembly location: C:\Users\Felix.Ho\Desktop\page\bin\Rain.Web.dll

using System;
using System.Data;
using System.Text;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Rain.BLL;
using Rain.Common;
using Rain.Web.UI;

namespace Rain.Web.admin.users
{
  public class user_list : ManagePage
  {
    protected string keywords = string.Empty;
    protected HtmlForm form1;
    protected LinkButton btnDelete;
    protected DropDownList ddlGroupId;
    protected TextBox txtKeywords;
    protected LinkButton lbtnSearch;
    protected Repeater rptList;
    protected TextBox txtPageNum;
    protected HtmlGenericControl PageContent;
    protected int totalCount;
    protected int page;
    protected int pageSize;
    protected int group_id;

    protected void Page_Load(object sender, EventArgs e)
    {
      this.ChkAdminLevel(nameof (user_list), DTEnums.ActionEnum.View.ToString());
      this.group_id = DTRequest.GetQueryInt("group_id");
      this.keywords = DTRequest.GetQueryString("keywords");
      this.pageSize = this.GetPageSize(10);
      if (this.Page.IsPostBack)
        return;
      this.TreeBind("is_lock=0");
      this.RptBind("id>0" + this.CombSqlTxt(this.group_id, this.keywords), "reg_time desc,id desc");
    }

    private void TreeBind(string strWhere)
    {
      DataTable table = new user_groups().GetList(0, strWhere, "id desc").Tables[0];
      this.ddlGroupId.Items.Clear();
      this.ddlGroupId.Items.Add(new ListItem("所有会员组", ""));
      foreach (DataRow row in (InternalDataCollectionBase) table.Rows)
        this.ddlGroupId.Items.Add(new ListItem(row["title"].ToString(), row["id"].ToString()));
    }

    private void RptBind(string _strWhere, string _orderby)
    {
      this.page = DTRequest.GetQueryInt("page", 1);
      if (this.group_id > 0)
        this.ddlGroupId.SelectedValue = this.group_id.ToString();
      this.txtKeywords.Text = this.keywords;
      this.rptList.DataSource = (object) new Rain.BLL.users().GetList(this.pageSize, this.page, _strWhere, _orderby, out this.totalCount);
      this.rptList.DataBind();
      this.txtPageNum.Text = this.pageSize.ToString();
      this.PageContent.InnerHtml = Utils.OutPageList(this.pageSize, this.page, this.totalCount, Utils.CombUrlTxt("user_list.aspx", "group_id={0}&keywords={1}&page={2}", this.group_id.ToString(), this.keywords, "__id__"), 8);
    }

    protected string CombSqlTxt(int _group_id, string _keywords)
    {
      StringBuilder stringBuilder = new StringBuilder();
      if (_group_id > 0)
        stringBuilder.Append(" and group_id=" + (object) _group_id);
      _keywords = _keywords.Replace("'", "");
      if (!string.IsNullOrEmpty(_keywords))
        stringBuilder.Append(" and (user_name like '%" + _keywords + "%' or mobile like '%" + _keywords + "%' or email like '%" + _keywords + "%' or nick_name like '%" + _keywords + "%')");
      return stringBuilder.ToString();
    }

    private int GetPageSize(int _default_size)
    {
      int result;
      if (int.TryParse(Utils.GetCookie("user_list_page_size", "UScmsPage"), out result) && result > 0)
        return result;
      return _default_size;
    }

    protected string GetUserStatus(int status)
    {
      string str = string.Empty;
      switch (status)
      {
        case 0:
          str = "正常";
          break;
        case 1:
          str = "待验证";
          break;
        case 2:
          str = "待审核";
          break;
        case 3:
          str = "已禁用";
          break;
      }
      return str;
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
      this.Response.Redirect(Utils.CombUrlTxt("user_list.aspx", "group_id={0}&keywords={1}", this.group_id.ToString(), this.txtKeywords.Text));
    }

    protected void ddlGroupId_SelectedIndexChanged(object sender, EventArgs e)
    {
      this.Response.Redirect(Utils.CombUrlTxt("user_list.aspx", "group_id={0}&keywords={1}", this.ddlGroupId.SelectedValue, this.keywords));
    }

    protected void txtPageNum_TextChanged(object sender, EventArgs e)
    {
      int result;
      if (int.TryParse(this.txtPageNum.Text.Trim(), out result) && result > 0)
        Utils.WriteCookie("user_list_page_size", "UScmsPage", result.ToString(), 14400);
      this.Response.Redirect(Utils.CombUrlTxt("user_list.aspx", "group_id={0}&keywords={1}", this.group_id.ToString(), this.keywords));
    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {
      this.ChkAdminLevel(nameof (user_list), DTEnums.ActionEnum.Delete.ToString());
      int num1 = 0;
      int num2 = 0;
      Rain.BLL.users users = new Rain.BLL.users();
      for (int index = 0; index < this.rptList.Items.Count; ++index)
      {
        int int32 = Convert.ToInt32(((HiddenField) this.rptList.Items[index].FindControl("hidId")).Value);
        if (((CheckBox) this.rptList.Items[index].FindControl("chkId")).Checked)
        {
          if (users.Delete(int32))
            ++num1;
          else
            ++num2;
        }
      }
      this.AddAdminLog(DTEnums.ActionEnum.Delete.ToString(), "删除用户" + (object) num1 + "条，失败" + (object) num2 + "条");
      this.JscriptMsg("删除成功" + (object) num1 + "条，失败" + (object) num2 + "条！", Utils.CombUrlTxt("user_list.aspx", "group_id={0}&keywords={1}", this.group_id.ToString(), this.keywords));
    }
  }
}
