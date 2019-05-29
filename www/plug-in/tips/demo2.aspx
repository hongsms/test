<%@ Page Language="C#" AutoEventWireup="true" CodeFile="demo2.aspx.cs" Inherits="plug_in_fancybox_demo2" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <script src="/js/jquery.js" type="text/javascript"></script>
    <script src="/js/website.js" type="text/javascript"></script>
    <link href="../../css/reset.css" rel="stylesheet" />
    <link href="/css/common.css" rel="stylesheet" type="text/css" />
    <link href="/plug-in/fancybox/css/jquery.fancybox-1.3.4.css" rel="stylesheet" type="text/css" />
    <script src="/plug-in/fancybox/js/jquery.fancybox-1.3.4.js" type="text/javascript"></script>
    <script src="js/tips.js" type="text/javascript"></script>
    <link href="css/tips.css" rel="stylesheet" />
    <script type="text/javascript">
        $(function () {
            var ot_info = '<p>购物车共有<em name="buyQuantity">2</em>件商品</p><p class="mt20">您可以：<a href="/cart.aspx">查看购物车</a><a href="javascript:site.closeFb();">继续选购</a></p>';
            msgBox.show("加入购物车",1,"商品已成功添加购物车！",ot_info);

            

        })
    </script>

   

</head>
<body>



    <%--<p><a class="msgbox2" href="#msgbox2" title="显示一个DIV内容" onclick="msgBox.show('加入购物车','测试1');" >点击这里</a>加载本页中一个隐藏的DIV。</p>
    <p><a class="msgbox2" href="#msgbox2" title="显示一个DIV内容" onclick="msgBox.show('加入购物车','测试2');" >点击这里</a>加载本页中一个隐藏的DIV。</p>--%>
        <%--<div style="display:none">
         <div id="msgbox3" class="msgbox2_warp">
                 <div class="msgbox2_title">加入购物车</div>
                 <div class="msgbox2_con">
            	    <div class="info success">商品已成功添加购物车！</div>
                    <div class="shop_card">
                        <p>购物车共有<em name="buyQuantity">2</em>件商品</p>
                        <p class="mt20">您可以：<a href="/cart.aspx">查看购物车</a><a href="javascript:site.closeFb();">继续选购</a></p>
                    </div>
                </div>
        </div>
    </div>--%>

    <%--<div class="msgbox2_layer_wrap">
        <div class="msgbox2_outer">
        <div class="msgbox2_title">加入购物车</div>
        <div class="msgbox2_con">
            <div class="msgbox2_text">
                <div class="info success">商品已成功添加购物车！</div>
                <div class="shop_card">
                        <p>购物车共有<em name="buyQuantity">2</em>件商品</p>
                        <p class="mt20">您可以：<a href="/cart.aspx">查看购物车</a><a href="javascript:site.closeFb();">继续选购</a></p>
                </div>
            </div>
        </div>
        </div>
    </div>--%>
    

</body>
</html>
