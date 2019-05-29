<%@ Page Language="C#" AutoEventWireup="true" CodeFile="demo1.aspx.cs" Inherits="plug_in_Validform_demo1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <script src="/js/jquery.js" type="text/javascript" ></script>
    
    <link href="/plug-in/Validform/css/style.5.1.css" rel="stylesheet" type="text/css" />
    <script src="/plug-in/Validform/js/Validform_v5.3.2.js" type="text/javascript"></script>
    <style type="text/css">
        .Validform_info{border:solid 1px #EAEAEA;padding:4px 20px 2px 5px;color:#666;position:absolute;display:none;line-height:20px;background-color:#fff;}
        .Validform_info .dec{bottom: -8px;display: block;height: 8px;overflow: hidden;position: absolute;left: 10px;width: 17px;}
        .Validform_info .dec em{font-family: simsun;font-size: 16px;height: 19px;left: 0;line-height: 21px;position: absolute;text-decoration: none;top: -9px;width: 17px;}
        .Validform_info .dec .dec1{color: #ccc;}
        .Validform_info .dec .dec2{color: #fff;top: -10px;}
    </style>
</head>
<body>

    <div style="padding:200px;overflow:hidden;">
    <form id="form1"  action="demo1.aspx?mod=Test" method="post" >
                 <table width="100%" style="table-layout:fixed;">
                            <tr>
                                <td class="need">*</td>
                                <td style="width:70px;">昵称：</td>
                                <td >
                                    <input type="text" value="" name="UserName" class="inputxt" datatype="s6-18" ajaxurl="$base/AjaxService.aspx?mod=Valid" nullmsg="请输入您的昵称！" errormsg="昵称至少6个字符,最多18个字符！"  />
                                    <div class="Validform_info"><span class="Validform_checktip">昵称至少6个字符,最多18个字符</span><span class="dec"><em class="dec1">&#9670;</em><em class="dec2">&#9670;</em></span></div>
                                </td>
                               
                            </tr>
                            
                            <tr>
                                <td class="need"></td>
                                <td></td>
                                <td colspan="2" style="padding:10px 0 18px 0;">
                                    <input type="submit" value="提 交" /> <input type="reset" value="重 置" />
                                </td>
                            </tr>
            </table>
    </form>
    </div>

    <script type="text/javascript">
        $(function () {
            //$(".registerform").Validform();  //就这一行代码！;

            $("#form1").Validform({
                tiptype: function (msg, o, cssctl) {
                    //msg：提示信息;
                    //o:{obj:*,type:*,curform:*}, obj指向的是当前验证的表单元素（或表单对象），type指示提示的状态，值为1、2、3、4， 1：正在检测/提交数据，2：通过验证，3：验证失败，4：提示ignore状态, curform为当前form对象;
                    //cssctl:内置的提示信息样式控制函数，该函数需传入两个参数：显示提示信息的对象 和 当前提示的状态（既形参o中的type）;
                    if (!o.obj.is("form")) {//验证表单元素时o.obj为该表单元素，全部验证通过提交表单时o.obj为该表单对象;
                        var objtip = o.obj.parent().find(".Validform_checktip");
                        cssctl(objtip, o.type);
                        objtip.text(msg);
                        
                        var infoObj = o.obj.parent().find(".Validform_info");
                        if (o.type == 2) {
                            infoObj.fadeOut(200);
                        } else {
                            if (infoObj.is(":visible")) { return; }
                            var left = o.obj.offset().left,
                                top = o.obj.offset().top;

                            infoObj.css({
                                left: left+o.obj.width()-30,
                                top: top - 45
                            }).show().animate({
                                top: top - 35
                            }, 200);
                        }

                    }
                }
            });
        })
</script>

</body>
</html>
