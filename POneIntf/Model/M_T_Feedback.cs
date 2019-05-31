using System;
using System.Collections.Generic;
using System.Web;

/// <summary>
/// Summary description for M_T_Feedback
/// </summary>
public class M_T_Feedback
{
    public const string TableName = "T_Feedback";

    public int FeedbackId;
    public string Title;
    public string Detail;
    public string Status; //状态 100-未回复 200-已回复 900-其他
    public string CreateBy;
    public DateTime CreateDate;
    public string LeaseNumber;
    public int Type;
    public string PropertyCode;
}