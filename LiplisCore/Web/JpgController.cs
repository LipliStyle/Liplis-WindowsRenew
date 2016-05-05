//=======================================================================
//  ClassName : Jpgコントローラー
//  概要      : JpgController
//
//  Liplisシステム      
//  Copyright(c) 2010-2016 sachin.Sachin
//=======================================================================
using Liplis.Com;
using Liplis.Utl;
using Liplis.Xml;
using System;
using System.Drawing;
using System.Net;
using System.Reflection;
using System.Text.RegularExpressions;

namespace Liplis.Web
{
    public class JpgController : XmlReadList, IDisposable
    {
        /// <summary>
        /// コンストラクター
        /// </summary>
        public JpgController()
        {
        }

        /// <summary>
        /// 破棄
        /// </summary>
        public void Dispose()
        {
            
        }

        /// <summary>
        /// サムネイルを取得する
        /// </summary>
        /// <returns></returns>
        public Bitmap getThumbnail(string thumbnail_url, string dlPath)
        {
            string fileName = "";
            try
            {
                if (!checkFileExist(dlPath))
                {
                    fileName = dlPath + getJpgFileName(thumbnail_url);
                    downLoad(thumbnail_url, fileName);
                    return new Bitmap(fileName);
                }

                return new Bitmap(0, 0);
            }
            catch (Exception err)
            {
                LpsLogController.writingLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, err.Message);
                return new Bitmap(0, 0);
            }
        }

        /// <summary>
        /// サムネイルをダウンロードし、名前を付けて保存する.
        /// ダウンロードしたパスを返す。
        /// </summary>
        /// <param name="uri"></param>
        /// <param name="cacheFilePath"></param>
        public string downLoadthumb(string uri, string cacheFilePath)
        {
            string fileName = "";
            try
            {
                if (!checkFileExist(cacheFilePath))
                {
                    fileName = cacheFilePath + getJpgFileName(uri);
                    downLoad(uri, fileName);
                }
                return fileName;
            }
            catch (Exception err)
            {
                LpsLogController.writingLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, err.Message);
                return fileName;
            }
        }

        /// <summary>
        /// サムネイルをダウンロードする
        /// </summary>
        public void downLoad(string uri, string cacheFilePath)
        {
            try
            {
                using (WebClient wc = new WebClient())
                {
                    wc.DownloadFile(uri, cacheFilePath);
                }
            }
            catch (WebException)
            {
            }
            catch (Exception err)
            {
                LpsLogController.writingLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, err.Message);
            }
        }

        /// <summary>
        /// jpgファイル名を取得する
        /// </summary>
        /// <returns></returns>
        private string getJpgFileName(string jpgUrl)
        {
            Regex r = new Regex(@"/[-_.!~*'()a-zA-Z0-9;?:@&=+$,%#]+jpg", RegexOptions.IgnoreCase | RegexOptions.Singleline);
            //TextBox1.Text内で正規表現と一致する対象をすべて検索
            MatchCollection mc = r.Matches(jpgUrl);
            try
            {
                foreach (System.Text.RegularExpressions.Match m in mc)
                {
                    //正規表現に一致したグループと位置を表示
                    return DateTime.Now.ToString("yyyyMMddHHmmss") + m.Groups[0].ToString().Substring(1);
                }
                return DateTime.Now.ToString("yyyyMMddHHmmss") + LpsLiplisUtil.getName(5) + ".jpg";
            }
            catch (Exception err)
            {
                LpsLogController.writingLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, err.Message);
                return DateTime.Now.ToString("yyyyMMddHHmmss") + LpsLiplisUtil.getName(5) + ".jpg";
            }
        }



        /// <summary>
        /// ファイル存在チェック
        /// </summary>
        /// <returns></returns>
        public static bool checkFileExist(string path)
        {
            return System.IO.File.Exists(path);
        }
    }
}
