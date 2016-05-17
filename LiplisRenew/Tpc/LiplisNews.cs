//=======================================================================
//  ClassName : LiplisNews
//  概要      : ニュースオブジェクト
//
//  Liplis5.0
//
//  Copyright(c) 2010-2016 LipliStyle.Sachin
//=======================================================================
using Liplis.MainSystem;
using Liplis.Msg;
using Liplis.Talk;
using Liplis.Utl;
using Liplis.Web.Clalis;
using System;
using System.Collections.Concurrent;
using System.Reflection;
using System.Threading.Tasks;

namespace Liplis.Tpc
{
    public class LiplisNews
    {
        ///=====================================
        /// 必須情報
        private LiplisPreference baseSetting;
        private LiplisWidgetPreference setting;
        private string toneUrl;

        ///=====================================
        /// 話題キュー
        protected ConcurrentQueue<MsgTalkMessage> singleNewsQ;

        private const Int32 LPS_NEWS_QUEUE_HOLD_CNT = 25;   //ニュースキューの最低保持件数
        private const Int32 LPS_NEWS_QUEUE_GET_CNT = 50;  //ニュースキューの取得件数
        private const Int32 UPDATE_INTERVAL = 60;

        ///=====================================
        /// 前回取得日時
        private DateTime prvTime;

        ///=============================
        /// フラグ
        protected bool flgCollect = false;

        public object FctLiplisMsg { get; private set; }

        /// <summary>
        /// コンストラクター
        /// </summary>
        /// <param name="setting"></param>
        public LiplisNews(LiplisPreference baseSetting, LiplisWidgetPreference setting, string toneUrl)
        {
            //設定の取得
            this.baseSetting = baseSetting;
            this.setting = setting;
            this.toneUrl = toneUrl;

            //ニュースキューの初期化
            singleNewsQ = new ConcurrentQueue<MsgTalkMessage>();
        }

        /// <summary>
        /// Qからサマリーニュースを一つ取得する
        /// </summary>
        /// <returns></returns>
        public MsgTalkMessage getSummaryNewsFromQ()
        {
            //ニュースキューチェック
            if (singleNewsQ.Count > 0)
            {
                if (singleNewsQ.Count <= 25)
                {
                    //ニュースキューの収集
                    collectTaskRun();
                }

                MsgTalkMessage result;

                if (singleNewsQ.TryDequeue(out result))
                {
                    return result;
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

                //2014/01/07 ver3.2.1 話題が尽きた時の挙動の修正
                if (setting.lpsNewsRunOut == 0)
                {
                    return getSummaryNews();
                }
                else
                {
                    return null;
                }

            }
        }

        /// <summary>
        /// 単発でサマリーニュースを取得する
        /// </summary>
        /// <returns></returns>
        public MsgTalkMessage getSummaryNews()
        {
            MsgTalkMessage result;
            try
            {
                result = ClalisForLiplis.getSummaryNews(baseSetting.uid, toneUrl, setting.getNewsFlg());

                if (result == null)
                {
                    result = LiplisFaildMessage.getMessage();
                }

                return result;
            }
            catch (Exception err)
            {
                LpsLogController.d(this.GetType().Name + ":" + MethodBase.GetCurrentMethod().Name + ":" + err.ToString());
                return LiplisFaildMessage.getMessage();
            }
        }


        public void checkNewsQueue()
        {
            if (this.singleNewsQ.Count < LPS_NEWS_QUEUE_HOLD_CNT)
            {
                if ((DateTime.Now - this.prvTime).TotalSeconds > UPDATE_INTERVAL)
                {
                    collectTaskRun();
                }
            }
        }

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

                ClalisForLiplis.getSummaryNewsList(singleNewsQ, baseSetting.uid, toneUrl, setting.getNewsFlg(), "100", setting.lpsTopicHour.ToString(), setting.lpsAlready.ToString(), "1", setting.lpsNewsRunOut.ToString());
            }
            catch
            {

            }
            finally
            {
                //完了時OFF
                flgCollect = false;
            }
        }








    }
}
