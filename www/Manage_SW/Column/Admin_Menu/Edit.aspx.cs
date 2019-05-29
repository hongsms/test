using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using WebSite.BLL;
using WebSite.Common;
using System.Data;
using WebSite.Model;

public partial class Manage_SW_Column_Admin_Menu_Edit : AdminRoot
{
    protected int id = DNTRequest.GetQueryInt("ID", 0);
    protected int ParentID = DNTRequest.GetQueryInt("ParentID", 0);
    protected string Model = DNTRequest.GetQueryString("Model");
    Bll_AdminMenu BAdmin_Menu = new Bll_AdminMenu();
    protected int hidShow = DNTRequest.GetQueryInt("hidShow", 0);
    public string strshow = "style=\" display:none\"";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindMenu();
            BindWebSite();
            DdlstTypeNameBind();
            Bind();

            if (hidShow != 0)
            {
                strshow = "";
            }
        }
    }

     private void DdlstTypeNameBind()
    {
        WebSite.BLL.Bll_BaseType BBaseType = new WebSite.BLL.Bll_BaseType();

        string strWhere = " WebSiteID=" + AdminManage.WebSiteID + " ";

        ddlstTypeName.Items.Clear();
        ListItem li = new ListItem("一级栏目", "0");
        ddlstTypeName.Items.Add(li);

        DataSet ds = BBaseType.GetList(strWhere);
        if (ds != null && ds.Tables.Count > 0)
        {
            DataTable dt = ds.Tables[0];
            GetTree(ddlstTypeName, dt, 0, 0);
        }
    }



    private void BindMenu()
    {
        ddlMenu.Items.Clear();
        ListItem li = new ListItem("无父级栏目", "0");
        ddlMenu.Items.Add(li);
        DataSet ds = BAdmin_Menu.GetList(" WebSiteID=" + AdminManage.WebSiteID + " ");
        if (ds != null && ds.Tables.Count > 0)
        {
            DataTable dt = ds.Tables[0];
            GetTree(ddlMenu, dt, 0, 0);
        }
    }

    private void GetTree(DropDownList ddl, DataTable dt, int ColumnID, int ParentID)
    {
        DataRow[] dr = dt.Select(" ParentID=" + ParentID, " OrderBy asc ");
        string txtstr = string.Empty;
        for (int i = 1; i < ColumnID; i++)
        {
            txtstr += "　";
        }
        if (ColumnID != 0)
        {
            txtstr += "　├ ";
        }
        for (int i = 0; i < dr.Length; i++)
        {
            ListItem li = new ListItem(txtstr + dr[i]["Title"].ToString(), dr[i]["ID"].ToString());
            ddl.Items.Add(li);
            GetTree(ddl, dt, ColumnID + 1, int.Parse(dr[i]["ID"].ToString()));
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
        ddlMenu.SelectedValue = ParentID.ToString();
        ddlWebSite.SelectedValue = AdminManage.WebSiteID.ToString();
        Mod_AdminMenu dto = new Mod_AdminMenu();
        if (id != 0)
        {
            dto = BAdmin_Menu.GetModel(id);
            if (dto != null && dto.WebSiteID == AdminManage.WebSiteID)
            {
                ddlMenu.SelectedValue = dto.ParentID.ToString();
                txtTitle.Text = dto.Title;
                txtOrderBy.Text = dto.OrderBy.ToString();
                rblState.SelectedValue = dto.State.ToString();
                rblIsCopy.SelectedValue = dto.IsCopy.ToString();
                
                txtUrl.Text = dto.Url;
                ddlWebSite.SelectedValue = dto.WebSiteID.ToString();

                FunctionModel.Text = dto.FunctionModel;
                ddlstTypeName.SelectedValue = dto.TypeName;
                ddlWebSiteManage.SelectedValue = dto.WebSiteManage;

                SetChecked(this.cblShow, dto.Attributes.ToString(), ",");
            }
            else
            {
                MessageBox.ShowRedirect(this, "信息已删除或不存在！", "Column/Admin_Menu/List.aspx?" + StringHelper.DelUrlParameter("ID"));
            }
        }
    }

    protected void btnEdit_Click(object sender, EventArgs e)
    {
        if (txtTitle.Text.Trim() == "")
        {
            MessageBox.Show(this, "请填写完整信息再提交保存");
            return;
        }
        Mod_AdminMenu dto = new Mod_AdminMenu();
        string ParentIDStr = string.Empty;
        string IDPathStr = string.Empty;
        string strtype = string.Empty;
        if (id != 0)
        {
            dto = BAdmin_Menu.GetModel(id);
            strtype = dto.TypeName;
            ParentIDStr = dto.ParentID.ToString();
            IDPathStr = dto.IDPath;
        }
        dto.Title = txtTitle.Text.Trim();
        dto.ParentID = int.Parse(ddlMenu.SelectedValue);
        dto.State = int.Parse(rblState.SelectedValue);
        dto.IsCopy = int.Parse(rblIsCopy.SelectedValue);
     
        dto.Url = txtUrl.Text.Trim();
        dto.OrderBy = int.Parse(txtOrderBy.Text.Trim());
        dto.WebSiteID = int.Parse(ddlWebSite.SelectedValue);

        dto.FunctionModel = FunctionModel.Text;
        dto.TypeName = ddlstTypeName.SelectedValue;
        dto.WebSiteManage = ddlWebSiteManage.SelectedValue;
        dto.Attributes = GetChecked(this.cblShow, ",");

        string parameter = string.Empty;
        for (int i = 0; i < cblShow.Items.Count; i++)
        {
            if (cblShow.Items[i].Selected)
            {
                parameter +="&"+cblShow.Items[i].Value+"=1";
            }
        }

        if (id != 0)
        {
            string strHref = string.Empty;
            switch (ddlWebSiteManage.SelectedValue)
            {
                case "Type": //分类管理
                    strHref = "Column/BaseType/List.aspx?Model={0}&IsAdd=1" + parameter;
                    dto.Url = string.Format(strHref, FunctionModel.Text);
                    break;
                case "About"://单篇文章
                    if (strtype != dto.TypeName)
                    {
                        strHref = "Column/About/Edit.aspx?ID={1}" + parameter;
                        WebSite.BLL.Bll_BaseType BBaseType = new WebSite.BLL.Bll_BaseType();
                        WebSite.BLL.Bll_Information BInformation = new WebSite.BLL.Bll_Information();
                        WebSite.Model.Mod_Information MInformation = new WebSite.Model.Mod_Information();
                        WebSite.Model.Mod_BaseType MBaseType = new WebSite.Model.Mod_BaseType();                    

                        MBaseType = BBaseType.GetModel(string.Format("ID={0} AND WebSiteID={1}", this.ddlstTypeName.SelectedValue, AdminManage.WebSiteID));

                        MInformation.Title = MBaseType.Title;
                        MInformation.TypeID = int.Parse(ddlstTypeName.SelectedItem.Value);
                        MInformation.State = 1;
                        MInformation.Content1 = MBaseType.Title;
                        MInformation.Model = MBaseType.Model.ToString();
                        MInformation.WebSiteID = AdminManage.WebSiteID;
                        MInformation.AddDate = DateTime.Now;
                        int flag = BInformation.Add(MInformation);
                        MInformation.ID = flag;
                        BInformation.Update(MInformation);
                        dto.Url = string.Format(strHref, MInformation.Model, flag);
                    }
                    else
                    {
                        dto.Url = dto.Url.Split('&')[0] + parameter;

                    }
                    break;
                case "News"://文字新闻
                    strHref = "Column/Information/List.aspx?Model={0}&managetype={1}" + parameter;
                    dto.Url = string.Format(strHref, FunctionModel.Text, ddlstTypeName.SelectedValue);
                    break;
                case "Link": //友情链接
                    strHref = "Column/Link/List.aspx?Model={0}" + parameter;
                    dto.Url = string.Format(strHref, FunctionModel.Text);
                    break;
                    
            }
            BAdmin_Menu.Update(dto,true);
        }
        else
        {
            string strHref = string.Empty;
            switch (ddlWebSiteManage.SelectedValue)
            {
                case "Type": //分类管理
                    strHref = "Column/BaseType/List.aspx?Model={0}&IsAdd=1" + parameter;
                    dto.Url = string.Format(strHref, FunctionModel.Text);
                    break;
                case "About"://单篇文章
                    strHref = "Column/About/Edit.aspx?ID={1}" + parameter;
                    WebSite.BLL.Bll_BaseType BBaseType = new WebSite.BLL.Bll_BaseType();
                    WebSite.BLL.Bll_Information BInformation = new WebSite.BLL.Bll_Information();
                    WebSite.Model.Mod_Information MInformation = new WebSite.Model.Mod_Information();
                    WebSite.Model.Mod_BaseType MBaseType = new WebSite.Model.Mod_BaseType();
           
                    MBaseType = BBaseType.GetModel(string.Format("ID={0} AND WebSiteID={1}", this.ddlstTypeName.SelectedValue, AdminManage.WebSiteID));
                    MInformation.Title = MBaseType.Title;
                    MInformation.TypeID = int.Parse(ddlstTypeName.SelectedItem.Value);
                    MInformation.State = 1;
                    MInformation.Content1 = MBaseType.Title;
                    MInformation.Model = MBaseType.Model.ToString();
                    MInformation.WebSiteID = AdminManage.WebSiteID;
                    MInformation.AddDate = DateTime.Now;
                    int flag = BInformation.Add(MInformation);
                     MInformation.ID = flag;
                     BInformation.Update(MInformation);
                    dto.Url = string.Format(strHref, MInformation.Model, flag);
                    break;
                case "News"://文字新闻
                    strHref = "Column/Information/List.aspx?Model={0}&managetype={1}" + parameter;
                    dto.Url = string.Format(strHref, FunctionModel.Text, ddlstTypeName.SelectedValue);
                    break;
                case "Link": //友情链接
                    strHref = "Column/Link/List.aspx?Model={0}" + parameter;
                    dto.Url = string.Format(strHref, FunctionModel.Text);
                    break;
            }

            BAdmin_Menu.Add(dto,true);

        }

        MessageBox.ShowRedirect(this, "信息（" + txtTitle.Text.Trim() + "）保存成功！", "Column/Admin_Menu/List.aspx?" + StringHelper.DelUrlParameter("ID"));
    }


    /// <summary>
    /// 初始化CheckBoxList中哪些是选中了的         /// </summary>
    /// <param name="checkList">CheckBoxList</param>
    /// <param name="selval">选中了的值串例如："0,1,1,2,1"</param>
    /// <param name="separator">值串中使用的分割符例如"0,1,1,2,1"中的逗号</param>
    public static string SetChecked(CheckBoxList checkList, string selval, string separator)
    {
        selval = separator + selval + separator;        //例如："0,1,1,2,1"->",0,1,1,2,1,"
        for (int i = 0; i < checkList.Items.Count; i++)
        {
            checkList.Items[i].Selected = false;
            string val = separator + checkList.Items[i].Value + separator;
            if (selval.IndexOf(val) != -1)
            {
                checkList.Items[i].Selected = true;
                selval = selval.Replace(val, separator);        //然后从原来的值串中删除已经选中了的
                if (selval == separator)        //selval的最后一项也被选中的话，此时经过Replace后，只会剩下一个分隔符
                {
                    selval += separator;        //添加一个分隔符
                }
            }
        }
        selval = selval.Substring(1, selval.Length - 2);        //除去前后加的分割符号
        return selval;
    }

    /// <summary>
    /// 得到CheckBoxList中选中了的值
    /// </summary>
    /// <param name="checkList">CheckBoxList</param>
    /// <param name="separator">分割符号</param>
    /// <returns></returns>
    public static string GetChecked(CheckBoxList checkList, string separator)
    {
        string selval = "";
        for (int i = 0; i < checkList.Items.Count; i++)
        {
            if (checkList.Items[i].Selected)
            {
                selval += checkList.Items[i].Value + separator;
            }
        }
        return selval;
    }
}