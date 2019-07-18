// Decompiled with JetBrains decompiler
// Type: Rain.Web.UI.UserPage
// Assembly: Rain.Web.UI, Version=4.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 3E78344A-857F-445F-804E-AF1B978FD5C7
// Assembly location: C:\Users\Felix.Ho\Desktop\page\bin\Rain.Web.UI.dll

using System;
using System.Web;

namespace Rain.Web.UI
{
  public class UserPage : BasePage
  {
    protected Rain.Model.users userModel;
    protected Rain.Model.user_groups groupModel;

    protected override void ShowPage()
    {
      this.Init += new EventHandler(this.UserPage_Init);
    }

    private void UserPage_Init(object sender, EventArgs e)
    {
      if (!this.IsUserLogin())
      {
        HttpContext.Current.Response.Redirect(this.linkurl("login"));
      }
      else
      {
        this.userModel = this.GetUserInfo();
        this.groupModel = new Rain.BLL.user_groups().GetModel(this.userModel.group_id);
        if (this.groupModel == null)
          this.groupModel = new Rain.Model.user_groups();
        this.InitPage();
      }
    }

    protected virtual void InitPage()
    {
    }
  }
}
