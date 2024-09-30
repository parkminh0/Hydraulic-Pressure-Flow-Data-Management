using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using DevExpress.UserSkins;
using DevExpress.Skins;
using DevExpress.LookAndFeel;
using DevExpress.XtraSplashScreen;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using SecureApp;
using System.Globalization;
using System.Threading;

namespace WooSungEngineering
{
    static class Program
    {
        public static OptionInfo Option;
        public static WarningMSG WMSG;
        public static bool GRunYN;

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            constance = new Constance();
            LoadConfig();

            ChangeLanguage(Option.cultureName);
            DevExpress.XtraEditors.WindowsFormsSettings.AllowRoundedWindowCorners = DevExpress.Utils.DefaultBoolean.True;

            // 기본스킨 Bezier로 변경 및 팔레트 설정 추가 (DB설정 및 로그인 창에도 적용이 되도록 설정시점 변경)
            if (Properties.Settings.Default.UserSkin != "")
            {
                if (Properties.Settings.Default.UserSkin == "The Bezier" && Properties.Settings.Default.UserPalette != "")
                    UserLookAndFeel.Default.SetSkinStyle(Properties.Settings.Default.UserSkin, Properties.Settings.Default.UserPalette);
                else
                    UserLookAndFeel.Default.SetSkinStyle(Properties.Settings.Default.UserSkin);
            }
            else
            {
                UserLookAndFeel.Default.SetSkinStyle("The Bezier", "Gloom Gloom");
            }
            // DB설정 및 로그인 창에도 적용이 되도록 폰트 설정시점 변경
            DevExpress.Utils.AppearanceObject.DefaultFont = new System.Drawing.Font("굴림", 9);

            //DB File Check
            chkDBExist();

            GRunYN = true;

            SplashScreenManager.ShowForm(mainApp, typeof(SplashScreen1), true, true, false);
            SplashScreenManager.Default.SendCommand(SplashScreen1.SplashScreenCommand.SetStatus, "skin setting ");
            SkinManager.EnableFormSkins();
            SplashScreenManager.Default.SendCommand(SplashScreen1.SplashScreenCommand.SetStatus, "tool license check");
            BonusSkins.Register();
            SplashScreenManager.Default.SendCommand(SplashScreen1.SplashScreenCommand.SetStatus, "font setting");

            mainApp = new MainForm();
            WMSG = new WarningMSG();
            string runCheck = mainApp.Splash();

            if (string.IsNullOrEmpty(runCheck) && GRunYN)
            {
                Application.Run(mainApp);
            }
            else
            {
                Option.DBFullPath = string.Empty;
                SaveConfig();

                SplashScreenManager.Default.SendCommand(SplashScreen1.SplashScreenCommand.SetStatus, runCheck);
                SplashScreenManager.CloseForm(true);
                WMSG.MSG(runCheck);
            }
        }

        public static MainForm mainApp;
        public static Constance constance;

        /// <summary> 설정파일 xml싱크. </summary>
        public static XmlSerializer m_serializer = new XmlSerializer(typeof(OptionInfo));

        /// <summary>
        /// 설정파일 xml 전체경로
        /// </summary>
        static string cfgPath = Path.GetFileNameWithoutExtension(System.Environment.GetCommandLineArgs()[0]) + "Settings.xml";
        public static string CfgPath
        {
            get
            {
                return Path.Combine(Program.constance.CommonFilePath, cfgPath);
            }
        }

        /// <summary>
        /// 설정정보 로드
        /// </summary>
        public static void LoadConfig()
        {
            if (File.Exists(Program.CfgPath))
            {
                bool isError = false;
                using (FileStream fs = File.OpenRead(Program.CfgPath))
                {
                    try
                    {
                        Program.Option = Program.m_serializer.Deserialize(fs) as OptionInfo;
                    }
                    catch
                    {
                        isError = true;
                    }
                }

                if (isError)
                {
                    File.Delete(Program.CfgPath);
                }
            }

            if (Program.Option == null)
            {
                Program.Option = new OptionInfo();
                Option.LoginID = "admin";
                SaveConfig();
            }
            //DB파일경로 검사
            if (!string.IsNullOrEmpty(Option.DBFullPath))
            {
                try
                {
                    if (Directory.Exists(Option.DBFullPath))
                        constance.DbFilePath = Option.DBFullPath;
                    else
                        Option.DBFullPath = string.Empty;
                }
                catch (Exception)
                {
                    Option.DBFullPath = string.Empty;
                }
                finally
                {
                    SaveConfig();
                }
            }
        }

        /// <summary>
        /// 설정정보 저장.
        /// </summary>
        public static void SaveConfig()
        {
            if (!File.Exists(Program.CfgPath))
            {
                File.Create(Program.CfgPath).Close();
            }

            using (XmlTextWriter xtw = new XmlTextWriter(Program.CfgPath, Encoding.UTF8))
            {
                Program.m_serializer.Serialize(xtw, Program.Option);
                xtw.Flush();
                xtw.Close();
            }
        }

        /// <summary>
        /// 인증키 확인
        /// </summary>
        /// <param name="trialDays">체험판 기간 일수</param>
        /// <returns></returns>
        private static bool CheckSecure(int trialDays)
        {
            string aName = Path.GetFileNameWithoutExtension(Path.GetFileNameWithoutExtension(System.Environment.GetCommandLineArgs()[0]));
            string regPath = string.Format(@"Software\wooribnc\{0}\Protection", aName);
            Secure scre = new Secure(trialDays);
            scre.runType = Secure.RunType.Simple;  //WIN, ONLYCHECK, SIMPLE
            string ps = AES.AESDecrypt256("k5XE8bYrWiqn6+GM7bpLeA==", "wooribnc");  //wooribnc132$

            bool isYn;
            try
            {
                isYn = scre.Algorithm(ps, regPath);
            }
            catch (Exception)
            {
                isYn = false;
            }
            return isYn;
        }

        /// <summary>
        /// 데이터베이스 파일이 존재하는지 검사하고 없으면 복사
        /// </summary>
        /// <returns></returns>
        public static bool chkDBExist()
        {
            try
            {
                if (!Directory.Exists(Program.constance.DbFilePath))
                {
                    Directory.CreateDirectory(Program.constance.DbFilePath);
                }
            }
            catch (Exception)
            {
                return false;
            }

            if (!File.Exists(Program.constance.DbTargetFullName))
            {
                try
                {
                    File.Copy(Program.constance.DbBaseOrgFullName, Program.constance.DbTargetFullName);
                }
                catch (Exception)
                {
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// 언어 변경
        /// </summary>
        /// <param name="cultureName"></param>
        public static void ChangeLanguage(string cultureName)
        {
            CultureInfo culture;
            if (string.IsNullOrWhiteSpace(cultureName))
            {
                string lang = CultureInfo.InstalledUICulture.Parent.IetfLanguageTag;
                if (lang == "ko" || lang == "en" || lang == "es" || lang == "id" || lang == "fr" || lang == "")
                    Option.cultureName = lang;
                else
                    Option.cultureName = "en";
            }
            else
            {
                 Option.cultureName = cultureName;
            }
            SaveConfig();

            culture = CultureInfo.CreateSpecificCulture(Option.cultureName);
            Thread.CurrentThread.CurrentCulture = culture;
            Thread.CurrentThread.CurrentUICulture = culture;
        }
    }
}
