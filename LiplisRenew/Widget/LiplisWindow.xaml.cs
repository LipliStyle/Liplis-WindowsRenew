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
using Liplis.MainSystem;
using Liplis.Wpf;
using System;
using System.Windows;
using System.Windows.Input;
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

        private void initWindow()
        {
            setWindow();
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
            //ボディ設定
            Dispatcher.Invoke(new Action(() =>
            {
                //テキストセット
                this.txbLpsTalkLabel.Text   = liplisChatText;

                //現在高さ取得
                nowTxbLpsTalkLabelHeight = (Int32)txbLpsTalkLabel.ActualHeight;

                //現在高さが変化していたら、アニメーションで高さ拡張
                if (prvTxbLpsTalkLabelHeight != nowTxbLpsTalkLabelHeight)
                {
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
