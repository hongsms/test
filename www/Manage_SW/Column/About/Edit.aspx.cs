using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using WebSite.BLL;
using WebSite.Common;
using System.Data;
using WebSite.Model;
using WebSite.DAL;

public partial class Manage_SW_Column_About_Edit : AdminRoot
{
    protected int id = DNTRequest.GetQueryInt("ID", 0);
    protected string Model = DNTRequest.GetQueryString("Model");
    Bll_Information BInformation = new Bll_Information();
    protected int IsInfo = DNTRequest.GetQueryInt("IsInfo", 0);
    protected int IsImage = DNTRequest.GetQueryInt("IsImage", 0);
    protected string Size = DNTRequest.GetQueryString("Size");
    protected int IsVideo = DNTRequest.GetQueryInt("IsVideo", 0);

    protected int IsAlbum = DNTRequest.GetQueryInt("IsAlbum", 0);
    Bll_PicList BPicList = new Bll_PicList();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Bind();
        }
    }

    private void Bind()
    {
        Mod_Information dto = new Mod_Information();
        if (id != 0)
        {
            dto = BInformation.GetModel(string.Format("ID={0} AND WebSiteID={1}", id, AdminManage.WebSiteID));
            if (dto != null)
            {
                txtTitle.Text = dto.Title;
                txtContent.Text = dto.Content1;
                txtImage.Text = dto.Image;
                txtVideo.Text = dto.FileURL;
                txtIntroduction.Text = dto.Introduction;

                //绑定图片集
                rptPicList.DataSource = BPicList.GetList(0, string.Format("ProductID={0} AND WebSiteID={1} AND Model='XWXC' and State=1 ", id, AdminManage.WebSiteID), " OrderBy asc,ID asc ");
                rptPicList.DataBind();
            }
            else
            {
                MessageBox.ShowRedirect(this, "信息已删除或不存在！", "Column/Default/Index.aspx");
            }
        }
    }

    protected void btnEdit_Click(object sender, EventArgs e)
    {
        if (txtTitle.Text.Trim() == "")
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
        dto.Content1 = txtContent.Text.Trim();

        //dto.SubTitle = txtSubTitle.Text.Trim();

        dto.WebSiteID = AdminManage.WebSiteID;
        dto.Model = Model;
        dto.ModifyDate = DateTime.Now;
        dto.Image = txtImage.Text.Trim();
        dto.FileURL = txtVideo.Text.Trim();
        dto.Introduction = txtIntroduction.Text;
        if (id == 0)
        {
            dto.UserID = AdminManage.AdminID;
            dto.UID=BInformation.Add(dto);
            dto.ID = dto.UID;
           
        }
        BInformation.Update(dto);

        if (dto.ID != 0)
        {
            Dal_PicList DPicList = new Dal_PicList();
            DPicList.OperateList(SetPicList(dto), dto.ID, AdminManage.WebSiteID, false);
        }

        MessageBox.ShowRedirect(this, "信息保存成功！");

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
}