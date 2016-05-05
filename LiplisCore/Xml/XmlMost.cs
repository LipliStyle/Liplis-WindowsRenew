//=======================================================================
//  ClassName : XmlMost
//  概要      : XMLコントローラの規定クラス
//
//  Liplis5.0
//
//  Copyright(c) 2010-2016 LipliStyle.Sachin
//=======================================================================
using System;
using System.Net;
using System.IO;
using System.Xml;
using System.Text;
using Liplis.Utl;
using System.Reflection;

namespace Liplis.Xml
{
    public abstract class XmlMost
    {
        ///=============================
        ///キャッシュファイルパス
        protected string xmlFilePath;
        protected string url;

        ///====================================================================
        ///
        ///                            初期化処理
        ///                         
        ///====================================================================

        /// <summary>
        /// コンストラクター
        /// </summary>
        public XmlMost()
        {

        }

        ///====================================================================
        ///
        ///                           XML読み込み処理
        ///                         
        ///====================================================================
        #region ダウンロード処理

        /// <summary>
        /// downLoadXml
        /// XMLをダウンロードする
        /// </summary>
        /// <param name="fileName">ファイル名</param>
        /// <param name="url">URL</param>
        public void downLoadXml(string fileName, string pUrl)
        {
            try
            {
                WebClient wc = new WebClient();
                xmlFilePath = LpsPathController.getXmlPath() + fileName;
                url = pUrl;

                wc.DownloadFile(pUrl, xmlFilePath);
            }
            catch (WebException)
            {
                //XML例外 XMLが読み込めませんでした。
                LpsLogController.writingLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, "Web例外 XMLが読み込めませんでした。サーバーが存在しない可能性があります");
            }
            catch (Exception err)
            {
                //ここでスルーするか
                //lc.writingLog("XmlMost : downLoadXml\n" + err);
                throw err;  //下層に投げる
            }
        }

        /// <summary>
        /// readXml
        /// XMLを読み込む
        /// </summary>
        protected abstract void readXml();

        /// <summary>
        /// xmlファイルを訂正する
        /// </summary>
        /// <param name="linenumber"></param>
        /// <param name="filepath"></param>
        
        protected bool ReplaceSpecialChars(long linenumber, string filepath, string tempfile, bool flgRetry, int cntRetry)
        {
            try
            {
                //無効文字があった場合、ファイルだうんろーどして除去
                if (!flgRetry)
                {
                    downLoadXml(tempfile, xmlFilePath);
                    xmlFilePath = LpsPathController.getXmlPath() + tempfile;
                }

                StreamReader strm;
                string strline;
                string strreplace = " ";
                try
                {
                    System.IO.File.Copy(filepath, tempfile, true);
                }
                catch
                {

                }

                StreamWriter strmwriter = new StreamWriter(filepath, false, Encoding.GetEncoding("Shift-JIS"));
                strmwriter.AutoFlush = true;
                strm = new StreamReader(tempfile, Encoding.GetEncoding("Shift-JIS"));
                long i = 0;
                while (i < linenumber - 1)
                {
                    strline = strm.ReadLine();
                    strmwriter.WriteLine(strline);
                    i = i + 1;
                }

                strline = strm.ReadLine();
                Int32 lineposition;

                lineposition = strline.IndexOf("&");
                if (lineposition > 0)
                {
                    strreplace = "＆";
                }
                else
                {
                    lineposition = strline.IndexOf("<", 1);
                    if (lineposition > 0)
                    {
                        strreplace = "＜";
                    }

                }
                strline = strline.Substring(0, lineposition - 1) + strreplace + strline.Substring(lineposition + 1);
                strmwriter.WriteLine(strline);

                strline = strm.ReadToEnd();
                strmwriter.WriteLine(strline);

                strm.Close();
                strm = null;

                strmwriter.Flush();
                strmwriter.Close();
                strmwriter = null;

                //指定したXMLファイルの読み込み
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(xmlFilePath);


                return true;
            }
            catch (System.Xml.XmlException err)
            {
                //XML例外 XMLが読み込めませんでした。
                LpsLogController.writingLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, xmlFilePath + "が読み込めません");

                //エラーを呼び出し元に移管
                throw err;
            }
            catch
            {
                return false;
            }


        }
        #endregion
    }
}
