//=======================================================================
//  ClassName : UrlEncoder
//  概要      : URLエンコードを行う
//
//  Tips      :一度バイト列に変換してからURLエンコードを行う
//
//  Liplis5.0
//
//  Copyright(c) 2010-2016 LipliStyle.Sachin
//=======================================================================
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System;
using System.IO;
using System.Threading;
using Liplis.Web.WebEncoding;
using Liplis.Tasks;
using Liplis.Utl;
using System.Reflection;

namespace Liplis.Web
{
    public class HtmlParser
    {

        /// <summary>
        /// コンストラクター
        /// </summary>
        public HtmlParser(){}
                
        /// <summary>
        /// httpソースに含まれる本文だけ取得する
        /// 一般系から逸脱する可能性があるため、スペシャルメソッドとする
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        #region getHtmlPlainTextFromSource
        public string getHtmlPlainTextFromSource(string source)
        {
            try
            {
                string result = source;

                //正規表現で無駄なものを除去
                result = brRegularReplace(result);
                result = htmlTagRegularRemove(result);
                result = htmlCommentRegularRemove(result);
                result = htmlGomiRegularRemove(result);
                result = htmlMultiNewLineRemove(result);
                result = htmYMDRemove(result);
                result = result.Replace("＃", Environment.NewLine);
                result = result.Replace("\t", "");
                result = result.Replace("&gt;", "");
                result = result.Replace("&nbsp;", "");
                result = result.Replace("&amp;", "");
                result = result.Replace("<", "");
                result = result.Replace(">", "");

                return result;
            }
            catch
            {
                return source;
            }
            finally
            {

            }
        }
        #endregion

        /// <summary>
        /// httpソースに含まれる本文だけ取得する
        /// 一般系から逸脱する可能性があるため、スペシャルメソッドとする
        /// ウェブブラウザにソースをかませて本文を取得。
        /// そのときに、プログラムが読みやすいように出力させるため、加工を施す
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        #region getHtmlPlainTextFromSource
        public string getHtmlPlainTextFromSourceWB(string source)
        {
            try
            {
                string result = source;

                //正規表現で無駄なものを除去
                result = pRegularReplace(result);

                return result;
            }
            catch
            {
                return source;
            }
            finally
            {

            }
        }
        #endregion

        /// <summary>
        /// タグで改行させる
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        #region getHtmlPlainTextFromSource
        public string getHtmlPlainTextFromSourceWBDirect(string source)
        {
            try
            {
                string result = source;

                //正規表現で無駄なものを除去
                result = pRegularReplace(result);

                return result;
            }
            catch
            {
                return source;
            }
            finally
            {

            }
        }
        #endregion

        /// <summary>
        /// HTMLソースを取得する
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        #region getHtmlSource
        [STAThread]
        public string getHtmlSource(string url)
        {
            return getHtmlSource(url, 30000);
        }
        //スレッド実行するときは以下のメソッドを使う
        public string getHtmlSourceThread(string url)
        {
            return getHtmlSourceThread(url, 30000);
        }
        [STAThread]
        public string getHtmlSource(string url, int TimeOut)
        {
            //近代化
            return getHtmlSourceSetUserAgentThread(url, UserEgent.GetInstance().getUserEgant(), TimeOut, WebRequest.GetSystemWebProxy());
        }
        [STAThread]
        public string getHtmlSourceThread(string url, int TimeOut)
        {
            string body = "";

            //STAタスク実行
            LpsSTATask.Run(() =>
            {
                body = getHtmlSource(url, TimeOut);
            }).Wait();

            return body;
        }
        [STAThread]
        public string getHtmlSourceSetUserAgent(string url, string userAgent, int TimeOut, IWebProxy wp)
        {
            //URL空チェック
            if (url == "") { return ""; }

            try
            {
                //結果
                string resStr = "";

                byte[] result;
                byte[] buffer = new byte[4096];

                //ウェブリクエスト
                HttpWebRequest wr = (HttpWebRequest)WebRequest.Create(url);
                wr.UserAgent = userAgent;

                wr.Timeout = TimeOut;

                //プロキシー設定がNULLでなければ、設定する
                if (wp != null)
                {
                    wr.Proxy = wp;
                }

                //レスポンス取得
                using (WebResponse response = wr.GetResponse())
                {
                    Console.WriteLine(response.ContentType);

                    //レスポンスをストリームで取得
                    using (Stream responseStream = response.GetResponseStream())
                    {
                        //ストリームをコピー
                        using (MemoryStream memoryStream = new MemoryStream())
                        {
                            int count = 0;
                            do
                            {
                                count = responseStream.Read(buffer, 0, buffer.Length);
                                memoryStream.Write(buffer, 0, count);

                            } while (count != 0);

                            //バイト配列に変換
                            result = memoryStream.ToArray();

                        }
                    }
                }

                // 得られたバイトコードを自動的にエンコードを調べ、テキストに変換する
                resStr = GetWebText(result);

                //結果を返す
                return resStr;
            }
            catch (WebException ex)
            {
                LpsLogController.writingLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, "取得失敗 理由: " + ex.Status + " URL: " + url);
                return "";
            }
            catch (OutOfMemoryException)
            {
                LpsLogController.writingLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, " アウトオブメモリ 内容が多すぎてダウンロード失敗 URL:" + url);
                return "";
            }
            catch (Exception err)
            {
                LpsLogController.writingLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, err + Environment.NewLine + url);
                return "";
            }
        }
        [STAThread]
        public string getHtmlSourceSetUserAgentThread(string url, string userAgent, int TimeOut, IWebProxy wp)
        {
            string body = "";
            //ダウンロード

            //STAタスク実行
            LpsSTATask.Run(() =>
            {
                body = getHtmlSourceSetUserAgent(url, userAgent, TimeOut, wp);
            }).Wait();

            return body;
        }
        #endregion
        
        /// <summary>
        /// 文字コードを判別する
        /// </summary>
        /// <param name="byts">文字コードを調べるデータ</param>
        /// <returns>適当と思われるEncodingオブジェクト。
        /// 判断できなかった時はnull。</returns>
        #region GetCode
        public static Encoding GetCode(byte[] byts)
        {
            return GetCharCode(byts).GetEncoding();
        }
        public static CharCode GetCharCode(byte[] byts)
        {
            string result = "";

            ReadJEnc rje = ReadJEnc.JP;

            return rje.GetEncoding(byts, byts.Length, out result);
        }
        #endregion

        /// <summary>
        /// 文字コードを自動判別して、エンコードし、テキストを返す
        /// </summary>
        /// <param name="byts"></param>
        /// <returns></returns>
        public static string GetWebText(byte[] byts)
        {
            string result = "";

            ReadJEnc.JP.GetEncoding(byts, byts.Length, out result);

            return result;
        }

        /// <summary>
        /// URL正規表現チェック
        /// </summary>
        /// <returns></returns>
        public static bool urlRegularCheck(string url)
        {
            Regex regex = new Regex("s?https?://[-_.!~*'()a-zA-Z0-9;/?:@&=+$,%#]+");
            try
            {
                return regex.IsMatch(url);
            }
            catch
            {
                return false;
            }
            finally
            {
                regex = null;
            }

        }

        /// <summary>
        /// HTMLタグ正規表現チェック
        /// </summary>
        /// <returns></returns>
        public static bool htmlTagRegularCheck(string discription)
        {
            Regex regex = new Regex("<.*?>");
            try
            {
                return regex.IsMatch(discription);
            }
            catch
            {
                return false;
            }
            finally
            {
                regex = null;
            }
        }

        /// <summary>
        /// brタグ改行変換
        /// </summary>
        /// <returns></returns>
        public static string brRegularReplace(string discription)
        {
            Regex regex = new Regex("<br .*?>");
            string result;
            try
            {
                result = regex.Replace(discription, "＃");
                return result;
            }
            catch
            {
                return "";
            }
            finally
            {
                regex = null;
            }
        }

        /// <summary>
        /// HTMLタグ正規表現除去
        /// </summary>
        /// <returns></returns>
        public static string htmlTagRegularRemove(string discription)
        {
            Regex regex = new Regex("<.*?>");

            string result;
            try
            {
                result = regex.Replace(discription, "");
                return result;
            }
            catch
            {
                return "";
            }
            finally
            {
                regex = null;
            }
        }

        /// <summary>
        /// 2chの投稿者を削除する
        /// </summary>
        /// <returns></returns>
        public static string html2chWriterRegularRemove(string discription)
        {
            try
            {
                return new Regex(@"([0-9]*:)|([0-9]*\s).*((ID:+[a-zA-Z0-9\s!-/:-@≠\[-`{-~]*)|([0-9]*:[0-9]*:[0-9]*\.[0-9]*\s[0-9]*)|([0-9]*:[0-9]*:[[0-9]*))").Replace(discription, "");
            }
            catch
            {
                return "";
            }
        }

        /// <summary>
        /// dcdescription
        /// </summary>
        /// <returns></returns>
        public static string htmlDcDescriptionRegularRemove(string discription)
        {
            try
            {
                return new Regex("dc[a-zA-Z]*(=\")|(=)").Replace(discription, "");
            }
            catch
            {
                return "";
            }
        }

        /// <summary>
        /// htmlEncodeRegularRemove
        /// htmlエンコード文字を除去する
        /// </summary>
        /// <returns></returns>
        public static string htmlEncodeRegularRemove(string discription)
        {
            try
            {
                return new Regex("&.*;").Replace(discription, "");
            }
            catch
            {
                return "";
            }
        }

        /// <summary>
        /// HTMLタグ正規表現除去
        /// </summary>
        /// <returns></returns>
        public static string htmlCommentRegularRemove(string discription)
        {
            Regex regex = new Regex("<!--.*-->");
            try
            {
                return regex.Replace(discription, "");
            }
            catch
            {
                return "";
            }
            finally
            {
                regex = null;
            }
        }

        /// <summary>
        /// 英語記号、半角スペースが連続１０以上HITの場合除去する
        /// </summary>
        /// <returns></returns>
        public static string htmlGomiRegularRemove(string discription)
        {
            return htmlGomiRegularRemove(discription, 10);
        }
        public static string htmlGomiRegularRemove(string discription, int renzoku)
        {
            Regex regex = new Regex("[-_.!~*'()a-zA-Z0-9;/?:@&=+$,%#a-zA-z0-9\\s\\\"\\[\\]\\{\\}]{" + renzoku.ToString() + ",}");
            try
            {
                return regex.Replace(discription, "");
            }
            catch
            {
                return "";
            }
            finally
            {
                regex = null;
            }
        }

        /// <summary>
        /// 同じ内容の行を削除する
        /// </summary>
        /// <returns></returns>
        public static string htmlMultiNewLineRemove(string discription)
        {
            return Regex.Replace(discription, @"^(.*)(\n)+$", "$1", RegexOptions.Multiline);
        }

        /// <summary>
        /// HTMLタグ正規表現除去
        /// </summary>
        /// <returns></returns>
        public static string htmYMDRemove(string discription)
        {
            Regex regex = new Regex("^[0-9]{4}年[0-9]{2}月[0-9]{2}日$");
            return regex.Replace(discription, "");
        }

        /// <summary>
        /// pタグ改行変換
        /// </summary>
        /// <returns></returns>
        public static string pRegularReplace(string discription)
        {
            Regex regex = new Regex("</p>");
            Regex regex2 = new Regex("<p .*?>");
            string result;
            result = regex.Replace(discription, "<br>");
            result = regex.Replace(result, "");
            return result;
        }

        /// <summary>
        /// HTMLタグ正規表現を使ってリストに整形
        /// </summary>
        /// <returns></returns>
        public List<string> htmlTagRegularList(string discription)
        {
            List<string> result = new List<string>();

            //discription内で正規表現と一致する対象をすべて検索
            Regex regex = new Regex("<.*?>");
            MatchCollection mc = regex.Matches(discription);

            try
            {
                foreach (System.Text.RegularExpressions.Match m in mc)
                {
                    //正規表現に一致したグループと位置を表示
                    foreach (var g in m.Groups)
                    {
                        System.Windows.Forms.Application.DoEvents();
                        result.Add(g.ToString());
                    }
                }
                return result;
            }
            catch
            {
                return new List<string>();
            }
            finally
            {
                mc = null;
                regex = null;
            }
        }

        /// <summary>
        /// HTMLタグ正規表現チェック
        /// </summary>
        /// <returns></returns>
        public bool kigoRegularCheck(string discription)
        {
            Regex regex = new Regex("@[｡-ﾟ]");
            return regex.IsMatch(discription);
        }

        /// <summary>
        /// Imgタグを走査し、存在したらURLを返す。
        /// </summary>
        /// <returns></returns>
        public List<string> searchImgTag(List<string> tagList)
        {
            List<string> result = new List<string>();
            string buf = "";

            try
            {
                foreach (string tag in tagList)
                {
                    System.Windows.Forms.Application.DoEvents();
                    if (checkImgTag(tag))
                    {
                        //src属性の検索と取得
                        Regex rSrc = new Regex("src=\"htt.*\"", RegexOptions.IgnoreCase | RegexOptions.Singleline);
                        MatchCollection mcSrc = rSrc.Matches(tag);
                        foreach (Match m in mcSrc)
                        {
                            //正規表現に一致したグループと位置を表示
                            buf = m.Groups[0].ToString();
                        }

                        rSrc = null;
                        mcSrc = null;

                        //httpアドレスを取得する
                        Regex rHttp = new Regex("s?https?://[-_.!~*'()a-zA-Z0-9;/?:@&=+$,%#]+", RegexOptions.IgnoreCase | RegexOptions.Singleline);
                        MatchCollection mcHttp = rHttp.Matches(tag);
                        foreach (Match m in mcHttp)
                        {
                            //正規表現に一致したグループと位置を表示
                            result.Add(m.Groups[0].ToString());
                        }

                        rHttp = null;
                        mcHttp = null;
                    }
                }
                return result;
            }
            catch
            {
                return new List<string>();
            }

        }

        /// <summary>
        /// imgタグが存在するかチェックする
        /// </summary>
        /// <returns></returns>
        public bool checkImgTag(string tag)
        {
            Regex regex = new Regex(@"<img.*>", RegexOptions.IgnoreCase | RegexOptions.Singleline);
            try
            {
                return regex.IsMatch(tag);
            }
            catch
            {
                return false;
            }
            finally
            {
                regex = null;
            }


        }

    }
}
