using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebSite.Common;
using WebSite.BLL;
using WebSite.Model;
using System.Data;
using System.Web.UI.WebControls;
using System.IO;
using System.Text;

/// <summary>
///Common 的摘要说明
/// </summary>
public class OperateHelper
{
    #region 后台处理
    /// <summary>
    /// 后台层级标题处理
    /// </summary>
    /// <param name="Title">标题</param>
    /// <param name="ColumnID">层级</param>
    /// <returns></returns>
    public static string SetHierarchyTitle(string Title, int ColumnID)
    {
        string txtstr = string.Empty;
        for (int i = 2; i < ColumnID; i++)
        {
            txtstr += "<span class=\"skin_k\"></span>";
        }
        return txtstr + (ColumnID == 1 ? "" : "<span class=\"skin_hs\"></span>");
    }


    #region 获取类别名称
    /// <summary>
    /// 获取类别名称
    /// </summary>
    /// <param name="TypeID">类别id</param>
    /// <returns>类别名称</returns>
    public static string GetManageTypeName(object TypeID)
    {
        if (StringHelper.IsNumberId(TypeID.ToString()))
        {
            Bll_BaseType BBaseType = new Bll_BaseType();
            Mod_BaseType MBaseType = new Mod_BaseType();



            MBaseType = BBaseType.GetModel(string.Format(" ID ={0} AND WebSiteID={1}", TypeID, AdminManage.WebSiteID));

        
            if (MBaseType != null)
            {
                return MBaseType.Title;
            }
            else
            {
                return "";
            }
        }
        else
        {
            return "";
        }
    }
    #endregion

    #endregion

    #region 绑定分类

    /// <summary>
    /// 绑定上级功能
    /// </summary>
    public static void BindBaseType(DropDownList ddl, int managetype, string Where, string Language)
    {
        Bll_BaseType BBaseType = new Bll_BaseType();
        string WhereStr = " WebSiteID=" + AdminManage.WebSiteID + " ";
        if (Where != "")
        {
            WhereStr += " and " + Where + " ";
        }
        DataSet ds = BBaseType.GetList(WhereStr);
        if (ds != null && ds.Tables.Count > 0)
        {
            DataTable dt = ds.Tables[0];
            if (managetype != 0)
            {          
                managetype = BBaseType.GetModel(string.Format("ID={0} AND WebSiteID={1}", managetype, AdminManage.WebSiteID)).ParentID;
            }
            GetTree(ddl, dt, 0, managetype, Language);
        }
    }
    //回朔算法
    public static void GetTree(DropDownList ddl, DataTable dt, int ColumnID, int ParentID, string Language)
    {
        DataRow[] dr = dt.Select(" ParentID=" + ParentID + " AND WebSiteID=" + AdminManage.WebSiteID, " OrderBy asc,ID asc ");
        string txtstr = " ";
        for (int i = 1; i < ColumnID; i++)
        {
            txtstr += "　";
        }
        if (ColumnID != 0)
        {
            txtstr += "　├ ";
        }
        for (int i = 0; i < dr.Length; i++)
        {
            string strtitle = string.Empty;
           
            strtitle = txtstr + dr[i]["Title"].ToString();
           
            ListItem li = new ListItem(strtitle, dr[i]["ID"].ToString());
            ddl.Items.Add(li);
            GetTree(ddl, dt, ColumnID + 1, int.Parse(dr[i]["ID"].ToString()), Language);
        }
    }





    #endregion


    /// <summary>
    /// 绑定上级功能
    /// </summary>
    public static void BindBaseType(ListBox ddl, int managetype, string Where, string Language)
    {
        Bll_BaseType BBaseType = new Bll_BaseType();
        string WhereStr = " WebSiteID=" + AdminManage.WebSiteID + " ";
        if (Where != "")
        {
            WhereStr += " and " + Where + " ";
        }
        DataSet ds = BBaseType.GetList(WhereStr);
        if (ds != null && ds.Tables.Count > 0)
        {
            DataTable dt = ds.Tables[0];
            if (managetype != 0)
            {
                managetype = BBaseType.GetModel(managetype).ParentID;
            }
            GetTree(ddl, dt, 0, managetype, Language);
        }
    }
    //回朔算法
    public static void GetTree(ListBox ddl, DataTable dt, int ColumnID, int ParentID, string Language)
    {
        DataRow[] dr = dt.Select(" ParentID=" + ParentID + " AND WebSiteID=" + AdminManage.WebSiteID, " OrderBy asc,ID asc ");
        string txtstr = " ";
        for (int i = 1; i < ColumnID; i++)
        {
            txtstr += "　";
        }
        if (ColumnID != 0)
        {
            txtstr += "　├ ";
        }
        for (int i = 0; i < dr.Length; i++)
        {
            string strtitle = string.Empty;
            if (Language == "cn")
            {
                strtitle = txtstr + dr[i]["Title"].ToString();
            }
            else
            {
                strtitle = txtstr + dr[i]["Title"].ToString();
            }
            ListItem li = new ListItem(strtitle, dr[i]["ID"].ToString());
            ddl.Items.Add(li);
            GetTree(ddl, dt, ColumnID + 1, int.Parse(dr[i]["ID"].ToString()), Language);
        }
    }








    #region 获取权限名称
    /// <summary>
    /// 获取类别名称
    /// </summary>
    /// <param name="TypeID">类别id</param>
    /// <returns>类别名称</returns>
    public static string GetRoleName(object RoleID)
    {
        if (StringHelper.IsNumberId(RoleID.ToString()))
        {
            Bll_AdminRole BAdmin_Role = new Bll_AdminRole();
             Mod_AdminRole MAdmin_Role = new Mod_AdminRole();
            MAdmin_Role = BAdmin_Role.GetModel(int.Parse(RoleID.ToString()));
            if (MAdmin_Role != null)
            {
                return MAdmin_Role.RoleName;
            }
            else
            {
                return "";
            }
        }
        else
        {
            return "";
        }
    }
    #endregion

    #region 获取类别名称
    /// <summary>
    /// 获取类别名称
    /// </summary>
    /// <param name="TypeID">类别id</param>
    /// <returns>类别名称</returns>
    public static string GetTypeName(object TypeID)
    {
        if (StringHelper.IsNumberId(TypeID.ToString()))
        {
            Bll_BaseType BBaseType = new Bll_BaseType();
            Mod_BaseType MBaseType = new Mod_BaseType();

            MBaseType = PageCommon.GetModelType(TypeID);
            if (MBaseType != null)
            {
                return MBaseType.Title;
            }
            else
            {
                return "";
            }
        }
        else
        {
            return "";
        }
    }
    #endregion

    #region 获取类别名称
    /// <summary>
    /// 获取类别名称
    /// </summary>
    /// <param name="TypeID">类别id</param>
    /// <returns>类别名称</returns>
    public static string GetTypeLink(object TypeID)
    {
        if (StringHelper.IsNumberId(TypeID.ToString()))
        {
            Bll_BaseType BBaseType = new Bll_BaseType();
            Mod_BaseType MBaseType = new Mod_BaseType();
            MBaseType = PageCommon.GetModelType(TypeID);
            if (MBaseType != null)
            {
                return MBaseType.Link;
            }
            else
            {
                return "";
            }
        }
        else
        {
            return "";
        }
    }
    #endregion

    #region 获取网站配置
    /// <summary>
    /// 获取网站配置
    /// </summary>
    /// <param name="WebSiteID">网站配置id</param>
    /// <returns>Mod_WebSite</returns>
    public static  Mod_AdminWebSite GetWebSite(int WebSiteID)
    {
        Bll_AdminWebSite BAdmin_WebSite = new Bll_AdminWebSite();
        Mod_AdminWebSite MAdmin_WebSite = new Mod_AdminWebSite();
        MAdmin_WebSite = BAdmin_WebSite.GetModel(WebSiteID);
        if (MAdmin_WebSite != null)
        {
            return MAdmin_WebSite;
        }
        else
        {
            return new Mod_AdminWebSite();
        }
    }
    #endregion

    #region 记录点击数

    public static void SetInformationClick(int id)
    {
        Bll_Information BInformation = new Bll_Information();
        if (id != 0 && GetCookie(id.ToString(), "InformationClick") == 0)
        {

            Mod_Information MInformation = PageCommon.GetModelInformation(id);
            if (MInformation != null)
            {
                MInformation.BrowseCount = (MInformation.BrowseCount + 1);
                BInformation.Update(MInformation);
                SetCookie(id.ToString(), id.ToString(), "InformationClick");
            }
        }

    }

    #endregion

    #region 操作cookie

    public static void SetCookie(string NameStr, string ValueStr, string CookieName)
    {
        HttpCookie cookie = HttpContext.Current.Request.Cookies[CookieName];
        if (cookie == null)
        {
            cookie = new HttpCookie(CookieName);
        }
        cookie.Values[NameStr] = ValueStr;
        cookie.Expires = DateTime.Now.Date.AddDays(1.0);
        HttpContext.Current.Response.SetCookie(cookie);
    }
    public static int GetCookie(string NameStr, string CookieName)
    {
        HttpCookie cookie = HttpContext.Current.Request.Cookies[CookieName];
        if (cookie == null || cookie[NameStr] == null)
        {
            return 0;
        }

        return Convert.ToInt32(cookie.Values[NameStr]);
    }
    public static string GetCookieStr(string NameStr, string CookieName)
    {
        HttpCookie cookie = HttpContext.Current.Request.Cookies.Get(CookieName);
        if (cookie == null || cookie[NameStr] == null)
        {
            return "";
        }

        return cookie.Values[NameStr];
    }

    #endregion

    #region 记录登录
    public static void SetLoginLog(int uid, int websiteid)
    {
        
        WebSite.BLL.Bll_User BUser = new WebSite.BLL.Bll_User();
        
        WebSite.Model.Mod_User MUser = BUser.GetModel(uid);
        if (MUser != null)
        {
            //操作最近登录
            MUser.LoginIP = MUser.NewLoginIP;
            MUser.LoginDate = MUser.NewLoginDate;
            MUser.NewLoginIP = System.Web.HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
            MUser.NewLoginDate = DateTime.Now;
            BUser.Update(MUser);
            //记录用户日志
           
        }
    }
    #endregion

    #region 根据区名称获取省市区ID集合

    public static string SetAddressToRegion(string Region)
    {
        string txtstr = "0,0,0";
        if (!string.IsNullOrEmpty(Region))
        {
            Bll_Region BRegion = new Bll_Region();
            DataSet ds = BRegion.GetList(" RegionName='" + Region + "' ");
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                int RegionID = int.Parse(ds.Tables[0].Rows[0]["ID"].ToString());
                Mod_Region dto = BRegion.GetModel(RegionID);
                if (dto != null)
                {
                    txtstr = dto.RegionPath.Substring(1, dto.RegionPath.Length - 2);
                }
            }
        }
        return txtstr;
    }

    #endregion

    #region 获取地区名称

    public static string GetAddressName(int id)
    {
        string txtstr = "";
        Bll_Region BRegion = new Bll_Region();
        Mod_Region dto = BRegion.GetModel(id);
        if (dto != null)
        {
            txtstr = dto.RegionName;
        }

        return txtstr;
    }

    #endregion

    #region 获取地区id

    public static int GetAddressID(string ParentName, string AddressName)
    {
        int aid = 0;
        if (!string.IsNullOrEmpty(AddressName))
        {
            Bll_Region BRegion = new Bll_Region();
            DataSet ds = BRegion.GetList(0, " RegionName='" + AddressName + "' ", " RegionPath asc ");
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    int Grade = int.Parse(ds.Tables[0].Rows[i]["RegionGrade"].ToString());
                    if (Grade > 1)
                    {
                        if (ds.Tables[0].Rows[i]["RegionPath"] != null && ds.Tables[0].Rows[i]["RegionPath"].ToString().Length > 1)
                        {
                            string path = ds.Tables[0].Rows[i]["RegionPath"].ToString().Substring(1, ds.Tables[0].Rows[i]["RegionPath"].ToString().Length - 2); ;
                            string[] array = path.Split(',');
                            Mod_Region MRegion = BRegion.GetModel(int.Parse(array[Grade - 2]));
                            if (MRegion != null && MRegion.RegionName == ParentName)
                            {
                                aid = int.Parse(ds.Tables[0].Rows[i]["ID"].ToString());
                            }
                        }
                    }
                    else
                    {
                        aid = int.Parse(ds.Tables[0].Rows[i]["ID"].ToString());
                    }
                }
            }
        }
        return aid;
    }

    #endregion

    #region 获取用户积分
    /// <summary>
    /// 获取用户积分
    /// </summary>
    /// <returns>用户积分</returns>
    public static int GetUserIntegral()
    {
        WebSite.BLL.Bll_User BUser = new WebSite.BLL.Bll_User();
        WebSite.Model.Mod_User MUser = BUser.GetModel(UserRoot.GetUserID);
        if (MUser != null)
        {
            return MUser.Integral;
        }
        else
        {
            return 0;
        }
    }
    #endregion  

    #region 记录网站浏览数

    public static void SetWebSiteBrowseNum(int WebSiteID)
    {
        Bll_AdminWebSite BAdmin_WebSite = new Bll_AdminWebSite();
        if (WebSiteID != 0 && GetCookie(WebSiteID.ToString(), "WebSiteBrowse") == 0)
        {
            Mod_AdminWebSite MAdmin_WebSite = BAdmin_WebSite.GetModel(WebSiteID);
            if (MAdmin_WebSite != null)
            {
                MAdmin_WebSite.Attr5 = (MAdmin_WebSite.Attr5 + 1);
                BAdmin_WebSite.Update(MAdmin_WebSite);
                SetCookie(WebSiteID.ToString(), "1", "WebSiteBrowse");
            }
        }

    }

    #endregion

    #region 上传单张图片
    /// <summary>
    /// 上传单张图片
    /// </summary>
    /// <param name="hpf">上传控件HttpPostedFile</param>
    /// <param name="OldPath">原来图片地址</param>
    /// <param name="NewPath">新图片保存路径</param>
    /// <param name="IsDel">是否删除旧图</param>
    /// <param name="Size">图片大小（k），如：等于或小于0则默认为1024k</param>
    /// <returns>新图片地址</returns>
    public static string UpPicture(HttpPostedFile hpf, string OldPath, string NewPath, bool IsDel, int Size, out string errorStr)
    {
        if (Size <= 0)
        {
            Size = 1024;
        }
        string txtstr = string.Empty;
        errorStr = "0";
        if (hpf.ContentLength > 0)
        {
            ImageUpload iu = new ImageUpload();
            iu.FormFile = hpf;
            iu.OutFileName = "";
            iu.MaxSize = 1024 * Size;
            iu.SavePath = @"~" + NewPath;
            iu.Upload();

            if (iu.Error != 0)
            {
                switch (iu.Error)
                {
                    case 1:
                        errorStr = "没有上传的文件";
                        break;
                    case 2:
                        errorStr = "类型不允许";
                        break;
                    case 3:
                        errorStr = "大小超限";
                        break;
                    case 4:
                        errorStr = "未知错误";
                        break;
                }
            }
            else
            {
                //删除原来图片
                if (IsDel && !string.IsNullOrEmpty(OldPath))
                {
                    string DUrl = System.Web.HttpContext.Current.Server.MapPath(@"~" + OldPath);
                    if (File.Exists(DUrl)) File.Delete(DUrl);
                }
            }

            txtstr = NewPath + iu.OutFileName;
        }
        else
        {
            txtstr = OldPath;
        }
        return txtstr;
    }
    #endregion



    #region 获取分类路径
    public static string GetTypePath(int TypeId)
    {
        string strPath = string.Empty;
        WebSite.BLL.Bll_BaseType GetInfo = new WebSite.BLL.Bll_BaseType();
        WebSite.Model.Mod_BaseType modBaseType = PageCommon.GetModelType(TypeId);


        if (modBaseType == null) return "<span>></span><p>Product</p>";
        string ObjIDPath = modBaseType.IDPath;
        string[] arrIDPath = ObjIDPath.ToString().Split(',');

        WebSite.Model.Mod_BaseType ModelInfo = new WebSite.Model.Mod_BaseType();
        for (int i = 0; i < arrIDPath.Length; i++)
        {
            ModelInfo = PageCommon.GetModelType(arrIDPath[i]);
            string strLink = string.Empty;
            if ((i + 1) == arrIDPath.Length)
            {
                strPath += " <span>></span><p>" + ModelInfo.Title + "</p>";
            }
            else
            {
                if (i == 0)
                {
                    strPath += " <span>></span><p ><a href=\"/ProductIndex.aspx?TypeId="+ModelInfo.ID+"\" style=\"color:#000\">" + ModelInfo.Title + "</a></p>";
                }
                else
                {
                    strPath += " <span>></span><p ><a href=\"/ProductList.aspx?TypeId=" + ModelInfo.ID + "\" style=\"color:#000\">" + ModelInfo.Title + "</a></p>";
                }
                
            }


        }
        return strPath;
    }
    #endregion
    #region 获取顶级分类ID
    public static int GetParentId(int TypeId)
    {
        string strPath = string.Empty;
        WebSite.BLL.Bll_BaseType GetInfo = new WebSite.BLL.Bll_BaseType();
        WebSite.Model.Mod_BaseType modBaseType = PageCommon.GetModelType(TypeId);

        if (modBaseType == null) return 0;
        string ObjIDPath = modBaseType.IDPath;
        string[] arrIDPath = ObjIDPath.ToString().Split(',');

        return StringHelper.StrToInt(arrIDPath[0], 0);
    }
    #endregion



    



    

    public static String GetImg(object strImage)
    {
        return strImage.ToString().Length > 0 ? strImage.ToString() : "/img/no.png";

    }


}