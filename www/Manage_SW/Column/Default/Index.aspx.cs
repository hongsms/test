using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebSite.BLL;
using System.Data;
using WebSite.Model;

public partial class Manage_SW_Column_Default_Index : AdminRoot
{
    protected string LastLoginTime = string.Empty;
    protected string LastLoginIP = string.Empty;


    
    
    Bll_AdminWebSite BAdmin_WebSite = new Bll_AdminWebSite();
 
  
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            GetLoginLog();
           
        }
    }


    private void GetLoginLog()
    {
        Bll_AdminLog BAdmin_Log = new Bll_AdminLog();
        DataSet ds = BAdmin_Log.GetList(2, " Model='AdminLogin' and UserID=" + AdminManage.AdminID + " ", " AddDate desc ");
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 1)
        {
            //获取第二个
            LastLoginTime = DateTime.Parse(ds.Tables[0].Rows[1]["AddDate"].ToString()).ToString("yyyy-MM-dd hh:mm:ss");
            LastLoginIP = ds.Tables[0].Rows[1]["UserIP"].ToString();
        }
    }
}