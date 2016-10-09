//=======================================================================
//  ClassName : XmlVersion
//  概要      : window.xmlの実体
//              window.xmlを読みこませることでインスタンス化、使うことができる。
//
//  Liplis5.0
//
//  Copyright(c) 2010-2016 LipliStyle.Sachin
//=======================================================================

using Liplis.Com;
using Liplis.Utl;
using System;
using System.Drawing;

namespace Liplis.Xml
{
    public class XmlLiplisWindow
    {
        ///=============================
        /// ウインドウディレクトリパス
        private string liplisWindowDirPath;

        ///=============================
        /// パーツファイルパス
        public string BATTERY_0 { get; set; }
        public string BATTERY_100 { get; set; }
        public string BATTERY_12 { get; set; }
        public string BATTERY_25 { get; set; }
        public string BATTERY_37 { get; set; }
        public string BATTERY_50 { get; set; }
        public string BATTERY_62 { get; set; }
        public string BATTERY_75 { get; set; }
        public string BATTERY_87 { get; set; }
        public string BATTERY_NON { get; set; }
        public string BTN { get; set; }
        public string CLOCK_0 { get; set; }
        public string CLOCK_1 { get; set; }
        public string CLOCK_2 { get; set; }
        public string CLOCK_3 { get; set; }
        public string CLOCK_4 { get; set; }
        public string CLOCK_5 { get; set; }
        public string CLOCK_6 { get; set; }
        public string CLOCK_7 { get; set; }
        public string CLOCK_8 { get; set; }
        public string CLOCK_9 { get; set; }
        public string CLOCK_CONMA { get; set; }
        public string CLOCK_SLASH { get; set; }
        public string ICON { get; set; }
        public string ICO_BACK { get; set; }
        public string ICO_BATTERYGAGE { get; set; }
        public string ICO_CHAR { get; set; }
        public string ICO_LOG { get; set; }
        public string ICO_NEXT { get; set; }
        public string ICO_POW { get; set; }
        public string ICO_RSS { get; set; }
        public string ICO_SETTING { get; set; }
        public string ICO_THINKING { get; set; }
        public string ICO_THINKING_NOT { get; set; }
        public string ICO_TRAY { get; set; }
        public string ICO_WAIKUP { get; set; }
        public string ICO_ZZZ { get; set; }
        public string ICO_INTRODUCTION { get; set; }
        public string SETTING { get; set; }
        public string WINDOW { get; set; }
        public string WINDOW_BLUE { get; set; }
        public string WINDOW_GREEN { get; set; }
        public string WINDOW_PINK { get; set; }
        public string WINDOW_PURPLE { get; set; }
        public string WINDOW_RED { get; set; }
        public string WINDOW_YELLOW { get; set; }

        ///=============================
        /// 定義
        public const string DEF_BASE_ICON_NAME = "ico_back.png";

        /// <summary>
        /// コンストラクター
        /// </summary>
        /// <param name="loadSkin"></param>
        public XmlLiplisWindow(string loadSkin)
        {
            //ベースディレクトリの設定
            this.liplisWindowDirPath = LpsPathController.getWindowPath(loadSkin);

            //ファイルパスの初期化
            initFilePath();
        }

        /// <summary>
        /// ファイルパスの初期化
        /// </summary>
        private void initFilePath()
        {
            this.BATTERY_0        = getIconPath("battery_0.png");
            this.BATTERY_100      = getIconPath("battery_100.png");
            this.BATTERY_12       = getIconPath("battery_12.png");
            this.BATTERY_25       = getIconPath("battery_25.png");
            this.BATTERY_37       = getIconPath("battery_37.png");
            this.BATTERY_50       = getIconPath("battery_50.png");
            this.BATTERY_62       = getIconPath("battery_62.png");
            this.BATTERY_75       = getIconPath("battery_75.png");
            this.BATTERY_87       = getIconPath("battery_87.png");
            this.BATTERY_NON      = getIconPath("battery_non.png");
            //this.BTN              = getIconPath("btn.png");
            //this.CLOCK_0          = getIconPath("clock_0.png");
            //this.CLOCK_1          = getIconPath("clock_1.png");
            //this.CLOCK_2          = getIconPath("clock_2.png");
            //this.CLOCK_3          = getIconPath("clock_3.png");
            //this.CLOCK_4          = getIconPath("clock_4.png");
            //this.CLOCK_5          = getIconPath("clock_5.png");
            //this.CLOCK_6          = getIconPath("clock_6.png");
            //this.CLOCK_7          = getIconPath("clock_7.png");
            //this.CLOCK_8          = getIconPath("clock_8.png");
            //this.CLOCK_9          = getIconPath("clock_9.png");
            //this.CLOCK_CONMA      = getIconPath("clock_conma.png");
            //this.CLOCK_SLASH      = getIconPath("clock_slash.png");
            this.ICON             = getIconPath("icon.png");
            this.ICO_BACK         = getIconPath(DEF_BASE_ICON_NAME);
            this.ICO_BATTERYGAGE  = getIconPath("ico_batterygage.png");
            this.ICO_CHAR         = getIconPath("ico_char.png");
            this.ICO_LOG          = getIconPath("ico_log.png");
            this.ICO_NEXT         = getIconPath("ico_next.png");
            this.ICO_POW          = getIconPath("ico_pow.png");
            this.ICO_RSS          = getIconPath("ico_rss.png");
            this.ICO_SETTING      = getIconPath("ico_setting.png");
            this.ICO_THINKING     = getIconPath("ico_thinking.png");
            this.ICO_THINKING_NOT = getIconPath("ico_thinking_not.png");
            this.ICO_TRAY         = getIconPath("ico_tray.png");
            this.ICO_WAIKUP       = getIconPath("ico_waikup.png");
            this.ICO_ZZZ          = getIconPath("ico_zzz.png");
            this.ICO_INTRODUCTION = getIconPath("ico_intro.png");
            this.SETTING          = getIconPath("setting.png");
            this.WINDOW           = getWindowPath("window.png");
            this.WINDOW_BLUE      = getWindowPath("window_blue.png");
            this.WINDOW_GREEN     = getWindowPath("window_green.png");
            this.WINDOW_PINK      = getWindowPath("window_pink.png");
            this.WINDOW_PURPLE    = getWindowPath("window_purple.png");
            this.WINDOW_RED       = getWindowPath("window_red.png");
            this.WINDOW_YELLOW    = getWindowPath("window_yellow.png");
        }

        /// <summary>
        /// アイコンパスを設定する
        /// </summary>
        /// <param name="iconFileName"></param>
        private string getIconPath(string iconFileName)
        {
            string iconPath = "";
            string iconBasePath = "";

            //アイコンパスの設定
            iconPath = this.liplisWindowDirPath + iconFileName;

            //アイコンが見つかれば、パスを返す
            if(LpsLiplisUtil.ExistsFile(iconPath))
            {
                return iconPath;
            }

            //デフォルト無地アイコンパスの設定
            iconBasePath = this.liplisWindowDirPath + DEF_BASE_ICON_NAME;

            //アイコンが見つからなければデフォルトアイコンを返す
            if (LpsLiplisUtil.ExistsFile(iconBasePath))
            {
                //アイコン生成を試みる
                if(createIcon(iconFileName))
                {
                    return iconPath;
                }
                else
                {
                    return iconBasePath;
                }
            }

            //デフォルト無地アイコンパスの設定
            iconPath = LpsPathController.getAppPath() + "\\Skin\\LiliRenew\\window\\" + "ico_back.png";

            if (LpsLiplisUtil.ExistsFile(iconPath))
            {
                return iconPath;
            }
            else
            {
                throw new System.Exception("");
            }
        }

        /// <summary>
        /// アイコンパスを設定する
        /// </summary>
        /// <param name="iconFileName"></param>
        public string getBatteryIcon(double batteryNowLevel)
        {
            if (batteryNowLevel <= 10)
            {
                return this.BATTERY_0;
            }
            else if (batteryNowLevel <= 12)
            {
                return this.BATTERY_12;
            }
            else if (batteryNowLevel <= 25)
            {
                return this.BATTERY_25;
            }
            else if (batteryNowLevel <= 37)
            {
                return this.BATTERY_37;
            }
            else if (batteryNowLevel <= 50)
            {
                return this.BATTERY_50;
            }
            else if (batteryNowLevel <= 62)
            {
                return this.BATTERY_62;
            }
            else if (batteryNowLevel <= 75)
            {
                return this.BATTERY_75;
            }
            else if (batteryNowLevel <= 87)
            {
                return this.BATTERY_87;
            }
            else if (batteryNowLevel > 87 && batteryNowLevel <= 100)
            {
                return this.BATTERY_100;
            }
            else
            {
                return this.BATTERY_100;
            }
        }

        /// <summary>
        /// ウインドウパスを取得する
        /// </summary>
        /// <param name="iconFileName"></param>
        /// <returns></returns>
        private string getWindowPath(string iconFileName)
        {
            string iconPath = "";

            //アイコンパスの設定
            iconPath = this.liplisWindowDirPath + iconFileName;

            //アイコンが見つかれば、パスを返す
            if (LpsLiplisUtil.ExistsFile(iconPath))
            {
                return iconPath;
            }

            //デフォルト無地アイコンパスの設定
            iconPath = LpsPathController.getAppPath() + "\\Skin\\LiliRenew\\window\\window.png";

            if (LpsLiplisUtil.ExistsFile(iconPath))
            {
                return iconPath;
            }
            else
            {
                throw new System.Exception("");
            }
        }


        /// <summary>
        /// アイコンを生成する。
        /// 生成に成功したらtrue,失敗ならfalseを返す
        /// </summary>
        /// <param name="iconFileName"></param>
        /// <returns></returns>
        private bool createIcon(string iconFileName)
        {
            try
            {
                string iconPath = this.liplisWindowDirPath + iconFileName;
                string iconBasePath = this.liplisWindowDirPath + DEF_BASE_ICON_NAME;

                using (Bitmap img1 = new Bitmap(iconBasePath))
                {
                    using (Bitmap img2 = getDefaultImage(iconFileName))
                    {
                        using (Bitmap img3 = new Bitmap(img1))
                        {
                            using (Graphics g = Graphics.FromImage(img3))
                            {
                                g.DrawImage(img2, img3.Width - img2.Width, img3.Height - img2.Height, img2.Width, img2.Height);
                                img3.Save(iconPath, System.Drawing.Imaging.ImageFormat.Png);
                            }
                        }
                    }

                }

                return true;
            }
            catch(Exception ex)
            {
                return false;
            }
        }

        /// <summary>
        /// デフォルトイメージアイコンの取得
        /// </summary>
        /// <param name="iconFileName"></param>
        /// <returns></returns>
        private Bitmap getDefaultImage(string iconFileName)
        {
            switch(iconFileName)
            {
                case "battery_0.png": return Properties.Resources.def_battery_0;
                case "battery_100.png": return Properties.Resources.def_battery_100;
                case "battery_12.png": return Properties.Resources.def_battery_12;
                case "battery_25.png": return Properties.Resources.def_battery_25;
                case "battery_37.png": return Properties.Resources.def_battery_37;
                case "battery_50.png": return Properties.Resources.def_battery_50;
                case "battery_62.png": return Properties.Resources.def_battery_62;
                case "battery_75.png": return Properties.Resources.def_battery_75;
                case "battery_87.png": return Properties.Resources.def_battery_87;
                case "battery_non.png": return Properties.Resources.def_battery_non;
                case "btn.png": return Properties.Resources.def_btn;
                case "clock_0.png": return Properties.Resources.def_clock_0;
                case "clock_1.png": return Properties.Resources.def_clock_1;
                case "clock_2.png": return Properties.Resources.def_clock_2;
                case "clock_3.png": return Properties.Resources.def_clock_3;
                case "clock_4.png": return Properties.Resources.def_clock_4;
                case "clock_5.png": return Properties.Resources.def_clock_5;
                case "clock_6.png": return Properties.Resources.def_clock_6;
                case "clock_7.png": return Properties.Resources.def_clock_7;
                case "clock_8.png": return Properties.Resources.def_clock_8;
                case "clock_9.png": return Properties.Resources.def_clock_9;
                case "clock_conma.png": return Properties.Resources.def_clock_conma;
                case "clock_slash.png": return Properties.Resources.def_clock_slash;
                case "icon.png": return Properties.Resources.def_icon;
                case DEF_BASE_ICON_NAME: return Properties.Resources.ico_back;
                case "ico_batterygage.png": return Properties.Resources.def_ico_batterygage;
                case "ico_char.png": return Properties.Resources.def_ico_char;
                case "ico_log.png": return Properties.Resources.def_ico_log;
                case "ico_next.png": return Properties.Resources.def_ico_next;
                case "ico_pow.png": return Properties.Resources.def_ico_pow;
                case "ico_rss.png": return Properties.Resources.def_ico_rss;
                case "ico_setting.png": return Properties.Resources.def_ico_setting;
                case "ico_thinking.png": return Properties.Resources.def_ico_thinking;
                case "ico_thinking_not.png": return Properties.Resources.def_ico_thinking_not;
                case "ico_tray.png": return Properties.Resources.def_ico_tray;
                case "ico_waikup.png": return Properties.Resources.def_ico_waikup;
                case "ico_zzz.png": return Properties.Resources.def_ico_zzz;
                case "ico_intro.png": return Properties.Resources.def_ico_intro;
                case "setting.png": return Properties.Resources.setting;
                default:return Properties.Resources.ico_back;
            }
        }







        /// <summary>
        /// ウインドウパスを取得する
        /// </summary>
        /// <param name="lpsWindow"></param>
        /// <returns></returns>
        public string getWindowPath(int lpsWindow)
        {
            switch (lpsWindow)
            {
                case 0:
                    return WINDOW;
                case 1:
                    return WINDOW_BLUE;
                case 2:
                    return WINDOW_GREEN;
                case 3:
                    return WINDOW_PINK;
                case 4:
                    return WINDOW_PURPLE;
                case 5:
                    return WINDOW_RED;
                case 6:
                    return WINDOW_YELLOW;
                default:
                    return WINDOW;
            }



        }

    }
}
