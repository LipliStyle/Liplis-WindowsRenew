//=======================================================================
//  ClassName : ResLpsLoginStatus
//  概要      : レスポンス ログインステータス
//
//  Liplis5.0
//
//  Copyright(c) 2010-2016 LipliStyle.Sachin
//=======================================================================
using System;
using System.Runtime.InteropServices;

namespace Clalis.v31.Res
{
    [SerializableAttribute]
    [ComVisibleAttribute(true)]
    public class ResLpsLoginStatus
    {
        ///=============================
        ///プロパティ
        public string responseCode { get; set; }
        public string userCode { get; set; }

        /// <summary>
        /// コンストラクター
        /// </summary>
        #region ResLpsLoginStatus
        public ResLpsLoginStatus()
        {
            this.responseCode = "";
            this.userCode = "";
        }
        public ResLpsLoginStatus(string responseCode, string userCode)
        {
            this.responseCode = responseCode;
            this.userCode = userCode;
        }
        #endregion
    }
}
