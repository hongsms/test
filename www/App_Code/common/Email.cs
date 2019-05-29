using System;
//添加的命名空间引用
using System.Text.RegularExpressions;
using System.Net;
using System.Net.Mail;
using System.Configuration;

namespace SendEmail
{
    public class Send
    {
        /// <summary>
        /// 发送邮件
        /// </summary>
        /// <param name="fromEmail">发送者邮箱地址</param>
        /// <param name="fromPsaaWord">发送者邮箱密码</param>
        /// <param name="toEmail">收件人邮箱地址</param>
        /// <param name="subject">邮件主题</param>
        /// <param name="body">邮件主要内容</param>
        /// <param name="attachmentsPath">邮件附件(无附件时为null)</param>
        /// <param name="smtp">大部分邮件服务器均加smtp.前缀</param>
        /// <returns>发送成功返回true,失败false</returns>
        public bool SendEmail(string fromEmail, string fromPsaaWord, string toEmail, string subject, string body, string[] attachmentsPath, string smtpStr)
        {
            bool Success = true;
            System.Text.StringBuilder errorMsg = new System.Text.StringBuilder();
            if (!Regex.IsMatch(fromEmail, @"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$"))
            {
                errorMsg.Append("参数fromEmail格式不正确!");
                Success = false;
            }
            if (!Regex.IsMatch(toEmail, @"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$"))
            {
                errorMsg.Append("参数toEmail格式不正确!");
                Success = false;
            }
            if (subject.Trim() == "")
            {
                errorMsg.Append("不支持无主题的邮件!");
                Success = false;
            }
            if (body.Trim() == "")
            {
                errorMsg.Append("不支持无内容的邮件!");
                Success = false;
            }
            try
            {
                SendReady(fromEmail, fromPsaaWord, toEmail, subject, body, attachmentsPath, smtpStr);
            }
            catch (Exception err)
            {
                Success = false;
                throw new Exception(errorMsg.ToString() + err);
            }
            return Success;
        }

        public void SendReady(string fromEmail, string fromPsaaWord, string toEmail, string subject, string body, string[] attachmentsPath, string smtpStr)
        {
            //验证合法邮箱的表达式,正确时返回true
            if (!Regex.IsMatch(fromEmail, @"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$"))
            {
                //邮箱不合法
            }
            //发件人地址
            MailAddress from = new MailAddress(fromEmail);
            //收件人地址
            MailAddress to = new MailAddress(toEmail);
            //邮件主题、内容
            MailMessage message = new MailMessage(from, to);
            //主题
            message.Subject = subject;
            message.SubjectEncoding = System.Text.Encoding.UTF8;
            //内容
            message.Body = body;
            message.BodyEncoding = System.Text.Encoding.UTF8;
            //在有附件的情况下添加附件
            try
            {
                if (attachmentsPath != null && attachmentsPath.Length > 0)
                {
                    Attachment attachFile = null;
                    foreach (string path in attachmentsPath)
                    {
                        attachFile = new Attachment(path);
                        message.Attachments.Add(attachFile);
                    }
                }
            }
            catch (Exception err)
            {
                throw new Exception("在添加附件时有错误:" + err);
            }
            try
            {
                //大部分邮件服务器均加smtp.前缀
                //SmtpClient client = new SmtpClient("smtp." + from.Host);
                SmtpClient client = new SmtpClient(smtpStr);
                SendMail(client, from, fromPsaaWord, to, message);
            }
            catch (SmtpException err)
            {
                //如果错误原因是没有找到服务器，则尝试不加smtp.前缀的服务器
                if (err.StatusCode == SmtpStatusCode.GeneralFailure)
                {
                    try
                    {
                        //有些邮件服务器不加smtp.前缀
                        SmtpClient client = new SmtpClient(from.Host);
                        SendMail(client, from, fromPsaaWord, to, message);
                    }
                    catch (SmtpException)
                    { }
                }
                else
                { }
            }
        }
        //根据指定的参数发送邮件
        public void SendMail(SmtpClient client, MailAddress from, string password, MailAddress to, MailMessage message)
        {
            //不使用默认凭证,注意此句必须放在client.Credentials的上面
            client.UseDefaultCredentials = false;
            //指定用户名、密码
            client.Credentials = new NetworkCredential(from.Address, password);
            //邮件通过网络发送到服务器
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            try
            {
                client.Send(message);
            }
            catch (Exception err)
            {
                throw new Exception("系统繁忙,发送失败!");
            }
            finally
            {
                //及时释放占用的资源
                message.Dispose();
            }
        }
    }
}


