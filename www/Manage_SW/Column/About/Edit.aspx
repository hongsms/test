<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Edit.aspx.cs" Inherits="Manage_SW_Column_About_Edit" %>

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
            //初始化上传控件
            $(".upload-img").InitUploader();
            $(".upload-video").InitUploader({ filetype: "file", filesize: 100 * 1024, filetypes: "rar,zip,doc,xls,swf,pdf,ppt,docx,xlsx,pptx,flv,mp4" });

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
            //初始化上传控件
            $(".upload-album").InitUploader({ btntext: "批量上传", multiple: true, thumbnail: true, twidth: 200, theight: 200 });


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
                <li role="presentation" class="active"><a href="javascript:void(0);">基本信息</a></li>
            </ul>
            <div class="nav-tabs-content active">
                <dl>
                    <dt>内容标题</dt>
                    <dd>
                        <asp:TextBox ID="txtTitle" runat="server" CssClass="form-control" datatype="*"></asp:TextBox>
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
                 <dl <%=IsVideo != 1 ? "style=\"display: none;\"" : ""%>>
                    <dt>文件上传</dt>
                    <dd>
                        <asp:TextBox ID="txtVideo" runat="server" CssClass="form-control upload-path" />
                        <div class="upload-box upload-video">
                        </div>
                        <span class="Validform_checktip">上传的视频限制大小：100M；</span>
                    </dd>
                </dl>

                  <dl <%=IsInfo != 1 ? "style=\"display: none;\"" : ""%>>
                    <dt>内容摘要</dt>
                    <dd>
                      <asp:TextBox ID="txtIntroduction" runat="server" TextMode="MultiLine" Width="500"
                            Height="80" CssClass="form-control"></asp:TextBox>
                  
                     
                    </dd>
                </dl>

                <dl>
                    <dt>内容描述</dt>
                    <dd>
                        <asp:TextBox CssClass="editor" ID="txtContent" runat="server" TextMode="MultiLine"></asp:TextBox>
                    </dd>
                </dl>

                <dl <%=IsAlbum != 1 ? "style=\"display:none;\"" : ""%>>
                    <dt>相册</dt>
                    <dd>
                        <div class="upload-box upload-album" model="XWXC">
                        </div>
                        （上传的图片限制大小：2M；格式：jpg/png/gif/bmp）
                        <div class="photo-list">
                            <ul>
                                <asp:Repeater ID="rptPicList" runat="server">
                                    <ItemTemplate>
                                        <li>
                                            <input type="hidden" name="hid_photo_name" value="<%#Eval("ID")%>|<%#Eval("OriginalUrl")%>|<%#Eval("ThumbUrl")%>" />
                                            <input type="hidden" name="hid_photo_remark" value="<%#Eval("Info")%>" />
                                              <input type="hidden" name="hid_photo_Title" value="<%#Eval("Title")%>" />
                                            <input type="hidden" name="hid_photo_ViceTitle" value="<%#Eval("ViceTitle")%>" />
                                            <input type="hidden" name="hid_photo_model" value="<%#Eval("Model")%>" />
                                            <div class="img-box" onclick="setFocusImg(this);">
                                                <img src="<%#Eval("ThumbUrl")%>" bigsrc="<%#Eval("OriginalUrl")%>" />
                                                <span class="remark"><i>
                                                    <%#Eval("Info").ToString() == "" ? "暂无描述..." : Eval("Info").ToString()%></i></span>
                                            </div>
                                            <a href="javascript:;" onclick="setRemark(this);">描述</a> <a href="javascript:;" onclick="delImg(this);">
                                                删除</a> </li>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </ul>
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
