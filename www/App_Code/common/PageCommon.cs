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
public class PageCommon : System.Web.UI.Page
{
    public static string LanguageID
    {
        get
        {
            //string url = HttpContext.Current.Request.Url.AbsolutePath;
            string url = HttpContext.Current.Request.Path;
            string catalog = url.Split('/')[1];
            switch (catalog.ToUpper())
            {
                case "EN": return "10002";
                default: return "10001";
            }

        }
        set
        {

        }
    }

    /// <summary>
    /// 根据类型取得介绍
    /// </summary>
    /// <param name="typeID">类型编号</param>    
    public static WebSite.Model.Mod_Information GetModelByTypeID(object typeID)
    {
        return new WebSite.BLL.Bll_Information().GetModel(string.Format("typeid={0} AND WebSiteID={1} and State=1 ", typeID, LanguageID));
    }
    /// <summary>
    /// 获取分类信息
    /// </summary>
    /// <param name="TypeId"></param>
    /// <returns></returns>
    public static WebSite.Model.Mod_BaseType GetModelType(object TypeId)
    {
        WebSite.BLL.Bll_BaseType bll_BaseType = new WebSite.BLL.Bll_BaseType();

        return bll_BaseType.GetModel(string.Format(" ID ={0} AND WebSiteID={1}", TypeId, PageCommon.LanguageID));
    }

    /// <summary>
    /// 获取新闻信息
    /// </summary>
    /// <param name="typeID">类型编号</param>    
    public static WebSite.Model.Mod_Information GetModelInformation(object ID)
    {
        return new WebSite.BLL.Bll_Information().GetModel(string.Format("ID={0} AND WebSiteID={1}", ID, LanguageID));
    }

}