<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Index.aspx.cs" Inherits="cn_Index" %>

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
    <div class="i-banner imgs">
        <ul class="sliders sm-dn">
            <%foreach (var item in AdbannerList)
              { %>
            <li><a href="<%=item.WebUrl %>" target="_blank">
                <img src="<%=item.Image %>" alt=""></a></li>
            <%} %>
        </ul>
        <ul class="sliders dn sm-db">
             <%foreach (var item in AdbannerList)
              { %> <li><a href="<%=item.WebUrl %>"><img class="lazyOwl" data-src="<%=item.ImageMobile %>" alt=""></a></li>            
            <%} %>
         
        
        </ul>
    </div>
    <!--Main-->
    <main class="i-main">
    <div class="i-video pb20">
      <div class="mauto ov">
        <ul class="hsms clearfix">
        <%foreach (var item in AdbannerIndexList)
          { %>
          <li class="lg-6 sm-12" hsm="fadeup">
            <h2><em class="fb"><%=item.Title%></em><span><%=item.FileName %></span></h2>
            <a href="<%=item.WebUrl %>" class="db img-md">
              <div class="imgs sm-ha"><img src="<%=item.Image %>" alt=""></div>
              <div class="els2 mt15 mb25">
                <%=WebSite.Common.DNTRequest.NewlineConversion(item.Info) %>
              </div>
            </a>
          </li>
        <%} %>
        
        </ul>
      </div>
    </div>
  </main>
    <!--Footer-->
    <%@ register src="UserControl/_ucfooter.ascx" tagname="_ucfooter" tagprefix="UserControl" %>
    <UserControl:_ucfooter ID="_ucfooter1" runat="server" />
</body>
</html>
