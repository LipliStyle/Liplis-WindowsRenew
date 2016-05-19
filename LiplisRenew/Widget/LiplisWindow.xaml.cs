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
using Liplis.MainSystem;
using Liplis.Wpf;
using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;

namespace Liplis.Widget
{
    /// <summary>
    /// XsamlWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class LiplisWindow : Window
    {
        //=================================
        //Liplis要素
        private LiplisWidgetPreference setting;
        private Skin skin;

        //=================================
        //ウインドウ制御プロパティ
        private Int32 nowTxbLpsTalkLabelHeight;
        private Int32 prvTxbLpsTalkLabelHeight;

        //=================================
        //リプリスウィジェットとウインドウのデフォルトインターバル
        private const Int32 WIDGET_WINDOW_INTERVAL = -10;



        //============================================================
        //
        //初期化処理
        //
        //============================================================
        #region 初期化処理

        /// <summary>
        /// コンストラクター
        /// </summary>
        public LiplisWindow(LiplisWidgetPreference setting, Skin skin)
        {
            //スキン取得
            this.skin = skin;
            this.setting = setting;

            InitializeComponent();

            //ウインドウ設定
            initWindow();
        }

        /// <summary>
        /// ウインドウの初期化
        /// </summary>
        private void initWindow()
        {
            //ウインドウをセットする
            this.setWindow();

            //初期化しておく
            //this.updateText("");

            //オパシティ0にしておく
            this.image.Opacity = 0;

            //初期セット
            this.txbLpsTalkLabel.Text = "";
            this.Height = nowTxbLpsTalkLabelHeight + 27;
            this.imageGrid.Height = nowTxbLpsTalkLabelHeight + 27;
            this.image.Height = nowTxbLpsTalkLabelHeight + 27;
            this.lblLpsTalkLabel.Height = nowTxbLpsTalkLabelHeight + 17;
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
        public void endWindow()
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
        public void setWindowLocation(Int32 widgetTop, Int32 widgetLeft, Int32 widgetWidth)
        {            
            Dispatcher.Invoke(new Action(() =>
            {
                //中心位置計算
                Int32 locationCenter = widgetLeft + widgetWidth / 2;
                //場所確定
                this.Left = locationCenter - (Int32)this.Width / 2; //レフト位置
                this.Top = widgetTop - (Int32)this.Height + WIDGET_WINDOW_INTERVAL;//トップ位置
            }));
        }

        /// <summary>
        /// テキストの更新
        /// </summary>
        /// <param name="liplisChatText"></param>
        public void  updateText(string liplisChatText)
        {
            Dispatcher.Invoke(new Action(() =>
            {
                //テキストセット
                this.txbLpsTalkLabel.Text = liplisChatText;

                //現在高さ取得
                nowTxbLpsTalkLabelHeight = (Int32)txbLpsTalkLabel.ActualHeight;

                //現在高さが変化していたら、アニメーションで高さ拡張
                if (prvTxbLpsTalkLabelHeight != nowTxbLpsTalkLabelHeight)
                {
                    this.UpdateLayout();
                    this.Height                 = nowTxbLpsTalkLabelHeight + 27;
                    this.imageGrid.Height       = nowTxbLpsTalkLabelHeight + 27;
                    WpfAnimation.windowHeightChange(this, this.image, this.image.Height, nowTxbLpsTalkLabelHeight +27);
                    this.lblLpsTalkLabel.Height = nowTxbLpsTalkLabelHeight + 17;
                }

                //前回値設定
                prvTxbLpsTalkLabelHeight = nowTxbLpsTalkLabelHeight;
            }));
        }

        /// <summary>
        /// スキップ表示
        /// </summary>
        /// <param name="liplisChatText"></param>
        public void updateSkip(string liplisChatText)
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

                //現在高さが変化していたら、アニメーションで高さ拡張
                this.Height = nowTxbLpsTalkLabelHeight + 27;
                this.imageGrid.Height = nowTxbLpsTalkLabelHeight + 27;
                WpfAnimation.windowHeightChange(this, this.image, this.image.Height, nowTxbLpsTalkLabelHeight + 27);
                this.lblLpsTalkLabel.Height = nowTxbLpsTalkLabelHeight + 17;

                this.UpdateLayout();
            }));
        }

        /// <summary>
        /// ウインドウを移動する
        /// TODO:移動ロジック要件等
        /// </summary>
        public void windowMove(double lpsTop, double lpsLeft, double lpsWidth, double lpsHeight)
        {
            int movePointX_Min = (int)(lpsLeft - (lpsWidth / 2));
            int movePointX_Max = (int)(lpsLeft + (lpsWidth * 1.5) - this.Width);

            int movePointY_Min = (int)(lpsTop + (lpsHeight / 2));
            int movePointY_Max = (int)(lpsTop + lpsHeight * 3 / 4);


            int movePointX = LpsLiplisUtil.getRandamInt(movePointX_Min, movePointX_Max);
            int movePointY = LpsLiplisUtil.getRandamInt(movePointY_Min, movePointY_Max);

            //アニメーション移動
            WpfAnimation.windowMove(this, movePointX, movePointY);
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

        #endregion


    }



}
