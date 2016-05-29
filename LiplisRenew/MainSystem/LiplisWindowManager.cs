//=======================================================================
//  ClassName : LiplisWindowManager
//  概要      : リプリスウインドウマネージャー
//              ウインドウの生成、消去など、管理を行う。
//              また、各ウインドウの位置も管理する。
//
//
//Liplis5.0
//
//アップデート履歴
//   2016/05/21 ver5.0.0 作成
//
//  Copyright(c) 2010-2016 LipliStyle.Sachin
//=======================================================================
using Liplis.Msg;
using Liplis.Widget;
using Liplis.Widget.LpsWindow;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Liplis.MainSystem
{
    public class LiplisWindowManager
    {
        //=================================
        //ウィジェット設定
        private LiplisWidgetPreference setting;
        private Skin skin;

        //=================================
        //ウインドウインスタンス
        public List<LiplisWindow> talkWindowList { get; set; }
        public LiplisWindow nowTalkWindow { get; set; }

        //=================================
        //ウインドウロケーション管理
        public List<LiplisWindow> centerList;
        public List<LiplisWindow> leftList;
        public List<LiplisWindow> rightList;


        //============================================================
        //
        //初期化処理
        //
        //============================================================
        #region 初期化処理

        /// <summary>
        /// コンストラクター
        /// </summary>
        public LiplisWindowManager(LiplisWidgetPreference setting, Skin skin)
        {
            this.skin = skin;
            this.setting = setting;

            //クラスの初期化
            initClass();
        }

        /// <summary>
        /// クラスの初期化
        /// </summary>
        public void initClass()
        {
            //ウインドウリストの初期化
            talkWindowList = new List<LiplisWindow>();

            //ロケーションリストの初期化
            centerList = new List<LiplisWindow>();
            leftList = new List<LiplisWindow>();
            rightList = new List<LiplisWindow>();
        }


        #endregion

        //============================================================
        //
        //テキスト更新処理
        //
        //============================================================
        #region テキスト更新処理

        /// <summary>
        /// 現在ウインドウのテキストを更新する
        /// </summary>
        /// <param name="liplisChatText"></param>
        public void updateText(string liplisChatText)
        {
            this.nowTalkWindow.updateText(liplisChatText);
        }

        /// <summary>
        /// スキップ表示する
        /// </summary>
        /// <param name="liplisChatText"></param>
        public void updateSkip(string liplisChatText)
        {
            this.nowTalkWindow.updateSkip(liplisChatText);
        }

        #endregion

        //============================================================
        //
        //ウインドウ操作処理
        //
        //============================================================
        #region ウインドウ操作処理

        /// <summary>
        /// 現ウインドウ存在チェック
        /// </summary>
        /// <returns></returns>
        public bool nowWindowIsNull()
        {
            return nowTalkWindow == null;
        }

        /// <summary>
        /// ウインドウリストにあるウインドウをすべて閉じる
        /// </summary>
        public void closeWindowList()
        {
            foreach (var talkWindow in talkWindowList)
            {
                talkWindow.endWindow();
            }

            talkWindowList.Clear();
            centerList.Clear();
            leftList.Clear();
            rightList.Clear();
        }

        /// <summary>
        /// ファーストウインドウを表示する
        /// 要ディスパッチャー
        /// </summary>
        public void createFirstWindow()
        {
            //ウインドウが残っていたら消しておく
            if (talkWindowList.Count > 0)
            {
                closeWindowList();
            }

            //リストの最初期化
            talkWindowList = new List<LiplisWindow>();

            //トークウインドウ生成
            nowTalkWindow = new LiplisWindow(this.setting, this.skin);

            //出現
            nowTalkWindow.Show();

            //追加
            talkWindowList.Add(nowTalkWindow);
        }


        /// <summary>
        /// 新しいウインドウを追加する
        /// </summary>
        public void addNewWindow(double lpsTop, double lpsLeft, double lpsWidth, double lpsHeight)
        {
            //なうウインドウの移動
            setPrvWindowMove(lpsTop, lpsLeft, lpsWidth, lpsHeight);

            //ランダム移動の場合はイカの夜にする
            //nowTalkWindow.windowMoveRandam(lpsTop, lpsLeft, lpsWidth, lpsHeight);

            //トークウインドウ生成
            nowTalkWindow = new LiplisWindow(this.setting, this.skin);

            //出現
            nowTalkWindow.Show();

            //追加
            talkWindowList.Add(nowTalkWindow);
        }

        /// <summary>
        /// 新規ウインドウのロケーションを取得する
        /// </summary>
        private void setPrvWindowMove(double lpsTop, double lpsLeft, double lpsWidth, double lpsHeight)
        {
            //左、中、右に配置
            if(setting.lpsWindowStackMode == LiplisWindowStack.RightStarck)
            {
                //右移動
                nowWindowRightMove(lpsTop, lpsLeft, lpsWidth);
            }
            else if (setting.lpsWindowStackMode == LiplisWindowStack.LeeftStack)
            {
                //左移動
                nowWindowLeftMove(lpsTop,lpsLeft);
            }
            else
            {
                //初期
                if(centerList.Count == rightList.Count && centerList.Count == leftList.Count && rightList.Count == leftList.Count)
                {
                    //右移動
                    nowWindowRightMove(lpsTop, lpsLeft, lpsWidth);
                }
                else if(centerList.Count == leftList.Count)
                {
                    //中央移動
                    nowWindowCenterMove(lpsTop, lpsLeft, lpsWidth, lpsHeight);
                }
                else
                {
                    //左移動
                    nowWindowLeftMove(lpsTop, lpsLeft);
                }
            }

            //なうウインドウ移動
            nowTalkWindow.windowMove();
        }

        /// <summary>
        /// ナウウインドウを右の位置に移動させる
        /// </summary>
        /// <param name="lpsTop"></param>
        /// <param name="lpsLeft"></param>
        /// <param name="lpsWidth"></param>
        private void nowWindowRightMove(double lpsTop, double lpsLeft, double lpsWidth)
        {
            //右に配置
            double left = lpsLeft + lpsWidth;

            //初期ウインドウ表示
            if (rightList.Count == 0)
            {
                nowTalkWindow.LocationX = left;
                nowTalkWindow.LocationY = lpsTop;
            }
            else
            {
                nowTalkWindow.LocationX = left;
                nowTalkWindow.LocationY = rightList[rightList.Count - 1].Top + rightList[rightList.Count - 1].Height;
            }

            //右リストに追加
            rightList.Add(nowTalkWindow);
        }

        /// <summary>
        /// なうウインドウを左の位置に移動させる
        /// </summary>
        /// <param name="lpsTop"></param>
        /// <param name="lpsLeft"></param>
        private void nowWindowLeftMove(double lpsTop, double lpsLeft)
        {
            //左に配置
            double left = lpsLeft - nowTalkWindow.Width;

            //初期ウインドウ表示
            if (leftList.Count == 0)
            {
                nowTalkWindow.LocationX = left;
                nowTalkWindow.LocationY = lpsTop;
            }
            else
            {
                nowTalkWindow.LocationX = left;
                nowTalkWindow.LocationY = leftList[leftList.Count - 1].Top + leftList[leftList.Count - 1].Height;
            }

            //左リストに追加
            leftList.Add(nowTalkWindow);
        }

        /// <summary>
        /// なうウインドウを左の位置に移動させる
        /// </summary>
        /// <param name="lpsTop"></param>
        /// <param name="lpsLeft"></param>
        private void nowWindowCenterMove(double lpsTop, double lpsLeft, double lpsWidth, double lpsHeight)
        {
            //左に配置
            double left = lpsLeft - nowTalkWindow.Width;

            //中心位置計算
            Int32 locationCenter = (Int32)(lpsLeft + lpsWidth / 2);

            //中央に配置
            left = locationCenter - (Int32)nowTalkWindow.Width / 2; //レフト位置

            if (centerList.Count == 0)
            {
                nowTalkWindow.LocationX = left;
                nowTalkWindow.LocationY = lpsTop + lpsHeight / 2;
            }
            else
            {
                nowTalkWindow.LocationX = left;
                nowTalkWindow.LocationY = centerList[centerList.Count - 1].Top + centerList[centerList.Count - 1].Height;
            }

            centerList.Add(nowTalkWindow);
        }

        #endregion
    }
}
