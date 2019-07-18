// Decompiled with JetBrains decompiler
// Type: Rain.Web.admin.index
// Assembly: Rain.Web, Version=4.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 7C74D3EC-D223-4A13-9304-5BE7CB5803E5
// Assembly location: C:\Users\Felix.Ho\Desktop\page\bin\Rain.Web.dll

using System;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Rain.Common;
using Rain.Model;
using Rain.Web.UI;

namespace Rain.Web.admin
{
  public class index : ManagePage
  {
    protected HtmlForm form1;
    protected LinkButton lbtnExit;
    protected Rain.Model.manager admin_info;

    protected void Page_Load(object sender, EventArgs e)
    {
      if (this.Page.IsPostBack)
        return;
      this.admin_info = this.GetAdminInfo();
    }

    protected void lbtnExit_Click(object sender, EventArgs e)
    {
      this.Session["dt_session_admin_info"] = (object) null;
      Utils.WriteCookie("AdminName", "Rain", -14400);
      Utils.WriteCookie("AdminPwd", "Rain", -14400);
      this.Response.Redirect("login.aspx");
    }
  }
}
