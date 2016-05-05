//=======================================================================
//  ClassName : ResLpsLoginRegisterInfoTw
//  概要      : レスポンス ツイッター登録結果
//
//  Liplis5.0
//
//  Copyright(c) 2010-2016 LipliStyle.Sachin
//=======================================================================
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace Clalis.v31.Res
{
    [SerializableAttribute]
    [ComVisibleAttribute(true)]
    public class ResLpsLoginRegisterInfoTw
    {
        ///=============================
        ///プロパティ
        public List<RegisterTwUserInfo> twuserlist { get; set; }

        /// <summary>
        /// コンストラクター
        /// </summary>
        #region ResLpsLoginRegisterInfoTw
        public ResLpsLoginRegisterInfoTw()
        {
            this.twuserlist = new List<RegisterTwUserInfo>();
        }
        public ResLpsLoginRegisterInfoTw(List<RegisterTwUserInfo> twuserlist)
        {
            this.twuserlist = twuserlist;
        }
        #endregion
    }
}
