//=======================================================================
//  ClassName : MsgTalkMessage
//  概要      : トークメッセージ
//
//  Liplis5.0
//
//  Copyright(c) 2010-2016 LipliStyle.Sachin
//=======================================================================
using Clalis.v50.Msg;
using Clalis.v50.Res;
using Liplis.Com;
using System;
using System.Collections.Generic;
using System.Text;

namespace Liplis.Msg
{
    public class MsgGilsTalk
    {
        ///=============================
        /// プロパティ
        public string newsId { get; set; }
        public string title { get; set; }
        public string url { get; set; }
        public string jpgUrl { get; set; }
        public List<MsgTalkMessage> descriptionList { get; set; }

        #region ResLpsGirlsTalk
        public MsgGilsTalk()
        {
            this.descriptionList = new List<MsgTalkMessage>();
        }
        #endregion

        #region MsgGilsTalk(Int64 idx, string title, string url, string jpgUrl, List<MsgTalkMessage> descriptionList)
        public MsgGilsTalk(Int64 idx, string title, string url, string jpgUrl, List<MsgTalkMessage> descriptionList)
        {
            this.newsId = idx.ToString();
            this.title = title;
            this.url = url;
            this.jpgUrl = jpgUrl;
            this.descriptionList = descriptionList;
        }
        #endregion

        #region ガールズトークレスポンスから変換
        public MsgGilsTalk(ResLpsGirlsTalk response)
        {
            this.newsId = response.newsId;
            this.title = response.title;
            this.url = response.url;
            this.jpgUrl = response.jpgUrl;

            //ディスクリプションリスト初期化
            this.descriptionList = new List<MsgTalkMessage>();

            //センテンスリストを回し、ニューストークメッセージに変換する
            foreach (MsgTalkData item in response.descriptionList)
            {
                //結果メッセージを作成
                MsgTalkMessage msg = new MsgTalkMessage();

                //ID取得
                msg.widgetIndex = item.cId;

                //ネームリスト、等作成
                msg.createList(item.sen);

                ////URLセット
                msg.url = response.url;

                ////タイトルセット
                msg.title = response.title;

                //最後のあっとまーくを除去する
                if (msg.nameList.Count > 0)
                {
                    if (msg.nameList[msg.nameList.Count - 1] == "@")
                    {
                        int targetIndex = msg.nameList.Count - 1;
                        msg.nameList.RemoveAt(targetIndex);
                        msg.emotionList.RemoveAt(targetIndex);
                        msg.pointList.RemoveAt(targetIndex);
                    }
                }

                this.descriptionList.Add(msg);
            }
        }

        #endregion

    }
}
