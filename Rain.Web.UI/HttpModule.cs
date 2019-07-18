// Decompiled with JetBrains decompiler
// Type: Rain.Web.UI.HttpModule
// Assembly: Rain.Web.UI, Version=4.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 3E78344A-857F-445F-804E-AF1B978FD5C7
// Assembly location: C:\Users\Felix.Ho\Desktop\page\bin\Rain.Web.UI.dll

using System;
using System.Collections;
using System.IO;
using System.Text.RegularExpressions;
using System.Web;
using Rain.Common;
using Rain.Model;

namespace Rain.Web.UI
{
  public class HttpModule : IHttpModule
  {
    public void Init(HttpApplication context)
    {
      context.BeginRequest += new EventHandler(this.ReUrl_BeginRequest);
    }

    public void Dispose()
    {
    }

    private void ReUrl_BeginRequest(object sender, EventArgs e)
    {
      HttpContext context = ((HttpApplication) sender).Context;
      Rain.Model.siteconfig siteconfig = new Rain.BLL.siteconfig().loadConfig();
      string lower1 = context.Request.Path.ToLower();
      if (this.IsDirExist("dt_cache_site_directory", siteconfig.webpath, siteconfig.webpath, lower1))
        return;
      string lower2 = context.Request.Url.Authority.ToLower();
      string sitePath = this.GetSitePath(siteconfig.webpath, lower1, lower2);
      string str = this.CutStringPath(siteconfig.webpath, sitePath, lower1);
      if (siteconfig.staticstatus == 0)
      {
        foreach (Rain.Model.url_rewrite url in SiteUrls.GetUrls().Urls)
        {
          if (url.page == lower1.Substring(lower1.LastIndexOf("/") + 1))
          {
            if (url.type == "plugin")
            {
              context.RewritePath(string.Format("{0}{1}/{2}{3}", (object) siteconfig.webpath, (object) "aspx", (object) "plugin", (object) str));
              break;
            }
            context.RewritePath(string.Format("{0}{1}/{2}{3}", (object) siteconfig.webpath, (object) "aspx", (object) sitePath, (object) str));
            break;
          }
        }
      }
      else
      {
        foreach (Rain.Model.url_rewrite url in SiteUrls.GetUrls().Urls)
        {
          if (url.url_rewrite_items.Count == 0 && Utils.GetUrlExtension(url.page, siteconfig.staticextension) == lower1.Substring(lower1.LastIndexOf("/") + 1))
          {
            if (url.type == "plugin")
            {
              context.RewritePath(string.Format("{0}{1}/{2}/{3}", (object) siteconfig.webpath, (object) "aspx", (object) "plugin", (object) url.page));
              break;
            }
            context.RewritePath(string.Format("{0}{1}/{2}/{3}", (object) siteconfig.webpath, (object) "aspx", (object) sitePath, (object) url.page));
            break;
          }
          foreach (url_rewrite_item urlRewriteItem in url.url_rewrite_items)
          {
            string urlExtension = Utils.GetUrlExtension(urlRewriteItem.pattern, siteconfig.staticextension);
            if (Regex.IsMatch(str, string.Format("^/{0}$", (object) urlExtension), RegexOptions.IgnoreCase) || url.page == "index.aspx" && Regex.IsMatch(str, string.Format("^/{0}$", (object) urlRewriteItem.pattern), RegexOptions.IgnoreCase))
            {
              if (siteconfig.staticstatus == 2 && (url.channel.Length > 0 || url.page.ToLower() == "index.aspx"))
              {
                context.RewritePath(siteconfig.webpath + "html/" + sitePath + Utils.GetUrlExtension(str, siteconfig.staticextension, true));
                return;
              }
              if (url.type == "plugin")
              {
                string queryString = Regex.Replace(str, string.Format("/{0}", (object) urlExtension), urlRewriteItem.querystring, RegexOptions.IgnoreCase);
                context.RewritePath(string.Format("{0}{1}/{2}/{3}", (object) siteconfig.webpath, (object) "aspx", (object) "plugin", (object) url.page), string.Empty, queryString);
                return;
              }
              string queryString1 = Regex.Replace(str, string.Format("/{0}", (object) urlExtension), urlRewriteItem.querystring, RegexOptions.IgnoreCase);
              context.RewritePath(string.Format("{0}{1}/{2}/{3}", (object) siteconfig.webpath, (object) "aspx", (object) sitePath, (object) url.page), string.Empty, queryString1);
              return;
            }
          }
        }
      }
    }

    private string GetFirstPath(string webPath, string requestPath)
    {
      if (requestPath.StartsWith(webPath))
      {
        string str = requestPath.Substring(webPath.Length);
        if (str.IndexOf("/") > 0)
          return str.Substring(0, str.IndexOf("/")).ToLower();
      }
      return string.Empty;
    }

    private string GetCurrDomainPath(string requestDomain)
    {
      if (SiteDomains.GetSiteDomains().Paths.ContainsValue(requestDomain))
        return SiteDomains.GetSiteDomains().Domains[requestDomain];
      return string.Empty;
    }

    private string GetCurrPagePath(string webPath, string requestPath)
    {
      string firstPath = this.GetFirstPath(webPath, requestPath);
      if (firstPath != string.Empty && SiteDomains.GetSiteDomains().Paths.ContainsKey(firstPath))
        return firstPath;
      return string.Empty;
    }

    private string GetSitePath(string webPath, string requestPath, string requestDomain)
    {
      string currDomainPath = this.GetCurrDomainPath(requestDomain);
      if (currDomainPath != string.Empty)
        return currDomainPath;
      string currPagePath = this.GetCurrPagePath(webPath, requestPath);
      if (currPagePath != string.Empty)
        return currPagePath;
      return SiteDomains.GetSiteDomains().DefaultPath;
    }

    private ArrayList GetSiteDirs(string cacheKey, string dirPath)
    {
      ArrayList arrayList = CacheHelper.Get<ArrayList>(cacheKey);
      if (arrayList == null)
      {
        arrayList = new ArrayList();
        foreach (DirectoryInfo directory in new DirectoryInfo(Utils.GetMapPath(dirPath)).GetDirectories())
          arrayList.Add((object) directory.Name.ToLower());
        CacheHelper.Insert(cacheKey, (object) arrayList, 2);
      }
      return arrayList;
    }

    private bool IsDirExist(string cacheKey, string webPath, string dirPath, string requestPath)
    {
      ArrayList siteDirs = this.GetSiteDirs(cacheKey, dirPath);
      string str1 = string.Empty;
      string empty = string.Empty;
      if (requestPath.StartsWith(webPath))
      {
        string str2 = requestPath.Substring(webPath.Length);
        if (str2.IndexOf("/") > 0)
          str1 = str2.Substring(0, str2.IndexOf("/"));
      }
      return str1.Length > 0 && siteDirs.Contains((object) str1.ToLower());
    }

    private string CutStringPath(string webPath, string sitePath, string requestPath)
    {
      if (requestPath.StartsWith(webPath))
        requestPath = requestPath.Substring(webPath.Length);
      sitePath += "/";
      if (requestPath.StartsWith(sitePath))
        requestPath = requestPath.Substring(sitePath.Length);
      return "/" + requestPath;
    }
  }
}
