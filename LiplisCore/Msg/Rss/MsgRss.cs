//=======================================================================
//  ClassName : MsgRssList
//  概要      : RSSオブジェクト
//
//  Liplis5.0
//
//  Copyright(c) 2010-2016 LipliStyle.Sachin
//=======================================================================
using Liplis.Xml.Rss;
using System;
using System.Collections.Generic;

namespace Liplis.Msg.Rss
{
    [Serializable]
    public class MsgRss
    {
        ///============================
        /// RSS情報
        public string title { get; set; }
        public string url { get; set; }
        public string cat { get; set; }
        public List<MsgTalkMessage> topicList { get; set; }


        /// <summary>
        /// 
        /// </summary>
        #region MsgRss
        public MsgRss()
        {

        }
        public MsgRss(string title, string url, string cat)
        {
            this.title = title;
            this.url = url;
            this.cat = cat;
            this.topicList = new List<MsgTalkMessage>();
        }
        #endregion

        /// <summary>
        /// updateTopicList
        /// 記事リストを更新する
        /// </summary>
        #region updateTopicList
        public void updateTopicList(RssReader2 rr)
        {
            int idx = 0;

            //更新件数チェック
            if (rr.urlList.Count <= 0)
            {
                return;
            }

            //ヌチェック
            if (topicList == null) { topicList = new List<MsgTalkMessage>(); }

            //URL1件目チェック
            if (this.topicList.Count > 0)
            {
                //1件目が同じなら未更新と判断
                if (this.topicList[0].url.Equals(rr.urlList[0]))
                {
                    return;
                }
            }

            List<MsgTalkMessage> newTopicList = new List<MsgTalkMessage>();

            //更新する
            foreach (string url in rr.urlList)
            {
                MsgTalkMessage n = new MsgTalkMessage();
                try
                {
                    n.url = url;
                    n.title = rr.urlTitleList[idx];
                }
                catch
                {
                    Console.WriteLine("ニュース収集エラー");
                }

                newTopicList.Add(n);

                idx++;
            }

            //更新完了
            topicList = newTopicList;
        }
        #endregion

    }
}
