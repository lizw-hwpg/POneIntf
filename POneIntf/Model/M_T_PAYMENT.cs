using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// T_Payment数据库实体类
/// </summary>
public class M_T_PAYMENT
{
    public const string TableName = "T_Payment";

    public int paymentid;
    public string leasenumber;
    public double amount;
    public string paytype;
    public DateTime paydate;
    public string status;
    public string pps_referenceno;
    public string pps_merchantid;
    public string pps_txcode;
    public double pps_amount;
    public string pps_opcode;
    public string pps_payfor;
    public string pps_locale;
    public string pps_userdata;
    public string pps_siteid;
    public string pps_statuscode;
    public string pps_responsecode;
    public string pps_bankaccount;
    public string pps_valuedate;
    public string pps_txdate;
    public string pps_postid;
    public string pps_isn;
    public string pps_wholemessage;
}