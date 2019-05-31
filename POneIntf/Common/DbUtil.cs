using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Reflection;
using Oracle.ManagedDataAccess.Client;
using System.Data;

namespace POneIntf.Common
{
    public enum DbVendor
    {
        Oracle,
        MySQL,
        Firebird,
        MSSQL
    }

    public class DbFactory
    {
        private System.Data.IDbConnection connection = null;
        private System.Data.IDbCommand command = null;

        public DbFactory(DbVendor vendor, string connstr, bool openTx)
        {

        }

        public int ExecuteNonQuery(string sql)
        {
            command = new OracleCommand(sql, this.connection as OracleConnection);
            return command.ExecuteNonQuery();
        }

        public void GetCommand(string sql)
        { }
    }

    public class DbUtil
    {
        private System.Data.IDbConnection connection = null;        
        private System.Data.IDbTransaction tranx = null;
        private DbVendor vendor;

        public DbUtil(DbVendor vendor, string connStr, bool openTx)
        {
            this.vendor = vendor;
            switch (vendor)
            {
                case DbVendor.Oracle:
                    this.connection = new OracleConnection(connStr);
                    break;
                case DbVendor.MySQL:
                    throw new Exception("MySQL not implemented!");
                case DbVendor.Firebird:
                    throw new Exception("Firebird not implemented!");
                case DbVendor.MSSQL:
                    throw new Exception("MSSQL not implemented!");
                default:
                    throw new Exception("Database vendor unknown!");
            }
            this.connection.Open();
            this.tranx = openTx ? this.connection.BeginTransaction() : null;
        }

        public int ExecuteNonQuery(string sql)
        {
            return this.GetCommand(this.vendor, sql).ExecuteNonQuery();
        }

        public System.Data.IDataReader ExecuteReader(string sql)
        {
            return GetCommand(this.vendor, sql).ExecuteReader();
        }

        public System.Data.DataTable ExecuteDataTable(string sql)
        {
            DataSet ds = new DataSet();
            this.GetDataAdapter(this.vendor, sql).Fill(ds);
            return ds.Tables[0];
        }

        /// <summary>
        /// 提交事务
        /// </summary>
        public void Commit()
        {
            try
            {
                if (this.tranx != null)
                    this.tranx.Commit();
            }
            catch
            {
                this.tranx.Rollback();
            }
            finally
            {
                if (this.connection.State == ConnectionState.Open)
                    this.connection.Close();
            }
        }

        /// <summary>
        /// 回滚事务
        /// </summary>
        public void Abort()
        {
            try
            {
                this.tranx.Rollback();
            }
            catch
            {
                if (this.connection.State == ConnectionState.Open)
                {
                    if (this.tranx != null)
                        this.tranx.Rollback();
                    this.connection.Close();
                }
            }
            finally
            {
                if (this.connection.State == ConnectionState.Open)
                {
                    this.connection.Close();
                }
            }
        }

        private IDbCommand GetCommand(DbVendor vendor, string sql)
        {
            switch (vendor)
            {
                case DbVendor.Oracle:
                    return new OracleCommand(sql, this.connection as OracleConnection);
                case DbVendor.MySQL:
                    throw new Exception("MySQL not implemented!");
                case DbVendor.Firebird:
                    throw new Exception("Firebird not implemented!");
                case DbVendor.MSSQL:
                    throw new Exception("MSSQL not implemented!");
                default:
                    throw new Exception("Database vendor unknown!");
            }
        }

        private IDataAdapter GetDataAdapter(DbVendor vendor, string sql)
        {
            switch (vendor)
            {
                case DbVendor.Oracle:
                    OracleCommand ocommand = GetCommand(vendor, sql) as OracleCommand;
                    return new OracleDataAdapter(ocommand);
                case DbVendor.MySQL:
                    throw new Exception("MySQL not implemented!");
                case DbVendor.Firebird:
                    throw new Exception("Firebird not implemented!");
                case DbVendor.MSSQL:
                    throw new Exception("MSSQL not implemented!");
                default:
                    throw new Exception("Database vendor unknown!");
            }
        }
    }

    /// <summary>
    /// Oracle数据库帮助类
    /// </summary>
    /// 添加字段的语法：alter table tablename add (column datatype [default value][null/not null],….);
    /// 修改字段的语法：alter table tablename modify (column datatype [default value][null/not null],….);
    /// 删除字段的语法：alter table tablename drop (column);
    public class DbUtil_o1
    {
        private OracleConnection connection = null;
        private OracleTransaction transxion = null;
      
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="openTrans">是否开启事务</param>
        public DbUtil_o1(bool openTrans)
        {
            try
            {
                this.connection = new OracleConnection(Common.Runtime.OracleConnStr);
                this.connection.Open();
                if (openTrans)
                    this.transxion = this.connection.BeginTransaction();
            }
            catch (Exception err)
            {
                throw new Exception("数据库连接异常,请检查连接配置!(" + err.Message + ")");
            }
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="connStr">指定的数据库连接字符串</param>
        /// <param name="openTrans">是否开启事务</param>
        /// The type initializer for 'Oracle.DataAccess.Client.OracleConnection' threw an exception
        public DbUtil_o1(string connStr, bool openTrans)
        {
            try
            {
                this.connection = new OracleConnection(connStr);
                this.connection.Open();
                if (openTrans)
                    this.transxion = this.connection.BeginTransaction();
            }
            catch (Exception err)
            {
                throw new Exception("数据库连接异常,请检查连接配置!(" + err.Message + ")");
            }
        }    

        /// <summary>
        /// 执行非结果集查询，返回影响的行数
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public int ExecuteNonQuery(string sql)
        {          
            OracleCommand command = new OracleCommand(sql, this.connection);
            return command.ExecuteNonQuery();
        }

        /// <summary>
        /// 执行结果集查询，返回数据实体列表
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        /// 1.数据库中的字段必须大于等于实体类对象中的字段
        /// 2.数据库字段名必须与实体类中定义的字段名相同 
        public List<T> ExecuteList<T>(string sql, params OracleParameter[] parameters)
        {
            DataTable dt = this.ExecuteDT(sql, parameters); //执行查询返回datatable对象

            List<T> list = new List<T>();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                object ety = Activator.CreateInstance(typeof(T)); //创建实体对象
                foreach (FieldInfo fi in ety.GetType().GetFields())
                {
                    if (fi.Name == "TableName") //如果是"TableName"字段则跳过
                        continue;

                    switch (Type.GetTypeCode(fi.FieldType))
                    {
                        case TypeCode.Double:
                            fi.SetValue(ety, Convert.ToDouble(dt.Rows[i][fi.Name]));
                            break;
                        case TypeCode.Decimal:
                            fi.SetValue(ety, Convert.ToDecimal(dt.Rows[i][fi.Name]));
                            break;
                        case TypeCode.Int32:
                            if (dt.Rows[i][fi.Name] != System.DBNull.Value)
                                fi.SetValue(ety, Convert.ToInt32(dt.Rows[i][fi.Name]));
                            break;
                        case TypeCode.DateTime:
                            if (dt.Rows[i][fi.Name] != DBNull.Value)
                                fi.SetValue(ety, Convert.ToDateTime(dt.Rows[i][fi.Name]));
                            break;
                        default:
                            fi.SetValue(ety, Convert.ToString(dt.Rows[i][fi.Name]));
                            break;
                    }
                }
                list.Add((T)ety);
            }
            return list;
        }

        /// <summary>
        /// 执行结果集查询，返回DataTable
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public DataTable ExecuteDT(string sql, params OracleParameter[] parameters)
        {
            DataTable dt = new DataTable();
            OracleCommand command = new OracleCommand(sql, this.connection);
            if (parameters != null)
            {
                foreach (OracleParameter p in parameters)
                    command.Parameters.Add(p);
            }

            OracleDataAdapter oda = new OracleDataAdapter(command);
            oda.Fill(dt);
            return dt;
        }

        public string ExecuteScalar(string sql)
        {
            OracleCommand command = new OracleCommand(sql, this.connection);
            return command.ExecuteScalar().ToString();
        }

        /// <summary>
        /// 提交事务
        /// </summary>
        public void Commit()
        {
            try
            {
                if (this.transxion != null)
                    this.transxion.Commit();
            }
            catch
            {
                this.transxion.Rollback();
            }
            finally
            {
                if (this.connection.State == ConnectionState.Open)
                    this.connection.Close();
            }
        }

        /// <summary>
        /// 回滚事务
        /// </summary>
        public void Abort()
        {
            try
            {
                this.transxion.Rollback();
            }
            catch
            {
                if (this.connection.State == ConnectionState.Open)
                {
                    if (this.transxion != null)
                        this.transxion.Rollback();
                    this.connection.Close();
                }
            }
            finally
            {
                if (this.connection.State == ConnectionState.Open)
                {
                    this.connection.Close();
                }
            }
        }

        /// <summary>
        /// 创建Oracle参数
        /// </summary>
        /// <param name="pName"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static OracleParameter CreateOracleParameter(string pName, object value)
        {
            OracleParameter p = new OracleParameter(pName, value);
            return p;
        }     
    }
}