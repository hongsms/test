using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using WebSite.Model;
using WebSite.BLL;

public partial class cn_UserControl_ucheader : System.Web.UI.UserControl
{
    private string typeid = string.Empty;
    public string TypeId
    {
        get { return typeid; }
        set { typeid = value; }
    }

    private string _ParentID = string.Empty;
    public string ParentID
    {
        get { return _ParentID; }
        set { _ParentID = value; }
    }

    public Mod_AdminWebSite modWebSite = new Mod_AdminWebSite();

    public List<WebSite.Model.Mod_Link> AdbannerList = new List<WebSite.Model.Mod_Link>();

    protected void Page_Load(object sender, EventArgs e)
    {
        modWebSite = OperateHelper.GetWebSite(int.Parse(PageCommon.LanguageID));

        Bll_Link BLink = new Bll_Link();
        string strWhere = string.Format(" Model='{0}' and State=1 and WebSiteID={1} ", "QTLJ", PageCommon.LanguageID);
        string txtOrder = " IsTop Desc, OrderBy Desc, AddDate ASC ";
        AdbannerList = BLink.GetModelList(int.MaxValue, strWhere, txtOrder);

    }


    public string NewBind(string ParentId)
    {
        string LeftHtml = string.Empty;

       

        WebSite.BLL.Bll_BaseType BBaseType = new WebSite.BLL.Bll_BaseType();
        String strWhere = String.Empty;
        DataTable dt = new DataTable();

        strWhere = " ID!=10147 AND State=1 AND ParentID =" + ParentId + " AND WebSiteID=" + PageCommon.LanguageID;
        WebSite.Model.Mod_BaseType MBaseType = PageCommon.GetModelType(ParentId);
        if (MBaseType == null) return "";
        dt = BBaseType.GetList(strWhere + " order by OrderBy desc,ID asc ").Tables[0];

        LeftHtml = "<li><a href=\"" + MBaseType.Link + "\" " + GetClass(MBaseType.ID.ToString()) + ">" + MBaseType.Title + "</a>";

      

        if (dt.Rows.Count > 1)
        {
            LeftHtml += "<div class=\"sub-nav abs\">";

          
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (dt.Rows[i]["Link"].ToString() != "")
                {
                    LeftHtml += "<a href=\"" + dt.Rows[i]["Link"].ToString() + "\"  class=\"db\">" + dt.Rows[i]["Title"].ToString() + "</a>";
                   
                }
                else
                {
                    switch (dt.Rows[i]["Model"].ToString())
                    {



                        case "DTSQ": LeftHtml += "<a href=\"NewsList.aspx?TypeId=" + dt.Rows[i]["ID"].ToString() + "\" class=\"db\">" + dt.Rows[i]["Title"].ToString() + "</a>";
                           
                            break;
                        case "CPXX": LeftHtml += "<a href=\"ProductList.aspx?TypeId=" + dt.Rows[i]["ID"].ToString() + "\" class=\"db\" >" + dt.Rows[i]["Title"].ToString() + "</a>";

                            break;
                        default: LeftHtml += "<a href=\"about.aspx?TypeId=" + dt.Rows[i]["ID"].ToString() + "\" class=\"db\" >" + dt.Rows[i]["Title"].ToString() + "</a>";
                           
                            break;
                    }
                }
            }
            LeftHtml += "</div>";
        }
        LeftHtml += "</li>";
        return LeftHtml;
    }



    public string NewMobileBind(string ParentId)
    {
        string LeftMobileHtml = string.Empty;
        WebSite.BLL.Bll_BaseType BBaseType = new WebSite.BLL.Bll_BaseType();
        String strWhere = String.Empty;
        DataTable dt = new DataTable();

        strWhere = "  ID!=10147 AND State=1 AND ParentID =" + ParentId + " AND WebSiteID=" + PageCommon.LanguageID;
        WebSite.Model.Mod_BaseType MBaseType = PageCommon.GetModelType(ParentId);
        if (MBaseType == null) return "";
        dt = BBaseType.GetList(strWhere + " order by OrderBy desc,ID asc ").Tables[0];
        LeftMobileHtml = "<li><a href=\"" + MBaseType.Link + "\">" + MBaseType.Title + "</a>";
        if (dt.Rows.Count > 1)
        {
            LeftMobileHtml += "<ul class=\"sub-nav hsms\">";
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (dt.Rows[i]["Link"].ToString() != "")
                {
                    LeftMobileHtml += "<li><a href=\"" + dt.Rows[i]["Link"].ToString() + "\">" + dt.Rows[i]["Title"].ToString() + "</a></li>";
                }
                else
                {
                    switch (dt.Rows[i]["Model"].ToString())
                    {

                        case "DTSQ":
                            LeftMobileHtml += "<li><a href=\"NewsList.aspx?TypeId=" + dt.Rows[i]["ID"].ToString() + "\" >" + dt.Rows[i]["Title"].ToString() + "</a></li>";
                            break;

                        case "CPXX":
                            LeftMobileHtml += "<li><a href=\"ProductList.aspx?TypeId=" + dt.Rows[i]["ID"].ToString() + "\" >" + dt.Rows[i]["Title"].ToString() + "</a></li>";
                            break;
                        default:
                            LeftMobileHtml += "<li><a href=\"about.aspx?TypeId=" + dt.Rows[i]["ID"].ToString() + "\" >" + dt.Rows[i]["Title"].ToString() + "</a></li>";
                            break;
                    }
                }
            }
            LeftMobileHtml += "</ul>";
        }
        LeftMobileHtml += "</li>";
        return LeftMobileHtml;
    }
    public string GetTypeClass(String Id)
    {
        if (Id == ParentID)
        {
            return " class=\"cur\" ";
        }
        return "";
    }


    public string GetClass(String Id)
    {
        if (Id == ParentID)
        {
            return " class=\"on\" ";
        }
        return "";
    }
}