using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for M_T_Notice
/// </summary>
public class M_T_Notice
{
    public const string TableName = "T_Notice";

    public int NoticeId;
    public string Type;
    public string Title;
    public string Detail;
    public string ImgUrlSmall;
    public string ImgUrlLarge;  //IMGURLLARGE	N	VARCHAR2(200)	Y			通知大图
    public string Status;       //STATUS	N	VARCHAR2(10)	Y			通知状态 A-有效 I-无效
    public string Approve;      //APPROVE	N	VARCHAR2(10)	Y			是否审批 A-已审批 I-未审批
    public string CreateBy;     //CREATEBY	N	VARCHAR2(30)	Y			
    public DateTime CreateDate;   //CREATEDATE	N	DATE	Y			
    public string UpdateBy;     //UPDATEBY	N	VARCHAR2(30)	Y			
    public DateTime UpdateDate;   //UPDATEDATE	N	DATE	Y			
    public DateTime StartDate;    //STARTDATE	N	DATE	N			通知开始日期
    public DateTime EndDate;      //ENDDATE	N	DATE	N			通知结束日期
    public string PropertyCode; //PROPERTYCODE	N	CHAR(15)	N			物业CODE
    public string Attach_Url;
    //public string LeaseNumber;  //LEASENUMBER	N	CHAR(30)	Y			租约号
}