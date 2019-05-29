using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Globalization;
using System.Configuration;


public class SqlstrAny : IHttpModule
{
    public void Init(HttpApplication application)
    {
        application.BeginRequest += (new
        EventHandler(this.Application_BeginRequest));
    }
    private void Application_BeginRequest(Object source, EventArgs e)
    {
        ProcessRequest pr = new ProcessRequest();
        pr.StartProcessRequest();
    }
    public void Dispose()
    {
    }
}

public class ProcessRequest
{
    private static string word = ConfigurationManager.AppSettings["SqlInject"].ToString();

    /// <summary>
    /// 比较内容类型
    /// </summary>
    /// <param name="s1"></param>
    /// <param name="s2"></param>
    /// <returns></returns>
    private static bool StringStartsWithAnotherIgnoreCase(string s1, string s2)
    {
        return (string.Compare(s1, 0, s2, 0, s2.Length, true, CultureInfo.InvariantCulture) == 0);
    }


    #region SQL注入式攻击代码分析
    /// <summary>
    /// 处理用户提交的请求
    /// </summary>
    public void StartProcessRequest()
    {
        if ((System.Web.HttpContext.Current.Request.RawUrl.ToLower().IndexOf("manage_sw") == -1))
        {


            var request = System.Web.HttpContext.Current.Request;

            try
            {
                //遍历Get参数。
                if (request.QueryString != null)
                {
                    foreach (string i in request.QueryString)
                    {
                        string putData = request.QueryString[i].ToString();
                        if (SqlFilter(putData))
                        {
                            ResponseWarnMessage("GET数据有恶意字符", putData);
                        }
                    }
                }
                //遍历Post参数
                if (request.Form != null)
                {
                    foreach (string i in request.Form)
                    {

                        string putData = request.Form[i].ToString();
                        if (SqlFilter(putData.Replace("+", " ")))
                        {
                            ResponseWarnMessage("POST数据有恶意字符", putData);
                        }
                    }
                }

                //遍历cookie参数
                if (request.Cookies != null)
                {
                    foreach (string i in request.Cookies)
                    {
                        string putData = request.Cookies[i].Value;
                        if (SqlFilter(putData))
                        {
                            ResponseWarnMessage("COOKIE数据有恶意字符", putData);
                        }
                    }
                }
            }
            catch
            {
                // 错误处理: 处理用户提交信息!
                //ResponseWarnMessage("SQL防注入模块异常","未知");
            }
        }
    }
    /// <summary>
    /// 非安全行为 输出警告信息
    /// </summary>
    /// <param name="errorMessage"></param>
    /// <param name="putData"></param>
    private void ResponseWarnMessage(string errorMessage, string putData)
    {
        //记录一下恶意攻击行为attack
        //var modLog = new Model.sw_Log();
        //modLog.Content = errorMessage + "内容：" + putData;
        //modLog.CreateTime = DateTime.Now;
        //modLog.Ip = Web.Common.GetIp();
        //modLog.Model = "sys_attack";
        //modLog.Name = "恶意访问行为";
        //Ins.bllLog.Insert(modLog);
        HttpContext.Current.Response.Redirect("/error/prompt.html");
    }
    /// <summary> 
    /// 检查过滤设定的危险字符
    /// </summary> 
    /// <param name="InText">要过滤的字符串 </param> 
    /// <returns>如果参数存在不安全字符，则返回true </returns> 
    public bool SqlFilter(string InText)
    {
        if (InText == null)
            return false;
        foreach (string i in word.Split('|'))
        {
            if ((InText.ToLower().IndexOf(i + " ") > -1) || (InText.ToLower().IndexOf(" " + i) > -1))
            {
                return true;
                //return false;
            }
        }
        return false;
    }
    #endregion
}

