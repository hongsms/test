<%@ Page Language="C#" AutoEventWireup="true" CodeFile="NewsList.aspx.cs" Inherits="cn_NewsList" %>

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
    <div class="news-list content">
      <div class="pt50 pb50 mauto ov clearfix">
        
           <%@ register src="UserControl/ucNewsList.ascx" tagname="ucNewsList" tagprefix="UserControl" %>
                <UserControl:ucNewsList ID="ucNewsList1" runat="server" />

      </div>
    </div>
  </main>
    <!--Footer-->
    <%@ register src="UserControl/_ucfooter.ascx" tagname="_ucfooter" tagprefix="UserControl" %>
    <UserControl:_ucfooter ID="_ucfooter1" runat="server" />
</body>
</html>
