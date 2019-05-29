<%@ Control Language="C#" AutoEventWireup="true" CodeFile="_ucfooter.ascx.cs" Inherits="cn_UserControl_ucfooter" %>
<footer>
    <div class="footer-head sm-dn">
      <div class="mauto pt45 pb45">
         <%=NewBind("10139")%>
              <%=NewBind("10148")%>
               <%=NewBind("10153")%>
        <%=NewBind("10158")%>
        <dl class="fl">
        <dt><a href="Contact.aspx">联系我们</a></dt>
        <div class="gdlt">
        <%foreach (var item in ModelListLXWM)
          { %>
          <dd><a href="Contact.aspx#<%=item.ID%>"><%=item.Title%></a></dd>
        <%} %>
      </div>
         
        </dl>
        <dl class="fl add">
          <dt>联系方式</dt>
          <dd><img src="images/footer_add_icon1.png" alt=""><%=modWebSite.Attr1%></dd>
          <dd><img src="images/footer_add_icon2.png" alt=""><%=modWebSite.Attr2%></dd>
          <dd><img src="images/footer_add_icon3.png" alt=""><%=modWebSite.HomePage%></dd>
        </dl>
         <div class="erweima fr" style=" margin-left:10px">
         
          <div class="img"><img src="<%=modWebSite.Attr9%>" alt="" class="wh1"></div>
          <em class="db tac">官方微信</em>
        </div>
        <div class="erweima fr"> <div class="img"><img src="<%=modWebSite.ReceiveEmail%>" alt="" class="wh1"></div>
          <em class="db tac">新浪微博</em>
        </div>
      </div>
    </div>
    <div class="copyright tac"><%=modWebSite.Copyright%></div>
  </footer>
