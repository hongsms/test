<%@ Page Language="C#" AutoEventWireup="true" CodeFile="demo1.aspx.cs" Inherits="plug_in_fancybox_demo1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <script src="/js/jquery.js" type="text/javascript"></script>
    <script src="/js/website.js" type="text/javascript"></script>
    <link href="/css/common.css" rel="stylesheet" type="text/css" />
    <link href="/plug-in/fancybox/css/jquery.fancybox-1.3.4.css" rel="stylesheet" type="text/css" />
    <script src="/plug-in/fancybox/js/jquery.fancybox-1.3.4.js" type="text/javascript"></script>
    <script type="text/javascript">
            $(function () {
                $(".demo1_test").fancybox({
                    'titleShow': false,
                    'padding': '3px',
                    'width': 800,
                    'height': 390,
                    'autoScale': false,
                    'transitionIn': 'none',
                    'transitionOut': 'none',
                    'type': 'iframe',
                    'scrolling': 'no',
                    'centerOnScroll': false,
                    'showCloseButton':false
                });
            })
    </script>
</head>
<body style="padding:300px;overflow:hidden;">
    <a class="demo1_test" href="demo1_test.aspx">SHOW</a>
</body>
</html>
