// Decompiled with JetBrains decompiler
// Type: Rain.Web.UI.Page.article_show
// Assembly: Rain.Web.UI, Version=4.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 3E78344A-857F-445F-804E-AF1B978FD5C7
// Assembly location: C:\Users\Felix.Ho\Desktop\page\bin\Rain.Web.UI.dll

using System.Data;
using System.Web;
using Rain.Common;

namespace Rain.Web.UI.Page
{
  public class article_show : BasePage
  {
    protected string page = string.Empty;
    protected Rain.Model.article model = new Rain.Model.article();
    protected int id;

    protected override void ShowPage()
    {
      this.id = DTRequest.GetQueryInt("id");
      this.page = DTRequest.GetQueryString("page");
      Rain.BLL.article article = new Rain.BLL.article();
      if (this.id > 0)
      {
        if (!article.Exists(this.id))
        {
          HttpContext.Current.Response.Redirect(this.linkurl("error", (object) ("?msg=" + Utils.UrlEncode("出错啦，您要浏览的页面不存在或已删除！"))));
          return;
        }
        this.model = article.GetModel(this.id);
      }
      else
      {
        if (string.IsNullOrEmpty(this.page))
          return;
        if (!article.Exists(this.page))
        {
          HttpContext.Current.Response.Redirect(this.linkurl("error", (object) ("?msg=" + Utils.UrlEncode("出错啦，您要浏览的页面不存在或已删除！"))));
          return;
        }
        this.model = article.GetModel(this.page);
      }
      if (this.model.link_url != null)
        this.model.link_url = this.model.link_url.Trim();
      if (string.IsNullOrEmpty(this.model.link_url))
        return;
      HttpContext.Current.Response.Redirect(this.model.link_url);
    }

    protected string get_prevandnext_article(
      string urlkey,
      int type,
      string defaultvalue,
      int callIndex)
    {
      string str1 = type == -1 ? "<" : ">";
      Rain.BLL.article article = new Rain.BLL.article();
      string empty = string.Empty;
      string str2 = " and category_id=" + (object) this.model.category_id;
      string filedOrder = type == -1 ? "id desc" : "id asc";
      DataSet list = article.GetList(1, "channel_id=" + (object) this.model.channel_id + " " + str2 + " and status=0 and Id" + str1 + (object) this.id, filedOrder);
      if (list == null || list.Tables[0].Rows.Count <= 0)
        return defaultvalue;
      if (callIndex == 1 && !string.IsNullOrEmpty(list.Tables[0].Rows[0]["call_index"].ToString()))
        return "<a href=\"" + this.linkurl(urlkey, (object) list.Tables[0].Rows[0]["call_index"].ToString()) + "\">" + list.Tables[0].Rows[0]["title"] + "</a>";
      return "<a href=\"" + this.linkurl(urlkey, (object) list.Tables[0].Rows[0]["id"].ToString()) + "\">" + list.Tables[0].Rows[0]["title"] + "</a>";
    }
  }
}
