<%@ Application Language="C#" %>
<script RunAt="server">

    void Application_Start(object sender, EventArgs e)
    {
        //在应用程序启动时运行的代码

    }

    void Application_End(object sender, EventArgs e)
    {
        //在应用程序关闭时运行的代码

    }

    void Application_Error(object sender, EventArgs e)
    {
        //在出现未处理的错误时运行的代码

    }

    void Application_BeginRequest(object sender, EventArgs e)
    {
        //每次会话，判断是否网站关闭
        if (OperateHelper.GetWebSite(10001).WebState == 0 && Request.RawUrl.ToLower().IndexOf("manage_sw") == -1 && Regex.IsMatch(Request.RawUrl, ".+(html|aspx|htm)$"))
        {
            Response.Write("<html><head><meta http-equiv=\"Content-Type\" content=\"text/html; charset=UTF-8\" />");
            Response.Write("<title>" + OperateHelper.GetWebSite(10001).WebName + "</title>");
            Response.Write("</head><body>");
            Response.Write(OperateHelper.GetWebSite(10001).CloseInfo);
            Response.Write("</body></html>");
            Response.End();
        }
    }
    void Session_Start(object sender, EventArgs e)
    {
        //在新会话启动时运行的代码

    }

    void Session_End(object sender, EventArgs e)
    {
        //在会话结束时运行的代码。 
        // 注意: 只有在 Web.config 文件中的 sessionstate 模式设置为
        // InProc 时，才会引发 Session_End 事件。如果会话模式 
        //设置为 StateServer 或 SQLServer，则不会引发该事件。

    }
       
</script>
