//=======================================================================
//  ClassName : MsgTalkData
//  概要      : トークデータメッセージ
//
//  Liplis5.0
//
//  Copyright(c) 2010-2016 LipliStyle.Sachin
//=======================================================================
using System;

namespace Clalis.v50.Msg
{
    public class MsgTalkData
    {
        public int cId { get; set; }    //口調リストに対応する番号
        public string sen { get; set; } //センテンス
    }
}
