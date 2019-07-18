using System.Xml;

namespace Rain.Common
{
  public class XmlHelper
  {
    public static bool AppendChild(string filePath, string xPath, XmlNode xmlNode)
    {
      try
      {
        XmlDocument xmlDocument = new XmlDocument();
        xmlDocument.Load(filePath);
        xmlDocument.SelectSingleNode(xPath).AppendChild(xmlDocument.ImportNode(xmlNode, true));
        xmlDocument.Save(filePath);
        return true;
      }
      catch
      {
        return false;
      }
    }

    public static bool AppendChild(
      string filePath,
      string xPath,
      string toFilePath,
      string toXPath)
    {
      try
      {
        XmlDocument xmlDocument = new XmlDocument();
        xmlDocument.Load(toFilePath);
        XmlNode xmlNode = xmlDocument.SelectSingleNode(toXPath);
        XmlNodeList xmlNodeList = XmlHelper.ReadNodes(filePath, xPath);
        if (xmlNodeList != null)
        {
          foreach (XmlElement xmlElement in xmlNodeList)
          {
            XmlNode newChild = xmlDocument.ImportNode((XmlNode) xmlElement, true);
            xmlNode.AppendChild(newChild);
          }
          xmlDocument.Save(toFilePath);
        }
        return true;
      }
      catch
      {
        return false;
      }
    }

    public static bool UpdateNodeInnerText(string filePath, string xPath, string value)
    {
      try
      {
        XmlDocument xmlDocument = new XmlDocument();
        xmlDocument.Load(filePath);
        xmlDocument.SelectSingleNode(xPath).InnerText = value;
        xmlDocument.Save(filePath);
      }
      catch
      {
        return false;
      }
      return true;
    }

    public static XmlDocument LoadXmlDoc(string filePath)
    {
      try
      {
        XmlDocument xmlDocument = new XmlDocument();
        xmlDocument.Load(filePath);
        return xmlDocument;
      }
      catch
      {
        return (XmlDocument) null;
      }
    }

    public static XmlNodeList ReadNodes(string filePath, string xPath)
    {
      try
      {
        XmlDocument xmlDocument = new XmlDocument();
        xmlDocument.Load(filePath);
        return xmlDocument.SelectSingleNode(xPath).ChildNodes;
      }
      catch
      {
        return (XmlNodeList) null;
      }
    }
  }
}
