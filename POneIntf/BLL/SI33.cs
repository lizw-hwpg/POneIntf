using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

using POneIntf.Common;
namespace POneIntf.BLL
{
    public class SI33 : IHandleReq
    {
        Model.Req33 req = null;

        #region IHandleReq 成员

        public string InvokeRequest(string postStr)
        {
            Model.Rsp33 rsp = new Model.Rsp33();

            try
            {
                Helper.LogWrite("SI33 Request:" + postStr);
                this.req = Helper.Deserialize<Model.Req33>(postStr);
                Model.M_Sys_Login_Account ety = Aid.GetLoginAcc(this.req.data.userid);
                if (ety.LoginName != "")
                {
                    if (ety.Password == this.req.data.oldpsw)
                    {
                        ety.Password = this.req.data.newpsw;
                        int cnt = this.UpdateLoginAcc(ety);
                        if (cnt > 0)
                        {
                            rsp.data.result = "100";
                            rsp.data.message = "成功";
                        }
                        else
                        {
                            rsp.data.result = "200";
                            rsp.data.message = "失败";
                        }
                    }
                    else
                    {
                        rsp.data.result = "200";
                        rsp.data.message = "旧密码不匹配";
                    }
                }
                else
                {
                    rsp.data.result = "100";
                    rsp.data.message = "用户不存在";
                }
            }
            catch (Exception err)
            {
                rsp.code = "100";
                rsp.msg = "系统错误:" + err.Message;           
            }
            rsp.raw = Common.Helper.JsonSerialize(rsp);
            return Common.Helper.JsonSerialize(rsp);
        }

        #endregion

        private int UpdateLoginAcc(Model.M_Sys_Login_Account ety)
        {
            BLL.CRUD biz = new CRUD(DbVendor.Oracle, Runtime.OracleConnStrLocal, true);
            try
            {
                int cnt = biz.Update(ety, "UserId");
                biz.Commit();

                return cnt;
            }
            catch (Exception)
            {
                biz.Abort();
                throw;
            }
        }
    }
}