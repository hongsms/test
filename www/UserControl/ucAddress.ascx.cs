using System;
using WebSite.Common;
using WebSite.BLL;
using System.Data;
using System.Text;

public partial class Web_Control_ucAddress : System.Web.UI.UserControl
{
    protected string AddressHtml = string.Empty;
    private string _CssClass = "";
    private string _SelectValue = "0,0,0";
    private string _Towns = "towns";
    private string _Citys = "citys";
    private string _Provinces = "provinces";
    private string _WebSiteID = "10001";

    public string CssClass
    {
        get { return _CssClass; }
        set { _CssClass = value; }
    }
    public string SelectValue
    {
        get { return _SelectValue; }
        set { _SelectValue = value; }
    }
    public string GetValue
    {
        get
        {
            return DNTRequest.GetFormInt(Provinces, 0) + "," + DNTRequest.GetFormInt(Citys, 0) + "," + DNTRequest.GetFormInt(Towns, 0);
        }
    }
    public string Towns
    {
        get { return _Towns; }
        set { _Towns = value; }
    }
    public string Citys
    {
        get { return _Citys; }
        set { _Citys = value; }
    }
    public string Provinces
    {
        get { return _Provinces; }
        set { _Provinces = value; }
    }
    public string WebSiteID
    {
        get { return _WebSiteID; }
        set { _WebSiteID = value; }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        Bind();
    }

    private void Bind()
    {
        Bll_Region BRegion = new Bll_Region();
        string[] arraySelcetValue = SelectValue.Split(',');
        if (arraySelcetValue != null && arraySelcetValue.Length == 3)
        {
            string ProvinceID = arraySelcetValue[0];
            string CityID = arraySelcetValue[1];
            string TownID = arraySelcetValue[2];

            StringBuilder sb = new StringBuilder();
            DataSet ds = BRegion.GetList(0, " RegionGrade=1 and ParentID=0 and WebSiteID=" + WebSiteID + " ", " OrderBy desc,RegionPath asc ");
            if (ds.Tables.Count > 0)
            {
                DataTable dt = ds.Tables[0];
                if (dt.Rows.Count > 0)
                {
                    //一级地区
                    sb.Append("<select name=\"" + Provinces + "\" id=\"" + Provinces + "\"  class=\"" + CssClass + "\" onchange=\"ProvinceChanged('" + Provinces + "','" + Citys + "','" + Towns + "','" + WebSiteID + "');\">");
                    sb.Append("<option value=\"0\">请选择</option>");
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {

                        if (ProvinceID == dt.Rows[i]["ID"].ToString())
                        {
                            sb.Append("<option value=\"" + dt.Rows[i]["ID"].ToString() + "\" selected=\"selected\">" + dt.Rows[i]["RegionName"].ToString() + "</option>");
                        }
                        else
                        {
                            sb.Append("<option value=\"" + dt.Rows[i]["ID"].ToString() + "\" >" + dt.Rows[i]["RegionName"].ToString() + "</option>");
                        }
                    }
                    sb.Append("</select>&nbsp;");

                    //二级地区
                    ds.Clear();
                    dt.Clear();
                    sb.Append("<select name=\"" + Citys + "\" id=\"" + Citys + "\"  class=\"" + CssClass + "\" onchange=\"CityChanged('" + Provinces + "','" + Citys + "','" + Towns + "','" + WebSiteID + "');\">");
                    sb.Append("<option value=\"0\">请选择</option>");
                    if (ProvinceID != "0")
                    {
                        ds = BRegion.GetList(0, " RegionGrade=2 and ParentID=" + ProvinceID + " and WebSiteID=" + WebSiteID + " ", " OrderBy desc,RegionPath asc ");
                        if (ds.Tables.Count > 0)
                        {
                            dt = ds.Tables[0];
                            for (int i = 0; i < dt.Rows.Count; i++)
                            {
                                if (CityID == dt.Rows[i]["ID"].ToString())
                                {
                                    sb.Append("<option value=\"" + dt.Rows[i]["ID"].ToString() + "\" selected=\"selected\">" + dt.Rows[i]["RegionName"].ToString() + "</option>");
                                }
                                else
                                {
                                    sb.Append("<option value=\"" + dt.Rows[i]["ID"].ToString() + "\" >" + dt.Rows[i]["RegionName"].ToString() + "</option>");
                                }
                            }
                        }
                    }
                    sb.Append("</select>&nbsp;");


                    //三级地区
                    ds.Clear();
                    dt.Clear();
                    sb.Append("<select name=\"" + Towns + "\" id=\"" + Towns + "\"  class=\"" + CssClass + "\" onchange=\"TownsChanged('" + Provinces + "','" + Citys + "','" + Towns + "','" + WebSiteID + "');\">");
                    sb.Append("<option value=\"0\">请选择</option>");
                    if (CityID != "0")
                    {
                        ds = BRegion.GetList(0, " RegionGrade=3 and ParentID=" + CityID + " and WebSiteID=" + WebSiteID + " ", " OrderBy desc,RegionPath asc ");
                        if (ds.Tables.Count > 0)
                        {
                            dt = ds.Tables[0];
                            for (int i = 0; i < dt.Rows.Count; i++)
                            {
                                if (TownID == dt.Rows[i]["ID"].ToString())
                                {
                                    sb.Append("<option value=\"" + dt.Rows[i]["ID"].ToString() + "\" selected=\"selected\">" + dt.Rows[i]["RegionName"].ToString() + "</option>");
                                }
                                else
                                {
                                    sb.Append("<option value=\"" + dt.Rows[i]["ID"].ToString() + "\" >" + dt.Rows[i]["RegionName"].ToString() + "</option>");
                                }
                            }
                        }
                    }
                    sb.Append("</select>");
                }
                AddressHtml = sb.ToString();
            }
        }
    }
}