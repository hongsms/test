<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Edit.aspx.cs" Inherits="Manage_SW_Column_Link_Edit" %>

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
    <script src="/Manage_SW/style/js/jquery.minicolors.min.js"></script>
    <link href="/Manage_SW/style/css/jquery.minicolors.css" rel="stylesheet" />
    <script src="/Manage_SW/style/webuploader/webuploader.min.js" type="text/javascript"></script>
    <script src="/Manage_SW/style/js/uploader.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(function () {
            //获取菜单路径
            var obj = parent.$("#mainframe");
            if (obj.attr("menupath")) {
                var menupath = "<li class=\"active\">" + obj.attr("menupath").replace(/,/ig, "</li><li class=\"active\">") + "</li>";
                $(".breadcrumb").append(menupath);
            }

            //初始化上传控件
            $(".upload-img").InitUploader();

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
    <script>
        $(document).ready(function () {
            $('.color').each(function () {
                $(this).minicolors({
                    control: $(this).attr('data-control') || 'hue',
                    defaultValue: $(this).attr('data-defaultValue') || '',
                    inline: $(this).attr('data-inline') === 'true',
                    letterCase: $(this).attr('data-letterCase') || 'lowercase',
                    opacity: $(this).attr('data-opacity'),
                    position: $(this).attr('data-position') || 'bottom left',
                    change: function (hex, opacity) {
                        if (!hex) return;
                        if (opacity) hex += ', ' + opacity;
                        try {
                            console.log(hex);
                        } catch (e) { }
                    },
                    theme: 'bootstrap'
                });

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
                    <dt>名称</dt>
                    <dd>
                        <asp:TextBox ID="txtTitle" runat="server" CssClass="form-control" datatype="*"></asp:TextBox>
                    </dd>
                </dl>
                  <dl <%=IsSubTitle != 1 ? "style=\"display: none;\"" : ""%>>
                    <dt>副标题</dt>
                    <dd>
                        <asp:TextBox ID="txtFileName" runat="server" CssClass="form-control"></asp:TextBox>
                    </dd>
                </dl>
                <dl <%=IsImage != 1 ? "style=\"display: none;\"" : ""%>>
                    <dt>封面图片</dt>
                    <dd>
                        <asp:TextBox ID="txtImage" runat="server" CssClass="form-control upload-path" />
                        <div class="upload-box upload-img">
                        </div>
                        <span class="Validform_checktip">
                            <%=Size != "" ? "图片宽高" + Size + "，" : ""%>上传的图片限制大小：2M；格式：jpg/png/gif/bmp</span>
                    </dd>
                </dl>
                  <dl <%=IsImage1 != 1 ? "style=\"display: none;\"" : ""%>>
                    <dt>手机图片</dt>
                    <dd>
                        <asp:TextBox ID="txtImageMobile" runat="server" CssClass="form-control upload-path" />
                        <div class="upload-box upload-img">
                        </div>
                        <span class="Validform_checktip">
                            <%=Size != "" ? "图片宽高" + Size + "，" : ""%>上传的图片限制大小：2M；格式：jpg/png/gif/bmp</span>
                    </dd>
                </dl>
                <%if (IsLink == 1)
                  { %>
                <dl>
                    <dt>链接地址</dt>
                    <dd>
                        <asp:TextBox ID="txtWebUrl" runat="server" Text="#" CssClass="form-control" datatype="*"></asp:TextBox>
                        <span class="Validform_checktip">注：链接必须带 http://</span>
                    </dd>
                </dl>
                   <dl>
                    <dt>链接类型</dt>
                    <dd>
                        <div class="btn-group btn-radio" data-toggle="buttons">
                            <asp:RadioButtonList ID="rblLinkType" runat="server" RepeatDirection="Horizontal"
                                RepeatLayout="Flow">
                                <asp:ListItem Value="2" Selected="True" class="btn btn-primary">站内链接</asp:ListItem>
                                <asp:ListItem Value="3" class="btn btn-primary">外网链接</asp:ListItem>
                            </asp:RadioButtonList>
                        </div>
                    </dd>
                </dl>
                <%} %>
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
                    <dt>内容摘要</dt>
                    <dd>
                        <asp:TextBox ID="txtIntroduction" runat="server" TextMode="MultiLine" Width="500"
                            Height="80" CssClass="form-control"></asp:TextBox>
                    </dd>
                </dl>
                <dl>
                    <dt>排序数字</dt>
                    <dd>
                        <asp:TextBox ID="txtOrderBy" runat="server" Width="80" Text="99" CssClass="form-control"
                            datatype="n"></asp:TextBox><span class="Validform_checktip">*数字，越大越向前</span>
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
