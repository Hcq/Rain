// Decompiled with JetBrains decompiler
// Type: Rain.Web.admin.center
// Assembly: Rain.Web, Version=4.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 7C74D3EC-D223-4A13-9304-5BE7CB5803E5
// Assembly location: C:\Users\Felix.Ho\Desktop\page\bin\Rain.Web.dll

using System;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Rain.Common;
using Rain.Web.UI;

namespace Rain.Web.admin
{
  public class center : ManagePage
  {
    protected HtmlForm form1;
    protected Literal litIP;
    protected Literal litBackIP;
    protected Literal litBackTime;
    protected Literal LitUpgrade;
    protected Literal LitNotice;

    protected void Page_Load(object sender, EventArgs e)
    {
      if (this.Page.IsPostBack)
        return;
      Rain.Model.manager adminInfo = this.GetAdminInfo();
      if (adminInfo != null)
      {
        Rain.BLL.manager_log managerLog = new Rain.BLL.manager_log();
        Rain.Model.manager_log model1 = managerLog.GetModel(adminInfo.user_name, 1, DTEnums.ActionEnum.Login.ToString());
        if (model1 != null)
          this.litIP.Text = model1.user_ip;
        Rain.Model.manager_log model2 = managerLog.GetModel(adminInfo.user_name, 2, DTEnums.ActionEnum.Login.ToString());
        if (model2 != null)
        {
          this.litBackIP.Text = model2.user_ip;
          this.litBackTime.Text = model2.add_time.ToString();
        }
      }
    }
  }
}
