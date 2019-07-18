// Decompiled with JetBrains decompiler
// Type: Rain.BLL.siteconfig
// Assembly: Rain.BLL, Version=4.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 8924FCAE-37FA-4301-A188-DA020D33C8EB
// Assembly location: C:\Users\Felix.Ho\Desktop\page\bin\Rain.BLL.dll

using Rain.Common;

namespace Rain.BLL
{
  public class siteconfig
  {
    private readonly Rain.DAL.siteconfig dal = new Rain.DAL.siteconfig();

    public Rain.Model.siteconfig loadConfig()
    {
      Rain.Model.siteconfig siteconfig = CacheHelper.Get<Rain.Model.siteconfig>("dt_cache_site_config");
      if (siteconfig == null)
      {
        CacheHelper.Insert("dt_cache_site_config", (object) this.dal.loadConfig(Utils.GetXmlMapPath("Configpath")), Utils.GetXmlMapPath("Configpath"));
        siteconfig = CacheHelper.Get<Rain.Model.siteconfig>("dt_cache_site_config");
      }
      return siteconfig;
    }

    public Rain.Model.siteconfig saveConifg(Rain.Model.siteconfig model)
    {
      return this.dal.saveConifg(model, Utils.GetXmlMapPath("Configpath"));
    }
  }
}
