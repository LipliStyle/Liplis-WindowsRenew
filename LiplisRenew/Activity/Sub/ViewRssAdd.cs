using Liplis.Com;
using Liplis.Gui;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Liplis.Activity.Sub
{
    public partial class ViewRssAdd : Form
    {
        ///=====================================
        //親インスタンス
        private ViewLiplisRssSetting vlr;


        //============================================================
        //
        //初期化処理
        //
        //============================================================
        #region 初期化処理
        /// <summary>
        /// コンストラクター
        /// </summary>
        public ViewRssAdd(ViewLiplisRssSetting arr)
        {
            InitializeComponent();
            this.vlr = arr;
            this.KeyPreview = true;
            initWindow();
        }

        /// <summary>
        /// ウインドウ初期化
        /// </summary>
        private void initWindow()
        {
            this.Text = "カテゴリ追加";
            this.btnCatAdd.Text = "修正";
            this.txtCat.Text = "";
        }

        /// <summary>
        /// ロードイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ActivityRssRegistAddCat_Load(object sender, EventArgs e)
        {

        }
        #endregion

        //============================================================
        //
        //イベントハンドラ
        //
        //============================================================
        #region イベントハンドラ
        /// <summary>
        /// 追加ボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCatAdd_Click(object sender, EventArgs e)
        {
            onCatRegist();
        }

        /// <summary>
        /// エンターキーで決定
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtCat_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == System.Windows.Forms.Keys.Enter)
            {
                btnCatAdd_Click(null, null);
            }
        }

        /// <summary>
        /// キャンセル
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// キーダウン
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ActivityRssRegistAddCat_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == System.Windows.Forms.Keys.Escape)
            {
                this.Close();
            }
        }
        #endregion

        /// <summary>
        /// onCatRegist
        /// カテゴリ登録
        /// </summary>
        #region onCatRegist
        private void onCatRegist()
        {
            //テキストチェック
            if (txtCat.Text.Equals(""))
            {
                LpsMessage.showError("カテゴリーを入力して下さい。");
                return;
            }

            //テキストチェック
            if (txtCat.Text.Equals(ViewLiplisRssSetting.CAT_DEFAULT))
            {
                LpsMessage.showError("その名前は使用できません");
                return;
            }

            //親に処理を投げる
            vlr.onCatRegist(txtCat.Text);

            this.Close();
        }
        #endregion



    }
}
