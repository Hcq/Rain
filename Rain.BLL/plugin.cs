// Decompiled with JetBrains decompiler
// Type: Rain.BLL.plugin
// Assembly: Rain.BLL, Version=4.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 8924FCAE-37FA-4301-A188-DA020D33C8EB
// Assembly location: C:\Users\Felix.Ho\Desktop\page\bin\Rain.BLL.dll

using System.Collections.Generic;

namespace Rain.BLL
{
  public class plugin
  {
    private readonly Rain.Model.siteconfig siteConfig = new siteconfig().loadConfig();
    private readonly Rain.DAL.plugin dal;

    public plugin()
    {
      this.dal = new Rain.DAL.plugin(this.siteConfig.sysdatabaseprefix);
    }

    public List<Rain.Model.plugin> GetList(string dirPath)
    {
      return this.dal.GetList(dirPath);
    }

    public Rain.Model.plugin GetInfo(string dirPath)
    {
      return this.dal.GetInfo(dirPath);
    }

    public void MarkTemplet(
      string sitePath,
      string tempPath,
      string skinName,
      string dirPath,
      string xPath)
    {
      this.dal.MarkTemplet(sitePath, tempPath, skinName, dirPath, xPath);
    }

    public bool UpdateNodeValue(string dirPath, string node, string value)
    {
      return this.dal.UpdateNodeValue(dirPath, node, value);
    }

    public bool ExeSqlStr(string dirPath, string xPath)
    {
      return this.dal.ExeSqlStr(dirPath, xPath);
    }

    public bool AppendNodes(string dirPath, string xPath)
    {
      return this.dal.AppendNodes(dirPath, xPath);
    }

    public bool RemoveNodes(string dirPath, string xPath)
    {
      return this.dal.RemoveNodes(dirPath, xPath);
    }

    public bool AppendMenuNodes(string navPath, string dirPath, string xPath, string parentName)
    {
      return this.dal.AppendMenuNodes(navPath, dirPath, xPath, parentName);
    }

    public void RemoveMenuNodes(string dirPath, string xPath)
    {
      this.dal.RemoveMenuNodes(dirPath, xPath);
    }
  }
}
