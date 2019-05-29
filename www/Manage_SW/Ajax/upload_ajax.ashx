<%@ WebHandler Language="C#" Class="upload_ajax" %>

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

public class upload_ajax : IHttpHandler, IRequiresSessionState
{

    public void ProcessRequest(HttpContext context)
    {
        if (!AdminManage.IsLogin)
        {
            HttpContext.Current.Response.Redirect("/");
            return;
        }

        string url = HttpContext.Current.Request.UrlReferrer.AbsolutePath.ToString();
        if (url.ToLower().IndexOf("manage_sw") == -1)
        {
            HttpContext.Current.Response.Redirect("/");
            return;
        }
        
        //取得处事类型
        string action = DNTRequest.GetQueryString("action");

        switch (action)
        {
            case "UpLoadFile": //普通上传
                UpLoadFile(context);
                break;
        }

    }

    private void UpLoadFile(HttpContext context)
    {
        HttpPostedFile _upfile = context.Request.Files["Filedata"];//上传控件
        string _delfile = DNTRequest.GetQueryString("DelFilePath");//要删除图片的路径
        string _uprootpath = "/UserFiles/upload/";//要保存的图片根路径
        string _uppath = "";//要保存的图片路径
        string _filetype = DNTRequest.GetQueryString("filetype");//上传类型
        string _filetypes = DNTRequest.GetQueryString("filetypes");//上传类型
        int _filesize = DNTRequest.GetQueryInt("filesize");//文件大小(k)
        
        bool _iswater = false; //默认不打水印
        bool _isthumbnail = false; //默认不生成缩略图

        int _twidth = DNTRequest.GetQueryInt("twidth", 300);//缩略图宽度
        int _theight = DNTRequest.GetQueryInt("theight", 300);//缩略图高度

        if (DNTRequest.GetQueryString("IsWater") == "1")
            _iswater = true;
        if (DNTRequest.GetQueryString("IsThumbnail") == "1")
            _isthumbnail = true;
        if (_upfile == null)
        {
            context.Response.Write("{\"status\": 0, \"msg\": \"请选择要上传文件！\"}");
            return;
        }

        FileUp fu = new FileUp();
        fu.MaxSize = _filesize * 1024;
        fu.IsWater = _iswater;
        fu.IsThumb = _isthumbnail;
        fu.ThumbStyle = 2;
        fu.ThumbWidth = _twidth;
        fu.ThumbHeight = _theight;
        fu.FormFile = _upfile;
        if (_filetypes == "")
        {
            switch (_filetype)
            {
                case "video":
                    fu.FileType = "flv;mp4";
                    break;
                case "file":
                    fu.FileType = "doc;xls;txt;rar;zip;pdf";
                    break;
                default:
                    fu.FileType = "jpg;jpge;png;gif";
                    break;
            }
        }
        else
        {
            fu.FileType = _filetypes.Replace(",", ";");
        }
        if (IsImage(System.IO.Path.GetExtension(_upfile.FileName).Replace(".", "")))
        {
            _uppath = _uprootpath + "image/" + DateTime.Now.ToString("yyyyMMdd") + "/";
            fu.SavePath = _uppath;
            _uprootpath += "image/";
        }
        else
        {
            _uppath = _uprootpath + "file/" + DateTime.Now.ToString("yyyyMMdd") + "/";
            fu.SavePath = _uppath;
            _uprootpath += "file/";
        }
        fu.Upload();

        string msg = string.Empty;
        if (fu.State)
        {
            msg = "{\"status\": 1, \"msg\": \"上传文件成功！\", \"name\": \"" + fu.OutFileName + "\", \"path\": \"" + _uppath + fu.OutFileName + "\", \"thumb\": \"" + _uppath + fu.OutThumbFileName + "\", \"size\": \"" + fu.FileSizeConversion + "\", \"ext\": \"" + fu.OutFileType + "\", \"filename\": \"" + _upfile.FileName + "\", \"filetype\": \"" + _filetype + "\"}";
        }
        else 
        {
            msg = "{\"status\": 0, \"msg\": \"" + fu.Message + "\"}";
        }

        //删除已存在的旧文件，旧文件不为空且应是上传文件，防止跨目录删除
        if (!string.IsNullOrEmpty(_delfile) && _delfile.IndexOf("../") == -1 && _delfile.ToLower().StartsWith(_uprootpath.ToLower()))
        {
            //DeleteUpFile(_delfile);
        }
        //返回成功信息
        context.Response.Write(msg);
        context.Response.End();
    }

    /// <summary>
    /// 删除上传的文件(及缩略图)
    /// </summary>
    /// <param name="_filepath"></param>
    private static void DeleteUpFile(string _filepath)
    {
        if (string.IsNullOrEmpty(_filepath))
        {
            return;
        }
        string fullpath = HttpContext.Current.Server.MapPath(_filepath); //原图
        if (System.IO.File.Exists(fullpath))
        {
            System.IO.File.Delete(fullpath);
        }
        if (_filepath.LastIndexOf("/") >= 0)
        {
            string thumbnailpath = _filepath.Substring(0, _filepath.LastIndexOf("/")) + "mall_" + _filepath.Substring(_filepath.LastIndexOf("/") + 1);
            string fullTPATH = HttpContext.Current.Server.MapPath(thumbnailpath); //宿略图
            if (System.IO.File.Exists(fullTPATH))
            {
                System.IO.File.Delete(fullTPATH);
            }
        }
    }

    /// <summary>
    /// 是否为图片文件
    /// </summary>
    /// <param name="_fileExt">文件扩展名，不含“.”</param>
    private bool IsImage(string Ext)
    {
        ArrayList al = new ArrayList();
        al.Add("bmp");
        al.Add("jpeg");
        al.Add("jpg");
        al.Add("gif");
        al.Add("png");
        if (al.Contains(Ext.ToLower()))
        {
            return true;
        }
        return false;
    }

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }

}