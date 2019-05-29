using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebSite.Common;
using WebSite.Model;
using WebSite.BLL;
public partial class cn_CustomInfo : System.Web.UI.Page
{

    protected WebSite.BLL.Bll_BaseType bll_BaseType = new WebSite.BLL.Bll_BaseType();
    protected int id = DNTRequest.GetQueryInt("Id", 0);
    public WebSite.Model.Mod_Information ModelInfo = new WebSite.Model.Mod_Information();
    private WebSite.BLL.Bll_Information GetInfo = new WebSite.BLL.Bll_Information();

    public WebSite.Model.Mod_BaseType ModelBaseType = new WebSite.Model.Mod_BaseType();
    public WebSite.Model.Mod_BaseType MBaseType = new WebSite.Model.Mod_BaseType();

    protected string NextInfo = string.Empty;
    protected string LastInfo = string.Empty;

    public string strLink = "Custom.aspx";

    public List<WebSite.Model.Mod_PicList> PicList = new List<Mod_PicList>();
    public string PageHtml = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        ModelInfo = PageCommon.GetModelInformation(id);

        ModelBaseType = PageCommon.GetModelType(ModelInfo.TypeID);
        string ObjIDPath = ModelBaseType.IDPath;

        TopNavigation1.ObjIDPath = ObjIDPath;
     

        MBaseType = PageCommon.GetModelType(int.Parse(ObjIDPath.Split(',')[0].ToString()));
        //if (ModelBaseType.Image != "")
        //{
        //    MBaseType.Image = ModelBaseType.Image;
        //}
        _ucheader1.ParentID = ObjIDPath.Split(',')[0].ToString();
        _ucheader1.TypeId = ModelBaseType.ID.ToString();

        //统计浏览量
        OperateHelper.SetInformationClick(ModelInfo.ID);


        Bll_PicList BPicList = new Bll_PicList();
        //绑定图片集
        PicList = BPicList.GetModelList("Model='XWXC' and ProductID=" + id + " and State=1 AND WebSiteID=" + PageCommon.LanguageID + " ORDER BY OrderBy asc,ID asc ");


        //上一页下一页
        Mod_Information dto2 = GetInfo.GetLastInfo(ModelInfo, "OrderBy desc,AddDate DESC", string.Format("TypeID={0} And Model='{1}' and WebSiteID={2}", ModelInfo.TypeID, ModelInfo.Model, PageCommon.LanguageID));
        if (dto2 != null)
        {
            LastInfo = "<div><strong>上一篇：</strong><a href=\"CustomInfo.aspx?id=" + dto2.ID + "\"  class=\"els\">" + dto2.Title + "</a></div>";

        }
        else
        {
            LastInfo = "<div><strong>上一篇：</strong><a class=\"els\">没有了</a></div>";
        }

        Mod_Information dto3 = GetInfo.GetNextInfo(ModelInfo, "OrderBy desc,AddDate DESC", string.Format("TypeID={0} And Model='{1}' and WebSiteID={2}", ModelInfo.TypeID, ModelInfo.Model, PageCommon.LanguageID));
        if (dto3 != null)
        {
            NextInfo = "<div><strong>下一篇：</strong><a href=\"CustomInfo.aspx?id=" + dto3.ID + "\"  class=\"els\">" + dto3.Title + "</a></div>";

        }
        else
        {
            NextInfo = "<div><strong>下一篇：</strong><a class=\"els\">没有了</a></div>";


        }
      
    }


}