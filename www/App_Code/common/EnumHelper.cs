using System;
using System.Collections;
using System.Collections.Generic;
using System.EnterpriseServices;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.UI.WebControls;

/// <summary>
/// EnumHelper 的摘要说明
/// </summary>
public  class EnumHelper
{
    /// <summary> 
    /// 枚举转成list
    /// </summary> 
    /// <param name="enumType">枚举类型</param> 
    /// <returns></returns> 
    public  static IList EnumToList(Type enumType)
    {
        ArrayList list = new ArrayList();

        foreach (int i in Enum.GetValues(enumType))
        {
            ListItem listitem = new ListItem(Enum.GetName(enumType, i), i.ToString());
            list.Add(listitem);
        }

        return list;
    }
}