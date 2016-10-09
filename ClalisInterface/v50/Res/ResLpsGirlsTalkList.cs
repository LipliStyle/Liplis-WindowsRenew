//=======================================================================
//  ClassName : ResLpsGirlsTalkList
//  概要      : ガールズトークリスト
//
//  SatelliteServer
//  Copyright(c) 2009-2016 sachin. All Rights Reserved. 
//=======================================================================
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace Clalis.v50.Res
{
    [SerializableAttribute]
    [ComVisibleAttribute(true)]
    public class ResLpsGirlsTalkList
    {
        ///=============================
        ///プロパティ
        public new List<ResLpsGirlsTalk> lstRes { get; set; }

        /// <summary>
        /// コンストラクター
        /// </summary>
        #region ResLpsGirlsTalkList
        public ResLpsGirlsTalkList()
        {
            this.lstRes = new List<ResLpsGirlsTalk>();
        }
        public ResLpsGirlsTalkList(List<ResLpsGirlsTalk> lstRes)
        {
            this.lstRes = lstRes;
        }
        #endregion
    }
}