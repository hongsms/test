using System;
using WebSite.BLL;
using WebSite.Common;
using System.Web.UI.WebControls;
using WebSite.Model;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.IO;
using System.Web.UI;

public partial class Manage_SW_Column_Message_List : AdminRoot
{
  
    protected string keywords = DNTRequest.GetQueryString("keywords");
    protected int page = DNTRequest.GetQueryInt("page", 1);
    Bll_Apply BMessage = new Bll_Apply();
    
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Bind();
        }
    }

    private void Bind()
    {
        string strWhere = " 1=1";

        #region 查询条件
        if (keywords != "")
        {
            strWhere += " and (JOBTITLE like '%" + StringHelper.CleanDangerSQL(keywords) + "%' or ZSXM like '%" + StringHelper.CleanDangerSQL(keywords) + "%') ";
        }
      
        #endregion

        int NumPerPage = 10;
        rptList.DataSource = BMessage.GetListByPage(strWhere, " AddDate Desc ", page, NumPerPage);
        rptList.DataBind();
        int TotleNum = BMessage.GetRecordCount(strWhere);
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
            BMessage.DeleteList(strID);
            MessageBox.ShowRedirect(this, "删除信息数据成功！");
        }
    }
    protected void btnToExcel_Click(object sender, EventArgs e)
    {
        //string strWhere = " Model='" + Model + "' ";

        //List<Mod_Message> Lists = BMessage.GetModelList(strWhere + " order by AddDate desc ");
        //if (Lists.Count > 0)
        //{
        //    #region 处理数据
        //    //新建一个datatable
        //    DataTable dt = new DataTable();
        //    dt.Columns.Add("邮箱地址", typeof(string));
        //    dt.Columns.Add("用户", typeof(string));
        //    dt.Columns.Add("性别", typeof(string));
        //    dt.Columns.Add("提交时间", typeof(string));
        //    dt.Columns.Add("提交IP", typeof(string));
        //    //向datatable添加数据
        //    foreach (Mod_Message item in Lists)
        //    {
        //        DataRow newRow;
        //        newRow = dt.NewRow();
        //        newRow["邮箱地址"] = item.Email;
        //        newRow["用户"] = item.FromName;
        //        newRow["性别"] = item.Sex;
        //        newRow["提交时间"] = item.AddDate.ToString();
        //        newRow["提交IP"] = item.Name;
        //        dt.Rows.Add(newRow);
        //    }
        //    #endregion
        //    ExcelHelper.DataTable3Excel(dt, "Newsletter" + DateTime.Now.ToString("yyyyMMddHHmmssfffffff"));
        //}
    }
}