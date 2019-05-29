using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebSite.BLL;
using System.Data;
using WebSite.Model;
using WebSite.Common;

public partial class Manage_SW_Admin_Main : AdminRoot
{
    protected List<Mod_AdminMenu> MAdmin_MenuList = new List<Mod_AdminMenu>();
    Bll_AdminRole BAdmin_Role = new Bll_AdminRole();
    Bll_AdminMenu BAdmin_Menu = new Bll_AdminMenu();
    Bll_AdminWebSite BAdmin_WebSite = new Bll_AdminWebSite();

    protected int w = DNTRequest.GetQueryInt("w", 0);

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (w != 0)
            {
                AdminManage.WebSiteID = w;
                Response.Redirect("/Manage_SW/Admin_Main.aspx");
            }
            GetWebSite();
            MenuBind();
        }
    }

    //获取站点
    private void GetWebSite()
    {
        if (AdminManage.RoleID == 10001)
        {
            Bll_AdminWebSite BWebSite = new Bll_AdminWebSite();
            rptWebSiteList.DataSource = BWebSite.GetList(0, " State=1 ", " OrderBy desc ");
            rptWebSiteList.DataBind();
        }
        else
        {
            Bll_AdminRole BAdmin_Role = new Bll_AdminRole();
            Mod_AdminRole MAdmin_Role = BAdmin_Role.GetModel(AdminManage.RoleID);
            if (MAdmin_Role != null && MAdmin_Role.WebSiteIDStr != "")
            {
                Bll_AdminWebSite BWebSite = new Bll_AdminWebSite();
                rptWebSiteList.DataSource = BWebSite.GetList(0, " ID in(" + MAdmin_Role.WebSiteIDStr + ") and State=1 ", " OrderBy desc ");
                rptWebSiteList.DataBind();
            }
        }
    }

    private void MenuBind()
    {
        if (AdminManage.RoleID == 10001)
        {
            MAdmin_MenuList = BAdmin_Menu.GetModelList(" State=1 and WebSiteID=" + AdminManage.WebSiteID + " Order By OrderBy asc,ID asc ");
            rptMenuRootTopList.DataSource = MAdmin_MenuList.Where(m => m.ParentID == 0);
            rptMenuRootTopList.DataBind();
        }
        else
        {
            Mod_AdminRole MAdmin_Role = BAdmin_Role.GetModel(AdminManage.RoleID);
            if (MAdmin_Role != null && !string.IsNullOrEmpty(MAdmin_Role.RoleKey))
            {
                MAdmin_MenuList = BAdmin_Menu.GetModelList(" State=1 and WebSiteID=" + AdminManage.WebSiteID + " and ID in(" + MAdmin_Role.RoleKey + ") Order By OrderBy asc,ID asc ");
                rptMenuRootTopList.DataSource = MAdmin_MenuList.Where(m => m.ParentID == 0);
                rptMenuRootTopList.DataBind();
            }
        }
    }
}