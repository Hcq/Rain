// Decompiled with JetBrains decompiler
// Type: Rain.DAL.plugin
// Assembly: Rain.DAL, Version=4.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 34D3CCEA-896B-46C7-93D3-7BA93E9004E2
// Assembly location: C:\Users\Felix.Ho\Desktop\page\bin\Rain.DAL.dll

using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using Rain.Common;
using Rain.DBUtility;

namespace Rain.DAL
{
  public class plugin
  {
    private string databaseprefix;

    public plugin(string _databaseprefix)
    {
      this.databaseprefix = _databaseprefix;
    }

    public List<Rain.Model.plugin> GetList(string dirPath)
    {
      List<Rain.Model.plugin> pluginList = new List<Rain.Model.plugin>();
      foreach (FileSystemInfo directory in new DirectoryInfo(dirPath).GetDirectories())
      {
        Rain.Model.plugin info = this.GetInfo(directory.FullName + "\\");
        pluginList.Add(new Rain.Model.plugin()
        {
          directory = info.directory,
          name = info.name,
          author = info.author,
          version = info.version,
          description = info.description,
          isload = info.isload
        });
      }
      return pluginList;
    }

    public Rain.Model.plugin GetInfo(string dirPath)
    {
      Rain.Model.plugin plugin = new Rain.Model.plugin();
      if (!File.Exists(dirPath + "plugin.config"))
        return plugin;
      XmlDocument xmlDocument = new XmlDocument();
      xmlDocument.Load(dirPath + "plugin.config");
      try
      {
        foreach (XmlNode childNode in xmlDocument.SelectSingleNode(nameof (plugin)).ChildNodes)
        {
          switch (childNode.Name)
          {
            case "directory":
              plugin.directory = childNode.InnerText;
              break;
            case "name":
              plugin.name = childNode.InnerText;
              break;
            case "author":
              plugin.author = childNode.InnerText;
              break;
            case "version":
              plugin.version = childNode.InnerText;
              break;
            case "description":
              plugin.description = childNode.InnerText;
              break;
            case "isload":
              plugin.isload = int.Parse(childNode.InnerText);
              break;
          }
        }
      }
      catch
      {
        plugin = new Rain.Model.plugin();
      }
      return plugin;
    }

    public void MarkTemplet(
      string sitePath,
      string tempPath,
      string skinName,
      string dirPath,
      string xPath)
    {
      foreach (XmlElement readNode in XmlHelper.ReadNodes(dirPath + "plugin.config", xPath))
      {
        if (readNode.NodeType != XmlNodeType.Comment && readNode.Name.ToLower() == "rewrite" && (readNode.Attributes["page"] != null && !string.IsNullOrEmpty(readNode.Attributes["page"].InnerText) && !string.IsNullOrEmpty(readNode.Attributes["templet"].InnerText) && !string.IsNullOrEmpty(readNode.Attributes["inherit"].InnerText)))
        {
          string pageSize = string.Empty;
          if (readNode.Attributes["pagesize"] != null)
            pageSize = readNode.Attributes["pagesize"].InnerText;
          PageTemplate.GetTemplate(sitePath, tempPath, skinName, readNode.Attributes["templet"].InnerText, readNode.Attributes["page"].InnerText, readNode.Attributes["inherit"].InnerText, nameof (plugin), string.Empty, pageSize, 1);
        }
      }
    }

    public bool UpdateNodeValue(string dirPath, string xPath, string value)
    {
      return XmlHelper.UpdateNodeInnerText(dirPath + "plugin.config", xPath, value);
    }

    public bool ExeSqlStr(string dirPath, string xPath)
    {
      bool flag = true;
      List<string> stringList = this.ReadChildNodesValue(dirPath + "plugin.config", xPath);
      if (stringList != null)
      {
        ArrayList SQLStringList = new ArrayList();
        foreach (string str in stringList)
          SQLStringList.Add((object) str);
        if (!DbHelperOleDb.ExecuteSqlTran(SQLStringList))
          flag = false;
      }
      return flag;
    }

    public bool AppendNodes(string dirPath, string xPath)
    {
      return new url_rewrite().Import(XmlHelper.ReadNodes(dirPath + "plugin.config", xPath));
    }

    public bool RemoveNodes(string dirPath, string xPath)
    {
      return new url_rewrite().Remove(XmlHelper.ReadNodes(dirPath + "plugin.config", xPath));
    }

    public bool AppendMenuNodes(string navPath, string dirPath, string xPath, string parentName)
    {
      XmlNodeList xmlNodeList = XmlHelper.ReadNodes(dirPath + "plugin.config", xPath);
      if (xmlNodeList.Count > 0)
      {
        foreach (XmlElement xmlElement in xmlNodeList)
        {
          if (xmlElement.NodeType != XmlNodeType.Comment && xmlElement.Name.ToLower() == "nav")
          {
            string empty1 = string.Empty;
            string empty2 = string.Empty;
            string link_url = string.Empty;
            string empty3 = string.Empty;
            if (xmlElement.Attributes["name"] != null)
              empty1 = xmlElement.Attributes["name"].Value;
            if (xmlElement.Attributes["title"] != null)
              empty2 = xmlElement.Attributes["title"].Value;
            if (xmlElement.Attributes["url"] != null)
              link_url = navPath + xmlElement.Attributes["url"].Value;
            if (xmlElement.Attributes["action"] != null)
              empty3 = xmlElement.Attributes["action"].Value;
            if (new navigation(this.databaseprefix).Add(parentName, empty1, empty2, link_url, 0, 0, empty3) < 1)
              return false;
            this.AppendMenuNodes(navPath, dirPath, xPath + "/nav", empty1);
          }
        }
      }
      return true;
    }

    public void RemoveMenuNodes(string dirPath, string xPath)
    {
      XmlNodeList xmlNodeList = XmlHelper.ReadNodes(dirPath + "plugin.config", xPath);
      if (xmlNodeList.Count <= 0)
        return;
      navigation navigation = new navigation(this.databaseprefix);
      foreach (XmlElement xmlElement in xmlNodeList)
      {
        if (xmlElement.NodeType != XmlNodeType.Comment && xmlElement.Name.ToLower() == "nav" && xmlElement.Attributes["name"] != null)
        {
          int navId = navigation.GetNavId(xmlElement.Attributes["name"].Value);
          if (navId > 0)
            navigation.Delete(navId);
        }
      }
    }

    private List<string> ReadChildNodesValue(string filePath, string xPath)
    {
      try
      {
        List<string> stringList = new List<string>();
        XmlNodeList xmlNodeList = XmlHelper.ReadNodes(filePath, xPath);
        if (xmlNodeList.Count > 0)
        {
          foreach (XmlElement xmlElement in xmlNodeList)
          {
            if (xmlElement.NodeType != XmlNodeType.Comment && xmlElement.Name.ToLower() == "sql" && !string.IsNullOrEmpty(xmlElement.InnerText))
              stringList.Add(xmlElement.InnerText.Replace("{databaseprefix}", this.databaseprefix));
          }
        }
        return stringList;
      }
      catch
      {
        return (List<string>) null;
      }
    }
  }
}
