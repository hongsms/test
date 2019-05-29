<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Contact.aspx.cs" Inherits="cn_Contact" %>

<!DOCTYPE html>
<html lang="zh-cn">
<head id="Head1" runat="server">
    <%@ Register Src="UserControl/_uchead.ascx" TagName="_uchead" TagPrefix="UserControl" %>
    <UserControl:_uchead ID="_uchead1" runat="server" />
</head>
<body onload="initialize()">
    <!--Header-->
    <%@ Register Src="UserControl/_ucheader.ascx" TagName="_ucheader" TagPrefix="UserControl" %>
    <UserControl:_ucheader ID="_ucheader1" runat="server" />
    <!--Banner-->
    <div class="banner" style="background-image: url(<%=MBaseType.Image %>);">
    </div>
    <!--Main-->
    <main class="main">
        <div class="main-home sm-dn">
            <div class="mauto">
                <h2 class="fl">
                    <em><%=MBaseType.Title %></em>
                    <span><%=MBaseType.IncludeType%></span>
                </h2>
                <%@ Register Src="UserControl/TopNavigation.ascx" TagName="TopNavigation" TagPrefix="UserControl" %>
                <UserControl:TopNavigation ID="TopNavigation1" runat="server" />
            </div>
        </div>
        <%@ Register Src="UserControl/ucLeft.ascx" TagName="ucLeft" TagPrefix="UserControl" %>
        <UserControl:ucLeft ID="ucLeft1" runat="server" />

        <div class="contact content">
            <div class="pb50 mauto ov clearfix">
                <%--     <div class="add">
          <div id="map_canvas" style="width:100%;height: 100%;"></div>
        </div>
      

        <style type="text/css">
            #map_canvas
            {
                width: auto;
                height: 350px;
            }
        </style>
       <script src="http://ditu.google.cn/maps/api/js?sensor=false&language=zh-cn&libraries=geometry&key=AIzaSyAlCN_zmRurk52FdFoRIg68n7ft90dlELM"
        type="text/javascript"></script>
    <script type="text/javascript">
        function initialize() {
            var myOptions = {
                zoom: 14,
                center: new google.maps.LatLng(<%=myOptions %>),
                mapTypeId: google.maps.MapTypeId.ROADMAP
            };
            var map = new google.maps.Map(document.getElementById("map_canvas"), myOptions);            
            // Google地图轨迹坐标集   纬度，经度  或使用ajax后台读取
            var trackPoints = [
            <%=trackPoints %>
            ];
             var trackAddress = [
              <%=trackAddress %>
            ];         
            
            for (var i = 0; i < trackPoints.length; i++) {
                //放置锚点  地图标记Marker
                var marker = new google.maps.Marker({
                    position: trackPoints[i],
                    map: map,
                });
                //创建标注窗口
                showinfomessage(marker, map,trackAddress[i]);       
            }
            trackPath.setMap(map);
        }

        function showinfomessage(marker, map,i) {
            var infoWindow = new google.maps.InfoWindow({
                content: "<div class='div_top'>"+i+"</div>"               // 创建信息窗口对象 
            }); 
            google.maps.event.addListener(marker,"click", function () {
                infoWindow.open(map,marker);                                    // 打开信息窗口 
            });
        }
    </script>--%>
                <div class="head tac pt15 pb15 mt20 mb20 clearfix">
                    <a href="http://wpa.qq.com/msgrd?v=3&uin=<%=modWebSite.Attr3%>&site=qq&menu=yes" target="_blank">
                        <img src="images/header_top_icon1.png" alt=""></a>
                    <a href="javascript:;" class="erweima-btn">
                        <img src="images/header_top_icon2.png" alt="">
                        <div class="img">
                            <img src="<%=modWebSite.Attr9%>" alt=""></div>
                    </a>

                    <%foreach (var item in AdbannerList)
                        { %>
                    <a href="<%=item.WebUrl %>" target="_blank">
                        <img src="<%=item.Image %>" alt=""></a>


                    <%} %>
                </div>
                <ul class="list hsms clearfix">
                    <%foreach (var item in ModelList)
                        { %>
                    <li class="lg-6 sm-12" hsm="fadeup" id="<%=item.ID %>">
                        <a href="<%=item.Link %>" target="_blank" class="db img-md clearfix">
                            <div class="imgs fl">
                                <img src="<%=item.Image %>" alt=""></div>
                            <div class="info fl">
                                <h3 class="els fb tra"><%=item.Title %></h3>
                                <div class="els2 li30">
                                    <%=WebSite.Common.DNTRequest.NewlineConversion(item.Introduction) %>
                                </div>
                            </div>
                        </a>
                    </li>
                    <%} %>
                </ul>
            </div>
        </div>
    </main>
    <!--Footer-->
    <%@ Register Src="UserControl/_ucfooter.ascx" TagName="_ucfooter" TagPrefix="UserControl" %>
    <UserControl:_ucfooter ID="_ucfooter1" runat="server" />
</body>
</html>
