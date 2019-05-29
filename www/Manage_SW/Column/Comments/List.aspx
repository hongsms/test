<%@ Page Language="C#" AutoEventWireup="true" CodeFile="List.aspx.cs" Inherits="Manage_SW_Column_Message_List" %>

<%@ Import Namespace="WebSite.Common" %>
<!DOCTYPE html>
<html lang="zh-CN">
<head>
    <meta name="viewport" content="width=device-width, initial-scale=1" />
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
        });
        function search_onclick() {
            var str = "this.location.href='?keywords=" + escape($("#keywords").val()) + "&<%=StringHelper.DelUrlParameter("keywords,page")%>'";
            eval(str);
            return false;
        }
    </script>
</head>
<body class="sub-body">
    <form id="form1" runat="server">
    <div class="main-content">
        <div class="top-path">
            <ol class="breadcrumb">
                <li><a href="javascript:history.back();"><span class="glyphicon glyphicon-arrow-left">
                </span>返回上一页</a></li>
                <li><a href="/Manage_SW/Column/Default/Index.aspx"><span class="glyphicon glyphicon-home">
                </span>首页</a></li>
            </ol>
        </div>
        <div class="top-toolbar">
            <div class="btn-group fl" role="group" aria-label="...">
                <button type="button" class="btn btn-default" onclick="checkAll(this);">
                    <span class="glyphicon glyphicon-check"></span>全选</button>
           <%--     <button type="button" class="btn btn-default" onclick="window.location.href='Edit.aspx?<%=StringHelper.DelUrlParameter("ID")%>';">
                    <span class="glyphicon glyphicon-plus"></span>新增</button>--%>
                <asp:LinkButton runat="server" ID="btnDelete" CssClass="btn btn-default" OnClientClick="javascript:return delAll('btnDelete','删除信息后不可恢复，您确定吗？');"
                    OnClick="btnDelAll_Click"><span class="glyphicon glyphicon-trash"></span>删除</asp:LinkButton>
                <%if (Model == "Newsletter")
                  { %>
                <asp:LinkButton runat="server" ID="btnToExcel" CssClass="btn btn-default" OnClick="btnToExcel_Click">导出全部记录</asp:LinkButton>
                <%} %>
            </div>
            <div class="fr pl10">
                <div class="input-group fl search ml10">
                    <input type="text" id="keywords" name="keywords" class="form-control" value="<%=keywords %>"
                        placeholder="搜索关键字" />
                    <span class="input-group-btn">
                        <button class="btn btn-default" type="button" onclick="search_onclick()">
                            <span class="glyphicon glyphicon-search"></span>搜索
                        </button>
                    </span>
                </div>
            </div>
        </div>
        <div class="table-list">
            <table class="table">
                <thead>
                    <tr>
                        <th style="width: 6%;">
                            选择
                        </th>
                        <th style="width: 15%;">
                            信息标题
                        </th>
                        <th style="width: 15%;">
                            会员号
                        </th>
                        <th>
                            评论内容
                        </th>
                        <th style="width: 15%;">
                            提交时间
                        </th>
                        <th style="width: 10%;">
                            审核状态
                        </th>
                        <th style="width: 10%;">
                            操作
                        </th>
                    </tr>
                </thead>
                <tbody>
                    <asp:Repeater ID="rptList" runat="server">
                        <ItemTemplate>
                            <tr>
                                <td>
                                    <span style="vertical-align: middle;" class="checkall">
                                        <input type="checkbox" name="chkID" value="<%#Eval("ID") %>"></span>
                                </td>
                                <td>
                                    <%#Eval("FromName")%>
                                </td>
                                <td>
                                    <%#Eval("UserName")%>
                                </td>
                                <td>
                                    <%#Eval("Content")%>
                                </td>
                             
                                <td>
                                    <%#Eval("AddDate")%>
                                </td>
                                <td>
                                    <%#(Eval("State").ToString() == "1") ? "<span class=\"glyphicon glyphicon-ok tis\"></span>" : "<span class=\"glyphicon glyphicon-remove tis\"></span>"%>
                                </td>
                                <td>
                                    <a href="Edit.aspx?<%#StringHelper.UpUrlParameter("ID", Eval("ID",""))%>">查看</a>
                                </td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                </tbody>
            </table>
        </div>
        <asp:Literal ID="cutepage" runat="server"></asp:Literal>
    </div>
    </form>
</body>
</html>
