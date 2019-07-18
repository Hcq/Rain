// Decompiled with JetBrains decompiler
// Type: Rain.Web.admin.users.message_list
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
  public class message_list : ManagePage
  {
    protected string keywords = string.Empty;
    protected HtmlForm form1;
    protected LinkButton btnDelete;
    protected DropDownList ddlType;
    protected TextBox txtKeywords;
    protected LinkButton lbtnSearch;
    protected Repeater rptList;
    protected TextBox txtPageNum;
    protected HtmlGenericControl PageContent;
    protected int totalCount;
    protected int page;
    protected int pageSize;
    protected int type_id;

    protected void Page_Load(object sender, EventArgs e)
    {
      this.type_id = DTRequest.GetQueryInt("type_id");
      this.keywords = DTRequest.GetQueryString("keywords");
      this.pageSize = this.GetPageSize(10);
      if (this.Page.IsPostBack)
        return;
      this.ChkAdminLevel("user_message", DTEnums.ActionEnum.View.ToString());
      this.RptBind("id>0" + this.CombSqlTxt(this.type_id, this.keywords), "post_time desc,id desc");
    }

    private void RptBind(string _strWhere, string _orderby)
    {
      this.page = DTRequest.GetQueryInt("page", 1);
      if (this.type_id > 0)
        this.ddlType.SelectedValue = this.type_id.ToString();
      this.txtKeywords.Text = this.keywords;
      this.rptList.DataSource = (object) new user_message().GetList(this.pageSize, this.page, _strWhere, _orderby, out this.totalCount);
      this.rptList.DataBind();
      this.txtPageNum.Text = this.pageSize.ToString();
      this.PageContent.InnerHtml = Utils.OutPageList(this.pageSize, this.page, this.totalCount, Utils.CombUrlTxt("message_list.aspx", "type_id={0}&keywords={1}&page={2}", this.type_id.ToString(), this.keywords, "__id__"), 8);
    }

    protected string CombSqlTxt(int _type_id, string _keywords)
    {
      StringBuilder stringBuilder = new StringBuilder();
      if (_type_id > 0)
        stringBuilder.Append(" and type=" + (object) _type_id);
      _keywords = _keywords.Replace("'", "");
      if (!string.IsNullOrEmpty(_keywords))
        stringBuilder.Append(" and (accept_user_name='" + _keywords + "' or post_user_name like '%" + _keywords + "%' or title like '%" + _keywords + "%')");
      return stringBuilder.ToString();
    }

    private int GetPageSize(int _default_size)
    {
      int result;
      if (int.TryParse(Utils.GetCookie("message_list_page_size", "UScmsPage"), out result) && result > 0)
        return result;
      return _default_size;
    }

    protected string GetMessageType(int _type)
    {
      string str = string.Empty;
      switch (_type)
      {
        case 1:
          str = "系统消息";
          break;
        case 2:
          str = "收件箱";
          break;
        case 3:
          str = "发件箱";
          break;
      }
      return str;
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
      this.Response.Redirect(Utils.CombUrlTxt("message_list.aspx", "type_id={0}&keywords={1}", this.type_id.ToString(), this.txtKeywords.Text));
    }

    protected void ddlType_SelectedIndexChanged(object sender, EventArgs e)
    {
      this.Response.Redirect(Utils.CombUrlTxt("message_list.aspx", "type_id={0}&keywords={1}", this.ddlType.SelectedValue, this.keywords));
    }

    protected void txtPageNum_TextChanged(object sender, EventArgs e)
    {
      int result;
      if (int.TryParse(this.txtPageNum.Text.Trim(), out result) && result > 0)
        Utils.WriteCookie("message_list_page_size", "UScmsPage", result.ToString(), 14400);
      this.Response.Redirect(Utils.CombUrlTxt("message_list.aspx", "type_id={0}&keywords={1}", this.type_id.ToString(), this.keywords));
    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {
      this.ChkAdminLevel("user_message", DTEnums.ActionEnum.Delete.ToString());
      int num1 = 0;
      int num2 = 0;
      user_message userMessage = new user_message();
      for (int index = 0; index < this.rptList.Items.Count; ++index)
      {
        int int32 = Convert.ToInt32(((HiddenField) this.rptList.Items[index].FindControl("hidId")).Value);
        if (((CheckBox) this.rptList.Items[index].FindControl("chkId")).Checked)
        {
          if (userMessage.Delete(int32))
            ++num1;
          else
            ++num2;
        }
      }
      this.AddAdminLog(DTEnums.ActionEnum.Delete.ToString(), "删除站内短消息成功" + (object) num1 + "条，失败" + (object) num2 + "条");
      this.JscriptMsg("删除成功" + (object) num1 + "条，失败" + (object) num2 + "条！", Utils.CombUrlTxt("message_list.aspx", "type_id={0}&keywords={1}", this.type_id.ToString(), this.keywords));
    }
  }
}
