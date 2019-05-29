<%@ Page Language="C#" AutoEventWireup="true" CodeFile="NewsInfo.aspx.cs" Inherits="cn_NewsInfo" %>

<!DOCTYPE html>
<html lang="zh-cn">
<head id="Head1" runat="server">
    <%@ Register Src="UserControl/_uchead.ascx" TagName="_uchead" TagPrefix="UserControl" %>
    <UserControl:_uchead ID="_uchead1" runat="server" />

    <script src="dispose.js" type="text/javascript"></script>
    <link href="/Web/style/lib/sweet-alert.css" rel="stylesheet" type="text/css" />
    <script src="/Web/style/lib/sweet-alert.min.js" type="text/javascript"></script>
    <link href="/plug-in/Validform_UI/css/style.5.1.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="/plug-in/Validform_UI/js/Validform_v5.3.2_min.js"></script>
    <script type="text/javascript" src="/plug-in/Validform_UI/js/Validform_v5.3.2_rule.js"></script>
     <style type="text/css">
        .edit img {
            margin-bottom:10px;
        }

    </style>
</head>
<body>
    <!--Header-->
    <%@ Register Src="UserControl/_ucheader.ascx" TagName="_ucheader" TagPrefix="UserControl" %>
    <UserControl:_ucheader ID="_ucheader1" runat="server" />
    <!--Banner-->
    <div class="banner" style="background-image: url(<%=MBaseType.Image %>);">
    </div>
    <!--Main-->
    <main class="main">
        <div class="main-home sm-dn">
            <div class="mauto">
                <h2 class="fl">
                    <em><%=MBaseType.Title %></em>
                    <span><%=MBaseType.IncludeType%></span>
                </h2>
                <%@ Register Src="UserControl/TopNavigation.ascx" TagName="TopNavigation" TagPrefix="UserControl" %>
                <UserControl:TopNavigation ID="TopNavigation1" runat="server" />
            </div>
        </div>
        <%@ Register Src="UserControl/ucLeft.ascx" TagName="ucLeft" TagPrefix="UserControl" %>
        <UserControl:ucLeft ID="ucLeft1" runat="server" />
        <div class="news-info content">
            <div class="pt50 pb50 mauto ov clearfix">
                <div class="rel">
                    <a href="<%=strLink %>" class="return">返回列表</a>
                    <div class="tt1 tac">
                        <h3 class="f24 fb pt35"><%=ModelInfo.Title %></h3>
                        <span class="pt10 pb30">时间：<%=ModelInfo.AddDate.ToString("yyyy-MM-dd") %>来源：<%=ModelInfo.Source %></span>
                    </div>
                    <!-- <div class="news-share">
                        <span class="fl">分享:</span>
                        <div class="fl bdsharebuttonbox bdshare-button-style0-16" data-bd-bind="1501606084965">
                            <a class="a1" href="" data-cmd="weixin" title="分享到微信"></a>
                            <a class="a2" href="" data-cmd="tsina" title="分享到新浪微博"></a>
                            <a class="a3" href="" data-cmd="tqq" title="腾讯微博"></a>
                        </div>
                    </div> -->
                </div>
                <div class="edit li30 f16 c6 pt15 pb15">
                    <%=ModelInfo.Content1 %>
                </div>


                <div class="b-con pt30 clearfix">
                    <div class="page lg-6 sm-12">
                        <%=LastInfo %>
                        <%=NextInfo %>
                    </div>
                    <div class="lg-6 pt10 sm-12 sm-p0 clearfix">
                        <a href="<%=strLink %>" class="b-return fr sm-dn">返回列表</a>

                        <div class="fr c6 bdsharebuttonbox bdshare-button-style0-16" data-bd-bind="1501606084965">
                            <a href="#" class="bds_more" data-cmd="more">分享</a>
                        </div>
                    </div>
                </div>

                <div class="comment pl40 pr40 pt45 pb50 sm-mt40">
                    <h3 class="mb15">评论留言</h3>
                    <div id="comments"></div>


                    <form action="">
                        <div class="item2 item lg-6 sm-12 clearfix" style="width: 100%">
                            <textarea id="Content"></textarea>
                        </div>
                        <div class="item2 item lg-6 sm-12 clearfix" style="width: 100%; margin-top:15px; margin-bottom:15px">

                            <div class="txt-w fl clearfix">
                                <input type="text" class="txt fl" style="width: 100px; margin-right: 0px" placeholder="请输入验证码" id="txtCode" name="txtCode" datatype="*"
                                    nullmsg="验证码不能为空">
                                <div class="img fl ml10" style="padding-top:7px">
                                    <img id="imgValidateCode" src="/ValidateCode.aspx" alt="" class="wh1" onclick="javascript:document.getElementById('imgValidateCode').src='/ValidateCode.aspx?id='+Math.random();return false;">
                                </div>

                            </div>


                        </div>

                        <input type="hidden" id="FromID" value="<%=ModelInfo.ID %>" />
                        <input type="hidden" id="FromName" value="<%=ModelInfo.Title %>" />
                        <div class="btn mt20">
                            <%-- <%if (UserRoot.GetUserID == -1)
                                { %> <input type="button" value="提交评论" class="login-btn">                           
                            <%}else { %>--%>
                            <input type="button" value="提交评论" onclick="AddMessage()">
                            <%-- <%} %>--%>
                        </div>
                    </form>
                </div>

            </div>
        </div>
    </main>




    <script type="text/javascript">

        var AddMessage = function () {
            var FromID = $("#FromID").val();
            var FromName = $("#FromName").val();

           
            var Content = $("#Content").val();
            if (Content == "") {
                alert("请输入评论内容！");
                return;
            }

            var txtCode = $("#txtCode").val();
            if (txtCode == "") {
                alert("请输入验证码！");
                return;
            }



            webcommon.ajax("Ajax.ashx", {
                action: "AddMessage", FromID: escape(FromID), FromName: escape(FromName),
                Content: escape(Content), CodeKey: escape(txtCode)
            }, function (data) {
                var state = $("response state", data).text();
                if (state == "yes") {
                    alert("留言评论成功，请等待管理员查看！");
                    $("#Content").val("");
                    $("#txtCode").val("");
                } else if (state == "login") {
                    alert($("response errorinfo", data).text());
                    $(".login").fadeIn();
                }
                else {
                    alert($("response errorinfo", data).text());
                    result = false;
                }
            }, false, false, null, "xml");

          document.getElementById('imgValidateCode').src = '/ValidateCode.aspx?id=' + Math.random(); return false;
        }
        var PageComments = function (page) {
            var FromID = $("#FromID").val();
            $.ajax({
                url: "Ajax.ashx",
                type: "POST",
                cache: false,
                data: { action: "PageComments", FromID: FromID, Model: "Comments", page: page },
                success: function (ReturnData) {

                    $("#comments").html(ReturnData.toString());

                }
            });
        }
        PageComments(1);
    </script>
      <script src="/js/webcommon.js" type="text/javascript"></script>
    <%--    <div class="register member-fix wh1 vcs">
        <div class="wh1 vcs">
            <div class="con baf rel pl45 pr45 pt25 pb35">
                <h3>会员注册</h3>
                <form id="form2" class="pt35 clearfix">
                    <div class="item lg-6 sm-12 clearfix">
                        <label for="" class="fl">用户名</label>
                        <div class="info fl clearfix">
                            <input type="text" placeholder="请输入用户名" class="txt fl" id="UserNamePhone" name="UserNamePhone" datatype="*" nullmsg="用户名不能为空"
                                sucmsg="用户名还未被使用，可以注册！" ajaxurl="Validform.ashx">
                            <strong class="fl">*</strong>
                        </div>
                    </div>
                    <div class="item lg-6 sm-12 clearfix">
                        <label for="" class="fl">姓名</label>
                        <div class="info fl clearfix">
                            <input type="text"  class="txt fl" id="RealName">
                            <strong class="fl">*</strong>
                        </div>
                    </div>
                    <div class="item lg-6 sm-12 clearfix">
                        <label for="" class="fl">密码</label>
                        <div class="info fl clearfix">
                            <input type="password" name="Password" id="Password" placeholder="密码范围在6-20位之间" errormsg="密码范围在6-20位之间" datatype="*6-20" nullmsg="登陆密码不能为空" class="txt fl">
                            <strong class="fl">*</strong>
                        </div>
                    </div>
                    <div class="item lg-6 sm-12 clearfix">
                        <label for="" class="fl">确认密码</label>
                        <div class="info fl clearfix">
                            <input type="password" datatype="*" errormsg="两次输入的密码不一致！" recheck="Password" nullmsg="确认密码不能为空" class="txt fl">
                            <strong class="fl">*</strong>
                        </div>
                    </div>
                    <div class="item lg-6 sm-12 clearfix">
                        <label for="" class="fl">手机号</label>
                        <div class="info fl clearfix">
                            <input type="text" class="txt fl" placeholder="请输入常用手机号" name="txtmobile" datatype="n11-11" errormsg="手机号格式不正确" nullmsg="手机号不能为空">
                            <strong class="fl">*</strong>
                        </div>
                    </div>

                    <div class="item2 item lg-6 sm-12 clearfix">
                        <label for="" class="fl">验证码</label>
                        <div class="info fl clearfix">
                            <div class="txt-w fl clearfix">
                                <input type="text" class="txt fl" placeholder="请输入验证码" id="txtCode" name="txtCode" datatype="*"
                                    nullmsg="验证码不能为空">
                                <div class="img fl ml10">
                                    <img id="imgValidateCode" src="/ValidateCode.aspx" alt="" class="wh1">
                                </div>
                                <a href="javascript:" class="fr" onclick="javascript:document.getElementById('imgValidateCode').src='/ValidateCode.aspx?id='+Math.random();return false;">
                                    <img src="images/login_icon1.png" alt=""></a>
                            </div>
                            <strong class="fl">*</strong>
                        </div>
                    </div>

                    <div class="clearfix"></div>
                    <div class="btn rel tac">
                        <input type="submit" value="注 册">
                        <div class="abs">已有账号？立刻<a href="javascript:;" class="btn btn2">登录</a></div>
                        <p id="objtip" class="objtip" style="color: #FF0000; width: 100%; font-size: 14px; padding-top: 2px; text-align: center;">
                        </p>
                    </div>

                </form>
                <div class="hide abs"></div>
            </div>
        </div>
    </div>

    <div class="login member-fix wh1">
        <div class="wh1 vcs">
            <div class="con baf rel pl45 pr45 pt25 pb15">
                <h3>会员登录</h3>
                <form action="" class="pt30">
                    <div class="item clearfix">
                        <label for="" class="fl">用户名</label>
                        <div class="info fr">
                            <input type="text" class="txt" placeholder="请输入用户名" name="txtname" id="txtname">
                        </div>
                    </div>
                    <div class="item clearfix">
                        <label for="" class="fl">密码</label>
                        <div class="info fr">
                            <input type="password" class="txt" placeholder="登录密码" name="txtpwd" id="txtpwd">
                        </div>
                    </div>

                    <div class="item2 item clearfix">
                        <label for="" class="fl"></label>
                        <div class="info fr">
                            <div class="fl">                               
                            </div>
                            <div class="fr">
                                <a href="javascript:;" class="btn btn1">注册用户</a>
                            </div>
                        </div>
                    </div>
                    <div class="btn item clearfix">
                        <label for="" class="fl"></label>
                        <div class="info fr">
                            <input type="button" value="登 录" onclick="Login();">
                        </div>
                    </div>
                </form>
                <div class="hide abs"></div>
            </div>
        </div>
    </div>
    <script>
        $(function () {
            $(".login-btn").click(function () {
                $(".login").fadeIn();
            });
            $(".login .btn1").click(function () {
                $(".member-fix").hide();
                $(".register").show();
            });
            $(".register .btn2").click(function () {
                $(".member-fix").hide();
                $(".login").show();
            });
            $(".member-fix .hide").click(function () {
                $(this).parents(".member-fix").fadeOut();
            });
        });
    </script>


  
    <script type="text/javascript">
        $(function () {

            var myForm = $("#form2").Validform({
                btnSubmit: ".button1",
                btnReset: ".button2",
                showAllError: false,
                ignoreHidden: false,
                tiptype: function (msg, o) {
                    if (!o.obj.is("form")) {
                        var objtip = $("#objtip");
                        //cssctl(objtip, o.type);                       
                        if (o.type == 1 || o.type == 3) {
                            objtip.text(msg).css({ "display": "block" });
                        } else {
                            objtip.hide();
                        }
                    }
                }
                 ,
                beforeSubmit: function (curform) {
                    var UserNamePhone = $("#UserNamePhone").val();
                    var RealName = $("#RealName").val();
                    var Password = $("#Password").val();
                    var txtmobile = $("#txtmobile").val();
                    var ImgCode = $("#txtCode").val();

                    var result = false;
                    webcommon.ajax("Ajax.ashx", {
                        action: "Register", UserNamePhone: escape(UserNamePhone), RealName: escape(RealName),
                        txtpwd: escape(Password), txtmobile: escape(txtmobile), ImgCode: escape(ImgCode)
                    }, function (data) {
                        var state = $("response state", data).text();
                        if (state == "yes") {
                            alert("会员注册成功！");
                            location.reload();
                        }
                        else {
                            alert($("response errorinfo", data).text());
                            result = false;
                        }
                    }, false, false, null, "xml");
                    return false;
                }
            });
        })
    </script>--%>


    <!--Footer-->
    <%@ Register Src="UserControl/_ucfooter.ascx" TagName="_ucfooter" TagPrefix="UserControl" %>
    <UserControl:_ucfooter ID="_ucfooter1" runat="server" />
</body>
</html>
