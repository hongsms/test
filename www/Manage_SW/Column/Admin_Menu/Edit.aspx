<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Edit.aspx.cs" Inherits="Manage_SW_Column_Admin_Menu_Edit" %>

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
                    <dt>上级栏目</dt>
                    <dd>
                        <asp:DropDownList ID="ddlMenu" runat="server" CssClass="selectpicker form-control"
                            data-size="15">
                        </asp:DropDownList>
                    </dd>
                </dl>
                <dl>
                    <dt>栏目标题</dt>
                    <dd>
                        <asp:TextBox ID="txtTitle" runat="server" CssClass="form-control" datatype="*"></asp:TextBox>
                    </dd>
                </dl>
                <dl <%=strshow%>>
                    <dt>类型标识</dt>
                    <dd>
                        <asp:TextBox ID="FunctionModel" runat="server" CssClass="form-control"></asp:TextBox>
                    </dd>
                </dl>
                <dl <%=strshow%>>
                    <dt>页面类型</dt>
                    <dd>
                        <asp:DropDownList ID="ddlWebSiteManage" runat="server">
                            <asp:ListItem Value="">请选择</asp:ListItem>
                            <asp:ListItem Value="Type">分类管理</asp:ListItem>
                            <asp:ListItem Value="About">单篇文章</asp:ListItem>
                            <asp:ListItem Value="News">新闻列表</asp:ListItem>
                            <asp:ListItem Value="FAQ">留言反馈</asp:ListItem>
                            <asp:ListItem Value="File">文件下载</asp:ListItem>
                            <asp:ListItem Value="Link">友情链接</asp:ListItem>
                        </asp:DropDownList>
                    </dd>
                </dl>
                <dl <%=strshow%>>
                    <dt>栏目分类</dt>
                    <dd>
                        <asp:DropDownList ID="ddlstTypeName" runat="server">
                        </asp:DropDownList>
                    </dd>
                </dl>
                <dl>
                    <dt>排序数字</dt>
                    <dd>
                        <asp:TextBox ID="txtOrderBy" runat="server" Width="80" Text="99" CssClass="form-control"
                            datatype="n"></asp:TextBox><span class="Validform_checktip">*数字，越小越向前</span>
                    </dd>
                </dl>
                <dl <%=strshow%>>
                    <dt>属性</dt>
                    <dd>
                        <div class="btn-group btn-check" data-toggle="buttons">
                            <asp:CheckBoxList ID="cblShow" runat="server" RepeatColumns="20" RepeatLayout="Flow">
                                <asp:ListItem Value="IsSubTitle" class="btn btn-primary">副标题</asp:ListItem>
                                <asp:ListItem Value="IsImage" class="btn btn-primary">封面图片</asp:ListItem>
                                <asp:ListItem Value="IsVideo" class="btn btn-primary">内容视频</asp:ListItem>
                                <asp:ListItem Value="IsSource" class="btn btn-primary">来源</asp:ListItem>
                                <asp:ListItem Value="IsType" class="btn btn-primary">所属类别</asp:ListItem>
                                <asp:ListItem Value="IsTags" class="btn btn-primary">标签选择</asp:ListItem>
                                <asp:ListItem Value="IsState" class="btn btn-primary">信息属性</asp:ListItem>
                                <asp:ListItem Value="IsBrowseCount" class="btn btn-primary">浏览次数</asp:ListItem>
                                <asp:ListItem Value="IsAddDate" class="btn btn-primary">发布时间</asp:ListItem>
                                <asp:ListItem Value="IsInfo" class="btn btn-primary">内容简介</asp:ListItem>
                                <asp:ListItem Value="IsContent" class="btn btn-primary">内容描述</asp:ListItem>
                                <asp:ListItem Value="IsChild" class="btn btn-primary">上级分类</asp:ListItem>
                                <asp:ListItem Value="IsAd" class="btn btn-primary">关联广告</asp:ListItem>
                                <asp:ListItem Value="IsModel" class="btn btn-primary">Model</asp:ListItem>
                                <asp:ListItem Value="IsLink" class="btn btn-primary">链接地址</asp:ListItem>
                                <asp:ListItem Value="IsAlbum" class="btn btn-primary">图片相册</asp:ListItem>
                                <asp:ListItem Value="IsImage1" class="btn btn-primary">手机图片</asp:ListItem>
                                 <asp:ListItem Value="IsFiles" class="btn btn-primary">批量文件</asp:ListItem>
                                
                            </asp:CheckBoxList>
                        </div>
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
                    <dt>是否生成</dt>
                    <dd>
                        <div class="btn-group btn-radio" data-toggle="buttons">
                            <asp:RadioButtonList ID="rblIsCopy" runat="server" RepeatColumns="2" RepeatLayout="Flow">
                                <asp:ListItem Value="1" Selected="True" class="btn btn-primary active">是</asp:ListItem>
                                <asp:ListItem Value="0" class="btn btn-primary">否</asp:ListItem>
                            </asp:RadioButtonList>
                        </div>
                    </dd>
                </dl>
                <dl>
                    <dt>链接地址</dt>
                    <dd>
                        <asp:TextBox ID="txtUrl" runat="server" CssClass="form-control"></asp:TextBox>
                    </dd>
                </dl>
                <dl>
                    <dt>版本</dt>
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
