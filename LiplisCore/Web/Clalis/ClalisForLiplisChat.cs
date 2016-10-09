//=======================================================================
//  ClassName : ClalisForLiplis
//  概要      : クラリスのLiplis向けAPIの呼び出し
//
//              非同期をベーシックに作り直し
//
//  Liplis5.0
//
//  Copyright(c) 2010-2016 LipliStyle.Sachin
//=======================================================================
using Clalis.v40.Res;
using Liplis.Com;
using Liplis.Msg;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Liplis.Web.Clalis
{
    public class ClalisForLiplisChat
    {
        ///=============================
        /// URL定義
        public const string LIPLIS_CHAT = @"http://liplis.mine.nu/Clalis/v40/Liplis/ClalisForLiplisTalk.aspx";

        ///=====================================
        /// 会話継続コンテキスト
        private string mode { get; set; }
        private string context { get; set; }

        ///=====================================
        /// イベント定義
        public delegate void reciveChatData(object sender, ReciveChatDataEventArgs e);
        public event reciveChatData rcvChat;

        /// <summary>
        /// コンストラクター
        /// </summary>
        public ClalisForLiplisChat()
        {
            this.mode = "";
            this.context = "";
        }

        /// <summary>
        /// タスクでデータ収集を行う
        /// </summary>
        public void apiPost(string uid, string toneUrl, string version, string sentence)
        {
            var apiPostTask = new Task<string>(() =>
            {
                //パラメーター生成
                FormUrlEncodedContent postData = new FormUrlEncodedContent(new Dictionary<string, string>
                {
                    { "userid", uid},          
                    { "tone",toneUrl},         
                    { "version",version},      
                    { "sentence",sentence},    
                    { "mode",mode},                      
                    { "context",context}
                });

                //Jsonで結果取得
                return HttpPost.sendPost(LIPLIS_CHAT, postData);
            });
            apiPostTask.Start();
            apiPostTask.ContinueWith(taskEnd =>
            {
                //json取得
                string jsonText = apiPostTask.Result;

                //jsonコンバート
                ResLpsChatResponse result = JsonConvert.DeserializeObject<ResLpsChatResponse>(jsonText);

                //返すデータの設定
                ReciveChatDataEventArgs e = new ReciveChatDataEventArgs(convertRlSumNjToMsg(result));

                //イベントの発生
                OnChatDataRecive(e);
            });
        }

        ///====================================================================
        ///
        ///                         イベントハンドラ実装
        ///                         
        ///====================================================================
        #region イベントハンドラ実装
        protected virtual void OnChatDataRecive(ReciveChatDataEventArgs e)
        {
            if (rcvChat != null)
            {
                rcvChat(this, e);
            }
        }
        #endregion

        /// <summary>
        ///convertRlSumNjToMsg
        /// ResLpsSummaryNews2Jsonからショートニュースメッセージに変換する
        /// </summary>
        /// <param name="rlsn2"></param>
        /// <returns></returns>
        #region convertRlSumNjToMsg
        protected MsgTalkMessage convertRlSumNjToMsg(ResLpsChatResponse rlsn2)
        {
            //結果メッセージを作成
            MsgTalkMessage msg = new MsgTalkMessage();

            //リザルトSB
            StringBuilder sbResult = new StringBuilder();

            //ネームリスト、等作成
            foreach (string desc in rlsn2.descriptionList)
            {
                try
                {
                    string[] bufList = desc.Split(';');

                    foreach (string buf in bufList)
                    {
                        string[] bufList2 = buf.Split(',');

                        if (bufList2.Length == 3)
                        {
                            msg.nameList.Add(bufList2[0]);
                            msg.emotionList.Add(int.Parse(bufList2[1]));
                            msg.pointList.Add(int.Parse(bufList2[2]));
                            sbResult.Append(bufList2[0]);
                        }
                        else
                        {

                        }
                    }
                }
                catch
                {

                }
            }

            //データの作成
            msg.result = sbResult.ToString();
            msg.sorce = sbResult.ToString();
            msg.title = "";

            string result = sbResult.ToString().Replace("EOS", "");

            //結果をメッセージに格納
            msg.url = LpsLiplisUtil.nullCheck(rlsn2.url);
            msg.title = LpsLiplisUtil.nullCheck(rlsn2.title);
            msg.result = result;
            msg.sorce = result;
            msg.calcNewsEmotion();

            //
            if (rlsn2.opList.Count == 2)
            {
                this.context = rlsn2.opList[0];
                this.mode = rlsn2.opList[1];
            }

            return msg;
        }
        #endregion
    }

    /// <summary>
    /// イベントアーグス
    /// </summary>
    public class ReciveChatDataEventArgs : EventArgs
    {
        public MsgTalkMessage result;

        public ReciveChatDataEventArgs(MsgTalkMessage jsonText)
        {
            this.result = jsonText;
        }
    }
}


