using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using WebSite.BLL;
using WebSite.Common;
using System.Collections.Generic;

/// <summary>
///TopList 的摘要说明
/// </summary>
public partial class TopList : System.Web.UI.UserControl
{
    #region 是否显示时间
    private string _showtime = "0";
    public string ShowTime
    {
        get { return _showtime; }
        set { _showtime = value; }
    }
    #endregion
    #region 显示的条数
    private Int32 _showtop = 10;
    public Int32 ShowTop
    {
        get { return _showtop; }
        set { _showtop = value; }
    }
    #endregion

    #region 显示的类别
    private string _typeid;
    public string TypeId
    {
        get { return _typeid; }
        set { _typeid = value; }
    }
    #endregion

    #region 显示的字数
    private Int32 _wordnum = 36;
    public Int32 WordNum
    {
        get { return _wordnum; }
        set { _wordnum = value; }
    }
    #endregion

    #region 资讯种类
    private string _model = "";
    public string Model
    {
        get { return _model; }
        set { _model = value; }
    }
    #endregion

    #region 标题链接路径
    private string _href = "#";
    public string Href
    {
        get { return _href; }
        set { _href = value; }
    }
    #endregion

    #region 跳转类型
    private Int32 _target = 0;
    public Int32 Target
    {
        get { return _target; }
        set { _target = value; }
    }
    #endregion

    #region 排序
    private string _order = "IsCommend desc,OrderBy asc,AddDate desc,id desc";
    public string Order
    {
        get { return _order; }
        set { _order = value; }
    }
    #endregion
    public String strWhere = "1=1";
    public List<WebSite.Model.Mod_VW_Information> ModelInfo = new List<WebSite.Model.Mod_VW_Information>();

    #region 显示的类别

    public int Id = 0;

    #endregion
    protected void Page_Load(object sender, EventArgs e)
    {


        WebSite.BLL.Bll_VW_Information BInformation = new WebSite.BLL.Bll_VW_Information();
        strWhere += " and WebSiteID=" + PageCommon.LanguageID + " and State='1'";
        if (TypeId != "")
        {
            strWhere += "  and  IDPath like '%" + TypeId + "%'";
        }
        if (WebSite.Common.DNTRequest.GetQueryStringStringDecode("keys").Trim() != "")
        {
            string Titel = WebSite.Common.DNTRequest.GetQueryStringStringDecode("keys").Trim();
            strWhere += " and title like '%" + WebSite.Common.StringHelper.CleanDangerSQL(Titel) + "%' ";
        }


        ModelInfo = BInformation.GetModelList(ShowTop, strWhere + " ORDER BY " + Order);

       

    }

    public String GetTarget()
    {
        if (Target == 1)
        {
            return "target=_blank";
        }
        return "";
    }

    public string getcur(int newid)
    {
        if (newid == Id)
        {
            return "cur";
        }
        return "";
    }
}
