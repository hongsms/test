$.fn.extend({ LimitInput: function () { $(this).keydown(function (event) { if ((45 < event.keyCode && event.keyCode < 58) || (95 < event.keyCode && event.keyCode < 106) || event.keyCode == 17 || event.keyCode == 16 || event.keyCode == 8 || (32 < event.keyCode && event.keyCode < 41)) { return } event.preventDefault() }) }, createInput: function (type, id, name, value, appendTarget) { if (type == null) return; var hdnInput = $("#" + id); if (hdnInput.length == 0) { var inputStr = '<input type="' + type + '" '; if (id != null) { inputStr += ' id="' + id + '" ' } if (name != null) { inputStr += ' name="' + name + '" ' } if (value != null) { inputStr += ' value="' + value + '" />' } if (appendTarget != null) { $(inputStr).appendTo(appendTarget) } else { return inputStr } } else { hdnInput.val(value) } }, removeInput: function (name) { $(name).remove() } });
String.prototype.replaceAll = function (reallyDo, replaceWith, ignoreCase) { if (!RegExp.prototype.isPrototypeOf(reallyDo)) { return this.replace(new RegExp(reallyDo, (ignoreCase ? "gi" : "g")), replaceWith) } else { return this.replace(reallyDo, replaceWith) } }
String.prototype.trim = function () { return this.replace(/^\s+|\s+$/g, ""); };
String.prototype.format = function (args) { var result = this; if (arguments.length > 0) { if (arguments.length == 1 && typeof (args) == "object") { for (var key in args) { if (args[key] != undefined) { var reg = new RegExp("({" + key + "})", "g"); result = result.replace(reg, args[key]) } } } else { for (var i = 0; i < arguments.length; i++) { if (arguments[i] != undefined) { var reg = new RegExp("({)" + i + "(})", "g"); result = result.replace(reg, arguments[i]) } } } } return result };
String.prototype.updateParams = function (pStr, vStr) {
    var query = this.substring(this.indexOf("?"));
    var newQuery = "";
    if (pStr && vStr) {
        var str = (query.substring(0, 1) == "?") ? query.substring(1) : query;
        var array = (str.indexOf("&")!=-1)?str.split('&'):[];
        for (var i = 0; i < array.length; i++) {
            if (pStr.indexOf(array[i].split('=')[0]) == -1) {
                newQuery += ((newQuery.indexOf("?") != -1) ? "&" : "?") + array[i]
            }
        }
        var pStrArray = pStr.split(',');
        var vStrArray = vStr.split(',');
        if (pStrArray.length == vStrArray.length) {
            for (var i = 0; i < pStrArray.length; i++) {
                newQuery += (newQuery.indexOf("?") != -1) ? "&" : "?";
                newQuery += pStrArray[i] + "=" + vStrArray[i]
            }
        }
        
        var url = this;
        if (url.indexOf("?") == -1) {
            return (url + newQuery);
        } else {
            return (url.substring(0, url.indexOf("?")) + newQuery);
        }
    }
    return null
};

(function ($) { $.fn.jdMarquee = function (option, callback) { if (typeof option == "function") { callback = option; option = {}; }; var s = $.extend({ deriction: "up", speed: 10, auto: false, width: null, height: null, step: 1, control: false, _front: null, _back: null, _stop: null, _continue: null, wrapstyle: "", stay: 5000, delay: 20, dom: "div>ul>li".split(">"), mainTimer: null, subTimer: null, tag: false, convert: false, btn: null, disabled: "disabled", pos: { ojbect: null, clone: null } }, option || {}); var object = this.find(s.dom[1]); var subObject = this.find(s.dom[2]); var clone; if (s.deriction == "up" || s.deriction == "down") { var height = object.eq(0).outerHeight(); var step = s.step * subObject.eq(0).outerHeight(); object.css({ width: s.width + "px", overflow: "hidden" }); }; if (s.deriction == "left" || s.deriction == "right") { var width = subObject.length * subObject.eq(0).outerWidth(); object.css({ width: width + "px", overflow: "hidden" }); var step = s.step * subObject.eq(0).outerWidth(); }; var init = function () { var wrap = "<div style='position:relative;overflow:hidden;z-index:1;width:" + s.width + "px;height:" + s.height + "px;" + s.wrapstyle + "'></div>"; object.css({ position: "absolute", left: 0, top: 0 }).wrap(wrap); s.pos.object = 0; clone = object.clone(); object.after(clone); switch (s.deriction) { default: case "up": object.css({ marginLeft: 0, marginTop: 0 }); clone.css({ marginLeft: 0, marginTop: height + "px" }); s.pos.clone = height; break; case "down": object.css({ marginLeft: 0, marginTop: 0 }); clone.css({ marginLeft: 0, marginTop: -height + "px" }); s.pos.clone = -height; break; case "left": object.css({ marginTop: 0, marginLeft: 0 }); clone.css({ marginTop: 0, marginLeft: width + "px" }); s.pos.clone = width; break; case "right": object.css({ marginTop: 0, marginLeft: 0 }); clone.css({ marginTop: 0, marginLeft: -width + "px" }); s.pos.clone = -width; break; }; if (s.auto) { initMainTimer(); object.hover(function () { clear(s.mainTimer); }, function () { initMainTimer(); }); clone.hover(function () { clear(s.mainTimer); }, function () { initMainTimer(); }); }; if (callback) { callback(); }; if (s.control) { initControls(); } }; var initMainTimer = function (delay) { clear(s.mainTimer); s.stay = delay ? delay : s.stay; s.mainTimer = setInterval(function () { initSubTimer() }, s.stay); }; var initSubTimer = function () { clear(s.subTimer); s.subTimer = setInterval(function () { roll() }, s.delay); }; var clear = function (timer) { if (timer != null) { clearInterval(timer); } }; var disControl = function (A) { if (A) { $(s._front).unbind("click"); $(s._back).unbind("click"); $(s._stop).unbind("click"); $(s._continue).unbind("click"); } else { initControls(); } }; var initControls = function () { if (s._front != null) { $(s._front).click(function () { $(s._front).addClass(s.disabled); disControl(true); clear(s.mainTimer); s.convert = true; s.btn = "front"; if (!s.auto) { s.tag = true; }; convert(); }); }; if (s._back != null) { $(s._back).click(function () { $(s._back).addClass(s.disabled); disControl(true); clear(s.mainTimer); s.convert = true; s.btn = "back"; if (!s.auto) { s.tag = true; }; convert(); }); }; if (s._stop != null) { $(s._stop).click(function () { clear(s.mainTimer); }); }; if (s._continue != null) { $(s._continue).click(function () { initMainTimer(); }); } }; var convert = function () { if (s.tag && s.convert) { s.convert = false; if (s.btn == "front") { if (s.deriction == "down") { s.deriction = "up"; }; if (s.deriction == "right") { s.deriction = "left"; } }; if (s.btn == "back") { if (s.deriction == "up") { s.deriction = "down"; }; if (s.deriction == "left") { s.deriction = "right"; } }; if (s.auto) { initMainTimer(); } else { initMainTimer(4 * s.delay); } } }; var setPos = function (y1, y2, x) { if (x) { clear(s.subTimer); s.pos.object = y1; s.pos.clone = y2; s.tag = true; } else { s.tag = false; }; if (s.tag) { if (s.convert) { convert(); } else { if (!s.auto) { clear(s.mainTimer); } } }; if (s.deriction == "up" || s.deriction == "down") { object.css({ marginTop: y1 + "px" }); clone.css({ marginTop: y2 + "px" }); }; if (s.deriction == "left" || s.deriction == "right") { object.css({ marginLeft: y1 + "px" }); clone.css({ marginLeft: y2 + "px" }); } }; var roll = function () { var y_object = (s.deriction == "up" || s.deriction == "down") ? parseInt(object.get(0).style.marginTop) : parseInt(object.get(0).style.marginLeft); var y_clone = (s.deriction == "up" || s.deriction == "down") ? parseInt(clone.get(0).style.marginTop) : parseInt(clone.get(0).style.marginLeft); var y_add = Math.max(Math.abs(y_object - s.pos.object), Math.abs(y_clone - s.pos.clone)); var y_ceil = Math.ceil((step - y_add) / s.speed); switch (s.deriction) { case "up": if (y_add == step) { setPos(y_object, y_clone, true); $(s._front).removeClass(s.disabled); disControl(false); } else { if (y_object <= -height) { y_object = y_clone + height; s.pos.object = y_object; }; if (y_clone <= -height) { y_clone = y_object + height; s.pos.clone = y_clone; }; setPos((y_object - y_ceil), (y_clone - y_ceil)); }; break; case "down": if (y_add == step) { setPos(y_object, y_clone, true); $(s._back).removeClass(s.disabled); disControl(false); } else { if (y_object >= height) { y_object = y_clone - height; s.pos.object = y_object; }; if (y_clone >= height) { y_clone = y_object - height; s.pos.clone = y_clone; }; setPos((y_object + y_ceil), (y_clone + y_ceil)); }; break; case "left": if (y_add == step) { setPos(y_object, y_clone, true); $(s._front).removeClass(s.disabled); disControl(false); } else { if (y_object <= -width) { y_object = y_clone + width; s.pos.object = y_object; }; if (y_clone <= -width) { y_clone = y_object + width; s.pos.clone = y_clone; }; setPos((y_object - y_ceil), (y_clone - y_ceil)); }; break; case "right": if (y_add == step) { setPos(y_object, y_clone, true); $(s._back).removeClass(s.disabled); disControl(false); } else { if (y_object >= width) { y_object = y_clone - width; s.pos.object = y_object; }; if (y_clone >= width) { y_clone = y_object - width; s.pos.clone = y_clone; }; setPos((y_object + y_ceil), (y_clone + y_ceil)); }; break; } }; if (s.deriction == "up" || s.deriction == "down") { if (height >= s.height && height >= s.step) { init(); } }; if (s.deriction == "left" || s.deriction == "right") { if (width >= s.width && width >= s.step) { init(); } } } })(jQuery);
(function () {  
             if (!window.air) {  
                 window.air = {};
             };  
             window.air = {
                         versions: function () {
                             var u = navigator.userAgent, app = navigator.appVersion;
                             return {//移动终端浏览器版本信息                                  
                                 trident: u.indexOf('Trident') > -1, //IE内核                                  
                                 presto: u.indexOf('Presto') > -1, //opera内核                                  
                                 webKit: u.indexOf('AppleWebKit') > -1, //苹果、谷歌内核                                  
                                 gecko: u.indexOf('Gecko') > -1 && u.indexOf('KHTML') == -1, //火狐内核                                 
                                 mobile: !!u.match(/AppleWebKit.*Mobile.*/)
                                         || !!u.match(/AppleWebKit/), //是否为移动终端                                  
                                 ios: !!u.match(/\(i[^;]+;( U;)? CPU.+Mac OS X/), //ios终端                  
                                 android: u.indexOf('Android') > -1 || u.indexOf('Linux') > -1, //android终端或者uc浏览器                                  
                                 iPhone: u.indexOf('iPhone') > -1 || u.indexOf('Mac') > -1, //是否为iPhone或者QQHD浏览器                     
                                 iPad: u.indexOf('iPad') > -1, //是否iPad        
                                 webApp: u.indexOf('Safari') == -1,//是否web应该程序，没有头部与底部 
                                 google: u.indexOf('Chrome') > -1
                             };
                         }(),
                         base: function () {
                             if (typeof (basePath) == 'undefined') {
                                 return null;
                             }
                             return basePath;
                         },
                         ajaxURL:function()
                         {
                             return (this.base() + '/tools/ajax.ashx');
                         },
                         undef: function (val,defaultVal) { 
                             if(typeof (defaultVal) != "string"){
                                 return (val === undefined || isNaN(val) || val<=0) ? defaultVal : val;
                             }else{
                                 return val === undefined ? defaultVal : val;
                             }
                         },
                         submitForm: function (formId, actionURL, target) {
                             var form = $(formId);
                             if (actionURL != null) {
                                 form.attr("target", target)
                             }
                             if (actionURL != null) {
                                 form.attr("action", actionURL)
                             }
                             setTimeout(function () {
                                 form.submit();
                             }, 50);
                         },
                         formToJson:function(form) {
				             var o = {};
                             var a = form.serializeArray();
                             $.each(a, function () {
                                 if (o[this.name] !== undefined) {
                                     if (!o[this.name].push) {
                                         o[this.name] = [o[this.name]];
                                     }
                                     o[this.name].push(this.value || '');
                                 } else {
                                     o[this.name] = this.value || '';
                                 }
                             });
                             return o;
                             },
                         createInput: function (type, id, name, value, target) {
                             if (!type) return;
                             var hdnInput = $("#" + id);
                             if (hdnInput.length == 0) {
                                 var inputStr = '<input type="' + type + '" ';
                                 if (id) {inputStr += ' id="' + id + '" '}
                                 if (name) {inputStr += ' name="' + name + '" '}
                                 if (value) {inputStr += ' value="' + value + '" />'}
                                 if (target) {$(inputStr).appendTo(target)
                                 }else {return inputStr}
                             } else {
                                 hdnInput.val(value);
                             }
                         },
                         eval:function(fn)
                         {
                             var Fn = Function;
                             return new Fn('return ' + fn)();
                         },
                         getVc:function(t) {
                             $(t).attr({
                                 src: this.base() + '/verify_code.do?id=' + Math.random()
                             });
                             return false;
                         },
                         initSelectById:function(id, value) {
                             $(id).find("option").each(function(i, n) {
                                 n.selected = (n.value != value) ? false : true
                             })
                         },
                         initSelectByName:function(name, value) {
                             $("select[name='" + name + "']").find("option").each(function(i, n) {
                                 n.selected = (n.value != value) ? false : true
                             })
                         },
                         initCheckBoxById:function(id, value) {
                             $(id).attr("checked", (value == 0) ? false : true)
                         },
                         initCheckBox:function(name, value) {
                             $("input:checkbox[name='" + name + "']").each(function(i, n) {
                                 n.checked = (n.value != value) ? false : true
                             })
                         },
                         initCheckBoxGourp:function(name, value) {
                             var valueSp = ',' + value + ',';
                             $("input:checkbox[name='" + name + "']").each(function(i, n) {
                                 n.checked = (valueSp.indexOf("," + n.value + ",") >= 0) ? true : false
                             })
                         },
                         initRadioByName:function(name, value) {
                             $("input:radio[name='" + name + "']").each(function(i, n) {
                                 n.checked = (n.value != value) ? false : true
                             })
                         },
                         isNull:function(val) {
                             switch (typeof(val)) {
                                 case "string":
                                     return val.trim().length == 0 ? true : false;
                                     break;
                                 case "number":
                                     return val == 0;
                                     break;
                                 case "object":
                                     return val == null;
                                     break;
                                 case "array":
                                     return val.length == 0;
                                     break;
                                 default:
                                     return true
                             }
                         },
                         isNumber:function(value,minValue) {
                             return ((typeof value === 'number' && isFinite(value)) && value > minValue) ? true : false;
                         },
                         isArray:function(obj) {
                             return Object.prototype.toString.call(obj) === '[object Array]';
                         },
                         isMobile: function () {
                             return (this.versions.android || this.versions.ios || this.versions.gecko) ? true : false;
                         },
                         isIsWeChat: function () {
                             var ua = window.navigator.userAgent.toLowerCase();
                             if (ua.match(/MicroMessenger/i) == 'micromessenger') {
                                 return true;
                             } else {
                                 return false;
                             }
                         },
                         asyncCheckbox:function(postName, appendTarget) {
                             $("input[type=hidden][name='" + postName + "']").remove();
                             $("input[type=checkbox][postName='" + postName + "']:checked").each(function(i, n) {
                                 this.createInput("hidden", null, postName, $(this).attr("postValue"), appendTarget)
                             })
                         },
                         queryString:function(name) {
                             var reg = new RegExp("(^|&)" + name + "=([^&]*)(&|$)", "i");
                             var r = window.location.search.substr(1).match(reg);
                             if (r != null) return unescape(r[2]);
                             return null
                         },
                         switchTab:function(id_prefix, index, on) {
                             $("#Tab_" + id_prefix + "_" + index).addClass(on).siblings().removeClass(on);
                             $("#List_" + id_prefix + "_" + index).fadeIn("fast").siblings().hide()
                         },
                         setExpireTime: function (obj, callback) {
                             //倒计时
                             var ele = obj,
                                 time = ele.attr('left_time_int');
                             if (isNaN(time) || Math.floor(time) <= 0) {
                                 ele.find("em").text("0");
                                 if (typeof(callback) != "undefined") {
                                     setTimeout(callback, 0)
                                 }
                             } else {
                                 var dd = Math.floor(time / (60 * 60 * 24));
                                 var hh = Math.floor(time / (60 * 60)) % 24;
                                 var mm = Math.floor(time / 60) % 60;
                                 var ss = Math.floor(time % 60);
                                 var ee = 0;
                                 var check_dd = dd;
                                 dd = dd > 9 ? dd : '0' + dd;
                                 hh = hh > 9 ? hh : '0' + hh;
                                 mm = mm > 9 ? mm : '0' + mm;
                                 ss = ss > 9 ? ss : '0' + ss;
                                 var time_str = time + '';
                                 ele.children(":eq(0)").text(dd);
                                 ele.children(":eq(1)").text(hh);
                                 ele.children(":eq(2)").text(mm);
                                 ele.children(":eq(3)").text(ss);
                                 time = time - 1;
                                 time = time.toFixed(1);
                                 ele.attr('left_time_int', time);
                                 setTimeout(function() {
                                     this.setExpireTime(obj, callback)
                                 }, 1000)
                             }
                         },
                         shake: function (ele, cls, times) {
                             //闪动
                             ele = $(ele);
                             var i = 0,
                                 t = false,
                                 o = ele.attr("class") + " ",
                                 c = "",
                                 times = times || 2;
                             if (t) return;
                             t = setInterval(function() {
                                 i++;
                                 c = i % 2 ? o + cls : o;
                                 ele.attr("class", c);
                                 if (i == 2 * times) {
                                     clearInterval(t);
                                     ele.removeClass(cls)
                                 }
                             }, 200)
                         },
                         hashTable:function() {
                             this.ObjArr = {}, this.Count = 0, this.Add = function(e, t) {
                                 return this.ObjArr.hasOwnProperty(e) ? !1 : (this.ObjArr[e] = t, this.Count++, !0)
                             }, this.Contains = function(e) {
                                 return this.ObjArr.hasOwnProperty(e)
                             }, this.GetValue = function(e) {
                                 if (this.Contains(e)) return this.ObjArr[e];
                                 throw Error("Hashtable not cotains the key: " + String(e))
                             }, this.Remove = function(e) {
                                 this.Contains(e) && (delete this.ObjArr[e], this.Count--)
                             }, this.Clear = function() {
                                 this.ObjArr = {}, this.Count = 0
                             }
                         },
                         changSendTime: function (time_flg, flag) {
                             //时间段选择
                             //<select class="start-time" name="send_start_time" id="send_start_time" onchange="bill.changSendTime(1)"><option selected value="">请选择....</option><option value="100000">10:00</option><option value="103000">10:30</option><option value="110000">11:00</option><option value="113000">11:30</option><option value="120000">12:00</option><option value="123000">12:30</option><option value="130000">13:00</option><option value="133000">13:30</option><option value="140000">14:00</option><option value="143000">14:30</option><option value="150000">15:00</option><option value="153000">15:30</option><option value="160000">16:00</option><option value="163000">16:30</option><option value="170000">17:00</option><option value="173000">17:30</option><option value="180000">18:00</option><option value="183000">18:30</option><option value="190000">19:00</option><option value="193000">19:30</option><option value="200000">20:00</option><option value="203000">20:30</option><option value="210000">21:00</option><option value="213000">21:30</option></select>
                             //<select class="end-time" name="send_end_time" id="send_end_time" onchange="bill.changSendTime(2)"><option selected value="">请选择...</option><option value="103000">10:30</option><option value="110000">11:00</option><option value="113000">11:30</option><option value="120000">12:00</option><option value="123000">12:30</option><option value="130000">13:00</option><option value="133000">13:30</option><option value="140000">14:00</option><option value="143000">14:30</option><option value="150000">15:00</option><option value="153000">15:30</option><option value="160000">16:00</option><option value="163000">16:30</option><option value="170000">17:00</option><option value="173000">17:30</option><option value="180000">18:00</option><option value="183000">18:30</option><option value="190000">19:00</option><option value="193000">19:30</option><option value="200000">20:00</option><option value="203000">20:30</option><option value="210000">21:00</option><option value="213000">21:30</option><option value="220000">22:00</option></select>
                             var sid = flag ? '#o_' : '#';
                             var send_start_time = $(sid + 'send_start_time').find("option:selected").val();
                             var send_end_time = $(sid + 'send_end_time').find("option:selected").val();
                             if (send_start_time < send_end_time) {
                                 if (time_flg == 2 && send_start_time == "") {
                                     if (parseInt(send_end_time.substring(2)) == 3000) {
                                         $(sid + "send_start_time").val(parseInt(send_end_time) - 7000);
                                     } else {
                                         var temp = parseInt(send_end_time.substring(0, 2));
                                         $(sid + "send_start_time").val(parseInt(send_end_time) - 3000);
                                     }
                                 }
                                 return false;
                             }
                             if (time_flg == 1) {
                                 if (send_start_time.substring(2) == "3000") {
                                     $(sid + "send_end_time").val(parseInt(send_start_time) + 7000);
                                 } else {
                                     var temp = parseInt(send_start_time.substring(0, 2));
                                     /*if((temp >= 10 && temp< 13) || (temp >= 16 && temp< 18)) {
                                         $("#send_end_time").val(parseInt(send_start_time) + 10000);
                                     } else {*/
                                     $(sid + "send_end_time").val(parseInt(send_start_time) + 3000);
                                     /*}*/
                                 }

                             } else {
                                 if (send_end_time) {
                                     if (parseInt(send_end_time.substring(2)) == 3000) {
                                         $(sid + "send_start_time").val(parseInt(send_end_time) - 3000);
                                     } else {
                                         var temp = parseInt(send_end_time.substring(0, 2));
                                         $(sid + "send_start_time").val(parseInt(send_end_time) - 7000);
                                     }
                                 } else
                                     $(sid + "send_start_time").find("option").eq(0).attr("selected", true);
                             }
                         },
                         resetModelTag:function(modeHtml) {
                             if (typeof(modeHtml) != "undefined" && modeHtml != "") {
                                 return modeHtml.replace("<!--", "").replace("-->", "").replaceAll("mtr", "tr").replaceAll("mtd", "td");
                             }
                             return null;
                         },
                         appendModel:function(e, modelItem, modelCenter, maxCount) {
                             modelCenter = $(modelCenter), modelCurCount = modelCenter.children().length;
                             if (maxCount - modelCurCount > 0) {
                                 if (modelCurCount + 1 == maxCount) {
                                     $(e).hide().children("span").addClass("dis");
                                 }
                                 $(this.resetModelTag($(modelItem).html()).replace(/{i}/g, modelCurCount + 1)).appendTo(modelCenter);
                             }
                         },
                         removeModel:function(e, appendModel, delName, delID, target) {
                             $(e).remove();
                             if (delID != null && !isNaN(delID) && Number(delID) > 0) {
                                 this.createInput("hidden", (delName + delID), delName, delID, (typeof(target) == "undefined") ? document.forms[0] : target);
                             }
                             $(appendModel).show().children("span").removeClass("dis");
                         },
                         parseURL: function (url) {
                             //调用方法
                             //var myURL = this.parseURL('http://abc.com:8080/dir/index.html?id=255&m=hello#top');
                             //myURL.file;     // = 'index.html'
                             //myURL.hash;     // = 'top'
                             //myURL.host;     // = 'abc.com'
                             //myURL.query;    // = '?id=255&m=hello'
                             //myURL.params;   // = Object = { id: 255, m: hello }
                             //myURL.path;     // = '/dir/index.html'
                             //myURL.segments; // = Array = ['dir', 'index.html']
                             //myURL.port;     // = '8080'
                             //myURL.protocol; // = 'http'
                             //myURL.source;   // = 'http://abc.com:8080/dir/index.html?id=255&m=hello#top'
                             var a = document.createElement('a');
                             a.href = url;
                             return {
                                 source: url,
                                 protocol: a.protocol.replace(':', ''),
                                 host: a.hostname,
                                 port: a.port,
                                 query: a.search,
                                 params: (function () {
                                     var ret = {},
                                         seg = a.search.replace(/^\?/, '').split('&'),
                                         len = seg.length, i = 0, s;
                                     for (; i < len; i++) {
                                         if (!seg[i]) { continue; }
                                         s = seg[i].split('=');
                                         ret[s[0]] = s[1];
                                     }
                                     return ret;
                                 })(),
                                 file: (a.pathname.match(/\/([^\/?#]+)$/i) || [, ''])[1],
                                 hash: a.hash.replace('#', ''),
                                 path: a.pathname.replace(/^([^\/])/, '/$1'),
                                 relative: (a.href.match(/tps?:\/\/[^\/]+(.+)/) || [, ''])[1],
                                 segments: a.pathname.replace(/^\//, '').split('/')
                             };
                         },
                         setCookie:function(name, value) {
                             var Days = 30;
                             var exp = new Date();
                             exp.setTime(exp.getTime() + Days * 24 * 60 * 60 * 1000);
                             document.cookie = name + "=" + escape(value) + ";expires=" + exp.toGMTString();
                         },
                         getCookie:function(name) {
                             var arr, reg = new RegExp("(^| )" + name + "=([^;]*)(;|$)");
                             if (arr = document.cookie.match(reg))
                                 return unescape(arr[2]);
                             else
                                 return null;
                         },
                         pager: {
                                //<div class="comment-warp"></div>
                                //   <script type="text/javascript">
                                //       air.pager.init(".comment-warp",{mod:"GetMsgList",model:"PRODUCT",id:"100"});
                                //   </script>
                             init: function (container,params) {
                                 this.container = $(container); 
                                 this.params = params;
                                 this.change(1); 
                             },
                             change: function (page) {
                                 var container = this.container;
                                 $.ajax({
                                     type: "POST",
                                     url: air.ajaxURL(),
                                     data:$.extend(this.params, { page: page }),
                                     dataType: 'json',
                                     beforeSend: function (xhr) {
                                         container.html("<div class=\"loading-box\" >评论数据载入中，请稍候..</div>");
                                     },
                                     success: function (data) {
                                         if (data.State != 0) {
                                             container.html(unescape(data.Response));
                                         } else {
                                             container.empty();
                                         }
                                     }
                                 });
                                 if (page > 1) {
                                     $("html,body").scrollTop(0).animate({ scrollTop: (container.offset().top + container.height()) }, 1);
                                 }
                             }
                         },
                         date: {
                             head: function () {
                                 return [31, 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31];
                             },
                             init: function (year, month, day, yVal, mVal, dVal) {
                                 var y = document.getElementById(year); var m = document.getElementById(month); var d = document.getElementById(day); var fy = new Date().getFullYear(); for (var i = (fy - 60) ; i < (fy + 60) ; i++) { this.appendOption(y, i) } this.selectValue(y, yVal); if (m != null) { this.writeMonth(m); this.selectValue(m, mVal) } if (d != null) { var n = this.head()[m.value - 1]; if (m.value == 2 && this.isPinYear(y.value)) n++; this.writeDay(d, n); this.selectValue(d, dVal) }
                             },
                             selectValue: function (target, value) { if (typeof (value) != "undefined") { target.value = value } },
                             appendOption: function (target, value) { target.options.add(new Option(value, value)) },
                             isPinYear: function (year) { return (0 == year % 4 && (year % 100 != 0 || year % 400 == 0)) },
                             writeMonth: function (e) { this.optionsClear(e); for (var i = 1; i < 13; i++) { this.appendOption(e, i) } },
                             writeDay: function (e, n) { this.optionsClear(e); for (var i = 1; i < (n + 1) ; i++) { this.appendOption(e, i) } },
                             changeYear: function (year, month, day) { var y = document.getElementById(year); var m = document.getElementById(month); var d = document.getElementById(day); if (m != null) { var YYYYvalue = y.options[y.selectedIndex].value; if (YYYYvalue == "") { this.optionsClear(m); return } this.writeMonth(m) } if (d != null) { var MMvalue = m.options[m.selectedIndex].value; this.optionsClear(d); var n = this.head()[MMvalue - 1]; if (MMvalue == 2 && this.isPinYear(y.value)) n++; this.writeDay(d, n) } }, changeMonth: function (year, month, day) { var y = document.getElementById(year); var m = document.getElementById(month); var d = document.getElementById(day); if (d != null) { var YYYYvalue = y.options[y.selectedIndex].value; if (YYYYvalue == "") { this.optionsClear(d); return } var n = this.head()[m.value - 1]; if (m.value == 2 && this.isPinYear(y.value)) n++; this.writeDay(d, n) } }, optionsClear: function (e) { e.options.length = 1 }
                         }
            };  
           
})();  

$(function () {

    //全选/反选 .closest("tr")
    $("input[type='checkbox'][checkedAll]").change(function () {
        var checkedAll = $(this);
        $("input[type=checkbox][isItem='true'][groupNum=" + checkedAll.attr("groupNum") + "]").not(":disabled").each(function () {
            var self = $(this), itemId = self.val(),
            curRow = $(".maintable tr[id='item_" + itemId + "']");
            if (!checkedAll.attr("checked")) {
                self.removeAttr("checked");
                curRow.removeClass("on");
            } else {
                self.attr("checked", "checked");
                curRow.addClass("on");
            }
        })
    });
    //隔行变色
    $(" tbody[islist] tr").hover(function () {
        $(this).addClass("hover");
    }, function () {
        $(this).removeClass("hover");
    });
    //初始化，选中下拉
    $("select[initval]").each(function (i, n) {
        $(this).val(n.getAttribute("initval"))
    });
    //初始化，选中单选
    $("input[type=radio][initval]").each(function (i, n) {
        $("input[name='" + n.getAttribute("name") + "'][value='" + n.getAttribute("initval") + "']").attr("checked", "checked");
    });
    //初始化，选中复选框
    $("input[type='checkbox'][initval]").each(function () {
        var selfa = $(this),
    		initVal = selfa.attr("initval"),
    		cVal = 1,
    		uVal = 0;
        if (initVal != "undef") {
            valArray = initVal.split(",");
            if (valArray && valArray.length > 1) {
                cVal = valArray[0];
                uVal = valArray[1]
            }
        }
        selfa.click(function () {
            var selfb = $(this);
            selfb.val(selfb.prop("checked") ? cVal : uVal)
        }).prop("checked", (selfa.val() != "0") ? true : false)
    });
    //单选或复选元素框点击后跳转到指定地址
    $("[clickskip]").click(function () {
        var self = $(this),
    		url = self.attr("href");
        if (typeof (url) != "undefined") {
            document.location.href = url;
        }
    });
    //下拉选择后跳转到指定地址
    $("select[clickskip]").change(function () {
        var self = $(this),
            url = self.attr("href");
        if (typeof (url) != "undefined") {
            location.href = url.replace('{0}', self.val());
        }
    });
    //设置元素背景
    $("[setbg]").each(function (i, n) {
        var self = $(this);
        var bgUrl = self.attr("bgurl");
        var repeat = self.attr("repeat");
        var x = self.attr("x");
        var y = self.attr("y");
        self.css({
            "background": "url(" + bgUrl + ") " + repeat + " " + x + " " + y + " "
        });
    });
    //元素获得焦点后改变样式
    $("input[focus-class]").focus(function () {
        var self = $(this),
            focusClass = self.attr("focus-class");
        self.addClass(focusClass);
    }).blur(function () {
        var self = $(this),
            focusClass = self.attr("focus-class");
        self.removeClass(focusClass);
    });
    //设置百分比
    $("[rate='true'] span").each(function () {
        var self = $(this),
            s1 = self.attr("r1"),
            s2 = self.attr("r2");
        self.css({
            "width": (Number(s2) / Number(s1) * 100).toFixed(2) + "%"
        });
    });
    //初始化倒计时
    $(".expire_time").each(function (i, n) {
        this.setExpireTime($(n));
    });
    //最多可输入字数
    $("[maxlimit]").keyup(function () {

        var self = $(this), maxLimit = Number(self.attr("maxlimit"));
        var len = self.val().length;
        if (len > maxLimit) {
            self.val(self.val().substring(0, maxLimit));
        }
        self.siblings(".word").text(((maxLimit + 1) - len));
    });
    //通用操作
    $(".op").on("click",function () {
        var self = $(this), mod = self.attr("mod"), act = self.attr("act"), itemId = air.undef(self.attr("itemid"), 0), msg = air.undef(self.attr("msg"), "确定执行该操作？");
        if (mod && act) {
            if (self.hasClass("iv") && $("input[name='ItemId']:checked").length <= 0) {
                layer.msg('尚未勾选任何项');
                return;
            }
            var actionURL = location.href.updateParams("mod,act", "{0},{1}").format(mod, act);
            if (itemId) {
                actionURL = actionURL.updateParams("itemid", "{0}").format(itemId);
            }
            var submitForm = self.parents("form").attr("action", actionURL);
            if (self.hasClass("nc")) {
                air.submitForm(submitForm);
            } else {
                layer.msg(msg, {
                    time: 0,
                    btn: ['确定', '取消'],
                    yes: function (index) {
                        layer.close(index);
                        air.submitForm(submitForm);
                    }
                });
            }

        }
    });
    //几秒后跳转
    var seconds = $(".seconds[skipurl]");
    if (seconds && seconds.length > 0) {
        var skipURL = seconds.attr("skipurl");
        if (skipURL) {
            var timerSeconds = seconds.text();
            var interval = setInterval(function () {
                if (timerSeconds <= 0) {
                    clearInterval(interval);
                }
                seconds.html(timerSeconds);
                if (timerSeconds <= 0) {
                    setTimeout(function () {
                        location.href = skipURL;
                    }, 300);
                }
                timerSeconds--;
            }, 1000);
        }
    }
    //提示确认后再跳转
    $("a[confirm]").click(function () {
        var self = $(this), info = self.attr("confirm");
        if (info && confirm(info)) {
            return true;
        }
        return false;
    });
    $(".submit-form").click(function () {
        var self = $(this),
            action = self.attr("action"),
            form = self.parents("form");
        if (form && form.length > 0) {
            if (typeof (action) != "undefined" && action != "") {
                form.attr("action",action);
            }
            form.submit();
        }
    });
});

