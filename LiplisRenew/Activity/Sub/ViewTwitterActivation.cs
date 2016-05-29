//=======================================================================
//  ClassName : ViewTwitterActivation
//  概要      : ツイッターアクティベーション画面
//
//Liplis5.0
//
//  Copyright(c) 2010-2016 LipliStyle.Sachin
//=======================================================================
using System;
using System.Windows.Forms;

namespace Liplis.Activity.Sub
{
    public partial class ViewTwitterActivation : Form
    {
        ///=====================================
        /// オブジェクト
        public string pin { get; set; }

        /// <summary>
        /// コンストラクター
        /// </summary>
        #region コンストラクター
        public ViewTwitterActivation()
        {
            InitializeComponent();
            this.KeyPreview = true;
        }
        #endregion

        /// <summary>
        /// ピンコード取得
        /// 2014/02/02 ver3.2.4 ツイッターの認証方式変更
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        #region btnSendPin_Click
        private void btnSendPin_Click(object sender, EventArgs e)
        {
            try
            {
                pin = txtPin.Text;
            }
            catch
            {
            }
            this.Close();
        }
        #endregion


        /// <summary>
        /// キャンセル
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        #region btnCancel_Click
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion

        /// <summary>
        /// txtPin_KeyDown
        /// キーダウン
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        #region txtPin_KeyDown
        private void txtPin_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == System.Windows.Forms.Keys.Enter)
            {
                btnSendPin_Click(null, null);
            }
        }
        private void ActivityTwitterActivation_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == System.Windows.Forms.Keys.Escape)
            {
                this.Close();
            }
        }
        #endregion
    }
}
