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
    public string TypeName = string.Empty;


    public int ProductId = 0;

    public WebSite.Model.Mod_BaseType ModBaseType = new WebSite.Model.Mod_BaseType();

    private string _Model = string.Empty;
    public string Model
    {
        get { return _Model; }
        set { _Model = value; }
    }

    public string strstryle = "";
    protected void Page_Load(object sender, EventArgs e)
    {

        NewBind();

        ModBaseType = PageCommon.GetModelType(ParentId);

    }
    public void NewBind()
    {
        WebSite.BLL.Bll_BaseType BBaseType = new WebSite.BLL.Bll_BaseType();
        String strWhere = String.Empty;
        DataTable dt = new DataTable();
        //strWhere = " State=1 AND WebSiteID=10001 AND ParentID=0 and Model ='" + Model + "'";
        strWhere = " State=1 AND ParentID =" + ParentId;
       
        List<WebSite.Model.Mod_BaseType> MBaseType = new List<WebSite.Model.Mod_BaseType>();
        dt = BBaseType.GetList(0, strWhere, "OrderBy asc,id asc").Tables[0];
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
                    case "News": LeftHtml += "<a href=\"News.aspx?TypeId=" + dt.Rows[i]["ID"].ToString() + "\" " + GetClass(dt.Rows[i]["ID"].ToString()) + ">" + dt.Rows[i]["Title"].ToString() + "</a>"; break;
                    case "Product": LeftHtml += "<a href=\"ProductList.aspx?TypeId=" + dt.Rows[i]["ID"].ToString() + "\" " + GetClass(dt.Rows[i]["ID"].ToString()) + " >" + dt.Rows[i]["Title"].ToString() + "</a>"; break;
                    default: LeftHtml += "<a href=\"about.aspx?TypeId=" + dt.Rows[i]["ID"].ToString() + "\" " + GetClass(dt.Rows[i]["ID"].ToString()) + ">" + dt.Rows[i]["Title"].ToString() + "</a>";

                        break;
                }
            }
        }
    }

    public String GetHref(String Href, String TypeName, String Id)
    {
        switch (ParentId)
        {

            default: LeftHtml = "<a href=\"" + Href + "\" " + GetClass(Id) + ">" + TypeName + "</a>";
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