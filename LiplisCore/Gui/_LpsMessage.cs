//=======================================================================
//  ClassName : LpsMessage
//  概要      : リプリスメッセージ
//
//  Liplis5.0
//
//  Copyright(c) 2010-2016 LipliStyle.Sachin
//=======================================================================
using System;
using System.Windows.Forms;

namespace Liplis.Gui
{
    public partial class _LpsMessage : Form
    {
        /// <summary>
        /// コンストラクター
        /// </summary>
        public _LpsMessage()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 閉じるボタン
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }


        /// <summary>
        /// メッセージを表示する
        /// </summary>
        /// <returns></returns>
        public void popMessage(string title, string msg)
        {
            try
            {
                this.Text = title;
                this.lblMessage.Text = msg;
                this.Focus();
                this.ShowDialog();
            }
            catch
            {
                return;
            }
        }

        /// <summary>
        /// キーイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LpsMessage_KeyDown(object sender, KeyEventArgs e)
        {
            //エスケープの受け取り。
            //エスケープを受け取る場合は、フォームのKeyPreviewプロパティをTrueに設定する
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }
    }
}
