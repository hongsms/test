using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebSite.Model;
using System.Web.UI.HtmlControls;
using System.Data;
using WebSite.BLL;

public partial class cn_UserControl_ucfooter : System.Web.UI.UserControl
{

    private string _ContentValue = string.Empty;
    public string ContentValue
    {
        get { return _ContentValue; }
        set { _ContentValue = value; }
    }
    public Mod_AdminWebSite modWebSite = new Mod_AdminWebSite();

    public List<WebSite.Model.Mod_Link> AdbannerList = new List<WebSite.Model.Mod_Link>();

    public List<WebSite.Model.Mod_Information> ModelListLXWM = new List<WebSite.Model.Mod_Information>();

    private WebSite.BLL.Bll_Information GetInfo = new WebSite.BLL.Bll_Information();
    protected void Page_Load(object sender, EventArgs e)
    {
        modWebSite = OperateHelper.GetWebSite(int.Parse(PageCommon.LanguageID));
        this.Page.Title = modWebSite.Title;
        AddMetaTag("keywords", modWebSite.Keywords);
        AddMetaTag("description", modWebSite.Description);

        Bll_Link BLink = new Bll_Link();
        string strWhere = string.Format(" Model='{0}' and State=1 and WebSiteID={1} ", "LINK", PageCommon.LanguageID);
        string txtOrder = " IsTop Desc, OrderBy Desc, AddDate ASC ";
        AdbannerList = BLink.GetModelList(int.MaxValue, strWhere, txtOrder);


        strWhere = "  WebSiteID=" + PageCommon.LanguageID + " and State='1'  and TypeID=10147";
        ModelListLXWM = GetInfo.GetModelList(int.MaxValue, strWhere + " ORDER BY OrderBy desc,AddDate DESC");

    }
    protected virtual void AddMetaTag(string name, string value)
    {
        if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(value)) return;
        HtmlMeta meta = new HtmlMeta();
        meta.Name = name;
        meta.Content = value;
        Page.Header.Controls.Add(meta);
    }

    public string NewBind(string ParentId)
    {
        string LeftHtml = string.Empty;
        WebSite.BLL.Bll_BaseType BBaseType = new WebSite.BLL.Bll_BaseType();
        String strWhere = String.Empty;
        DataTable dt = new DataTable();

        strWhere = " ID!=10147 AND  State=1 AND ParentID =" + ParentId + " AND WebSiteID=" + PageCommon.LanguageID;
        WebSite.Model.Mod_BaseType MBaseType = PageCommon.GetModelType(ParentId);
        if (MBaseType == null) return "";
        dt = BBaseType.GetList(strWhere + " order by OrderBy desc,ID asc ").Tables[0];
   




        LeftHtml = "<dl class=\"fl\"><dt><a href=\"" + MBaseType.Link + "\">" + MBaseType.Title + "</a></dt>";
        if (dt.Rows.Count > 1)
        {
            LeftHtml += "<div class=\"gdlt\">";
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (dt.Rows[i]["Link"].ToString() != "")
                {
                    LeftHtml += "<dd><a href=\"" + dt.Rows[i]["Link"].ToString() + "\">" + dt.Rows[i]["Title"].ToString() + "</a></dt>";

                }
                else
                {
                    switch (dt.Rows[i]["Model"].ToString())
                    {

                        case "CPHZZ": LeftHtml += "<dd><a href=\"ProductList.aspx?TypeId=" + dt.Rows[i]["ID"].ToString() + "\" >" + dt.Rows[i]["Title"].ToString() + "</a></dt>";
                            break;
                        case "XTHJJFA": LeftHtml += "<dd><a href=\"Solution.aspx?TypeId=" + dt.Rows[i]["ID"].ToString() + "\" >" + dt.Rows[i]["Title"].ToString() + "</a></dt>";
                            break;
                        case "KJDZ": LeftHtml += "<dd><a href=\"Custom.aspx#" + dt.Rows[i]["ID"].ToString() + "\" >" + dt.Rows[i]["Title"].ToString() + "</a></dt>";
                            break;
                        default: LeftHtml += "<dd><a href=\"about.aspx?TypeId=" + dt.Rows[i]["ID"].ToString() + "\" >" + dt.Rows[i]["Title"].ToString() + "</a></dt>";
                            break;
                    }
                }
            }
          
        }
        LeftHtml += "</dl>";
        return LeftHtml;
    }
}