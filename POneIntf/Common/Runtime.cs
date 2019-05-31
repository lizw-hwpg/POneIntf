using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace POneIntf.Common
{
    public class Runtime
    {
        private static System.Collections.Specialized.NameValueCollection nvc = null;

        public static string StartPath { get; set; }
        public static string OracleConnStr { get { return nvc["oracleConnString"]; } }
        public static string OracleConnStrLocal { get { return nvc["oracleConnStringLocal"]; } }

        public static string CurrentUser { get; set; }
        public static void Init(System.Collections.Specialized.NameValueCollection config, string path)
        {
            nvc = config;
            StartPath = path;
        }        
    }
}