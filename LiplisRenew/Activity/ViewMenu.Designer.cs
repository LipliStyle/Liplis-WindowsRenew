namespace Liplis.Activity
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ViewMenu));
            this.pnl = new System.Windows.Forms.Panel();
            this.lblTitle = new System.Windows.Forms.Label();
            this.btnCross = new System.Windows.Forms.Label();
            this.btnChar = new System.Windows.Forms.Button();
            this.btnSleep = new System.Windows.Forms.Button();
            this.btnSetting = new System.Windows.Forms.Button();
            this.btnLog = new System.Windows.Forms.Button();
            this.btnEnd = new System.Windows.Forms.Button();
            this.btnFlow = new System.Windows.Forms.Button();
            this.pnl.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnl
            // 
            this.pnl.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(184)))), ((int)(((byte)(222)))), ((int)(((byte)(235)))));
            this.pnl.Controls.Add(this.btnCross);
            this.pnl.Controls.Add(this.btnChar);
            this.pnl.Controls.Add(this.btnSleep);
            this.pnl.Controls.Add(this.btnSetting);
            this.pnl.Controls.Add(this.btnLog);
            this.pnl.Controls.Add(this.btnEnd);
            this.pnl.Controls.Add(this.btnFlow);
            this.pnl.Controls.Add(this.lblTitle);
            this.pnl.Location = new System.Drawing.Point(1, 1);
            this.pnl.Name = "pnl";
            this.pnl.Size = new System.Drawing.Size(202, 289);
            this.pnl.TabIndex = 11;
            // 
            // lblTitle
            // 
            this.lblTitle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(184)))), ((int)(((byte)(222)))), ((int)(((byte)(235)))));
            this.lblTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblTitle.ForeColor = System.Drawing.Color.Black;
            this.lblTitle.Location = new System.Drawing.Point(0, 0);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(202, 20);
            this.lblTitle.TabIndex = 13;
            this.lblTitle.Text = "Liplis メニュー";
            this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblTitle.MouseDown += new System.Windows.Forms.MouseEventHandler(this.lblTitle_MouseDown);
            this.lblTitle.MouseMove += new System.Windows.Forms.MouseEventHandler(this.lblTitle_MouseMove);
            // 
            // btnCross
            // 
            this.btnCross.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(184)))), ((int)(((byte)(222)))), ((int)(((byte)(235)))));
            this.btnCross.Location = new System.Drawing.Point(172, 0);
            this.btnCross.Name = "btnCross";
            this.btnCross.Size = new System.Drawing.Size(30, 20);
            this.btnCross.TabIndex = 11;
            this.btnCross.Click += new System.EventHandler(this.btnCross_Click);
            // 
            // btnChar
            // 
            this.btnChar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(91)))), ((int)(((byte)(179)))), ((int)(((byte)(208)))));
            this.btnChar.ForeColor = System.Drawing.Color.White;
            this.btnChar.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnChar.Location = new System.Drawing.Point(1, 20);
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
            this.btnSleep.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnSleep.Location = new System.Drawing.Point(1, 199);
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
            this.btnSetting.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnSetting.Location = new System.Drawing.Point(101, 20);
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
            this.btnLog.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnLog.Location = new System.Drawing.Point(101, 109);
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
            this.btnEnd.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnEnd.Location = new System.Drawing.Point(101, 199);
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
            this.btnFlow.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnFlow.Location = new System.Drawing.Point(1, 109);
            this.btnFlow.Name = "btnFlow";
            this.btnFlow.Size = new System.Drawing.Size(100, 90);
            this.btnFlow.TabIndex = 2;
            this.btnFlow.Text = "会話フロー";
            this.btnFlow.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnFlow.UseVisualStyleBackColor = false;
            this.btnFlow.Click += new System.EventHandler(this.btnFlow_Click);
            this.btnFlow.Enter += new System.EventHandler(this.btnFlow_Enter);
            // 
            // ViewMenu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(204, 291);
            this.Controls.Add(this.pnl);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "ViewMenu";
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
        private System.Windows.Forms.Panel pnl;
        private System.Windows.Forms.Label btnCross;
        private System.Windows.Forms.Label lblTitle;
    }
}

