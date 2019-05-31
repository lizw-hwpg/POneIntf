using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for M_T_PAYMENT_INFO
/// </summary>
public class M_T_PAYMENT_INFO
{
    public const string TableName = "T_Payment_Info";

    public int paymentid;
    public string invoicenumber;
    public int invoicelinenum;
    public string chargecode;
    public double amount;
    public double actualpay;
}