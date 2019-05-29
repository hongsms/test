using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Xml;
using System.IO;
using System.Collections;
using System.Text.RegularExpressions;
using System.Web;
using System.Runtime.Serialization.Json;
using System.Security.Cryptography;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Newtonsoft.Json;

using System.Data;
using WebSite.Common;




namespace WebCommon
{

    public class General : System.Web.UI.Page
    {
        public string jsTag = "<script type='text/javascript'>{0}</script>";
        protected string mod = StringUtil.ChangeFirstLetterToUpper(HttpContext.Current.Request.QueryString["mod"]);

        protected string defaultURL = HttpContext.Current.Request.Url.AbsolutePath;
        public string escape(string data)
        {
            if (isNotNull(data))
            {
                return Microsoft.JScript.GlobalObject.escape(data);
            }
            return data;
        }
        #region 通用验证/检查
        /// <summary>判断对象是否为空
        /// 判断对象是否为空
        /// </summary>
        /// <param name="obj">对象</param>
        /// <returns>如果对象为空则返回true</returns>
        public bool isNotNull(object obj)
        {
            if (!(obj == null || obj.ToString().Trim().Equals("") || obj.Equals("undefined")))
                return true;
            return false;
        }
        /// <summary>
        /// 检查一个字符串是否是纯数字构成的，一般用于查询字符串参数的有效性验证。
        /// </summary>
        /// <param name="_value">需验证的字符串。。</param>
        /// <returns>是否合法的bool值。</returns>
        public bool IsNumber_1(string _value)
        {

            Regex myRegex = new Regex("^[1-9]*[0-9]*$");
            if (_value.Length == 0)
            {
                return false;
            }
            return myRegex.IsMatch(_value);
        }
        /// <summary>
        /// 判断是否是数字，包括小数和整数。
        /// </summary>
        /// <param name="_value">需验证的字符串。</param>
        /// <returns>是否合法的bool值。</returns>
        public bool IsNumber_2(string _value)
        {
            Regex myRegex = new Regex("^(0|([1-9]+[0-9]*))(.[0-9]+)?$");
            if (_value.Length == 0)
            {
                return false;
            }
            return myRegex.IsMatch(_value);
        }
        /// <summary>
        /// 检查一个字符串是否是纯字母或数字构成的，一般用于查询字符串参数的有效性验证。
        /// </summary>
        /// <param name="_value">需验证的字符串。</param>
        /// <returns>是否合法的bool值。</returns>
        public bool IsLetterOrNumber(string _value)
        {
            Regex myRegex = new Regex("^[a-zA-Z0-9_]*$");
            if (_value.Length == 0)
            {
                return false;
            }
            return myRegex.IsMatch(_value);
        }

        /// <summary>
        /// 检查一个字符串是否可以转化为日期，一般用于验证用户输入日期的合法性。
        /// </summary>
        /// <param name="_value">需验证的字符串。</param>
        /// <returns>是否可以转化为日期的bool值。</returns>
        public bool IsStringDate(string _value)
        {
            DateTime dt;
            try
            {
                dt = DateTime.Parse(_value);
            }
            catch (FormatException e)
            {
                //日期格式不正确时
                Console.WriteLine(e.Message);
                return false;
            }
            return true;
        }
        /// <summary>
        /// 判断一个字符串是否是时间格式
        /// </summary>
        /// <param name="strValue"></param>
        /// <returns></returns>
        public bool IsDatetime(string strValue)
        {
            string strReg = @"([1-2][0-9][0-9][0-9])-(0*[1-9]|1[0-2])-(0*[1-9]|[12][0-9]|3[01])\ (0*[0-9]|1[0-9]|2[0-3]):(0*[0-9]|[1-5][0-9]):(0[0-9]|[1-5][0-9])";
            if (strValue == "")
            {
                return false;
            }
            else
            {
                Regex re = new Regex(strReg);
                MatchCollection mc = re.Matches(strValue);
                if (mc.Count == 1)
                    foreach (Match m in mc)
                    {
                        if (m.Value == strValue)
                            return true;
                    }
            }
            return false;
        }
        /// <summary>
        /// 判断一个字符串是否是日期格式
        /// </summary>
        /// <param name="strValue"></param>
        /// <returns></returns>
        public bool IsDate(string strValue)
        {
            try
            {
                Convert.ToDateTime(strValue);
            }
            catch
            {
                return false;
            }
            return true;
        }
        /// <summary>
        /// 判断是否是电子邮件
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public bool IsEmail(string _value)
        {
            string strExp = @"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$";
            Regex r = new Regex(strExp);
            Match m = r.Match(_value);
            if (m.Success)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 是否手机号码
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public bool IsMobile(string _value)
        {
            string strExp = @"^13[0-9]{9}$|14[0-9]{9}|15[0-9]{9}$|18[0-9]{9}$";
            Regex r = new Regex(strExp);
            Match m = r.Match(_value);
            if (m.Success)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// 对时间进行格式化，如：2007-1-15,2007/5/2
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="geshi">如：y-m-d；y/m/d；y-m-d h:mm:ss；m-d-y；m/d/y</param>
        /// <param name="spstr">分隔符号，如：-，/</param>
        /// <returns></returns>
        public string DateString(DateTime dt, string geshi, string spstr)
        {
            string str = "";
            string y, m, d, h, mm, ss;
            y = dt.Year.ToString();
            m = dt.Month.ToString();
            if (m.Length < 2) m = "0" + m;
            d = dt.Day.ToString();
            if (d.Length < 2) d = "0" + d;
            h = dt.Hour.ToString();
            if (h.Length < 2) h = "0" + h;
            mm = dt.Minute.ToString();
            if (mm.Length < 2) mm = "0" + mm;
            ss = dt.Second.ToString();
            if (ss.Length < 2) ss = "0" + ss;

            if (geshi == "y-m-d")
            {
                str = y + spstr + m + spstr + d;
            }
            else if (geshi == "y-m-d h:mm:ss")
            {
                str = y + spstr + m + spstr + d + " " + h + ":" + mm + ":" + ss;
            }
            else if (geshi == "m-d-y")
            {
                str = m + spstr + d + spstr + y;
            }
            else if (geshi == "d-m-y")
            {
                str = d + spstr + m + spstr + y;
            }
            else if (geshi == "yy-mm-dd")
            {
                str = y.Substring(2) + spstr + m + spstr + d + " " + h + ":" + mm + ":" + ss;
            }
            else
            {
                str = DateTime.Now.ToString();
            }
            return str;
        }
        /// <summary>
        /// string型转换为float型
        /// </summary>
        /// <param name="strValue">要转换的字符串</param>
        /// <param name="defValue">缺省值</param>
        /// <returns>转换后的int类型结果</returns>
        public float StrToFloat(string _value, float defValue)
        {
            if ((_value == null) || (_value.Length > 10))
            {
                return defValue;
            }

            float intValue = defValue;
            if (_value != null)
            {
                bool IsFloat = Regex.IsMatch(_value, @"^([-]|[0-9])[0-9]*(\.\w*)?$");
                if (IsFloat)
                {
                    float.TryParse(_value, out intValue);
                }
            }
            return intValue;
        }

        /// <summary>
        /// 将字符串转换为Int32类型
        /// </summary>
        /// <param name="expression">要转换的字符串</param>
        /// <param name="defValue">缺省值</param>
        /// <returns>转换后的int类型结果</returns>
        public int StrToInt(string _value, int defValue)
        {
            if (string.IsNullOrEmpty(_value) || _value.Trim().Length >= 11 || !Regex.IsMatch(_value.Trim(), @"^([-]|[0-9])[0-9]*(\.\w*)?$"))
                return defValue;
            int rv;
            if (Int32.TryParse(_value, out rv))
                return rv;
            return Convert.ToInt32(StrToFloat(_value, defValue));
        }

        /// <summary>
        /// 是否为Double类型
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        public bool IsDouble(object _value)
        {
            if (_value != null)
            {
                return Regex.IsMatch(_value.ToString(), @"^([0-9])[0-9]*(\.\w*)?$");
            }
            return false;
        }

        /// <summary>
        /// string型转换为bool型
        /// </summary>
        /// <param name="strValue">要转换的字符串</param>
        /// <param name="defValue">缺省值</param>
        /// <returns>转换后的bool类型结果</returns>
        public bool StrToBool(object _value, bool defValue)
        {
            if (_value != null)
            {
                return StrToBool(_value, defValue);
            }
            return defValue;
        }
        /// <summary>
        /// string型转换为bool型
        /// </summary>
        /// <param name="strValue">要转换的字符串</param>
        /// <param name="defValue">缺省值</param>
        /// <returns>转换后的bool类型结果</returns>
        public bool StrToBool(string _value, bool defValue)
        {
            if (_value != null)
            {
                if (string.Compare(_value, "true", true) == 0)
                {
                    return true;
                }
                else if (string.Compare(_value, "false", true) == 0)
                {
                    return false;
                }
            }
            return defValue;
        }
        /// <summary>
        /// 判断给定的字符串数组(strNumber)中的数据是不是都为数值型
        /// </summary>
        /// <param name="strNumber">要确认的字符串数组</param>
        /// <returns>是则返加true 不是则返回 false</returns>
        public bool IsNumericArray(string[] strNumber)
        {
            if (strNumber == null)
            {
                return false;
            }
            if (strNumber.Length < 1)
            {
                return false;
            }
            foreach (string id in strNumber)
            {
                if (!IsNumber_1(id))
                {
                    return false;
                }
            }
            return true;

        }
        #endregion

        #region 应用通用

        /// <summary>
        /// 显示Tips信息，默认
        /// </summary>
        /// <param name="info">信息内容</param>
        /// <param name="type">类型:0)失败;1)成功;2)警告)</param>
        /// <param name="isClosecloseLoading">是否关闭正在加载Tips</param>
        /// <returns>返回Tips脚本</returns>
        public string ShowMsg(object info, int type, bool isClosecloseLoading)
        {
            return jsTag.Replace("{0}", string.Format("{0} window.top.msgBox.tips('{1}',{2},2000);", (!isClosecloseLoading) ? "" : "window.top.msgBox.closeLoading();", info, type));
        }

        /// <summary>
        /// 显示Tips信息，自定义关闭时间
        /// </summary>
        /// <param name="info">信息内容</param>
        /// <param name="type">类型:0)失败;1)成功;2)警告)</param>
        /// <param name="isClosecloseLoading">是否关闭正在加载Tips</param>
        /// <returns>返回Tips脚本</returns>
        public string ShowMsg(string msg, string type)
        {
            return jsTag.Replace("{0}", "swal(\"\", \"" + msg + "\", \"" + type + "\");");
        }

        /// <summary>
        /// 显示Tips信息，自定义URL
        /// </summary>
        /// <param name="info">信息内容</param>
        /// <param name="type">类型:0)失败;1)成功;2)警告)</param>
        /// <param name="url">类型:0)失败;1)成功;2)警告)</param>
        /// <param name="isClosecloseLoading">是否关闭正在加载Tips</param>
        /// <returns>返回Tips脚本</returns>
        public string ShowMsg(string msg, string type, string url)
        {
            return jsTag.Replace("{0}", string.Format("swal({{ title: \"\", text: \"{0}\", type: \"{1}\" }}, function () {{ window.location.href = \"{2}\" ; }});", msg, type, url));

            }

        /// <summary>
        /// 显示Tips信息，自定义URL
        /// </summary>
        /// <param name="info">信息内容</param>
        /// <param name="type">类型:0)失败;1)成功;2)警告)</param>
        /// <param name="url">类型:0)失败;1)成功;2)警告)</param>
        /// <param name="isClosecloseLoading">是否关闭正在加载Tips</param>
        /// <returns>返回Tips脚本</returns>
        public string ShowMsgBack(object info, int type, bool isClosecloseLoading)
        {
            return jsTag.Replace("{0}", string.Format("{0} window.top.msgBox.tips('{1}',{2},2000,function(){{history.back();}});", (!isClosecloseLoading) ? "" : "window.top.msgBox.closeLoading();", info, type));
        }


        /// <summary>
        /// 显示Tips信息，自定义URL
        /// </summary>
        /// <param name="info">信息内容</param>
        /// <param name="type">类型:0)失败;1)成功;2)警告)</param>
        /// <param name="url">显示时间毫秒数</param>
        /// <param name="url">类型:0)失败;1)成功;2)警告)</param>
        /// <param name="isClosecloseLoading">是否关闭正在加载Tips</param>
        /// <returns>返回Tips脚本</returns>
        public string ShowMsg(object info, int type, int time, string url, bool isClosecloseLoading)
        {
            return jsTag.Replace("{0}", string.Format("{0} window.top.msgBox.tips('{1}',{2},{3},function(){{document.location.href='{4}';}});", (!isClosecloseLoading) ? "" : "window.top.msgBox.closeLoading();", info, type, time, url));
        }

        /// <summary>
        /// 显示提示信息，默认(Tips)自定义回调函数
        /// </summary>
        /// <param name="info">信息内容</param>
        /// <param name="type">类型:0)失败;1)成功;2)警告)</param>
        /// <param name="time">回调函数</param>
        /// <param name="callback">回调函数</param>
        /// <param name="isClosecloseLoading">是否关闭正在加载Tips</param>
        /// <param name="currentPage">当前页</param>
        /// <returns>返回Tips脚本</returns>
        public string ShowMsgAndCallBack(object info, int type, string callback, bool isClosecloseLoading)
        {
            return jsTag.Replace("{0}", string.Format("{0} window.top.msgBox.tips('{1}',{2},2000,{3});", (!isClosecloseLoading) ? "" : "window.top.msgBox.closeLoading();", info, type, callback));
        }

        /// <summary>
        /// 显示提示信息，默认(Tips)自定义回调函数、自定义时间
        /// </summary>
        /// <param name="info">信息内容</param>
        /// <param name="type">类型:0)失败;1)成功;2)警告)</param>
        /// <param name="time">回调函数</param>
        /// <param name="callback">回调函数</param>
        /// <param name="isClosecloseLoading">是否关闭正在加载Tips</param>
        /// <param name="currentPage">当前页</param>
        /// <returns>返回Tips脚本</returns>
        public string ShowMsgAndCallBack(object info, int type, int time, string callback, bool isClosecloseLoading)
        {
            return jsTag.Replace("{0}", string.Format("{0} window.top.msgBox.tips('{1}',{2},{3},{4});", (!isClosecloseLoading) ? "" : "window.top.msgBox.closeLoading();", info, type, time, callback));
        }

        /// <summary>
        /// 跳转至操作提示页
        /// </summary>
        /// <param name="info">信息内容</param>
        /// <param name="type">类型:0)失败;1)成功;2)警告)</param>
        /// <param name="lUrl">列表页地址</param>
        public void ShowMsg(object info, int type, string lUrl)
        {
            Response.Redirect(string.Format("/Manage/ShowMsg.aspx?Info={0}&LUrl={1}&Type={2}", escape(StringUtil.ConvertToString(info)), escape(lUrl), type));
        }

        /// <summary>
        /// 跳转至操作提示页
        /// </summary>
        /// <param name="info">信息内容</param>
        /// <param name="type">类型:0)失败;1)成功;2)警告)</param>
        /// <param name="lUrl">列表页地址警告)</param>
        /// <param name="eUrl">编辑页地址警告)</param>
        public void ShowMsg(object info, int type, string lUrl, string eUrl)
        {
            Response.Redirect(string.Format("/Manage/ShowMsg.aspx?Info={0}&LUrl={1}&EUrl={2}&Type={3}", escape(StringUtil.ConvertToString(info)), escape(lUrl), escape(eUrl), type));
        }

        /// <summary>
        /// 跳转至操作提示页
        /// </summary>
        /// <param name="info">信息内容</param>
        /// <param name="type">类型:0)失败;1)成功;2)警告)</param>
        /// <param name="lUrl">列表页地址警告)</param>
        /// <param name="eUrl">编辑页地址警告)</param>
        public void ShowMsg(object location, string info, int type, string lUrl, string eUrl)
        {
            Response.Redirect(string.Format("{0}showmsg.aspx?Info={1}&LUrl={2}&EUrl={3}&Type={4}", location, escape(info), escape(lUrl), escape(eUrl), type));
        }

        /// <summary>
        /// 页面跳转
        /// </summary>
        /// <param name="url">地址</param>
        public void PageSkip(string url)
        {
            Response.Redirect(url);
        }






        #endregion


        #region 防止表单重复提交，请求处理

        /// <summary>
        /// 页面初始化
        /// </summary>
        public void PageInit()
        {
            if (!StringUtil.IsNullOrEmpty(mod))
            {
                this.GetType().InvokeMember(mod, System.Reflection.BindingFlags.Default | System.Reflection.BindingFlags.InvokeMethod, null, this, null);
            }
        }
        /// <summary>
        /// 判断令牌是否匹配
        /// </summary>
        /// <returns></returns>
        public bool CheckToken()
        {
            try
            {
                return Request.Form["Token"].Equals(GetToken());
            }
            catch
            {
                return false;
            }
        }
        /// <summary>
        /// 获取令牌
        /// </summary>
        /// <returns></returns>
        public string GetToken()
        {
            object sessionToken = Session["Token"];
            return sessionToken == null ? null : sessionToken.ToString();
        }
     
        #endregion

        #region ObjectToJson

        public void ObjectToJson(object obj)
        {
            if (obj != null)
            {
                DataContractJsonSerializer json = new DataContractJsonSerializer(obj.GetType());
                using (MemoryStream stream = new MemoryStream())
                {
                    json.WriteObject(stream, obj);
                    stream.Close();

                    HttpContext.Current.Response.Write(System.Text.Encoding.UTF8.GetString(stream.ToArray()));
                    HttpContext.Current.Response.End();
                }
            }
            HttpContext.Current.Response.Write(null);
        }

        #endregion

        public static string GetDefaultpic(string strpic)
        {
            return strpic.Length > 0 ? strpic : "/testimg/noimage.png";
        }
        public static string GetPrices(object price1, object price2)
        {
            decimal min_price = Convert.ToDecimal(price1);
            decimal max_price = Convert.ToDecimal(price2);
            if (min_price >= max_price)
            {
                return min_price.ToString("0.0");
            }
            if ((min_price + max_price) == 0)
            {
                return min_price.ToString("0.0");
            }
            return min_price.ToString("0.0") + "-" + max_price.ToString("0.0");
        }





        #region 文件操作


        /// <summary>
        /// 输出日志，用于调试
        /// </summary>
        /// <param name="readme"></param>
        public void WriteLog(string readme)
        {
            //StreamWriter dout = new StreamWriter(@"c:/" + System.DateTime.Now.ToString("yyyMMddHHmmss") + ".txt");  
            StreamWriter dout = new StreamWriter(@"c:/" + "WebLog.txt", true);
            dout.Write("\r\n事件：" + readme + "\r\n操作时间：" + System.DateTime.Now.ToString("yyy-MM-dd HH:mm:ss"));
            dout.Close();
        }

        /// <summary>
        /// 图片上传
        /// </summary>
        /// <param name="name">file元素名称</param>
        /// <param name="folder">保存目录</param>
        /// <returns>文件相关信息</returns>
        public Hashtable UploadImage(string name, string folder, int maxLength)
        {
            //获得上传数据
            var file = HttpContext.Current.Request.Files[name];


            bool fileOK = false;
            if (file != null && file.ContentLength > 0)
            {
                Hashtable htFileInfo = new Hashtable();
                if (maxLength > 0)
                {
                    if (file.ContentLength > maxLength)
                    {
                        htFileInfo.Add("State", -1);
                        return htFileInfo;
                    }
                }
                //获得上传文件的后缀
                String fileExt = Path.GetExtension(file.FileName).ToLower();
                String[] allowedExtensions = { ".gif", ".png", ".jpeg", ".jpg", ".bmp" };

                for (int i = 0; i < allowedExtensions.Length; i++)
                {
                    if (fileExt == allowedExtensions[i])
                    {
                        fileOK = true;
                    }
                }
                if (fileOK)
                {
                    return FileUpload(file, folder);
                }
                htFileInfo.Add("State", 0);
                return htFileInfo;
            }
            else
            {
                return null;
            }



        }
        /// <summary>
        /// 附件上传
        /// </summary>
        /// <param name="name">file元素名称</param>
        /// <param name="folder">保存目录</param>
        /// <returns>文件相关信息</returns>
        public Hashtable FileUpload(string name, string folder, string[] allowedExtensions, int maxLength)
        {
            //获得上传数据
            var file = HttpContext.Current.Request.Files[name];

            if (file != null && file.ContentLength > 0)
            {
                Hashtable htFileInfo = new Hashtable();
                if (maxLength > 0)
                {
                    if (file.ContentLength > maxLength)
                    {
                        htFileInfo.Add("State", -1);
                        return htFileInfo;
                    }
                }
                //获得上传文件的后缀
                String fileExt = Path.GetExtension(file.FileName).ToLower();
                if (allowedExtensions != null && allowedExtensions.Length > 0)
                {
                    bool fileOK = false;
                    for (int i = 0; i < allowedExtensions.Length; i++)
                    {
                        if (fileExt == allowedExtensions[i])
                        {
                            fileOK = true;
                        }
                    }
                    if (!fileOK)
                    {
                        htFileInfo.Add("State", 0);
                        return htFileInfo;
                    }
                }
                return FileUpload(file, folder);
            }
            return null;
        }

        public Hashtable FileUpload(HttpPostedFile file, string folder)
        {

            if (file != null)
            {
                Hashtable htFileInfo = new Hashtable();
                //初始化文件夹相对路径
                StringBuilder client_dirpath = new StringBuilder(string.Format(folder + "{0:d}", DateTime.Now));
                //获得文件夹物理路径
                string server_dirpath = HttpContext.Current.Server.MapPath(client_dirpath.ToString());
                //判断文件夹是否存在，如果不存在则创建文件夹。
                if (!Directory.Exists(server_dirpath))
                {
                    Directory.CreateDirectory(server_dirpath);
                }
                //获得上传文件的后缀
                String fileExt = Path.GetExtension(file.FileName).ToLower();
                //初始化文件名称，并追加到文件夹路径后面
                client_dirpath.Append(string.Format("/{0}", Web.Rand.RndDateStr() + fileExt));
                //获得文件物理路径
                string completePath = HttpContext.Current.Server.MapPath(client_dirpath.ToString());
                //保存文件
                file.SaveAs(completePath);
                //上传完毕，向客户端返回文件路径

                //保存上传文件的基本信息
                htFileInfo.Add("State", 1);
                htFileInfo.Add("FilePath", client_dirpath);
                htFileInfo.Add("FileName", file.FileName);
                htFileInfo.Add("FileExt", fileExt);
                htFileInfo.Add("ContentLength", file.ContentLength);
                return htFileInfo;
            }
            else
            {
                return null;
            }
        }
        public bool DeleteFile(string filePath)
        {
            try
            {
                System.IO.File.Delete(Server.MapPath(filePath));
                return true;
            }
            catch
            {
                return false;
            }
        }

        public IList<HttpPostedFile> GetHttpPostedFiles(string controlName)
        {
            var httpPostedFileList = new List<HttpPostedFile>();
            var hfc = Request.Files;
            string[] AllKeys = Request.Files.AllKeys;
            for (int i = 0; i < AllKeys.Length; i++)
            {
                if (AllKeys[i] != null && AllKeys[i].ToString() == controlName)
                {
                    httpPostedFileList.Add(hfc[i]);
                }
            }
            return httpPostedFileList;
        }
        /// <summary>
        /// 检查文件类型
        /// </summary>
        /// <param name="hfc"></param>
        /// <param name="fileTypes"></param>
        /// <returns></returns>
        public bool CheckFileType(HttpPostedFile hfc, string fileTypes)
        {
            try
            {
                switch (fileTypes)
                {
                    case "image":
                        fileTypes = "gif,jpg,jpeg,png,bmp";
                        break;
                }
                var fileExt = Path.GetExtension(hfc.FileName).ToLower();
                if (!StringUtil.IsNullOrEmpty(fileExt))
                {
                    if (Array.IndexOf(fileTypes.Split(','), fileExt.Substring(1).ToLower()) == -1)
                    {
                        return false;
                    }
                }
            }
            catch
            {

            }
            return true;
        }

        #endregion


        public string UploadFile(string fuName, string oldPic,out bool flag)
        {
            flag = true;
            var fuPic = HttpContext.Current.Request.Files[fuName];
            if (fuPic != null && fuPic.ContentLength > 0)
            {
                string fileTypes = "gif,jpg,jpeg,bmp,doc,xls,txt,mp3,wma,rar,zip,swf,flv,png";
                string fileExt = Path.GetExtension(fuPic.FileName).ToLower();

                if (!String.IsNullOrEmpty(fileExt))
                {
                    if (Array.IndexOf(fileTypes.Split(','), fileExt.Substring(1).ToLower()) == -1)
                    {
                        flag = false;
                    }
                    else
                    {
                        StringBuilder client_dirpath = new StringBuilder(string.Format("/uploads/{0:d}", DateTime.Now.ToString("yyMMddHHmmssf")));
                        string server_dirpath = Server.MapPath(client_dirpath.ToString());
                        if (!Directory.Exists(server_dirpath))
                        {
                            Directory.CreateDirectory(server_dirpath);
                        }
                        client_dirpath.Append(string.Format("/{0}", Web.Rand.RndDateStr(1)[0] + fileExt));
                        string completePath = HttpContext.Current.Server.MapPath(client_dirpath.ToString());
                        fuPic.SaveAs(completePath);
                        return client_dirpath.ToString();
                    }

                }
            }
            return oldPic;
        }

        public int GetDataLength(string paramName, int maxCount)
        {
            try
            {
                int idLength = WebSite.Common.DNTRequest.GetFormString(paramName).Split(',').Length;
                return idLength < maxCount ? idLength : maxCount;
            }
            catch
            {

            }
            return 0;
        }

    
    }





}
