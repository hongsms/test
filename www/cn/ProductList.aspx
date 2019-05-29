<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ProductList.aspx.cs" Inherits="cn_ProductList" %>

<!DOCTYPE html>
<html lang="zh-cn">
<head id="Head1" runat="server">
    <%@ register src="UserControl/_uchead.ascx" tagname="_uchead" tagprefix="UserControl" %>
    <UserControl:_uchead ID="_uchead1" runat="server" />
</head>
<body>
    <!--Header-->
    <%@ register src="UserControl/_ucheader.ascx" tagname="_ucheader" tagprefix="UserControl" %>
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
       <%@ register src="UserControl/TopNavigation.ascx" tagname="TopNavigation" tagprefix="UserControl" %>
            <UserControl:TopNavigation ID="TopNavigation1" runat="server" />
      </div>
    </div>
   <%@ register src="UserControl/ucLeft.ascx" tagname="ucLeft" tagprefix="UserControl" %>
    <UserControl:ucLeft ID="ucLeft1" runat="server" />
    <div class="pro-list content">
      <div class="pb50 mauto ov clearfix">
        <ul class="mb30 list hsms clearfix">
        <%foreach (var item in ModelList)
          { %>
          <li class="lg-6 sm-12" hsm="fadeup">
            <a class="db img-md" href="javascript:;">
            <!-- <a href="<%=item.Link %>" target=_blank> -->
              <div class="imgs sm-ha" style=" cursor:pointer"><img src="<%=item.Image %>" alt=""></div>
              <div class="info">
                <h3 class="els fb tra mb5"><%=item.Title %></h3>
                <div class="els2">
                  <%=WebSite.Common.DNTRequest.NewlineConversion(item.Introduction)%>
                </div>
              </div>
              </a>
          </li>
          <%} %>
        </ul>
        <!--Page-->
        <%=PageHtml%>
        <div class="fix-pro fix-wrap">
          <div class="wh1 vc mauto">
           <%foreach (var item in ModelList)
             { %>
          <ul class="dn tac">
              <%=GetPicList(item.ID,item.Image) %>
            </ul>
          <%} %>
            
            
          </div>
          <button class="hide"></button>
        </div>
        <script>$(function(){$(".pro-list .list a").click(function(){$(".fix-pro").fadeIn();var i=$(this).parent().index();$(".fix-pro ul").eq(i).find("img").each(function(){var url=$(this).attr("data-src");$(this).attr("src",url)});$(".fix-pro ul").eq(i).show().siblings().hide()});$(".fix-pro ul").hsm({transitionStyle:"fade",singleItem:true,autoPlay:false,stopOnHover:true,autoHeight:true,navigation:true,mouseDrag:false,})});</script>
      </div>
    </div>
  </main>
    <!--Footer-->
    <%@ register src="UserControl/_ucfooter.ascx" tagname="_ucfooter" tagprefix="UserControl" %>
    <UserControl:_ucfooter ID="_ucfooter1" runat="server" />
</body>
</html>
