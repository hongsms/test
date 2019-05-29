using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebSite.Common;

/// <summary>
///OperateUser 的摘要说明
/// </summary>
public class UserRoot : System.Web.UI.Page
{

    public UserRoot()
    {
        //
        //TODO: 在此处添加构造函数逻辑
        //
    }

    protected override void OnInit(EventArgs e)
    {
        this.Init += new System.EventHandler(ValidatePermission);
        base.OnInit(e);
    }

    #region 验证是否已登录

    /// <summary>
    /// 验证是否已登录
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void ValidatePermission(object sender, System.EventArgs e)
    {
        if (GetUserID == -1)
        {
            //string urlstr = Request.RawUrl;
            //string pathstr = string.Empty;
            //if (urlstr.IndexOf("Mobile/") > -1)
            //{
            //    pathstr = "/Mobile";
            //}
            if (UserRoot.GetUserID == -1)
            {
                Response.Clear();
                Response.Write("<script defer>window.alert('请先登录您的帐户！');parent.location='Login.aspx?rurl=" + HttpUtility.UrlEncode(Request.Url.PathAndQuery) + "';</script>");
                Response.End();
            }

        }
    }
    #endregion


    /// <summary>
    ///  清除cookie
    /// </summary>
    public static void CleanCookie()
    {
        HttpCookie UserLogCookie = new HttpCookie("UserLog");
        UserLogCookie.Expires = DateTime.Now.AddDays(-1000d);
        HttpContext.Current.Response.Cookies.Add(UserLogCookie);
    }

    /// <summary>
    ///  自动登录
    /// </summary>
    /// <returns></returns>
    private static bool _IsAutoLogin = false;
    public static bool IsAutoLogin
    {
        get
        {
            return _IsAutoLogin;
        }
        set
        {

            _IsAutoLogin = value;
        }
    }


    /// <summary>
    ///  设置管理员登录状态
    /// </summary>
    public static void SetUserLogin(int UserID, string UserName, int UserType)
    {
        HttpCookie cookie = HttpContext.Current.Request.Cookies["UserLog"];
        if (cookie == null)
        {
            cookie = new HttpCookie("UserLog");
        }
        cookie.Values["UserID"] = StringHelper.EncryptDES(UserID.ToString());
        cookie.Values["UserName"] = StringHelper.EncryptDES(UserName);
        cookie.Values["UserType"] = StringHelper.EncryptDES(UserType.ToString());
        HttpContext.Current.Response.SetCookie(cookie);
    }


    /// <summary>
    ///  获取用户id
    /// </summary>
    /// <returns></returns>
    public static int GetUserID
    {
        get
        {

            HttpCookie cookie = HttpContext.Current.Request.Cookies.Get("UserLog");
            if (cookie == null || cookie["UserID"] == null)
            {
                return -1;
            }

            return Convert.ToInt32(StringHelper.DecryptDES(cookie["UserID"]));
        }
        set
        {
            HttpCookie cookie = System.Web.HttpContext.Current.Request.Cookies["UserLog"];
            if (cookie == null)
            {
                cookie = new HttpCookie("UserLog");
            }
            cookie.Values["UserID"] = StringHelper.EncryptDES(value.ToString());
            if (IsAutoLogin)
            {
                cookie.Expires = DateTime.Now.Date.AddDays(7.0);
            }
            else
            {
                cookie.Expires = DateTime.Now.Date.AddDays(1.0);
            }
            System.Web.HttpContext.Current.Response.SetCookie(cookie);
        }
    }

    /// <summary>
    ///  获取用户名
    /// </summary>
    /// <returns></returns>
    public static string GetUserName
    {
        get
        {
            HttpCookie cookie = HttpContext.Current.Request.Cookies.Get("UserLog");
            if (cookie == null || cookie["UserName"] == null)
            {
                return "";
            }
            return StringHelper.DecryptDES(cookie["UserName"]);
        }
        set
        {
            HttpCookie cookie = System.Web.HttpContext.Current.Request.Cookies["UserLog"];
            if (cookie == null)
            {
                cookie = new HttpCookie("UserLog");
            }
            cookie.Values["UserName"] = StringHelper.EncryptDES(value.ToString());
            if (IsAutoLogin)
            {
                cookie.Expires = DateTime.Now.Date.AddDays(7.0);
            }
            else
            {
                cookie.Expires = DateTime.Now.Date.AddDays(1.0);
            }
            System.Web.HttpContext.Current.Response.SetCookie(cookie);
        }
    }

  
}