<%@ Control Language="C#" AutoEventWireup="true" CodeFile="/UserControl/ControlSource/TopList.cs"
    Inherits="TopList" %>
<%foreach (var model in ModelInfo)
  {%>
<li hsm="fadeup">
    <div class="con-l vcs lg-6 sm-12 sm-db">
        <div class="date sm-12">
            <%=model.Title %></div>
        <div class="imgs sm-12 sm-ha">
            <%if (model.Image != "")
              { %>
            <img src="<%=model.Image %>" />
            <%} %></div>
    </div>
    <div class="con-r lg-6 sm-12">
        <div class="edit">
            <%=WebSite.Common.DNTRequest.NewlineConversion(model.Introduction) %>
        </div>
    </div>
    <div class="clear">
    </div>
</li>
<%}%>
