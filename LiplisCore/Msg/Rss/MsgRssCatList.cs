//=======================================================================
//  ClassName : MsgRssCatList
//  概要      : RSSリスト
//
//  Liplis5.0
//
//  Copyright(c) 2010-2016 LipliStyle.Sachin
//=======================================================================
using System;
using System.Collections.Generic;

namespace Liplis.Msg.Rss
{
    [Serializable]
    public class MsgRssCatList
    {
        public int maxIdx { get; set; }
        public string cat { get; set; }
        public List<MsgRss> rssList { get; set; }

        /// <summary>
        /// コンストラクター
        /// </summary>
        #region MsgRssCatList
        public MsgRssCatList(string cat)
        {
            this.maxIdx = 0;
            this.cat = cat;
            this.rssList = new List<MsgRss>();
        }
        #endregion
    }
}
