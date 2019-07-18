// Decompiled with JetBrains decompiler
// Type: Rain.Web.UI.HtmlBuilder
// Assembly: Rain.Web.UI, Version=4.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 3E78344A-857F-445F-804E-AF1B978FD5C7
// Assembly location: C:\Users\Felix.Ho\Desktop\page\bin\Rain.Web.UI.dll

using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using Rain.Common;
using Rain.Model;

namespace Rain.Web.UI
{
  public class HtmlBuilder
  {
    private Rain.BLL.article_category objarticle_category = new Rain.BLL.article_category();
    private Rain.BLL.channel objchannel = new Rain.BLL.channel();
    private Rain.BLL.channel_site objchannel_site = new Rain.BLL.channel_site();
    private Rain.BLL.article objarticle = new Rain.BLL.article();
    private Rain.Model.siteconfig config = new Rain.BLL.siteconfig().loadConfig();
    private Rain.Model.channel modelchanel = new Rain.Model.channel();
    protected internal Rain.Model.siteconfig siteConfig = new Rain.BLL.siteconfig().loadConfig();
    private const string urlstr = "\"{0}tools/admin_ajax.ashx?action=get_builder_html&lang={1}&html_filename=&indexy=&aspx_filename={2}&catalogue={3}\"";

    public void getpublishsite(HttpContext context)
    {
      string queryString1 = DTRequest.GetQueryString("lang");
      string queryString2 = DTRequest.GetQueryString("name");
      string queryString3 = DTRequest.GetQueryString("type");
      StringBuilder stringBuilder = new StringBuilder();
      Rain.BLL.url_rewrite urlRewrite1 = new Rain.BLL.url_rewrite();
      List<Rain.Model.url_rewrite> urlRewriteList = !string.IsNullOrEmpty(queryString3) ? urlRewrite1.GetList(queryString2, queryString3) : urlRewrite1.GetList(queryString2);
      string empty = string.Empty;
      stringBuilder.Append("[");
      if (queryString3 == "indexlist")
      {
        foreach (Rain.Model.url_rewrite urlRewrite2 in !string.IsNullOrEmpty("list") ? urlRewrite1.GetList(queryString2, "list") : urlRewrite1.GetList(queryString2, "list"))
        {
          if (urlRewrite2.url_rewrite_items.Count > 0 && (urlRewrite2.channel == string.Empty || urlRewrite2.channel == queryString2))
          {
            foreach (url_rewrite_item urlRewriteItem in urlRewrite2.url_rewrite_items)
            {
              if (stringBuilder.ToString().Length > 1)
                stringBuilder.Append(",");
              switch (urlRewrite2.type.ToLower())
              {
                case "list":
                  stringBuilder.Append(this.GetArticleIndexUrlList(queryString1, queryString2, urlRewrite2.page, urlRewriteItem.pattern, urlRewriteItem.path, urlRewriteItem.querystring, Utils.StrToInt(urlRewrite2.pagesize, 0)));
                  break;
              }
            }
          }
        }
      }
      else
      {
        foreach (Rain.Model.url_rewrite urlRewrite2 in urlRewriteList)
        {
          if (urlRewrite2.url_rewrite_items.Count > 0 && (urlRewrite2.channel == string.Empty || urlRewrite2.channel == queryString2))
          {
            foreach (url_rewrite_item urlRewriteItem in urlRewrite2.url_rewrite_items)
            {
              if (urlRewriteItem.querystring == string.Empty)
              {
                string str1 = string.Format("{0}/{1}/{2}", (object) "aspx", (object) queryString1, (object) urlRewrite2.page);
                string str2 = string.Format("{0}/{1}/{2}", (object) "html", (object) queryString1, (object) Utils.GetUrlExtension(urlRewriteItem.pattern, this.config.staticextension));
                if (stringBuilder.ToString().Length > 1)
                  stringBuilder.Append(",");
                stringBuilder.AppendFormat("\"{0}tools/admin_ajax.ashx?action=get_builder_html&lang={1}&html_filename=&indexy=&aspx_filename={2}&catalogue={3}\"", (object) this.config.webpath, (object) queryString1, (object) str1, (object) str2);
              }
              else
              {
                if (stringBuilder.ToString().Length > 1)
                  stringBuilder.Append(",");
                switch (urlRewrite2.type.ToLower())
                {
                  case "list":
                    stringBuilder.Append(this.GetArticleUrlList(queryString1, queryString2, urlRewrite2.page, urlRewriteItem.pattern, urlRewriteItem.path, urlRewriteItem.querystring, Utils.StrToInt(urlRewrite2.pagesize, 0)));
                    break;
                  case "detail":
                    stringBuilder.Append(this.GetDetailUrlList(queryString1, queryString2, urlRewrite2.page, urlRewriteItem.pattern, urlRewriteItem.path, urlRewriteItem.querystring));
                    break;
                  case "category":
                    stringBuilder.Append(this.GetCategoryUrlList(queryString1, queryString2, urlRewrite2.page, urlRewriteItem.pattern, urlRewriteItem.path, urlRewriteItem.querystring, Utils.StrToInt(urlRewrite2.pagesize, 0)));
                    break;
                }
              }
            }
          }
        }
      }
      stringBuilder.Append("]");
      context.Response.Write(stringBuilder.ToString());
    }

    private string GetArticleIndexUrlList(
      string lang,
      string channelname,
      string page,
      string pattern,
      string path,
      string querystring,
      int pagesize)
    {
      StringBuilder stringBuilder = new StringBuilder();
      int num1 = 0;
      if (!string.IsNullOrEmpty(querystring))
        num1 = querystring.Split('&').Length;
      int num2 = this.GetPageSize(this.objarticle.GetCount(channelname, 0, ""), pagesize);
      if (num1 == 1)
        num2 = 1;
      for (int index = 1; index <= num2; ++index)
      {
        string str1 = Regex.Replace(string.Format(path, (object) "0", (object) index), pattern, querystring, RegexOptions.IgnoreCase);
        string str2 = string.Format("{0}/{1}/{2}?{3}", (object) "aspx", (object) lang, (object) page, (object) str1);
        string str3 = string.Format("{0}/{1}/{2}", (object) "html", (object) lang, (object) Utils.GetUrlExtension(string.Format(path, (object) "0", (object) index), this.config.staticextension));
        if (!string.IsNullOrEmpty(stringBuilder.ToString()))
          stringBuilder.Append(",");
        stringBuilder.AppendFormat("\"{0}tools/admin_ajax.ashx?action=get_builder_html&lang={1}&html_filename=&indexy=&aspx_filename={2}&catalogue={3}\"", (object) this.config.webpath, (object) lang, (object) str2.Replace("&", "^"), (object) str3);
      }
      return stringBuilder.ToString();
    }

    private string GetArticleUrlList(
      string lang,
      string channelname,
      string page,
      string pattern,
      string path,
      string querystring,
      int pagesize)
    {
      StringBuilder stringBuilder = new StringBuilder();
      DataTable list = this.objarticle_category.GetList(0, channelname);
      if (list != null && list.Rows.Count > 0)
      {
        for (int index1 = 0; index1 < list.Rows.Count; ++index1)
        {
          int num1 = 0;
          if (!string.IsNullOrEmpty(querystring))
            num1 = querystring.Split('&').Length;
          int num2 = this.GetPageSize(this.objarticle.GetCount(channelname, Convert.ToInt32(list.Rows[index1]["id"].ToString()), ""), pagesize);
          if (num1 == 1)
            num2 = 1;
          for (int index2 = 1; index2 <= num2; ++index2)
          {
            string str1 = Regex.Replace(string.Format(path, (object) list.Rows[index1]["id"].ToString(), (object) index2), pattern, querystring, RegexOptions.IgnoreCase);
            string str2 = string.Format("{0}/{1}/{2}?{3}", (object) "aspx", (object) lang, (object) page, (object) str1);
            string str3 = string.Format("{0}/{1}/{2}", (object) "html", (object) lang, (object) Utils.GetUrlExtension(string.Format(path, (object) list.Rows[index1]["id"].ToString(), (object) index2), this.config.staticextension));
            if (!string.IsNullOrEmpty(stringBuilder.ToString()))
              stringBuilder.Append(",");
            stringBuilder.AppendFormat("\"{0}tools/admin_ajax.ashx?action=get_builder_html&lang={1}&html_filename=&indexy=&aspx_filename={2}&catalogue={3}\"", (object) this.config.webpath, (object) lang, (object) str2.Replace("&", "^"), (object) str3);
          }
        }
      }
      return stringBuilder.ToString();
    }

    private string GetCategoryUrlList(
      string lang,
      string channelname,
      string page,
      string pattern,
      string path,
      string querystring,
      int pagesize)
    {
      StringBuilder stringBuilder = new StringBuilder();
      DataTable list = this.objarticle_category.GetList(0, channelname);
      if (list != null && list.Rows.Count > 0)
      {
        for (int index = 0; index < list.Rows.Count; ++index)
        {
          string str1 = Regex.Replace(string.Format(path, (object) list.Rows[index]["id"].ToString()), pattern, querystring, RegexOptions.IgnoreCase);
          string str2 = string.Format("{0}/{1}/{2}?{3}", (object) "aspx", (object) lang, (object) page, (object) str1);
          string str3 = string.Format("{0}/{1}/{2}", (object) "html", (object) lang, (object) Utils.GetUrlExtension(string.Format(path, (object) list.Rows[index]["id"].ToString()), this.config.staticextension));
          if (!string.IsNullOrEmpty(stringBuilder.ToString()))
            stringBuilder.Append(",");
          stringBuilder.AppendFormat("\"{0}tools/admin_ajax.ashx?action=get_builder_html&lang={1}&html_filename=&indexy=&aspx_filename={2}&catalogue={3}\"", (object) this.config.webpath, (object) lang, (object) str2, (object) str3);
        }
      }
      return stringBuilder.ToString();
    }

    private string GetDetailUrlList(
      string lang,
      string channelname,
      string page,
      string pattern,
      string path,
      string querystring)
    {
      StringBuilder stringBuilder = new StringBuilder();
      DataTable table = this.objarticle.GetList(channelname, 0, "", " id desc").Tables[0];
      if (table != null && table.Rows.Count > 0)
      {
        for (int index = 0; index < table.Rows.Count; ++index)
        {
          string str1 = string.IsNullOrEmpty(table.Rows[index]["call_index"].ToString()) ? table.Rows[index]["id"].ToString() : table.Rows[index]["call_index"].ToString();
          string str2 = Regex.Replace(string.Format(path, (object) str1), pattern, querystring, RegexOptions.IgnoreCase);
          string str3 = string.Format("{0}/{1}/{2}?{3}", (object) "aspx", (object) lang, (object) page, (object) str2);
          string str4 = string.Format("{0}/{1}/{2}", (object) "html", (object) lang, (object) Utils.GetUrlExtension(string.Format(path, (object) str1), this.config.staticextension));
          if (!string.IsNullOrEmpty(stringBuilder.ToString()))
            stringBuilder.Append(",");
          stringBuilder.AppendFormat("\"{0}tools/admin_ajax.ashx?action=get_builder_html&lang={1}&html_filename=&indexy=&aspx_filename={2}&catalogue={3}\"", (object) this.config.webpath, (object) lang, (object) str3, (object) str4);
        }
      }
      return stringBuilder.ToString();
    }

    private int GetPageSize(int totalCount, int pageSize)
    {
      if (totalCount < 1 || pageSize < 1)
        return 1;
      int num1 = totalCount / pageSize;
      if (num1 < 1)
        return 1;
      if (totalCount % pageSize > 0)
      {
        int num2;
        return num2 = num1 + 1;
      }
      if (totalCount % pageSize == 0)
        return num1;
      return num1 <= 1 ? 1 : 1;
    }

    public void handleHtml(HttpContext context)
    {
      this.CreateIndexHtml(DTRequest.GetQueryString("lang"), DTRequest.GetQueryString("aspx_filename"), DTRequest.GetQueryString("catalogue"));
    }

    private void CreateIndexHtml(string lang, string aspx_filename, string catalogue)
    {
      if (System.IO.File.Exists(Utils.GetMapPath(this.config.webpath + aspx_filename.Substring(0, aspx_filename.IndexOf(".aspx") + 5))))
      {
        string str1 = this.config.webpath + aspx_filename.Replace("^", "&");
        string strPath = this.config.webpath + catalogue;
        if (strPath.IndexOf(".") < 0)
          strPath = strPath + "index." + this.config.staticextension;
        string path = HttpContext.Current.Server.MapPath(strPath.Substring(0, strPath.LastIndexOf("/")));
        if (!Directory.Exists(path))
          Directory.CreateDirectory(path);
        string str2 = HttpContext.Current.Request.Url.Authority;
        Rain.Model.channel_site model = this.objchannel_site.GetModel(lang);
        if (model != null && !string.IsNullOrEmpty(model.domain))
          str2 = model.domain;
        string end = new StreamReader(WebRequest.Create("http://" + str2 + str1).GetResponse().GetResponseStream(), Encoding.GetEncoding("utf-8")).ReadToEnd();
        using (StreamWriter streamWriter = new StreamWriter(Utils.GetMapPath(strPath), false, Encoding.UTF8))
        {
          streamWriter.WriteLine(end);
          streamWriter.Flush();
          streamWriter.Close();
        }
      }
      else
        HttpContext.Current.Response.Write("1");
    }
  }
}
