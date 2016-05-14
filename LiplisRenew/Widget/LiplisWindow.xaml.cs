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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Liplis.Widget
{
    /// <summary>
    /// XsamlWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class LiplisWindow : Window
    {
        //=================================
        //リプリスウィジェットとウインドウのデフォルトインターバル
        private const Int32 WIDGET_WINDOW_INTERVAL = 5;

        //============================================================
        //
        //初期化処理
        //
        //============================================================
        #region 初期化処理

        /// <summary>
        /// コンストラクター
        /// </summary>
        public LiplisWindow()
        {
            InitializeComponent();
        }

        private void initWindow()
        {

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
                this.txbLpsTalkLabel.Text   = liplisChatText;
                this.lblLpsTalkLabel.Height = (Int32)txbLpsTalkLabel.ActualHeight + 10;
                this.image.Height           = (Int32)lblLpsTalkLabel.ActualHeight + 20;
                this.imageGrid.Height       = (Int32)lblLpsTalkLabel.ActualHeight + 20;
                this.Height                 = (Int32)lblLpsTalkLabel.ActualHeight + 20;
            }));
        }

        #endregion

    }



}
