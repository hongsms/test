using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Net;
using System.IO;
using System.Xml;
using System.Text;

/// <summary>
///SMS 的摘要说明
/// </summary>
public class SMS
{
    private static string SN = "8SDdK-EMY-6699-AGWTA";
    private static string PWD = "65d1234";
    private static string sb = "";
    public SMS()
    {
        //
        //TODO: 在此处添加构造函数逻辑
        //
    }
    ///// <summary>
    ///// 短信接口注册
    ///// </summary>
    ///// <param name="SN">序列号</param>
    ///// <param name="PWD">密码</param>
    ///// <returns>返回操作结果（如：注册成功）</returns>
    public static int Register()
    {
        string _Response = "";
        //int result = EUCPComm.Register(SN, PWD, "1", "1", "1", "1", "1", "1", "1", "1");
        //if (result == 1)
        //    _Response = "注册成功";
        //else if (result == 101)
        //    _Response = "网络故障";
        //else if (result == 102)
        //    _Response = "其它故障";
        //else if (result == 0)
        //    _Response = "失败";
        //else if (result == 100)
        //    _Response = "序列号码为空或无效";
        //else if (result == 103)
        //    _Response = "注册企业基本信息失败，当软件注册号码注册成功,但整体还是失败，要重新注册";
        //else if (result == 104)
        //    _Response = "注册信息填写不完整";
        //else if (result == 114)
        //    _Response = "得到标识错误";
        //else
        //    _Response = "其他故障值：" + result.ToString();
        sb+="http://hprpt2.eucp.b2m.cn:8080/sdkproxy/regist.action?";
        sb+="cdkey="+SN;
        sb += "&password=" + PWD;
        _Response = GetSource(sb.ToString());
        string[] result = ReadXML(_Response);
        if (result != null)
        {
            return Convert.ToInt32(result[0]);
        }
        else
        {
            return 1;
        }
    }
    ///// <summary>
    ///// 短信接口注销
    ///// </summary>
    ///// <param name="SN">序列号</param>
    ///// <returns>返回操作结果</returns>
    //public static int UnRegister()
    //{
    //    //return EUCPComm.UnRegister(SN).ToString();
    //    string _Response = "";
    //    sb.Append("http://hprpt2.eucp.b2m.cn:8080/sdkproxy/logout.action?");
    //    sb.AppendFormat("cdkey={0}", SN);
    //    sb.AppendFormat("&password={0}", PWD);
    //    _Response = GetSource(sb.ToString());
    //    string[] result = ReadXML(_Response);
    //    if (result != null)
    //    {
    //        return Convert.ToInt32(result[0]);
    //    }
    //    else
    //    {
    //        return 1;
    //    }
    //}

    /// <summary>
    ///  发送短信
    /// </summary>
    /// <param name="Mobile">手机号（多个号码时以“，”分格开）</param>
    /// <param name="Content">短信内容小于70个汉字</param>
    /// <returns>返回1时为发送成功</returns>
    public static int Send(string Mobile, string Content)
    {
        return 0;
        string _Response = "";
        //int result=  EUCPComm.SendSMS(SN, Mobile, Content+ConfigManager.GeneralConfig.smsSign.Trim(), "");
        //if (result == 1)
        //    _Response = "发送成功";
        //else if (result == 101)
        //    _Response = "网络故障";
        //else if (result == 102)
        //    _Response = "其它故障";
        //else if (result == 0)
        //    _Response = "失败";
        //else if (result == 100)
        //    _Response = "序列号码为空或无效";
        //else if (result == 107)
        //    _Response = "手机号码为空或者超过1000个";
        //else if (result == 108)
        //    _Response = "手机号码分割符号不正确";
        //else if (result == 109)
        //    _Response = "部分手机号码不正确，已删除，其余手机号码被发送";
        //else if (result == 110)
        //    _Response = "短信内容为空或超长（70个汉字）";
        //else if (result == 201)
        //    _Response = "计费失败，请充值";
        //else
        //    _Response = "其他故障值：" + result.ToString();
        //if (result != 1)
        //{
        //    LoggingManager log = new LoggingManager();
        //    log.LoggingDirectory = "SMSLog";
        //    log.AppendFormat("短信发送：错误代号：{0}，错误提示：{0}", result, _Response);
        //    log.Save();
        //}
        //return result;
        sb = "http://hprpt2.eucp.b2m.cn:8080/sdkproxy/sendsms.action?";
        sb = sb + "cdkey=" + SN;
        sb = sb + "&password=" + PWD;
        sb = sb + "&phone=" + Mobile;
        sb = sb + "&message=" + Content;
        sb = sb + "&addserial=";
        _Response = GetSource(sb);
        string[] result = ReadXML(_Response);
        if (result != null)
        {
            return Convert.ToInt32(result[0]);
        }
        else
        {
            return 1;
        }
    }

    //public string SMSBalance()
    //{
    //    string _Response = "";
    //    //int result = EUCPComm.Register(SN, PWD, "1", "1", "1", "1", "1", "1", "1", "1");
    //    //if (result == 1)
    //    //    _Response = "注册成功";
    //    //else if (result == 101)
    //    //    _Response = "网络故障";
    //    //else if (result == 102)
    //    //    _Response = "其它故障";
    //    //else if (result == 0)
    //    //    _Response = "失败";
    //    //else if (result == 100)
    //    //    _Response = "序列号码为空或无效";
    //    //else if (result == 103)
    //    //    _Response = "注册企业基本信息失败，当软件注册号码注册成功,但整体还是失败，要重新注册";
    //    //else if (result == 104)
    //    //    _Response = "注册信息填写不完整";
    //    //else if (result == 114)
    //    //    _Response = "得到标识错误";
    //    //else
    //    //    _Response = "其他故障值：" + result.ToString();
    //    sb.Append("http://hprpt2.eucp.b2m.cn:8080/sdkproxy/querybalance.action?");
    //    sb.AppendFormat("cdkey={0}", SN);
    //    sb.AppendFormat("&password={0}", PWD);
    //    _Response = GetSource(sb.ToString());
    //    string[] result = ReadXML(_Response);
    //    if (result != null)
    //    {
    //        return result[1];
    //    }
    //    else
    //    {
    //        return "fail";
    //    }
    //}

    private static string GetSource(string url)
    {
        string result = "";
        if (!string.IsNullOrEmpty(url))
        {
            WebRequest request = null;
            HttpWebResponse response = null;
            Stream stream = null;
            StreamReader sr = null;
            try
            {
                request = WebRequest.Create(url);
                request.Credentials = CredentialCache.DefaultCredentials;
                request.Method = "POST";
                response = (HttpWebResponse)request.GetResponse();
                stream = response.GetResponseStream();
                sr = new StreamReader(stream, System.Text.Encoding.UTF8);
                result = sr.ReadToEnd();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (sr != null)
                {
                    sr.Close();
                    sr.Dispose();
                }

                if (stream != null)
                {
                    stream.Close();
                    stream.Dispose();
                }
            }
        }
        return result;
    }

    private static string[] ReadXML(string xml)
    {
        string[] result = new string[2];
        if (!string.IsNullOrEmpty(xml))
        {
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(xml.Trim());
            result[0] = doc.SelectSingleNode("response/error").InnerText;
            result[1] = doc.SelectSingleNode("response/message").InnerText;
        }
        return result;
    }
    
}
