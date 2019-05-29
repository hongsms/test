using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using WebSite.BLL;
using WebSite.Common;

public partial class Manage_SW_Column_Information_List : AdminRoot
{
    protected int Show = DNTRequest.GetQueryInt("Show", 0);
    protected int TypeID = DNTRequest.GetQueryInt("TypeID", 0);
    protected string Model = DNTRequest.GetQueryString("Model");
    protected int managetype = DNTRequest.GetQueryInt("managetype", 0);
    protected string keywords = DNTRequest.GetQueryString("keywords").Trim();
    protected int page = DNTRequest.GetQueryInt("page", 1);
    Bll_Information BInformation = new Bll_Information();

    protected int IsType = DNTRequest.GetQueryInt("IsType", 0);
    protected int IsState = DNTRequest.GetQueryInt("IsState", 0);

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindType();
            ddlShow.SelectedValue = Show.ToString();
            ddlBaseType.SelectedValue = TypeID.ToString();
            Bind();
        }
    }

    private void BindType()
    {
        ddlBaseType.Items.Clear();
        ListItem li = new ListItem("分类", "0");
        ddlBaseType.Items.Add(li);

        string strwhere = " IsAdmin=1";


        if (Model != "")
        {
            strwhere += " and Model='" + Model + "' ";
        }
        if (managetype != 0)
        {
            strwhere += " and IDPath like '%" + managetype + "%'";
        }

        OperateHelper.BindBaseType(ddlBaseType, managetype, strwhere, "cn");
    }

    private void Bind()
    {
        string strWhere = " WebSiteID=" + AdminManage.WebSiteID + " ";

        if (Model != "")
        {
            strWhere += " and Model='" + Model + "' ";
        }

        if (keywords != "")
        {
            strWhere += " and Title like '%" + StringHelper.CleanDangerSQL(keywords) + "%' ";
        }
        if (TypeID != 0)
        {
            strWhere = strWhere + " and IDPath like '%" + TypeID + "%'";
        }
        else
        {
            if (managetype !=0)
            {
                strWhere += " and IDPath like '%" + managetype + "%'";
            }
        }

        if (Show != 0)
        {
            switch (Show)
            {
                case 1:
                    strWhere += " and State=1 ";
                    break;
                case 2:
                    strWhere += " and IsTop=1 ";
                    break;
                case 3:
                    strWhere += " and IsCommend=1 ";
                    break;
            }
        }
        int NumPerPage = 10;
        int TotleNum = 0;
        rptList.DataSource = new WebSite.BLL.Bll_VW_Information().GetModelListByPage(strWhere, "OrderBy desc, AddDate desc ", page, NumPerPage, out TotleNum);
        rptList.DataBind();
        cutepage.Text = PageHelper.ManagePageStr(TotleNum, NumPerPage, page);
    }

    protected void btnDelAll_Click(object sender, EventArgs e)
    {
        string strID = DNTRequest.GetFormString("chkID");
        if (string.IsNullOrEmpty(strID))
        {
            MessageBox.Show(this, "请先选取你要操作的数据，再重试本操作！");
        }
        else
        {
            BInformation.Delete(string.Format(" ID In (" + strID + ") AND  WebSiteID=" + AdminManage.WebSiteID));
            MessageBox.ShowRedirect(this, "删除信息数据成功！");
        }
    }
}