using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

using POneIntf.Common;
namespace POneIntf.BLL
{
    public class SI32 : IHandleReq
    {
        Model.Req32 req = null;

        #region IHandleReq 成员

        public string InvokeRequest(string postStr)
        {
            Model.Rsp32 rsp = new Model.Rsp32();
            
            try
            {
                Helper.LogWrite("SI32 Request:" + postStr);
                this.req = Common.Helper.JsonDeserialize<Model.Req32>(postStr);
                
                /// 1 取指定用户基本信息
                List<Model.M_Sys_Login_Account> etyLst = this.GetLogUser();

                /// 2 生成临时密码/验证码，并写入log
                if (etyLst.Count > 0)
                {                  
                    string tmpStr = System.Guid.NewGuid().ToString().Substring(0, 6);
                    Model.M_Sys_Log ety = new Model.M_Sys_Log();
                    ety.CreateDate = DateTime.Parse(Helper.Today);
                    ety.LogId = GetLogId();
                    ety.UserId = req.data.userid;
                    ety.Msg = "发送验证码" + tmpStr + "至" + etyLst[0].Email;
                    ety.Type = 0;
                    this.Save2Log(ety);

                    rsp.data.result = "100";
                    rsp.data.message = "生成验证码成功";
                    rsp.data.checkcode = tmpStr;
                    rsp.data.email = Helper.DbNull2Str(etyLst[0].Email);
                    rsp.data.oldpsw = Helper.DbNull2Str(etyLst[0].Password);
                }
                else
                {
                    rsp.data.result = "200";
                    rsp.data.message = "Can not find user!";
                }
            }
            catch (Exception err)
            {
                rsp.code = "100";
                rsp.msg = err.Message;
                rsp.status = "false";
            }
            rsp.raw = Common.Helper.JsonSerialize(rsp);
            return Common.Helper.JsonSerialize(rsp);
        }

        #endregion

        private List<Model.M_Sys_Login_Account> GetLogUser()
        {           
            CRUD biz01 = new CRUD(DbVendor.Oracle, Runtime.OracleConnStrLocal, false);
            try
            {
                Clause c = new Clause();
                c.Add("trim(LoginName)", this.req.data.userid);                
                List<Model.M_Sys_Login_Account> etyLst = biz01.Retrieve<Model.M_Sys_Login_Account>(c.m_values);
                biz01.Commit();

                return etyLst;
            }
            catch (Exception err)
            {
                biz01.Abort();
                throw err;
            }
        }

        private int GetLogId()
        {
            string sql = "select max(LogId) from Sys_log";
            DbUtil_o1 db = new DbUtil_o1(Runtime.OracleConnStrLocal, false);            
            try
            {
                string result = db.ExecuteScalar(sql);

                if (result != "")
                    return Convert.ToInt32(result) + 1;
                else
                    return 1;
            }
            catch (Exception err)
            {
                throw new Exception(err.Message + ":" + err.StackTrace);
            }
        }

        private void Save2Log(Model.M_Sys_Log ety)
        {
            CRUD_o1 biz = new CRUD_o1(Runtime.OracleConnStrLocal, true);            
            try
            {
                biz.Create<Model.M_Sys_Log>(ety);

                biz.Commit();
            }
            catch (Exception err)
            {
                biz.Abort();
                throw err;
            }
        }
    }
}