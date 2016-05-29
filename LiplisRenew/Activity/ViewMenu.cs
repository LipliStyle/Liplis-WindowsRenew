//=======================================================================
//  ClassName : ViewMenu
//  概要      : リプリスの各画面を呼び出すメニュー
//
//   LiplisWidget
//
//
//Liplis5.0
//
//アップデート履歴
//   2016/05/08 ver5.0.0 作成
//
//  Copyright(c) 2010-2016 LipliStyle.Sachin
//=======================================================================
using Liplis.Wpf;
using System;
using System.Windows;
using System.Windows.Forms;

namespace Liplis.Activity
{
    public partial class ViewMenu : Form
    {  
        //=================================
        //画面移動制御用座標
        private System.Drawing.Point mousePoint;

        //=================================
        //デスクトップインスタンス
        private ViewDeskTop desktop;
        private ViewLiplisSetting viewSetting;
        private ViewLiplisRssSetting viewRss;
        private ViewCharacter viewCharacter;

        ///====================================================================
        ///
        ///                             初期化処理
        ///                         
        ///====================================================================
        #region 初期化処理
        /// <summary>
        /// コンストラクター
        /// </summary>
        public ViewMenu(ViewDeskTop desktop)
        {
            //デスクトップインスタンスの取得
            this.desktop = desktop;

            //画面の初期化
            InitializeComponent();

            //クラスの初期化、設定の読み込み
            initClass();
        }

        /// <summary>
        /// クラスの初期化
        /// </summary>
        private void initClass()
        {
            
        }

        #endregion

        ///====================================================================
        ///
        ///                           イベントハンドラ
        ///                         
        ///====================================================================
        #region イベントハンドラ
        private void btnCross_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// タイトルラベルの移動
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lblTitle_MouseDown(object sender, MouseEventArgs e)
        {
            onMouseDown(sender, e);
        }
        private void lblTitle_MouseMove(object sender, MouseEventArgs e)
        {
            onMouseMove(sender, e);
        }

        /// <summary>
        /// 終了ボタン
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnEnd_Click(object sender, EventArgs e)
        {
            //デスクトップの終了処理を呼び出す
            desktop.liplisEnd();
        }


        private void btnLog_Click(object sender, EventArgs e)
        {
            desktop.openViewLog();
        }

        private void btnSleep_Click(object sender, EventArgs e)
        {

        }

        private void btnFlow_Click(object sender, EventArgs e)
        {

        }

        private void btnSetting_Click(object sender, EventArgs e)
        {
            openViewSetting();
        }

        private void btnRss_Click(object sender, EventArgs e)
        {
            openViewRss();
        }

        /// <summary>
        /// キャラクターボタン
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnChar_Click(object sender, EventArgs e)
        {
            openViewCharacter();
        }

        

        /// <summary>
        /// ボタンエンター時 フォーカス
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnChar_Enter(object sender, EventArgs e)
        {
            this.btnChar.Select();
        }
        private void btnSetting_Enter(object sender, EventArgs e)
        {
            this.btnSetting.Focus();
        }
        private void btnFlow_Enter(object sender, EventArgs e)
        {
            this.btnFlow.Select();
        }
        private void btnLog_Enter(object sender, EventArgs e)
        {
            this.btnLog.Select();
        }
        private void btnSleep_Enter(object sender, EventArgs e)
        {
            this.btnSleep.Select();
        }
        private void btnEnd_Enter(object sender, EventArgs e)
        {
            this.btnEnd.Select();
        }


        #endregion


        ///====================================================================
        ///
        ///                             画面操作
        ///                         
        ///====================================================================
        #region 画面操作
        /// <summary>
        /// マウスダウン時処理
        /// ☆Miniオーバーライド
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">MouseEventArgs</param>
        protected virtual void onMouseDown(object sender, MouseEventArgs e)
        {
            if ((e.Button & MouseButtons.Left) == MouseButtons.Left)
            {
                //位置を記憶する
                mousePoint = new System.Drawing.Point(e.X, e.Y);
            }
        }

        /// <summary>
        /// マウスムーブ時処理
        /// ☆Miniオーバーライド
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">MouseEventArgs</param>
        protected virtual void onMouseMove(object sender, MouseEventArgs e)
        {
            if ((e.Button & MouseButtons.Left) == MouseButtons.Left)
            {
                this.Left += e.X - mousePoint.X;
                this.Top += e.Y - mousePoint.Y;
            }
        }

        /// <summary>
        /// キャラクター選択画面を表示する
        /// </summary>
        private void openViewCharacter()
        {
            //インスタンス化されているか
            if (viewCharacter == null)
            {
                createViewCharacter();
            }

            //閉じられているか?
            if (WpfUtil.isWpfDisposed(viewCharacter))
            {
                createViewCharacter();
            }

            //フォームを開く
            viewCharacter.Show();
            viewCharacter.Activate();
        }
        private void createViewCharacter()
        {
            viewCharacter = new ViewCharacter(this.desktop);
        }

        /// <summary>
        /// 設定画面を開く
        /// </summary>
        private void openViewSetting()
        {
            //インスタンス化されているか
            if (viewSetting == null)
            {
                createViewSetting();
            }

            //閉じられているか?
            if (viewSetting.IsDisposed)
            {
                createViewSetting();
            }

            //フォームを開く
            viewSetting.Show();
            viewSetting.Activate();
        }
        private void createViewSetting()
        {
            viewSetting = new ViewLiplisSetting(this.desktop.baseSetting);
        }

        /// <summary>
        /// RSS設定画面を開く
        /// </summary>
        private void openViewRss()
        {
            //インスタンス化されているか
            if (viewRss == null)
            {
                createViewRss();
            }

            //閉じられているか?
            if (viewRss.IsDisposed)
            {
                createViewRss();
            }

            //フォームを開く
            viewRss.Show();
            viewRss.Activate();
        }
        private void createViewRss()
        {
            viewRss = new ViewLiplisRssSetting(this.desktop.baseSetting);
        }

        #endregion


    }
}
