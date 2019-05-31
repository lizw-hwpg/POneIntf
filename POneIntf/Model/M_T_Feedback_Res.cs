using System;
using System.Collections.Generic;
using System.Web;

/// <summary>
/// Summary description for M_T_Feedback_Res
/// </summary>
public class M_T_Feedback_Res
{
    public const string TableName = "T_Feedback_Res";

    public int Id;
    public int FeedbackId;
    public string LeaseNum;
    public string Status;
    public string Approve;
    public string CreateBy;
    public DateTime CreateDate;
    public string Detail;
    public string ReplyType;
    public string ReplyPerson;
}