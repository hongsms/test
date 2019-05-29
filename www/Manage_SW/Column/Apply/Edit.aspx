<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Edit.aspx.cs" Inherits="Manage_SW_Column_Message_Edit" %>

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
                    <dt>应聘职位</dt>
                    <dd>
                        <asp:TextBox ID="JOBTITLE" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                    </dd>
                </dl>
                <dl>
                    <dt>真实姓名</dt>
                    <dd>
                        <asp:TextBox ID="ZSXM" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                    </dd>
                </dl>
                <dl>
                    <dt>性别</dt>
                    <dd>
                        <asp:TextBox ID="XB" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                    </dd>
                </dl>
                <dl>
                    <dt>最高学历</dt>
                    <dd>
                        <asp:TextBox ID="ZGXL" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                    </dd>
                </dl>
                <dl>
                    <dt>婚姻状况</dt>
                    <dd>
                        <asp:TextBox ID="HYZK" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                    </dd>
                </dl>
                <dl>
                    <dt>出生日期</dt>
                    <dd>
                        <asp:TextBox ID="CSRQ" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                    </dd>
                </dl>
                <dl>
                    <dt>邮箱地址</dt>
                    <dd>
                        <asp:TextBox ID="YXDZ" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                    </dd>
                </dl>
                <dl>
                    <dt>手机号码</dt>
                    <dd>
                        <asp:TextBox ID="SJHM" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                    </dd>
                </dl>
                <dl>
                    <dt>籍贯</dt>
                    <dd>
                        <asp:TextBox ID="JG" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                    </dd>
                </dl>
                <dl>
                    <dt>工作经历</dt>
                    <dd>
                        <asp:TextBox ID="GZJL" runat="server" TextMode="MultiLine" Width="500" Height="80"
                            ReadOnly="true" CssClass="form-control"></asp:TextBox>
                    </dd>
                </dl>
                <dl>
                    <dt>职业技能</dt>
                    <dd>
                        <asp:TextBox ID="ZYJN" runat="server" TextMode="MultiLine" Width="500" Height="80"
                            ReadOnly="true" CssClass="form-control"></asp:TextBox>
                    </dd>
                </dl>
                <dl>
                    <dt>提交时间</dt>
                    <dd>
                        <asp:TextBox ID="txtAddDate" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                    </dd>
                </dl>
            </div>
        </div>
        <div class="bottom-btn">
            <%--<asp:Button ID="btnEdit" runat="server" Text="提交保存" CssClass="btn btn-primary" OnClick="btnEdit_Click" />--%>
            <button type="button" class="btn btn-default" onclick="history.back();">
                返回上一页</button>
        </div>
    </div>
    </form>
</body>
</html>
