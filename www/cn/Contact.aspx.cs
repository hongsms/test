using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebSite.BLL;
using WebSite.Common;
using WebSite.Model;
public partial class cn_Contact : System.Web.UI.Page
{
    protected WebSite.BLL.Bll_BaseType bll_BaseType = new WebSite.BLL.Bll_BaseType();
    public Int32 TypeId { get; set; }

    public WebSite.Model.Mod_BaseType ModelBaseType = new WebSite.Model.Mod_BaseType();

    protected int ParentID = 0;

    public WebSite.Model.Mod_BaseType MBaseType = new WebSite.Model.Mod_BaseType();

    public WebSite.BLL.Bll_VW_Information BInformation = new WebSite.BLL.Bll_VW_Information();

    public List<WebSite.Model.Mod_VW_Information> ModelList = new List<WebSite.Model.Mod_VW_Information>();

    public string trackAddress = string.Empty;
    public string trackPoints = string.Empty;
    public string myOptions = "";
    public Mod_AdminWebSite modWebSite = new Mod_AdminWebSite();

    public List<WebSite.Model.Mod_Link> AdbannerList = new List<WebSite.Model.Mod_Link>();

    protected void Page_Load(object sender, EventArgs e)
    {
        modWebSite = OperateHelper.GetWebSite(int.Parse(PageCommon.LanguageID));

        TypeId = DNTRequest.GetQueryInt("TypeId", 10147);

        ModelBaseType = PageCommon.GetModelType(TypeId);

        string ObjIDPath = ModelBaseType.IDPath;
        TopNavigation1.ObjIDPath = ObjIDPath;
        MBaseType = PageCommon.GetModelType(int.Parse(ObjIDPath.Split(',')[0].ToString()));
        ucLeft1.TypeId = ObjIDPath.Split(',')[1].ToString();
        ucLeft1.ParentId = ObjIDPath.Split(',')[0].ToString();

        _ucheader1.ParentID = ObjIDPath.Split(',')[0].ToString();



        string strWhere = "WebSiteID=" + PageCommon.LanguageID + " and State='1' and  IDPath like '%" + TypeId + "%'";
        ModelList = BInformation.GetModelList(int.MaxValue, strWhere + " ORDER BY OrderBy desc,AddDate desc");

        foreach (var item in ModelList)
        {
            trackPoints += "new google.maps.LatLng(" + item.Author + ")";


            trackAddress += "\"" + item.Title + "\"";
            if ((ModelList.IndexOf(item) + 1) != ModelList.Count)
            {
                trackAddress += ",";
                trackPoints += ",";
            }
            if (ModelList.IndexOf(item) == 0)
            {
                myOptions = item.Author;
            }
        }


        Bll_Link BLink = new Bll_Link();
        strWhere = string.Format(" Model='{0}' and State=1 and WebSiteID={1} ", "QTLJ", PageCommon.LanguageID);
        string txtOrder = " IsTop Desc, OrderBy Desc, AddDate ASC ";
        AdbannerList = BLink.GetModelList(int.MaxValue, strWhere, txtOrder);
    }
}