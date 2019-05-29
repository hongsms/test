using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
///PageHelper 的摘要说明
/// </summary>
public class PageHelper
{
    #region 后台分页的面码显示
    /// <summary>
    /// 后台分页的面码显示
    /// </summary>
    /// <param name="TotleNum">数据条数</param>
    /// <param name="NumPerPage">一页要显示的条数</param>
    /// <param name="Thepage">显示第几页</param>
    /// <returns></returns>
    public static string ManagePageStr(int TotleNum, int NumPerPage, int Thepage)
    {
        string FileUrl = HttpContext.Current.Request.Url.Query;
        if (!string.IsNullOrEmpty(FileUrl))
        {
            string UrlWhere = string.Empty;
            string str = FileUrl.Substring(1, FileUrl.Length - 1);
            string[] array = str.Split('&');
            for (int i = 0; i < array.Length; i++)
            {
                if ("page" != array[i].Split('=')[0])
                {
                    UrlWhere += array[i] + "&";
                }
            }

            FileUrl = "?" + UrlWhere;
        }
        else
        {
            FileUrl = "?";
        }

        string strTemp;
        int n;
        int p;
        int ii;
        if (Convert.ToInt32(TotleNum) <= Convert.ToInt32(NumPerPage))
            return "";

        if (Convert.ToInt32(TotleNum) % Convert.ToInt32(NumPerPage) == 0)
        {
            n = TotleNum / NumPerPage;
        }
        else
        {
            n = TotleNum / NumPerPage + 1;
        }
        strTemp = "<div class=\"bottom-page\"><div role=\"navigation\">";

        strTemp += "<ul class=\"pagination\"><li><span>显示第<input type=\"text\" style=\"width:35px;height: 17px;border: 1px solid #FFF;text-align: center;color: #337ab7;\" onchange=\"gotopage(this);\"  value=\"" + Thepage + "\">页/共 " + n + " 页</span></li><li><span>共" + TotleNum + "记录</span></li>";


        if (Convert.ToInt32(Thepage) - 1 % 10 == 0)
        {
            p = (Thepage - 1) / 10;
        }
        else
        {
            p = (Thepage - 1) / 10;
        }
        if (Convert.ToInt32(Thepage) < 2)
        {
            strTemp += "<li class=\"disabled\"><a href=\"javascript:void(0);\">首页</a></li><li class=\"disabled\"><a href=\"javascript:void(0);\" aria-label=\"Previous\">上一页</a></li>";
        }
        else
        {
            strTemp += "<li><a href=\"" + FileUrl + "page=1\">首页</a></li>";
            strTemp += "<li><a href=\"" + FileUrl + "page=" + (Thepage - 1) + "\" aria-label=\"Previous\">上一页</a></li>"; ;
        }
        int uming_i = 1;
        for (ii = p * 10 + 1; ii <= p * 10 + 10; ii++)
        {
            if (ii == Thepage)
            {
                strTemp += "<li class=\"active\"><a href=\"javascript:void(0);\">" + ii + "</a></li>";
            }
            else
            {
                strTemp += "<li><a href=\"" + FileUrl + "page=" + ii + "\">" + ii + "</a></li>";
            }
            if (ii == n)
            {
                break;
            }
            uming_i = uming_i + 1;
        }
        if (n - Convert.ToInt32(Thepage) < 1)
        {
            strTemp += "<li class=\"disabled\"><a href=\"javascript:void(0);\" aria-label=\"Next\">下一页</a></li><li class=\"disabled\"><a href=\"javascript:void(0);\">末页</a></li>";
        }
        else
        {
            strTemp += "<li><a href='" + FileUrl + "page=" + (Thepage + 1) + "'>下一页</a></li>";
            strTemp += "<li><a href='" + FileUrl + "page=" + n + "'>末页</a></li>";
        }
        strTemp += "</ul></div></div><script language=javascript>function gotopage(obj) {var num = $(obj).val();if (isNaN(num)) {parent.message('warning', '仅限于输入数字！'); $(obj).val(" + Thepage + "); }else {if (num>0&&num <= " + n + ") {window.location.href = '" + FileUrl + "page=' + num;} else { parent.message('warning', '超出总页数，请重新输入！'); $(obj).val(" + Thepage + "); }}}</script>";
        return (string)strTemp;
    }
    #endregion

    #region 前端简单分页显示
    /// <summary>
    /// 前端简单分页显示
    /// </summary>
    /// <param name="TotleNum">数据条数</param>
    /// <param name="NumPerPage">一页要显示的条数</param>
    /// <param name="Thepage">显示第几页</param>
    /// <param name="StartUrl">链接头部</param>
    /// <param name="EndUrl">链接尾部</param>
    /// <returns></returns>
    public static string UrlPageHtml(int TotleNum, int NumPerPage, int Thepage, String StartUrl, String EndUrl)
    {
        string strTemp = "";
        int n;
        int p;
        int ii;
        if (Convert.ToInt32(TotleNum) <= Convert.ToInt32(NumPerPage))
            return "";

        if (Convert.ToInt32(TotleNum) % Convert.ToInt32(NumPerPage) == 0)
        {
            n = TotleNum / NumPerPage;
        }
        else
        {
            n = TotleNum / NumPerPage + 1;
        }

        strTemp += "<span>共" + n + "页/" + TotleNum + "条</span>";

        if (Convert.ToInt32(Thepage) < 2)
        {
            strTemp += "<a>首页</a>";
            strTemp += "<a>上一页</a>";
        }
        else
        {
            strTemp += "<a href=\"" + StartUrl + 1 + EndUrl + "\">首页</a>";
            strTemp += "<a href=\"" + StartUrl + Convert.ToString(Thepage - 1) + EndUrl + "\">上一页</a>";
        }

        if (Convert.ToInt32(Thepage) - 1 % 8 == 0)
        {
            p = (Thepage - 1) / 8;
        }
        else
        {
            p = (Thepage - 1) / 8;
        }

        int uming_i = 1;
        for (ii = p * 8 + 1; ii <= p * 8 + 8; ii++)
        {
            if (ii == Thepage)
            {
                strTemp += "<a class=\"act\">" + ii + "</a>";
            }
            else
            {
                strTemp += "<a href=\"" + StartUrl + ii + EndUrl + "\">" + ii + "</a>";
            }
            if (ii == n)
            {
                break;
            }
            uming_i = uming_i + 1;
        }

        if (n - Convert.ToInt32(Thepage) < 1)
        {
            strTemp += "<a>下一页</a>";
            strTemp += "<a>末页</a>";
        }
        else
        {
            strTemp += "<a href=\"" + StartUrl + Convert.ToString(Thepage + 1) + EndUrl + "\">下一页</a>";
            strTemp += "<a href=\"" + StartUrl + n + EndUrl + "\">末页</a></li>";
        }

        strTemp += "";
        return (string)strTemp;
    }
    #endregion

    #region 前端超简单分页显示
    /// <summary>
    /// 前端超简单分页显示
    /// </summary>
    /// <param name="TotleNum">数据条数</param>
    /// <param name="NumPerPage">一页要显示的条数</param>
    /// <param name="Thepage">显示第几页</param>
    /// <param name="StartUrl">头部</param>
    /// <param name="EndUrl">尾部</param>
    /// <returns></returns>
    public static string UrlSimplePageHtml(int TotleNum, int NumPerPage, int Thepage, String StartUrl, String EndUrl)
    {
        string strTemp;
        int n;
        if (Convert.ToInt32(TotleNum) <= Convert.ToInt32(NumPerPage))
        {
            return "<span>共 1/1 页</span><span class=\"pages\"><a href=\"javascript:void(0);\"><</a></span><span class=\"pages\"><a href=\"javascript:void(0);\">></a></span>";
        }
        if (Convert.ToInt32(TotleNum) % Convert.ToInt32(NumPerPage) == 0)
        {
            n = TotleNum / NumPerPage;
        }
        else
        {
            n = TotleNum / NumPerPage + 1;
        }

        strTemp = "<span>共 " + Thepage + "/" + n + " 页</span>";


        if (Convert.ToInt32(Thepage) < 2)
        {
            strTemp += "<span class=\"pages\"><a href=\"javascript:void(0);\"><</a></span>";
        }
        else
        {
            strTemp += "<span class=\"pages\"><a href='" + StartUrl + Convert.ToString(Thepage - 1) + EndUrl + "'><</a></span>";
        }

        if (n - Convert.ToInt32(Thepage) < 1)
        {
            strTemp += "<span class=\"pages\"><a href=\"javascript:void(0);\">></a></span>";
        }
        else
        {
            strTemp += "<span class=\"pages\"><a href='" + StartUrl + Convert.ToString(Thepage + 1) + EndUrl + "'>></a></span>";
        }
        return (string)strTemp;
    }
    #endregion

    #region 手机前端简单分页显示
    /// <summary>
    /// 手机前端简单分页显示
    /// </summary>
    /// <param name="TotleNum">数据条数</param>
    /// <param name="NumPerPage">一页要显示的条数</param>
    /// <param name="Thepage">显示第几页</param>
    /// <param name="StartUrl">链接头部</param>
    /// <param name="EndUrl">链接尾部</param>
    /// <returns></returns>
    public static string UrlMobilePageHtml(int TotleNum, int NumPerPage, int Thepage, String StartUrl, String EndUrl)
    {
        string strTemp;
        int n;
        int p;
        int ii;
        if (Convert.ToInt32(TotleNum) <= Convert.ToInt32(NumPerPage))
            return "";

        if (Convert.ToInt32(TotleNum) % Convert.ToInt32(NumPerPage) == 0)
        {
            n = TotleNum / NumPerPage;
        }
        else
        {
            n = TotleNum / NumPerPage + 1;
        }

        strTemp = "<div class=\"page\">";

        if (Convert.ToInt32(Thepage) - 1 % 3 == 0)
        {
            p = (Thepage - 1) / 3;
        }
        else
        {
            p = (Thepage - 1) / 3;
        }

        if (Convert.ToInt32(Thepage) < 2)
        {
            strTemp += "<a href=\"javascript:void(0);\" class=\"first\">上一页</a>";
        }
        else
        {
            strTemp += "<a href=\"" + StartUrl + Convert.ToString(Thepage - 1) + EndUrl + "\" class=\"first\">上一页</a>";
        }

        if (p * 3 > 0)
        {
            strTemp += "<a href=\"" + StartUrl + p * 3 + EndUrl + "\" >..</a>";
        }

        int uming_i = 1;
        for (ii = p * 3 + 1; ii <= p * 3 + 3; ii++)
        {
            if (ii == Thepage)
            {
                strTemp += "<a href=\"javascript:void(0);\" class=\"active\">" + ii + "</a>";
            }
            else
            {
                strTemp += "<a href=\"" + StartUrl + ii + EndUrl + "\">" + ii + "</a>";
            }
            if (ii == n)
            {
                break;
            }
            uming_i = uming_i + 1;
        }

        if (ii <= n && uming_i == 4)
        {
            strTemp += "<a href=\"" + StartUrl + ii + EndUrl + "\" >..</a>";
        }

        if (n - Convert.ToInt32(Thepage) < 1)
        {
            strTemp += "<a href=\"javascript:void(0);\" class=\"first\">下一页</a>";
        }
        else
        {
            strTemp += "<a href=\"" + StartUrl + Convert.ToString(Thepage + 1) + EndUrl + "\" class=\"first\">下一页</a>";
        }
        strTemp += "</div>";
        return (string)strTemp;
    }
    #endregion

    #region  普通分页

    //普通分页<非ajax>
    /// <summary>
    /// 有数字的分页
    /// </summary>
    /// <param name="countNum">总记录数</param>
    /// <param name="Current">页码</param>
    ///  <param name="pageSize">每页显示的条数</param>
    ///  <param name="PageFilter">条件：&aa=1</param>
    /// <returns>分页Html</returns>
    public static String NewPageHtml(int countNum, int Current, int pageSize, String PageWhere)
    {


        String pageHtml = String.Empty;
        String PageStr = String.Empty;

        int pageCount = 0;

        if (countNum % pageSize == 0)
        {
            pageCount = countNum / pageSize;
        }
        else
        {
            pageCount = countNum / pageSize + 1;
        }




        if (pageCount > 1)
        {

            string uphtml = "";
            string downHtml = "";




            if (Current > 1)
            {
                uphtml = "<a href=\"?current=" + Convert.ToString(1) + "" + PageWhere + " \" class=\"page-prev\">首页</a> <a href=\"?current=" + Convert.ToString(Current - 1) + "" + PageWhere + " \" class=\"page-prev\">上一页</a> ";
            }
            else
            {
                uphtml = "<a href=\"?current=" + Convert.ToString(1) + "" + PageWhere + " \" class=\"page-prev\">首页</a> <a href=\"?current=" + Convert.ToString(Current) + "" + PageWhere + " \" class=\"page-prev\">上一页</a> ";


            }

            if (Current < pageCount)
            {
                downHtml = "<a  href=\"?current=" + Convert.ToString(Current + 1) + "" + PageWhere + " \"  class=\"page-prev\">下一页</a> <a  href=\"?current=" + pageCount + "" + PageWhere + " \" class=\"page-prev\">尾页</a> ";


            }
            else
            {

                downHtml = "<a  href=\"?current=" + Convert.ToString(Current) + "" + PageWhere + " \"  class=\"page-prev\">下一页</a> <a  href=\"?current=" + pageCount + "" + PageWhere + " \" class=\"page-prev\">尾页</a> ";


            }

            int j = 1;
            int topj = 1;

            j = Current - 4;
            topj = Current + 5;
            if (j < 1) { topj = topj - j + 1; j = 1; }
            if (topj > pageCount)
            {
                j = j - (topj - pageCount);
                if (j < 1) { j = 1; }
                topj = pageCount;
            }

            for (int i = j; i <= topj; i++)
            {
                if (i == Current)
                {   //<a href="#">10</a>
                    //<a href="#">1</a>
                    pageHtml += "<a class=\"cur\" href=\"?current=" + i.ToString() + "" + PageWhere + " \" >" + Convert.ToString(i) + "</a> ";
                }
                else
                {
                    pageHtml += "<a class=\"\" href=\"?current=" + i.ToString() + "" + PageWhere + " \">" + Convert.ToString(i) + "</a> ";
                }
            }





            PageStr = "<div class=\"in-page tac pl25 clearfix sm-pl0\"><div class=\"fl sm-12\">共 " + pageCount + " 页 当前第 " + Current + " 页</div><div class=\"fr sm-12 sm-pt20 sm-pb20\">" + uphtml + pageHtml + downHtml + "</div></div>";
        }



   


        return PageStr;
    }




    #endregion

    #region  普通分页

    //普通分页<非ajax>
    /// <summary>
    /// 有数字的分页
    /// </summary>
    /// <param name="countNum">总记录数</param>
    /// <param name="Current">页码</param>
    ///  <param name="pageSize">每页显示的条数</param>
    ///  <param name="PageFilter">条件：&aa=1</param>
    /// <returns>分页Html</returns>
    public static String TopPageHtml(int countNum, int Current, int pageSize, String PageWhere)
    {


        String pageHtml = String.Empty;
        String PageStr = String.Empty;

        int pageCount = 0;

        if (countNum % pageSize == 0)
        {
            pageCount = countNum / pageSize;
        }
        else
        {
            pageCount = countNum / pageSize + 1;
        }




      

            string uphtml = "";
            string downHtml = "";




            if (Current > 1)
            {

                uphtml = "<a  href=\"?current=" + Convert.ToString(Current - 1) + "" + PageWhere + " \" class=\"btn-l tra\"></a>";
            }
            else
            {
                uphtml = "<a href=\"?current=" + Convert.ToString(Current) + "" + PageWhere + " \" class=\"btn-l tra\"></a>";


            }

            if (Current < pageCount)
            {
                downHtml = "<a href=\"?current=" + Convert.ToString(Current + 1) + "" + PageWhere + " \"  class=\"btn-r tra\"></a> ";


            }
            else
            {

                downHtml = "<a href=\"?current=" + Convert.ToString(Current) + "" + PageWhere + " \"  class=\"btn-r tra\"></a>";


            }

            PageStr = "<i class=\"pr5 fb\">" + countNum + "</i> Products <span class=\"pl25 mr20\">" + Current + "/" + pageCount + "</span> " + uphtml + downHtml;


      


       

        return PageStr;
    }




    #endregion




    #region  普通分页

    //普通分页<非ajax>
    /// <summary>
    /// 有数字的分页
    /// </summary>
    /// <param name="countNum">总记录数</param>
    /// <param name="Current">页码</param>
    ///  <param name="pageSize">每页显示的条数</param>
    ///  <param name="PageFilter">条件：&aa=1</param>
    /// <returns>分页Html</returns>
    public static String PageHtml(int countNum, int Current, int pageSize, String PageWhere)
    {


        String pageHtml = String.Empty;
        String PageStr = String.Empty;

        int pageCount = 0;

        if (countNum % pageSize == 0)
        {
            pageCount = countNum / pageSize;
        }
        else
        {
            pageCount = countNum / pageSize + 1;
        }




        if (pageCount > 1)
        {

            string uphtml = "";
            string downHtml = "";


            if (Current > 1)
            {
                uphtml = "<a href=\"?current=" + Convert.ToString(Current - 1) + "" + PageWhere + " \" class=\"prev\"><img src=\"images/prev.jpg\" /></a>";
            }
            else
            {
                uphtml = "<a href=\"?current=" + Convert.ToString(Current) + "" + PageWhere + " \" class=\"prev\"><img src=\"images/prev.jpg\" /></a>";


            }

            if (Current < pageCount)
            {
                downHtml = "<a  href=\"?current=" + Convert.ToString(Current + 1) + "" + PageWhere + " \"  class=\"next\"><img src=\"images/next.jpg\" /></a>";


            }
            else
            {

                downHtml = "<a  href=\"?current=" + Convert.ToString(Current) + "" + PageWhere + " \"  class=\"next\"><img src=\"images/next.jpg\" /></a>";


            }

            pageHtml = "<span>" + Current + "</span>/" + pageCount;

            PageStr = "<div class=\"sndl fr\">" + uphtml + pageHtml + downHtml + "</div>";
        }

        return PageStr;
    }
    #endregion

    #region 前端Ajax伪静态分页显示
    /// <summary>
    /// 前端Ajax伪静态分页显示
    /// </summary>
    /// <param name="countNum">总记录数</param>
    /// <param name="Current">页码</param>
    /// <param name="pageSize">每页显示的条数</param>
    /// <param name="StartUrl">头部</param>
    /// <param name="EndUrl">尾部</param>
    /// <returns>分页Html</returns>
    public static String UrlAjaxPageHtml(int countNum, int Current, int pageSize, String StartUrl, String EndUrl)
    {
        string PageStr = string.Empty;
        string pageHtml = string.Empty;
        int pageCount = 0;
        if (countNum % pageSize == 0)
        {
            pageCount = countNum / pageSize;
        }
        else
        {
            pageCount = countNum / pageSize + 1;
        }
        if (pageCount > 1)
        {



          

            PageStr = "<div class=\"page\">";
            string uphtml = "";
            string downHtml = "";

         

            int j = 1;
            int topj = 1;

            j = Current - 4;
            topj = Current + 5;
            if (j < 1) { topj = topj - j + 1; j = 1; }
            if (topj > pageCount)
            {
                j = j - (topj - pageCount);
                if (j < 1) { j = 1; }
                topj = pageCount;
            }

            for (int i = j; i <= topj; i++)
            {
                if (i == Current)
                {
                    pageHtml += "<a  class=\"cur\">" + Convert.ToString(i) + "</a> ";
                }
                else
                {
                    pageHtml += "<a href=\"javascript:void(0);\" " + StartUrl + i.ToString() + EndUrl + " >" + Convert.ToString(i) + "</a> ";
                }
            }

            pageHtml = uphtml + pageHtml + downHtml;
            PageStr += pageHtml + "</div>";
        }
        return PageStr;
    }
    #endregion
}