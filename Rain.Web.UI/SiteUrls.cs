// Decompiled with JetBrains decompiler
// Type: Rain.Web.UI.SiteUrls
// Assembly: Rain.Web.UI, Version=4.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 3E78344A-857F-445F-804E-AF1B978FD5C7
// Assembly location: C:\Users\Felix.Ho\Desktop\page\bin\Rain.Web.UI.dll

using System.Collections;
using Rain.Common;
using Rain.Model;

namespace Rain.Web.UI
{
  public class SiteUrls
  {
    private static object lockHelper = new object();
    private static volatile SiteUrls instance = (SiteUrls) null;
    private ArrayList _urls;

    public ArrayList Urls
    {
      get
      {
        return this._urls;
      }
      set
      {
        this._urls = value;
      }
    }

    private SiteUrls()
    {
      this.Urls = new ArrayList();
      foreach (Rain.Model.url_rewrite urlRewrite in new Rain.BLL.url_rewrite().GetList(""))
      {
        foreach (url_rewrite_item urlRewriteItem in urlRewrite.url_rewrite_items)
          urlRewriteItem.querystring = urlRewriteItem.querystring.Replace("^", "&");
        this.Urls.Add((object) urlRewrite);
      }
    }

    public static SiteUrls GetUrls()
    {
      SiteUrls siteUrls = CacheHelper.Get<SiteUrls>("dt_cache_http_module");
      lock (SiteUrls.lockHelper)
      {
        if (siteUrls == null)
        {
          CacheHelper.Insert("dt_cache_http_module", (object) new SiteUrls(), Utils.GetXmlMapPath("Urlspath"));
          SiteUrls.instance = CacheHelper.Get<SiteUrls>("dt_cache_http_module");
        }
      }
      return SiteUrls.instance;
    }
  }
}
