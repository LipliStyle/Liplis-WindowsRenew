﻿//=======================================================================
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
            this.BTN              = getIconPath("btn.png");
            this.CLOCK_0          = getIconPath("clock_0.png");
            this.CLOCK_1          = getIconPath("clock_1.png");
            this.CLOCK_2          = getIconPath("clock_2.png");
            this.CLOCK_3          = getIconPath("clock_3.png");
            this.CLOCK_4          = getIconPath("clock_4.png");
            this.CLOCK_5          = getIconPath("clock_5.png");
            this.CLOCK_6          = getIconPath("clock_6.png");
            this.CLOCK_7          = getIconPath("clock_7.png");
            this.CLOCK_8          = getIconPath("clock_8.png");
            this.CLOCK_9          = getIconPath("clock_9.png");
            this.CLOCK_CONMA      = getIconPath("clock_conma.png");
            this.CLOCK_SLASH      = getIconPath("clock_slash.png");
            this.ICON             = getIconPath("icon.png");
            this.ICO_BACK         = getIconPath("ico_back.png");
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
            this.ICO_INTRODUCTION = getIconPath("ico_intro");
            this.SETTING          = getIconPath("setting.png");
            this.WINDOW           = getIconPath("window.png");
            this.WINDOW_BLUE      = getIconPath("window_blue.png");
            this.WINDOW_GREEN     = getIconPath("window_green.png");
            this.WINDOW_PINK      = getIconPath("window_pink.png");
            this.WINDOW_PURPLE    = getIconPath("window_purple.png");
            this.WINDOW_RED       = getIconPath("window_red.png");
            this.WINDOW_YELLOW    = getIconPath("window_yellow.png");
        }

        /// <summary>
        /// アイコンパスを設定する
        /// </summary>
        /// <param name="iconFileName"></param>
        private string getIconPath(string iconFileName)
        {
            string iconPath = "";

            //アイコンパスの設定
            iconPath = this.liplisWindowDirPath + iconFileName;

            //アイコンが見つかれば、パスを返す
            if(LpsLiplisUtil.ExistsFile(iconPath))
            {
                return iconPath;
            }

            //デフォルト無地アイコンパスの設定
            iconPath = this.liplisWindowDirPath + "ico_back.png";

            //アイコンが見つからなければデフォルトアイコンを返す
            if (LpsLiplisUtil.ExistsFile(iconPath))
            {
                return iconPath;
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

    }
}
