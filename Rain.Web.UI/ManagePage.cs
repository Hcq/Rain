// Decompiled with JetBrains decompiler
// Type: Rain.Web.UI.ManagePage
// Assembly: Rain.Web.UI, Version=4.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 3E78344A-857F-445F-804E-AF1B978FD5C7
// Assembly location: C:\Users\Felix.Ho\Desktop\page\bin\Rain.Web.UI.dll

using System;
using System.Web.UI;
using Rain.Common;

namespace Rain.Web.UI
{
  public class ManagePage : System.Web.UI.Page
    {
    protected internal Rain.Model.siteconfig siteConfig;

    public ManagePage()
    {
      this.Load += new EventHandler(this.ManagePage_Load);
      this.siteConfig = new Rain.BLL.siteconfig().loadConfig();
    }

    private void ManagePage_Load(object sender, EventArgs e)
    {
      if (this.IsAdminLogin())
        return;
      this.Response.Write("<script>parent.location.href='" + this.siteConfig.webpath + this.siteConfig.webmanagepath + "/login.aspx'</script>");
      this.Response.End();
    }

    public bool IsAdminLogin()
    {
      if (this.Session["dt_session_admin_info"] != null)
        return true;
      string cookie1 = Utils.GetCookie("AdminName", "Rain");
      string cookie2 = Utils.GetCookie("AdminPwd", "Rain");
      if (cookie1 != "" && cookie2 != "")
      {
        Rain.Model.manager model = new Rain.BLL.manager().GetModel(cookie1, cookie2);
        if (model != null)
        {
          this.Session["dt_session_admin_info"] = (object) model;
          return true;
        }
      }
      return false;
    }

    public Rain.Model.manager GetAdminInfo()
    {
      if (this.IsAdminLogin())
      {
        Rain.Model.manager manager = this.Session["dt_session_admin_info"] as Rain.Model.manager;
        if (manager != null)
          return manager;
      }
      return (Rain.Model.manager) null;
    }

    public void ChkAdminLevel(string nav_name, string action_type)
    {
      if (new Rain.BLL.manager_role().Exists(this.GetAdminInfo().role_id, nav_name, action_type))
        return;
      this.Response.Write("<script type=\"text/javascript\">" + "parent.jsdialog(\"错误提示\", \"您没有管理该页面的权限，请勿非法进入！\", \"back\")" + "</script>");
      this.Response.End();
    }

    public bool AddAdminLog(string action_type, string remark)
    {
      if (this.siteConfig.logstatus > 0)
      {
        Rain.Model.manager adminInfo = this.GetAdminInfo();
        if (new Rain.BLL.manager_log().Add(adminInfo.id, adminInfo.user_name, action_type, remark) > 0)
          return true;
      }
      return false;
    }

    protected void JscriptMsg(string msgtitle, string url)
    {
      this.ClientScript.RegisterClientScriptBlock(this.Page.GetType(), "JsPrint", "parent.jsprint(\"" + msgtitle + "\", \"" + url + "\")", true);
    }

    protected void JscriptMsg(string msgtitle, string url, string callback)
    {
      this.ClientScript.RegisterClientScriptBlock(this.Page.GetType(), "JsPrint", "parent.jsprint(\"" + msgtitle + "\", \"" + url + "\", " + callback + ")", true);
    }
  }
}
