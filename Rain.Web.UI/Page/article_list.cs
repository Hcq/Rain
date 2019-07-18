// Decompiled with JetBrains decompiler
// Type: Rain.Web.UI.Page.article_list
// Assembly: Rain.Web.UI, Version=4.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 3E78344A-857F-445F-804E-AF1B978FD5C7
// Assembly location: C:\Users\Felix.Ho\Desktop\page\bin\Rain.Web.UI.dll

using Rain.Common;

namespace Rain.Web.UI.Page
{
  public class article_list : BasePage
  {
    protected Rain.Model.article_category model = new Rain.Model.article_category();
    protected int page;
    protected int category_id;
    protected int totalcount;
    protected string pagelist;

    protected override void ShowPage()
    {
      this.page = DTRequest.GetQueryInt("page", 1);
      this.category_id = DTRequest.GetQueryInt("category_id");
      Rain.BLL.article_category articleCategory = new Rain.BLL.article_category();
      this.model.title = "所有类别";
      if (this.category_id <= 0 || !articleCategory.Exists(this.category_id))
        return;
      this.model = articleCategory.GetModel(this.category_id);
    }
  }
}
