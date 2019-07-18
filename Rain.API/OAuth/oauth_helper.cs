// Decompiled with JetBrains decompiler
// Type: Rain.API.OAuth.oauth_helper
// Assembly: Rain.API, Version=4.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 114351D3-1A2F-4CC4-A6A6-43C59DB5A56B
// Assembly location: C:\Users\Felix.Ho\Desktop\page\bin\Rain.API.dll

using System.Web;

namespace Rain.API.OAuth
{
  public class oauth_helper
  {
    public static oauth_config get_config(string oauth_name)
    {
      Rain.Model.user_oauth_app model = new Rain.BLL.user_oauth_app().GetModel(oauth_name);
      if (model == null)
        return (oauth_config) null;
      Rain.Model.siteconfig siteconfig = new Rain.BLL.siteconfig().loadConfig();
      return new oauth_config()
      {
        oauth_name = model.api_path.Trim(),
        oauth_app_id = model.app_id.Trim(),
        oauth_app_key = model.app_key.Trim(),
        return_uri = "http://" + HttpContext.Current.Request.Url.Authority.ToLower() + siteconfig.webpath + "api/oauth/" + model.api_path + "/return_url.aspx"
      };
    }
  }
}
