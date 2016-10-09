namespace Liplis.Activity
{
    partial class ViewDeskTop
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ViewDeskTop));
            this.tsmSleep = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmMinimize = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmSetting = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmLog = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmEnd = new System.Windows.Forms.ToolStripMenuItem();
            this.liplisContext = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsmMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.tss01 = new System.Windows.Forms.ToolStripSeparator();
            this.tss02 = new System.Windows.Forms.ToolStripSeparator();
            this.icon = new System.Windows.Forms.NotifyIcon(this.components);
            this.liplisContext.SuspendLayout();
            this.SuspendLayout();
            // 
            // tsmSleep
            // 
            this.tsmSleep.Name = "tsmSleep";
            this.tsmSleep.Size = new System.Drawing.Size(163, 22);
            this.tsmSleep.Text = "全員 お休み/復帰";
            this.tsmSleep.Click += new System.EventHandler(this.tsmSleep_Click);
            // 
            // tsmMinimize
            // 
            this.tsmMinimize.Name = "tsmMinimize";
            this.tsmMinimize.Size = new System.Drawing.Size(163, 22);
            this.tsmMinimize.Text = "非表示/戻す";
            this.tsmMinimize.Click += new System.EventHandler(this.tsmMinimize_Click);
            // 
            // tsmSetting
            // 
            this.tsmSetting.Name = "tsmSetting";
            this.tsmSetting.Size = new System.Drawing.Size(163, 22);
            this.tsmSetting.Text = "設定";
            this.tsmSetting.Click += new System.EventHandler(this.tsmSetting_Click);
            // 
            // tsmLog
            // 
            this.tsmLog.Name = "tsmLog";
            this.tsmLog.Size = new System.Drawing.Size(163, 22);
            this.tsmLog.Text = "ログ";
            this.tsmLog.Click += new System.EventHandler(this.tsmLog_Click);
            // 
            // tsmEnd
            // 
            this.tsmEnd.Name = "tsmEnd";
            this.tsmEnd.Size = new System.Drawing.Size(163, 22);
            this.tsmEnd.Text = "終了";
            this.tsmEnd.Click += new System.EventHandler(this.tsmEnd_Click);
            // 
            // liplisContext
            // 
            this.liplisContext.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmMenu,
            this.tsmSetting,
            this.tsmLog,
            this.tss01,
            this.tsmSleep,
            this.tsmMinimize,
            this.tss02,
            this.tsmEnd});
            this.liplisContext.Name = "liplisContext";
            this.liplisContext.Size = new System.Drawing.Size(164, 148);
            // 
            // tsmMenu
            // 
            this.tsmMenu.Name = "tsmMenu";
            this.tsmMenu.Size = new System.Drawing.Size(163, 22);
            this.tsmMenu.Text = "Liplis操作メニュー";
            this.tsmMenu.Click += new System.EventHandler(this.tsmMenu_Click);
            // 
            // tss01
            // 
            this.tss01.Name = "tss01";
            this.tss01.Size = new System.Drawing.Size(160, 6);
            // 
            // tss02
            // 
            this.tss02.Name = "tss02";
            this.tss02.Size = new System.Drawing.Size(160, 6);
            // 
            // icon
            // 
            this.icon.ContextMenuStrip = this.liplisContext;
            this.icon.Icon = ((System.Drawing.Icon)(resources.GetObject("icon.Icon")));
            this.icon.Text = "Liplis";
            this.icon.Visible = true;
            this.icon.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.icon_MouseDoubleClick);
            // 
            // ViewDeskTop
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(220, 210);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "ViewDeskTop";
            this.Text = "Liplis Menu";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.ViewDeskTop_FormClosed);
            this.Load += new System.EventHandler(this.ViewDeskTop_Load);
            this.liplisContext.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ToolStripMenuItem tsmSleep;
        private System.Windows.Forms.ToolStripMenuItem tsmMinimize;
        private System.Windows.Forms.ToolStripMenuItem tsmSetting;
        private System.Windows.Forms.ToolStripMenuItem tsmLog;
        private System.Windows.Forms.ToolStripMenuItem tsmEnd;
        private System.Windows.Forms.ContextMenuStrip liplisContext;
        private System.Windows.Forms.NotifyIcon icon;
        private System.Windows.Forms.ToolStripMenuItem tsmMenu;
        private System.Windows.Forms.ToolStripSeparator tss01;
        private System.Windows.Forms.ToolStripSeparator tss02;
    }
}

