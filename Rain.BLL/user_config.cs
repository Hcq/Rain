// Decompiled with JetBrains decompiler
// Type: Rain.BLL.userconfig
// Assembly: Rain.BLL, Version=4.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 8924FCAE-37FA-4301-A188-DA020D33C8EB
// Assembly location: C:\Users\Felix.Ho\Desktop\page\bin\Rain.BLL.dll

using Rain.Common;

namespace Rain.BLL
{
  public class userconfig
  {
    private readonly Rain.DAL.userconfig dal = new Rain.DAL.userconfig();

    public Rain.Model.userconfig loadConfig()
    {
      Rain.Model.userconfig userconfig = CacheHelper.Get<Rain.Model.userconfig>("dt_cache_user_config");
      if (userconfig == null)
      {
        CacheHelper.Insert("dt_cache_user_config", (object) this.dal.loadConfig(Utils.GetXmlMapPath("Userpath")), Utils.GetXmlMapPath("Userpath"));
        userconfig = CacheHelper.Get<Rain.Model.userconfig>("dt_cache_user_config");
      }
      return userconfig;
    }

    public Rain.Model.userconfig saveConifg(Rain.Model.userconfig model)
    {
      return this.dal.saveConifg(model, Utils.GetXmlMapPath("Userpath"));
    }
  }
}
