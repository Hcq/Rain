// Decompiled with JetBrains decompiler
// Type: Rain.Web.admin.article.comment_list
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

namespace Rain.Web.admin.article
{
    public class comment_list : ManagePage
    {
        protected string channel_name = string.Empty;
        protected string property = string.Empty;
        protected string keywords = string.Empty;
        protected int channel_id;
        protected int totalCount;
        protected int page;
        protected int pageSize;
        protected HtmlForm form1;
        protected LinkButton btnAudit;
        protected LinkButton btnDelete;
        protected DropDownList ddlProperty;
        protected TextBox txtKeywords;
        protected LinkButton lbtnSearch;
        protected Repeater rptList;
        protected TextBox txtPageNum;
        protected HtmlGenericControl PageContent;

        protected void Page_Load(object sender, EventArgs e)
        {
            this.channel_id = DTRequest.GetQueryInt("channel_id");
            this.channel_name = new Rain.BLL.channel().GetChannelName(this.channel_id);
            this.property = DTRequest.GetQueryString("property");
            this.keywords = DTRequest.GetQueryString("keywords");
            if (this.channel_id == 0)
            {
                this.JscriptMsg("频道参数不正确！", "back");
            }
            else
            {
                this.pageSize = this.GetPageSize(10);
                if (this.Page.IsPostBack)
                    return;
                this.ChkAdminLevel("channel_" + this.channel_name + "_comment", DTEnums.ActionEnum.View.ToString());
                this.RptBind("channel_id=" + (object)this.channel_id + this.CombSqlTxt(this.keywords, this.property), "add_time desc");
            }
        }

        private void RptBind(string _strWhere, string _orderby)
        {
            this.page = DTRequest.GetQueryInt("page", 1);
            this.ddlProperty.SelectedValue = this.property;
            this.txtKeywords.Text = this.keywords;
            this.rptList.DataSource = (object)new article_comment().GetList(this.pageSize, this.page, _strWhere, _orderby, out this.totalCount);
            this.rptList.DataBind();
            this.txtPageNum.Text = this.pageSize.ToString();
            this.PageContent.InnerHtml = Utils.OutPageList(this.pageSize, this.page, this.totalCount, Utils.CombUrlTxt("comment_list.aspx", "channel_id={0}&keywords={1}&property={2}&page={3}", this.channel_id.ToString(), this.keywords, this.property, "__id__"), 8);
        }

        protected string CombSqlTxt(string _keywords, string _property)
        {
            StringBuilder stringBuilder = new StringBuilder();
            _keywords = _keywords.Replace("'", "");
            if (!string.IsNullOrEmpty(_keywords))
                stringBuilder.Append(" and (user_name like '%" + _keywords + "%' or content like '%" + _keywords + "%')");
            if (!string.IsNullOrEmpty(_property))
            {
                switch (_property)
                {
                    case "isLock":
                        stringBuilder.Append(" and is_lock=1");
                        break;
                    case "unLock":
                        stringBuilder.Append(" and is_lock=0");
                        break;
                }
            }
            return stringBuilder.ToString();
        }

        private int GetPageSize(int _default_size)
        {
            int result;
            if (int.TryParse(Utils.GetCookie("article_comment_page_size", "UScmsPage"), out result) && result > 0)
                return result;
            return _default_size;
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            this.Response.Redirect(Utils.CombUrlTxt("comment_list.aspx", "channel_id={0}&keywords={1}&property={2}", this.channel_id.ToString(), this.txtKeywords.Text, this.property));
        }

        protected void ddlProperty_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.Response.Redirect(Utils.CombUrlTxt("comment_list.aspx", "channel_id={0}&keywords={1}&property={2}", this.channel_id.ToString(), this.keywords, this.ddlProperty.SelectedValue));
        }

        protected void txtPageNum_TextChanged(object sender, EventArgs e)
        {
            int result;
            if (int.TryParse(this.txtPageNum.Text.Trim(), out result) && result > 0)
                Utils.WriteCookie("article_comment_page_size", "UScmsPage", result.ToString(), 14400);
            this.Response.Redirect(Utils.CombUrlTxt("comment_list.aspx", "channel_id={0}&keywords={1}&property={2}", this.channel_id.ToString(), this.keywords, this.property));
        }

        protected void btnAudit_Click(object sender, EventArgs e)
        {
            this.ChkAdminLevel("channel_" + this.channel_name + "_comment", DTEnums.ActionEnum.Audit.ToString());
            article_comment articleComment = new article_comment();
            for (int index = 0; index < this.rptList.Items.Count; ++index)
            {
                int int32 = Convert.ToInt32(((HiddenField)this.rptList.Items[index].FindControl("hidId")).Value);
                if (((CheckBox)this.rptList.Items[index].FindControl("chkId")).Checked)
                    articleComment.UpdateField(int32, "is_lock=0");
            }
            this.AddAdminLog(DTEnums.ActionEnum.Audit.ToString(), "审核" + this.channel_name + "频道评论信息");
            this.JscriptMsg("审核通过成功！", Utils.CombUrlTxt("comment_list.aspx", "channel_id={0}&keywords={1}&property={2}", this.channel_id.ToString(), this.keywords, this.property));
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            this.ChkAdminLevel("channel_" + this.channel_name + "_comment", DTEnums.ActionEnum.Delete.ToString());
            int num1 = 0;
            int num2 = 0;
            article_comment articleComment = new article_comment();
            for (int index = 0; index < this.rptList.Items.Count; ++index)
            {
                int int32 = Convert.ToInt32(((HiddenField)this.rptList.Items[index].FindControl("hidId")).Value);
                if (((CheckBox)this.rptList.Items[index].FindControl("chkId")).Checked)
                {
                    if (articleComment.Delete(int32))
                        ++num1;
                    else
                        ++num2;
                }
            }
            this.AddAdminLog(DTEnums.ActionEnum.Delete.ToString(), "删除" + this.channel_name + "频道评论成功" + (object)num1 + "条，失败" + (object)num2 + "条");
            this.JscriptMsg("删除成功" + (object)num1 + "条，失败" + (object)num2 + "条！", Utils.CombUrlTxt("comment_list.aspx", "channel_id={0}&keywords={1}&property={2}", this.channel_id.ToString(), this.keywords, this.property));
        }
    }
}
