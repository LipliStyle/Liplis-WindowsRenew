//=======================================================================
//  ClassName : LiplisGilsTalk
//  概要      : ガールズトークの管理をおこなう
//
//  Liplis5.0
//
//  Copyright(c) 2010-2016 LipliStyle.Sachin
//=======================================================================
using Clalis.v50.Msg;
using Liplis.Activity;
using Liplis.Com;
using Liplis.Com.Defile;
using Liplis.MainSystem;
using Liplis.Msg;
using Liplis.Talk;
using Liplis.Utl;
using Liplis.Web.Clalis;
using Liplis.Widget;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
namespace Liplis.Tpc
{
    public class LiplisGilsTalk
    {
        ///=====================================
        /// 必須情報
        private LiplisPreference baseSetting;       //ベース設定
        public List<LiplisWidget> widgetList;
        private ViewDeskTop desk;

        ///=====================================
        /// 話題キュー
        protected ConcurrentQueue<string> newsIdQ;
        protected ConcurrentQueue<MsgGilsTalk> gilsTalkQ;

        ///=====================================
        /// 前回取得日時
        private DateTime prvTime;

        ///=============================
        /// フラグ
        protected bool flgCollect = false;          //データ収集中
        public bool flgChatting = false;         //会話中

        ///=============================
        /// 会話データ
        private MsgGilsTalk liplisNowTalk;                                  //現在ロードおしゃべりデータ 
        public LpsTable<LiplisWidget> everyoneTalkWidgetList;               //有効ウィジェット
        private int nowTalkId = 0;

        ///=============================
        /// json保存ファイル
        public const string GAILS_TALK_JSON_DATA = "gailsTalk.json";

        //============================================================
        //
        //初期化処理
        //
        //============================================================
        #region 初期化処理

        /// <summary>
        /// コンストラクター
        /// </summary>
        /// <param name="setting"></param>
        public LiplisGilsTalk(List<LiplisWidget> widgetList, LiplisPreference baseSetting, ViewDeskTop desk)
        {
            //設定の取得
            this.widgetList = widgetList;
            this.baseSetting = baseSetting;
            this.desk = desk;

            //ニュースキューの初期化
            newsIdQ = new ConcurrentQueue<string>();
            gilsTalkQ = new ConcurrentQueue<MsgGilsTalk>();

            //トークウィジェットリスト作成
            createEveryoneTalkWidgetList();

            //トークデータをロードする
            loadJson();

            //初期収集開始
            collectReplacementAllTaskRun();
        }

        /// <summary>
        /// みんなでおしゃべり設定されているウィジェットリスト
        /// </summary>
        public void createEveryoneTalkWidgetList()
        {
            everyoneTalkWidgetList = new LpsTable<LiplisWidget>();

            foreach (var widget in widgetList)
            {
                if (widget.setting.lpsTalkMode == (int)LPS_TALK_MODE.EVERYONE)
                {
                    everyoneTalkWidgetList.Add(widget.setting.key,widget);
                }
            }

        }
        #endregion


        //============================================================
        //
        //テーブル操作
        //
        //============================================================
        #region テーブル操作
        /// <summary>
        /// 有効リストに追加する
        /// </summary>
        /// <param name="widget"></param>
        public void addWidgetEveryoneTalkWidgetList(LiplisWidget widget)
        {
            //ウィジェット追加
            everyoneTalkWidgetList.Add(widget.setting.key, widget);

            //データを取り直す
            collectReplacementAllTaskRun();
        }

        /// <summary>
        /// 有効リストから削除する
        /// </summary>
        /// <param name="widget"></param>
        public void removeWidgetEveryoneTalkWidgetList(LiplisWidget widget)
        {
            //ウィジェット削除
            everyoneTalkWidgetList.Remove(widget.setting.key);

            //データを取り直す
            collectReplacementAllTaskRun();
        }

        #endregion

        //============================================================
        //
        //データ取得
        //
        //============================================================
        #region データ取得

        /// <summary>
        /// Qからサマリーニュースを一つ取得する
        /// </summary>
        /// <returns></returns>
        public MsgGilsTalk getGilsTalkFromQ()
        {
            //ニュースキューチェック
            if (gilsTalkQ.Count > 0)
            {
                if (gilsTalkQ.Count <= 25)
                {
                    //ニュースキューの収集
                    collectTaskRun();
                }

                MsgGilsTalk newsId;

                if (gilsTalkQ.TryDequeue(out newsId))
                {
                    //return ClalisForLiplis.getGalsTalkDataFromNewsId(createMsgGirlsTalkSendDataSpecifyNewsId(newsId));
                    return newsId;
                }
                else
                {
                    return null;
                }
            }
            else
            {
                //収集要請
                collectTaskRun();

                return getGilsTalk();
            }
        }


        /// <summary>
        /// 単発でサマリーニュースを取得する
        /// この処理は同期処理で動かさないとデットロックする！
        /// </summary>
        /// <returns></returns>
        public MsgGilsTalk getGilsTalk()
        {
            MsgGilsTalk result;
            try
            {
                result = ClalisForLiplis.getGalsTalkDataRandom(createMsgGirlsTalkSendData());

                if (result == null)
                {
                    result = getFaildData();
                }

                return result;
            }
            catch (Exception err)
            {
                LpsLogController.d(this.GetType().Name + ":" + MethodBase.GetCurrentMethod().Name + ":" + err.ToString());
                return getFaildData();
            }
        }

        /// <summary>
        /// 取得失敗メッセージを生成する
        /// </summary>
        /// <returns></returns>
        private MsgGilsTalk getFaildData()
        {
            MsgGilsTalk msg = new MsgGilsTalk();

            try
            {
                msg.newsId = "0";
                msg.title = "";
                msg.url = "";
                msg.jpgUrl = "";

                //URLリスト
                foreach (var item in getToneUrlList())
                {
                    msg.descriptionList.Add(LiplisFaildMessage.getMessage());
                }
            }
            catch
            { 

                
            }

            return msg;
        }

        #endregion

        //============================================================
        //
        //データ収集処理
        //
        //============================================================
        #region データ収集処理

        /// <summary>
        /// タスクでデータ収集を行う
        /// </summary>
        public void collectTaskRun()
        {
            if (!flgCollect)
            {
                Task.Run(() =>
                {
                    collect();
                });
            }
        }

        /// <summary>
        /// データ収集する
        /// </summary>
        public virtual void collect()
        {
            try
            {
                //開始時ON
                flgCollect = true;
                prvTime = DateTime.Now;

                //ClalisForLiplis.getGalsTalkNewsIdList(newsIdQ, createMsgGirlsTalkSendData());

                //Jsonを取得する
                string jsonText = ClalisForLiplis.getGalsTalkDataRandomListJson(createMsgGirlsTalkSendData());

                //Jsonをコンバートし、キューに入れる
                this.gilsTalkQ = ClalisForLiplis.getGalsTalkDataRandomListFromJson(jsonText);

                //Jsonを保存する
                saveJson(jsonText);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex);
            }
            finally
            {
                //完了時OFF
                flgCollect = false;
            }
        }


        /// <summary>
        /// 総入れ替え
        /// </summary>
        public void collectReplacementAllTaskRun()
        {
            Task.Run(() =>
            {
                collectReplacementAll();
            });
        }
        public virtual void collectReplacementAll()
        {
            try
            {
                //開始時ON
                flgCollect = true;
                prvTime = DateTime.Now;

                ConcurrentQueue<MsgGilsTalk> bufQ = new ConcurrentQueue<MsgGilsTalk>();

                //ClalisForLiplis.getGalsTalkNewsIdList(newsIdQ, createMsgGirlsTalkSendData());

                //Jsonを取得する
                string jsonText = ClalisForLiplis.getGalsTalkDataRandomListJson(createMsgGirlsTalkSendData());

                //Jsonをコンバートし、キューに入れる
                bufQ = ClalisForLiplis.getGalsTalkDataRandomListFromJson(jsonText);

                //バッファーキューをガールズトークQに入れる(置き換える)
                this.gilsTalkQ = bufQ;

                //Jsonを保存する
                if(jsonText != "{\"lstRes\":[]}")
                {
                    saveJson(jsonText);
                }
                
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            finally
            {
                //完了時OFF
                flgCollect = false;
            }
        }

        /// <summary>
        /// Jsonを保存する
        /// </summary>
        /// <param name="jsonText"></param>
        private void saveJson(string jsonText)
        {
            using (StreamWriter writer = new StreamWriter(LpsPathController.getSettingPath() + GAILS_TALK_JSON_DATA,false, Encoding.UTF8))
            {
                writer.Write(jsonText);
                writer.Flush();
            }
        }

        /// <summary>
        /// Jsonをロードする
        /// </summary>
        private void loadJson()
        {
            string jsonText = "";

            //ファイル存在チェック
            if(!File.Exists(LpsPathController.getSettingPath() + GAILS_TALK_JSON_DATA))
            {
                return;
            }

            //JSON読み込み
            using (StreamReader sr = new StreamReader(LpsPathController.getSettingPath() + GAILS_TALK_JSON_DATA, Encoding.UTF8))
            {
                jsonText = sr.ReadToEnd();
            }

            //Jsonをコンバートし、キューに入れる
            this.gilsTalkQ = ClalisForLiplis.getGalsTalkDataRandomListFromJson(jsonText, 20);
        }


        /// <summary>
        /// URLリストを取得する
        /// </summary>
        /// <returns></returns>
        public List<string> getToneUrlList()
        {
            //トーンURLリスト
            List<string> toneUrlList = new List<string>();

            //URLリストに変換
            foreach (var widget in everyoneTalkWidgetList.table)
            {
                toneUrlList.Add(widget.Value.skin.xmlSkin.toneUrl);
            }

            //トーンURLリストを返す
            return toneUrlList;
        }

        #endregion

        //============================================================
        //
        //送信データ生成
        //
        //============================================================
        #region 送信データ生成
        /// <summary>
        /// 送信データを生成する
        /// </summary>
        /// <returns></returns>
        public virtual msgGirlsTalkSendData createMsgGirlsTalkSendData()
        {
            //送信データ
            msgGirlsTalkSendData sendData = new msgGirlsTalkSendData();

            //URLリスト設定
            sendData.toneList = getToneUrlList();

            LiplisWidgetPreference lwp = desk.getMinnaRepresentativeSetting();

            if(lwp != null)
            {
                sendData.reqNum = 100;
                sendData.newsFlgBit = lwp.getNewsFlg();
                sendData.hour = lwp.lpsTopicHour;
            }
            else
            {
                sendData.reqNum = 100;
                sendData.newsFlgBit = "1111111";
                sendData.hour = 6;
            }

            return sendData;
        }

        /// <summary>
        /// ID指定データ生成
        /// </summary>
        /// <returns></returns>
        public virtual msgGirlsTalkSendDataSpecifyNewsId createMsgGirlsTalkSendDataSpecifyNewsId(string id)
        {
            //送信データ
            msgGirlsTalkSendDataSpecifyNewsId sendData = new msgGirlsTalkSendDataSpecifyNewsId();

            sendData.newsId = id;
            sendData.subId = "-1"; //通常-1
            sendData.reqNum = 100;
            sendData.toneList = getToneUrlList();

            return sendData;
        }
        #endregion

        //============================================================
        //
        //会話処理
        //
        //============================================================
        #region 会話処理




        public void nextTopic(LiplisWidget lips)
        {
            //0番目以外の要求は無視する
            if (everyoneTalkWidgetList[0].setting.key != lips.setting.key && !flgChatting)
            {
                return;
            }

            nextTopic();
        }
        public void nextTopic()
        {
            //すべてのおしゃべりを終了する
            widgetChatStop();

            //チャット中フラグON
            flgChatting = true;
            nowTalkId = 0;

            //会話実行中でなければ次の話題を取得し、会話処理開始
            liplisNowTalk = getGilsTalkFromQ();

            //情報セット
            callEveryoneTitleWindow();

            //次の話題配信
            nextTalkDelivery();
        }

        /// <summary>
        /// 次トピックのリクエストを出す
        /// </summary>
        public void nextTopicRequest()
        {
            //ナウトークがnullならチャット中解除
            if (liplisNowTalk == null)
            {
                flgChatting = false;
            }

            //会話実行中なら、要求を無視
            if (flgChatting)
            {
                nextTalkDelivery();
                return;
            }
            

            //要求する
            nextTopic();
        }

        /// <summary>
        /// みんなでおしゃべりタイトルウインドウ
        /// </summary>
        private void callEveryoneTitleWindow()
        {
            //ウインドウを出すウィジェットの番号をランダムでケテイ
            int targetIndex = LpsLiplisUtil.getRandamInt(0, everyoneTalkWidgetList.indexTable.Count);
            int idx = 0;

            //決定したウインドウを作成する
            everyoneTalkWidgetList[targetIndex].callEveryoneTitleWindow(liplisNowTalk.title, liplisNowTalk.url, liplisNowTalk.jpgUrl);

            //それ以外のウインドウは閉じる
            foreach (var widget in everyoneTalkWidgetList.table)
            {
                if(targetIndex != idx)
                {
                    widget.Value.closeEveryoneWindow();
                }
               
                idx++;
            }

            
        }

        /// <summary>
        /// 次のトークを要求する
        /// </summary>
        /// <returns></returns>
        public void nextTalkDelivery()
        {
            //会話実行中でなければ、要求を無視
            if (!flgChatting)
            {
                return;
            }

            //
            //widgetReSetUpdateCountEveryoneTlak();

            MsgTalkMessage talkData = new MsgTalkMessage();

            //情報取得
            bool flgSuccess = true;

            if (liplisNowTalk.descriptionList.Count > nowTalkId)
            {
                //トークデータ取得
                talkData = liplisNowTalk.descriptionList[nowTalkId];
                try
                {
                    //対象ウィジェット取得
                    if (everyoneTalkWidgetList.indexTable.Count > talkData.widgetIndex)
                    {
                        LiplisWidget widget = everyoneTalkWidgetList[talkData.widgetIndex];

                        //おしゃべり要求実行
                        widget.setTopicEveryone(talkData);

                        //カウントセット
                        everyoneCountSet();

                        //成功
                        flgSuccess = true;
                    }
                    else
                    {
                        //成功
                        flgSuccess = false;
                    }


                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);

                    //失敗
                    flgSuccess = false;
                }
            }
            else
            {
                //終了済みならスルー
                if (!flgChatting) { return; }

                //チャット終了
                chatEnd();
                return;
            }

            //IDインクリメント
            nowTalkId++;

            //成功可否
            if (!flgSuccess)
            {
                nextTalkDelivery();

            }            
        }

        /// <summary>
        /// チャット終了
        /// </summary>
        private void chatEnd()
        {
            //おしゃべり終了
            flgChatting = false;

            //カウントセット
            everyoneCountSet();

            //次のおしゃべりカウント
            everyoneTalkWidgetList[0].reSetUpdateCount();
        }


        /// <summary>
        /// アップデートカウントをリセットする
        /// </summary>
        //private void widgetReSetUpdateCountEveryoneTlak()
        //{
        //    //URLリストに変換
        //    foreach (var widget in everyoneTalkWidgetList.table)
        //    {
        //        widget.Value.reSetUpdateCountEveryoneTlak();
        //    }
        //}


        /// <summary>
        /// ウィジェットのおしゃべりを止める。
        /// </summary>
        private void widgetChatStop()
        {
            //URLリストに変換
            foreach (var widget in everyoneTalkWidgetList.table)
            {
                widget.Value.chatStopFromEveryone();
            }
        }

        /// <summary>
        /// ウィジェットウインドウを閉じる
        /// </summary>
        private void closeWidgetEveryoneWindow()
        {
            //URLリストに変換
            foreach (var widget in everyoneTalkWidgetList.table)
            {
                widget.Value.closeEveryoneWindow();
            }
        }

        /// <summary>
        /// 現在おしゃべり中の話題の件数を返す
        /// </summary>
        /// <returns></returns>
        public int getTalkDataAllCount()
        {
            try
            {
                return liplisNowTalk.descriptionList.Count;
            }
            catch
            {
                return 0;
            }
        }

        /// <summary>
        /// 現在おしゃべり中のインデックスを返す
        /// </summary>
        /// <returns></returns>
        public int getTalkDataIndex()
        {
            try
            {
                return nowTalkId;
            }
            catch
            {
                return 0;
            }
        }

        /// <summary>
        /// みんなでおしゃべりカウントセット
        /// </summary>
        private void everyoneCountSet()
        {
            foreach (var widget in everyoneTalkWidgetList.table)
            {
                widget.Value.everyoneCountSet(getTalkDataIndex(), getTalkDataAllCount());
            }
        }

        #endregion



    }
}
