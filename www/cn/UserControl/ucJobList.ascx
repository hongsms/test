<%@ Control Language="C#" AutoEventWireup="true" CodeFile="/UserControl/ControlSource/NewList.cs"
    Inherits="NewList" %>
<ul class="hsms">
    <%foreach (var model in ModelInfo)
      {%>
    <li hsm="fader">
        <div class="top els">
            职位：<%=model.Title%></div>
        <div class="edit li30 pt10 pb10 pl15 pr15 dn">
            <%=model.Content1%>
        </div>
    </li>
    <%}%>
</ul>
<script>
    $(".join-list .top").click(function () {
        $(this).toggleClass("cur").parent("li").siblings("li").find(".top").removeClass("cur");
        $(this).siblings(".edit").stop().slideToggle().parent("li").siblings("li").find(".edit").slideUp();
    });
</script>
<%=PageHtml %>
