<%@ Control Language="C#" AutoEventWireup="true" CodeFile="/UserControl/ControlSource/TopList.cs"
    Inherits="TopList" %>
<%foreach (var model in ModelInfo)
  {%>
<li><a href="NewsInfo.aspx?Id=<%=model.ID.ToString()%>">
    <div class="item">
        <div class="img">
            <img src="<%=model.Image %>" /></div>
        <div class="text">
            <h3>
                <%=WebSite.Common.StringHelper.SubstringNoTitle(model.Title, 100, "...")%></h3>
            <p>
                <%=WebSite.Common.StringHelper.SubstringNoTitle(WebSite.Common.StringHelper.ClearHtml(model.Content1), 100, "")%></p>
            <div class="more">
                +查看详情</div>
        </div>
    </div>
</a></li>
<%}%>
