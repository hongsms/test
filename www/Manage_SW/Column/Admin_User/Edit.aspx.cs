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
            BindRole();
            BindWebSite();
            Bind();
        }
    }

    private void BindRole()
    {
        ddlRole.Items.Clear();
        Bll_AdminRole BAdmin_Role = new Bll_AdminRole();
        DataSet ds = BAdmin_Role.GetList(0, " State=1 ", " ID DESC ");
        ListItem li = new ListItem();
        if (ds.Tables.Count > 0)
        {
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                li = new ListItem(ds.Tables[0].Rows[i]["RoleName"].ToString(), ds.Tables[0].Rows[i]["ID"].ToString());
                ddlRole.Items.Add(li);
            }
        }
    }

    protected void BindWebSite()
    {
        ddlWebSite.Items.Clear();
        Bll_AdminWebSite BAdmin_WebSite = new Bll_AdminWebSite();
        DataSet ds = BAdmin_WebSite.GetList(" State=1 ");
        if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            ListItem li = new ListItem();
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                li = new ListItem(ds.Tables[0].Rows[i]["WebName"].ToString(), ds.Tables[0].Rows[i]["ID"].ToString());
                ddlWebSite.Items.Add(li);
            }
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
                txtUserName.Text = dto.UserName;
                txtUserName.Enabled = false;
                txtPassword.Attributes.Add("value", "0|0|0|0");  
                ddlRole.SelectedValue = dto.RoleID.ToString();
                txtRealName.Text = dto.RealName;
                txtMobile.Text = dto.Mobile;
                txtQQ.Text = dto.QQ;
                txtEmail.Text = dto.Email;
                txtDepartment.Text = dto.Department;
                txtJob.Text = dto.Job;
                ddlWebSite.SelectedValue = dto.WebSiteID.ToString();
                rblState.SelectedValue = dto.State.ToString();
            }
            else
            {
                MessageBox.ShowRedirect(this, "信息已删除或不存在！", "Column/Admin_User/List.aspx?" + StringHelper.DelUrlParameter("ID"));
            }
        }
    }

    protected void btnEdit_Click(object sender, EventArgs e)
    {
        if (txtUserName.Text.Trim() == "" || (id == 0 && txtPassword.Text.Trim() == ""))
        {
            MessageBox.Show(this, "请填写完整信息再提交保存！");
            return;
        }
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
        else
        {
            //判断账户是否存在
            if (BAdmin_User.ExistsUserName(txtUserName.Text.Trim()))
            {
                MessageBox.Show(this, "用户名已经存在！");
                return;
            }
        }
        if (txtPassword.Text.Trim() != "0|0|0|0")
        {
            dto.Password = StringHelper.GetMD5(txtPassword.Text.Trim());
        }
        dto.RoleID = int.Parse(ddlRole.SelectedValue);
        dto.RealName = txtRealName.Text.Trim();
        dto.Mobile = txtMobile.Text.Trim();
        dto.QQ = txtQQ.Text.Trim();
        dto.Email = txtEmail.Text.Trim();
        dto.Department = txtDepartment.Text.Trim();
        dto.Job = txtJob.Text.Trim();
        dto.WebSiteID = int.Parse(ddlWebSite.SelectedValue);
        dto.State = int.Parse(rblState.SelectedValue);
        if (id != 0)
        {
            if (dto.ID == 10001)
            {
                if (dto.RoleID == 10001 && dto.State == 1)
                {
                    BAdmin_User.Update(dto);
                }
                else
                {
                    MessageBox.Show(this, "该用户无法更改角色和状态！");
                    return;
                }
            }
            else
            {
                BAdmin_User.Update(dto);
            }
        }
        else
        {
            dto.UserName = txtUserName.Text.Trim();
            dto.AddDate = DateTime.Now;
            BAdmin_User.Add(dto);
        }

        MessageBox.ShowRedirect(this, "信息保存成功！", "Column/Admin_User/List.aspx?" + StringHelper.DelUrlParameter("ID"));
    }
}