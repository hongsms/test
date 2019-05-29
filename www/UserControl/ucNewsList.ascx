<%@ Control Language="C#" AutoEventWireup="true" CodeFile="/UserControl/ControlSource/NewList.cs"
    Inherits="NewList" %>
<ul class="newlit">
    <%foreach (var model in ModelInfo)
      {%>
    <li><a href="NewsInfo.aspx?Id=<%=model.ID.ToString()%>">
        <div class="pic">
            <img src="<%=model.Image %>" width="100%" style=" height:157px;"></div>
        <div class="date">
            <span>
                <%=model.AddDate.ToString("yyyy") %></span><br>
            <%=model.AddDate.ToString("MM-dd") %></div>
        <div class="tit">
            <strong>
                <%=WebSite.Common.StringHelper.SubstringNoTitle(model.Title, 100, "...")%></strong></div>
        <div class="con">
            <%=WebSite.Common.StringHelper.SubstringNoTitle(WebSite.Common.StringHelper.ClearHtml(model.Content1), 250, "...")%></div>
        <div class="yle">
        </div>
    </a></li>
    <%}%>
</ul>
<%=PageHtml %>
