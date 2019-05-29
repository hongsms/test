﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Edit.aspx.cs" Inherits="Manage_SW_Column_Admin_User_Edit" %>

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
                    <dt>用户名</dt>
                    <dd>
                        <asp:TextBox ID="txtUserName" runat="server" CssClass="form-control" datatype="/^[a-zA-Z0-9\-\_]{2,50}$/"></asp:TextBox>
                        <span class="Validform_checktip">*字母、下划线，不可修改</span>
                    </dd>
                </dl>
                <dl>
                    <dt>密码</dt>
                    <dd>
                        <asp:TextBox ID="txtPassword" TextMode="Password" runat="server" CssClass="form-control" errormsg="密码范围在6-20位之间" nullmsg="请设置密码" datatype="*6-20"></asp:TextBox>
                    </dd>
                </dl>
                <dl>
                    <dt>角色</dt>
                    <dd>
                        <asp:DropDownList ID="ddlRole" runat="server" CssClass="selectpicker form-control">
                        </asp:DropDownList>
                    </dd>
                </dl>
                <dl>
                    <dt>姓名</dt>
                    <dd>
                        <asp:TextBox ID="txtRealName" runat="server" CssClass="form-control"></asp:TextBox>
                    </dd>
                </dl>
                <dl>
                    <dt>移动电话</dt>
                    <dd>
                        <asp:TextBox ID="txtMobile" runat="server" CssClass="form-control"></asp:TextBox>
                    </dd>
                </dl>
                <dl>
                    <dt>QQ</dt>
                    <dd>
                        <asp:TextBox ID="txtQQ" runat="server" CssClass="form-control"></asp:TextBox>
                    </dd>
                </dl>
                <dl>
                    <dt>邮箱</dt>
                    <dd>
                        <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control"></asp:TextBox>
                    </dd>
                </dl>
                <dl>
                    <dt>部门</dt>
                    <dd>
                        <asp:TextBox ID="txtDepartment" runat="server" CssClass="form-control"></asp:TextBox>
                    </dd>
                </dl>
                <dl>
                    <dt>工作单位</dt>
                    <dd>
                        <asp:TextBox ID="txtJob" runat="server" CssClass="form-control"></asp:TextBox>
                    </dd>
                </dl>
                <dl>
                    <dt>状态</dt>
                    <dd>
                        <div class="btn-group btn-radio" data-toggle="buttons">
                            <asp:RadioButtonList ID="rblState" runat="server" RepeatColumns="2" RepeatLayout="Flow">
                                <asp:ListItem Value="1" Selected="True" class="btn btn-primary active">正常</asp:ListItem>
                                <asp:ListItem Value="0" class="btn btn-primary">锁定</asp:ListItem>
                            </asp:RadioButtonList>
                        </div>
                        <span class="Validform_checktip">*锁定则无法使用该账户登录</span>
                    </dd>
                </dl>
                
                <dl>
                    <dt>默认版本</dt>
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