// Decompiled with JetBrains decompiler
// Type: Rain.Web.UI.Page.search
// Assembly: Rain.Web.UI, Version=4.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 3E78344A-857F-445F-804E-AF1B978FD5C7
// Assembly location: C:\Users\Felix.Ho\Desktop\page\bin\Rain.Web.UI.dll

using System;
using System.Data;
using Rain.Common;

namespace Rain.Web.UI.Page
{
  public class search : BasePage
  {
    protected string channel = string.Empty;
    protected string keyword = string.Empty;
    protected string pagelist = string.Empty;
    protected int page;
    protected int totalcount;

    protected override void ShowPage()
    {
      this.page = DTRequest.GetQueryInt("page", 1);
      this.keyword = DTRequest.GetQueryString("keyword").Replace("'", string.Empty);
      this.channel = DTRequest.GetQueryString("channel").Replace("'", string.Empty);
    }

    protected DataTable get_search_list(int _pagesize, out int _totalcount)
    {
      DataTable dataTable = new DataTable();
      dataTable.Columns.Add("id", Type.GetType("System.Int32"));
      dataTable.Columns.Add("title", Type.GetType("System.String"));
      dataTable.Columns.Add("remark", Type.GetType("System.String"));
      dataTable.Columns.Add("channel_id", Type.GetType("System.String"));
      dataTable.Columns.Add("link_url", Type.GetType("System.String"));
      dataTable.Columns.Add("add_time", Type.GetType("System.String"));
      dataTable.Columns.Add("img_url", Type.GetType("System.String"));
      DataSet search = new Rain.BLL.article().GetSearch(this.channel, _pagesize, this.page, "(title like '%" + this.keyword + "%' or zhaiyao like '%" + this.keyword + "%')", "add_time desc,id desc", out _totalcount);
      if (search.Tables[0].Rows.Count > 0)
      {
        for (int index = 0; index < search.Tables[0].Rows.Count; ++index)
        {
          DataRow row1 = search.Tables[0].Rows[index];
          string urlRewrite = this.get_url_rewrite(Utils.StrToInt(row1["channel_id"].ToString(), 0), row1["call_index"].ToString(), Utils.StrToInt(row1["id"].ToString(), 0));
          if (!string.IsNullOrEmpty(urlRewrite))
          {
            DataRow row2 = dataTable.NewRow();
            row2["id"] = row1["id"];
            row2["title"] = row1["title"];
            row2["remark"] = row1["zhaiyao"];
            row2["link_url"] = (object) urlRewrite;
            row2["add_time"] = row1["add_time"];
            row2["channel_id"] = row1["channel_id"];
            row2["img_url"] = row1["img_url"];
            dataTable.Rows.Add(row2);
          }
        }
      }
      return dataTable;
    }

    private string get_url_rewrite(int channel_id, string call_index, int id)
    {
      if (channel_id == 0)
        return string.Empty;
      string str = id.ToString();
      string channelName = new Rain.BLL.channel().GetChannelName(channel_id);
      if (string.IsNullOrEmpty(channelName))
        return string.Empty;
      if (!string.IsNullOrEmpty(call_index))
        str = call_index;
      Rain.Model.url_rewrite info = new Rain.BLL.url_rewrite().GetInfo(channelName, "detail");
      if (info == null)
        return string.Empty;
      return this.linkurl(info.name, (object) str);
    }
  }
}
