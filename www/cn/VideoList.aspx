<%@ Page Language="C#" AutoEventWireup="true" CodeFile="VideoList.aspx.cs" Inherits="cn_VideoList" %>

<!DOCTYPE html>
<html lang="zh-cn">
<head id="Head1" runat="server">
    <%@ Register Src="UserControl/_uchead.ascx" TagName="_uchead" TagPrefix="UserControl" %>
    <UserControl:_uchead ID="_uchead1" runat="server" />
</head>
<body>
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
        <div class="video-info2 content">
            <div class="pt50 sm-pb50 ov clearfix">
                <div class="pic-sliders rel">
                    <div id="pic_slider_t">


                        <div class="item">
                            <div class="video-w">
                                <%if (ModelInfo.FileURL != "")
                                    { %>
                                <video poster="<%=ModelInfo.Image %>" controls="">
                                    <source src="<%=ModelInfo.FileURL %>" type="video/mp4">您的浏览器不支持 video 标签。
                                </video>
                                <%}
                                    else
                                    { %>
                                <%=ModelInfo.Content1 %>

                                <%} %>
                            </div>
                            <div class="txt">
                                <h3 class="fb"><%=ModelInfo.Title%></h3>

                            </div>
                        </div>

                    </div>
                    <div class="b-con pb25 pt45 mt35">
                        <div id="pic_slider_b" class="mauto">

                            <%foreach (var item in ModelList)
                                { %>

                            <a href="VideoList.aspx?Id=<%=item.ID %>" class="wh1 db">
                                <div class="item"> 
                                    <div class="imgs wh1">
                                        <img src="<%=item.Image %>" alt="">
                                    </div>
                                    <h3 class="els tac"><%=item.Title %></h3>
                                </div>
                            </a>
                            <%} %>
                        </div>
                    </div>
                </div>
                <script>$(function(){b_slider=$("#pic_slider_b");b_slider.hsm({items:6,navigation:true,pagination:false,mouseDrag:false,itemsMobile:[479,2],})});</script>
            </div>
        </div>
    </main>
    <!--Footer-->
    <%@ Register Src="UserControl/_ucfooter.ascx" TagName="_ucfooter" TagPrefix="UserControl" %>
    <UserControl:_ucfooter ID="_ucfooter1" runat="server" />
</body>
</html>
