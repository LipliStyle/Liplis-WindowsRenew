//=======================================================================
//  ClassName : ResLpsTopicSearchWord
//  概要      : リプリス設定情報 検索設定
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
    public class ResLpsTopicSearchWord
    {
        #region プロパティ
        public int topicId { get; set; }
        public string word { get; set; }
        public int flgEnable { get; set; }
        #endregion

        #region コンストラクター
        public ResLpsTopicSearchWord()
        {
        }
        public ResLpsTopicSearchWord(int topicId, string word, int flgEnable)
        {
            this.topicId = topicId;
            this.word = word;
            this.flgEnable = flgEnable;
        }
        #endregion
    }
}
