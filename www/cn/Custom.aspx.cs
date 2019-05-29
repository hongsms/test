using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebSite.Common;
using WebSite.BLL;
using WebCommon;

public partial class cn_Custom : General
{

    protected WebSite.BLL.Bll_BaseType bll_BaseType = new WebSite.BLL.Bll_BaseType();
    public Int32 TypeId { get; set; }
    private WebSite.BLL.Bll_Information GetInfo = new WebSite.BLL.Bll_Information();
    public WebSite.Model.Mod_BaseType ModelBaseType = new WebSite.Model.Mod_BaseType();
    protected int ParentID = 0;
    public WebSite.Model.Mod_BaseType MBaseType = new WebSite.Model.Mod_BaseType();

    public WebSite.Model.Mod_Information XMJSModelInfo = new WebSite.Model.Mod_Information();
    public WebSite.Model.Mod_Information WMDYSModelInfo = new WebSite.Model.Mod_Information();
    public WebSite.Model.Mod_Information DZLCModelInfo = new WebSite.Model.Mod_Information();

    public List<WebSite.Model.Mod_Information> ModelListXM = new List<WebSite.Model.Mod_Information>();
    public List<WebSite.Model.Mod_Information> ModelListALZS = new List<WebSite.Model.Mod_Information>();
    public List<WebSite.Model.Mod_Information> ModelListPPHZ = new List<WebSite.Model.Mod_Information>();
    public List<WebSite.Model.Mod_Information> ModelListDZXW = new List<WebSite.Model.Mod_Information>();

    public WebSite.Model.Mod_Information CSHHRModelInfo = new WebSite.Model.Mod_Information();
    protected void Page_Load(object sender, EventArgs e)
    {
        TypeId = DNTRequest.GetQueryInt("TypeId", 10158);





        ModelBaseType = PageCommon.GetModelType(TypeId);

        string ObjIDPath = ModelBaseType.IDPath;
        TopNavigation1.ObjIDPath = ObjIDPath;

        MBaseType = PageCommon.GetModelType(int.Parse(ObjIDPath.Split(',')[0].ToString()));


        _ucheader1.ParentID = ObjIDPath.Split(',')[0].ToString();
        _ucheader1.TypeId = ModelBaseType.ID.ToString();

        XMJSModelInfo = PageCommon.GetModelByTypeID(10159);

        string strWhere = "  WebSiteID=" + PageCommon.LanguageID + " and State='1'  and TypeID=10160";
        ModelListXM = GetInfo.GetModelList(int.MaxValue, strWhere + " ORDER BY OrderBy desc,AddDate desc");

        WMDYSModelInfo = PageCommon.GetModelByTypeID(10161);

        DZLCModelInfo = PageCommon.GetModelByTypeID(10162);

        CSHHRModelInfo = PageCommon.GetModelByTypeID(10169);

        strWhere = "  WebSiteID=" + PageCommon.LanguageID + " and State='1'  and TypeID=10163";
        ModelListALZS = GetInfo.GetModelList(int.MaxValue, strWhere + " ORDER BY OrderBy desc,AddDate desc");


        strWhere = "  WebSiteID=" + PageCommon.LanguageID + " and State='1'  and TypeID=10164";
        ModelListPPHZ = GetInfo.GetModelList(int.MaxValue, strWhere + " ORDER BY OrderBy desc,AddDate desc");

        strWhere = "  WebSiteID=" + PageCommon.LanguageID + " and State='1'  and TypeID=10165";
        ModelListDZXW = GetInfo.GetModelList(int.MaxValue, strWhere + " ORDER BY OrderBy desc,AddDate desc");
        PageInit();
    }
    public void Submit()
    {
        string Name = DNTRequest.GetFormStringDecode("Name");
        string Email = DNTRequest.GetFormStringDecode("Email").Trim();
        string Tel = DNTRequest.GetFormStringDecode("Tel").Trim();
        string Address = DNTRequest.GetFormStringDecode("Address").Trim();
        string Zip = DNTRequest.GetFormStringDecode("Zip").Trim();
        string Content = DNTRequest.GetFormStringDecode("Content").Trim();
        string Company = DNTRequest.GetFormStringDecode("Company").Trim();
        string FromName = DNTRequest.GetFormStringDecode("FromName").Trim();
        string Title = DNTRequest.GetFormStringDecode("Title").Trim();
        if (Name == "")
        {
            return;
        }
        WebSite.BLL.Bll_Message BMessage = new WebSite.BLL.Bll_Message();
        WebSite.Model.Mod_Message MMessage = new WebSite.Model.Mod_Message();
        MMessage.Name = Name;
        MMessage.Email = Email;
        MMessage.Tel = Tel;
        MMessage.Address = Address;
        MMessage.Company = Company;
        MMessage.Zip = Zip;
        MMessage.FromName = FromName;
        MMessage.Content = Content;
        MMessage.Title = Title;
        MMessage.Model = "FAQ";
        MMessage.WebSiteID = 10001;
        MMessage.AddDate = DateTime.Now;
        int flag = BMessage.Add(MMessage);
        if (flag > 0)
        {
            MyMessageBox.ShowAndDirect(this.Page, "信息提交成功，请等待管理员查看！", "Custom.aspx");
        }
        else
        {
            litJs.Text = new General().ShowMsg("信息提交失败，请稍后再试!", "warning");
        }
    }
    public string GetPicList(int pid)
    {
        string strhtml = string.Empty;
        Bll_PicList BPicList = new Bll_PicList();
        //绑定图片集
        List<WebSite.Model.Mod_PicList> PicList = BPicList.GetModelList("Model='XWXC' and ProductID=" + pid + " and State=1 AND WebSiteID=" + PageCommon.LanguageID + " ORDER BY OrderBy asc,ID asc ");
        if (PicList.Count > 0)
        {
            foreach (var item in PicList)
            {
                strhtml += "<li><img src=\"" + item.OriginalUrl + "\" ></li>";
            }
        }
        return strhtml;
    }

    public List<WebSite.Model.Mod_PicList> GetPicLists(int pid)
    {
        Bll_PicList BPicList = new Bll_PicList();
        //绑定图片集
        List<WebSite.Model.Mod_PicList> PicList = BPicList.GetModelList("Model='XWXC' and ProductID=" + pid + " and State=1 AND WebSiteID=" + PageCommon.LanguageID + " ORDER BY OrderBy asc,ID asc ");
        return PicList;
    }
}