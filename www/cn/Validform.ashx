<%@ WebHandler Language="C#" Class="Validform" %>

using System;
using System.Web;

public class Validform : IHttpHandler, System.Web.SessionState.IRequiresSessionState
{

    public void ProcessRequest(HttpContext context)
    {
      
        context.Response.ContentType = "text/plain";
        context.Response.Buffer = true;
        context.Response.ExpiresAbsolute = DateTime.Now.AddDays(-1);
        context.Response.AddHeader("pragma", "no-cache");
        context.Response.AddHeader("cache-control", "");
        context.Response.CacheControl = "no-cache";

        Valid(context);
    }

    #region 用户名、EMAIL、验证码验证、短信验证码、手机号、金额
    public void Valid(HttpContext context)
    {
        string name = WebSite.Common.DNTRequest.GetFormString("name");
        string param = WebSite.Common.DNTRequest.GetFormString("param");
        WebSite.Model.Mod_User model = new WebSite.Model.Mod_User();
        WebSite.BLL.Bll_User bllUser = new WebSite.BLL.Bll_User();
        bool result = true;
        switch (name)
        {
            case "UserName":
            case "UserNamePhone":
                model = bllUser.GetModel(string.Format("UserName='{0}'", param));
                if (model == null) { result = false; }
                context.Response.Write(result ? "该帐户已存在，请重新输入" : "y");
                break;
            default:
                context.Response.Write(name);
                break;
        }
    }
    #endregion

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }

}