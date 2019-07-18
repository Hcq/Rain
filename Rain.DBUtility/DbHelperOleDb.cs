using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Rain.DBUtility
{
    public abstract class DbHelperOleDb
    {
        public static string connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString.ToString() + HttpContext.Current.Server.MapPath(ConfigurationManager.AppSettings["DbPath"]) + ";";

        public static int GetMaxID(string FieldName, string TableName)
        {
            object single = DbHelperOleDb.GetSingle("select top 1 " + FieldName + " from " + TableName + " order by " + FieldName + " desc");
            if (single == null)
                return 0;
            return int.Parse(single.ToString());
        }

        public static bool Exists(string strSql)
        {
            object single = DbHelperOleDb.GetSingle(strSql);
            return (!object.Equals(single, (object)null) && !object.Equals(single, (object)DBNull.Value) ? int.Parse(single.ToString()) : 0) != 0;
        }

        public static bool Exists(string strSql, params OleDbParameter[] cmdParms)
        {
            object single = DbHelperOleDb.GetSingle(strSql, cmdParms);
            return (!object.Equals(single, (object)null) && !object.Equals(single, (object)DBNull.Value) ? int.Parse(single.ToString()) : 0) != 0;
        }

        public static int ExecuteSql(string SQLString)
        {
            using (OleDbConnection connection = new OleDbConnection(DbHelperOleDb.connectionString))
            {
                using (OleDbCommand oleDbCommand = new OleDbCommand(SQLString, connection))
                {
                    try
                    {
                        connection.Open();
                        return oleDbCommand.ExecuteNonQuery();
                    }
                    catch (OleDbException ex)
                    {
                        connection.Close();
                        throw new Exception(ex.Message);
                    }
                }
            }
        }

        public static int ExecuteSql(
          OleDbConnection connection,
          OleDbTransaction trans,
          string SQLString)
        {
            using (OleDbCommand oleDbCommand = new OleDbCommand(SQLString, connection))
            {
                try
                {
                    oleDbCommand.Connection = connection;
                    oleDbCommand.Transaction = trans;
                    return oleDbCommand.ExecuteNonQuery();
                }
                catch (OleDbException ex)
                {
                    throw new Exception(ex.Message);
                }
            }
        }

        public static bool ExecuteSqlTran(ArrayList SQLStringList)
        {
            using (OleDbConnection oleDbConnection = new OleDbConnection(DbHelperOleDb.connectionString))
            {
                oleDbConnection.Open();
                OleDbCommand oleDbCommand = new OleDbCommand();
                oleDbCommand.Connection = oleDbConnection;
                OleDbTransaction oleDbTransaction = oleDbConnection.BeginTransaction();
                oleDbCommand.Transaction = oleDbTransaction;
                try
                {
                    for (int index = 0; index < SQLStringList.Count; ++index)
                    {
                        string str = SQLStringList[index].ToString();
                        if (str.Trim().Length > 1)
                        {
                            oleDbCommand.CommandText = str;
                            oleDbCommand.ExecuteNonQuery();
                        }
                    }
                    oleDbTransaction.Commit();
                }
                catch (OleDbException ex)
                {
                    oleDbTransaction.Rollback();
                    return false;
                }
                return true;
            }
        }

        public static int ExecuteSql(string SQLString, string content)
        {
            using (OleDbConnection connection = new OleDbConnection(DbHelperOleDb.connectionString))
            {
                OleDbCommand oleDbCommand = new OleDbCommand(SQLString, connection);
                OleDbParameter oleDbParameter = new OleDbParameter("@content", OleDbType.VarChar);
                oleDbParameter.Value = (object)content;
                oleDbCommand.Parameters.Add(oleDbParameter);
                try
                {
                    connection.Open();
                    return oleDbCommand.ExecuteNonQuery();
                }
                catch (OleDbException ex)
                {
                    throw new Exception(ex.Message);
                }
                finally
                {
                    oleDbCommand.Dispose();
                    connection.Close();
                }
            }
        }

        public static int ExecuteSqlInsertImg(string strSQL, byte[] fs)
        {
            using (OleDbConnection connection = new OleDbConnection(DbHelperOleDb.connectionString))
            {
                OleDbCommand oleDbCommand = new OleDbCommand(strSQL, connection);
                OleDbParameter oleDbParameter = new OleDbParameter("@fs", OleDbType.Binary);
                oleDbParameter.Value = (object)fs;
                oleDbCommand.Parameters.Add(oleDbParameter);
                try
                {
                    connection.Open();
                    return oleDbCommand.ExecuteNonQuery();
                }
                catch (OleDbException ex)
                {
                    throw new Exception(ex.Message);
                }
                finally
                {
                    oleDbCommand.Dispose();
                    connection.Close();
                }
            }
        }

        public static object GetSingle(string SQLString)
        {
            using (OleDbConnection connection = new OleDbConnection(DbHelperOleDb.connectionString))
            {
                using (OleDbCommand oleDbCommand = new OleDbCommand(SQLString, connection))
                {
                    try
                    {
                        connection.Open();
                        object objA = oleDbCommand.ExecuteScalar();
                        if (object.Equals(objA, (object)null) || object.Equals(objA, (object)DBNull.Value))
                            return (object)null;
                        return objA;
                    }
                    catch (OleDbException ex)
                    {
                        connection.Close();
                        throw new Exception(ex.Message);
                    }
                }
            }
        }

        public static object GetSingle(
          OleDbConnection connection,
          OleDbTransaction trans,
          string SQLString)
        {
            using (OleDbCommand oleDbCommand = new OleDbCommand(SQLString, connection))
            {
                try
                {
                    oleDbCommand.Connection = connection;
                    oleDbCommand.Transaction = trans;
                    object objA = oleDbCommand.ExecuteScalar();
                    if (object.Equals(objA, (object)null) || object.Equals(objA, (object)DBNull.Value))
                        return (object)null;
                    return objA;
                }
                catch (OleDbException ex)
                {
                    throw new Exception(ex.Message);
                }
            }
        }

        public static object GetSingle(
          OleDbConnection connection,
          OleDbTransaction trans,
          string SQLString,
          params OleDbParameter[] cmdParms)
        {
            using (OleDbCommand cmd = new OleDbCommand())
            {
                try
                {
                    DbHelperOleDb.PrepareCommand(cmd, connection, trans, SQLString, cmdParms);
                    object objA = cmd.ExecuteScalar();
                    cmd.Parameters.Clear();
                    if (object.Equals(objA, (object)null) || object.Equals(objA, (object)DBNull.Value))
                        return (object)null;
                    return objA;
                }
                catch (SqlException ex)
                {
                    throw ex;
                }
            }
        }

        public static OleDbDataReader ExecuteReader(string strSQL)
        {
            OleDbConnection connection = new OleDbConnection(DbHelperOleDb.connectionString);
            OleDbCommand oleDbCommand = new OleDbCommand(strSQL, connection);
            try
            {
                connection.Open();
                return oleDbCommand.ExecuteReader();
            }
            catch (OleDbException ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static DataSet Query(string SQLString)
        {
            using (OleDbConnection selectConnection = new OleDbConnection(DbHelperOleDb.connectionString))
            {
                DataSet dataSet = new DataSet();
                try
                {
                    selectConnection.Open();
                    new OleDbDataAdapter(SQLString, selectConnection).Fill(dataSet, "ds");
                }
                catch (OleDbException ex)
                {
                    throw new Exception(ex.Message);
                }
                return dataSet;
            }
        }

        public static bool ExitColumnName(string tablename, string column_name)
        {
            bool flag = false;
            using (OleDbConnection oleDbConnection = new OleDbConnection(DbHelperOleDb.connectionString))
            {
                oleDbConnection.Open();
                OleDbCommand oleDbCommand = new OleDbCommand();
                oleDbCommand.CommandText = "select * from " + tablename + " where 1=0";
                oleDbCommand.Connection = oleDbConnection;
                OleDbDataReader oleDbDataReader = oleDbCommand.ExecuteReader();
                for (int ordinal = 0; ordinal < oleDbDataReader.FieldCount; ++ordinal)
                {
                    if (oleDbDataReader.GetName(ordinal).ToLower().Trim() == column_name.ToLower().Trim())
                    {
                        flag = true;
                        break;
                    }
                }
            }
            return flag;
        }

        public static DataSet Query(
          OleDbConnection connection,
          OleDbTransaction trans,
          string SQLString)
        {
            DataSet dataSet = new DataSet();
            try
            {
                new OleDbDataAdapter(SQLString, connection)
                {
                    SelectCommand = {
            Transaction = trans
          }
                }.Fill(dataSet, "ds");
            }
            catch (OleDbException ex)
            {
                throw new Exception(ex.Message);
            }
            return dataSet;
        }

        public static int ExecuteSql(string SQLString, params OleDbParameter[] cmdParms)
        {
            using (OleDbConnection conn = new OleDbConnection(DbHelperOleDb.connectionString))
            {
                using (OleDbCommand cmd = new OleDbCommand())
                {
                    try
                    {
                        DbHelperOleDb.PrepareCommand(cmd, conn, (OleDbTransaction)null, SQLString, cmdParms);
                        int num = cmd.ExecuteNonQuery();
                        cmd.Parameters.Clear();
                        return num;
                    }
                    catch (OleDbException ex)
                    {
                        throw new Exception(ex.Message);
                    }
                }
            }
        }

        public static int ExecuteSql(
          OleDbConnection connection,
          OleDbTransaction trans,
          string SQLString,
          params OleDbParameter[] cmdParms)
        {
            using (OleDbCommand cmd = new OleDbCommand())
            {
                try
                {
                    DbHelperOleDb.PrepareCommand(cmd, connection, trans, SQLString, cmdParms);
                    int num = cmd.ExecuteNonQuery();
                    cmd.Parameters.Clear();
                    return num;
                }
                catch (OleDbException ex)
                {
                    throw new Exception(ex.Message);
                }
            }
        }

        public static bool ExecuteSqlTran(Hashtable SQLStringList)
        {
            using (OleDbConnection conn = new OleDbConnection(DbHelperOleDb.connectionString))
            {
                conn.Open();
                using (OleDbTransaction trans = conn.BeginTransaction())
                {
                    OleDbCommand cmd = new OleDbCommand();
                    try
                    {
                        foreach (DictionaryEntry sqlString in SQLStringList)
                        {
                            string cmdText = sqlString.Key.ToString();
                            OleDbParameter[] cmdParms = (OleDbParameter[])sqlString.Value;
                            DbHelperOleDb.PrepareCommand(cmd, conn, trans, cmdText, cmdParms);
                            cmd.ExecuteNonQuery();
                            cmd.Parameters.Clear();
                        }
                        trans.Commit();
                    }
                    catch
                    {
                        trans.Rollback();
                        return false;
                    }
                }
            }
            return true;
        }

        public static object GetSingle(string SQLString, params OleDbParameter[] cmdParms)
        {
            using (OleDbConnection conn = new OleDbConnection(DbHelperOleDb.connectionString))
            {
                using (OleDbCommand cmd = new OleDbCommand())
                {
                    try
                    {
                        DbHelperOleDb.PrepareCommand(cmd, conn, (OleDbTransaction)null, SQLString, cmdParms);
                        object objA = cmd.ExecuteScalar();
                        cmd.Parameters.Clear();
                        if (object.Equals(objA, (object)null) || object.Equals(objA, (object)DBNull.Value))
                            return (object)null;
                        return objA;
                    }
                    catch (OleDbException ex)
                    {
                        throw new Exception(ex.Message);
                    }
                }
            }
        }

        public static OleDbDataReader ExecuteReader(
          string SQLString,
          params OleDbParameter[] cmdParms)
        {
            OleDbConnection conn = new OleDbConnection(DbHelperOleDb.connectionString);
            OleDbCommand cmd = new OleDbCommand();
            try
            {
                DbHelperOleDb.PrepareCommand(cmd, conn, (OleDbTransaction)null, SQLString, cmdParms);
                OleDbDataReader oleDbDataReader = cmd.ExecuteReader();
                cmd.Parameters.Clear();
                return oleDbDataReader;
            }
            catch (OleDbException ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static DataSet Query(string SQLString, params OleDbParameter[] cmdParms)
        {
            using (OleDbConnection conn = new OleDbConnection(DbHelperOleDb.connectionString))
            {
                OleDbCommand oleDbCommand = new OleDbCommand();
                DbHelperOleDb.PrepareCommand(oleDbCommand, conn, (OleDbTransaction)null, SQLString, cmdParms);
                using (OleDbDataAdapter oleDbDataAdapter = new OleDbDataAdapter(oleDbCommand))
                {
                    DataSet dataSet = new DataSet();
                    try
                    {
                        oleDbDataAdapter.Fill(dataSet, "ds");
                        oleDbCommand.Parameters.Clear();
                    }
                    catch (OleDbException ex)
                    {
                        throw new Exception(ex.Message);
                    }
                    return dataSet;
                }
            }
        }

        public static DataSet Query(
          OleDbConnection connection,
          OleDbTransaction trans,
          string SQLString,
          params OleDbParameter[] cmdParms)
        {
            OleDbCommand oleDbCommand = new OleDbCommand();
            DbHelperOleDb.PrepareCommand(oleDbCommand, connection, trans, SQLString, cmdParms);
            using (OleDbDataAdapter oleDbDataAdapter = new OleDbDataAdapter(oleDbCommand))
            {
                DataSet dataSet = new DataSet();
                try
                {
                    oleDbDataAdapter.Fill(dataSet, "ds");
                    oleDbCommand.Parameters.Clear();
                }
                catch (OleDbException ex)
                {
                    throw new Exception(ex.Message);
                }
                return dataSet;
            }
        }

        private static void PrepareCommand(
          OleDbCommand cmd,
          OleDbConnection conn,
          OleDbTransaction trans,
          string cmdText,
          OleDbParameter[] cmdParms)
        {
            if (conn.State != ConnectionState.Open)
                conn.Open();
            cmd.Connection = conn;
            cmd.CommandText = cmdText;
            if (trans != null)
                cmd.Transaction = trans;
            cmd.CommandType = CommandType.Text;
            if (cmdParms == null)
                return;
            foreach (OleDbParameter cmdParm in cmdParms)
                cmd.Parameters.Add(cmdParm);
        }

        public static OleDbDataReader RunProcedure(
          string storedProcName,
          IDataParameter[] parameters)
        {
            OleDbConnection connection = new OleDbConnection(DbHelperOleDb.connectionString);
            connection.Open();
            OleDbCommand oleDbCommand = DbHelperOleDb.BuildQueryCommand(connection, storedProcName, parameters);
            oleDbCommand.CommandType = CommandType.StoredProcedure;
            return oleDbCommand.ExecuteReader();
        }

        public static DataSet RunProcedure(
          string storedProcName,
          IDataParameter[] parameters,
          string tableName)
        {
            using (OleDbConnection connection = new OleDbConnection(DbHelperOleDb.connectionString))
            {
                DataSet dataSet = new DataSet();
                connection.Open();
                new OleDbDataAdapter()
                {
                    SelectCommand = DbHelperOleDb.BuildQueryCommand(connection, storedProcName, parameters)
                }.Fill(dataSet, tableName);
                connection.Close();
                return dataSet;
            }
        }

        private static OleDbCommand BuildQueryCommand(
          OleDbConnection connection,
          string storedProcName,
          IDataParameter[] parameters)
        {
            OleDbCommand oleDbCommand = new OleDbCommand(storedProcName, connection);
            oleDbCommand.CommandType = CommandType.StoredProcedure;
            foreach (OleDbParameter parameter in parameters)
                oleDbCommand.Parameters.Add(parameter);
            return oleDbCommand;
        }

        public static int RunProcedure(
          string storedProcName,
          IDataParameter[] parameters,
          out int rowsAffected)
        {
            using (OleDbConnection connection = new OleDbConnection(DbHelperOleDb.connectionString))
            {
                connection.Open();
                OleDbCommand oleDbCommand = DbHelperOleDb.BuildIntCommand(connection, storedProcName, parameters);
                rowsAffected = oleDbCommand.ExecuteNonQuery();
                return (int)oleDbCommand.Parameters["ReturnValue"].Value;
            }
        }

        private static OleDbCommand BuildIntCommand(
          OleDbConnection connection,
          string storedProcName,
          IDataParameter[] parameters)
        {
            OleDbCommand oleDbCommand = DbHelperOleDb.BuildQueryCommand(connection, storedProcName, parameters);
            oleDbCommand.Parameters.Add(new OleDbParameter("ReturnValue", OleDbType.Integer, 4, ParameterDirection.ReturnValue, false, (byte)0, (byte)0, string.Empty, DataRowVersion.Default, (object)null));
            return oleDbCommand;
        }
    }
}
