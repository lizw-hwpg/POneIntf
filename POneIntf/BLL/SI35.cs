using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Web;
using System.Data;

using POneIntf.Common;
namespace POneIntf.BLL
{
    public class SI35 : IHandleReq
    {
        Model.Req35 req = null;
        M_T_PAYMENT etyH = null;
        M_T_PAYMENT_INFO etyD = null;

        #region IHandleReq 成员

        public string InvokeRequest(string postStr)
        {
            Model.Rsp35 rsp = new Model.Rsp35();

            try
            {
                Helper.LogWrite("SI35 Request:" + postStr);
                this.req = Helper.JsonDeserialize<Model.Req35>(postStr);

                /// 0 在本地保存支付信息
                this.Save();
                /// 1 创建receipt
                CreateReceipt();
                /// 2 更新本地支付信息
                M_T_PAYMENT ety = this.GetPayInfo(etyH.paymentid);
                ety.status = "1";
                this.UpdatePayInfo(ety);
                /// 3 创建分录
            }
            catch (Exception)
            {
                throw;
            }

            rsp.raw = Common.Helper.JsonSerialize(rsp);
            return Common.Helper.JsonSerialize(rsp);
        }

        #endregion

        private void CreateDisribution(string reNum,string reType)
        {
            BLL.CRUD dbiz = new CRUD(DbVendor.Oracle, Runtime.OracleConnStr, true);
            try
            {
                COA c = new COA(dbiz);
                ArrayList al = c.Generate_Distribution(reNum, reType);
                string strType = "R";
                if (reType=="UD")               
                    strType = "U";
               
                dbiz.ExecuteNonQuery("delete from AR_Line_Distribution where TRANSACTION_NO='" + reNum + "' and TRANSACTION_TYPE='" + strType + "' and post_status<>'P'");
                c.Insert_Distribution(al);
                dbiz.Commit();
            }
            catch (Exception)
            {
                dbiz.Abort();
                throw;
            }
        }

        private M_T_PAYMENT GetPayInfo(int id)
        {
            CRUD dbiz = new CRUD(DbVendor.Oracle, Runtime.OracleConnStrLocal, false);

            try
            {
                Clause c = new Clause();
                c.Add(" paymentid", id.ToString());
                List<M_T_PAYMENT> list= dbiz.Retrieve<M_T_PAYMENT>(c.Get());
                return list[0];
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void UpdatePayInfo(M_T_PAYMENT ety)        
        {
            CRUD dbiz = new CRUD(DbVendor.Oracle, Runtime.OracleConnStrLocal, true);
            try
            {
                dbiz.Update(ety, "paymentid");
                dbiz.Commit();
            }
            catch (Exception)
            {
                dbiz.Abort();
                throw;
            }
        }

        /// <summary>
        /// 创建receipt后保存支付信息
        /// </summary>
        private void Save()
        {
            BLL.CRUD biz = new CRUD(DbVendor.Oracle, Runtime.OracleConnStrLocal, true);           
            try
            {
                etyH = new M_T_PAYMENT();
                etyH.paymentid = Bizhub.FetchPaymentId(biz) + 1;
                etyH.amount = this.req.data.actualamount;
                etyH.leasenumber = this.req.data.leasenum;
                etyH.paydate = DateTime.Parse(this.req.data.actualpaydate);
                etyH.paytype = this.req.data.actualpaytype;
                etyH.status = "0"; // Status取值定义:0=本地保存完成;1=Receipt创建完成;2=分录创建完成。状态必须从0~2顺序转换。
                biz.Create(etyH);              

               
                Model.Req35dataDtl detail = null;
                etyD = new M_T_PAYMENT_INFO();
                for (int i = 0; i < this.req.data.actualpayinfo.Count; i++)
                {
                    detail = this.req.data.actualpayinfo[i];

                    etyD.actualpay = detail.actualpay;
                    etyD.amount = detail.amount;
                    etyD.chargecode = detail.chargecode;
                    etyD.invoicenumber = detail.transno;
                    etyD.invoicelinenum = detail.invoicelinenum;
                    etyD.paymentid = etyH.paymentid;
                    biz.Create(etyD);
                }
                biz.Commit();
            }
            catch (Exception err)
            {
                biz.Abort();
                throw err;
            }
        }

        private void CreateReceipt()
        {
            CRUD dbiz=new CRUD(DbVendor.Oracle,Runtime.OracleConnStr,true);
            try
            {
                Receipt r = new Receipt(dbiz);
                r.Create(this.req.data);
                
                dbiz.Commit();
            }
            catch (Exception)
            {
                dbiz.Abort();
                throw;
            }
        }
    }
}