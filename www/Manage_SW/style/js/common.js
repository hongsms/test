//登录
function Login() {
    if ($.trim($("#UserName").val()) == "") {
        parent.message("warning", "用户名不能为空！");
        $("#UserName").focus();
        return;
    }
    if ($.trim($("#Password").val()) == "") {
        parent.message("warning", "密码不能为空！");
        $("#Password").focus();
        return;
    }
    if ($.trim($("#txtCode").val()) == "") {
        parent.message("warning", "验证码不能为空！");
        $("#txtCode").focus();
        return;
    }

    
    $.ajax({
        url: "Ajax/Ajax.ashx",
        type: "POST",
        cache: false,
        dataType: "text",
        data: { action: "Login", UserName: escape($("#UserName").val()), Code: escape($("#txtCode").val()), Password: escape($("#Password").val()) },
        success: function (ReturnData) {
            if (ReturnData == "yes") {
                window.location.href = "Admin_Main.aspx";
            }
            else {
                parent.message("warning", ReturnData);
            }
        }
    });
}
//全选/取消
function checkAll(chkobj) {
    if ($(chkobj).html().indexOf("全选") != -1) {
        $(chkobj).html("<span class=\"glyphicon glyphicon-unchecked\"></span>取消");
        $(".checkall input:enabled").prop("checked", true);
    } else {
        $(chkobj).html("<span class=\"glyphicon glyphicon-check\"></span>全选");
        $(".checkall input:enabled").prop("checked", false);
    }
}
//批量删除
function delAll(objId, objmsg) {
    if ($(".checkall input:checked").size() < 1) {
        parent.message("warning", "对不起，请选中您要操作的信息！");
        return false;
    }
    parent.message("confirm", objmsg, function () { __doPostBack(objId, ''); });
    return false;
}

//批量删除
function ShowConfirm(objId, objmsg) {
    parent.message("confirm", objmsg, function () { __doPostBack(objId, ''); });
    return false;
}


//确定提示
function selConfirm(objId, objmsg) {
    if ($(".checkall input:checked").size() < 1) {
        parent.message("warning", "对不起，请选中您要操作的信息！");
        return false;
    }
    parent.message("confirm", objmsg, function () { __doPostBack(objId, ''); });
    return false;
}

$(function () {
    //单选框设置选中状态
    $(".btn-radio .btn-primary input:checked").parent().addClass('active').siblings().removeClass('active');
    //复选框设置选中状态
    $(".btn-check .btn-primary input:checked").parent().addClass('active');
});