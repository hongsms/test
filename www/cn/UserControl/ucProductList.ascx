<%@ Control Language="C#" AutoEventWireup="true" CodeFile="/UserControl/ControlSource/NewList.cs"
    Inherits="NewList" %>
<ul class="hsms">
    <%foreach (var model in ModelInfo)
      {%>
    <li hsm="fadeup"><a href="NewsInfo.aspx?Id=<%=model.ID.ToString()%>" class="db img-md clearfix">
        <div class="imgs fl sm-12 sm-ha">
            <img src="<%=model.Image%>" alt=""></div>
        <div class="info-w vcs sm-12">
            <div class="date fl">
                <div class="dtm tal">
                    <em class="db">
                        <%=model.AddDate.ToString("yyyy")%></em> <span class="db">
                            <%=model.AddDate.ToString("MM-dd")%></span>
                </div>
            </div>
            <div class="info fl">
                <h3 class="els tra">
                    <%=model.Title%></h3>
                <div class="els2 tra">
                    <%=WebSite.Common.StringHelper.SubstringNoTitle(WebSite.Common.StringHelper.ClearHtml(model.Content1), 250, "...")%>
                </div>
            </div>
            <div class="icon fr tra sm-dn">
            </div>
        </div>
    </a></li>
    <%}%>
</ul>
<!--Page-->
<%=PageHtml %>
