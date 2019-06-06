using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Web;

namespace POneIntf.BLL
{
    public class Bizhub
    {
        /// <summary>
        /// 获取receipt实体
        /// </summary>
        /// <param name="receiptNum"></param>
        /// <param name="dbiz"></param>
        /// <returns></returns>
        public static M_Receipt GetReceiptByNum(string receiptNum, BLL.CRUD dbiz)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * from AR_RECEIPT ");
            strSql.Append(" where RECEIPT_NUMBER='" + receiptNum + "'");
            M_Receipt ety = new M_Receipt();
            DataTable dt = dbiz.ExecuteDataTable(strSql.ToString());

            ety.RECEIPT_NUMBER = receiptNum;
            if (dt.Rows.Count > 0)
            {
                ety.POST_STATUS = dt.Rows[0]["POST_STATUS"].ToString();
                ety.RECEIPT_DATE = dt.Rows[0]["RECEIPT_DATE"].ToString();
                ety.RECEIPT_NUMBER = dt.Rows[0]["RECEIPT_NUMBER"].ToString();
                ety.PAYMENT_METHOD = dt.Rows[0]["PAYMENT_METHOD"].ToString();
                ety.CHEQUE_NUMBER = dt.Rows[0]["CHEQUE_NUMBER"].ToString();
                ety.RECEIPT_TYPE = dt.Rows[0]["RECEIPT_TYPE"].ToString();
                ety.CUSTOMER_NUMBER = dt.Rows[0]["CUSTOMER_NUMBER"].ToString();
                ety.SITE_NUMBER = dt.Rows[0]["SITE_NUMBER"].ToString();
                ety.GL_DATE = dt.Rows[0]["GL_DATE"].ToString();
                ety.POSTING_DATE = dt.Rows[0]["POSTING_DATE"].ToString();
                ety.BANK_IN_DATE = dt.Rows[0]["BANK_IN_DATE"].ToString();
                ety.CURRENCY = dt.Rows[0]["CURRENCY"].ToString();
                if (dt.Rows[0]["RATE"].ToString() != "")
                {
                    ety.RATE = double.Parse(dt.Rows[0]["RATE"].ToString());
                }
                if (dt.Rows[0]["AMOUNT"].ToString() != "")
                {
                    ety.AMOUNT = double.Parse(dt.Rows[0]["AMOUNT"].ToString());
                }
                ety.DESCRIPTION = dt.Rows[0]["DESCRIPTION"].ToString();
                ety.RECEIVING_COMPANY_CODE = dt.Rows[0]["RECEIVING_COMPANY_CODE"].ToString();
                ety.PROPERTY = dt.Rows[0]["PROPERTY"].ToString();
                ety.APPLICATION_STATUS = dt.Rows[0]["APPLICATION_STATUS"].ToString();
                ety.RECEIPT_STATUS = dt.Rows[0]["RECEIPT_STATUS"].ToString();
                if (dt.Rows[0]["PRINT"].ToString() != "")
                {
                    ety.PRINT = double.Parse(dt.Rows[0]["PRINT"].ToString());
                }
                ety.PRINT_DATE = dt.Rows[0]["PRINT_DATE"].ToString();
                ety.BANK_ACCOUNT = dt.Rows[0]["BANK_ACCOUNT"].ToString();
                ety.CREATED_BY = dt.Rows[0]["CREATED_BY"].ToString();
                ety.CREATION_DATE = dt.Rows[0]["CREATION_DATE"].ToString();
                ety.LAST_UPDATED_BY = dt.Rows[0]["LAST_UPDATED_BY"].ToString();
                ety.LAST_UPDATED_DATE = dt.Rows[0]["LAST_UPDATED_DATE"].ToString();
                ety.BILLING_COMPANY_CODE = dt.Rows[0]["BILLING_COMPANY_CODE"].ToString();
                ety.APPROVAL_STATUS = dt.Rows[0]["APPROVAL_STATUS"].ToString();
                ety.ACTIVE = dt.Rows[0]["ACTIVE"].ToString();
                ety.VOID_DATE = dt.Rows[0]["VOID_DATE"].ToString();
                ety.PDC_RECEIPT_DATE = dt.Rows[0]["PDC_RECEIPT_DATE"].ToString();
                ety.IDENTIFIED_DATE = dt.Rows[0]["IDENTIFIED_DATE"].ToString();
                ety.IND_VOID_RECEIPT_ON_STATEMENT = dt.Rows[0]["IND_VOID_RECEIPT_ON_STATEMENT"].ToString();
                ety.RECEIPT_SOURCE = dt.Rows[0]["RECEIPT_SOURCE"].ToString();
                ety.PAYER = dt.Rows[0]["PAYER"].ToString();
                if (dt.Rows[0]["OVERPLUS_AMOUNT"].ToString() != "")
                {
                    ety.OVERPLUS_AMOUNT = double.Parse(dt.Rows[0]["OVERPLUS_AMOUNT"].ToString());
                }
                ety.LEASE_NUMBER = dt.Rows[0]["lease_number"].ToString();
                return ety;
            }
            else
            {
                return null;
            }
        }

        public static int FetchPaymentId(BLL.CRUD dbiz)
        {
            string sql = "select max(PaymentId) from T_Payment ";
            int id = Common.Helper.DbNull2Int(dbiz.ExecuteDataTable(sql).Rows[0][0]);            
            return id;
        }
    }
}