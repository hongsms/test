<%@ Control Language="C#" AutoEventWireup="true" CodeFile="/UserControl/ControlSource/NewList.cs"
    Inherits="NewList" %>
<ul class="rolimg">
    <%foreach (var model in ModelInfo)
      {%>
    <li><a href="ProductView.aspx?Id=<%=model.ID.ToString()%>">
        <div class="pic">
            <img src="<%=model.Image %>" width="100%"></div>
        <div class="tit">
            <%=WebSite.Common.StringHelper.SubstringNoTitle(model.Title, 100, "...")%></div>
    </a></li>
    <%}%>
    <br class="clear">
</ul>
<%=PageHtml %>
