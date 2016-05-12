//=======================================================================
//  ClassName : LiplisBattery
//  概要      : リプリス電池制御クラス
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
using System.Drawing;
using System.Windows.Forms;

namespace Liplis.MainSystem
{
    public class LiplisBattery
    {
        ///=============================
        /// パワステ
        protected PowerStatus ps;

        ///=============================
        /// ステータス
        public string batteryText { get; set; }
        public bool batteryExists { get; set; }
        public bool batteryStatusChange { get; set; }
        public string nowBatteryImagePath { get; set; }
        public string prvBatteryImagePath { get; set; }

        /// <summary>
        /// コンストラクター
        /// </summary>
        #region コンストラクター
        public LiplisBattery()
        {
            //パワステの取得
            this.ps = SystemInformation.PowerStatus;

            //バッテリーステータスを取得する
            batteryExists = setBatteryExists(ps);
        }
        #endregion

        /// <summary>
        /// setBatteryExists
        /// バッテリー存在チェック
        /// </summary>
        /// <param name="ps"></param>
        /// <returns></returns>
        #region Dispose
        private bool setBatteryExists(PowerStatus ps)
        {
            switch (ps.BatteryChargeStatus)
            {
                case BatteryChargeStatus.High:
                    return true;
                case BatteryChargeStatus.Low:
                    return true;
                case BatteryChargeStatus.Critical:
                    return true;
                case BatteryChargeStatus.Charging:
                    return true;
                case BatteryChargeStatus.NoSystemBattery:
                    return false;
                case BatteryChargeStatus.Unknown:
                    return true;
                default:
                    return true;
            }
        }
        #endregion
        
        /// <summary>
        /// バッテリー割合を取得する
        /// </summary>
        /// <returns></returns>
        public double getBatteryRatel(){
            return ps.BatteryLifePercent * 100;
        }
    }
}
