// Decompiled with JetBrains decompiler
// Type: Rain.Web.tools.submit_ajax
// Assembly: Rain.Web, Version=4.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 7C74D3EC-D223-4A13-9304-5BE7CB5803E5
// Assembly location: C:\Users\Felix.Ho\Desktop\page\bin\Rain.Web.dll

using Microsoft.JScript;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Web;
using System.Web.SessionState;
using Rain.BLL;
using Rain.Common;
using Rain.Model;
using Rain.Web.UI;

namespace Rain.Web.tools
{
  public class submit_ajax : IHttpHandler, IRequiresSessionState
  {
    private Rain.Model.siteconfig siteConfig = new Rain.BLL.siteconfig().loadConfig();
    private Rain.Model.userconfig userConfig = new Rain.BLL.userconfig().loadConfig();

    public void ProcessRequest(HttpContext context)
    {
      switch (DTRequest.GetQueryString("action"))
      {
        case "comment_add":
          this.comment_add(context);
          break;
        case "comment_list":
          this.comment_list(context);
          break;
        case "validate_username":
          this.validate_username(context);
          break;
        case "user_login":
          this.user_login(context);
          break;
        case "user_check_login":
          this.user_check_login(context);
          break;
        case "user_oauth_bind":
          this.user_oauth_bind(context);
          break;
        case "user_oauth_register":
          this.user_oauth_register(context);
          break;
        case "user_register":
          this.user_register(context);
          break;
        case "user_verify_smscode":
          this.user_verify_smscode(context);
          break;
        case "user_verify_email":
          this.user_verify_email(context);
          break;
        case "user_info_edit":
          this.user_info_edit(context);
          break;
        case "user_avatar_crop":
          this.user_avatar_crop(context);
          break;
        case "user_password_edit":
          this.user_password_edit(context);
          break;
        case "user_getpassword":
          this.user_getpassword(context);
          break;
        case "user_repassword":
          this.user_repassword(context);
          break;
        case "user_invite_code":
          this.user_invite_code(context);
          break;
        case "user_point_convert":
          this.user_point_convert(context);
          break;
        case "user_amount_recharge":
          this.user_amount_recharge(context);
          break;
        case "user_message_add":
          this.user_message_add(context);
          break;
        case "user_point_delete":
          this.user_point_delete(context);
          break;
        case "user_amount_delete":
          this.user_amount_delete(context);
          break;
        case "user_recharge_delete":
          this.user_recharge_delete(context);
          break;
        case "user_message_delete":
          this.user_message_delete(context);
          break;
        case "cart_goods_add":
          this.cart_goods_add(context);
          break;
        case "cart_goods_buy":
          this.cart_goods_buy(context);
          break;
        case "cart_goods_update":
          this.cart_goods_update(context);
          break;
        case "cart_goods_delete":
          this.cart_goods_delete(context);
          break;
        case "order_save":
          this.order_save(context);
          break;
        case "order_cancel":
          this.order_cancel(context);
          break;
        case "view_article_click":
          this.view_article_click(context);
          break;
        case "view_comment_count":
          this.view_comment_count(context);
          break;
        case "view_attach_count":
          this.view_attach_count(context);
          break;
        case "view_cart_count":
          this.view_cart_count(context);
          break;
      }
    }

    private void comment_add(HttpContext context)
    {
      StringBuilder stringBuilder = new StringBuilder();
      Rain.BLL.article_comment articleComment = new Rain.BLL.article_comment();
      Rain.Model.article_comment model1 = new Rain.Model.article_comment();
      string formString1 = DTRequest.GetFormString("txtCode");
      int queryInt = DTRequest.GetQueryInt("article_id");
      string formString2 = DTRequest.GetFormString("txtContent");
      string s = this.verify_code(context, formString1);
      if (s != "success")
        context.Response.Write(s);
      else if (queryInt == 0)
        context.Response.Write("{\"status\": 0, \"msg\": \"对不起，参数传输有误！\"}");
      else if (string.IsNullOrEmpty(formString2))
      {
        context.Response.Write("{\"status\": 0, \"msg\": \"对不起，请输入评论的内容！\"}");
      }
      else
      {
        Rain.Model.article model2 = new Rain.BLL.article().GetModel(queryInt);
        if (model2 == null)
        {
          context.Response.Write("{\"status\": 0, \"msg\": \"对不起，主题不存在或已删除！\"}");
        }
        else
        {
          int num = 0;
          string str = "匿名用户";
          Rain.Model.users userInfo = new BasePage().GetUserInfo();
          if (userInfo != null)
          {
            num = userInfo.id;
            str = userInfo.user_name;
          }
          model1.channel_id = model2.channel_id;
          model1.article_id = model2.id;
          model1.content = Utils.ToHtml(formString2);
          model1.user_id = num;
          model1.user_name = str;
          model1.user_ip = DTRequest.GetIP();
          model1.is_lock = this.siteConfig.commentstatus;
          model1.add_time = DateTime.Now;
          model1.is_reply = 0;
          if (articleComment.Add(model1) > 0)
            context.Response.Write("{\"status\": 1, \"msg\": \"恭喜您，留言提交成功！\"}");
          else
            context.Response.Write("{\"status\": 0, \"msg\": \"对不起，保存过程中发生错误！\"}");
        }
      }
    }

    private void comment_list(HttpContext context)
    {
      int queryInt1 = DTRequest.GetQueryInt("article_id");
      int queryInt2 = DTRequest.GetQueryInt("page_index");
      int queryInt3 = DTRequest.GetQueryInt("page_size");
      StringBuilder stringBuilder = new StringBuilder();
      if (queryInt1 == 0 || queryInt3 == 0)
      {
        context.Response.Write("获取失败，传输参数有误！");
      }
      else
      {
        int recordCount;
        DataSet list = new Rain.BLL.article_comment().GetList(queryInt3, queryInt2, string.Format("is_lock=0 and article_id={0}", (object) queryInt1.ToString()), "add_time asc", out recordCount);
        if (list.Tables[0].Rows.Count > 0)
        {
          stringBuilder.Append("[");
          for (int index = 0; index < list.Tables[0].Rows.Count; ++index)
          {
            DataRow row = list.Tables[0].Rows[index];
            stringBuilder.Append("{");
            stringBuilder.Append("\"user_id\":" + row["user_id"]);
            stringBuilder.Append(",\"user_name\":\"" + row["user_name"] + "\"");
            if (System.Convert.ToInt32(row["user_id"]) > 0)
            {
              Rain.Model.users model = new Rain.BLL.users().GetModel(System.Convert.ToInt32(row["user_id"]));
              if (model != null)
                stringBuilder.Append(",\"avatar\":\"" + model.avatar + "\"");
            }
            stringBuilder.Append("");
            stringBuilder.Append(",\"content\":\"" + GlobalObject.escape(row["content"]) + "\"");
            stringBuilder.Append(",\"add_time\":\"" + row["add_time"] + "\"");
            stringBuilder.Append(",\"is_reply\":" + row["is_reply"]);
            if (System.Convert.ToInt32(row["is_reply"]) == 1)
            {
              stringBuilder.Append(",\"reply_content\":\"" + GlobalObject.escape(row["reply_content"]) + "\"");
              stringBuilder.Append(",\"reply_time\":\"" + row["reply_time"] + "\"");
            }
            stringBuilder.Append("}");
            if (index < list.Tables[0].Rows.Count - 1)
              stringBuilder.Append(",");
          }
          stringBuilder.Append("]");
        }
        context.Response.Write(stringBuilder.ToString());
      }
    }

    private void validate_username(HttpContext context)
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

    private void user_login(HttpContext context)
    {
      string queryString = DTRequest.GetQueryString("site");
      string formString1 = DTRequest.GetFormString("txtUserName");
      string formString2 = DTRequest.GetFormString("txtPassword");
      string formString3 = DTRequest.GetFormString("chkRemember");
      if (string.IsNullOrEmpty(queryString))
        context.Response.Write("{\"status\": 0, \"msg\": \"错误提示：站点传输参数不正确！\"}");
      else if (string.IsNullOrEmpty(formString1) || string.IsNullOrEmpty(formString2))
      {
        context.Response.Write("{\"status\": 0, \"msg\": \"温馨提示：请输入用户名或密码！\"}");
      }
      else
      {
        Rain.BLL.users users = new Rain.BLL.users();
        Rain.Model.users model = users.GetModel(formString1, formString2, this.userConfig.emaillogin, this.userConfig.mobilelogin, true);
        if (model == null)
          context.Response.Write("{\"status\":0, \"msg\":\"错误提示：用户名或密码错误，请重试！\"}");
        else if (model.status == 1)
        {
          if (this.userConfig.regverify == 1)
            context.Response.Write("{\"status\":1, \"url\":\"" + new BasePage().getlink(queryString, new BasePage().linkurl("register", (object) ("?action=sendmail&username=" + Utils.UrlEncode(model.user_name)))) + "\", \"msg\":\"会员尚未通过验证！\"}");
          else
            context.Response.Write("{\"status\":1, \"url\":\"" + new BasePage().getlink(queryString, new BasePage().linkurl("register", (object) ("?action=sendsms&username=" + Utils.UrlEncode(model.user_name)))) + "\", \"msg\":\"会员尚未通过验证！\"}");
        }
        else if (model.status == 2)
        {
          context.Response.Write("{\"status\":1, \"url\":\"" + new BasePage().getlink(queryString, new BasePage().linkurl("register", (object) ("?action=verify&username=" + Utils.UrlEncode(model.user_name)))) + "\", \"msg\":\"会员尚未通过审核！\"}");
        }
        else
        {
          if (!new Rain.BLL.user_login_log().ExistsDay(model.user_name) && this.userConfig.pointloginnum > 0)
          {
            new Rain.BLL.user_point_log().Add(model.id, model.user_name, this.userConfig.pointloginnum, "每天登录获得积分", true);
            model = users.GetModel(formString1, formString2, this.userConfig.emaillogin, this.userConfig.mobilelogin, true);
          }
          context.Session["dt_session_user_info"] = (object) model;
          context.Session.Timeout = 45;
          if (formString3.ToLower() == "true")
          {
            Utils.WriteCookie("dt_cookie_user_name_remember", "Rain", model.user_name, 43200);
            Utils.WriteCookie("dt_cookie_user_pwd_remember", "Rain", model.password, 43200);
          }
          else
          {
            Utils.WriteCookie("dt_cookie_user_name_remember", "Rain", model.user_name);
            Utils.WriteCookie("dt_cookie_user_pwd_remember", "Rain", model.password);
          }
          new Rain.BLL.user_login_log().Add(model.id, model.user_name, "会员登录");
          context.Response.Write("{\"status\":1, \"msg\":\"会员登录成功！\"}");
        }
      }
    }

    private void user_check_login(HttpContext context)
    {
      Rain.Model.users userInfo = new BasePage().GetUserInfo();
      if (userInfo == null)
        context.Response.Write("{\"status\":0, \"username\":\"匿名用户\"}");
      else
        context.Response.Write("{\"status\":1, \"username\":\"" + userInfo.user_name + "\"}");
    }

    private void user_oauth_bind(HttpContext context)
    {
      if (context.Session["oauth_name"] == null)
      {
        context.Response.Write("{\"status\": 0, \"msg\": \"错误提示：授权参数不正确！\"}");
      }
      else
      {
        string jsonText = Utils.UrlExecute(this.siteConfig.webpath + "api/oauth/" + context.Session["oauth_name"].ToString() + "/result_json.aspx");
        if (jsonText.Contains("error"))
        {
          context.Response.Write("{\"status\": 0, \"msg\": \"错误提示：请检查URL是否正确！\"}");
        }
        else
        {
          Dictionary<string, object> dictionary = JsonHelper.DataRowFromJSON(jsonText);
          if (dictionary["ret"].ToString() != "0")
          {
            context.Response.Write("{\"status\": 0, \"msg\": \"错误代码：" + dictionary["ret"] + "，描述：" + dictionary["msg"] + "\"}");
          }
          else
          {
            string user_name = DTRequest.GetString("txtUserName");
            string password = DTRequest.GetString("txtPassword");
            if (string.IsNullOrEmpty(user_name) || string.IsNullOrEmpty(password))
            {
              context.Response.Write("{\"status\": 0, \"msg\": \"温馨提示：请输入用户名或密码！\"}");
            }
            else
            {
              Rain.Model.users model = new Rain.BLL.users().GetModel(user_name, password, this.userConfig.emaillogin, this.userConfig.mobilelogin, true);
              if (model == null)
                context.Response.Write("{\"status\":0, \"msg\":\"错误提示：用户名或密码错误！\"}");
              else if (new Rain.BLL.user_oauth().Add(new Rain.Model.user_oauth()
              {
                oauth_name = dictionary["oauth_name"].ToString(),
                user_id = model.id,
                user_name = model.user_name,
                oauth_access_token = dictionary["oauth_access_token"].ToString(),
                oauth_openid = dictionary["oauth_openid"].ToString()
              }) < 1)
              {
                context.Response.Write("{\"status\":0, \"msg\":\"错误提示：绑定过程中出错，请重新获取！\"}");
              }
              else
              {
                context.Session["dt_session_user_info"] = (object) model;
                context.Session.Timeout = 45;
                Utils.WriteCookie("dt_cookie_user_name_remember", "Rain", model.user_name);
                Utils.WriteCookie("dt_cookie_user_pwd_remember", "Rain", model.password);
                new Rain.BLL.user_login_log().Add(model.id, model.user_name, "会员登录");
                context.Response.Write("{\"status\":1, \"msg\":\"会员登录成功！\"}");
              }
            }
          }
        }
      }
    }

    private void user_oauth_register(HttpContext context)
    {
      if (context.Session["oauth_name"] == null)
      {
        context.Response.Write("{\"status\": 0, \"msg\": \"错误提示：授权参数不正确！\"}");
      }
      else
      {
        string jsonText = Utils.UrlExecute(this.siteConfig.webpath + "api/oauth/" + context.Session["oauth_name"].ToString() + "/result_json.aspx");
        if (jsonText.Contains("error"))
        {
          context.Response.Write("{\"status\": 0, \"msg\": \"错误提示：请检查URL是否正确！\"}");
        }
        else
        {
          string Text = DTRequest.GetFormString("txtPassword").Trim();
          string html1 = Utils.ToHtml(DTRequest.GetFormString("txtEmail").Trim());
          string html2 = Utils.ToHtml(DTRequest.GetFormString("txtMobile").Trim());
          string ip = DTRequest.GetIP();
          Dictionary<string, object> dictionary = JsonHelper.DataRowFromJSON(jsonText);
          if (dictionary["ret"].ToString() != "0")
          {
            context.Response.Write("{\"status\": 0, \"msg\": \"错误代码：" + dictionary["ret"] + "，" + dictionary["msg"] + "\"}");
          }
          else
          {
            Rain.BLL.users users = new Rain.BLL.users();
            Rain.Model.users model1 = new Rain.Model.users();
            if (this.userConfig.mobilelogin == 1 && !string.IsNullOrEmpty(html2) && users.ExistsMobile(html2))
              context.Response.Write("{\"status\":0, \"msg\":\"对不起，该手机号码已被使用！\"}");
            else if (this.userConfig.emaillogin == 1 && !string.IsNullOrEmpty(html1) && users.ExistsEmail(html1))
            {
              context.Response.Write("{\"status\":0, \"msg\":\"对不起，该电子邮箱已被使用！\"}");
            }
            else
            {
              Rain.Model.user_groups userGroups = new Rain.BLL.user_groups().GetDefault();
              if (userGroups == null)
              {
                context.Response.Write("{\"status\":0, \"msg\":\"用户尚未分组，请联系管理员！\"}");
              }
              else
              {
                model1.group_id = userGroups.id;
                model1.user_name = users.GetRandomName(10);
                model1.salt = Utils.GetCheckCode(6);
                model1.password = DESEncrypt.Encrypt(Text, model1.salt);
                model1.email = html1;
                model1.mobile = html2;
                if (!string.IsNullOrEmpty(dictionary["nick"].ToString()))
                  model1.nick_name = dictionary["nick"].ToString();
                if (dictionary["avatar"].ToString().StartsWith("http://"))
                  model1.avatar = dictionary["avatar"].ToString();
                if (!string.IsNullOrEmpty(dictionary["sex"].ToString()))
                  model1.sex = dictionary["sex"].ToString();
                if (!string.IsNullOrEmpty(dictionary["birthday"].ToString()))
                  model1.birthday = new DateTime?(Utils.StrToDateTime(dictionary["birthday"].ToString()));
                model1.reg_ip = ip;
                model1.reg_time = DateTime.Now;
                model1.status = 0;
                model1.id = users.Add(model1);
                if (model1.id < 1)
                {
                  context.Response.Write("{\"status\":0, \"msg\":\"注册失败，请联系网站管理员！\"}");
                }
                else
                {
                  if (userGroups.point > 0)
                    new Rain.BLL.user_point_log().Add(model1.id, model1.user_name, userGroups.point, "注册赠送积分", false);
                  if (userGroups.amount > new Decimal(0))
                    new Rain.BLL.user_amount_log().Add(model1.id, model1.user_name, userGroups.amount, "注册赠送金额");
                  if (this.userConfig.regmsgstatus == 1)
                    new Rain.BLL.user_message().Add(1, "", model1.user_name, "欢迎您成为本站会员", this.userConfig.regmsgtxt);
                  else if (this.userConfig.regmsgstatus == 2)
                  {
                    Rain.Model.mail_template model2 = new Rain.BLL.mail_template().GetModel("welcomemsg");
                    if (model2 != null)
                    {
                      string subj = model2.maill_title.Replace("{username}", model1.user_name);
                      string bodys = model2.content.Replace("{webname}", this.siteConfig.webname).Replace("{weburl}", this.siteConfig.weburl).Replace("{webtel}", this.siteConfig.webtel).Replace("{username}", model1.user_name);
                      DTMail.sendMail(this.siteConfig.emailsmtp, this.siteConfig.emailssl, this.siteConfig.emailusername, this.siteConfig.emailpassword, this.siteConfig.emailnickname, this.siteConfig.emailfrom, model1.email, subj, bodys);
                    }
                  }
                  else if (this.userConfig.regmsgstatus == 3 && html2 != "")
                  {
                    Rain.Model.sms_template model2 = new Rain.BLL.sms_template().GetModel("welcomemsg");
                    if (model2 != null)
                    {
                      string content = model2.content.Replace("{webname}", this.siteConfig.webname).Replace("{weburl}", this.siteConfig.weburl).Replace("{webtel}", this.siteConfig.webtel).Replace("{username}", model1.user_name);
                      string msg = string.Empty;
                      new sms_message().Send(model1.mobile, content, 2, out msg);
                    }
                  }
                  new Rain.BLL.user_oauth().Add(new Rain.Model.user_oauth()
                  {
                    oauth_name = dictionary["oauth_name"].ToString(),
                    user_id = model1.id,
                    user_name = model1.user_name,
                    oauth_access_token = dictionary["oauth_access_token"].ToString(),
                    oauth_openid = dictionary["oauth_openid"].ToString()
                  });
                  context.Session["dt_session_user_info"] = (object) model1;
                  context.Session.Timeout = 45;
                  Utils.WriteCookie("dt_cookie_user_name_remember", "Rain", model1.user_name);
                  Utils.WriteCookie("dt_cookie_user_pwd_remember", "Rain", model1.password);
                  new Rain.BLL.user_login_log().Add(model1.id, model1.user_name, "会员登录");
                  context.Response.Write("{\"status\":1, \"msg\":\"会员登录成功！\"}");
                }
              }
            }
          }
        }
      }
    }

    private void user_register(HttpContext context)
    {
      string str1 = DTRequest.GetQueryString("site").Trim();
      string str2 = DTRequest.GetFormString("txtCode").Trim();
      string html1 = Utils.ToHtml(DTRequest.GetFormString("txtUserName").Trim());
      string Text = DTRequest.GetFormString("txtPassword").Trim();
      string html2 = Utils.ToHtml(DTRequest.GetFormString("txtEmail").Trim());
      string html3 = Utils.ToHtml(DTRequest.GetFormString("txtMobile").Trim());
      string ip = DTRequest.GetIP();
      if (string.IsNullOrEmpty(str1))
        context.Response.Write("{\"status\":0, \"msg\":\"对不起，网站传输参数有误！\"}");
      else if (this.siteConfig.memberstatus == 0)
        context.Response.Write("{\"status\":0, \"msg\":\"对不起，会员功能已关闭，无法注册！\"}");
      else if (this.userConfig.regstatus == 0)
        context.Response.Write("{\"status\":0, \"msg\":\"对不起，系统暂不允许注册新用户！\"}");
      else if (string.IsNullOrEmpty(html1) || string.IsNullOrEmpty(Text))
        context.Response.Write("{\"status\":0, \"msg\":\"对不起，用户名和密码不能为空！\"}");
      else if (this.userConfig.regstatus == 2 && string.IsNullOrEmpty(html3))
        context.Response.Write("{\"status\":0, \"msg\":\"错误：手机号码不能为空！\"}");
      else if (this.userConfig.regstatus == 3 && string.IsNullOrEmpty(html2))
      {
        context.Response.Write("{\"status\":0, \"msg\":\"对不起，电子邮箱不能为空！\"}");
      }
      else
      {
        Rain.BLL.users users1 = new Rain.BLL.users();
        if (users1.Exists(html1))
          context.Response.Write("{\"status\":0, \"msg\":\"对不起，该用户名已经存在！\"}");
        else if (this.userConfig.mobilelogin == 1 && !string.IsNullOrEmpty(html3) && users1.ExistsMobile(html3))
          context.Response.Write("{\"status\":0, \"msg\":\"对不起，该手机号码已被使用！\"}");
        else if (this.userConfig.emaillogin == 1 && !string.IsNullOrEmpty(html2) && users1.ExistsEmail(html2))
          context.Response.Write("{\"status\":0, \"msg\":\"对不起，该电子邮箱已被使用！\"}");
        else if (this.userConfig.regctrl > 0 && users1.Exists(ip, this.userConfig.regctrl))
        {
          context.Response.Write("{\"status\":0, \"msg\":\"对不起，同IP在" + (object) this.userConfig.regctrl + "小时内禁止重复注册！\"}");
        }
        else
        {
          Rain.Model.user_groups userGroups = new Rain.BLL.user_groups().GetDefault();
          if (userGroups == null)
          {
            context.Response.Write("{\"status\":0, \"msg\":\"用户尚未分组，请联系网站管理员！\"}");
          }
          else
          {
            switch (this.userConfig.regstatus)
            {
              case 1:
                string s1 = this.verify_code(context, str2);
                if (s1 != "success")
                {
                  context.Response.Write(s1);
                  return;
                }
                break;
              case 2:
                string s2 = this.verify_sms_code(context, str2);
                if (s2 != "success")
                {
                  context.Response.Write(s2);
                  return;
                }
                break;
              case 4:
                string s3 = this.verify_invite_reg(html1, str2);
                if (s3 != "success")
                {
                  context.Response.Write(s3);
                  return;
                }
                break;
            }
            Rain.Model.users users2 = new Rain.Model.users()
            {
              group_id = userGroups.id,
              user_name = html1,
              salt = Utils.GetCheckCode(6)
            };
            users2.password = DESEncrypt.Encrypt(Text, users2.salt);
            users2.email = html2;
            users2.mobile = html3;
            users2.reg_ip = ip;
            users2.reg_time = DateTime.Now;
            users2.status = this.userConfig.regstatus != 3 ? (this.userConfig.regverify != 1 ? 0 : 2) : 1;
            users2.id = users1.Add(users2);
            if (users2.id < 1)
            {
              context.Response.Write("{\"status\":0, \"msg\":\"系统故障，请联系网站管理员！\"}");
            }
            else
            {
              if (userGroups.point > 0)
                new Rain.BLL.user_point_log().Add(users2.id, users2.user_name, userGroups.point, "注册赠送积分", false);
              if (userGroups.amount > new Decimal(0))
                new Rain.BLL.user_amount_log().Add(users2.id, users2.user_name, userGroups.amount, "注册赠送金额");
              if (this.userConfig.regmsgstatus == 1)
                new Rain.BLL.user_message().Add(1, string.Empty, users2.user_name, "欢迎您成为本站会员", this.userConfig.regmsgtxt);
              else if (this.userConfig.regmsgstatus == 2 && !string.IsNullOrEmpty(html2))
              {
                Rain.Model.mail_template model = new Rain.BLL.mail_template().GetModel("welcomemsg");
                if (model != null)
                {
                  string subj = model.maill_title.Replace("{username}", users2.user_name);
                  string bodys = model.content.Replace("{webname}", this.siteConfig.webname).Replace("{weburl}", this.siteConfig.weburl).Replace("{webtel}", this.siteConfig.webtel).Replace("{username}", users2.user_name);
                  DTMail.sendMail(this.siteConfig.emailsmtp, this.siteConfig.emailssl, this.siteConfig.emailusername, this.siteConfig.emailpassword, this.siteConfig.emailnickname, this.siteConfig.emailfrom, users2.email, subj, bodys);
                }
              }
              else if (this.userConfig.regmsgstatus == 3 && !string.IsNullOrEmpty(html3))
              {
                Rain.Model.sms_template model = new Rain.BLL.sms_template().GetModel("welcomemsg");
                if (model != null)
                {
                  string content = model.content.Replace("{webname}", this.siteConfig.webname).Replace("{weburl}", this.siteConfig.weburl).Replace("{webtel}", this.siteConfig.webtel).Replace("{username}", users2.user_name);
                  string msg = string.Empty;
                  new sms_message().Send(users2.mobile, content, 2, out msg);
                }
              }
              if (this.userConfig.regstatus == 3)
              {
                string s4 = this.send_verify_email(str1, users2);
                if (s4 != "success")
                  context.Response.Write(s4);
                else
                  context.Response.Write("{\"status\":1, \"msg\":\"注册成功，请进入邮箱验证激活账户！\", \"url\":\"" + new BasePage().getlink(str1, new BasePage().linkurl("register", (object) ("?action=sendmail&username=" + Utils.UrlEncode(users2.user_name)))) + "\"}");
              }
              else if (this.userConfig.regverify == 1)
              {
                context.Response.Write("{\"status\":1, \"msg\":\"注册成功，请等待审核通过！\", \"url\":\"" + new BasePage().getlink(str1, new BasePage().linkurl("register", (object) ("?action=verify&username=" + Utils.UrlEncode(users2.user_name)))) + "\"}");
              }
              else
              {
                context.Session["dt_session_user_info"] = (object) users2;
                context.Session.Timeout = 45;
                Utils.WriteCookie("dt_cookie_user_name_remember", "Rain", users2.user_name);
                Utils.WriteCookie("dt_cookie_user_pwd_remember", "Rain", users2.password);
                new Rain.BLL.user_login_log().Add(users2.id, users2.user_name, "会员登录");
                context.Response.Write("{\"status\":1, \"msg\":\"注册成功，欢迎成为本站会员！\", \"url\":\"" + new BasePage().getlink(str1, new BasePage().linkurl("usercenter", (object) "index")) + "\"}");
              }
            }
          }
        }
      }
    }

    private void user_verify_smscode(HttpContext context)
    {
      string html = Utils.ToHtml(DTRequest.GetString("mobile"));
      if (string.IsNullOrEmpty(html))
        context.Response.Write("{\"status\":0, \"msg\":\"发送失败，请填写手机号码！\"}");
      string s = this.send_verify_sms_code(context, html);
      if (s != "success")
        context.Response.Write(s);
      else
        context.Response.Write("{\"status\":1, \"time\":\"" + (object) this.userConfig.regsmsexpired + "\", \"msg\":\"手机验证码发送成功！\"}");
    }

    private void user_verify_email(HttpContext context)
    {
      string site = DTRequest.GetString("site");
      string html = Utils.ToHtml(DTRequest.GetFormString("username"));
      if (string.IsNullOrEmpty(site))
        context.Response.Write("{\"status\":0, \"msg\":\"网站传输参数有误！\"}");
      else if (string.IsNullOrEmpty(html))
        context.Response.Write("{\"status\":0, \"msg\":\"请检查用户名是否正确！\"}");
      else if (Utils.GetCookie("dt_cookie_user_email") == html)
      {
        context.Response.Write("{\"status\":0, \"msg\":\"邮件发送间隔为二十分钟，请稍候再提交吧！\"}");
      }
      else
      {
        Rain.Model.users model = new Rain.BLL.users().GetModel(html);
        if (model == null)
          context.Response.Write("{\"status\":0, \"msg\":\"该用户不存在或已删除！\"}");
        else if (model.status != 1)
        {
          context.Response.Write("{\"status\":0, \"msg\":\"该用户不需要邮箱验证！\"}");
        }
        else
        {
          string s = this.send_verify_email(site, model);
          if (s != "success")
          {
            context.Response.Write(s);
          }
          else
          {
            context.Response.Write("{\"status\":1, \"msg\":\"邮件已发送，请进入邮箱查看！\"}");
            Utils.WriteCookie("dt_cookie_user_email", html, 20);
          }
        }
      }
    }

    private void user_info_edit(HttpContext context)
    {
      Rain.Model.users userInfo = new BasePage().GetUserInfo();
      if (userInfo == null)
      {
        context.Response.Write("{\"status\":0, \"msg\":\"对不起，用户尚未登录或已超时！\"}");
      }
      else
      {
        string html1 = Utils.ToHtml(DTRequest.GetFormString("txtNickName"));
        string formString1 = DTRequest.GetFormString("rblSex");
        string formString2 = DTRequest.GetFormString("txtBirthday");
        string html2 = Utils.ToHtml(DTRequest.GetFormString("txtEmail"));
        string html3 = Utils.ToHtml(DTRequest.GetFormString("txtMobile"));
        string html4 = Utils.ToHtml(DTRequest.GetFormString("txtTelphone"));
        string html5 = Utils.ToHtml(DTRequest.GetFormString("txtQQ"));
        string html6 = Utils.ToHtml(DTRequest.GetFormString("txtMsn"));
        string html7 = Utils.ToHtml(DTRequest.GetFormString("txtProvince"));
        string html8 = Utils.ToHtml(DTRequest.GetFormString("txtCity"));
        string html9 = Utils.ToHtml(DTRequest.GetFormString("txtArea"));
        string html10 = Utils.ToHtml(context.Request.Form["txtAddress"]);
        if (string.IsNullOrEmpty(html1))
          context.Response.Write("{\"status\":0, \"msg\":\"对不起，请输入您的姓名昵称！\"}");
        else if (string.IsNullOrEmpty(html7) || string.IsNullOrEmpty(html8) || string.IsNullOrEmpty(html9))
        {
          context.Response.Write("{\"status\":0, \"msg\":\"对不起，请选择您所在的省市区！\"}");
        }
        else
        {
          Rain.BLL.users users = new Rain.BLL.users();
          if (this.userConfig.regstatus == 2 || this.userConfig.mobilelogin == 1)
          {
            if (string.IsNullOrEmpty(html3))
            {
              context.Response.Write("{\"status\":0, \"msg\":\"对不起，请输入您的手机号码！\"}");
              return;
            }
            if (userInfo.mobile != html3 && users.ExistsMobile(html3))
            {
              context.Response.Write("{\"status\":0, \"msg\":\"对不起，该手机号码已被使用！\"}");
              return;
            }
          }
          if (this.userConfig.regstatus == 3 || this.userConfig.emaillogin == 1)
          {
            if (string.IsNullOrEmpty(html2))
            {
              context.Response.Write("{\"status\":0, \"msg\":\"对不起，请输入您的电子邮箱！\"}");
              return;
            }
            if (userInfo.email != html2 && users.ExistsEmail(html2))
            {
              context.Response.Write("{\"status\":0, \"msg\":\"对不起，该电子邮箱已被使用！\"}");
              return;
            }
          }
          userInfo.nick_name = html1;
          userInfo.sex = formString1;
          DateTime result;
          if (DateTime.TryParse(formString2, out result))
            userInfo.birthday = new DateTime?(result);
          userInfo.email = html2;
          userInfo.mobile = html3;
          userInfo.telphone = html4;
          userInfo.qq = html5;
          userInfo.msn = html6;
          userInfo.area = html7 + "," + html8 + "," + html9;
          userInfo.address = html10;
          users.Update(userInfo);
          context.Response.Write("{\"status\":1, \"msg\":\"账户资料已修改成功！\"}");
        }
      }
    }

    private void user_avatar_crop(HttpContext context)
    {
      Rain.Model.users userInfo = new BasePage().GetUserInfo();
      if (userInfo == null)
      {
        context.Response.Write("{\"status\":0, \"msg\":\"对不起，用户尚未登录或已超时！\"}");
      }
      else
      {
        string formString = DTRequest.GetFormString("hideFileName");
        int formInt1 = DTRequest.GetFormInt("hideX1");
        int formInt2 = DTRequest.GetFormInt("hideY1");
        int formInt3 = DTRequest.GetFormInt("hideWidth");
        int formInt4 = DTRequest.GetFormInt("hideHeight");
        if (!Utils.FileExists(formString) || formInt3 == 0 || formInt4 == 0)
          context.Response.Write("{\"status\":0, \"msg\":\"对不起，请先上传一张图片！\"}");
        else if (!new UpLoad().cropSaveAs(formString, formString, 180, 180, formInt3, formInt4, formInt1, formInt2))
        {
          context.Response.Write("{\"status\": 0, \"msg\": \"图片裁剪过程中发生意外错误！\"}");
        }
        else
        {
          Utils.DeleteFile(userInfo.avatar);
          userInfo.avatar = formString;
          new Rain.BLL.users().UpdateField(userInfo.id, "avatar='" + userInfo.avatar + "'");
          context.Response.Write("{\"status\": 1, \"msg\": \"头像上传成功！\", \"avatar\": \"" + userInfo.avatar + "\"}");
        }
      }
    }

    private void user_password_edit(HttpContext context)
    {
      Rain.Model.users userInfo = new BasePage().GetUserInfo();
      if (userInfo == null)
      {
        context.Response.Write("{\"status\":0, \"msg\":\"对不起，用户尚未登录或已超时！\"}");
      }
      else
      {
        int id = userInfo.id;
        string formString1 = DTRequest.GetFormString("txtOldPassword");
        string formString2 = DTRequest.GetFormString("txtPassword");
        if (string.IsNullOrEmpty(formString1))
          context.Response.Write("{\"status\":0, \"msg\":\"请输入您的旧登录密码！\"}");
        else if (string.IsNullOrEmpty(formString2))
          context.Response.Write("{\"status\":0, \"msg\":\"请输入您的新登录密码！\"}");
        else if (userInfo.password != DESEncrypt.Encrypt(formString1, userInfo.salt))
        {
          context.Response.Write("{\"status\":0, \"msg\":\"对不起，您输入的旧密码不正确！\"}");
        }
        else
        {
          userInfo.password = DESEncrypt.Encrypt(formString2, userInfo.salt);
          new Rain.BLL.users().Update(userInfo);
          context.Response.Write("{\"status\":1, \"msg\":\"您的密码已修改成功，请记住新密码！\"}");
        }
      }
    }

    private void user_getpassword(HttpContext context)
    {
      string queryString = DTRequest.GetQueryString("site");
      string formString1 = DTRequest.GetFormString("txtCode");
      string formString2 = DTRequest.GetFormString("txtType");
      string user_name = DTRequest.GetFormString("txtUserName").Trim();
      if (string.IsNullOrEmpty(queryString))
        context.Response.Write("{\"status\":0, \"msg\":\"对不起，网站传输参数有误！\"}");
      else if (string.IsNullOrEmpty(user_name))
        context.Response.Write("{\"status\":0, \"msg\":\"对不起，用户名不可为空！\"}");
      else if (string.IsNullOrEmpty(formString2))
      {
        context.Response.Write("{\"status\":0, \"msg\":\"对不起，请选择取回密码类型！\"}");
      }
      else
      {
        string s = this.verify_code(context, formString1);
        if (s != "success")
        {
          context.Response.Write(s);
        }
        else
        {
          Rain.Model.users model1 = new Rain.BLL.users().GetModel(user_name);
          if (model1 == null)
            context.Response.Write("{\"status\":0, \"msg\":\"对不起，您输入的用户名不存在！\"}");
          else if (formString2.ToLower() == "mobile")
          {
            if (string.IsNullOrEmpty(model1.mobile))
            {
              context.Response.Write("{\"status\":0, \"msg\":\"您尚未绑定手机号码，无法取回密码！\"}");
            }
            else
            {
              Rain.Model.sms_template model2 = new Rain.BLL.sms_template().GetModel("usercode");
              if (model2 == null)
                context.Response.Write("{\"status\":0, \"msg\":\"发送失败，短信模板不存在，请联系管理员！\"}");
              string str = Utils.Number(4);
              Rain.BLL.user_code userCode = new Rain.BLL.user_code();
              Rain.Model.user_code model3 = userCode.GetModel(user_name, DTEnums.CodeEnum.RegVerify.ToString(), "d");
              if (model3 == null)
              {
                model3 = new Rain.Model.user_code();
                model3.user_id = model1.id;
                model3.user_name = model1.user_name;
                model3.type = DTEnums.CodeEnum.Password.ToString();
                model3.str_code = str;
                model3.eff_time = DateTime.Now.AddMinutes((double) this.userConfig.regsmsexpired);
                model3.add_time = DateTime.Now;
                userCode.Add(model3);
              }
              string content = model2.content.Replace("{webname}", this.siteConfig.webname).Replace("{weburl}", this.siteConfig.weburl).Replace("{webtel}", this.siteConfig.webtel).Replace("{code}", model3.str_code).Replace("{valid}", this.userConfig.regsmsexpired.ToString());
              string msg = string.Empty;
              if (!new sms_message().Send(model1.mobile, content, 1, out msg))
                context.Response.Write("{\"status\":0, \"msg\":\"发送失败，" + msg + "\"}");
              else
                context.Response.Write("{\"status\":1, \"msg\":\"手机验证码发送成功！\", \"url\":\"" + new BasePage().getlink(queryString, new BasePage().linkurl("repassword", (object) ("?action=mobile&username=" + Utils.UrlEncode(model1.user_name)))) + "\"}");
            }
          }
          else if (formString2.ToLower() == "email")
          {
            if (string.IsNullOrEmpty(model1.email))
            {
              context.Response.Write("{\"status\":0, \"msg\":\"您尚未绑定邮箱，无法取回密码！\"}");
            }
            else
            {
              string checkCode = Utils.GetCheckCode(20);
              Rain.Model.mail_template model2 = new Rain.BLL.mail_template().GetModel("getpassword");
              if (model2 == null)
              {
                context.Response.Write("{\"status\":0, \"msg\":\"邮件发送失败，邮件模板内容不存在！\"}");
              }
              else
              {
                Rain.BLL.user_code userCode = new Rain.BLL.user_code();
                Rain.Model.user_code model3 = userCode.GetModel(user_name, DTEnums.CodeEnum.RegVerify.ToString(), "d");
                if (model3 == null)
                {
                  model3 = new Rain.Model.user_code();
                  model3.user_id = model1.id;
                  model3.user_name = model1.user_name;
                  model3.type = DTEnums.CodeEnum.Password.ToString();
                  model3.str_code = checkCode;
                  model3.eff_time = DateTime.Now.AddDays((double) this.userConfig.regemailexpired);
                  model3.add_time = DateTime.Now;
                  userCode.Add(model3);
                }
                string maillTitle = model2.maill_title;
                string content = model2.content;
                string subj = maillTitle.Replace("{webname}", this.siteConfig.webname).Replace("{username}", model1.user_name);
                string bodys = content.Replace("{webname}", this.siteConfig.webname).Replace("{weburl}", this.siteConfig.weburl).Replace("{webtel}", this.siteConfig.webtel).Replace("{valid}", this.userConfig.regemailexpired.ToString()).Replace("{username}", model1.user_name).Replace("{linkurl}", "http://" + HttpContext.Current.Request.Url.Authority.ToLower() + new BasePage().getlink(queryString, new BasePage().linkurl("repassword", (object) ("?action=email&code=" + model3.str_code))));
                try
                {
                  DTMail.sendMail(this.siteConfig.emailsmtp, this.siteConfig.emailssl, this.siteConfig.emailusername, DESEncrypt.Decrypt(this.siteConfig.emailpassword), this.siteConfig.emailnickname, this.siteConfig.emailfrom, model1.email, subj, bodys);
                }
                catch
                {
                  context.Response.Write("{\"status\":0, \"msg\":\"邮件发送失败，请联系本站管理员！\"}");
                  return;
                }
                context.Response.Write("{\"status\":1, \"msg\":\"邮件发送成功，请登录邮箱查看邮件！\"}");
              }
            }
          }
          else
            context.Response.Write("{\"status\":0, \"msg\":\"发生未知错误，请检查参数是否正确！\"}");
        }
      }
    }

    private void user_repassword(HttpContext context)
    {
      string formString1 = DTRequest.GetFormString("hideCode");
      string formString2 = DTRequest.GetFormString("txtPassword");
      if (string.IsNullOrEmpty(formString1))
        context.Response.Write("{\"status\":0, \"msg\":\"对不起，校检码字符串不能为空！\"}");
      else if (string.IsNullOrEmpty(formString2))
      {
        context.Response.Write("{\"status\":0, \"msg\":\"对不起，请输入您的新密码！\"}");
      }
      else
      {
        Rain.BLL.user_code userCode = new Rain.BLL.user_code();
        Rain.Model.user_code model1 = userCode.GetModel(formString1);
        if (model1 == null)
        {
          context.Response.Write("{\"status\":0, \"msg\":\"对不起，校检码不存在或已过期！\"}");
        }
        else
        {
          Rain.BLL.users users = new Rain.BLL.users();
          if (!users.Exists(model1.user_id))
          {
            context.Response.Write("{\"status\":0, \"msg\":\"对不起，该用户不存在或已被删除！\"}");
          }
          else
          {
            Rain.Model.users model2 = users.GetModel(model1.user_id);
            model2.password = DESEncrypt.Encrypt(formString2, model2.salt);
            users.Update(model2);
            model1.count = 1;
            model1.status = 1;
            userCode.Update(model1);
            context.Response.Write("{\"status\":1, \"msg\":\"修改密码成功，请记住新密码！\"}");
          }
        }
      }
    }

    private void user_invite_code(HttpContext context)
    {
      Rain.Model.users userInfo = new BasePage().GetUserInfo();
      if (userInfo == null)
        context.Response.Write("{\"status\":0, \"msg\":\"对不起，用户尚未登录或已超时！\"}");
      else if (this.userConfig.regstatus != 4)
      {
        context.Response.Write("{\"status\":0, \"msg\":\"对不起，系统不允许通过邀请注册！\"}");
      }
      else
      {
        Rain.BLL.user_code userCode = new Rain.BLL.user_code();
        if (this.userConfig.invitecodenum > 0)
        {
          if (userCode.GetCount("user_name='" + userInfo.user_name + "' and type='" + DTEnums.CodeEnum.Register.ToString() + "' and datediff('d',add_time,date())=0") >= this.userConfig.invitecodenum)
          {
            context.Response.Write("{\"status\":0, \"msg\":\"对不起，您申请邀请码的数量已超过每天限制！\"}");
            return;
          }
        }
        userCode.Delete("type='" + DTEnums.CodeEnum.Register.ToString() + "' and status=1 or datediff('d',eff_time,date())>0");
        string checkCode = Utils.GetCheckCode(8);
        userCode.Add(new Rain.Model.user_code()
        {
          user_id = userInfo.id,
          user_name = userInfo.user_name,
          type = DTEnums.CodeEnum.Register.ToString(),
          str_code = checkCode,
          user_ip = DTRequest.GetIP(),
          eff_time = this.userConfig.invitecodeexpired <= 0 ? DateTime.Now.AddDays(1.0) : DateTime.Now.AddDays((double) this.userConfig.invitecodeexpired)
        });
        context.Response.Write("{\"status\":1, \"msg\":\"恭喜您，申请邀请码已成功！\"}");
      }
    }

    private void user_point_convert(HttpContext context)
    {
      if (this.userConfig.pointcashrate == new Decimal(0))
      {
        context.Response.Write("{\"status\":0, \"msg\":\"对不起，网站未开启兑换积分功能！\"}");
      }
      else
      {
        Rain.Model.users userInfo = new BasePage().GetUserInfo();
        if (userInfo == null)
        {
          context.Response.Write("{\"status\":0, \"msg\":\"对不起，用户尚未登录或已超时！\"}");
        }
        else
        {
          int formInt = DTRequest.GetFormInt("txtAmount");
          string formString = DTRequest.GetFormString("txtPassword");
          if (userInfo.amount < new Decimal(1))
            context.Response.Write("{\"status\":0, \"msg\":\"对不起，您账户上的余额不足！\"}");
          else if (formInt < 1)
            context.Response.Write("{\"status\":0, \"msg\":\"对不起，最小兑换金额为1元！\"}");
          else if ((Decimal) formInt > userInfo.amount)
            context.Response.Write("{\"status\":0, \"msg\":\"对不起，您兑换的金额大于账户余额！\"}");
          else if (formString == "")
            context.Response.Write("{\"status\":0, \"msg\":\"对不起，请输入您账户的密码！\"}");
          else if (DESEncrypt.Encrypt(formString, userInfo.salt) != userInfo.password)
          {
            context.Response.Write("{\"status\":0, \"msg\":\"对不起，您输入的密码不正确！\"}");
          }
          else
          {
            int num = (int) (System.Convert.ToDecimal(formInt) * this.userConfig.pointcashrate);
            if (new Rain.BLL.user_amount_log().Add(userInfo.id, userInfo.user_name, (Decimal) (formInt * -1), "用户兑换积分") < 1)
              context.Response.Write("{\"status\":0, \"msg\":\"转换过程中发生错误，请重新提交！\"}");
            else if (new Rain.BLL.user_point_log().Add(userInfo.id, userInfo.user_name, num, "用户兑换积分", true) < 1)
            {
              new Rain.BLL.user_amount_log().Add(userInfo.id, userInfo.user_name, (Decimal) formInt, "用户兑换积分失败，返还金额");
              context.Response.Write("{\"status\":0, \"msg\":\"转换过程中发生错误，请重新提交！\"}");
            }
            else
              context.Response.Write("{\"status\":1, \"msg\":\"恭喜您，积分兑换成功！\"}");
          }
        }
      }
    }

    private void user_amount_recharge(HttpContext context)
    {
      Rain.Model.users userInfo = new BasePage().GetUserInfo();
      if (userInfo == null)
      {
        context.Response.Write("{\"status\":0, \"msg\":\"对不起，用户尚未登录或已超时！\"}");
      }
      else
      {
        string queryString = DTRequest.GetQueryString("site");
        Decimal formDecimal = DTRequest.GetFormDecimal("order_amount", new Decimal(0));
        int formInt = DTRequest.GetFormInt("payment_id");
        if (string.IsNullOrEmpty(queryString))
          context.Response.Write("{\"status\": 0, \"msg\": \"错误提示：站点传输参数不正确！\"}");
        else if (formDecimal == new Decimal(0))
          context.Response.Write("{\"status\":0, \"msg\":\"对不起，请输入正确的充值金额！\"}");
        else if (formInt == 0)
          context.Response.Write("{\"status\":0, \"msg\":\"对不起，请选择正确的支付方式！\"}");
        else if (!new Rain.BLL.payment().Exists(formInt))
        {
          context.Response.Write("{\"status\":0, \"msg\":\"对不起，支付方式不存在或已删除！\"}");
        }
        else
        {
          string recharge_no = "R" + Utils.GetOrderNumber();
          new Rain.BLL.user_recharge().Add(userInfo.id, userInfo.user_name, recharge_no, formInt, formDecimal);
          context.Response.Write("{\"status\":1, \"msg\":\"订单保存成功！\", \"url\":\"" + new BasePage().getlink(queryString, new BasePage().linkurl("payment", (object) ("?action=confirm&order_no=" + recharge_no))) + "\"}");
        }
      }
    }

    private void user_message_add(HttpContext context)
    {
      Rain.Model.users userInfo = new BasePage().GetUserInfo();
      if (userInfo == null)
      {
        context.Response.Write("{\"status\":0, \"msg\":\"对不起，用户尚未登录或已超时！\"}");
      }
      else
      {
        string strcode = context.Request.Form["txtCode"];
        string formString = DTRequest.GetFormString("sendSave");
        string html1 = Utils.ToHtml(DTRequest.GetFormString("txtUserName"));
        string html2 = Utils.ToHtml(DTRequest.GetFormString("txtTitle"));
        string html3 = Utils.ToHtml(DTRequest.GetFormString("txtContent"));
        string s = this.verify_code(context, strcode);
        if (s != "success")
          context.Response.Write(s);
        else if (string.IsNullOrEmpty(html1) || !new Rain.BLL.users().Exists(html1))
          context.Response.Write("{\"status\":0, \"msg\":\"对不起，该用户不存在或已删除！\"}");
        else if (string.IsNullOrEmpty(html2))
          context.Response.Write("{\"status\":0, \"msg\":\"对不起，请输入短消息标题！\"}");
        else if (string.IsNullOrEmpty(html3))
        {
          context.Response.Write("{\"status\":0, \"msg\":\"对不起，请输入短消息内容！\"}");
        }
        else
        {
          Rain.Model.user_message model = new Rain.Model.user_message();
          model.type = 2;
          model.post_user_name = userInfo.user_name;
          model.accept_user_name = html1;
          model.title = html2;
          model.content = Utils.ToHtml(html3);
          new Rain.BLL.user_message().Add(model);
          if (formString == "true")
          {
            model.type = 3;
            new Rain.BLL.user_message().Add(model);
          }
          context.Response.Write("{\"status\":1, \"msg\":\"发布短信息成功！\"}");
        }
      }
    }

    private void user_point_delete(HttpContext context)
    {
      Rain.Model.users userInfo = new BasePage().GetUserInfo();
      if (userInfo == null)
      {
        context.Response.Write("{\"status\":0, \"msg\":\"对不起，用户尚未登录或已超时！\"}");
      }
      else
      {
        string formString = DTRequest.GetFormString("checkId");
        if (string.IsNullOrEmpty(formString))
        {
          context.Response.Write("{\"status\":0, \"msg\":\"删除失败，请检查传输参数！\"}");
        }
        else
        {
          string str = formString;
          char[] chArray = new char[1]{ ',' };
          foreach (string expression in str.Split(chArray))
          {
            int id = Utils.StrToInt(expression, 0);
            if (id > 0)
              new Rain.BLL.user_point_log().Delete(id, userInfo.user_name);
          }
          context.Response.Write("{\"status\":1, \"msg\":\"积分明细删除成功！\"}");
        }
      }
    }

    private void user_amount_delete(HttpContext context)
    {
      Rain.Model.users userInfo = new BasePage().GetUserInfo();
      if (userInfo == null)
      {
        context.Response.Write("{\"status\":0, \"msg\":\"对不起，用户尚未登录或已超时！\"}");
      }
      else
      {
        string formString = DTRequest.GetFormString("checkId");
        if (string.IsNullOrEmpty(formString))
        {
          context.Response.Write("{\"status\":0, \"msg\":\"删除失败，请检查传输参数！\"}");
        }
        else
        {
          string str = formString;
          char[] chArray = new char[1]{ ',' };
          foreach (string expression in str.Split(chArray))
          {
            int id = Utils.StrToInt(expression, 0);
            if (id > 0)
              new Rain.BLL.user_amount_log().Delete(id, userInfo.user_name);
          }
          context.Response.Write("{\"status\":1, \"msg\":\"收支明细删除成功！\"}");
        }
      }
    }

    private void user_recharge_delete(HttpContext context)
    {
      Rain.Model.users userInfo = new BasePage().GetUserInfo();
      if (userInfo == null)
      {
        context.Response.Write("{\"status\":0, \"msg\":\"对不起，用户尚未登录或已超时！\"}");
      }
      else
      {
        string formString = DTRequest.GetFormString("checkId");
        if (string.IsNullOrEmpty(formString))
        {
          context.Response.Write("{\"status\":0, \"msg\":\"删除失败，请检查传输参数！\"}");
        }
        else
        {
          string str = formString;
          char[] chArray = new char[1]{ ',' };
          foreach (string expression in str.Split(chArray))
          {
            int id = Utils.StrToInt(expression, 0);
            if (id > 0)
              new Rain.BLL.user_recharge().Delete(id, userInfo.user_name);
          }
          context.Response.Write("{\"status\":1, \"msg\":\"充值记录删除成功！\"}");
        }
      }
    }

    private void user_message_delete(HttpContext context)
    {
      Rain.Model.users userInfo = new BasePage().GetUserInfo();
      if (userInfo == null)
      {
        context.Response.Write("{\"status\":0, \"msg\":\"对不起，用户尚未登录或已超时！\"}");
      }
      else
      {
        string formString = DTRequest.GetFormString("checkId");
        if (string.IsNullOrEmpty(formString))
        {
          context.Response.Write("{\"status\":0, \"msg\":\"删除失败，请检查传输参数！\"}");
        }
        else
        {
          string str = formString;
          char[] chArray = new char[1]{ ',' };
          foreach (string expression in str.Split(chArray))
          {
            int id = Utils.StrToInt(expression, 0);
            if (id > 0)
              new Rain.BLL.user_message().Delete(id, userInfo.user_name);
          }
          context.Response.Write("{\"status\":1, \"msg\":\"删除短消息成功！\"}");
        }
      }
    }

    private void cart_goods_add(HttpContext context)
    {
      int formInt1 = DTRequest.GetFormInt("article_id", 0);
      int formInt2 = DTRequest.GetFormInt("goods_id", 0);
      int formInt3 = DTRequest.GetFormInt("quantity", 1);
      if (formInt1 == 0)
      {
        context.Response.Write("{\"status\":0, \"msg\":\"您提交的商品参数有误！\"}");
      }
      else
      {
        int group_id = 0;
        Rain.Model.users userInfo = new BasePage().GetUserInfo();
        if (userInfo != null)
          group_id = userInfo.group_id;
        ShopCart.Add(formInt1, formInt2, formInt3);
        cart_total total = ShopCart.GetTotal(group_id);
        context.Response.Write("{\"status\":1, \"msg\":\"商品已成功添加到购物车！\", \"quantity\":" + (object) total.total_quantity + ", \"amount\":" + (object) total.real_amount + "}");
      }
    }

    private void cart_goods_buy(HttpContext context)
    {
      string formString = DTRequest.GetFormString("jsondata");
      if (string.IsNullOrEmpty(formString))
        context.Response.Write("{\"status\":0, \"msg\":\"商品传输参数不正确！\"}");
      else if (JsonHelper.JSONToObject<List<cart_keys>>(formString) != null)
      {
        Utils.WriteCookie("dt_cookie_shopping_buy", formString);
        context.Response.Write("{\"status\":1, \"msg\":\"商品已成功加入购物清单！\"}");
      }
      else
        context.Response.Write("{\"status\":0, \"msg\":\"商品数据传输不正确！\"}");
    }

    private void cart_goods_update(HttpContext context)
    {
      int formInt1 = DTRequest.GetFormInt("article_id", 0);
      DTRequest.GetFormInt("goods_id", 0);
      int formInt2 = DTRequest.GetFormInt("quantity", 0);
      if (formInt1 == 0)
        context.Response.Write("{\"status\":0, \"msg\":\"您提交的商品参数有误！\"}");
      else if (formInt2 == 0)
      {
        context.Response.Write("{\"status\":0, \"msg\":\"购买数量不能小于1！\"}");
      }
      else
      {
        cart_keys cartKeys = ShopCart.Update(formInt1, formInt2);
        if (cartKeys != null)
          context.Response.Write("{\"status\":1, \"msg\":\"商品数量修改成功！\", \"article_id\":" + (object) cartKeys.article_id + ", \"quantity\":" + (object) cartKeys.quantity + "}");
        else
          context.Response.Write("{\"status\":0, \"msg\":\"更新失败，请检查操作是否有误！\"}");
      }
    }

    private void cart_goods_delete(HttpContext context)
    {
      int formInt1 = DTRequest.GetFormInt("clear", 0);
      int formInt2 = DTRequest.GetFormInt("article_id", 0);
      int formInt3 = DTRequest.GetFormInt("goods_id", 0);
      if (formInt1 == 1)
      {
        ShopCart.Clear();
        context.Response.Write("{\"status\":1, \"msg\":\"购物车清空成功！\"}");
      }
      else if (formInt2 == 0)
      {
        context.Response.Write("{\"status\":0, \"msg\":\"您提交的商品参数有误！\"}");
      }
      else
      {
        ShopCart.Clear(formInt2, formInt3);
        context.Response.Write("{\"status\":1, \"msg\":\"商品移除成功！\"}");
      }
    }

    private void order_save(HttpContext context)
    {
      string cookie = Utils.GetCookie("dt_cookie_shopping_buy");
      string queryString = DTRequest.GetQueryString("site");
      int formInt1 = DTRequest.GetFormInt("payment_id");
      int formInt2 = DTRequest.GetFormInt("express_id");
      int formInt3 = DTRequest.GetFormInt("is_invoice", 0);
      string html1 = Utils.ToHtml(DTRequest.GetFormString("accept_name"));
      string html2 = Utils.ToHtml(DTRequest.GetFormString("province"));
      string html3 = Utils.ToHtml(DTRequest.GetFormString("city"));
      string html4 = Utils.ToHtml(DTRequest.GetFormString("area"));
      string html5 = Utils.ToHtml(DTRequest.GetFormString("address"));
      string html6 = Utils.ToHtml(DTRequest.GetFormString("telphone"));
      string html7 = Utils.ToHtml(DTRequest.GetFormString("mobile"));
      string html8 = Utils.ToHtml(DTRequest.GetFormString("email"));
      string html9 = Utils.ToHtml(DTRequest.GetFormString("post_code"));
      string html10 = Utils.ToHtml(DTRequest.GetFormString("message"));
      string html11 = Utils.ToHtml(DTRequest.GetFormString("invoice_title"));
      Rain.Model.orderconfig orderconfig = new Rain.BLL.orderconfig().loadConfig();
      if (string.IsNullOrEmpty(cookie))
        context.Response.Write("{\"status\":0, \"msg\":\"对不起，无法获取商品信息！\"}");
      else if (string.IsNullOrEmpty(queryString))
        context.Response.Write("{\"status\": 0, \"msg\": \"错误提示：站点传输参数不正确！\"}");
      else if (formInt2 == 0)
        context.Response.Write("{\"status\":0, \"msg\":\"对不起，请选择配送方式！\"}");
      else if (formInt1 == 0)
      {
        context.Response.Write("{\"status\":0, \"msg\":\"对不起，请选择支付方式！\"}");
      }
      else
      {
        Rain.Model.express model1 = new Rain.BLL.express().GetModel(formInt2);
        if (model1 == null)
        {
          context.Response.Write("{\"status\":0, \"msg\":\"对不起，配送方式不存在或已删除！\"}");
        }
        else
        {
          Rain.Model.payment model2 = new Rain.BLL.payment().GetModel(formInt1);
          if (model2 == null)
            context.Response.Write("{\"status\":0, \"msg\":\"对不起，支付方式不存在或已删除！\"}");
          else if (string.IsNullOrEmpty(html1))
            context.Response.Write("{\"status\":0, \"msg\":\"对不起，请输入收货人姓名！\"}");
          else if (string.IsNullOrEmpty(html6) && string.IsNullOrEmpty(html7))
            context.Response.Write("{\"status\":0, \"msg\":\"对不起，请输入收货人联系电话或手机！\"}");
          else if (string.IsNullOrEmpty(html2) && string.IsNullOrEmpty(html3))
            context.Response.Write("{\"status\":0, \"msg\":\"对不起，请选择您所在的省市区！\"}");
          else if (string.IsNullOrEmpty(html5))
          {
            context.Response.Write("{\"status\":0, \"msg\":\"对不起，请输入详细的收货地址！\"}");
          }
          else
          {
            int num1 = 0;
            int group_id = 0;
            string str = string.Empty;
            Rain.Model.users userInfo = new BasePage().GetUserInfo();
            if (userInfo != null)
            {
              num1 = userInfo.id;
              group_id = userInfo.group_id;
              str = userInfo.user_name;
            }
            if (orderconfig.anonymous == 0 && userInfo == null)
            {
              context.Response.Write("{\"status\":0, \"msg\":\"对不起，用户尚未登录或已超时！\"}");
            }
            else
            {
              List<cart_keys> ls = JsonHelper.JSONToObject<List<cart_keys>>(cookie);
              List<cart_items> list = ShopCart.ToList(ls, group_id);
              cart_total total = ShopCart.GetTotal(list);
              if (list == null)
              {
                context.Response.Write("{\"status\":0, \"msg\":\"对不起，商品为空，无法结算！\"}");
              }
              else
              {
                Rain.Model.orders model3 = new Rain.Model.orders();
                model3.order_no = "B" + Utils.GetOrderNumber();
                model3.user_id = num1;
                model3.user_name = str;
                model3.payment_id = formInt1;
                model3.express_id = formInt2;
                model3.accept_name = html1;
                model3.area = html2 + "," + html3 + "," + html4;
                model3.address = html5;
                model3.telphone = html6;
                model3.mobile = html7;
                model3.message = html10;
                model3.email = html8;
                model3.post_code = html9;
                model3.is_invoice = formInt3;
                model3.payable_amount = total.payable_amount;
                model3.real_amount = total.real_amount;
                model3.express_status = 1;
                model3.express_fee = model1.express_fee;
                if (model2.type == 1)
                {
                  model3.payment_status = 1;
                  int num2 = model2.poundage_type != 1 ? 1 : (!(model2.poundage_amount > new Decimal(0)) ? 1 : 0);
                  model3.payment_fee = num2 != 0 ? model2.poundage_amount : model3.real_amount * model2.poundage_amount / new Decimal(100);
                }
                if (model3.is_invoice == 1)
                {
                  model3.invoice_title = html11;
                  int num2 = orderconfig.taxtype != 1 ? 1 : (!(orderconfig.taxamount > new Decimal(0)) ? 1 : 0);
                  model3.invoice_taxes = num2 != 0 ? orderconfig.taxamount : model3.real_amount * orderconfig.taxamount / new Decimal(100);
                }
                model3.order_amount = model3.real_amount + model3.express_fee + model3.payment_fee + model3.invoice_taxes;
                model3.point = total.total_point;
                model3.add_time = DateTime.Now;
                List<order_goods> orderGoodsList = new List<order_goods>();
                foreach (cart_items cartItems in list)
                {
                  if (new Rain.BLL.article().GetStockQuantity(cartItems.article_id) < cartItems.quantity)
                  {
                    context.Response.Write("{\"status\":0, \"msg\":\"订单中某个商品库存不足，请修改重试！\"}");
                    return;
                  }
                  orderGoodsList.Add(new order_goods()
                  {
                    article_id = cartItems.article_id,
                    goods_no = cartItems.goods_no,
                    goods_title = cartItems.title,
                    img_url = cartItems.img_url,
                    spec_text = cartItems.spec_text,
                    goods_price = cartItems.sell_price,
                    real_price = cartItems.user_price,
                    quantity = cartItems.quantity,
                    point = cartItems.point
                  });
                }
                model3.order_goods = orderGoodsList;
                if (new Rain.BLL.orders().Add(model3) < 1)
                {
                  context.Response.Write("{\"status\":0, \"msg\":\"订单保存发生错误，请联系管理员！\"}");
                }
                else
                {
                  if (model3.point < 0)
                    new Rain.BLL.user_point_log().Add(model3.user_id, model3.user_name, model3.point, "积分换购，订单号：" + model3.order_no, false);
                  ShopCart.Clear(ls);
                  Utils.WriteCookie("dt_cookie_shopping_buy", "");
                  context.Response.Write("{\"status\":1, \"url\":\"" + new BasePage().getlink(queryString, new BasePage().linkurl("payment", (object) ("?action=confirm&order_no=" + model3.order_no))) + "\", \"msg\":\"恭喜您，订单已成功提交！\"}");
                }
              }
            }
          }
        }
      }
    }

    private void order_cancel(HttpContext context)
    {
      Rain.Model.users userInfo = new BasePage().GetUserInfo();
      if (userInfo == null)
      {
        context.Response.Write("{\"status\":0, \"msg\":\"对不起，用户尚未登录或已超时！\"}");
      }
      else
      {
        string queryString = DTRequest.GetQueryString("order_no");
        Rain.Model.orders model = new Rain.BLL.orders().GetModel(queryString);
        if (queryString == "" || model == null)
          context.Response.Write("{\"status\":0, \"msg\":\"对不起，订单号不存在或已删除！\"}");
        else if (userInfo.id != model.user_id)
          context.Response.Write("{\"status\":0, \"msg\":\"对不起，不能取消别人的订单状态！\"}");
        else if (model.status > 1)
          context.Response.Write("{\"status\":0, \"msg\":\"对不起，订单不是生成状态，不能取消！\"}");
        else if (!new Rain.BLL.orders().UpdateField(queryString, "status=4"))
        {
          context.Response.Write("{\"status\":0, \"msg\":\"对不起，操作过程中发生不可遇知的错误！\"}");
        }
        else
        {
          if (model.point < 0)
            new Rain.BLL.user_point_log().Add(model.user_id, model.user_name, -1 * model.point, "取消订单，返还换购积分，订单号：" + model.order_no, false);
          context.Response.Write("{\"status\":1, \"msg\":\"取消订单成功！\"}");
        }
      }
    }

    private void view_article_click(HttpContext context)
    {
      int id = DTRequest.GetInt("id", 0);
      int num1 = DTRequest.GetInt("click", 0);
      int num2 = DTRequest.GetInt("hide", 0);
      int num3 = 0;
      if (id > 0)
      {
        Rain.BLL.article article = new Rain.BLL.article();
        num3 = article.GetClick(id);
        if (num1 > 0)
          article.UpdateField(id, "click=click+1");
      }
      if (num2 != 0)
        return;
      context.Response.Write("document.write('" + (object) num3 + "');");
    }

    private void view_comment_count(HttpContext context)
    {
      int num1 = DTRequest.GetInt("id", 0);
      int num2 = 0;
      if (num1 > 0)
        num2 = new Rain.BLL.article_comment().GetCount("is_lock=0 and article_id=" + (object) num1);
      context.Response.Write("document.write('" + (object) num2 + "');");
    }

    private void view_attach_count(HttpContext context)
    {
      int num1 = DTRequest.GetInt("id", 0);
      string str = DTRequest.GetString("view");
      int num2 = 0;
      if (num1 > 0)
        num2 = !(str.ToLower() == "count") ? new Rain.BLL.article_attach().GetDownNum(num1) : new Rain.BLL.article_attach().GetCountNum(num1);
      context.Response.Write("document.write('" + (object) num2 + "');");
    }

    private void view_cart_count(HttpContext context)
    {
      context.Response.Write("document.write('" + (object) ShopCart.GetQuantityCount() + "');");
    }

    private string verify_code(HttpContext context, string strcode)
    {
      if (string.IsNullOrEmpty(strcode))
        return "{\"status\":0, \"msg\":\"对不起，请输入验证码！\"}";
      if (context.Session["dt_session_code"] == null)
        return "{\"status\":0, \"msg\":\"对不起，验证码超时或已过期！\"}";
      if (strcode.ToLower() != context.Session["dt_session_code"].ToString().ToLower())
        return "{\"status\":0, \"msg\":\"您输入的验证码与系统的不一致！\"}";
      context.Session["dt_session_code"] = (object) null;
      return "success";
    }

    private string verify_sms_code(HttpContext context, string strcode)
    {
      if (string.IsNullOrEmpty(strcode))
        return "{\"status\":0, \"msg\":\"对不起，请输入验证码！\"}";
      if (context.Session["dt_session_sms_code"] == null)
        return "{\"status\":0, \"msg\":\"对不起，验证码超时或已过期！\"}";
      if (strcode.ToLower() != context.Session["dt_session_sms_code"].ToString().ToLower())
        return "{\"status\":0, \"msg\":\"您输入的验证码与系统的不一致！\"}";
      context.Session["dt_session_sms_code"] = (object) null;
      return "success";
    }

    private string verify_invite_reg(string user_name, string invite_code)
    {
      if (string.IsNullOrEmpty(invite_code))
        return "{\"status\":0, \"msg\":\"邀请码不能为空！\"}";
      Rain.BLL.user_code userCode = new Rain.BLL.user_code();
      Rain.Model.user_code model = userCode.GetModel(invite_code);
      if (model == null)
        return "{\"status\":0, \"msg\":\"邀请码不正确或已过期！\"}";
      if (this.userConfig.invitecodecount > 0 && model.count >= this.userConfig.invitecodecount)
      {
        model.status = 1;
        return "{\"status\":0, \"msg\":\"该邀请码已经被使用！\"}";
      }
      if (this.userConfig.pointinvitenum > 0)
        new Rain.BLL.user_point_log().Add(model.user_id, model.user_name, this.userConfig.pointinvitenum, "邀请用户【" + user_name + "】注册获得积分", true);
      ++model.count;
      userCode.Update(model);
      return "success";
    }

    private string send_verify_sms_code(HttpContext context, string mobile)
    {
      if (string.IsNullOrEmpty(mobile))
        return "{\"status\":0, \"msg\":\"发送失败，请填写手机号码！\"}";
      if (Utils.GetCookie("dt_cookie_user_mobile") == mobile)
        return "{\"status\":1, \"time\":\"" + (object) this.userConfig.regsmsexpired + "\", \"msg\":\"已发送短信，" + (object) this.userConfig.regsmsexpired + "分钟后再试！\"}";
      Rain.Model.sms_template model = new Rain.BLL.sms_template().GetModel("usercode");
      if (model == null)
        return "{\"status\":0, \"msg\":\"发送失败，短信模板不存在，请联系管理员！\"}";
      string newValue = Utils.Number(4);
      string content = model.content.Replace("{webname}", this.siteConfig.webname).Replace("{weburl}", this.siteConfig.weburl).Replace("{webtel}", this.siteConfig.webtel).Replace("{code}", newValue).Replace("{valid}", this.userConfig.regsmsexpired.ToString());
      string msg = string.Empty;
      if (!new sms_message().Send(mobile, content, 1, out msg))
        return "{\"status\":0, \"msg\":\"发送失败，" + msg + "\"}";
      context.Session["dt_session_sms_code"] = (object) newValue;
      Utils.WriteCookie("dt_cookie_user_mobile", mobile, this.userConfig.regsmsexpired);
      return "success";
    }

    private string send_verify_email(string site, Rain.Model.users userModel)
    {
      string checkCode = Utils.GetCheckCode(20);
      Rain.Model.user_code model1 = new Rain.BLL.user_code().GetModel(userModel.user_name, DTEnums.CodeEnum.RegVerify.ToString(), "d");
      if (model1 == null)
      {
        model1 = new Rain.Model.user_code();
        model1.user_id = userModel.id;
        model1.user_name = userModel.user_name;
        model1.type = DTEnums.CodeEnum.RegVerify.ToString();
        model1.str_code = checkCode;
        model1.eff_time = DateTime.Now.AddDays((double) this.userConfig.regemailexpired);
        model1.add_time = DateTime.Now;
        new Rain.BLL.user_code().Add(model1);
      }
      Rain.Model.mail_template model2 = new Rain.BLL.mail_template().GetModel("regverify");
      if (model2 == null)
        return "{\"status\":0, \"msg\":\"邮件发送失败，邮件模板内容不存在！\"}";
      string maillTitle = model2.maill_title;
      string content = model2.content;
      string subj = maillTitle.Replace("{webname}", this.siteConfig.webname).Replace("{username}", userModel.user_name);
      string bodys = content.Replace("{webname}", this.siteConfig.webname).Replace("{webtel}", this.siteConfig.webtel).Replace("{weburl}", this.siteConfig.weburl).Replace("{username}", userModel.user_name).Replace("{valid}", this.userConfig.regemailexpired.ToString()).Replace("{linkurl}", "http://" + HttpContext.Current.Request.Url.Authority.ToLower() + new BasePage().getlink(site, new BasePage().linkurl("register", (object) ("?action=checkmail&code=" + model1.str_code))));
      try
      {
        DTMail.sendMail(this.siteConfig.emailsmtp, this.siteConfig.emailssl, this.siteConfig.emailusername, DESEncrypt.Decrypt(this.siteConfig.emailpassword), this.siteConfig.emailnickname, this.siteConfig.emailfrom, userModel.email, subj, bodys);
      }
      catch
      {
        return "{\"status\":0, \"msg\":\"邮件发送失败，请联系本站管理员！\"}";
      }
      return "success";
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
