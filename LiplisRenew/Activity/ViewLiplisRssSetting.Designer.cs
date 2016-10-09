namespace Liplis.Activity
{
    partial class ViewLiplisRssSetting
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.TreeNode treeNode1 = new System.Windows.Forms.TreeNode("カテゴリ");
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ViewLiplisRssSetting));
            this.csmCat = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsmiCatAdd = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiCatDel = new System.Windows.Forms.ToolStripMenuItem();
            this.msMenu = new System.Windows.Forms.MenuStrip();
            this.tsmiFile = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiEnd = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiRss = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiCatAdd_ = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiCatSearch = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiCatReload = new System.Windows.Forms.ToolStripMenuItem();
            this.spc = new System.Windows.Forms.SplitContainer();
            this.spc3 = new System.Windows.Forms.SplitContainer();
            this.tvRss = new System.Windows.Forms.TreeView();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnCatAdd = new System.Windows.Forms.Button();
            this.spc2 = new System.Windows.Forms.SplitContainer();
            this.dgv = new System.Windows.Forms.DataGridView();
            this.TITLE = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.URL = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnDel = new System.Windows.Forms.Button();
            this.txtSelCat = new System.Windows.Forms.TextBox();
            this.lblRssSelCat = new System.Windows.Forms.Label();
            this.btnRegist = new System.Windows.Forms.Button();
            this.txtUrl = new System.Windows.Forms.TextBox();
            this.lblRssUrl = new System.Windows.Forms.Label();
            this.bwLoadRss = new System.ComponentModel.BackgroundWorker();
            this.bwDeleteRssCat = new System.ComponentModel.BackgroundWorker();
            this.bwRegisterRss = new System.ComponentModel.BackgroundWorker();
            this.bwDeleteRss = new System.ComponentModel.BackgroundWorker();
            this.csmCat.SuspendLayout();
            this.msMenu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.spc)).BeginInit();
            this.spc.Panel1.SuspendLayout();
            this.spc.Panel2.SuspendLayout();
            this.spc.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.spc3)).BeginInit();
            this.spc3.Panel1.SuspendLayout();
            this.spc3.Panel2.SuspendLayout();
            this.spc3.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.spc2)).BeginInit();
            this.spc2.Panel1.SuspendLayout();
            this.spc2.Panel2.SuspendLayout();
            this.spc2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).BeginInit();
            this.SuspendLayout();
            // 
            // csmCat
            // 
            this.csmCat.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiCatAdd,
            this.tsmiCatDel});
            this.csmCat.Name = "csmCat";
            this.csmCat.Size = new System.Drawing.Size(137, 48);
            // 
            // tsmiCatAdd
            // 
            this.tsmiCatAdd.Name = "tsmiCatAdd";
            this.tsmiCatAdd.Size = new System.Drawing.Size(136, 22);
            this.tsmiCatAdd.Text = "カテゴリ 追加";
            this.tsmiCatAdd.Click += new System.EventHandler(this.tsmiCatAdd_Click);
            // 
            // tsmiCatDel
            // 
            this.tsmiCatDel.Name = "tsmiCatDel";
            this.tsmiCatDel.Size = new System.Drawing.Size(136, 22);
            this.tsmiCatDel.Text = "カテゴリ 削除";
            this.tsmiCatDel.Click += new System.EventHandler(this.tsmiCatDel_Click);
            // 
            // msMenu
            // 
            this.msMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiFile,
            this.tsmiRss});
            this.msMenu.Location = new System.Drawing.Point(0, 0);
            this.msMenu.Name = "msMenu";
            this.msMenu.Size = new System.Drawing.Size(784, 24);
            this.msMenu.TabIndex = 8;
            this.msMenu.Text = "Twitter";
            // 
            // tsmiFile
            // 
            this.tsmiFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiEnd});
            this.tsmiFile.Name = "tsmiFile";
            this.tsmiFile.Size = new System.Drawing.Size(66, 20);
            this.tsmiFile.Text = "ファイル(&F)";
            // 
            // tsmiEnd
            // 
            this.tsmiEnd.Name = "tsmiEnd";
            this.tsmiEnd.Size = new System.Drawing.Size(113, 22);
            this.tsmiEnd.Text = "終了(&X)";
            this.tsmiEnd.Click += new System.EventHandler(this.tsmiEnd_Click);
            // 
            // tsmiRss
            // 
            this.tsmiRss.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiCatAdd_,
            this.tsmiCatSearch,
            this.tsmiCatReload});
            this.tsmiRss.Name = "tsmiRss";
            this.tsmiRss.Size = new System.Drawing.Size(53, 20);
            this.tsmiRss.Text = "RSS(&R)";
            // 
            // tsmiCatAdd_
            // 
            this.tsmiCatAdd_.Name = "tsmiCatAdd_";
            this.tsmiCatAdd_.Size = new System.Drawing.Size(133, 22);
            this.tsmiCatAdd_.Text = "カテゴリ追加";
            this.tsmiCatAdd_.Click += new System.EventHandler(this.tsmiCatAdd__Click);
            // 
            // tsmiCatSearch
            // 
            this.tsmiCatSearch.Name = "tsmiCatSearch";
            this.tsmiCatSearch.Size = new System.Drawing.Size(133, 22);
            this.tsmiCatSearch.Text = "検索";
            this.tsmiCatSearch.Click += new System.EventHandler(this.tsmiCatSearch_Click);
            // 
            // tsmiCatReload
            // 
            this.tsmiCatReload.Name = "tsmiCatReload";
            this.tsmiCatReload.Size = new System.Drawing.Size(133, 22);
            this.tsmiCatReload.Text = "再読み込み";
            this.tsmiCatReload.Click += new System.EventHandler(this.tsmiCatReload_Click);
            // 
            // spc
            // 
            this.spc.BackColor = System.Drawing.Color.White;
            this.spc.Dock = System.Windows.Forms.DockStyle.Fill;
            this.spc.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.spc.Location = new System.Drawing.Point(0, 24);
            this.spc.Name = "spc";
            // 
            // spc.Panel1
            // 
            this.spc.Panel1.Controls.Add(this.spc3);
            // 
            // spc.Panel2
            // 
            this.spc.Panel2.Controls.Add(this.spc2);
            this.spc.Size = new System.Drawing.Size(784, 537);
            this.spc.SplitterDistance = 195;
            this.spc.TabIndex = 10;
            // 
            // spc3
            // 
            this.spc3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.spc3.Location = new System.Drawing.Point(0, 0);
            this.spc3.Name = "spc3";
            this.spc3.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // spc3.Panel1
            // 
            this.spc3.Panel1.Controls.Add(this.tvRss);
            // 
            // spc3.Panel2
            // 
            this.spc3.Panel2.Controls.Add(this.panel1);
            this.spc3.Size = new System.Drawing.Size(195, 537);
            this.spc3.SplitterDistance = 475;
            this.spc3.TabIndex = 4;
            // 
            // tvRss
            // 
            this.tvRss.AllowDrop = true;
            this.tvRss.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(31)))), ((int)(((byte)(31)))));
            this.tvRss.ContextMenuStrip = this.csmCat;
            this.tvRss.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tvRss.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.tvRss.HideSelection = false;
            this.tvRss.Location = new System.Drawing.Point(0, 0);
            this.tvRss.Name = "tvRss";
            treeNode1.Name = "nodeCat";
            treeNode1.Text = "カテゴリ";
            this.tvRss.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode1});
            this.tvRss.Size = new System.Drawing.Size(195, 475);
            this.tvRss.TabIndex = 4;
            this.tvRss.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.tvRss_AfterSelect);
            this.tvRss.MouseDown += new System.Windows.Forms.MouseEventHandler(this.tvRss_MouseDown);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(31)))), ((int)(((byte)(31)))));
            this.panel1.Controls.Add(this.btnCatAdd);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(195, 58);
            this.panel1.TabIndex = 9;
            // 
            // btnCatAdd
            // 
            this.btnCatAdd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(223)))), ((int)(((byte)(116)))), ((int)(((byte)(1)))));
            this.btnCatAdd.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnCatAdd.Location = new System.Drawing.Point(11, 7);
            this.btnCatAdd.Name = "btnCatAdd";
            this.btnCatAdd.Size = new System.Drawing.Size(174, 45);
            this.btnCatAdd.TabIndex = 8;
            this.btnCatAdd.Text = "カテゴリ追加";
            this.btnCatAdd.UseVisualStyleBackColor = false;
            this.btnCatAdd.Click += new System.EventHandler(this.btnCatAdd_Click);
            // 
            // spc2
            // 
            this.spc2.BackColor = System.Drawing.Color.White;
            this.spc2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.spc2.IsSplitterFixed = true;
            this.spc2.Location = new System.Drawing.Point(0, 0);
            this.spc2.Name = "spc2";
            this.spc2.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // spc2.Panel1
            // 
            this.spc2.Panel1.Controls.Add(this.dgv);
            // 
            // spc2.Panel2
            // 
            this.spc2.Panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(31)))), ((int)(((byte)(31)))));
            this.spc2.Panel2.Controls.Add(this.btnDel);
            this.spc2.Panel2.Controls.Add(this.txtSelCat);
            this.spc2.Panel2.Controls.Add(this.lblRssSelCat);
            this.spc2.Panel2.Controls.Add(this.btnRegist);
            this.spc2.Panel2.Controls.Add(this.txtUrl);
            this.spc2.Panel2.Controls.Add(this.lblRssUrl);
            this.spc2.Size = new System.Drawing.Size(585, 537);
            this.spc2.SplitterDistance = 475;
            this.spc2.TabIndex = 0;
            // 
            // dgv
            // 
            this.dgv.AllowDrop = true;
            this.dgv.AllowUserToAddRows = false;
            this.dgv.AllowUserToDeleteRows = false;
            this.dgv.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgv.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(31)))), ((int)(((byte)(31)))));
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("MS UI Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Info;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgv.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgv.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.TITLE,
            this.URL});
            this.dgv.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgv.Location = new System.Drawing.Point(0, 0);
            this.dgv.Name = "dgv";
            this.dgv.ReadOnly = true;
            this.dgv.RowTemplate.Height = 21;
            this.dgv.Size = new System.Drawing.Size(585, 475);
            this.dgv.TabIndex = 0;
            // 
            // TITLE
            // 
            this.TITLE.HeaderText = "TITLE";
            this.TITLE.Name = "TITLE";
            this.TITLE.ReadOnly = true;
            // 
            // URL
            // 
            this.URL.HeaderText = "URL";
            this.URL.Name = "URL";
            this.URL.ReadOnly = true;
            // 
            // btnDel
            // 
            this.btnDel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(223)))), ((int)(((byte)(116)))), ((int)(((byte)(1)))));
            this.btnDel.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnDel.Location = new System.Drawing.Point(506, 7);
            this.btnDel.Name = "btnDel";
            this.btnDel.Size = new System.Drawing.Size(73, 45);
            this.btnDel.TabIndex = 9;
            this.btnDel.Text = "削除";
            this.btnDel.UseVisualStyleBackColor = false;
            this.btnDel.Click += new System.EventHandler(this.btnDel_Click);
            // 
            // txtSelCat
            // 
            this.txtSelCat.BackColor = System.Drawing.SystemColors.Info;
            this.txtSelCat.Location = new System.Drawing.Point(76, 8);
            this.txtSelCat.Name = "txtSelCat";
            this.txtSelCat.ReadOnly = true;
            this.txtSelCat.Size = new System.Drawing.Size(118, 19);
            this.txtSelCat.TabIndex = 8;
            // 
            // lblRssSelCat
            // 
            this.lblRssSelCat.AutoSize = true;
            this.lblRssSelCat.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.lblRssSelCat.Location = new System.Drawing.Point(7, 11);
            this.lblRssSelCat.Name = "lblRssSelCat";
            this.lblRssSelCat.Size = new System.Drawing.Size(63, 12);
            this.lblRssSelCat.TabIndex = 7;
            this.lblRssSelCat.Text = "選択カテゴリ";
            // 
            // btnRegist
            // 
            this.btnRegist.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(223)))), ((int)(((byte)(116)))), ((int)(((byte)(1)))));
            this.btnRegist.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnRegist.Location = new System.Drawing.Point(403, 7);
            this.btnRegist.Name = "btnRegist";
            this.btnRegist.Size = new System.Drawing.Size(73, 45);
            this.btnRegist.TabIndex = 6;
            this.btnRegist.Text = "登録";
            this.btnRegist.UseVisualStyleBackColor = false;
            this.btnRegist.Click += new System.EventHandler(this.btnRegist_Click);
            // 
            // txtUrl
            // 
            this.txtUrl.BackColor = System.Drawing.SystemColors.Info;
            this.txtUrl.Location = new System.Drawing.Point(76, 33);
            this.txtUrl.Name = "txtUrl";
            this.txtUrl.Size = new System.Drawing.Size(321, 19);
            this.txtUrl.TabIndex = 4;
            // 
            // lblRssUrl
            // 
            this.lblRssUrl.AutoSize = true;
            this.lblRssUrl.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.lblRssUrl.Location = new System.Drawing.Point(7, 36);
            this.lblRssUrl.Name = "lblRssUrl";
            this.lblRssUrl.Size = new System.Drawing.Size(27, 12);
            this.lblRssUrl.TabIndex = 1;
            this.lblRssUrl.Text = "URL";
            // 
            // bwLoadRss
            // 
            this.bwLoadRss.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bwLoadRss_DoWork);
            this.bwLoadRss.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bwLoadRss_RunWorkerCompleted);
            // 
            // bwDeleteRssCat
            // 
            this.bwDeleteRssCat.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bwDeleteRssCat_DoWork);
            this.bwDeleteRssCat.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bwDeleteRssCat_RunWorkerCompleted);
            // 
            // bwRegisterRss
            // 
            this.bwRegisterRss.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bwRegisterRss_DoWork);
            this.bwRegisterRss.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bwRegisterRss_RunWorkerCompleted);
            // 
            // bwDeleteRss
            // 
            this.bwDeleteRss.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bwDeleteRss_DoWork);
            this.bwDeleteRss.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bwDeleteRss_RunWorkerCompleted);
            // 
            // ViewLiplisRssSetting
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(31)))), ((int)(((byte)(31)))));
            this.ClientSize = new System.Drawing.Size(784, 561);
            this.Controls.Add(this.spc);
            this.Controls.Add(this.msMenu);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "ViewLiplisRssSetting";
            this.Text = "Liplis Rss設定";
            this.csmCat.ResumeLayout(false);
            this.msMenu.ResumeLayout(false);
            this.msMenu.PerformLayout();
            this.spc.Panel1.ResumeLayout(false);
            this.spc.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.spc)).EndInit();
            this.spc.ResumeLayout(false);
            this.spc3.Panel1.ResumeLayout(false);
            this.spc3.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.spc3)).EndInit();
            this.spc3.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.spc2.Panel1.ResumeLayout(false);
            this.spc2.Panel2.ResumeLayout(false);
            this.spc2.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.spc2)).EndInit();
            this.spc2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ContextMenuStrip csmCat;
        private System.Windows.Forms.ToolStripMenuItem tsmiCatAdd;
        private System.Windows.Forms.ToolStripMenuItem tsmiCatDel;
        private System.Windows.Forms.MenuStrip msMenu;
        private System.Windows.Forms.ToolStripMenuItem tsmiFile;
        private System.Windows.Forms.ToolStripMenuItem tsmiEnd;
        private System.Windows.Forms.ToolStripMenuItem tsmiRss;
        private System.Windows.Forms.ToolStripMenuItem tsmiCatAdd_;
        private System.Windows.Forms.ToolStripMenuItem tsmiCatSearch;
        private System.Windows.Forms.ToolStripMenuItem tsmiCatReload;
        private System.Windows.Forms.SplitContainer spc;
        private System.Windows.Forms.SplitContainer spc3;
        private System.Windows.Forms.TreeView tvRss;
        private System.Windows.Forms.Button btnCatAdd;
        private System.Windows.Forms.SplitContainer spc2;
        private System.Windows.Forms.DataGridView dgv;
        private System.Windows.Forms.DataGridViewTextBoxColumn TITLE;
        private System.Windows.Forms.DataGridViewTextBoxColumn URL;
        private System.Windows.Forms.TextBox txtSelCat;
        private System.Windows.Forms.Label lblRssSelCat;
        private System.Windows.Forms.Button btnRegist;
        private System.Windows.Forms.TextBox txtUrl;
        private System.Windows.Forms.Label lblRssUrl;
        private System.Windows.Forms.Button btnDel;
        private System.Windows.Forms.Panel panel1;
        private System.ComponentModel.BackgroundWorker bwLoadRss;
        private System.ComponentModel.BackgroundWorker bwDeleteRssCat;
        private System.ComponentModel.BackgroundWorker bwRegisterRss;
        private System.ComponentModel.BackgroundWorker bwDeleteRss;
    }
}