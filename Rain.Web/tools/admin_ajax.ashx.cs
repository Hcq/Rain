// Decompiled with JetBrains decompiler
// Type: Rain.Web.tools.admin_ajax
// Assembly: Rain.Web, Version=4.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 7C74D3EC-D223-4A13-9304-5BE7CB5803E5
// Assembly location: C:\Users\Felix.Ho\Desktop\page\bin\Rain.Web.dll

using System;
using System.Collections.Generic;
using System.Data;
using System.Net;
using System.Web;
using System.Web.SessionState;
using Rain.BLL;
using Rain.Common;
using Rain.Model;
using Rain.Web.UI;

namespace Rain.Web.tools
{
  public class admin_ajax : IHttpHandler, IRequiresSessionState
  {
    private Rain.Model.siteconfig siteConfig = new Rain.BLL.siteconfig().loadConfig();
    private Rain.Model.userconfig userConfig = new Rain.BLL.userconfig().loadConfig();

    public void ProcessRequest(HttpContext context)
    {
      if (!new ManagePage().IsAdminLogin())
      {
        context.Response.Write("{\"status\": 0, \"msg\": \"尚未登录或已超时，请登录后操作！\"}");
      }
      else
      {
        switch (DTRequest.GetQueryString("action"))
        {
          case "username_validate":
            this.username_validate(context);
            break;
          case "attribute_field_validate":
            this.attribute_field_validate(context);
            break;
          case "channel_name_validate":
            this.channel_name_validate(context);
            break;
          case "channel_site_validate":
            this.channel_site_validate(context);
            break;
          case "urlrewrite_name_validate":
            this.urlrewrite_name_validate(context);
            break;
          case "navigation_validate":
            this.navigation_validate(context);
            break;
          case "manager_validate":
            this.manager_validate(context);
            break;
          case "get_navigation_list":
            this.get_navigation_list(context);
            break;
          case "get_remote_fileinfo":
            this.get_remote_fileinfo(context);
            break;
          case "sms_message_post":
            this.sms_message_post(context);
            break;
          case "edit_order_status":
            this.edit_order_status(context);
            break;
          case "get_builder_urls":
            this.get_builder_urls(context);
            break;
          case "get_builder_html":
            this.get_builder_html(context);
            break;
        }
      }
    }

    private void username_validate(HttpContext context)
    {
      string str1 = DTRequest.GetString("param");
      if (string.IsNullOrEmpty(str1))
      {
        context.Response.Write("{ \"info\":\"用户名不可为空\", \"status\":\"n\" }");
      }
      else
      {
        string regkeywords = this.userConfig.regkeywords;
        char[] chArray = new char[1]{ ',' };
        foreach (string str2 in regkeywords.Split(chArray))
        {
          if (str2.ToLower() == str1.ToLower())
          {
            context.Response.Write("{ \"info\":\"该用户名不可用\", \"status\":\"n\" }");
            return;
          }
        }
        if (!new Rain.BLL.users().Exists(str1.Trim()))
          context.Response.Write("{ \"info\":\"该用户名可用\", \"status\":\"y\" }");
        else
          context.Response.Write("{ \"info\":\"该用户名已被注册\", \"status\":\"n\" }");
      }
    }

    private void attribute_field_validate(HttpContext context)
    {
      string column_name = DTRequest.GetString("param");
      if (string.IsNullOrEmpty(column_name))
        context.Response.Write("{ \"info\":\"名称不可为空\", \"status\":\"n\" }");
      else if (new Rain.BLL.article_attribute_field().Exists(column_name))
        context.Response.Write("{ \"info\":\"该名称已被占用，请更换！\", \"status\":\"n\" }");
      else
        context.Response.Write("{ \"info\":\"该名称可使用\", \"status\":\"y\" }");
    }

    private void channel_name_validate(HttpContext context)
    {
      string name = DTRequest.GetString("param");
      string str = DTRequest.GetString("old_channel_name");
      if (string.IsNullOrEmpty(name))
        context.Response.Write("{ \"info\":\"频道名称不可为空！\", \"status\":\"n\" }");
      else if (name.ToLower() == str.ToLower())
        context.Response.Write("{ \"info\":\"该名称可使用\", \"status\":\"y\" }");
      else if (new Rain.BLL.channel().Exists(name))
        context.Response.Write("{ \"info\":\"该名称已被占用，请更换！\", \"status\":\"n\" }");
      else
        context.Response.Write("{ \"info\":\"该名称可使用\", \"status\":\"y\" }");
    }

    private void channel_site_validate(HttpContext context)
    {
      string build_path = DTRequest.GetString("param");
      string str = DTRequest.GetString("old_build_path");
      if (string.IsNullOrEmpty(build_path))
        context.Response.Write("{ \"info\":\"该目录名不可为空！\", \"status\":\"n\" }");
      else if (build_path.ToLower() == str.ToLower())
        context.Response.Write("{ \"info\":\"该目录名可使用\", \"status\":\"y\" }");
      else if (new Rain.BLL.channel_site().Exists(build_path))
        context.Response.Write("{ \"info\":\"该目录名已被占用，请更换！\", \"status\":\"n\" }");
      else
        context.Response.Write("{ \"info\":\"该目录名可使用\", \"status\":\"y\" }");
    }

    private void urlrewrite_name_validate(HttpContext context)
    {
      string name = DTRequest.GetString("param");
      string str = DTRequest.GetString("old_name");
      if (string.IsNullOrEmpty(name))
        context.Response.Write("{ \"info\":\"名称不可为空！\", \"status\":\"n\" }");
      else if (name.ToLower() == str.ToLower())
        context.Response.Write("{ \"info\":\"该名称可使用\", \"status\":\"y\" }");
      else if (new Rain.BLL.url_rewrite().Exists(name))
        context.Response.Write("{ \"info\":\"该名称已被使用，请更换！\", \"status\":\"n\" }");
      else
        context.Response.Write("{ \"info\":\"该名称可使用\", \"status\":\"y\" }");
    }

    private void navigation_validate(HttpContext context)
    {
      string name = DTRequest.GetString("param");
      string str = DTRequest.GetString("old_name");
      if (string.IsNullOrEmpty(name))
        context.Response.Write("{ \"info\":\"该导航别名不可为空！\", \"status\":\"n\" }");
      else if (name.ToLower() == str.ToLower())
        context.Response.Write("{ \"info\":\"该导航别名可使用\", \"status\":\"y\" }");
      else if (name.ToLower().StartsWith("channel_"))
        context.Response.Write("{ \"info\":\"该导航别名系统保留，请更换！\", \"status\":\"n\" }");
      else if (new Rain.BLL.navigation().Exists(name))
        context.Response.Write("{ \"info\":\"该导航别名已被占用，请更换！\", \"status\":\"n\" }");
      else
        context.Response.Write("{ \"info\":\"该导航别名可使用\", \"status\":\"y\" }");
    }

    private void manager_validate(HttpContext context)
    {
      string user_name = DTRequest.GetString("param");
      if (string.IsNullOrEmpty(user_name))
        context.Response.Write("{ \"info\":\"请输入用户名\", \"status\":\"n\" }");
      else if (new Rain.BLL.manager().Exists(user_name))
        context.Response.Write("{ \"info\":\"用户名已被占用，请更换！\", \"status\":\"n\" }");
      else
        context.Response.Write("{ \"info\":\"用户名可使用\", \"status\":\"y\" }");
    }

    private void get_navigation_list(HttpContext context)
    {
      Rain.Model.manager adminInfo = new ManagePage().GetAdminInfo();
      if (adminInfo == null)
        return;
      Rain.Model.manager_role model = new Rain.BLL.manager_role().GetModel(adminInfo.role_id);
      if (model == null)
        return;
      DataTable list = new Rain.BLL.navigation().GetList(0, DTEnums.NavigationEnum.System.ToString());
      this.get_navigation_childs(context, list, 0, model.role_type, model.manager_role_values);
    }

    private void get_navigation_childs(
      HttpContext context,
      DataTable oldData,
      int parent_id,
      int role_type,
      List<manager_role_value> ls)
    {
      DataRow[] dr = oldData.Select("parent_id=" + (object) parent_id);
      bool flag1 = false;
      for (int i = 0; i < dr.Length; ++i)
      {
        bool flag2 = true;
        if (int.Parse(dr[i]["is_lock"].ToString()) == 1)
          flag2 = false;
        if (flag2 && role_type > 1)
        {
          string str1 = dr[i]["action_type"].ToString();
          char[] chArray = new char[1]{ ',' };
          foreach (string str2 in str1.Split(chArray))
          {
            if (str2 == "Show" && ls.Find((Predicate<manager_role_value>) (p => p.nav_name == dr[i]["name"].ToString() && p.action_type == "Show")) == null)
              flag2 = false;
          }
        }
        if (!flag2)
        {
          if (flag1 && i == dr.Length - 1 && parent_id > 0)
            context.Response.Write("</ul>\n");
        }
        else if (parent_id == 0)
        {
          context.Response.Write("<div class=\"list-group\">\n");
          context.Response.Write("<h1 title=\"" + dr[i]["sub_title"].ToString() + "\">");
          if (!string.IsNullOrEmpty(dr[i]["icon_url"].ToString().Trim()))
            context.Response.Write("<img src=\"" + dr[i]["icon_url"].ToString() + "\" />");
          context.Response.Write("</h1>\n");
          context.Response.Write("<div class=\"list-wrap\">\n");
          context.Response.Write("<h2>" + dr[i]["title"].ToString() + "<i></i></h2>\n");
          this.get_navigation_childs(context, oldData, int.Parse(dr[i]["id"].ToString()), role_type, ls);
          context.Response.Write("</div>\n");
          context.Response.Write("</div>\n");
        }
        else
        {
          if (!flag1)
          {
            flag1 = true;
            context.Response.Write("<ul>\n");
          }
          context.Response.Write("<li>\n");
          context.Response.Write("<a navid=\"" + dr[i]["name"].ToString() + "\"");
          if (!string.IsNullOrEmpty(dr[i]["link_url"].ToString()))
          {
            if (int.Parse(dr[i]["channel_id"].ToString()) > 0)
              context.Response.Write(" href=\"" + dr[i]["link_url"].ToString() + "?channel_id=" + dr[i]["channel_id"].ToString() + "\" target=\"mainframe\"");
            else
              context.Response.Write(" href=\"" + dr[i]["link_url"].ToString() + "\" target=\"mainframe\"");
          }
          if (!string.IsNullOrEmpty(dr[i]["icon_url"].ToString()))
            context.Response.Write(" icon=\"" + dr[i]["icon_url"].ToString() + "\"");
          context.Response.Write(" target=\"mainframe\">\n");
          context.Response.Write("<span>" + dr[i]["title"].ToString() + "</span>\n");
          context.Response.Write("</a>\n");
          this.get_navigation_childs(context, oldData, int.Parse(dr[i]["id"].ToString()), role_type, ls);
          context.Response.Write("</li>\n");
          if (i == dr.Length - 1)
            context.Response.Write("</ul>\n");
        }
      }
    }

    private void get_remote_fileinfo(HttpContext context)
    {
      string formString = DTRequest.GetFormString("remotepath");
      if (string.IsNullOrEmpty(formString))
        context.Response.Write("{\"status\": 0, \"msg\": \"没有找到远程附件地址！\"}");
      else if (!formString.ToLower().StartsWith("http://"))
      {
        context.Response.Write("{\"status\": 0, \"msg\": \"不是远程附件地址！\"}");
      }
      else
      {
        try
        {
          int contentLength = (int) WebRequest.Create(formString).GetResponse().ContentLength;
          string str = formString.Substring(formString.LastIndexOf("/") + 1);
          string upper = formString.Substring(formString.LastIndexOf(".") + 1).ToUpper();
          context.Response.Write("{\"status\": 1, \"msg\": \"获取远程文件成功！\", \"name\": \"" + str + "\", \"path\": \"" + formString + "\", \"size\": " + (object) contentLength + ", \"ext\": \"" + upper + "\"}");
        }
        catch
        {
          context.Response.Write("{\"status\": 0, \"msg\": \"远程文件不存在！\"}");
        }
      }
    }

    private void sms_message_post(HttpContext context)
    {
      string formString1 = DTRequest.GetFormString("mobiles");
      string formString2 = DTRequest.GetFormString("content");
      if (formString1 == "")
        context.Response.Write("{\"status\": 0, \"msg\": \"手机号码不能为空！\"}");
      else if (formString2 == "")
      {
        context.Response.Write("{\"status\": 0, \"msg\": \"短信内容不能为空！\"}");
      }
      else
      {
        string msg = string.Empty;
        if (new sms_message().Send(formString1, formString2, 2, out msg))
          context.Response.Write("{\"status\": 1, \"msg\": \"" + msg + "\"}");
        else
          context.Response.Write("{\"status\": 0, \"msg\": \"" + msg + "\"}");
      }
    }

    private void edit_order_status(HttpContext context)
    {
      Rain.Model.manager adminInfo = new ManagePage().GetAdminInfo();
      if (adminInfo == null)
      {
        context.Response.Write("{\"status\": 0, \"msg\": \"未登录或已超时，请重新登录！\"}");
      }
      else
      {
        Rain.Model.orderconfig orderconfig = new Rain.BLL.orderconfig().loadConfig();
        string order_no = DTRequest.GetString("order_no");
        string str = DTRequest.GetString("edit_type");
        if (order_no == "")
          context.Response.Write("{\"status\": 0, \"msg\": \"传输参数有误，无法获取订单号！\"}");
        else if (str == "")
        {
          context.Response.Write("{\"status\": 0, \"msg\": \"无法获取修改订单类型！\"}");
        }
        else
        {
          Rain.BLL.orders orders = new Rain.BLL.orders();
          Rain.Model.orders model1 = orders.GetModel(order_no);
          if (model1 == null)
          {
            context.Response.Write("{\"status\": 0, \"msg\": \"订单号不存在或已被删除！\"}");
          }
          else
          {
            switch (str.ToLower())
            {
              case "order_confirm":
                if (!new Rain.BLL.manager_role().Exists(adminInfo.role_id, "order_list", DTEnums.ActionEnum.Confirm.ToString()))
                {
                  context.Response.Write("{\"status\": 0, \"msg\": \"您没有确认订单的权限！\"}");
                  break;
                }
                if (model1.status > 1)
                {
                  context.Response.Write("{\"status\": 0, \"msg\": \"订单已经确认，不能重复处理！\"}");
                  break;
                }
                model1.status = 2;
                model1.confirm_time = new DateTime?(DateTime.Now);
                if (!orders.Update(model1))
                {
                  context.Response.Write("{\"status\": 0, \"msg\": \"订单确认失败！\"}");
                  break;
                }
                new Rain.BLL.manager_log().Add(adminInfo.id, adminInfo.user_name, DTEnums.ActionEnum.Confirm.ToString(), "确认订单号:" + model1.order_no);
                if (orderconfig.confirmmsg > 0)
                {
                  switch (orderconfig.confirmmsg)
                  {
                    case 1:
                      if (string.IsNullOrEmpty(model1.mobile))
                      {
                        context.Response.Write("{\"status\": 1, \"msg\": \"订单确认成功，但无法发送短信<br/ >对方未填写手机号码！\"}");
                        return;
                      }
                      Rain.Model.sms_template model2 = new Rain.BLL.sms_template().GetModel(orderconfig.confirmcallindex);
                      if (model2 == null)
                      {
                        context.Response.Write("{\"status\": 1, \"msg\": \"订单确认成功，但无法发送短信<br/ >短信通知模板不存在！\"}");
                        return;
                      }
                      string content1 = model2.content.Replace("{webname}", this.siteConfig.webname).Replace("{username}", model1.accept_name).Replace("{orderno}", model1.order_no).Replace("{amount}", model1.order_amount.ToString());
                      string msg1 = string.Empty;
                      if (!new sms_message().Send(model1.mobile, content1, 2, out msg1))
                      {
                        context.Response.Write("{\"status\": 1, \"msg\": \"订单确认成功，但无法发送短信<br/ >" + msg1 + "\"}");
                        return;
                      }
                      break;
                    case 2:
                      if (string.IsNullOrEmpty(model1.email))
                      {
                        context.Response.Write("{\"status\": 1, \"msg\": \"订单确认成功，但无法发送邮件<br/ >该用户没有填写邮箱地址。\"}");
                        return;
                      }
                      Rain.Model.mail_template model3 = new Rain.BLL.mail_template().GetModel(orderconfig.confirmcallindex);
                      if (model3 == null)
                      {
                        context.Response.Write("{\"status\": 1, \"msg\": \"订单确认成功，但无法发送邮件<br/ >邮件通知模板不存在。\"}");
                        return;
                      }
                      string subj1 = model3.maill_title.Replace("{username}", model1.user_name);
                      string bodys1 = model3.content.Replace("{webname}", this.siteConfig.webname).Replace("{weburl}", this.siteConfig.weburl).Replace("{webtel}", this.siteConfig.webtel).Replace("{username}", model1.user_name).Replace("{orderno}", model1.order_no).Replace("{amount}", model1.order_amount.ToString());
                      DTMail.sendMail(this.siteConfig.emailsmtp, this.siteConfig.emailssl, this.siteConfig.emailusername, this.siteConfig.emailpassword, this.siteConfig.emailnickname, this.siteConfig.emailfrom, model1.email, subj1, bodys1);
                      break;
                  }
                }
                context.Response.Write("{\"status\": 1, \"msg\": \"订单确认成功！\"}");
                break;
              case "order_payment":
                if (!new Rain.BLL.manager_role().Exists(adminInfo.role_id, "order_list", DTEnums.ActionEnum.Confirm.ToString()))
                {
                  context.Response.Write("{\"status\": 0, \"msg\": \"您没有确认付款的权限！\"}");
                  break;
                }
                if (model1.status > 1 || model1.payment_status == 2)
                {
                  context.Response.Write("{\"status\": 0, \"msg\": \"订单已确认，不能重复处理！\"}");
                  break;
                }
                model1.payment_status = 2;
                model1.payment_time = new DateTime?(DateTime.Now);
                model1.status = 2;
                model1.confirm_time = new DateTime?(DateTime.Now);
                if (!orders.Update(model1))
                {
                  context.Response.Write("{\"status\": 0, \"msg\": \"订单确认付款失败！\"}");
                  break;
                }
                new Rain.BLL.manager_log().Add(adminInfo.id, adminInfo.user_name, DTEnums.ActionEnum.Confirm.ToString(), "确认付款订单号:" + model1.order_no);
                if (orderconfig.confirmmsg > 0)
                {
                  switch (orderconfig.confirmmsg)
                  {
                    case 1:
                      if (string.IsNullOrEmpty(model1.mobile))
                      {
                        context.Response.Write("{\"status\": 1, \"msg\": \"订单确认成功，但无法发送短信<br/ >对方未填写手机号码！\"}");
                        return;
                      }
                      Rain.Model.sms_template model4 = new Rain.BLL.sms_template().GetModel(orderconfig.confirmcallindex);
                      if (model4 == null)
                      {
                        context.Response.Write("{\"status\": 1, \"msg\": \"订单确认成功，但无法发送短信<br/ >短信通知模板不存在！\"}");
                        return;
                      }
                      string content2 = model4.content.Replace("{webname}", this.siteConfig.webname).Replace("{username}", model1.user_name).Replace("{orderno}", model1.order_no).Replace("{amount}", model1.order_amount.ToString());
                      string msg2 = string.Empty;
                      if (!new sms_message().Send(model1.mobile, content2, 2, out msg2))
                      {
                        context.Response.Write("{\"status\": 1, \"msg\": \"订单确认成功，但无法发送短信<br/ >" + msg2 + "\"}");
                        return;
                      }
                      break;
                    case 2:
                      if (string.IsNullOrEmpty(model1.email))
                      {
                        context.Response.Write("{\"status\": 1, \"msg\": \"订单确认成功，但无法发送邮件<br/ >该用户没有填写邮箱地址。\"}");
                        return;
                      }
                      Rain.Model.mail_template model5 = new Rain.BLL.mail_template().GetModel(orderconfig.confirmcallindex);
                      if (model5 == null)
                      {
                        context.Response.Write("{\"status\": 1, \"msg\": \"订单确认成功，但无法发送邮件<br/ >邮件通知模板不存在。\"}");
                        return;
                      }
                      string subj2 = model5.maill_title.Replace("{username}", model1.user_name);
                      string bodys2 = model5.content.Replace("{webname}", this.siteConfig.webname).Replace("{weburl}", this.siteConfig.weburl).Replace("{webtel}", this.siteConfig.webtel).Replace("{username}", model1.user_name).Replace("{orderno}", model1.order_no).Replace("{amount}", model1.order_amount.ToString());
                      DTMail.sendMail(this.siteConfig.emailsmtp, this.siteConfig.emailssl, this.siteConfig.emailusername, this.siteConfig.emailpassword, this.siteConfig.emailnickname, this.siteConfig.emailfrom, model1.email, subj2, bodys2);
                      break;
                  }
                }
                context.Response.Write("{\"status\": 1, \"msg\": \"订单确认付款成功！\"}");
                break;
              case "order_express":
                if (!new Rain.BLL.manager_role().Exists(adminInfo.role_id, "order_list", DTEnums.ActionEnum.Confirm.ToString()))
                {
                  context.Response.Write("{\"status\": 0, \"msg\": \"您没有确认发货的权限！\"}");
                  break;
                }
                if (model1.status > 2 || model1.express_status == 2)
                {
                  context.Response.Write("{\"status\": 0, \"msg\": \"订单已完成或已发货，不能重复处理！\"}");
                  break;
                }
                int formInt = DTRequest.GetFormInt("express_id");
                string formString1 = DTRequest.GetFormString("express_no");
                if (formInt == 0)
                {
                  context.Response.Write("{\"status\": 0, \"msg\": \"请选择配送方式！\"}");
                  break;
                }
                model1.express_id = formInt;
                model1.express_no = formString1;
                model1.express_status = 2;
                model1.express_time = new DateTime?(DateTime.Now);
                if (!orders.Update(model1))
                {
                  context.Response.Write("{\"status\": 0, \"msg\": \"订单发货失败！\"}");
                  break;
                }
                new Rain.BLL.manager_log().Add(adminInfo.id, adminInfo.user_name, DTEnums.ActionEnum.Confirm.ToString(), "确认发货订单号:" + model1.order_no);
                if (orderconfig.expressmsg > 0)
                {
                  switch (orderconfig.expressmsg)
                  {
                    case 1:
                      if (string.IsNullOrEmpty(model1.mobile))
                      {
                        context.Response.Write("{\"status\": 1, \"msg\": \"订单确认成功，但无法发送短信<br/ >对方未填写手机号码！\"}");
                        return;
                      }
                      Rain.Model.sms_template model6 = new Rain.BLL.sms_template().GetModel(orderconfig.expresscallindex);
                      if (model6 == null)
                      {
                        context.Response.Write("{\"status\": 1, \"msg\": \"订单确认成功，但无法发送短信<br/ >短信通知模板不存在！\"}");
                        return;
                      }
                      string content3 = model6.content.Replace("{webname}", this.siteConfig.webname).Replace("{username}", model1.user_name).Replace("{orderno}", model1.order_no).Replace("{amount}", model1.order_amount.ToString());
                      string msg3 = string.Empty;
                      if (!new sms_message().Send(model1.mobile, content3, 2, out msg3))
                      {
                        context.Response.Write("{\"status\": 1, \"msg\": \"订单确认成功，但无法发送短信<br/ >" + msg3 + "\"}");
                        return;
                      }
                      break;
                    case 2:
                      if (string.IsNullOrEmpty(model1.email))
                      {
                        context.Response.Write("{\"status\": 1, \"msg\": \"订单确认成功，但无法发送邮件<br/ >该用户没有填写邮箱地址。\"}");
                        return;
                      }
                      Rain.Model.mail_template model7 = new Rain.BLL.mail_template().GetModel(orderconfig.expresscallindex);
                      if (model7 == null)
                      {
                        context.Response.Write("{\"status\": 1, \"msg\": \"订单确认成功，但无法发送邮件<br/ >邮件通知模板不存在。\"}");
                        return;
                      }
                      string subj3 = model7.maill_title.Replace("{username}", model1.user_name);
                      string bodys3 = model7.content.Replace("{webname}", this.siteConfig.webname).Replace("{weburl}", this.siteConfig.weburl).Replace("{webtel}", this.siteConfig.webtel).Replace("{username}", model1.user_name).Replace("{orderno}", model1.order_no).Replace("{amount}", model1.order_amount.ToString());
                      DTMail.sendMail(this.siteConfig.emailsmtp, this.siteConfig.emailssl, this.siteConfig.emailusername, this.siteConfig.emailpassword, this.siteConfig.emailnickname, this.siteConfig.emailfrom, model1.email, subj3, bodys3);
                      break;
                  }
                }
                context.Response.Write("{\"status\": 1, \"msg\": \"订单发货成功！\"}");
                break;
              case "order_complete":
                if (!new Rain.BLL.manager_role().Exists(adminInfo.role_id, "order_list", DTEnums.ActionEnum.Confirm.ToString()))
                {
                  context.Response.Write("{\"status\": 0, \"msg\": \"您没有确认完成订单的权限！\"}");
                  break;
                }
                if (model1.status > 2)
                {
                  context.Response.Write("{\"status\": 0, \"msg\": \"订单已经完成，不能重复处理！\"}");
                  break;
                }
                model1.status = 3;
                model1.complete_time = new DateTime?(DateTime.Now);
                if (!orders.Update(model1))
                {
                  context.Response.Write("{\"status\": 0, \"msg\": \"确认订单完成失败！\"}");
                  break;
                }
                if (model1.user_id > 0 && model1.point > 0)
                  new Rain.BLL.user_point_log().Add(model1.user_id, model1.user_name, model1.point, "购物获得积分，订单号：" + model1.order_no, true);
                new Rain.BLL.manager_log().Add(adminInfo.id, adminInfo.user_name, DTEnums.ActionEnum.Confirm.ToString(), "确认交易完成订单号:" + model1.order_no);
                if (orderconfig.completemsg > 0)
                {
                  switch (orderconfig.completemsg)
                  {
                    case 1:
                      if (string.IsNullOrEmpty(model1.mobile))
                      {
                        context.Response.Write("{\"status\": 1, \"msg\": \"订单确认成功，但无法发送短信<br/ >对方未填写手机号码！\"}");
                        return;
                      }
                      Rain.Model.sms_template model8 = new Rain.BLL.sms_template().GetModel(orderconfig.completecallindex);
                      if (model8 == null)
                      {
                        context.Response.Write("{\"status\": 1, \"msg\": \"订单确认成功，但无法发送短信<br/ >短信通知模板不存在！\"}");
                        return;
                      }
                      string content4 = model8.content.Replace("{webname}", this.siteConfig.webname).Replace("{username}", model1.user_name).Replace("{orderno}", model1.order_no).Replace("{amount}", model1.order_amount.ToString());
                      string msg4 = string.Empty;
                      if (!new sms_message().Send(model1.mobile, content4, 2, out msg4))
                      {
                        context.Response.Write("{\"status\": 1, \"msg\": \"订单确认成功，但无法发送短信<br/ >" + msg4 + "\"}");
                        return;
                      }
                      break;
                    case 2:
                      if (string.IsNullOrEmpty(model1.email))
                      {
                        context.Response.Write("{\"status\": 1, \"msg\": \"订单确认成功，但无法发送邮件<br/ >该用户没有填写邮箱地址。\"}");
                        return;
                      }
                      Rain.Model.mail_template model9 = new Rain.BLL.mail_template().GetModel(orderconfig.completecallindex);
                      if (model9 == null)
                      {
                        context.Response.Write("{\"status\": 1, \"msg\": \"订单确认成功，但无法发送邮件<br/ >邮件通知模板不存在。\"}");
                        return;
                      }
                      string subj4 = model9.maill_title.Replace("{username}", model1.user_name);
                      string bodys4 = model9.content.Replace("{webname}", this.siteConfig.webname).Replace("{weburl}", this.siteConfig.weburl).Replace("{webtel}", this.siteConfig.webtel).Replace("{username}", model1.user_name).Replace("{orderno}", model1.order_no).Replace("{amount}", model1.order_amount.ToString());
                      DTMail.sendMail(this.siteConfig.emailsmtp, this.siteConfig.emailssl, this.siteConfig.emailusername, this.siteConfig.emailpassword, this.siteConfig.emailnickname, this.siteConfig.emailfrom, model1.email, subj4, bodys4);
                      break;
                  }
                }
                context.Response.Write("{\"status\": 1, \"msg\": \"确认订单完成成功！\"}");
                break;
              case "order_cancel":
                if (!new Rain.BLL.manager_role().Exists(adminInfo.role_id, "order_list", DTEnums.ActionEnum.Cancel.ToString()))
                {
                  context.Response.Write("{\"status\": 0, \"msg\": \"您没有取消订单的权限！\"}");
                  break;
                }
                if (model1.status > 2)
                {
                  context.Response.Write("{\"status\": 0, \"msg\": \"订单已经完成，不能取消订单！\"}");
                  break;
                }
                model1.status = 4;
                if (!orders.Update(model1))
                {
                  context.Response.Write("{\"status\": 0, \"msg\": \"取消订单失败！\"}");
                  break;
                }
                if (DTRequest.GetFormInt("check_revert") == 1)
                {
                  if (model1.user_id > 0 && model1.point < 0)
                    new Rain.BLL.user_point_log().Add(model1.user_id, model1.user_name, model1.point * -1, "取消订单返还积分，订单号：" + model1.order_no, false);
                  if (model1.user_id > 0 && model1.payment_status == 2 && model1.order_amount > new Decimal(0))
                    new Rain.BLL.user_amount_log().Add(model1.user_id, model1.user_name, model1.order_amount, "取消订单退还金额，订单号：" + model1.order_no);
                }
                new Rain.BLL.manager_log().Add(adminInfo.id, adminInfo.user_name, DTEnums.ActionEnum.Cancel.ToString(), "取消订单号:" + model1.order_no);
                context.Response.Write("{\"status\": 1, \"msg\": \"取消订单成功！\"}");
                break;
              case "order_invalid":
                if (!new Rain.BLL.manager_role().Exists(adminInfo.role_id, "order_list", DTEnums.ActionEnum.Invalid.ToString()))
                {
                  context.Response.Write("{\"status\": 0, \"msg\": \"您没有作废订单的权限！\"}");
                  break;
                }
                if (model1.status != 3)
                {
                  context.Response.Write("{\"status\": 0, \"msg\": \"订单尚未完成，不能作废订单！\"}");
                  break;
                }
                model1.status = 5;
                if (!orders.Update(model1))
                {
                  context.Response.Write("{\"status\": 0, \"msg\": \"作废订单失败！\"}");
                  break;
                }
                if (DTRequest.GetFormInt("check_revert") == 1)
                {
                  if (model1.user_id > 0 && model1.point > 0)
                    new Rain.BLL.user_point_log().Add(model1.user_id, model1.user_name, model1.point * -1, "作废订单扣除积分，订单号：" + model1.order_no, false);
                  if (model1.user_id > 0 && model1.order_amount > new Decimal(0))
                    new Rain.BLL.user_amount_log().Add(model1.user_id, model1.user_name, model1.order_amount - model1.express_fee, "取消订单退还金额，订单号：" + model1.order_no);
                }
                new Rain.BLL.manager_log().Add(adminInfo.id, adminInfo.user_name, DTEnums.ActionEnum.Invalid.ToString(), "作废订单号:" + model1.order_no);
                context.Response.Write("{\"status\": 1, \"msg\": \"作废订单成功！\"}");
                break;
              case "edit_accept_info":
                if (!new Rain.BLL.manager_role().Exists(adminInfo.role_id, "order_list", DTEnums.ActionEnum.Edit.ToString()))
                {
                  context.Response.Write("{\"status\": 0, \"msg\": \"您没有修改收货信息的权限！\"}");
                  break;
                }
                if (model1.express_status == 2)
                {
                  context.Response.Write("{\"status\": 0, \"msg\": \"订单已经发货，不能修改收货信息！\"}");
                  break;
                }
                string formString2 = DTRequest.GetFormString("accept_name");
                string formString3 = DTRequest.GetFormString("province");
                string formString4 = DTRequest.GetFormString("city");
                string formString5 = DTRequest.GetFormString("area");
                string formString6 = DTRequest.GetFormString("address");
                string formString7 = DTRequest.GetFormString("post_code");
                string formString8 = DTRequest.GetFormString("mobile");
                string formString9 = DTRequest.GetFormString("telphone");
                string formString10 = DTRequest.GetFormString("email");
                if (formString2 == "")
                {
                  context.Response.Write("{\"status\": 0, \"msg\": \"请填写收货人姓名！\"}");
                  break;
                }
                if (formString5 == "")
                {
                  context.Response.Write("{\"status\": 0, \"msg\": \"请选择所在地区！\"}");
                  break;
                }
                if (formString6 == "")
                {
                  context.Response.Write("{\"status\": 0, \"msg\": \"请填写详细的送货地址！\"}");
                  break;
                }
                if (formString8 == "" && formString9 == "")
                {
                  context.Response.Write("{\"status\": 0, \"msg\": \"联系手机或电话至少填写一项！\"}");
                  break;
                }
                model1.accept_name = formString2;
                model1.area = formString3 + "," + formString4 + "," + formString5;
                model1.address = formString6;
                model1.post_code = formString7;
                model1.mobile = formString8;
                model1.telphone = formString9;
                model1.email = formString10;
                if (!orders.Update(model1))
                {
                  context.Response.Write("{\"status\": 0, \"msg\": \"修改收货人信息失败！\"}");
                  break;
                }
                new Rain.BLL.manager_log().Add(adminInfo.id, adminInfo.user_name, DTEnums.ActionEnum.Edit.ToString(), "修改收货信息，订单号:" + model1.order_no);
                context.Response.Write("{\"status\": 1, \"msg\": \"修改收货人信息成功！\"}");
                break;
              case "edit_order_remark":
                string formString11 = DTRequest.GetFormString("remark");
                if (formString11 == "")
                {
                  context.Response.Write("{\"status\": 0, \"msg\": \"请填写订单备注内容！\"}");
                  break;
                }
                model1.remark = formString11;
                if (!orders.Update(model1))
                {
                  context.Response.Write("{\"status\": 0, \"msg\": \"修改订单备注失败！\"}");
                  break;
                }
                new Rain.BLL.manager_log().Add(adminInfo.id, adminInfo.user_name, DTEnums.ActionEnum.Edit.ToString(), "修改订单备注，订单号:" + model1.order_no);
                context.Response.Write("{\"status\": 1, \"msg\": \"修改订单备注成功！\"}");
                break;
              case "edit_real_amount":
                if (!new Rain.BLL.manager_role().Exists(adminInfo.role_id, "order_list", DTEnums.ActionEnum.Edit.ToString()))
                {
                  context.Response.Write("{\"status\": 0, \"msg\": \"您没有修改商品金额的权限！\"}");
                  break;
                }
                if (model1.status > 1)
                {
                  context.Response.Write("{\"status\": 0, \"msg\": \"订单已经确认，不能修改金额！\"}");
                  break;
                }
                Decimal formDecimal1 = DTRequest.GetFormDecimal("real_amount", new Decimal(0));
                model1.real_amount = formDecimal1;
                if (!orders.Update(model1))
                {
                  context.Response.Write("{\"status\": 0, \"msg\": \"修改商品总金额失败！\"}");
                  break;
                }
                new Rain.BLL.manager_log().Add(adminInfo.id, adminInfo.user_name, DTEnums.ActionEnum.Edit.ToString(), "修改商品金额，订单号:" + model1.order_no);
                context.Response.Write("{\"status\": 1, \"msg\": \"修改商品总金额成功！\"}");
                break;
              case "edit_express_fee":
                if (!new Rain.BLL.manager_role().Exists(adminInfo.role_id, "order_list", DTEnums.ActionEnum.Edit.ToString()))
                {
                  context.Response.Write("{\"status\": 0, \"msg\": \"您没有配送费用的权限！\"}");
                  break;
                }
                if (model1.status > 1)
                {
                  context.Response.Write("{\"status\": 0, \"msg\": \"订单已经确认，不能修改金额！\"}");
                  break;
                }
                Decimal formDecimal2 = DTRequest.GetFormDecimal("express_fee", new Decimal(0));
                model1.express_fee = formDecimal2;
                if (!orders.Update(model1))
                {
                  context.Response.Write("{\"status\": 0, \"msg\": \"修改配送费用失败！\"}");
                  break;
                }
                new Rain.BLL.manager_log().Add(adminInfo.id, adminInfo.user_name, DTEnums.ActionEnum.Edit.ToString(), "修改配送费用，订单号:" + model1.order_no);
                context.Response.Write("{\"status\": 1, \"msg\": \"修改配送费用成功！\"}");
                break;
              case "edit_payment_fee":
                if (!new Rain.BLL.manager_role().Exists(adminInfo.role_id, "order_list", DTEnums.ActionEnum.Edit.ToString()))
                {
                  context.Response.Write("{\"status\": 0, \"msg\": \"您没有修改支付手续费的权限！\"}");
                  break;
                }
                if (model1.status > 1)
                {
                  context.Response.Write("{\"status\": 0, \"msg\": \"订单已经确认，不能修改金额！\"}");
                  break;
                }
                Decimal formDecimal3 = DTRequest.GetFormDecimal("payment_fee", new Decimal(0));
                model1.payment_fee = formDecimal3;
                if (!orders.Update(model1))
                {
                  context.Response.Write("{\"status\": 0, \"msg\": \"修改支付手续费失败！\"}");
                  break;
                }
                new Rain.BLL.manager_log().Add(adminInfo.id, adminInfo.user_name, DTEnums.ActionEnum.Edit.ToString(), "修改支付手续费，订单号:" + model1.order_no);
                context.Response.Write("{\"status\": 1, \"msg\": \"修改支付手续费成功！\"}");
                break;
              case "edit_invoice_taxes":
                if (!new Rain.BLL.manager_role().Exists(adminInfo.role_id, "order_list", DTEnums.ActionEnum.Edit.ToString()))
                {
                  context.Response.Write("{\"status\": 0, \"msg\": \"您没有修改发票税金的权限！\"}");
                  break;
                }
                if (model1.status > 1)
                {
                  context.Response.Write("{\"status\": 0, \"msg\": \"订单已经确认，不能修改金额！\"}");
                  break;
                }
                Decimal formDecimal4 = DTRequest.GetFormDecimal("invoice_taxes", new Decimal(0));
                model1.invoice_taxes = formDecimal4;
                if (!orders.Update(model1))
                {
                  context.Response.Write("{\"status\": 0, \"msg\": \"修改订单发票税金失败！\"}");
                  break;
                }
                new Rain.BLL.manager_log().Add(adminInfo.id, adminInfo.user_name, DTEnums.ActionEnum.Edit.ToString(), "修改订单发票税金，订单号:" + model1.order_no);
                context.Response.Write("{\"status\": 1, \"msg\": \"修改发票税金成功！\"}");
                break;
            }
          }
        }
      }
    }

    private void get_builder_urls(HttpContext context)
    {
      int builderStatus = this.get_builder_status();
      if (builderStatus == 1)
        new HtmlBuilder().getpublishsite(context);
      else
        context.Response.Write((object) builderStatus);
    }

    private void get_builder_html(HttpContext context)
    {
      int builderStatus = this.get_builder_status();
      if (builderStatus == 1)
        new HtmlBuilder().handleHtml(context);
      else
        context.Response.Write((object) builderStatus);
    }

    private int get_builder_status()
    {
      Rain.Model.manager adminInfo = new ManagePage().GetAdminInfo();
      if (adminInfo == null)
        return -1;
      if (!new Rain.BLL.manager_role().Exists(adminInfo.role_id, "sys_builder_html", DTEnums.ActionEnum.Build.ToString()))
        return -2;
      return this.siteConfig.staticstatus != 2 ? -3 : 1;
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
