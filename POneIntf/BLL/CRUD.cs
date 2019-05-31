using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Reflection;
using System.Data;

using POneIntf.Common;
namespace POneIntf.BLL
{
    public class Clause
    {
        public Dictionary<string, object> m_values = new Dictionary<string, object>();

        public void Add(string key, object value)
        {
            this.m_values.Add(key, value);
        }
    }

    public class CRUD_o1 : DbUtil_o1
    {      
        public CRUD_o1(string connStr, bool openTx):base(connStr,openTx){}

        public int Create<T>(T ety)
        {
            string cols = "";
            string vals = "";
            try
            {
                FieldInfo[] fis = ety.GetType().GetFields();
                string sql = "";
                sql += "insert into " + ety.GetType().GetField("TableName").GetValue(ety).ToString();

                foreach (FieldInfo fi in fis)
                {
                    /// 生成字段列表
                    if (fi.Name == "TableName")
                        continue;

                    if (Type.GetTypeCode(fi.FieldType) == TypeCode.DateTime)
                    {
                        DateTime dObj = (DateTime)fi.GetValue(ety);
                        if (dObj.Year == 1)
                            continue;
                    }
                    cols += fi.Name + ",";

                    /// 生成值列表
                    switch (Type.GetTypeCode(fi.FieldType))
                    {
                        case TypeCode.Boolean:
                            break;
                        case TypeCode.DateTime:
                            if (fi.GetValue(ety) != null || fi.GetValue(ety) != DBNull.Value)
                            {
                                DateTime dObj = (DateTime)fi.GetValue(ety);
                                if (dObj.Year != 1) //如果不等1说明不是默认值，需要赋值，否则不需要
                                {
                                    vals += "to_date('" + Helper.DateFmt19(dObj) + "','yyyy-mm-dd hh24:mi:ss'),";
                                }
                            }
                            break;
                        case TypeCode.Decimal:
                        case TypeCode.Double:
                        case TypeCode.Int16:
                        case TypeCode.Int32:
                        case TypeCode.Int64:
                        case TypeCode.SByte:
                        case TypeCode.Single:
                        case TypeCode.UInt16:
                        case TypeCode.UInt32:
                        case TypeCode.UInt64:
                            vals += Helper.DbNull2Dec(fi.GetValue(ety)).ToString() + ",";
                            break;
                        default:
                            vals += "'" + Helper.DbNull2Str(fi.GetValue(ety)) + "',";
                            break;
                    }
                }
                cols = cols.Substring(0, cols.Length - 1);
                vals = vals.Substring(0, vals.Length - 1);
                sql += "(" + cols + ")" + " values (" + vals + ")";
                int cnt = this.ExecuteNonQuery(sql);
                return cnt;
            }
            catch (Exception err)
            {
                throw err;
            }
        }

        public List<T> Retrieve<T>(Dictionary<string, object> clause)
        {
            T obj = Activator.CreateInstance<T>();
            FieldInfo[] fis = obj.GetType().GetFields();
            string sql = "select * from " + obj.GetType().GetField("TableName").GetValue(obj).ToString() + " ";
            sql += " where 1=1";

            foreach (var item in clause)
            {
                switch (Type.GetTypeCode(item.Value.GetType()))
                {
                    case TypeCode.Double:
                    case TypeCode.Decimal:
                    case TypeCode.Int16:
                    case TypeCode.Int32:
                    case TypeCode.Int64:
                        sql += " and " + item.Key + "=" + item.Value.ToString() + "";
                        break;
                    default:
                        sql += " and " + item.Key + "='" + item.Value.ToString() + "'";
                        break;
                }
            }
            List<T> etys = this.ExecuteList<T>(sql, null);
            return etys;
        }

        public static int Update<T>(T ety, List<string> keys)
        {
            try
            {
                DbUtil_o1 db = new Common.DbUtil_o1(Runtime.OracleConnStrLocal, true);

                FieldInfo[] fis = ety.GetType().GetFields();
                string sql = "";
                sql += "update " + ety.GetType().GetField("TableName").GetValue(ety).ToString() + " ";
                sql += " set ";
                foreach (FieldInfo fi in fis)
                {
                    if (fi.Name == "TableName")
                        continue;

                    if (!keys.Contains(fi.Name)) //如果不是关键字段
                    {
                        switch (Type.GetTypeCode(fi.FieldType))
                        {
                            case TypeCode.DateTime:
                                sql += fi.Name + "=to_date('" + Helper.DateFmt19(DateTime.Parse(fi.GetValue(ety).ToString())) + "','yyyy-mm-dd hh24:mi:ss'),";
                                break;
                            case TypeCode.Decimal:
                            case TypeCode.Double:
                            case TypeCode.Int16:
                            case TypeCode.Int32:
                                sql += fi.Name + "=" + Helper.DbNull2Str(fi.GetValue(ety)) + ",";
                                break;
                            default:
                                sql += fi.Name + "='" + Helper.DbNull2Str(fi.GetValue(ety)) + "',";
                                break;
                        }
                    }
                }
                sql = sql.Substring(0, sql.Length - 1);
                sql += " where 1=1 ";
                FieldInfo fiInfo = null;
                for (int i = 0; i < keys.Count; i++)
                {
                    fiInfo = ety.GetType().GetField(keys[i]);
                    switch (Type.GetTypeCode(fiInfo.FieldType))
                    {
                        case TypeCode.DateTime:
                            sql += " and " + fiInfo.Name + "='" + Helper.DbNull2Str(fiInfo.GetValue(ety)) + "',";
                            break;
                        case TypeCode.Decimal:
                        case TypeCode.Double:
                        case TypeCode.Int16:
                        case TypeCode.Int32:
                            sql += " and " + fiInfo.Name + "=" + Helper.DbNull2Str(fiInfo.GetValue(ety)) + ",";
                            break;
                        default:
                            sql += " and " + fiInfo.Name + "='" + Helper.DbNull2Str(fiInfo.GetValue(ety)) + "',";
                            break;
                    }
                }
                sql = sql.Substring(0, sql.Length - 1);
                return db.ExecuteNonQuery(sql);
            }
            catch (Exception err)
            {
                throw err;
            }
        }

        public static int Update<T>(DataRow dr, params string[] keys)
        {
            if (keys.Length==0)
            {
                throw new Exception("primary key not exists,can not execute update statement!");
            }

            DbUtil_o1 db = new DbUtil_o1(Runtime.OracleConnStrLocal, true);
            try
            {
                object obj = Activator.CreateInstance(typeof(T));
                FieldInfo[] fis = obj.GetType().GetFields();

                string sql = "";
                sql += "update " + obj.GetType().GetField("TableName").GetValue(obj).ToString() + " ";
                sql += " set ";
                foreach (FieldInfo fi in fis)
                {
                    if (fi.Name == "TableName")
                        continue;

                    if (!keys.Contains(fi.Name)) //如果不是关键字段
                    {
                        switch (Type.GetTypeCode(fi.FieldType))
                        {
                            case TypeCode.DateTime:
                                sql += fi.Name + "=to_date('" + Helper.DateFmt19(DateTime.Parse(dr[fi.Name].ToString())) + "','yyyy-mm-dd hh24:mi:ss'),";
                                break;
                            case TypeCode.Decimal:
                            case TypeCode.Double:
                            case TypeCode.Int16:
                            case TypeCode.Int32:
                                sql += fi.Name + "=" + Helper.DbNull2Str(dr[fi.Name]) + ",";
                                break;
                            default:
                                sql += fi.Name + "='" + Helper.DbNull2Str(dr[fi.Name]) + "',";
                                break;
                        }
                    }
                }
                sql = sql.Substring(0, sql.Length - 1);

                sql += " where 1=1 ";
                FieldInfo fiInfo = null;
                for (int i = 0; i < keys.Length; i++)
                {
                    fiInfo = obj.GetType().GetField(keys[i]);
                    switch (Type.GetTypeCode(fiInfo.FieldType))
                    {
                        case TypeCode.DateTime:
                            sql += " and " + fiInfo.Name + "='" + Helper.DbNull2Str(dr[fiInfo.Name]) + "',";
                            break;
                        case TypeCode.Decimal:
                        case TypeCode.Double:
                        case TypeCode.Int16:
                        case TypeCode.Int32:
                            sql += " and " + fiInfo.Name + "=" + Helper.DbNull2Str(dr[fiInfo.Name]) + ",";
                            break;
                        default:
                            sql += " and " + fiInfo.Name + "='" + Helper.DbNull2Str(dr[fiInfo.Name]) + "',";
                            break;
                    }
                }
                sql = sql.Substring(0, sql.Length - 1);
                int cnt = db.ExecuteNonQuery(sql);
                db.Commit();
                return cnt;
            }
            catch (Exception err)
            {
                db.Abort();
                throw err;
            }
        }
    }

    public class CRUD: DbUtil
    {
        public CRUD(DbVendor vendor, string connstr, bool openTx) : base(vendor, connstr, openTx) { }

        public int Create<T>(T ety)
        {
            string cols = "";
            string vals = "";
            try
            {
                FieldInfo[] fis = ety.GetType().GetFields();
                string sql = "";
                sql += "insert into " + ety.GetType().GetField("TableName").GetValue(ety).ToString();

                foreach (FieldInfo fi in fis)
                {
                    /// 生成字段列表
                    if (fi.Name == "TableName")
                        continue;

                    if (Type.GetTypeCode(fi.FieldType) == TypeCode.DateTime)
                    {
                        DateTime dObj = (DateTime)fi.GetValue(ety);
                        if (dObj.Year == 1)
                            continue;
                    }
                    cols += fi.Name + ",";

                    /// 生成值列表
                    switch (Type.GetTypeCode(fi.FieldType))
                    {
                        case TypeCode.Boolean:
                            break;
                        case TypeCode.DateTime:
                            if (fi.GetValue(ety) != null || fi.GetValue(ety) != DBNull.Value)
                            {
                                DateTime dObj = (DateTime)fi.GetValue(ety);
                                if (dObj.Year != 1) //如果不等1说明不是默认值，需要赋值，否则不需要
                                {
                                    vals += "to_date('" + Helper.DateFmt19(dObj) + "','yyyy-mm-dd hh24:mi:ss'),";
                                }
                            }
                            break;
                        case TypeCode.Decimal:
                        case TypeCode.Double:
                        case TypeCode.Int16:
                        case TypeCode.Int32:
                        case TypeCode.Int64:
                        case TypeCode.SByte:
                        case TypeCode.Single:
                        case TypeCode.UInt16:
                        case TypeCode.UInt32:
                        case TypeCode.UInt64:
                            vals += Helper.DbNull2Dec(fi.GetValue(ety)).ToString() + ",";
                            break;
                        default:
                            vals += "'" + Helper.DbNull2Str(fi.GetValue(ety)) + "',";
                            break;
                    }
                }
                cols = cols.Substring(0, cols.Length - 1);
                vals = vals.Substring(0, vals.Length - 1);
                sql += "(" + cols + ")" + " values (" + vals + ")";
                int cnt = this.ExecuteNonQuery(sql);
                return cnt;
            }
            catch (Exception err)
            {
                throw err;
            }
        }

        /// <summary>
        /// 取数据，返回泛型列表。
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="clause"></param>
        /// <returns></returns>
        public List<T> Retrieve<T>(Dictionary<string, object> clause)
        {
            string sql = GenSelectSql<T>(clause);
            IDataReader reader = this.ExecuteReader(sql);

            List<T> etys = new List<T>();
            while (reader.Read())
            {
                T ety = Activator.CreateInstance<T>();
                foreach (FieldInfo fi in ety.GetType().GetFields())
                {
                    if (fi.Name == "TableName")
                        continue;
                    
                    switch (Type.GetTypeCode(fi.FieldType))
                    {
                        case TypeCode.Boolean:
                            break;
                        case TypeCode.DateTime:
                            fi.SetValue(ety, reader[fi.Name]);
                            break;
                        case TypeCode.Decimal:
                            fi.SetValue(ety, Convert.ToDecimal(reader[fi.Name]));
                            break;
                        case TypeCode.Int16:
                        case TypeCode.Int32:
                            fi.SetValue(ety,Convert.ToInt32(reader[fi.Name]));                      
                            break;
                        default:
                            fi.SetValue(ety, reader[fi.Name].ToString());
                            break;
                    }                    
                }
                etys.Add(ety);
            }
            return etys;
        }

        public DataTable Select<T>(Dictionary<string, object> clause)
        {
            string sql=GenSelectSql<T>(clause);
            return this.ExecuteDataTable(sql);
        }

        public int Update<T>(T ety, List<string> keys)
        {
            try
            {             
                FieldInfo[] fis = ety.GetType().GetFields();
                string sql = "";
                sql += "update " + ety.GetType().GetField("TableName").GetValue(ety).ToString() + " ";
                sql += " set ";
                foreach (FieldInfo fi in fis)
                {
                    if (fi.Name == "TableName")
                        continue;

                    if (!keys.Contains(fi.Name)) //如果不是关键字段
                    {
                        switch (Type.GetTypeCode(fi.FieldType))
                        {
                            case TypeCode.DateTime:
                                sql += fi.Name + "=to_date('" + Helper.DateFmt19(DateTime.Parse(fi.GetValue(ety).ToString())) + "','yyyy-mm-dd hh24:mi:ss'),";
                                break;
                            case TypeCode.Decimal:
                            case TypeCode.Double:
                            case TypeCode.Int16:
                            case TypeCode.Int32:
                                sql += fi.Name + "=" + Helper.DbNull2Str(fi.GetValue(ety)) + ",";
                                break;
                            default:
                                sql += fi.Name + "='" + Helper.DbNull2Str(fi.GetValue(ety)) + "',";
                                break;
                        }
                    }
                }
                sql = sql.Substring(0, sql.Length - 1);
                sql += " where 1=1 ";
                FieldInfo fiInfo = null;
                for (int i = 0; i < keys.Count; i++)
                {
                    fiInfo = ety.GetType().GetField(keys[i]);
                    switch (Type.GetTypeCode(fiInfo.FieldType))
                    {
                        case TypeCode.DateTime:
                            sql += " and " + fiInfo.Name + "='" + Helper.DbNull2Str(fiInfo.GetValue(ety)) + "',";
                            break;
                        case TypeCode.Decimal:
                        case TypeCode.Double:
                        case TypeCode.Int16:
                        case TypeCode.Int32:
                            sql += " and " + fiInfo.Name + "=" + Helper.DbNull2Str(fiInfo.GetValue(ety)) + ",";
                            break;
                        default:
                            sql += " and " + fiInfo.Name + "='" + Helper.DbNull2Str(fiInfo.GetValue(ety)) + "',";
                            break;
                    }
                }
                sql = sql.Substring(0, sql.Length - 1);
                return this.ExecuteNonQuery(sql);
            }
            catch (Exception err)
            {
                throw err;
            }
        }

        public int Update<T>(DataRow dr, params string[] keys)
        {
            if (keys.Length == 0)
            {
                throw new Exception("primary key not exists,can not execute update statement!");
            }
           
            try
            {
                object obj = Activator.CreateInstance(typeof(T));
                FieldInfo[] fis = obj.GetType().GetFields();

                string sql = "";
                sql += "update " + obj.GetType().GetField("TableName").GetValue(obj).ToString() + " ";
                sql += " set ";
                foreach (FieldInfo fi in fis)
                {
                    if (fi.Name == "TableName")
                        continue;

                    if (!keys.Contains(fi.Name)) //如果不是关键字段
                    {
                        switch (Type.GetTypeCode(fi.FieldType))
                        {
                            case TypeCode.DateTime:
                                sql += fi.Name + "=to_date('" + Helper.DateFmt19(DateTime.Parse(dr[fi.Name].ToString())) + "','yyyy-mm-dd hh24:mi:ss'),";
                                break;
                            case TypeCode.Decimal:
                            case TypeCode.Double:
                            case TypeCode.Int16:
                            case TypeCode.Int32:
                                sql += fi.Name + "=" + Helper.DbNull2Str(dr[fi.Name]) + ",";
                                break;
                            default:
                                sql += fi.Name + "='" + Helper.DbNull2Str(dr[fi.Name]) + "',";
                                break;
                        }
                    }
                }
                sql = sql.Substring(0, sql.Length - 1);

                sql += " where 1=1 ";
                FieldInfo fiInfo = null;
                for (int i = 0; i < keys.Length; i++)
                {
                    fiInfo = obj.GetType().GetField(keys[i]);
                    switch (Type.GetTypeCode(fiInfo.FieldType))
                    {
                        case TypeCode.DateTime:
                            sql += " and " + fiInfo.Name + "='" + Helper.DbNull2Str(dr[fiInfo.Name]) + "',";
                            break;
                        case TypeCode.Decimal:
                        case TypeCode.Double:
                        case TypeCode.Int16:
                        case TypeCode.Int32:
                            sql += " and " + fiInfo.Name + "=" + Helper.DbNull2Str(dr[fiInfo.Name]) + ",";
                            break;
                        default:
                            sql += " and " + fiInfo.Name + "='" + Helper.DbNull2Str(dr[fiInfo.Name]) + "',";
                            break;
                    }
                }
                sql = sql.Substring(0, sql.Length - 1);
                int cnt = this.ExecuteNonQuery(sql);             
                return cnt;
            }
            catch (Exception err)
            {              
                throw err;
            }
        }

        private string GenSelectSql<T>(Dictionary<string, object> clause)
        {
            T obj = Activator.CreateInstance<T>();
            FieldInfo[] fis = obj.GetType().GetFields();
            string sql = "select * from " + obj.GetType().GetField("TableName").GetValue(obj).ToString() + " ";
            sql += " where 1=1";

            foreach (var item in clause)
            {
                switch (Type.GetTypeCode(item.Value.GetType()))
                {
                    case TypeCode.Double:
                    case TypeCode.Decimal:
                    case TypeCode.Int16:
                    case TypeCode.Int32:
                    case TypeCode.Int64:
                        sql += " and " + item.Key + "=" + item.Value.ToString() + "";
                        break;
                    default:
                        sql += " and " + item.Key + "='" + item.Value.ToString() + "'";
                        break;
                }
            }

            return sql;
        }
    }
}