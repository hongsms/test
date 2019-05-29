<%@ WebHandler Language="C#" Class="upload_ajax" %>

using System;
using System.Web;
using System.IO;
using LitJson;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;

public class upload_ajax : IHttpHandler
{
    public string filepath = "/UploadFiles/";
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
        string action = context.Request["action"];

        switch (action)
        {
            case "EditorFile": //编辑器文件
                EditorFile(context);
                break;
            case "ManagerFile": //管理文件
                ManagerFile(context);
                break;
            default: //普通上传
                UpLoadFile(context);
                break;
        }
    }


    #region 上传文件处理===================================
    private void UpLoadFile(HttpContext context)
    {
        bool _iswater = false; //默认不打水印
        if (context.Request.QueryString["IsWater"] == "1")
            _iswater = true;
        string _delfile = context.Request["DelFilePath"];
        HttpPostedFile _upfile = context.Request.Files["Filedata"];
        if (_upfile == null)
        {
            context.Response.Write("{\"status\": 0, \"msg\": \"请选择要上传文件！\"}");
            return;
        }
        string path = filepath + "User/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + DateTime.Now.Day + "/";
        string fullUpLoadPath = HttpContext.Current.Server.MapPath(path);
        if (!Directory.Exists(fullUpLoadPath))
        {
            Directory.CreateDirectory(fullUpLoadPath);
        }

        string fileExt = GetFileExt(_upfile.FileName); //文件扩展名，不含“.”
        string fileName = DateTime.Now.ToString("yyyyMMddhhssmm") + new Random().Next(0, 1000) + "." + fileExt;
        _upfile.SaveAs(fullUpLoadPath + fileName);
        if (_iswater)
        {
            // 水印
        }
        //删除已存在的旧文件
        if (!string.IsNullOrEmpty(_delfile))
        {
            //DeleteUpFile(_delfile);
        }
        //返回成功信息
        // {"status": 1, "msg": "上传文件成功！", "name": "down_banner.jpg", "path": "/upload/201505/20/201505200948030241.jpg", "thumb": "/upload/201505/20/thumb_201505200948030241.jpg", "size": 17072, "ext": "jpg"}
        context.Response.Write(Newtonsoft.Json.JsonConvert.SerializeObject(new { status = 1, msg = "上传文件成功.", name = fileName, path = path + fileName, thumb = path + fileName, ext = fileExt }));
        context.Response.End();
    }
    #endregion

    #region 编辑器上传处理===================================
    private void EditorFile(HttpContext context)
    {
        bool _iswater = false; //默认不打水印
        if (context.Request.QueryString["IsWater"] == "1")
            _iswater = true;
        HttpPostedFile imgFile = context.Request.Files["imgFile"];
        if (imgFile == null)
        {
            showError(context, "请选择要上传文件！");
            return;
        }
        string path = filepath + "User/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + DateTime.Now.Day + "/";
        string fullUpLoadPath = HttpContext.Current.Server.MapPath(path);
        if (!Directory.Exists(fullUpLoadPath))
        {
            Directory.CreateDirectory(fullUpLoadPath);
        }

        string fileExt = GetFileExt(imgFile.FileName); //文件扩展名，不含“.”
        string fileName = DateTime.Now.ToString("yyyyMMddhhssmm") + new Random().Next(0, 1000) + "." + fileExt;
        imgFile.SaveAs(fullUpLoadPath + fileName);

        //string remsg = upFiles.fileSaveAs(imgFile, false, _iswater);
        //JsonData jd = JsonMapper.ToObject(remsg);
        string status = "1";

        if (status == "0")
        {
            showError(context, "上传失败.");
            return;
        }
        string filePath = path + fileName; //取得上传后的路径
        Hashtable hash = new Hashtable();
        hash["error"] = 0;
        hash["url"] = filePath;
        context.Response.AddHeader("Content-Type", "text/html; charset=UTF-8");
        context.Response.Write(JsonMapper.ToJson(hash));
        context.Response.End();
    }
    //显示错误
    private void showError(HttpContext context, string message)
    {
        Hashtable hash = new Hashtable();
        hash["error"] = 1;
        hash["message"] = message;
        context.Response.AddHeader("Content-Type", "text/html; charset=UTF-8");
        context.Response.Write(JsonMapper.ToJson(hash));
        context.Response.End();
    }
    #endregion

    /// <summary>
    /// 返回文件扩展名，不含“.”
    /// </summary>
    /// <param name="_filepath">文件全名称</param>
    /// <returns>string</returns>
    public string GetFileExt(string _filepath)
    {
        if (string.IsNullOrEmpty(_filepath))
        {
            return "";
        }
        if (_filepath.LastIndexOf(".") > 0)
        {
            return _filepath.Substring(_filepath.LastIndexOf(".") + 1); //文件扩展名，不含“.”
        }
        return "";
    }

    /// <summary>
    /// 删除上传的文件(及缩略图)
    /// </summary>
    /// <param name="_filepath"></param>
    public void DeleteUpFile(string _filepath)
    {
        if (string.IsNullOrEmpty(_filepath))
        {
            return;
        }
        string fullpath = GetMapPath(_filepath); //原图
        if (System.IO.File.Exists(fullpath))
        {
            System.IO.File.Delete(fullpath);
        }
        if (_filepath.LastIndexOf("/") >= 0)
        {
            string thumbnailpath = _filepath.Substring(0, _filepath.LastIndexOf("/")) + "mall_" + _filepath.Substring(_filepath.LastIndexOf("/") + 1);
            string fullTPATH = GetMapPath(thumbnailpath); //宿略图
            if (System.IO.File.Exists(fullTPATH))
            {
                System.IO.File.Delete(fullTPATH);
            }
        }
    }

    #region 获得当前绝对路径
    /// <summary>
    /// 获得当前绝对路径
    /// </summary>
    /// <param name="strPath">指定的路径</param>
    /// <returns>绝对路径</returns>
    public string GetMapPath(string strPath)
    {
        if (strPath.ToLower().StartsWith("http://"))
        {
            return strPath;
        }
        if (HttpContext.Current != null)
        {
            return HttpContext.Current.Server.MapPath(strPath);
        }
        else //非web程序引用
        {
            strPath = strPath.Replace("/", "\\");
            if (strPath.StartsWith("\\"))
            {
                strPath = strPath.Substring(strPath.IndexOf('\\', 1)).TrimStart('\\');
            }
            return System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, strPath);
        }
    }
    #endregion

    #region 浏览文件处理=====================================
    private void ManagerFile(HttpContext context)
    {

        //根目录路径，相对路径
        String rootPath = filepath; //站点目录+上传目录
        //根目录URL，可以指定绝对路径，比如 http://www.yoursite.com/attached/
        String rootUrl = filepath;
        //图片扩展名
        String fileTypes = "gif,jpg,jpeg,png,bmp";

        String currentPath = "";
        String currentUrl = "";
        String currentDirPath = "";
        String moveupDirPath = "";

        String dirPath = GetMapPath(rootPath);
        String dirName = context.Request.QueryString["dir"];

        //根据path参数，设置各路径和URL
        String path = context.Request.QueryString["path"];
        path = String.IsNullOrEmpty(path) ? "" : path;
        if (path == "")
        {
            currentPath = dirPath;
            currentUrl = rootUrl;
            currentDirPath = "";
            moveupDirPath = "";
        }
        else
        {
            currentPath = dirPath + path;
            currentUrl = rootUrl + path;
            currentDirPath = path;
            moveupDirPath = Regex.Replace(currentDirPath, @"(.*?)[^\/]+\/$", "$1");
        }

        //排序形式，name or size or type
        String order = context.Request.QueryString["order"];
        order = String.IsNullOrEmpty(order) ? "" : order.ToLower();

        //不允许使用..移动到上一级目录
        if (Regex.IsMatch(path, @"\.\."))
        {
            context.Response.Write("Access is not allowed.");
            context.Response.End();
        }
        //最后一个字符不是/
        if (path != "" && !path.EndsWith("/"))
        {
            context.Response.Write("Parameter is not valid.");
            context.Response.End();
        }
        //目录不存在或不是目录
        if (!Directory.Exists(currentPath))
        {
            context.Response.Write("Directory does not exist.");
            context.Response.End();
        }

        //遍历目录取得文件信息
        string[] dirList = Directory.GetDirectories(currentPath);
        string[] fileList = Directory.GetFiles(currentPath);

        switch (order)
        {
            case "size":
                Array.Sort(dirList, new NameSorter());
                Array.Sort(fileList, new SizeSorter());
                break;
            case "type":
                Array.Sort(dirList, new NameSorter());
                Array.Sort(fileList, new TypeSorter());
                break;
            case "name":
            default:
                Array.Sort(dirList, new NameSorter());
                Array.Sort(fileList, new NameSorter());
                break;
        }

        Hashtable result = new Hashtable();
        result["moveup_dir_path"] = moveupDirPath;
        result["current_dir_path"] = currentDirPath;
        result["current_url"] = currentUrl;
        result["total_count"] = dirList.Length + fileList.Length;
        List<Hashtable> dirFileList = new List<Hashtable>();
        result["file_list"] = dirFileList;
        for (int i = 0; i < dirList.Length; i++)
        {
            DirectoryInfo dir = new DirectoryInfo(dirList[i]);
            Hashtable hash = new Hashtable();
            hash["is_dir"] = true;
            hash["has_file"] = (dir.GetFileSystemInfos().Length > 0);
            hash["filesize"] = 0;
            hash["is_photo"] = false;
            hash["filetype"] = "";
            hash["filename"] = dir.Name;
            hash["datetime"] = dir.LastWriteTime.ToString("yyyy-MM-dd HH:mm:ss");
            dirFileList.Add(hash);
        }
        for (int i = 0; i < fileList.Length; i++)
        {
            FileInfo file = new FileInfo(fileList[i]);
            Hashtable hash = new Hashtable();
            hash["is_dir"] = false;
            hash["has_file"] = false;
            hash["filesize"] = file.Length;
            hash["is_photo"] = (Array.IndexOf(fileTypes.Split(','), file.Extension.Substring(1).ToLower()) >= 0);
            hash["filetype"] = file.Extension.Substring(1);
            hash["filename"] = file.Name;
            hash["datetime"] = file.LastWriteTime.ToString("yyyy-MM-dd HH:mm:ss");
            dirFileList.Add(hash);
        }
        context.Response.AddHeader("Content-Type", "application/json; charset=UTF-8");
        context.Response.Write(JsonMapper.ToJson(result));
        context.Response.End();
    }

    #region Helper
    public class NameSorter : IComparer
    {
        public int Compare(object x, object y)
        {
            if (x == null && y == null)
            {
                return 0;
            }
            if (x == null)
            {
                return -1;
            }
            if (y == null)
            {
                return 1;
            }
            FileInfo xInfo = new FileInfo(x.ToString());
            FileInfo yInfo = new FileInfo(y.ToString());

            return xInfo.FullName.CompareTo(yInfo.FullName);
        }
    }

    public class SizeSorter : IComparer
    {
        public int Compare(object x, object y)
        {
            if (x == null && y == null)
            {
                return 0;
            }
            if (x == null)
            {
                return -1;
            }
            if (y == null)
            {
                return 1;
            }
            FileInfo xInfo = new FileInfo(x.ToString());
            FileInfo yInfo = new FileInfo(y.ToString());

            return xInfo.Length.CompareTo(yInfo.Length);
        }
    }

    public class TypeSorter : IComparer
    {
        public int Compare(object x, object y)
        {
            if (x == null && y == null)
            {
                return 0;
            }
            if (x == null)
            {
                return -1;
            }
            if (y == null)
            {
                return 1;
            }
            FileInfo xInfo = new FileInfo(x.ToString());
            FileInfo yInfo = new FileInfo(y.ToString());

            return xInfo.Extension.CompareTo(yInfo.Extension);
        }
    }
    #endregion
    #endregion


    public bool IsReusable
    {
        get
        {
            return false;
        }
    }

}