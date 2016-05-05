//=======================================================================
//  ClassName : LpsLogController
//  概要      : リプリスログコントローラー
//
//  Liplis5.0
//
//  Copyright(c) 2010-2016 LipliStyle.Sachin
//=======================================================================
using System;
using System.IO;
using System.Text;
using System.Windows.Forms;


namespace Liplis.Utl
{
    public class LpsLogController
    {
        ///=============================
        /// プロパティ
        private string logFilePath;
        private string logStr;
        private Encoding enc;

        ///====================================================================
        ///
        ///                            コンストラクター
        ///                         
        ///====================================================================
        #region コンストラクター
        /// <summary>
        /// コンストラクター
        /// </summary>
        public LpsLogController()
        {
            //ログファイルパスの取得
            logFilePath = getLogPath();

            //ログエンコーディングの設定
            enc = Encoding.GetEncoding(932);
        }

        /// <summary>
        /// コンストラクター
        /// </summary>
        public LpsLogController(string fileName)
        {
            checkDir(getLogDir());

            //ログファイルパスの取得
            logFilePath = getAppPath() + "\\log\\" + fileName;

            //ログエンコーディングの設定
            enc = Encoding.GetEncoding(932);
        }

        #endregion

        ///====================================================================
        ///
        ///                            書き込み処理
        ///                         
        ///====================================================================
        #region 書き込み処理
        /// <summary>
        /// writingLog
        /// 引数で指定された内容をログに書き込む
        /// </summary>
        /// <param name="body">書き込み内容</param>
        public void writingLog(string body)
        {
            string logStr = "[INFO ] " + DateTime.Now + body + "\n";

            try { File.AppendAllText(logFilePath, logStr, enc); }
            catch (System.ComponentModel.Win32Exception)
            {
                Application.Exit();
            }
            catch { }

        }
        public static void writingLogSt(string body)
        {
            string logStr = "[INFO ] " + DateTime.Now + body + "\n";

            try { File.AppendAllText(getLogPath(), logStr, Encoding.GetEncoding(932)); }
            catch (System.ComponentModel.Win32Exception)
            {
                d("ログ書き込みエラー");
            }
            catch { }

        }
        public static void writingLog(string className, string methodName, string body)
        {
            string logStr = "[INFO ] " + DateTime.Now + " " + className + " " + methodName + ":" + body + Environment.NewLine;

            try { File.AppendAllText(getLogPath(), logStr, Encoding.GetEncoding(932)); }
            catch (System.ComponentModel.Win32Exception)
            {
                d("ログ書き込みエラー");
            }
            catch { }

        }

        /// <summary>
        /// writingPlaneText
        /// 引数で指定された内容をログに書き込む
        /// </summary>
        /// <param name="body">書き込み内容</param>
        public void writingPlaneText(string body)
        {
            try { File.AppendAllText(logFilePath, body, enc); }
            catch (System.ComponentModel.Win32Exception)
            {
                Application.Exit();
            }
            catch { }

        }
        #endregion

        ///====================================================================
        ///
        ///                       メッセージボックスの表示
        ///                         
        ///====================================================================
        #region メッセージボックスの表示
        /// <summary>
        /// 引数で指定された内容をログに書き込む
        /// </summary>
        /// <param name="errBody"></param>
        public void callErrMsg(Exception e)
        {
            //ログ文の作成
            logStr = "[ERROR] " + DateTime.Now + " " + e.ToString() + "\r\n";

            //メッセージボックス
            MessageBox.Show(e.ToString(), "Liplis");

            //ログ書込
            try { File.AppendAllText(logFilePath, logStr, enc); }
            catch { }
        }
        public void callErrMsg(string msg)
        {
            //ログ文の作成
            logStr = "[ERROR] " + DateTime.Now + " " + msg + "\r\n";

            //メッセージボックス
            MessageBox.Show(msg, "Liplis");

            //ログ書込
            try { File.AppendAllText(logFilePath, logStr, enc); }
            catch { }
        }


        /// <summary>
        /// ダイアログ表示
        /// </summary>
        /// <param name="errBody"></param>
        public void openDialog(string errBody)
        {
            //メッセージボックス
            MessageBox.Show(errBody, "Liplis");
        }
        #endregion

        ///====================================================================
        ///
        ///                             一般処理
        ///                         
        ///====================================================================
        #region 一般処理
        /// <summary>
        /// ログファイルパスを返す
        /// </summary>
        /// <returns></returns>
        public static string getLogPath()
        {
            try
            {
                checkDir(getAppPath() + "\\log");
                return getAppPath() + "\\log\\liplis.log";
            }
            catch (Exception)
            {
                return "";
            }
        }

        /// <summary>
        /// ログディレクトリを返す
        /// </summary>
        /// <returns></returns>
        public static string getLogDir()
        {
            try
            {
                return getAppPath() + "\\log";
            }
            catch (Exception)
            {
                return "";
            }
        }

        /// <summary>
        /// ディレクトリの存在チェックを行い、無かったら生成する
        /// </summary>
        /// <param name="path"></param>
        public static void checkDir(string path)
        {
            try
            {
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
            }
            catch
            {
                //MessageBox.Show("ファイルの書き込みが許可されているか確認して下さい。\nLiplisをインストールし直すと解決する可能性もあります。","Liplis");
                Application.Exit();
            }
        }

        /// <summary>
        /// アプリケーションの起動パスを返す
        /// </summary>
        /// <returns></returns>
        public static string getAppPath()
        {
            return Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
        }

        /// <summary>
        /// d
        /// メッセージをダンプする
        /// </summary>
        /// <param name="msg">ダンプメッセージ</param>
        public static void d(string msg)
        {
            Console.WriteLine(msg);
        }
        #endregion

    }
}
