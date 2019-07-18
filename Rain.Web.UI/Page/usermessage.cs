// Decompiled with JetBrains decompiler
// Type: Rain.Web.UI.Page.usermessage
// Assembly: Rain.Web.UI, Version=4.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 3E78344A-857F-445F-804E-AF1B978FD5C7
// Assembly location: C:\Users\Felix.Ho\Desktop\page\bin\Rain.Web.UI.dll

using Rain.Common;

namespace Rain.Web.UI.Page
{
  public class usermessage : UserPage
  {
    protected string action = string.Empty;
    protected int page;
    protected int totalcount;

    protected override void InitPage()
    {
      this.action = DTRequest.GetQueryString("action");
      this.page = DTRequest.GetQueryInt("page", 1);
    }
  }
}
