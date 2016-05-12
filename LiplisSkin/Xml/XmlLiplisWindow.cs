//=======================================================================
//  ClassName : XmlVersion
//  概要      : window.xmlの実体
//              window.xmlを読みこませることでインスタンス化、使うことができる。
//
//  Liplis5.0
//
//  Copyright(c) 2010-2016 LipliStyle.Sachin
//=======================================================================

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
        public string INTRODUCTION { get; set; }
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
            this.BATTERY_0        = this.liplisWindowDirPath + "battery_0.png";
            this.BATTERY_100      = this.liplisWindowDirPath + "battery_100.png";
            this.BATTERY_12       = this.liplisWindowDirPath + "battery_12.png";
            this.BATTERY_25       = this.liplisWindowDirPath + "battery_25.png";
            this.BATTERY_37       = this.liplisWindowDirPath + "battery_37.png";
            this.BATTERY_50       = this.liplisWindowDirPath + "battery_50.png";
            this.BATTERY_62       = this.liplisWindowDirPath + "battery_62.png";
            this.BATTERY_75       = this.liplisWindowDirPath + "battery_75.png";
            this.BATTERY_87       = this.liplisWindowDirPath + "battery_87.png";
            this.BATTERY_NON      = this.liplisWindowDirPath + "battery_non.png";
            this.BTN              = this.liplisWindowDirPath + "btn.png";
            this.CLOCK_0          = this.liplisWindowDirPath + "clock_0.png";
            this.CLOCK_1          = this.liplisWindowDirPath + "clock_1.png";
            this.CLOCK_2          = this.liplisWindowDirPath + "clock_2.png";
            this.CLOCK_3          = this.liplisWindowDirPath + "clock_3.png";
            this.CLOCK_4          = this.liplisWindowDirPath + "clock_4.png";
            this.CLOCK_5          = this.liplisWindowDirPath + "clock_5.png";
            this.CLOCK_6          = this.liplisWindowDirPath + "clock_6.png";
            this.CLOCK_7          = this.liplisWindowDirPath + "clock_7.png";
            this.CLOCK_8          = this.liplisWindowDirPath + "clock_8.png";
            this.CLOCK_9          = this.liplisWindowDirPath + "clock_9.png";
            this.CLOCK_CONMA      = this.liplisWindowDirPath + "clock_conma.png";
            this.CLOCK_SLASH      = this.liplisWindowDirPath + "clock_slash.png";
            this.ICON             = this.liplisWindowDirPath + "icon.png";
            this.ICO_BACK         = this.liplisWindowDirPath + "ico_back.png";
            this.ICO_BATTERYGAGE  = this.liplisWindowDirPath + "ico_batterygage.png";
            this.ICO_CHAR         = this.liplisWindowDirPath + "ico_char.png";
            this.ICO_LOG          = this.liplisWindowDirPath + "ico_log.png";
            this.ICO_NEXT         = this.liplisWindowDirPath + "ico_next.png";
            this.ICO_POW          = this.liplisWindowDirPath + "ico_pow.png";
            this.ICO_RSS          = this.liplisWindowDirPath + "ico_rss.png";
            this.ICO_SETTING      = this.liplisWindowDirPath + "ico_setting.png";
            this.ICO_THINKING     = this.liplisWindowDirPath + "ico_thinking.png";
            this.ICO_THINKING_NOT = this.liplisWindowDirPath + "ico_thinking_not.png";
            this.ICO_TRAY         = this.liplisWindowDirPath + "ico_tray.png";
            this.ICO_WAIKUP       = this.liplisWindowDirPath + "ico_waikup.png";
            this.ICO_ZZZ          = this.liplisWindowDirPath + "ico_zzz.png";
            this.INTRODUCTION     = this.liplisWindowDirPath + "introduction.png";
            this.SETTING          = this.liplisWindowDirPath + "setting.png";
            this.WINDOW           = this.liplisWindowDirPath + "window.png";
            this.WINDOW_BLUE      = this.liplisWindowDirPath + "window_blue.png";
            this.WINDOW_GREEN     = this.liplisWindowDirPath + "window_green.png";
            this.WINDOW_PINK      = this.liplisWindowDirPath + "window_pink.png";
            this.WINDOW_PURPLE    = this.liplisWindowDirPath + "window_purple.png";
            this.WINDOW_RED       = this.liplisWindowDirPath + "window_red.png";
            this.WINDOW_YELLOW    = this.liplisWindowDirPath + "window_yellow.png";

        }
    }
}
