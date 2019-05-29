using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using WebSite.BLL;
using WebSite.Common;
using System.Data;
using WebSite.Model;

public partial class Manage_SW_Column_Admin_User_Edit : AdminRoot
{
    protected int id = DNTRequest.GetQueryInt("ID", 0);
    Bll_AdminUser BAdmin_User = new Bll_AdminUser();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            
        }
    }

    protected void btnEdit_Click(object sender, EventArgs e)
    {
        if (txtOldPassword.Text.Trim() == "" || txtNewPassword.Text.Trim() == "" || txtReNewPassword.Text.Trim() == "")
        {
            MessageBox.Show(this, "请填写完整信息再提交保存！");
            return;
        }
        if (txtNewPassword.Text.Trim() != txtReNewPassword.Text.Trim())
        {
            MessageBox.Show(this, "确认新密码不一致！");
            return;
        }

        Mod_AdminUser dto = new Mod_AdminUser();

        dto = AdminManage.MAdmin_User;
        if (dto == null)
        {
            MessageBox.ShowRedirect(this, "请先登录！", "/Manage_SW/Admin_Login.aspx");
            return;
        }

        if (StringHelper.GetMD5(txtOldPassword.Text.Trim()) != dto.Password)
        {
            MessageBox.Show(this, "原密码不正确！");
            return;
        }
        dto.Password = StringHelper.GetMD5(txtNewPassword.Text.Trim());

        BAdmin_User.Update(dto);

        MessageBox.ShowRedirect(this, "密码保存成功！");
    }
}