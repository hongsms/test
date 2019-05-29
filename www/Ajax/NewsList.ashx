<%@ WebHandler Language="C#" Class="NewsList" %>

using System;
using System.Web;
using WebSite.BLL;

public class NewsList : IHttpHandler
{
 //string strWhere = " IsOK='1' and  IDPath like '%" + TypeId + "%'";


    public void ProcessRequest(HttpContext context)
    {
        context.Response.ContentType = "text/plain";
        context.Response.Buffer = true;
        context.Response.ExpiresAbsolute = DateTime.Now.AddDays(-1);
        context.Response.AddHeader("pragma", "no-cache");
        context.Response.AddHeader("cache-control", "");
        context.Response.CacheControl = "no-cache";

        int TypeId = WebSite.Common.DNTRequest.GetQueryInt("TypeId", 10647);
        int PageSize = WebSite.Common.DNTRequest.GetQueryInt("PageSize", 8); ;
        int pageIndex = WebSite.Common.DNTRequest.GetQueryInt("page", 1);
        int WordNum = WebSite.Common.DNTRequest.GetQueryInt("WordNum", 60);
        WebSite.BLL.Bll_Information BInformation = new WebSite.BLL.Bll_Information();
        string strWhere = " State='1' and  IDPath like '%" + TypeId + "%'";
        //string strWhere = " State='1' ";
        System.Collections.Generic.List<NewsInfo> LNews = new System.Collections.Generic.List<NewsInfo>();
        
        if (WebSite.Common.DNTRequest.GetQueryStringStringDecode("keys").Trim() != "")
        {
            string Titel = WebSite.Common.DNTRequest.GetQueryStringStringDecode("keys").Trim();
            
           
            strWhere += " and title like '%" + WebSite.Common.StringHelper.CleanDangerSQL(Titel) + "%' ";
        }
        
        int TotleNum = 0;
        string Order = "OrderBy DESC,AddDate DESC";
        System.Collections.Generic.List<WebSite.Model.Mod_VW_Information>  ModelInfo = new WebSite.BLL.Bll_VW_Information().GetModelListByPage(strWhere, Order, pageIndex, PageSize, out TotleNum);
        
        
     
        for (int i = 0; i < ModelInfo.Count; i++)
        {
            NewsInfo tNews = new NewsInfo();
            tNews.ID = ModelInfo[i].ID;
            tNews.Title = ModelInfo[i].Title;


            tNews.Image = ModelInfo[i].Image.Length > 0 ? ModelInfo[i].Image : "/images/img_focus.jpg";
            
            tNews.BrowseCount = ModelInfo[i].BrowseCount;          
            tNews.AddDate = ModelInfo[i].AddDate.ToString("yyyy-MM-dd");
            tNews.Introduction = WebSite.Common.StringHelper.SubstringNoTitle(ModelInfo[i].Introduction, 110,"");
            tNews.Content = WebSite.Common.StringHelper.SubstringNoTitle(WebSite.Common.StringHelper.ClearHtml(ModelInfo[i].Content1), WordNum, "");

            LNews.Add(tNews);
        }

        //context.Response.Write("[{\"CanComment\":\"1\",\"TitleColor\":\"\",\"Link\":\"\"}]");
        DateInfo dinfo = new DateInfo();
        dinfo.page = pageIndex + 1;
        dinfo.expiredPage = pageIndex;
        dinfo.items = LNews;


        ResultInfo info = new ResultInfo();
        info.success = 1;
        info.total = TotleNum;
        info.data = dinfo;

        string Content =Newtonsoft.Json.JsonConvert.SerializeObject(info);
        context.Response.Write(Content);
    }




    public class ResultInfo
    {
        public int success { get; set; }
        public int total { get; set; }
        public DateInfo data { get; set; }
    }

    public class DateInfo
    {
        public int page { get; set; }
        public int expiredPage { get; set; }
        public System.Collections.Generic.List<NewsInfo> items { get; set; }
    }



    public class NewsInfo
    {
        #region Model
        private int _id;
        private string _title;
        private string _content;
        private string _image;

        private string _citationtitle;
        private int _commentcount;
        private int _browsecount;

        private string _source;

        private string _introduction;

        private string _adddate;

        /// <summary>
        /// 标题
        /// </summary>
        public string AddDate
        {
            set { _adddate = value; }
            get { return _adddate; }
        }
        
        /// <summary>
        /// 标题
        /// </summary>
        public string Introduction
        {
            set { _introduction = value; }
            get { return _introduction; }
        }
        /// <summary>
        /// 标题
        /// </summary>
        public string Source
        {
            set { _source = value; }
            get { return _source; }
        }
        /// <summary>
        /// 标题
        /// </summary>
        public string CitationTitle
        {
            set { _citationtitle = value; }
            get { return _citationtitle; }
        }
        /// <summary>
        /// 标题
        /// </summary>
        public int CommentCount
        {
            set { _commentcount = value; }
            get { return _commentcount; }
        }
        /// <summary>
        /// 标题
        /// </summary>
        public int BrowseCount
        {
            set { _browsecount = value; }
            get { return _browsecount; }
        }



        /// <summary>
        /// 编号
        /// </summary>
        public int ID
        {
            set { _id = value; }
            get { return _id; }
        }

        /// <summary>
        /// 标题
        /// </summary>
        public string Title
        {
            set { _title = value; }
            get { return _title; }
        }
        /// <summary>
        /// 内容
        /// </summary>
        public string Content
        {
            set { _content = value; }
            get { return _content; }
        }
        /// <summary>
        /// 文章图片
        /// </summary>
        public string Image
        {
            set { _image = value; }
            get { return _image; }
        }

        #endregion Model
    }
    public bool IsReusable
    {
        get
        {
            return false;
        }
    }

}