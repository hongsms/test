using System;
//添加的命名空间引用
using System.Text.RegularExpressions;
using System.Net;
using System.Net.Mail;
using System.Configuration;
using System.Collections;

public class MyEnum
{
    //审核状态
    public enum IsAudit : int
    {
        未完善资料 = 0, 审核通过 = 1, 待审核 = 2, 审核未通过 = 3
    }
    //会员类型
    public enum UserType : int
    {
        普通会员 = 10000, 原材料供应商 = 10001, 联盟商家 = 10002
    }

    //会员类型字段
    public enum UserTypeModel : int
    {
        Ordinary = 10000, Supplier = 10001, Union = 10002
    }

    //订单状态
    public enum OrderState : int
    {
        已生成 = 0, 已确认 = 10, 已完成 = 20, 已取消 = 30, 已退货 = 40
    }

    //支付状态
    public enum PayState : int
    {
        待支付 = 0, 首款已支付 = 1, 尾款已支付 = 2, 已全款支付 = 3
    }

    //首款支付状态
    public enum DownPaymentState : int
    {
        待支付 = 0, 已支付 = 1
    }

    //尾款支付状态
    public enum FinalPaymentState : int
    {
        待支付 = 0, 已支付 = 1
    }

    //配送状态
    public enum LogisticsState : int
    {
        待发货 = 0, 已发货 = 1
    }

    //退货状态
    public enum ReturnGoodsState : int
    {
        未退货 = 0, 发起退货 = 1, 退货成功 = 2
    }

    //收货状态
    public enum ReceivingState : int
    {
        未收货 = 0, 已收货 = 1
    }
}


