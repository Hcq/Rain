// Decompiled with JetBrains decompiler
// Type: Rain.API.Payment.tenpaypc.RequestHandler
// Assembly: Rain.API, Version=4.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 114351D3-1A2F-4CC4-A6A6-43C59DB5A56B
// Assembly location: C:\Users\Felix.Ho\Desktop\page\bin\Rain.API.dll

using System.Collections;
using System.Text;
using System.Web;

namespace Rain.API.Payment.tenpaypc
{
  public class RequestHandler
  {
    private string gateUrl;
    private string key;
    protected Hashtable parameters;
    private string debugInfo;
    protected HttpContext httpContext;

    public RequestHandler(HttpContext httpContext)
    {
      this.parameters = new Hashtable();
      this.httpContext = httpContext;
      this.setGateUrl("https://www.tenpay.com/cgi-bin/v1.0/service_gate.cgi");
    }

    public virtual void init()
    {
    }

    public string getGateUrl()
    {
      return this.gateUrl;
    }

    public void setGateUrl(string gateUrl)
    {
      this.gateUrl = gateUrl;
    }

    public string getKey()
    {
      return this.key;
    }

    public void setKey(string key)
    {
      this.key = key;
    }

    public virtual string getRequestURL()
    {
      this.createSign();
      StringBuilder stringBuilder = new StringBuilder();
      ArrayList arrayList = new ArrayList(this.parameters.Keys);
      arrayList.Sort();
      foreach (string strB in arrayList)
      {
        string parameter = (string) this.parameters[(object) strB];
        if (parameter != null && "key".CompareTo(strB) != 0)
          stringBuilder.Append(strB + "=" + new TenpayUtil().UrlEncode(parameter, this.getCharset()) + "&");
      }
      if (stringBuilder.Length > 0)
        stringBuilder.Remove(stringBuilder.Length - 1, 1);
      return this.getGateUrl() + "?" + stringBuilder.ToString();
    }

    protected virtual void createSign()
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
      this.setParameter("sign", lower);
      this.setDebugInfo(stringBuilder.ToString() + " => sign:" + lower);
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

    public void doSend()
    {
      this.httpContext.Response.Redirect(this.getRequestURL());
    }

    public string getDebugInfo()
    {
      return this.debugInfo;
    }

    public void setDebugInfo(string debugInfo)
    {
      this.debugInfo = debugInfo;
    }

    public Hashtable getAllParameters()
    {
      return this.parameters;
    }

    protected virtual string getCharset()
    {
      return this.httpContext.Request.ContentEncoding.BodyName;
    }
  }
}
