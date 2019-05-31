using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

using POneIntf.Common;

namespace POneIntf.Model
{
    /// <summary>
    /// 自定义用户登录表
    /// </summary>
    public class M_Sys_Login_Account
    {
        public const string TableName = "Sys_Login_Account";

        public int UserId;              //用户ID
        public string LoginName = "";   //登录名
        public string Password = "";    //登录名对应的密码
        public int LeaseGroup;          //登录名对应的租约号组
        public string Email = "";       //邮箱地址
        public string Phone = "";       //电话
        public string Status = "";      //账户状态 A-可用 I-不可用 C-密码已更改  T-临时密码 F-首次登陆
        public string TempPsw = "";     //临时密码
        public string CreateBy = "";    //
        public DateTime CreateDate;     //
        public string UpdateBy;         //
        public DateTime UpdateDate;
        public string CustName;

        public int testField { get; set; }

        public M_Sys_Login_Account() { }
        public M_Sys_Login_Account(DataRow dr)
        {
            this.UserId = Helper.DbNull2Int(dr["UserId"]);
            this.LoginName = Helper.DbNull2Str(dr["LoginName"]);
            this.Password = Helper.DbNull2Str(dr["Password"]);
            if (dr["LeaseGroup"].ToString() != "")
                this.LeaseGroup = Helper.DbNull2Int(dr["LeaseGroup"]);
            else
                this.LeaseGroup = -1; //表示没有值的情况

            this.Email = Helper.DbNull2Str(dr["Email"]);
            this.Phone = Helper.DbNull2Str(dr["Phone"]);
            this.Status = Helper.DbNull2Str(dr["Status"]);
            this.TempPsw = Helper.DbNull2Str(dr["TempPsw"]);
            this.CreateBy = Helper.DbNull2Str(dr["CreateBy"]);
            this.CreateDate = DateTime.Parse(dr["CreateDate"].ToString());
            this.UpdateBy = Helper.DbNull2Str(dr["UpdateBy"]);
            this.UpdateDate = DateTime.Parse(dr["UpdateDate"].ToString());
            this.CustName = Helper.DbNull2Str(dr["CustName"]);
        }
    }
}
