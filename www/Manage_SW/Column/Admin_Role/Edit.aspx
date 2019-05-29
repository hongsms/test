<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Edit.aspx.cs" Inherits="Manage_SW_Column_Admin_Role_Edit" %>

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
    <link href="/Manage_SW/style/css/Validform.css" rel="stylesheet" type="text/css" />
    <script src="/Manage_SW/style/js/Validform_v5.3.2.js" type="text/javascript"></script>
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
            //全选/取消
            $("input[type='checkbox']").on('click', function () {
                var websiteid = $(this).attr("websiteid");
                var columnid = $(this).attr("columnid");
                var ischecked = $(this).is(":checked");
                var idpath = $(this).attr("idpath");
                if (columnid == "1") {
                    if (ischecked) {
                        $("input:enabled[name='cbmenu'][websiteid='" + websiteid + "']").prop("checked", true);
                    } else {
                        $("input:enabled[name='cbmenu'][websiteid='" + websiteid + "']").prop("checked", false);
                    }
                }
                else {
                    $("input[type='checkbox'][name='cbmenu']").each(function (index, obj) {
                        if ($(obj).attr("idpath").indexOf(idpath) > -1) {
                            if (ischecked) {
                                $(obj).prop("checked", true);
                            } else {
                                $(obj).prop("checked", false);
                            }
                        }
                    });
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
                    <dt>角色名称</dt>
                    <dd>
                        <asp:TextBox ID="txtRoleName" runat="server" CssClass="form-control" datatype="*"></asp:TextBox>
                    </dd>
                </dl>
                <dl>
                    <dt>是否显示</dt>
                    <dd>
                        <div class="btn-group btn-radio" data-toggle="buttons">
                            <asp:RadioButtonList ID="rblState" runat="server" RepeatColumns="2" RepeatLayout="Flow">
                                <asp:ListItem Value="1" Selected="True" class="btn btn-primary active">显示</asp:ListItem>
                                <asp:ListItem Value="0" class="btn btn-primary">隐藏</asp:ListItem>
                            </asp:RadioButtonList>
                        </div>
                    </dd>
                </dl>
                <dl>
                    <dt>管理权限</dt>
                    <dd>
                        <div class="table-list">
                            <table class="table">
                                <thead>
                                    <tr>
                                        <th style="text-align: left;">
                                            栏目名称
                                        </th>
                                        <th style="width: 10%;">
                                            选择
                                        </th>
                                    </tr>
                                </thead>
                                <tbody>
                                    <asp:Repeater ID="rptWebSite" runat="server">
                                        <ItemTemplate>
                                            <tr>
                                                <td style="text-align: left;">
                                                    <%#OperateHelper.SetHierarchyTitle(Eval("Title", ""), int.Parse(Eval("ColumnID", "")))%>
                                                    <%#Eval("ColumnID", "") == "1" ? "" : "<span class=\"glyphicon glyphicon-folder-close tis\"></span>"%>
                                                    <%#Eval("Title") %>
                                                </td>
                                                <td>
                                                    <span style="vertical-align: middle;" class="checkall">
                                                        <input type="checkbox" name="<%#Eval("ColumnID", "") == "1" ? "cbwebsite":"cbmenu"%>"
                                                            columnid="<%#Eval("ColumnID")%>" websiteid="<%#Eval("WebSiteID")%>" idpath="<%#Eval("IDPath")%>" value="<%#Eval("IDPath", "") + "|" + Eval("WebSiteID", "")%>" <%#Eval("IsCheckbox", "") == "1" ? "checked=\"checked\"" : ""%>></span>
                                                </td>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </tbody>
                            </table>
                        </div>
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
