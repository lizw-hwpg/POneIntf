using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

using POneIntf.Common;
namespace POneIntf.BLL
{
    public class SI34 : IHandleReq
    {
        Model.Req34 req = null;

        #region IHandleReq 成员

        public string InvokeRequest(string postStr)
        {
            Model.Rsp34 rsp = new Model.Rsp34();

            try
            {
                Helper.LogWrite("SI34 Request:" + postStr);
                this.req = Helper.Deserialize<Model.Req34>(postStr);
                if (req.data.leasenum != "")
                {
                    DataTable dt = this.GetPendingPay(req.data.leasenum);
                    if (dt.Rows.Count > 0)
                    {
                        Aid.AttachRowId(dt);
                        rsp.data.result = "100";
                        rsp.data.message = "获取成功!";
                        rsp.data.customername = Helper.DbNull2Str(dt.Rows[0]["customername"]);
                        rsp.data.payinfonum = Helper.DbNull2Int(dt.Rows.Count);
                        rsp.data.premisename = Helper.DbNull2Str(dt.Rows[0]["premisename"]);
                        rsp.data.shoparea = Helper.DbNull2Str(dt.Rows[0]["shoparea"]);
                        rsp.data.shopname = Helper.DbNull2Str(dt.Rows[0]["shopname"]);

                        rsp.data.totalamount = Helper.DbNull2Dec(dt.Compute("sum(outstanding)", ""));
                        rsp.data.payinfo = Aid.DT2List<Model.Rsp34dataDtl>(dt) as List<Model.Rsp34dataDtl>;
                    }
                    else
                    {
                        rsp.data.result = "200";
                        rsp.data.message = "没有找到数据";
                    }
                }
                else
                {

                    rsp.code = "100";
                    rsp.status = "true";
                    rsp.msg = "请求成功!";
                    rsp.time = Helper.Today;

                    rsp.data.result = "200";
                    rsp.data.message = "租约号为空,无法查询";
                }
            }
            catch (Exception err)
            {
                rsp.code = "200";
                rsp.status = "false";
                rsp.msg = "系统错误:" + err.Message;
                rsp.time = Helper.Today;
            }
            rsp.raw = Common.Helper.JsonSerialize(rsp);
            return Common.Helper.JsonSerialize(rsp);
        }

        #endregion

        private DataTable GetPendingPay(string leaseNum)
        {
            string sql = "";
            sql += " select ai.customer_number,";
            sql += "ac.customer_name as customername,";
            sql += "'' as shopname, ";
            sql += "l.premise_name1 as premisename, ";
            sql += "'' as shoparea, ";
            sql += "0 as totalamount, ";
            sql += " 0 as payinfonum, ";
            //sql += "---- start payinfo detail ";
            sql += "ai.invoice_number as transno, ";
            sql += "ail.invoice_line_number as invoicelinenum,";
            sql += "ail.charge_code as chargeitem, ";
            sql += "ail.charge_description as descr, ";
            sql += "ail.invoice_amount as amount, ";
            sql += " ail.outstanding_amount as outstanding, ";
            sql += " ai.payment_due_date as duedate ";
            //sql += " ---- end payinfo detail ";
            sql += "from ar_invoice ai, ar_invoice_line ail, ar_customer ac,lm_lease l,ar_customer_site acs ";
            sql += " where 1 = 1 ";

            sql += " and ai.invoice_number = ail.invoice_number ";
            sql += " and Upper(SUBSTR(ail.PAY_STATUS, 1, 1)) in ('U', 'P') and ail.outstanding_amount > 0 ";
            sql += " and ai.lease_number = trim(l.lease_number) and l.status = 'A' and l.active = 'A' ";
            sql += " and ai.customer_number = ac.customer_number and ac.customer_number = acs.customer_number ";
            sql += " and trim(l.site_number) = trim(acs.site_number) ";
            sql += " and ai.lease_number ='" + leaseNum + "'";

            CRUD biz=new CRUD(DbVendor.Oracle,Runtime.OracleConnStr,false);
            try
            {
                DataTable dt = biz.ExecuteDataTable(sql);
                dt.TableName = "PayInfo";
                biz.Commit();

                return dt;
            }
            catch (Exception)
            {
                biz.Abort();
                throw;
            }
        }
    }
}