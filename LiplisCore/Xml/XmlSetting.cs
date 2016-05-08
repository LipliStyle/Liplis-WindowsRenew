//=======================================================================
//  ClassName : XmlSetting
//  概要      : XML設定
//
//  Liplis5.0
//
//  Copyright(c) 2010-2016 LipliStyle.Sachin
//=======================================================================
namespace Liplis.Xml
{
    public abstract class XmlSetting
    {
        ///=============================
        ///クラス
        protected SharedPreferences setting;

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
        #region bitCheck
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
        #endregion
    }
}
