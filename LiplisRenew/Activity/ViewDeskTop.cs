//=======================================================================
//  ClassName : ViewDeskTop
//  概要      : デスクトップの管理クラス
//              ウィジェットインスタンスの保持と管理を行う
//
//LiplisWidget(WPF画面)を表示させるためには、以下のプロジェクトを参照する必要がある。
//   LiplisWidget
//
//WPF画面を表示させるためには、以下のアセンブリの参照が必要
//   PresentationCore
//   PresentationFramework
//   WindowsFormsIntegration
//   System.Xaml
//   WindowsBase
//
// iOS版の場合、画面インスタンスはAppDelegateに持つが、Windows版の場合、
// デスクトップクラスで管理する
//
//
//
//Liplis5.0
//
//アップデート履歴
//   2016/05/08 ver5.0.0 作成
//
//  Copyright(c) 2010-2016 LipliStyle.Sachin
//=======================================================================
using Liplis.Json;
using Liplis.MainSystem;
using Liplis.Msg;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows;
using System.Security.Permissions;
using Liplis.Wpf.Xaml;

namespace Liplis.Activity
{
    public partial class ViewDeskTop : Form
    {
        ///=============================
        ///設定関連
        public LiplisKeyManager kman ;
        public LiplisPreference setting;

        //=================================
        //リプリスウィジェット
        public List<XamlLiplisImage> widgetList;

        //=================================
        //logリスト
        public MsgLiplisLogList lpsLog;

        //=================================
        //スキン管理
        public SkinController sc;

        //=================================
        //画面
        private ViewMenu menu;


        ///====================================================================
        ///
        ///                             初期化処理
        ///                         
        ///====================================================================
        #region 初期化処理

        /// <summary>
        /// 画面非表示のおまじない
        /// フォームのCreateParamsプロパティをオーバーライドする
        /// </summary>
        protected override CreateParams CreateParams
        {
            [SecurityPermission(SecurityAction.Demand,Flags = SecurityPermissionFlag.UnmanagedCode)]
            get
            {
                const int WS_EX_TOOLWINDOW = 0x80;
                const long WS_POPUP = 0x80000000L;
                const int WS_VISIBLE = 0x10000000;
                const int WS_SYSMENU = 0x80000;
                const int WS_MAXIMIZEBOX = 0x10000;

                CreateParams cp = base.CreateParams;
                cp.ExStyle = WS_EX_TOOLWINDOW;
                cp.Style = unchecked((int)WS_POPUP) |
                    WS_VISIBLE | WS_SYSMENU | WS_MAXIMIZEBOX;
                cp.Width = 0;
                cp.Height = 0;

                return cp;
            }
        }

        /// <summary>
        /// コンストラクター
        /// </summary>
        public ViewDeskTop()
        {
            InitializeComponent();

            //クラスの初期化、設定の読み込み
            initClass();

            //ウィジェットリストの初期化
            initWidgetList();

            //暫定
            openMenu();
        }

        /// <summary>
        /// クラスの初期化
        /// </summary>
        private void initClass()
        {
            this.kman    = new LiplisKeyManager();  //キーマネージャーの初期化
            this.lpsLog  = new MsgLiplisLogList();  //ログリストの初期化
            this.setting = new LiplisPreference();  //設定読み込み
            this.sc      = new SkinController();    //スキン管理クラス
        }

        /// <summary>
        /// ウィジェットリストの初期化
        /// </summary>
        private void initWidgetList()
        {
            widgetList = new List<XamlLiplisImage>();


        }
        #endregion

        ///====================================================================
        ///
        ///                       アイコンイベントハンドラ
        ///                         
        ///====================================================================
        #region アイコンイベントハンドラ
        /// <summary>
        /// メニュークリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsmMenu_Click(object sender, EventArgs e)
        {
            openMenu();
        }

        /// <summary>
        /// 終了クリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsmEnd_Click(object sender, EventArgs e)
        {
            liplisEnd();
        }



        /// <summary>
        /// 設定
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsmSetting_Click(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// ログ画面を開く
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsmLog_Click(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// 非表示(最小化)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsmMinimize_Click(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// 全員おやすみ/ 起床
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsmSleep_Click(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// ダブルクイック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void icon_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            this.WindowState = FormWindowState.Normal;
            this.Activate();
        }
        #endregion

        ///====================================================================
        ///
        ///                             画面操作
        ///                         
        ///====================================================================
        #region 画面操作
        /// <summary>
        /// リプリスの終了
        /// </summary>
        public void liplisEnd()
        {
            //TODO: ViewDeskTop liplisEnd もろもろの終了処理が必要か？

            this.Close();
        }

        /// <summary>
        /// メニューを開く
        /// </summary>
        public void openMenu()
        {
            //インスタンス化されているか
            if (menu == null)
            {
                createMenu();
            }

            //閉じられているか?
            if(menu.IsDisposed)
            {
                createMenu();
            }

            //フォームを開く
            menu.Show();
            menu.Activate();
        }
        private void createMenu()
        {
            menu = new ViewMenu(this);
        }


        #endregion


    }
}
