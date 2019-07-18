// Decompiled with JetBrains decompiler
// Type: Rain.BLL.article_category
// Assembly: Rain.BLL, Version=4.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 8924FCAE-37FA-4301-A188-DA020D33C8EB
// Assembly location: C:\Users\Felix.Ho\Desktop\page\bin\Rain.BLL.dll

using System.Data;

namespace Rain.BLL
{
  public class article_category
  {
    private readonly Rain.Model.siteconfig siteConfig = new siteconfig().loadConfig();
    private readonly Rain.DAL.article_category dal;

    public article_category()
    {
      this.dal = new Rain.DAL.article_category(this.siteConfig.sysdatabaseprefix);
    }

    public bool Exists(int id)
    {
      return this.dal.Exists(id);
    }

    public string GetTitle(int id)
    {
      return this.dal.GetTitle(id);
    }

    public int Add(Rain.Model.article_category model)
    {
      return this.dal.Add(model);
    }

    public void UpdateField(int id, string strValue)
    {
      this.dal.UpdateField(id, strValue);
    }

    public bool Update(Rain.Model.article_category model)
    {
      return this.dal.Update(model);
    }

    public void Delete(int id)
    {
      this.dal.Delete(id);
    }

    public Rain.Model.article_category GetModel(int id)
    {
      return this.dal.GetModel(id);
    }

    public DataTable GetChildList(int parent_id, int channel_id)
    {
      return this.dal.GetChildList(parent_id, channel_id);
    }

    public DataTable GetChildList(int parent_id, string channel_name)
    {
      int channelId = new channel().GetChannelId(channel_name);
      return this.dal.GetChildList(parent_id, channelId);
    }

    public DataTable GetList(int parent_id, int channel_id)
    {
      return this.dal.GetList(parent_id, channel_id);
    }

    public DataTable GetList(int parent_id, string channel_name)
    {
      int channelId = new channel().GetChannelId(channel_name);
      return this.dal.GetList(parent_id, channelId);
    }

    public int GetParentId(int id)
    {
      return this.dal.GetParentId(id);
    }
  }
}
