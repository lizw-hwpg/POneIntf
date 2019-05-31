using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

using POneIntf.Common;

/// <summary>
/// 租约用户登录表
/// </summary>
public class M_Sys_Login_Lease
{
    public const string TableName = "Sys_Login_Lease";

    public int UserId;          //
    public string LeaseNumber;  //	N	CHAR(30)	N			租约号
    public string Password;     //PASSWORD	N	VARCHAR2(50)	N			密码
    public string Email;        //EMAIL	N	VARCHAR2(50)	N			邮箱
    public string Phone;        //PHONE	N	VARCHAR2(30)	Y			电话
    public string Status;       //STATUS	N	VARCHAR2(10)	N			账户状态 A-可用 I-不可用 C-密码已更改  T-临时密码 F-首次登陆
    public string SiteNumber;   //SITENUMBER	N	CHAR(10)	Y			Site号
    public string TempPsw;      //	N	VARCHAR2(50)	Y			临时密码
    public string CreateBy;     //	N	VARCHAR2(30)	Y			
    public DateTime CreateDate; //CREATEDATE	N	DATE	Y			
    public string UpdateBy;     //
    public DateTime UpdateDate; //

    public M_Sys_Login_Lease() { }

    public M_Sys_Login_Lease(DataRow dr)
    {
        this.UserId =  Helper.DbNull2Int(dr["UserId"]);
        this.LeaseNumber = Helper.DbNull2Str(dr["LeaseNumber"]);
        this.Password = Helper.DbNull2Str(dr["Password"]);
        this.Email = Helper.DbNull2Str(dr["Email"]);
        this.Phone = Helper.DbNull2Str(dr["Phone"]);
        this.Status = Helper.DbNull2Str(dr["Status"]);
        this.SiteNumber = Helper.DbNull2Str(dr["SiteNumber"]);
        this.TempPsw = Helper.DbNull2Str(dr["TempPsw"]);
        this.CreateBy = Helper.DbNull2Str(dr["CreateBy"]);
        this.CreateDate = DateTime.Parse(dr["CreateDate"].ToString());
        this.UpdateBy = Helper.DbNull2Str(dr["UpdateBy"]);
        this.UpdateDate = DateTime.Parse(dr["UpdateDate"].ToString());
    }
}