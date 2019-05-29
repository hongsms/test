using System;
using WebSite.BLL;
using WebSite.Common;
using System.Web.UI.WebControls;
using System.IO;
using System.Web.UI;
using WebSite.Model;
using System.Collections.Generic;
using System.Data;

public partial class Manage_SW_Column_User_List : AdminRoot
{
    protected int TypeID = DNTRequest.GetQueryInt("TypeID", 0);
    protected string Model = DNTRequest.GetQueryString("Model");
    protected string keywords = DNTRequest.GetQueryString("keywords");
    protected int page = DNTRequest.GetQueryInt("page", 1);
    WebSite.BLL.Bll_User BUser = new WebSite.BLL.Bll_User();

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
            strWhere += " and (UserName like '%" + StringHelper.CleanDangerSQL(keywords) + "%' or RealName like '%" + StringHelper.CleanDangerSQL(keywords) + "%'  or Mobile like '%" + StringHelper.CleanDangerSQL(keywords) + "%' or Email like '%" + StringHelper.CleanDangerSQL(keywords) + "%') ";
        }
        if (TypeID != 0)
        {
            strWhere += " and TypeID=" + TypeID + " ";
        }
        int NumPerPage = 10;
        int TotleNum = 0;
        rptList.DataSource = BUser.GetListByPage(strWhere, " RegisterDate desc ", page, NumPerPage, out TotleNum);
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
            BUser.DeleteList(strID);
            MessageBox.ShowRedirect(this, "删除信息数据成功！");
        }
    }
    protected void btnToExcel_Click(object sender, EventArgs e)
    {
        string strWhere = " Model='" + Model + "' and WebSiteID=" + AdminManage.WebSiteID + " ";

        List<WebSite.Model.Mod_User> Lists = BUser.GetModelList(strWhere + " order by RegisterDate desc ");
        if (Lists.Count > 0)
        {
            #region 处理数据
            //新建一个datatable
            DataTable dt = new DataTable();
            dt.Columns.Add("会员账号", typeof(string));
            dt.Columns.Add("姓名", typeof(string));          
            dt.Columns.Add("电话号码", typeof(string));        
            dt.Columns.Add("注册时间", typeof(string));
            //向datatable添加数据
            foreach (var item in Lists)
            {
                DataRow newRow;
                newRow = dt.NewRow();
                newRow["会员账号"] = item.UserName;
                newRow["姓名"] = item.RealName;              
                newRow["电话号码"] = item.Mobile;             
                newRow["注册时间"] = item.RegisterDate.ToString();
                dt.Rows.Add(newRow);
            }
            #endregion
            ExcelHelper.DataTable3Excel(dt, "Memberlist" + DateTime.Now.ToString("yyyyMMddHHmmssfffffff"));
        }
    }
}