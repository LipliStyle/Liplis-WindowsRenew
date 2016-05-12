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
        public string lpsUid;                   //0
        public Int32 lpsAutoSleep;              //1
        public Int32 lpsAutoWakeup;             //2
        //public Int32 lpsTalkWindowClickMode;    //3
        //public Int32 lpsBrowserMode;            //4
        //public Int32 lpsAutoRescue;             //5

        public const string PREFS_UID               = "uid";
        public const string PREFS_AUTO_SLEEP        = "lpsAutoSleep";
        public const string PREFS_AUTO_WAKEUP       = "lpsAutoWakeup";
        //public const string PREFS_WINDOW_CLIKC_MODE = "lpsTalkWindowClickMode";
        //public const string PREFS_BRW_MODE          = "lpsBrowserMode";
        //public const string PREFS_AUTO_RESCUE       = "lpsAutoRescue";

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
                this.lpsUid = getString(PREFS_UID, LpsGuidCreator.createLiplisGuid());
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
