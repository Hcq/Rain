// Decompiled with JetBrains decompiler
// Type: Rain.Web.admin.channel.attribute_field_list
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

namespace Rain.Web.admin.channel
{
  public class attribute_field_list : ManagePage
  {
    protected string control_type = string.Empty;
    protected string keywords = string.Empty;
    protected int totalCount;
    protected int page;
    protected int pageSize;
    protected HtmlForm form1;
    protected LinkButton btnSave;
    protected LinkButton btnDelete;
    protected DropDownList ddlControlType;
    protected TextBox txtKeywords;
    protected LinkButton lbtnSearch;
    protected Repeater rptList;
    protected TextBox txtPageNum;
    protected HtmlGenericControl PageContent;

    protected void Page_Load(object sender, EventArgs e)
    {
      this.control_type = DTRequest.GetQueryString("control_type");
      this.keywords = DTRequest.GetQueryString("keywords");
      this.pageSize = this.GetPageSize(10);
      if (this.Page.IsPostBack)
        return;
      this.ChkAdminLevel("sys_channel_field", DTEnums.ActionEnum.View.ToString());
      this.RptBind("id>0" + this.CombSqlTxt(this.control_type, this.keywords), "is_sys desc,sort_id asc,id desc");
    }

    private void RptBind(string _strWhere, string _orderby)
    {
      this.page = DTRequest.GetQueryInt("page", 1);
      this.ddlControlType.SelectedValue = this.control_type;
      this.txtKeywords.Text = this.keywords;
      this.rptList.DataSource = (object) new article_attribute_field().GetList(this.pageSize, this.page, _strWhere, _orderby, out this.totalCount);
      this.rptList.DataBind();
      this.txtPageNum.Text = this.pageSize.ToString();
      this.PageContent.InnerHtml = Utils.OutPageList(this.pageSize, this.page, this.totalCount, Utils.CombUrlTxt("attribute_field_list.aspx", "control_type={0}&keywords={1}&page={2}", this.control_type, this.keywords, "__id__"), 8);
    }

    protected string CombSqlTxt(string _control_type, string _keywords)
    {
      StringBuilder stringBuilder = new StringBuilder();
      if (!string.IsNullOrEmpty(_control_type))
        stringBuilder.Append(" and control_type='" + _control_type + "'");
      _keywords = _keywords.Replace("'", "");
      if (!string.IsNullOrEmpty(_keywords))
        stringBuilder.Append(" and (name like  '%" + _keywords + "%' or title like '%" + _keywords + "%')");
      return stringBuilder.ToString();
    }

    private int GetPageSize(int _default_size)
    {
      int result;
      if (int.TryParse(Utils.GetCookie("attribute_field_page_size", "UScmsPage"), out result) && result > 0)
        return result;
      return _default_size;
    }

    protected string GetTypeCn(string _control_type)
    {
      string str;
      switch (_control_type)
      {
        case "single-text":
          str = "单行文本";
          break;
        case "multi-text":
          str = "多行文本";
          break;
        case "editor":
          str = "编辑器";
          break;
        case "images":
          str = "图片上传";
          break;
        case "video":
          str = "视频上传";
          break;
        case "number":
          str = "数字";
          break;
        case "checkbox":
          str = "复选框";
          break;
        case "multi-radio":
          str = "多项单选";
          break;
        case "multi-checkbox":
          str = "多项多选";
          break;
        default:
          str = "未知类型";
          break;
      }
      return str;
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
      this.Response.Redirect(Utils.CombUrlTxt("attribute_field_list.aspx", "control_type={0}&keywords={1}", this.control_type, this.txtKeywords.Text));
    }

    protected void ddlControlType_SelectedIndexChanged(object sender, EventArgs e)
    {
      this.Response.Redirect(Utils.CombUrlTxt("attribute_field_list.aspx", "control_type={0}&keywords={1}", this.ddlControlType.SelectedValue, this.keywords));
    }

    protected void txtPageNum_TextChanged(object sender, EventArgs e)
    {
      int result;
      if (int.TryParse(this.txtPageNum.Text.Trim(), out result) && result > 0)
        Utils.WriteCookie("attribute_field_page_size", "UScmsPage", result.ToString(), 43200);
      this.Response.Redirect(Utils.CombUrlTxt("attribute_field_list.aspx", "control_type={0}&keywords={1}", this.control_type, this.keywords));
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
      this.ChkAdminLevel("sys_channel_field", DTEnums.ActionEnum.Edit.ToString());
      article_attribute_field articleAttributeField = new article_attribute_field();
      for (int index = 0; index < this.rptList.Items.Count; ++index)
      {
        int int32 = Convert.ToInt32(((HiddenField) this.rptList.Items[index].FindControl("hidId")).Value);
        int result;
        if (!int.TryParse(((TextBox) this.rptList.Items[index].FindControl("txtSortId")).Text.Trim(), out result))
          result = 99;
        articleAttributeField.UpdateField(int32, "sort_id=" + result.ToString());
      }
      this.AddAdminLog(DTEnums.ActionEnum.Edit.ToString(), "保存扩展字段排序");
      this.JscriptMsg("保存排序成功！", Utils.CombUrlTxt("attribute_field_list.aspx", "control_type={0}&keywords={1}", this.control_type, this.keywords));
    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {
      this.ChkAdminLevel("sys_channel_field", DTEnums.ActionEnum.Delete.ToString());
      int num1 = 0;
      int num2 = 0;
      article_attribute_field articleAttributeField = new article_attribute_field();
      for (int index = 0; index < this.rptList.Items.Count; ++index)
      {
        int int32 = Convert.ToInt32(((HiddenField) this.rptList.Items[index].FindControl("hidId")).Value);
        if (((CheckBox) this.rptList.Items[index].FindControl("chkId")).Checked)
        {
          if (articleAttributeField.Delete(int32))
            ++num1;
          else
            ++num2;
        }
      }
      this.AddAdminLog(DTEnums.ActionEnum.Delete.ToString(), "删除扩展字段成功" + (object) num1 + "条，失败" + (object) num2 + "条");
      this.JscriptMsg("删除成功" + (object) num1 + "条，失败" + (object) num2 + "条！", Utils.CombUrlTxt("attribute_field_list.aspx", "control_type={0}&keywords={1}", this.control_type, this.keywords));
    }
  }
}
