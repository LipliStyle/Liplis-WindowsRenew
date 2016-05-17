//=======================================================================
//  ClassName : HttpPost
//  概要      : httppostし、情報を取得する
//
//  Liplis5.0
//
//  Update : 2011/07/17 ver0.3 作成
//           2013/01/12 verMT  ストアアプリとの互換性なし。45バージョンを作成する。
//  Copyright(c) 2010-2016 LipliStyle.Sachin
//=======================================================================
using System;
using System.Collections.Specialized;
using System.IO;
using System.Net;
using System.Net.Cache;
using System.Text;

namespace Liplis.Web
{
    public static class HttpPostOld
    {
        const int WEB_POST_TIMEOUT = 30000;
        const string WEB_POST_METHOD = "POST";
        const string WEB_POST_CONTENT_TYPE = "application/x-www-form-urlencoded";

        ///====================================================================
        ///
        ///                        パブリックメソッド
        ///                         
        ///====================================================================

        /// <summary>
        /// ポストを送信する
        /// (UTF-8のみ)
        /// 
        /// </summary>
        /// <param name="url"></param>
        /// <param name="postData"></param>
        /// <returns></returns>
        public static string sendPost(string url, NameValueCollection postData)
        {
            //受信したデータを表示する
            return sendPost(url, getParamDataByte(postData));
        }
        public static string sendPost(string url, byte[] data)
        {
            //ウェブリクエストの取得
            HttpWebRequest req = getWebRequest(url, data.Length, WEB_POST_TIMEOUT);

            req.CachePolicy = new HttpRequestCachePolicy(HttpRequestCacheLevel.NoCacheNoStore);

            // ポスト・データの書き込み
            sendPostRequest(req, data);

            //受信したデータを表示する
            return getWebResponse(req);
        }
        public static string sendPost(string url, NameValueCollection postData, int postTimeout)
        {
            //受信したデータを表示する
            return sendPost(url, getParamDataByte(postData), postTimeout);
        }
        public static string sendPost(string url, byte[] data, int postTimeout)
        {
            //ウェブリクエストの取得
            HttpWebRequest req = getWebRequest(url, data.Length, postTimeout);

            req.CachePolicy = new HttpRequestCachePolicy(HttpRequestCacheLevel.NoCacheNoStore);

            // ポスト・データの書き込み
            sendPostRequest(req, data);

            //受信したデータを表示する
            return getWebResponse(req);
        }

        /// <summary>
        /// ポストを送信する
        /// (送りっぱなし)
        /// </summary>
        /// <param name="url"></param>
        /// <param name="postData"></param>
        public static void throwPost(string url, NameValueCollection postData)
        {
            throwPost(url, getParamDataByte(postData));
        }
        public static void throwPost(string url, byte[] data)
        {
            //ウェブリクエストの取得
            HttpWebRequest req = getWebRequest(url, data.Length, WEB_POST_TIMEOUT);

            // ポスト・データの書き込み
            sendPostRequest(req, data);
        }


        ///====================================================================
        ///
        ///                          JSONリクエスト
        ///                         
        ///====================================================================
        public static string sendPostJsonReq(string url, NameValueCollection postData)
        {
            //受信したデータを表示する
            return sendPostJsonReq(url, getParamDataByte(postData));
        }
        public static string sendPostJsonReq(string url, byte[] data)
        {
            //ウェブリクエストの取得
            HttpWebRequest req = getWebRequest(url, data.Length, WEB_POST_TIMEOUT);

            req.CachePolicy = new HttpRequestCachePolicy(HttpRequestCacheLevel.NoCacheNoStore);

            req.ContentType = "application/json";


            //ヘッダーデータの作成
            //NameValueCollection headData = new NameValueCollection();
            //headData.Add("Content-Type", "application/json");
            //req.Headers.Add(headData);

            // ポスト・データの書き込み
            sendPostRequest(req, data);

            //受信したデータを表示する
            return getWebResponse(req);
        }

        ///====================================================================
        ///
        ///                       POSTの実行メソッド
        ///                         
        ///====================================================================

        /// <summary>
        /// getParamDataByte
        /// パラメータをバイトで取得する
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        private static byte[] getParamDataByte(NameValueCollection postData)
        {
            string param = "";

            //バラメータの取得
            foreach (string k in postData)
            {
                param += String.Format("{0}={1}&", k, postData[k]);
            }

            //パラメータをバイト変換
            return Encoding.UTF8.GetBytes(param);
        }

        /// <summary>
        /// getWebRequest
        /// ウェブリクエストを取得する
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        private static HttpWebRequest getWebRequest(string url, long paramLength, int postTimeout)
        {
            // リクエストの作成
            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);
            req.Timeout = postTimeout;
            req.Method = WEB_POST_METHOD;
            req.ContentType = WEB_POST_CONTENT_TYPE;
            req.ContentLength = paramLength;

            return req;
        }

        /// <summary>
        /// getWebResponse
        /// ウェブレスポンスを取得する
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        private static string getWebResponse(HttpWebRequest req)
        {
            using (Stream resStream = req.GetResponse().GetResponseStream())
            {
                using (StreamReader sr = new StreamReader(resStream, Encoding.UTF8))
                {
                    return sr.ReadToEnd();
                }
            }
        }


        /// <summary>
        /// getWebRequest
        /// ウェブリクエストを取得する
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        private static void sendPostRequest(HttpWebRequest req, byte[] pramData)
        {
            using (Stream reqStream = req.GetRequestStream())
            {
                reqStream.Write(pramData, 0, pramData.Length);
            }
        }
    }
}
