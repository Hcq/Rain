// Decompiled with JetBrains decompiler
// Type: Rain.Web.UI.Page.usermessage_show
// Assembly: Rain.Web.UI, Version=4.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 3E78344A-857F-445F-804E-AF1B978FD5C7
// Assembly location: C:\Users\Felix.Ho\Desktop\page\bin\Rain.Web.UI.dll

using System;
using System.Web;
using Rain.Common;

namespace Rain.Web.UI.Page
{
  public class usermessage_show : UserPage
  {
    protected Rain.Model.user_message model = new Rain.Model.user_message();
    protected int id;

    protected override void InitPage()
    {
      this.id = DTRequest.GetQueryInt("id");
      Rain.BLL.user_message userMessage = new Rain.BLL.user_message();
      if (!userMessage.Exists(this.id))
      {
        HttpContext.Current.Response.Redirect(this.linkurl("error", (object) ("?msg=" + Utils.UrlEncode("出错了，您要浏览的页面不存在或已删除！"))));
      }
      else
      {
        this.model = userMessage.GetModel(this.id);
        if (this.model.accept_user_name != this.userModel.user_name && this.model.post_user_name != this.userModel.user_name)
          HttpContext.Current.Response.Redirect(this.linkurl("error", (object) ("?msg=" + Utils.UrlEncode("出错了，您所查看的并非自己的短消息！"))));
        else
          userMessage.UpdateField(this.id, "is_read=1,read_time='" + (object) DateTime.Now + "'");
      }
    }
  }
}
