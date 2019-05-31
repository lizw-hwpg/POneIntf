using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace POneIntf
{
    /// <summary>
    /// gear 的摘要说明
    /// </summary>
    public class gear : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            Common.Runtime.Init(System.Configuration.ConfigurationManager.AppSettings,context.Request.MapPath("."));
            string outstr="";
            try
            {
                string act = context.Request.QueryString["act"];
                string postStr = Common.Helper.GetRequestJson(context.Request.InputStream);
                outstr = BLL.Entry.GetHandler(act).InvokeRequest(postStr);

            }
            catch (Exception err)
            { 
                Model.RspBase rsp = new Model.RspBase();
                rsp.code = "200";
                rsp.msg = err.Message;
                rsp.status = "true";
                rsp.time = Common.Helper.Today;
                outstr = Common.Helper.JsonSerialize(rsp);
            }
            context.Response.Write(outstr);
            context.Response.End();

            //context.Response.ContentType = "text/plain";
            //context.Response.Write("Hello World");
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }

        private void CheckDbConn()
        {
            Oracle.ManagedDataAccess.Client.OracleConnection conn = new Oracle.ManagedDataAccess.Client.OracleConnection(Common.Runtime.OracleConnStrLocal);
            conn.Open();
            if (conn.State == System.Data.ConnectionState.Open)
            {
                Common.Helper.LogWrite("Db success!");
            }
            else
            {
                Common.Helper.LogWrite("Db  failure!");
            }
            conn.Clone();
        }
    }
}