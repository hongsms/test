<%@ Control Language="C#" AutoEventWireup="true" CodeFile="_ucheader.ascx.cs" Inherits="cn_UserControl_ucheader" %>
<header>
    <div class="header">
      <div class="header-top">
        <div class="mauto">
          <a href="Index.aspx" class="logo fl"><img src="images/logo.png" alt="Logo"><img src="images/logo2.png" alt="Logo"></a>
          <div class="fr vcs">
            <div class="language rel fl">
              <em>English</em>
              <ul class="abs dn">
                <li><a href="/cn/Index.aspx">CN</a></li>
                <li><a href="/en/Index.aspx">EN</a></li>
              </ul>
            </div>
            <a href="http://wpa.qq.com/msgrd?v=3&uin=<%=modWebSite.Attr3%>&site=qq&menu=yes" target=_blank class="fl"><img src="images/header_top_icon1.png" alt=""></a>
            <a href="javascript:;" class="erweima-btn fl">
              <img src="images/header_top_icon2.png" alt="">
              <div class="img"><img src="<%=modWebSite.Attr9%>" alt=""></div>
            </a>
              <%foreach (var item in AdbannerList)
                  { %>              
                <a href="<%=item.WebUrl %>" target=_blank class="fl"><img src="<%=item.Image %>" alt=""></a>           
              <%} %>
          <%--  <a href="<%=modWebSite.Attr4%>" target=_blank class="fl"><img src="images/header_top_icon3.png" alt=""></a>
            <a href="<%=modWebSite.WebUrl%>" target=_blank class="fl"><img src="images/header_top_icon4.png" alt=""></a>
            <a href="<%=modWebSite.EmailName%>" target=_blank class="fl"><img src="images/header_top_icon5.png" alt=""></a>
            <a href="<%=modWebSite.EmailPwd%>" target=_blank class="fl"><img src="images/header_top_icon6.png" alt=""></a>--%>
          </div>
        </div>
      </div>
      <div class="header-con">
        <div class="mauto">
          <nav class="fl">
            <ul class="clearfix">
              <li>
                <a href="Index.aspx" <%=GetClass("")%>>首页</a>
              </li>
             <%=NewBind("10139")%>
              <%=NewBind("10148")%>
               <%=NewBind("10153")%>
                <li>
                <a href="Custom.aspx" <%=GetClass("10158")%>>空间定制</a>
              </li>
              <li>
                <a href="Contact.aspx" <%=GetClass("10147")%>>联系我们</a>
              </li>
            </ul>
          </nav>
          <div class="search-btn fr"></div>
        </div>
      </div>
    </div>
    <div class="header-m dn">
      <a href="Index.aspx" class="logo"><img src="images/m-logo.png" alt="Logo"></a>
      <div class="m-menu-b"></div>
      <div class="m-menu">
        <ul class="nav hsms">
          <li>
            <a href="Index.aspx">首页</a>
          </li>
            <%=NewMobileBind("10139")%>
              <%=NewMobileBind("10148")%>
               <%=NewMobileBind("10153")%>
                <li>
                <a href="Custom.aspx" >空间定制</a>
              </li>
              <li>
                <a href="Contact.aspx">联系我们</a>
              </li>
         
        </ul>
        <div class="m-language">
          <a href="/cn/Index.aspx">CN</a>
          <a href="/en/Index.aspx">EN</a>
        </div>
      </div>
      <div class="m-search-b"></div>
    </div>
    <div class="fix-search fix-wrap dn">
      <div class="vcs wh1">
        <form class="search vcs ov">
          <input class="txt fl" placeholder="请输入关键字" type="text" id="txtkeys2">
          <input class="btn fr" value="搜索" type=button onclick="SerachInfo($('#txtkeys2').val())">
        </form>
      </div>
      <button class="hide"></button>
    </div>
  </header><script type="text/javascript">
               var SerachInfo = function (keys) {
                   if (keys == "") {
                       alert("请输入关键字!")
                       return;
                   }
                   location.href = "NewsSearch.aspx?TypeId=10148&keys=" + escape(keys);
               }
</script>