using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace WooSungEngineering
{
    public class Constance
    {
        public string compName = "wooribnc";
        public string SystemTitle = LangResx.Main.title;
        //-----------------------------------------------------------------------------------------------------------------//
        // Program Update History
        /* 1.0.1버전 (2023.05.03)
         * 1. 무슨수정
         *  - 어떻게 수정했다
         */
        //-----------------------------------------------------------------------------------------------------------------//
        public string DBTestQuery = "select datetime('now')";
        public bool DBConnectInSplash = true;
        
        /// <summary>
        /// OS공통 작업폴더
        /// </summary>
        public string CommonFilePath
        {
            get
            {
                return Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            }
        }

        /// <summary>
        /// DB FILE위치
        /// </summary>
        private string dbFilePathOrg = Environment.CurrentDirectory;
        public string DbFilePathOrg
        {
            get { return dbFilePathOrg; }
        }

        private string dbFilePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "wooribnc");
        /// <summary>
        /// 실제 DB파일이 위치할 폴더
        /// </summary>
        public string DbFilePath
        {
            get { return dbFilePath; }
            set
            {
                dbFilePath = value;
            }
        }

        private string dbFileName = "WooSungEngineering.db"; // SQLite
        public string DbFileName
        {
            get { return dbFileName; }
        }

        /// <summary>
        /// 실제 DB파일의 FULL PATH + Name
        /// </summary>
        public string DbTargetFullName
        {
            get
            {
                return Path.Combine(dbFilePath, dbFileName);
            }
        }

        private string dbBaseFileName = "WooSungEngineering.db";
        private string dbInitFileName = "WooSungEngineering.db";
        /// <summary>
        /// Initial DB Name
        /// </summary>
        public string DbInitFileName
        {
            get { return dbInitFileName; }
        }
        /// <summary>
        /// 원본 Init파일 Full Path + Name
        /// </summary>
        public string DbInitOrgFullName
        {
            get
            {
                return Path.Combine(dbFilePathOrg, dbInitFileName);
            }
        }

        /// <summary>
        /// MainDB가 되기 직전의 Full Name
        /// </summary>
        public string DbInitFullName
        {
            get
            {
                return Path.Combine(dbFilePath, dbInitFileName);
            }
        }

        /// <summary>
        /// 원본 base파일 Full Path + Name : 초기데이터 들어있는 Init DB
        /// </summary>
        public string DbBaseOrgFullName
        {
            get
            {
                return Path.Combine(dbFilePathOrg, dbBaseFileName);
            }
        }
    }
}
