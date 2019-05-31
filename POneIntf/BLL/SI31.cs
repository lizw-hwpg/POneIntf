using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

using POneIntf.Common;
namespace POneIntf.BLL
{
    public class SI31 : IHandleReq
    {
        Model.Req31 req = null;

        #region IHandleReq 成员

        public string InvokeRequest(string postStr)
        {
            Helper.LogWrite("SI31 Request:" + postStr);
            Model.Rsp31 rsp = new Model.Rsp31();
            this.req = Common.Helper.JsonDeserialize<Model.Req31>(postStr);

            DataTable dtUser = this.GetUserInfo(); //取登录用户信息

            if (dtUser.Rows.Count > 0)
            {
                string status = Helper.DbNull2Str(dtUser.Rows[0]["Status"]);
                string pwd = Helper.DbNull2Str(dtUser.Rows[0]["PASSWORD"]);

                if (this.req.data.password == pwd) // 如果密码正确
                {
                    rsp.data.result = "100";
                    rsp.data.message = "验证成功";
                    rsp.data.lastlogin = dtUser.Rows[0]["updatedate"].ToString(); //上次登录
                    Common.Runtime.CurrentUser = this.req.data.username;

                    if (this.req.data.logintype == "C" || this.req.data.logintype == "E")
                    {
                        //取租约信息
                        rsp.data.leaseinfo = this.GetGroupLease(int.Parse(dtUser.Rows[0]["userid"].ToString()));
                        rsp.data.leasecount = rsp.data.leaseinfo.Count;
                    }

                    //更新最后登录时间 
                    dtUser.Rows[0]["UpdateDate"] = DateTime.Now.ToString(); //本次登录更新
                    if (this.req.data.logintype != "E")
                    {
                        CRUD biz = new CRUD(DbVendor.Oracle, Runtime.OracleConnStrLocal, true);
                        int cnt = biz.Update<Model.M_Sys_Login_Account>(dtUser.Rows[0], "UserId");
                        biz.Commit();                        
                    }
                }
                else
                {
                    rsp.data.result = "200";
                    rsp.data.message = "Wrong password!";
                }
                rsp.data.userid = Helper.DbNull2Int(dtUser.Rows[0]["UserId"]);
                rsp.data.status = Helper.DbNull2Str(dtUser.Rows[0]["Status"]);

                /// 判断用户状态
                switch (status)
                {
                    case "A":
                    case "F":
                    case "C":
                        break;
                    case "I":
                        rsp.data.result = "200";
                        rsp.data.message = "Forbidden User!";
                        break;

                    case "T":
                        if (this.req.data.password == Helper.DbNull2Str(dtUser.Rows[0]["TempPsw"]))
                        {
                            rsp.data.result = "100";
                            rsp.data.message = "Success!";
                            Runtime.CurrentUser = this.req.data.username;

                            dtUser.Rows[0]["TempPsw"] = "";
                            dtUser.Rows[0]["Status"] = "A";                            
                            UpdateLoginAccount(dtUser.Rows[0]);
                        }
                        break;
                }
            }
            else
            {
                rsp.data.result = "200";
                rsp.data.message = "User not exist!";
            }

            return Common.Helper.JsonSerialize(rsp);
        }

        #endregion

        private void UpdateLoginAccount(DataRow dr)
        {
            CRUD biz = new CRUD(DbVendor.Oracle, Runtime.OracleConnStrLocal, true);
            try
            {
                biz.Update<Model.M_Sys_Login_Account>(dr, "UserId");
                biz.Commit();
            }
            catch (Exception err)
            {
                biz.Abort();
                throw err;
            }

        }

        private DataTable GetUserInfo()
        {
            CRUD biz = new CRUD(DbVendor.Oracle, Runtime.OracleConnStrLocal, false);
            try
            {
                Clause c = new Clause();

                string sql = "";
                switch (this.req.data.logintype) //判断登录用户类型
                {
                    case "S":
                        sql += "select * from " + M_Sys_Login_System.TableName + " where 1=1 ";
                        sql += " and trim(LoginName)='" + req.data.username + "'";
                        break;
                    case "L":
                        sql += "select * from " + M_Sys_Login_Lease.TableName + " where 1=1 ";
                        sql += " and trim(LeaseNumber)='" + req.data.username + "'";
                        break;
                    case "E":
                    case "C":
                        c.Add("trim(LoginName)", req.data.username);
                        break;
                    default:
                        throw new Exception("Unknown login type!");
                }
                DataTable dt = biz.Select<Model.M_Sys_Login_Account>(c.m_values);
                biz.Commit();
                return dt;
            }
            catch (Exception err)
            {
                biz.Abort();
                throw err;
            }
        }

        private List<string> GetGroupLease(int userid)
        {
            DbUtil_o1 db = new DbUtil_o1(Runtime.OracleConnStrLocal, false);
            string sql = "select trim(leasenumber) as leasenumber from SYS_USERS_GROUP_LEASE where UserId=" + userid.ToString();

            DataTable dt = db.ExecuteDT(sql, null);

            List<string> leases = new List<string>();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                leases.Add(dt.Rows[i][0].ToString());
            }
            return leases;
        }       
    }
}