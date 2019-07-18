// Decompiled with JetBrains decompiler
// Type: Rain.Web.UI.Page.repassword
// Assembly: Rain.Web.UI, Version=4.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 3E78344A-857F-445F-804E-AF1B978FD5C7
// Assembly location: C:\Users\Felix.Ho\Desktop\page\bin\Rain.Web.UI.dll

using System.Web;
using Rain.Common;

namespace Rain.Web.UI.Page
{
  public class repassword : BasePage
  {
    protected string username = string.Empty;
    protected string code = string.Empty;
    protected string action;

    protected override void ShowPage()
    {
      this.action = DTRequest.GetQueryString("action");
      if (this.action == "mobile")
      {
        this.username = DTRequest.GetQueryString("username");
      }
      else
      {
        if (!(this.action == "email"))
          return;
        this.code = DTRequest.GetQueryString("code");
        Rain.Model.user_code model = new Rain.BLL.user_code().GetModel(this.code);
        if (model == null)
          HttpContext.Current.Response.Redirect(this.linkurl(nameof (repassword), (object) "?action=error"));
        else
          this.username = model.user_name;
      }
    }
  }
}
