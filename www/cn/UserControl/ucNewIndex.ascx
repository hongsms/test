<%@ Control Language="C#" AutoEventWireup="true" CodeFile="/UserControl/ControlSource/TopList.cs"
    Inherits="TopList" %>
<%foreach (var model in ModelInfo)
  {%>
<div class="article">
    <div class="blsot rel">
        <span>
            <%=model.AddDate.ToString("yyyy-MM-dd")%></span> <a href="NewsInfo.aspx?Id=<%=model.ID.ToString()%>">
                <%=model.Title%></a>
    </div>
</div>
<%}%>