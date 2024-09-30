using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Data;
using System.Data.SQLite;

namespace WooSungEngineering
{
    public class DBMSSQLite
    {
        private SQLiteConnection moleConnect;
        private SQLiteDataAdapter moleAdapter;
        public SQLiteCommand moleCommand;
        private SQLiteTransaction tx;
        private bool isready;

        public int mUpdate(string query)
        {
            int result = -1;
            moleCommand.CommandType = CommandType.Text;
            if (!isready) return result;

            moleCommand.CommandText = query;
            moleCommand.Connection.Open();
            moleCommand.Parameters.Clear();

            result = moleCommand.ExecuteNonQuery();
            moleCommand.Connection.Close();

            return result;
        }

        public int mUpdate(ParamQuerySQLite query)
        {
            int result = -1;
            moleCommand.CommandType = CommandType.Text;
            if (!isready) return result;

            moleCommand.CommandText = query.Sql;
            moleCommand.Parameters.AddRange(query.ParamList.ToArray());
            moleCommand.Connection.Open();

            result = moleCommand.ExecuteNonQuery();
            moleCommand.Parameters.Clear();
            moleCommand.Connection.Close();

            return result;
        }

        public void mOpen()
        {
            try
            {
                moleCommand.Connection.Open();
            }
            catch (Exception pException)
            {
                throw new ExceptionManager(this, "Connection.Open()", pException);
            }
        }

        public void mClose()
        {
            try
            {
                moleCommand.Connection.Close();
            }
            catch (Exception pException)
            {
                throw new ExceptionManager(this, "mClose()", pException);
            }
        }

        public void mNonQuery(string query)
        {
            moleCommand.CommandType = CommandType.Text;
            moleCommand.CommandText = query;
            try
            {
                moleCommand.ExecuteNonQuery();
            }
            catch (Exception pException)
            {
                throw new ExceptionManager(this, "mNonQuery(string query)", pException);
            }
        }

        /// <summary>
        /// count, sum 등 Integer형 sql 실행
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public int GetIntScalar(string query)
        {
            int v = 0;
            string aaa = string.Empty;
            if (!isready) return v;

            moleCommand.CommandType = CommandType.Text;
            moleCommand.CommandText = query;
            moleCommand.Connection.Open();
            //v = (int)moleCommand.ExecuteScalar();
            try
            {
                var rs = moleCommand.ExecuteScalar();
                v = int.Parse(rs.ToString());
                moleCommand.Connection.Close();
            }
            catch (Exception ex)
            {
                moleCommand.Connection.Close();
                throw ex;
            }

            return v;
        }

        /// <summary>
        /// sql Bulk copy
        /// </summary>
        /// <param name="keepNulls"></param>
        /// <param name="reader"></param>
        public void DoBulkCopy(DataTable dt, string destTable)
        {
            moleCommand.Connection.Open();
            DataTable dtTableInfo = new DataTable();
            moleCommand.CommandType = CommandType.Text;
            moleCommand.CommandText = "PRAGMA table_info('" + destTable + "')";
            moleAdapter.Fill(dtTableInfo);

            string insertQuery = string.Empty;
            insertQuery += "INSERT INTO '" + destTable + "' VALUES (";
            foreach (DataRow dr in dtTableInfo.Rows)
            {
                string colNo = dr["cid"] + "";
                if (colNo != "0") insertQuery += ", ";

                insertQuery += "@v" + colNo + "";
            }
            insertQuery += ")";

            tx = moleConnect.BeginTransaction();
            moleCommand.Transaction = tx;
            try
            {
                foreach (DataRow dr in dt.Rows)
                {
                    moleCommand.CommandType = CommandType.Text;
                    moleCommand.CommandText = insertQuery;
                    int colNo = 0;
                    foreach (object obj in dr.ItemArray)
                    {
                        moleCommand.Parameters.AddWithValue("@v" + colNo.ToString(), obj);
                        colNo++;
                    }
                    moleCommand.ExecuteNonQuery();
                }

                tx.Commit();   //Commit
            }
            catch (Exception ex)
            {
                tx.Rollback(); //Rollback
                throw ex;
            }
            finally
            {
                moleCommand.Connection.Close();
            }
        }
        
        /// <summary>
        /// sql Bulk copy
        /// </summary>
        /// <param name="keepNulls"></param>
        /// <param name="reader"></param>
        public void DoBulkCopyTI(DataTable dt, string destTable)
        {
            moleCommand.Connection.Open();
            DataTable dtTableInfo = new DataTable();
            moleCommand.CommandType = CommandType.Text;
            moleCommand.CommandText = "PRAGMA table_info('" + destTable + "')";
            moleAdapter.Fill(dtTableInfo);

            string insertQuery = string.Empty;
            insertQuery += "INSERT INTO '" + destTable + "' VALUES (";
            foreach (DataRow dr in dtTableInfo.Rows)
            {
                string colNo = dr["cid"] + "";
                if (colNo != "0") insertQuery += ", ";

                insertQuery += "@v" + colNo + "";
            }
            insertQuery += ")";

            tx = moleConnect.BeginTransaction();
            moleCommand.Transaction = tx;
            try
            {
                string deleteQuery;
                deleteQuery = "DELETE FROM " + destTable + " ";
                moleCommand.CommandType = CommandType.Text;
                moleCommand.CommandText = deleteQuery;
                moleCommand.ExecuteNonQuery();

                // 추가하기
                foreach (DataRow dr in dt.Rows)
                {
                    moleCommand.CommandType = CommandType.Text;
                    moleCommand.CommandText = insertQuery;
                    int colNo = 0;
                    foreach (object obj in dr.ItemArray)
                    {
                        moleCommand.Parameters.AddWithValue("@v" + colNo.ToString(), obj);
                        colNo++;
                    }
                    moleCommand.ExecuteNonQuery();
                }

                tx.Commit();   //Commit
            }
            catch (Exception ex)
            {
                tx.Rollback(); //Rollback
                throw ex;
            }
            finally
            {
                moleCommand.Connection.Close();
            }
        }

        /// <summary>
        /// sql Bulk copy
        /// </summary>
        /// <param name="keepNulls"></param>
        /// <param name="reader"></param>
        public void DoBulkCopyTI(DataTable dt, string destTable, int key)
        {
            moleCommand.Connection.Open();
            DataTable dtTableInfo = new DataTable();
            moleCommand.CommandType = CommandType.Text;
            moleCommand.CommandText = "PRAGMA table_info('" + destTable + "')";
            moleAdapter.Fill(dtTableInfo);

            string insertQuery = string.Empty;
            insertQuery += "INSERT INTO '" + destTable + "' VALUES (";
            foreach (DataRow dr in dtTableInfo.Rows)
            {
                string colNo = dr["cid"] + "";
                if (colNo != "0") insertQuery += ", ";

                insertQuery += "@v" + colNo + "";
            }
            insertQuery += ")";

            tx = moleConnect.BeginTransaction();
            moleCommand.Transaction = tx;
            try
            {
                string deleteQuery;
                // ProcessedData TI
                deleteQuery = "DELETE FROM " + destTable + $" WHERE RecordKey = {key} ";
                moleCommand.CommandType = CommandType.Text;
                moleCommand.CommandText = deleteQuery;
                moleCommand.ExecuteNonQuery();

                // 추가하기
                foreach (DataRow dr in dt.Rows)
                {
                    moleCommand.CommandType = CommandType.Text;
                    moleCommand.CommandText = insertQuery;
                    int colNo = 0;
                    foreach (object obj in dr.ItemArray)
                    {
                        moleCommand.Parameters.AddWithValue("@v" + colNo.ToString(), obj);
                        colNo++;
                    }
                    moleCommand.ExecuteNonQuery();
                }

                tx.Commit();   //Commit
            }
            catch (Exception ex)
            {
                tx.Rollback(); //Rollback
                throw ex;
            }
            finally
            {
                moleCommand.Connection.Close();
            }
        }

        /// <summary>
        /// 결과를 DataTable 로 리턴 한다.
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public DataTable GetDataTable(string query)
        {
            DataTable dt = new DataTable();
            if (!isready) return dt;

            moleCommand.CommandType = CommandType.Text;
            moleCommand.CommandText = query;

            moleAdapter.Fill(dt);
            return dt;
        }

        /// <summary>
        /// 디비 준비 시키기.
        /// </summary>
        /// <param name="pdbm"></param>
        public string SetReady(string strs)
        {
            string result = "";
            string connStr = DumpBuilderContents(strs);
            moleConnect = new SQLiteConnection(connStr);
            moleCommand = new SQLiteCommand();
            moleCommand.Connection = moleConnect;
            moleAdapter = new SQLiteDataAdapter(moleCommand);

            try
            {
                moleCommand.CommandText = Program.constance.DBTestQuery;
                moleAdapter.Fill(new DataTable());
                isready = true;
            }
            catch (Exception e)
            {
                isready = false;
                result = e.Message;
            }
            return result;
        }

        private string DumpBuilderContents(string connectString)
        {
            SQLiteConnectionStringBuilder builder = new SQLiteConnectionStringBuilder(connectString);
            return builder.ConnectionString;
        }

        /// <summary>
        /// DB 연결 준비 여부
        /// </summary>
        public bool IsReady
        {
            get
            {
                return isready;
            }
        }

        /// <summary>
        /// Tranction처리
        /// Commit / Rollback
        /// </summary>
        /// <param name="querys"></param>
        /// <returns>작업 성공 시 빈문자열 Return, 실패 시 Exception Message Return</returns>
        public string ExcuteTransaction(string[] querys)
        {
            string msg = "";
            moleCommand.Connection.Open();
            tx = moleConnect.BeginTransaction();
            moleCommand.Transaction = tx;
            try
            {
                for (int i = 0; i < querys.Length; i++)
                {
                    moleCommand.CommandType = CommandType.Text;
                    moleCommand.CommandText = querys[i];
                    moleCommand.ExecuteNonQuery();
                }

                tx.Commit();   //Commit
            }
            catch (Exception ex)
            {
                msg = ex.Message;
                tx.Rollback(); //Rollback
            }
            finally
            {
                moleCommand.Connection.Close();
            }

            return msg;
        }
        /// <summary>
        /// Transaction처리 List
        /// </summary>
        /// <param name="querys"></param>
        /// <returns>작업 성공 시 빈문자열 Return, 실패 시 Exception Message Return</returns>
        public string ExcuteTransaction(List<string> querys)
        {
            string[] sqls = querys.ToArray();
            return ExcuteTransaction(sqls);
        }

        public string ExcuteTransaction(ParamQuerySQLite[] querys)
        {
            string str = "";

            moleCommand.Connection.Open();
            tx = moleConnect.BeginTransaction();
            moleCommand.Transaction = tx;
            try
            {
                for (int i = 0; i < querys.Length; i++)
                {
                    moleCommand.CommandType = CommandType.Text;
                    moleCommand.CommandText = querys[i].Sql;
                    moleCommand.Parameters.AddRange(querys[i].ParamList.ToArray());
                    moleCommand.ExecuteNonQuery();
                    moleCommand.Parameters.Clear();
                }

                tx.Commit();   //Commit
            }
            catch (Exception ex)
            {
                str = ex.Message;
                tx.Rollback(); //Rollback
            }
            finally
            {
                moleCommand.Connection.Close();
            }
            return str;
        }

    }
}
