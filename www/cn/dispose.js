//用户登录
function Login() {
    var txtname = $("#txtname").val();
    var txtpwd = $("#txtpwd").val();
    if (txtname == "") {
        alert("请输入您的账户！");
        //swal("登录失败", "请输入您的账户！", "warning");
        return false;
    }
    if (txtpwd == "") {
        alert("请输入密码！");
        //swal("登录失败", "请输入密码！", "warning");
        return;
    }
    var ckeval = "0";
    if ($("input[name=ckeval]").attr("checked")) {
        ckeval = "1";
    }
    $.ajax({
        url: "Ajax.ashx",
        type: "POST",
        cache: false,
        dataType: "text",
        data: { action: "Login", username: escape(txtname), userpwd: escape(txtpwd), ckeval: ckeval },
        success: function (ReturnData) {
            if (ReturnData == "yes") {
                location.reload();
            }
            //else if (ReturnData == "findpassword") {
            //    swal({ title: "登录失败", text: "新网站登录，请先找回密码后再登录！", type: "warning" }, function () { window.location.href = "/ForgotPwd.html"; });
            //    alert("登录失败，新网站登录，请先找回密码后再登录！");

            //}
            else {
                //swal("登录失败", ReturnData, "warning");
                alert(ReturnData);
            }
        }
    });
}

