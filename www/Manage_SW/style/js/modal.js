function message(type, content, method) {

    switch (type) {
        case "warning":
            parent.$(".modal-backdrop").remove();
            var str = "<div class=\"modal fade bs-example-modal-sm\" id=\"myModal\" tabindex=\"-1\" role=\"dialog\" aria-labelledby=\"myModalLabel\">";
            str += "<div class=\"modal-dialog modal-sm\" role=\"document\">";
            str += "<div class=\"modal-content\">";
            str += "<div class=\"modal-header\">";
            str += "<button type=\"button\" class=\"close\" data-dismiss=\"modal\" aria-label=\"Close\"><span aria-hidden=\"true\">&times;</span></button>";
            str += "<h4 class=\"modal-title\" id=\"myModalLabel\">提示</h4>";
            str += "</div>";
            str += "<div class=\"modal-body\">" + content + "</div>";
            str += "<div class=\"modal-footer\">";
            str += "<button type=\"button\" id=\"modal-btn-ok\" data-dismiss=\"modal\" class=\"btn btn-primary\">确定</button>";
            str += "</div>";
            str += "</div></div>";
            parent.$("#mymessage").html(str);
            parent.$("#myModal").modal("show");
            break;
        case "confirm":
            parent.$(".modal-backdrop").remove();
            var str = "<div class=\"modal fade bs-example-modal-sm\" id=\"myModal\" tabindex=\"-1\" role=\"dialog\" aria-labelledby=\"myModalLabel\" data-backdrop=\"static\">";
            str += "<div class=\"modal-dialog modal-sm\" role=\"document\">";
            str += "<div class=\"modal-content\">";
            str += "<div class=\"modal-header\">";
            str += "<button type=\"button\" class=\"close\" data-dismiss=\"modal\" aria-label=\"Close\"><span aria-hidden=\"true\">&times;</span></button>";
            str += "<h4 class=\"modal-title\" id=\"myModalLabel\">提示</h4>";
            str += "</div>";
            str += "<div class=\"modal-body\">" + content + "</div>";
            str += "<div class=\"modal-footer\">";
            str += "<button type=\"button\" class=\"btn btn-default\" data-dismiss=\"modal\">取消</button>";
            str += "<button type=\"button\" id=\"modal-btn-ok\" class=\"btn btn-primary\">确定</button>";
            str += "</div>";
            str += "</div></div>";
            parent.$("#mymessage").html(str);
            parent.$("#myModal").modal("show");
            if (method) {
                parent.$("#modal-btn-ok").on("click", function () { parent.$("#myModal").modal("hide"); method(); });
            }
            break;
        case "success":
            parent.$(".modal-backdrop").remove();
            var str = "<div class=\"modal fade bs-example-modal-sm\" id=\"myModal\" tabindex=\"-1\" role=\"dialog\" aria-labelledby=\"myModalLabel\" data-backdrop=\"static\">";
            str += "<div class=\"modal-dialog modal-sm\" role=\"document\">";
            str += "<div class=\"modal-content\">";
            str += "<div class=\"modal-header\">";
            str += "<button type=\"button\" class=\"close\" data-dismiss=\"modal\" aria-label=\"Close\"><span aria-hidden=\"true\">&times;</span></button>";
            str += "<h4 class=\"modal-title\" id=\"myModalLabel\">提示</h4>";
            str += "</div>";
            str += "<div class=\"modal-body\">" + content + "</div>";
            str += "<div class=\"modal-footer\">";
            str += "<button type=\"button\" id=\"modal-btn-ok\" class=\"btn btn-primary\">确定</button>";
            str += "</div>";
            str += "</div></div>";
            parent.$("#mymessage").html(str);
            parent.$("#myModal").modal("show");
            if (method) {
                parent.$("#modal-btn-ok").on("click", function () { parent.$("#myModal").modal("hide"); parent.$("#mainframe").attr("src", method); });
            }
            break;
        case "imageremark":
            parent.$(".modal-backdrop").remove();
            var str = "<div class=\"modal fade bs-example-modal-sm\" id=\"myModal\" tabindex=\"-1\" role=\"dialog\" aria-labelledby=\"myModalLabel\" data-backdrop=\"static\">";
            str += "<div class=\"modal-dialog modal-sm\" role=\"document\" style=\"width:323px;\">";
            str += "<div class=\"modal-content\">";
            str += "<div class=\"modal-header\">";
            str += "<button type=\"button\" class=\"close\" data-dismiss=\"modal\" aria-label=\"Close\"><span aria-hidden=\"true\">&times;</span></button>";
            str += "<h4 class=\"modal-title\" id=\"myModalLabel\">图片描述</h4>";
            str += "</div>";
            str += "<div class=\"modal-body\"><textarea id=\"ImageRemark\" style=\"margin:10px 0;font-size:12px;padding:3px;color:#000;border:1px #d2d2d2 solid;vertical-align:middle;width:300px;height:50px;\">" + content.val() + "</textarea></div>";
            str += "<div class=\"modal-footer\">";
            str += "<button type=\"button\" id=\"modal-btn-many\" class=\"btn btn-default\">批量描述</button>";
            str += "<button type=\"button\" id=\"modal-btn-single\" class=\"btn btn-primary\">单张描述</button>";
            str += "</div>";
            str += "</div></div>";
            parent.$("#mymessage").html(str);
            parent.$("#myModal").modal("show");
            parent.$("#modal-btn-many").on("click", function () { parent.$("#myModal").modal("hide"); var remarkObj = $('#ImageRemark', parent.document); method.parent().parent().find("li input[name='hid_photo_remark']").val(remarkObj.val()); method.parent().parent().find("li .img-box .remark i").html(remarkObj.val()); });
            parent.$("#modal-btn-single").on("click", function () { parent.$("#myModal").modal("hide"); var remarkObj = $('#ImageRemark', parent.document); content.val(remarkObj.val()); method.siblings(".img-box").children(".remark").children("i").html(remarkObj.val()); });
            break;
        case "Logistics":
            parent.$(".modal-backdrop").remove();
            var str = "<div class=\"modal fade bs-example-modal-sm\" id=\"myModal\" tabindex=\"-1\" role=\"dialog\" aria-labelledby=\"myModalLabel\" data-backdrop=\"static\">";
            str += "<div class=\"modal-dialog modal-sm\" role=\"document\" style=\"width:290px;\">";
            str += "<div class=\"modal-content\">";
            str += "<div class=\"modal-header\">";
            str += "<button type=\"button\" class=\"close\" data-dismiss=\"modal\" aria-label=\"Close\"><span aria-hidden=\"true\">&times;</span></button>";
            str += "<h4 class=\"modal-title\" id=\"myModalLabel\">提示</h4>";
            str += "</div>";
            str += "<div class=\"modal-body\"><table style=\"margin: 5px 15px;\"><tr><td>物流公司：</td><td><select id=\"LogisticsCompany\" style=\"width:150px;\" name=\"LogisticsCompany\"><option value=\"申通\">申通</option><option value=\"EMS\">EMS</option><option value=\"顺丰\">顺丰</option><option value=\"圆通\">圆通</option><option value=\"中通\">中通</option><option value=\"韵达\">韵达</option><option value=\"天天\">天天</option><option value=\"汇通\">汇通</option><option value=\"德邦\">德邦</option><option value=\"宅急送\">宅急送</option></select></td></tr><tr><td>物流单号：</td><td><input id=\"LogisticsNo\" style=\"margin:10px 0;font-size:12px;padding:3px;color:#000;border:1px #d2d2d2 solid;vertical-align:middle;width:150px;\"/></td><tr></table></div>";
            str += "<div class=\"modal-footer\">";
            str += "<button type=\"button\" class=\"btn btn-default\" data-dismiss=\"modal\">取消</button>";
            str += "<button type=\"button\" id=\"modal-btn-ok\" class=\"btn btn-primary\">确定</button>";
            str += "</div>";
            str += "</div></div>";
            parent.$("#mymessage").html(str);
            parent.$("#myModal").modal("show");
            parent.$("#modal-btn-ok").on("click", function () {
                parent.$("#myModal").modal("hide");
                var LogisticsNo = $('#LogisticsNo', parent.document).val();
                var LogisticsCompany = $('#LogisticsCompany', parent.document).val();
                method.siblings("#LogisticsNo").val(LogisticsNo);
                method.siblings("#LogisticsCompany").val(LogisticsCompany);
                if (content) {
                    content();
                }
            });
            break;
        case "specvalue":
            parent.$(".modal-backdrop").remove();
            var str = "<div class=\"modal fade bs-example-modal-sm\" id=\"myModal\" tabindex=\"-1\" role=\"dialog\" aria-labelledby=\"myModalLabel\" data-backdrop=\"static\">";
            str += "<div class=\"modal-dialog modal-sm\" role=\"document\" style=\"width:500px;margin: 15% auto 20%;\">";
            str += "<form id=\"form1\" class=\"editform\">";
            str += "<div class=\"modal-content\">";
            str += "<div class=\"modal-header\">";
            str += "<button type=\"button\" class=\"close\" data-dismiss=\"modal\" aria-label=\"Close\"><span aria-hidden=\"true\">&times;</span></button>";
            str += "<h4 class=\"modal-title\" id=\"myModalLabel\">商品规格</h4>";
            str += "</div>";
            str += "<div class=\"modal-body\">";
            str += "<table style=\"margin: 5px auto;width:100%;\">";
            str += "<tr><td style=\"text-align:right;width:150px;\">选项名称：</td><td><input id=\"SpecValue\" style=\"height: 30px;margin:10px 0;font-size:12px;padding:3px;color:#000;border:1px #d2d2d2 solid;vertical-align:middle;width:150px;\" datatype=\"*\"/></td></tr>";
            str += "<tr><td style=\"text-align:right;\">选项别名：</td><td><input id=\"Alias\" style=\"height: 30px;margin:10px 0;font-size:12px;padding:3px;color:#000;border:1px #d2d2d2 solid;vertical-align:middle;width:150px;\"/></td></tr>";
            str += "<tr><td style=\"text-align:right;\">图片：</td><td><input id=\"Image\" style=\"height: 30px;margin: 10px 10px 10px 0;font-size:12px;padding:3px;color:#000;border:1px #d2d2d2 solid;vertical-align:middle;width:150px;\" class=\"upload-path\"/><div class=\"upload-box upload-img\"></div></td></tr>";
            str += "<tr><td style=\"text-align:right;\">排序：</td><td><input id=\"OrderBy\" style=\"height: 30px;margin:10px 0;font-size:12px;padding:3px;color:#000;border:1px #d2d2d2 solid;vertical-align:middle;width:80px;\" value=\"99\" datatype=\"n\"/></td></tr>";
            str += "<tr><td style=\"text-align:right;\">&nbsp;</td><td>提示：文字选项图片可以不用上传</td></tr>";
            str += "</table>";
            str += "</div>";
            str += "<div class=\"modal-footer\">";
            str += "<button type=\"button\" class=\"btn btn-default\" data-dismiss=\"modal\">取消</button>";
            str += "<button type=\"button\" id=\"modal-btn-ok\" class=\"btn btn-primary\">确定</button>";
            str += "</div>";
            str += "</form></div></div>";
            parent.$("#mymessage").html(str);
            parent.$("#myModal").modal("show");
            //初始化传值
            if (content == "1") {
                parent.$("#SpecValue").val(method.parent().parent().find("input[name=SpecValue]").val());
                parent.$("#Alias").val(method.parent().parent().find("input[name=Alias]").val());
                parent.$("#Image").val(method.parent().parent().find("input[name=Image]").val());
                parent.$("#OrderBy").val(method.parent().parent().find("input[name=OrderBy]").val());
            }
            //上传控件初始化
            $(".upload-img").InitUploader();
            //控件验证初始化
            $(".editform").Validform({
                btnSubmit: "#modal-btn-ok", showAllError: true, beforeSubmit: function (curform) {
                    parent.$("#myModal").modal("hide");
                    if (content == "1") {
                        method.parent().parent().find(".txt_SpecValue").html(parent.$("#SpecValue").val());
                        method.parent().parent().find(".txt_Alias").html(parent.$("#Alias").val());
                        if (parent.$("#Image").val() != "") {
                            method.parent().parent().find(".txt_Image").attr("src", parent.$("#Image").val());
                        }
                        method.parent().parent().find(".txt_OrderBy").html(parent.$("#OrderBy").val());
                        method.parent().parent().find("input[name=SpecValue]").val(parent.$("#SpecValue").val());
                        method.parent().parent().find("input[name=Alias]").val(parent.$("#Alias").val());
                        method.parent().parent().find("input[name=Image]").val(parent.$("#Image").val());
                        method.parent().parent().find("input[name=OrderBy]").val(parent.$("#OrderBy").val());
                    } else {
                        var SpecValue = parent.$("#SpecValue").val();
                        var Alias = parent.$("#Alias").val();
                        var Image = "/Manage_SW/style/images/spec_def.gif";
                        if (parent.$("#Image").val() != "") {
                            Image = parent.$("#Image").val();
                        }
                        var OrderBy = parent.$("#OrderBy").val();
                        var txtstr = "<tr>";
                        txtstr += "<td><span class=\"txt_SpecValue\">" + SpecValue + "</span><input type=\"hidden\" value=\"0\" name=\"SpecValueID\"><input type=\"hidden\" value=\"" + SpecValue + "\" name=\"SpecValue\"></td>";
                        txtstr += "<td><span class=\"txt_Alias\">" + Alias + "</span><input type=\"hidden\" value=\"" + Alias + "\" name=\"Alias\"></td>";
                        txtstr += "<td><img class=\"txt_Image\" src=\"" + Image + "\" width=\"20\" height=\"20\" /><input type=\"hidden\" name=\"Image\" value=\"" + Image + "\" /></td>";
                        txtstr += "<td><span class=\"txt_OrderBy\">" + OrderBy + "</span><input type=\"hidden\" name=\"OrderBy\" value=\"" + OrderBy + "\" /></td>";
                        txtstr += "<td><a title=\"编辑\" onclick=\"setSpecValue(this,'1')\" href=\"javascript:void(0);\" style=\"margin-right: 10px;\"><span class=\"glyphicon glyphicon-pencil\"></span></a>";
                        txtstr += "<a title=\"删除\" onclick=\"delSpecValue(this)\" href=\"javascript:void(0);\"><span class=\"glyphicon glyphicon-trash\"></span></a></td>";
                        txtstr += "</tr>";
                        method.parent().find(".table tbody").append(txtstr);
                    }
                    return false;
                }
            });
            break;
        case "setspec":
            parent.$(".modal-backdrop").remove();
            var str = "<div class=\"modal fade bs-example-modal-sm\" id=\"myModal\" tabindex=\"-1\" role=\"dialog\" aria-labelledby=\"myModalLabel\" data-backdrop=\"static\">";
            str += "<div class=\"modal-dialog modal-sm\" role=\"document\" style=\"width:500px;margin: 15% auto 20%;\">";
            str += "<div class=\"modal-content\">";
            str += "<div class=\"modal-header\">";
            str += "<button type=\"button\" class=\"close\" data-dismiss=\"modal\" aria-label=\"Close\"><span aria-hidden=\"true\">&times;</span></button>";
            str += "<h4 class=\"modal-title\" id=\"myModalLabel\">商品规格</h4>";
            str += "</div>";
            str += "<div class=\"modal-body\" style=\"height: 350px;\">";
            str += "<iframe width=\"100%\" height=\"100%\" frameborder=\"no\" src=\"/Manage_SW/Column/Product/SetSpec.aspx?tid=" + content + "\" name=\"specDialogId\" id=\"specDialogId\" allowtransparency=\"yes\" scrolling=\"no\"></iframe>";
            str += "</div>";
            str += "<div class=\"modal-footer\">";
            str += "<button type=\"button\" class=\"btn btn-default\" data-dismiss=\"modal\">取消</button>";
            str += "<button type=\"button\" id=\"modal-btn-ok\" class=\"btn btn-primary\">确定</button>";
            str += "</div>";
            str += "</div></div>";
            parent.$("#mymessage").html(str);
            parent.$("#myModal").modal("show");
            parent.$("#modal-btn-ok").on("click", function () {
                parent.$("#myModal").modal("hide");
                window.frames["specDialogId"].createSpecHtml();
            });
            break;
    }
    return false;
}
$(function () {
    if (parent.$("#mymessage").length <= 0) {
        var str = "<div id=\"mymessage\"></div>";
        parent.$("body").append(str);
    }
})