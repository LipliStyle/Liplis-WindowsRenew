﻿//=======================================================================
//  ClassName : ViewLiplisLog
//  概要      : ログ画面
//
// iOS版と同等
//  デザインは一新
//
//Liplis5.0
//
//アップデート履歴
//   2016/05/08 ver5.0.0 作成
//
//  Copyright(c) 2010-2016 LipliStyle.Sachin
//=======================================================================
using Liplis.MainSystem;
using Liplis.Msg;
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

namespace Liplis.Activity
{
    /// <summary>
    /// ViewLiplisLog.xaml の相互作用ロジック
    /// </summary>
    public partial class ViewLiplisLog : Window
    {

        //=================================
        //デスクトップインスタンス
        private ViewDeskTop desktop;

        //=================================
        //エンドフラグ
        private bool flgEnd = false;

        /// <summary>
        /// コンストラクター
        /// </summary>
        /// <param name="desktop"></param>
        public ViewLiplisLog(ViewDeskTop desktop)
        {
            this.desktop = desktop;

            InitializeComponent();
        }

        /// <summary>
        /// ログビューを閉じる
        /// </summary>
        private void closeLogView()
        {
            this.flgEnd = true;
            this.Close();
        }

        /// <summary>
        /// ウインドウ閉じ禁止
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if(!flgEnd)
            {
                this.Hide();
                e.Cancel = true;
            }
        }

        /// <summary>
        /// ログの追加
        /// </summary>
        /// <param name="log"></param>
        public void addLog(MsgTalkMessageLog log, Skin skin, LiplisWidgetPreference setting)
        {
            Dispatcher.BeginInvoke(new Action(() =>
            {
                //ベースのドックパネル生成
                DockPanel dp = createBaseDockPanel();

                //トークイメージ生成
                Image talkImage = createTalkImage(log, skin);

                //インナーグリッド生成
                Grid innerGrid = new Grid();

                //表示テキストブロック生成(ラベルのコンテント)
                TextBlock tb = createTextBlock(log);

                //バックグラウンドウインドウ生成
                Image windowBagkGround = createWindowBagkGround(skin,setting);

                //表示ラベル生成
                Label talkLog = createTalkLogLabel(tb);

                //グリッド要素に追加
                innerGrid.Children.Add(windowBagkGround);
                innerGrid.Children.Add(talkLog);

                //右グリッド生成
                Grid rightGrid = createRightGrid();


                //要素の追加
                dp.Children.Add(talkImage);
                dp.Children.Add(rightGrid);
                dp.Children.Add(innerGrid);

                //子要素追加
                logListPanel.Children.Add(dp);

                ///500件以上は削除
                if (logListPanel.Children.Count > 500){logListPanel.Children.RemoveAt(0);}

                //高さ調整
                logListPanel.Height = logListPanel.Children.Count * 100;

                //レイアウト更新
                this.UpdateLayout();

                //高さ調整2 (更新順序に意味あり アクチュアルヘイトは更新してからでないと、変化しないため。)
                innerGrid.Height = tb.ActualHeight + 20;
                windowBagkGround.Height = tb.ActualHeight + 20;
                talkLog.Height = tb.ActualHeight + 16;


                //停止状態でなければ、下までスクロールする。(停止状態 = ストップボタンが有効のとき)
                if(btnStop.IsEnabled)
                {
                    //スクロールビューの最後までスクロールする
                    this.sv.ScrollToEnd();
                }
            }));
        }

        /// <summary>
        /// ベースドックパネル生成
        /// </summary>
        /// <returns></returns>
        private DockPanel createBaseDockPanel()
        {
            DockPanel dp = new DockPanel();
            dp.Height = 100;
            dp.LastChildFill = false;
            dp.Background = new SolidColorBrush(Color.FromRgb(31, 31, 31));

            return dp;
        }

        /// <summary>
        /// セリフイメージを生成する
        /// </summary>
        /// <param name="log"></param>
        /// <param name="skin"></param>
        /// <returns></returns>
        private Image createTalkImage(MsgTalkMessageLog log, Skin skin)
        {
            //トークイメージ生成
            Image talkImage = new Image();
            talkImage.Height = 100;
            talkImage.Width = (skin.xmlBody.height / 2);
            talkImage.Stretch = Stretch.UniformToFill;

            //上半分でクリップする
            CroppedBitmap cb = new CroppedBitmap(new BitmapImage(new Uri(skin.xmlBody.getLiplisBody(log.newsEmotion, log.newsPoint).getBodyPath(0, 0))),
                               new Int32Rect(0, 0, skin.xmlBody.width, skin.xmlBody.height / 2));       //select region rect

            //トークイメージソースにクリップした画像を設定
            talkImage.Source = cb;

            //ドックパネル設定
            DockPanel.SetDock(talkImage, Dock.Left);

            //トークイメージを返す
            return talkImage;
        }
        
        /// <summary>
        /// テキストブロック生成
        /// </summary>
        /// <returns></returns>
        private TextBlock createTextBlock(MsgTalkMessageLog log)
        {
            //テキストブロック生成
            TextBlock tb = new TextBlock();
            tb.TextWrapping = TextWrapping.Wrap;
            tb.Text = log.result;

            //テキストブロックを返す
            return tb;
        }

        /// <summary>
        /// 背景画像設定
        /// </summary>
        /// <returns></returns>
        private Image createWindowBagkGround(Skin skin, LiplisWidgetPreference setting)
        {
            Image windowBagkGround = new Image();
            windowBagkGround.Height = 50;
            windowBagkGround.Margin = new Thickness(0, 0, 0, 0);
            windowBagkGround.VerticalAlignment = VerticalAlignment.Top;
            windowBagkGround.Stretch = Stretch.Fill;
            windowBagkGround.Source = new BitmapImage(new Uri(skin.xmlWindow.getWindowPath(setting.lpsWindow)));
            return windowBagkGround;
        }

        /// <summary>
        /// トークログラベルを生成する
        /// </summary>
        /// <param name="tb"></param>
        /// <returns></returns>
        private Label createTalkLogLabel(TextBlock tb)
        {
            Label talkLog = new Label();
            talkLog.HorizontalAlignment = HorizontalAlignment.Left;
            talkLog.VerticalAlignment = VerticalAlignment.Top;
            talkLog.Margin = new Thickness(2, 2, 0, 0);
            talkLog.Height = 46;
            talkLog.Foreground = new SolidColorBrush(Color.FromRgb(0, 0, 0));
            talkLog.Content = tb;

            return talkLog;
        }

        /// <summary>
        /// 右グリッドを生成する
        /// </summary>
        /// <returns></returns>
        private Grid createRightGrid()
        {
            //右側グリッド生成
            Grid rightGrid = new Grid();
            rightGrid.Width = 50;
            rightGrid.Height = 100;

            //時刻ラベル生成
            Label clockLabel = new Label();
            clockLabel.Content = "00,00";
            clockLabel.Height = 25;
            clockLabel.Width = 50;
            clockLabel.Margin = new Thickness(0, 75, 0, 0);
            clockLabel.VerticalAlignment = VerticalAlignment.Center;
            clockLabel.HorizontalAlignment = HorizontalAlignment.Center;
            clockLabel.Foreground = Brushes.SkyBlue;
            clockLabel.Content = DateTime.Now.ToString("HH:mm");

            //ボタン1生成
            Button b1 = new Button();
            b1.Width = 25;
            b1.Height = 25;
            b1.Margin = new Thickness(0, -50, 0, 0);

            //ボタン2生成
            Button b2 = new Button();
            b2.Width = 25;
            b2.Height = 25;
            b2.Margin = new Thickness(0, 0, 0, 0);

            //要素の追加
            rightGrid.Children.Add(b1);
            rightGrid.Children.Add(b2);
            rightGrid.Children.Add(clockLabel);

            //ドックパネル設定
            DockPanel.SetDock(rightGrid, Dock.Right);

            //右グリッドを返す
            return rightGrid;
        }

        //============================================================
        //
        //イベントハンドラ
        //
        //============================================================
        #region イベントハンドラ

        /// <summary>
        /// ログストップボタン
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnStop_Click(object sender, RoutedEventArgs e)
        {
            btnStop.IsEnabled = false;
            btnStart.IsEnabled = true;
            sv.ScrollToVerticalOffset(logListPanel.Height - 3);
        }

        /// <summary>
        /// ログ再開ボタン
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnStart_Click(object sender, RoutedEventArgs e)
        {
            btnStop.IsEnabled = true;
            btnStart.IsEnabled = false;
            sv.ScrollToEnd();
        }

        /// <summary>
        /// 検索ボタン押下
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_Click(object sender, RoutedEventArgs e)
        {

        }
        #endregion


    }
}
