// Decompiled with JetBrains decompiler
// Type: Rain.DAL.url_rewrite
// Assembly: Rain.DAL, Version=4.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 34D3CCEA-896B-46C7-93D3-7BA93E9004E2
// Assembly location: C:\Users\Felix.Ho\Desktop\page\bin\Rain.DAL.dll

using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using Rain.Common;
using Rain.Model;

namespace Rain.DAL
{
  public class url_rewrite
  {
    public bool Add(Rain.Model.url_rewrite model)
    {
      try
      {
        string xmlMapPath = Utils.GetXmlMapPath("Urlspath");
        XmlDocument xmlDocument = new XmlDocument();
        xmlDocument.Load(xmlMapPath);
        XmlNode xmlNode1 = xmlDocument.SelectSingleNode("urls");
        XmlElement element1 = xmlDocument.CreateElement("rewrite");
        if (!string.IsNullOrEmpty(model.name))
          element1.SetAttribute("name", model.name);
        if (!string.IsNullOrEmpty(model.type))
          element1.SetAttribute("type", model.type);
        if (!string.IsNullOrEmpty(model.page))
          element1.SetAttribute("page", model.page);
        if (!string.IsNullOrEmpty(model.inherit))
          element1.SetAttribute("inherit", model.inherit);
        if (!string.IsNullOrEmpty(model.templet))
          element1.SetAttribute("templet", model.templet);
        if (!string.IsNullOrEmpty(model.channel))
          element1.SetAttribute("channel", model.channel);
        if (!string.IsNullOrEmpty(model.pagesize))
          element1.SetAttribute("pagesize", model.pagesize);
        XmlNode xmlNode2 = xmlNode1.AppendChild((XmlNode) element1);
        foreach (url_rewrite_item urlRewriteItem in model.url_rewrite_items)
        {
          XmlElement element2 = xmlDocument.CreateElement("item");
          if (!string.IsNullOrEmpty(urlRewriteItem.path))
            element2.SetAttribute("path", urlRewriteItem.path);
          if (!string.IsNullOrEmpty(urlRewriteItem.pattern))
            element2.SetAttribute("pattern", urlRewriteItem.pattern);
          if (!string.IsNullOrEmpty(urlRewriteItem.querystring))
            element2.SetAttribute("querystring", urlRewriteItem.querystring);
          xmlNode2.AppendChild((XmlNode) element2);
        }
        xmlDocument.Save(xmlMapPath);
        return true;
      }
      catch
      {
        return false;
      }
    }

    public bool Edit(Rain.Model.url_rewrite model)
    {
      string xmlMapPath = Utils.GetXmlMapPath("Urlspath");
      XmlDocument xmlDocument = new XmlDocument();
      xmlDocument.Load(xmlMapPath);
      XmlNodeList childNodes1 = xmlDocument.SelectSingleNode("urls").ChildNodes;
      if (childNodes1.Count > 0)
      {
        foreach (XmlElement xmlElement1 in childNodes1)
        {
          if (xmlElement1.Attributes["name"].Value.ToLower() == model.name.ToLower())
          {
            if (!string.IsNullOrEmpty(model.type))
              xmlElement1.SetAttribute("type", model.type);
            else if (xmlElement1.Attributes["type"] != null)
              xmlElement1.Attributes["type"].RemoveAll();
            if (!string.IsNullOrEmpty(model.page))
              xmlElement1.SetAttribute("page", model.page);
            else if (xmlElement1.Attributes["page"] != null)
              xmlElement1.Attributes["page"].RemoveAll();
            if (!string.IsNullOrEmpty(model.inherit))
              xmlElement1.SetAttribute("inherit", model.inherit);
            else if (xmlElement1.Attributes["inherit"] != null)
              xmlElement1.Attributes["inherit"].RemoveAll();
            if (!string.IsNullOrEmpty(model.templet))
              xmlElement1.SetAttribute("templet", model.templet);
            else if (xmlElement1.Attributes["templet"] != null)
              xmlElement1.Attributes["templet"].RemoveAll();
            if (!string.IsNullOrEmpty(model.channel))
              xmlElement1.SetAttribute("channel", model.channel);
            else if (xmlElement1.Attributes["channel"] != null)
              xmlElement1.Attributes["channel"].RemoveAll();
            if (!string.IsNullOrEmpty(model.pagesize))
              xmlElement1.SetAttribute("pagesize", model.pagesize);
            else if (xmlElement1.Attributes["pagesize"] != null)
              xmlElement1.Attributes["pagesize"].RemoveAll();
            XmlNodeList childNodes2 = xmlElement1.ChildNodes;
            foreach (XmlElement xmlElement2 in childNodes2)
            {
              for (int index = childNodes2.Count - 1; index >= 0; --index)
              {
                XmlElement xmlElement3 = (XmlElement) childNodes2.Item(index);
                xmlElement1.RemoveChild((XmlNode) xmlElement3);
              }
            }
            foreach (url_rewrite_item urlRewriteItem in model.url_rewrite_items)
            {
              XmlElement element = xmlDocument.CreateElement("item");
              if (!string.IsNullOrEmpty(urlRewriteItem.path))
                element.SetAttribute("path", urlRewriteItem.path);
              if (!string.IsNullOrEmpty(urlRewriteItem.pattern))
                element.SetAttribute("pattern", urlRewriteItem.pattern);
              if (!string.IsNullOrEmpty(urlRewriteItem.querystring))
                element.SetAttribute("querystring", urlRewriteItem.querystring);
              xmlElement1.AppendChild((XmlNode) element);
            }
            xmlDocument.Save(xmlMapPath);
            return true;
          }
        }
      }
      return false;
    }

    public bool Remove(string attrName, string attrValue)
    {
      string xmlMapPath = Utils.GetXmlMapPath("Urlspath");
      XmlDocument xmlDocument = new XmlDocument();
      xmlDocument.Load(xmlMapPath);
      XmlNode xmlNode = xmlDocument.SelectSingleNode("urls");
      XmlNodeList childNodes = xmlNode.ChildNodes;
      if (childNodes.Count <= 0)
        return false;
      for (int index = childNodes.Count - 1; index >= 0; --index)
      {
        XmlElement xmlElement = (XmlElement) childNodes.Item(index);
        if (xmlElement.Attributes[attrName] != null && xmlElement.Attributes[attrName].Value.ToLower() == attrValue.ToLower())
          xmlNode.RemoveChild((XmlNode) xmlElement);
      }
      xmlDocument.Save(xmlMapPath);
      return true;
    }

    public bool Remove(XmlNodeList xnList)
    {
      try
      {
        string xmlMapPath = Utils.GetXmlMapPath("Urlspath");
        XmlDocument xmlDocument = new XmlDocument();
        xmlDocument.Load(xmlMapPath);
        XmlNode xmlNode = xmlDocument.SelectSingleNode("urls");
        foreach (XmlElement xn in xnList)
        {
          for (int index = xmlNode.ChildNodes.Count - 1; index >= 0; --index)
          {
            XmlElement xmlElement = (XmlElement) xmlNode.ChildNodes.Item(index);
            if (xmlElement.Attributes["name"].Value.ToLower() == xn.Attributes["name"].Value.ToLower())
              xmlNode.RemoveChild((XmlNode) xmlElement);
          }
        }
        xmlDocument.Save(xmlMapPath);
        return true;
      }
      catch
      {
        return false;
      }
    }

    public bool Import(XmlNodeList xnList)
    {
      try
      {
        string xmlMapPath = Utils.GetXmlMapPath("Urlspath");
        XmlDocument xmlDocument = new XmlDocument();
        xmlDocument.Load(xmlMapPath);
        XmlNode xmlNode = xmlDocument.SelectSingleNode("urls");
        foreach (XmlElement xn in xnList)
        {
          if (xn.NodeType != XmlNodeType.Comment && xn.Name.ToLower() == "rewrite" && xn.Attributes["name"] != null)
          {
            XmlNode newChild = xmlDocument.ImportNode((XmlNode) xn, true);
            xmlNode.AppendChild(newChild);
          }
        }
        xmlDocument.Save(xmlMapPath);
        return true;
      }
      catch
      {
        return false;
      }
    }

    public Rain.Model.url_rewrite GetInfo(string attrValue)
    {
      Rain.Model.url_rewrite urlRewrite = new Rain.Model.url_rewrite();
      string xmlMapPath = Utils.GetXmlMapPath("Urlspath");
      XmlDocument xmlDocument = new XmlDocument();
      xmlDocument.Load(xmlMapPath);
      XmlNodeList childNodes = xmlDocument.SelectSingleNode("urls").ChildNodes;
      if (childNodes.Count > 0)
      {
        foreach (XmlElement xmlElement in childNodes)
        {
          if (xmlElement.Attributes["name"].Value.ToLower() == attrValue.ToLower())
          {
            if (xmlElement.Attributes["name"] != null)
              urlRewrite.name = xmlElement.Attributes["name"].Value;
            if (xmlElement.Attributes["type"] != null)
              urlRewrite.type = xmlElement.Attributes["type"].Value;
            if (xmlElement.Attributes["page"] != null)
              urlRewrite.page = xmlElement.Attributes["page"].Value;
            if (xmlElement.Attributes["inherit"] != null)
              urlRewrite.inherit = xmlElement.Attributes["inherit"].Value;
            if (xmlElement.Attributes["templet"] != null)
              urlRewrite.templet = xmlElement.Attributes["templet"].Value;
            if (xmlElement.Attributes["channel"] != null)
              urlRewrite.channel = xmlElement.Attributes["channel"].Value;
            if (xmlElement.Attributes["pagesize"] != null)
              urlRewrite.pagesize = xmlElement.Attributes["pagesize"].Value;
            List<url_rewrite_item> urlRewriteItemList = new List<url_rewrite_item>();
            foreach (XmlElement childNode in xmlElement.ChildNodes)
            {
              if (childNode.NodeType != XmlNodeType.Comment && childNode.Name.ToLower() == "item")
              {
                url_rewrite_item urlRewriteItem = new url_rewrite_item();
                if (childNode.Attributes["path"] != null)
                  urlRewriteItem.path = childNode.Attributes["path"].Value;
                if (childNode.Attributes["pattern"] != null)
                  urlRewriteItem.pattern = childNode.Attributes["pattern"].Value;
                if (childNode.Attributes["querystring"] != null)
                  urlRewriteItem.querystring = childNode.Attributes["querystring"].Value;
                urlRewriteItemList.Add(urlRewriteItem);
              }
            }
            urlRewrite.url_rewrite_items = urlRewriteItemList;
            return urlRewrite;
          }
        }
      }
      return (Rain.Model.url_rewrite) null;
    }

    public Hashtable GetList()
    {
      Hashtable hashtable = new Hashtable();
      foreach (Rain.Model.url_rewrite urlRewrite in this.GetList(""))
      {
        if (!hashtable.Contains((object) urlRewrite.name))
        {
          foreach (url_rewrite_item urlRewriteItem in urlRewrite.url_rewrite_items)
            urlRewriteItem.querystring = urlRewriteItem.querystring.Replace("^", "&");
          hashtable.Add((object) urlRewrite.name, (object) urlRewrite);
        }
      }
      return hashtable;
    }

    public List<Rain.Model.url_rewrite> GetList(string channel)
    {
      List<Rain.Model.url_rewrite> urlRewriteList = new List<Rain.Model.url_rewrite>();
      string xmlMapPath = Utils.GetXmlMapPath("Urlspath");
      XmlDocument xmlDocument = new XmlDocument();
      xmlDocument.Load(xmlMapPath);
      foreach (XmlElement childNode1 in xmlDocument.SelectSingleNode("urls").ChildNodes)
      {
        if (childNode1.NodeType != XmlNodeType.Comment && childNode1.Name.ToLower() == "rewrite" && childNode1.Attributes["name"] != null)
        {
          if (!string.IsNullOrEmpty(channel))
          {
            if (childNode1.Attributes[nameof (channel)] != null && channel.ToLower() == childNode1.Attributes[nameof (channel)].Value.ToLower())
            {
              Rain.Model.url_rewrite urlRewrite = new Rain.Model.url_rewrite();
              if (childNode1.Attributes["name"] != null)
                urlRewrite.name = childNode1.Attributes["name"].Value;
              if (childNode1.Attributes["type"] != null)
                urlRewrite.type = childNode1.Attributes["type"].Value;
              if (childNode1.Attributes["page"] != null)
                urlRewrite.page = childNode1.Attributes["page"].Value;
              if (childNode1.Attributes["inherit"] != null)
                urlRewrite.inherit = childNode1.Attributes["inherit"].Value;
              if (childNode1.Attributes["templet"] != null)
                urlRewrite.templet = childNode1.Attributes["templet"].Value;
              if (childNode1.Attributes[nameof (channel)] != null)
                urlRewrite.channel = childNode1.Attributes[nameof (channel)].Value;
              if (childNode1.Attributes["pagesize"] != null)
                urlRewrite.pagesize = childNode1.Attributes["pagesize"].Value;
              StringBuilder stringBuilder = new StringBuilder();
              List<url_rewrite_item> urlRewriteItemList = new List<url_rewrite_item>();
              foreach (XmlElement childNode2 in childNode1.ChildNodes)
              {
                if (childNode2.NodeType != XmlNodeType.Comment && childNode2.Name.ToLower() == "item")
                {
                  url_rewrite_item urlRewriteItem = new url_rewrite_item();
                  if (childNode2.Attributes["path"] != null)
                    urlRewriteItem.path = childNode2.Attributes["path"].Value;
                  if (childNode2.Attributes["pattern"] != null)
                    urlRewriteItem.pattern = childNode2.Attributes["pattern"].Value;
                  if (childNode2.Attributes["querystring"] != null)
                    urlRewriteItem.querystring = childNode2.Attributes["querystring"].Value;
                  stringBuilder.Append(urlRewriteItem.path + "," + urlRewriteItem.pattern + "," + urlRewriteItem.querystring + "&");
                  urlRewriteItemList.Add(urlRewriteItem);
                }
              }
              urlRewrite.url_rewrite_str = Utils.DelLastChar(stringBuilder.ToString(), "&");
              urlRewrite.url_rewrite_items = urlRewriteItemList;
              urlRewriteList.Add(urlRewrite);
            }
          }
          else
          {
            Rain.Model.url_rewrite urlRewrite = new Rain.Model.url_rewrite();
            if (childNode1.Attributes["name"] != null)
              urlRewrite.name = childNode1.Attributes["name"].Value;
            if (childNode1.Attributes["type"] != null)
              urlRewrite.type = childNode1.Attributes["type"].Value;
            if (childNode1.Attributes["page"] != null)
              urlRewrite.page = childNode1.Attributes["page"].Value;
            if (childNode1.Attributes["inherit"] != null)
              urlRewrite.inherit = childNode1.Attributes["inherit"].Value;
            if (childNode1.Attributes["templet"] != null)
              urlRewrite.templet = childNode1.Attributes["templet"].Value;
            if (childNode1.Attributes[nameof (channel)] != null)
              urlRewrite.channel = childNode1.Attributes[nameof (channel)].Value;
            if (childNode1.Attributes["pagesize"] != null)
              urlRewrite.pagesize = childNode1.Attributes["pagesize"].Value;
            StringBuilder stringBuilder = new StringBuilder();
            List<url_rewrite_item> urlRewriteItemList = new List<url_rewrite_item>();
            foreach (XmlElement childNode2 in childNode1.ChildNodes)
            {
              if (childNode2.NodeType != XmlNodeType.Comment && childNode2.Name.ToLower() == "item")
              {
                url_rewrite_item urlRewriteItem = new url_rewrite_item();
                if (childNode2.Attributes["path"] != null)
                  urlRewriteItem.path = childNode2.Attributes["path"].Value;
                if (childNode2.Attributes["pattern"] != null)
                  urlRewriteItem.pattern = childNode2.Attributes["pattern"].Value;
                if (childNode2.Attributes["querystring"] != null)
                  urlRewriteItem.querystring = childNode2.Attributes["querystring"].Value;
                stringBuilder.Append(urlRewriteItem.path + "," + urlRewriteItem.pattern + "," + urlRewriteItem.querystring + "&");
                urlRewriteItemList.Add(urlRewriteItem);
              }
            }
            urlRewrite.url_rewrite_str = Utils.DelLastChar(stringBuilder.ToString(), "&");
            urlRewrite.url_rewrite_items = urlRewriteItemList;
            urlRewriteList.Add(urlRewrite);
          }
        }
      }
      return urlRewriteList;
    }
  }
}
