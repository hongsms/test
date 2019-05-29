String.prototype.replaceAll = function (reallyDo, replaceWith, ignoreCase) { if (!RegExp.prototype.isPrototypeOf(reallyDo)) { return this.replace(new RegExp(reallyDo, (ignoreCase ? "gi" : "g")), replaceWith) } else { return this.replace(reallyDo, replaceWith) } }

//回到顶部
//$(document).ready(function () { (function (d) { var k = d.scrollTo = function (a, i, e) { d(window).scrollTo(a, i, e) }; k.defaults = { axis: 'xy', duration: parseFloat(d.fn.jquery) >= 1.3 ? 0 : 1 }; k.window = function (a) { return d(window)._scrollable() }; d.fn._scrollable = function () { return this.map(function () { var a = this, i = !a.nodeName || d.inArray(a.nodeName.toLowerCase(), ['iframe', '#document', 'html', 'body']) != -1; if (!i) return a; var e = (a.contentWindow || a).document || a.ownerDocument || a; return d.browser.safari || e.compatMode == 'BackCompat' ? e.body : e.documentElement }) }; d.fn.scrollTo = function (n, j, b) { if (typeof j == 'object') { b = j; j = 0 } if (typeof b == 'function') b = { onAfter: b }; if (n == 'max') n = 9e9; b = d.extend({}, k.defaults, b); j = j || b.speed || b.duration; b.queue = b.queue && b.axis.length > 1; if (b.queue) j /= 2; b.offset = p(b.offset); b.over = p(b.over); return this._scrollable().each(function () { var q = this, r = d(q), f = n, s, g = {}, u = r.is('html,body'); switch (typeof f) { case 'number': case 'string': if (/^([+-]=)?\d+(\.\d+)?(px|%)?$/.test(f)) { f = p(f); break } f = d(f, this); case 'object': if (f.is || f.style) s = (f = d(f)).offset() } d.each(b.axis.split(''), function (a, i) { var e = i == 'x' ? 'Left' : 'Top', h = e.toLowerCase(), c = 'scroll' + e, l = q[c], m = k.max(q, i); if (s) { g[c] = s[h] + (u ? 0 : l - r.offset()[h]); if (b.margin) { g[c] -= parseInt(f.css('margin' + e)) || 0; g[c] -= parseInt(f.css('border' + e + 'Width')) || 0 } g[c] += b.offset[h] || 0; if (b.over[h]) g[c] += f[i == 'x' ? 'width' : 'height']() * b.over[h] } else { var o = f[h]; g[c] = o.slice && o.slice(-1) == '%' ? parseFloat(o) / 100 * m : o } if (/^\d+$/.test(g[c])) g[c] = g[c] <= 0 ? 0 : Math.min(g[c], m); if (!a && b.queue) { if (l != g[c]) t(b.onAfterFirst); delete g[c] } }); t(b.onAfter); function t(a) { r.animate(g, j, b.easing, a && function () { a.call(this, n, b) }) } }).end() }; k.max = function (a, i) { var e = i == 'x' ? 'Width' : 'Height', h = 'scroll' + e; if (!d(a).is('html,body')) return a[h] - d(a)[e.toLowerCase()](); var c = 'client' + e, l = a.ownerDocument.documentElement, m = a.ownerDocument.body; return Math.max(l[h], m[h]) - Math.min(l[c], m[c]) }; function p(a) { return typeof a == 'object' ? a : { top: a, left: a} } })(jQuery); });
//$(document).ready(function () { jQuery.fn.topLink = function (settings) { settings = jQuery.extend({ min: 1, fadeSpeed: 200, ieOffset: 50 }, settings); return this.each(function () { var el = $(this); el.css('display', 'none'); $(window).scroll(function () { if ($(window).scrollTop() >= settings.min) { el.fadeIn(settings.fadeSpeed) } else { el.fadeOut(settings.fadeSpeed) } }) }) } });
//$(document).ready(function () { $('#top-link').topLink({ min: 80, fadeSpeed: 500 }); $('#top-link').click(function (e) { e.preventDefault(); $.scrollTo(0, 300) }) });


$(function () {
      //全选/反选
    $("input[type='checkbox'][checkedAll='true']").change(function () { var checkedAll = $(this); $("input[type=checkbox][isItem='true'][groupNum=" + checkedAll.attr("groupNum") + "]").each(function () { if (!checkedAll.attr("checked")) { $(this).removeAttr("checked") } else { $(this).attr("checked", "checked") } }) });
    //隔行变色
    $(" tbody[islist='true'] tr ").hover(function () { $(this).addClass("rowhover"); }, function () { $(this).removeClass("rowhover"); });
    //单选或复选元素框点击后跳转到指定地址
    $("[clickskip='true']").find("input").click(function () { var $this_ = $(this), $url_ = $this_.attr("href"); if (typeof ($url_) != "undefined") { document.location.href = $url_ } });
    //下拉选择后跳转到指定地址
    $("div[clickskip='true']").find("select").change(function () { var $this_ = $(this), $url_ = $this_.attr("href"); if (typeof ($url_) != "undefined") { document.location.href = $url_.replace('{0}', $this_.val()) } });
    //初始化，选中下拉
    $("select[initselect='true']").each(function (i, n) { $(this).val(n.getAttribute("val")) });
      //初始化，选中复选框
    $("[initchecked='true']").each(function () { var selfa = $(this); selfa.click(function () { var selfb = $(this); selfb.val(selfb.prop("checked") ? 1 : 0); }).prop("checked", (selfa.val() != "0") ? true : false); });

});


var webcommon = new Object();
//页面跳转
webcommon.redirect = function (url) { url == null ? document.location.reload() : document.location.href = url; };
//创建追加表单元素
webcommon.createInput = function (type, id, name, value, appendTarget) { if (type == null) return; var hdnInput = $("#" + id); if (hdnInput.length == 0) { var inputStr = '<input type="' + type + '" '; if (id != null) { inputStr += ' id="' + id + '" ' } if (name != null) { inputStr += ' name="' + name + '" ' } if (value != null) { inputStr += ' value="' + value + '" />' } if (appendTarget != null) { $(inputStr).appendTo(appendTarget) } else { return inputStr } } else { hdnInput.val(value) } };
//回车提交
webcommon.keyUpSubmitForm = function (event, callback) { if (event.keyCode == 13) eval(callback); };
//提交表单
webcommon.submitForm = function (formID, actionURL, target) { var form = $(formID); if (actionURL != null) { form.attr("target", target) } if (actionURL != null) { form.attr("action", actionURL) } form.submit() };
//表单提交(延迟)
webcommon.LazySubmitForm = function (form) {
    setTimeout(function () { form.submit(); }, 50);
}
//ajax处理
webcommon.ajax = function (url, data, success_callback, async, iev, submitInput, dataType) {  jQuery.ajax({ url: url, beforeSend: function () {  if (submitInput != null) { $(submitInput).attr("disabled", "disabled") } }, complete: function () { if (submitInput != null) { setTimeout(function () { $(submitInput).removeAttr("disabled") }, 300) } }, cache: false, async: async, type: 'post', dataType: dataType, data: data, success: function (data) { success_callback(iev ? (new Function("", "return " + data))() : data) } }) };
//初始化选中元素
webcommon.initSelectById = function (id, value) { $(id).find("option").each(function (i, n) { n.selected = (n.value != value) ? false : true }) };
webcommon.initSelectByName = function (name, value) { $("select[name='" + name + "']").find("option").each(function (i, n) { n.selected = (n.value != value) ? false : true }) };
webcommon.initCheckBoxById = function (id, value) { $(id).attr("checked", (value == 0) ? false : true) };
webcommon.initCheckBox = function (name, value) { $("input:checkbox[name='" + name + "']").each(function (i, n) { n.checked = (n.value != value) ? false : true }) };
webcommon.initCheckBoxGourp = function (name, value) { var valueSp = ',' + value + ','; $("input:checkbox[name='" + name + "']").each(function (i, n) { n.checked = (valueSp.indexOf("," + n.value + ",") >= 0) ? true : false }) };
webcommon.initRadioByName = function (name, value) { if (value.toString() != "") { $("input:radio[name='" + name + "']").each(function (i, n) { n.checked = (n.value != value) ? false : true }) } };
//判断对象是否为空
webcommon.isNull = function (arg1) { return !arg1 && arg1 !== 0 && typeof arg1 !== "boolean" ? true : false }
webcommon.enlarge = { init: function (e) { var e = $(e); if (e.length > 0) { $('<div id="enlarge_images"></div>').appendTo(document.body); var ei = $("#enlarge_images").css({ "position": "absolute", "display": "none", "z-index": "2", "border": "5px solid #f4f4f4" }); $("img[enlarge='true']").mousemove(function (event) { event = event || window.event; var top = document.body.scrollTop + event.clientY + 10 + "px"; var left = document.body.scrollLeft + event.clientX + 10 + "px"; ei.css({ "left": left, "top": top }).html('<img src="' + this.src + '"  />').show() }).mouseout(function () { ei.html(null).hide() }) } } };
//模拟checkbox异步提交
webcommon.asyncCheckbox = function (postName, appendTarget) { $("input[type=hidden][name='" + postName + "']").remove(); $("input[type=checkbox][postName='" + postName + "']:checked").each(function (i, n) { webcommon.createInput("hidden", null, postName, $(this).attr("postValue"), appendTarget) }) };
//显示隐藏元素
webcommon.showElement = function (t, id) { $(t).hide(); $(id).show() };
//获取地址栏参数
webcommon.QueryString = function (name) { var reg = new RegExp("(^|&)" + name + "=([^&]*)(&|$)", "i"); var r = window.location.search.substr(1).match(reg); if (r != null) return unescape(r[2]); return null };
//TAB切换
webcommon.switchTab = function (id_prefix, index, on) { $("#Tab_" + id_prefix + "_" + index).addClass(on).siblings().removeClass(on); $("#List_" + id_prefix + "_" + index).fadeIn("fast").siblings().hide() }
webcommon.switchTab4 = function (i, cla) { jQuery(i).removeClass(cla).siblings().addClass(cla) }
webcommon.switchTab5 = function (i1, cla1, i2) { $(i1).addClass(cla1).siblings().removeClass(cla1).end().parents().find(i2).show().siblings().hide() }
//闪动
webcommon.shake = function (ele, cls, times) { ele = $(ele); var i = 0, t = false, o = ele.attr("class") + " ", c = "", times = times || 2; if (t) return; t = setInterval(function () { i++; c = i % 2 ? o + cls : o; ele.attr("class", c); if (i == 2 * times) { clearInterval(t); ele.removeClass(cls) } }, 200) };
//判断数值
webcommon.isNumber = function (value, minValue) { value = Number(value); minValue = Number(minValue); return (!isNaN(value) && value > minValue) ? true : false; }



//关闭弹出框
webcommon.closeFb = function (info, type, time, isReload) {  $.fancybox.close(); if (typeof (info) != "undefined" && info != "") { window.parent.msgBox.tips(info, type, time); if (typeof (isReload) != "undefined" && isReload) { setTimeout(webcommon.reload, 1000) } } }
//重新加载
webcommon.reload = function () { document.location.reload() }


webcommon.resetModelTag = function (modeHtml) {
   
    if (typeof (modeHtml) != "undefined" && modeHtml != "") {
        return modeHtml.replace("<!--", "").replace("-->", "").replaceAll("MTR", "tr").replaceAll("mtr", "tr").replaceAll("mtd", "td").replaceAll("MTD", "td");
    }
    return null;
};
//追加模板
webcommon.appendModel = function (e, modelItem, modelCenter, maxCount) {
 
    modelCenter = $(modelCenter),
    modelCurCount = modelCenter.children().length;
    if (maxCount - modelCurCount > 0) {
        if (modelCurCount + 1 == maxCount) {
            $(e).hide().children("span").addClass("dis");
        }
        $(webcommon.resetModelTag($(modelItem).html()).replace(/{i}/g, modelCurCount + 1)).appendTo(modelCenter);
    }
};
//删除模板
webcommon.removeModel = function (e, appendModel, delName, delID, target) {
    $(e).remove();
    if (delID != null) {
        common.createInput("hidden", null, delName, delID, target);
    }
    $(appendModel).show().children("span").removeClass("dis");
};




$(function () {

    //显示fancybox
    $("[ispop]").each(function () {

        var self = $(this);
        mode = self.attr("ispop");
        switch (mode) {
            case "1":
                $(self).fancybox({
                    'padding': '1px',
                    'overlayShow': true,
                    'transitionIn': 'none',
                    'transitionOut': 'none'
                });
                break;
            case "2":

                $(self).fancybox({
                    'titleShow': false,
                    'padding': '0',
                    'width': self.attr("width"),
                    'height': self.attr("height"),
                    'autoScale': false,
                    'transitionIn': 'none',
                    'transitionOut': 'none',
                    'type': 'iframe',
                    'scrolling': 'no',
                    'centerOnScroll': true,
                    'showCloseButton': false
                });
                break;
            case "3":
                $(self).fancybox({
                    'titleShow': false,
                    'padding': '3px',
                    'width': self.attr("width"),
                    'height': self.attr("height"),
                    'autoScale': false,
                    'transitionIn': 'none',
                    'transitionOut': 'none',
                    'type': 'swf',
                    'scrolling': 'no',
                    'centerOnScroll': false,
                    'showCloseButton': true
                });
                break
            case "4":
                $(self).fancybox({
                    'titleShow': true,
                    'padding': '3px',
                    'width': self.attr("width"),
                    'height': self.attr("height"),
                    'autoScale': false,
                    'transitionIn': 'none',
                    'transitionOut': 'none',
                    'type': 'iframe',
                    'centerOnScroll': true,
                    'overlayShow': true
                });
                break;
            case "5":
                $(self).fancybox({
                    'titleShow': false,
                    'padding': '3px',
                    'width': self.attr("width"),
                    'height': self.attr("height"),
                    'autoScale': false,
                    'transitionIn': 'none',
                    'transitionOut': 'none',
                    'type': 'iframe'
                });
                break;
            default:
                $(self).fancybox({
                    'titleShow': false,
                    'padding': '3px',
                    'transitionIn': 'none',
                    'transitionOut': 'none',
                    'centerOnScroll': true,
                    'overlayShow': true
                });
                break;
        }
    });



})