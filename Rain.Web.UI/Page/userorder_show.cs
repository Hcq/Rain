// Decompiled with JetBrains decompiler
// Type: Rain.Web.UI.Page.userorder_show
// Assembly: Rain.Web.UI, Version=4.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 3E78344A-857F-445F-804E-AF1B978FD5C7
// Assembly location: C:\Users\Felix.Ho\Desktop\page\bin\Rain.Web.UI.dll

using System.Web;
using Rain.Common;

namespace Rain.Web.UI.Page
{
  public class userorder_show : UserPage
  {
    protected int id;
    protected Rain.Model.orders model;
    protected Rain.Model.payment payModel;

    protected override void InitPage()
    {
      this.id = DTRequest.GetQueryInt("id");
      Rain.BLL.orders orders = new Rain.BLL.orders();
      if (!orders.Exists(this.id))
      {
        HttpContext.Current.Response.Redirect(this.linkurl("error", (object) ("?msg=" + Utils.UrlEncode("出错了，您要浏览的页面不存在或已删除！"))));
      }
      else
      {
        this.model = orders.GetModel(this.id);
        if (this.model.user_id != this.userModel.id)
        {
          HttpContext.Current.Response.Redirect(this.linkurl("error", (object) ("?msg=" + Utils.UrlEncode("出错了，您所查看的并非自己的订单信息！"))));
        }
        else
        {
          this.payModel = new Rain.BLL.payment().GetModel(this.model.payment_id);
          if (this.payModel == null)
            this.payModel = new Rain.Model.payment();
          if (this.model.status <= 1 || this.model.status >= 4 || this.model.express_status != 2 || this.model.express_no.Trim().Length <= 0)
            return;
          new Rain.BLL.express().GetModel(this.model.express_id);
          new Rain.BLL.orderconfig().loadConfig();
        }
      }
    }
  }
}
