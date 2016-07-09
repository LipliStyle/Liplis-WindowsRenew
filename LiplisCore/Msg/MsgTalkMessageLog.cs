//=======================================================================
//  ClassName : MsgTalkMessageLog
//  概要      : トークメッセージログ
//
//  Liplis5.0
//
//  Copyright(c) 2010-2016 LipliStyle.Sachin
//=======================================================================
using Liplis.Com;
using System;
using System.Collections.Generic;
using System.Text;

namespace Liplis.Msg
{
    [Serializable]
    public class MsgTalkMessageLog
    {
        ///=============================
        ///プロパティ
        public string url { get; set; }
        public string title { get; set; }
        public string result { get; set; }
        public string jpgUrl { get; set; }
        public int newsEmotion { get; set; }
        public int newsPoint { get; set; }

        /// <summary>
        /// コンストラクター
        /// このコンストラクターを使用する場合は、リザルトとソースは必ず設定する必要がある！！
        ///
        /// 
        /// 
        /// </summary>
        #region コンストラクター
        public MsgTalkMessageLog(MsgTalkMessage talkMsg, string liplisChatText)
        {
            url = talkMsg.url;
            title = talkMsg.title;
            result = liplisChatText;
            jpgUrl = talkMsg.jpgUrl;
            newsEmotion = talkMsg.newsEmotion;
            newsPoint = talkMsg.newsPoint;
        }
        #endregion
    }
}
