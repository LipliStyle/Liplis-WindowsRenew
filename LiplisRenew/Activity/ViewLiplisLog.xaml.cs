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
        public void addLog(MsgTalkMessageLog log)
        {
            Dispatcher.BeginInvoke(new Action(() =>
            {
                logListPanel.Children.Add(createCharGrid(log));

                logListPanel.Height = logListPanel.Children.Count * 52;
            }));
        }


        /// <summary>
        /// グリッド作成
        /// </summary>
        /// <param name="skin"></param>
        /// <returns></returns>
        private Grid createCharGrid(MsgTalkMessageLog log)
        {
            //グリッドの作成
            Grid grid = new Grid();

            grid.Height = 52;
            grid.Width = 100;
            grid.VerticalAlignment = VerticalAlignment.Top;


            DockPanel dp = new DockPanel();
            dp.Height = 50;
            dp.LastChildFill = false;
            dp.Background = new SolidColorBrush(Color.FromRgb(31, 31, 31));

            //要素の追加
            dp.Children.Add(createTalkImage());
            dp.Children.Add(createTalkLogGrid(log));
            dp.Children.Add(createUtilPanel());

            //ドックパネルの追加
            grid.Children.Add(dp);

            //作成したグリッドを返す
            return grid;
        }

        private Image createTalkImage()
        {
            Image talkImage = new Image();

            return talkImage;
        }

        private Grid createTalkLogGrid(MsgTalkMessageLog log)
        {
            //グリッドの作成
            Grid innerGrid = new Grid();

            Image windowBagkGround = new Image();
            Label talkLog = new Label();

            windowBagkGround.Height = 50;
            windowBagkGround.Width = 393;
            windowBagkGround.VerticalAlignment = VerticalAlignment.Top;

            talkLog.HorizontalAlignment = HorizontalAlignment.Left;
            talkLog.VerticalAlignment = VerticalAlignment.Top;
            talkLog.Margin = new Thickness(2, 2, 0, 0);
            talkLog.Height = 46;
            talkLog.Width = 388;
            talkLog.Foreground = new SolidColorBrush(Color.FromRgb(97, 193, 0));
            talkLog.Content = log.result;

            innerGrid.Children.Add(windowBagkGround);
            innerGrid.Children.Add(talkLog);

            return innerGrid;
        }

        private DockPanel createUtilPanel()
        {
            DockPanel dp = new DockPanel();

            return dp;
        }

    }
}
