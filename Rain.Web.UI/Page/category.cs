// Decompiled with JetBrains decompiler
// Type: Rain.Web.UI.Page.category
// Assembly: Rain.Web.UI, Version=4.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 3E78344A-857F-445F-804E-AF1B978FD5C7
// Assembly location: C:\Users\Felix.Ho\Desktop\page\bin\Rain.Web.UI.dll

using Rain.Common;

namespace Rain.Web.UI.Page
{
  public class category : BasePage
  {
    protected int category_id;

    protected override void ShowPage()
    {
      this.category_id = DTRequest.GetQueryInt("category_id");
    }
  }
}
