// Decompiled with JetBrains decompiler
// Type: Rain.Web.UI.SiteDomains
// Assembly: Rain.Web.UI, Version=4.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 3E78344A-857F-445F-804E-AF1B978FD5C7
// Assembly location: C:\Users\Felix.Ho\Desktop\page\bin\Rain.Web.UI.dll

using System.Collections.Generic;
using Rain.Common;

namespace Rain.Web.UI
{
  public class SiteDomains
  {
    private static object lockHelper = new object();
    private static volatile SiteDomains instance = (SiteDomains) null;
    private string _default_path = string.Empty;
    private Dictionary<string, string> _paths;
    private Dictionary<string, string> _domains;
    private List<Rain.Model.channel_site> _sitelist;

    public string DefaultPath
    {
      get
      {
        return this._default_path;
      }
      set
      {
        this._default_path = value;
      }
    }

    public Dictionary<string, string> Paths
    {
      get
      {
        return this._paths;
      }
      set
      {
        this._paths = value;
      }
    }

    public Dictionary<string, string> Domains
    {
      get
      {
        return this._domains;
      }
      set
      {
        this._domains = value;
      }
    }

    public List<Rain.Model.channel_site> SiteList
    {
      get
      {
        return this._sitelist;
      }
      set
      {
        this._sitelist = value;
      }
    }

    public SiteDomains()
    {
      this.SiteList = new Rain.BLL.channel_site().GetModelList();
      this.Paths = new Dictionary<string, string>();
      this.Domains = new Dictionary<string, string>();
      if (this.SiteList == null)
        return;
      foreach (Rain.Model.channel_site site in this.SiteList)
      {
        this.Paths.Add(site.build_path, site.domain);
        if (site.domain.Length > 0 && !this.Domains.ContainsKey(site.domain))
          this.Domains.Add(site.domain, site.build_path);
        if (site.is_default == 1 && this.DefaultPath == string.Empty)
          this.DefaultPath = site.build_path;
      }
    }

    public static SiteDomains GetSiteDomains()
    {
      SiteDomains siteDomains = CacheHelper.Get<SiteDomains>("dt_cache_http_domain");
      lock (SiteDomains.lockHelper)
      {
        if (siteDomains == null)
        {
          CacheHelper.Insert("dt_cache_http_domain", (object) new SiteDomains(), 10);
          SiteDomains.instance = CacheHelper.Get<SiteDomains>("dt_cache_http_domain");
        }
      }
      return SiteDomains.instance;
    }
  }
}
