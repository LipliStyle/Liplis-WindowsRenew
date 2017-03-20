//=======================================================================
//  ClassName : LiplisWidget
//  概要      : リプリスウィジェット
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
using Liplis.Activity;
using Liplis.Body;
using Liplis.Com;
using Liplis.Com.Defile;
using Liplis.Exp;
using Liplis.Gui;
using Liplis.MainSystem;
using Liplis.Msg;
using Liplis.Talk;
using Liplis.Tpc;
using Liplis.Utl;
using Liplis.Voc;
using Liplis.Wpf;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;

namespace Liplis.Widget
{
    /// <summary>
    /// LiplisWidget.xaml の相互作用ロジック
    /// </summary>
    public partial class LiplisWidget : Window
    {
        //=================================
        //Liplis要素
        public LiplisWidgetPreference setting;
        public Skin skin;
        private LiplisWindowManager windowManager;

        //=================================
        //ウインドウ
        private ViewLiplisWidgetSetting viewWidgetSetting;
        private ViewChat viewChat;

        //=================================
        //ロードボディオブジェクト
        private LiplisBody lpsBody;

        //=================================
        //デスクトップインスタンス
        public ViewDeskTop desk;

        //=================================
        //ニュースインスタンス
        private LiplisNews lpsNews;
        private LiplisBattery lpsBattery;
        //private LiplisApiChat lpsChatTalk;

        //=================================
        //ボイスロイドインスタンス
        private LpsVoiceRoid lvr;

        //=================================
        //タイマー
        private Timer updateTimer;
        private Timer nextTimer;
        private Timer leaveTimer;
        private Int32 flgAlarm = 0;

        //行方向位置
        Int32 row1YMargin;
        Int32 row3YMargin;
        Int32 row2YMargin;

        //=================================
        //制御プロパティ
        private MsgTalkMessage liplisNowTopic;          //現在ロードおしゃべりデータ
        private string liplisNowWord          = "";     //ロード中の単語
        private string liplisChatText         = "";     //テキストバッファ
        private string liplisUrl              = "";     //現在ロード中の記事のURL
        private Int32 cntLct                  = 0;      //チャットテキストカウント
        private Int32 cntLnw                  = 0;      //ナウワードカウント
        private Int32 nowPoint                = 0;      //現在感情レベル
        private Int32 nowPos                  = 0;      //現在品詞
        private Int32 nowEmotion              = 0;      //現在感情
        private Int32 prvEmotion              = 0;      //前回感情
        private Int32 cntMouth                = 0;      //口パクカウント
        private Int32 cntBlink                = 0;      //まばたきカウント
        private Int32 nowBlink                = 0;      //現在目のオープン状態
        private Int32 prvBlink                = 0;      //１つ前の目のオープン状態
        private Int32 nowDirection            = 0;      //現在の方向
        private Int32 prvDirection            = 0;      //１つ前の方向
        private Int32 cntSlow                 = 0;      //スローカウント

        //=================================
        //制御フラグ
        public bool flgConnect      = false;   //接続フラグ
        public bool flgBodyChencge  = false;   //ボディ変更フラグ
        public bool flgChatting     = false;   //おしゃべり中フラグ
        public bool flgSkip         = false;   //スキップフラグ
        public bool flgSkipping     = false;   //スキップ中フラグ
        public bool flgSitdown      = false;   //おすわり中フラグ
        public bool flgThinking     = false;   //考え中フラグ
        public bool flgEnd          = false;   //おしゃべり終了フラグ
        public bool flgTag          = false;   //タグチェック
        public bool flgChatTalk     = false;   //
        public bool flgEveryoneTalk = false;   //みんなでおしゃべり中
        public bool flgDebug        = false;   //
        public bool flgOutputDemo   = false;   //
        public bool flgIconOn       = false;   //アイコン表示状態
        public bool flgWidgetDelete = false;   //

        ///=====================================
        /// 設定値
        private Int32 liplisRefresh  = 10;          //リフレッシュレート

        ///=====================================
        /// 時報制御
        private Int32 prvHour = 0;

        ///=====================================
        /// ウィイェット関連
        LiplisIconImage lpsIcoSleep;
        LiplisIconImage lpsIcoLog;
        LiplisIconImage lpsIcoSetting;
        LiplisIconImage lpsIcoChat;
        LiplisIconImage lpsIcoClock;
        LiplisIconImage lpsIcoBattery;

        ///=====================================
        /// バックグラウンド処理
        BackgroundWorker workLoading;

        //============================================================
        //
        //初期化処理
        //
        //============================================================
        #region 初期化処理 

        /// <summary>
        /// コンストラクター
        /// </summary>
        public LiplisWidget(ViewDeskTop desk, LiplisWidgetPreference setting, Skin skin)
        {
            try
            {
                //画面要素初期化
                InitializeComponent();

                //設定データ
                this.setting = setting;

                //デスクトップクラス取得
                this.desk = desk;

                //スキンデータ取得
                this.skin = skin;

                //ワーカーの初期化
                this.initWorker();
            }
            catch (Exception err)
            {
                errorEnd(err);
            }
        }

        /// <summary>
        /// ロードイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                //ロードワーカー開始
                this.workLoading.RunWorkerAsync();
            }
            catch (Exception err)
            {
                errorEnd(err);
            }
        }

        /// <summary>
        /// ロード(ブロック可能)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void workLoading_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            try
            {
                //クラスの初期化
                this.initClass();
            }
            catch (Exception err)
            {
                errorEnd(err);
            }
        }

        /// <summary>
        /// すべての初期化完了時、おしゃべり開始
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void workLoading_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
            try
            {
                //ビューの初期化;
                this.initView();

                //あいさつ
                this.greet();
            }
            catch (Exception err)
            {
                errorEnd(err);
            }
        }


        /// <summary>
        /// クラスの初期化
        /// </summary>
        private void initClass()
        {
            try
            {
                //デフォルトボディロード
                this.lpsBody = skin.xmlBody.getDefaultBody();

                //ニュースの初期化
                this.lpsNews = new LiplisNews(desk.baseSetting, setting,skin.xmlSkin.toneUrl);

                //音声管理クラスの初期化
                initVoiceRoid();

                //バッテリーオブジェクトの初期化
                this.lpsBattery = new LiplisBattery();
            }
            catch (Exception ex)
            {
                throw new ExpWidgetInitException(ex);
            }
        }

        /// <summary>
        /// 画面要素の初期化
        /// </summary>
        private void initView()
        {
            try
            {
                //非アクティブ
                this.ShowActivated = false;

                //座標調整
                this.Top = setting.locationY;
                this.Left = setting.locationX;

                //サイズ設定
                this.setSize(skin.xmlBody.height, skin.xmlBody.width);

                //ウインドウの初期化
                this.windowManager = new LiplisWindowManager(this,this.setting,this.skin);

                //ウインドウアイコンの設定
                this.Icon = BitmapFrame.Create(new Uri(this.skin.xmlWindow.ICO_SETTING, UriKind.RelativeOrAbsolute));

                //アイコンサイズ調整
                Int32 iconBaseSide = (Int32)this.Width;

                //高さの方が小さければ、高さをベースに設定
                if (this.Width > this.Height){iconBaseSide = (Int32)this.Height;}

                //アイコンの1辺を計算
                Int32 iconSide = (Int32)(iconBaseSide / 6);

                //アイコン座標設定準備
                //列方向位置
                Int32 leftSideXMargin = 10;
                Int32 rightSideXMargin = (Int32)this.Width - 10 - iconSide;

                //行方向位置
                this.row1YMargin = (Int32)(this.Height / 2 - iconSide / 2); //1行目アイコン位置
                this.row3YMargin = (Int32)(this.Height - iconSide - 10);    //3行目アイコン位置
                this.row2YMargin = (Int32)(row1YMargin - iconSide / 2 + (row3YMargin + iconSide - row1YMargin) / 2); //2行目アイコン位置 1行目、2行目の間の真ん中に来るように計算

                //アイコンインスタンス取得
                this.lpsIcoSleep   = new LiplisIconImage(this.icoSleep, this.skin.xmlWindow.ICO_ZZZ, iconSide, leftSideXMargin, row1YMargin);
                this.lpsIcoLog     = new LiplisIconImage(this.icoLog, this.skin.xmlWindow.ICO_LOG, iconSide, leftSideXMargin, row2YMargin);
                this.lpsIcoSetting = new LiplisIconImage(this.icoSetting, this.skin.xmlWindow.ICO_SETTING, iconSide, leftSideXMargin, row3YMargin);
                this.lpsIcoChat    = new LiplisIconImage(this.icoChat, this.skin.xmlWindow.ICO_INTRODUCTION, iconSide, rightSideXMargin, row1YMargin);
                this.lpsIcoClock   = new LiplisIconImage(this.icoClock, this.skin.xmlWindow.ICO_BACK, iconSide, rightSideXMargin, row2YMargin);
                this.lpsIcoBattery = new LiplisIconImage(this.icoBattery, this.skin.xmlWindow.BATTERY_100, iconSide, rightSideXMargin, row3YMargin);

                //デフォルトアイコン座標調整(✕、最小化ボタン)
                this.icoEnd.Margin = new Thickness(this.Width - 14, 0, 0, 0);
                this.icoMinimize.Margin = new Thickness(this.Width - 42, 0, 0, 0);



                this.canClock.Width = iconSide;
                this.canClock.Height = iconSide;
                this.canClock.Margin = new Thickness(rightSideXMargin , row2YMargin , 10 + (24-iconSide/2), 68 + (24 - iconSide/2));

                this.HourLine.X1       = iconSide / 2;
                this.HourLine.Y1       = iconSide / 2;
                this.HourLine.X2       = iconSide / 2;
                this.AngleHour.CenterX = iconSide / 2;
                this.AngleHour.CenterX = iconSide / 2;

                this.MinuteLine.X1       = iconSide / 2;
                this.MinuteLine.Y1       = iconSide / 2;
                this.MinuteLine.X2       = iconSide / 2;
                this.AngleMinute.CenterX = iconSide / 2;
                this.AngleMinute.CenterX = iconSide / 2;

                this.SecondLine.X1       = iconSide / 2;
                this.SecondLine.Y1       = iconSide / 2;
                this.SecondLine.X2       = iconSide / 2;
                this.AngleSecond.CenterX = iconSide / 2;
                this.AngleSecond.CenterX = iconSide / 2;

                Canvas.SetLeft(this.CenterDot, (iconSide / 2) - 3);
                Canvas.SetTop(this.CenterDot, (iconSide / 2) - 3);

                //時計初期化
                DateTime dt = DateTime.Now;
                this.AngleSecond.Angle = dt.Second * 360.0 / 60.0;
                this.AngleMinute.Angle = (dt.Minute + dt.Second / 60.0) * 360.0 / 60.0;
                this.AngleHour.Angle = (dt.Hour + dt.Minute / 60.0) * 360.0 / 12;



                //時計情報の初期化
                this.initClock();

                //アイコンオパシティを0にしておく
                if (LpsLiplisUtil.bitToBool(setting.lpsDisplayIcon))
                {
                    flgIconOn = true;
                    this.canClock.Opacity = 100;
                }
                else
                {
                    this.icoMinimize.Opacity = 0;
                    this.icoEnd.Opacity      = 0;
                    this.icoSleep.Opacity    = 0;
                    this.icoLog.Opacity      = 0;
                    this.icoSetting.Opacity  = 0;
                    this.icoChat.Opacity     = 0;
                    this.icoClock.Opacity    = 0;
                    this.icoBattery.Opacity  = 0;
                    this.canClock.Opacity    = 0;
                    this.HourLine.Opacity    = 0;
                    this.MinuteLine.Opacity  = 0;
                    this.SecondLine.Opacity  = 0;
                }


                //ロケーション設定
                setWidgetLocation();
            }
            catch(Exception ex)
            {
                throw new ExpWidgetInitException(ex);
            }
        }



        /// <summary>
        /// チャット情報の初期化
        /// </summary>
        private void initChatInfo()
        {
            //チャットテキストの初期化
            this.liplisChatText = "";

            //なうワードの初期化
            this.liplisNowWord = "";

            //ナウ文字インデックスの初期化
            this.cntLct = 0;

            //ナウワードインデックスの初期化
            this.cntLnw = 0;
        }



        //時計の針の名前定義
        private const string CLOCK_HOUR_HAND   = "HourHand";
        private const string CLOCK_MIN_HAND    = "MinuteHand";
        private const string CLOCK_SECOND_HAND = "SecondHand";

        /// <summary>
        /// 時計アニメーションの初期化
        /// </summary>
        private void initClock()
        {
            StartAnimation(CLOCK_HOUR_HAND, this.AngleHour.Angle);
            StartAnimation(CLOCK_MIN_HAND, this.AngleMinute.Angle);
            StartAnimation(CLOCK_SECOND_HAND, this.AngleSecond.Angle);
        }

        /// <summary>
        /// 時計アニメーションの開始
        /// </summary>
        /// <param name="name"></param>
        /// <param name="angle"></param>
        private void StartAnimation(string name, double angle)
        {
            var sb = this.Resources[name] as Storyboard;
            var da = sb.Children[0] as DoubleAnimation;
            da.From = angle;
            da.To = da.From + 360.0;
            sb.Begin();
        }

        /// <summary>
        /// initLiplisIcon
        /// リプリスボイスロイド
        /// </summary>
        public void initVoiceRoid()
        {
            if (lvr != null)
            {
                lvr.Dispose();
            }

            lvr = setting.getSelectedVoiceRoid();
        }

        /// <summary>
        /// 初期化処理でエラーが発生した場合は、メッセージを表示して終了する
        /// </summary>
        private void errorEnd(Exception err)
        {
            //エラーメッセージ
            LpsMessage.showError(err.Message);

            //ログ出力
            LpsLogController.writingLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, err.ToString());

            //ウィジェットを閉じる
            this.Close();
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

        //============================================================
        //
        //終了処理
        //
        //============================================================
        /// <summary>
        /// ウィジェットを終了する
        /// </summary>
        public void Dispose()
        {
            //まずはタイマーをとめる
            flgAlarm = 0;               //フラグ0
            stopNextTimer();
            stopUpdateTimer();

            nextTimer.Dispose();
            updateTimer.Dispose();
            if (leaveTimer != null) { leaveTimer.Dispose(); }

            //ボイスロイドクラスの破棄
            lvr.Dispose();

            //設定の保存
            this.setting.setPreferenceData();

            //デスクトップからこのウィジェットを削除する
            this.deleteWidgetFromDesktop();

            //閉じる
            Dispatcher.Invoke(new Action(() =>
            {
                this.windowManager.closeWindowList();
                this.Close();
            }));

        }

        /// <summary>
        /// グッバイリプリス
        /// </summary>
        public void goodBaydLiplis()
        {
            //既に終了処理が走っている場合は無効
            if (flgEnd) { return; }

            //アップデートカウントをほぼ無限に設定
            if(nextTimer != null)
            {
                nextTimer.Interval = 99999;
            }

            //終了フラグ有効
            flgEnd = true;

            //チャットを停止しておく
            chatStop();

            //お別れの挨拶セット
            goodBay();
        }

        /// <summary>
        /// デスクトップからこのウィジェットを削除する
        /// </summary>
        private void deleteWidgetFromDesktop()
        {
            //デスクトップのウィジェットから削除する
            if (flgWidgetDelete)
            {
                desk.delWidgetRegisterData(this);
            }
        }


        //============================================================
        //
        //タイマー処理
        //
        //============================================================

        /// <summary>
        /// アップデートタイマースタート
        /// </summary>
        private void  startUpdateTimer()
        {
            //作成されていなければ作成する。開始済みなら、一旦止める
            if(updateTimer == null)
            {
                this.updateTimer = new Timer();
                this.updateTimer.Elapsed += new ElapsedEventHandler(onUpdate);
                this.updateTimer.Interval = chatSpeedCulc();
            }
            else
            {
                this.stopUpdateTimer();
            }


            this.updateTimer.Start();
        }
        private void stopUpdateTimer()
        {
            if(updateTimer != null)
            {
                updateTimer.Stop();
            }
        }

        /// <summary>
        /// ネクストタイマースタート
        /// </summary>
        /// <param name=""></param>
        /// <param name=""></param>
        private void startNextTimer(Double pInterval)
        {
            //すでに動作中なら破棄しておく
            
            //作成されていなければ作成する。開始済みなら、一旦止める
            if (nextTimer == null)
            {
                this.nextTimer = new Timer();
                this.nextTimer.Elapsed += new ElapsedEventHandler(onNext);
                this.nextTimer.Interval = pInterval;
            }
            else
            {
                this.stopNextTimer();
            }

            //タイマースタート
            this.nextTimer.Start();            
        }
        private void stopNextTimer()
        {
            //すでに起動していたら破棄する
            if (nextTimer != null)
            {
                nextTimer.Stop();
            }
        }


        /*
        おしゃべりタイマー
        10秒間隔で実行
        おしゃべり中は停止する
        */
        private void onNext(object sender, ElapsedEventArgs e)
        {
            //次の話題
            this.nextLiplis(NEXT_LIPLIS_MODE_AUTO);
        }


        /// <summary>
        /// おしゃべりタイマー
        /// 0.1間隔で実行
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void onUpdate(object sender, ElapsedEventArgs e)
        {
            try
            {
                //おしゃべり中なら、Liplisアップデート
                if (this.flgAlarm == 12)
                {
                    this.updateLiplis();
                }
            }
            catch(Exception err)
            {
                LpsLogController.writingLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, err.ToString());

                Console.WriteLine(err);

            }

        }


        /// <summary>
        /// アップデートカウントのリセット
        /// </summary>
        public void reSetUpdateCount()
        {
            //無口の場合はタイマーを更新しない
            if (this.setting.lpsMode == 3)
            {
                //すでに動作中なら停止しておく
                this.stopNextTimer();
                return;
            }

            //チャットトーク時は、60秒のインターバルを設定
            if(!this.flgChatTalk)
            {
                this.startNextTimer(this.setting.lpsInterval);
            }
            else
            {
                this.startNextTimer(60000);
            }
        }
        //public void reSetUpdateCountEveryoneTlak()
        //{
        //    //無口の場合はタイマーを更新しない
        //    if (this.setting.lpsMode == 3)
        //    {
        //        //すでに動作中なら停止しておく
        //        this.stopNextTimer();
        //        return;
        //    }

        //    //チャットトーク時は、60秒のインターバルを設定
        //    this.startNextTimer(60000);
        //}

        /// <summary>
        /// リーブタイマーのスタート
        /// </summary>
        private void startLeaveTimer()
        {
            if (leaveTimer == null)
            {
                this.leaveTimer = new Timer();
                this.leaveTimer.Elapsed += new ElapsedEventHandler(onLeave);
                this.leaveTimer.Interval = 10000;
            }
            else
            {
                this.stopLeaveTimer();
            }

            leaveTimer.Start();
        }
        private void stopLeaveTimer()
        {
            //すでに起動していたら破棄する
            if (leaveTimer != null)
            {
                leaveTimer.Stop();
            }
        }

        /// <summary>
        /// リーブ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void onLeave(object sender, ElapsedEventArgs e)
        {
            //タイマーオフ
            stopLeaveTimer();

            //ディスプレイアイコンONでなければアイコンをオフにする
            if(!LpsLiplisUtil.bitToBool(setting.lpsDisplayIcon))
            {
                Dispatcher.Invoke(new Action(() =>
                {
                    iconOff();
                }));
            }
        }
        

        //============================================================
        //
        //イベントハンドラ
        //
        //============================================================

        /// <summary>
        /// ボディマウスダウン
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void image_MouseDown(object sender, MouseButtonEventArgs e)
        {
            //ボタン処理
            if (e.RightButton == MouseButtonState.Pressed)
            {
                rightButtonDown();
            }
            else
            {
                leftButtonDown();
            }
        }

        /// <summary>
        /// 左ボタンが押された時
        /// </summary>
        private void leftButtonDown()
        {
            //ベースレクと生成
            Rect windowRect = new Rect(0, 0, this.Width, this.Height);

            //クリックアニメ
            WpfAnimation.imageClickDownAnimeation(this, windowRect, this.image);

            //ドラッグ
            try
            {
                DragMove();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

            //ウインドウの追随
            setWidgetLocation();

            //座標記録
            setting.setLocation((int)this.Left, (int)this.Top);

            onNextClick();

            //クリック終了時アニメ
            WpfAnimation.imageClickUpAnimeation(this, windowRect, this.image);
        }

        /// <summary>
        /// 右ボタンが押された時
        /// </summary>
        private void rightButtonDown()
        {
            if(!flgIconOn)
            {
                iconOn();
            }
            else
            {
                if (!LpsLiplisUtil.bitToBool(setting.lpsDisplayIcon))
                {
                    iconOff();
                }
            }
        }

        /// <summary>
        /// ボディクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void image_MouseUp(object sender, MouseButtonEventArgs e)
        {
            
        }

        private void icoEnd_MouseDown(object sender, MouseButtonEventArgs e)
        {

        }

        /// <summary>
        /// 終了ボタン押下
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void icoEnd_MouseUp(object sender, MouseButtonEventArgs e)
        {
            //削除フラグON
            flgWidgetDelete = true;

            //グッバイメッセージ
            goodBaydLiplis();
        }

        /// <summary>
        /// 終了ボタンの色付け
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void icoEnd_MouseEnter(object sender, MouseEventArgs e)
        {
            try
            {
                Dispatcher.Invoke(new Action(() =>
                {
                    this.icoEnd.Source = new BitmapImage(new Uri("pack://application:,,,/Resources/ico_win_wnd_on.png"));
                }));
            }
            catch
            {

            }
        }

        private void icoEnd_MouseLeave(object sender, MouseEventArgs e)
        {
            try
            {
                Dispatcher.Invoke(new Action(() =>
                {
                    this.icoEnd.Source = new BitmapImage(new Uri("pack://application:,,,/Resources/ico_win_wnd.png"));
                }));
            }
            catch
            {

            }
        }

        private void icoMinimize_MouseDown(object sender, MouseButtonEventArgs e)
        {
            
        }

        /// <summary>
        /// 最小化ボタン押下
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void icoMinimize_MouseUp(object sender, MouseButtonEventArgs e)
        {
            this.WindowState = WindowState.Minimized;   
        }

        /// <summary>
        /// 最小化ボタンの色付け
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void icoMinimize_MouseEnter(object sender, MouseEventArgs e)
        {
            Dispatcher.Invoke(new Action(() =>
            {

                this.icoMinimize.Source = new BitmapImage(new Uri("pack://application:,,,/Resources/ico_win_min_on.png"));
            }));
        }
        private void icoMinimize_MouseLeave(object sender, MouseEventArgs e)
        {
            Dispatcher.Invoke(new Action(() =>
            {
                this.icoMinimize.Source = new BitmapImage(new Uri("pack://application:,,,/Resources/ico_win_min.png"));
            }));
        }

        /// <summary>
        /// 状態変化時
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_StateChanged(object sender, EventArgs e)
        {
            //TODO: Window_StateChanged ひとまずコメント ウインドウ状態をどうするか決定する
            //this.talkWindow.WindowState = this.WindowState;
        }



        /*
        各パーツをボディに追随させる
        */
        private void  setWidgetLocation()
        {
            if(windowManager.nowWindowIsNull())
            {
                return;
            }

            //TODO: Window_StateChanged ひとまずコメント どうやってウインドウをついづいさせるか決定する
            Dispatcher.BeginInvoke(new Action(() =>
            {
                windowManager.nowTalkWindow.setWindowLocation((Int32)this.Top, (Int32)this.Left, (Int32)this.Width);

            }));
        }

        /// <summary>
        /// おやすみボタン押下時処理
        /// </summary>
        public void onClickSleep()
        {
            //おやすみチェック
            if (this.flgSitdown)
            {
                //おやすみ中なら、起こす
                this.wakeup();
            }
            else
            {
                //通常なら、おやすみ
                this.sleep();
            }
        }

        /// <summary>
        /// ネクストアイコン押下時処理
        /// </summary>
        public void onNextClick()
        {
            //チャット中チェック
            if (!flgChatting)
            {
                nextLiplis(NEXT_LIPLIS_MODE_MANUAL);
            }
            //2015/09/04 Liplis4.5.4 連打対策
            else if (flgChatting && flgSkip)
            {
                flgSkip = false;
                nextLiplis(NEXT_LIPLIS_MODE_MANUAL);
            }
            else
            {
                flgSkip = true;
            }
        }

        /// <summary>
        /// ウィジェットをデスクトップ内に復帰させる
        /// </summary>
        /// <param name="offset"></param>
        public void rescue(Int32 offset)
        {
            int h = System.Windows.Forms.Screen.PrimaryScreen.Bounds.Height;     //ディスプレイの高さ
            int w = System.Windows.Forms.Screen.PrimaryScreen.Bounds.Width;      //ディスプレイの幅

            if (this.Top > h)
            {
                this.Top = h - this.Height;
            }
            if (this.Top < 0)
            {
                this.Top = 0;
            }

            if (this.Left > w)
            {
                this.Left = w - this.Width;
            }

            if (this.Left < 0)
            {
                this.Left = 0;
            }

            //パーツの位置調整
            this.setWidgetLocation();

            //座標セーブ
            this.saveLocation();
        }

        /// <summary>
        /// 座標をセーブルする
        /// </summary>
        private void saveLocation()
        {
            this.setting.locationX = (Int32)this.Left;
            this.setting.locationY = (Int32)this.Top;
            this.setting.setPreferenceData();
        }

        /// <summary>
        /// ウインドウからマウスカーソルが出た時の処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_MouseLeave(object sender, MouseEventArgs e)
        {
            if (!LpsLiplisUtil.bitToBool(setting.lpsDisplayIcon))
            {
                startLeaveTimer();
            }
        }

        /// <summary>
        /// ウインドウにマウスカーソルが入った時の処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_MouseEnter(object sender, MouseEventArgs e)
        {
            stopLeaveTimer();
        }



        //============================================================
        //
        //ボタンイベント
        //
        //============================================================

        /// <summary>
        /// スリープボタンイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void icoSleep_MouseDown(object sender, MouseButtonEventArgs e)  {iconAnimationButtonOn(sender);}
        private void icoSleep_MouseLeave(object sender, MouseEventArgs e)       {iconAnimationButtonOff(sender);}
        private void icoSleep_MouseUp(object sender, MouseButtonEventArgs e)
        {
            iconAnimationButtonOff(sender);

            //UPのときにクリックされたと判定
            onClickSleep();
        }

        /// <summary>
        /// ログボタンイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void icoLog_MouseDown(object sender, MouseButtonEventArgs e)    {iconAnimationButtonOn(sender);}
        private void icoLog_MouseLeave(object sender, MouseEventArgs e)         {iconAnimationButtonOff(sender);}
        private void icoLog_MouseUp(object sender, MouseButtonEventArgs e)
        {
            iconAnimationButtonOff(sender);

            //ログ画面を表示する
            desk.openViewLog();
        }


        /// <summary>
        /// 設定ボタンイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void icoSetting_MouseDown(object sender, MouseButtonEventArgs e){iconAnimationButtonOn(sender);}
        private void icoSetting_MouseLeave(object sender, MouseEventArgs e){iconAnimationButtonOff(sender);}
        private void icoSetting_MouseUp(object sender, MouseButtonEventArgs e)
        {
            iconAnimationButtonOff(sender);

            //設定画面を開く
            callWidgetSetting();
        }


        /// <summary>
        /// チャットボタンイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void icoChat_MouseDown(object sender, MouseButtonEventArgs e){iconAnimationButtonOn(sender);}
        private void icoChat_MouseLeave(object sender, MouseEventArgs e){iconAnimationButtonOff(sender);}
        private void icoChat_MouseUp(object sender, MouseButtonEventArgs e)
        {
            iconAnimationButtonOff(sender);

            //チャットウインドウを開く
            callChatWindow();
        }


        /// <summary>
        /// クロックボタンイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void icoClock_MouseDown(object sender, MouseButtonEventArgs e){iconAnimationButtonOn(sender);}
        private void icoClock_MouseLeave(object sender, MouseEventArgs e){iconAnimationButtonOff(sender);}
        private void icoClock_MouseUp(object sender, MouseButtonEventArgs e)
        {
            iconAnimationButtonOff(sender);

            //時刻情報おしゃべり
            clockInfo();
        }


        /// <summary>
        /// バッテリーボタンイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void icoBattery_MouseDown(object sender, MouseButtonEventArgs e){iconAnimationButtonOn(sender);}
        private void icoBattery_MouseLeave(object sender, MouseEventArgs e){iconAnimationButtonOff(sender);}
        private void icoBattery_MouseUp(object sender, MouseButtonEventArgs e)
        {
            iconAnimationButtonOff(sender);

            //バッテリー情報おしゃべり
            batteryInfo();
        }

        /// <summary>
        /// アイコンをおした時のアニメーション
        /// </summary>
        /// <param name="sender"></param>
        private void iconAnimationButtonOn(object sender)
        {
            //イメージ取得
            Image image = (Image)sender;

            //アイコンイメージ取得
            LiplisIconImage lii = (LiplisIconImage)image.Tag;

            //クリックアニメ
            WpfAnimation.imageClickDownAnimeation(this, new Rect(lii.xMargin, lii.yMargin, lii.iconSide, lii.iconSide), image);

            //ONフラグ設定
            lii.buttonOn = true;
        }

        /// <summary>
        /// アイコンからマウスボタンを離したときのアニメーション
        /// </summary>
        /// <param name="sender"></param>
        private void iconAnimationButtonOff(object sender)
        {
            //イメージ取得
            Image image = (Image)sender;

            //アイコンイメージ取得
            LiplisIconImage lii = (LiplisIconImage)image.Tag;

            //クリック終了時アニメ
            if (lii.buttonOn) { WpfAnimation.imageClickUpAnimeation(this, new Rect(lii.xMargin, lii.yMargin, lii.iconSide, lii.iconSide), image); }


            //ONフラグ設定
            lii.buttonOn = false;
        }

        //============================================================
        //
        //定型おしゃべり
        //
        //============================================================

        /// <summary>
        /// 挨拶
        /// </summary>
        public void greet()
        {
            //挨拶メッセージ取得
            this.liplisNowTopic = this.skin.xmlChat.getGreet(LpsDefine.CHAT_DEF_GREET);

            if (this.liplisNowTopic.getMessage() == "")
            {
                this.liplisNowTopic = new MsgTalkMessage(name: "なうろーでぃんぐ♪", emotion: 0, point: 0);
            }

            //チャット情報の初期化
            this.initChatInfo();

            //チャットスタート
            this.chatStart();
        }

        /// <summary>
        /// バッテリー情報おしゃべり
        /// </summary>
        private void batteryInfo()
        {
            //座り中なら回避
            if (this.flgSitdown) { return; }

            //挨拶の選定
            this.liplisNowTopic = this.skin.xmlChat.getBatteryInfo((Int32)(this.lpsBattery.getBatteryRatel()), this.lpsBattery.batteryExists);

            //チャット情報の初期化
            this.initChatInfo();

            //チャットスタート
            this.chatStart();
        }

        /// <summary>
        /// 時刻情報おしゃべり
        /// windows版には時計おしゃべりなし
        /// </summary>
        /// <returns></returns>
        private void clockInfo()
        {
            //座り中なら回避
            if (this.flgSitdown) { return; }

            //挨拶の選定
            this.liplisNowTopic = this.skin.xmlChat.getClockInfo();

            //チャット情報の初期化
            this.initChatInfo();

            //チャットスタート
            this.chatStart();
        }

        /// <summary>
        /// 時報チェック
        /// </summary>
        /// <returns></returns>
        private bool onTimeSignal()
        {
            bool result = false;
        
            //現在時間取得
            Int32 nowHour = DateTime.Now.Hour;
        
            if(nowHour != this.prvHour)
            {
                //座り中なら回避
                if(this.flgSitdown){ return false; }

                //時報取得
                this.liplisNowTopic = this.skin.xmlChat.getTimeSignal(nowHour);
            
                //時報データが無い場合は時報をおしゃべりしない
                if (this.liplisNowTopic == null)
                {
                    this.prvHour = nowHour;
                    return false;
                }

                //チャット情報の初期化
                this.initchatInfo();

                //おしゃべりスタート
                this.chatStart();

                result = true;
            }

            //前回時刻のセット
            this.prvHour = nowHour;

            return result;
        }

        /// <summary>
        /// goodBay
        /// さようなら
        /// </summary>
        #region goodBay
        protected void goodBay()
        {
            //挨拶の選定
            liplisNowTopic = skin.xmlChat.getGreet(LpsDefine.CHAT_DEF_GOODBYE);

            //空だったらろーでぃんぐなう♪
            if (liplisNowTopic.getMessage().Equals(""))
            {
                liplisNowTopic = new MsgTalkMessage("終了いたします。", 0, 0);
            }

            //チャット情報の初期化
            initChatInfo();

            //おしゃべりスレッドスタート
            chatStart();
        }
        #endregion

        /// <summary>
        /// 話しかけ取得
        /// </summary>
        /// <param name="chatText"></param>
        public void chatTalkRecive(MsgTalkMessage chatMessage)
        {
            //座り中なら回避
            if (this.flgSitdown) { return; }

            this.flgChatTalk = true;

            //挨拶の選定
            this.liplisNowTopic = chatMessage;

            //チャット情報の初期化
            this.initChatInfo();

            //チャットスタート
            this.chatStart();
        }




        //============================================================
        //
        //おしゃべり準備処理
        //
        //============================================================

        /// <summary>
        /// チャット情報の初期化
        /// </summary>
        private void initchatInfo()
        {
            //チャットテキストの初期化
            this.liplisChatText = "";

            //ナウワードの初期化
            this.liplisNowWord = "";

            //ナウ文字インデックスの初期化
            this.cntLct = 0;

            //なうワードインデックスの初期化
            this.cntLnw = 0;
        }

        /// <summary>
        /// 話題残量チェック
        /// </summary>
        /// <returns></returns>
        private bool checkRunout()
        {
            //return (self.os.lpsNewsRunOut == 1) && self.lpsNews.checkNewsQueueCount(self.getPostDataForLiplisNewsList())
            return true;
        }

        //============================================================
        //
        //おしゃべり処理
        //
        //============================================================

        /// <summary>
        /// 次の話題
        /// mode:0 自動実行、1:手動実行
        /// </summary>
        private const int NEXT_LIPLIS_MODE_AUTO = 0;
        private const int NEXT_LIPLIS_MODE_MANUAL = 1;

        private void nextLiplis(int mode)
        {
            //暫定
            if (NEXT_LIPLIS_MODE_AUTO == mode && desk.lpsGilsTalk.flgChatting)
            {
                return;
            }

            //個々みきたらチャッティングはOFFにしてみる
            desk.lpsGilsTalk.flgChatting = false;

            this.flgAlarm = 0;
            Console.WriteLine("flgAlarm0:nextLiplis:" + skin.charName);

            //バッテリーチェック
            Dispatcher.BeginInvoke(new Action(() =>
            {
                this.lpsIcoBattery.setImage(this.skin.xmlWindow.getBatteryIcon(this.lpsBattery.getBatteryRatel()));
            }));
            
            //おすわりチェック
            if (this.flgSitdown)
            {
                this.stopNextTimer();
                this.stopUpdateTimer();
                return;
            }
            

            //URLの初期化
            this.liplisUrl = "";

            //モードチェック
            if (this.setting.lpsMode == 0 || this.setting.lpsMode == 1 || this.setting.lpsMode == 2 || this.setting.lpsMode == 3)
            {
                //時報チェック
                if (this.onTimeSignal())
                {
                    return;
                }

                //みんなでおしゃべりチェック
                if (this.setting.lpsTalkMode == (int)LPS_TALK_MODE.EVERYONE)
                {
                    //みんなでおしゃべり
                    if(NEXT_LIPLIS_MODE_MANUAL == mode ||( NEXT_LIPLIS_MODE_AUTO == mode && !desk.lpsGilsTalk.flgChatting))
                    {
                        this.runLiplisEveryone();
                    }
                    
                    return;
                }
                else
                {
                    //一人でおしゃべり
                    //次の話題おしゃべり
                    this.runLiplis();
                }

            }
            else if (this.setting.lpsMode == 4)
            {
                //時計＆バッテリー(windows版ではなし)
            }
        }

        /// <summary>
        /// 次の話題スタート
        /// </summary>
        private void runLiplis()
        {
            //チャット中なら終了する
            if (this.flgChatting) { this.chatStop(); return; }

            //座り中なら回避
            if (this.flgSitdown) { this.chatStop(); return; }

            //ウインドウチェック ウインドウが消える仕様は保留する
            //windowsOn()

            //クロックチェック
            //windows版では時計表示なし

            //立ち絵をデフォルトに戻す
            this.setObjectBodyNeutral();

            //トピックを取得する
            this.getTopic();

            //チャット情報の初期化
            this.initChatInfo();

            //チャットスタート
            this.chatStart();

            //ネクストタイマーを止めておく
            this.stopNextTimer();
        }

        /// <summary>
        /// みんなでおしゃべり 次の話題スタート
        /// </summary>
        private void runLiplisEveryone()
        {
            try
            {
                //座り中なら回避
                if (this.flgSitdown) { this.chatStop(); return; }

                //トピックを取得する
                this.nextEveryoneTopic();
                Console.WriteLine("nextTalkDelivery:nextTopicRequest runLiplisEveryone");
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        /// <summary>
        /// リプリスの更新
        /// </summary>
        private void updateLiplis()
        {
            try
            {
                if (this.setting.lpsSpeed != 0)
                {

                    refreshLiplis();
                }
                else
                {
                    this.immediateRefresh();
                }

            }
            catch(Exception ex)
            {
                checkEnd();
            }
        }

        /// <summary>
        /// リフレッシュ
        /// </summary>
        private void refreshLiplis()
        {
            System.Threading.Thread.Sleep(10);

            //キャンセルフェーズ
            if (this.checkMsg()){ return; }

            //おすわりチェック
            if (this.checkSitdown()){ return; }

            //タグチェック ひとまず保留
            //if checkTag(){}

            //スキップチェック
            if (this.checkSkip())
            {
                //this.updateText();
                return;
            }

            //ナウワード取得、ナウテキスト設定
            if (this.setText()) { return; }

            //テキストの設定
            this.updateText();

            //ボディの更新
            this.updateBody();
        }

        /// <summary>
        /// 瞬時リフレッシュ
        /// </summary>
        private void immediateRefresh()
        {
            //キャンセルフェーズ
            if (this.checkMsg()){ return; }

            //スキップ
            this.skipLiplis();

            //テキストの設定
            this.updateText();

            //ボディの更新
            this.updateBody();

            //終了
            this.checkEnd();
        }

        /// <summary>
        /// メッセージチェック
        /// </summary>
        /// <returns></returns>
        private bool checkMsg()
        {
            //現在の話題が破棄されていれば、アップデートカウントをリセットする
            if (this.liplisNowTopic == null)
            {
                //途中で話題がNULLになる対策
                if (this.setting.lpsTalkMode == (int)LPS_TALK_MODE.EVERYONE)
                {
                    //次の人にバトンを渡す
                    batonTouch();
                }

                this.reSetUpdateCount();
                return true;
            }
            return false;
        }
    
        /// <summary>
        /// スキップチェック
        /// </summary>
        /// <returns></returns>
        private bool checkSkip()
        {
            bool result = false;

            //スキップチェック
            if (this.flgSkip){
                //スキップ処理中有効
                this.flgSkipping = true;

                //チャット停止
                this.chatStop();


                //音声おしゃべりをストップさせる
                if (setting.lpsVoiceOn == 1)
                {
                    stopSpeechText();
                }

                //スキップ処理
                result = this.skipLiplis();

                //スキップ処理中終了
                this.flgSkipping = false;
            }
            return result;
        }

        /// <summary>
        /// スキップ
        /// </summary>
        /// <returns></returns>
        private bool skipLiplis()
        {
            try
            {
                if (liplisNowTopic != null && !liplisNowTopic.result.Equals(""))
                {
                    skipText();

                    //終端設定
                    cntLnw = liplisNowTopic.nameList.Count;
                    cntLct = liplisNowWord.Length;

                    //チャットを中断する
                    checkEnd();

                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception err)
            {
                LpsLogController.writingLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, err.ToString());

                //終端設定
                cntLnw = liplisNowTopic.nameList.Count;
                cntLct = liplisNowWord.Length;

                return false;
            }

        }

        /// <summary>
        /// 座りチェック
        /// </summary>
        /// <returns></returns>
        private bool checkSitdown()
        {
            if (this.flgSitdown)
            {
                this.liplisNowTopic = null;
                this.updateBodySitDown();
                return true;
            }
            return false;
        }

        /// <summary>
        /// 終了チェック
        /// </summary>
        /// <returns></returns>
        private bool checkEnd()
        {
            if (this.cntLnw >= this.liplisNowTopic.nameList.Count)
            {

                //ログ出力
                desk.addLog(new MsgTalkMessageLog(this.liplisNowTopic, this.liplisChatText),this.skin,setting);

                //チャットストップ
                this.chatStop();

                //半めチェック
                this.herfEyeCheck();

                //トピック初期化
                this.liplisNowTopic = null;

                //終了フラグがONなら、終了する
                if (flgEnd) { Dispose(); }

                return true;
            }
            return false;
        }

        /// <summary>
        /// テキストの更新
        /// </summary>
        /// <returns></returns>
        private bool setText()
        {
            try
            {
                //送りワード文字数チェック
                if (this.cntLnw != 0)
                {
                    if (this.cntLct >= this.liplisNowWord.Length)
                    {
                        //終了チェック
                        if (this.checkEnd()) { return true; }

                        //チャットテキストカウントの初期化
                        this.cntLct = 0;

                        //なうワードの初期化
                        this.liplisNowWord = this.liplisNowTopic.nameList[cntLnw];

                        //プレブエモーションセット
                        this.prvEmotion = this.nowEmotion;

                        //なうエモーションの取得
                        this.nowEmotion = this.liplisNowTopic.emotionList[this.cntLnw];

                        //なうポイントの取得
                        this.nowPoint = this.liplisNowTopic.pointList[this.cntLnw];

                        //インデックスインクリメント
                        this.cntLnw = this.cntLnw + 1;
                    }
                }
                else if (this.cntLnw == 0)
                {
                    //チャットテキストカウントの初期化
                    this.cntLct = 0;

                    //空チェック
                    if (this.liplisNowTopic.nameList.Count == 0)
                    {
                        this.checkEnd();
                        return true;
                    }

                    //なうワードの初期化
                    this.liplisNowWord = this.liplisNowTopic.nameList[this.cntLnw];

                    //次ワード遷移
                    this.cntLnw = this.cntLnw + 1;

                    //空だったら、空じゃなくなるまで繰り返す
                    if (this.liplisNowWord == "")
                    {
                        while (this.liplisNowWord == "")
                        {
                            //チェックエンド
                            checkEnd();

                            //終了チェック
                            if (this.cntLnw > this.liplisNowTopic.nameList[cntLnw].Length) { break; }

                            //ナウワードの初期化
                            this.liplisNowWord = this.liplisNowTopic.nameList[this.cntLnw];

                            //次ワード遷移
                            this.cntLnw = this.cntLnw + 1;
                        }
                    }
                }
                else
                {

                }

                //今回送り出し文字
                string nowSendChar = this.liplisNowWord.Substring(this.cntLct, 1);

                //アットマーク検出(新規ウインドウ追加)
                if (nowSendChar == "@")
                {
                    //アットマークは送らず、ウインドウ生成
                    addNewWindow();
                }
                else
                {
                    //おしゃべり
                    this.liplisChatText = this.liplisChatText + nowSendChar;
                }


                //LCTカウントインクリメント
                this.cntLct = this.cntLct + 1;

                return false;
            }
            catch (Exception err)
            {
                LpsLogController.writingLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, err.ToString());
                return false;
            }
        }

        /// <summary>
        /// テキストビューの更新
        /// </summary>
        public void updateText()
        {
            try
            {
                if (liplisChatText.Equals(""))
                {
                    return;
                }

                //テキスト出力
                if (liplisNowTopic.result.Length > cntLct - 1)
                {
                    //ウインドウテキスト出力
                    windowManager.updateText(liplisChatText);

                    //位置自動調整
                    setWidgetLocation();
                }

            }
            catch (Exception err)
            {
                LpsLogController.writingLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, err.ToString());
            }
        }

        /// <summary>
        /// MethodType : child
        /// MethodName : skipText
        /// テキストの更新
        /// </summary>
        #region skipText
        protected bool skipText()
        {
            try
            {
                //ウインドウ数
                int windowIdx = 0;

                //おしゃべり
                string zanSentence = liplisNowTopic.result.Remove(0, this.liplisChatText.Length);

                //残リスト生成
                List<string> zanList = new List<string>(zanSentence.Split('@'));

                //残リストを回して、ウインドウ生成
                foreach(string sentence in zanList)
                {
                    if(sentence == "")
                    {
                        continue;
                    }

                    if(windowIdx == 0)
                    {
                        //途中の文章に追加する
                        this.liplisChatText = this.liplisChatText + sentence;
                    }
                    else
                    {
                        //ウインドウアッド
                        addNewWindow();

                        //新しい文章をチャットテキストに設定
                        this.liplisChatText = sentence;
                    }

                    //描画
                    this.windowManager.updateSkip(this.liplisChatText);

                    //インデックスインクリメント
                    windowIdx++;
                }

                //位置自動調整
                setWidgetLocation();

                return true;
            }
            catch (Exception err)
            {
                LpsLogController.writingLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, err.ToString());
                return false;
            }
        }
        #endregion

        /// <summary>
        /// 音声おしゃべり
        /// </summary>
        protected void speechText()
        {
            if (liplisNowTopic != null)
            {
                if (lvr != null)
                {
                    //おしゃべりする場合、一つ前をキャンセルしてからおしゃべり
                    lvr.callStopButtonDown();

                    //キューに入れる
                    lvr.addMessage(liplisNowTopic.sorce.Replace("@", ""));
                }
            }
        }

        /// <summary>
        /// 音声おしゃべりを停止する
        /// </summary>
        protected void stopSpeechText()
        {
            if (liplisNowTopic != null)
            {
                if (lvr != null)
                {
                    lvr.callStopButtonDown();
                }
            }
        }
        /// <summary>
        /// ボディの更新
        /// </summary>
        /// <returns></returns>
        public bool updateBody()
        {
            try
            {
                //感情変化セット
                this.setObjectBody();

                //口パクカウント
                if (this.flgChatting)
                {
                    if (this.cntMouth == 1) { this.cntMouth = 2; }
                    else { this.cntMouth = 1; }
                }
                else
                {
                    this.cntMouth = 1;
                }

                //めぱちカウント
                if (this.cntBlink == 0) { this.cntBlink = this.getBlincCnt(); }
                else { this.cntBlink = this.cntBlink - 1; }

                //ボディ設定
                Dispatcher.BeginInvoke(new Action(() =>
                {
                    this.image.Source = new BitmapImage(new Uri(this.lpsBody.getBodyPath(this.getBlinkState(), this.cntMouth)));
                }));


                //成功
                return true;
            }
            catch (Exception err)
            {
                lpsBody = skin.xmlBody.getLiplisBody(0, 0);
                LpsLogController.writingLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, err.ToString());
                return false;
            }
        }


        /// <summary>
        /// ボディの更新
        /// </summary>
        private void updateBodySitDown()
        {
            //this.imgBody.image = self.lpsBody.sleep;
            Dispatcher.Invoke(new Action(() =>
            {
                this.image.Source = new BitmapImage(new Uri(this.skin.xmlBody.sleep().getBodyPath11()));
            }));
        }

        /// <summary>
        /// みんなでおしゃべり時のバトンタッチ
        /// </summary>
        private void batonTouch()
        {
            desk.lpsGilsTalk.nextTalkDelivery();
        }

        /// <summary>
        /// みんなでおしゃべりカウントセット
        /// </summary>
        /// <param name="val"></param>
        /// <param name="max"></param>
        public void everyoneCountSet(int val, int max)
        {
            windowManager.everyoneCountSet(val, max);
        }


        //============================================================
        //
        //ボディ制御
        //
        //============================================================

        /// <summary>
        /// ボディの取得
        /// </summary>
        /// <returns></returns>
        public bool setObjectBody()
        {
            if (this.nowEmotion != this.prvEmotion && this.flgChatting)
            {
                if (this.setting.lpsHealth == 1 && lpsBattery.getBatteryRatel() < 75)
                {
                    //ヘルス設定ONでバッテリー残量75%以下なら、小破以下の画像取得
                    this.lpsBody = skin.xmlBody.getLiplisBodyHelth((int)lpsBattery.getBatteryRatel(), nowEmotion, nowPoint);
                }
                else
                {
                    //現在の感情値、感情レベルからボディを一つ取得
                    this.lpsBody = skin.xmlBody.getLiplisBody(nowEmotion, nowPoint);
                    //Console.WriteLine("c" + prvEmotion + " : " + nowEmotion);
                }


                //取得成功
                return true;
            }

            return false;
        }

        /// <summary>
        /// ボディを初期状態に戻す
        /// </summary>
        /// <returns></returns>
        private bool setObjectBodyNeutral()
        {
            this.cntMouth = 1;
            this.cntBlink = 0;
            this.nowEmotion = 0;
            this.nowPoint = 0;

            if (this.setting.lpsHealth == 1 && this.lpsBattery.getBatteryRatel() > 75)
            {
                //ヘルス設定ONでバッテリー残量が７５％以下なら小破以下の画像取得
                this.lpsBody = this.skin.xmlBody.getLiplisBodyHelth((Int32)this.lpsBattery.getBatteryRatel(),this.nowEmotion, this.nowPoint);
            }
            else
            {
                this.lpsBody = this.skin.xmlBody.getLiplisBody(this.nowEmotion, point: this.nowPoint);
            }

            //reqReflesh()

            return true;
        }
        
        /// <summary>
        /// まばたきカウント
        /// </summary>
        /// <returns></returns>
        private Int32 getBlincCnt()
        {
            return LpsLiplisUtil.getRandamInt(30, 50);
        }

        /// <summary>
        /// まばたき状態取得
        /// </summary>
        /// <returns></returns>
        private Int32 getBlinkState()
        {
            switch (this.cntBlink)
            {
                case 0:
                    return 1;
                case 1:
                    return 2;
                case 2:
                    return 3;
                case 3:
                    return 2;
                default:
                    return 1;
            }
        }

        /// <summary>
        /// 半目チェック
        /// </summary>
        private void herfEyeCheck()
        {
            if (this.cntBlink == 1 || this.cntBlink == 3)
            {
                this.cntBlink = 0;
                this.updateBody();
            }
        }


        //============================================================
        //
        //チャット制御
        //
        //============================================================

        /// <summary>
        /// チャットスタート
        /// </summary>
        private void  chatStart()
        {
            //nullチェック
            if(liplisNowTopic == null)
            {
                chatStop();
            }

            //チャット中フラグON
            this.flgChatting = true;

            //開いているウインドウを終了して、ウインドウを初期化する
            createFirstWindow();

            //瞬時表示チェック
            if (this.setting.lpsSpeed == 3)
            {
                this.immediateRefresh();
            }
            else
            {
                //実行モードを12に設定する
                this.flgAlarm = 12;

                //更新タイマーをONする
                this.startUpdateTimer();

                //音声おしゃべり
                if (setting.lpsVoiceOn == 1)
                {
                    speechText();
                }
            }
        }
        private void chatStartEveryone()
        {
            //nullチェック
            if (liplisNowTopic == null)
            {
                chatStop();
            }

            //チャット中フラグON
            this.flgChatting = true;

            //瞬時表示チェック
            if (this.setting.lpsSpeed == 3)
            {
                this.immediateRefresh();
            }
            else
            {
                //実行モードを12に設定する
                this.flgAlarm = 12;

                //更新タイマーをONする
                this.startUpdateTimer();

                //音声おしゃべり
                if (setting.lpsVoiceOn == 1)
                {
                    speechText();
                }
            }
        }

        /// <summary>
        /// チャットストップ
        /// </summary>
        public void chatStop()
        {
            try
            {
                //アラームIDを0に設定
                this.flgAlarm = 0;

                //チャット終了
                this.flgChatting = false;

                //スキップフラグも寝かせる
                this.flgSkip = false;

                if (!flgEnd)
                {
                    //みんなでおしゃべりモードの場合、バトンタッチ
                    //if (this.setting.lpsTalkMode == (int)LPS_TALK_MODE.EVERYONE && flgEveryoneTalk)
                    if (this.setting.lpsTalkMode == (int)LPS_TALK_MODE.EVERYONE && flgEveryoneTalk)
                    {
                        //次の人にバトンを渡す
                        batonTouch();
                    }
                }

                //アップデートカウントをリセットする
                this.reSetUpdateCount();

                //みんなでおしゃべりフラグを寝かす
                flgEveryoneTalk = false;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        /// <summary>
        /// エブリワンからチャットを止める
        /// </summary>
       public void chatStopFromEveryone()
        {
            //アラームIDを0に設定
            this.flgAlarm = 0;

            //チャット終了
            this.flgChatting = false;

            //スキップフラグも寝かせる
            this.flgSkip = false;

            //アップデートカウント更新
            //reSetUpdateCountEveryoneTlak();

            //みんなでおしゃべりフラグを寝かす
            flgEveryoneTalk = false;

            //チャットクローズ
            //closeWindow();
        }

        /// <summary>
        /// おしゃべり速度計算
        /// </summary>
        /// <returns></returns>
        protected int chatSpeedCulc()
        {
            return (int)((100 - setting.lpsSpeed) * 1.66 + 33);
        }

        /// <summary>
        /// チャットスピード変更
        /// </summary>
        public void chatSpeedChange()
        {
            //チャットスピード計算
            updateTimer.Interval =chatSpeedCulc();
        }

        //============================================================
        //
        //ウインドウ制御
        //
        //============================================================



        /// <summary>
        /// ファーストウインドウを表示する
        /// </summary>
        private void createFirstWindow()
        {                //新しいウインドウを生成する
            Dispatcher.BeginInvoke(new Action(() =>
            {
                try
                {

                    if (this.liplisNowTopic == null)
                    {
                        //話題が無いときは何もしない
                    }
                    else if (this.liplisNowTopic.url == "")
                    {
                        windowManager.createFirstWindow(this.Top, this.Left, this.Width);
                    }
                    else if (this.liplisNowTopic.url != "" && this.liplisNowTopic.jpgUrl != "")
                    {
                        windowManager.createTitleWindow(this.Top, this.Left, this.Width, this.Height, this.liplisNowTopic.title, this.liplisNowTopic.url, this.liplisNowTopic.jpgUrl);
                    }
                    else
                    {
                        windowManager.createTitleWindow(this.Top, this.Left, this.Width, this.Height, this.liplisNowTopic.title, this.liplisNowTopic.url, "");
                    }
                }
                catch(Exception ex)
                {
                    chatStop();
                    return;
                }

            }));

            //あらかじめ位置を合わせておく
            setWidgetLocation();
        }

        /// <summary>
        /// 新しいウインドウを追加する
        /// </summary>
        private void addNewWindow()
        {
            //なうウインドウの移動
            Dispatcher.Invoke(new Action(() =>
            {
                windowManager.addNewWindow(this.Top,this.Left,this.Width,this.Height);

                desk.addLog(new MsgTalkMessageLog(this.liplisNowTopic, this.liplisChatText), this.skin, setting);
            }));

            //チャットテキストの初期化
            this.liplisChatText = "";

            //あらかじめ位置を合わせておく
            setWidgetLocation();
        }

        /// <summary>
        /// ウインドウを閉じる
        /// </summary>
        public void closeWindow()
        {
            Dispatcher.BeginInvoke(new Action(() =>
            {
                try
                {
                    windowManager.closeWindowList();
                    windowManager.closeEveryoneTitleWindow();
                }
                catch (Exception ex)
                {
                    chatStop();
                    return;
                }

            }));
        }
        public void closeEveryoneWindow()
        {
            Dispatcher.BeginInvoke(new Action(() =>
            {
                try
                {
                    windowManager.closeEveryoneTitleWindow();
                }
                catch (Exception ex)
                {
                    chatStop();
                    return;
                }

            }));
        }

        //============================================================
        //
        //話題管理
        //
        //============================================================

        /// <summary>
        ///  1件のトピックを取得する
        /// </summary>
        private void getTopic()
        {
            this.flgChatTalk = false;
            this.flgThinking = true;

            this.getShortNews();

            this.flgThinking = false;
        }

        /// <summary>
        /// ニュースキューチェック
        /// </summary>
        private void onCheckNewsQueue()
        {
            if (!this.flgSitdown)
            {
                this.lpsNews.checkNewsQueue();
            }
        }

        /// <summary>
        /// ニュースを取得する
        /// </summary>
        private void getShortNews()
        {
            this.liplisNowTopic = this.lpsNews.getSummaryNewsFromQ();

            //取得失敗メッセージ
            if (this.liplisNowTopic == null)
            {
                // TODO:失敗したら、次のインターバルは長くしたい
                this.liplisNowTopic = LiplisFaildMessage.getMessage();
                liplisUrl = "";
            }
            else
            {
                this.liplisUrl = this.liplisNowTopic.url;
            }
        }

        /// <summary>
        ///  次のみんなでおしゃべりを要求する
        /// </summary>
        private void nextEveryoneTopic()
        {
            this.desk.lpsGilsTalk.nextTopicRequest();
        }

        /// <summary>
        /// みんなでおしゃべり話題セット
        /// </summary>
        public void setTopicEveryone(MsgTalkMessage nextTalk)
        {
            //みんなでおしゃべり
            this.flgEveryoneTalk = true;

            //話題取得
            this.liplisNowTopic = nextTalk;

            //チャット情報の初期化
            this.initChatInfo();

            //チャットスタート
            this.chatStartEveryone();
        }




        //============================================================
        //
        //状態制御
        //
        //============================================================

        /// <summary>
        /// おはよう
        /// </summary>
        public void wakeup()
        {
            //おやすみ状態の場合、ウェイクアップ
            this.flgSitdown = false;

            //アイコン変更
            this.lpsIcoSleep.setImage(this.skin.xmlWindow.ICO_ZZZ);

            //あいさつ
            this.greet();
        }

        /// <summary>
        /// おやすみ
        /// </summary>
        public void sleep()
        {
            //ウェイクアップ状態の場合、おやすみ
            this.flgSitdown = true;

            //ヴォイスロイドストップ
            if (setting.lpsVoiceOn == 1)
            {
                lvr.callStopButtonDown();
            }

            //アイコン変更
            this.lpsIcoSleep.setImage(this.skin.xmlWindow.ICO_WAIKUP);

            Dispatcher.Invoke(new Action(() =>
            {
                this.windowManager.closeWindowList();
            }));

            //おやすみの立ち絵に変更
            this.updateBodySitDown();
        }

        /// <summary>
        /// アイコン表示
        /// </summary>
        private void iconOn()
        {
            this.flgIconOn = true;

            WpfAnimation.opacityUp(this, icoMinimize);
            WpfAnimation.opacityUp(this, icoEnd);
            WpfAnimation.opacityUp(this, icoSleep);
            WpfAnimation.opacityUp(this, icoLog);
            WpfAnimation.opacityUp(this, icoSetting);
            WpfAnimation.opacityUp(this, icoChat);
            WpfAnimation.opacityUp(this, icoClock);
            WpfAnimation.opacityUp(this, icoBattery);

            WpfAnimation.opacityUp(this, canClock);
            WpfAnimation.opacityUp(this, HourLine);
            WpfAnimation.opacityUp(this, MinuteLine);
            WpfAnimation.opacityUp(this, SecondLine);
        }

        /// <summary>
        /// アイコン非表示
        /// </summary>
        private void iconOff()
        {
            this.flgIconOn = false;

            WpfAnimation.opacityDown(this, icoMinimize);
            WpfAnimation.opacityDown(this, icoEnd);
            WpfAnimation.opacityDown(this, icoSleep);
            WpfAnimation.opacityDown(this, icoLog);
            WpfAnimation.opacityDown(this, icoSetting);
            WpfAnimation.opacityDown(this, icoChat);
            WpfAnimation.opacityDown(this, icoClock);
            WpfAnimation.opacityDown(this, icoBattery);

            WpfAnimation.opacityDown(this, canClock);
            WpfAnimation.opacityDown(this, HourLine);
            WpfAnimation.opacityDown(this, MinuteLine);
            WpfAnimation.opacityDown(this, SecondLine);
        }

        /// <summary>
        /// サイズを設定する
        /// </summary>
        /// <param name="height"></param>
        /// <param name="width"></param>
        private void setSize(int height, int width)
        {
            this.Height = height;
            this.Width = width;
            this.imageGrid.Height = height;
            this.imageGrid.Width = width;
            this.image.Height = height;
            this.image.Width = width;
        }

        /// <summary>
        /// 最小化する
        /// </summary>
        public void minimize()
        {
            Dispatcher.Invoke(new Action(() =>
            {
                this.WindowState = WindowState.Minimized;
                this.sleep();

                 this.windowManager.closeWindowList();
            }));

        }

        //============================================================
        //
        //ほか画面呼び出し
        //
        //============================================================
        #region ほか画面呼び出し
        /// <summary>
        /// ウィジェット設定画面を呼び出す
        /// </summary>
        private void callWidgetSetting()
        {
            if (viewWidgetSetting == null)
            {
                viewWidgetSetting = new ViewLiplisWidgetSetting(this, this.desk.baseSetting, this.setting, this.skin);
            }
            else
            {
                if (viewWidgetSetting.IsDisposed)
                {
                    viewWidgetSetting = new ViewLiplisWidgetSetting(this, this.desk.baseSetting, this.setting, this.skin);
                }
            }

            viewWidgetSetting.Show();
        }

        /// <summary>
        /// チャット画面を呼び出す
        /// </summary>
        private void callChatWindow()
        {
            if (viewChat == null)
            {
                viewChat = new ViewChat(this);
            }
            else
            {
                if (viewChat.IsDisposed)
                {
                    viewChat = new ViewChat(this);
                }
            }

            viewChat.Show();
        }

        /// <summary>
        /// みんなでおしゃべり時のタイトルウインドウを表示する
        /// </summary>
        public void callEveryoneTitleWindow(string title, string url, string jpgUrl)
        {
            Dispatcher.Invoke(new Action(() =>
            {
                if (url != "" && jpgUrl != "")
                {
                    windowManager.createEveryoneTitleWindow(this.Top, this.Left, this.Width, this.Height, title, url, jpgUrl);
                }
                else
                {
                    windowManager.createEveryoneTitleWindow(this.Top, this.Left, this.Width, this.Height, title, url, "");
                }
            }));

        }

        /// <summary>
        /// みんなでおしゃべり時のタイトルウインドウを消去する
        /// </summary>
        public void closeEveryoneTitleWindow()
        {
            Dispatcher.Invoke(new Action(() =>
            {
                this.windowManager.closeEveryoneTitleWindow();
            }));        }

        #endregion



    }
}
