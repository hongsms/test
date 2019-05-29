using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebSite.Common;

public partial class cn_about : System.Web.UI.Page
{
    protected WebSite.BLL.Bll_BaseType bll_BaseType = new WebSite.BLL.Bll_BaseType();
    public Int32 TypeId { get; set; }
    public WebSite.Model.Mod_Information ModelInfo = new WebSite.Model.Mod_Information();
    private WebSite.BLL.Bll_Information GetInfo = new WebSite.BLL.Bll_Information();
    public WebSite.Model.Mod_BaseType ModelBaseType = new WebSite.Model.Mod_BaseType();

    protected int ParentID = 0;

    public WebSite.Model.Mod_BaseType MBaseType = new WebSite.Model.Mod_BaseType();

    protected void Page_Load(object sender, EventArgs e)
    {
        TypeId = DNTRequest.GetQueryInt("TypeId", 10140);
        ModelInfo = PageCommon.GetModelByTypeID(TypeId);
        if (ModelInfo == null) ModelInfo = new WebSite.Model.Mod_Information();
        ModelBaseType = PageCommon.GetModelType(TypeId);

        string ObjIDPath = ModelBaseType.IDPath;
        TopNavigation1.ObjIDPath = ObjIDPath;

        MBaseType = PageCommon.GetModelType(int.Parse(ObjIDPath.Split(',')[0].ToString()));


        _ucheader1.ParentID = ObjIDPath.Split(',')[0].ToString();
        _ucheader1.TypeId = ModelBaseType.ID.ToString();

        ucLeft1.TypeId = ObjIDPath.Split(',')[1].ToString();
        ucLeft1.ParentId = ObjIDPath.Split(',')[0].ToString();
    }
}