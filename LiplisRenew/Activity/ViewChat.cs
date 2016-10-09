//=======================================================================
//  ClassName : ActivityChat
//  概要      : チャット画面。
//              会話機能追加
//
//  Liplis4.5
//  Copyright(c) 2010-2015 LipliStyle.Sachin
//=======================================================================
using Liplis.Activity.Ctrl;
using Liplis.Com;
using Liplis.Com.Defile;
using Liplis.Gui;
using Liplis.Msg;
using Liplis.Web.Clalis;
using Liplis.Widget;
using System;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;

namespace Liplis.Activity
{
    public partial class ViewChat : Form
    {
        ///=====================================
        /// リプリス
        private LiplisWidget lips;

        ///=====================================
        /// 会話インスタンス
        private ClalisForLiplisChat chat;

        ///=====================================
        /// フラグ
        private bool flgEnd = false;

        ///=====================================
        /// 前回値
        private string prvUrl = " ";
        private string prvTitle = " ";

        ///====================================================================
        ///
        ///                             初期化処理
        ///                         
        ///====================================================================
        #region 初期化処理
        //コンストラクター
        public ViewChat(LiplisWidget lips)
        {
            //リプリスインスタンス取得
            this.lips = lips;

            //イニシャルコンポーネント
            InitializeComponent();

            //クラスの初期化
            initClass();
            
            //ウインドウ設定初期化
            initSettingWindow();
        }

        /// <summary>
        /// initSettingWindow
        /// initSettingWindowの初期化
        /// </summary>

        private void initSettingWindow()
        {
            this.components = new System.ComponentModel.Container();

            //サイズを設定する
            this.Size = new Size(490, 500);

            //オパシティ1
            this.Opacity = 1;

            this.StartPosition = FormStartPosition.CenterScreen;
        }

        /// <summary>
        /// クラスの初期化
        /// </summary>
        private void initClass()
        {
            chat = new ClalisForLiplisChat();
            chat.rcvChat += Chat_rcvChat;
        }

        #endregion

        ///====================================================================
        ///
        ///                              onDelete
        ///                         
        ///====================================================================

        /// <summary>
        /// onDelete
        /// </summary>
        #region Dispose
        public void dispose()
        {
            flgEnd = true;
            this.Close();
        }
        #endregion

        /// <summary>
        /// onDelete
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        #region ActivityRss_FormClosing
        private void ActivityLog_FormClosing(object sender, FormClosingEventArgs e)
        {
            //エンドフラグが有効でなければ、ハイドさせる
            if (!flgEnd)
            {
                e.Cancel = true;
                this.Hide();
            }
        }
        #endregion

        ///====================================================================
        ///
        ///                           PanelOnRecive
        ///                         
        ///====================================================================

        /// <summary>
        /// mouseEnter
        /// マウスが上に来たときにはFLPにフォーカスする
        /// (スクロール対策)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        #region mouseEnter
        private void mouseEnter(object sender, EventArgs e)
        {
            this.flp.Focus();
        }
        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        #region
        private void btnSend_Click(object sender, EventArgs e)
        {
            if(lips.setting.lpsTalkMode == (int)LPS_TALK_MODE.EVERYONE)
            {
                LpsMessage.showError("みんなでおしゃべりモードの時は" + Environment.NewLine + "返事をすることができません。" + Environment.NewLine + "設定を変更して下さい。");
                return;
            }

            addTell(txtTell.Text);
        }
        #endregion

        /// <summary>
        /// エンターキー押下
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        #region txtTell_KeyDown
        private void txtTell_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                addTell(txtTell.Text);
                e.Handled = true;
            }
        }

        private void txtTell_KeyUp(object sender, KeyEventArgs e)
        {

        }
        #endregion



        ///====================================================================
        ///
        ///                           ログの追加
        ///                         
        ///====================================================================

        /// <summary>
        /// addLog
        /// ログの追加
        /// </summary>
        #region addLog
        public void addLog(MsgTalkMessage msg, Bitmap charBody)
        {
            addPanel(msg.url, msg.title, msg.sorce, msg.jpgUrl, msg.newsEmotion, msg.newsPoint, charBody);
        }
        #endregion

        /// <summary>
        /// addLog
        /// ログの追加
        /// </summary>
        #region addPanel
        private void addPanel(string url, string title, string discription, string jpgPath, int newsEmotion, int newsPoint, Bitmap charBody)
        {
            //前回値と同じなら登録しない
            if (!url.Equals("") && url.Equals(prvUrl) || !title.Equals("") && title.Equals(prvTitle)) { return; }

            //データパネル
            CusCtlDataPanel d;

            //前回値上書き
            prvUrl = url;
            prvTitle = title;

            //500件目の破棄
            if (flp.Controls.Count >= 500)
            {
                //500件目のパネルを取得
                using (CusCtlDataPanel dc = (CusCtlDataPanel)flp.Controls[499])
                {
                    try
                    {
                        //ゴミ捨て
                        LpsLiplisUtil.DeleteFile(dc.jpgPath);

                        //破棄
                        dc.dispose();

                        //flpから追放
                        flp.Controls.RemoveAt(499);
                    }
                    catch
                    {

                    }
                }
            }

            //新規要素の追加
            d = new CusCtlTellPanelChar(lips, url, title, discription, newsEmotion, newsPoint, charBody, new System.EventHandler(this.mouseEnter), this.components);

            //アッドする
            flp.Controls.Add(d);
            flp.Controls.SetChildIndex(d, 0);
            flp.VerticalScroll.Value = flp.VerticalScroll.Maximum;

            this.Refresh();

            this.txtTell.Text = "";
        }
        #endregion


        /// <summary>
        /// addTell
        /// 話しかけ処理
        /// </summary>
        #region addTell
        private void addTell(string description)
        {
            //空でエンターが押された時の対策。
            string tellString = txtTell.Text.Replace(Environment.NewLine, "");

            //入力チェック
            if (tellString.Equals(""))
            {
                return;
            }

            //データパネル
            CusCtlDataPanel d;

            //500件目の破棄
            if (flp.Controls.Count >= 500)
            {
                //500件目のパネルを取得
                using (CusCtlDataPanel dc = (CusCtlDataPanel)flp.Controls[499])
                {
                    try
                    {
                        //ゴミ捨て
                        LpsLiplisUtil.DeleteFile(dc.jpgPath);

                        //破棄
                        dc.dispose();

                        //flpから追放
                        flp.Controls.RemoveAt(499);
                    }
                    catch
                    {

                    }
                }
            }

            //新規要素の追加
            d = new CusCtlTellPanel(lips, description, this.components);

            //アッドする
            flp.Controls.Add(d);
            flp.Controls.SetChildIndex(d, 0);
            flp.VerticalScroll.Value = flp.VerticalScroll.Maximum;

            this.Refresh();

            //チャットデータ送信
            sendChat(description);
        }

        /// <summary>
        /// チャットデータ送信
        /// </summary>
        /// <param name="sentence"></param>
        private void sendChat(string sentence)
        {
            chat.apiPost(lips.desk.baseSetting.uid, lips.skin.xmlSkin.toneUrl, "W" + Assembly.GetExecutingAssembly().GetName().Version.ToString(), sentence);
        }

        /// <summary>
        /// チャットデータ受信
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Chat_rcvChat(object sender, ReciveChatDataEventArgs e)
        {
            MsgTalkMessage msg = e.result;
            Bitmap charBody = getCharBody(msg);
   
            Invoke(new LpsDelegate.dlgMsnToVoid(addLog), msg, charBody);

            lips.chatTalkRecive(e.result);
        }
        protected Bitmap getCharBody(MsgTalkMessage msg)
        {
            double wid = 75.0 * (double)((double)this.Width / (double)this.Height);
            Bitmap charBody = new Bitmap(75, (int)wid);
            using (Graphics g = Graphics.FromImage(charBody))
            {
                g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                g.DrawImage(lips.skin.xmlBody.getLiplisBody(msg.newsEmotion, msg.newsPoint).getBody(0, 0, 0), 0, 0, (int)wid, 75);
            }

            return charBody;
        }
        #endregion


        ///====================================================================
        ///
        ///                           APIアクセス
        ///                         
        ///====================================================================

        #region APIアクセス
        #endregion


    }
}
