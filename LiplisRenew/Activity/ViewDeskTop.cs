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
using Liplis.Widget;
using Liplis.Gui;
using Liplis.Com;

namespace Liplis.Activity
{
    public partial class ViewDeskTop : Form
    {
        ///=============================
        ///設定関連
        public LiplisKeyManager kman ;
        public LiplisPreference baseSetting;

        //=================================
        //リプリスウィジェット
        public List<LiplisWidget> widgetList;

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

            //起動前チェック
            liplisStartCheck();

            //ウィジェットリストの初期化
            initLiplis();

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
            this.baseSetting = new LiplisPreference();  //設定読み込み
            this.sc      = new SkinController();    //スキン管理クラス
        }

        /// <summary>
        /// スタートチェック
        /// </summary>
        private void liplisStartCheck()
        {
            //スキン数チェック
            if(sc.lstSkin.Count == 0)
            {
                LpsMessage.showError(LiplisErrorDefine.LPS_ERROR_001_SKIN_NOT_FOUND);
                Environment.Exit(1);
            }
        }

        /// <summary>
        /// ウィジェットリストの初期化
        /// </summary>
        private void initLiplis()
        {
            //ウィジェットリストの初期化
            widgetList = new List<LiplisWidget>();

            if (this.kman.keyList.Count == 0)
            {
                //キーリストが０件の場合、リリ(または他キャラ)を一個追加して起動
                this.addNewDefaultWidget();
            }
            else
            {
                //キーリストを回してウィジェットインスタンスを生成する
                foreach(var key in this.kman.keyList)
                {
                    this.addLoadWidget(key);
                }
            }

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


        //============================================================
        //
        //ウィジェット処理
        //
        //============================================================

        /// <summary>
        /// ウィジェットを追加する(新規追加)
        /// </summary>
        public void addNewDefaultWidget()
        {
            //リリのスキンを探す
            Skin defaultSkin = sc.getSkin(LpsDefine.DEFAULT_SKIN);

            //リリが見つからなければ他を探す
            if (defaultSkin == null)
            {
                defaultSkin = sc.getSkinRandam();
            }

            //新規キー作成
            LiplisWidgetPreference widgetSetting = createWidgetSettingFromSkin(defaultSkin);

            //ウィジェット
            LiplisWidget lps = new LiplisWidget(this, widgetSetting, defaultSkin);

            //ウィジェットリストに追加する
            this.widgetList.Add(lps);

            //キーマネージャに追加する
            this.kman.addKey(defaultSkin.charName);
        }

        /// <summary>
        /// ウィジェットを追加する(読み込み追加)
        /// </summary>
        /// <param name="key"></param>
        public void addWidget(Skin skin)
        {
            //キー取得
            LiplisWidgetPreference widgetSetting = createWidgetSettingFromSkin(skin);

            LiplisWidget lps = new LiplisWidget(this, widgetSetting, skin);

            //ウィジェットリストに追加する
            this.widgetList.Add(lps);

            //召喚
            lps.Show();

            //キーマネージャに追加する
            this.kman.addKey(widgetSetting.key);
        }

        /// <summary>
        /// ウィジェットを追加する(読み込み追加)
        /// </summary>
        /// <param name="key"></param>
        private void addLoadWidget(string key)
        {
            //キー取得
            LiplisWidgetPreference widgetSetting = createWidgetSetttingFromKey(key);

            //スキンデータ取得
            Skin skin = sc.getSkin(widgetSetting.charName);

            //スキンデータ取得チェック
            if (skin != null)
            {
                LiplisWidget lps = new LiplisWidget(this, widgetSetting, skin);

                //ウィジェットリストに追加する
                this.widgetList.Add(lps);
            }
        }


        /// <summary>
        /// キーからプリファレンスを読み出す
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        private LiplisWidgetPreference createWidgetSetttingFromKey(string key)
        {
            LiplisWidgetPreference widgetSetting = new LiplisWidgetPreference();

            if (key == "")
            {
                //初期座標の設定
                widgetSetting.locationX = Screen.PrimaryScreen.Bounds.Width / 2 ;
                widgetSetting.locationY = Screen.PrimaryScreen.Bounds.Height / 2;
            }
            else
            {
                //キーからロード
                widgetSetting = new LiplisWidgetPreference(key);
            }

            return widgetSetting;
        }

        /// <summary>
        /// スキンからプリファレンスを作成する
        /// </summary>
        /// <param name="skin"></param>
        /// <returns></returns>
        private LiplisWidgetPreference createWidgetSettingFromSkin(Skin skin)
        {
            LiplisWidgetPreference widgetSetting = new LiplisWidgetPreference();

            //キャラクター名設定
            widgetSetting.charName = skin.charName;

            //初期座標の設定
            widgetSetting.locationX = Screen.PrimaryScreen.Bounds.Width / 2;
            widgetSetting.locationY = Screen.PrimaryScreen.Bounds.Height / 2;

            return widgetSetting;
        }



        /*
        ウィジェットをデスクトップから削除する
        */
        private void delWidget(LiplisWidget widget)
        {
            //キーマネージャーからキーを削除する
            this.kman.delKey(widget.setting.charName);

            //ウィジェットリストから削除する
            this.delFromWidgetList(widget);

            //Liplisオブジェクトの削除
            Invoke((MethodInvoker)delegate
            {
                widget.Dispose();
            });
        }

        /*
        デスクトップの全てのウィジェットを削除する
        */
        private void delWidgetAll()
        {
            foreach(var widget in widgetList)
            {
                this.delWidget(widget);
            }

            //キーは全て削除
            this.kman.delAllKey();
        }

        /*
        ウィジェットをウィジェットリストから削除する
        */
        private void delFromWidgetList(LiplisWidget widget)
        {
            //ヒットしたら削除
            if (widgetList.Contains(widget))
            {
                this.widgetList.Remove(widget);
            }
        }

        /*
        ウィジェットをスリープにする
        */
        private void widgetSleep()
        {
            //設定チェック
            if (this.baseSetting.lpsAutoSleep == 1)
            {
                //ウィジェットリストを回してらりほー
                foreach(var wid in this.widgetList)
                {
                    Invoke((MethodInvoker)delegate
                    {
                        wid.sleep();
                    });
                }
            }
        }

        /*
        ウィジェットをウェイクアップする
        */
        private void widgetWakeup()
        {
            ////設定チェック
            //if (this.baseSetting.lpsAutoWakeup == 1)
            //{
            //    //ウィジェットリストを回して起こす

            //    foreach (var widget in this.widgetList)
            //    {
            //        widget.wakeup();
            //    }
            //}
        }

        /// <summary>
        /// デスクトップの全てのウィジェットにレスキューする
        /// </summary>
        private void rescueWidgetAll()
        {
            foreach (var widget in this.widgetList)
            {
                Invoke((MethodInvoker)delegate
                {
                    widget.rescue(0);
                });
            }
        }
    }
}
