using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using WebSite.BLL;
using WebSite.Common;
using System.Data;
using WebSite.Model;


public partial class Manage_SW_Column_Admin_WebSite_Edit : AdminRoot
{
     Bll_AdminWebSite BAdmin_WebSite = new Bll_AdminWebSite();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Bind();
        }
    }

    private void Bind()
    {
        Mod_AdminWebSite dto = new Mod_AdminWebSite();
        dto = BAdmin_WebSite.GetModel(AdminManage.WebSiteID);
        if (dto != null)
        {
            rblWebState.SelectedValue = dto.WebState.ToString();

            txtCloseInfo.Text = dto.CloseInfo;
            txtWebName.Text = dto.WebName;
            txtTitle.Text = dto.Title;
            txtKeywords.Text = dto.Keywords;
            txtDescription.Text = dto.Description;
            txtEmailSmtp.Text = dto.EmailSmtp;
            txtEmailName.Text = dto.EmailName;
            txtEmailPwd.Text = dto.EmailPwd;
            txtCopyright.Text = dto.Copyright;

            txtAttr1.Text = dto.Attr1;
            txtAttr2.Text = dto.Attr2;
            txtAttr3.Text = dto.Attr3;

            txtAttr4.Text = dto.Attr4;
            txtWebUrl.Text = dto.WebUrl;
            txtHomePage.Text = dto.HomePage;
            txtAttr9.Text = dto.Attr9;

            txtReceiveEmail.Text = dto.ReceiveEmail;

           
            rblIsIntegral.SelectedValue = dto.IsIntegral.ToString();
            txtIntegralConversion.Text = Convert.ToInt32(dto.IntegralConversion).ToString();

        }
        else
        {
            MessageBox.ShowRedirect(this, "信息已删除或不存在！", "Column/Default/Index.aspx");
        }
    }

    protected void btnEdit_Click(object sender, EventArgs e)
    {
        Mod_AdminWebSite dto = new Mod_AdminWebSite();
        dto = BAdmin_WebSite.GetModel(AdminManage.WebSiteID);
        if (dto != null)
        {
            dto.WebState = int.Parse(rblWebState.SelectedValue);
            dto.CloseInfo = txtCloseInfo.Text.Trim();
            dto.WebName = txtWebName.Text.Trim();
            dto.Title = txtTitle.Text.Trim();
            dto.Keywords = txtKeywords.Text.Trim();
            dto.Description = txtDescription.Text.Trim();
            dto.EmailSmtp = txtEmailSmtp.Text.Trim();
            dto.EmailName = txtEmailName.Text.Trim();
            dto.EmailPwd = txtEmailPwd.Text.Trim();
            dto.Copyright = txtCopyright.Text.Trim();

            dto.IsIntegral = int.Parse(rblIsIntegral.SelectedValue);
            dto.IntegralConversion = decimal.Parse(txtIntegralConversion.Text.Trim());

            dto.Attr1 = txtAttr1.Text;
            dto.Attr2 = txtAttr2.Text;
            dto.Attr3 = txtAttr3.Text;

            dto.Attr4 = txtAttr4.Text;
            dto.WebUrl = txtWebUrl.Text;
            dto.HomePage = txtHomePage.Text;
            dto.Attr9 = txtAttr9.Text;

            dto.ReceiveEmail = txtReceiveEmail.Text;
            
            BAdmin_WebSite.Update(dto);

            MessageBox.ShowRedirect(this, "信息保存成功！");
        }
    }
}