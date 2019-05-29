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
public partial class cn_UserControl_TopNavigation : System.Web.UI.UserControl
{

    private string objIDPath = string.Empty;
    public string ObjIDPath
    {
        get { return objIDPath; }
        set { objIDPath = value; }
    }
    public string LeftHtml = string.Empty;

    public string TypeName = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        GetTypePath();
    }
    private string TypeId = "";

    public string OtherHtml = "";
    public String GetTypePath()
    {
        String strHref = String.Empty;

        string[] arrIDPath = ObjIDPath.ToString().Split(',');
        WebSite.BLL.Bll_BaseType GetInfo = new WebSite.BLL.Bll_BaseType();
        WebSite.Model.Mod_BaseType ModelInfo = new WebSite.Model.Mod_BaseType();
        for (int i = 0; i < arrIDPath.Length; i++)
        {
            ModelInfo = PageCommon.GetModelType(Convert.ToInt32(arrIDPath[i]));
            string strLink = string.Empty;


            //if (ModelInfo.Link!= "")
            //{
            //    strLink = ModelInfo.Link;
            //}
            //else
            //{
            //    switch (ModelInfo.Model)
            //    {
            //        case "ZL": strLink = "ExhibitionList.aspx?TypeId=" + ModelInfo.ID;
            //            break;
            //        case "BNTB": strLink = "AnniversaryList.aspx?TypeId=" + ModelInfo.ID;
            //            break;
            //        case "YJ":
            //        case "HD": strLink = "News.aspx?TypeId=" + ModelInfo.ID;
            //            break;
            //        case "DC": strLink = "collection.aspx?TypeId=" + ModelInfo.ID;
            //            break;
            //        default: strLink = "about.aspx?TypeId=" + ModelInfo.ID;
            //            break;
            //    }
            //}
        



            if ((i + 1) == arrIDPath.Length)
            {
                LeftHtml += " > " + ModelInfo.Title + " ";
            }
            else
            {
                LeftHtml += " > " + ModelInfo.Title + " ";
            }

        }

        return strHref;
    }


}


