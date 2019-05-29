using System;
using WebSite.BLL;
using WebSite.Common;
using System.Web.UI.WebControls;

public partial class Manage_SW_Column_Link_List : AdminRoot
{
    protected int TypeID = DNTRequest.GetQueryInt("TypeID", 0);
    protected string Model = DNTRequest.GetQueryString("Model");
    protected string keywords = DNTRequest.GetQueryString("keywords").Trim();
    protected int page = DNTRequest.GetQueryInt("page", 1);
    Bll_Link BLink = new Bll_Link();
    protected int IsImage = DNTRequest.GetQueryInt("IsImage", 0);
    protected int IsLink = DNTRequest.GetQueryInt("IsLink", 1);

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Bind();
        }
    }

    private void Bind()
    {
        string strWhere = " Model='" + Model + "' and WebSiteID=" + AdminManage.WebSiteID + " ";
        if (keywords != "")
        {
            strWhere += " and Title like '%" + StringHelper.CleanDangerSQL(keywords) + "%' ";
        }
        int NumPerPage = 10;
        rptList.DataSource = BLink.GetListByPage(strWhere, " OrderBy desc,AddDate asc ", page, NumPerPage);
        rptList.DataBind();
        int TotleNum = BLink.GetRecordCount(strWhere);
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
            BLink.Delete(string.Format(" ID In (" + strID + ") AND  WebSiteID=" + AdminManage.WebSiteID));
        
            MessageBox.ShowRedirect(this, "删除信息数据成功！");


        }
    }
}