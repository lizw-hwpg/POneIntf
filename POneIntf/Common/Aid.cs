using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Reflection;

namespace POneIntf.Common
{
    public class Aid
    {
        public static double MyRound(double value, int decimals)
        {
            return Math.Round(value, decimals, MidpointRounding.AwayFromZero);
        }

        public static string GetPaymentNm(string code)
        {
            switch (code)
            {
                case "100":
                    return "PPS";
                default:
                    return "";
            }
        }
        /// <summary>
        /// 日期字符串转换为yyyy-mm-dd格式
        /// </summary>
        /// <param name="dateStr"></param>
        /// <returns></returns>
        public static string DateFmt10(string dateStr)
        {
            DateTime d = DateTime.Parse(dateStr);
            return d.ToString("yyyy-MM-dd");
        }
        /// <summary>
        /// 取数据库表字段值（带transaction）
        /// </summary>
        /// <param name="sqlStr"></param>
        /// <param name="db">使用transaction的数据库连接</param>
        /// <returns></returns>
        public static string GetSingleValue(string sqlStr, BLL.CRUD db)
        {
            System.Data.DataTable dt = db.ExecuteDataTable(sqlStr);
            if (dt.Rows.Count > 0)
            {
                return Common.Helper.DbNull2Str(dt.Rows[0][0]);
            }
            else
                return "";
        }

        public static string DateFmt(DateTime d, string fmt)
        {
            return d.ToString(fmt);
        }

        public static string EstrSql(string exp)
        {
            if (!string.IsNullOrEmpty(exp))
                exp = exp.Replace("'", "''");
            return exp;
        }

        /// <summary>
        /// 为datatable加一列rowid
        /// </summary>
        /// <param name="dt"></param>
        public static void AttachRowId(System.Data.DataTable dt)
        {
            dt.Columns.Add("rowid", typeof(int));
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                dt.Rows[i]["rowid"] = i + 1;
            }
        }

        /// <summary>
        /// DataTable转List<T>,list中field命名必须与datatable中字段名一致
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static object DT2List<T>(System.Data.DataTable dt)
        {
            List<T> list = new List<T>();

            FieldInfo[] fields = null;            
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                T obj = (T)Activator.CreateInstance(typeof(T));

                fields = obj.GetType().GetFields();
                foreach (FieldInfo fi in fields)
                {
                    switch (Type.GetTypeCode(fi.FieldType))
                    {
                        case TypeCode.Int16:
                        case TypeCode.Int32:
                            fi.SetValue(obj, Convert.ToInt32(dt.Rows[i][fi.Name]));
                            break;
                        case TypeCode.Decimal:
                            fi.SetValue(obj, Convert.ToDecimal(dt.Rows[i][fi.Name]));
                            break;
                        default:
                            fi.SetValue(obj, Convert.ToString(dt.Rows[i][fi.Name]));
                            break;
                    }
                }
                list.Add(obj);
            }
            return list;
        }

        /// <summary>
        /// 获取 Sys_Login_Account 登录用户信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static Model.M_Sys_Login_Account GetLoginAcc(string id)
        {
            BLL.CRUD biz = new BLL.CRUD(DbVendor.Oracle, Runtime.OracleConnStrLocal, false);

            try
            {
                BLL.Clause c = new BLL.Clause();
                c.Add("trim(LoginName)", id);
                List<Model.M_Sys_Login_Account> etylist = biz.Retrieve<Model.M_Sys_Login_Account>(c.Get());
                biz.Commit();

                if (etylist.Count > 0)
                {
                    return etylist[0];
                }
                else
                    return new Model.M_Sys_Login_Account();
            }
            catch (Exception)
            {
                biz.Abort();
                throw;
            }         
        }
    }
}