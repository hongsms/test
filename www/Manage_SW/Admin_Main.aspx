<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Admin_Main.aspx.cs" Inherits="Manage_SW_Admin_Main" %>

<%@ Import Namespace="WebSite.Common" %>
<%@ Import Namespace="WebSite.BLL" %>
<%@ Import Namespace="WebSite.Model" %>
<!DOCTYPE html>
<html lang="zh-CN" style="overflow: hidden;">
<head>
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <title>三五互联管理系统</title>
    <link href="style/css/reset.min.css" rel="stylesheet" type="text/css" />
    <link href="style/css/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <link href="style/css/theme.css" rel="stylesheet" type="text/css" />
    <script src="style/js/jquery.min.js" type="text/javascript"></script>
    <script src="style/js/bootstrap.min.js" type="text/javascript"></script>
    <script src="style/js/modal.js" type="text/javascript"></script>
    <style type="text/css">
        html
        {
            overflow: hidden;
        }
    </style>
    <script type="text/javascript">
        $(function () {
            $(".top-root-menu").on('click', function () {
                var obj = $(this);
                if (!obj.hasClass("active")) {
                    $(".top-root-menu").removeClass("active");
                    obj.addClass("active");
                    $(".main-menu-wide>ul").hide();
                    $(".main-menu-wide>ul").eq(obj.index()).show();
                }
            });
            $(".one-menu>li>a").on('click', function () {
                var obj = $(this);
                var obj_root_ul = obj.parent("li").parent("ul");
                if (obj.parent("li").hasClass("active")) {
                    obj.children("span").eq(0).removeClass("glyphicon-folder-open").addClass("glyphicon-folder-close");
                    obj.children(".more").removeClass("glyphicon-menu-down").addClass("glyphicon-menu-right");
                    obj.parent("li").removeClass("active");
                    obj.siblings("ul").slideUp(200);
                }
                else {
                    obj_root_ul.find(">li>a>.more").removeClass("glyphicon-menu-down").addClass("glyphicon-menu-right");
                    obj.children(".more").removeClass("glyphicon-menu-right").addClass("glyphicon-menu-down");
                    obj_root_ul.find(">li>a>span").removeClass("glyphicon-folder-open").addClass("glyphicon-folder-close");
                    obj.children("span").eq(0).removeClass("glyphicon-folder-close").addClass("glyphicon-folder-open");
                    obj_root_ul.find(">li").removeClass("active").children("ul").slideUp(200);
                    obj.parent("li").addClass("active").children("ul").slideDown(200);
                }
            });
            $(".two-menu>li>a").on('click', function () {
                var obj = $(this).parent("li");
                if (obj.find(".three-menu").length > 0) {
                    if (!obj.hasClass("act")) {
                        obj.siblings().removeClass("act").children("ul").slideUp(200);
                        obj.siblings().children("a").children(".more").removeClass("glyphicon-menu-down").addClass("glyphicon-menu-right");
                        obj.addClass("act");
                        obj.children("a").children(".more").removeClass("glyphicon-menu-right").addClass("glyphicon-menu-down");
                        obj.children("ul").slideDown(200);
                    } else {
                        obj.removeClass("act");
                        obj.children("a").children(".more").removeClass("glyphicon-menu-down").addClass("glyphicon-menu-right");
                        obj.children("ul").slideUp(200);
                    }
                }
                else {
                    if (!obj.hasClass("active")) {
                        obj.parent("ul").parent("li").parent("ul").parent("div").find("ul>li>ul>li").removeClass("active");
                        obj.addClass("active");
                    }
                }
            });
            $(".three-menu>li>a").on('click', function () {
                var obj = $(this).parent("li");
                if (!obj.hasClass("active")) {
                    obj.parents(".main-menu-wide").find("ul>li>ul>li").removeClass("active");
                    $(".three-menu li").removeClass("active");
                    obj.addClass("active");
                }
            });
        });
        function gotoUrl(url, str) {
            if (url != "") {
                var obj = $("#mainframe");
                $(obj).attr("menuurl", url);
                $(obj).attr("menupath", str);
                eval('parent.mainframe.location.href="' + url + '";');
            }
        }
        function outSystem() {
            parent.message('confirm', '是否要退出三五管理系统？', function () { location.href = 'Admin_Logout.aspx'; });
        }
    </script>
</head>
<body class="main-body">
    <div role="navigation" class="top-container navbar-fixed-top">
        <div class="container-fluid">
            <div class="navbar-header">
                <a href="javascript:gotoUrl('Column/Default/Index.aspx','管理首页')" class="navbar-brand">
                    三五管理系统</a>
            </div>
            <div class="collapse navbar-collapse">
                <ul class="nav navbar-nav top-menu">
                    <%--<li class="top-operation-menu"><a href="#"><span class="glyphicon glyphicon-menu-hamburger">
                    </span></a></li>--%>
                    <asp:Repeater ID="rptMenuRootTopList" runat="server">
                        <ItemTemplate>
                            <li class="top-root-menu <%#Container.ItemIndex == 0 ? "active" : ""%>"><a href="javascript:void(0);">
                                <%#Eval("Title") %></a></li>
                        </ItemTemplate>
                    </asp:Repeater>
                </ul>
                <ul class="nav navbar-nav navbar-right top-state">
                    <%if (rptWebSiteList.Items.Count > 1)
                      { %>
                    <li class="dropdown"><a href="#" class="dropdown-toggle" data-toggle="dropdown" role="button"
                        aria-haspopup="true" aria-expanded="false">
                        <%=OperateHelper.GetWebSite(AdminManage.WebSiteID).WebName %><span class="caret"></span></a>
                        <ul class="dropdown-menu">
                            <asp:Repeater ID="rptWebSiteList" runat="server">
                                <ItemTemplate>
                                    <li><a href="/Manage_SW/Admin_Main.aspx?w=<%#Eval("ID") %>" target="_parent"><span
                                        class="glyphicon glyphicon-th-large"></span>&nbsp;<%#Eval("WebName") %></a></li>
                                </ItemTemplate>
                            </asp:Repeater>
                        </ul>
                    </li>
                    <%} %>
                    <li class="dropdown"><a href="#" class="dropdown-toggle" data-toggle="dropdown" role="button"
                        aria-haspopup="true" aria-expanded="false"><span class="glyphicon glyphicon-user">
                        </span>
                        <%=AdminManage.AdminName %><span class="caret"></span></a>
                        <ul class="dropdown-menu">
                            <li><a href="/" target="_blank"><span class="glyphicon glyphicon-home"></span>&nbsp;前台首页</a></li>
                            <li><a href="javascript:gotoUrl('Column/Admin_User/InfoEdit.aspx','个人资料')"><span
                                class="glyphicon glyphicon-file"></span>&nbsp;个人资料</a></li>
                            <li><a href="javascript:gotoUrl('Column/Admin_User/ChangePwd.aspx','修改密码')"><span
                                class="glyphicon glyphicon-lock"></span>&nbsp;修改密码</a></li>
                          
                            <li role="separator" class="divider"></li>
                            <li><a href="javascript:void(0);" onclick="outSystem()"><span class="glyphicon glyphicon-log-out">
                            </span>&nbsp;安全退出</a></li>
                        </ul>
                    </li>
                </ul>
            </div>
        </div>
    </div>
    <div class="main-container">
        <div class="main-menu">
            <div class="main-menu-wide">
                <%
                    int i = 0;
                    foreach (var MenuRoot in MAdmin_MenuList.Where(m => m.ParentID == 0))
                    {
                %>
                <ul class="one-menu" <%=i == 0 ? "" : "style=\"display: none;\""%>>
                    <% 
                        foreach (var MenuOne in MAdmin_MenuList.Where(m => m.ParentID == MenuRoot.ID))
                        {
                    %>
                    <li><a href="javascript:gotoUrl('<%=MenuOne.Url%>','<%=MenuRoot.Title + "," + MenuOne.Title%>')">
                        <span class="glyphicon glyphicon-folder-close"></span>
                        <%=MenuOne.Title%><span class="glyphicon glyphicon-menu-right more"></span></a>
                        <ul class="two-menu">
                            <% 
                            foreach (var MenuTwo in MAdmin_MenuList.Where(m => m.ParentID == MenuOne.ID))
                            {
                                List<Mod_AdminMenu> MenuThreeList = MAdmin_MenuList.Where(m => m.ParentID == MenuTwo.ID).ToList();
                            %>
                            <li>
                                <%
                                if (MenuThreeList.Count == 0)
                                { 
                                %>
                                <a href="javascript:gotoUrl('<%=MenuTwo.Url%>','<%=MenuRoot.Title + "," + MenuOne.Title + "," + MenuTwo.Title%>')"><span class="glyphicon glyphicon-file"></span><%=MenuTwo.Title%></a>
                                <%
                                }
                                else
                                { 
                                %>
                                <a href="javascript:void(0);"><span class="glyphicon glyphicon-file"></span><%=MenuTwo.Title%><span class="glyphicon glyphicon-menu-right more"></span></a>
                                <ul class="three-menu">
                                    <% 
                                    foreach (var MenuThree in MenuThreeList)
                                    {
                                    %>
                                    <li><a href="javascript:gotoUrl('<%=MenuThree.Url%>','<%=MenuRoot.Title + "," + MenuOne.Title + "," + MenuTwo.Title+ "," + MenuThree.Title%>')">
                                        <span class="glyphicon glyphicon-triangle-right"></span>
                                        <%=MenuThree.Title%></a>
                                    </li>
                                    <%   
                                    } 
                                    %>
                                </ul>
                                <%
                                }
                                %>
                            </li>
                            <%   
                            } 
                            %>
                        </ul>
                    </li>
                    <%   
                        } 
                    %>
                </ul>
                <%
                        i++;
                    } 
                %>
            </div>
        </div>
        <div class="main-iframe">
            <iframe frameborder="0" src="Column/Default/Index.aspx" name="mainframe" id="mainframe">
            </iframe>
        </div>
    </div>
    <div role="navigation" class="bottom-container navbar-fixed-bottom">
        <div class="container">
            <div class="navbar-left">
                版本号： 3.0.20160310
            </div>
            <div class="navbar-right">
                &copy; 2016 35.com Co., Ltd. All Rights Reserved.
            </div>
        </div>
    </div>
</body>
</html>
