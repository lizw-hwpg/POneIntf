using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using POneIntf.Common;
namespace POneIntf.Model
{
    #region 3.1 用户登录校验
    public class Req31 : ReqBase
    {
        public Req31Data data = new Req31Data();
    }
    public class Req31Data
    {
        public string username { get; set; }
        public string password { get; set; }
        public string logintype { get; set; }
    }

    public class Rsp31 : RspBase
    {
        public Rsp31Data data = new Rsp31Data();
    }
    public class Rsp31Data
    {
        public string result;
        public string message;
        public int userid;
        public string status;
        public int leasecount;
        public string lastlogin;
        public List<string> leaseinfo = new List<string>();
    }
    #endregion

    #region 3.2 用户忘记密码
    public class Req32 : ReqBase
    {
        public Req32_1 data = new Req32_1();
    }
    public class Req32_1
    {
        public string userid; //这里是指租约号
        public string logintype = "";
    }

    public class Rsp32 : RspBase
    {
        public Rsp32_1 data = new Rsp32_1();
    }
    public class Rsp32_1
    {
        public string result = "";
        public string message = "";
        //public string temppsw = "";
        public string checkcode = "";
        public string email = "";
        public string oldpsw = "";
    }
    #endregion

    #region 3.3 用户更改密码

    public class Req33 : ReqBase
    {
        public Req33data data = new Req33data();
    }
    public class Req33data
    {
        public string userid;
        public string logintype;
        public string oldpsw;
        public string newpsw;
    }

    public class Rsp33 : RspBase
    {
        public Rsp33data data = new Rsp33data();
    }
    public class Rsp33data
    {
        public string result = "";
        public string message = "";
    }
    #endregion

    #region 3.4 获取待支付账单信息
    public class Req34 : ReqBase
    {
        public Req34data data;
    }
    public class Req34data
    {
        public string leasenum = "";
    }

    /// <summary>
    ///待支付账单信息
    /// </summary>
    public class Rsp34 : RspBase
    {
        public Rsp34data data = new Rsp34data();
    }
    /// <summary>
    /// 待支付账单信息主类
    /// </summary>
    public class Rsp34data
    {
        public string result;
        public string message;

        public string customername;
        public string shopname;
        public string premisename;
        public string shoparea;
        public decimal totalamount;
        public int payinfonum;

        public List<Rsp34dataDtl> payinfo = new List<Rsp34dataDtl>();
    }
    /// <summary>
    /// 待支付账单信息明细类
    /// </summary>
    public class Rsp34dataDtl
    {
        public int rowid;
        public string transno;      //=>invoice_number
        public string chargeitem;   // =>chargecode
        public string descr;
        public decimal amount;
        public decimal outstanding;
        public string duedate;
        public string invoicelinenum;
    }

    #endregion

    #region 3.5 通知后台支付结果，后台以此请求参数创建Receipt    
    public class Req35 : ReqBase
    {
        public Req35data data;
    }
    public class Req35data //通知后台支付结果请求类
    {
        public string leasenum = "";         //租约号
        public double actualamount = 0;    //实际支付总额
        /// <summary>
        /// 实际支付类型100-PPS,200-支付宝,300-银联,400-微信支付,900-其他支付方式
        /// </summary>
        public string actualpaytype = "";
        public string receivecompanycode = "";  //required 对应界面上的收款公司
        public string currencycode = "";        //required
        public string bankaccount = "";         //required
        public string customercode = "";        //required
        public string actualpaydate = "";

        public string status = ""; //支付状态:10-Send Payment Request(发起支付请求);20-Payment Enquiry(支付结果查询);30-Process Reply Success (支付成功);40- Process Reply AP Fail(支付失败)
        public string ppsreferenceno = "";
        public string ppsmerchantid = "";
        public string ppstxcode = "";
        public string ppsamount = "";
        public string ppsopcode = "";
        public string ppspayfor = "";
        public string ppslocale = "";
        public string ppsuserdata = "";
        public string ppssiteid = "";
        public string ppsstatuscode = ""; //“AP”=approved;“RJ”=rejected;“CC”=cancelled;“NF”=not found;“IP”=in progress
        public string ppsresponsecode = "";
        public string ppsbankaccount = "";
        public string ppsvaluedate = "";
        public string ppstxdate = "";
        public string ppspostid = "";
        public string ppsisn = "";
        public string ppswholemessage = "";

        public List<Req35dataDtl> actualpayinfo;
    }
    public class Req35dataDtl //支付结果明细类
    {
        /// <summary>
        /// 流水号(invoice_number)
        /// </summary>
        public string transno = "";
        public double amount = 0;             //应支付总额
        public double actualpay = 0;          //实际支付金额
        public string chargecode = "";      //required
        public int invoicelinenum = 0;       //required
    }

    public class Rsp35 : RspBase
    {
        public Rsp35data data = new Rsp35data();
    }
    public class Rsp35data //返回明细类
    {
        public string result;
        public string message;
    }
    #endregion

    #region RNR基类
    public class ReqBase
    {
        public string from { get; set; }
        public string timestamp { get; set; }
        public string nonce { get; set; }
        public string sign { get; set; }
    }
    public class RspBase
    {
        /// <summary>
        /// true：请求成功 false：请求失败。
        /// </summary>
        public string status = "true";

        /// <summary>
        /// 请求结果的描述，如果失败，则为失败原因。
        /// </summary>
        public string msg = "请求成功";

        /// <summary>
        /// 接口调用状态码，100-成功200-失败。
        /// </summary>
        public string code = "100";

        /// <summary>
        /// 服务器返回处理完的时间戳，数据格式：2015-06-30 12:50:20。非必要
        /// </summary>
        public string time = Helper.Today;

        public string raw { get; set; }
    }
    #endregion
}