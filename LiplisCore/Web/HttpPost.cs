//=======================================================================
//  ClassName : HttpPost
//  概要      : HttpPostを投げて、結果を取得する
//
//
//  Liplis5.0
//
//  Copyright(c) 2010-2016 LipliStyle.Sachin
//=======================================================================
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Liplis.Web
{
    public class HttpPost
    {
        ///====================================================================
        ///
        ///                        パブリックメソッド
        ///                         
        ///====================================================================
        
        /// <summary>
        /// Httpポストを実行する
        /// </summary>
        /// <param name="uri"></param>
        /// <param name="postData"></param>
        /// <returns></returns>
        public static string sendPost(string uri, FormUrlEncodedContent postData)
        {
            //Post実行
            Task<string> task = sendPostTask(uri, postData);

            //待機
            task.Wait();

            //Jsonで結果取得
            return task.Result;
        }
        public static string sendPost(string uri, string userAgent, string language, TimeSpan timeOut, FormUrlEncodedContent postData)
        {
            //Post実行
            Task<string> task = sendPostTask(uri, userAgent, language, timeOut, postData);

            //待機
            task.Wait();

            //Jsonで結果取得
            return task.Result;
        }

        ///====================================================================
        ///
        ///                            実行メソッド
        ///                         
        ///====================================================================
        #region 実行メソッド
        
        /// <summary>
        /// 非同期でポストを実行する
        /// </summary>
        /// <param name="uri"></param>
        /// <param name="postData"></param>
        /// <returns></returns>
        private static async Task<string> sendPostTask(string uri, FormUrlEncodedContent postData)
        {
            return await sendPostTask(uri, "Liplis5.x", "ja-JP", TimeSpan.FromSeconds(20.0), postData);
        }
        private static async Task<string> sendPostTask(string uri, string userAgent, string language, TimeSpan timeOut, FormUrlEncodedContent postData)
        {
            using (HttpClient client = new HttpClient())
            {
                // ユーザーエージェント文字列をセット（オプション）
                client.DefaultRequestHeaders.Add("User-Agent", userAgent);

                // 受け入れ言語をセット（オプション）
                client.DefaultRequestHeaders.Add("Accept-Language", language);

                // タイムアウトをセット（オプション）
                client.Timeout = timeOut;

                try
                {
                    //WEBデータ取得
                    var response = await client.PostAsync(uri, postData);
                    return await response.Content.ReadAsStringAsync();
                }
                catch (HttpRequestException e)
                {
                    // 404エラーや、名前解決失敗など
                    throw e;
                }
                catch (TaskCanceledException e)
                {
                    //タイムアウト等
                    throw e;
                }
            }
        }

        #endregion
    }
}
