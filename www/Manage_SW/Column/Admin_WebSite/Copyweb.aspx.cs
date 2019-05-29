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

public partial class Manage_SW_Column_Admin_WebSite_Copyweb : System.Web.UI.Page
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
        DataSet ds = BAdmin_WebSite.GetList(" State=1 ");
        if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            ListItem li = new ListItem();
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                li = new ListItem(ds.Tables[0].Rows[i]["WebName"].ToString(), ds.Tables[0].Rows[i]["ID"].ToString());
                ddlWebSite.Items.Add(li);
            }
        }

        ddlSite.Items.Clear();
        BAdmin_WebSite = new Bll_AdminWebSite();
        ds = BAdmin_WebSite.GetList(" 1=1 ");
        if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            ListItem li = new ListItem();
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                li = new ListItem(ds.Tables[0].Rows[i]["WebName"].ToString(), ds.Tables[0].Rows[i]["ID"].ToString());
                ddlSite.Items.Add(li);
            }
        }
    }


    //回朔算法
    public void GetTree(DataTable dt, int ParentID, int MenuID)
    {
        DataRow[] dr = dt.Select(" IsCopy=1 AND ParentID=" + ParentID + " AND WebSiteID=" + ddlWebSite.SelectedValue, " OrderBy asc,ID asc ");

        for (int i = 0; i < dr.Length; i++)
        {
            Mod_AdminMenu dto = new Mod_AdminMenu();
            dto.Title = dr[i]["Title"].ToString();
            dto.ParentID = MenuID;
            dto.State = int.Parse(dr[i]["State"].ToString());
            dto.Url = dr[i]["Url"].ToString();
            dto.OrderBy = int.Parse(dr[i]["OrderBy"].ToString());
            dto.WebSiteID = int.Parse(ddlSite.SelectedValue);
            dto.FunctionModel = dr[i]["FunctionModel"].ToString();
            dto.TypeName = dr[i]["TypeName"].ToString();
            dto.WebSiteManage = dr[i]["WebSiteManage"].ToString();
            dto.Attributes = dr[i]["Attributes"].ToString();
            dto.Url = dr[i]["Url"].ToString();
            int PID = BAdmin_Menu.Add(dto, true);

            GetTree(dt, int.Parse(dr[i]["ID"].ToString()), PID);

        }

    }



    protected void btnEdit_Click(object sender, EventArgs e)
    {
        string WhereStr = " WebSiteID=" + ddlWebSite.SelectedValue + " AND IsCopy=1 ";
        if (ddlWebSite.SelectedValue == ddlSite.SelectedValue)
        {
            MessageBox.ShowMsgAndRedirect(this, "目标站点与生成站点不能一样！", "/Manage_SW/Column/Admin_WebSite/Copyweb.aspx");
            return;
        }
        WebSite.BLL.Bll_AdminMenu bll_AdminMenu = new Bll_AdminMenu();
        if (bll_AdminMenu.Exists(string.Format("WebSiteID={0}", ddlSite.SelectedValue)))
        {
            MessageBox.ShowMsgAndRedirect(this, "生成站点已存在！", "/Manage_SW/Column/Admin_WebSite/Copyweb.aspx");
            return;
        }


        Bll_AdminWebSite BAdmin_WebSite = new Bll_AdminWebSite();
        Mod_AdminWebSite modWebSite = BAdmin_WebSite.GetModel(int.Parse(ddlWebSite.SelectedValue));
        modWebSite.ID = int.Parse(ddlSite.SelectedValue);
        modWebSite.State = 1;
        modWebSite.WebName = ddlSite.SelectedItem.Text;
        BAdmin_WebSite.Update(modWebSite);


        DataSet ds = BAdmin_Menu.GetList(WhereStr);
        if (ds != null && ds.Tables.Count > 0)
        {
            DataTable dt = ds.Tables[0];
            GetTree(dt, 0, 0);

            List<String> SQLStringList = new List<string>();
            //分类表
            DataTable table_dt = GetColumnList("SW_BaseType").Tables[0];
            string strColumn = string.Empty;
            string strSql = string.Empty;
            for (int i = 1; i < table_dt.Rows.Count; i++)
            {
                strColumn += table_dt.Rows[i]["COLUMN_NAME"] + ",";
            }
            strColumn = strColumn.Substring(0, strColumn.Length - 1);

            strSql = "insert  into SW_BaseType (" + strColumn + ") ";
            strSql += "select " + strColumn.Replace("WebSiteID", ddlSite.SelectedValue) + " from SW_BaseType where WebSiteID=" + ddlWebSite.SelectedValue;
            SQLStringList.Add(strSql);

            //新闻表
            table_dt = GetColumnList("SW_Information").Tables[0];
            strColumn = string.Empty;
            strSql = string.Empty;
            for (int i = 1; i < table_dt.Rows.Count; i++)
            {
                strColumn += table_dt.Rows[i]["COLUMN_NAME"] + ",";
            }
            strColumn = strColumn.Substring(0, strColumn.Length - 1);

            strSql = "insert  into SW_Information (" + strColumn + ") ";
            strSql += "select " + strColumn.Replace("WebSiteID", ddlSite.SelectedValue) + " from SW_Information where WebSiteID=" + ddlWebSite.SelectedValue;
            SQLStringList.Add(strSql);

            //新闻相册表
            table_dt = GetColumnList("SW_PicList").Tables[0];
            strColumn = string.Empty;
            strSql = string.Empty;
            for (int i = 1; i < table_dt.Rows.Count; i++)
            {
                strColumn += table_dt.Rows[i]["COLUMN_NAME"] + ",";
            }
            strColumn = strColumn.Substring(0, strColumn.Length - 1);

            strSql = "insert  into SW_PicList (" + strColumn + ") ";
            strSql += "select " + strColumn.Replace("WebSiteID", ddlSite.SelectedValue) + " from SW_PicList where WebSiteID=" + ddlWebSite.SelectedValue;
            SQLStringList.Add(strSql);

            //新闻属性表
            table_dt = GetColumnList("SW_Attr").Tables[0];
            strColumn = string.Empty;
            strSql = string.Empty;
            for (int i = 1; i < table_dt.Rows.Count; i++)
            {
                strColumn += table_dt.Rows[i]["COLUMN_NAME"] + ",";
            }
            strColumn = strColumn.Substring(0, strColumn.Length - 1);

            strSql = "insert  into SW_Attr (" + strColumn + ") ";
            strSql += "select " + strColumn.Replace("WebSiteID", ddlSite.SelectedValue) + " from SW_Attr where WebSiteID=" + ddlWebSite.SelectedValue;
            SQLStringList.Add(strSql);

            //友情链接
            table_dt = GetColumnList("SW_Link").Tables[0];
            strColumn = string.Empty;
            strSql = string.Empty;
            for (int i = 1; i < table_dt.Rows.Count; i++)
            {
                strColumn += table_dt.Rows[i]["COLUMN_NAME"] + ",";
            }
            strColumn = strColumn.Substring(0, strColumn.Length - 1);

            strSql = "insert  into SW_Link (" + strColumn + ") ";
            strSql += "select " + strColumn.Replace("WebSiteID", ddlSite.SelectedValue) + " from SW_Link where WebSiteID=" + ddlWebSite.SelectedValue;
            SQLStringList.Add(strSql);

            int rows = WebSite.DBUtility.DbHelperSQL.ExecuteSqlTran(SQLStringList);
            if (rows == 0)
            {
                MessageBox.ShowMsgAndRedirect(this, "数据生成失败！", "/Manage_SW/Column/Admin_WebSite/Copyweb.aspx");
                return;
            }
            else
            {

               

                MessageBox.ShowMsgAndRedirect(this, "数据生成成功！", "/Manage_SW/Column/Admin_WebSite/Copyweb.aspx");
                return;
            }
        }

    }

    public DataSet GetColumnList(string table_name)
    {
        string strSql = "select COLUMN_NAME from information_schema.columns where table_name='" + table_name + "'";
        return DbHelperSQL.Query(strSql.ToString());
    }
}