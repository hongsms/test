using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using System.Linq;
using WebSite.BLL;
using WebSite.Common;
using System.Data;
using WebSite.Model;

public partial class Manage_SW_Column_Admin_Role_Edit : AdminRoot
{
    protected int id = DNTRequest.GetQueryInt("ID", 0);
    Bll_AdminRole BAdmin_Role = new Bll_AdminRole();
    Bll_AdminWebSite BAdmin_WebSite = new Bll_AdminWebSite();
    Bll_AdminMenu BAdmin_Menu = new Bll_AdminMenu();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Bind();
        }
    }

    private void Bind()
    {
        Mod_AdminRole dto = new Mod_AdminRole();
        if (id != 0)
        {
            dto = BAdmin_Role.GetModel(id);
            if (dto != null)
            {
                txtRoleName.Text = dto.RoleName;
                rblState.SelectedValue = dto.State.ToString();
                BindColumn(dto.RoleKey, dto.WebSiteIDStr);
            }
            else
            {
                MessageBox.ShowRedirect(this, "信息已删除或不存在！", "Column/Admin_Role/List.aspx?" + StringHelper.DelUrlParameter("ID"));
            }
        }
        else 
        {
            BindColumn("", "");
        }
    }

    private void BindColumn(string RoleKey, string WebSiteIDStr)
    {
        List<Mod_AdminWebSite> WebSiteList = BAdmin_WebSite.GetModelList(" state=1 Order By OrderBy asc ");
        List<Mod_AdminMenu> MenuList = BAdmin_Menu.GetModelList(" state=1 Order By IDPath asc ");


        //新建一个datatable
        DataTable dt = new DataTable();
        dt.Columns.Add("ID", typeof(int));
        dt.Columns.Add("Title", typeof(string));
        dt.Columns.Add("ColumnID", typeof(int));
        dt.Columns.Add("WebSiteID", typeof(int));
        dt.Columns.Add("IDPath", typeof(string));
        dt.Columns.Add("IsCheckbox", typeof(int));
        //向datatable添加数据
        foreach (var WebSite in WebSiteList)
        {
            DataRow newRow;
            newRow = dt.NewRow();
            newRow["ID"] = WebSite.ID;
            newRow["Title"] = WebSite.Title;
            newRow["ColumnID"] = 1;
            newRow["WebSiteID"] = WebSite.ID;
            newRow["IDPath"] = WebSite.ID;
            if (WebSiteIDStr.IndexOf(WebSite.ID.ToString()) > -1)
            {
                newRow["IsCheckbox"] = 1;
            }
            else
            {
                newRow["IsCheckbox"] = 0;
            }
            dt.Rows.Add(newRow);

            IEnumerable<Mod_AdminMenu> SubMenuList = MenuList.Where(m => m.WebSiteID == WebSite.ID);
            if (SubMenuList.Count() > 0)
            {
                foreach (var Menu in SubMenuList)
                {
                    DataRow newSubRow;
                    newSubRow = dt.NewRow();
                    newSubRow["ID"] = Menu.ID;
                    newSubRow["Title"] = Menu.Title;
                    newSubRow["ColumnID"] = Menu.ColumnID + 1;
                    newSubRow["WebSiteID"] = WebSite.ID;
                    newSubRow["IDPath"] = Menu.IDPath;
                    if (RoleKey.IndexOf(Menu.ID.ToString()) > -1)
                    {
                        newSubRow["IsCheckbox"] = 1;
                    }
                    else
                    {
                        newSubRow["IsCheckbox"] = 0;
                    }
                    dt.Rows.Add(newSubRow);
                }
            }
        }
        //绑定
        rptWebSite.DataSource = dt;
        rptWebSite.DataBind();

    }

    protected void btnEdit_Click(object sender, EventArgs e)
    {
        if (txtRoleName.Text.Trim() == "")
        {
            MessageBox.Show(this, "请填写完整信息再提交保存！");
            return;
        }
        Mod_AdminRole dto = new Mod_AdminRole();
        if (id != 0)
        {
            dto = BAdmin_Role.GetModel(id);
        }
        dto.RoleName = txtRoleName.Text.Trim();
        dto.State = int.Parse(rblState.SelectedValue);

        string RoleKey = string.Empty;
        string RoleWebSite = string.Empty;
        string[] checkbox_name = Request.Form.GetValues("cbmenu");
        if (checkbox_name != null && checkbox_name.Length > 0)
        {
            for (int i = 0; i < checkbox_name.Length; i++)
            {
                if (checkbox_name[i] != "")
                {
                    string[] checkbox_name_value = checkbox_name[i].Split('|');
                    string[] checkbox_name_value_sub = checkbox_name_value[0].Split(',');
                    for (int j = 0; j < checkbox_name_value_sub.Length; j++)
                    {
                        if (RoleKey.IndexOf(checkbox_name_value_sub[j] + ",") <= -1)
                        {
                            RoleKey += checkbox_name_value_sub[j] + ",";
                        }
                    }
                    if (RoleWebSite.IndexOf(checkbox_name_value[1] + ",") <= -1)
                    {
                        RoleWebSite += checkbox_name_value[1] + ",";
                    }
                }
            }

            dto.RoleKey = RoleKey.Trim(',');
            dto.WebSiteIDStr = RoleWebSite.Trim(',');
        }
        else
        {
            dto.RoleKey = "";
            dto.WebSiteIDStr = "";
        }


        if (id != 0)
        {
            BAdmin_Role.Update(dto);
        }
        else
        {
            BAdmin_Role.Add(dto);
        }

        MessageBox.ShowRedirect(this, "信息保存成功！", "Column/Admin_Role/List.aspx?" + StringHelper.DelUrlParameter("ID"));
    }
}