// Decompiled with JetBrains decompiler
// Type: Rain.DAL.siteconfig
// Assembly: Rain.DAL, Version=4.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 34D3CCEA-896B-46C7-93D3-7BA93E9004E2
// Assembly location: C:\Users\Felix.Ho\Desktop\page\bin\Rain.DAL.dll

using Rain.Common;

namespace Rain.DAL
{
  public class siteconfig
  {
    private static object lockHelper = new object();

    public Rain.Model.siteconfig loadConfig(string configFilePath)
    {
      return (Rain.Model.siteconfig) SerializationHelper.Load(typeof (Rain.Model.siteconfig), configFilePath);
    }

    public Rain.Model.siteconfig saveConifg(Rain.Model.siteconfig model, string configFilePath)
    {
      lock (siteconfig.lockHelper)
        SerializationHelper.Save((object) model, configFilePath);
      return model;
    }
  }
}
