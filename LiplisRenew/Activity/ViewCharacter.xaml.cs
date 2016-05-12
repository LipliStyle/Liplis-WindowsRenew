//=======================================================================
//  ClassName : ViewCharacter
//  概要      : キャラクター選択画面
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
using Liplis.Widget;
using Liplis.Wpf;
using System;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Liplis.Activity
{
    /// <summary>
    /// ViewCharacter.xaml の相互作用ロジック
    /// </summary>
    public partial class ViewCharacter : Window
    {
        //=================================
        //デスクトップインスタンス
        private ViewDeskTop desktop;

        //=================================
        //選択中スキン
        private Skin selectedSkin;

        //=================================
        //タイマー
        Timer timerBtnLeft = new Timer();
        Timer timerBtnRight = new Timer();

        ///====================================================================
        ///
        ///                             初期化処理
        ///                         
        ///====================================================================
        #region 初期化処理
        /// コンストラクター
        /// </summary>
        /// <param name="desktop"></param>
        public ViewCharacter(ViewDeskTop desktop)
        {
            //デスクトップインスタンス取得
            this.desktop = desktop;

            //画面の初期化
            InitializeComponent();

            //キャラクターリストの初期化
            initCharList();

            //タイマーの初期化
            initTimer();
        }

        /// <summary>
        /// タイマーの初期化
        /// </summary>
        private void initTimer()
        {
            timerBtnLeft  = new Timer();
            timerBtnRight = new Timer();

            timerBtnLeft.Interval = 10;
            timerBtnRight.Interval = 10;
            timerBtnLeft.Elapsed += new ElapsedEventHandler(OnElapsed_timerBtnLeft);
            timerBtnRight.Elapsed += new ElapsedEventHandler(OnElapsed_timerBtnRight);
        }

        /// <summary>
        /// キャラクターリストの初期化
        /// </summary>
        private void initCharList()
        {
            //スキンコントローラーの取得
            SkinController sc = this.desktop.sc;

            foreach(Skin skin in sc.lstSkin)
            {
                //グリッド追加
                this.dpChar.Children.Add(createCharGrid(skin));
            }

            //幅設定
            dpChar.Width = sc.lstSkin.Count * 100;

            //初期選択
            selectSkin(sc.lstSkin[0]);
        }

        /// <summary>
        /// グリッド作成
        /// </summary>
        /// <param name="skin"></param>
        /// <returns></returns>
        private Grid createCharGrid(Skin skin)
        {
            //グリッドの作成
            Grid grid = new Grid();

            grid.Height = 335;
            grid.Width = 100;
            grid.VerticalAlignment = VerticalAlignment.Top;

            //要素の追加
            grid.Children.Add(createCharNameLabel(skin));
            grid.Children.Add(createCharImage(skin));

            //作成したグリッドを返す
            return grid;
        }

        /// <summary>
        /// キャラクター名ラベルを作成する
        /// </summary>
        /// <returns></returns>
        private Label createCharNameLabel(Skin skin)
        {
            Label lb = new Label();

            lb.Content = skin.charName;

            lb.Name = "Lbl" + skin.charName;
            lb.HorizontalAlignment = HorizontalAlignment.Left;
            lb.VerticalAlignment = VerticalAlignment.Top;
            lb.Width = 100;
            lb.Height = 25;
            lb.Foreground = new SolidColorBrush(Color.FromRgb(214,182,36));
            lb.MouseDown += Lb_MouseDown;
            lb.Tag = skin;

            return lb;
        }

        /// <summary>
        /// ラベルマウスダウン時イベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Lb_MouseDown(object sender, MouseButtonEventArgs e)
        {
            //ラベルを取得
            Label im = (Label)sender;

            //タグにセットされたスキンを復元し、渡す
            selectSkin((Skin)im.Tag);
        }

        /// <summary>
        /// キャラクターイメージラベルを作成する
        /// </summary>
        /// <returns></returns>
        private Image createCharImage(Skin skin)
        {
            //ピクチャーのパス取得
            string picPath = skin.xmlBody.getLiplisBody(0, 0).getBodyPath11();

            //イメージ生成
            Image im               = new Image();
            im.Source              = createCharBitmapIamge(picPath);//クリッピングイメージをセット
            im.Stretch             = Stretch.UniformToFill;         //調整モード
            im.HorizontalAlignment = HorizontalAlignment.Left;      //レフト寄せ
            im.Height              = 300;                           //高さ:300
            im.Width               = 100;                           //幅:100
            im.Margin              = new Thickness(0, 25, 0, 0);    //オフセット位置
            im.VerticalAlignment   = VerticalAlignment.Top;         //位置
            im.Tag                 = skin;                          //スキンをセットしておく
            im.MouseDown          += Im_MouseDown;

            //作成したイメージを返す
            return im;
        }

        /// <summary>
        /// マウスダウン時イベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Im_MouseDown(object sender, MouseButtonEventArgs e)
        {
            //イメージを取得
            Image im = (Image)sender;

            //タグにセットされたスキンを復元し、渡す
            selectSkin((Skin)im.Tag);
        }

        /// <summary>
        /// キャラクターのカットインイメージを作成する
        /// </summary>
        /// <param name="picPath"></param>
        /// <returns></returns>
        private ImageSource createCharBitmapIamge(string picPath)
        {
            //ビットマップイメージ生成
            BitmapImage bmpImage = new BitmapImage(new Uri(picPath));

            //切り抜き
            CroppedBitmap clippingImage = new CroppedBitmap(bmpImage, new Int32Rect((int)bmpImage.Width / 2 - 50, 0, 100, 300));

            //差k性イメージを返す
            return clippingImage;
        }
        #endregion

        ///====================================================================
        ///
        ///                              イベントハンドラ
        ///                         
        ///====================================================================
        #region イベントハンドラ
        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            this.btnRight.Margin = new Thickness(this.Width-38, 27, 0, 0);
        }
        #endregion

        ///====================================================================
        ///
        ///                              処理
        ///                         
        ///====================================================================
        #region 処理
        /// <summary>
        /// スキン選択
        /// </summary>
        /// <param name="skin"></param>
        private void selectSkin(Skin skin)
        {
            //選択スキン取得
            selectedSkin = skin;

            //パス取得
            string picPath = skin.xmlBody.getLiplisBody(0, 0).getBodyPath11();

            //画像セット
            WpfAnimation.opacityDown(this, imgChar);
            imgChar.Source = new BitmapImage(new Uri(picPath));
            WpfAnimation.opacityUp(this, imgChar);

            //ラベルセット
            lblCharName.Content = skin.xmlSkin.charName;

            //紹介文章セット
            lblCharIntroduction.Content = skin.xmlSkin.charIntroduction;
        }

        private void skinSelectAnimation()
        {

        }
        #endregion



        ///====================================================================
        ///
        ///                          マウス置き移動制御
        ///                         
        ///====================================================================
        #region 処理
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnLeft_MouseEnter(object sender, MouseEventArgs e)
        {
            //ふんわり出現のアニメーションを取得する
            Storyboard storyboard = WpfAnimation.opacityUpStoryboard(btnLeft,0.6);

            //ふんわり出現完了時、タイマーを起動させる
            storyboard.Completed += (s, et) => {
                if (btnLeft.Opacity >= 0.55) {
                    timerBtnLeft.Start();
                } };

            ////アニメーション開始
            storyboard.Begin(this);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnLeft_MouseLeave(object sender, MouseEventArgs e)
        {
            WpfAnimation.opacityDown(this, btnLeft);

            timerBtnLeft.Stop();
            
        }
        private void OnElapsed_timerBtnLeft(object sender, ElapsedEventArgs e)
        {
            Dispatcher.Invoke(new Action(() =>
            {
                if (this.sv.HorizontalOffset > 0)
                {
                    try
                    {
                        this.sv.ScrollToHorizontalOffset(this.sv.HorizontalOffset - 10);

                    }
                    catch
                    {
                    }
                }
            }));
        }
        private void btnRight_MouseEnter(object sender, MouseEventArgs e)
        {
            Storyboard storyboard = WpfAnimation.opacityUpStoryboard(btnRight,0.6);

            storyboard.Completed += (s, et) => {
                if (btnRight.Opacity >= 0.55)
                {
                    timerBtnRight.Start();
                }
            };

            ////アニメーション開始
            storyboard.Begin(this);
        }
        private void btnRight_MouseLeave(object sender, MouseEventArgs e)
        {
            WpfAnimation.opacityDown(this, btnRight);

            timerBtnRight.Stop();
        }
        private void OnElapsed_timerBtnRight(object sender, ElapsedEventArgs e)
        {
            Dispatcher.Invoke(new Action(() =>
            {
                if (this.sv.HorizontalOffset < this.sv.ScrollableWidth)
                {
                    try
                    {
                        this.sv.ScrollToHorizontalOffset(this.sv.HorizontalOffset + 10);

                    }
                    catch
                    {
                    }
                }
            }));
        }
        #endregion

        private void btnCharSelect_Click(object sender, RoutedEventArgs e)
        {
            if(selectedSkin != null)
            {
                desktop.addWidget(selectedSkin);
            }
            
            //LiplisWidget liplis = new LiplisWidget();
            //LiplisWindow window = new LiplisWindow();

            //liplis.Show();
            //window.Show();

        }
    }
}
