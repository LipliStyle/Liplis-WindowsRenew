//=======================================================================
//  ClassName : ResLpsTopicSearchWordList
//  概要      : リプリス設定情報 検索設定リスト
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
    public class ResLpsTopicSearchWordList
    {
        #region プロパティ
        public List<ResLpsTopicSearchWord> wordList { get; set; }
        #endregion

        #region コンストラクター
        public ResLpsTopicSearchWordList()
        {
            wordList = new List<ResLpsTopicSearchWord>();
        }
        public ResLpsTopicSearchWordList(List<ResLpsTopicSearchWord> wordList)
        {
            this.wordList = wordList;
        }
        #endregion
    }
}
