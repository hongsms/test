<%@ Page Language="C#" AutoEventWireup="true" CodeFile="List.aspx.cs" Inherits="Manage_SW_Column_Information_List" %>

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
            var str = "this.location.href='?keywords=" + escape($("#keywords").val()) + "&Show=" + escape($("#ddlShow").val()) + "&TypeID=" + escape($("#ddlBaseType").val()) + "&<%=StringHelper.DelUrlParameter("keywords,Show,TypeID,page")%>'";
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
                <li><a href="javascript:history.back();" ><span class="glyphicon glyphicon-arrow-left">
                </span>返回上一页</a></li>
                <li><a href="/Manage_SW/Column/Default/Index.aspx"><span class="glyphicon glyphicon-home">
                </span>首页</a></li>
            </ol>
        </div>
        <div class="top-toolbar">
            <div class="btn-group fl" role="group" aria-label="...">
                <button type="button" class="btn btn-default" onclick="checkAll(this);">
                    <span class="glyphicon glyphicon-check"></span>全选</button>
                <button type="button" class="btn btn-default" onclick="window.location.href='Edit.aspx?<%=StringHelper.DelUrlParameter("ID")%>';">
                    <span class="glyphicon glyphicon-plus"></span>新增</button>
                <asp:LinkButton runat="server" ID="btnDelete" CssClass="btn btn-default" OnClientClick="javascript:return delAll('btnDelete','删除信息后不可恢复，您确定吗？');"
                    OnClick="btnDelAll_Click"><span class="glyphicon glyphicon-trash"></span>删除</asp:LinkButton>
                <div class="btn-group" role="group" <%=IsState != 1 ? "style=\"display:none;\"" : ""%>>
                    <asp:DropDownList ID="ddlShow" onchange="search_onclick()" runat="server" CssClass="selectpicker form-control">
                        <asp:ListItem Text="属性" Value="0"></asp:ListItem>
                        <asp:ListItem Text="显示" Value="1"></asp:ListItem>
                        <%--  <asp:ListItem Text="置顶" Value="2"></asp:ListItem>
                            <asp:ListItem Text="推荐" Value="3"></asp:ListItem>--%>
                    </asp:DropDownList>
                </div>
                <div class="btn-group" role="group" <%=IsType != 1 ? "style=\"display: none;\"" : ""%>>
                    <asp:DropDownList ID="ddlBaseType" onchange="search_onclick()" runat="server" CssClass="selectpicker form-control">
                    </asp:DropDownList>
                </div>
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
                        <th style="text-align: left;">
                            标题
                        </th>
                        <%if (IsType == 1)
                          {%>
                        <th style="width: 10%;">
                            所属类别
                        </th>
                        <%} %>
                        <th style="width: 15%;">
                            发布时间
                        </th>
                        <th style="width: 10%;">
                            排序
                        </th>
                        <%if (IsState == 1)
                          {%>
                        <th style="width: 10%;">
                            属性
                        </th>
                        <%} %>
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
                                <td style="text-align: left;">
                                    <a href="Edit.aspx?<%#StringHelper.UpUrlParameter("ID", Eval("ID",""))%>">
                                        <%#Eval("Title") %></a>
                                </td>
                                <%if (IsType == 1)
                                  {%>
                                <td>
                                    <%#OperateHelper.GetManageTypeName(Eval("TypeID"))%>
                                </td>
                                <%} %>
                                <td>
                                    <%#Eval("AddDate") %>
                                </td>
                                <td>
                                    <%#Eval("OrderBy") %>
                                </td>
                                <%if (IsState == 1)
                                  {%>
                                <td>
                                    <%#(Eval("State").ToString() == "1") ? "<span style=\"color:Red\">[显示]</span>" : "<span style=\"color:#c3c3c3\">[显示]</span>"%>
                                    <%--  <%#(Eval("IsTop").ToString() == "1") ? "<span style=\"color:Red\">[置顶]</span>" : "<span style=\"color:#c3c3c3\">[置顶]</span>"%>
                                        <%#(Eval("IsCommend").ToString() == "1") ? "<span style=\"color:Red\">[推荐]</span>" : "<span style=\"color:#c3c3c3\">[推荐]</span>"%>--%>
                                </td>
                                <%} %>
                                <td>
                                    <a href="Edit.aspx?<%#StringHelper.UpUrlParameter("ID", Eval("ID",""))%>">编辑</a>
                                   
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
