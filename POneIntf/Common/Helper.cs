using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Runtime.Serialization.Json;

namespace POneIntf.Common
{
    public class Helper
    {
        public enum DateFmt
        {
            /// <summary>
            /// yyyyMMdd
            /// </summary>
            Simple = 0,

            /// <summary>
            /// yyyy-mm-dd
            /// </summary>
            Standard = 1,

            /// <summary>
            /// yyyy/mm/dd 
            /// </summary>
            StandardSlash = 2,

            /// <summary>
            /// yyyy-mm-dd HH:MM:ss 
            /// </summary>
            Wholly = 3,

            /// <summary>
            /// yyyy-MM-dd HH:mm:ss.fff 带毫秒
            /// </summary>
            WhollyMs = 4
        }

        public static string Today
        {
            get { return DateTime.Now.ToString("yyyy-MM-dd H:mm:ss"); }
        }

        public static void LogWrite(string txt)
        {
            try
            {
                string dir = Common.Runtime.StartPath + "\\Log\\";
                if (!Directory.Exists(dir))
                    Directory.CreateDirectory(dir);

                FileInfo fi = new FileInfo(dir + Date2Str(DateTime.Now, DateFmt.Standard) + ".log");

                StreamWriter sw = null;
                if (!fi.Exists)
                    sw = fi.CreateText();
                else
                    sw = fi.AppendText();

                sw.WriteLine("[" + Date2Str(DateTime.Now,DateFmt.WhollyMs) + "] " + txt);
                sw.Flush();
                sw.Close();
            }
            catch
            {
                throw;
            }
        }

        public static string GetRequestJson(Stream s)
        {
            StreamReader sRead = new StreamReader(s, System.Text.Encoding.UTF8);
            string reqStr = sRead.ReadToEnd();
            sRead.Close();
            return reqStr;
        }
        
        /// <summary>
        /// 日期转指定形式的字符串
        /// </summary>
        /// <param name="val"></param>
        /// <param name="given"></param>
        /// <returns></returns>
        public static string Date2Str(DateTime val, DateFmt given)
        {
            switch (given)
            {
                case DateFmt.Simple:
                    return val.ToString("yyyyMMdd");
                case DateFmt.Standard:
                    return val.ToString("yyyy-MM-dd");
                case DateFmt.StandardSlash:
                    return val.ToString("yyyy/MM/dd");
                case DateFmt.Wholly:
                    return val.ToString("yyyy-MM-dd HH:mm:ss");
                case DateFmt.WhollyMs:
                    return val.ToString("yyyy-MM-dd HH:mm:ss.fff");
                default:
                    return val.ToString();
            }
        }

        /// <summary>
        /// Serialize with DataContractJsonSerializer
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string Serialize<T>(T obj)
        {
            MemoryStream ms = new MemoryStream();
            DataContractJsonSerializer dcjs = new DataContractJsonSerializer(typeof(T));
            dcjs.WriteObject(ms, obj);
            byte[] bytes = new byte[ms.Length];
            ms.Position = 0;
            ms.Read(bytes, 0, (int)ms.Length);
            string outStr = System.Text.Encoding.UTF8.GetString(bytes);
            return outStr;
        }

        /// <summary>
        /// Deserialize with DataContractJsonSerializer
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="jsonStr"></param>
        /// <returns></returns>
        public static T Deserialize<T>(string jsonStr) where T : class
        {
            DataContractJsonSerializer dcjs = new DataContractJsonSerializer(typeof(T));
            MemoryStream ms = new MemoryStream(System.Text.Encoding.UTF8.GetBytes(jsonStr));
            T t = Activator.CreateInstance<T>();
            t = dcjs.ReadObject(ms) as T;
            return t;
        }

        /// <summary>
        /// Serialize with JavaScriptSerializer
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <returns></returns>
        public static string JsonSerialize<T>(T t) where T : class
        {
            System.Web.Script.Serialization.JavaScriptSerializer jss = new System.Web.Script.Serialization.JavaScriptSerializer();
            return jss.Serialize(t);
        }

        /// <summary>
        /// Deserialize with JavaScriptSerializer
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="jsonStr"></param>
        /// <returns></returns>
        public static T JsonDeserialize<T>(string jsonStr) where T : class
        {
            System.Web.Script.Serialization.JavaScriptSerializer jss = new System.Web.Script.Serialization.JavaScriptSerializer();
            T t = jss.Deserialize<T>(jsonStr);
            return t;
        }

        public static string DateFmt19(DateTime d)
        {
            return d.ToString("yyyy-MM-dd H:mm:ss");
        }

        /// <summary>
        /// 安全转换为字符串
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string DbNull2Str(object value)
        {
            if (value != null)
                return value.ToString();
            else
                return "";
        }

        /// <summary>
        /// 安全转换为Integer
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static int DbNull2Int(object value)
        {

            if (value == "" || value == DBNull.Value)
            {
                return 0;
            }
            return Convert.ToInt32(value);
        }

        public static double DbNull2Doub(object value)
        {
            if (value == DBNull.Value || value == null)
            {
                return 0;
            }
            else
                return Convert.ToDouble(value);
        }

        /// <summary>
        /// 安全转换为Decimal
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static decimal DbNull2Dec(object value)
        {
            if (value == DBNull.Value || value == null)
            {
                return 0;
            }
            else
                return Convert.ToDecimal(value);
        }

       
    }
}