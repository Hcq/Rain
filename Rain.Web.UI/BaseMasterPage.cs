// Decompiled with JetBrains decompiler
// Type: Rain.Web.UI.BasePage
// Assembly: Rain.Web.UI, Version=4.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 3E78344A-857F-445F-804E-AF1B978FD5C7
// Assembly location: C:\Users\Felix.Ho\Desktop\page\bin\Rain.Web.UI.dll

using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using Rain.Common;
using Rain.Model;

namespace Rain.Web.UI
{
    public class BaseMasterPage : System.Web.UI.MasterPage
    {
        protected internal Rain.Model.siteconfig config = new Rain.BLL.siteconfig().loadConfig();
        protected internal Rain.Model.userconfig uconfig = new Rain.BLL.userconfig().loadConfig();
        protected internal Rain.Model.channel_site site = new Rain.Model.channel_site();

        protected DataTable get_payment_list(int top, string strwhere)
        {
            DataTable dataTable = new DataTable();
            string strWhere = "is_lock=0";
            if (!string.IsNullOrEmpty(strwhere))
                strWhere = strWhere + " and " + strwhere;
            return new Rain.BLL.payment().GetList(top, strWhere, "sort_id asc,id desc").Tables[0];
        }

        protected string get_payment_title(int payment_id)
        {
            return new Rain.BLL.payment().GetTitle(payment_id);
        }

        protected Decimal get_payment_poundage_amount(int payment_id, Decimal total_amount)
        {
            Rain.Model.payment model = new Rain.BLL.payment().GetModel(payment_id);
            if (model == null)
                return new Decimal(0);
            Decimal num = model.poundage_amount;
            if (model.poundage_type == 1)
                num = num * total_amount / new Decimal(100);
            return num;
        }

        protected int get_comment_count(int article_id, string strwhere)
        {
            int num = 0;
            if (article_id > 0)
            {
                string strWhere = string.Format("article_id={0}", (object)article_id);
                if (!string.IsNullOrEmpty(strwhere))
                    strWhere = strWhere + " and " + strwhere;
                num = new Rain.BLL.article_comment().GetCount(strWhere);
            }
            return num;
        }

        protected DataTable get_comment_list(int article_id, int top, string strwhere)
        {
            DataTable dataTable = new DataTable();
            if (article_id > 0)
            {
                string strWhere = string.Format("article_id={0}", (object)article_id);
                if (!string.IsNullOrEmpty(strwhere))
                    strWhere = strWhere + " and " + strwhere;
                dataTable = new Rain.BLL.article_comment().GetList(top, strWhere, "add_time desc").Tables[0];
            }
            return dataTable;
        }

        protected DataTable get_comment_list(
          int article_id,
          int page_size,
          int page_index,
          string strwhere,
          out int totalcount)
        {
            DataTable dataTable = new DataTable();
            if (article_id > 0)
            {
                string strWhere = string.Format("article_id={0}", (object)article_id);
                if (!string.IsNullOrEmpty(strwhere))
                    strWhere = strWhere + " and " + strwhere;
                dataTable = new Rain.BLL.article_comment().GetList(page_size, page_index, strWhere, "add_time desc", out totalcount).Tables[0];
            }
            else
                totalcount = 0;
            return dataTable;
        }

        protected DataTable get_article_list(string channel_name, int top, string strwhere)
        {
            DataTable dataTable = new DataTable();
            if (!string.IsNullOrEmpty(channel_name))
                dataTable = new Rain.BLL.article().GetList(channel_name, top, strwhere, "sort_id asc,add_time desc").Tables[0];
            return dataTable;
        }

        protected DataTable get_article_list(
          string channel_name,
          int category_id,
          int top,
          string strwhere)
        {
            DataTable dataTable = new DataTable();
            if (!string.IsNullOrEmpty(channel_name))
                dataTable = new Rain.BLL.article().GetList(channel_name, category_id, top, strwhere, "sort_id asc,add_time desc").Tables[0];
            return dataTable;
        }

        protected DataTable get_article_list(
          string channel_name,
          int category_id,
          int top,
          string strwhere,
          string orderby)
        {
            DataTable dataTable = new DataTable();
            if (!string.IsNullOrEmpty(channel_name))
                dataTable = new Rain.BLL.article().GetList(channel_name, category_id, top, strwhere, orderby).Tables[0];
            return dataTable;
        }

        protected DataTable get_article_list(
          string channel_name,
          int category_id,
          int page_size,
          int page_index,
          string strwhere,
          string orderby,
          out int totalcount)
        {
            DataTable dataTable = new DataTable();
            if (!string.IsNullOrEmpty(channel_name))
                dataTable = new Rain.BLL.article().GetList(channel_name, category_id, page_size, page_index, strwhere, orderby, out totalcount).Tables[0];
            else
                totalcount = 0;
            return dataTable;
        }

        protected DataTable get_article_list(
          string channel_name,
          int category_id,
          int page_size,
          int page_index,
          string strwhere,
          out int totalcount,
          out string pagelist,
          string _key,
          params object[] _params)
        {
            DataTable dataTable = new DataTable();
            if (!string.IsNullOrEmpty(channel_name))
            {
                dataTable = new Rain.BLL.article().GetList(channel_name, category_id, page_size, page_index, strwhere, "sort_id asc,add_time desc", out totalcount).Tables[0];
                pagelist = Utils.OutPageList(page_size, page_index, totalcount, this.linkurl(_key, _params), 8);
            }
            else
            {
                totalcount = 0;
                pagelist = "";
            }
            return dataTable;
        }

        protected DataTable get_article_list(
          string channel_name,
          int category_id,
          int page_size,
          int page_index,
          string strwhere,
          string orderby,
          out int totalcount,
          out string pagelist,
          string _key,
          params object[] _params)
        {
            DataTable dataTable = new DataTable();
            if (!string.IsNullOrEmpty(channel_name))
            {
                dataTable = new Rain.BLL.article().GetList(channel_name, category_id, page_size, page_index, strwhere, orderby, out totalcount).Tables[0];
                pagelist = Utils.OutPageList(page_size, page_index, totalcount, this.linkurl(_key, _params), 8);
            }
            else
            {
                totalcount = 0;
                pagelist = "";
            }
            return dataTable;
        }

        protected string get_article_content(string call_index)
        {
            if (string.IsNullOrEmpty(call_index))
                return string.Empty;
            Rain.BLL.article article = new Rain.BLL.article();
            if (article.Exists(call_index))
                return article.GetModel(call_index).content;
            return string.Empty;
        }

        protected string get_article_img_url(int article_id)
        {
            Rain.Model.article model = new Rain.BLL.article().GetModel(article_id);
            if (model != null)
                return model.img_url;
            return "";
        }

        protected string get_article_field(int article_id, string field_name)
        {
            Rain.Model.article model = new Rain.BLL.article().GetModel(article_id);
            if (model != null && model.fields.ContainsKey(field_name))
                return model.fields[field_name];
            return string.Empty;
        }

        protected string get_article_field(string call_index, string field_name)
        {
            if (string.IsNullOrEmpty(call_index))
                return string.Empty;
            Rain.BLL.article article = new Rain.BLL.article();
            if (!article.Exists(call_index))
                return string.Empty;
            Rain.Model.article model = article.GetModel(call_index);
            if (model != null && model.fields.ContainsKey(field_name))
                return model.fields[field_name];
            return string.Empty;
        }

        protected int get_cart_quantity()
        {
            return ShopCart.GetQuantityCount();
        }

        protected List<cart_items> get_cart_list()
        {
            int group_id = 0;
            Rain.Model.users userInfo = this.GetUserInfo();
            if (userInfo != null)
                group_id = userInfo.group_id;
            return ShopCart.GetList(group_id) ?? new List<cart_items>();
        }

        public DataTable get_plugin_method(
          string assemblyName,
          string className,
          string methodName,
          params object[] objParas)
        {
            DataTable dataTable = new DataTable();
            try
            {
                object instance = Assembly.Load(assemblyName).CreateInstance(assemblyName + "." + className);
                foreach (MethodInfo method in instance.GetType().GetMethods(BindingFlags.Instance | BindingFlags.Public))
                {
                    if (method.Name == methodName && method.GetParameters().Length == objParas.Length)
                        return method.Invoke(instance, objParas) as DataTable;
                }
            }
            catch
            {
            }
            return dataTable;
        }

        protected int get_user_order_count(string strwhere)
        {
            return new Rain.BLL.orders().GetCount(strwhere);
        }

        protected DataTable get_order_list(int top, string strwhere)
        {
            return new Rain.BLL.orders().GetList(top, strwhere, "add_time desc,id desc").Tables[0];
        }

        protected DataTable get_order_list(
          int page_size,
          int page_index,
          string strwhere,
          out int totalcount)
        {
            return new Rain.BLL.orders().GetList(page_size, page_index, strwhere, "add_time desc,id desc", out totalcount).Tables[0];
        }

        protected List<Rain.Model.article> get_order_goods_list(int order_id)
        {
            Rain.Model.orders model1 = new Rain.BLL.orders().GetModel(order_id);
            if (model1 == null)
                return (List<Rain.Model.article>)null;
            List<Rain.Model.article> articleList = new List<Rain.Model.article>();
            if (model1.order_goods != null)
            {
                foreach (order_goods orderGood in model1.order_goods)
                {
                    Rain.Model.article model2 = new Rain.BLL.article().GetModel(orderGood.article_id);
                    if (model2 != null)
                        articleList.Add(model2);
                }
            }
            return articleList;
        }

        protected string get_order_status(int _id)
        {
            string str = "";
            Rain.Model.orders model = new Rain.BLL.orders().GetModel(_id);
            switch (model.status)
            {
                case 1:
                    str = model.payment_status <= 0 ? "待确认" : "待付款";
                    break;
                case 2:
                    str = model.express_status <= 1 ? "待发货" : "已发货";
                    break;
                case 3:
                    str = "交易完成";
                    break;
                case 4:
                    str = "已取消";
                    break;
                case 5:
                    str = "已作废";
                    break;
            }
            return str;
        }

        protected bool get_order_payment_status(int order_id)
        {
            Rain.Model.orders model1 = new Rain.BLL.orders().GetModel(order_id);
            if (model1 == null || model1.status != 1)
                return false;
            Rain.Model.payment model2 = new Rain.BLL.payment().GetModel(model1.payment_id);
            return model2 != null && (model2.type == 1 && model1.payment_status == 1);
        }

        protected Decimal get_order_taxamount(Decimal total_amount)
        {
            Rain.Model.orderconfig orderconfig = new Rain.BLL.orderconfig().loadConfig();
            Decimal num = orderconfig.taxamount;
            if (orderconfig.taxtype == 1)
                num = num * total_amount / new Decimal(100);
            return num;
        }

        protected DataTable get_express_list(int top, string strwhere)
        {
            DataTable dataTable = new DataTable();
            string strWhere = "is_lock=0";
            if (!string.IsNullOrEmpty(strwhere))
                strWhere = strWhere + " and " + strwhere;
            return new Rain.BLL.express().GetList(top, strWhere, "sort_id asc,id desc").Tables[0];
        }

        protected string get_express_title(int express_id)
        {
            return new Rain.BLL.express().GetTitle(express_id);
        }

        public BaseMasterPage()
        {
            if (this.config.webstatus == 0)
            {
                HttpContext.Current.Response.Redirect(this.linkurl("error", (object)("?msg=" + Utils.UrlEncode(this.config.webclosereason))));
            }
            else
            {
                this.site = this.GetSiteModel();
                this.ShowPage();
            }
        }

        protected virtual void ShowPage()
        {
        }

        protected Rain.Model.channel_site GetSiteModel()
        {
            string sitePath = this.GetSitePath(HttpContext.Current.Request.RawUrl.ToLower(), HttpContext.Current.Request.Url.Authority.ToLower());
            return SiteDomains.GetSiteDomains().SiteList.Find((Predicate<Rain.Model.channel_site>)(p => p.build_path == sitePath));
        }

        public string linkurl(string _key, params object[] _params)
        {
            Rain.Model.url_rewrite urlRewrite = new Rain.BLL.url_rewrite().GetList()[(object)_key] as Rain.Model.url_rewrite;
            if (urlRewrite == null)
                return string.Empty;
            string linkStartString = this.GetLinkStartString(HttpContext.Current.Request.RawUrl.ToLower(), HttpContext.Current.Request.Url.Authority.ToLower());
            if (urlRewrite.url_rewrite_items.Count == 0)
            {
                if (this.config.staticstatus > 0)
                {
                    if (_params.Length > 0)
                        return linkStartString + this.GetUrlExtension(urlRewrite.page, this.config.staticextension) + string.Format("{0}", _params);
                    return linkStartString + this.GetUrlExtension(urlRewrite.page, this.config.staticextension);
                }
                if (_params.Length > 0)
                    return linkStartString + urlRewrite.page + string.Format("{0}", _params);
                return linkStartString + urlRewrite.page;
            }
            foreach (url_rewrite_item urlRewriteItem in urlRewrite.url_rewrite_items)
            {
                if (this.IsUrlMatch(urlRewriteItem, _params))
                {
                    if (this.config.staticstatus > 0)
                        return linkStartString + string.Format(this.GetUrlExtension(urlRewriteItem.path, this.config.staticextension), _params);
                    string str = Regex.Replace(string.Format(urlRewriteItem.path, _params), urlRewriteItem.pattern, urlRewriteItem.querystring, RegexOptions.IgnoreCase);
                    if (str.Length > 0)
                        str = "?" + str;
                    return linkStartString + urlRewrite.page + str;
                }
            }
            return string.Empty;
        }

        public string getlink(string sitepath, string urlpath)
        {
            if (string.IsNullOrEmpty(sitepath) || string.IsNullOrEmpty(urlpath))
                return urlpath;
            string lower = HttpContext.Current.Request.Url.Authority.ToLower();
            Dictionary<string, string> paths = SiteDomains.GetSiteDomains().Paths;
            if (SiteDomains.GetSiteDomains().DefaultPath == sitepath.ToLower() || paths.ContainsKey(sitepath.ToLower()) && paths.ContainsValue(lower))
                return urlpath;
            int length = this.config.webpath.Length;
            if (urlpath.StartsWith(this.config.webpath))
                urlpath = urlpath.Substring(length);
            return this.config.webpath + sitepath.ToLower() + "/" + urlpath;
        }

        protected string get_page_link(
          int pagesize,
          int pageindex,
          int totalcount,
          string _key,
          params object[] _params)
        {
            return Utils.OutPageList(pagesize, pageindex, totalcount, this.linkurl(_key, _params), 8);
        }

        protected string get_page_link(int pagesize, int pageindex, int totalcount, string linkurl)
        {
            return Utils.OutPageList(pagesize, pageindex, totalcount, linkurl, 8);
        }

        public bool IsUserLogin()
        {
            if (HttpContext.Current.Session["dt_session_user_info"] != null)
                return true;
            string cookie1 = Utils.GetCookie("dt_cookie_user_name_remember", "Rain");
            string cookie2 = Utils.GetCookie("dt_cookie_user_pwd_remember", "Rain");
            if (cookie1 != "" && cookie2 != "")
            {
                Rain.Model.users model = new Rain.BLL.users().GetModel(cookie1, cookie2, 0, 0, false);
                if (model != null)
                {
                    HttpContext.Current.Session["dt_session_user_info"] = (object)model;
                    return true;
                }
            }
            return false;
        }

        public Rain.Model.users GetUserInfo()
        {
            if (this.IsUserLogin())
            {
                Rain.Model.users users = HttpContext.Current.Session["dt_session_user_info"] as Rain.Model.users;
                if (users != null)
                    return new Rain.BLL.users().GetModel(users.id);
            }
            return (Rain.Model.users)null;
        }

        private string GetFirstPath(string requestPath)
        {
            int length = this.config.webpath.Length;
            if (requestPath.StartsWith(this.config.webpath + "aspx/"))
                length = (this.config.webpath + "aspx/").Length;
            string key = requestPath.Substring(length);
            if (key.IndexOf("/") > 0)
                key = key.Substring(0, key.IndexOf("/"));
            if (key != string.Empty && SiteDomains.GetSiteDomains().Paths.ContainsKey(key))
                return key;
            return string.Empty;
        }

        private string GetLinkStartString(string requestPath, string requestDomain)
        {
            string firstPath = this.GetFirstPath(requestPath);
            if (SiteDomains.GetSiteDomains().Paths.ContainsValue(requestDomain))
                return "/";
            if (firstPath == string.Empty || firstPath == SiteDomains.GetSiteDomains().DefaultPath)
                return this.config.webpath;
            return this.config.webpath + firstPath + "/";
        }

        private string GetSitePath(string requestPath, string requestDomain)
        {
            if (SiteDomains.GetSiteDomains().Paths.ContainsValue(requestDomain))
                return SiteDomains.GetSiteDomains().Domains[requestDomain];
            string firstPath = this.GetFirstPath(requestPath);
            if (firstPath != string.Empty)
                return firstPath;
            return SiteDomains.GetSiteDomains().DefaultPath;
        }

        private bool IsUrlMatch(url_rewrite_item item, params object[] _params)
        {
            int num = 0;
            if (!string.IsNullOrEmpty(item.querystring))
                num = item.querystring.Split('&').Length;
            return num == _params.Length && Regex.IsMatch(string.Format(item.path, _params).Replace("__id__", "1"), item.pattern, RegexOptions.IgnoreCase);
        }

        private string GetUrlExtension(string urlPage, string staticExtension)
        {
            return Utils.GetUrlExtension(urlPage, staticExtension);
        }

        protected string get_category_title(int category_id, string default_value)
        {
            Rain.BLL.article_category articleCategory = new Rain.BLL.article_category();
            if (articleCategory.Exists(category_id))
                return articleCategory.GetTitle(category_id);
            return default_value;
        }

        protected Rain.Model.article_category get_category_model(int category_id)
        {
            return new Rain.BLL.article_category().GetModel(category_id);
        }

        protected string get_category_menu(string urlKey, int category_id)
        {
            StringBuilder strTxt = new StringBuilder();
            if (category_id > 0)
                this.LoopChannelMenu(strTxt, urlKey, category_id);
            return strTxt.ToString();
        }

        protected DataTable get_category_list(string channel_name, int parent_id)
        {
            return new Rain.BLL.article_category().GetList(parent_id, channel_name);
        }

        protected DataTable get_category_child_list(string channel_name, int parent_id)
        {
            return new Rain.BLL.article_category().GetChildList(parent_id, channel_name);
        }

        private void LoopChannelMenu(StringBuilder strTxt, string urlKey, int category_id)
        {
            Rain.BLL.article_category articleCategory = new Rain.BLL.article_category();
            int parentId = articleCategory.GetParentId(category_id);
            if (parentId > 0)
                this.LoopChannelMenu(strTxt, urlKey, parentId);
            strTxt.Append("&nbsp;&gt;&nbsp;<a href=\"" + this.linkurl(urlKey, (object)category_id, (object)1) + "\">" + articleCategory.GetTitle(category_id) + "</a>");
        }

        protected DataTable get_oauth_app_list(int top, string strwhere)
        {
            string str1 = "is_lock=0";
            if (!string.IsNullOrEmpty(strwhere))
            {
                string str2 = str1 + " and " + strwhere;
            }
            return new Rain.BLL.user_oauth_app().GetList(top, "is_lock=0", "sort_id asc,id desc").Tables[0];
        }

        protected string get_user_avatar(string user_name)
        {
            Rain.BLL.users users = new Rain.BLL.users();
            if (!users.Exists(user_name))
                return "";
            return users.GetModel(user_name).avatar;
        }

        protected int get_user_message_count(string strwhere)
        {
            return new Rain.BLL.user_message().GetCount(strwhere);
        }

        protected DataTable get_user_message_list(int top, string strwhere)
        {
            return new Rain.BLL.user_message().GetList(top, strwhere, "sort_id asc,post_time desc").Tables[0];
        }

        protected DataTable get_user_message_list(
          int page_size,
          int page_index,
          string strwhere,
          out int totalcount)
        {
            return new Rain.BLL.user_message().GetList(page_size, page_index, strwhere, "is_read asc,post_time desc", out totalcount).Tables[0];
        }

        protected DataTable get_user_point_list(
          int page_size,
          int page_index,
          string strwhere,
          out int totalcount)
        {
            return new Rain.BLL.user_point_log().GetList(page_size, page_index, strwhere, "add_time desc,id desc", out totalcount).Tables[0];
        }

        protected DataTable get_user_amount_list(
          int page_size,
          int page_index,
          string strwhere,
          out int totalcount)
        {
            return new Rain.BLL.user_amount_log().GetList(page_size, page_index, strwhere, "add_time desc,id desc", out totalcount).Tables[0];
        }

        protected DataTable get_user_recharge_list(
          int page_size,
          int page_index,
          string strwhere,
          out int totalcount)
        {
            return new Rain.BLL.user_recharge().GetList(page_size, page_index, strwhere, "add_time desc,id desc", out totalcount).Tables[0];
        }

        protected DataTable get_user_invite_list(int top, string strwhere)
        {
            string strWhere = "type='" + DTEnums.CodeEnum.Register.ToString() + "'";
            if (!string.IsNullOrEmpty(strwhere))
                strWhere = strWhere + " and " + strwhere;
            return new Rain.BLL.user_code().GetList(top, strWhere, "add_time desc,id desc").Tables[0];
        }

        protected bool get_invite_status(string str_code)
        {
            return new Rain.BLL.user_code().GetModel(str_code) != null;
        }
    }
}
