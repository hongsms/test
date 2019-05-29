/*----------------------------------------------------------------
// Copyright (C) 2010 
// 版权所有
//
// 文件名：MyMessageBox.cs
// 文件功能描述：
//
//
// 创建标识：CXB 2010-02-03 08:58:23 星期三
//
// 修改标识：
// 修改描述：
//----------------------------------------------------------------*/

using System;
using System.Text;

/// <summary>
/// 显示消息提示对话框
/// </summary>
public class MyMessageBox
{
    private MyMessageBox()
    {
    }

    #region ScriptBlockMsg

    /// <summary>
    /// 向System.Web.UI.Page对象注册客户端脚本，脚本位置 <form runat="server"> 之后。
    /// </summary>
    /// <param name="page">页面</param>
    /// <param name="script">脚本</param>
    protected static void ScriptBlockMsg(System.Web.UI.Page page, string script)
    {
        Guid guid = Guid.NewGuid();
        string scriptkey = guid.ToString();

        if (page.ClientScript.IsClientScriptBlockRegistered(page.GetType(), scriptkey)) // 确定 page 是否注册了客户端脚本块。
        {
            ScriptBlockMsg(page, script);
        }

        page.ClientScript.RegisterClientScriptBlock(page.GetType(), scriptkey, script, true);
    }

    /// <summary>
    /// 显示消息提示对话框，并进行页面跳转
    /// </summary>
    /// <param name="page">当前页面指针，一般为this</param>
    /// <param name="msg">提示信息</param>
    /// <param name="url">跳转的目标URL</param>
    public static void ShowDirect(System.Web.UI.Page page, string msg, string url)
    {
        StringBuilder script = new StringBuilder();
        script.AppendFormat("window.alert('{0}');", msg);
        script.AppendFormat("window.location.href='{0}';", url);

        ScriptBlockMsg(page, script.ToString());
    }

    /// <summary>
    /// 显示消息提示对话框，并跳出框架进行页面跳转
    /// </summary>
    /// <param name="page">当前页面指针，一般为this</param>
    /// <param name="msg">提示信息</param>
    /// <param name="url">跳转的目标URL</param>
    public static void ShowDirectFrame(System.Web.UI.Page page, string msg, string url)
    {
        StringBuilder script = new StringBuilder();
        script.AppendFormat("window.alert('{0}');", msg);
        script.AppendFormat("parent.location.href='{0}';", url);

        ScriptBlockMsg(page, script.ToString());
    }

    #endregion

    #region StartupScriptMsg

    /// <summary>
    /// 向System.Web.UI.Page对象注册客户端脚本，脚本位置 </form> 之前。
    /// </summary>
    /// <param name="page">页面</param>
    /// <param name="script">脚本</param>
    protected static void StartupScriptMsg(System.Web.UI.Page page, string script)
    {
        Guid guid = Guid.NewGuid();
        string scriptkey = guid.ToString();

        if (page.ClientScript.IsStartupScriptRegistered(page.GetType(), scriptkey)) // 确定 page 是否注册了客户端启动脚本。
        {
            StartupScriptMsg(page, script);
        }

        page.ClientScript.RegisterStartupScript(page.GetType(), scriptkey, script, true);
    }

    /// <summary>
    /// 显示消息提示对话框
    /// </summary>
    /// <param name="page">当前页面指针，一般为this</param>
    /// <param name="msg">提示信息</param>
    public static void Show(System.Web.UI.Page page, string msg)
    {
        StartupScriptMsg(page, string.Format("window.alert('{0}');", msg));
    }

    /// <summary>
    /// 显示消息提示对话框
    /// </summary>
    /// <param name="page">当前页面指针，一般为this</param>
    /// <param name="msg">提示信息</param>
    public static void ShowAndBack(System.Web.UI.Page page, string msg)
    {
        StartupScriptMsg(page, string.Format("window.alert('{0}'); window.history.back();", msg));
    }

    /// <summary>
    /// 显示消息提示对话框，返回历史页面
    /// </summary>
    /// <param name="page">当前页面指针，一般为this</param>
    /// <param name="msg">提示信息</param>
    /// <param name="index"></param>
    public static void ShowAndHistoryBack(System.Web.UI.Page page, string msg, int index)
    {
        StartupScriptMsg(page, String.Format("window.alert('{0}'); window.history.go({1});", msg, index));
    }

    /// <summary>
    /// 显示消息提示对话框，并在当前页面进行跳转
    /// </summary>
    /// <param name="page">当前页面指针，一般为this</param>
    /// <param name="msg">提示信息</param>
    /// <param name="url">跳转的目标URL</param>
    public static void ShowAndRedirect(System.Web.UI.Page page, string msg, string url)
    {
        StringBuilder script = new StringBuilder();
        script.AppendFormat("window.alert('{0}');", msg);
        script.AppendFormat("self.location.href='{0}';", url);

        StartupScriptMsg(page, script.ToString());
    }

    /// <summary>
    /// 显示消息提示对话框，并进行页面跳转
    /// </summary>
    /// <param name="page">当前页面指针，一般为this</param>
    /// <param name="msg">提示信息</param>
    /// <param name="url">跳转的目标URL</param>
    public static void ShowAndDirect(System.Web.UI.Page page, string msg, string url)
    {
        StringBuilder script = new StringBuilder();
        script.AppendFormat("window.alert('{0}');", msg);
        script.AppendFormat("window.location.href='{0}';", url);

        StartupScriptMsg(page, script.ToString());
    }

    /// <summary>
    /// 显示消息提示对话框，并跳出框架进行页面跳转
    /// </summary>
    /// <param name="page">当前页面指针，一般为this</param>
    /// <param name="msg">提示信息</param>
    /// <param name="url">跳转的目标URL</param>
    public static void ShowAndDirectFrame(System.Web.UI.Page page, string msg, string url)
    {
        StringBuilder script = new StringBuilder();
        script.AppendFormat("window.alert('{0}');", msg);
        script.AppendFormat("parent.location.href='{0}';", url);

        StartupScriptMsg(page, script.ToString());
    }

    /// <summary>
    /// 显示消息提示对话框，在框架内进行刷新
    /// </summary>
    /// <param name="page">当前页面指针，一般为this</param>
    /// <param name="msg">提示信息</param>
    /// <param name="url">跳转的目标URL</param>
    public static void ShowReflashFrame(System.Web.UI.Page page, string msg, string url)
    {
        StringBuilder script = new StringBuilder();
        script.AppendFormat("window.alert('{0}');", msg);
        script.AppendFormat("parent.window.frameLeft.location.href='{0}';", url);

        StartupScriptMsg(page, script.ToString());
    }

    #endregion

    #region Anthem Message

    ///// <summary>
    ///// 以alert信息在客户端显示信息(anthem控件)
    ///// </summary>
    ///// <param name="page">页面对象</param>
    ///// <param name="script">消息</param>
    //public static void ShowClientAnthemMessage(System.Web.UI.Page page, string message)
    //{
    //    if (string.IsNullOrEmpty(message))
    //    {
    //        return;
    //    }

    //    AnthemStartupScript(page, string.Format("alert(\"{0}\");", message));
    //}

    ///// <summary>
    ///// 向System.Web.UI.Page对象注册客户端脚本(anthem控件)
    ///// </summary>
    ///// <param name="page">页面对象</param>
    ///// <param name="script">消息</param>
    //public static void AnthemStartupScript(System.Web.UI.Page page, string script)
    //{
    //    Guid guid = Guid.NewGuid();

    //    string scriptkey = guid.ToString();

    //    Anthem.Manager.RegisterStartupScript(page.GetType(), scriptkey, script, true);
    //}

    #endregion

    /// <summary>
    /// 控件点击 消息确认提示框
    /// </summary>
    /// <param name="page">当前页面指针，一般为this</param>
    /// <param name="msg">提示信息</param>
    public static void ShowConfirm(System.Web.UI.WebControls.WebControl Control, string msg)
    {
        //Control.Attributes.Add("onClick","if (!window.confirm('"+msg+"')){return false;}");
        Control.Attributes.Add("onclick", "return confirm('" + msg + "');");
    }


    #region  响应 不输入页面

    public static void ShowWithOutPage(string msg)
    {
        ShowWithOutPage(msg);
    }
    public static void ShowWithOutPage(string msg, bool isParent)
    {
        StringBuilder script = new StringBuilder();
        script.AppendFormat("{1}alert('{0}');", msg, isParent ? "parent." : "");
        System.Web.HttpContext.Current.Response.Write(string.Format("<script>{0}</script>", script.ToString()));
        System.Web.HttpContext.Current.Response.End();
    }
    public static void ShowAndDirectWithOutPage(string msg, string url, bool isParent)
    {
        StringBuilder script = new StringBuilder();
        script.AppendFormat("{1}alert('{0}');", msg, isParent ? "parent." : "");
        script.AppendFormat("{1}parent.location.href='{0}';", url, isParent ? "parent." : "");
        System.Web.HttpContext.Current.Response.Write(string.Format("<script>{0}</script>", script.ToString()));
        System.Web.HttpContext.Current.Response.End();
    }
    public static void ShowAndDirectWithOutPage(string msg, string url)
    {
        ShowAndDirectWithOutPage(msg, url, false);
    }
    public static void DirectWithOutPage(string url, bool isParent)
    {
        StringBuilder script = new StringBuilder();
        script.AppendFormat("{1}parent.location.href='{0}';", url, isParent ? "parent." : "");
        System.Web.HttpContext.Current.Response.Write(string.Format("<script>{0}</script>", script.ToString()));
        System.Web.HttpContext.Current.Response.End();
    }
    public static void DirectWithOutPage(string url)
    {
        DirectWithOutPage(url, false);
    }

    #endregion

    public static void Show(string msg, bool isParent)
    {
        StringBuilder script = new StringBuilder();
        script.AppendFormat("{1}alert('{0}');", msg, isParent ? "parent." : "");
        System.Web.HttpContext.Current.Response.Write(string.Format("<script>{0}</script>", script.ToString()));
        System.Web.HttpContext.Current.Response.End();
    }
    public static void Show(string msg)
    {
        Show(msg, false);
    }
    /// <summary>
    /// 显示消息提示对话框
    /// </summary>
    /// <param name="msg">提示信息</param>
    public static void ShowAndBack(string msg)
    {
        StringBuilder script = new StringBuilder();
        script.AppendFormat("alert('{0}');window.history.back();", msg );
        System.Web.HttpContext.Current.Response.Write(string.Format("<script>{0}</script>", script.ToString()));
        System.Web.HttpContext.Current.Response.End();
    }
    public static void ShowAndDirect(string msg, string url)
    {
        ShowAndDirect(msg, url, false);
    }
    public static void ShowAndDirect(string msg, string url, bool isParent)
    {
        StringBuilder script = new StringBuilder();
        script.AppendFormat("{1}alert('{0}');", msg, isParent ? "parent." : "");
        script.AppendFormat("{1}parent.location.href='{0}';", url, isParent ? "parent." : "");
        System.Web.HttpContext.Current.Response.Write(string.Format("<script>{0}</script>", script.ToString()));
        System.Web.HttpContext.Current.Response.End();
    }
}