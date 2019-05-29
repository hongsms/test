<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AboutInfo.aspx.cs" Inherits="Manage_SW_Column_Admin_WebSite_AboutInfo" %>

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
    <script src="/Manage_SW/style/webuploader/webuploader.min.js" type="text/javascript"></script>
    <script src="/Manage_SW/style/js/uploader.js" type="text/javascript"></script>
    <script src="/35Parse/kindeditor.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(function () {
            //获取菜单路径
            var obj = parent.$("#mainframe");
            if (obj.attr("menupath")) {
                var menupath = "<li class=\"active\">" + obj.attr("menupath").replace(/,/ig, "</li><li class=\"active\">") + "</li>";
                $(".breadcrumb").append(menupath);
            }
           
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
                <li role="presentation" class="active"><a href="javascript:void(0);">信息设置</a></li>
              
            </ul>
            <div class="nav-tabs-content active">
              <dl>
                    <dt>股票代码</dt>
                    <dd>
                        <asp:TextBox ID="txtAttr6" runat="server" CssClass="form-control" datatype="*"></asp:TextBox>
                    </dd>
                </dl>
                <dl>
                    <dt>现价（人民币）</dt>
                    <dd>
                        <asp:TextBox ID="txtAttr1" runat="server" CssClass="form-control" datatype="*"></asp:TextBox>
                    </dd>
                </dl>
                <dl>
                    <dt>涨跌幅（%）</dt>
                    <dd>
                        <asp:TextBox ID="txtAttr2" runat="server" CssClass="form-control" datatype="*"></asp:TextBox>
                    </dd>
                </dl>
                <dl>
                    <dt>成交量（手）</dt>
                    <dd>
                        <asp:TextBox ID="txtAttr3" runat="server" CssClass="form-control" datatype="*"></asp:TextBox>
                    </dd>
                </dl>
                <dl>
                    <dt>成交量（万）</dt>
                    <dd>
                        <asp:TextBox ID="txtAttr4" runat="server" CssClass="form-control" datatype="*"></asp:TextBox>
                    </dd>
                </dl>
                <dl>
                    <dt>总市值（亿）</dt>
                    <dd>
                        <asp:TextBox ID="txtAttr5" runat="server" CssClass="form-control" datatype="*"></asp:TextBox>
                    </dd>
                </dl>
            </div>
        </div>
        <div class="bottom-btn">
            <asp:Button ID="btnEdit" runat="server" Text="提交保存" CssClass="btn btn-primary" OnClick="btnSave_Click" />
            <button type="button" class="btn btn-default" onclick="history.back();">
                返回上一页</button>
        </div>
    </div>
    </form>
</body>
</html>
