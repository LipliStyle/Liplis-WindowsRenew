//=======================================================================
//  ClassName : LiplisFaildMessage
//  概要      : Liplisデータ取得失敗メッセージ
//
//  Liplis5.0
//
//  Copyright(c) 2010-2016 LipliStyle.Sachin
//=======================================================================
using Liplis.Msg;

namespace Liplis.Talk
{
    public class LiplisFaildMessage
    {
        /// <summary>
        /// getMessage
        /// データ取得失敗メッセージの作成
        /// </summary>
        #region getMessage
        public static MsgTalkMessage getMessage()
        {
            MsgTalkMessage msg = new MsgTalkMessage();

            msg.result = "データの取得に失敗しました。";

            msg.nameList.Add("データ");
            msg.nameList.Add("の");
            msg.nameList.Add("取得");
            msg.nameList.Add("に");
            msg.nameList.Add("失敗");
            msg.nameList.Add("し");
            msg.nameList.Add("まし");
            msg.nameList.Add("た。");

            msg.emotionList.Add(1);
            msg.emotionList.Add(1);
            msg.emotionList.Add(1);
            msg.emotionList.Add(1);
            msg.emotionList.Add(1);
            msg.emotionList.Add(1);
            msg.emotionList.Add(1);
            msg.emotionList.Add(1);

            msg.pointList.Add(-1);
            msg.pointList.Add(-1);
            msg.pointList.Add(-1);
            msg.pointList.Add(-1);
            msg.pointList.Add(-1);
            msg.pointList.Add(-1);
            msg.pointList.Add(-1);
            msg.pointList.Add(-1);

            return msg;

        }
        #endregion
    }
}
