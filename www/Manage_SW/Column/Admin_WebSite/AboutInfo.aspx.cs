using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using WebSite.Common;

public partial class Manage_SW_Column_Admin_WebSite_AboutInfo : System.Web.UI.Page
{

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            // 文广新局简介
            LoadXml();
        }
    }

    private void LoadXml()
    {

        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.Load(Server.MapPath("~/Manage_SW/Column/Admin_WebSite/AboutInfo.xml"));
        txtAttr1.Text = xmlDoc.SelectSingleNode("/edit/Attr1").InnerText;
        txtAttr2.Text = xmlDoc.SelectSingleNode("/edit/Attr2").InnerText;
        txtAttr3.Text = xmlDoc.SelectSingleNode("/edit/Attr3").InnerText;
        txtAttr4.Text = xmlDoc.SelectSingleNode("/edit/Attr4").InnerText;
        txtAttr5.Text = xmlDoc.SelectSingleNode("/edit/Attr5").InnerText;
        txtAttr6.Text = xmlDoc.SelectSingleNode("/edit/Attr6").InnerText;
        

    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(Server.MapPath("~/Manage_SW/Column/Admin_WebSite/AboutInfo.xml"));

            xmlDoc.SelectSingleNode("/edit/Attr1").InnerText = txtAttr1.Text;
            xmlDoc.SelectSingleNode("/edit/Attr2").InnerText = txtAttr2.Text;
            xmlDoc.SelectSingleNode("/edit/Attr3").InnerText = txtAttr3.Text;

            xmlDoc.SelectSingleNode("/edit/Attr4").InnerText = txtAttr4.Text;
            xmlDoc.SelectSingleNode("/edit/Attr5").InnerText = txtAttr5.Text;

            xmlDoc.SelectSingleNode("/edit/Attr6").InnerText = txtAttr6.Text;


            xmlDoc.Save(Server.MapPath("~/Manage_SW/Column/Admin_WebSite/AboutInfo.xml"));

            MessageBox.ShowRedirect(this, "信息保存成功！");

        }
    }
}