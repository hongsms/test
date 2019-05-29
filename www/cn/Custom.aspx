<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Custom.aspx.cs" Inherits="cn_Custom" %>

<!DOCTYPE html>
<html lang="zh-cn">
<head id="Head1" runat="server">
    <%@ register src="UserControl/_uchead.ascx" tagname="_uchead" tagprefix="UserControl" %>
    <UserControl:_uchead ID="_uchead1" runat="server" />
    <script type="text/javascript" src="/Web/style/js/jquery.bxslider.min.js"></script>
    <script type="text/javascript" src="/Web/style/js/main.js"></script>
    <script src="/Web/style/js/dispose.js" type="text/javascript"></script>
    <link href="/Web/style/lib/sweet-alert.css" rel="stylesheet" type="text/css" />
    <script src="/Web/style/lib/sweet-alert.min.js" type="text/javascript"></script>
    <link href="/plug-in/Validform_UI/css/style.5.1.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="/plug-in/Validform_UI/js/Validform_v5.3.2_min.js"></script>
    <script type="text/javascript" src="/plug-in/Validform_UI/js/Validform_v5.3.2_rule.js"></script>
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
    <div class="in-pro content">
      <div class="pt50 pb50 mauto clearfix">

      <%if (XMJSModelInfo != null)
        { %>
        <h2 class="tac fb pb45" id="10159"><%=XMJSModelInfo.Title %></h2>
        <div class="txt1 edit c6 li30 pb40 mb10" hsm="fadeup">
          <div class="tac">
           <%=XMJSModelInfo.Content1 %>
          </div>
        </div>
        <%} %>


        <%if (ModelListXM.Count > 0)
          { %>
        <h2 class="tac fb pt50 pb40" id="10160">定制项目</h2>
        <ul class="list1 hsms clearfix mb50" hsm="fadeup">
        <%foreach (var item in ModelListXM)
          { %>
           <li>
            <a href="javascript:"" class="db img-md">
              <div class="imgs rel sm-ha">
                <img src="<%=item.Image%>" alt="">
                <div class="info wh1 abs cf vc">
                  <em class="db"><%=item.Title%></em>
                  <span class="db"><%=item.SubTitle %>E</span>
                </div>
              </div>
              <div class="els2 mt25">
               <%=WebSite.Common.DNTRequest.NewlineConversion(item.Introduction)%>
              </div>
            </a>
          </li>
        <%} %>         
        </ul>
        <%} %>

        
          <%if (WMDYSModelInfo != null)
            { %>
        <h2 class="tac fb pt50 pb50 sm-pt0" id="10161"><%=WMDYSModelInfo.Title%></h2>
        <div class="advantage clearfix mb50" hsm="fadeup">
          <div class="imgs rel">
            <img src="<%=WMDYSModelInfo.Image%>" alt="">
            <div class="info abs vc cf">
             <%=WMDYSModelInfo.Content1%>
            </div>
          </div>
        </div><%} %>



        
          <%if (DZLCModelInfo != null)
            { %>
        <h2 class="tac fb pt50 pb45 sm-pt0" id="10162"><%=DZLCModelInfo.Title%></h2>
        <div class="tac" hsm="fadeup">
         <%=DZLCModelInfo.Content1%>
        </div>
        <%} %>



        <%if (ModelListALZS.Count > 0)
          { %>
        <h2 class="tac fb pt50 mt50 pb45 sm-pt0" id="10163">定制空间案例展示</h2>
        <div class="tab-wrap" hsm="fadeup">
          <ul class="tab-head cf tac clearfix">
          <%foreach (var item in ModelListALZS)
            { %>
             <li <%=ModelListALZS.IndexOf(item) == 0 ? "class=\"cur fl lg-3 els\"" : "class=\"fl lg-3 els\""%>><%=item.Title %></li>
          <%} %>
           
          </ul>
          <ul class="tab-body">

            <%foreach (var item in ModelListALZS)
              { %>
            <li <%=ModelListALZS.IndexOf(item) == 0 ? "class=\"cur\"" : ""%>>
              <ul class="sliders">
               <%=GetPicList(item.ID) %>
              </ul>
              <div class="pt35 pb35 pl25 pr25">
                <h3 class="fb mb15"><%=item.Title%></h3>
                <div class="edit c6 li30">
                 <%=item.Content1 %>
                </div>
              </div>
            </li>
            <%} %>          
          </ul>
        </div>
        <%} %>


        <%if (ModelListPPHZ.Count > 0)
          { %>
        <h2 class="tac fb pt50 mt50 pb50 sm-pt0"  id="10164">合作品牌</h2>
        <div class="cooperation mb45" hsm="fadeup">
          <ul>
          <%foreach (var item in ModelListPPHZ)
            { %>
            <li>
              <a  href="<%=item.Link%>" target=_blank class="db img-md">
                <div class="img rel"><img src="<%=item.Image%>" alt="" class="po-auto"></div>
              </a>
            </li>
          <%} %>
          </ul>
        </div>
        <%} %>

        <%if (ModelListDZXW.Count > 0)
          { %>
        <h2 class="tac fb pt50 mt50 pb50 sm-pt0" id="10165">空间定制新闻</h2>
        <div class="news mb15" hsm="fadeup">
          <ul>
          <%foreach (var item in ModelListDZXW)
            { %>
            <li>
              <a href="CustomInfo.aspx?Id=<%=item.ID.ToString()%>" class="db img-md" target=_blank>
                <div class="imgs"><img src="<%=item.Image %>" alt=""></div>
                <div class="info pt25 pb25 pl20 pr20">
                  <h3 class="els fb pb20"><%=item.Title %></h3>
                  <div class="els2">
                    <%=WebSite.Common.StringHelper.SubstringNoTitle(WebSite.Common.StringHelper.ClearHtml(item.Content1), 80, "...")%>
                  </div>
                </div>
              </a>
            </li>
           <%} %>
          </ul>
        </div>
        <%} %>

        <h2 class="tac fb pt50 mt50 sm-pt0" id="10169"><%=CSHHRModelInfo.Title %></h2>
        <div class="txt edit li30 mt10 mb50" hsm="fadeup">
          <div class="tac">
           <%=CSHHRModelInfo.Content1%>
          </div>
        </div>
        <ul class="partner-list mt5 mb30 clearfix">
        <%foreach (var item in GetPicLists(CSHHRModelInfo.ID))
          { %>
           <li class="lg-3 sm-6 tac" hsm="fadez">
            <div><img src="<%=item.OriginalUrl%>" alt=""></div>
            <h3><%=WebSite.Common.DNTRequest.NewlineConversion(item.Info)%></h3>
          </li>
        <%} %>
         
        </ul>


        <h2 class="tac fb pt50 mt20 pb40 sm-pt20">在线留言</h2>
      <form id="form2" action="?mod=submit" runat="server" method="post" enctype="multipart/form-data" >
      <div class="message clearfix" hsm="fadeup">
          <div class="rel fl">
            <strong>*</strong>
            <label for="">联系人</label>
            <input type="text" class="txt" datatype="*" nullmsg="联系人姓名不能为空！"
                        name="Name">
          </div>
          <div class="rel fl">
            <strong>*</strong>
            <label for="">公司</label>
            <input type="text" class="txt" datatype="*" nullmsg="公司名称不能为空！" name="Company"
                        >
          </div>
          <div class="rel fl">
            <strong>*</strong>
            <label for="">电话</label>
            <input type="text" class="txt" datatype="*" nullmsg="电话不能为空！" name="Tel">
          </div>
          <div class="rel fl">
            <label for="">传真</label>
            <input type="text" class="txt" name="Zip" >
          </div>
          <div class="rel fl">
            <label for="">邮件</label>
            <input type="text" class="txt" name="Email" >
          </div>
          <div class="rel fl">
            <label for="">QQ</label>
            <input type="text" class="txt" name="FromName" >
          </div>
          <div class="rel fl title">
            <strong>*</strong>
            <label for="">标题</label>
            <input type="text" class="txt" datatype="*" nullmsg="标题不能为空！"
                        name="Title">
          </div>
          <div class="textarea fl">
            <textarea  datatype="*" nullmsg="留言内容不能为空！" name="Content" cols="" rows=""></textarea>
          </div>
          <div class="rel fl btn"> <input type="hidden" name="Token" value="<%=GetToken() %>" />
            <input type="button" value="提交留言" class="btn1">
            <input type="button" value="重置" class="btn2 ml5">
             <p id="objtip" class="objtip" style="color: #FF0000; width: 100%; font-size: 14px;
                        padding-top: 2px; text-align: center;">
                    </p>
          </div>
          </div>
        </form>
          <asp:Literal ID="litJs" runat="server"></asp:Literal>
    <script type="text/javascript">
        $(function () {

            var myForm = $("#form2").Validform({
                btnSubmit: ".btn1",
                btnReset: ".btn2",
                showAllError: false,
                ignoreHidden: false,
                tiptype: function (msg, o) {
                    if (!o.obj.is("form")) {
                        var objtip = $("#objtip");
                        //cssctl(objtip, o.type);                       
                        if (o.type == 1 || o.type == 3) {
                            objtip.text(msg).css({ "display": "block" });
                        } else {
                            objtip.hide();
                        }
                    }
                }
            });
        })
    </script>


        <script>            $(".cooperation ul").hsm({ items: 4, autoPlay: true, stopOnHover: true, autoHeight: true, navigation: true, mouseDrag: false }); $(".list1,.news ul").hsm({ items: 3, autoPlay: true, stopOnHover: true, autoHeight: true, navigation: true, mouseDrag: false });</script>
      </div>
    </div>
  </main>
    <!--Footer-->
    <%@ register src="UserControl/_ucfooter.ascx" tagname="_ucfooter" tagprefix="UserControl" %>
    <UserControl:_ucfooter ID="_ucfooter1" runat="server" />
</body>
</html>
