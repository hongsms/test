/*----------------------------------------------------------------
// Copyright (C) 2010 
// 版权所有
//
// 文件名：StringUtil.cs
// 文件功能描述：
//
//
// 创建标识：CXB 2010-01-28 15:13:17 星期四
//
// 修改标识：
// 修改描述：
//----------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.Text;

using System.Web;
using System.Text.RegularExpressions;

public class StringUtil
{
    #region 验证
    public static bool IsNullOrEmpty(string where)
    {
        return (where == null || where.Trim() == "");
    }
    public static bool IsMatch(string str, string reg)
    {
        return Regex.IsMatch(str, reg, RegexOptions.IgnoreCase);
    }
    #endregion

    #region 属性

    /// <summary>
    /// 整数默认值
    /// </summary>
    public static int DefaultValue
    {
        get
        {
            return -1;
        }
    }

    #endregion

    /// <summary>
    /// 需要转换的字符把每个字符串的首字母转换大写
    /// </summary>
    /// <param name="strChange">需要转换的字符</param>
    /// <returns>返回已经转换的字符</returns>
    public static string ChangeFirstLetterToUpper(string strChange)
    {
        try
        {
            string tempFirst = strChange.Substring(0, 1);
            string tempElse = strChange.Substring(1, strChange.Length - 1);
            return (tempFirst.ToUpper() + tempElse);
        }
        catch
        {
            return strChange;
        }
    }

    #region 构造函数

    /// <summary>
    /// 默认构造函数
    /// </summary>
    public StringUtil()
    {
        //
        //TODO: 在此处添加构造函数逻辑
        //
    }

    #endregion

    #region 时间日期

    /// <summary>
    /// 当前时间字符串
    /// </summary>
    public static string CurrentDateTimeString
    {
        get
        {
            return System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"); // 时间24小时制
        }
    }

    /// <summary>
    /// 当前日期字符串
    /// </summary>
    public static string CurrentDateString
    {
        get
        {
            return System.DateTime.Now.ToString("yyyy-MM-dd");
        }
    }

    /// <summary>
    /// 时间字符串 — 将 DateTime 类型时间转化成字符串
    /// </summary>
    /// <param name="dt">时间</param>
    /// <returns></returns>
    public static string DateTimeString(DateTime dt)
    {
        return dt.ToString("yyyy-MM-dd HH:mm:ss");
    }

    /// <summary>
    /// 时间字符串 — 将 DateTime 类型时间转化成字符串
    /// </summary>
    /// <param name="dt">时间</param>
    /// <returns></returns>
    public static string DateTimeString(DateTime? dt)
    {
        if (dt != null)
        {
            return Convert.ToDateTime(dt).ToString("yyyy-MM-dd HH:mm:ss");
        }

        return CurrentDateTimeString;
    }

    /// <summary>
    /// 时间字符串 — 将 object 类型时间转化成字符串
    /// </summary>
    /// <param name="value">时间</param>
    /// <returns></returns>
    public static string DateTimeString(object value)
    {
        try
        {
            return Convert.ToDateTime(value).ToString("yyyy-MM-dd HH:mm:ss");
        }
        catch
        {
            return CurrentDateTimeString;
        }
    }

    /// <summary>
    /// 日期字符串 — 将 DateTime 类型时间转化成字符串
    /// </summary>
    /// <param name="dt">时间</param>
    /// <returns></returns>
    public static string DateString(DateTime dt)
    {
        return dt.ToString("yyyy-MM-dd");
    }

    /// <summary>
    /// 日期字符串 — 将 DateTime 类型时间转化成字符串
    /// </summary>
    /// <param name="dt">时间</param>
    /// <returns></returns>
    public static string DateString(DateTime? dt)
    {
        if (dt != null)
        {
            return Convert.ToDateTime(dt).ToString("yyyy-MM-dd");
        }

        return CurrentDateString;
    }

    /// <summary>
    /// 日期字符串 — 将 object 类型时间转化成字符串
    /// </summary>
    /// <param name="value">时间</param>
    /// <returns></returns>
    public static string DateString(object value)
    {
        try
        {
            return Convert.ToDateTime(value).ToString("yyyy-MM-dd");
        }
        catch
        {
            return CurrentDateString;
        }
    }

    #endregion

    #region 字符串操作

    /// <summary>
    /// 获取字符串长度 — 双字节字符 2 个单位长度
    /// </summary>
    /// <param name="value">字符串</param>
    /// <returns>长度</returns>
    public static int Length(string value)
    {
        if (String.IsNullOrEmpty(value))
        {
            return 0;
        }

        //int length = 0;
        //foreach (char chr in value)
        //{
        //    if (((int)chr) < 0 || ((int)chr) > 126)
        //    {
        //        length = length + 2;
        //    }
        //    else
        //    {
        //        length = length + 1;
        //    }
        //}

        //return length;

        return Encoding.Default.GetBytes(value).Length;
    }
    /// <summary>
    /// 获取由指定数量符号组成的字符串
    /// </summary>
    /// <param name="mark"></param>
    /// <param name="num"></param>
    /// <returns></returns>
    public static string FillStr(string mark, int num)
    {
        StringBuilder str = new StringBuilder();
        for (int i = 0; i < num; i++)
        {
            str.Append(mark);
        }
        return str.ToString();
    }

    /// <summary>
    /// 截取字符串
    /// </summary>
    /// <param name="value">字符串</param>
    /// <param name="length">最多显示的字符长度(双字节字符 2 个单位长度),超出部分使用...代替</param>
    /// <returns></returns>
    public static string SubString(string value, int length)
    {
        int realLength = 0;
        for (int i = 0; i < value.Length; i++)
        {
            //if (value[i] >= 0x4e00 && value[i] <= 0x9fa5)
            //{
            //    realLength += 2;
            //}
            //else
            //{
            //    realLength++;
            //}

            realLength += Encoding.Default.GetBytes(value[i].ToString()).Length;

            if (realLength == length)
            {
                return value.Substring(0, i + 1) + "...";
            }
            else if (realLength > length)
            {
                return value.Substring(0, i) + "...";
            }
        }

        return value;
    }

    /// <summary>
    /// 截取字符串
    /// </summary>
    /// <param name="value">字符串</param>
    /// <param name="length">最多显示的字符长度(双字节字符 2 个单位长度),超出部分不显示</param>
    /// <returns></returns>
    public static string SubStringIgnore(string value, int length)
    {
        int realLength = 0;
        //字符串小于指定长度 不操作
        if (Encoding.Default.GetBytes(value).Length < length)
        {
            return value;
        }
        for (int i = 0; i < value.Length; i++)
        {
            //if (value[i] >= 0x4e00 && value[i] <= 0x9fa5)
            //{
            //    realLength += 2;
            //}
            //else
            //{
            //    realLength++;
            //}

            realLength += Encoding.Default.GetBytes(value[i].ToString()).Length;

            if (realLength == length)
            {
                return value.Substring(0, i + 1);
            }
            else if (realLength > length)
            {
                return value.Substring(0, i);
            }
        }

        return value;
    }

    /// <summary>
    /// 分割字符串
    /// </summary>
    /// <param name="strContent">源</param>
    /// <param name="strSplit">分隔符</param>
    /// <returns></returns>
    public static string[] SplitString(string strContent, string strSplit)
    {
        if (!string.IsNullOrEmpty(strContent))
        {
            if (strContent.IndexOf(strSplit) < 0)
                return new string[] { strContent };

            return Regex.Split(strContent, Regex.Escape(strSplit), RegexOptions.IgnoreCase);
        }
        else
            return new string[] { "" };
    }
    /// <summary>
    /// 分割字符串,去除空项
    /// </summary>
    /// <param name="strContent">源</param>
    /// <param name="strSplit">分隔符</param>
    /// <returns></returns>
    public static string[] SplitStringTrim(string strContent, string strSplit)
    {
        string[] arry = null;
        string[] sp = { strSplit };
        arry = strContent.Split(sp, StringSplitOptions.RemoveEmptyEntries);
        return arry;
    }
    /// <summary>
    /// 分割字符串 指定项数量
    /// </summary>
    /// <returns></returns>
    public static string[] SplitString(string strContent, string strSplit, int count)
    {
        string[] result = new string[count];
        string[] splited = SplitString(strContent, strSplit);

        for (int i = 0; i < count; i++)
        {
            if (i < splited.Length)
                result[i] = splited[i];
            else
                result[i] = string.Empty;
        }

        return result;
    }


    /// <summary>
    /// 检测网址
    /// </summary>
    /// <param name="url">网址</param>
    /// <returns>网址格式字符串</returns>
    public static string FormatUrl(string url)
    {
        //string[] UserHost = url.Split(new Char[] { ':' });  //数组，以“.”分隔
        //string str = UserHost[0].ToString();

        string str = url.Split(new char[] { ':' })[0].ToString();

        if (str.ToLower().Equals("http"))
        {
            return url;
        }
        else
        {
            return "http://" + url;
        }
    }

    /// <summary>
    /// 根据真实 ipAddress 把最后ip地址变成*
    /// </summary>
    /// <param name="ipAddress">ip地址</param>
    /// <returns></returns>
    public static string FormatIPAddress(string ipAddress)
    {
        if (String.IsNullOrEmpty(ipAddress) || ipAddress.LastIndexOf(".") == -1)
        {
            return "127.0.0.*";
        }

        return ipAddress.Substring(0, ipAddress.LastIndexOf(".")) + ".*";
    }

    /// <summary>
    /// 空字符串替换html格式
    /// </summary>
    /// <param name="value">字符串</param>
    /// <returns></returns>
    public static string FormatEmpty(string value)
    {
        if (String.IsNullOrEmpty(value))
        {
            return "&nbsp;";
        }

        return value;
    }

    #endregion

    #region Html 标记

    /// <summary>
    /// 过滤Html标记
    /// </summary>
    /// <param name="value">内容</param>
    /// <returns></returns>
    public static string RemoveHtml(string value)
    {
        //删除脚本
        value = Regex.Replace(value, @"<[^>]*?>.*?</>", "", RegexOptions.IgnoreCase);

        //删除表格HTML
        value = Regex.Replace(value, @"<?xml[^>]*>", "", RegexOptions.IgnoreCase);
        value = value.Replace("<?", "");
        value = Regex.Replace(value, @"</?table[^>]*>", "", RegexOptions.IgnoreCase);
        value = Regex.Replace(value, @"</?o:p[^>]*>", "", RegexOptions.IgnoreCase);
        value = Regex.Replace(value, @"</?p[^>]*>", "", RegexOptions.IgnoreCase);
        value = Regex.Replace(value, @"</?font[^>]*>", "", RegexOptions.IgnoreCase);
        value = Regex.Replace(value, @"</?div[^>]*>", "", RegexOptions.IgnoreCase);
        value = Regex.Replace(value, @"</?span[^>]*>", "", RegexOptions.IgnoreCase);
        value = Regex.Replace(value, @"</?b style[^>]*>", "", RegexOptions.IgnoreCase);
        value = Regex.Replace(value, @"</?tr[^>]*>", "", RegexOptions.IgnoreCase);
        value = Regex.Replace(value, @"</?td[^>]*>", "", RegexOptions.IgnoreCase);
        value = Regex.Replace(value, @"</?th[^>]*>", "", RegexOptions.IgnoreCase);
        value = Regex.Replace(value, @"</?blockquote[^>]*>", "", RegexOptions.IgnoreCase);
        value = Regex.Replace(value, @"</?tbody[^>]*>", "", RegexOptions.IgnoreCase);
        value = Regex.Replace(value, @"<style[^\s]*", "", RegexOptions.IgnoreCase);

        value = value.Replace("&nbsp;", "");

        return value;
    }

    /// <summary>
    /// 获取指定长度的内容简介
    /// </summary>
    /// <param name="value">内容</param>
    /// <param name="length">指定获取长度</param>
    /// <returns></returns>
    public static string RemoveHtmlSubString(string value, int length)
    {
        return SubString(RemoveHtml(value), length);
    }

    /// <summary>
    /// 过滤Html标记
    /// </summary>
    /// <param name="value">内容</param>
    /// <returns></returns>
    public static string ClearHtml(string value)
    {
        if (String.IsNullOrEmpty(value))
        {
            return String.Empty;
        }

        value = Regex.Replace(value, "<[^>]*>", "");

        return value.Replace("&nbsp;", "");
    }

    /// <summary>
    /// 获取指定长度的内容简介
    /// </summary>
    /// <param name="value">内容</param>
    /// <param name="length">指定获取长度</param>
    /// <returns></returns>
    public static string ClearHtmlSubString(string value, int length)
    {
        return SubString(ClearHtml(value), length);
    }

    /// <summary>
    /// Converts plain text to HTML
    /// </summary>
    /// <param name="Text">Text</param>
    /// <returns>Formatted text</returns>
    public static string ConvertPlainTextToHtml(string Text)
    {
        if (String.IsNullOrEmpty(Text))
        {
            return String.Empty;
        }

        Text = Text.Replace("\r\n", "<br />");
        Text = Text.Replace("\r", "<br />");
        Text = Text.Replace("\n", "<br />");
        Text = Text.Replace("\t", "&nbsp;&nbsp;");
        Text = Text.Replace("  ", "&nbsp;&nbsp;");

        return Text;
    }

    /// <summary>
    /// Converts HTML to plain text
    /// </summary>
    /// <param name="Text">Text</param>
    /// <returns>Formatted text</returns>
    public static string ConvertHtmlToPlainText(string Text)
    {
        if (String.IsNullOrEmpty(Text))
        {
            return String.Empty;
        }

        Text = Text.Replace("<br>", "\n");
        Text = Text.Replace("<br >", "\n");
        Text = Text.Replace("<br />", "\n");
        Text = Text.Replace("&nbsp;&nbsp;", "\t");
        Text = Text.Replace("&nbsp;&nbsp;", "  ");

        return Text;
    }

    #endregion

    #region 数据类型转化

    /// <summary>
    /// Boolean 类型转化
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    public static bool ConvertToBoolean(object value)
    {
        try
        {
            return Convert.ToBoolean(value);
        }
        catch
        {
            return false;
        }
    }

    /// <summary>
    /// Boolean 类型转化
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    public static bool ConvertToBoolean(int? value)
    {
        try
        {
            return Convert.ToBoolean(value);
        }
        catch
        {
            return false;
        }
    }

    /// <summary>
    /// Byte 类型转化
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    public static byte ConvertToByte(object value)
    {
        try
        {
            return Convert.ToByte(value);
        }
        catch
        {
            return (byte)DefaultValue;
        }
    }

    /// <summary>
    /// Byte 类型转化
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    public static byte ConvertToByte(object value, byte DefaultValue)
    {
        try
        {
            return Convert.ToByte(value);
        }
        catch
        {
            return DefaultValue;
        }
    }

    /// <summary>
    /// Byte 类型转化
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    public static byte ConvertToByte(bool value)
    {
        if (value)
        {
            return 1;
        }

        return 0;
    }

    /// <summary>
    /// Int32 类型转化
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    public static int ConvertToInt32(object value)
    {
        try
        {
            return Convert.ToInt32(value);
        }
        catch
        {
            return DefaultValue;
        }
    }

    /// <summary>
    /// Int32 类型转化
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    public static int ConvertToInt32(bool value)
    {
        if (value)
        {
            return 1;
        }

        return 0;
    }

    /// <summary>
    /// Int32 类型转化
    /// </summary>
    /// <param name="value"></param>
    /// <param name="_defaultValue">转化失败时默认值</param>
    /// <returns></returns>
    public static int ConvertToInt32(string value, int _defaultValue)
    {
        try
        {
            if (string.IsNullOrEmpty(value)) return _defaultValue;
            return Convert.ToInt32(value);
        }
        catch
        {
            return _defaultValue;
        }
    }

    /// <summary>
    /// Decimal 类型转化
    /// </summary>
    /// <param name="value"></param>
    /// <param name="_defaultValue">转化失败时默认值</param>
    /// <returns></returns>
    public static decimal ConvertToDecimal(string value, decimal _defaultValue)
    {
        try
        {
            return Convert.ToDecimal(value);
        }
        catch
        {
            return _defaultValue;
        }
    }

    /// <summary>
    /// String 类型转化
    /// </summary>
    /// <param name="value"></param>
    /// <param name="_defaultValue">转化失败时默认值</param>
    /// <returns></returns>
    public static string ConvertToString(object value)
    {
        try
        {
            return value.ToString();
        }
        catch
        {
            return null;
        }
    }
    /// <summary>
    /// String 类型转化
    /// </summary>
    /// <param name="value"></param>
    /// <param name="_defaultValue">转化失败时默认值</param>
    /// <returns></returns>
    public static string ConvertToString(object value, string _defaultValue)
    {
        try
        {
            return value.ToString();
        }
        catch
        {
            return _defaultValue;
        }
    }
    /// <summary>
    /// DateTime 类型转化
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    public static System.DateTime ConvertToDateTime(string value)
    {
        try
        {
            return Convert.ToDateTime(value);
        }
        catch
        {
            return System.DateTime.Now;
        }
    }

    #endregion

    #region HTTP 查询字符串变量

    /// <summary>
    /// String 类型变量
    /// </summary>
    /// <param name="name">变量名</param>
    /// <returns></returns>
    public static string QueryString(string name)
    {
        string result = String.Empty;

        if (HttpContext.Current != null
            && HttpContext.Current.Request.QueryString[name] != null)
        {
            result = HttpContext.Current.Request.QueryString[name].ToString();
        }

        return result;
    }

    /// <summary>
    /// Boolean 类型变量
    /// </summary>
    /// <param name="name">变量名</param>
    /// <returns></returns>
    public static bool QueryStringToBoolean(string name)
    {
        string result = QueryString(name).ToUpperInvariant();
        return (result == "YES" || result == "TRUE" || result == "1");
    }

    /// <summary>
    /// Int32 类型变量
    /// </summary>
    /// <param name="name">变量名</param>
    /// <returns></returns>
    public static int QueryStringToInt32(string name)
    {
        string result = QueryString(name).ToUpperInvariant();

        if (result.Length > 0)
        {
            try
            {
                return Convert.ToInt32(result);
            }
            catch
            {
                return DefaultValue;
            }
        }

        return DefaultValue;
    }

    /// <summary>
    /// Int32 类型变量
    /// </summary>
    /// <param name="name">变量名</param>
    /// <param name="_defaultValue">转化失败时的默认值</param>
    /// <returns></returns>
    public static int QueryStringToInt32(string name, int _defaultValue)
    {
        string result = QueryString(name).ToUpperInvariant();

        if (result.Length > 0)
        {
            try
            {
                return Convert.ToInt32(result);
            }
            catch
            {
                return _defaultValue;
            }
        }

        return _defaultValue;
    }

    /// <summary>
    /// DateTime 类型变量
    /// </summary>
    /// <param name="name">变量名</param>
    /// <returns></returns>
    public static DateTime QueryStringToDateTime(string name)
    {
        string result = QueryString(name).ToUpperInvariant();

        if (result.Length > 0)
        {
            try
            {
                return Convert.ToDateTime(result);
            }
            catch
            {
                return System.DateTime.Now;
            }
        }

        return System.DateTime.Now;
    }

    /// <summary>
    /// DateTime? 类型变量
    /// </summary>
    /// <param name="name">变量名</param>
    /// <returns></returns>
    public static DateTime? QueryStringToDateTimeNullable(string name)
    {
        string result = QueryString(name).ToUpperInvariant();

        if (result.Length > 0)
        {
            try
            {
                return Convert.ToDateTime(result);
            }
            catch
            {
                return null;
            }
        }
        else
        {
            return null;
        }
    }

    #endregion

    #region 数据库设计字段格式化

    /// <summary>
    /// 性别
    /// </summary>
    /// <param name="sex"></param>
    /// <returns></returns>
    public static string FormatSex(object sex)
    {
        switch (Convert.ToByte(sex))
        {
            case 0:
                return "女";

            case 1:
                return "男";

            case 2:
                return "女";

            default:
                return String.Empty;
        }
    }

    #endregion

    #region UI控制

    /// <summary>
    /// 根据属性值，换回前台控件的颜色 true:红色;false:灰色#c3c3c3;
    /// </summary>
    /// <param name="enable">属性值</param>
    /// <returns></returns>
    public static string GetControlColor(object enable)
    {
        bool value = ConvertToBoolean(enable);

        return value ? "red" : "#c3c3c3";
    }

    #endregion

    #region 随机生成字符串

    /// <summary>
    /// 随机生成字符串
    /// </summary>
    /// <returns></returns>
    public static string CreateCodeString()
    {
        //定义字符数组 
        char[] allCharArray =   {   '0','1','2','3','4','5','6','7','8','9', 
                        'a','b','c','d','e','f','g','h','i','j','k','l','m','n','o', 
                        'p','q','r','s','t','u','v','w','x','y','z'};

        //定义字符串 
        string randomCode = "";
        Random rand = new Random();

        //生成6位字符串 
        for (int i = 0; i < 6; i++)
            randomCode += allCharArray[rand.Next(allCharArray.Length)];
        return randomCode;
    }

    #endregion

    #region 随机生成纯数字

    /// <summary>
    /// 随机生成数字
    /// </summary>
    /// <returns></returns>
    public static string CreateCodeData()
    {
        //定义数字数组 
        char[] allCharArray = { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' };

        //定义数字 
        string randomCode = "";
        Random rand = new Random();

        //生成6位数字 
        for (int i = 0; i < 6; i++)
            randomCode += allCharArray[rand.Next(allCharArray.Length)];
        return randomCode;
    }

    #endregion


}
