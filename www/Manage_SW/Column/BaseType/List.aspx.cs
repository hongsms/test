using System;
using WebSite.BLL;
using WebSite.Common;

public partial class Manage_SW_Column_BaseType_List : AdminRoot
{
    Bll_BaseType BBaseType = new Bll_BaseType();
    protected int page = DNTRequest.GetQueryInt("page", 1);
    protected int IsModel = DNTRequest.GetQueryInt("IsModel", 0);
    protected int IsAdd = DNTRequest.GetQueryInt("IsAdd", 0);
    protected int IsChild = DNTRequest.GetQueryInt("IsChild", 0);
    protected string Model = DNTRequest.GetQueryString("Model");
    protected int IsIncludeType = DNTRequest.GetQueryInt("IsIncludeType", 0);
    protected int IsAd = DNTRequest.GetQueryInt("IsAd", 0);
    protected int IsAlbum = DNTRequest.GetQueryInt("IsAlbum", 0);
    protected int IsLink = DNTRequest.GetQueryInt("IsLink", 0);
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
        if (Model != "")
        {
            strWhere += " and Model = '" + Model + "' ";
        }
        int NumPerPage = 100;
        rptList.DataSource = BBaseType.GetListByPage(strWhere, " IDPath ASC ", page, NumPerPage);
        rptList.DataBind();
        int TotleNum = BBaseType.GetRecordCount(strWhere);
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
            BBaseType.Delete(" WebSiteID=" + AdminManage.WebSiteID + " AND IDPath like '%" + strID.Replace(",", "%' or IDPath like '%") + "%'");
            MessageBox.ShowRedirect(this, "删除信息数据成功！");
        }
    }
}