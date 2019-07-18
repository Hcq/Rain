// Decompiled with JetBrains decompiler
// Type: Rain.Web.UI.Page.error
// Assembly: Rain.Web.UI, Version=4.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 3E78344A-857F-445F-804E-AF1B978FD5C7
// Assembly location: C:\Users\Felix.Ho\Desktop\page\bin\Rain.Web.UI.dll

using Rain.Common;

namespace Rain.Web.UI.Page
{
  public class error : System.Web.UI.Page
  {
    protected internal Rain.Model.siteconfig config = new Rain.BLL.siteconfig().loadConfig();
    protected string msg = string.Empty;

    public error()
    {
      this.msg = Utils.ToHtml(DTRequest.GetQueryString(nameof (msg)));
    }
  }
}
