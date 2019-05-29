<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Index.aspx.cs" Inherits="Manage_SW_Column_Default_Index" %>

<%@ Import Namespace="WebSite.Common" %>
<!DOCTYPE html>
<html lang="zh-CN">
<head>
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <title>三五互联管理系统</title>
    <link href="/Manage_SW/style/css/reset.min.css" rel="stylesheet" type="text/css" />
    <link href="/Manage_SW/style/css/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <link href="/Manage_SW/style/css/theme.css" rel="stylesheet" type="text/css" />
    <script src="/Manage_SW/style/js/jquery.min.js" type="text/javascript"></script>
</head>
<body class="sub-body">
    <form id="form1" runat="server">
    <div class="main-content">
        <div class="top-path">
            <ol class="breadcrumb">
                <li class="active"><span class="glyphicon glyphicon-home"></span>管理首页</li>
            </ol>
        </div>
        <div class="default-index">
            <div class="bs-callout bs-callout-info clear">
                <div class="fl img">
                    <img class="img-circle" src="/Manage_SW/style/images/img_bg.jpg">
                </div>
                <div class="fl con" style="padding-top: 20px;">
                    <ul>
                        <li>
                            <h4>
                                欢迎你，<%=AdminManage.RoleName %>：<span class="user"><%=AdminManage.AdminName %></span></h4>
                        </li>
                        <li><span>您上次登录时间是：<%=LastLoginTime%>，登录IP是：<%=LastLoginIP %></span></li>
                    </ul>
                </div>
            </div>
            
            
        </div>
    </div>
    </form>
</body>
</html>
