using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebSite.Common;
using WebSite.BLL;


public partial class cn_ProductList : System.Web.UI.Page
{
    protected WebSite.BLL.Bll_BaseType bll_BaseType = new WebSite.BLL.Bll_BaseType();
    public Int32 TypeId { get; set; }
    public WebSite.Model.Mod_BaseType ModelBaseType = new WebSite.Model.Mod_BaseType();
    protected int ParentID = 0;

    public WebSite.Model.Mod_BaseType MBaseType = new WebSite.Model.Mod_BaseType();

    
    public List<WebSite.Model.Mod_BaseType> BaseTypeList = new List<WebSite.Model.Mod_BaseType>();



    #region 显示页码
    private Int32 _pageindex = 1;
    public Int32 pageIndex
    {
        get { return _pageindex; }
        set { _pageindex = value; }
    }
    #endregion
    #region 显示的条数
    private Int32 _pagesize = 10;
    public Int32 PageSize
    {
        get { return _pagesize; }
        set { _pagesize = value; }
    }
    #endregion

    #region 排序
    private string _order = "IsCommend desc,OrderBy desc,AddDate DESC,id desc";
    public string Order
    {
        get { return _order; }
        set { _order = value; }
    }
    #endregion


    public string PageHtml = string.Empty;

    public List<WebSite.Model.Mod_VW_Information> ModelList = new List<WebSite.Model.Mod_VW_Information>();
    protected void Page_Load(object sender, EventArgs e)
    {
        TypeId = DNTRequest.GetQueryInt("TypeId", 10153);

        ModelBaseType = PageCommon.GetModelType(TypeId);


     
        string ObjIDPath = ModelBaseType.IDPath;
        TopNavigation1.ObjIDPath = ObjIDPath;

        MBaseType = PageCommon.GetModelType(int.Parse(ObjIDPath.Split(',')[0].ToString()));


        if (ModelBaseType.Image != "")
        {
            MBaseType.Image = ModelBaseType.Image;
        }

        _ucheader1.ParentID = ObjIDPath.Split(',')[0].ToString();
        _ucheader1.TypeId = ModelBaseType.ID.ToString();
        if (ObjIDPath.Split(',').Length > 1)
        {
            ucLeft1.TypeId = ObjIDPath.Split(',')[1].ToString();
        }
        else
        {
            ucLeft1.TypeId = ObjIDPath.Split(',')[0].ToString();
        }
        ucLeft1.ParentId = ObjIDPath.Split(',')[0].ToString();


        //string strWhere = "  ParentID =" + ObjIDPath.Split(',')[1].ToString() + " and State=1 AND WebSiteID=" + PageCommon.LanguageID;
        //BaseTypeList = bll_BaseType.GetModelList(strWhere + " ORDER BY OrderBy DESC,ID asc");

        DataBind();
    }

    public void DataBind()
    {
        WebSite.BLL.Bll_Information BInformation = new WebSite.BLL.Bll_Information();
        if (Request.QueryString["current"] != null)
        {
            pageIndex = WebSite.Common.DNTRequest.GetQueryInt("current");
        }
        String PageWhere = WebSite.Common.DNTRequest.GetParameter();

        String strWhere = "WebSiteID=" + PageCommon.LanguageID + " and State='1'";

        if (TypeId !=0)
        {
            strWhere += " and IDPath like '%" + TypeId + "%' ";
        }
       
        int TotleNum = 0;

        ModelList = new WebSite.BLL.Bll_VW_Information().GetModelListByPage(strWhere, Order, pageIndex, PageSize, out TotleNum);


        PageHtml = PageHelper.NewPageHtml(TotleNum, pageIndex, PageSize, PageWhere);
       

    }

    public string GetPicList(int pid,string imgs)
    {
        string strhtml=string.Empty;
        Bll_PicList BPicList = new Bll_PicList();
        //绑定图片集
        List<WebSite.Model.Mod_PicList>  PicList = BPicList.GetModelList("Model='XWXC' and ProductID=" + pid + " and State=1 AND WebSiteID=" + PageCommon.LanguageID + " ORDER BY OrderBy asc,ID asc ");
        if (PicList.Count > 0)
        {
            foreach (var item in PicList)
            {
                strhtml += "<li><img data-src=\"" + item.OriginalUrl + "\" ></li>";
            }
        }
        else
        {
            strhtml = "<li><img data-src=\"" + imgs + "\" ></li>";
        }
        return strhtml;
    }
}