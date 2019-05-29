<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Admin_Login.aspx.cs" Inherits="Manage_SW_Admin_Login" %>


<!DOCTYPE html>
<html lang="zh-CN">
<head>
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <title>三五互联管理系统</title>
    <link href="style/css/reset.min.css" rel="stylesheet" type="text/css" />
    <link href="style/css/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <link href="style/css/theme.css" rel="stylesheet" type="text/css" />
    <script src="style/js/jquery.min.js" type="text/javascript"></script>
    <script src="style/js/bootstrap.min.js" type="text/javascript"></script>
    <script src="style/js/modal.js" type="text/javascript"></script>
    <script src="style/js/common.js" type="text/javascript"></script>
    <!--[if lt IE 9]><style>.modal-dialog{width:300px;}</style><script>$(function (){parent.message("warning", "为了更好的用户体验，请使用IE9+以上、火狐、谷歌的浏览器！");});</script><![endif]-->
    <style type="text/css">
        html
        {
            overflow: hidden;
        }
    </style>
    <script language="javascript">  
        if (top.location != location) {
            top.location.href = location.href;
        }
    </script>
</head>
<body class="main-body login-bg">
    <div class="login-main">
        <h1 class="login-logo">
            35+</h1>
        <h3 class="login-welcome">
            欢迎使用三五互联管理系统</h3>
        <div class="form-group">
            <input type="text" placeholder="用户名" class="form-control" id="UserName" onkeydown="if(event.keyCode==13) Login();" />
        </div>
        <div class="form-group">
            <input type="password" placeholder="密码" class="form-control" id="Password" onkeydown="if(event.keyCode==13) Login();" />
        </div>
         <div class="form-group">
            <input type="text" placeholder="验证码" class="form-control" id="txtCode" onkeydown="if(event.keyCode==13) Login();"
                style="width: 230px; margin-right: 10px; float: left" />
            <a href="javascript:" onclick="javascript:document.getElementById('imgValidateCode').src='/ValidateCode.aspx?id='+Math.random();return false;">
                <img id="imgValidateCode" src="/ValidateCode.aspx" />
            </a>
            <div style="clear: both">
            </div>
        </div>
        <button class="btn btn-primary btn-lg btn-block" type="button" onclick="Login();">
            登 录</button>
        <p class="text-muted text-center">
            © 2016 35.com Co., Ltd. All Rights Reserved<br />
            请使用IE9+以上、火狐、谷歌的浏览器</p>
    </div>
    <script type="text/javascript">
        $(function () {
            $("#UserName").focus();
        });
    </script>
</body>
</html>
