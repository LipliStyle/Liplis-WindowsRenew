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
    public class LiplisPreference : XmlSetting
    {
        /// <summary>
        /// コンストラクター
        /// </summary>
        #region コンストラクター
        public LiplisPreference()
        {
            try
            {
                //設定の取得
                setting = new SharedPreferences(LpsPathController.getSettingFilePath());

                //読み込み
                getPreferenceData();

            }
            catch (System.Exception err)
            {
                //設定ファイルの読み込みエラーの旨、異常位置を知らせるウインドウを出すべき
                LpsLogController.writingLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, LpsPathController.getSettingFilePath() + "が存在しないため作成します" + Environment.NewLine + err);
            }
        }
        #endregion


        /// <summary>
        /// 設定の読み込み
        /// </summary>
        public override void getPreferenceData()
        {
            try
            {

            }
            catch (System.Exception err)
            {
                LpsLogController.writingLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, "readResult:設定の読込失敗" + Environment.NewLine + err);
            }

        }

        /// <summary>
        /// 設定の保存
        /// </summary>
        public override void setPreferenceData()
        {
        }
    }
}
