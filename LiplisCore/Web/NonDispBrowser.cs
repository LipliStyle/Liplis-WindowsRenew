//=======================================================================
//  ClassName : NonDispBrowser
//  概要      : フォーム非表示、バックグラウンドでWebページを描画する。
//              その後、ブラウザに描画されたドキュメントを取得したりする。
//         
//              WebBrowserコンポーネント(IEコンポーネント)を使用している。
//              これはCOMであるため、スレッドはSTAで実行される必要がある。
//              LpsSTATask.Runを使用することを推奨する。
//
//  Liplis5.0
//
//  Copyright(c) 2010-2016 LipliStyle.Sachin
//=======================================================================
using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace Liplis.Web
{
    public class NonDispBrowser : WebBrowser
    {
        protected bool done;

        // タイムアウト時間（10秒）
        protected TimeSpan timeout = new TimeSpan(0, 0, 10);



        ///====================================================================
        ///
        ///                           初期化処理
        ///                         
        ///====================================================================
        #region 初期化処理
        /// <summary>
        /// コンストラクター
        /// </summary>
        public NonDispBrowser()
        {
            // スクリプト・エラーを表示しない
            this.ScriptErrorsSuppressed = true;
            this.NewWindow += new CancelEventHandler(NonDispBrowser_NewWindow);
        }
        #endregion

        ///====================================================================
        ///
        ///                           イベントハンドラ
        ///                         
        ///====================================================================
        #region イベントハンドラ
        /// <summary>
        /// 読み込み完了時イベント
        /// </summary>
        /// <param name="e"></param>
        [STAThread]
        protected override void OnDocumentCompleted(
                      WebBrowserDocumentCompletedEventArgs e)
        {
            try
            {
                //エラートラップ
                //this.Document.Window.Error += new HtmlElementErrorEventHandler(Window_Error);

                // ページにフレームが含まれる場合にはフレームごとに
                // このメソッドが実行されるため実際のURLを確認する
                if (e.Url == this.Url)
                {
                    done = true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

        }

        /// <summary>
        /// 新規ウインドウ表示時イベント
        /// </summary>
        /// <param name="e"></param>
        [STAThread]
        protected override void OnNewWindow(CancelEventArgs e)
        {
            // ポップアップ・ウィンドウをキャンセル
            e.Cancel = true;
        }

        /// <summary>
        /// 新規ウインドウ表示時イベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected virtual void NonDispBrowser_NewWindow(object sender, CancelEventArgs e)
        {
            string url = this.StatusText;
            if (url != "")
            {
                //開こうとしていたページに移る
                this.Navigate(url);
            }

            // キャンセル
            e.Cancel = true;
        }

        /// <summary>
        /// ウインドウエラー発生時
        /// 無視する
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected virtual void Window_Error(object sender, HtmlElementErrorEventArgs e)
        {
            // Ignore the error and suppress the error dialog box. 
            e.Handled = false;
        }
        #endregion

        ///====================================================================
        ///
        ///                           パブリックメソッド
        ///                         
        ///====================================================================
        #region パブリックメソッド
        /// <summary>
        /// ナビゲイトして完了まで待つ
        /// </summary>
        /// <param name="url"></param>
        [STAThread]
        public bool NavigateAndWait(string url)
        {
            try
            {
                base.Navigate(url); // ページの移動

                done = false;
                DateTime start = DateTime.Now;

                while (done == false)
                {
                    if (DateTime.Now - start > timeout)
                    {
                        // タイムアウト
                        return false;
                    }
                    Application.DoEvents();
                }
                return true;
            }
            catch
            {
                return false;
            }
        }
       


        /// <summary>
        /// ナビゲイトして完了まで待つ
        /// </summary>
        /// <param name="url"></param>
        [STAThread]
        public virtual bool NavigateAndWaitFromSource(string source)
        {
            try
            {
                base.DocumentText = source; // ページの移動

                done = false;
                DateTime start = DateTime.Now;


                Application.DoEvents();
                while (done == false)
                {
                    if (DateTime.Now - start > timeout)
                    {
                        // タイムアウト
                        return false;
                    }
                    Application.DoEvents();
                }
                return true;
            }
            catch
            {
                return false;
            }
        }
        #endregion





    }
}
