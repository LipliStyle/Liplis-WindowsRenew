//=======================================================================
//  ClassName : msgGirlsTalkSendDataSpecifyNewsId
//  概要      : ガールズトーク送信メッセージ ニュースID指定
//
//  LiplisSystemシステム      
//  Copyright(c) 2010-2016 sachin. All Rights Reserved. 
//=======================================================================
using System.Collections.Generic;
namespace Clalis.v50.Msg
{
    public class msgGirlsTalkSendDataSpecifyNewsId
    {
        public string newsId { get; set; }   //ニュースID
        public string subId { get; set; }   //サブID
        public int reqNum { get; set; }   //要求会話数
        public List<string> toneList { get; set; }   //口調URLリスト

        /// <summary>
        /// コンストラクター
        /// </summary>
        public msgGirlsTalkSendDataSpecifyNewsId()
        {
            this.newsId = "";
            this.subId = "-1"; //通常-1
            this.reqNum = 100;
            this.toneList = new List<string>();
        }
    }
}