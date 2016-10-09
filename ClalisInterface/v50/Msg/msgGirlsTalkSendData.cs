//=======================================================================
//  ClassName : msgGirlsTalkSendData
//  概要      : ガールズトーク送信メッセージ
//
//  LiplisSystemシステム      
//  Copyright(c) 2010-2016 sachin. All Rights Reserved. 
//=======================================================================
using System.Collections.Generic;

namespace Clalis.v50.Msg
{
    public class msgGirlsTalkSendData
    {
        public int reqNum { get; set; }      //要求会話数
        public List<string> toneList { get; set; }   //口調URLリスト
        public string newsFlgBit { get; set; }   //ニュースフラグビット
        public int hour { get; set; }   //取得範囲

        /// <summary>
        /// コンストラクター
        /// </summary>
        public msgGirlsTalkSendData()
        {
            this.reqNum = 100;
            this.toneList = new List<string>();
            this.newsFlgBit = "1111111";
            this.hour = 6;
        }

    }
}