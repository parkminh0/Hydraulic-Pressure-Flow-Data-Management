using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SQLite;

namespace WooSungEngineering
{
    public class ParamQuerySQLite
    {
        private string _Sql;

        public string Sql
        {
            get { return _Sql; }
            set { _Sql = value; }
        }

        private List<SQLiteParameter> _ParamList;

        public List<SQLiteParameter> ParamList
        {
            get { return _ParamList; }
            set { _ParamList = value; }
        }

        /// <summary>
        /// 생성자
        /// </summary>
        public ParamQuerySQLite()
        {
            _ParamList = new List<SQLiteParameter>();
        }

        /// <summary>
        /// Add Parameter
        /// </summary>
        /// <param name="sqlParam"></param>
        public void AddParameter(SQLiteParameter sqlParam)
        {
            _ParamList.Add(sqlParam);
        }

        /// <summary>
        /// Add Parameter 2
        /// </summary>
        /// <param name="parameterName"></param>
        /// <param name="value"></param>
        public void AddParameter(string parameterName, object value)
        {
            _ParamList.Add(new SQLiteParameter(parameterName, value));
        }
    }
}
