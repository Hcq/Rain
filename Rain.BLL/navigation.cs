// Decompiled with JetBrains decompiler
// Type: Rain.BLL.navigation
// Assembly: Rain.BLL, Version=4.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 8924FCAE-37FA-4301-A188-DA020D33C8EB
// Assembly location: C:\Users\Felix.Ho\Desktop\page\bin\Rain.BLL.dll

using System.Data;

namespace Rain.BLL
{
  public class navigation
  {
    private readonly Rain.Model.siteconfig siteConfig = new siteconfig().loadConfig();
    private readonly Rain.DAL.navigation dal;

    public navigation()
    {
      this.dal = new Rain.DAL.navigation(this.siteConfig.sysdatabaseprefix);
    }

    public bool Exists(int id)
    {
      return this.dal.Exists(id);
    }

    public bool Exists(string name)
    {
      return this.dal.Exists(name);
    }

    public int Add(Rain.Model.navigation model)
    {
      return this.dal.Add(model);
    }

    public bool Update(Rain.Model.navigation model)
    {
      return this.dal.Update(model);
    }

    public bool Delete(int id)
    {
      return this.dal.Delete(id);
    }

    public Rain.Model.navigation GetModel(int id)
    {
      return this.dal.GetModel(id);
    }

    public Rain.Model.navigation GetModel(string name)
    {
      return this.dal.GetModel(name);
    }

    public DataTable GetList(int parent_id, string nav_type)
    {
      return this.dal.GetList(parent_id, nav_type);
    }

    public int GetNavId(string nav_name)
    {
      return this.dal.GetNavId(nav_name);
    }

    public int Add(
      string parent_name,
      string nav_name,
      string title,
      string link_url,
      int sort_id,
      int channel_id,
      string action_type)
    {
      return this.dal.Add(parent_name, nav_name, title, link_url, sort_id, channel_id, action_type);
    }

    public bool UpdateField(int id, string strValue)
    {
      return this.dal.UpdateField(id, strValue);
    }

    public bool UpdateField(string name, string strValue)
    {
      return this.dal.UpdateField(name, strValue);
    }
  }
}
