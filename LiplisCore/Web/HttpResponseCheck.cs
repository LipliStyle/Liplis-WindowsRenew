//=======================================================================
//  ClassName : HttpResponseCheck
//  概要      : Webサイトの応答を確認する
//  Liplis5.0
//
//  Copyright(c) 2010-2016 LipliStyle.Sachin
//=======================================================================
using System.Net;

namespace Liplis.Web
{
    public static class HttpResponseCheck
    {
        /// <summary>
        /// ターゲットのURLのレスポンスコードを取得する
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        #region getHttpStatus
        public static HttpStatusCode getHttpStatus(string url)
        {
            try
            {
                //WebRequestの作成
                HttpWebRequest webreq = (HttpWebRequest)WebRequest.Create(url);
                webreq.Timeout = 10000;


                //サーバーからの応答を受信するためのWebResponseを取得
                using (HttpWebResponse webres = (HttpWebResponse)webreq.GetResponse())
                {
                    //応答ステータスコードを表示する
                    return webres.StatusCode;
                }
            }
            catch (System.Net.WebException ex)
            {
                //HTTPプロトコルエラーかどうか調べる
                if (ex.Status == WebExceptionStatus.ProtocolError)
                {
                    //HttpWebResponseを取得
                    HttpWebResponse errres = (HttpWebResponse)ex.Response;

                    return errres.StatusCode;
                }
                else
                {
                    return HttpStatusCode.NotFound;
                }

            }
            catch
            {
                return HttpStatusCode.NotFound;
            }
        }
        #endregion

        /// <summary>
        /// ターゲットのURLの生存チェック
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        #region chekcLiveUrl
        public static bool chekcLiveUrl(string url)
        {
            return getHttpStatus(url) == HttpStatusCode.OK;
        }
        #endregion
    }
}
