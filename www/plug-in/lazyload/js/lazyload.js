function $C(classname, ele, tag) {
    var returns = [];
    ele = ele || document;
    tag = tag || '*';
    if (ele.getElementsByClassName) {
        var eles = ele.getElementsByClassName(classname);
        if (tag != '*') {
            for (var i = 0, L = eles.length; i < L; i++) {
                if (eles[i].tagName.toLowerCase() == tag.toLowerCase()) {
                    returns.push(eles[i]);
                }
            }
        } else {
            returns = eles;
        }
    } else {
        eles = ele.getElementsByTagName(tag);
        var pattern = new RegExp("(^|\\s)" + classname + "(\\s|$)");
        for (i = 0, L = eles.length; i < L; i++) {
            if (pattern.test(eles[i].className)) {
                returns.push(eles[i]);
            }
        }
    }
    return returns;
}


function _attachEvent(obj, evt, func, eventobj) {
    eventobj = !eventobj ? obj : eventobj;
    if (obj.addEventListener) {
        obj.addEventListener(evt, func, false);
    } else if (eventobj.attachEvent) {
        obj.attachEvent('on' + evt, func);
    }
}

function thumbImg(obj, method) {
    if (!obj) {
        return;
    }
    obj.onload = null;
    file = obj.src;
    zw = obj.offsetWidth;
    zh = obj.offsetHeight;
    if (zw < 2) {
        if (!obj.id) {
            obj.id = 'img_' + Math.random();
        }
        setTimeout("thumbImg($('" + obj.id + "'), " + method + ")", 100);
        return;
    }
    zr = zw / zh;
    method = !method ? 0 : 1;
    if (method) {
        fixw = obj.getAttribute('_width');
        fixh = obj.getAttribute('_height');
        if (zw > fixw) {
            zw = fixw;
            zh = zw / zr;
        }
        if (zh > fixh) {
            zh = fixh;
            zw = zh * zr;
        }
    } else {
       
        //fixw = typeof imagemaxwidth == 'undefined' ? '600' : imagemaxwidth;
        //if (zw > fixw) {
        //    zw = fixw;
        //    zh = zw / zr;
        //    obj.style.cursor = 'pointer';
        //    if (!obj.onclick) {
        //        obj.onclick = function () {
        //            zoom(obj, obj.src);
        //        };
        //    }
        //}
    }
    obj.width = zw;
    obj.height = zh;
}


function lazyload(className) {
	var cache = [];
	var cacheImage = document.createElement('img');
	cacheImage.src = "/plug-in/lazyload/images/loading_5.gif";
	cache.push(cacheImage);
    var obj = this;
    lazyload.className = className;
    this.getOffset = function (el, isLeft) {
        var retValue = 0;
        while (el != null) {
            retValue += el["offset" + (isLeft ? "Left" : "Top")];
            el = el.offsetParent;
        }
        return retValue;
    };
    this.initImages = function (ele) {
        lazyload.imgs = [];
        var eles = lazyload.className ? $C(lazyload.className, ele) : [document.body];
        for (var i = 0; i < eles.length; i++) {
            var imgs = eles[i].getElementsByTagName('IMG');
            for (var j = 0; j < imgs.length; j++) {
                if (imgs[j].getAttribute('file') && !imgs[j].getAttribute('lazyloaded')) {
                    if (this.getOffset(imgs[j]) > document.documentElement.clientHeight) {
                        lazyload.imgs.push(imgs[j]);
                    } else {
                        imgs[j].onload = function () { thumbImg(this); };
                        imgs[j].setAttribute('src', imgs[j].getAttribute('file'));
                        imgs[j].setAttribute('lazyloaded', 'true');
                    }
                }
            }
        }
    };
    this.showImage = function () {
        this.initImages();
        if (!lazyload.imgs.length) return false;
        var imgs = [];
        var scrollTop = Math.max(document.documentElement.scrollTop, document.body.scrollTop);
        for (var i = 0; i < lazyload.imgs.length; i++) {
            var img = lazyload.imgs[i];
            var offsetTop = this.getOffset(img);
            if (!img.getAttribute('lazyloaded') && offsetTop > document.documentElement.clientHeight && (offsetTop - scrollTop < document.documentElement.clientHeight)) {
                var dom = document.createElement('div');
                var width = img.getAttribute('width') ? img.getAttribute('width') : 100;
                var height = img.getAttribute('height') ? img.getAttribute('height') : 100;
                dom.innerHTML = '<div style="width: ' + width + 'px; height: ' + height + 'px;background: url(' + cache[0].src + ') no-repeat center center;"></div>';
                img.parentNode.insertBefore(dom.childNodes[0], img);
                img.onload = function () {
                    if (!this.getAttribute('_load')) {
                        this.setAttribute('_load', 1);
                        this.style.width = this.style.height = '';
                        this.parentNode.removeChild(this.previousSibling);
                        if (this.getAttribute('lazyloadthumb')) {
                            thumbImg(this);
                        }
                    }
                };
                img.style.width = img.style.height = '1px';
                img.setAttribute('src', img.getAttribute('file') ? img.getAttribute('file') : img.getAttribute('src'));
                img.setAttribute('lazyloaded', true);
            } else {
                imgs.push(img);
            }
        }
        lazyload.imgs = imgs;
        return true;
    };
    this.showImage();
    _attachEvent(window, 'scroll', function () { obj.showImage(); });
}