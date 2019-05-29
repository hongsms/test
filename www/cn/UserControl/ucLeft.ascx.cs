using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebSite.BLL;
using System.Data;

public partial class UserControl_ucLeft : System.Web.UI.UserControl
{
    private string parentid = string.Empty;
    public string ParentId
    {
        get { return parentid; }
        set { parentid = value; }
    }

    private string typeid = string.Empty;
    public string TypeId
    {
        get { return typeid; }
        set { typeid = value; }
    }
    public string LeftHtml = string.Empty;
    public int ProductId = 0;
    private string _Model = string.Empty;
    public string Model
    {
        get { return _Model; }
        set { _Model = value; }
    }
    public string stylehtml = string.Empty;
    public string classhtml = "menu5";

    protected void Page_Load(object sender, EventArgs e)
    {

        NewBind();


    }
    public void NewBind()
    {
        WebSite.BLL.Bll_BaseType BBaseType = new WebSite.BLL.Bll_BaseType();
        String strWhere = String.Empty;
        DataTable dt = new DataTable();
        strWhere = " State=1 AND ParentID =" + ParentId + " AND WebSiteID=" + PageCommon.LanguageID;
        List<WebSite.Model.Mod_BaseType> MBaseType = new List<WebSite.Model.Mod_BaseType>();
        dt = BBaseType.GetList(strWhere + " ORDER BY OrderBy DESC,ID asc").Tables[0];
        stylehtml = dt.Rows.Count.ToString();
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            if (dt.Rows[i]["Link"].ToString() != "")
            {
                LeftHtml += GetHref(dt.Rows[i]["Link"].ToString(), dt.Rows[i]["Title"].ToString(), dt.Rows[i]["ID"].ToString());
            }
            else
            {
                switch (dt.Rows[i]["Model"].ToString())
                {
                    case "DTSQ": LeftHtml += "<li " + GetClass(dt.Rows[i]["ID"].ToString()) + " hsm=\"fadel\"><a href=\"NewsList.aspx?TypeId=" + dt.Rows[i]["ID"].ToString() + "\"  " + GetClass(dt.Rows[i]["ID"].ToString()) + ">" + dt.Rows[i]["Title"].ToString() + "</a></li>";
                        break;
                    case "CPXX": LeftHtml += "<li " + GetClass(dt.Rows[i]["ID"].ToString()) + " hsm=\"fadel\"><a href=\"ProductList.aspx?TypeId=" + dt.Rows[i]["ID"].ToString() + "\"  " + GetClass(dt.Rows[i]["ID"].ToString()) + ">" + dt.Rows[i]["Title"].ToString() + "</a></li>";
                        break;
                    default: LeftHtml += "<li " + GetClass(dt.Rows[i]["ID"].ToString()) + " hsm=\"fadel\"><a href=\"about.aspx?TypeId=" + dt.Rows[i]["ID"].ToString() + "\" >" + dt.Rows[i]["Title"].ToString() + "</a></li>";
                        break;
                }
            }
        }

   
    }

   
    public String GetHref(String Href, String TypeName, String Id)
    {
        switch (ParentId)
        {

            default: LeftHtml = "<li  " + GetClass(Id) + " hsm=\"fadel\"><a href=\"" + Href + "\">" + TypeName + "</a></li>";
                break;
        }
        return LeftHtml;

    }
    public string GetClass(String Id)
    {
        if (Id == TypeId)
        {
            return " class=\"cur\" ";
        }
        return "";
    }



}