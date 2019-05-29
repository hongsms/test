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


            case "Login": Login(context);
                break;
            case "Register": Register(context);
                break;

            case "SendRegCode": SendRegCode(context);
                break;
            case "SendCode": SendCode(context);
                break;
            case "ForgetPwd": ForgetPwd(context);
                break;
            case "ForgetPwdForEmail": ForgetPwdForEmail(context);
                break;

            case "GetSubRegion": GetSubRegion(context);
                break;

            case "EditUserInfo": EditUserInfo(context);
                break;
            case "ModifyPwd": ModifyPwd(context);
                break;

            case "AddMessage": AddMessage(context);
                break;


            case "PageComments": PageComments(context);
                break;


        }
    }


    //显示评论
    public void PageComments(HttpContext context)
    {
        Int32 TypeId = DNTRequest.GetFormInt("TypeId");
        string Model = DNTRequest.GetFormStringDecode("Model").Trim();
        int page = DNTRequest.GetFormInt("page", 0);
        Bll_Message BMessage = new Bll_Message();
        string txtWhere = string.Format("[State]=1 and [TypeID]={0} and Model='{1}'", TypeId, Model);
        string txtstr = " ";
        string PageHtml = string.Empty;
        int NumPerPage = 5;
        int Thepage = 1;
        if (page != 0)
        {
            Thepage = page;
        }

        System.Collections.Generic.List<WebSite.Model.Mod_Message> Lists = BMessage.DataTableToList(BMessage.GetListByPage(txtWhere, " AddDate Desc ", Thepage, NumPerPage).Tables[0]);
        int TotleNum = BMessage.GetRecordCount(txtWhere);
        txtstr = "<div class=\"msg\"><ul >";
        foreach (var item in Lists)
        {
            txtstr += "<li><div class=\"m\">" + item.Content + "</div><div class=\"time\">" + item.AddDate.ToString("yyyy/MM/dd") + "</div></li>";
        }
        txtstr += "</ul></div>";
        PageHtml = PageHelper.UrlAjaxPageHtml(TotleNum, Thepage, NumPerPage, "onclick=\"PageComments(", ");\"");
        txtstr = txtstr + PageHtml;
        context.Response.Write(txtstr);
    }


    public void AddMessage(HttpContext context)
    {

        string Title = DNTRequest.GetFormStringDecode("Title").Trim();
        string Model = DNTRequest.GetFormStringDecode("Model").Trim();
        string Email = DNTRequest.GetFormStringDecode("Email").Trim();
        string Company = DNTRequest.GetFormStringDecode("Company").Trim();
        string Tel = DNTRequest.GetFormStringDecode("Tel").Trim();
        string Content = DNTRequest.GetFormStringDecode("Content").Trim();

        Int32 FromID = DNTRequest.GetFormInt("FromID", 0);
        string FromName = DNTRequest.GetFormStringDecode("FromName").Trim();
        Int32 WebSiteID = DNTRequest.GetFormInt("WebSiteID", 10001);

        Bll_Message BMessage = new Bll_Message();
        Mod_Message MMessage = new Mod_Message();
        MMessage.FromID = FromID;
        MMessage.FromName = FromName;

        MMessage.Model = Model;
        MMessage.Email = Email;
        MMessage.Company = Company;
        MMessage.Tel = Tel;
        MMessage.Content = Content;
        MMessage.Title = Title;
        MMessage.State = 0;
        MMessage.WebSiteID = WebSiteID;
        MMessage.AddDate = DateTime.Now;


        //StringBuilder sb = new StringBuilder();
        //sb.AppendFormat("FromName='{0}' and DateDiff(minute,AddDate,GETDATE())<1 ", MMessage.FromName);
        //if (BMessage.GetRecordCount(sb.ToString()) > 0)
        //{
        //    context.Response.Write("<response><state>error</state><errorinfo><![CDATA[发布频率过高，请稍候!]]></errorinfo><item></item></response>");
        //    return;
        //}

        BMessage.Add(MMessage);

        context.Response.Write("<response><state>yes</state><errorinfo><![CDATA[]]></errorinfo><item></item></response>");
        return;

    }


    public void ModifyPwd(HttpContext context)
    {
        if (UserRoot.GetUserID == -1)
        {
            context.Response.Write("请先登录您的账户！");
            return;
        }
        string txtOldpwd = DNTRequest.GetFormString("txtOldpwd").Trim();
        string txtNewPwd = DNTRequest.GetFormString("txtNewPwd").Trim();

        if (txtOldpwd == "")
        {
            context.Response.Write("请输入当前密码！");
            return;
        }
        if (txtNewPwd == "")
        {
            context.Response.Write("请输入新密码！");
            return;
        }
        else
        {
            if (txtNewPwd.Length < 6 || txtNewPwd.Length > 18)
            {
                context.Response.Write("请输入6-18位英文或数字，区分大小写！");
                return;
            }
        }

        WebSite.BLL.Bll_User BUser = new WebSite.BLL.Bll_User();
        WebSite.Model.Mod_User dto = new WebSite.Model.Mod_User();

        dto = BUser.GetModel(UserRoot.GetUserID);
        if (dto != null)
        {
            if (StringHelper.GetMD5(txtOldpwd) == dto.Password)
            {
                dto.Password = StringHelper.GetMD5(txtNewPwd);
                dto.ModifyDate = DateTime.Now;
                BUser.Update(dto);
                context.Response.Write("yes");
            }
            else
            {
                context.Response.Write("当前密码输入错误！");
            }
        }
        else
        {
            context.Response.Write("用户不存在，请重新登录！");
        }
    }

    public void EditUserInfo(HttpContext context)
    {
        if (UserRoot.GetUserID == -1)
        {
            context.Response.Write("请先登录您的账户！");
            return;
        }
        string txtrealname = DNTRequest.GetFormString("txtrealname").Trim();
        string txtsex = DNTRequest.GetFormString("txtsex").Trim();
        string txtbirthdate = DNTRequest.GetFormString("txtbirthdate").Trim();
        string txtemail = DNTRequest.GetFormString("txtemail").Trim();
        string txtmobile = DNTRequest.GetFormString("txtmobile").Trim();
        if (txtbirthdate != "")
        {
            try
            {
                txtbirthdate = DateTime.Parse(txtbirthdate).ToString("yyyy-MM-dd");
            }
            catch
            {
                context.Response.Write("请输入正确的出生日期！");
                return;
            }
        }
        if (!StringHelper.IsValidEmail(txtemail))
        {
            context.Response.Write("请输入正确的邮箱！");
            return;
        }
        if (!StringHelper.IsMobile(txtmobile))
        {
            context.Response.Write("请输入正确的手机号！");
            return;
        }

        WebSite.BLL.Bll_User BUser = new WebSite.BLL.Bll_User();
        WebSite.Model.Mod_User dto = new WebSite.Model.Mod_User();
        dto = BUser.GetModel(UserRoot.GetUserID);
        if (dto != null)
        {
            dto.RealName = txtrealname;
            dto.Sex = txtsex;
            dto.BirthDate = txtbirthdate;
            dto.Email = txtemail;
            dto.Mobile = txtmobile;
            dto.ModifyDate = DateTime.Now;
            BUser.Update(dto);
            context.Response.Write("yes");
        }
        else
        {
            context.Response.Write("用户不存在，请重新登录！");
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


    public void ForgetPwdForEmail(HttpContext context)
    {
        String txtname = DNTRequest.GetFormStringDecode("txtname").Trim();
        String txtemail = DNTRequest.GetFormStringDecode("txtemail").Trim();
        if (txtname == "" || txtemail == "")
        {
            context.Response.Write("<response><state>error</state><errorinfo><![CDATA[用户名、手机和验证码为必填资料!]]></errorinfo><item></item></response>");
            return;
        }
        if (!StringHelper.IsValidEmail(txtemail))
        {
            context.Response.Write("<response><state>error</state><errorinfo><![CDATA[请填写正确的邮箱!]]></errorinfo><item></item></response>");
            return;
        }

        WebSite.BLL.Bll_User BUser = new WebSite.BLL.Bll_User();
        WebSite.Model.Mod_User MUser = BUser.GetModel(string.Format("UserName='{0}' and Email='{1}'", txtname, txtemail));
        if (MUser == null || MUser.UserName != txtname)
        {
            context.Response.Write("<response><state>error</state><errorinfo><![CDATA[用户名或邮箱错误!]]></errorinfo><item></item></response>");
            return;
        }

        string txtNewPword = RandomOperate.GenerateCheckCode(6);
        string[] attachmentsPath = null;
        SendEmail.Send sendEmail = new SendEmail.Send();
        try
        {
            //int result = SMS.Send(txtmobile, "【" + WebSiteName + "】您好，您的密码已经成功重置为：" + InsertCode(txtmobile) + "。请登录网站重新修改密码，以便您更好的记住密码！");
            bool result2 = sendEmail.SendEmail(OperateHelper.GetWebSite(10001).EmailName, OperateHelper.GetWebSite(10001).EmailPwd, txtemail, "【" + WebSiteName + "】找回密码", "【" + WebSiteName + "】您好，您的密码已经成功重置为：" + txtNewPword + "。请登录网站重新修改密码，以便您更好的记住密码！", attachmentsPath, OperateHelper.GetWebSite(10001).EmailSmtp);
            if (result2 == true)
            {
                MUser.Password = StringHelper.GetMD5(txtNewPword);
                //新网站找回密码后设置为空
                MUser.UnionID = "";
                BUser.Update(MUser);
                context.Response.Write("<response><state>yes</state><errorinfo><![CDATA[找回密码成功，请查看邮箱！]]></errorinfo><item></item></response>");
                return;
            }
            else
            {
                context.Response.Write("<response><state>error</state><errorinfo><![CDATA[邮箱发送失败]]></errorinfo><item></item></response>");
                return;
            }
        }
        catch
        {
            context.Response.Write("<response><state>error</state><errorinfo><![CDATA[邮箱发送错误]]></errorinfo><item></item></response>");
            return;
        }
    }
    public void ForgetPwd(HttpContext context)
    {

        String txtmobile = DNTRequest.GetFormStringDecode("txtmobile").Trim();
        String txtmobilecode = DNTRequest.GetFormStringDecode("txtmobilecode").Trim();

        if (txtmobile == "" || txtmobilecode == "")
        {
            context.Response.Write("<response><state>error</state><errorinfo><![CDATA[手机和验证码为必填资料!]]></errorinfo><item></item></response>");
            return;
        }
        if (txtmobile.Length != 11 || !StringHelper.IsNumberId(txtmobile))
        {
            context.Response.Write("<response><state>error</state><errorinfo><![CDATA[请填写正确的手机号码!]]></errorinfo><item></item></response>");
            return;
        }
        Bll_Code BCode = new Bll_Code();
        if (!StringHelper.IsNumberId(txtmobilecode) || !BCode.ExistsCode(txtmobile, txtmobilecode))
        {
            context.Response.Write("<response><state>error</state><errorinfo><![CDATA[验证码输入错误!]]></errorinfo><item></item></response>");
            return;
        }

        WebSite.BLL.Bll_User BUser = new WebSite.BLL.Bll_User();
        WebSite.Model.Mod_User MUser = BUser.GetModel(string.Format("Mobile='{0}'", txtmobile));
        if (MUser == null)
        {
            context.Response.Write("<response><state>error</state><errorinfo><![CDATA[用户名或手机错误!]]></errorinfo><item></item></response>");
            return;
        }

        string txtNewPword = RandomOperate.GenerateCheckCode(6);

        try
        {
            int result = SMS.Send(txtmobile, "【" + WebSiteName + "】您好，您的密码已经成功重置为：" + InsertCode(txtmobile) + "。请登录网站重新修改密码，以便您更好的记住密码！");
            if (result == 0)
            {
                MUser.Password = StringHelper.GetMD5(txtNewPword);

                BUser.Update(MUser);
                context.Response.Write("<response><state>yes</state><errorinfo><![CDATA[找回密码成功，请查看短信！]]></errorinfo><item></item></response>");
                return;
            }
            else
            {
                context.Response.Write("<response><state>error</state><errorinfo><![CDATA[短信接口发送失败]]></errorinfo><item></item></response>");
                return;
            }
        }
        catch
        {
            context.Response.Write("<response><state>error</state><errorinfo><![CDATA[短信接口错误]]></errorinfo><item></item></response>");
            return;
        }
    }

    public void SendCode(HttpContext context)
    {
        string txtmobile = DNTRequest.GetFormStringDecode("txtmobile").Trim();
        //Regex Reg = new Regex("^1[345678]\\d{9}$");
        //if (!Reg.IsMatch(txtmobile))
        //{
        //    context.Response.Write("<response><state>error</state><errorinfo><![CDATA[请填写正确的手机号码]]></errorinfo><item></item></response>");
        //    return;
        //}
        WebSite.BLL.Bll_User BUser = new WebSite.BLL.Bll_User();

    
        if (!BUser.Exists(string.Format("Mobile='{0}'", txtmobile)))
        {
            context.Response.Write("<response><state>error</state><errorinfo><![CDATA[该手机号码不存在，请重新输入！]]></errorinfo><item></item></response>");
            return;
        }

        try
        {

            int result = SMS.Send(txtmobile, "【" + WebSiteName + "】您好，您的验证码是：" + InsertCode(txtmobile) + "。请正确输入验证码。感谢您的支持。");
            if (result == 0)
            {
                context.Response.Write("<response><state>yes</state><errorinfo><![CDATA[验证码已发送，请查收]]></errorinfo><item></item></response>");
                return;
            }
            else
            {
                context.Response.Write("<response><state>error</state><errorinfo><![CDATA[验证码发送失败]]></errorinfo><item></item></response>");
                return;
            }
        }
        catch
        {
            context.Response.Write("<response><state>error</state><errorinfo><![CDATA[短信接口错误]]></errorinfo><item></item></response>");
            return;
        }
    }


    public void SendRegCode(HttpContext context)
    {
        string txtmobile = DNTRequest.GetFormStringDecode("txtmobile").Trim();
        //Regex Reg = new Regex("^1[345678]\\d{9}$");
        //if (!Reg.IsMatch(txtmobile))
        //{
        //    context.Response.Write("<response><state>error</state><errorinfo><![CDATA[请填写正确的手机号码]]></errorinfo><item></item></response>");
        //    return;
        //}
        WebSite.BLL.Bll_User BUser = new WebSite.BLL.Bll_User();
        if (BUser.ExistsMobile(txtmobile))
        {
            context.Response.Write("<response><state>error</state><errorinfo><![CDATA[该手机号码已存在，请重新输入！]]></errorinfo><item></item></response>");
            return;
        }
        try
        {
            int result = SMS.Send(txtmobile, "【" + WebSiteName + "】您好，您的验证码是：" + InsertCode(txtmobile) + "。请正确输入验证码。感谢您的支持。");
            if (result == 0)
            {
                context.Response.Write("<response><state>yes</state><errorinfo><![CDATA[验证码已发送，请查收]]></errorinfo><item></item></response>");
                return;
            }
            else
            {
                context.Response.Write("<response><state>error</state><errorinfo><![CDATA[验证码发送失败]]></errorinfo><item></item></response>");
                return;
            }
        }
        catch
        {
            context.Response.Write("<response><state>error</state><errorinfo><![CDATA[短信接口错误]]></errorinfo><item></item></response>");
            return;
        }
    }

    private string InsertCode(string mobile)
    {
        string code = CreateCode();
        Bll_Code BCode = new Bll_Code();
        Mod_Code dto = BCode.GetModel(string.Format("Object='{0}'", mobile));
        if (dto == null)
        {
            dto = new Mod_Code();
            dto.Object = mobile;
            dto.AddDate = DateTime.Now;
            dto.Model = "Register";
            dto.ModifyDate = DateTime.Now;
            dto.State = 1;
            dto.Code = code;
            BCode.Add(dto);
        }
        else
        {
            if (dto.ModifyDate > DateTime.Now.AddHours(-1))
            {
                code = dto.Code;
            }
            else
            {
                dto.ModifyDate = DateTime.Now;
                dto.Code = code;
                BCode.Update(dto);
            }
        }
        return code;
    }
    private string CreateCode()
    {
        return "111111";
        //定义验证码的字符数组 
        char[] allCharArray = { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' };
        //定义验证码字符串 
        string randomCode = "";
        Random rand = new Random();
        //生成6位验证码字符串 
        for (int i = 0; i < 6; i++)
            randomCode += allCharArray[rand.Next(allCharArray.Length)];
        return randomCode;
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
        string txtname = DNTRequest.GetFormStringDecode("txtname").Trim();
        string txtpwd = DNTRequest.GetFormStringDecode("txtpwd").Trim();
        string txtcode = DNTRequest.GetFormStringDecode("txtcode").Trim();
        //string txtmobile = DNTRequest.GetFormStringDecode("txtmobile").Trim();
        //string txtemail = DNTRequest.GetFormStringDecode("txtemail").Trim();
        //string txtmobilecode = DNTRequest.GetFormStringDecode("txtmobilecode").Trim();
        //string txtuid = DNTRequest.GetFormStringDecode("txtuid").Trim();


        //if (txtname == "" || txtpwd == "" || txtemail == "")
        //{
        //    context.Response.Write("请填写完整资料！");
        //    return;
        //}
        //if (txtmobilecode == "")
        //{
        //    context.Response.Write("请输入手机验证码！");
        //    return;
        //}
        //if (txtname.Length < 5 || txtname.Length > 15)
        //{
        //    context.Response.Write("账户长度必须为5-15字符！");
        //    return;
        //}
        //if (!StringHelper.QuickValidate("^[a-zA-Z0-9]*$", txtname))
        //{
        //    context.Response.Write("账户只能包含数字、字母！");
        //    return;
        //}
        //if (txtpwd.Length < 6 || txtpwd.Length > 20)
        //{
        //    context.Response.Write("请填写正确的密码(6-20个字符)！");
        //    return;
        //}
        //if (!StringHelper.IsValidEmail(txtemail))
        //{
        //    context.Response.Write("请填写正确的邮箱！");
        //    return;
        //}
        //if (txtmobile.Length != 11 || !StringHelper.IsNumberId(txtmobile))
        //{
        //    context.Response.Write("请填写正确的手机号码！");
        //    return;
        //}

        WebSite.BLL.Bll_User BUser = new WebSite.BLL.Bll_User();
        WebSite.Model.Mod_User MUser = new WebSite.Model.Mod_User();




        if (BUser.Exists(string.Format("UserName='{0}'", txtname)))
        {
            context.Response.Write("The account already exists, please re-enter！");
            return;
        }
        if (!txtcode.ToUpper().Equals(AdminManage.ValidateCode))
        {
            context.Response.Write("Verification code input error, please re-enter！");
            return;
        }
        //if (BUser.ExistsMobile(txtmobile))
        //{
        //    context.Response.Write("该手机号码已存在，请重新输入！");
        //    return;
        //}
        //if (BUser.ExistsEmail(txtemail))
        //{
        //    context.Response.Write("该邮箱已存在，请重新输入！");
        //    return;
        //}

        //Bll_Code BCode = new Bll_Code();
        //if (!BCode.ExistsCode(txtmobile, txtmobilecode))
        //{
        //    context.Response.Write("请输入正确的手机验证码！");
        //    return;
        //}
        //if (txtuid != "0")
        //{
        //    Mod_User dto = BUser.GetModel(Convert.ToInt32(txtuid));
        //    dto.Integral = dto.Integral + 10;
        //    dto.TotalIntegral = dto.TotalIntegral + 10;
        //    BUser.Update(dto);
        //    OperateHelper.SetUserLog(dto.ID, dto.UserName, "Integral", "会员推荐注册赠送积分。", "+10", 10001);

        //}

        MUser.UserName = txtname;
        MUser.Password = StringHelper.GetMD5(txtpwd);
        //MUser.Email = txtemail;
        //MUser.Mobile = txtmobile;

        MUser.State = 1;
        MUser.EmailAudit = 1;
        //MUser.EmailCode = txtmobilecode;
        MUser.RegisterDate = DateTime.Now;
        MUser.RegisterIP = System.Web.HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
        MUser.WebSiteID = 10001;
        MUser.Model = "Product";

        BUser.Add(MUser);

        UserRoot.SetUserLogin(MUser.ID, MUser.UserName, MUser.TypeID);

        ////UserRoot.GetUserID = MUser.ID;
        ////UserRoot.GetUserName = MUser.UserName;

        //记录登录
        OperateHelper.SetLoginLog(MUser.ID, 10001);

        context.Response.Write("yes");
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
