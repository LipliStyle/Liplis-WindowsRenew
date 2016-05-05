
//=======================================================================
//  ClassName : ResLpsShortNews2Json
//  概要      : レスポンスショートニュースJsonオブジェクトリスト
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
    public class ResUserOnetimePass
    {
        ///=============================
        ///プロパティ
        public string oneTimePass { get; set; }

        /// <summary>
        /// コンストラクター
        /// </summary>
        #region ResUserOnetimePass
        public ResUserOnetimePass()
        {
            this.oneTimePass = "";
        }
        public ResUserOnetimePass(string oneTimePass)
        {
            this.oneTimePass = oneTimePass;
        }
        #endregion
    }
}
