﻿//=======================================================================
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
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace Liplis.MainSystem
{
    public class LiplisPreference : SharedPreferences
    {
        ///=============================
        /// プロパティ
        public string uid;                  public const string PREFS_UID                 = "uid";
        public Int32 lpsAutoSleep;          public const string PREFS_AUTO_SLEEP          = "lpsAutoSleep";
        public Int32 lpsAutoWakeup;         public const string PREFS_AUTO_WAKEUP         = "lpsAutoWakeup";

        public Int32 lpsNewsRange;          public const string KEY_LPSNEWSRANGE          = "lpsNewsRange";       
        public Int32 lpsNewsAlready;        public const string KEY_LPSNEWSALREADY        = "lpsNewsAlready";     
        public Int32 lpsNewsRunOut;         public const string KEY_LPSNEWSRUNOUT         = "lpsNewsRunOut";      

        public Int32 lpsTopicNews;          public const string KEY_LPSTOPIC_NEWS         = "lpsTopicNews";       
        public Int32 lpsTopic2ch;           public const string KEY_LPSTOPIC_2CH          = "lpsTopic2ch";        
        public Int32 lpsTopicNico;          public const string KEY_LPSTOPIC_NICO         = "lpsTopicNico";       
        public Int32 lpsTopicRss;           public const string KEY_LPSTOPIC_RSS          = "lpsTopicRss";        
        public Int32 lpsTopicTwitter;       public const string KEY_LPSTOPIC_TWITTER      = "lpsTopicTwitter";    
        public Int32 lpsTopicTwitterPu;     public const string KEY_LPSTOPIC_TWITTERPU    = "lpsTopicTwitterPu";  
        public Int32 lpsTopicTwitterMy;     public const string KEY_LPSTOPIC_TWITTERMY    = "lpsTopicTwitterMy";  
        public Int32 lpsTopicTwitterMode;   public const string KEY_LPSTOPIC_TWITTERMODE  = "lpsTopicTwitterMode";
        public Int32 lpsTopicCharMsg;       public const string KEY_LPSTOPIC_TOPICCHARMSG = "lpsTopicCharMsg";

        public Int32 lpsTwitterActivate;    public const string KEY_TWITTER_ACTIVATE      = "lpsTwitterActivate";


        public List<voiceRoidSet> voiceRoidSetList;    public const string KEY_VOICE_ROID = "lpsVoiceRoidList";


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
                this.uid                = getString(PREFS_UID, LpsGuidCreator.createLiplisGuid());
                this.lpsAutoSleep       = getInt(PREFS_AUTO_SLEEP, 0);
                this.lpsAutoWakeup      = getInt(PREFS_AUTO_WAKEUP, 0);
                this.lpsTwitterActivate = getInt(KEY_TWITTER_ACTIVATE, 0);
                this.voiceRoidSetList   = JsonConvert.DeserializeObject<List<voiceRoidSet>>(getString(KEY_VOICE_ROID,""));
               
                //ヴォイスロイドリストがNULLの場合には、一旦保存
                if (this.voiceRoidSetList == null)
                {
                    this.voiceRoidSetList = new List<voiceRoidSet>();
                    setPreferenceData();
                }

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
            setString(PREFS_UID, this.uid);
            setInt(PREFS_AUTO_SLEEP, this.lpsAutoSleep);
            setInt(PREFS_AUTO_WAKEUP, this.lpsAutoWakeup);
            setInt(KEY_TWITTER_ACTIVATE, this.lpsTwitterActivate);
            setString(KEY_VOICE_ROID, JsonConvert.SerializeObject(this.voiceRoidSetList));
            saveSettings();
        }

        /// <summary>
        /// ボイスロイド登録チェック
        /// </summary>
        public bool existsVoiceRoid(string voiceRoidName)
        {
            //結果
            bool result = false;

            foreach (voiceRoidSet vrs in this.voiceRoidSetList)
            {
                //登録リストの中にあったら、登録済み trueを返す
                if(vrs.voiceRoidName == voiceRoidName)
                {
                    return true;
                }
            }

            //登録がなければfalseを返す
            return result;
        }
    }
}


/// <summary>
/// ボイスロイド設定クラス
/// </summary>
public class voiceRoidSet
{
    public string voiceRoidName { get; set; }
    public string path { get; set; }

    /// <summary>
    /// コンストラクター
    /// </summary>
    /// <param name="voiceRoidName"></param>
    /// <param name="path"></param>
    public voiceRoidSet(string voiceRoidName, string path)
    {
        this.voiceRoidName = voiceRoidName;
        this.path = path;
    }

    /// <summary>
    /// ToString
    /// </summary>
    /// <returns></returns>
    public override string ToString()
    {
        return this.voiceRoidName;
    }

}