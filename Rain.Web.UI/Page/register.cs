// Decompiled with JetBrains decompiler
// Type: Rain.Web.UI.Page.register
// Assembly: Rain.Web.UI, Version=4.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 3E78344A-857F-445F-804E-AF1B978FD5C7
// Assembly location: C:\Users\Felix.Ho\Desktop\page\bin\Rain.Web.UI.dll

using System.Web;
using Rain.Common;

namespace Rain.Web.UI.Page
{
  public class register : BasePage
  {
    protected string action = string.Empty;
    protected string username = string.Empty;

    protected override void ShowPage()
    {
      this.action = DTRequest.GetQueryString("action");
      this.username = DTRequest.GetQueryString("username");
      this.username = Utils.DropHTML(this.username);
      if (this.action == "" && this.uconfig.regstatus == 0)
      {
        HttpContext.Current.Response.Redirect(this.linkurl(nameof (register), (object) "?action=close"));
      }
      else
      {
        if (!(this.action == "checkmail"))
          return;
        string queryString = DTRequest.GetQueryString("code");
        Rain.BLL.user_code userCode = new Rain.BLL.user_code();
        Rain.Model.user_code model = userCode.GetModel(queryString);
        if (model == null)
        {
          HttpContext.Current.Response.Redirect(this.linkurl(nameof (register), (object) "?action=checkerror"));
        }
        else
        {
          model.status = 1;
          userCode.Update(model);
          new Rain.BLL.users().UpdateField(model.user_id, "status=0");
        }
      }
    }
  }
}
