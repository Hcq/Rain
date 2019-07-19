<%@ Page Language="C#" AutoEventWireup="true" Inherits="Rain.Web.UI.Page.index" ValidateRequest="false" %>

<%@ Import Namespace="System.Collections.Generic" %>
<%@ Import Namespace="System.Text" %>
<%@ Import Namespace="System.Data" %>
<%@ Import Namespace="Rain.Common" %>

<script runat="server">
    override protected void OnInit(EventArgs e)
    {

        /* 
            This page was created by Rain Template Engine at 2017/8/7 11:41:18.
            本页面代码由Rain模板引擎生成于 2017/8/7 11:41:18. 
        */

        base.OnInit(e);
        StringBuilder templateBuilder = new StringBuilder(220000);

        templateBuilder.Append("<!DOCTYPE html>\r\n<html lang=\"zh-CN\">\r\n<head>\r\n    <meta charset=\"utf-8\">\r\n    <meta http-equiv=\"X-UA-Compatible\" content=\"IE=edge\">\r\n    <meta name=\"viewport\" content=\"width=device-width, initial-scale=1\">\r\n    <title>");
        templateBuilder.Append(Utils.ObjectToStr(site.seo_title));
        templateBuilder.Append("</title>\r\n    <meta name=\"keywords\" content=\"");
        templateBuilder.Append(Utils.ObjectToStr(site.seo_keyword));
        templateBuilder.Append("\" />\r\n    <meta name=\"description\" content=\"");
        templateBuilder.Append(Utils.ObjectToStr(site.seo_description));
        templateBuilder.Append("\" />\r\n    <link href=\"");
        templateBuilder.Append("/templates/webhtml");
        templateBuilder.Append("/css/bootstrap.css\" rel=\"stylesheet\">\r\n    <link href=\"");
        templateBuilder.Append("/templates/webhtml");
        templateBuilder.Append("/css/bxslider.css\" rel=\"stylesheet\">\r\n    <link href=\"");
        templateBuilder.Append("/templates/webhtml");
        templateBuilder.Append("/css/style.css\" rel=\"stylesheet\">\r\n    <script src=\"");
        templateBuilder.Append("/templates/webhtml");
        templateBuilder.Append("/js/jquery.min.js\"></");
        templateBuilder.Append("script>\r\n    <script src=\"");
        templateBuilder.Append("/templates/webhtml");
        templateBuilder.Append("/js/bxslider.min.js\"></");
        templateBuilder.Append("script>\r\n    <script src=\"");
        templateBuilder.Append("/templates/webhtml");
        templateBuilder.Append("/js/common.js\"></");
        templateBuilder.Append("script>\r\n    <script src=\"");
        templateBuilder.Append("/templates/webhtml");
        templateBuilder.Append("/js/bootstrap.js\"></");
        templateBuilder.Append("script>\r\n    <!--[if lt IE 9]>\r\n	  <script src=\"");
        templateBuilder.Append("/templates/webhtml");
        templateBuilder.Append("/js/html5shiv.min.js\"></");
        templateBuilder.Append("script>\r\n	  <script src=\"");
        templateBuilder.Append("/templates/webhtml");
        templateBuilder.Append("/js/respond.min.js\"></");
        templateBuilder.Append("script>\r\n	<![endif]-->\r\n</head>\r\n<body>\r\n    <!--Header-->\r\n        ");

        templateBuilder.Append("<header>\r\n  <!-- Fixed navbar -->\r\n  <nav class=\"navbar navbar-default\">\r\n    <div class=\"container\">\r\n      <div class=\"navbar-header\">\r\n        <button type=\"button\" class=\"navbar-toggle collapsed\" data-toggle=\"collapse\" data-target=\"#navbar\" aria-expanded=\"false\" aria-controls=\"navbar\"><span class=\"sr-only\">导航菜单</span><span class=\"icon-bar\"></span><span class=\"icon-bar\"></span><span class=\"icon-bar\"></span></button>\r\n        <a href=\"/\"><img src=\"");
        templateBuilder.Append("/templates/webhtml");
        templateBuilder.Append("/images/logo.png\" class=\"logo\" alt=\"\"/></a></div>\r\n      <div id=\"navbar\" class=\"navbar-collapse collapse\">\r\n        <ul class=\"nav navbar-nav\">\r\n          <li><a href=\"");
        templateBuilder.Append(linkurl("index"));

        templateBuilder.Append("\">网站首页</a></li>\r\n          <li><a href=\"");
        templateBuilder.Append(linkurl("content", 54));

        templateBuilder.Append("\">关于我们</a></li>\r\n          <li class=\"dropdown\"> <a href=\"");
        templateBuilder.Append(linkurl("goods_list", 55));

        templateBuilder.Append("\">产品中心</a> <a href=\"");
        templateBuilder.Append(linkurl("goods_list", 55));

        templateBuilder.Append("\" id=\"app_menudown\" class=\"dropdown-toggle\" data-toggle=\"dropdown\" role=\"button\" aria-expanded=\"false\"><span class=\"glyphicon glyphicon-menu-down btn-xs\"></span></a>\r\n            <ul class=\"dropdown-menu nav_small\" role=\"menu\">\r\n              <li><a href=\"");
        templateBuilder.Append(linkurl("goods_list", 46));

        templateBuilder.Append("\">电脑整机</a></li>\r\n              <li><a href=\"");
        templateBuilder.Append(linkurl("goods_list", 47));

        templateBuilder.Append("\">外设产品</a></li>\r\n              <li><a href=\"");
        templateBuilder.Append(linkurl("goods_list", 48));

        templateBuilder.Append("\">办公打印</a></li>\r\n            </ul>\r\n          </li>\r\n          <li class=\"dropdown\"> <a href=\"");
        templateBuilder.Append(linkurl("news_list", 3));

        templateBuilder.Append("\">新闻中心</a> <a href=\"");
        templateBuilder.Append(linkurl("news_list", 3));

        templateBuilder.Append("\" id=\"app_menudown\" class=\"dropdown-toggle\" data-toggle=\"dropdown\" role=\"button\" aria-expanded=\"false\"><span class=\"glyphicon glyphicon-menu-down btn-xs\"></span></a>\r\n            <ul class=\"dropdown-menu nav_small\" role=\"menu\">\r\n              <li><a href=\"");
        templateBuilder.Append(linkurl("news_list", 6));

        templateBuilder.Append("\">公司新闻</a></li>\r\n              <li><a href=\"");
        templateBuilder.Append(linkurl("news_list", 7));

        templateBuilder.Append("\">行业动态</a></li>\r\n              <li><a href=\"");
        templateBuilder.Append(linkurl("news_list", 8));

        templateBuilder.Append("\">电子商务</a></li>\r\n            </ul>\r\n          </li>\r\n          <li> <a href=\"");
        templateBuilder.Append(linkurl("content", 107));

        templateBuilder.Append("\">业务服务</a></li>\r\n          <li> <a href=\"");
        templateBuilder.Append(linkurl("content", 105));

        templateBuilder.Append("\">人才招聘</a></li>\r\n          <li><a href=\"");
        templateBuilder.Append(linkurl("content", 104));

        templateBuilder.Append("\">公司荣誉</a></li>\r\n          <li><a href=\"");
        templateBuilder.Append(linkurl("content", 106));

        templateBuilder.Append("\">联系我们</a></li>\r\n        </ul>\r\n      </div>\r\n      <!--/.nav-collapse -->\r\n    </div>\r\n  </nav>\r\n  <!-- bxslider -->\r\n  <div class=\"flash\">\r\n    <ul class=\"bxslider\">\r\n      ");
        DataTable newsList0 = get_article_list("news", 4, 4, "status=0");

        int newdr0__loop__id = 0;
        foreach (DataRow newdr0 in newsList0.Rows)
        {
            newdr0__loop__id++;


            templateBuilder.Append("\r\n  <li><a href=\"#\"><img src=\"" + Utils.ObjectToStr(newdr0["img_url"]) + "\" /></a></li>\r\n        ");
        }   //end for if

        templateBuilder.Append("\r\n      \r\n    </ul>\r\n  </div>\r\n  <script type=\"text/javascript\">\r\n      $('.bxslider').bxSlider({\r\n          adaptiveHeight: true,\r\n          infiniteLoop: true,\r\n          hideControlOnEnd: true,\r\n          auto: true\r\n      });\r\n    </");
        templateBuilder.Append("script>\r\n</header>");


        templateBuilder.Append("\r\n        <!--/Header-->\r\n<!-- main -->\r\n<div class=\"main\">\r\n  <div class=\"container\">\r\n    <div class=\"row\">\r\n      <div class=\"col-xs-12 col-sm-12 col-md-12\">\r\n        <div class=\"about_box\">\r\n          <h2 class=\"about_h2\">公司简介</h2>\r\n          <span class=\"about_span\">ABOUT US</span>\r\n          <section>\r\n          <img align=\"left\" src=\"");
        templateBuilder.Append("/templates/webhtml");
        templateBuilder.Append("/picture/about.png\" alt=\"公司简介\">\r\n          <p class=\"about_contents\"> 海天设计CMS是一款支持多分站、多语种，集成内容发布、信息发布、自定义表单、自定义模型、会员系统、业务管理等功能于一体的网站管理系统，于2009年正式发布，目前全国用户已经超过50万以上，被广泛用于企业、中小学、大专院校，政府部门的网站搭建。海天设计CMS是一款支持多分站、多语种，集成内容发布、信息发布、自定义表单、自定义模型、会员系统、业务管理等功能于一体的网站管理系统，于2009年正式发布，目前全国用户已经超过50万以上，被广泛用于企业、中小学、大专院校，政府部门的网站搭建。</p>\r\n          <section>\r\n        </div>\r\n      </div>\r\n    </div>\r\n  </div>\r\n  <div class=\"container\">\r\n    <div class=\"row\">\r\n      <div class=\"col-xs-12 col-sm-12 col-md-12\">\r\n        <div class=\"index_product\"><img src=\"");
        templateBuilder.Append("/templates/webhtml");
        templateBuilder.Append("/picture/tea.png\" alt=\"产品展示\" style=\"display: block; margin:0 auto; padding-top:50px;\" />\r\n          <div class=\"product_list\">\r\n		    ");
        DataTable goodslist = get_article_list("goods", 55, 6, "status=0");

        foreach (DataRow dr in goodslist.Rows)
        {

            templateBuilder.Append("\r\n     <div class=\"col-sm-4 col-md-2 col-mm-6 product_img\"><a href=\"");
            templateBuilder.Append(linkurl("goods_show", Utils.ObjectToStr(dr["id"])));

            templateBuilder.Append("\"><img src=\"" + Utils.ObjectToStr(dr["img_url"]) + "\" class=\"opacity_img\" alt=\"");
            templateBuilder.Append(Utils.DropHTML(Utils.ObjectToStr(dr["title"]), 15));

            templateBuilder.Append("\"></a>\r\n              <p class=\"product_title\"><a href=\"");
            templateBuilder.Append(linkurl("goods_show", Utils.ObjectToStr(dr["id"])));

            templateBuilder.Append("\" title=\"");
            templateBuilder.Append(Utils.DropHTML(Utils.ObjectToStr(dr["title"]), 15));

            templateBuilder.Append("\">");
            templateBuilder.Append(Utils.DropHTML(Utils.ObjectToStr(dr["title"]), 15));

            templateBuilder.Append("</a></p>\r\n            </div>\r\n        ");
        }   //end for if

        templateBuilder.Append("\r\n		  \r\n           \r\n          \r\n          </div>\r\n        </div>\r\n        \r\n      </div>\r\n    </div>\r\n  </div>\r\n  <div class=\"container\">\r\n  <div class=\"row\">\r\n    <div class=\"col-xs-12 col-sm-8 col-md-7\">\r\n      <div class=\"news_box\">\r\n        <h2 class=\"about_h2\">新闻中心</h2>\r\n        <span class=\"about_span\">NEWS CENTER</span>\r\n        <ul class=\"index_news\">\r\n		 ");
        DataTable newsList = get_article_list("news", 3, 4, "status=0");

        int newdr__loop__id = 0;
        foreach (DataRow newdr in newsList.Rows)
        {
            newdr__loop__id++;


            templateBuilder.Append("\r\n       <li><a href=\"");
            templateBuilder.Append(linkurl("news_show", Utils.ObjectToStr(newdr["id"])));

            templateBuilder.Append("\" title=\"");
            templateBuilder.Append(Utils.DropHTML(Utils.ObjectToStr(newdr["title"]), 60));

            templateBuilder.Append("\">");
            templateBuilder.Append(Utils.DropHTML(Utils.ObjectToStr(newdr["title"]), 60));

            templateBuilder.Append("</a><span\r\n                            class='news_time'>"); templateBuilder.Append(Utils.ObjectToDateTime(Utils.ObjectToStr(newdr["add_time"])).ToString("MM-dd"));

            templateBuilder.Append("</span></li>\r\n        ");
        }   //end for if

        templateBuilder.Append("\r\n		\r\n        </ul>\r\n      </div>\r\n    </div>\r\n   <div class=\"col-xs-12 col-sm-4 col-md-5\">\r\n      <div class=\"index_contact\">\r\n        <h2 class=\"about_h2\">联系我们</h2>\r\n        <span class=\"about_span\">CONTACT US</span>\r\n                    <p style=\"padding-top: 20px;\">\r\n                        联系人：HitCMS</p>\r\n                    <p>\r\n                        Q Q：978337593</p>\r\n                    \r\n                    <p>\r\n                        邮 箱：978337593@qq.com</p>\r\n                    <p>\r\n                        地 址：广东省广州市</p>\r\n                </div>\r\n      \r\n    </div>\r\n  </div>\r\n</div>\r\n</div>\r\n<nav class=\"navbar navbar-default navbar-fixed-bottom footer_nav\">\r\n  <div class=\"foot_nav btn-group dropup\"> <a class=\"dropdown-toggle\"  data-toggle=\"dropdown\" aria-haspopup=\"true\" aria-expanded=\"false\" href=\"#\"> <span class=\"glyphicon glyphicon-share btn-lg\" aria-hidden=\"true\"></span> 分享</a>\r\n    <div class=\"dropdown-menu webshare\">\r\n      <!-- JiaThis Button BEGIN -->\r\n      <div class=\"jiathis_style_32x32\"> <a class=\"jiathis_button_qzone\"></a> <a class=\"jiathis_button_tsina\"></a> <a class=\"jiathis_button_tqq\"></a> <a class=\"jiathis_button_weixin\"></a> <a class=\"jiathis_button_renren\"></a> <a href=\"http://www.jiathis.com/share\" class=\"jiathis jiathis_txt jtico jtico_jiathis\" target=\"_blank\"></a> </div>\r\n      <script type=\"text/javascript\" src=\"");
        templateBuilder.Append("/templates/webhtml");
        templateBuilder.Append("/js/jia.js\" charset=\"utf-8\"></");
        templateBuilder.Append("script>\r\n      <!-- JiaThis Button END -->\r\n    </div>\r\n  </div>\r\n  <div class=\"foot_nav\"><a href=\"tel:13933336666\"><span class=\"glyphicon glyphicon-phone btn-lg\" aria-hidden=\"true\"></span>手机</a></div>\r\n  <div class=\"foot_nav\"><a id=\"gotocate\" href=\"#\"><span class=\"glyphicon glyphicon-th-list btn-lg\" aria-hidden=\"true\"></span>分类</a></div>\r\n  <div class=\"foot_nav\"><a id=\"gototop\" href=\"#\"><span class=\"glyphicon glyphicon-circle-arrow-up btn-lg\" aria-hidden=\"true\"></span>顶部</a></div>\r\n</nav>\r\n    <!--Footer-->\r\n        ");

        templateBuilder.Append("<footer>\r\n  <div class=\"copyright\">\r\n    <p>CopyRight 2016 All Right Reserved HitCMS&nbsp;ICP:123456 </p>\r\n    <p class=\"hidden-xs\">地址：广东省广州市 &nbsp;电话：020-87961814 &nbsp;传真：020-123456&nbsp;</p>\r\n  </div>\r\n</footer>");


        templateBuilder.Append("\r\n        <!--/Footer-->\r\n    \r\n</body>\r\n</html>\r\n");
        Response.Write(templateBuilder.ToString());
    }
</script>
