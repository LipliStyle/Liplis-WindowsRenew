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
using Liplis.Com;
using Liplis.Com.Defile;
using Liplis.Gui;
using Liplis.MainSystem;
using Liplis.Msg;
using Liplis.Tpc;
using Liplis.Utl;
using Liplis.Widget;
using Liplis.Wpf;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Security.Permissions;
using System.Threading.Tasks;
using System.Windows.Forms;

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
        //ニュースインスタンス
        public LiplisGilsTalk lpsGilsTalk;

        //=================================
        //画面
        public ViewMenu menu;
        public ViewLiplisLog vLog;
        //public ViewChattingWithEveryone vEveryone;
        public ViewLiplisSetting viewSetting;
        public ViewLiplisRssSetting viewRss;
        public ViewCharacter viewCharacter;

        ///=====================================
        /// バックグラウンド処理
        BackgroundWorker workLoading;

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

            //ワーカーの初期化
            this.initWorker();
        }

        /// <summary>
        /// ロードイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ViewDeskTop_Load(object sender, EventArgs e)
        {
            //ウィジェットリストの初期化
            this.initLiplis();

            //ロードワーカー開始
            this.workLoading.RunWorkerAsync();
        }

        /// <summary>
        /// ノラリスからデスクトップをロードする
        /// </summary>
        public void ViewDeskTop_Load_FromNoralis()
        {
            //ウィジェットリストの初期化
            widgetList = new List<LiplisWidget>();
        }

        /// <summary>
        /// ロード(ブロック可能)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void workLoading_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            //ガールズトークデータ収集初期化
            this.initGilsTalk();

            Invoke((MethodInvoker)delegate
            {
                widgetLoadShow();
            });
            
        }


        /// <summary>
        /// すべての初期化完了時、おしゃべり開始
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void workLoading_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
            //メニューを開く
            if (baseSetting.lpsMenuOpen == 1) { openMenu(); }
            //openViewEveryoneWindow();
        }

        /// <summary>
        /// クラスの初期化
        /// </summary>
        private void initClass()
        {
            this.kman                = new LiplisKeyManager();  //キーマネージャーの初期化
            this.lpsLog              = new MsgLiplisLogList();  //ログリストの初期化
            this.baseSetting         = new LiplisPreference();  //設定読み込み
            this.sc                  = new SkinController();    //スキン管理クラス
            this.createViewLog();                       //ログウインドウを開く
            //this.createViewEveryoneWindow();            //みんなでおしゃべりウインドウを開く
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
                List<string> delList = new List<string>();
                foreach(var key in this.kman.keyList)
                {
                    //スキン読み込みに失敗した場合は削除リストに追加する
                    if(!this.addLoadWidget(key))
                    {
                        delList.Add(key);
                    }
                }

                //スキンが存在しないキーを削除する
                this.kman.delKey(delList);
            }

        }

        /// <summary>
        /// ログ画面の初期化
        /// </summary>
        private void createViewLog()
        {
            vLog = new ViewLiplisLog(this);
            vLog.Opacity = 0;
            vLog.Hide();
            vLog.Opacity = 100;
        }

        /// <summary>
        /// ログ画面の初期化
        /// </summary>
        //private void createViewEveryoneWindow()
        //{
        //    vEveryone = new ViewChattingWithEveryone(this);
        //    //vEveryone.Show();
        //    vEveryone.Hide();
        //}

        /// <summary>
        /// ガールズトーク初期化
        /// </summary>
        private void initGilsTalk()
        {
            this.lpsGilsTalk = new LiplisGilsTalk(this.widgetList, this.baseSetting, this);
        }

        #endregion

        //============================================================
        //
        //ワーカー初期化処理
        //
        //============================================================
        #region ワーカー初期化処理 
        /// <summary>
        /// ロードワーカー初期化
        /// </summary>
        private void initWorker()
        {

            this.workLoading = new System.ComponentModel.BackgroundWorker();
            this.workLoading.DoWork += new System.ComponentModel.DoWorkEventHandler(workLoading_DoWork);
            this.workLoading.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(workLoading_RunWorkerCompleted);

            this.workLoading.WorkerReportsProgress = true;
            this.workLoading.WorkerSupportsCancellation = true;
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
            openViewSetting();
        }

        /// <summary>
        /// ログ画面を開く
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsmLog_Click(object sender, EventArgs e)
        {
            openViewLog();
        }

        /// <summary>
        /// 非表示(最小化)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsmMinimize_Click(object sender, EventArgs e)
        {
            bool allHide = true;

            foreach (var widget in widgetList)
            {
                if (widget.WindowState == System.Windows.WindowState.Normal)
                {
                    allHide = false;
                    break;
                }
            }

            //全員最小化していたら、全員復帰、それ以外なら、全員最小化する
            if (allHide)
            {
                widgetNormalizeAll();
            }
            else
            {
                widgetMinmizeAll();
            }
        }


        /// <summary>
        /// 全員おやすみ/ 起床
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsmSleep_Click(object sender, EventArgs e)
        {
            bool allSleep = true;

            foreach (var widget in widgetList)
            {
                if(!widget.flgSitdown)
                {
                    allSleep = false;
                    break;
                }
            }

            //全員寝ていたらウェイクアップを発動し、起きていたらスリープを発動する
            if(allSleep)
            {
                widgetWakeup();
            }
            else
            {
                widgetSleep();
            }

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

            //ログファイル削除
            LpsLiplisUtil.DeleteFileTargetDir(LpsPathController.getTempPath2());
            LpsLiplisUtil.DeleteFile(LpsPathController.getLogPath());

            //すべてのウィジェットを終了する
            this.goodByeAllWidget();

            //5s待つ
            LpsLiplisUtil.wait(5000);

            //終了
            this.Close();
        }

        //フォーム終了後、アプリケーションを終了する。
        private void ViewDeskTop_FormClosed(object sender, FormClosedEventArgs e)
        {
            Environment.Exit(0);
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
        #region ウィジェット処理        
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

            //あっどウィジェット
            addWidget(defaultSkin);
        }

        /// <summary>
        /// ウィジェットを追加する(読み込み追加)
        /// </summary>
        /// <param name="key"></param>
        public bool addLoadWidget(string key)
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

                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// ウィジェットを追加する
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
        /// ノラリスからウィジェットをロードする
        /// </summary>
        /// <param name="skin"></param>
        /// <returns></returns>
        public bool addLoadWidgetFromNoralis(Skin skin)
        {
            //スキンデータ取得チェック
            if (skin != null)
            {
                //キー取得
                LiplisWidgetPreference widgetSetting = createWidgetSettingFromSkin(skin);

                LiplisWidget lps = new LiplisWidget(this, widgetSetting, skin);

                //キーマネージャに追加する
                this.kman.addKey(widgetSetting.key);

                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// ウィジェットリストを回し、showする
        /// </summary>
        private void widgetLoadShow()
        {
            foreach (var lps in this.widgetList)
            {
                lps.Show();
            }
        }


        /// <summary>
        /// キーからプリファレンスを読み出す
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        private LiplisWidgetPreference createWidgetSetttingFromKey(string key)
        {
            LiplisWidgetPreference widgetSetting = new LiplisWidgetPreference(key);

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


            //設定の保存
            widgetSetting.setPreferenceData();

            //設定を返す
            return widgetSetting;
        }

        /// <summary>
        /// すべてのウィジェットを終了させる
        /// </summary>
        public void endAllWidget()
        {
            foreach (LiplisWidget widget in widgetList)
            {
                widget.Close();
            }
        }

        /// <summary>
        /// すべてのウィジェットをグッバイする
        /// </summary>
        public void goodByeAllWidget()
        {
            foreach (LiplisWidget widget in widgetList)
            {
                widget.goodBaydLiplis();
            }
        }

        /// <summary>
        /// すべてのウィジェットを最小化する
        /// </summary>
        public void miniMizeWidget()
        {
            foreach (LiplisWidget widget in widgetList)
            {
                widget.minimize();
            }
        }

        /// <summary>
        /// ウィジェットをデスクトップから削除する
        /// </summary>
        /// <param name="widget"></param>
        public void delWidget(LiplisWidget widget)
        {
            //キーマネージャーからキーを削除する
            delWidgetRegisterData(widget);

            //Liplisオブジェクトの削除
            Invoke((MethodInvoker)delegate
            {
                widget.Dispose();
            });
        }

        /// <summary>
        /// ウィジェット登録情報を削除する
        /// </summary>
        /// <param name="widget"></param>
        public void delWidgetRegisterData(LiplisWidget widget)
        {
            //キーマネージャーからキーを削除する
            this.kman.delKey(widget.setting.key);

            //ウィジェットリストから削除する
            this.delFromWidgetList(widget);
        }

        /// <summary>
        /// デスクトップの全てのウィジェットを削除する
        /// </summary>
        public void delWidgetAll()
        {
            foreach(var widget in widgetList)
            {
                this.delWidget(widget);
            }

            //キーは全て削除
            this.kman.delAllKey();
        }

        /// <summary>
        /// ウィジェットをウィジェットリストから削除する
        /// </summary>
        /// <param name="widget"></param>
        private void delFromWidgetList(LiplisWidget widget)
        {
            //ヒットしたら削除
            if (widgetList.Contains(widget))
            {
                this.widgetList.Remove(widget);
            }
        }

        /// <summary>
        /// ウィジェットをスリープにする
        /// </summary>
        private void widgetSleepAuto()
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
        public void widgetSleep()
        {
            //ウィジェットリストを回してらりほー
            foreach (var wid in this.widgetList)
            {
                Invoke((MethodInvoker)delegate
                {
                    wid.sleep();
                });
            }
        }

        /// <summary>
        ///  ウィジェットをウェイクアップする
        /// </summary>
        private void widgetWakeupAuto()
        {
            //設定チェック
            if (this.baseSetting.lpsAutoWakeup == 1)
            {
                //ウィジェットリストを回して起こす

                foreach (var widget in this.widgetList)
                {
                    Invoke((MethodInvoker)delegate
                    {
                        widget.wakeup();
                    });
                    
                }
            }
        }
        public void widgetWakeup()
        {
            foreach (var widget in this.widgetList)
            {
                Invoke((MethodInvoker)delegate
                {
                    widget.wakeup();
                });

            }
        }

        /// <summary>
        /// デスクトップの全てのウィジェットにレスキューする
        /// </summary>
        public void rescueWidgetAll()
        {
            foreach (var widget in this.widgetList)
            {
                Invoke((MethodInvoker)delegate
                {
                    widget.rescue(0);
                });
            }
        }

        /// <summary>
        /// すべてのウィジェットを最小化する
        /// </summary>
        public void widgetMinmizeAll()
        {
            foreach (var widget in widgetList)
            {
                widget.WindowState =System.Windows.WindowState.Minimized;
            }
        }

        /// <summary>
        /// すべてのウィジェットを復帰させる
        /// </summary>
        public void widgetNormalizeAll()
        {
            foreach (var widget in widgetList)
            {
                widget.WindowState = System.Windows.WindowState.Normal;
            }
        }

        #endregion

        //============================================================
        //
        //画面処理
        //
        //============================================================
        #region 画面処理

        /// <summary>
        /// ログウィンドウを開く
        /// </summary>
        public void openViewLog()
        {
            //インスタンス化されているか
            if (vLog == null)
            {
                createViewLog();
            }

            //閉じられているか?
            if (WpfUtil.isWpfDisposed(vLog))
            {
                createViewLog();
            }

            //フォームを開く
            vLog.Show();
            vLog.WindowState = System.Windows.WindowState.Normal;
            vLog.Activate();
        }

        /// <summary>
        /// ログの追加
        /// </summary>
        public void addLog(MsgTalkMessageLog log, Skin skin, LiplisWidgetPreference setting)
        {
            vLog.addLog(log, skin, setting);
        }

        /// <summary>
        /// みんなでおしゃべりウインドウを開く
        /// </summary>
        //public void openViewEveryoneWindow()
        //{
        //    //インスタンス化されているか
        //    if (vLog == null)
        //    {
        //        createViewEveryoneWindow();
        //    }

        //    //閉じられているか?
        //    if (vEveryone.IsDisposed)
        //    {
        //        createViewEveryoneWindow();
        //    }

        //    //フォームを開く
        //    vEveryone.Show();
        //    vEveryone.Activate();
        //}


        /// <summary>
        /// キャラクター選択画面を表示する
        /// </summary>
        public void openViewCharacter()
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
        public void createViewCharacter()
        {
            viewCharacter = new ViewCharacter(this);
        }

        /// <summary>
        /// 設定画面を開く
        /// </summary>
        public void openViewSetting()
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
        public void createViewSetting()
        {
            viewSetting = new ViewLiplisSetting(this.baseSetting);
        }

        /// <summary>
        /// RSS設定画面を開く
        /// </summary>
        public void openViewRss()
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
        public void createViewRss()
        {
            viewRss = new ViewLiplisRssSetting(this.baseSetting);
        }

        #endregion

        //============================================================
        //
        //設定同期
        //
        //============================================================
        #region 設定同期  
        /// <summary>
        /// 設定同期
        /// みんなでおしゃべりする場合は、話題設定が同期される
        /// </summary>
        /// <param name="setWidget"></param>
        public void syncSetting(LiplisWidget setWidget)
        {
            foreach (LiplisWidget widget in widgetList)
            {
                if (widget.setting.lpsTalkMode == (int)LPS_TALK_MODE.EVERYONE)
                {
                    if(widget.setting.key != setWidget.setting.key)
                    {
                        widget.setting.lpsTopicNews      = setWidget.setting.lpsTopicNews; 
                        widget.setting.lpsTopic2ch       = setWidget.setting.lpsTopic2ch; 
                        widget.setting.lpsTopicNico      = setWidget.setting.lpsTopicNico; 
                        widget.setting.lpsTopicRss       = setWidget.setting.lpsTopicRss;
                        widget.setting.lpsTopicTwitterPu = setWidget.setting.lpsTopicTwitterPu;
                        widget.setting.lpsTopicTwitterMy = setWidget.setting.lpsTopicTwitterMy;
                        widget.setting.setPreferenceData();
                    }
                }
            }
        }

        /// <summary>
        /// 元々登録されているウィジェットに話題設定を同期する
        /// </summary>
        public void syncSetting()
        {
            if(widgetList.Count < 1)
            {
                return;
            }
            
            //一番最初のウィジェットに同期する
            LiplisWidget setWidget = widgetList[0];

            foreach (LiplisWidget widget in widgetList)
            {
                if (widget.setting.lpsTalkMode == (int)LPS_TALK_MODE.EVERYONE)
                {
                    if (widget.setting.key != setWidget.setting.key)
                    {
                        widget.setting.lpsTopicNews = setWidget.setting.lpsTopicNews;
                        widget.setting.lpsTopic2ch = setWidget.setting.lpsTopic2ch;
                        widget.setting.lpsTopicNico = setWidget.setting.lpsTopicNico;
                        widget.setting.lpsTopicRss = setWidget.setting.lpsTopicRss;
                        widget.setting.lpsTopicTwitterPu = setWidget.setting.lpsTopicTwitterPu;
                        widget.setting.lpsTopicTwitterMy = setWidget.setting.lpsTopicTwitterMy;
                        widget.setting.setPreferenceData();
                    }
                }
            }
        }

        /// <summary>
        ///　みんなでおしゃべりの代表設定を返す
        /// </summary>
        /// <returns></returns>
        public LiplisWidgetPreference getMinnaRepresentativeSetting()
        {
            if (widgetList.Count < 1)
            {
                return null;
            }

            //一番最初のウィジェットに同期する
            return widgetList[0].setting;
        }




        #endregion
    }
}
