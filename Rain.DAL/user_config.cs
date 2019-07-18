// Decompiled with JetBrains decompiler
// Type: Rain.DAL.userconfig
// Assembly: Rain.DAL, Version=4.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 34D3CCEA-896B-46C7-93D3-7BA93E9004E2
// Assembly location: C:\Users\Felix.Ho\Desktop\page\bin\Rain.DAL.dll

using Rain.Common;

namespace Rain.DAL
{
  public class userconfig
  {
    private static object lockHelper = new object();

    public Rain.Model.userconfig loadConfig(string configFilePath)
    {
      return (Rain.Model.userconfig) SerializationHelper.Load(typeof (Rain.Model.userconfig), configFilePath);
    }

    public Rain.Model.userconfig saveConifg(Rain.Model.userconfig model, string configFilePath)
    {
      lock (userconfig.lockHelper)
        SerializationHelper.Save((object) model, configFilePath);
      return model;
    }
  }
}
