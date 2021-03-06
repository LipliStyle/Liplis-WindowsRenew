﻿namespace Liplis.Activity
{
    partial class ViewMenu
    {
        /// <summary>
        /// 必要なデザイナー変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージ リソースを破棄する場合は true を指定し、その他の場合は false を指定します。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows フォーム デザイナーで生成されたコード

        /// <summary>
        /// デザイナー サポートに必要なメソッドです。このメソッドの内容を
        /// コード エディターで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ViewMenu));
            this.btnCross = new System.Windows.Forms.Label();
            this.lblTitle = new System.Windows.Forms.Label();
            this.pnl = new System.Windows.Forms.Panel();
            this.btnMinimize = new System.Windows.Forms.Button();
            this.btnRescue = new System.Windows.Forms.Button();
            this.btnWakeUp = new System.Windows.Forms.Button();
            this.btnRss = new System.Windows.Forms.Button();
            this.btnChar = new System.Windows.Forms.Button();
            this.btnSleep = new System.Windows.Forms.Button();
            this.btnSetting = new System.Windows.Forms.Button();
            this.btnLog = new System.Windows.Forms.Button();
            this.btnEnd = new System.Windows.Forms.Button();
            this.btnFlow = new System.Windows.Forms.Button();
            this.toolTips = new System.Windows.Forms.ToolTip(this.components);
            this.pnl.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnCross
            // 
            this.btnCross.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(227)))), ((int)(((byte)(96)))));
            this.btnCross.Location = new System.Drawing.Point(269, 0);
            this.btnCross.Name = "btnCross";
            this.btnCross.Size = new System.Drawing.Size(30, 20);
            this.btnCross.TabIndex = 11;
            this.btnCross.Text = "✕";
            this.btnCross.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.btnCross.Click += new System.EventHandler(this.btnCross_Click);
            // 
            // lblTitle
            // 
            this.lblTitle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(227)))), ((int)(((byte)(96)))));
            this.lblTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblTitle.ForeColor = System.Drawing.Color.Black;
            this.lblTitle.Location = new System.Drawing.Point(0, 0);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(302, 20);
            this.lblTitle.TabIndex = 13;
            this.lblTitle.Text = "Liplis メニュー";
            this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblTitle.MouseDown += new System.Windows.Forms.MouseEventHandler(this.lblTitle_MouseDown);
            this.lblTitle.MouseMove += new System.Windows.Forms.MouseEventHandler(this.lblTitle_MouseMove);
            // 
            // pnl
            // 
            this.pnl.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(31)))), ((int)(((byte)(31)))));
            this.pnl.Controls.Add(this.btnMinimize);
            this.pnl.Controls.Add(this.btnRescue);
            this.pnl.Controls.Add(this.btnWakeUp);
            this.pnl.Controls.Add(this.btnRss);
            this.pnl.Controls.Add(this.btnCross);
            this.pnl.Controls.Add(this.btnChar);
            this.pnl.Controls.Add(this.btnSleep);
            this.pnl.Controls.Add(this.btnSetting);
            this.pnl.Controls.Add(this.btnLog);
            this.pnl.Controls.Add(this.btnEnd);
            this.pnl.Controls.Add(this.btnFlow);
            this.pnl.Controls.Add(this.lblTitle);
            this.pnl.Location = new System.Drawing.Point(2, 2);
            this.pnl.Name = "pnl";
            this.pnl.Size = new System.Drawing.Size(302, 292);
            this.pnl.TabIndex = 11;
            // 
            // btnMinimize
            // 
            this.btnMinimize.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(91)))), ((int)(((byte)(179)))), ((int)(((byte)(208)))));
            this.btnMinimize.ForeColor = System.Drawing.Color.White;
            this.btnMinimize.Image = global::Liplis.Properties.Resources.ico_tray;
            this.btnMinimize.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnMinimize.Location = new System.Drawing.Point(101, 111);
            this.btnMinimize.Name = "btnMinimize";
            this.btnMinimize.Size = new System.Drawing.Size(100, 90);
            this.btnMinimize.TabIndex = 18;
            this.btnMinimize.Text = "最小化";
            this.btnMinimize.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnMinimize.UseVisualStyleBackColor = false;
            this.btnMinimize.Click += new System.EventHandler(this.btnMinimize_Click);
            // 
            // btnRescue
            // 
            this.btnRescue.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(91)))), ((int)(((byte)(179)))), ((int)(((byte)(208)))));
            this.btnRescue.ForeColor = System.Drawing.Color.White;
            this.btnRescue.Image = global::Liplis.Properties.Resources.ico_rescue;
            this.btnRescue.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnRescue.Location = new System.Drawing.Point(201, 111);
            this.btnRescue.Name = "btnRescue";
            this.btnRescue.Size = new System.Drawing.Size(100, 90);
            this.btnRescue.TabIndex = 17;
            this.btnRescue.Text = "画面復帰";
            this.btnRescue.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnRescue.UseVisualStyleBackColor = false;
            this.btnRescue.Click += new System.EventHandler(this.btnRescue_Click);
            // 
            // btnWakeUp
            // 
            this.btnWakeUp.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(91)))), ((int)(((byte)(179)))), ((int)(((byte)(208)))));
            this.btnWakeUp.ForeColor = System.Drawing.Color.White;
            this.btnWakeUp.Image = global::Liplis.Properties.Resources.ico_waikup;
            this.btnWakeUp.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnWakeUp.Location = new System.Drawing.Point(101, 201);
            this.btnWakeUp.Name = "btnWakeUp";
            this.btnWakeUp.Size = new System.Drawing.Size(100, 90);
            this.btnWakeUp.TabIndex = 16;
            this.btnWakeUp.Text = "起床";
            this.btnWakeUp.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnWakeUp.UseVisualStyleBackColor = false;
            this.btnWakeUp.Click += new System.EventHandler(this.btnWakeUp_Click);
            // 
            // btnRss
            // 
            this.btnRss.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(91)))), ((int)(((byte)(179)))), ((int)(((byte)(208)))));
            this.btnRss.ForeColor = System.Drawing.Color.White;
            this.btnRss.Image = global::Liplis.Properties.Resources.ico_rss;
            this.btnRss.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnRss.Location = new System.Drawing.Point(201, 21);
            this.btnRss.Name = "btnRss";
            this.btnRss.Size = new System.Drawing.Size(100, 90);
            this.btnRss.TabIndex = 14;
            this.btnRss.Text = "RSS";
            this.btnRss.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnRss.UseVisualStyleBackColor = false;
            this.btnRss.Click += new System.EventHandler(this.btnRss_Click);
            // 
            // btnChar
            // 
            this.btnChar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(91)))), ((int)(((byte)(179)))), ((int)(((byte)(208)))));
            this.btnChar.ForeColor = System.Drawing.Color.White;
            this.btnChar.Image = global::Liplis.Properties.Resources.ico_char_lili;
            this.btnChar.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnChar.Location = new System.Drawing.Point(1, 21);
            this.btnChar.Name = "btnChar";
            this.btnChar.Size = new System.Drawing.Size(100, 90);
            this.btnChar.TabIndex = 0;
            this.btnChar.Text = "キャラクター";
            this.btnChar.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnChar.UseVisualStyleBackColor = false;
            this.btnChar.Click += new System.EventHandler(this.btnChar_Click);
            this.btnChar.Enter += new System.EventHandler(this.btnChar_Enter);
            // 
            // btnSleep
            // 
            this.btnSleep.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(91)))), ((int)(((byte)(179)))), ((int)(((byte)(208)))));
            this.btnSleep.ForeColor = System.Drawing.Color.White;
            this.btnSleep.Image = global::Liplis.Properties.Resources.ico_zzz;
            this.btnSleep.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnSleep.Location = new System.Drawing.Point(1, 201);
            this.btnSleep.Name = "btnSleep";
            this.btnSleep.Size = new System.Drawing.Size(100, 90);
            this.btnSleep.TabIndex = 4;
            this.btnSleep.Text = "おやすみ";
            this.btnSleep.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnSleep.UseVisualStyleBackColor = false;
            this.btnSleep.Click += new System.EventHandler(this.btnSleep_Click);
            this.btnSleep.Enter += new System.EventHandler(this.btnSleep_Enter);
            // 
            // btnSetting
            // 
            this.btnSetting.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(91)))), ((int)(((byte)(179)))), ((int)(((byte)(208)))));
            this.btnSetting.ForeColor = System.Drawing.Color.White;
            this.btnSetting.Image = global::Liplis.Properties.Resources.ico_setting;
            this.btnSetting.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnSetting.Location = new System.Drawing.Point(101, 21);
            this.btnSetting.Name = "btnSetting";
            this.btnSetting.Size = new System.Drawing.Size(100, 90);
            this.btnSetting.TabIndex = 1;
            this.btnSetting.Text = "設定";
            this.btnSetting.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnSetting.UseVisualStyleBackColor = false;
            this.btnSetting.Click += new System.EventHandler(this.btnSetting_Click);
            this.btnSetting.Enter += new System.EventHandler(this.btnSetting_Enter);
            // 
            // btnLog
            // 
            this.btnLog.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(91)))), ((int)(((byte)(179)))), ((int)(((byte)(208)))));
            this.btnLog.ForeColor = System.Drawing.Color.White;
            this.btnLog.Image = global::Liplis.Properties.Resources.ico_log;
            this.btnLog.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnLog.Location = new System.Drawing.Point(1, 111);
            this.btnLog.Name = "btnLog";
            this.btnLog.Size = new System.Drawing.Size(100, 90);
            this.btnLog.TabIndex = 3;
            this.btnLog.Text = "ログ";
            this.btnLog.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnLog.UseVisualStyleBackColor = false;
            this.btnLog.Click += new System.EventHandler(this.btnLog_Click);
            this.btnLog.Enter += new System.EventHandler(this.btnLog_Enter);
            // 
            // btnEnd
            // 
            this.btnEnd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(91)))), ((int)(((byte)(179)))), ((int)(((byte)(208)))));
            this.btnEnd.ForeColor = System.Drawing.Color.White;
            this.btnEnd.Image = global::Liplis.Properties.Resources.ico_pow;
            this.btnEnd.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnEnd.Location = new System.Drawing.Point(199, 201);
            this.btnEnd.Name = "btnEnd";
            this.btnEnd.Size = new System.Drawing.Size(100, 90);
            this.btnEnd.TabIndex = 5;
            this.btnEnd.Text = "終了";
            this.btnEnd.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnEnd.UseVisualStyleBackColor = false;
            this.btnEnd.Click += new System.EventHandler(this.btnEnd_Click);
            this.btnEnd.Enter += new System.EventHandler(this.btnEnd_Enter);
            // 
            // btnFlow
            // 
            this.btnFlow.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(91)))), ((int)(((byte)(179)))), ((int)(((byte)(208)))));
            this.btnFlow.ForeColor = System.Drawing.Color.White;
            this.btnFlow.Image = global::Liplis.Properties.Resources.ico_view;
            this.btnFlow.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnFlow.Location = new System.Drawing.Point(309, 113);
            this.btnFlow.Name = "btnFlow";
            this.btnFlow.Size = new System.Drawing.Size(100, 90);
            this.btnFlow.TabIndex = 2;
            this.btnFlow.Text = "会話フロー";
            this.btnFlow.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnFlow.UseVisualStyleBackColor = false;
            this.btnFlow.Visible = false;
            this.btnFlow.Click += new System.EventHandler(this.btnFlow_Click);
            this.btnFlow.Enter += new System.EventHandler(this.btnFlow_Enter);
            // 
            // ViewMenu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(210)))), ((int)(((byte)(233)))), ((int)(((byte)(241)))));
            this.ClientSize = new System.Drawing.Size(306, 295);
            this.Controls.Add(this.pnl);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "ViewMenu";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Liplis Menu";
            this.pnl.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button btnChar;
        private System.Windows.Forms.Button btnSetting;
        private System.Windows.Forms.Button btnLog;
        private System.Windows.Forms.Button btnFlow;
        private System.Windows.Forms.Button btnEnd;
        private System.Windows.Forms.Button btnSleep;
        private System.Windows.Forms.Label btnCross;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Panel pnl;
        private System.Windows.Forms.Button btnRss;
        private System.Windows.Forms.Button btnWakeUp;
        private System.Windows.Forms.Button btnRescue;
        private System.Windows.Forms.Button btnMinimize;
        private System.Windows.Forms.ToolTip toolTips;
    }
}

