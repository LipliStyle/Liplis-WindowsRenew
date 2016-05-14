//=======================================================================
//  ClassName : LiplisWidgetSetting
//  概要      : リプリス設定
//
//Liplis5.0
//
//アップデート履歴
//   2016/05/08 ver5.0.0 作成
//
//  Copyright(c) 2010-2016 LipliStyle.Sachin
//=======================================================================
using Liplis.Com;
using Liplis.Utl;
using Liplis.Xml;
using System;
using System.Reflection;

namespace Liplis.MainSystem
{
    public class LiplisWidgetPreference : SharedPreferences
    {
        ///=============================
        /// JSON管理
        public string key { get; set; }

        ///=============================
        /// プロパティ
        public string charName              ;         public const string KEY_CHAR_NAME              = "charName";               //0
        public Int32 locationX               = 0;     public const string KEY_LOCATION_X             = "locationX";              //1
        public Int32 locationY               = 0;     public const string KEY_LOCATION_Y             = "locationY";              //2
        public Int32 lpsMode                 = 0;     public const string KEY_LPSMODE                = "lpsMode";                //3
        public Int32 lpsSpeed               ;         public const string KEY_LPSSPEED               = "lpsSpeed";               //5
        public Int32 lpsWindow              ;         public const string KEY_LPSWINDOW              = "lpsWindow";              //6
        public Int32 lpsDisplayIcon         ;         public const string KEY_LPSDISPLAYICON         = "lpsDisplayIcon";         //7
        public Int32 lpsHealth              ;         public const string KEY_LPSHELTH               = "lpsHealth";              //8

        public Int32 lpsNewsRange           ;         public const string KEY_LPSNEWSRANGE           = "lpsNewsRange";           //9
        public Int32 lpsNewsAlready         ;         public const string KEY_LPSNEWSALREADY         = "lpsNewsAlready";         //10
        public Int32 lpsNewsRunOut          ;         public const string KEY_LPSNEWSRUNOUT          = "lpsNewsRunOut";          //11
                                                       
        public Int32 lpsTopicNews           ;         public const string KEY_LPSTOPIC_NEWS          = "lpsTopicNews";           //12
        public Int32 lpsTopic2ch            ;         public const string KEY_LPSTOPIC_2CH           = "lpsTopic2ch";            //13
        public Int32 lpsTopicNico           ;         public const string KEY_LPSTOPIC_NICO          = "lpsTopicNico";           //14
        public Int32 lpsTopicRss            ;         public const string KEY_LPSTOPIC_RSS           = "lpsTopicRss";            //15
        public Int32 lpsTopicTwitter        ;         public const string KEY_LPSTOPIC_TWITTER       = "lpsTopicTwitter";        //16
        public Int32 lpsTopicTwitterPu      ;         public const string KEY_LPSTOPIC_TWITTERPU     = "lpsTopicTwitterPu";      //17
        public Int32 lpsTopicTwitterMy      ;         public const string KEY_LPSTOPIC_TWITTERMY     = "lpsTopicTwitterMy";      //18
        public Int32 lpsTopicTwitterMode    ;         public const string KEY_LPSTOPIC_TWITTERMODE   = "lpsTopicTwitterMode";    //19
        public Int32 lpsTopicCharMsg        ;         public const string KEY_LPSTOPIC_TOPICCHARMSG  = "lpsTopicCharMsg";        //20

        public string lpsWindowColor;
        public double lpsInterval;


        /// <summary>
        /// コンストラクター
        /// </summary>
        #region コンストラクター
        public LiplisWidgetPreference(string fileName):base(LpsPathController.getSettingPath() + fileName)
        {
            try
            {
                //キーセット
                this.key = fileName;

                //読み込み
                this.setData();

                //モード設定
                this.setMode();

                //ウインドウ設定
                this.setWindow();
            }
            catch (Exception err)
            {
                //設定ファイルの読み込みエラーの旨、異常位置を知らせるウインドウを出すべき
                LpsLogController.writingLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, LpsPathController.getSettingFilePath() + "が存在しないため作成します" + Environment.NewLine + err);
            }
        }
        public LiplisWidgetPreference() : base()
        {
            try
            {
                //キーセット
                this.key = LpsLiplisUtil.getName(20);

                //読み込み
                this.setInitData();

                //モード設定
                this.setMode();

                //ウインドウ設定
                this.setWindow();

                //保存
                this.xmlFilePath = LpsPathController. getSettingPath() + this.key;
                setPreferenceData();
            }
            catch (Exception err)
            {
                //設定ファイルの読み込みエラーの旨、異常位置を知らせるウインドウを出すべき
                LpsLogController.writingLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, LpsPathController.getSettingFilePath() + "が存在しないため作成します" + Environment.NewLine + err);
            }
        }
        #endregion

        /// <summary>
        /// デフォルト設定
        /// </summary>
        public void setInitData()
        {
            this.charName       = "";
            this.locationX      = 0;
            this.locationY      = 0;
            this.lpsMode        = 0;
            this.lpsSpeed       = 0;
            this.lpsWindow      = 0;
            this.lpsDisplayIcon = 1;
            this.lpsNewsRange   = 2;
            this.lpsNewsAlready = 0;
            this.lpsNewsRunOut  = 0;
            this.lpsHealth      = 1;


            this.lpsTopicNews        = 1;
            this.lpsTopic2ch         = 0;
            this.lpsTopicNico        = 0;
            this.lpsTopicRss         = 0;
            this.lpsTopicTwitter     = 0;
            this.lpsTopicTwitterPu   = 0;
            this.lpsTopicTwitterMy   = 0;
            this.lpsTopicTwitterMode = 0;
            this.lpsTopicCharMsg     = 0;
        }


        /// <summary>
        /// 設定の読み込み
        /// </summary>
        public void setData()
        {
            this.charName            = getString(KEY_CHAR_NAME,"");
            this.locationX           = getInt(KEY_LOCATION_X,0);
            this.locationY           = getInt(KEY_LOCATION_Y, 0);
            this.lpsMode             = getInt(KEY_LPSMODE, 0);
            this.lpsSpeed            = getInt(KEY_LPSSPEED, 0);
            this.lpsWindow           = getInt(KEY_LPSWINDOW, 0);
            this.lpsDisplayIcon      = getInt(KEY_LPSDISPLAYICON, 0);
            this.lpsHealth           = getInt(KEY_LPSHELTH, 0);
            this.lpsNewsRange        = getInt(KEY_LPSNEWSRANGE, 0);
            this.lpsNewsAlready      = getInt(KEY_LPSNEWSALREADY, 0);
            this.lpsNewsRunOut       = getInt(KEY_LPSNEWSRUNOUT, 0);
            this.lpsTopicNews        = getInt(KEY_LPSTOPIC_NEWS, 0);
            this.lpsTopic2ch         = getInt(KEY_LPSTOPIC_2CH, 0);
            this.lpsTopicNico        = getInt(KEY_LPSTOPIC_NICO, 0);
            this.lpsTopicRss         = getInt(KEY_LPSTOPIC_RSS, 0);
            this.lpsTopicTwitter     = getInt(KEY_LPSTOPIC_TWITTER, 0);
            this.lpsTopicTwitterPu   = getInt(KEY_LPSTOPIC_TWITTERPU, 0);
            this.lpsTopicTwitterMy   = getInt(KEY_LPSTOPIC_TWITTERMY, 0);
            this.lpsTopicTwitterMode = getInt(KEY_LPSTOPIC_TWITTERMODE, 0);
            this.lpsTopicCharMsg     = getInt(KEY_LPSTOPIC_TOPICCHARMSG, 0);
        }

        /// <summary>
        /// 設定の保存
        /// </summary>
        public void setPreferenceData()
        {
            setString(KEY_CHAR_NAME, this.charName);
            setInt(KEY_LOCATION_X, this.locationX);
            setInt(KEY_LOCATION_Y, this.locationY);
            setInt(KEY_LPSMODE, this.lpsMode);
            setInt(KEY_LPSSPEED, this.lpsSpeed);
            setInt(KEY_LPSWINDOW, this.lpsWindow);
            setInt(KEY_LPSDISPLAYICON, this.lpsDisplayIcon);
            setInt(KEY_LPSHELTH, this.lpsHealth);
            setInt(KEY_LPSNEWSRANGE, this.lpsNewsRange);
            setInt(KEY_LPSNEWSALREADY, this.lpsNewsAlready);
            setInt(KEY_LPSNEWSRUNOUT, this.lpsNewsRunOut);
            setInt(KEY_LPSTOPIC_NEWS, this.lpsTopicNews);
            setInt(KEY_LPSTOPIC_2CH, this.lpsTopic2ch);
            setInt(KEY_LPSTOPIC_NICO, this.lpsTopicNico);
            setInt(KEY_LPSTOPIC_RSS, this.lpsTopicRss);
            setInt(KEY_LPSTOPIC_TWITTER, this.lpsTopicTwitter);
            setInt(KEY_LPSTOPIC_TWITTERPU, this.lpsTopicTwitterPu);
            setInt(KEY_LPSTOPIC_TWITTERMY, this.lpsTopicTwitterMy);
            setInt(KEY_LPSTOPIC_TWITTERMODE, this.lpsTopicTwitterMode);
            setInt(KEY_LPSTOPIC_TOPICCHARMSG, this.lpsTopicCharMsg);
            saveSettings();
        }

        //============================================================
        //
        //設定の取得
        //
        //============================================================
        #region 設定の取得
        /// <summary>
        /// ニュースフラグの取得
        /// </summary>
        public string getNewsFlg()
        {
            return lpsTopicNews + "," + lpsTopic2ch + "," + lpsTopicNico + "," + lpsTopicRss + "," + lpsTopicTwitterPu + "," + lpsTopicTwitterMy + "," + lpsTopicTwitter;
        }
        #endregion

        //============================================================
        //
        //設定処理
        //
        //============================================================


        /// <summary>
        /// モードとインターバルの設定
        /// </summary>
        private void setMode()
        {
            if (this.lpsMode == 0)
            {
                this.lpsInterval = 10.0;
            }
            else if (this.lpsMode == 1)
            {
                this.lpsInterval = 30.0;
            }
            else if (this.lpsMode == 2)
            {
                this.lpsInterval = 60.0;
            }
            else
            {
                this.lpsInterval = 10.0;
            }
        }

        /// <summary>
        /// ウインドウカラーを設定する
        /// </summary>
        private void setWindow()
        {
            switch (lpsWindow)
            {
                case 0:
                    lpsWindowColor = "window.png";
                    break;
            case 1:
                    lpsWindowColor = "window_blue.png";
                    break;
            case 2:
                    lpsWindowColor = "window_green.png";
                    break;
            case 3:
                    lpsWindowColor = "window_pink.png";
                    break;
            case 4:
                    lpsWindowColor = "window_purple.png";
                    break;
            case 5:
                    lpsWindowColor = "window_red.png";
                    break;
            case 6:
                    lpsWindowColor = "window_yellow.png";
                    break;
            default:
                    lpsWindowColor = "window.png";
                    break;
            }
        }
    }
}
