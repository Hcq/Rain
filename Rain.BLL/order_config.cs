// Decompiled with JetBrains decompiler
// Type: Rain.BLL.orderconfig
// Assembly: Rain.BLL, Version=4.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 8924FCAE-37FA-4301-A188-DA020D33C8EB
// Assembly location: C:\Users\Felix.Ho\Desktop\page\bin\Rain.BLL.dll

using Rain.Common;

namespace Rain.BLL
{
  public class orderconfig
  {
    private readonly Rain.DAL.orderconfig dal = new Rain.DAL.orderconfig();

    public Rain.Model.orderconfig loadConfig()
    {
      Rain.Model.orderconfig orderconfig = CacheHelper.Get<Rain.Model.orderconfig>("dt_cache_order_config");
      if (orderconfig == null)
      {
        CacheHelper.Insert("dt_cache_order_config", (object) this.dal.loadConfig(Utils.GetXmlMapPath("Orderpath")), Utils.GetXmlMapPath("Orderpath"));
        orderconfig = CacheHelper.Get<Rain.Model.orderconfig>("dt_cache_order_config");
      }
      return orderconfig;
    }

    public Rain.Model.orderconfig saveConifg(Rain.Model.orderconfig model)
    {
      return this.dal.saveConifg(model, Utils.GetXmlMapPath("Orderpath"));
    }
  }
}
