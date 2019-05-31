using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace POneIntf.Model
{
    /// <summary>
    /// Summary description for M_Log
    /// </summary>
    public class M_Sys_Log
    {
        public const string TableName = "SYS_Log";

        public int LogId;
        public int Type;
        public string UserId;
        public string Msg;
        public DateTime CreateDate;
    }
}
