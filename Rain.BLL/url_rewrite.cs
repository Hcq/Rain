// Decompiled with JetBrains decompiler
// Type: Rain.BLL.url_rewrite
// Assembly: Rain.BLL, Version=4.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 8924FCAE-37FA-4301-A188-DA020D33C8EB
// Assembly location: C:\Users\Felix.Ho\Desktop\page\bin\Rain.BLL.dll

using System.Collections;
using System.Collections.Generic;
using System.Xml;
using Rain.Common;

namespace Rain.BLL
{
  public class url_rewrite
  {
    private readonly Rain.DAL.url_rewrite dal = new Rain.DAL.url_rewrite();

    public bool Add(Rain.Model.url_rewrite model)
    {
      return this.dal.Add(model);
    }

    public bool Edit(Rain.Model.url_rewrite model)
    {
      return this.dal.Edit(model);
    }

    public bool Remove(string attrName, string attrValue)
    {
      return this.dal.Remove(attrName, attrValue);
    }

    public bool Remove(XmlNodeList xnList)
    {
      return this.dal.Remove(xnList);
    }

    public bool Exists(string name)
    {
      if (string.IsNullOrEmpty(name))
        return false;
      foreach (Rain.Model.url_rewrite urlRewrite in this.GetListAll())
      {
        if (urlRewrite.name == name)
          return true;
      }
      return false;
    }

    public Rain.Model.url_rewrite GetInfo(string attrValue)
    {
      return this.dal.GetInfo(attrValue);
    }

    public Rain.Model.url_rewrite GetInfo(string channel, string attrType)
    {
      foreach (Rain.Model.url_rewrite urlRewrite in this.GetListAll())
      {
        if ((!(channel != "") || !(channel != urlRewrite.channel)) && (!(attrType != "") || !(attrType != urlRewrite.type)))
          return urlRewrite;
      }
      return (Rain.Model.url_rewrite) null;
    }

    public Hashtable GetList()
    {
      Hashtable hashtable = CacheHelper.Get<Hashtable>("dt_cache_site_urls");
      if (hashtable == null)
      {
        CacheHelper.Insert("dt_cache_site_urls", (object) this.dal.GetList(), Utils.GetXmlMapPath("Urlspath"));
        hashtable = CacheHelper.Get<Hashtable>("dt_cache_site_urls");
      }
      return hashtable;
    }

    public List<Rain.Model.url_rewrite> GetListAll()
    {
      List<Rain.Model.url_rewrite> urlRewriteList = CacheHelper.Get<List<Rain.Model.url_rewrite>>("dt_cache_site_urls_list");
      if (urlRewriteList == null)
      {
        CacheHelper.Insert("dt_cache_site_urls_list", (object) this.dal.GetList(""), Utils.GetXmlMapPath("Urlspath"));
        urlRewriteList = CacheHelper.Get<List<Rain.Model.url_rewrite>>("dt_cache_site_urls_list");
      }
      return urlRewriteList;
    }

    public List<Rain.Model.url_rewrite> GetList(string channel)
    {
      List<Rain.Model.url_rewrite> listAll = this.GetListAll();
      if (channel == "")
        return listAll;
      List<Rain.Model.url_rewrite> urlRewriteList = new List<Rain.Model.url_rewrite>();
      foreach (Rain.Model.url_rewrite urlRewrite in listAll)
      {
        if (urlRewrite.channel == channel)
          urlRewriteList.Add(urlRewrite);
      }
      return urlRewriteList;
    }

    public List<Rain.Model.url_rewrite> GetList(string channel, string attrType)
    {
      List<Rain.Model.url_rewrite> urlRewriteList = new List<Rain.Model.url_rewrite>();
      foreach (Rain.Model.url_rewrite urlRewrite in this.GetListAll())
      {
        if ((!(channel != "") || !(channel != urlRewrite.channel)) && (!(attrType != "") || !(attrType != urlRewrite.type)))
          urlRewriteList.Add(urlRewrite);
      }
      return urlRewriteList;
    }
  }
}
