//=======================================================================
//  ClassName : ResLpsRegisterTwitterInfoRespons
//  概要      : ツイッターユーザー情報登録レスポンス
//
//  Liplis5.0
//
//  Copyright(c) 2010-2016 LipliStyle.Sachin
//=======================================================================

namespace Clalis.v31.Res
{
    public class ResLpsRegisterTwitterInfoRespons
    {
        ///=============================
        ///プロパティ
        public string responseCode { get; set; }

        /// <summary>
        /// コンストラクター
        /// </summary>
        #region ResLpsRegisterTwitterInfoRespons
        public ResLpsRegisterTwitterInfoRespons()
        {
            this.responseCode = "";
        }
        public ResLpsRegisterTwitterInfoRespons(string responseCode)
        {
            this.responseCode = responseCode;
        }
        #endregion
    }
}
