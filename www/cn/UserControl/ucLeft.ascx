<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ucLeft.ascx.cs" Inherits="UserControl_ucLeft" %>
<div class="main-nav-p tac">
    <div class="mauto">
        <ul class="hsms clearfix">
            <%=LeftHtml %>
        </ul>
    </div>
</div>
<div class="main-nav-m dn">
    <div class="title">
        <strong>
            <%=OperateHelper.GetTypeName(TypeId)%></strong> <i></i>
    </div>
    <ul class="hsms">
        <%=LeftHtml.Replace("class=\"cur\"", "").Replace("hsm=\"fadel\"", "")%>
    </ul>
</div>
