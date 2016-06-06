//=======================================================================
//  ClassName : LiplisNewsPicture
//  概要      : リプリスピクチャー
//
//Liplis5.0
//
//アップデート履歴
//   2016/05/21 ver5.0.0 作成
//
//  Copyright(c) 2010-2016 LipliStyle.Sachin
//=======================================================================
using Liplis.Wpf;
using System;
using System.Windows;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;

namespace Liplis.Widget.LpsWindow
{
    /// <summary>
    /// LiplisNewsPicture.xaml の相互作用ロジック
    /// </summary>
    public partial class LiplisNewsPicture : Window
    {
        /// <summary>
        /// コンストラクター
        /// </summary>
        /// <param name="jpgUri"></param>
        public LiplisNewsPicture(Uri jpgUri, double left, double top)
        {
            //初期化
            InitializeComponent();

            //イメージの設定
            setImage(jpgUri);

            //ウインドウの初期化
            initWindow(top, left);
        }

        /// <summary>
        /// ウインドウの初期化
        /// </summary>
        private void initWindow(double top, double left)
        {
            //表示位置マニュアル設定
            this.WindowStartupLocation = WindowStartupLocation.Manual;

            //表示座標設定
            this.Top = top;
            this.Left = left;
        }

        /// <summary>
        /// イメージのセット
        /// </summary>
        private void setImage(Uri jpgUri)
        {
            //イメージソース生成(URI指定 非同期でダウンロードされる)
            BitmapImage imageSource = new BitmapImage(jpgUri);

            //イメージソース設定
            this.image.Source = imageSource;

            //ダウンロード時イベント
            imageSource.DownloadCompleted += new EventHandler((Object sender, EventArgs e) =>
            {
                this.Width = imageSource.PixelWidth;
                this.Height = imageSource.PixelHeight;
            });
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

        /// <summary>
        /// ウインドウの移動
        /// </summary>
        public virtual void windowMove(int LocationX, int LocationY)
        {
            //アニメーション移動
            WpfAnimation.windowMove(this, LocationX, LocationY);
        }

    }
}
