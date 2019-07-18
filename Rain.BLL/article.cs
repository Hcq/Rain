// Decompiled with JetBrains decompiler
// Type: Rain.BLL.article
// Assembly: Rain.BLL, Version=4.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 8924FCAE-37FA-4301-A188-DA020D33C8EB
// Assembly location: C:\Users\Felix.Ho\Desktop\page\bin\Rain.BLL.dll

using System.Data;
using Rain.Common;

namespace Rain.BLL
{
  public class article
  {
    private readonly Rain.Model.siteconfig siteConfig = new siteconfig().loadConfig();
    private readonly Rain.DAL.article dal;

    public article()
    {
      this.dal = new Rain.DAL.article(this.siteConfig.sysdatabaseprefix);
    }

    public bool Exists(int id)
    {
      return this.dal.Exists(id);
    }

    public bool Exists(string call_index)
    {
      return this.dal.Exists(call_index);
    }

    public int Add(Rain.Model.article model)
    {
      return this.dal.Add(model);
    }

    public bool Update(Rain.Model.article model)
    {
      return this.dal.Update(model);
    }

    public bool Delete(int id)
    {
      string content = this.dal.GetContent(id);
      bool flag = this.dal.Delete(id);
      if (flag && !string.IsNullOrEmpty(content))
        Utils.DeleteContentPic(content, this.siteConfig.webpath + this.siteConfig.filepath);
      return flag;
    }

    public Rain.Model.article GetModel(int id)
    {
      return this.dal.GetModel(id);
    }

    public Rain.Model.article GetModel(string call_index)
    {
      return this.dal.GetModel(call_index);
    }

    public DataSet GetList(int Top, string strWhere, string filedOrder)
    {
      return this.dal.GetList(Top, strWhere, filedOrder);
    }

    public DataSet GetList(
      int channel_id,
      int category_id,
      int pageSize,
      int pageIndex,
      string strWhere,
      string filedOrder,
      out int recordCount)
    {
      return this.dal.GetList(channel_id, category_id, pageSize, pageIndex, strWhere, filedOrder, out recordCount);
    }

    public bool ExistsTitle(string title)
    {
      return this.dal.ExistsTitle(title);
    }

    public bool ExistsTitle(string title, int category_id)
    {
      return this.dal.ExistsTitle(title, category_id);
    }

    public string GetTitle(int id)
    {
      return this.dal.GetTitle(id);
    }

    public int GetClick(int id)
    {
      return this.dal.GetClick(id);
    }

    public int GetCount(string strWhere)
    {
      return this.dal.GetCount(strWhere);
    }

    public int GetStockQuantity(int id)
    {
      return this.dal.GetStockQuantity(id);
    }

    public void UpdateField(int id, string strValue)
    {
      this.dal.UpdateField(id, strValue);
    }

    public DataSet GetList(
      string channel_name,
      int Top,
      string strWhere,
      string filedOrder)
    {
      return this.dal.GetList(channel_name, Top, strWhere, filedOrder);
    }

    public DataSet GetList(
      string channel_name,
      int category_id,
      int Top,
      string strWhere,
      string filedOrder)
    {
      return this.dal.GetList(channel_name, category_id, Top, strWhere, filedOrder);
    }

    public DataSet GetList(
      string channel_name,
      int category_id,
      int pageSize,
      int pageIndex,
      string strWhere,
      string filedOrder,
      out int recordCount)
    {
      return this.dal.GetList(channel_name, category_id, pageSize, pageIndex, strWhere, filedOrder, out recordCount);
    }

    public int GetCount(string channel_name, int category_id, string strWhere)
    {
      return this.dal.GetCount(channel_name, category_id, strWhere);
    }

    public DataSet GetSearch(
      string channel_name,
      int pageSize,
      int pageIndex,
      string strWhere,
      string filedOrder,
      out int recordCount)
    {
      return this.dal.GetSearch(channel_name, pageSize, pageIndex, strWhere, filedOrder, out recordCount);
    }
  }
}
