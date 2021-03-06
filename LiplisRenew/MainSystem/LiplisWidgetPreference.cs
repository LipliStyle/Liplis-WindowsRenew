﻿//=======================================================================
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
using Liplis.Voc;
using Liplis.Widget.LpsWindow;
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
        public string charName;                         public const string KEY_CHAR_NAME       = "charName";              
        public Int32 locationX= 0;                      public const string KEY_LOCATION_X      = "locationX";             
        public Int32 locationY= 0;                      public const string KEY_LOCATION_Y      = "locationY";
        public Int32 lpsTalkMode = 0;                   public const string KEY_LPSTALKMODE     = "lpsTalkMode";
        public Int32 lpsMode  = 0;                      public const string KEY_LPSMODE         = "lpsMode";               
        public Int32 lpsSpeed;                          public const string KEY_LPSSPEED        = "lpsSpeed";              
        public Int32 lpsWindow;                         public const string KEY_LPSWINDOW       = "lpsWindow";             
        public Int32 lpsDisplayIcon;                    public const string KEY_LPSDISPLAYICON  = "lpsDisplayIcon";        
        public Int32 lpsHealth;                         public const string KEY_LPSHELTH        = "lpsHealth";

        public Int32 lpsTopicHour;                      public const string KEY_LPSNEWSRANGE     = "lpsNewsRange";           
        public Int32 lpsAlready;       　　             public const string KEY_LPSNEWSALREADY   = "lpsNewsAlready";     
        public Int32 lpsNewsRunOut;                     public const string KEY_LPSNEWSRUNOUT    = "lpsNewsRunOut";          

        public Int32 lpsTopicNews;                      public const string KEY_LPSTOPIC_NEWS         = "lpsTopicNews";         
        public Int32 lpsTopic2ch;                       public const string KEY_LPSTOPIC_2CH          = "lpsTopic2ch";           
        public Int32 lpsTopicNico;                      public const string KEY_LPSTOPIC_NICO         = "lpsTopicNico";          
        public Int32 lpsTopicRss;                       public const string KEY_LPSTOPIC_RSS          = "lpsTopicRss";           
        public Int32 lpsTopicTwitter;                   public const string KEY_LPSTOPIC_TWITTER      = "lpsTopicTwitter";       
        public Int32 lpsTopicTwitterPu;                 public const string KEY_LPSTOPIC_TWITTERPU    = "lpsTopicTwitterPu";     
        public Int32 lpsTopicTwitterMy;                 public const string KEY_LPSTOPIC_TWITTERMY    = "lpsTopicTwitterMy";     
        public Int32 lpsTopicTwitterMode;               public const string KEY_LPSTOPIC_TWITTERMODE  = "lpsTopicTwitterMode";   
        public Int32 lpsTopicCharMsg;                   public const string KEY_LPSTOPIC_TOPICCHARMSG = "lpsTopicCharMsg";

        public Int32 lpsVoiceOn;                        public const string KEY_LPS_VOICEROID_ON = "lpsVoiceOn";
        public string lpsVoiceName;                     public const string KEY_LPS_VOICEROID_NAME = "lpsVoiceName";
        public string lpsVoicePath;                     public const string KEY_LPS_VOICEROID_PATH = "lpsVoicePath";

        public LiplisWindowStack lpsWindowPos;          public const string KEY_LPS_WINDOW_POS = "lpsWindowPos";    //0:左中右,1:右,2:左
        public Int32 lpsWindowNum;                      public const string KEY_LPS_WINDOW_NUM = "lpsWindowNum";


        public string lpsWindowColor;
        public double lpsInterval;


        /// <summary>
        /// コンストラクター
        /// </summary>
        #region コンストラクター
        public LiplisWidgetPreference(string fileName) : base(LpsPathController.getSettingPath() + fileName)
        {
            try
            {
                //キーセット
                this.key = fileName;

                //読み込み
                this.setData();

                //モード設定
                this.setMode();
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

                //保存
                this.xmlFilePath = LpsPathController.getSettingPath() + this.key;
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
            this.charName = "";
            this.locationX = 0;
            this.locationY = 0;
            this.lpsTalkMode = 0;
            this.lpsMode = 0;
            this.lpsSpeed = 75;
            this.lpsWindow = 0;
            this.lpsDisplayIcon = 1;
            this.lpsTopicHour = 2;
            this.lpsAlready = 0;
            this.lpsNewsRunOut = 0;
            this.lpsHealth = 1;

            this.lpsTopicNews = 1;
            this.lpsTopic2ch = 0;
            this.lpsTopicNico = 0;
            this.lpsTopicRss = 0;
            this.lpsTopicTwitter = 0;
            this.lpsTopicTwitterPu = 0;
            this.lpsTopicTwitterMy = 0;
            this.lpsTopicTwitterMode = 0;
            this.lpsTopicCharMsg = 0;
            this.lpsVoiceOn = 0;
            this.lpsVoiceName = "";
            this.lpsVoicePath = "";

            this.lpsWindowPos = LiplisWindowStack.LeeftStack;  //デフォルト表示位置左
            this.lpsWindowNum = 5;  //でおフォルト表示数5
    }


        /// <summary>
        /// 設定の読み込み
        /// </summary>
        public void setData()
        {
            this.charName            = getString(KEY_CHAR_NAME, "");
            this.locationX           = getInt(KEY_LOCATION_X, 0);
            this.locationY           = getInt(KEY_LOCATION_Y, 0);
            this.lpsTalkMode         = getInt(KEY_LPSTALKMODE, 0);
            this.lpsMode             = getInt(KEY_LPSMODE, 0);
            this.lpsSpeed            = getInt(KEY_LPSSPEED, 75);
            this.lpsWindow           = getInt(KEY_LPSWINDOW, 0);
            this.lpsDisplayIcon      = getInt(KEY_LPSDISPLAYICON, 0);
            this.lpsHealth           = getInt(KEY_LPSHELTH, 0);
            this.lpsTopicHour        = getInt(KEY_LPSNEWSRANGE, 0);
            this.lpsAlready          = getInt(KEY_LPSNEWSALREADY, 0);
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
            this.lpsVoiceOn          = getInt(KEY_LPS_VOICEROID_ON, 0);
            this.lpsVoiceName        = getString(KEY_LPS_VOICEROID_NAME, "");
            this.lpsVoicePath        = getString(KEY_LPS_VOICEROID_PATH, "");
            this.lpsWindowPos        = (LiplisWindowStack)getInt(KEY_LPS_WINDOW_POS, (int)LiplisWindowStack.LeeftStack);
            this.lpsWindowNum        = getInt(KEY_LPS_WINDOW_NUM, 5);
        }

        /// <summary>
        /// 設定の保存
        /// </summary>
        public void setPreferenceData()
        {
            setString(KEY_CHAR_NAME, this.charName);
            setInt(KEY_LOCATION_X, this.locationX);
            setInt(KEY_LOCATION_Y, this.locationY);
            setInt(KEY_LPSTALKMODE, this.lpsTalkMode);
            setInt(KEY_LPSMODE, this.lpsMode);
            setInt(KEY_LPSSPEED, this.lpsSpeed);
            setInt(KEY_LPSWINDOW, this.lpsWindow);
            setInt(KEY_LPSDISPLAYICON, this.lpsDisplayIcon);
            setInt(KEY_LPSHELTH, this.lpsHealth);
            setInt(KEY_LPSNEWSRANGE, this.lpsTopicHour);
            setInt(KEY_LPSNEWSALREADY, this.lpsAlready);
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
            setInt(KEY_LPS_VOICEROID_ON, this.lpsVoiceOn);
            setString(KEY_LPS_VOICEROID_NAME, this.lpsVoiceName);
            setString(KEY_LPS_VOICEROID_PATH, this.lpsVoicePath);
            setInt(KEY_LPS_WINDOW_POS, (int)this.lpsWindowPos);
            setInt(KEY_LPS_WINDOW_NUM, this.lpsWindowNum);

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


        /// <summary>
        /// ボイスロイド取得
        /// </summary>
        /// <returns></returns>
        public LpsVoiceRoid getSelectedVoiceRoid()
        {
            return EnableVoiceRoid.GetInstance().getSelectedVoiceRoid(this.lpsVoicePath);
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
            switch (lpsMode)
            {
                case 0:
                    this.lpsInterval = 999999; //きまぐれ
                    this.lpsInterval = 5000;
                    break;
                case 1:
                    this.lpsInterval = 10000;
                    break;
                case 2:
                    this.lpsInterval = 20000;
                    break;
                case 3:
                    this.lpsInterval = 60000;
                    break;
                case 4:
                    this.lpsInterval = 120000;
                    break;
                case 5:
                    this.lpsInterval = 180000;
                    break;
                case 6:
                    this.lpsInterval = 0;
                    break;
                default:
                    this.lpsInterval = 10000;
                    break;
            }
        }

        /// <summary>
        /// ロケーションを設定する
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public void setLocation(int x, int y)
        {
            this.locationX = x;
            this.locationY = y;
            setPreferenceData();
        }



    }
}
