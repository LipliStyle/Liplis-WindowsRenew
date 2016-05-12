//=======================================================================
//  ClassName : LpsGuidCreator
//  概要      : GUIDクリエイター
//
//  Liplis5.0
//  Copyright(c) 2010-2016 LipliStyle.Sachin
//=======================================================================
using System;

namespace Liplis.Utl
{
    public class LpsGuidCreator
    {
        /// <summary>
        /// createLiplisGuid
        /// 独自Guidを作成する
        /// </summary>
        #region createLiplisGuid
        public static string createLiplisGuid()
        {
            return Guid.NewGuid().ToString() + String.Format("{0:000}", DateTime.Now.Millisecond);
        }
        #endregion

        /// <summary>
        /// createGuid
        /// Guidを作成する
        /// </summary>
        #region createGuid
        public static string createGuid()
        {
            return Guid.NewGuid().ToString();
        }
        #endregion
    }
}
