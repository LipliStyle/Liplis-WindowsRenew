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
        public void setWindowLocation(Int32 widgetTop, Int32 widgetWidth)
        {
            //中心位置計算
            Int32 locationCenter = widgetTop + widgetWidth / 2;

            //レフト位置
            Int32 left = locationCenter + (Int32)this.Width;

            //トップ位置
            Int32 top = widgetTop - (Int32)this.Height + WIDGET_WINDOW_INTERVAL;

        }
        #endregion

    }



}
