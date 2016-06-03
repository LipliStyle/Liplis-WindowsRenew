//=======================================================================
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
                //----------------------------------------------------------------------------------------
                DockPanel dp = new DockPanel();
                dp.Height = 100;
                dp.LastChildFill = false;
                dp.Background = new SolidColorBrush(Color.FromRgb(31, 31, 31));

                //----------------------------------------------------------------------------------------
                Image talkImage = new Image();
                talkImage.Height = 100;
                talkImage.Width = (skin.xmlBody.height / 2);
                talkImage.Stretch = Stretch.UniformToFill;


                //クリッピング
                //1. 頭は半分以上上にある(可能性が高いので) 半分にする
                double herfHeight = 100;
                double herfWidth = (skin.xmlBody.height / skin.xmlBody.width) * 100;
                int offsetX = 0;
                int offsetY = 0;

                //上半分でクリップする
                CroppedBitmap cb = new CroppedBitmap(new BitmapImage(new Uri(skin.xmlBody.getLiplisBody(log.newsEmotion, log.newsPoint).getBodyPath(0, 0))),
                                   new Int32Rect(offsetX, offsetY, skin.xmlBody.width, skin.xmlBody.height / 2));       //select region rect


                talkImage.Source = cb;
                DockPanel.SetDock(talkImage, Dock.Left);
                //----------------------------------------------------------------------------------------
                Grid innerGrid = new Grid();

                //表示テキストブロック生成(ラベルのコンテント)
                TextBlock tb = new TextBlock();
                tb.TextWrapping = TextWrapping.Wrap;
                tb.Text = log.result;

                //バックグラウンドウインドウ生成
                Image windowBagkGround = new Image();
                windowBagkGround.Height = 50;
                //windowBagkGround.Width = 393;
                windowBagkGround.Margin = new Thickness(0, 0, 0, 0);
                windowBagkGround.VerticalAlignment = VerticalAlignment.Top;
                windowBagkGround.Stretch = Stretch.Fill;
                windowBagkGround.Source = new BitmapImage(new Uri(skin.xmlWindow.getWindowPath(setting.lpsWindow)));

                //表示ラベル生成
                Label talkLog = new Label();
                talkLog.HorizontalAlignment = HorizontalAlignment.Left;
                talkLog.VerticalAlignment = VerticalAlignment.Top;
                talkLog.Margin = new Thickness(2, 2, 0, 0);
                talkLog.Height = 46;
                //talkLog.Width = 388;
                talkLog.Foreground = new SolidColorBrush(Color.FromRgb(0, 0, 0));
                talkLog.Content = tb;

                //グリッド要素に追加
                innerGrid.Children.Add(windowBagkGround);
                innerGrid.Children.Add(talkLog);
                //----------------------------------------------------------------------------------------
                Grid rightGrid = new Grid();
                rightGrid.Width = 50;
                rightGrid.Height = 100;

                Label l = new Label();
                l.Content = "00,00";
                l.Height = 25;
                l.Width = 50;
                l.Margin = new Thickness(0, 75, 0, 0);
                l.VerticalAlignment = VerticalAlignment.Center;
                l.HorizontalAlignment = HorizontalAlignment.Center;
                l.Foreground = Brushes.SkyBlue;
                l.Content = DateTime.Now.ToString("HH:mm");

                Button b1 = new Button();
                b1.Width = 25;
                b1.Height = 25;
                b1.Margin = new Thickness(0, -50, 0, 0);
                //b1.VerticalAlignment = VerticalAlignment.Top;
                //DockPanel.SetDock(l, Dock.Right);


                Button b2 = new Button();
                b2.Width = 25;
                b2.Height = 25;
                b2.Margin = new Thickness(0, 0, 0, 0);
                //b2.VerticalAlignment = VerticalAlignment.Top;




                rightGrid.Children.Add(b1);
                rightGrid.Children.Add(b2);
                rightGrid.Children.Add(l);


                DockPanel.SetDock(rightGrid, Dock.Right);



                //----------------------------------------------------------------------------------------
                //要素の追加
                dp.Children.Add(talkImage);
                dp.Children.Add(rightGrid);
                dp.Children.Add(innerGrid);
                
                //----------------------------------------------------------------------------------------

                logListPanel.Children.Add(dp);

                logListPanel.Height = logListPanel.Children.Count * 100;





                this.UpdateLayout();

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
        /// グリッド作成
        /// </summary>
        /// <param name="skin"></param>
        /// <returns></returns>
        private DockPanel createCharGrid(MsgTalkMessageLog log, Skin skin, LiplisWidgetPreference setting)
        {
            DockPanel dp = new DockPanel();
            dp.Height = 100;
            dp.LastChildFill = false;
            dp.Background = new SolidColorBrush(Color.FromRgb(31, 31, 31));

            //要素の追加
            dp.Children.Add(createTalkImage(log, skin));
            dp.Children.Add(createTalkLogGrid(log,skin,setting));
            dp.Children.Add(createUtilPanel());

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
            Image talkImage = new Image();
            talkImage.Height = 100;
            talkImage.Width = (skin.xmlBody.height / skin.xmlBody.width) * 100;


            //クリッピング
            //1. 頭は半分以上上にある(可能性が高いので) 半分にする
            double herfHeight = 100;
            double herfWidth = (skin.xmlBody.height / skin.xmlBody.width) * 100;
            int offsetX = 0;
            int offsetY = 0;

            //2. 上半分を持ってくる

            //3. 正方形にしにいく。このとき、幅と高さのパターンが3パターン考えられる
            
            //if (skin.xmlBody.width > herfHeight)
            //{
            //    //3.1 幅>算出高さ(順当に行けばこのパターンになるはず)
            //    offsetX = herfWidth - (herfHeight / 2);
            //}
            //else if (skin.xmlBody.width == herfHeight)
            //{
            //    //3.2 幅=算出高さ
            //    //加工必要なし
            //}
            //else
            //{
            //    //3.3 幅<算出高さ(ほぼ無いと考えている)
            //    offsetY = (herfHeight / 2) - herfWidth;
            //}


            //上半分でクリップする
            CroppedBitmap cb = new CroppedBitmap(new BitmapImage(new Uri(skin.xmlBody.getLiplisBody(log.newsEmotion, log.newsPoint).getBodyPath(0, 0))),
   　　　　　　　　　　　　　　new Int32Rect(offsetX, offsetY, skin.xmlBody.width, skin.xmlBody.height/2));       //select region rect


            talkImage.Source = cb;
            DockPanel.SetDock(talkImage, Dock.Left);

            return talkImage;
        }

        private Grid createTalkLogGrid(MsgTalkMessageLog log, Skin skin, LiplisWidgetPreference setting)
        {
            //グリッドの作成
            Grid innerGrid = new Grid();

            //表示テキストブロック生成(ラベルのコンテント)
            TextBlock tb = new TextBlock();
            tb.TextWrapping = TextWrapping.Wrap;
            tb.Text = log.result;

            //バックグラウンドウインドウ生成
            Image windowBagkGround = new Image();
            windowBagkGround.Height = 50;
            //windowBagkGround.Width = 393;
            windowBagkGround.Margin = new Thickness(0, 0, 0, 0);
            windowBagkGround.VerticalAlignment = VerticalAlignment.Top;
            windowBagkGround.Stretch = Stretch.Fill;
            windowBagkGround.Source = new BitmapImage(new Uri(skin.xmlWindow.getWindowPath(setting.lpsWindow)));

            //表示ラベル生成
            Label talkLog = new Label();
            talkLog.HorizontalAlignment = HorizontalAlignment.Left;
            talkLog.VerticalAlignment = VerticalAlignment.Top;
            talkLog.Margin = new Thickness(2, 2, 0, 0);
            talkLog.Height = 46;
            //talkLog.Width = 388;
            talkLog.Foreground = new SolidColorBrush(Color.FromRgb(0, 0, 0));
            talkLog.Content = tb;

            //グリッド要素に追加
            innerGrid.Children.Add(windowBagkGround);
            innerGrid.Children.Add(talkLog);

            return innerGrid;
        }

        private DockPanel createUtilPanel()
        {
            DockPanel dp = new DockPanel();
            dp.Width = 100;
            DockPanel.SetDock(dp, Dock.Right);
            return dp;
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
        #endregion
    }
}
