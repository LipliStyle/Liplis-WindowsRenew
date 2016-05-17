//=======================================================================
//  ClassName : LiplisPreference
//  概要      : リプリス設定
//
//Liplis5.0
//
//アップデート履歴
//   2016/05/08 ver5.0.0 作成
//
//  Copyright(c) 2010-2016 LipliStyle.Sachin
//=======================================================================
using Liplis.Utl;
using Liplis.Xml;
using System;
using System.Reflection;

namespace Liplis.MainSystem
{
    public class LiplisPreference : SharedPreferences
    {
        ///=============================
        /// プロパティ
        public string uid;                  public const string PREFS_UID = "uid";
        public Int32 lpsAutoSleep;          public const string PREFS_AUTO_SLEEP = "lpsAutoSleep";
        public Int32 lpsAutoWakeup;         public const string PREFS_AUTO_WAKEUP = "lpsAutoWakeup";

        public Int32 lpsNewsRange;          public const string KEY_LPSNEWSRANGE   = "lpsNewsRange";           //9
        public Int32 lpsNewsAlready;        public const string KEY_LPSNEWSALREADY = "lpsNewsAlready";         //10
        public Int32 lpsNewsRunOut;         public const string KEY_LPSNEWSRUNOUT  = "lpsNewsRunOut";          //11

        public Int32 lpsTopicNews;          public const string KEY_LPSTOPIC_NEWS         = "lpsTopicNews";           //12
        public Int32 lpsTopic2ch;           public const string KEY_LPSTOPIC_2CH          = "lpsTopic2ch";            //13
        public Int32 lpsTopicNico;          public const string KEY_LPSTOPIC_NICO         = "lpsTopicNico";           //14
        public Int32 lpsTopicRss;           public const string KEY_LPSTOPIC_RSS          = "lpsTopicRss";            //15
        public Int32 lpsTopicTwitter;       public const string KEY_LPSTOPIC_TWITTER      = "lpsTopicTwitter";        //16
        public Int32 lpsTopicTwitterPu;     public const string KEY_LPSTOPIC_TWITTERPU    = "lpsTopicTwitterPu";      //17
        public Int32 lpsTopicTwitterMy;     public const string KEY_LPSTOPIC_TWITTERMY    = "lpsTopicTwitterMy";      //18
        public Int32 lpsTopicTwitterMode;   public const string KEY_LPSTOPIC_TWITTERMODE  = "lpsTopicTwitterMode";    //19
        public Int32 lpsTopicCharMsg;       public const string KEY_LPSTOPIC_TOPICCHARMSG = "lpsTopicCharMsg";        //20




        /// <summary>
        /// コンストラクター
        /// </summary>
        #region コンストラクター
        public LiplisPreference():base(LpsPathController.getSettingFilePath())
        {
            //読み込み
            setData();
        }
        #endregion


        /// <summary>
        /// 設定の読み込み
        /// </summary>
        public void setData()
        {
            try
            {
                this.uid = getString(PREFS_UID, LpsGuidCreator.createLiplisGuid());
                this.lpsAutoSleep = getInt(PREFS_AUTO_SLEEP, 0);
                this.lpsAutoWakeup = getInt(PREFS_AUTO_WAKEUP, 0);
            }
            catch (Exception err)
            {
                LpsLogController.writingLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, "readResult:設定の読込失敗" + Environment.NewLine + err);
            }

        }

        /// <summary>
        /// 設定の保存
        /// </summary>
        public void setPreferenceData()
        {
        }
    }
}
