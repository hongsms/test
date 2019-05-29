using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebSite.BLL;

public partial class cn_Index : System.Web.UI.Page
{
    public List<WebSite.Model.Mod_Link> AdbannerList = new List<WebSite.Model.Mod_Link>();

    public List<WebSite.Model.Mod_Link> AdbannerIndexList = new List<WebSite.Model.Mod_Link>();

    protected void Page_Load(object sender, EventArgs e)
    {
        Bll_Link BLink = new Bll_Link();
        string strWhere = string.Format(" Model='{0}' and State=1 and WebSiteID={1} ", "Banner_CN", PageCommon.LanguageID);
        string txtOrder = " IsTop Desc, OrderBy Desc, AddDate ASC ";
        AdbannerList = BLink.GetModelList(int.MaxValue, strWhere, txtOrder);


        strWhere = string.Format(" Model='{0}' and State=1 and WebSiteID={1} ", "Banner_INDEX", PageCommon.LanguageID);
        txtOrder = " IsTop Desc, OrderBy Desc, AddDate ASC ";
        AdbannerIndexList = BLink.GetModelList(int.MaxValue, strWhere, txtOrder);
    }
}