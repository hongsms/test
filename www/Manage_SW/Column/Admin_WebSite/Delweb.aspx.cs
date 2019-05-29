using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebSite.Model;
using WebSite.BLL;
using System.Data;
using WebSite.DBUtility;
using WebSite.Common;


public partial class Manage_SW_Column_Admin_WebSite_Delweb : System.Web.UI.Page
{
    Bll_AdminMenu BAdmin_Menu = new Bll_AdminMenu();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindWebSite();
        }
    }
    protected void BindWebSite()
    {
        ddlWebSite.Items.Clear();
        Bll_AdminWebSite BAdmin_WebSite = new Bll_AdminWebSite();
        DataSet ds = BAdmin_WebSite.GetList(" 1=1 ");
        if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            ListItem li = new ListItem();
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                li = new ListItem(ds.Tables[0].Rows[i]["WebName"].ToString(), ds.Tables[0].Rows[i]["ID"].ToString());
                ddlWebSite.Items.Add(li);
            }
            li = new ListItem("请选择站点", "0");
            ddlWebSite.Items.Insert(0, li);
        }
    }




    protected void btnEdit_Click(object sender, EventArgs e)
    {
        if (ddlWebSite.SelectedValue == "0")
        {
            MessageBox.ShowMsgAndRedirect(this, "请选择删除站点！", "/Manage_SW/Column/Admin_WebSite/Delweb.aspx");
            return;
        }

        List<String> SQLStringList = new List<string>();

        //新闻表
        string strSql = " DELETE FROM SW_Information where WebSiteID=" + ddlWebSite.SelectedValue;
        SQLStringList.Add(strSql);
        //新闻相册表
        strSql = " DELETE FROM SW_PicList where WebSiteID=" + ddlWebSite.SelectedValue;
        SQLStringList.Add(strSql);

        //新闻属性表
        strSql = " DELETE FROM SW_Attr where WebSiteID=" + ddlWebSite.SelectedValue;
        SQLStringList.Add(strSql);

        //友情链接
        strSql = " DELETE FROM SW_Link where WebSiteID=" + ddlWebSite.SelectedValue;
        SQLStringList.Add(strSql);
        //后台菜单
        strSql = " DELETE FROM SW_AdminMenu where WebSiteID=" + ddlWebSite.SelectedValue;
        SQLStringList.Add(strSql);
        //网站分类
        strSql = " DELETE FROM SW_BaseType where WebSiteID=" + ddlWebSite.SelectedValue;
        SQLStringList.Add(strSql);

        int rows = WebSite.DBUtility.DbHelperSQL.ExecuteSqlTran(SQLStringList);
        if (rows == 0)
        {
            MessageBox.ShowMsgAndRedirect(this, "网站数据删除失败！", "/Manage_SW/Column/Admin_WebSite/Delweb.aspx");
            return;
        }
        else
        {
            MessageBox.ShowMsgAndRedirect(this, "网站数据删除成功！", "/Manage_SW/Column/Admin_WebSite/Delweb.aspx");
            return;
        }


    }


}