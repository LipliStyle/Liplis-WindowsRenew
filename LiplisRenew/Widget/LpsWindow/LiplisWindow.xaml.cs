//=======================================================================
//  ClassName : LiplisWindow
//  概要      : リプリスウインドウ
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
using Liplis.Com;
using Liplis.Gui;
using Liplis.MainSystem;
using Liplis.Web.Clalis;
using Liplis.Wpf;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;

namespace Liplis.Widget.LpsWindow
{
    /// <summary>
    /// XsamlWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class LiplisWindow : Window
    {
        //=================================
        //Liplis要素
        protected LiplisWidget lips;
        protected LiplisWidgetPreference setting;
        protected Skin skin;

        //=================================
        //ウインドウ制御プロパティ
        protected Int32 nowTxbLpsTalkLabelHeight;
        protected Int32 prvTxbLpsTalkLabelHeight;
        public LiplisWindowStack windowPos;

        //=================================
        //ロケーションプロパティ
        public double LocationX { get; set; }
        public double LocationY { get; set; }

        //=================================
        //リプリスウィジェットとウインドウのデフォルトインターバル
        protected const Int32 WIDGET_WINDOW_INTERVAL = -10;

        //=================================
        //LabelHeight 変更する場合は、この数値ではなく、initWindowの引数を操作する。
        protected Int32 TALK_WINDOW_HEIGHT_MARGIN = 27;
        protected Int32 TALK_LABEL_HEIGHT_MARGIN  = 17;

        //=================================
        //ボタンロケーションマージン
        protected Int32 BTN_LOCATION_MARGINE = 28;
        protected Int32 PRG_LOCATION_MARGINE = 22;
        protected Int32 LBL_LOCATION_MARGINE = 31;

        //=================================
        //バックグラウンドワーク
        protected System.ComponentModel.BackgroundWorker workerTweet;

        //============================================================
        //
        //初期化処理
        //
        //============================================================
        #region 初期化処理

        /// <summary>
        /// コンストラクター
        /// </summary>
        public LiplisWindow(LiplisWidget lips, LiplisWidgetPreference setting, Skin skin, double lpsTop, double lpsLeft, double lpsWidth, LiplisWindowStack windowPos)
        {
            //スキン取得
            this.lips = lips;
            this.skin = skin;
            this.setting = setting;
            this.windowPos = windowPos;

            InitializeComponent();

            //ウインドウ設定
            initWindow(lpsTop, lpsLeft, lpsWidth,27,17);
        }

        /// <summary>
        /// ウインドウの初期化
        /// </summary>
        protected virtual void initWindow(double lpsTop, double lpsLeft, double lpsWidth, int talkWindowHeightMargin, int talkLabelHeightMargin)
        {
            //ウインドウをセットする
            this.setWindow();

            //初期化しておく
            //this.updateText("");

            //非アクティブ
            this.ShowActivated = false; 

            //オパシティ0にしておく
            this.image.Opacity = 0;

            //トークラベルマージン
            this.TALK_WINDOW_HEIGHT_MARGIN = talkWindowHeightMargin;
            this.TALK_LABEL_HEIGHT_MARGIN = talkLabelHeightMargin;

            //初期セット
            this.WindowStartupLocation  = WindowStartupLocation.Manual;
            this.txbLpsTalkLabel.Text   = "";
            this.Height                 = nowTxbLpsTalkLabelHeight + TALK_WINDOW_HEIGHT_MARGIN;
            this.imageGrid.Height       = nowTxbLpsTalkLabelHeight + TALK_WINDOW_HEIGHT_MARGIN;
            this.image.Height           = nowTxbLpsTalkLabelHeight + TALK_WINDOW_HEIGHT_MARGIN;
            this.lblLpsTalkLabel.Height = nowTxbLpsTalkLabelHeight + TALK_LABEL_HEIGHT_MARGIN;

            //ボタンの設定
            this.btnNext.Visibility              = Visibility.Collapsed;
            this.btnCopyUrl.Visibility           = Visibility.Collapsed;
            this.btnWeb.Visibility               = Visibility.Collapsed;
            this.btnTweet.Visibility             = Visibility.Collapsed;
            this.prgEveryOneTalkGage.Visibility  = Visibility.Collapsed;
            this.lblEveryOneTalkCount.Visibility = Visibility.Collapsed;

            this.prgEveryOneTalkGage.Maximum = 100;
            this.prgEveryOneTalkGage.Minimum = 0;
            this.prgEveryOneTalkGage.Value = 0;
            this.lblEveryOneTalkCount.Content = "";


            //ウインドウ位置設定
            _setWindowLocation((Int32)lpsTop, (Int32)lpsLeft, (Int32)lpsWidth);
        }

        /// <summary>
        /// ウインドウロード時
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            WpfAnimation.opacityUp(this, this.image, 10000000);
        }


        /// <summary>
        /// ウインドウを終了する
        /// </summary>
        public virtual void endWindow()
        {
            Dispatcher.Invoke(new Action(() =>
            {
                //オパシティチェンジのストーリボード取得
                Storyboard sb = WpfAnimation.opacityDownStoryboard(this);

                //終了時イベント設定
                sb.Completed += (s, e) => { this.Close(); };

                //アニメーション開始
                sb.Begin();
            }));
        }


        /// <summary>
        /// アイコンロケーションを設定する
        /// </summary>
        public virtual void setIconLocation(Control btn)
        {
            btn.Margin = getCtrlLocation(btn, BTN_LOCATION_MARGINE);
        }
        
        /// <summary>
        /// コントロールのロケーションを設定する
        /// </summary>
        /// <param name="ctrl"></param>
        /// <param name="margine"></param>
        public virtual void setCtrlLocation(Control ctrl, int margine)
        {
            ctrl.Margin = getCtrlLocation(ctrl, margine);
        }

        /// <summary>
        /// 対象コントロールのロケーションを取得する
        /// </summary>
        /// <param name="ctrl"></param>
        /// <param name="margine"></param>
        /// <returns></returns>
        public virtual Thickness getCtrlLocation(Control ctrl, int margine)
        {
            return new Thickness(ctrl.Margin.Left, this.Height - margine, 0, 0);
        }
        #endregion


        //============================================================
        //
        //イベントハンドラ
        //
        //============================================================
        #region イベントハンドラ
        /// <summary>
        /// ドラッグムーブ
        /// </summary>
        /// <param name="e"></param>
        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonDown(e);
            DragMove();
        }

        /// <summary>
        /// サイズチェンジイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            this.image.Width = this.Width;
            this.image.Height = this.Height;
        }

        /// <summary>
        /// 次へボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected virtual void btnNext_Click(object sender, RoutedEventArgs e)
        {
            lips.onNextClick();
        }

        /// <summary>
        /// 子クラスで定義
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected virtual void btnCopyUrl_Click(object sender, RoutedEventArgs e)
        {

        }

        /// <summary>
        /// 子クラスで定義
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected virtual void btnWeb_Click(object sender, RoutedEventArgs e)
        {

        }

        /// <summary>
        /// ツイッターツイート
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected virtual void btnTweet_Click(object sender, RoutedEventArgs e)
        {
            
        }
        #endregion

        //============================================================
        //
        //リプリスインターフェース
        //
        //============================================================
        #region リプリスインターフェース
        /// <summary>
        /// ウインドウのロケーション設定
        /// </summary>
        public void setWindowLocation(Int32 lpsTop, Int32 lpsLeft, Int32 lpsWidth)
        {            
            Dispatcher.Invoke(new Action(() =>
            {
                _setWindowLocation(lpsTop, lpsLeft, lpsWidth);
            }));
        }
        public void _setWindowLocation(Int32 lpsTop, Int32 lpsLeft, Int32 lpsWIdth)
        {
            //中心位置計算
            Int32 locationCenter = lpsLeft + lpsWIdth / 2;
            //場所確定
            this.Left = locationCenter - (Int32)this.Width / 2; //レフト位置
            this.Top = lpsTop - (Int32)this.Height + WIDGET_WINDOW_INTERVAL;//トップ位置
        }

        /// <summary>
        /// テキストの更新
        /// </summary>
        /// <param name="liplisChatText"></param>
        public virtual void  updateText(string liplisChatText)
        {
            Dispatcher.BeginInvoke(new Action(() =>
            {
                //テキストセット
                this.txbLpsTalkLabel.Text = liplisChatText;

                //現在高さ取得
                nowTxbLpsTalkLabelHeight = (Int32)txbLpsTalkLabel.ActualHeight;

                //現在高さが変化していたら、アニメーションで高さ拡張
                if (prvTxbLpsTalkLabelHeight != nowTxbLpsTalkLabelHeight)
                {
                    this.UpdateLayout();
                    sizeChanveAnimation();
                }

                //前回値設定
                prvTxbLpsTalkLabelHeight = nowTxbLpsTalkLabelHeight;
            }));
        }

        /// <summary>
        /// スキップ表示
        /// </summary>
        /// <param name="liplisChatText"></param>
        public virtual void updateSkip(string liplisChatText)
        {
            if(liplisChatText == "")
            {
                Console.WriteLine("");
            }

            Dispatcher.Invoke(new Action(() =>
            {
                //テキストセット
                this.txbLpsTalkLabel.Text = liplisChatText;
                this.UpdateLayout();

                //現在高さ取得
                nowTxbLpsTalkLabelHeight = (Int32)txbLpsTalkLabel.ActualHeight;
                sizeChanveAnimation();

                this.UpdateLayout();
            }));
        }

        /// <summary>
        ///高さをアニメーションで変化させる
        /// </summary>
        public void sizeChanveAnimation()
        {
            this.Height = nowTxbLpsTalkLabelHeight + TALK_WINDOW_HEIGHT_MARGIN;
            this.imageGrid.Height = nowTxbLpsTalkLabelHeight + TALK_WINDOW_HEIGHT_MARGIN;
            WpfAnimation.windowHeightChange(this, this.image, this.image.Height, nowTxbLpsTalkLabelHeight + TALK_WINDOW_HEIGHT_MARGIN);
            this.lblLpsTalkLabel.Height = nowTxbLpsTalkLabelHeight + TALK_LABEL_HEIGHT_MARGIN;

            //アイコンロケーションを設定する
            setIconLocation(this.btnNext);
            setIconLocation(this.btnCopyUrl);
            setIconLocation(this.btnWeb);
            setIconLocation(this.btnTweet);
            setCtrlLocation(this.prgEveryOneTalkGage, PRG_LOCATION_MARGINE);
            setCtrlLocation(this.lblEveryOneTalkCount, LBL_LOCATION_MARGINE);
        }

        /// <summary>
        /// ランダムにウインドウを移動する
        /// </summary>
        public void windowMoveRandam(double lpsTop, double lpsLeft, double lpsWidth, double lpsHeight)
        {
            int movePointX_Min = (int)(lpsLeft - (lpsWidth / 2));
            int movePointX_Max = (int)(lpsLeft + (lpsWidth * 1.5) - this.Width);

            int movePointY_Min = (int)(lpsTop + (lpsHeight / 2));
            int movePointY_Max = (int)(lpsTop + lpsHeight * 3 / 4);


            int movePointX = LpsLiplisUtil.getRandamInt(movePointX_Min, movePointX_Max);
            int movePointY = LpsLiplisUtil.getRandamInt(movePointY_Min, movePointY_Max);

            //アニメーション移動
            this.windowMove(movePointX, movePointY, windowPos);
        }

        /// <summary>
        /// ウインドウを移動する
        /// </summary>
        public virtual void windowMove(double movePointX, double movePointY, LiplisWindowStack windowPos)
        {
            this.windowPos = windowPos;
            //アニメーション移動
            WpfAnimation.windowMove(this, movePointX, movePointY);
        }
        public virtual void windowMove(LiplisWindowStack windowPos)
        {
            this.windowPos = windowPos;
            //アニメーション移動
            WpfAnimation.windowMove(this, this.LocationX, this.LocationY);
        }


        /// <summary>
        /// ウインドウをセットする
        /// TODO: 設定画面でウインドウを変更した場合、このメソッドを呼ぶ
        /// </summary>
        public void setWindow()
        {
            Dispatcher.Invoke(new Action(() =>
            {
                this.image.Source = new BitmapImage(new Uri(this.skin.xmlWindow.getWindowPath(setting.lpsWindow)));
            }));
        }

        /// <summary>
        /// プログレスをセットする
        /// </summary>
        public void setProgress(int val, int max)
        {
            try
            {
                //閉じる
                Dispatcher.Invoke(new Action(() =>
                {
                    this.prgEveryOneTalkGage.Minimum = 0;
                    this.lblEveryOneTalkCount.Content = val + "/" + max;
                    this.prgEveryOneTalkGage.Maximum = max;
                    this.prgEveryOneTalkGage.Value = val;
                }));
            }
            catch
            {
                this.prgEveryOneTalkGage.Maximum = 100;
                this.prgEveryOneTalkGage.Minimum = 0;
                this.prgEveryOneTalkGage.Value = 0;
                this.lblEveryOneTalkCount.Content = val + "/" + max;
            }
        }
        #endregion



        //============================================================
        //
        //バックグラウンド処理
        //
        //============================================================
        #region バックグラウンド処理
        public void tweet(string sentence)
        {
            //ツイッター登録チェック
            if (lips.desk.baseSetting.lpsTwitterActivate != 1)
            {
                LpsMessage.showError("ツイッター登録されていません。" + Environment.NewLine + "アカウントを登録してから実行して下さい。");
                return;
            }

            this.workerTweet = new System.ComponentModel.BackgroundWorker();
            this.workerTweet.DoWork += (s, e) => tweetAsync(sentence);
            this.workerTweet.RunWorkerAsync();
        }

        /// <summary>
        /// 非同期処理実行
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="sentence"></param>
        private void tweetAsync(string sentence)
        {
            ClalisForLiplis.tweet(lips.desk.baseSetting.uid, sentence);
        }
        #endregion
    }



}
