<%@ WebHandler Language="C#" Class="Web_ZNHZWY_CN_Ajax" %>

using System;
using System.Web;
using WebSite.Common;
using System.Web.SessionState;
using System.Text;
using WebSite.BLL;
using System.Data;
using WebSite.Model;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text.RegularExpressions;

public class Web_ZNHZWY_CN_Ajax : IHttpHandler, IRequiresSessionState
{
    protected string WebSiteName = OperateHelper.GetWebSite(10001).WebName;

    public void ProcessRequest(HttpContext context)
    {
        context.Response.ContentType = "text/plain";
        context.Response.Buffer = true;
        context.Response.ExpiresAbsolute = DateTime.Now.AddDays(-1);
        context.Response.AddHeader("pragma", "no-cache");
        context.Response.AddHeader("cache-control", "");
        context.Response.CacheControl = "no-cache";

        switch (DNTRequest.GetFormString("action"))
        {


            case "Login":
                Login(context);
                break;
            case "Register":
                Register(context);
                break;

            case "AddMessage":
                AddMessage(context);
                break;


            case "PageComments":
                PageComments(context);
                break;

            case "ValidateImgCode":
                ValidateImgCode(context);
                break;
        }
    }
    public void ValidateImgCode(HttpContext context)
    {

        String ImgCode = DNTRequest.GetFormStringDecode("CodeKey").Trim();
        if (ImgCode == "")
        {
            context.Response.Write("<response><state>error</state><errorinfo><![CDATA[验证码为必填资料!]]></errorinfo><item></item></response>");
            return;
        }

        if (!ImgCode.ToUpper().Equals(AdminManage.ValidateCode))
        {
            context.Response.Write("<response><state>error</state><errorinfo><![CDATA[图片验证码输入错误，请重新输入！]]></errorinfo><item></item></response>");
            return;
        }


        context.Response.Write("<response><state>yes</state><errorinfo><![CDATA[]]></errorinfo><item></item></response>");

    }


    //显示评论
    public void PageComments(HttpContext context)
    {
        Int32 FromID = DNTRequest.GetFormInt("FromID");
        string Model = DNTRequest.GetFormStringDecode("Model").Trim();
        int page = DNTRequest.GetFormInt("page", 0);
        Bll_Message BMessage = new Bll_Message();
        string txtWhere = string.Format(" State=1 and [FromID]={0} and Model='{1}'", FromID, Model);
        string txtstr = " ";
        string PageHtml = string.Empty;
        int NumPerPage = 10;
        int Thepage = 1;
        if (page != 0)
        {
            Thepage = page;
        }

        System.Collections.Generic.List<WebSite.Model.Mod_Message> Lists = BMessage.DataTableToList(BMessage.GetListByPage(txtWhere, " AddDate Desc ", Thepage, NumPerPage).Tables[0]);
        int TotleNum = BMessage.GetRecordCount(txtWhere);
        txtstr = "<ul >";
        foreach (var item in Lists)
        {
            txtstr += "<li class=\"clearfix\"><div class=\"head clearfix\"><i class=\"fl tac cf\">" + (Lists.IndexOf(item) + 1) + "</i><div class=\"info fl pl20 pr20 clearfix\">";
            txtstr += "<div class=\"txt fl els sm-12\">" + item.Content + "</span></div>";
            txtstr += "<div class=\"fr sm-dn\">留言时间：" + item.AddDate.ToString() + "</div></div></div>";


            if (item.ReplyContent != "")
            {
                txtstr += "<div class=\"info txt els pl20 pr20 fr\"><em>管理员：</em>" + item.ReplyContent + "</div>";
            }

            txtstr += "</li>";
        }
        txtstr += "</ul>";
        PageHtml = PageHelper.UrlAjaxPageHtml(TotleNum, Thepage, NumPerPage, "onclick=\"PageComments(", ");\"");
        txtstr = txtstr + PageHtml;
        context.Response.Write(txtstr);
    }


    public void AddMessage(HttpContext context)
    {

        string FromName = DNTRequest.GetFormStringDecode("FromName").Trim();
        Int32 FromID = DNTRequest.GetFormInt("FromID", 0);
        string Content = DNTRequest.GetFormStringDecode("Content").Trim();

        //if (UserRoot.GetUserID==-1)
        //{
        //    context.Response.Write("<response><state>login</state><errorinfo><![CDATA[请先登陆您的帐号！]]></errorinfo><item></item></response>");
        //    return;
        //}


        String ImgCode = DNTRequest.GetFormStringDecode("CodeKey").Trim();
        if (ImgCode == "")
        {
            context.Response.Write("<response><state>error</state><errorinfo><![CDATA[验证码为必填资料!]]></errorinfo><item></item></response>");
            return;
        }

        if (!ImgCode.ToUpper().Equals(AdminManage.ValidateCode))
        {
            context.Response.Write("<response><state>error</state><errorinfo><![CDATA[图片验证码输入错误，请重新输入！]]></errorinfo><item></item></response>");
            return;
        }

        Int32 WebSiteID = DNTRequest.GetFormInt("WebSiteID", 10001);

        Bll_Message BMessage = new Bll_Message();
        Mod_Message MMessage = new Mod_Message();
        MMessage.FromID = FromID;
        MMessage.FromName = FromName;
        MMessage.Model = "Comments";
        //MMessage.UserID = UserRoot.GetUserID;
        //MMessage.UserName = UserRoot.GetUserName;
        MMessage.State = 0;
        MMessage.WebSiteID = WebSiteID;
        MMessage.AddDate = DateTime.Now;
        MMessage.Content = Content;

        //StringBuilder sb = new StringBuilder();
        //sb.AppendFormat("FromName='{0}' and DateDiff(minute,AddDate,GETDATE())<1 ", MMessage.FromName);
        //if (BMessage.GetRecordCount(sb.ToString()) > 0)
        //{
        //    context.Response.Write("<response><state>error</state><errorinfo><![CDATA[发布频率过高，请稍候!]]></errorinfo><item></item></response>");
        //    return;
        //}

        BMessage.Add(MMessage);
        AdminManage.ValidateCode = "";
        context.Response.Write("<response><state>yes</state><errorinfo><![CDATA[]]></errorinfo><item></item></response>");
        return;

    }



    public void Login(HttpContext context)
    {
        string username = DNTRequest.GetFormStringDecode("username").Trim();
        string userpwd = StringHelper.GetMD5(DNTRequest.GetFormStringDecode("userpwd").Trim());
        string strckeval = DNTRequest.GetFormString("ckeval");
        WebSite.BLL.Bll_User BUser = new WebSite.BLL.Bll_User();

        //if (username == "")
        //{
        //    context.Response.Write("请输入账户和密码！");
        //    return;
        //}
        //List<Mod_User> MUserlist = BUser.GetModelList(" UserName='" + StringHelper.CleanDangerSQL(username) + "' and Email !='' ");
        //if (MUserlist.Count == 1)
        //{
        //if (MUserlist.First().UnionID == "1")
        //{
        //    context.Response.Write("findpassword");
        //    return;
        //}
        //else
        //{
        //if (userpwd == "")
        //{
        //    context.Response.Write("请输入密码！");
        //    return;
        //}
        //if (username.Length < 2 || username.Length > 15)
        //{
        //    context.Response.Write("账户长度必须为2-15字符！");
        //    return;
        //}

        WebSite.Model.Mod_User MUser = BUser.LoginUser(username, userpwd);
        if (MUser == null)
        {
            context.Response.Write("账户或密码错误，请重新输入！");
            return;
        }
        if (MUser.State != 1)
        {
            context.Response.Write("账户被锁定，请联系管理员！");
            return;
        }
        if (strckeval == "1")
        {
            UserRoot.IsAutoLogin = true;
        }


        UserRoot.SetUserLogin(MUser.ID, MUser.UserName, MUser.TypeID);
        //记录登录
        OperateHelper.SetLoginLog(MUser.ID, 10001);

        context.Response.Write("yes");
        //}
        //}

    }
    public void Register(HttpContext context)
    {
        string UserNamePhone = DNTRequest.GetFormStringDecode("UserNamePhone").Trim();
        string RealName = DNTRequest.GetFormStringDecode("RealName").Trim();
        string txtpwd = DNTRequest.GetFormStringDecode("txtpwd").Trim();
        string txtmobile = DNTRequest.GetFormStringDecode("txtmobile").Trim();

        String ImgCode = DNTRequest.GetFormStringDecode("ImgCode").Trim();
        if (ImgCode == "")
        {
            context.Response.Write("<response><state>error</state><errorinfo><![CDATA[验证码为必填资料!]]></errorinfo><item></item></response>");
            return;
        }

        if (!ImgCode.ToUpper().Equals(AdminManage.ValidateCode))
        {
            context.Response.Write("<response><state>error</state><errorinfo><![CDATA[图片验证码输入错误，请重新输入！]]></errorinfo><item></item></response>");
            return;
        }




        WebSite.BLL.Bll_User BUser = new WebSite.BLL.Bll_User();
        WebSite.Model.Mod_User MUser = new WebSite.Model.Mod_User();




        if (BUser.Exists(string.Format("UserName='{0}'", UserNamePhone)))
        {
            context.Response.Write("该帐户已存在，请重新输入！");
            return;
        }


        MUser.UserName = UserNamePhone;
        MUser.RealName = RealName;
        MUser.Password = StringHelper.GetMD5(txtpwd);
        //MUser.Email = txtemail;
        MUser.Mobile = txtmobile;

        MUser.State = 1;
        MUser.EmailAudit = 1;
        //MUser.EmailCode = txtmobilecode;
        MUser.RegisterDate = DateTime.Now;
        MUser.RegisterIP = System.Web.HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
        MUser.WebSiteID = 10001;
        MUser.Model = "USER";

        BUser.Add(MUser);

        UserRoot.SetUserLogin(MUser.ID, MUser.UserName, MUser.TypeID);

        ////UserRoot.GetUserID = MUser.ID;
        ////UserRoot.GetUserName = MUser.UserName;

        //记录登录
        OperateHelper.SetLoginLog(MUser.ID, 10001);

        context.Response.Write("<response><state>yes</state><errorinfo><![CDATA[]]></errorinfo><item></item></response>");

        return;
    }



    public bool IsReusable
    {
        get
        {
            return false;
        }
    }
}
