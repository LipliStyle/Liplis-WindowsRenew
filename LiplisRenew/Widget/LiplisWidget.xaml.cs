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
using Liplis.Gui;
using Liplis.MainSystem;
using Liplis.Msg;
using Liplis.Utl;
using Liplis.Wpf;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

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
        private Skin skin;
        private LiplisWindow window;

        //=================================
        //ロードボディオブジェクト
        private LiplisBody lpsBody;

        //=================================
        //デスクトップインスタンス
        private ViewDeskTop desk;

        //=================================
        //ニュースインスタンス
        //private LiplsNews lpsNews;
        private LiplisBattery lpsBattery;
        //private LiplisApiChat lpsChatTalk;

        //=================================
        //タイマー
        private Timer updateTimer;
        private Timer nextTimer;
        private Timer startMoveTimer;
        private Int32 flgAlarm = 0;
    
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
        private bool flgConnect     = false;   //接続フラグ
        private bool flgBodyChencge = false;   //ボディ変更フラグ
        private bool flgChatting    = false;   //おしゃべり中フラグ
        private bool flgSkip        = false;   //スキップフラグ
        private bool flgSkipping    = false;   //スキップ中フラグ
        private bool flgSitdown     = false;   //おすわり中フラグ
        private bool flgThinking    = false;   //考え中フラグ
        private bool flgEnd         = false;   //おしゃべり終了フラグ
        private bool flgTag         = false;   //タグチェック
        private bool flgChatTalk    = false;   //
        private bool flgDebug       = false;   //
        private bool flgOutputDemo  = false;   //

        ///=====================================
        /// 設定値
        private Int32 liplisInterval = 100;		    //インターバル
        private Int32 liplisRefresh  = 10;          //リフレッシュレート

        ///=====================================
        /// 時報制御
        private Int32 prvHour = 0;

        /// <summary>
        /// コンストラクター
        /// </summary>
        public LiplisWidget(ViewDeskTop desk, LiplisWidgetPreference setting, Skin skin)
        {
            try
            {
                InitializeComponent();

                //設定データ
                this.setting = setting;

                //デスクトップクラス取得
                this.desk = desk;

                //スキンデータ取得
                this.skin = skin;

                //ビューの初期化;
                this.initView();

                //クラスの初期化
                this.initClass();

                //あいさつ
                this.greet();
            }
            catch (Exception ex)
            {
                initErrorEnd(ex);
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

                //TODO: LiplisWidget initClass モードによっては話題収集の実行が必要
                //TODO: LiplisWidget 話題収集はデスクトップクラスで行い、そこからしゅとくするようにする。
                //話題収集の実行
                //this.onCheckNewsQueue();

                //TODO: LiplisWidget initClass バッテリーオブジェクトの作成

                //バッテリーオブジェクトの初期化
                this.lpsBattery = new LiplisBattery();
            }
            catch (Exception ex)
            {
                initErrorEnd(ex);
            }
        }

        /// <summary>
        /// 画面要素の初期化
        /// </summary>
        private void initView()
        {
            try
            {
                //ウインドウの初期化
                this.window = new LiplisWindow();

                this.window.Show();

                //サイズ設定
                setSize(skin.xmlBody.height, skin.xmlBody.width);

                //アイコン設定
                this.icoSleep.Source = new BitmapImage(new Uri(this.skin.xmlWindow.ICO_ZZZ));
                this.icoLog.Source = new BitmapImage(new Uri(this.skin.xmlWindow.ICO_LOG));
                this.icoSetting.Source = new BitmapImage(new Uri(this.skin.xmlWindow.ICO_SETTING));
                this.icoChat.Source = new BitmapImage(new Uri(this.skin.xmlWindow.ICO_INTRODUCTION));
                this.icoClock.Source = new BitmapImage(new Uri(this.skin.xmlWindow.ICO_BACK));
                this.icoBattery.Source = new BitmapImage(new Uri(this.skin.xmlWindow.BATTERY_100));

                //ロケーション設定
                setWidgetLocation();
            }
            catch(Exception ex)
            {
                initErrorEnd(ex);
            }
        }

        /// <summary>
        /// 初期化時のエラー 終了処理
        /// </summary>
        /// <param name="ex"></param>
        private void initErrorEnd(Exception ex)
        {
            //エラーメッセージ作成
            StringBuilder message = new StringBuilder();

            message.AppendLine("ウィジェットの初期化に失敗しました。Skinに問題がある可能性があります。");
            message.Append("エラー原因:");
            message.Append(ex.Message);

            //エラーメッセージ
            LpsMessage.showError(message.ToString());

            //ウィジェットを閉じる
            this.Close();
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

            //設定の保存
            this.setting.saveSettings();

            //閉じる
            this.Close();
        }

        /// <summary>
        /// グッバイリプリス
        /// </summary>
        public void goodBaydLiplis()
        {
            //既に終了処理が走っている場合は無効
            if (flgEnd) { return; }

            //アップデートカウントをほぼ無限に設定
            nextTimer.Interval = 99999;

            //チャットを停止しておく
            chatStop();

            //終了フラグ有効
            flgEnd = true;

            //お別れの挨拶セット
            goodBay();
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
                this.updateTimer.Interval = liplisInterval;
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
                this.nextTimer.Interval = 10000;
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
            //アイコン表示オフなら、アイコンを消去する
            if(this.setting.lpsDisplayIcon == 0)
            {
                this.iconOff();
            }

            //次の話題
            this.nextLiplis();
        }


        /// <summary>
        /// おしゃべりタイマー
        /// 0.1間隔で実行
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void onUpdate(object sender, ElapsedEventArgs e)
        {
            //おしゃべり中なら、Liplisアップデート
            if (this.flgAlarm == 12)
            {
                this.updateLiplis();
            }
        }


        /// <summary>
        /// アップデートカウントのリセット
        /// </summary>
        private void reSetUpdateCount()
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
                this.startNextTimer(60.0);
            }
        }

        //============================================================
        //
        //イベントハンドラ
        //
        //============================================================


        /// <summary>
        /// ドラッグムーブ
        /// </summary>
        /// <param name="e"></param>
        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonDown(e);
            DragMove();

            //ウインドウの追随
            setWidgetLocation();
        }

        /*
        各パーツをボディに追随させる
        */
        private void  setWidgetLocation()
        {
            window.setWindowLocation((Int32)this.Top, (Int32)this.Width);
        }

        /*
        おやすみボタン押下時処理
        */
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

        /*
        ウィジェットをデスクトップ内に復帰させる
        */
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

        /*
        座標をセーブルする
        */
        private void saveLocation()
        {
            this.setting.locationX = (Int32)this.Left;
            this.setting.locationY = (Int32)this.Top;
            this.setting.setPreferenceData();
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


        //TODO: LiplisWidget 話しかけ処理 要実装
        /// <summary>
        /// 話しかけ取得
        /// </summary>
        /// <param name="chatText"></param>
        private void chatTalkRecive(string chatText)
        {
            ////座り中なら回避
            //if (this.flgSitdown) { return; }

            //this.flgChatTalk = true;

            ////挨拶の選定
            //this.liplisNowTopic = lpsChatTalk.apiPost(this.desk.baseSetting.lpsUid, toneUrl: this.skin.xmlSkin.tone, version: "I" + LiplisUtil.getAppVersion(), chatText);

            ////チャット情報の初期化
            //this.initChatInfo();

            ////チャットスタート
            //this.chatStart();
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
        /// </summary>
        private void nextLiplis()
        {
            this.flgAlarm = 0;

            //TODO: LiplisWidget バッテリーアイコン実装時　バッテリーアイコン変更処理
            //バッテリーチェック
            //this.icoBattery.setImage(this.lpsIcon.getBatteryIcon(this.lpsBattery.getBatteryRatel()), forState: UIControlState.Normal);

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
            if (this.setting.lpsMode == 0 || this.setting.lpsMode == 1 || this.setting.lpsMode == 2)
            {
                //時報チェック
                if (this.onTimeSignal())
                {
                    return;
                }

                //話題がつきたかチェック
                if (this.checkRunout())
                {
                    //再カウント
                    reSetUpdateCount();
                    return;
                }

                //次の話題おしゃべり
                this.runLiplis();
            }
            else if (this.setting.lpsMode == 3)
            {
                this.runLiplis();
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

            //アイコンカウント
            //iconCloseCheck()

            //立ち絵をデフォルトに戻す
            this.setObjectBodyNeutral();

            //トピックを取得する
            this.getTopic();

            //チャット情報の初期化
            this.initChatInfo();

            //チャットスタート
            this.chatStart();
        }

        /// <summary>
        /// リプリスの更新
        /// </summary>
        private void updateLiplis()
        {
            //設定速度に応じて動作をウェイトさせる
            switch (this.setting.lpsSpeed)
            {
                case 0:    //最速　常に実行
                    if (this.flgAlarm != 0) { this.refreshLiplis(); }
                    break;
                case 1:    //普通　１回休む
                        if (this.cntSlow >= 1)
                        {
                            this.refreshLiplis();
                            this.cntSlow = 0;
                        }
                        else
                        {
                            this.cntSlow = this.cntSlow + 1;
                        }
                        break;
                case 2:    //おそい　２回休む
                        if (this.cntSlow >= 2)
                        {
                            refreshLiplis();
                            this.cntSlow = 0;
                        }
                        else
                        {
                            this.cntSlow = this.cntSlow + 1;
                        }
                        break;
                case 3:    //瞬間表示
                        this.immediateRefresh();
                        break;
                default:   //ほかの値が設定された場合は瞬間表示とする
                    this.immediateRefresh();
                break;
            }
        }

        /// <summary>
        /// リフレッシュ
        /// </summary>
        private void refreshLiplis()
        {
            //キャンセルフェーズ
            if (this.checkMsg()){ return; }

            //おすわりチェック
            if (this.checkSitdown()){ return; }

            //タグチェック ひとまず保留
            //if checkTag(){}

            //スキップチェック
            if (this.checkSkip())
            {
                this.updateText();
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
                //TODO: LiplisWidget ログの送信 ログウィンドウ実装時、実装する
                //this.desk.lpsLog.append(this.liplisChatText, url: this.liplisNowTopic.url);
                this.chatStop();
                this.herfEyeCheck();
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
            ////送りワード文字数チェック
            //if(self.cntLnw != 0)
            //{
            //    if(self.cntLct >= self.liplisNowWord.utf16.count)
            //    {
            //        //終了チェック
            //        if(self.checkEnd()){return true}

            //        //チャットテキストカウントの初期化
            //        self.cntLct = 0

            //        //なうワードの初期化
            //        self.liplisNowWord = self.liplisNowTopic.nameList[cntLnw]

            //        //プレブエモーションセット
            //        self.prvEmotion = self.nowEmotion

            //        //なうエモーションの取得
            //        self.nowEmotion = self.liplisNowTopic.emotionList[self.cntLnw]

            //        //なうポイントの取得
            //        self.nowPoint = self.liplisNowTopic.pointList[self.cntLnw]

            //        //インデックスインクリメント
            //        self.cntLnw = self.cntLnw + 1
            //    }
            //}
            //else if(self.cntLnw == 0)
            //{
            //    //チャットテキストカウントの初期化
            //    self.cntLct = 0

            //    //空チェック
            //    if self.liplisNowTopic.nameList.count == 0
            //    {
            //        self.checkEnd()
            //        return true
            //    }

            //    //なうワードの初期化
            //    self.liplisNowWord = self.liplisNowTopic.nameList[self.cntLnw]

            //    //次ワード遷移
            //    self.cntLnw = self.cntLnw + 1

            //    //空だったら、空じゃなくなるまで繰り返す
            //    if(self.liplisNowWord == "")
            //{
            //    repeat
            //                {
            //        //チェックエンド
            //        checkEnd()

            //                    //終了チェック
            //        if (self.cntLnw > self.liplisNowTopic.nameList[cntLnw].utf16.count) { break}

            //        //ナウワードの初期化
            //        self.liplisNowWord = self.liplisNowTopic.nameList[self.cntLnw]

            //                    //次ワード遷移
            //        self.cntLnw = self.cntLnw + 1
            //                }
            //    while (self.liplisNowWord == "")
            //            }
            //        }
            //        else
            //        {

            //        }
            //        //おしゃべり
            //        self.liplisChatText = self.liplisChatText + (self.liplisNowWord as NSString).substringWithRange(NSRange(location : self.cntLct, length : 1))
            //        self.cntLct = self.cntLct + 1

            return false;
        }

        /// <summary>
        /// テキストビューの更新
        /// </summary>
        public void updateText()
        {
            //TODO: LiplisWidget テキストウィンドウ実装完了時、実装する

            //try
            //{
            //    if (liplisChatText.Equals(""))
            //    {
            //        return true;
            //    }

            //    //テキスト出力
            //    if (liplisNowTopic.result.Length > cntLct - 1)
            //    {
            //        Invoke(new LpsDelegate.dlgS1ToVoid(at.setTextTotalkWindow), liplisChatText);
            //        Invoke(new LpsDelegate.dlgI2ToVoid(at.setEmotionWindow), nowEmotion, nowPoint);
            //    }

            //}
            //catch (Exception err)
            //{
            //    LpsLogController.writingLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, err.ToString());
            //}
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
                //送り
                while (liplisNowTopic.result.Length > cntLct)
                {
                    setText();
                }

                //描画
                updateText();

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
                Dispatcher.Invoke(new Action(() =>
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
            //this.imgBody.image = null;
            //this.imgBody.image = self.lpsBody.sleep;
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
                    Console.WriteLine("c" + prvEmotion + " : " + nowEmotion);
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
            }
        }

        /*
        チャットストップ
        */
        private void chatStop()
        {
            this.flgAlarm = 0;

            this.reSetUpdateCount();

            this.flgChatting = false;
            this.flgSkip = false;
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


        //TODO: LiplisWidget ニュースキューチェック処理 要実装
        /// <summary>
        /// ニュースキューチェック
        /// </summary>
        private void onCheckNewsQueue()
        {
            //if (!this.flgSitdown)
            //{
            //    this.lpsNews.checkNewsQueue(this.getPostDataForLiplisNewsList());
            //}
        }

        //TODO: LiplisWidget ニュース取得処理 要実装
        /// <summary>
        /// ニュースを取得する
        /// </summary>
        private void getShortNews()
        {
            //this.liplisNowTopic = this.lpsNews.getShortNews(this.getPostDataForLiplisNews());
    
            ////取得失敗メッセージ
            //if (this.liplisNowTopic == null)
            //{
            //    this.liplisNowTopic = FctLiplisMsg.createMsgMassageDlFaild();
            //    liplisUrl = "";
            //}
            //else
            //{
            //    this.liplisUrl = this.liplisNowTopic.url;
            //}

        }

        /// <summary>
        /// ポストパラメーターの作成(ニュース単体向け)
        /// </summary>
        /// <returns></returns>
        private NameValueCollection getPostDataForLiplisNews()
        {
            NameValueCollection nameValuePair = new NameValueCollection();
            nameValuePair.Add("tone", this.skin.xmlSkin.toneUrl);                       //TONE_URLの指定
            nameValuePair.Add("newsFlg", this.setting.getNewsFlg());                    //NEWS_FLGの指定
            nameValuePair.Add("randomkey",DateTime.Now.ToString("yyyyyMMddHHmmss"));    //キャッシュ防止
            return nameValuePair;
        }

        /// <summary>
        /// ポストパラメーターの作成(ニュースリスト向け)
        /// </summary>
        /// <returns></returns>
        private NameValueCollection getPostDataForLiplisNewsList()
        {
            NameValueCollection nameValuePair = new NameValueCollection();
            nameValuePair.Add("userid",         desk.baseSetting.lpsUid);                       //UIDLの指定
            nameValuePair.Add("tone",           this.skin.xmlSkin.toneUrl);                     //TONE_URLの指定
            nameValuePair.Add("newsFlg",        this.setting.getNewsFlg());                     //ニュースフラグの指定
            nameValuePair.Add("num",            "100");                                         //個数
            nameValuePair.Add("hour",           this.setting.lpsNewsRange.ToString());          //時間範囲の指定
            nameValuePair.Add("already",        this.setting.lpsNewsAlready.ToString());        //オールレディ
            nameValuePair.Add("twitterMode",    this.setting.lpsTopicTwitterMode.ToString());   //ツイッターモード
            nameValuePair.Add("runout",         this.setting.lpsNewsRunOut.ToString());         //ランアウト

            return nameValuePair;
        }

        //============================================================
        //
        //状態制御
        //
        //============================================================

        //TODO: LiplisWidget おはよう処理 要実装
        /// <summary>
        /// おはよう
        /// </summary>
        public void wakeup()
        {
            //おやすみ状態の場合、ウェイクアップ
            this.flgSitdown = false;

            //アイコン変更
            //this.icoSleep.setImage(self.lpsIcon.imgSleep, forState: UIControlState.Normal);

            //あいさつ
            this.greet();
        }

        //TODO: LiplisWidget おやすみ処理 要実装
        /// <summary>
        /// おやすみ
        /// </summary>
        public void sleep()
        {
            //ウェイクアップ状態の場合、おやすみ
            this.flgSitdown = true;

            //アイコン変更
            //self.icoSleep.setImage(self.lpsIcon.imgWakeup, forState: UIControlState.Normal);

            //表示テキスト変更
            //this.lblLpsTalkLabel.text = "zzz";

            //おやすみの立ち絵に変更
            this.updateBodySitDown();
        }

        /// <summary>
        /// アイコン表示
        /// </summary>
        private void iconOn()
        {
            //TODO: LiplisWidgetアイコンを作成したら、処理を作成する
        }

        /// <summary>
        /// アイコン非表示
        /// </summary>
        private void iconOff()
        {
            //TODO: LiplisWidgetアイコンを作成したら、処理を作成する
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

        //============================================================
        //
        //ほか画面呼び出し
        //
        //============================================================
        /// <summary>
        /// ウィジェット設定画面を呼び出す
        /// </summary>
        private void callWidgetSetting()
        {
            //TODO: LiplisWidget 設定画面とアイコンを作成したら、処理を作成する
        }

        /// <summary>
        /// チャット画面を呼び出す
        /// </summary>
        private void callChat()
        {
            //TODO: LiplisWidget チャット画面とアイコンを作成したら、処理を作成する
        }

    }
}
