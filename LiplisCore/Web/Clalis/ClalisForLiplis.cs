//=======================================================================
//  ClassName : ClalisForLiplis
//  概要      : クラリスのLiplis向けAPIの呼び出し
//
//              非同期をベーシックに作り直し
//
//  Liplis5.0
//
//  Copyright(c) 2010-2016 LipliStyle.Sachin
//=======================================================================

using Clalis.v40.Res;
using Liplis.Msg;
using Liplis.Web.Clalis.Json;
using Newtonsoft.Json;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Liplis.Web.Clalis
{
    public class ClalisForLiplis
    {
        ///=============================
        /// URL定義
        #region URL定義
        public const string LIPLIS_API_SUMMARY_NEWS               = @"http://liplis.mine.nu/Clalis/v41/Liplis/ClalisForLiplis.aspx";                        //2014/04/07 ver4.0.0 Clalis4.0採用
        public const string LIPLIS_API_SUMMARY_NEWS_LIST          = @"http://liplis.mine.nu/Clalis/v41/Liplis/ClalisForLiplisFx.aspx";                      //2014/04/07 ver4.0.0 Clalis4.0採用                  //2014/04/07 ver4.0.0 Clalis4.0採用
        public const string LIPLIS_API_SHORT_NEWS                 = @"http://liplis.mine.nu/Clalis/v41/Liplis/ClalisForLiplisWeb.aspx";                     //2014/04/07 ver4.0.0 Clalis4.0採用         
        public const string LIPLIS_API_SHORT_NEWS_LIST            = @"http://liplis.mine.nu/Clalis/v41/Liplis/ClalisForLiplisWebFx.aspx";
        public const string LIPLIS_API_REGISTER_RSS               = @"http://liplis.mine.nu/Clalis/v31/Liplis/ClalisForLiplisRegisterAddRssUrl.aspx";
        public const string LIPLIS_API_TOPIC_SETTING              = @"http://liplis.mine.nu/Clalis/v32/Liplis/ClalisForLiplisSettingTopicSetting.aspx";
        public const string LIPLIS_API_DELETE_RSS                 = @"http://liplis.mine.nu/Clalis/v31/Liplis/ClalisForLiplisRegisterDelRssUrl.aspx";
        public const string LIPLIS_API_GET_RSSLIST                = @"http://liplis.mine.nu/Clalis/v31/Liplis/ClalisForLiplisSettingRssUrlListEachCat.aspx";
        public const string LIPLIS_API_REGISTER_TWITTER           = @"http://liplis.mine.nu/Clalis/v31/Liplis/ClalisForLiplisRegisterAddTwitterUserEx.aspx";
        public const string LIPLIS_API_DELETE_TWITTER             = @"http://liplis.mine.nu/Clalis/v31/Liplis/ClalisForLiplisRegisterDelTwitterUserEx.aspx";
        public const string LIPLIS_API_GET_TWITTERLIST            = @"http://liplis.mine.nu/Clalis/v31/Liplis/ClalisForLiplisSettingTwitterUserList.aspx";
        public const string LIPLIS_API_GET_SEARCH_WORD            = @"http://liplis.mine.nu/Clalis/v32/Liplis/ClalisForLiplisSettingGetSearchWord.aspx";
        public const string LIPLIS_API_REGISTER_SEARCH_WORD       = @"http://liplis.mine.nu/Clalis/v32/Liplis/ClalisForLiplisSettingAddSearchWord.aspx";
        public const string LIPLIS_API_DELETE_SEARCH_WORD         = @"http://liplis.mine.nu/Clalis/v32/Liplis/ClalisForLiplisSettingDelSearchWord.aspx";
        public const string LIPLIS_API_REGISTER_TWITTER_USER_INFO = @"http://liplis.mine.nu/Clalis/v40/Liplis/ClalisForLiplisRegisterTwitterInfo.aspx";
        public const string LIPLIS_API_GET_ONETIME_PASS           = @"http://liplis.mine.nu/Clalis/v31/Liplis/ClalisForLiplisGetOnetimePass.aspx";
        public const string LIPLIS_API_GET_USERID                 = @"http://liplis.mine.nu/Clalis/v31/Liplis/ClalisForLiplisGetUserId.aspx";
        public const string LIPLIS_NEW_EXE                        = @"http://liplis.mine.nu/LiplisWinUpd/1_X/Liplis.lps";
        public const string LIPLIS_NEW_XML                        = @"http://liplis.mine.nu/LiplisWinUpd/1_X/version.xml";
        public const string LIPLIS_HELP                           = @"http://liplis.mine.nu/lipliswiki/webroot/?LiplisWindows%20Manual";
        public const string LIPLIS_LIPLISTYLE                     = @"http://liplis.mine.nu/";
        public const string LIPLIS_FREE_TALK                      = @"http://liplis.mine.nu/Clalis/v41/Liplis/ClalisForLiplisFreeTalk.aspx";
        public const string LIPLIS_TWEET                          = @"http://liplis.mine.nu/Clalis/v40/Liplis/ClalisForLiplisTweet.aspx";
        public const string LIPLIS_CHAT                           = @"http://liplis.mine.nu/Clalis/v40/Liplis/ClalisForLiplisTalk.aspx";
        #endregion


        /// <summary>
        /// コンストラクター
        /// </summary>
        public ClalisForLiplis()
        {

        }


        public static MsgTalkMessage getSummaryNews(string uid, string toneUrl, string newsFlg)
        {
            try
            {
                //パラメーター生成
                FormUrlEncodedContent postData = new FormUrlEncodedContent(new Dictionary<string, string>
                {
                    { "userid", uid},               //UIDLの指定
                    { "tone",toneUrl},              //TONE_URLの指定
                    { "newsFlg",newsFlg}            //ニュースフラグの指定
                });

                //Jsonで結果取得
                string jsonText = HttpPost.sendPost(LIPLIS_API_SUMMARY_NEWS, postData);

                //Jsonをニュースメッセージリストに変換
                return LiplisNewsJpJson.getSummaryNews(jsonText);
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// サマリーニュースのリストを取得する
        /// </summary>
        /// <param name="newsQ"></param>
        /// <param name="uid"></param>
        /// <param name="toneUrl"></param>
        /// <param name="newsFlg"></param>
        /// <param name="num"></param>
        /// <param name="hour"></param>
        /// <param name="already"></param>
        /// <param name="twitterMode"></param>
        /// <param name="runout"></param>
        public static void getSummaryNewsList(ConcurrentQueue<MsgTalkMessage> newsQ, string uid, string toneUrl, string newsFlg, string num, string hour, string already, string twitterMode, string runout)
        {
            try
            {
                //パラメーター生成
                FormUrlEncodedContent postData = new FormUrlEncodedContent(new Dictionary<string, string>
                {
                    { "userid", uid},               //UIDLの指定
                    { "tone",toneUrl},              //TONE_URLの指定
                    { "newsFlg",newsFlg},           //ニュースフラグの指定
                    { "num",num},                   //個数
                    { "hour",hour},                 //時間範囲の指定
                    { "already",already},           //オールレディ
                    { "twitterMode",twitterMode},   //ツイッターモード
                    { "runout",runout}              //ランアウト
                });

                //Jsonで結果取得
                string jsonText = HttpPost.sendPost(LIPLIS_API_SUMMARY_NEWS_LIST, postData);

                //Jsonをニュースメッセージリストに変換
                List<MsgTalkMessage> newsList = LiplisNewsJpJson.getSummaryNewsList(jsonText);

                //エンキューする
                foreach (var msg in newsList)
                {
                    newsQ.Enqueue(msg);
                }
            }
            catch
            {

            }
            return;
        }




    }
}
