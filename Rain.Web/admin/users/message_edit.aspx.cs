// Decompiled with JetBrains decompiler
// Type: Rain.Web.admin.users.message_edit
// Assembly: Rain.Web, Version=4.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 7C74D3EC-D223-4A13-9304-5BE7CB5803E5
// Assembly location: C:\Users\Felix.Ho\Desktop\page\bin\Rain.Web.dll

using System;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Rain.Common;
using Rain.Web.UI;

namespace Rain.Web.admin.users
{
  public class message_edit : ManagePage
  {
    private string action = DTEnums.ActionEnum.Add.ToString();
    private int id = 0;
    protected HtmlForm form1;
    protected HtmlGenericControl div_view;
    protected Label labType;
    protected Label labPostUserName;
    protected Label labAcceptUserName;
    protected Label labPostTime;
    protected Label labIsRead;
    protected Label labReadTime;
    protected Label labTitle;
    protected Literal litContent;
    protected HtmlGenericControl div_add;
    protected TextBox txtUserName;
    protected TextBox txtTitle;
    protected HtmlTextArea txtContent;
    protected Button btnSubmit;

    protected void Page_Load(object sender, EventArgs e)
    {
      string queryString = DTRequest.GetQueryString("action");
      if (!string.IsNullOrEmpty(queryString) && queryString == DTEnums.ActionEnum.View.ToString())
      {
        this.action = DTEnums.ActionEnum.View.ToString();
        this.id = DTRequest.GetQueryInt("id");
        if (this.id == 0)
        {
          this.JscriptMsg("传输参数不正确！", "back");
          return;
        }
        if (!new Rain.BLL.user_message().Exists(this.id))
        {
          this.JscriptMsg("记录不存在或已被删除！", "back");
          return;
        }
      }
      if (this.Page.IsPostBack)
        return;
      this.ChkAdminLevel("user_message", DTEnums.ActionEnum.View.ToString());
      if (this.action == DTEnums.ActionEnum.View.ToString())
        this.ShowInfo(this.id);
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

    private void ShowInfo(int _id)
    {
      Rain.Model.user_message model = new Rain.BLL.user_message().GetModel(_id);
      this.div_view.Visible = true;
      this.div_add.Visible = false;
      this.btnSubmit.Visible = false;
      this.labType.Text = this.GetMessageType(model.type);
      this.labPostUserName.Text = string.IsNullOrEmpty(model.post_user_name) ? "-" : model.post_user_name;
      this.labAcceptUserName.Text = model.accept_user_name;
      this.labPostTime.Text = model.post_time.ToString();
      this.labIsRead.Text = model.is_read == 1 ? "已阅读" : "未阅读";
      this.labReadTime.Text = !model.read_time.HasValue ? "-" : model.read_time.ToString();
      this.labTitle.Text = model.title;
      this.litContent.Text = model.content;
    }

    private bool DoAdd()
    {
      bool flag = true;
      Rain.Model.user_message model = new Rain.Model.user_message();
      Rain.BLL.user_message userMessage = new Rain.BLL.user_message();
      model.title = this.txtTitle.Text.Trim();
      model.content = this.txtContent.Value;
      string[] strArray = this.txtUserName.Text.Trim().Split(',');
      if (strArray.Length > 0)
      {
        foreach (string user_name in strArray)
        {
          if (new Rain.BLL.users().Exists(user_name))
          {
            model.accept_user_name = user_name;
            if (userMessage.Add(model) < 1)
              flag = false;
          }
        }
      }
      return flag;
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
      this.ChkAdminLevel("user_message", DTEnums.ActionEnum.Add.ToString());
      if (!this.DoAdd())
      {
        this.JscriptMsg("发送过程中发生错误！", "");
      }
      else
      {
        this.AddAdminLog(DTEnums.ActionEnum.Add.ToString(), "发送站内短消息");
        this.JscriptMsg("发送短消息成功", "message_list.aspx");
      }
    }
  }
}
