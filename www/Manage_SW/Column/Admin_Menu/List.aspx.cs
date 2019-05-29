using System;
using WebSite.BLL;
using WebSite.Common;
using System.Data;

public partial class Manage_SW_Column_Admin_Menu_List : AdminRoot
{
    Bll_AdminMenu BAdmin_Menu = new Bll_AdminMenu();
    protected int page = DNTRequest.GetQueryInt("page", 1);
    DataTable dtMenuList = new DataTable();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Bind();
        }
    }

    private void Bind()
    {
        string strWhere = " WebSiteID=" + AdminManage.WebSiteID + " ";
        int NumPerPage = 300;
         DataSet ds =   BAdmin_Menu.GetListByPage(strWhere, " IDPath ASC ", page, NumPerPage);
         if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
         {
             dtMenuList = ds.Tables[0].Clone();
             SetDataTable(ds.Tables[0], 0);
         }

         rptList.DataSource = dtMenuList;
         rptList.DataBind();


        rptList.DataBind();
        int TotleNum = BAdmin_Menu.GetRecordCount(strWhere);
        cutepage.Text = PageHelper.ManagePageStr(TotleNum, NumPerPage, page);
    }

    protected void btnDelAll_Click(object sender, EventArgs e)
    {
        string strID = DNTRequest.GetFormString("chkID");
        if (string.IsNullOrEmpty(strID))
        {
            MessageBox.Show(this, "对不起，请选中您要操作的信息！");
        }
        else
        {
            BAdmin_Menu.Delete(" IDPath like '%" + strID.Replace(",", "%' or IDPath like '%") + "%'");
            MessageBox.ShowRedirect(this, "删除信息数据成功！");
        }
    }

    private void SetDataTable(DataTable dt, int ParentID)
    {
        DataRow[] dr = dt.Select(" ParentID=" + ParentID + " ", " OrderBy asc ");
        for (int i = 0; i < dr.Length; i++)
        {
            dtMenuList.ImportRow((DataRow)dr[i]);
            if (dt.Select(" ParentID=" + dr[i]["ID"] + " ", " OrderBy asc ").Length > 0)
            {
                SetDataTable(dt, int.Parse(dr[i]["ID"].ToString()));
            }
        }
    }
    public string GetHref(string ColumnID, string ParentID, string ID)
    {
        string strHref = string.Empty;
        if (int.Parse(ColumnID) <= 2)
        {
            if (ColumnID == "1")
            {
                strHref = "<a href=\"Edit.aspx?" + StringHelper.OtherUrlParameter("ParentID,hidShow", ID + ",0") + "\">添加子菜单</a> | ";
            }
            else
            {
                strHref = "<a href=\"Edit.aspx?" + StringHelper.OtherUrlParameter("ParentID,hidShow", ID + ",1") + "\">添加子菜单</a> | ";
            }

        }

        if (int.Parse(ColumnID) <= 2)
        {
            strHref += "<a href=\"Edit.aspx?" + StringHelper.OtherUrlParameter("ID,hidShow", ID + ",0") + "\">编辑</a>";
        }
        else
        {
            strHref += "<a href=\"Edit.aspx?" + StringHelper.OtherUrlParameter("ID,hidShow", ID + ",1") + "\">编辑</a>";
        }


        return strHref;
    }

}

