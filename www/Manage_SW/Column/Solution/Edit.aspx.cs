using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using WebSite.BLL;
using WebSite.Common;
using System.Data;
using WebSite.Model;
using WebSite.DAL;

public partial class Manage_SW_Column_Information_Edit : AdminRoot
{
    protected int id = DNTRequest.GetQueryInt("ID", 0);
    protected string Model = DNTRequest.GetQueryString("Model");
    Bll_Information BInformation = new Bll_Information();

    protected int IsType = DNTRequest.GetQueryInt("IsType", 0);
    protected int IsAddDate = DNTRequest.GetQueryInt("IsAddDate", 0);
    protected int IsState = DNTRequest.GetQueryInt("IsState", 0);
    protected int IsImage = DNTRequest.GetQueryInt("IsImage", 0);
    protected string Size = DNTRequest.GetQueryString("Size");
    protected int IsVideo = DNTRequest.GetQueryInt("IsVideo", 0);
    protected int IsSource = DNTRequest.GetQueryInt("IsSource", 0);
    protected int IsBrowseCount = DNTRequest.GetQueryInt("IsBrowseCount", 0);
    protected int IsInfo = DNTRequest.GetQueryInt("IsInfo", 0);
    protected int IsTags = DNTRequest.GetQueryInt("IsTags", 0);
    protected int IsSubTitle = DNTRequest.GetQueryInt("IsSubTitle", 0);
    protected int IsContent = DNTRequest.GetQueryInt("IsContent", 0);
    protected int managetype = DNTRequest.GetQueryInt("managetype", 0);
    protected int IsAuthor = DNTRequest.GetQueryInt("IsAuthor", 0);
  
    protected int IsAlbum = DNTRequest.GetQueryInt("IsAlbum", 0);
    protected int IsFiles = DNTRequest.GetQueryInt("IsFiles", 0);

    protected int IsLink = DNTRequest.GetQueryInt("IsLink", 0);

    Bll_PicList BPicList = new Bll_PicList();
    Bll_Attr BAttr = new Bll_Attr();
    public string gjhtml = string.Empty;
    Bll_BaseType BBaseType = new Bll_BaseType();
    public DataTable gjdt = new DataTable();
    protected void Page_Load(object sender, EventArgs e)
    {
        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        string WhereStr = " WebSiteID=" + AdminManage.WebSiteID + " AND Model='GJ' ";
        gjdt = BBaseType.GetList(WhereStr).Tables[0];
        //一级地区
        sb.Append("<select name=\"AttrValue2\" class=\"form-control\" style=\"width: 100%;\">");
        for (int i = 0; i < gjdt.Rows.Count; i++)
        {
            sb.Append("<option value=\"" + gjdt.Rows[i]["ID"].ToString() + "\" >" + gjdt.Rows[i]["Title"].ToString() + "</option>");
        }
        sb.Append("</select>");
        gjhtml = sb.ToString();

        if (!IsPostBack)
        {

            BindType();
            BindProductBrand();
            Bind();
        }
    }

    private void BindProductBrand()
    {
        lbTagsBrand.Items.Clear();


        WebSite.BLL.Bll_VW_Information BInfo = new WebSite.BLL.Bll_VW_Information();
        string strWhere = " WebSiteID=" + PageCommon.LanguageID + " and  IDPath like '%10919%'";


        List<WebSite.Model.Mod_VW_Information> InformationList = BInfo.GetModelList(int.MaxValue, strWhere + " ORDER BY OrderBy desc,AddDate desc");

        foreach (var item in InformationList)
        {
            ListItem li = new ListItem(item.Title, item.ID.ToString());
            lbTagsBrand.Items.Add(li);
        }

        lbTagsBrand.Attributes.Add("title ", "--绑定商品--");
    }
    public string GetGJhtml(string selvalue)
    {
        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        sb.Append("<select name=\"AttrValue2\" class=\"form-control\" style=\"width: 100%;\">");
        for (int i = 0; i < gjdt.Rows.Count; i++)
        {
            if (selvalue == gjdt.Rows[i]["ID"].ToString())
            {
                sb.Append("<option value=\"" + gjdt.Rows[i]["ID"].ToString() + "\" selected=\"selected\">" + gjdt.Rows[i]["Title"].ToString() + "</option>");
            }
            else
            {
                sb.Append("<option value=\"" + gjdt.Rows[i]["ID"].ToString() + "\" >" + gjdt.Rows[i]["Title"].ToString() + "</option>");
            }
        }
        sb.Append("</select>");
        return sb.ToString();
    }
    private void BindType()
    {
        ddlBaseType.Items.Clear();
        ListItem li = new ListItem();

        string strwhere = " IsAdmin=1";

        if (Model != "")
        {
            strwhere += " and Model='" + Model + "' ";
        }
        if (managetype != 0)
        {
            strwhere += " and IDPath like '%" + managetype + "%'";
        }



        OperateHelper.BindBaseType(ddlBaseType, managetype, strwhere, "cn");
    }

    private void Bind()
    {
        if (IsType == 1)
        {
            ddlBaseType.Attributes.Add("datatype", "no0");
        }
        if (IsBrowseCount == 1)
        {
            txtBrowseCount.Attributes.Add("datatype", "n");
        }
        if (IsAddDate == 1)
        {
            txtAddDate.Attributes.Add("datatype", "datetime");
        }

        txtAddDate.Value = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
     
        Mod_Information dto = new Mod_Information();
        if (id != 0)
        {
            dto = BInformation.GetModel(string.Format("ID={0} AND WebSiteID={1}", id, AdminManage.WebSiteID));
            if (dto != null)
            {
                txtTitle.Text = dto.Title;
                txtSubTitle.Text = dto.SubTitle;
                ddlBaseType.SelectedValue = dto.TypeID.ToString();



                txtImage.Text = dto.Image;
                txtVideo.Text = dto.FileURL;
                txtIntroduction.Text = dto.Introduction;
                txtContent1.Text = dto.Content1;

                txtContent2.Text = dto.Content2;
                txtOrderBy.Text = dto.OrderBy.ToString();

                foreach (ListItem item in cblShow.Items)
                {
                    switch (item.Value)
                    {
                        case "1":
                            item.Selected = (dto.State == 1);
                            break;
                        case "2":
                            item.Selected = (dto.IsTop == 1);
                            break;
                        case "3":
                            item.Selected = (dto.IsCommend == 1);
                            break;
                    }
                }

                txtOrderBy.Text = dto.OrderBy.ToString();
                txtBrowseCount.Text = dto.BrowseCount.ToString();
                txtAddDate.Value = DateTime.Parse(dto.AddDate.ToString()).ToString("yyyy-MM-dd HH:mm:ss");
             

                txtSource.Text = dto.Source;
                txtAuthor.Text = dto.Author;

                txtLink.Text = dto.Link;


                foreach (ListItem item in lbTagsBrand.Items)
                {
                    if (dto.Tags.IndexOf(item.Value) > -1)
                    {
                        item.Selected = true;
                    }
                }



              
                //绑定图片集
                rptPicList.DataSource = BPicList.GetList(0, string.Format("ProductID={0} AND WebSiteID={1} AND Model='XWXC' and State=1 ", id, AdminManage.WebSiteID), " OrderBy asc,ID asc ");
                rptPicList.DataBind();

                //绑定文件集
                rptFileList.DataSource = BPicList.GetList(0, string.Format("ProductID={0} AND WebSiteID={1} AND Model='XTWJ' and State=1 ", id, AdminManage.WebSiteID), " OrderBy asc,ID asc ");
                rptFileList.DataBind();

                  //绑定文件集
                rptJFP.DataSource = BAttr.GetList(0, string.Format("InfoID={0}  AND Model='JFP' AND WebSiteID={1}  ", id, AdminManage.WebSiteID), " ID asc ");
                rptJFP.DataBind();

              
            }
            else
            {
                MessageBox.ShowRedirect(this, "信息已删除或不存在！", "Column/Solution/List.aspx?" + StringHelper.DelUrlParameter("ID"));
            }
        }
        else
        {
            int TypeID = DNTRequest.GetQueryInt("TypeID", 0);
            ddlBaseType.SelectedValue = TypeID.ToString();

        }
    }

    //保存图片集
    private List<Mod_PicList> SetPicList(Mod_Information MProduct)
    {
        string[] albumArr = Request.Form.GetValues("hid_photo_name");
        string[] remarkArr = Request.Form.GetValues("hid_photo_remark");
        string[] modelArr = Request.Form.GetValues("hid_photo_model");

        string[] TitleArr = Request.Form.GetValues("hid_photo_Title");
        string[] ViceTitleArr = Request.Form.GetValues("hid_photo_ViceTitle");




        List<Mod_PicList> ls = new List<Mod_PicList>();
        if (albumArr != null)
        {

            for (int i = 0; i < albumArr.Length; i++)
            {
                string[] imgArr = albumArr[i].Split('|');
                int img_id = StringHelper.StrToInt(imgArr[0], 0);
                if (imgArr.Length == 3)
                {
                    ls.Add(new Mod_PicList { ID = img_id, Model = modelArr[i], Title = TitleArr[i], ViceTitle = ViceTitleArr[i], ProductID = id, OriginalUrl = imgArr[1], ThumbUrl = imgArr[2], Info = remarkArr[i], State = 1, IsDefault = 0, WebSiteID = AdminManage.WebSiteID });
                }
            }
        }
        return ls;
    }

    //保存图片集
    private List<Mod_Attr> SetAttrList(Mod_Information MProduct)
    {
        string[] AttrID = Request.Form.GetValues("AttrID");
        string[] hidModel = Request.Form.GetValues("hidModel");
        string[] AttrValue1 = Request.Form.GetValues("AttrValue1");
        string[] AttrValue2 = Request.Form.GetValues("AttrValue2");
        string[] AttrValue3 = Request.Form.GetValues("AttrValue3");
        string[] AttrValue4 = Request.Form.GetValues("AttrValue4");



        List<Mod_Attr> ls = new List<Mod_Attr>();
        if (AttrID != null)
        {

            for (int i = 0; i < AttrID.Length; i++)
            {

                ls.Add(new Mod_Attr { ID = StringHelper.StrToInt(AttrID[i], 0), Model = hidModel[i], AttrValue1 = AttrValue1[i], AttrValue2 = AttrValue2[i], InfoID = id, AttrValue3 = AttrValue3[i], AttrValue4 = AttrValue4[i], WebSiteID = AdminManage.WebSiteID });

            }
        }
        return ls;
    }

    protected void btnEdit_Click(object sender, EventArgs e)
    {
        if (txtTitle.Text.Trim() == "" || !StringHelper.IsNumberId(txtOrderBy.Text.Trim()) || !StringHelper.IsNumberId(txtBrowseCount.Text.Trim()))
        {
            MessageBox.Show(this, "请正确填写信息再提交保存！");
            return;
        }

        Mod_Information dto = new Mod_Information();

        if (id != 0)
        {
            dto = BInformation.GetModel(string.Format("ID={0} AND WebSiteID={1}", id, AdminManage.WebSiteID));
        }

        dto.Title = txtTitle.Text.Trim();
        dto.SubTitle = txtSubTitle.Text.Trim();
        dto.TypeID = int.Parse(ddlBaseType.SelectedValue);
        string tagsStr = string.Empty;




        dto.Image = txtImage.Text.Trim();
        dto.FileURL = txtVideo.Text.Trim();
        foreach (ListItem item in cblShow.Items)
        {
            switch (item.Value)
            {
                case "1":
                    dto.State = item.Selected ? 1 : 0;
                    break;
                case "2":
                    dto.IsTop = item.Selected ? 1 : 0;
                    break;
                case "3":
                    dto.IsCommend = item.Selected ? 1 : 0;
                    break;
            }
        }

        dto.Introduction = txtIntroduction.Text.Trim();
        dto.Content1 = txtContent1.Text.Trim();
          dto.Content2 = txtContent2.Text.Trim();
       
        dto.OrderBy = int.Parse(txtOrderBy.Text.Trim());
        dto.BrowseCount = int.Parse(txtBrowseCount.Text.Trim());
        dto.AddDate = DateTime.Parse(txtAddDate.Value.Trim());
       
        dto.WebSiteID = AdminManage.WebSiteID;
        dto.Model = Model;
        dto.ModifyDate = DateTime.Now;

        dto.Source = txtSource.Text.Trim();
        dto.Author = txtAuthor.Text.Trim();
        dto.Link = txtLink.Text.Trim();

        string Brands = string.Empty;
        foreach (ListItem item in lbTagsBrand.Items)
        {
            if (item.Selected)
            {
                Brands += item.Value + ",";
            }
        }
        dto.Tags = Brands.Trim(',');


        for (int i = 0; i < 1; i++)
        {
        if (id == 0)
        {
            dto.UserID = AdminManage.AdminID;
            dto.UID = BInformation.Add(dto);
            dto.ID = dto.UID;


        }
        BInformation.Update(dto);

        if (dto.ID != 0)
        {
            Dal_PicList DPicList = new Dal_PicList();
            DPicList.OperateList(SetPicList(dto), dto.ID, AdminManage.WebSiteID, false);

            Dal_Attr DAttr = new Dal_Attr();
            DAttr.OperateList(SetAttrList(dto), dto.ID, AdminManage.WebSiteID, false);
        }

        }

        MessageBox.ShowRedirect(this, "信息保存成功！", "Column/Solution/List.aspx?" + StringHelper.DelUrlParameter("ID"));

    }
}