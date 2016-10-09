//=======================================================================
//  ClassName : ViewLiplisSetting
//  概要      : 設定画面
//
// iOS版と同等
//  デザインは一新
//
//Liplis5.0
//
//アップデート履歴
//   2016/05/08 ver5.0.0 作成
//
//  Copyright(c) 2010-2016 LipliStyle.Sachin
//=======================================================================
using CoreTweet;
using Liplis.Activity.Sub;
using Liplis.Com;
using Liplis.Gui;
using Liplis.MainSystem;
using Liplis.Utl;
using Liplis.Voc;
using Liplis.Web.Clalis;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Liplis.Activity
{
    public partial class ViewLiplisSetting : Form
    {
        ///=============================
        ///設定関連
        public LiplisPreference baseSetting;

        //============================================================
        //
        //初期化処理
        //
        //============================================================
        #region 初期化処理
        /// <summary>
        /// コンストラクター
        /// </summary>
        /// <param name="baseSetting"></param>
        public ViewLiplisSetting(LiplisPreference baseSetting)
        {
            //ベースセッティング取得
            this.baseSetting = baseSetting;

            //画面初期化
            InitializeComponent();

            //ウインドウの初期化
            initWindow();
        }

        public void initWindow()
        {
            //単一行選択設定
            dgvVoiceRoidList.MultiSelect = false;
            dgvVoiceRoidList.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            //環境設定チェックボックス設定
            chkAutoSleep.Checked = LpsLiplisUtil.bitToBool(this.baseSetting.lpsAutoSleep);
            chkAutoWakeup.Checked = LpsLiplisUtil.bitToBool(this.baseSetting.lpsAutoWakeup);
            chkMenuOpen.Checked = LpsLiplisUtil.bitToBool(this.baseSetting.lpsMenuOpen);

            //ツイッターアクティベートラベル設定
            setTwitterActivateLabel();

            //ボイスロイドリストの更新
            initVoiceRoidList();
        }

        /// <summary>
        /// ボイスロイドリストの初期化
        /// </summary>
        public void initVoiceRoidList()
        {
            //ボイスロイドリストの展開
            dgvVoiceRoidList.Rows.Clear();
            foreach (voiceRoidSet item in baseSetting.voiceRoidSetList)
            {
                dgvVoiceRoidList.Rows.Add(item.voiceRoidName, item.path);
            }
        }

        #endregion


        //============================================================
        //
        //メニューボタン押下処理
        //
        //============================================================
        #region メニューボタン押下処理
        /// <summary>
        /// 環境設定ボタン
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnEnvironmentSetting_Click(object sender, EventArgs e)
        {
            this.tab.SelectedTab = this.tbpEnvironment;
            this.setButtonDefaultColor();
            this.btnEnvironmentSetting.BackColor = Color.FromArgb(192, 255, 255);
            this.btnEnvironmentSetting.ForeColor = Color.Black;
        }

        private void btnVoiceSetting_Click(object sender, EventArgs e)
        {
            this.tab.SelectedTab = this.tbpVoice;
            this.setButtonDefaultColor();
            this.btnVoiceSetting.BackColor = Color.FromArgb(192, 255, 255);
            this.btnVoiceSetting.ForeColor = Color.Black;
        }

        private void btnTwetterSetting_Click(object sender, EventArgs e)
        {
            this.tab.SelectedTab = this.tbpTwitter;
            this.setButtonDefaultColor();
            this.btnTwetterSetting.BackColor = Color.FromArgb(192, 255, 255);
            this.btnTwetterSetting.ForeColor = Color.Black;
        }

        private void btnSync_Click(object sender, EventArgs e)
        {
            this.tab.SelectedTab = this.tbpSync;
            this.setButtonDefaultColor();
            this.btnSync.BackColor = Color.FromArgb(192, 255, 255);
            this.btnSync.ForeColor = Color.Black;
        }

        private void setButtonDefaultColor()
        {
            this.btnEnvironmentSetting.BackColor = Color.FromArgb(223, 116, 1);
            this.btnVoiceSetting.BackColor = Color.FromArgb(223, 116, 1);
            this.btnTwetterSetting.BackColor = Color.FromArgb(223, 116, 1);
            this.btnSync.BackColor = Color.FromArgb(223, 116, 1);
            this.btnEnvironmentSetting.ForeColor = Color.White;
            this.btnVoiceSetting.ForeColor = Color.White;
            this.btnTwetterSetting.ForeColor = Color.White;
            this.btnSync.ForeColor = Color.White;
        }

        private void tsmiVersion_Click(object sender, EventArgs e)
        {
            using (ViewVersion f = new ViewVersion())
            {
                f.ShowDialog();
            }
        }

        private void tsmiOpenHelp_Click(object sender, EventArgs e)
        {
            dlgCallBrowser(LpsDefine.LIPLIS_HELP);
        }

        private void tsmiOpenSite_Click(object sender, EventArgs e)
        {
            dlgCallBrowser(LpsDefine.LIPLIS_LIPLISTYLE);
        }

        private void tsmiDefault_Click(object sender, EventArgs e)
        {
            if (LpsMessage.showMessageDialog("設定をデフォルトに戻しますか？"))
            {
                //ロードデフォルト
                baseSetting.defaultLoad();

                //設定の初期化
                initWindow();
            }
        }

        private void tsmiEnd_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        #endregion




        //============================================================
        //
        //環境設定操作
        //
        //============================================================
        #region 環境設定操作
        /// <summary>
        /// オートスリープクリック時
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chkAutoSleep_CheckedChanged(object sender, EventArgs e)
        {
            this.baseSetting.lpsAutoSleep = LpsLiplisUtil.boolToBit(chkAutoSleep.Checked);
            this.baseSetting.setPreferenceData();
        }

        /// <summary>
        /// オートウェイクアップクリック時
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chkAutoWakeup_CheckedChanged(object sender, EventArgs e)
        {
            this.baseSetting.lpsAutoWakeup = LpsLiplisUtil.boolToBit(chkAutoWakeup.Checked);
            this.baseSetting.setPreferenceData();
        }

        /// <summary>
        /// メニュー開き可否
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chkMenuOpen_CheckedChanged(object sender, EventArgs e)
        {
            this.baseSetting.lpsMenuOpen = LpsLiplisUtil.boolToBit(chkMenuOpen.Checked);
            this.baseSetting.setPreferenceData();
        }
        #endregion


        //============================================================
        //
        //ボイスロイド設定画面操作
        //
        //============================================================
        #region ボイスロイド設定画面操作
        private void btnVoiceRoid_Click(object sender, EventArgs e)
        {
            registerVoiceRoid();
        }

        private void btnVoiceRoidDel_Click(object sender, EventArgs e)
        {
            delVoiceRoid();
        }

        /// <summary>
        /// ボイスロイドの登録
        /// </summary>
        private void registerVoiceRoid()
        {
            //指定ファイル存在チェック
            if (!LpsLiplisUtil.ExistsFile(txtPath.Text))
            {
                LpsMessage.showError("指定されたファイルは見つかりません。");
                return;
            }

            //有効ボイスロイドクラス
            EnableVoiceRoid evr = EnableVoiceRoid.GetInstance();

            //もし、有効なボイスロイドでなければ、メッセージを表示し、設定には追加しない
            if (!evr.voiceRoidCheck(txtPath.Text))
            {
                LpsMessage.showError("登録できるボイスロイドは以下のとおりです。" + Environment.NewLine + evr.getEnableVoiceRoidListStr());
                return;
            }

            //ボイスロイド名を取得する
            string voiceRoidName = evr.voiceRoidName(txtPath.Text);

            //ボイスロイド登録チェック
            if(baseSetting.existsVoiceRoid(voiceRoidName))
            {
                LpsMessage.showError("指定のEXEは既に登録されています。");
                return;
            }

            //ここまできたらOKなので、登録する
            addVoiceRoid(voiceRoidName, txtPath.Text);
        }

        /// <summary>
        /// ボイスロイド登録を行う。
        /// </summary>
        /// <param name="voiceRoidName"></param>
        /// <param name="path"></param>
        private void addVoiceRoid(string voiceRoidName, string path)
        {
            //設定に追加
            baseSetting.voiceRoidSetList.Add(new voiceRoidSet(voiceRoidName,path));

            //セーブ
            baseSetting.setPreferenceData();

            //データグリッドに追加
            dgvVoiceRoidList.Rows.Add(voiceRoidName, path);
        }

        /// <summary>
        /// 選択されている行の設定を削除する
        /// </summary>
        private void delVoiceRoid()
        {
            //未選択チェック
            if(dgvVoiceRoidList.SelectedRows.Count == 0)
            {
                return;   
            }

            //選択行取得
            DataGridViewRow row = dgvVoiceRoidList.SelectedRows[0];

            //削除対象
            voiceRoidSet target = null;

            //リスト比較。ヒットしたら削除する
            foreach (voiceRoidSet vrs in baseSetting.voiceRoidSetList)
            {
                //一致したらターゲットとみなす
                if(vrs.voiceRoidName == (string)row.Cells[0].Value)
                {
                    target = vrs;
                    break;
                }
            }

            //HITしたら削除する
            if(target != null)
            {
                //ターゲットをリストから除去する
                baseSetting.voiceRoidSetList.Remove(target);

                //セーブ
                baseSetting.setPreferenceData();

                //表示更新
                initVoiceRoidList();
            }

        }



        #endregion

        //============================================================
        //
        //ツイッター登録操作
        //
        //============================================================
        #region ツイッター登録操作
        /// <summary>
        /// ツイッター登録ボタン押下
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnTwitterRegister_Click(object sender, EventArgs e)
        {
            TwitterOAuth();
        }

        /// <summary>
        /// ツイッター登録実行
        /// </summary>
        private void TwitterOAuth()
        {
            try
            {
                // 1.オーサライズセッション取得
                var session = OAuth.Authorize(LpsDefine.TWITTER_OAUTH_CONSUMERKEY, LpsDefine.TWITTER_OAUTH_CONSUMERSECRET);

                // ブラウザ起動しPINコードを表示
                System.Diagnostics.Process.Start(session.AuthorizeUri.ToString());

                // 2.PINコード認証
                string pin;

                //ピンコード入力画面を表示する
                using (ViewTwitterActivation ftip = new ViewTwitterActivation())
                {
                    //画面表示
                    ftip.ShowDialog();

                    pin = ftip.pin;
                }

                // 3.アクセストークン取得
                var tokens = session.GetTokens(pin);

                // 4. ツイッター登録非同期実行
                bwTwitterRegister.RunWorkerAsync(new List<string>(new string[] { baseSetting.uid, tokens.AccessToken, tokens.AccessTokenSecret, tokens.UserId.ToString(), tokens.ScreenName }));                
            }
            catch (Exception)
            {
                LpsMessage.showError("アクセストークンの取得に失敗しました。");
            }
        }

        /// <summary>
        /// 非同期実行
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private bool twitterRegisterResult = true;
        private void bwTwitterRegister_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                //引数取得
                List<string> arg = (List<string>)e.Argument;
                
                //登録
                ClalisForLiplis.twitterRegister(arg[0], arg[1], arg[2], arg[3], arg[4]);

                //結果設定
                twitterRegisterResult = true;
            }
            catch
            {
                //失敗
                twitterRegisterResult = false;
            }
        }

        /// <summary>
        /// 登録完了
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bwTwitterRegister_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            //非同期実行の結果を評価し、メッセージを表示
            if(twitterRegisterResult)
            {
                this.baseSetting.lpsTwitterActivate = 1;
                LpsMessage.showMessage("アクセストークンの取得に成功しました。");
            }
            else
            {
                this.baseSetting.lpsTwitterActivate = 0;
                LpsMessage.showError("アクセストークンの取得に失敗しました。");
            }

            //設定保存
            this.baseSetting.setPreferenceData();

            //アクティベートラベルを更新する
            setTwitterActivateLabel();
        }

        /// <summary>
        /// ツイッターアクティベートラベルの設定
        /// </summary>
        private void setTwitterActivateLabel()
        {
            if (LpsLiplisUtil.bitToBool(baseSetting.lpsTwitterActivate))
            {
                lbllblTitleTwitterRegisterStatus.BackColor = Color.FromArgb(128, 255, 128);
                lbllblTitleTwitterRegisterStatus.Text = "登録済み";
            }
            else
            {
                lbllblTitleTwitterRegisterStatus.BackColor = Color.FromArgb(255, 192, 192);
                lbllblTitleTwitterRegisterStatus.Text = "未登録";
            }
        }

        #endregion


        //============================================================
        //
        //同期設定
        //
        //============================================================
        #region 同期設定
        /// <summary>
        /// ワンタイムパスワード取得
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSyncPassIssue_Click(object sender, EventArgs e)
        {
            //ワンタイム取得 非同期処理実行開始
            bwGetOneTimePassword.RunWorkerAsync();
        }

        /// <summary>
        /// ワンタイムパスワード取得バックグラウンド処理実行
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private string oneTimePassword = "";
        private void bwGetOneTimePassword_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                //登録
                oneTimePassword = ClalisForLiplis.getOneTimePass(baseSetting.uid);
            }
            catch
            {
                oneTimePassword = "";
            }
        }

        /// <summary>
        /// ワンタイムパスワード取得非同期処理完了
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bwGetOneTimePassword_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            //ワンタイムパスワード画面表示
            this.txtSyncPassIssue.Text = oneTimePassword;

            //失敗時、メッセージ表示
            if (oneTimePassword == "")
            {
                LpsMessage.showError("ワンタイムパスワードの取得に失敗しました。" + Environment.NewLine + "時間を置いて、やり直してみてください。");
            }
        }


        /// <summary>
        /// 同期処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnFromOtherSync_Click(object sender, EventArgs e)
        {
            //ワンタイム取得 非同期処理実行開始
            bwSync.RunWorkerAsync();
        }

        /// <summary>
        /// ユーザーID取得処理実行
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private string userId;
        private void bwSync_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                //登録
                userId = ClalisForLiplis.getLiplisId(txtFromOtherSync.Text);
            }
            catch
            {
                userId = "";
            }
        }

        /// <summary>
        /// ユーザーID取得処理完了
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bwSync_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            updateUserIdAndSetting();
        }

        /// <summary>
        /// 設定の更新
        /// </summary>
        private void updateUserIdAndSetting()
        {
            //失敗時、メッセージ表示
            if (userId == "")
            {
                LpsMessage.showError("設定の取得に失敗しました。ワンタイムパスワードを再度取得し、実行してみて下さい。");
            }

            //最終確認
            if (MessageBox.Show("RSS設定やツイッター認証設定を上書きしますが、よろしいでしょうか？", "Liplis", MessageBoxButtons.OKCancel) == System.Windows.Forms.DialogResult.OK)
            {
                //ユーザーID変更履歴の出力
                try { System.IO.File.AppendAllText(LpsPathController.getSettingPath() + DateTime.Now.ToString("yyyyMMddhhmmss") + "userPassChange.log", userId, Encoding.GetEncoding(932)); }
                catch
                { }

                //ユーザーIDの上書き
                baseSetting.uid = userId;
                baseSetting.setPreferenceData();

                //TODO: RSSの再読み込みを行う必要がある。

                //完了メッセージ表示
                LpsMessage.showMessage("同期処理が完了しました。");
            }
        }




        #endregion


        ///====================================================================
        ///
        ///                       デリゲート
        ///                         
        ///====================================================================

        /// <summary>
        /// dlgSetBackGround
        /// 背景を設定する
        /// </summary>
        #region dlgSetBackGround
        private void dlgSetBackGround(Bitmap bmp)
        {
            this.BackgroundImage = bmp;
        }
        #endregion

        /// <summary>
        /// ブラウザをコールする
        /// </summary>
        #region dlgCallBrowser
        private void dlgCallBrowser(string url)
        {
            try
            {
                System.Diagnostics.Process.Start(url);
            }
            catch (System.ComponentModel.Win32Exception fileNotFoundErr)
            {
                Console.Write(fileNotFoundErr);
                //lips.chatFixedSentence(ComDefine.err_BrowzerErr);
                //lips.expression = ComDefine.EXPRESSION_CRY;
            }
            catch (System.Exception err)
            {
                Console.Write(err);
            }
        }
        #endregion

    }
}


