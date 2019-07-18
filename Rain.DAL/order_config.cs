// Decompiled with JetBrains decompiler
// Type: Rain.DAL.orderconfig
// Assembly: Rain.DAL, Version=4.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 34D3CCEA-896B-46C7-93D3-7BA93E9004E2
// Assembly location: C:\Users\Felix.Ho\Desktop\page\bin\Rain.DAL.dll

using Rain.Common;

namespace Rain.DAL
{
  public class orderconfig
  {
    private static object lockHelper = new object();

    public Rain.Model.orderconfig loadConfig(string configFilePath)
    {
      return (Rain.Model.orderconfig) SerializationHelper.Load(typeof (Rain.Model.orderconfig), configFilePath);
    }

    public Rain.Model.orderconfig saveConifg(Rain.Model.orderconfig model, string configFilePath)
    {
      lock (orderconfig.lockHelper)
        SerializationHelper.Save((object) model, configFilePath);
      return model;
    }
  }
}
