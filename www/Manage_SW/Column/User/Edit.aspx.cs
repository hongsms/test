using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using WebSite.BLL;
using WebSite.Common;
using System.Data;
using WebSite.Model;

public partial class Manage_SW_Column_User_Edit : AdminRoot
{
    protected int id = DNTRequest.GetQueryInt("ID", 0);
    protected string Model = DNTRequest.GetQueryString("Model");
    WebSite.BLL.Bll_User BUser = new WebSite.BLL.Bll_User();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Bind();
        }
    }

    private void Bind()
    {

        WebSite.Model.Mod_User dto = new WebSite.Model.Mod_User();
        if (id != 0)
        {
            dto = BUser.GetModel(id);
            if (dto != null && dto.WebSiteID == AdminManage.WebSiteID)
            {
                //基本资料
                txtUserName.Text = dto.UserName;
                rblState.SelectedValue = dto.State.ToString();
                txtPassword.Attributes.Add("value", "0|0|0|0");
                txtRePassword.Attributes.Add("value", "0|0|0|0");
                txtRealName.Text = dto.RealName;
                rblSex.SelectedValue = dto.Sex.ToString();
                txtBirthDate.Value = dto.BirthDate;
                txtMobile.Text = dto.Mobile;
                txtEmail.Text = dto.Email;

                //账户信息
                txtConsumptionMoney.Text = dto.ConsumptionMoney.ToString();
                txtIntegral.Text = dto.Integral.ToString();
                txtRegisterDate.Text = dto.RegisterDate.ToString();
                txtRegisterIP.Text = dto.RegisterIP;
                if (dto.NewLoginDate > DateTime.Parse("2000-01-01 01:01:01"))
                {
                    txtNewLoginDate.Text = dto.NewLoginDate.ToString();
                }
                txtNewLoginIP.Text = dto.NewLoginIP;

                txtUserName.ReadOnly = true;
                txtUserName.Enabled = true;
                return;
            }
        }
        //MessageBox.ShowRedirect(this, "信息已删除或不存在！", "Column/User/List.aspx?" + StringHelper.DelUrlParameter("ID"));
    }



    protected void btnEdit_Click(object sender, EventArgs e)
    {
        if (txtUserName.Text.Trim() == "")
        {
            MessageBox.Show(this, "请正确填写信息再提交保存！");
            return;
        }

        WebSite.Model.Mod_User dto = new WebSite.Model.Mod_User();

        if (id != 0)
        {
            dto = BUser.GetModel(id);
        }
        //基本信息
        dto.State = int.Parse(rblState.SelectedValue);
        if (txtPassword.Text.Trim() != "0|0|0|0")
        {
            if (txtPassword.Text.Trim() == txtRePassword.Text.Trim())
            {
                dto.Password = StringHelper.GetMD5(txtPassword.Text.Trim());
            }
            else
            {
                MessageBox.Show(this, "密码输入不一致！");
                return;
            }
        }
        dto.UserName = txtUserName.Text.Trim();
        dto.RealName = txtRealName.Text.Trim();
        dto.Sex = rblSex.SelectedValue;
        dto.BirthDate = txtBirthDate.Value.Trim();
        dto.Mobile = txtMobile.Text.Trim();
        dto.Email = txtEmail.Text.Trim();


        if (BUser.Exists(string.Format("UserName='{0}' AND ID!={1}", dto.UserName, dto.ID)))
        {
            MessageBox.Show(this, "该帐号已存在，请重新输入！");
            return;
        }

        //账户信息

        dto.WebSiteID = AdminManage.WebSiteID;
        dto.Model = Model;
        dto.ModifyDate = DateTime.Now;

        if (id != 0)
        {
            BUser.Update(dto);

        }
        else
        {
            BUser.Add(dto);
        }

        MessageBox.ShowRedirect(this, "信息保存成功！", "Column/User/List.aspx?" + StringHelper.DelUrlParameter("ID"));
    }
}