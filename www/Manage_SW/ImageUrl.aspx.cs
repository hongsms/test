using System;
using System.Web;
using System.Drawing;
using System.IO;
using System.Drawing.Imaging;
using WebSite.Common;

public partial class ImageUrl : System.Web.UI.Page
{
    private int imgwidth = DNTRequest.GetQueryInt("w", 100);
    private int imgheight = DNTRequest.GetQueryInt("h", 100);
    private string imgUrl = DNTRequest.GetQueryString("url");
    private string _model = DNTRequest.GetQueryString("mode");

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!StringHelper.IsNullOrEmpty(imgUrl))
        {
            GetThumbimg(imgUrl, imgwidth, imgheight, _model);
        }
    }

    #region 取图片缩略图
    /// <summary>
    /// 取图片缩略图
    /// </summary>
    /// <param name="app"></param>
    public static void GetThumbimg(string url, int iwidth, int iheight, string mode)
    {
        string imgsrc = url;
        string bgcolor = string.Empty;
        System.Drawing.Image image = MakeThumbnail(imgsrc, iwidth, iheight, mode, bgcolor);
        System.IO.MemoryStream memoryStream = new MemoryStream();
        image.Save(memoryStream, ImageFormat.Jpeg);

        HttpContext.Current.Response.ClearContent();
        HttpContext.Current.Response.ContentType = "image/gif";
        HttpContext.Current.Response.BinaryWrite(memoryStream.ToArray());

        image.Dispose();

        HttpContext.Current.Response.End();
    }
    /// <summary>
    /// 生成缩略图
    /// </summary>
    /// <param name="originalImagePath">源图路径（物理路径）</param>
    /// <param name="width">缩略图宽度</param>
    /// <param name="height">缩略图高度</param>
    /// <param name="mode">生成缩略图的方式</param>    
    private static Image MakeThumbnail(string originalImagePath, int width, int height, string mode, string bgcolor)
    {
        Image originalImage = Image.FromFile(System.Web.HttpContext.Current.Server.MapPath(originalImagePath));

        //新图坐标及尺寸
        int tox = 0;
        int toy = 0;
        int towidth = width;
        int toheight = height;

        //旧图坐标及尺寸
        int x = 0;
        int y = 0;
        int ow = originalImage.Width;
        int oh = originalImage.Height;
        int ogx = 0;
        int ogy = 0;
        int ogwidth = ow;
        int ogheight = oh;


        if (width > 0 && height > 0)
        {
            if ((ow * 1.0 / oh) > (width * 1.0 / height))
            {
                //旧图偏宽，固定宽度
                towidth = width;
                toheight = oh * width / ow;
                toy = (height - toheight) / 2;

                //取旧图位置
                ogwidth = oh * width / height;
                ogx = (ow - ogwidth) / 2;

            }
            else
            {
                //旧图偏高，固定高度
                toheight = height;
                towidth = ow * height / oh;
                tox = (width - towidth) / 2;

                //取旧图位置
                ogheight = ow * height / width;
                ogy = (oh - ogheight) / 2;
            }
        }
        else if (width == 0)
        {
            //宽度为0，即固定高度，宽随高等比例缩文
            toheight = height;
            towidth = ow * height / oh;
            width = towidth;
            tox = (width - towidth) / 2;

            //取旧图位置
            ogheight = oh;
            ogy = (oh - ogheight) / 2;

        }
        else if (height == 0)
        {
            //高度为0，即固定宽度，高随宽等比例缩文
            towidth = width;
            toheight = oh * width / ow;
            height = toheight;
            toy = (height - toheight) / 2;

            //取旧图位置
            ogwidth = ow;
            ogx = (ow - ogwidth) / 2;

        }

        //新建一个bmp图片
        Image bitmap = new Bitmap(width, height);

        //新建一个画板
        Graphics g = Graphics.FromImage(bitmap);

        //设置高质量插值法
        g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.High;

        //设置高质量,低速度呈现平滑程度
        g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;


        //在指定位置并且按指定大小绘制原图片的指定部分
        string modeName = ((string.IsNullOrEmpty(mode) || mode.ToLower().Equals("cut")) ? "cut" : "fill");
        switch (modeName)
        {
            case "cut":
                g.Clear(Color.Transparent);
                g.DrawImage(originalImage
                    , new Rectangle(0, 0, width, height)
                    , new Rectangle(ogx, ogy, ogwidth, ogheight)
                    , GraphicsUnit.Pixel);
                break;
            default:
                if (string.IsNullOrEmpty(bgcolor))
                {
                    g.Clear(Color.White);
                }
                else
                {
                    g.Clear(ColorTranslator.FromHtml("#" + bgcolor));
                }
                g.DrawImage(originalImage
                    , new Rectangle(tox, toy, towidth, toheight)
                    , new Rectangle(x, y, ow, oh)
                    , GraphicsUnit.Pixel);
                break;
        }


        try
        {

            return bitmap;

        }
        catch (System.Exception e)
        {
            throw e;

        }
        finally
        {
            originalImage.Dispose();
            g.Dispose();
        }
    }
    #endregion

}