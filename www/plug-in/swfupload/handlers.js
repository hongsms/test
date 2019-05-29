
//ajax处理
var ajax = function (url, data, success_callback, async, iev, submitInput) { jQuery.ajax({ url: url, beforeSend: function () { if (submitInput != null) { $(submitInput).attr("disabled", "disabled") } }, complete: function () { if (submitInput != null) { setTimeout(function () { $(submitInput).removeAttr("disabled") }, 300) } }, cache: false, async: async, type: 'post', dataType: 'json', data: data, success: function (data) { success_callback(iev ? eval(data) : data) } }) };



function fileQueueError(file, errorCode, message) {
            try {

                var v = $("#mes").html();
                var str = "触发fileQueueError事件，参数file：" + file + "，参数errorCode：" + errorCode + "，参数message：" + message;
                $("#mes").html(v + str + "<br />");

            } catch (ex) {
                this.debug(ex);
            }

        }

        function fileDialogComplete(numFilesSelected, numFilesQueued) {
            try {
                if (numFilesQueued > 0) {
                    $("#swfu_thum").fadeIn();
                    this.startUpload();
                }
            } catch (ex) {
                this.debug(ex);
            }
        }
        function uploadProgress(file, bytesLoaded) {

            try {
                var percent = Math.ceil((bytesLoaded / file.size) * 100); // 计算百分比
                $(".swfu_ui").fadeIn();
                $(".filename").html(file.name);
                $(".process").css({ "width": percent + "%" });
                $(".percent").text(percent+"%");
            } catch (ex) {
                this.debug(ex);
            }
        }
        function uploadSuccess(file, serverData) {

            try {
                $('<span id="' + serverData + '"  class="mdu-pic fl" ><div class="img"><img src="/thumbnail.aspx?id=' + serverData + '" width="50" height="50" /></div><a href="javascript:;" class="del" onclick="if (confirm(\'您确实要删除该图片吗？\')) delthum(this,\'' + serverData + '\');" >删除</a></span>').appendTo("#swfu_thum");
                



            } catch (ex) {
                this.debug(ex);
            }
        }
        function delthum(t, id) {
            ajax("/AjaxService.aspx?mod=SwfuThumDel", { id: id }, function (data) {
                $("#swfu_thum span[id=" + id + "]").remove();
            }, true, false);
        }
        function uploadComplete(file) {
            try {
                /*  I want the next upload to continue automatically so I'll call startUpload here */

                if (this.getStats().files_queued > 0) {
                    this.startUpload();

                } else {
                    $(".swfu_ui").fadeOut();
                }
            } catch (ex) {
                this.debug(ex);
            }
        }

        function uploadError(file, errorCode, message) {
            alert(message);
            this.StopUpload();
//            try {
//                switch (errorCode) {
//                    case SWFUpload.UPLOAD_ERROR.FILE_CANCELLED:
//                        try {

//                        }
//                        catch (ex1) {
//                            this.debug(ex1);
//                        }
//                        break;
//                    case SWFUpload.UPLOAD_ERROR.UPLOAD_STOPPED:
//                        try {

//                        }
//                        catch (ex2) {
//                            this.debug(ex2);
//                        }
//                    case SWFUpload.UPLOAD_ERROR.UPLOAD_LIMIT_EXCEEDED:

//                        break;
//                    default:
//                        alert(message);
//                        break;
//                }
//            } catch (ex3) {
//                this.debug(ex3);
//            }

        }