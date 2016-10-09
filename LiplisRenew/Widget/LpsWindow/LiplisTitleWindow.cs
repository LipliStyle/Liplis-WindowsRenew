//=======================================================================
//  ClassName : LiplisTitleWindow
//  概要      : リプリスタイトルウインドウ
//
// iOS版:UiImageに対応
//
//Liplis5.0
//
//アップデート履歴
//   2016/05/08 ver5.0.0 作成
//
//  Copyright(c) 2010-2016 LipliStyle.Sachin
//=======================================================================
using Liplis.MainSystem;
using System;
using System.Diagnostics;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows;

namespace Liplis.Widget.LpsWindow
{
    public class LiplisTitleWindow : LiplisWindow
    {
        //=================================
        //ウインドウ要素
        protected Hyperlink hl;
        protected TextBlock tb;

        //=================================
        //ピクチャー
        protected LiplisNewsPicture picture;

        //============================================================
        //
        //初期化処理
        //
        //============================================================
        #region 初期化処理

        /// <summary>
        /// コンストラクター
        /// </summary>
        /// <param name="setting"></param>
        /// <param name="skin"></param>
        public LiplisTitleWindow(LiplisWidget lips, LiplisWidgetPreference setting, Skin skin, double lpsTop, double lpsLeft, double lpsWidth, LiplisWindowStack windowPos,  string title, Uri url, Uri jpgUrl) : base(lips, setting, skin, lpsTop, lpsLeft, lpsWidth, windowPos)
        {
            //ハイパーリンクの生成
            setHypderLink(title,url);

            //小ウインドウの生成
            createPicuteWindow(jpgUrl);
        }

        /// <summary>
        /// ウインドウの初期化
        /// (ハイパーリンクの作成)
        /// </summary>
        protected void setHypderLink(string title, Uri url)
        {
            //要素インスタンス化
            this.hl = new Hyperlink();
            this.tb = new TextBlock();

            //タイトル設定
            this.tb.Text = title;
            this.tb.TextWrapping = System.Windows.TextWrapping.Wrap;

            //URL設定
            this.hl.NavigateUri =url;
            this.hl.RequestNavigate += hl_RequestNavigate;

            //ハイパーリンクのインラインにテキストブロック設定
            this.hl.Inlines.Add(this.tb);

            //ラベルコンテントの上書き
            this.lblLpsTalkLabel.Content = this.hl;
        }

        /// <summary>
        /// ウインドウの初期化
        /// </summary>
        /// <param name="lpsTop"></param>
        /// <param name="lpsLeft"></param>
        /// <param name="lpsWidth"></param>
        /// <param name="talkWindowHeightMargin"></param>
        /// <param name="talkLabelHeightMargin"></param>
        protected override void initWindow(double lpsTop, double lpsLeft, double lpsWidth, int talkWindowHeightMargin, int talkLabelHeightMargin)
        {
            //ウインドウをセットする
            base.initWindow(lpsTop, lpsLeft, lpsWidth, 46, 36);

            //ボタンの可視化
            this.btnNext.Visibility = Visibility.Visible;
            this.btnCopyUrl.Visibility = Visibility.Visible;
            this.btnWeb.Visibility = Visibility.Visible;
            this.btnTweet.Visibility = Visibility.Visible;

            //アイコンロケーションを設定する
            setIconLocation(this.btnNext);
            setIconLocation(this.btnCopyUrl);
            setIconLocation(this.btnWeb);
            setIconLocation(this.btnTweet);
        }

        /// <summary>
        /// ピクチャーの生成
        /// </summary>
        private void createPicuteWindow(Uri jpgUrl)
        {
            if(jpgUrl != null)
            {
                int locationX = (int)this.Left + (int)this.Width / 2 - 50;
                int locationY = (int)this.Top - 110;


                picture = new LiplisNewsPicture(jpgUrl, locationX, locationY);
                picture.Show();
            }
        }

        /// <summary>
        /// ウインドウを終了する
        /// </summary>
        public override void endWindow()
        {
            base.endWindow();

            if(picture != null)
            {
                picture.endWindow();
            }
        }

        #endregion

        //============================================================
        //
        //イベントハンドラ
        //
        //============================================================
        #region イベントハンドラ
        /// <summary>
        /// リンククリック時イベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void hl_RequestNavigate(object sender, System.Windows.Navigation.RequestNavigateEventArgs e)
        {
            Process.Start(new ProcessStartInfo(e.Uri.AbsoluteUri));
            e.Handled = true;
        }

        /// <summary>
        /// リンクコピー
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected override void btnCopyUrl_Click(object sender, RoutedEventArgs e)
        {
            Clipboard.SetText(this.hl.NavigateUri.ToString());
        }

        /// <summary>
        /// ウェブボタン
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected override void btnWeb_Click(object sender, RoutedEventArgs e)
        {
            Process.Start(new ProcessStartInfo(this.hl.NavigateUri.ToString()));
        }

        /// <summary>
        /// ツイートボタン
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected override void btnTweet_Click(object sender, RoutedEventArgs e)
        {
            tweet(this.tb.Text);
        }
        #endregion


        //============================================================
        //
        //処理
        //
        //============================================================
        #region 処理
        /// <summary>
        /// スキップ表示
        /// </summary>
        /// <param name="liplisChatText"></param>
        public override void updateSkip(string liplisChatText)
        {
            Dispatcher.Invoke(new Action(() =>
            {
                //タイトル設定
                this.tb.Text = liplisChatText;

                this.UpdateLayout();

                //現在高さ取得
                nowTxbLpsTalkLabelHeight = (Int32)tb.ActualHeight;
                sizeChanveAnimation();
            }));
        }

        /// <summary>
        /// ウインドウを移動する
        /// </summary>
        public override void windowMove(LiplisWindowStack windowPos)
        {
            //ウインドウ移動
            base.windowMove(windowPos);

            //子ウインドウがあれば追随する
            if (picture != null)
            {
                int locationX = (int)this.LocationX + (int)this.Width / 2 - 50;
                int locationY = (int)this.LocationY - 110;

                picture.windowMove(locationX, locationY);
            }
        }
        #endregion

    }
}
