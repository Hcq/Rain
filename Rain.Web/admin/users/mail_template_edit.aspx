﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="mail_template_edit.aspx.cs" Inherits="Rain.Web.admin.users.mail_template_edit" ValidateRequest="false" %>
<%@ Import namespace="Rain.Common" %>

<!DOCTYPE html>
<html>
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<meta name="viewport" content="width=device-width,minimum-scale=1.0,maximum-scale=1.0,initial-scale=1.0,user-scalable=no" />
<meta name="apple-mobile-web-app-capable" content="yes" />
<title>编辑邮件模板</title>
<link href="../../scripts/artdialog/ui-dialog.css" rel="stylesheet" type="text/css" />
<link href="../skin/default/style.css" rel="stylesheet" type="text/css" />
<script type="text/javascript" charset="utf-8" src="../../scripts/jquery/jquery-1.11.2.min.js"></script>
<script type="text/javascript" charset="utf-8" src="../../scripts/jquery/Validform_v5.3.2_min.js"></script>
<script type="text/javascript" charset="utf-8" src="../../scripts/artdialog/dialog-plus-min.js"></script>
<script type="text/javascript" charset="utf-8" src="../../editor/kindeditor-min.js"></script>
<script type="text/javascript" charset="utf-8" src="../js/laymain.js"></script>
<script type="text/javascript" charset="utf-8" src="../js/common.js"></script>
<script type="text/javascript">
    $(function () {
        //初始化表单验证
        $("#form1").initValidform();
        //初始化编辑器
        var editorMini = KindEditor.create('#txtContent', {
            width: '100%',
            height: '250px',
            resizeType: 1,
            uploadJson: '../../tools/upload_ajax.ashx?action=EditorFile&IsWater=1',
            items: [
				'source', 'fontname', 'fontsize', '|', 'forecolor', 'hilitecolor', 'bold', 'italic', 'underline',
				'removeformat', '|', 'justifyleft', 'justifycenter', 'justifyright', 'insertorderedlist',
				'insertunorderedlist', '|', 'image', 'link', 'fullscreen']
        });
    });
</script>
</head>

<body class="mainbody">
<form id="form1" runat="server">
<!--导航栏-->
<div class="location">
  <a href="mail_template_list.aspx" class="back"><i></i><span>返回列表页</span></a>
  <a href="../center.aspx" class="home"><i></i><span>首页</span></a>
  <i class="arrow"></i>
  <span>会员设置</span>
  <i class="arrow"></i>
  <span>编辑邮件模板</span>
</div>
<div class="line10"></div>
<!--/导航栏-->

<!--内容-->
<div id="floatHead" class="content-tab-wrap">
  <div class="content-tab">
    <div class="content-tab-ul-wrap">
      <ul>
        <li><a class="selected" href="javascript:;">编辑邮件内容</a></li>
      </ul>
    </div>
  </div>
</div>

<div class="tab-content">
  <dl>
    <dt>模板标题</dt>
    <dd>
      <asp:TextBox ID="txtTitle" runat="server" CssClass="input normal" datatype="*1-100" sucmsg=" "></asp:TextBox>
      <span class="Validform_checktip">*标题名称，100字符内</span>
    </dd>
  </dl>
  <dl>
    <dt>调用别名</dt>
    <dd>
      <asp:TextBox ID="txtCallIndex" runat="server" CssClass="input normal" datatype="s0-50" sucmsg=" "></asp:TextBox>
      <span class="Validform_checktip">程序调用的别名，50字符内</span>
    </dd>
  </dl>
  <dl>
    <dt>邮件标题</dt>
    <dd>
      <asp:TextBox ID="txtMailTitle" runat="server" CssClass="input normal" datatype="*1-100" sucmsg=" "></asp:TextBox>
      <span class="Validform_checktip">*邮件标题，100字符内</span>
    </dd>
  </dl>
  <dl>
    <dt>邮件内容</dt>
    <dd>
      <textarea id="txtContent" class="editor" style="visibility:hidden;" runat="server"></textarea>
    </dd>
  </dl>
</div>
<!--/内容-->

<!--工具栏-->
<div class="page-footer">
  <div class="btn-wrap">
    <asp:Button ID="btnSubmit" runat="server" Text="提交保存" CssClass="btn" onclick="btnSubmit_Click" />
    <input name="btnReturn" type="button" value="返回上一页" class="btn yellow" onclick="javascript:history.back(-1);" />
  </div>
</div>
<!--/工具栏-->

</form>
</body>
</html>
