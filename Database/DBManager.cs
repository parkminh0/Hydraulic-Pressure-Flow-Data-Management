using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.IO;

namespace WooSungEngineering
{
    public class DBManager
    {
        public static string DBMErrString;
        public static bool IsConnected = false;
        static DBManager instance;
        public static DBManager Instance
        {
            get
            {
                if (instance == null || !IsConnected)
                {
                    // DB 연결 실패된 상태에서 DB 사용 시 연결 재시도
                    instance = new DBManager();
                }
                return instance;
            }
        }
        OptionInfo OCF
        {
            get
            {
                return Program.Option;
            }
        }

        string pswd = AES.AESDecrypt256("6snUHxhR5ee36Xa5C6NcQg==", Program.constance.compName); // passw0rd132$
        public string Pswd
        {
            get
            {
                return pswd;
            }
        }

        const int CONN_CNT = 1;
        DBMSSQLite[] dbArr = new DBMSSQLite[CONN_CNT];

        /// <summary>
        /// 생성자
        /// </summary>
        public DBManager()
        {
            lock (thisLock)
            {
                // Sqlite
                string connStr = string.Format("Data Source={0};Password={1}; Cache Size=10000; PRAGMA page_count=100000000; PRAGMA page_size=32768;", Program.constance.DbTargetFullName, pswd);
                for (int i = 0; i < dbArr.Length; i++)
                {
                    dbArr[i] = new DBMSSQLite();
                    DBMErrString = dbArr[i].SetReady(connStr);
                    if (!string.IsNullOrEmpty(DBMErrString))
                    {
                        // DB 연결 실패 시 항상 메세지 출력
                        IsConnected = false;
                        DevExpress.XtraEditors.XtraMessageBox.Show(LangResx.Main.Splash_DB_Err + "\r\n" + DBMErrString, "Error");
                    }
                    else
                    {
                        IsConnected = true;
                    }
                }
            }
        }

        object thisLock = new object();
        int currIdx = -1;
        public DBMSSQLite MDB
        {
            get
            {
                DBMSSQLite db = null;
                lock (thisLock)
                {
                    if (currIdx >= CONN_CNT - 1)
                    {
                        currIdx = -1;
                    }
                    currIdx++;
                    db = dbArr[currIdx];
                }
                return db;
            }
        }

        /// <summary>
        /// Database에서 데이터를 불러와 DataTable에 담아 넘겨줌
        /// </summary>
        /// <param name="query"></param>
        /// <param name="isMultiThread">Thread 사용 시 true</param>
        /// <returns></returns>
        public DataTable GetDataTable(string query, bool isMultiThread = false)
        {
            DataTable resultDT = null;
            try
            {
                resultDT = MDB.GetDataTable(query);
            }
            catch (ExceptionManager pException)
            {
                MDB.moleCommand.Connection.Close();
                if (!isMultiThread)
                {
                    Program.WMSG.MSG(string.Format("Exception Method = {0}\r\n InnerException = {1} \r\n Message = {2} ", pException.Method, pException.InnerException.Message, pException.Message));
                }
                else
                {
                    throw pException;
                }
            }
            catch (Exception e)
            {
                MDB.moleCommand.Connection.Close();
                if (!isMultiThread)
                {
                    Program.WMSG.MSG(e.Message);
                }
                else
                {
                    throw e;
                }
            }

            return resultDT;
        }

        /// <summary>
        /// 실행
        /// </summary>
        /// <param name="query"></param>
        /// <param name="isMultiThread">Thread 사용 시 true</param>
        /// <returns></returns>
        public int ExcuteDataUpdate(string query, bool isMultiThread = false)
        {
            int result = -1;
            try
            {
                result = MDB.mUpdate(query);
            }
            catch (ExceptionManager pException)
            {
                MDB.moleCommand.Connection.Close();
                if (!isMultiThread)
                {
                    Program.WMSG.MSG(string.Format("Exception Method = {0}\r\n InnerException = {1} \r\n Message = {2} ", pException.Method, pException.InnerException.Message, pException.Message));
                }
                else
                {
                    throw pException;
                }
            }
            catch (Exception e)
            {
                MDB.moleCommand.Connection.Close();
                if (!isMultiThread)
                {
                    Program.WMSG.MSG(e.Message);
                }
                else
                {
                    throw e;
                }
            }

            return result;
        }

        /// <summary>
        /// 실행 (ParamQuery)
        /// </summary>
        /// <param name="query"></param>
        /// <param name="isMultiThread">Thread 사용 시 true</param>
        /// <returns></returns>
        public int ExcuteDataUpdate(ParamQuerySQLite query, bool isMultiThread = false)
        {
            int result = -1;
            try
            {
                result = MDB.mUpdate(query);
            }
            catch (ExceptionManager pException)
            {
                MDB.moleCommand.Connection.Close();
                if (isMultiThread)
                {
                    Program.WMSG.MSG(string.Format("Exception Method = {0}\r\n InnerException = {1} \r\n Message = {2} ", pException.Method, pException.InnerException.Message, pException.Message));
                }
                else
                {
                    throw pException;
                }
            }
            catch (Exception e)
            {
                MDB.moleCommand.Connection.Close();
                if (!isMultiThread)
                {
                    Program.WMSG.MSG(e.Message);
                }
                else
                {
                    throw e;
                }
            }

            return result;
        }

        /// <summary>
        /// 벌크 Insert (Only Insert)
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="targetTableName"></param>
        /// <returns></returns>
        public bool DoBulkCopy(DataTable dt, string targetTableName)
        {
            try
            {
                MDB.DoBulkCopy(dt, targetTableName);
                return true;
            }
            catch (Exception e)
            {
                MDB.moleCommand.Connection.Close();
                Program.WMSG.MSG(e.Message);
                return false;
                //throw;
            }
        }

        /// <summary>
        /// 벌크 Insert (Truncate and Insert)
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="targetTableName"></param>
        /// <returns></returns>
        public bool DoBulkCopyTI(DataTable dt, string targetTableName)
        {
            try
            {
                MDB.DoBulkCopyTI(dt, targetTableName);
                return true;
            }
            catch (Exception e)
            {
                MDB.moleCommand.Connection.Close();
                Program.WMSG.MSG(e.Message);
                return false;
                //throw;
            }
        }

        /// <summary>
        /// 벌크 Insert (Delete Key and Insert)
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="targetTableName"></param>
        /// <returns></returns>
        public bool DoBulkCopyTI(DataTable dt, string targetTableName, int key)
        {
            try
            {
                MDB.DoBulkCopyTI(dt, targetTableName, key);
                return true;
            }
            catch (Exception e)
            {
                MDB.moleCommand.Connection.Close();
                Program.WMSG.MSG(e.Message);
                return false;
                //throw;
            }
        }

        /// <summary>
        /// 트랜잭션 처리
        /// </summary>
        /// <param name="querys"></param>
        /// <returns></returns>
        public string ExcuteTransaction(string[] querys)
        {
            return MDB.ExcuteTransaction(querys);
        }
        /// <summary>
        /// 트랜잭션 처리
        /// </summary>
        /// <param name="querys"></param>
        /// <returns></returns>
        public string ExcuteTransaction(List<string> querys)
        {
            return MDB.ExcuteTransaction(querys.ToArray());
        }

        /// <summary>
        /// 파리미터값이 있는 트랜잭션 처리(Image처리)
        /// </summary>
        /// <param name="querys"></param>
        /// <returns></returns>
        //public string ExcuteTransaction(ParamQuery[] querys)
        public string ExcuteTransaction(ParamQuerySQLite[] querys)
        {
            return MDB.ExcuteTransaction(querys);
        }

        /// <summary>
        /// 파리미터값이 있는 트랜잭션 처리(Image처리)
        /// </summary>
        /// <param name="querys"></param>
        /// <returns></returns>
        //public string ExcuteTransaction(List<ParamQuery> querys)
        public string ExcuteTransaction(List<ParamQuerySQLite> querys)
        {
            return MDB.ExcuteTransaction(querys.ToArray());
        }
    }
}
