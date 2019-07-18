// Decompiled with JetBrains decompiler
// Type: Rain.Web.tools.download
// Assembly: Rain.Web, Version=4.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 7C74D3EC-D223-4A13-9304-5BE7CB5803E5
// Assembly location: C:\Users\Felix.Ho\Desktop\page\bin\Rain.Web.dll

using System.IO;
using System.Text;
using System.Web;
using System.Web.SessionState;
using Rain.Common;
using Rain.Web.UI;

namespace Rain.Web.tools
{
  public class download : IHttpHandler, IRequiresSessionState
  {
    private Rain.Model.siteconfig siteConfig = new Rain.BLL.siteconfig().loadConfig();

    public void ProcessRequest(HttpContext context)
    {
      string queryString = DTRequest.GetQueryString("site");
      int queryInt = DTRequest.GetQueryInt("id");
      if (string.IsNullOrEmpty(queryString))
        context.Response.Write("出错了，站点传输参数不正确！");
      else if (queryInt < 1)
      {
        context.Response.Redirect(new BasePage().getlink(queryString, new BasePage().linkurl("error", (object) ("?msg=" + Utils.UrlEncode("出错了，文件参数传值不正确！")))));
      }
      else
      {
        Rain.BLL.article_attach articleAttach = new Rain.BLL.article_attach();
        if (!articleAttach.Exists(queryInt))
        {
          context.Response.Redirect(new BasePage().getlink(queryString, new BasePage().linkurl("error", (object) ("?msg=" + Utils.UrlEncode("出错了，您要下载的文件不存在或已经被删除！")))));
        }
        else
        {
          Rain.Model.article_attach model = articleAttach.GetModel(queryInt);
          if (model.point > 0)
          {
            Rain.Model.users userInfo = new BasePage().GetUserInfo();
            if (userInfo == null)
              HttpContext.Current.Response.Redirect(new BasePage().getlink(queryString, new BasePage().linkurl("login")));
            if (!articleAttach.ExistsLog(model.id, userInfo.id))
            {
              if (model.point > userInfo.point)
              {
                context.Response.Redirect(new BasePage().getlink(queryString, new BasePage().linkurl("error", (object) ("?msg=" + Utils.UrlEncode("出错啦，您的积分不足支付本次下载！")))));
                return;
              }
              if (new Rain.BLL.user_point_log().Add(userInfo.id, userInfo.user_name, model.point * -1, "下载附件：“" + model.file_name + "”，扣减积分", false) > 0)
                articleAttach.AddLog(userInfo.id, userInfo.user_name, model.id, model.file_name);
            }
          }
          articleAttach.UpdateField(queryInt, "down_num=down_num+1");
          if (model.file_path.ToLower().StartsWith("http://"))
          {
            context.Response.Redirect(model.file_path);
          }
          else
          {
            string mapPath = Utils.GetMapPath(model.file_path);
            if (!File.Exists(mapPath))
            {
              context.Response.Redirect(new BasePage().getlink(queryString, new BasePage().linkurl("error", (object) ("?msg=" + Utils.UrlEncode("出错了，您要下载的文件不存在或已经被删除！")))));
            }
            else
            {
              FileInfo fileInfo = new FileInfo(mapPath);
              context.Response.ContentEncoding = Encoding.GetEncoding("UTF-8");
              context.Response.AddHeader("Content-Disposition", "attachment; filename=" + HttpUtility.UrlEncode(model.file_name));
              context.Response.AddHeader("Content-length", fileInfo.Length.ToString());
              context.Response.ContentType = "application/pdf";
              context.Response.WriteFile(fileInfo.FullName);
              context.Response.End();
            }
          }
        }
      }
    }

    public bool IsReusable
    {
      get
      {
        return false;
      }
    }
  }
}
