//=======================================================================
//  ClassName : LiplisNewsJpJson
//  概要      : LiplisAPIの応答データを変換する
//
//
//  Liplis5.0
//
//  Copyright(c) 2010-2016 LipliStyle.Sachin
//=======================================================================

using Clalis.v40.Res;
using Liplis.Com;
using Liplis.Msg;
using Liplis.Talk;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Text;

namespace Liplis.Web.Clalis.Json
{
    public class LiplisNewsJpJson
    {
        //============================================================
        //
        //                       パブリックメソッド
        //
        //============================================================
        #region 変換処理

        /// <summary>
        /// サマリーニュースの結果をMsgTalkMessageに変換して取得する
        /// </summary>
        /// <param name="jsonText"></param>
        /// <returns></returns>
        public static MsgTalkMessage getSummaryNews(string jsonText)
        {
            return json2MsgTalk(jsonText);
        }

        /// <summary>
        /// サマリーニュースリストの結果をMsgTalkMessageのリストにして取得する
        /// </summary>
        /// <param name="jsonText"></param>
        /// <returns></returns>
        public static List<MsgTalkMessage> getSummaryNewsList(string jsonText)
        {
            return json2MsgNewsList(jsonText);
        }

        #endregion

        //============================================================
        //
        //                            変換処理
        //
        //============================================================
        #region 変換処理
        /// <summary>
        /// サマリーニュースの取得結果をMsgTalkMessageに変換する
        /// </summary>
        /// <param name="jsonText"></param>
        /// <returns></returns>
        protected static MsgTalkMessage json2MsgTalk(string jsonText)
        {
            return json2MsgShortNews(JsonConvert.DeserializeObject<ResLpsSummaryNews2Json>(jsonText));
        }
        protected static MsgTalkMessage json2MsgShortNews(ResLpsSummaryNews2Json rlsn2)
        {
            //ディスクリプションチェック
            if (rlsn2.descriptionList.Count < 1)
            {
                return LiplisFaildMessage.getMessage();
            }

            //結果メッセージを作成
            MsgTalkMessage msg = new MsgTalkMessage();

            //リザルトSB
            StringBuilder sbResult = new StringBuilder();

            //ネームリスト、等作成
            foreach (string desc in rlsn2.descriptionList)
            {
                //ネームリストに追記
                msg.createList(desc);

                //全文章追記
                sbResult.Append(msg.result);
            }

            //EOSの除去
            string result = sbResult.ToString().Replace("EOS", "");

            //結果をメッセージに格納
            msg.url = LpsLiplisUtil.nullCheck(rlsn2.url);
            msg.title = LpsLiplisUtil.nullCheck(rlsn2.title);
            msg.result = result;
            msg.sorce = result;
            msg.jpgUrl = "";
            msg.calcNewsEmotion();

            ///jpgURLのセット
            if (rlsn2.jpgUrl != null)
            {
                if (!rlsn2.jpgUrl.Equals(""))
                {
                    msg.jpgUrl = rlsn2.jpgUrl;
                }

            }
            return msg;
        }

        /// <summary>
        /// json2MsgNewsList
        /// サマリーニュースの取得結果をトMsgTalkMessageリストに変換する
        /// </summary>
        /// <param name="jsonText"></param>
        /// <returns></returns>
        public static List<MsgTalkMessage> json2MsgNewsList(string jsonText)
        {
            //結果リスト
            List<MsgTalkMessage> resList = new List<MsgTalkMessage>();

            //JSON内容取得結果
            ResLpsSummaryNews2JsonList result = JsonConvert.DeserializeObject<ResLpsSummaryNews2JsonList>(jsonText);

            foreach (ResLpsSummaryNews2Json rlsn2 in result.lstNews)
            {
                resList.Add(json2MsgShortNews(rlsn2));
            }

            return resList;
        }
        #endregion
    }
}
