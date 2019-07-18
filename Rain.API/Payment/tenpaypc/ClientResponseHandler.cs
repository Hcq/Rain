// Decompiled with JetBrains decompiler
// Type: Rain.API.Payment.tenpaypc.ClientResponseHandler
// Assembly: Rain.API, Version=4.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 114351D3-1A2F-4CC4-A6A6-43C59DB5A56B
// Assembly location: C:\Users\Felix.Ho\Desktop\page\bin\Rain.API.dll

using System.Collections;
using System.Text;
using System.Xml;

namespace Rain.API.Payment.tenpaypc
{
  public class ClientResponseHandler
  {
    private string charset = "gb2312";
    private string key;
    protected Hashtable parameters;
    private string debugInfo;
    protected string content;

    public ClientResponseHandler()
    {
      this.parameters = new Hashtable();
    }

    public string getContent()
    {
      return this.content;
    }

    public virtual void setContent(string content)
    {
      this.content = content;
      XmlDocument xmlDocument = new XmlDocument();
      xmlDocument.LoadXml(content);
      foreach (XmlNode childNode in xmlDocument.SelectSingleNode("root").ChildNodes)
        this.setParameter(childNode.Name, childNode.InnerXml);
    }

    public string getKey()
    {
      return this.key;
    }

    public void setKey(string key)
    {
      this.key = key;
    }

    public string getParameter(string parameter)
    {
      string parameter1 = (string) this.parameters[(object) parameter];
      return parameter1 == null ? "" : parameter1;
    }

    public void setParameter(string parameter, string parameterValue)
    {
      if (parameter == null || !(parameter != ""))
        return;
      if (this.parameters.Contains((object) parameter))
        this.parameters.Remove((object) parameter);
      this.parameters.Add((object) parameter, (object) parameterValue);
    }

    public virtual bool isTenpaySign()
    {
      StringBuilder stringBuilder = new StringBuilder();
      ArrayList arrayList = new ArrayList(this.parameters.Keys);
      arrayList.Sort();
      foreach (string strB in arrayList)
      {
        string parameter = (string) this.parameters[(object) strB];
        if (parameter != null && "".CompareTo(parameter) != 0 && "sign".CompareTo(strB) != 0 && "key".CompareTo(strB) != 0)
          stringBuilder.Append(strB + "=" + parameter + "&");
      }
      stringBuilder.Append("key=" + this.getKey());
      string lower = MD5Util.GetMD5(stringBuilder.ToString(), this.getCharset()).ToLower();
      this.setDebugInfo(stringBuilder.ToString() + " => sign:" + lower);
      return this.getParameter("sign").ToLower().Equals(lower);
    }

    public string getDebugInfo()
    {
      return this.debugInfo;
    }

    protected void setDebugInfo(string debugInfo)
    {
      this.debugInfo = debugInfo;
    }

    protected virtual string getCharset()
    {
      return this.charset;
    }

    public void setCharset(string charset)
    {
      this.charset = charset;
    }

    public virtual bool _isTenpaySign(ArrayList akeys)
    {
      StringBuilder stringBuilder = new StringBuilder();
      foreach (string akey in akeys)
      {
        string parameter = (string) this.parameters[(object) akey];
        if (parameter != null && "".CompareTo(parameter) != 0 && "sign".CompareTo(akey) != 0 && "key".CompareTo(akey) != 0)
          stringBuilder.Append(akey + "=" + parameter + "&");
      }
      stringBuilder.Append("key=" + this.getKey());
      string lower = MD5Util.GetMD5(stringBuilder.ToString(), this.getCharset()).ToLower();
      this.setDebugInfo(stringBuilder.ToString() + " => sign:" + lower);
      return this.getParameter("sign").ToLower().Equals(lower);
    }
  }
}
