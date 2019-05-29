<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CustomInfo.aspx.cs" Inherits="cn_CustomInfo" %>

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
    <div class="news-info content">
      <div class="pt50 pb50 mauto ov clearfix">
        <div class="rel">
          <a href="<%=strLink %>" class="return">返回列表</a>
          <div class="tt1 tac">
            <h3 class="f24 fb pt35"><%=ModelInfo.Title %></h3>
            <span class="pt10 pb30">时间：<%=ModelInfo.AddDate.ToString("yyyy-MM-dd") %>来源：<%=ModelInfo.Source %></span>
          </div>
          <div class="news-share">
            <span class="fl">分享:</span>
            <div class="fl bdsharebuttonbox bdshare-button-style0-16" data-bd-bind="1501606084965">
              <a class="a1" href="" data-cmd="weixin" title="分享到微信"></a>
              <a class="a2" href="" data-cmd="tsina" title="分享到新浪微博"></a>
              <a class="a3" href="" data-cmd="tqq" title="腾讯微博"></a>
            </div>
          </div>
        </div>
        <div class="edit li30 f16 c6 pt15 pb15">
         <%=ModelInfo.Content1 %>
        </div>
        <div class="page">
         <%=LastInfo %>
                    <%=NextInfo %>
          
          <a href="<%=strLink %>" class="return">返回列表</a>
        </div>
      </div>
    </div>
  </main>
    <!--Footer-->
    <%@ register src="UserControl/_ucfooter.ascx" tagname="_ucfooter" tagprefix="UserControl" %>
    <UserControl:_ucfooter ID="_ucfooter1" runat="server" />
</body>
</html>
