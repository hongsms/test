using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using WebSite.BLL;
using WebSite.Common;
using System.Data;
using WebSite.Model;

public partial class Manage_SW_Column_Admin_User_InfoEdit : AdminRoot
{
    protected int id = AdminManage.AdminID;
    Bll_AdminUser BAdmin_User = new Bll_AdminUser();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Bind();
        }
    }

    private void Bind()
    {
        Mod_AdminUser dto = new Mod_AdminUser();
        if (id != 0)
        {
            dto = BAdmin_User.GetModel(id);
            if (dto != null)
            {
                txtRealName.Text = dto.RealName;
                txtMobile.Text = dto.Mobile;
                txtQQ.Text = dto.QQ;
                txtEmail.Text = dto.Email;
                txtDepartment.Text = dto.Department;
                txtJob.Text = dto.Job;
            }
            else
            {
                MessageBox.ShowRedirect(this, "信息已删除或不存在！", "Column/Default/Index.aspx");
            }
        }
    }

    protected void btnEdit_Click(object sender, EventArgs e)
    {
        //验证邮箱是否正确
        if (txtEmail.Text.Trim() != "" && !StringHelper.IsValidEmail(txtEmail.Text.Trim()))
        {
            MessageBox.Show(this, "请输入正确的邮箱！");
            return;
        }
        Mod_AdminUser dto = new Mod_AdminUser();
        if (id != 0)
        {
            dto = BAdmin_User.GetModel(id);
        }

        dto.RealName = txtRealName.Text.Trim();
        dto.Mobile = txtMobile.Text.Trim();
        dto.QQ = txtQQ.Text.Trim();
        dto.Email = txtEmail.Text.Trim();
        dto.Department = txtDepartment.Text.Trim();
        dto.Job = txtJob.Text.Trim();
        if (id != 0)
        {
            BAdmin_User.Update(dto);
        }

        MessageBox.ShowRedirect(this, "信息保存成功！");
    }
}