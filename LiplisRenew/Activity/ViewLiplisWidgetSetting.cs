﻿//=======================================================================
//  ClassName : ViewLiplisWidgetSetting
//  概要      : ウィジェット設定画面
//
// iOS版と同等
//  デザインは一新
//
//Liplis5.0
//
//アップデート履歴
//   2016/05/25 ver5.0.0 作成
//
//  Copyright(c) 2010-2016 LipliStyle.Sachin
//=======================================================================
using Liplis.Com;
using Liplis.MainSystem;
using Liplis.Widget;
using System.Drawing;
using System.Windows.Forms;

namespace Liplis.Activity
{
    public partial class ViewLiplisWidgetSetting : Form
    {
        //=================================
        //Liplis要素
        private LiplisWidgetPreference setting;
        private LiplisPreference baseSetting;
        private Skin skin;
        private LiplisWidget lips;

        //============================================================
        //
        //初期化処理
        //
        //============================================================
        #region 初期化処理
        public ViewLiplisWidgetSetting(LiplisWidget lips, LiplisPreference baseSetting,  LiplisWidgetPreference setting, Skin skin)
        {
            //設定取得
            this.lips = lips;
            this.baseSetting = baseSetting;
            this.setting = setting;
            this.skin = skin;

            //画面初期化
            InitializeComponent();

            //ウインドウの初期化
            initWindow();

        }

        /// <summary>
        /// ウインドウの初期化
        /// </summary>
        private void initWindow()
        {
            //ウインドウ画像設定
            setWindowPic();

            //トークモード設定
            setTalkMode(this.setting.lpsMode);

            //おしゃべり速度取得
            setActive();

            //アイコン表示
            chkIconOn.Checked = LpsLiplisUtil.bitToBool(this.setting.lpsDisplayIcon);

            //バッテリーリンク
            chkBattery.Checked = LpsLiplisUtil.bitToBool(this.setting.lpsHealth);

            //ウインドウ設定
            setWindow(this.setting.lpsWindow);

            //話題設定
            chkTopicNews.Checked         = LpsLiplisUtil.bitToBool(this.setting.lpsTopicNews);
            chkTopic2ch.Checked          = LpsLiplisUtil.bitToBool(this.setting.lpsTopic2ch);
            chkTopicNico.Checked         = LpsLiplisUtil.bitToBool(this.setting.lpsTopicNico);
            chkTopicTwPublic.Checked     = LpsLiplisUtil.bitToBool(this.setting.lpsTopicTwitterPu);
            chkTopicRss.Checked          = LpsLiplisUtil.bitToBool(this.setting.lpsTopicRss);
            chkTopicTwMyTimeLine.Checked = LpsLiplisUtil.bitToBool(this.setting.lpsTopicTwitterMy);

            //おしゃべり速度取得
            setRange();

            //ボイスロイド
            setCboVoiceEngineSetting();

            //ボイスロイド使用チェック
            chkVoiceSetting.Checked = LpsLiplisUtil.bitToBool(this.setting.lpsVoiceOn);
        }

        /// <summary>
        /// ウインドウ画像設定
        /// </summary>
        private void setWindowPic()
        {
            setWindowImage(this.rbWindow1, this.picWindow1, 0);
            setWindowImage(this.rbWindow2, this.picWindow2, 1);
            setWindowImage(this.rbWindow3, this.picWindow3, 2);
            setWindowImage(this.rbWindow4, this.picWindow4, 3);
            setWindowImage(this.rbWindow5, this.picWindow5, 4);
            setWindowImage(this.rbWindow6, this.picWindow6, 5);
            setWindowImage(this.rbWindow7, this.picWindow7, 6);

        }

        /// <summary>
        /// ウインドウイメージをセットする
        /// </summary>
        /// <param name="rb"></param>
        /// <param name="pic"></param>
        /// <param name="window"></param>
        private void setWindowImage(RadioButton rb, PictureBox pic, int window)
        {
            //存在チェック
            if (!LpsLiplisUtil.ExistsFile(skin.xmlWindow.getWindowPath(window)))
            {
                rb.Enabled = false;
                pic.Enabled = false;
                return;
            }

            Bitmap canvas = new Bitmap(pic.Width, pic.Height);

            using (Bitmap image = new Bitmap(skin.xmlWindow.getWindowPath(window)))
            {
                using (Graphics g = Graphics.FromImage(canvas))
                {
                    g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                    g.DrawImage(image, 0, 0, 96, 42);

                }
            }

            pic.Image = canvas;
        }

        /// <summary>
        /// アクティブ度を設定する
        /// </summary>
        private void setActive()
        {
            int active = this.setting.lpsSpeed;

            if(active > this.trcActive.Maximum)
            {
                active = this.trcActive.Minimum;
            }
            else if (active < this.trcActive.Minimum)
            {
                active = this.trcActive.Minimum;
            }

            this.trcActive.Value = active;
            this.txtActiveValue.Text = active.ToString();
        }

        /// <summary>
        /// レンジ設定
        /// </summary>
        private void setRange()
        {
            int hour = this.setting.lpsTopicHour;

            if (hour > this.trcRange.Maximum)
            {
                hour = this.trcRange.Minimum;
            }
            else if (hour < this.trcRange.Minimum)
            {
                hour = this.trcRange.Minimum;
            }

            this.trcRange.Value = hour;
            this.txtRange.Text = hour.ToString();
        }

        /// <summary>
        /// ボイスロイドコンボを設定する
        /// </summary>
        private void setCboVoiceEngineSetting()
        {
            //空セット
            cboVoiceEngineSetting.Items.Add(new voiceRoidSet("", ""));

            //コンボボックスに設定
            foreach (voiceRoidSet item in baseSetting.voiceRoidSetList)
            {
                cboVoiceEngineSetting.Items.Add(item);
            }

            //ボイスロイドテキスト設定
            cboVoiceEngineSetting.Text = setting.lpsVoiceName;
        }

        #endregion

        //============================================================
        //
        //タブ操作ボタン
        //
        //============================================================
        #region タブ操作ボタン
        private void btnWidgetSetting_Click(object sender, System.EventArgs e)
        {
            this.tab.SelectedTab = this.tbpEnvironment;
            this.setButtonDefaultColor();
            this.btnWidgetSetting.BackColor = Color.FromArgb(192, 255, 255);
            this.btnWidgetSetting.ForeColor = Color.Black;
        }


        private void btnTopicSetting_Click(object sender, System.EventArgs e)
        {
            
            this.tab.SelectedTab = this.tbpTopic;
            this.setButtonDefaultColor();
            this.btnTopicSetting.BackColor = Color.FromArgb(192, 255, 255);
            this.btnTopicSetting.ForeColor = Color.Black;
        }


        private void btnVoiceSetting_Click(object sender, System.EventArgs e)
        {
            this.tab.SelectedTab = this.tbpVoice;
            this.setButtonDefaultColor();
            this.btnVoiceSetting.BackColor = Color.FromArgb(192, 255, 255);
            this.btnVoiceSetting.ForeColor = Color.Black;
        }

        private void setButtonDefaultColor()
        {
            this.btnWidgetSetting.BackColor      = Color.FromArgb(223, 116, 1);
            this.btnTopicSetting.BackColor       = Color.FromArgb(223, 116, 1);
            this.btnVoiceSetting.BackColor     = Color.FromArgb(223, 116, 1);
            this.btnWidgetSetting.ForeColor      = Color.White;
            this.btnTopicSetting.ForeColor       = Color.White;
            this.btnVoiceSetting.ForeColor       = Color.White;
        }
        #endregion



        //============================================================
        //
        //設定反映
        //
        //============================================================
        #region 設定反映
        /// <summary>
        /// おしゃべりモードのラジオボタンをONにする
        /// </summary>
        /// <param name="talkMode"></param>
        private void setTalkMode(int talkMode)
        {
            switch(talkMode)
            {
                case 0:
                    rdoFrqMachen.Checked = true;
                    break;
                case 1:
                    rdoFrqVerryNoisy.Checked = true;
                    break;
                case 2:
                    rdoFrqTalkative.Checked = true;
                    break;
                case 3:
                    rdoFrqNormal.Checked = true;
                    break;
                case 4:
                    rdoFrqQuiet.Checked = true;
                    break;
                case 5:
                    rdoFrqKeeps.Checked = true;
                    break;
                case 6:
                    rdoFrqReticent.Checked = true;
                    break;
                default:
                    rdoFrqMachen.Checked = true;
                    break;
            }
        }

        /// <summary>
        /// ウインドウ設定
        /// </summary>
        /// <param name="windowId"></param>
        private void setWindow(int windowId)
        {
            switch (windowId)
            {
                case 0:
                    rbWindow1.Checked = true;
                    break;
                case 1:
                    rbWindow2.Checked = true;
                    break;
                case 2:
                    rbWindow3.Checked = true;
                    break;
                case 3:
                    rbWindow4.Checked = true;
                    break;
                case 4:
                    rbWindow5.Checked = true;
                    break;
                case 5:
                    rbWindow6.Checked = true;
                    break;
                case 6:
                    rbWindow7.Checked = true;
                    break;
                default:
                    rbWindow1.Checked = true;
                    break;
            }
        }



        #endregion

        //============================================================
        //
        //ウィジェット設定
        //
        //============================================================
        #region ウィジェット設定


        /// <summary>
        /// アイコンオン設定
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chkIconOn_CheckedChanged(object sender, System.EventArgs e)
        {
            setting.lpsDisplayIcon = LpsLiplisUtil.boolToBit(chkIconOn.Checked);
            setting.setPreferenceData();
        }

        /// <summary>
        /// バッテリーチェンジ設定
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chkBattery_CheckedChanged(object sender, System.EventArgs e)
        {
            setting.lpsHealth = LpsLiplisUtil.boolToBit(chkBattery.Checked);
            setting.setPreferenceData();
        }

        /// <summary>
        /// モード設定
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void rdoFrqMachen_CheckedChanged(object sender, System.EventArgs e)
        {
            saveLpsLpsMode(0);
        }

        private void rdoFrqVerryNoisy_CheckedChanged(object sender, System.EventArgs e)
        {
            saveLpsLpsMode(1);
        }

        private void rdoFrqTalkative_CheckedChanged(object sender, System.EventArgs e)
        {
            saveLpsLpsMode(2);
        }

        private void rdoFrqNormal_CheckedChanged(object sender, System.EventArgs e)
        {
            saveLpsLpsMode(3);
        }

        private void rdoFrqQuiet_CheckedChanged(object sender, System.EventArgs e)
        {
            saveLpsLpsMode(4);
        }

        private void rdoFrqKeeps_CheckedChanged(object sender, System.EventArgs e)
        {
            saveLpsLpsMode(5);
        }

        private void rdoFrqReticent_CheckedChanged(object sender, System.EventArgs e)
        {
            saveLpsLpsMode(6);
        }
        private void saveLpsLpsMode(int mode)
        {
            setting.lpsMode = mode;
            setting.setPreferenceData();
        }


        /// <summary>
        /// おしゃべりスピード設定
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void trcActive_ValueChanged(object sender, System.EventArgs e)
        {
            txtActiveValue.Text = trcActive.Value.ToString();
            setting.lpsSpeed = trcActive.Value;
            setting.setPreferenceData();

            //リプリスのチャットスピードを変更しておく
            lips.chatSpeedChange();
        }
        private void btnActiove100_Click(object sender, System.EventArgs e)
        {
            trcActive.Value = 100;
        }
        private void btnActiove75_Click(object sender, System.EventArgs e)
        {
            trcActive.Value = 75;
        }
        private void btnActiove50_Click(object sender, System.EventArgs e)
        {
            trcActive.Value = 50;
        }
        private void btnActiove24_Click(object sender, System.EventArgs e)
        {
            trcActive.Value = 24;
        }
        private void btnActiove0_Click(object sender, System.EventArgs e)
        {
            trcActive.Value = 0;
        }

        /// <summary>
        /// ウインドウ設定
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void rbWindow1_CheckedChanged(object sender, System.EventArgs e)
        {
            saveLpsWindow(0);
        }

        private void rbWindow2_CheckedChanged(object sender, System.EventArgs e)
        {
            saveLpsWindow(1);
        }

        private void rbWindow3_CheckedChanged(object sender, System.EventArgs e)
        {
            saveLpsWindow(2);
        }

        private void rbWindow4_CheckedChanged(object sender, System.EventArgs e)
        {
            saveLpsWindow(3);
        }

        private void rbWindow5_CheckedChanged(object sender, System.EventArgs e)
        {
            saveLpsWindow(4);
        }

        private void rbWindow6_CheckedChanged(object sender, System.EventArgs e)
        {
            saveLpsWindow(5);
        }

        private void rbWindow7_CheckedChanged(object sender, System.EventArgs e)
        {
            saveLpsWindow(6);
        }
        private void saveLpsWindow(int window)
        {
            setting.lpsWindow = window;
            setting.setPreferenceData();
        }

        /// <summary>
        /// ウインドウ画像クリック
        /// ウインドウ画像をクリックしたら、ラジオボタンをクリックしたことにする。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void picWindow1_Click(object sender, System.EventArgs e)
        {
            rbWindow1.Checked = true;
        }

        private void picWindow2_Click(object sender, System.EventArgs e)
        {
            rbWindow2.Checked = true;
        }

        private void picWindow3_Click(object sender, System.EventArgs e)
        {
            rbWindow3.Checked = true;
        }

        private void picWindow4_Click(object sender, System.EventArgs e)
        {
            rbWindow4.Checked = true;
        }

        private void picWindow5_Click(object sender, System.EventArgs e)
        {
            rbWindow5.Checked = true;
        }

        private void picWindow6_Click(object sender, System.EventArgs e)
        {
            rbWindow6.Checked = true;
        }

        private void picWindow7_Click(object sender, System.EventArgs e)
        {
            rbWindow7.Checked = true;
        }

        /// <summary>
        /// 話題設定
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chkTopicNews_CheckedChanged(object sender, System.EventArgs e)
        {
            setting.lpsTopicNews = LpsLiplisUtil.boolToBit(chkTopicNews.Checked);
            setting.setPreferenceData();
        }

        private void chkTopic2ch_CheckedChanged(object sender, System.EventArgs e)
        {
            setting.lpsTopic2ch = LpsLiplisUtil.boolToBit(chkTopic2ch.Checked);
            setting.setPreferenceData();
        }

        private void chkTopicNico_CheckedChanged(object sender, System.EventArgs e)
        {
            setting.lpsTopicNico = LpsLiplisUtil.boolToBit(chkTopicNico.Checked);
            setting.setPreferenceData();
        }

        private void chkTopicTwPublic_CheckedChanged(object sender, System.EventArgs e)
        {
            setting.lpsTopicTwitterPu = LpsLiplisUtil.boolToBit(chkTopicTwPublic.Checked);
            setting.setPreferenceData();
        }

        private void chkTopicRss_CheckedChanged(object sender, System.EventArgs e)
        {
            setting.lpsTopicRss = LpsLiplisUtil.boolToBit(chkTopicRss.Checked);
            setting.setPreferenceData();
        }

        private void chkTopicTwMyTimeLine_CheckedChanged(object sender, System.EventArgs e)
        {
            setting.lpsTopicTwitterMy = LpsLiplisUtil.boolToBit(chkTopicTwMyTimeLine.Checked);
            setting.setPreferenceData();
        }

        /// <summary>
        /// 話題取得範囲設定
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void trcRange_ValueChanged(object sender, System.EventArgs e)
        {
            txtRange.Text = trcRange.Value.ToString();
            setting.lpsTopicHour = trcRange.Value;
            setting.setPreferenceData();
        }
        private void btnRange1Hour_Click(object sender, System.EventArgs e)
        {
            trcRange.Value = 1;
        }

        private void btnRange3Hour_Click(object sender, System.EventArgs e)
        {
            trcRange.Value = 3;
        }

        private void btnRange6Hour_Click(object sender, System.EventArgs e)
        {
            trcRange.Value = 6;
        }


        private void btnRange12Hour_Click(object sender, System.EventArgs e)
        {
            trcRange.Value = 12;
        }

        private void btnRange24Hour_Click(object sender, System.EventArgs e)
        {
            trcRange.Value = 24;
        }

        private void btnRange48Hour_Click(object sender, System.EventArgs e)
        {
            trcRange.Value = 48;
        }

        private void btnRange72Hour_Click(object sender, System.EventArgs e)
        {
            trcRange.Value = 72;
        }

        /// <summary>
        /// ボイスロイド使用可否設定
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chkVoiceSetting_CheckedChanged(object sender, System.EventArgs e)
        {
            setting.lpsVoiceOn = LpsLiplisUtil.boolToBit(chkVoiceSetting.Checked);
            setting.setPreferenceData();
        }

        private void cboVoiceEngineSetting_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            ComboBox cbo = (ComboBox)sender;
            voiceRoidSet vrs = (voiceRoidSet)cbo.SelectedItem;

            if(vrs != null)
            {
                setting.lpsVoiceName = vrs.voiceRoidName;
                setting.lpsVoicePath = vrs.path;
                setting.setPreferenceData();

                //TODO:リプリスに音声おしゃべり設定

            }

        }
        #endregion


    }
}