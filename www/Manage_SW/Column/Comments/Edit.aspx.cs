using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using WebSite.BLL;
using WebSite.Common;
using System.Data;
using WebSite.Model;

public partial class Manage_SW_Column_Message_Edit : AdminRoot
{
    protected int id = DNTRequest.GetQueryInt("ID", 0);
    protected string Model = DNTRequest.GetQueryString("Model");
    Bll_Message BMessage = new Bll_Message();
    protected Mod_Message dto = new Mod_Message();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Bind();
        }
    }

    private void Bind()
    {
        if (id != 0)
        {
            dto = BMessage.GetModel(id);
            if (dto != null )
            {
                dto.IsLook = 1;
                BMessage.Update(dto);
                rblState.SelectedValue = dto.State.ToString();
                txtReply.Text = dto.ReplyContent;
                txtZip.Text = dto.Zip;
                txtName.Text = dto.Name;
                txtTel.Text = dto.Tel;
                txtContent.Text = dto.Content;
                txtCompany.Text = dto.Company;
                txtAddress.Text = dto.Address;


                txtAddDate.Text = dto.AddDate.ToString();

                txtTitle.Text = dto.Title;

                txtFromName.Text = dto.FromName;
                txtEmail.Text = dto.Email;
            }
            else
            {
                MessageBox.ShowRedirect(this, "信息已删除或不存在！", "Column/Comments/List.aspx?" + StringHelper.DelUrlParameter("ID"));
            }
        }
    }
    protected void btnEdit_Click(object sender, EventArgs e)
    {
        dto = BMessage.GetModel(id);
        dto.State = int.Parse(rblState.SelectedValue);
        dto.ReplyContent = txtReply.Text.Trim();
        dto.ReplyDate = DateTime.Now;
        BMessage.Update(dto);
        MessageBox.ShowRedirect(this, "信息保存成功！", "Column/Comments/List.aspx?" + StringHelper.DelUrlParameter("ID"));

    }
}