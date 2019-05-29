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
using System.Collections.Generic;

/// <summary>
///NewList 的摘要说明
/// </summary>
public partial class NewList : System.Web.UI.UserControl
{
    #region 显示页码
    private Int32 _pageindex = 1;
    public Int32 pageIndex
    {
        get { return _pageindex; }
        set { _pageindex = value; }
    }
    #endregion
    #region 显示的条数
    private Int32 _pagesize = 10;
    public Int32 PageSize
    {
        get { return _pagesize; }
        set { _pagesize = value; }
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
    #region 排序
    private string _order = "IsCommend desc,OrderBy asc,AddDate DESC,id desc";
    public string Order
    {
        get { return _order; }
        set { _order = value; }
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
    #region 分页HTML
    /// <summary>
    /// 分页HTML
    /// </summary>
    public String PageHtml = String.Empty;
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
    #region 显示的字数
    private Int32 _wordnum = 36;
    public Int32 WordNum
    {
        get { return _wordnum; }
        set { _wordnum = value; }
    }
    #endregion



    public String strWhere = "1=1";
    public List<WebSite.Model.Mod_VW_Information> ModelInfo = new List<WebSite.Model.Mod_VW_Information>();

    public string TopPageHtml = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        DataBind();
    }

    public void DataBind()
    {
        WebSite.BLL.Bll_Information BInformation = new WebSite.BLL.Bll_Information();
        if (Request.QueryString["current"] != null)
        {
            pageIndex =WebSite.Common.DNTRequest.GetQueryInt("current");
        }
        String PageWhere = WebSite.Common.DNTRequest.GetParameter();

        strWhere += " and WebSiteID=" + PageCommon.LanguageID + " and State='1'";
        if (Model != "")
        {
            strWhere += " and model='" + Model + "'";
        }

        if (TypeId != "")
        {
            strWhere += " and IDPath like '%" + TypeId + "%' ";
        }
        if (WebSite.Common.DNTRequest.GetQueryStringStringDecode("keys").Trim() != "")
        {
            string Titel = WebSite.Common.DNTRequest.GetQueryStringStringDecode("keys").Trim();
            strWhere += " and title like '%" + WebSite.Common.StringHelper.CleanDangerSQL(Titel) + "%' ";
        }

        string Tags = WebSite.Common.DNTRequest.GetQueryStringStringDecode("Tags").Trim();
        if (Tags != "")
        {
            foreach (var item in Tags.Split(','))
            {
                strWhere += " and title like '%" + item + "%' ";
            }
        }

        int TotleNum = 0;

        ModelInfo = new WebSite.BLL.Bll_VW_Information().GetModelListByPage(strWhere, Order, pageIndex, PageSize, out TotleNum);
    

        PageHtml = PageHelper.NewPageHtml(TotleNum, pageIndex, PageSize, PageWhere);
        TopPageHtml = PageHelper.TopPageHtml(TotleNum, pageIndex, PageSize, PageWhere);


    }

    public String GetTarget()
    {
        if (Target == 1)
        {
            return "target=_blank";
        }
        return "";
    }

}
