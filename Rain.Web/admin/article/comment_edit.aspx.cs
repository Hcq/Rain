// Decompiled with JetBrains decompiler
// Type: Rain.Web.admin.article.comment_edit
// Assembly: Rain.Web, Version=4.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 7C74D3EC-D223-4A13-9304-5BE7CB5803E5
// Assembly location: C:\Users\Felix.Ho\Desktop\page\bin\Rain.Web.dll

using System;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Rain.Common;
using Rain.Web.UI;

namespace Rain.Web.admin.article
{
  public class comment_edit : ManagePage
  {
    private int id = 0;
    private string channel_name = string.Empty;
    protected Rain.Model.article_comment model = new Rain.Model.article_comment();
    protected HtmlForm form1;
    protected RadioButtonList rblIsLock;
    protected TextBox txtReContent;
    protected Button btnSubmit;

    protected void Page_Load(object sender, EventArgs e)
    {
      this.id = DTRequest.GetQueryInt("id");
      if (this.id == 0)
        this.JscriptMsg("传输参数不正确！", "back");
      else if (!new Rain.BLL.article_comment().Exists(this.id))
      {
        this.JscriptMsg("记录不存在或已删除！", "back");
      }
      else
      {
        this.model = new Rain.BLL.article_comment().GetModel(this.id);
        this.channel_name = new Rain.BLL.channel().GetChannelName(this.model.channel_id);
        if (this.Page.IsPostBack)
          return;
        this.ShowInfo();
      }
    }

    private void ShowInfo()
    {
      this.txtReContent.Text = Utils.ToTxt(this.model.reply_content);
      this.rblIsLock.SelectedValue = this.model.is_lock.ToString();
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
      this.ChkAdminLevel("channel_" + this.channel_name + "_comment", DTEnums.ActionEnum.Reply.ToString());
      Rain.BLL.article_comment articleComment = new Rain.BLL.article_comment();
      this.model.is_reply = 1;
      this.model.reply_content = Utils.ToHtml(this.txtReContent.Text);
      this.model.is_lock = int.Parse(this.rblIsLock.SelectedValue);
      this.model.reply_time = new DateTime?(DateTime.Now);
      articleComment.Update(this.model);
      this.AddAdminLog(DTEnums.ActionEnum.Reply.ToString(), "回复" + this.channel_name + "频道评论ID:" + (object) this.model.id);
      this.JscriptMsg("评论回复成功！", "comment_list.aspx?channel_id=" + (object) this.model.channel_id);
    }
  }
}
