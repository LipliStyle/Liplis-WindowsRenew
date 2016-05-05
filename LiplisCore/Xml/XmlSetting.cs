//=======================================================================
//  ClassName : XmlSettingObject
//  概要      : XML設定
//
//  Liplis5.0
//
//  Copyright(c) 2010-2016 LipliStyle.Sachin
//=======================================================================
using Liplis.Utl;
using System;

namespace Liplis.Xml
{
    [Serializable]
    public abstract class XmlSetting
    {
        ///=============================
        ///クラス
        protected SharedPreferences setting;

        //===========================================================
        //　　　　　　　　　　　初期化処理
        //===========================================================
        /// <summary>
        /// コンストラクター
        /// </summary>
        public XmlSetting()
        {
            //プリファレンスは子クラスのコンストラクターで初期化する
        }

        //===========================================================
        //　　　　　　　　　　　実装メソッド
        //===========================================================

        /// <summary>
        /// プリファレンスの取得
        /// </summary>
        public abstract void getPreferenceData();

        /// <summary>
        /// プリファレンスの設定
        /// </summary>
        public abstract void setPreferenceData();

        //===========================================================
        //　　　　　　　　　　　チェッカー
        //===========================================================


        /// <summary>
        /// ビット値のチェック
        /// </summary>
        protected int bitCheck(int bit)
        {
            if (bit == 1 || bit == 0)
            {
                return bit;
            }
            else
            {
                return 0;
            }
        }
    }
}
