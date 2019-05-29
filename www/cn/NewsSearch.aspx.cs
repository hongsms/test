using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebSite.Common;


public partial class cn_NewsSearch : System.Web.UI.Page
{
    protected WebSite.BLL.Bll_BaseType bll_BaseType = new WebSite.BLL.Bll_BaseType();
    public Int32 TypeId { get; set; }
    public WebSite.Model.Mod_BaseType ModelBaseType = new WebSite.Model.Mod_BaseType();
    protected int ParentID = 0;

    public WebSite.Model.Mod_BaseType MBaseType = new WebSite.Model.Mod_BaseType();

    public List<WebSite.Model.Mod_BaseType> ModelList = new List<WebSite.Model.Mod_BaseType>();

    public List<WebSite.Model.Mod_BaseType> BaseTypeList = new List<WebSite.Model.Mod_BaseType>();

    protected void Page_Load(object sender, EventArgs e)
    {
        TypeId = DNTRequest.GetQueryInt("TypeId", 10149);

        ModelBaseType = PageCommon.GetModelType(TypeId);


        ucNewsList1.TypeId = TypeId.ToString();
        ucNewsList1.PageSize = 10;

        string ObjIDPath = ModelBaseType.IDPath;
      

        MBaseType = PageCommon.GetModelType(int.Parse(ObjIDPath.Split(',')[0].ToString()));

        _ucheader1.ParentID = ObjIDPath.Split(',')[0].ToString();
        _ucheader1.TypeId = ModelBaseType.ID.ToString();
      

        //string strWhere = "  ParentID =" + ObjIDPath.Split(',')[1].ToString() + " and State=1 AND WebSiteID=" + PageCommon.LanguageID;
        //BaseTypeList = bll_BaseType.GetModelList(strWhere + " ORDER BY OrderBy DESC,ID asc");



    }

    public string GetClass(int Id)
    {
        if (Id == TypeId)
        {
            return " class=\"cur\" ";
        }
        return "";
    }
}


