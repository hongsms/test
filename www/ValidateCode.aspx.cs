using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

using System.IO;
using System.Drawing;
using System.Drawing.Imaging;

public partial class Manage_ValidateCode : System.Web.UI.Page
{

    protected void Page_Load(object sender, EventArgs e)
    {
        CreateCheckCodeImage();
    }

    /// <summary>
    /// 生成验证码字符
    /// </summary>
    /// <returns></returns>
    public static string CreateCheckCodeString()
    {
        //定义验证码的字符数组 
        char[] allCharArray =   {   '0','1','2','3','4','5','6','7','8','9', 
                        'A','B','C','D','E','F','G','H','I','J','K','L','M','N','O', 
                        'P','Q','R','S','T','U','V','W','X','Y','Z'};

        //定义验证码字符串 
        string randomCode = "";
        Random rand = new Random();

        //生成6位验证码字符串 
        for (int i = 0; i < 4; i++)
            randomCode += allCharArray[rand.Next(allCharArray.Length)];
        return randomCode;
    }
    /// <summary>
    /// 生成验证码图片 
    /// </summary>
    /// <param name="checkCode"></param>
    public static void CreateCheckCodeImage()
    {
        string checkCode = CreateCheckCodeString();

        //定义图片宽度 
        int iWidth = 60;
        //定义图片高度 
        int iHeight = 22;
        //定义大小为12pt的   Arial字体,用于绘制文字 
        Font font = new Font("Arial", 12, FontStyle.Bold);
        //定义黑色的单色画笔,用于绘制文字 
        SolidBrush brush = new SolidBrush(Color.Red);
        //定义钢笔,用户绘制干扰线 
        Pen pen1 = new Pen(Color.SeaGreen, 0);   //注意这里直接获得一个现有的Color对象 
        Pen pen2 = new Pen(Color.FromArgb(255, 255, 255), 0);   //注意这里根据Argb值获得了一个Color对象 

        //创建一个px*20px的图象 
        Bitmap image = new Bitmap(iWidth, iHeight);
        //从图象获得一个绘图面 
        Graphics g = Graphics.FromImage(image);
        //清除整个绘图画面并以指定颜色填充 
        g.Clear(ColorTranslator.FromHtml("#FFFFFF"));   //注意这里从html颜色代码获取Color对象 
        //定义文字的绘制矩形区域 
        RectangleF rect = new RectangleF(8, 3, iWidth, iHeight);
        //定义一个随机对象，用于绘制干扰线 
        Random rand = new Random();
        //生成两条横向干扰线 
        for (int i = 0; i < 3; i++)
        {
            //定义起点 
            Point p1 = new Point(0, rand.Next(iHeight));
            //定义终点 
            Point p2 = new Point(iWidth, rand.Next(iHeight));
            //绘制直线 
            g.DrawLine(pen1, p1, p2);
        }

        //生成4条纵向干扰线 
        for (int i = 0; i < 5; i++)
        {
            //定义起点 
            Point p1 = new Point(rand.Next(iHeight), 0);
            //定义终点 
            Point p2 = new Point(rand.Next(iHeight), iHeight);
            //绘制直线 
            g.DrawLine(pen2, p1, p2);
        }

        //绘制验证码文字 
        g.DrawString(checkCode, font, brush, rect);
        //保存图片为Jpeg格式 
        //ImageFormat.后面能更改保存图片的格式类型 
        MemoryStream ms = new MemoryStream();
        image.Save(ms, ImageFormat.Jpeg);
        HttpContext.Current.Response.ClearContent();
        HttpContext.Current.Response.ContentType = "image/Jpeg";
        HttpContext.Current.Response.BinaryWrite(ms.ToArray());
        //HttpContext.Current.Session["ValidateCode"] = checkCode;
        //将验证码存入cookies
        //HttpCookie cookies = new HttpCookie("ValidateCode");
        // Cookie保存验证码
        AdminManage.ValidateCode = checkCode;
        //cookies.Value = checkCode;
        //HttpContext.Current.Response.Cookies.Add(cookies);
        //释放对象 
        g.Dispose();
        image.Dispose();
    }
}
