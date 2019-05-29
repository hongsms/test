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
    Bll_Apply BMessage = new Bll_Apply();
    protected Mod_Apply dto = new Mod_Apply();

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
                dto.State = 1;
                BMessage.Update(dto);

                JOBTITLE.Text = dto.JOBTITLE;

                ZSXM.Text = dto.ZSXM;
                XB.Text = dto.XB;
                ZGXL.Text = dto.ZGXL;
                HYZK.Text = dto.HYZK;
                CSRQ.Text = dto.CSRQ;
                YXDZ.Text = dto.YXDZ;
                JG.Text = dto.JG;
                SJHM.Text = dto.SJHM;
                GZJL.Text = dto.GZJL;
                ZYJN.Text = dto.ZYJN;

                txtAddDate.Text = dto.AddDate.ToString();

             
            }
            else
            {
                MessageBox.ShowRedirect(this, "信息已删除或不存在！", "Column/Apply/List.aspx?" + StringHelper.DelUrlParameter("ID"));
            }
        }
    }
    //protected void btnEdit_Click(object sender, EventArgs e)
    //{
    //    dto = BMessage.GetModel(id);
    //    dto.State = int.Parse(rblState.SelectedValue);
    //    dto.ReplyContent = txtReply.Text.Trim();
    //    dto.ReplyDate = DateTime.Now;
    //    BMessage.Update(dto);
    //    MessageBox.ShowRedirect(this, "信息保存成功！", "Column/Message/List.aspx?" + StringHelper.DelUrlParameter("ID"));

    //}
}