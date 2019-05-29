<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Edit.aspx.cs" Inherits="Manage_SW_Column_Admin_WebSite_Edit" %>

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
            $(".upload-img").InitUploader();
            //初始化编辑器
            var editor = KindEditor.create('.editor', {
                width: '98%',
                height: '350px',
                resizeType: 1,
                uploadJson: '/tools/upload_ajax.ashx?action=EditorFile&IsWater=0',
                fileManagerJson: '/tools/upload_ajax.ashx?action=ManagerFile',
                filterMode: false, //是否开启过滤模式
                allowFileManager: true
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
                <li role="presentation" class="active"><a href="javascript:void(0);">站点设置</a></li>
                <li role="presentation" style="display: none"><a href="javascript:void(0);">站点状态</a></li>
                <li role="presentation"><a href="javascript:void(0);">SEO设置</a></li>
                <li role="presentation" style="display: none"><a href="javascript:void(0);">数值设置</a></li>
            </ul>
            <div class="nav-tabs-content active">
                <dl>
                    <dt>站点名称</dt>
                    <dd>
                        <asp:TextBox ID="txtWebName" runat="server" CssClass="form-control" datatype="*"></asp:TextBox>
                    </dd>
                </dl>
                <dl>
                    <dt>公司名称</dt>
                    <dd>
                        <asp:TextBox ID="txtAttr1" runat="server" CssClass="form-control"></asp:TextBox>
                    </dd>
                </dl>
                <dl>
                    <dt>服务热线</dt>
                    <dd>
                        <asp:TextBox ID="txtHomePage" runat="server" CssClass="form-control"></asp:TextBox>
                    </dd>
                </dl>
                <dl>
                    <dt>地址</dt>
                    <dd>
                        <asp:TextBox ID="txtAttr2" runat="server" TextMode="MultiLine" Width="500" Height="80"
                            CssClass="form-control"></asp:TextBox>
                    </dd>
                </dl>
                <dl>
                    <dt>微信二维码</dt>
                    <dd>
                        <asp:TextBox ID="txtAttr9" runat="server" CssClass="form-control upload-path" />
                        <div class="upload-box upload-img">
                        </div>
                        <span class="Validform_checktip">上传的图片限制大小：2M；格式：jpg/png/gif/bmp</span>
                    </dd>
                </dl>

                <dl>
                    <dt>微博二维码</dt>
                    <dd>
                        <asp:TextBox ID="txtReceiveEmail" runat="server" CssClass="form-control upload-path" />
                        <div class="upload-box upload-img">
                        </div>
                        <span class="Validform_checktip">上传的图片限制大小：2M；格式：jpg/png/gif/bmp</span>
                    </dd>
                </dl>


                <dl>
                    <dt>QQ客服</dt>
                    <dd>
                        <asp:TextBox ID="txtAttr3" runat="server" CssClass="form-control"></asp:TextBox>
                    </dd>
                </dl>
                <dl>
                    <dt>新浪微博</dt>
                    <dd>
                        <asp:TextBox ID="txtAttr4" runat="server" CssClass="form-control"></asp:TextBox>
                    </dd>
                </dl>
                <dl>
                    <dt>淘宝链接</dt>
                    <dd>
                        <asp:TextBox ID="txtWebUrl" runat="server" CssClass="form-control"></asp:TextBox>
                    </dd>
                </dl>
                <dl>
                    <dt>京东链接</dt>
                    <dd>
                        <asp:TextBox ID="txtEmailName" runat="server" CssClass="form-control"></asp:TextBox>
                    </dd>
                </dl>
                <dl>
                    <dt>阿里巴巴</dt>
                    <dd>
                        <asp:TextBox ID="txtEmailPwd" runat="server" CssClass="form-control"></asp:TextBox>
                    </dd>
                </dl>
                <dl>
                    <dt>网站版权说明</dt>
                    <dd>
                        <asp:TextBox CssClass="editor" ID="txtCopyright" runat="server" TextMode="MultiLine"></asp:TextBox>
                    </dd>
                </dl>
            </div>
            <div class="nav-tabs-content" style="display: none">
                <dl>
                    <dt>网站状态</dt>
                    <dd>
                        <div class="btn-group btn-radio" data-toggle="buttons">
                            <asp:RadioButtonList ID="rblWebState" runat="server" RepeatColumns="2" RepeatLayout="Flow">
                                <asp:ListItem Value="1" Selected="True" class="btn btn-primary active">显示</asp:ListItem>
                                <asp:ListItem Value="0" class="btn btn-primary">关闭</asp:ListItem>
                            </asp:RadioButtonList>
                        </div>
                    </dd>
                </dl>
                <dl>
                    <dt>关闭提示</dt>
                    <dd>
                        <asp:TextBox CssClass="editor" ID="txtCloseInfo" runat="server" TextMode="MultiLine"></asp:TextBox>
                    </dd>
                </dl>
            </div>
            <div class="nav-tabs-content">
                <dl>
                    <dt>网站标题</dt>
                    <dd>
                        <asp:TextBox ID="txtTitle" runat="server" CssClass="form-control" datatype="*"></asp:TextBox>
                    </dd>
                </dl>
                <dl>
                    <dt>SEO关键字</dt>
                    <dd style="height: 80px; line-height: 80px;">
                        <asp:TextBox ID="txtKeywords" runat="server" TextMode="MultiLine" Width="500" Height="80"
                            CssClass="form-control"></asp:TextBox>
                    </dd>
                </dl>
                <dl>
                    <dt>SEO描述</dt>
                    <dd>
                        <asp:TextBox ID="txtDescription" runat="server" TextMode="MultiLine" Width="500"
                            Height="80" CssClass="form-control"></asp:TextBox>
                    </dd>
                </dl>
            </div>
            <div class="nav-tabs-content">
                <dl>
                    <dt>万吨锌锭年生产力</dt>
                    <dd>
                        <asp:TextBox ID="txtEmailSmtp" runat="server" CssClass="form-control"></asp:TextBox>
                    </dd>
                </dl>
            </div>
            <div class="nav-tabs-content" style="display: none">
                <dl>
                    <dt>积分兑现</dt>
                    <dd>
                        <div class="btn-group btn-radio" data-toggle="buttons">
                            <asp:RadioButtonList ID="rblIsIntegral" runat="server" RepeatColumns="2" RepeatLayout="Flow">
                                <asp:ListItem Value="1" class="btn btn-primary">开启</asp:ListItem>
                                <asp:ListItem Value="0" Selected="True" class="btn btn-primary active">关闭</asp:ListItem>
                            </asp:RadioButtonList>
                        </div>
                    </dd>
                </dl>
                <dl>
                    <dt>积分换算</dt>
                    <dd>
                        <asp:TextBox ID="txtIntegralConversion" runat="server" Width="80" CssClass="form-control"
                            datatype="n"></asp:TextBox>
                        <span class="Validform_checktip">*如设置1000，代表1000积分换算1块钱</span>
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
