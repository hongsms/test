<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Delweb.aspx.cs" Inherits="Manage_SW_Column_Admin_WebSite_Delweb" %>

<!DOCTYPE html>
<html lang="zh-CN">
<head>
    <title>三五互联管理系统</title>
    <link href="/Manage_SW/style/css/reset.min.css" rel="stylesheet" type="text/css" />
    <link href="/Manage_SW/style/css/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <link href="/Manage_SW/style/css/theme.css" rel="stylesheet" type="text/css" />
    <link href="/Manage_SW/style/css/bootstrap-select.min.css" rel="stylesheet" type="text/css" />
    <script src="/Manage_SW/style/js/jquery.min.js" type="text/javascript"></script>
    <script src="/Manage_SW/style/js/bootstrap.min.js" type="text/javascript"></script>
    <script src="/Manage_SW/style/js/bootstrap-select.min.js" type="text/javascript"></script>
    <script src="/Manage_SW/style/js/common.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(function () {
            //获取菜单路径
            var obj = parent.$("#mainframe");
            if (obj.attr("menupath")) {
                var menupath = "<li class=\"active\">" + obj.attr("menupath").replace(/,/ig, "</li><li class=\"active\">") + "</li>";
                $(".breadcrumb").append(menupath);
            }

            //编辑页标签切换
            $(".nav-tabs li").on('click', function () {
                var obj = $(this);
                if (!obj.hasClass("active")) {
                    $(".nav-tabs li").removeClass("active");
                    obj.addClass("active");
                    $(".content-edit>.nav-tabs-content").removeClass("active");
                    $(".content-edit>.nav-tabs-content").eq(obj.index()).addClass("active");
                }
            });

        });
    </script>
</head>
<body class="sub-body">
    <form id="form1" runat="server" class="editform">
    <div class="main-content">
        <div class="top-path">
            <ol class="breadcrumb">
                <li><a href="javascript:history.back();"><span class="glyphicon glyphicon-arrow-left">
                </span>返回上一页</a></li>
                <li><a href="/Manage_SW/Column/Default/Index.aspx"><span class="glyphicon glyphicon-home">
                </span>首页</a></li>
            </ol>
        </div>
        <div class="content-edit">
            <ul class="nav nav-tabs">
                <li role="presentation" class="active"><a href="javascript:void(0);">基本信息</a></li>
            </ul>
            <div class="nav-tabs-content active">
                <dl>
                    <dt>删除站点</dt>
                    <dd>
                        <asp:DropDownList ID="ddlWebSite" runat="server" CssClass="selectpicker form-control">
                        </asp:DropDownList>
                    </dd>
                </dl>
                
            </div>
        </div>
        <div class="bottom-btn">
            <asp:Button ID="btnEdit" runat="server" Text="提交保存" CssClass="btn btn-primary" OnClick="btnEdit_Click" />
            <button type="button" class="btn btn-default" onclick="history.back();">
                返回上一页</button>
        </div>
    </div>
    </form>
</body>
</html>
