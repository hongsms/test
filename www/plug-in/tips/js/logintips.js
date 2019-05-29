var tipsBox = '<div class="msgbox_layer_wrap"><span class="msgbox_layer" style="z-index: 10000;" id="mode_tips_v2"><span class="${scla}"></span><span class="gtl_info" ></span><span class="gtl_end"></span>	</span></div>';
var loadingBox = '<div class="loading_layer_wrap"><span class="loading_layer" style="z-index: 10000;" id="mode_tips_v2"><span class="gtl_ico_clear"></span><img src="/plug-in/tips/images/loading.gif" alt="">${info}<span class="gtl_end"></span></span></div>';
var confirmBox = '<div class="confirm_layer_wrap"><div class="confirm_con"><div class="confirm_text"></div><div class="confirm_bar"><a href="javascript:;"  class="msgbox_confirm_btn success"   >直接登录</a>&nbsp;&nbsp;<a href="javascript:;" class="msgbox_confirm_btn cancel" onclick="return false;">现在注册</a></div></div></div>';
var timerSkipBox = '<div style="" id="q_Msgbox" class="msgbox_layer_wrap"><span class="msgbox_layer" style="z-index: 10000;" id="mode_tips_v2"><span class="gtl_ico_clear"></span><img src="/images/loading.gif" alt="">${info}<span class="gtl_end"></span></span></div>';
var tipsBoxArray = new Array("gtl_ico_fail", "gtl_ico_succ", "gtl_ico_hits", "gtl_ico_clear");
var msgBoxArray = new Array("error", "success","none");
var msgBox2 = '<div id="msgbox2_wrap" class="msgbox2_layer_wrap"><div class="msgbox2_outer"><div class="msgbox2_box"><span class="msgbox2_title" >系统提示</span><a class="msgbox2_close" onclick="msgBox.hide();" >x</a></div><div class="msgbox2_con"><div class="msgbox2_text"><div class="info_box"><span class="info"></span></div><div class="ot_info"></div></div></div></div></div>';

var msgBox = new Object();

var tipsSt;
msgBox.tips = function (info, type, time, callback, ctime, overlayShow) {
    var msgbox_layer_wrap = $(window.top.document.body).find(".msgbox_layer_wrap");
    if (msgbox_layer_wrap.length <= 0) {
        msgbox_layer_wrap = $(tipsBox).appendTo(window.top.document.body);
    } else {
        msgbox_layer_wrap.show();
    }
    if (typeof (overlayShow) != "undefined" && overlayShow != false) {
        msgBox.createShadeDiv();
    }
    msgbox_layer_wrap = msgbox_layer_wrap.find(".msgbox_layer").find("span:eq(0)").removeAttr("class").attr("class", tipsBoxArray[type]).end().find("span:eq(1)").html(info).end().end();
    clearTimeout(tipsSt);
    tipsSt = setTimeout(function () {
        msgbox_layer_wrap.hide();
        msgBox.closeShadeDiv();
        if (typeof (callback) != "undefined") {
         
            setTimeout(callback, ctime);
        }
    }, time);

}

msgBox.show = function (title, type, info, ot_info,overlayShow) {
    var msgbox2_warp = $(window.top.document.body).find("#msgbox2_wrap");
    if (msgbox2_warp.length <= 0) {
        msgbox2_warp = $(msgBox2).appendTo(window.top.document.body);
    }
    msgbox2_warp.find(".msgbox2_title").text(title).end().find(".msgbox2_text .info_box").removeClass("error").removeClass("success").addClass(msgBoxArray[type]).end().find(".info").html(info).end().find(".ot_info").html(ot_info).end().show();
    if (typeof (overlayShow) != "undefined" && overlayShow!=false) {
        msgBox.createShadeDiv();
    }
}
msgBox.hide = function () {
    msgBox.closeShadeDiv();
    $("#msgbox2_wrap").hide();
}

//创建一个遮罩层
msgBox.createShadeDiv = function () {
    var shadeDiv = $(document.body).find("#shadeDiv");
    if (shadeDiv.length <= 0) {
        var sWidth, sHeight;
        sWidth = $(window.top.document).width();
        sHeight = $(window.top.document).height();
        $("<div id=\"shadeDiv\"></div>").css({ filter: "Alpha(opacity=20)", opacity: "0.3", width: sWidth, height: sHeight }).appendTo(window.top.document.body);
        $(window.top.document.body).attr("oncontextmenu", "return false");
    }
}
//关闭遮罩层
msgBox.closeShadeDiv = function () {
    $(window.top.document.body).find("#shadeDiv").remove();
    $(window.top.document.body).removeAttr("oncontextmenu");
}

//显示正在加载(info:加载信息;isShowShadeDiv:是否显示遮罩层)
msgBox.showLoading = function (info, isShowShadeDiv) {
    if ($(window.top.document.body).find(".loading_layer_wrap").length <= 0) {
        $(window.top.document.body).find(".msgbox_layer_wrap").hide();
        $(loadingBox.replace("${info}", (typeof (info) == "undefined") ? "正在处理，请稍候..." : info)).appendTo(window.top.document.body);
        if ((typeof (isShowShadeDiv) != "undefined") && isShowShadeDiv) {
            msgBox.createShadeDiv();
        }
    }
}
//关闭正在加载
msgBox.closeLoading = function () {
    $(window.top.document.body).find(".loading_layer_wrap").remove();
    msgBox.closeShadeDiv();
    
}
//关闭正在加载(info:提示文本;successCallback:确定时回调函数;cencelCallback:取消时回调函数)
msgBox.confirm = function (info, successCallback,cencelCallback) {
    var confirm_layer_wrap = $(window.top.document.body).find(".confirm_layer_wrap");
    if (confirm_layer_wrap.length <= 0) {
        confirm_layer_wrap = $(confirmBox).appendTo(window.top.document.body);
    } else {
        confirm_layer_wrap.show();
    }
    confirm_layer_wrap = confirm_layer_wrap.find(".confirm_text").html(info).end();
    var successBtn = confirm_layer_wrap.find("a.success");
    successBtn.unbind("click");
    successBtn.click(function () {
        confirm_layer_wrap.hide();
        msgBox.closeShadeDiv();
        if (typeof (successCallback) != "undefined") {
            successCallback();
        }
    });
    var cancelBtn = confirm_layer_wrap.find("a.cancel");
    cancelBtn.unbind("click");
    cancelBtn.click(function () {
        confirm_layer_wrap.hide();
        msgBox.closeShadeDiv();
        if (typeof (cencelCallback) != "undefined") {
            cencelCallback();
        }
    });
    msgBox.createShadeDiv();
}

//显示倒计时(seconds:秒数;info:倒计时文本;url:跳转地址;isShowShadeDiv:是否显示遮罩层)
var timerSeconds = 10;
msgBox.timerSkip = function (seconds, info, url, isShowShadeDiv) {
    timerSeconds = seconds;
    var confirm_layer_wrap = $(window.top.document.body).find(".msgbox_layer_wrap");
    if (confirm_layer_wrap.length <= 0) {
        confirm_layer_wrap = $(timerSkipBox.replace("${info}", (typeof (info) == "undefined") ? " 页面 <font color='red' >" + timerSeconds + "</font> 秒后跳转.." : info)).appendTo(window.top.document.body);
    } else {
        confirm_layer_wrap.show();
    }
    setInterval(function () {
        if (timerSeconds <= 0) {
            confirm_layer_wrap.remove();
            setTimeout(function(){
                document.location.href = url;
            },300);
        }
        confirm_layer_wrap = confirm_layer_wrap.find("font").text(timerSeconds).end();
        timerSeconds -- ;
    }, 1000);

    if ((typeof (isShowShadeDiv) != "undefined") && isShowShadeDiv) {
        msgBox.createShadeDiv();
    }
}

   