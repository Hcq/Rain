// Decompiled with JetBrains decompiler
// Type: Rain.Web.admin.users.user_sms
// Assembly: Rain.Web, Version=4.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 7C74D3EC-D223-4A13-9304-5BE7CB5803E5
// Assembly location: C:\Users\Felix.Ho\Desktop\page\bin\Rain.Web.dll

using System;
using System.Collections;
using System.Data;
using System.Text;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Rain.BLL;
using Rain.Common;
using Rain.Web.UI;

namespace Rain.Web.admin.users
{
  public class user_sms : ManagePage
  {
    private string mobiles = string.Empty;
    protected HtmlForm form1;
    protected RadioButtonList rblSmsType;
    protected HtmlGenericControl div_group;
    protected CheckBoxList cblGroupId;
    protected HtmlGenericControl div_mobiles;
    protected TextBox txtMobileNumbers;
    protected TextBox txtSmsContent;
    protected Button btnSubmit;

    protected void Page_Load(object sender, EventArgs e)
    {
      this.mobiles = DTRequest.GetString("mobiles");
      if (this.Page.IsPostBack)
        return;
      this.ChkAdminLevel(nameof (user_sms), DTEnums.ActionEnum.View.ToString());
      this.ShowInfo(this.mobiles);
      this.TreeBind("is_lock=0");
    }

    private void TreeBind(string strWhere)
    {
      DataTable table = new user_groups().GetList(0, strWhere, "grade asc,id asc").Tables[0];
      this.cblGroupId.Items.Clear();
      foreach (DataRow row in (InternalDataCollectionBase) table.Rows)
        this.cblGroupId.Items.Add(new ListItem(row["title"].ToString(), row["id"].ToString()));
    }

    private void ShowInfo(string _mobiles)
    {
      if (!string.IsNullOrEmpty(_mobiles))
      {
        this.div_mobiles.Visible = true;
        this.div_group.Visible = false;
        this.rblSmsType.SelectedValue = "1";
        this.txtMobileNumbers.Text = _mobiles;
      }
      else
      {
        this.rblSmsType.SelectedValue = "2";
        this.div_mobiles.Visible = false;
        this.div_group.Visible = true;
      }
    }

    private string GetGroupMobile(ArrayList al)
    {
      StringBuilder stringBuilder = new StringBuilder();
      foreach (object obj in al)
      {
        foreach (DataRow row in (InternalDataCollectionBase) new Rain.BLL.users().GetList(0, "group_id=" + (object) Convert.ToInt32(obj), "reg_time desc,id desc").Tables[0].Rows)
        {
          if (!string.IsNullOrEmpty(row["mobile"].ToString()))
            stringBuilder.Append(row["mobile"].ToString() + ",");
        }
      }
      return Utils.DelLastComma(stringBuilder.ToString());
    }

    protected void rblSmsType_SelectedIndexChanged(object sender, EventArgs e)
    {
      if (this.rblSmsType.SelectedValue == "1")
      {
        this.div_group.Visible = false;
        this.div_mobiles.Visible = true;
      }
      else
      {
        this.div_group.Visible = true;
        this.div_mobiles.Visible = false;
      }
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
      this.ChkAdminLevel(nameof (user_sms), DTEnums.ActionEnum.Add.ToString());
      if (this.txtSmsContent.Text.Trim() == "")
        this.JscriptMsg("请输入短信内容！", "");
      else if (this.rblSmsType.SelectedValue == "1")
      {
        if (this.txtMobileNumbers.Text.Trim() == "")
        {
          this.JscriptMsg("请输入手机号码！", "");
        }
        else
        {
          string msg = string.Empty;
          if (new sms_message().Send(this.txtMobileNumbers.Text.Trim(), this.txtSmsContent.Text.Trim(), 2, out msg))
          {
            this.AddAdminLog(DTEnums.ActionEnum.Add.ToString(), "发送手机短信");
            this.JscriptMsg(msg, "user_list.aspx");
          }
          else
            this.JscriptMsg(msg, "");
        }
      }
      else
      {
        ArrayList al = new ArrayList();
        for (int index = 0; index < this.cblGroupId.Items.Count; ++index)
        {
          if (this.cblGroupId.Items[index].Selected)
            al.Add((object) this.cblGroupId.Items[index].Value);
        }
        if (al.Count < 1)
        {
          this.JscriptMsg("请选择会员组别！", "");
        }
        else
        {
          string groupMobile = this.GetGroupMobile(al);
          string msg = string.Empty;
          if (new sms_message().Send(groupMobile, this.txtSmsContent.Text.Trim(), 2, out msg))
          {
            this.AddAdminLog(DTEnums.ActionEnum.Add.ToString(), "发送手机短信");
            this.JscriptMsg(msg, "user_sms.aspx");
          }
          else
            this.JscriptMsg(msg, "");
        }
      }
    }
  }
}
