using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WooSungEngineering
{
    public class OptionInfo
    {
        public OptionInfo OCF
        {
            get
            {
                return Program.Option;
            }
        }

        string loginID;
        public string LoginID
        {
            get { return loginID; }
            set
            {
                loginID = value;
            }
        }

        private string _DBFullPath;
        /// <summary>
        /// Database File 위치
        /// </summary>
        public string DBFullPath
        {
            get { return _DBFullPath; }
            set { _DBFullPath = value; }
        }

        private string _CompName;
        public string CompName
        {
            get { return _CompName; }
            set { _CompName = value; }
        }

        private string _cultureName = "";
        public string cultureName
        {
            get { return _cultureName; }
            set { _cultureName = value; }
        }

        private byte[] _ReportImage;
        public byte[] ReportImage
        {
            get { return _ReportImage; }
            set { _ReportImage = value; }
        }

        private double _Flow1 = 0;

        public double Flow1
        {
            get { return _Flow1; }
            set { _Flow1 = value; }
        }
        private double _Pressure1 = 0;

        public double Pressure1
        {
            get { return _Pressure1; }
            set { _Pressure1 = value; }
        }
        private double _Flow2 = 0;

        public double Flow2
        {
            get { return _Flow2; }
            set { _Flow2 = value; }
        }
        private double _Pressure2 = 0;

        public double Pressure2
        {
            get { return _Pressure2; }
            set { _Pressure2 = value; }
        }
    }
}
