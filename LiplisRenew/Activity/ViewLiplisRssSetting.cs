//=======================================================================
//  ClassName : ViewLiplisRssSetting
//  概要      : RSS設定画面
//
//
//Liplis5.0
//
//アップデート履歴
//   2016/05/08 ver5.0.0 作成
//
//  Copyright(c) 2010-2016 LipliStyle.Sachin
//=======================================================================
using Clalis.v31.Res;
using Liplis.Activity.Sub;
using Liplis.Gui;
using Liplis.MainSystem;
using Liplis.Web.Clalis;
using Liplis.Xml.Rss;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;

namespace Liplis.Activity
{
    public partial class ViewLiplisRssSetting : Form
    {
        //=================================
        //Liplis要素
        public LiplisPreference baseSetting;

        ///=====================================
        /// RSS関連情報
        ResLpsLoginRegisterInfoRssEachCat rssList;
        LiplisRssCategroyManager lrcm;
        RegisterRsUserInfoCat nowSelectCat;

        ///=====================================
        /// フラグ
        public const string CAT_DEFAULT = "デフォルトカテゴリ";

        //============================================================
        //
        //初期化処理
        //
        //============================================================
        #region 初期化処理
        /// <summary>
        /// コンストラクター
        /// </summary>
        public ViewLiplisRssSetting(LiplisPreference baseSetting)
        {
            //
            this.baseSetting = baseSetting;

            //画面初期化
            InitializeComponent();

            //ウインドウの初期化
            initWindow();

            //カテゴリマネージャー初期化
            lrcm = new LiplisRssCategroyManager();

            //RSS同期
            reloadRssList();
        }
        
        /// <summary>
        /// ウインドウの初期化
        /// </summary>
        private void initWindow()
        {
            dgv.MultiSelect = false;
            dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        }

        /// <summary>
        /// ツリーをリビルドする
        /// </summary>
        public void reBuildTree()
        {
            //ノードインデックス
            int parNodeIdx = 0;

            //オープンリスト
            List<string> openList = new List<string>();

            //▼このウインドウをロードするためにLiplisから呼ばれている
            this.Opacity = 1;

            //▼ツリービュー更新開始
            tvRss.BeginUpdate();

            //▼開いているツリービュー名を記録する
            foreach (TreeNode p in tvRss.Nodes) { if (p.IsExpanded) { openList.Add(p.Text); } }

            //▼ツリービュー初期化
            tvRss.Nodes.Clear();

            //▼カテゴリのツリー親ノード作成
            foreach (string cat in this.lrcm.catList)
            {
                //▼ノードの作成
                TreeNode tne = new TreeNode(cat);
                tne.Tag = new RegisterRsUserInfoCat(cat);
                tvRss.Nodes.Add(tne);
            }

            //▼RSSリストを回して、読み込み
            foreach (RegisterRsUserInfoCat rri in rssList.rsslist)
            {
                //▼カテゴリ名が空なら、"なし"を登録
                if (rri.cat == null || rri.cat == "")
                {
                    rri.cat = CAT_DEFAULT;
                }

                //▼フラグチェック
                if (lrcm.catList.Contains(rri.cat))
                {
                    //登録リストにあったので、カテゴリリストを更新する
                    TreeNode ltn = getTargetTreeNode(rri.cat);

                    //念のためNULLチェック(NULLはありえないが・・・)
                    if (ltn != null)
                    {
                        //ORCLをセットする
                        ltn.Tag = rri;
                        parNodeIdx = tvRss.Nodes.IndexOf(ltn);
                    }
                }
                else
                {
                    //▼ノードの作成                 
                    TreeNode tne = new TreeNode(rri.cat);
                    tvRss.Nodes.Add(tne);
                    parNodeIdx = tvRss.Nodes.Count - 1;
                }

                //▼子ノードの作成
                foreach (RegisterRsUserInfo rss in rri.rsslist)
                {
                    TreeNode cld = new TreeNode(rss.title.Trim());
                    cld.Tag = rss;
                    tvRss.Nodes[parNodeIdx].Nodes.Add(cld);
                }
            }

            //▼開いていたツリービューを開きなおす
            foreach (string name in openList)
            {
                foreach (TreeNode p in tvRss.Nodes)
                {
                    if (p.Text.Equals(name))
                    {
                        p.Expand();
                    }
                }
            }

            //▼ツリービュー更新完了
            tvRss.EndUpdate();

            //カテゴリーリストを更新しておく
            this.lrcm.saveKeyList();


            //なうカテゴリを更新しておく
            if(nowSelectCat != null)
            {
                //更新されたカテゴリデータを取得する
                RegisterRsUserInfoCat catData = rssList.getCatData(nowSelectCat.cat);

                //NULLでなければ再セットする
                if(catData != null)
                {
                    nowSelectCat = catData;
                }
            }
        }

        /// <summary>
        /// 名前から対象ノードを探す
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        private TreeNode getTargetTreeNode(string name)
        {
            foreach (TreeNode tn in tvRss.Nodes)
            {
                if (tn.Text == name)
                {
                    return tn;
                }
            }

            return null;
        }

        /// <summary>
        /// RSSリストの更新処理
        /// 非同期
        /// </summary>
        private void reloadRssList()
        {
            bwLoadRss.RunWorkerAsync();
        }

        /// <summary>
        /// RSS取得処理を実行する
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bwLoadRss_DoWork(object sender, DoWorkEventArgs e)
        {
            //RSSリストの再取得
            rssList = ClalisForLiplis.getUserRssList(baseSetting.uid);
        }

        /// <summary>
        /// RSSリスト更新完了時処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bwLoadRss_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            //NULLなら初期化しておく
            if(rssList == null)
            {
                rssList = new ResLpsLoginRegisterInfoRssEachCat();
            }

            //ツリーのリビルド
            reBuildTree();

            //データグリッドの更新
            onLoadDgv();

        }

        #endregion



        //============================================================
        //
        //メニューイベントハンドラ
        //
        //============================================================
        #region メニューイベントハンドラ
        private void tsmiEnd_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void tsmiCatAdd__Click(object sender, EventArgs e)
        {
            btnCatAdd_Click(sender, e);
        }

        private void tsmiCatSearch_Click(object sender, EventArgs e)
        {

        }

        private void tsmiCatReload_Click(object sender, EventArgs e)
        {
            //リビルド
            reBuildTree();

            //データグリッドの更新
            onLoadDgv();
        }
        #endregion

        //============================================================
        //
        //右クリックメニューイベントハンドラ
        //
        //============================================================
        #region 右クリックメニューイベントハンドラ
        private void tsmiCatAdd_Click(object sender, EventArgs e)
        {
            btnCatAdd_Click(sender, e);
        }

        private void tsmiCatDel_Click(object sender, EventArgs e)
        {
            onCatDelete();
        }
        #endregion


        //============================================================
        //
        //イベントハンドラ
        //
        //============================================================
        #region イベントハンドラ

        private void btnCatAdd_Click(object sender, EventArgs e)
        {
            using (ViewRssAdd vra = new ViewRssAdd(this))
            {
                vra.ShowDialog();
            }
        }
        private void btnRegist_Click(object sender, EventArgs e)
        {
            onRegist();
        }

        private void btnDel_Click(object sender, EventArgs e)
        {
            onDelete();
        }


        #endregion

        //============================================================
        //
        //リストビュー操作
        //
        //============================================================
        #region リストビュー操作
        /// <summary>
        /// ツリーセレクト時イベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tvRss_AfterSelect(object sender, TreeViewEventArgs e)
        {
            onSelect(e);
        }

        private void tvRss_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                tvRss.SelectedNode = tvRss.GetNodeAt(e.X, e.Y);
            }
        }

        /// <summary>
        /// ツリー選択時 データグリッドビュー表示処理
        /// </summary>
        /// <param name="e"></param>
        private void onSelect(TreeViewEventArgs e)
        {
            //選択ノードNULLチェック
            if (e.Node == null){return;}
            if (e.Node.Tag == null){return;}

            //選択ノード、ノード所持クラス
            TreeNode par = null;
            RegisterRsUserInfoCat rric = null;

            //ノード所持クラスのクラス名の取得
            string className = e.Node.Tag.GetType().Name;

            //クラス名から、カテゴリをくりくしたか、子をクリックしたか判断
            if (className == "RegisterRsUserInfoCat")
            {
                //カテゴリクリックなので自分自身を取得
                par = e.Node;
                rric = (RegisterRsUserInfoCat)e.Node.Tag;

            }
            else if (className == "RegisterRsUserInfo")
            {
                //子のクリックなので、親を取得
                par = e.Node.Parent;
                rric = (RegisterRsUserInfoCat)e.Node.Parent.Tag;
            }

            //ヌルチェック
            if (par == null) { return; }

            //カテゴリテキストに出力
            txtSelCat.Text = rric.cat;

            //カテゴリーリストを取得
            nowSelectCat = rric;

            //DGVの再表示
            onLoadDgv();

            //子のクリックなら、DGVの同行を選択する
            if (className == "RegisterRsUserInfo")
            {
                foreach (DataGridViewRow row in dgv.Rows)
                {
                    if(row.Cells[0].Value.ToString() == e.Node.Text)
                    {
                        dgv.CurrentCell = row.Cells[0];
                        break;
                    }
                }
            }
        }


        /// <summary>
        /// DGVの表示
        /// </summary>
        private void onLoadDgv()
        {
            try
            {
                //選択されていなければ無視
                if(nowSelectCat == null)
                {
                    return;
                }

                //dgvのクリア
                dgv.Rows.Clear();

                //dgvの作成
                foreach (RegisterRsUserInfo rss in nowSelectCat.rsslist)
                {
                    dgv.Rows.Add(new object[] { rss.title.Trim(), rss.url });
                }
            }
            catch
            {

            }

        }
        #endregion


        //============================================================
        //
        //RSS操作
        //
        //============================================================
        #region RSS操作
        /// <summary>
        /// RSS登録処理
        /// </summary>
        private void onRegist()
        {
            //空チェック
            if (txtUrl.Text.Equals(""))
            {
                LpsMessage.showError("URLが空です。");
                return;
            }

            //URL妥当性チェック
            if (!RssEnableChecker.checkRssConnect(txtUrl.Text))
            {
                LpsMessage.showError("有効なRSSのURLではありません");
                return;
            }

            //登録処理
            if (!rssList.containsRss(txtUrl.Text))
            {
                //登録処理 非同期実行
                bwRegisterRss.RunWorkerAsync();
            }
            else
            {
                LpsMessage.showError("既に登録されています。");
                return;
            }
        }
        /// <summary>
        /// RSS登録 非同期実行
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        string responseCode = "";
        private void bwRegisterRss_DoWork(object sender, DoWorkEventArgs e)
        {
            responseCode = ClalisForLiplis.registRss(txtUrl.Text, txtSelCat.Text, baseSetting.uid);
        }

        /// <summary>
        /// RSS登録 非同期実行完了
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bwRegisterRss_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (responseCode != "0")
            {
                LpsMessage.showError("RSSの登録に失敗しました。");
                return;
            }

            //ツリーのリビルド
            reloadRssList();

            //テキストのクリア
            txtUrl.Text = "";
        }

        /// <summary>
        /// RSS削除
        /// </summary>
        private void onDelete()
        {
            //削除処理
            if (rssList.containsRss(dgv[1, dgv.CurrentCell.RowIndex].Value.ToString()))
            {
                bwDeleteRss.RunWorkerAsync();
            }
            else
            {
                LpsMessage.showError("既に削除されています。");
                return;
            }
            //ツリーのリビルド
            reloadRssList();

            //データグリッドの更新
            onLoadDgv();
        }

        /// <summary>
        /// RSS削除 非同期実行
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bwDeleteRss_DoWork(object sender, DoWorkEventArgs e)
        {
            responseCode = ClalisForLiplis.deleteRss(baseSetting.uid, dgv[1, dgv.CurrentCell.RowIndex].Value.ToString());
        }

        /// <summary>
        /// RSS削除 非同期実行完了
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bwDeleteRss_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            //レスポンスコードが0でなければエラー
            if (responseCode != "0")
            {
                LpsMessage.showError("RSSの登録に削除しました。");
                return;
            }
        }

        #endregion

        //============================================================
        //
        //カテゴリ操作
        //
        //============================================================
        #region カテゴリ操作
        /// <summary>
        /// カテゴリ追加
        /// </summary>
        /// <param name="catName"></param>
        public void onCatRegist(string catName)
        {
            //カテゴリ追加
            lrcm.addKeyFromiUi(catName);

            //ツリーを再構成する
            reBuildTree();

            //データグリッドの更新
            onLoadDgv();
        }

        /// <summary>
        /// カテゴリデリート
        /// </summary>
        public void onCatDelete()
        {
            //削除確認
            if (MessageBox.Show("本当に削除しますか？","",MessageBoxButtons.OKCancel) == DialogResult.Cancel)
            {
                return;
            }

            if (tvRss.SelectedNode == null)
            {
                LpsMessage.showError("削除するカテゴリーを選択して下さい。");
                return;
            }

            if (tvRss.SelectedNode.Text.Equals(CAT_DEFAULT))
            {
                LpsMessage.showError("デフォルトカテゴリは削除できません。");
                return;
            }

            //選択チェック
            if (tvRss.SelectedNode.Text.Equals(""))
            {
                LpsMessage.showError("削除するカテゴリーを選択して下さい。");
                return;
            }


            //カテゴリ 非同期削除
            bwDeleteRssCat.RunWorkerAsync();
        }

        /// <summary>
        /// カテゴリ削除 非同期処理実行
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bwDeleteRssCat_DoWork(object sender, DoWorkEventArgs e)
        {
            //カテゴリーに含まれるURLをすべて削除する
            foreach (RegisterRsUserInfoCat cat in rssList.rsslist)
            {
                if (cat.cat == tvRss.SelectedNode.Text)
                {
                    foreach (RegisterRsUserInfo item in cat.rsslist)
                    {
                        ClalisForLiplis.deleteRss(baseSetting.uid, item.url);
                    }
                }
            }
        }

        /// <summary>
        /// カテゴリ削除処理完了時
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bwDeleteRssCat_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            //カテゴリを削除する
            lrcm.catList.Remove(tvRss.SelectedNode.Text);

            //ツリーのリビルド
            reloadRssList();

            //データグリッドの更新
            onLoadDgv();
        }


        #endregion


    }
}
