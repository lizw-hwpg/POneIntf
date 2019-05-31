using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

using POneIntf.Common;

/// <summary>
/// Summary description for M_Sys_Login_System
/// </summary>
public class M_Sys_Login_System
{
    public const string TableName = "Sys_Login_System";

    public int UserId;              //	用户ID
    public string LoginName = "";   //	登录名
    public string Password = "";    //	登录名对应的密码
    public string Email = "";       //	邮箱地址
    public string Phone = "";       //	电话
    public string Status = "";      //	账户状态 A-可用 I-不可用 C-密码已更改  T-临时密码 F-首次登陆
    public string SiteNumber = "";  //         
    public int UserGroup;           //  用户组
    public string TempPsw = "";     //	临时密码
    public string CreateBy = "";    //	
    public DateTime CreateDate;     //
    public string UpdateBy;         //			
    public DateTime UpdateDate;

    public M_Sys_Login_System(DataRow dr)
    {
        this.UserId = Helper.DbNull2Int(dr["UserId"]);
        this.LoginName = Helper.DbNull2Str(dr["LoginName"]);
        this.Password = Helper.DbNull2Str(dr["Password"]);
        this.Email = Helper.DbNull2Str(dr["Email"]);
        this.Phone = Helper.DbNull2Str(dr["LoginName"]);
        this.Status = Helper.DbNull2Str(dr["Status"]);
        this.SiteNumber = Helper.DbNull2Str(dr["SiteNumber"]);
        this.UserGroup = Helper.DbNull2Int(dr["UserGroup"]);
        this.TempPsw = Helper.DbNull2Str(dr["TempPsw"]);
        this.CreateBy = Helper.DbNull2Str(dr["CreateBy"]);
        this.CreateDate = DateTime.Parse(Helper.DbNull2Str(dr["CreateDate"]));
        this.UpdateBy = Helper.DbNull2Str(dr["UpdateBy"]);
        this.UpdateDate = DateTime.Parse(Helper.DbNull2Str(dr["UpdateDate"]));
    }
}