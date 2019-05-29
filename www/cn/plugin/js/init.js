//载入资源(SCRIPT、CSS)文件
$.extend({
    include: function (includePath, file) {
        var files = typeof file == "string" ? [file] : file;
        for (var i = 0; i < files.length; i++) {
            var name = files[i];
            var att = name.split('.');
            var ext = att[att.length - 1].toLowerCase();
            var isCSS = ext == "css";
            var tag = isCSS ? "link" : "script";
            var attr = isCSS ? " type='text/css' rel='stylesheet' " : " type='text/javascript' ";
            var link = (isCSS ? "href" : "src") + "='" + includePath + name + "'";
            if ($(tag + "[" + link + "]").length == 0) $("head").append("<" + tag + attr + link + "></" + tag + ">");
        }
    }
});
$(function () {
    

    try {
        //初始化视频播放器
        var videoList = $("[src*='mp4']").hide();
        if (videoList && videoList.length > 0) {
            var playerPath = "/common/plugin/ckplayer6.6/ckplayer/";
            $.getScript(playerPath + "ckplayer.js", function () {
                videoList.each(function (i) {
                    var self = $(this), src = self.attr("src") + "?n=" + Math.random();
                    if (!air.isMobile()) {
                        var width = self.attr("width"), height = self.attr("height");
                        self.replaceWith("<div id=\"" + ("video-box-" + i) + "\" class=\"video-box\"></div>");
                        CKobject.embed(playerPath + 'ckplayer.swf', 'video-box-' + i, 'player_' + i, width, height, false, ({ f: src, c: 0 }));
                    } else {
                        self.replaceWith(" <video src=\"" + src + "\" width=\"100%\" height=\"auto\" controls preload></video>");
                    }
                });
            });
        }
        //初始化日期选择组件
        var date = $("[date]");
        if (date && date.length > 0) {
            $.include('/common/plugin/wap/datepicker/', ['datepicker.js']);
            date.click(function () { WdatePicker({ whyGreen: 'twoer', dateFmt: 'yyyy-MM-dd HH:mm' }); });
        }
        //初始化表单验证组件
        var form = $("form[valid]");
        if (form && form.length > 0) {
            $.include('/common/plugin/validform/', ['css/style.css', 'js/validform.js', 'js/rule.js']);
            if (form.attr("valid") != "-1") {
                form.Validform({
                    ignoreHidden:false,
                    beforeSubmit: function (curform) {
                        $(curform).find("[type='submit'],.submit").attr("disabled", "disabled").addClass("do");
                    }
                });
            }
        }
        //初始化区域\城市选择组件
        var area = $("[area]");
        if (area && area.length > 0) {
            $.include('/common/plugin/jquery.area/', ['js/city.min.1.js', 'js/cityselect.js']);
            area.each(function () {
                $(this).citySelect((new Function("return " + $(this).attr("param"))()));
            });
        }
        //初始化lazyload
        var imgLazy = $("img.lazy");
        if (imgLazy && imgLazy.length > 0) {
            $.include('/common/plugin/jquery.lazyload/', ['js/jquery.lazyload.js']);
        }
        //初始化评价星级插件
        var starBar = $(".starbar");
        if (starBar.length > 0) {
            $.getScript("/common/plugin/jquery.raty/js/jquery.raty.js", function () {
                starBar.raty({
                    size: 35,
                    score: starBar.attr("score")
                });
            });
        }
 
    } catch (e) {

    }
    
});