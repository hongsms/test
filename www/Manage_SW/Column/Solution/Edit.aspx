<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Edit.aspx.cs" Inherits="Manage_SW_Column_Information_Edit" %>

<!DOCTYPE html>
<html lang="zh-CN">
<head>
    <title>玩牌特工管理系统</title>
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
    <script src="/Manage_SW/style/My97DatePicker/WdatePicker.js" type="text/javascript"></script>
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
            //初始化上传控件
            $(".upload-img").InitUploader();
            $(".upload-video").InitUploader({ filetype: "file", filesize: 100 * 1024, filetypes: "rar,zip,doc,xls,swf,pdf,ppt,docx,xlsx,pptx,flv,mp4" });

            $(".upload-files").InitUploader({ btntext: "批量上传", multiple: true, filetype: "file", filetypes: "rar,zip,doc,xls,swf,pdf,ppt,docx,xlsx,pptx,flv,mp4" });

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
                        <span class="Validform_checktip">上传的文件限制大小：100M；</span>
                    </dd>
                </dl>
                 <dl <%=IsType != 1 ? "style=\"display: none;\"" : ""%>>
                    <dt>所属类别</dt>
                    <dd>
                        <asp:DropDownList ID="ddlBaseType" runat="server" CssClass="selectpicker form-control">
                        </asp:DropDownList>
                    </dd>
                </dl>
                  <dl class="spec-bind">
                    <dt>绑定产品</dt>
                    <dd>
                        <asp:ListBox ID="lbTagsBrand" runat="server" Height="350" SelectionMode="Multiple" CssClass="selectpicker form-control">
                        </asp:ListBox>
                    </dd>
                </dl>
                <dl style="display: none">
                    <dt>奖金</dt>
                    <dd>
                        <asp:TextBox ID="txtSource" runat="server" CssClass="form-control" Text=""></asp:TextBox>
                    </dd>
                </dl>
                <dl style="display: none">
                    <dt>地点</dt>
                    <dd>
                        <asp:TextBox ID="txtSubTitle" runat="server" CssClass="form-control"></asp:TextBox>
                    </dd>
                </dl>
                 <dl style=" display:none">
                    <dt>作者</dt>
                    <dd>
                        <asp:TextBox ID="txtAuthor" runat="server" CssClass="form-control" Text=""></asp:TextBox>
                    </dd>
                </dl>
               
                <dl <%=IsLink != 1 ? "style=\"display:none;\"" : ""%>>
                    <dt>链接地址</dt>
                    <dd>
                        <asp:TextBox ID="txtLink" runat="server" Text="" CssClass="form-control"></asp:TextBox>
                        <span class="Validform_checktip">注：链接必须带 http://</span>
                    </dd>
                </dl>
                <script type="text/javascript">
                    $(function () {
                        $(document).on("click", ".addattr", function () {
                            var self = $(this),
                                    divTableAttr = self.next(".divtableattr");
                            ;
                            if (divTableAttr.find(".table tbody tr").length < 1) {
                                divTableAttr.show();
                            }
                            var attrhtml = "<tr>";

                            if (self.attr("pid") == "JFP") {
                                attrhtml += "<td><input type=\"text\" class=\"form-control\" name=\"AttrValue1\" style=\"width: 100%;\"  datatype=\"*\">";
                                attrhtml += "<input type=\"hidden\" name=\"AttrID\"><input type=\"hidden\"  name=\"hidModel\" value=\"JFP\"><input type=\"hidden\"  name=\"AttrValue3\" ><input type=\"hidden\"  name=\"AttrValue4\" ></td>";
                                attrhtml += "<td><input type=\"text\" class=\"form-control\" name=\"AttrValue2\" style=\"width: 100%;\"  datatype=\"*\"></td>";
                                attrhtml += "<td><a class=\"delattr\" title=\"删除\" onclick=\"delattr(this)\" href=\"javascript:void(0);\"><span class=\"glyphicon glyphicon-minus\"></span></a></td>";

                            }
                            else if (self.attr("pid") == "MZJG") {
                                attrhtml += "<td><input type=\"text\" class=\"form-control\" name=\"AttrValue1\" style=\"width: 100%;\"  datatype=\"*\">";
                                attrhtml += "<input type=\"hidden\" name=\"AttrID\"><input type=\"hidden\"  name=\"hidModel\" value=\"MZJG\"></td>";
                                attrhtml += "<td><input type=\"text\" class=\"form-control\" name=\"AttrValue2\" style=\"width: 100%;\"  datatype=\"*\"></td>";
                                attrhtml += "<td><input type=\"text\" class=\"form-control\" name=\"AttrValue3\" style=\"width: 100%;\"  datatype=\"*\"></td>";
                                attrhtml += "<td><input type=\"text\" class=\"form-control\" name=\"AttrValue4\" style=\"width: 100%;\"  datatype=\"*\"></td>";
                                attrhtml += "<td><a class=\"delattr\" title=\"删除\" onclick=\"delattr(this)\" href=\"javascript:void(0);\"><span class=\"glyphicon glyphicon-minus\"></span></a></td>";
                            }
                            else {
                                attrhtml += "<td><input type=\"text\" class=\"form-control\" name=\"AttrValue1\" style=\"width: 100%;\"  datatype=\"*\">";
                                attrhtml += "<input type=\"hidden\" name=\"AttrID\"><input type=\"hidden\"  name=\"hidModel\" value=\"JLQ\"><input type=\"hidden\"  name=\"AttrValue4\" ></td>";
                                attrhtml += "<td>" + '<%=gjhtml %>' + "</td>";
                                attrhtml += "<td><input type=\"text\" class=\"form-control\" name=\"AttrValue3\" style=\"width: 100%;\"  datatype=\"*\"></td>";
                                attrhtml += "<td><a class=\"delattr\" title=\"删除\" onclick=\"delattr(this)\" href=\"javascript:void(0);\"><span class=\"glyphicon glyphicon-minus\"></span></a></td>";
                            }



                            attrhtml += "</tr>";
                            divTableAttr.find(".table tbody").append(attrhtml);
                        });
                    });
                    function delattr(obj) {
                        var self = $(obj);
                        self.parents("tr").remove();
                        var divTableAttr = self.parents(".divtableattr");
                        if (divTableAttr.find(".table tbody tr").length < 1) {
                            divTableAttr.hide();
                        }
                    }
                      
                </script>
                <dl>
                    <dt>相关链接</dt>
                    <dd>
                        <button class="btn btn-default addattr" type="button" style="border-radius: 0;" pid="JFP">
                            <span class="glyphicon glyphicon-plus"></span>添加</button>
                        <div class="table-list divtableattr" style="width: 50%; margin-top: 10px; margin-bottom: 5px;">
                            <table class="table">
                                <thead>
                                    <tr>
                                        <th style="width: 10%;">
                                            链接标题
                                        </th>
                                        <th style="width: 15%;">
                                            链接地址
                                        </th>
                                        <th style="width: 5%;">
                                            操作
                                        </th>
                                    </tr>
                                </thead>
                                <tbody>
                                    <asp:Repeater ID="rptJFP" runat="server">
                                        <ItemTemplate>
                                            <tr>
                                                <td>
                                                    <input type="text" class="form-control" value="<%#Eval("AttrValue1") %>" name="AttrValue1"
                                                        style="width: 100%;" datatype="*">
                                                    <input type="hidden" value="<%#Eval("ID") %>" name="AttrID">
                                                    <input type="hidden" value="<%#Eval("Model") %>" name="hidModel">
                                                    <input type="hidden" name="AttrValue3">
                                                    <input type="hidden" name="AttrValue4">
                                                </td>
                                                <td>
                                                    <input type="text" class="form-control" value="<%#Eval("AttrValue2") %>" name="AttrValue2"
                                                        style="width: 100%;" datatype="*">
                                                </td>
                                                <td>
                                                    <a class="delattr" title="删除" onclick="delattr(this)" href="javascript:void(0);"><span
                                                        class="glyphicon glyphicon-minus"></span></a>
                                                </td>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </tbody>
                            </table>
                        </div>
                    </dd>
                </dl>
                <dl <%=IsState != 1 ? "style=\"display: none;\"" : ""%>>
                    <dt>属性</dt>
                    <dd>
                        <div class="btn-group btn-check" data-toggle="buttons">
                            <asp:CheckBoxList ID="cblShow" runat="server" RepeatColumns="4" RepeatLayout="Flow">
                                <asp:ListItem Value="1" Selected="True" class="btn btn-primary">显示</asp:ListItem>
                                <%--<asp:ListItem Value="2" class="btn btn-primary">置顶首页</asp:ListItem>--%>
                                <asp:ListItem Value="3" class="btn btn-primary">推荐</asp:ListItem>
                            </asp:CheckBoxList>
                        </div>
                    </dd>
                </dl>
              
                <dl>
                    <dt>排序数字</dt>
                    <dd>
                        <asp:TextBox ID="txtOrderBy" runat="server" Width="80" Text="9999" CssClass="form-control"
                            datatype="n"></asp:TextBox><span class="Validform_checktip">*数字，越大越向前</span>
                    </dd>
                </dl>
                <dl <%=IsBrowseCount != 1 ? "style=\"display: none;\"" : ""%>>
                    <dt>浏览次数</dt>
                    <dd>
                        <asp:TextBox ID="txtBrowseCount" runat="server" Width="80" Text="0" CssClass="form-control"></asp:TextBox><span
                            class="Validform_checktip">点击浏览该信息自动+1</span>
                    </dd>
                </dl>
                <dl>
                    <dt>发布时间</dt>
                    <dd>
                        <div class="form-group has-feedback" style="width: 180px; display: inline-block;">
                            <input type="text" class="form-control" runat="server" id="txtAddDate" onclick="WdatePicker({dateFmt:'yyyy-MM-dd HH:mm:ss'})"
                                style="width: 180px;" />
                            <span class="glyphicon glyphicon-calendar form-control-feedback" style="color: #3f93ed;">
                            </span>
                        </div>
                    </dd>
                </dl>
                <dl <%=IsInfo != 1 ? "style=\"display: none;\"" : ""%>>
                    <dt>内容摘要</dt>
                    <dd>
                        <asp:TextBox ID="txtIntroduction" runat="server" TextMode="MultiLine" Width="500"
                            Height="80" CssClass="form-control"></asp:TextBox>
                    </dd>
                </dl>
                <dl <%=IsContent != 1 ? "style=\"display: none;\"" : ""%>>
                    <dt>内容描述</dt>
                    <dd>
                        <asp:TextBox CssClass="editor" ID="txtContent2" runat="server" TextMode="MultiLine"></asp:TextBox>
                        <%--<Editor:UEditor ID="txtContent1" runat="server"></Editor:UEditor>--%>
                    </dd>
                </dl>
                <dl <%=IsContent != 1 ? "style=\"display: none;\"" : ""%>>
                    <dt>赛程表</dt>
                    <dd>
                        <asp:TextBox CssClass="editor" ID="txtContent1" runat="server" TextMode="MultiLine"></asp:TextBox>
                        <%--<Editor:UEditor ID="txtContent1" runat="server"></Editor:UEditor>--%>
                    </dd>
                </dl>
                <dl <%=IsFiles != 1 ? "style=\"display:none;\"" : ""%>>
                    <dt>文件</dt>
                    <dd>
                        <div class="upload-box upload-files" model="XTWJ">
                        </div>
                        <div class="file-list">
                            <ul>
                                <asp:Repeater ID="rptFileList" runat="server">
                                    <ItemTemplate>
                                        <li>
                                            <input type="hidden" name="hid_photo_name" value="<%#Eval("ID")%>|<%#Eval("OriginalUrl")%>|<%#Eval("ThumbUrl")%>" />
                                            <input type="hidden" name="hid_photo_remark" value="<%#Eval("Info")%>" />
                                            <input type="hidden" name="hid_photo_Title" value="<%#Eval("Title")%>" />
                                            <input type="hidden" name="hid_photo_ViceTitle" value="<%#Eval("ViceTitle")%>" />
                                            <input type="hidden" name="hid_photo_model" value="<%#Eval("Model")%>" />
                                            <a href="<%#Eval("OriginalUrl")%>" target="_blank">
                                                <%#Eval("Title")%></a> <a href="javascript:;" onclick="delImg(this);">删除</a>
                                        </li>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </ul>
                        </div>
                    </dd>
                </dl>
                <dl <%=IsAlbum != 1 ? "style=\"display:none;\"" : ""%>>
                    <dt>赛事图片</dt>
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
