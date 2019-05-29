using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
///AdminLogin 的摘要说明
/// </summary>
public class AdminRoot : System.Web.UI.Page
{
    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);

        // 验证登录
        if (!AdminManage.IsLogin)
        {
            HttpContext.Current.Response.Write("<script>window.location.href='/Manage_SW/Admin_Login.aspx'</script>");
            HttpContext.Current.Response.End();
        }
        else
        {
            //判断是否有权限
            
        }
    }
}