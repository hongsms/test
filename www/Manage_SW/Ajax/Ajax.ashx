<%@ WebHandler Language="C#" Class="Ajax" %>

using System;
using System.Web;
using WebSite.Common;
using WebSite.BLL;
using WebSite.Model;
using System.Collections.Generic;
using System.Web.SessionState;
using System.Data;
using System.Text;
using System.Collections;

public class Ajax : IHttpHandler, IRequiresSessionState
{
    
    public void ProcessRequest (HttpContext context) {
        context.Response.ContentType = "text/plain";
        context.Response.Buffer = true;
        context.Response.ExpiresAbsolute = DateTime.Now.AddDays(-1);
        context.Response.AddHeader("pragma", "no-cache");
        context.Response.AddHeader("cache-control", "");
        context.Response.CacheControl = "no-cache";

        string action = DNTRequest.GetFormString("action");
        switch (action)
        {
            case "Login": Login(context);
                break;
            case "GetSubRegion": GetSubRegion(context);
                break;
        }
    }

    public void GetSubRegion(HttpContext context)
    {
        int parentid = DNTRequest.GetFormInt("parentid", 0);
        int WebSiteID = DNTRequest.GetFormInt("WebSiteID", 0);

        if (parentid == 0)
        {
            context.Response.Write("no");
            return;
        }
        Bll_Region BRegion = new Bll_Region();
        DataSet ds = BRegion.GetList(0, " ParentID=" + parentid + " and WebSiteID=" + WebSiteID + " ", " OrderBy desc,RegionPath asc ");
        if (ds.Tables.Count > 0)
        {
            DataTable dt = ds.Tables[0];
            if (dt.Rows.Count > 0)
            {
                string txtstr = string.Empty;
                txtstr = "[";
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    txtstr += "'" + dt.Rows[i]["RegionName"].ToString() + "',";
                    txtstr += "'" + dt.Rows[i]["ID"].ToString() + "',";
                }
                if (txtstr.Length > 1)
                {
                    txtstr = txtstr.Substring(0, txtstr.Length - 1);
                }
                txtstr += "]";
                context.Response.Write(txtstr);
                return;
            }
            else
            {
                context.Response.Write("no");
                return;
            }
        }
        else
        {
            context.Response.Write("no");
            return;
        }
    }

    public void Login(HttpContext context)
    {
        string UserName = DNTRequest.GetFormString("UserName").Trim();
        string Password = DNTRequest.GetFormString("Password").Trim();
        string ImgCode = DNTRequest.GetFormString("Code").Trim();

        if (string.IsNullOrEmpty(UserName) || string.IsNullOrEmpty(Password) || string.IsNullOrEmpty(ImgCode))
        {
            context.Response.Write("输入内容不能为空！");
            return;
        }

        if (!ImgCode.ToUpper().Equals(AdminManage.ValidateCode))
        {
            context.Response.Write("图片验证码输入错误，请重新输入！");
            return;
           
        }
        
        
        //判断登录错误次数
        string UserIP = context.Request.ServerVariables["REMOTE_ADDR"];
        Bll_AdminLog BAdmin_Log = new Bll_AdminLog();
        if (BAdmin_Log.GetLoginNum(UserIP) > 30)
        {
            context.Response.Write("您登录次数过多，请5分钟后再试。");
            return;
        }
        
        Bll_AdminUser BAdmin_User = new Bll_AdminUser();
        Mod_AdminUser MAdmin_User = BAdmin_User.ExistsLogin(UserName, StringHelper.GetMD5(Password));
        if (MAdmin_User != null)
        {
            if (MAdmin_User.State == 1)
            {
                //设置登录状态
                AdminManage.SetAdminLogin(MAdmin_User.ID, MAdmin_User.UserName, 10001, false);
                //写入日志
                AdminManage.SetAdminLog("AdminLogin", MAdmin_User.ID, MAdmin_User.UserName, context.Request.ServerVariables["REMOTE_ADDR"], "后台登陆成功");
                //返回信息
                context.Response.Write("yes");
                return;
            }
            else
            {
                //写入日志
                AdminManage.SetAdminLog("AdminLogin", 0, UserName, context.Request.ServerVariables["REMOTE_ADDR"], "后台登陆失败-账户已被锁定");
                //返回信息
                context.Response.Write("该账户已被锁定，请联系管理员！");
                return;
            }
        }
        else
        {
            //写入日志
            AdminManage.SetAdminLog("AdminLogin", 0, UserName, context.Request.ServerVariables["REMOTE_ADDR"], "后台登陆失败");
            //返回信息
            context.Response.Write("用户名或密码错误！");
            return;
        }
    }
 
    public bool IsReusable {
        get {
            return false;
        }
    }

}