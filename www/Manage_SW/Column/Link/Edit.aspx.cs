using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using WebSite.BLL;
using WebSite.Common;
using System.Data;
using WebSite.Model;

public partial class Manage_SW_Column_Link_Edit : AdminRoot
{
    protected int id = DNTRequest.GetQueryInt("ID", 0);
    protected string Model = DNTRequest.GetQueryString("Model");
    Bll_Link BLink = new Bll_Link();

    protected int IsLink = DNTRequest.GetQueryInt("IsLink", 1);
    protected int IsSub = DNTRequest.GetQueryInt("IsSub", 0);

    protected int IsSubTitle = DNTRequest.GetQueryInt("IsSubTitle", 0);

    protected int IsImage = DNTRequest.GetQueryInt("IsImage", 0);
    protected int IsImage1 = DNTRequest.GetQueryInt("IsImage1", 0);
    protected string Size = DNTRequest.GetQueryString("Size");

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            
            Bind();
        }
    }

    private void Bind()
    {
        Mod_Link dto = new Mod_Link();
        if (id != 0)
        {
         
            dto = BLink.GetModel(string.Format("ID={0} AND WebSiteID={1}", id, AdminManage.WebSiteID));
            if (dto != null )
            {
                txtTitle.Text = dto.Title;
                txtImage.Text = dto.Image;
                txtImageMobile.Text = dto.ImageMobile;
                
                txtWebUrl.Text = dto.WebUrl;


                rblState.SelectedValue = dto.State.ToString();
                txtOrderBy.Text = dto.OrderBy.ToString();
                txtFileName.Text = dto.FileName;
                txtIntroduction.Text = dto.Info;
                
                rblLinkType.SelectedValue = dto.LinkType.ToString();

              
            }
            else
            {
                MessageBox.ShowRedirect(this, "信息已删除或不存在！", "Column/Link/List.aspx?" + StringHelper.DelUrlParameter("ID"));
            }
        }
    }

  

    protected void btnEdit_Click(object sender, EventArgs e)
    {
        if (txtTitle.Text.Trim() == "" || txtWebUrl.Text.Trim() == "" || !StringHelper.IsNumberId(txtOrderBy.Text.Trim()))
        {
            MessageBox.Show(this, "请正确填写信息再提交保存！");
            return;
        }

        Mod_Link dto = new Mod_Link();

        if (id != 0)
        {           
            dto = BLink.GetModel(string.Format("ID={0} AND WebSiteID={1}", id, AdminManage.WebSiteID));
        }

        dto.Title = txtTitle.Text.Trim();
        dto.WebUrl = txtWebUrl.Text.Trim();
        dto.State = int.Parse(rblState.SelectedValue);
        dto.OrderBy = int.Parse(txtOrderBy.Text.Trim());
        dto.WebSiteID = AdminManage.WebSiteID;
        dto.Model = Model;
        dto.ModifyDate = DateTime.Now;
        dto.Image = txtImage.Text.Trim();
        dto.ImageMobile = txtImageMobile.Text.Trim();
       
        dto.FileName = txtFileName.Text.Trim();
        dto.Info = txtIntroduction.Text.Trim();
        dto.LinkType = int.Parse(rblLinkType.SelectedValue);
        if (id== 0)
        {
            dto.AddDate = DateTime.Now;
            dto.UserID = AdminManage.AdminID;
            dto.UID = BLink.Add(dto);
            dto.ID = dto.UID;
        }

        BLink.Update(dto);


        MessageBox.ShowRedirect(this, "信息保存成功！", "Column/Link/List.aspx?" + StringHelper.DelUrlParameter("ID"));

    }

 
}