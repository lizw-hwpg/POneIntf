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