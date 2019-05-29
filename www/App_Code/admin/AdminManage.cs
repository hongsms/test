using System;
using System.Web;
using System.Configuration;
using WebSite.Common;
using WebSite.BLL;
using WebSite.Model;

/// <summary>
///CookieManager 的摘要说明
/// </summary>
public class AdminManage
{
    public AdminManage()
    {
        //
        //TODO: 在此处添加构造函数逻辑
        //
    }

    #region 管理员

    /// <summary>
    /// 是否登录
    /// </summary>
    public static bool IsLogin
    {
        get
        {
            return AdminID > 0;
        }
    }

    /// <summary>
    /// 管理员ID
    /// </summary>
    public static int AdminID
    {
        get
        {
            HttpCookie cookie = HttpContext.Current.Request.Cookies["AdminInfo"];
            if (cookie == null || cookie["AdminID"] == null)
            {
                return 0;
            }
            return int.Parse(StringHelper.DecryptDES(cookie["AdminID"]));
        }
        set
        {
            HttpCookie cookie = HttpContext.Current.Request.Cookies["AdminInfo"];
            if (cookie == null)
            {
                cookie = new HttpCookie("AdminInfo");
            }
            cookie.Values["AdminID"] = StringHelper.EncryptDES(value.ToString());
            HttpContext.Current.Response.SetCookie(cookie);
        }
    }

    /// <summary>
    /// 管理员账号
    /// </summary>
    public static string AdminName
    {
        get
        {
            HttpCookie cookie = HttpContext.Current.Request.Cookies["AdminInfo"];
            if (cookie == null || cookie["AdminName"] == null)
            {
                return "";
            }
            return StringHelper.DecryptDES(cookie["AdminName"]);
        }
        set
        {
            HttpCookie cookie = HttpContext.Current.Request.Cookies["AdminInfo"];
            if (cookie == null)
            {
                cookie = new HttpCookie("AdminInfo");
            }
            cookie.Values["AdminName"] = StringHelper.EncryptDES(value.ToString());
            HttpContext.Current.Response.SetCookie(cookie);
        }
    }

    /// <summary>
    /// 管理权限
    /// </summary>
    public static int RoleID
    {
        get
        {
            if (IsLogin)
            {
                Bll_AdminUser BAdmin_User = new Bll_AdminUser();
                Mod_AdminUser MAdmin_User = BAdmin_User.GetModel(AdminID);
                if (MAdmin_User != null)
                {
                    return MAdmin_User.RoleID;
                }
            }
            return 0;
        }
    }

    /// <summary>
    ///  获取管理员角色名称
    /// </summary>
    public static string RoleName
    {
        get
        {
            if (IsLogin)
            {
                 Bll_AdminRole BAdmin_Role = new Bll_AdminRole();
                Mod_AdminRole MAdmin_Role = BAdmin_Role.GetModel(RoleID);
                if (MAdmin_Role != null)
                {
                    return MAdmin_Role.RoleName;
                }
            }
            return "";
        }
    }

    /// <summary>
    /// 当前管理员
    /// </summary>
    public static  Mod_AdminUser MAdmin_User
    {
        get
        {
            if (IsLogin)
            {
                Bll_AdminUser BAdmin_User = new Bll_AdminUser();
                return BAdmin_User.GetModel(AdminID);
            }
            return null;
        }
    }

    /// <summary>
    /// 管理员退出
    /// </summary>
    public static void Clear()
    {
        HttpCookie cookie = HttpContext.Current.Request.Cookies["AdminInfo"];
        if (cookie != null)
        {
            cookie.Expires = DateTime.Now.AddYears(-1);
            HttpContext.Current.Response.SetCookie(cookie);
        }
    }

    /// <summary>
    /// 版本
    /// </summary>
    public static int WebSiteID
    {
        get
        {
            HttpCookie cookie = HttpContext.Current.Request.Cookies["AdminInfo"];
            if (cookie == null || cookie["WebSiteID"] == null)
            {
                return int.Parse(ConfigurationManager.AppSettings["ManageDefaultWebSiteID"]);
            }
            else
            {
                return int.Parse(StringHelper.DecryptDES(cookie["WebSiteID"]));
            }
        }
        set
        {
            HttpCookie cookie = HttpContext.Current.Request.Cookies["AdminInfo"];
            if (cookie == null)
            {
                cookie = new HttpCookie("AdminInfo");
            }
            cookie.Values["WebSiteID"] = StringHelper.EncryptDES(value.ToString());
            HttpContext.Current.Response.SetCookie(cookie);
        }
    }

    /// <summary>
    /// 验证码
    /// </summary>
    public static string ValidateCode
    {
        get
        {
            HttpCookie cookie = HttpContext.Current.Request.Cookies["AdminInfo"];
            if (cookie == null || cookie["ValidateCode"] == null)
            {
                return "";
            }
            return StringHelper.DecryptDES(cookie["ValidateCode"]);
        }
        set
        {
            HttpCookie cookie = HttpContext.Current.Request.Cookies["AdminInfo"];
            if (cookie == null)
            {
                cookie = new HttpCookie("AdminInfo");
            }
            cookie.Values["ValidateCode"] = StringHelper.EncryptDES(value.ToString());
            HttpContext.Current.Response.SetCookie(cookie);
        }
    }

    /// <summary>
    ///  设置管理员登录状态
    /// </summary>
    public static void SetAdminLogin(int AdminID, string AdminName, int WebSiteID, bool IsAutoLogin)
    {
        HttpCookie cookie = HttpContext.Current.Request.Cookies["AdminInfo"];
        if (cookie == null)
        {
            cookie = new HttpCookie("AdminInfo");
        }
        cookie.Values["AdminID"] = StringHelper.EncryptDES(AdminID.ToString());
        cookie.Values["AdminName"] = StringHelper.EncryptDES(AdminName);
        cookie.Values["WebSiteID"] = StringHelper.EncryptDES(WebSiteID.ToString());
        HttpContext.Current.Response.SetCookie(cookie);
    }

    /// <summary>
    ///  管理员操作写入日志
    /// </summary>
    public static void SetAdminLog(string Model, int AdminID, string AdminName, string IP, string Content)
    {
        
        Bll_AdminLog BAdmin_Log = new Bll_AdminLog();
        Mod_AdminLog MAdmin_Log = new Mod_AdminLog();
        MAdmin_Log.Model = Model;
        MAdmin_Log.UserID = AdminID;
        MAdmin_Log.UserName = AdminName;
        MAdmin_Log.UserIP = IP;
        MAdmin_Log.Content = Content;
        MAdmin_Log.AddDate = DateTime.Now;
        BAdmin_Log.Add(MAdmin_Log);
    }

    #endregion
}