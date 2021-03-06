﻿//=======================================================================
//  ClassName : ClalisForLiplis
//  概要      : クラリスのLiplis向けAPIの呼び出し
//
//              非同期をベーシックに作り直し
//
//  Liplis5.0
//
//  Copyright(c) 2010-2016 LipliStyle.Sachin
//=======================================================================

using Clalis.v31.Res;
using Clalis.v40.Res;
using Clalis.v50.Msg;
using Clalis.v50.Res;
using Liplis.Com;
using Liplis.Lst;
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
        public const string LIPLIS_GILS_TALK                      = @"http://liplis.mine.nu/Clalis/v50/Liplis/ClalisForLiplisGirlsTalk.aspx";
        public const string LIPLIS_GILS_TALKLIST                  = @"http://liplis.mine.nu/Clalis/v50/Liplis/ClalisForLiplisGirlsTalkList.aspx";
        public const string LIPLIS_GILS_TALK_FROM_NEWSID          = @"http://liplis.mine.nu/Clalis/v50/Liplis/ClalisForLiplisGirlsTalkSpecifyNews.aspx";
        public const string LIPLIS_GILS_TALK_GET_NEWSIDLIST       = @"http://liplis.mine.nu/Clalis/v50/Liplis/ClalisForLiplisGirlsTalkNewsIdList.aspx";
        #endregion


        ///====================================================================
        ///
        ///                             初期化処理
        ///                        
        ///====================================================================
        #region 初期化処理 

        /// <summary>
        /// コンストラクター
        /// </summary>
        public ClalisForLiplis()
        {

        }
        #endregion

        ///====================================================================
        ///
        ///                       サマリーニュース関連
        ///                        
        ///====================================================================
        #region サマリーニュース関連
        /// <summary>
        /// サマリーニュースの取得
        /// </summary>
        /// <param name="uid"></param>
        /// <param name="toneUrl"></param>
        /// <param name="newsFlg"></param>
        /// <returns></returns>
        public static MsgTalkMessage getSummaryNews(string uid, string toneUrl, string newsFlg)
        {
            try
            {
                //パラメーター生成
                //FormUrlEncodedContent postData = new FormUrlEncodedContent(new Dictionary<string, string>
                //{
                //    { "userid", uid},               //UIDLの指定
                //    { "tone",toneUrl},              //TONE_URLの指定
                //    { "newsFlg",newsFlg}            //ニュースフラグの指定
                //});
                NameValueCollection postData = new NameValueCollection();
                postData.Add("userid", uid);                //TONE_URLの指定
                postData.Add("tone", toneUrl);                //TONE_URLの指定
                postData.Add("newsFlg", newsFlg);             //NEWS_FLGの指定


                //Jsonで結果取得
                string jsonText = HttpPostOld.sendPost(LIPLIS_API_SUMMARY_NEWS, postData);

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
                    { "runout",runout},              //ランアウト
                    { "randomkey",DateTime.Now.ToString("yyyyMMddHHmmss") + LpsLiplisUtil.getName(20)}              //ランダム文字列
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
        #endregion

        ///====================================================================
        ///
        ///                        ガールズトーク関連
        ///                        
        ///====================================================================
        #region ガールズトーク関連
        /// <summary>
        /// ガールズトークデータ取得
        /// </summary>
        /// <param name="sd"></param>
        /// <returns></returns>
        public static MsgGilsTalk getGalsTalkDataRandom(msgGirlsTalkSendData sd)
        {
            try
            {
                NameValueCollection postData = new NameValueCollection();
                postData.Add("sd", JsonConvert.SerializeObject(sd));                //送信データ


                //Jsonで結果取得
                string jsonText = HttpPostOld.sendPost(LIPLIS_GILS_TALK, postData);

                //結果取得
                return new MsgGilsTalk(JsonConvert.DeserializeObject<ResLpsGirlsTalk>(jsonText));

            }
            catch
            {
                //失敗時NULLを返す
                return null;
            }
        }

        /// <summary>
        /// ニュースIDからガールズトークデータ取得
        /// </summary>
        /// <param name="sd"></param>
        /// <returns></returns>
        public static MsgGilsTalk getGalsTalkDataFromNewsId(msgGirlsTalkSendDataSpecifyNewsId sd)
        {
            try
            {
                NameValueCollection postData = new NameValueCollection();
                postData.Add("sd", JsonConvert.SerializeObject(sd));                //送信データ


                //Jsonで結果取得
                string jsonText = HttpPostOld.sendPost(LIPLIS_GILS_TALK_FROM_NEWSID, postData);

                ResLpsGirlsTalk result = JsonConvert.DeserializeObject<ResLpsGirlsTalk>(jsonText);

                //結果取得
                return new MsgGilsTalk(result);
            }
            catch(Exception ex)
            {
                //失敗時NULLを返す
                return null;
            }
        }

        /// <summary>
        /// ニュースIDリストを取得する
        /// </summary>
        /// <param name="newsIdQ"></param>
        /// <param name="sd"></param>
        public static void getGalsTalkNewsIdList(ConcurrentQueue<string> newsIdQ, msgGirlsTalkSendData sd)
        {
            try
            {
                //パラメーター生成
                FormUrlEncodedContent postData = new FormUrlEncodedContent(new Dictionary<string, string>
                {
                    { "sd", JsonConvert.SerializeObject(sd)},               //UIDLの指定
                });

                //Jsonで結果取得
                string jsonText = HttpPost.sendPost(LIPLIS_GILS_TALK_GET_NEWSIDLIST, postData);

                //結果取得
                LstShufflableList<string> newsIdList = JsonConvert.DeserializeObject<LstShufflableList<string>>(jsonText);

                //シャッフル
                newsIdList.Shuffle();

                //エンキューする
                foreach (string newsId in newsIdList)
                {
                    newsIdQ.Enqueue(newsId);
                }
            }
            catch
            {

            }
            return;
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
        public static void getGalsTalkDataRandomList(ConcurrentQueue<MsgGilsTalk> gilsTalkQ, msgGirlsTalkSendData sd)
        {
            try
            {
                //パラメーター生成
                FormUrlEncodedContent postData = new FormUrlEncodedContent(new Dictionary<string, string>
                {
                    { "sd", JsonConvert.SerializeObject(sd)},               //UIDLの指定
                });

                //Jsonで結果取得
                string jsonText = HttpPost.sendPost(LIPLIS_GILS_TALKLIST, postData);

                //結果取得
                ResLpsGirlsTalkList gltkList = JsonConvert.DeserializeObject<ResLpsGirlsTalkList>(jsonText);

                //エンキューする
                foreach (ResLpsGirlsTalk msg in gltkList.lstRes)
                {
                    gilsTalkQ.Enqueue(new MsgGilsTalk(msg));
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex);
            }
            return;
        }

        /// <summary>
        /// jsonを取得する
        /// </summary>
        /// <param name="sd"></param>
        /// <returns></returns>
        public static string getGalsTalkDataRandomListJson(msgGirlsTalkSendData sd)
        {
            try
            {
                //パラメーター生成
                FormUrlEncodedContent postData = new FormUrlEncodedContent(new Dictionary<string, string>
                {
                    { "sd", JsonConvert.SerializeObject(sd)},               //UIDLの指定
                });

                //Jsonで結果取得
                return HttpPost.sendPost(LIPLIS_GILS_TALKLIST, postData);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            return "";
        }

        /// <summary>
        /// 取得JSONをMsgGilsTalkメッセージに変換し、Qに入れる
        /// </summary>
        /// <param name="gilsTalkQ"></param>
        /// <param name="jsonText"></param>
        public static ConcurrentQueue<MsgGilsTalk> getGalsTalkDataRandomListFromJson(string jsonText)
        {
            //結果取得
            ResLpsGirlsTalkList gltkList = JsonConvert.DeserializeObject<ResLpsGirlsTalkList>(jsonText);

            //エンキューする
            //foreach (ResLpsGirlsTalk msg in gltkList.lstRes)
            //{
            //    gilsTalkQ.Enqueue(new MsgGilsTalk(msg));
            //}

            //エンキューした結果を入れる

            return getShuffledList(gltkList.lstRes);
        }
        public static ConcurrentQueue<MsgGilsTalk> getGalsTalkDataRandomListFromJson(string jsonText, int count)
        {
            int idx = 0;

            ResLpsGirlsTalkList gltkList = new ResLpsGirlsTalkList();

            try
            {
                //結果取得
                gltkList = JsonConvert.DeserializeObject<ResLpsGirlsTalkList>(jsonText);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex);
            }


            //エンキューする
            //foreach (ResLpsGirlsTalk msg in gltkList.lstRes)
            //{
            //    //指定数まで追加したら抜ける
            //    if(idx == count)
            //    {
            //        break;
            //    }

            //    gilsTalkQ.Enqueue(new MsgGilsTalk(msg));

            //    //インクリメント
            //    idx++;
            //}

            //エンキューした結果を入れる
            return getShuffledList(gltkList.lstRes, count);
        }

        /// <summary>
        /// シャッフル済みリストを返す
        /// </summary>
        /// <param name="gltkList"></param>
        /// <returns></returns>
        private static ConcurrentQueue<MsgGilsTalk> getShuffledList(List<ResLpsGirlsTalk> lstRes)
        {
            return getShuffledList(lstRes, 99999);
        }
        private static ConcurrentQueue<MsgGilsTalk> getShuffledList(List<ResLpsGirlsTalk> lstRes, int count)
        {
            int idx = 0;
            LstShufflableList<MsgGilsTalk> shuffledList = new LstShufflableList<MsgGilsTalk>();
            ConcurrentQueue<MsgGilsTalk> result = new ConcurrentQueue<MsgGilsTalk>();

            //結果リストを回しシャッフル可能リストに入れる
            foreach (ResLpsGirlsTalk msg in lstRes)
            {
                shuffledList.Add(new MsgGilsTalk(msg));
            }

            //シャッフルする
            shuffledList.Shuffle();

            //シャッフル結果をキューニれる
            foreach (MsgGilsTalk item in shuffledList)
            {
                //指定数まで追加したら抜ける
                if (idx == count)
                {
                    break;
                }

                result.Enqueue(item);

                //インクリメント
                idx++;
            }

            //キューを返す
            return result;
        }

        #endregion


        //====================================================================
        //
        //                         ツイッター関連
        //                        
        //====================================================================
        #region ツイッター関連
        /// <summary>
        /// ツイッター登録
        /// 非同期
        /// </summary>
        /// <param name="uid"></param>
        /// <param name="token"></param>
        /// <param name="secret"></param>
        /// <param name="userId"></param>
        /// <param name="screanNam"></param>
        /// <returns></returns>
        public static string twitterRegister(string uid, string token, string secret, string userId, string screanNam)
        {
            try
            {
                //引数の指定
                FormUrlEncodedContent postData = new FormUrlEncodedContent(new Dictionary<string, string>
                {
                    { "userid", uid},            //LiplisユーザーID
                    { "token",token},            //トークン
                    { "secret",secret},          //トークンシークレット
                    { "twitteruid",userId},      //ツイッターユーザーID
                    { "twittersname",screanNam}, //ツイッタースクリーンネーム
                });

                //Jsonで結果取得
                string jsonText = HttpPost.sendPost(LIPLIS_API_REGISTER_TWITTER_USER_INFO, postData);

                //APIの結果受け取り用クラス
                return JsonConvert.DeserializeObject<ResLpsRegisterTwitterInfoRespons>(jsonText).responseCode;
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Clalisシステムへのツイッター情報登録に失敗しました。", ex);
            }
        }
        #endregion



        ///====================================================================
        ///
        ///                      ワンタイムパスワード関連
        ///                        
        ///====================================================================
        #region ワンタイムパスワード関連
        /// <summary>
        /// getOneTimePass
        /// ワンタイムパスワードを取得する
        /// 非同期
        /// 
        /// 成功の場合、ワンタイムパスワードを返す
        /// 失敗の場合、空を返す
        /// </summary>
        /// <param name="uid"></param>
        /// <returns></returns>

        public static string getOneTimePass(string uid)
        {
            try
            {
                FormUrlEncodedContent postData = new FormUrlEncodedContent(new Dictionary<string, string>
                {
                    { "userid", uid},            //LiplisユーザーID
                });

                //Jsonで結果取得
                string jsonText = HttpPost.sendPost(LIPLIS_API_GET_ONETIME_PASS, postData);

                //ワンタイムパスワードを返す
                return JsonConvert.DeserializeObject<ResUserOnetimePass>(jsonText).oneTimePass;
            }
            catch
            {
                //取得失敗(空を返す)
                return "";
            }
        }

        /// <summary>
        /// getLiplisId
        /// リプリスIDを取得する
        /// </summary>
        /// <param name="uid"></param>
        /// <returns></returns>
        public static string getLiplisId(string oneTimePass)
        {
            try
            {
                //引数の指定
                FormUrlEncodedContent postData = new FormUrlEncodedContent(new Dictionary<string, string>
                {
                    { "onetimePass", oneTimePass},            //LiplisユーザーID
                });

                //Jsonで結果取得
                string jsonText = HttpPost.sendPost(LIPLIS_API_GET_USERID, postData);

                //APIの結果受け取り用クラス
                return JsonConvert.DeserializeObject<ResLiplisId>(jsonText).userId;
            }
            catch
            {
                return "";
            }
        }
        #endregion


        //====================================================================
        //
        //                              RSS関連
        //                        
        //====================================================================
        #region RSS関連 
        /// <summary>
        /// RSSリスト取得
        /// 非同期
        /// </summary>
        /// <param name="uid"></param>
        /// <returns></returns>
        public static ResLpsLoginRegisterInfoRssEachCat getUserRssList(string uid)
        {
            try
            {
                //引数の指定
                FormUrlEncodedContent postData = new FormUrlEncodedContent(new Dictionary<string, string>
                {
                    { "userid", uid},            //LiplisユーザーID
                });


                //Jsonで結果取得
                string jsonText = HttpPost.sendPost(LIPLIS_API_GET_RSSLIST, postData);

                //APIの結果受け取り用クラス
                return JsonConvert.DeserializeObject<ResLpsLoginRegisterInfoRssEachCat>(jsonText);
            }
            catch
            {
                return new ResLpsLoginRegisterInfoRssEachCat();
            }
        }

        /// <summary>
        /// RSS登録
        /// 非同期
        /// </summary>
        /// <param name="url"></param>
        /// <param name="cat"></param>
        /// <param name="uid"></param>
        /// <returns></returns>
        public static string registRss(string url, string cat, string uid)
        {
            try
            {
                if (cat == "デフォルトカテゴリ")
                {
                    cat = "";
                }

                FormUrlEncodedContent postData = new FormUrlEncodedContent(new Dictionary<string, string>
                {
                    { "userid", uid},       //LiplisユーザーID
                　  { "addRssUrl", url},    //登録URL
                    { "addRssCat", cat},    //登録カテゴリ
                });


                //Jsonで結果取得
                string jsonText = HttpPost.sendPost(LIPLIS_API_REGISTER_RSS, postData);

                //APIの結果受け取り用クラス
                return JsonConvert.DeserializeObject<ResLpsLoginStatus>(jsonText).responseCode;
            }
            catch
            {
                return "-1";
            }
        }

        /// <summary>
        /// RSS削除
        /// 非同期
        /// </summary>
        /// <param name="uid"></param>
        /// <param name="url"></param>
        /// <returns></returns>
        public static string deleteRss(string uid, string url)
        {
            try
            {
                //引数の指定
                FormUrlEncodedContent postData = new FormUrlEncodedContent(new Dictionary<string, string>
                {
                    { "userid", uid},               //LiplisユーザーID
                    { "addRssUrl", url},            //対象URL
                });

                //Jsonで結果取得
                string jsonText = HttpPost.sendPost(LIPLIS_API_DELETE_RSS, postData);

                //APIの結果受け取り用クラス
                return JsonConvert.DeserializeObject<ResLpsLoginStatus>(jsonText).responseCode;
            }
            catch
            {
                return "-1";
            }
        }
        #endregion



        ///====================================================================
        ///
        ///                         ツイッター関連
        ///                        
        ///====================================================================
        #region twitter
        /// <summary>
        /// ツイートする
        /// </summary>
        /// <param name="uid"></param>
        /// <param name="topicId"></param>
        /// <returns></returns>
        public static ResLpsLoginStatus tweet(string uid, string sentence)
        {
            try
            {
                //引数の指定
                FormUrlEncodedContent postData = new FormUrlEncodedContent(new Dictionary<string, string>
                {
                    { "userid", uid},               //LiplisユーザーID
                    { "sentence", sentence},            //対象URL
                });


                //Jsonで結果取得
                string jsonText = HttpPost.sendPost(LIPLIS_TWEET, postData);

                //APIの結果受け取り用クラス
                return JsonConvert.DeserializeObject<ResLpsLoginStatus>(jsonText);
            }
            catch
            {
                return new ResLpsLoginStatus("-1", uid);
            }
        }
        #endregion



    }
}
