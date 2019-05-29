using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using WebSite.BLL;
using WebSite.Common;
using System.Data;
using WebSite.Model;
using WebSite.DAL;

public partial class Manage_SW_Column_BaseType_Edit : AdminRoot
{
    protected int id = DNTRequest.GetQueryInt("ID", 0);
    protected int ParentID = DNTRequest.GetQueryInt("ParentID", 0);
    protected string Model = DNTRequest.GetQueryString("Model");
    Bll_BaseType BBaseType = new Bll_BaseType();

    protected int IsModel = DNTRequest.GetQueryInt("IsModel", 0);
    protected int IsSubTitle = DNTRequest.GetQueryInt("IsSubTitle", 0);
    protected int IsLink = DNTRequest.GetQueryInt("IsLink", 0);
    protected int IsInfo = DNTRequest.GetQueryInt("IsInfo", 0);
    protected int IsContent = DNTRequest.GetQueryInt("IsContent", 0);
    protected int IsChild = DNTRequest.GetQueryInt("IsChild", 0);
    protected int IsImage = DNTRequest.GetQueryInt("IsImage", 0);
    protected string Size = DNTRequest.GetQueryStringDecode("Size");
    protected int IsAlbum = DNTRequest.GetQueryInt("IsAlbum", 0);
    protected int IsVideo = DNTRequest.GetQueryInt("IsVideo", 0);
    protected int IsRecommend = DNTRequest.GetQueryInt("IsRecommend", 0);
    protected int IsUser = DNTRequest.GetQueryInt("IsRecommend", 0);

    Bll_PicList BPicList = new Bll_PicList();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindBaseType();
            Bind();
        }
    }
    private void BindBaseType()
    {
        ddlBaseType.Items.Clear();
        ListItem li = new ListItem("一级分类", "0");
        ddlBaseType.Items.Add(li);
        OperateHelper.BindBaseType(ddlBaseType, 0, " Model='" + Model + "' ", "cn");
    }

    private void Bind()
    {
        ddlBaseType.SelectedValue = ParentID.ToString();
        Mod_BaseType dto = new Mod_BaseType();
        if (id != 0)
        {
            dto = BBaseType.GetModel(string.Format("ID={0} AND WebSiteID={1}", id, AdminManage.WebSiteID));
            if (dto != null)
            {
                ddlBaseType.SelectedValue = dto.ParentID.ToString();
                txtModel.Text = dto.Model;
                txtTitle.Text = dto.Title;
                txtOrderBy.Text = dto.OrderBy.ToString();
                rblState.SelectedValue = dto.State.ToString();
                rblIsAdmin.SelectedValue = dto.IsAdmin.ToString();
                rblDisplayMode.SelectedValue = dto.DisplayMode.ToString();

                rblIsRecommend.SelectedValue = dto.IsRecommend.ToString();
                rblIsUser.SelectedValue = dto.IsUser.ToString();

                txtIncludeType.Text = dto.IncludeType;
                txtLink.Text = dto.Link;
                txtInfo.Text = dto.Info;
                txtContent.Text = dto.Content;
                txtImage.Text = dto.Image;

                txtFileURl.Text = dto.FileURl;

                rblIsAdmin.SelectedValue = dto.IsAdmin.ToString();





                //绑定图片集
                rptPicList.DataSource = BPicList.GetList(0, string.Format("ProductID={0} AND WebSiteID={1} AND Model='FLXC' and State=1 ", id, AdminManage.WebSiteID), " OrderBy asc,ID asc ");
                rptPicList.DataBind();
            }
            else
            {
                MessageBox.ShowRedirect(this, "信息已删除或不存在！", "Column/BaseType/List.aspx?" + StringHelper.DelUrlParameter("ID"));
            }
        }
        else
        {
            txtModel.Text = Model;
        }
    }


    //保存图片集
    private List<Mod_PicList> SetPicList()
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


    protected void btnEdit_Click(object sender, EventArgs e)
    {
        if (txtTitle.Text.Trim() == "")
        {
            MessageBox.Show(this, "带*的是必须项或是必选项，请填写完整再提交保存！");
            return;
        }


        if (id != 0)
        {

            WebSite.Model.Mod_BaseType dto = new WebSite.Model.Mod_BaseType();

            dto = BBaseType.GetModel(string.Format("ID={0} AND WebSiteID={1}", id, AdminManage.WebSiteID));
            dto.ParentID = int.Parse(ddlBaseType.SelectedValue);
            dto.Model = txtModel.Text.Trim();
            dto.Title = txtTitle.Text.Trim();
            dto.Image = txtImage.Text.Trim();
            dto.FileURl = txtFileURl.Text.Trim();


            dto.OrderBy = int.Parse(txtOrderBy.Text.Trim());
            dto.State = int.Parse(rblState.SelectedValue);
            dto.IsAdmin = int.Parse(rblIsAdmin.SelectedValue);
            dto.DisplayMode = int.Parse(rblDisplayMode.SelectedValue);
            dto.IsRecommend = int.Parse(rblIsRecommend.SelectedValue);
            dto.IsUser = int.Parse(rblIsUser.SelectedValue);


            dto.IsUser = int.Parse(rblIsUser.SelectedValue);

            dto.IncludeType = txtIncludeType.Text.Trim();
            dto.Link = txtLink.Text.Trim();
            dto.Info = txtInfo.Text.Trim();
            dto.Content = txtContent.Text.Trim();
            dto.WebSiteID = AdminManage.WebSiteID;

            BBaseType.Update(dto, true);
            if (dto.ParentID == 0)
            {
                dto.IDPath = dto.ID.ToString();
            }
            else
            {
                dto.IDPath = BBaseType.GetModel(string.Format("ID={0} AND WebSiteID={1}", dto.ParentID, AdminManage.WebSiteID)).IDPath + "," + dto.ID;
            }
            BBaseType.Update(dto, true);

            //操作图片集
            Dal_PicList DPicList = new Dal_PicList();
            DPicList.OperateList(SetPicList(), dto.ID, AdminManage.WebSiteID, false);

        }
        else
        {
            WebSite.Model.Mod_BaseType dto = new WebSite.Model.Mod_BaseType();
            dto.Title = txtTitle.Text.Trim();
            dto.Image = txtImage.Text.Trim();
            dto.FileURl = txtFileURl.Text.Trim();

            //添加新数据
            string[] titleArr = dto.Title.Split("|".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
            for (int i = 0; i < titleArr.Length; i++)
            {
                if (titleArr[i] == "")
                {
                    continue;
                }
                dto.Title = titleArr[i];
                dto.IncludeType = txtIncludeType.Text.Trim();
                dto.Link = txtLink.Text.Trim();
                dto.Info = txtInfo.Text.Trim();
                dto.ParentID = int.Parse(ddlBaseType.SelectedValue);
                dto.State = int.Parse(rblState.SelectedValue);
                dto.IsAdmin = int.Parse(rblIsAdmin.SelectedValue);
                dto.DisplayMode = int.Parse(rblDisplayMode.SelectedValue);
                dto.IsRecommend = int.Parse(rblIsRecommend.SelectedValue);
                dto.IsUser = int.Parse(rblIsUser.SelectedValue);
                dto.Content = txtContent.Text.Trim();
                dto.OrderBy = int.Parse(txtOrderBy.Text.Trim());
                dto.WebSiteID = AdminManage.WebSiteID;
                dto.Model = txtModel.Text.Trim();
                dto.AddDate = DateTime.Now;
                dto.UID = BBaseType.Add(dto, true);
                dto.ID = dto.UID;
                if (dto.ParentID == 0)
                {
                    dto.IDPath = dto.ID.ToString();
                }
                else
                {
                    dto.IDPath = BBaseType.GetModel(string.Format("ID={0} AND WebSiteID={1}", dto.ParentID, AdminManage.WebSiteID)).IDPath + "," + dto.ID;
                }
                BBaseType.Update(dto, true);

                //操作图片集
                Dal_PicList DPicList = new Dal_PicList();
                DPicList.OperateList(SetPicList(), dto.ID, AdminManage.WebSiteID, false);
            }


        }



        MessageBox.ShowRedirect(this, "信息（" + txtTitle.Text.Trim() + "）保存成功！", "Column/BaseType/List.aspx?" + StringHelper.DelUrlParameter("ID"));
    }
}