using System;
using WebSite.BLL;
using WebSite.Common;

public partial class Manage_SW_Column_Admin_Role_List : AdminRoot
{
    Bll_AdminRole BAdmin_Role = new Bll_AdminRole();
    protected int page = DNTRequest.GetQueryInt("page", 1);

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Bind();
        }
    }

    private void Bind()
    {
        string strWhere = " 1=1 ";
        int NumPerPage = 10;
        rptList.DataSource = BAdmin_Role.GetListByPage(strWhere, " ID ASC ", page, NumPerPage);
        rptList.DataBind();
        int TotleNum = BAdmin_Role.GetRecordCount(strWhere);
        cutepage.Text = PageHelper.ManagePageStr(TotleNum, NumPerPage, page);
    }

    protected void btnDelAll_Click(object sender, EventArgs e)
    {
        string strID = DNTRequest.GetFormString("chkID");
        if (strID == "10001")
        {
            MessageBox.Show(this, "对不起，该角色不能删除！");
        }
        else if (string.IsNullOrEmpty(strID))
        {
            MessageBox.Show(this, "对不起，请选中您要操作的信息！");
        }
        else
        {
            BAdmin_Role.DeleteList((strID + ",").Replace("10001,", "").Trim(','));
            MessageBox.ShowRedirect(this, "删除信息数据成功！");
        }
    }
}