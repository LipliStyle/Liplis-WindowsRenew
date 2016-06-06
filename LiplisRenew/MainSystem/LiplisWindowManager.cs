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
using Liplis.Widget;
using Liplis.Widget.LpsWindow;
using System;
using System.Collections.Generic;

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
        public void createFirstWindow(double lpsTop, double lpsLeft, double lpsWidth)
        {
            //ウインドウが残っていたら消しておく
            if (talkWindowList.Count > 0)
            {
                closeWindowList();
            }

            //リストの最初期化
            talkWindowList = new List<LiplisWindow>();

            //トークウインドウ生成
            nowTalkWindow = new LiplisWindow(this.setting, this.skin, lpsTop, lpsLeft, lpsWidth, LiplisWindowStack.NowTalkPos);

            //出現
            nowTalkWindow.Show();

            //追加
            talkWindowList.Add(nowTalkWindow);
        }

        /// <summary>
        /// タイトルウインドウを表示する
        /// </summary>
        public void createTitleWindow(double lpsTop, double lpsLeft, double lpsWidth, double lpsHeight, string title, string url, string jpgUrl)
        {
            //URI
            Uri uri;
            Uri jpgUri = null;

            try
            {
                //URI生成
                uri = new Uri(url);
            }
            catch
            {
                //適切なURI出ない場合は、通常ウインドウとして開くウインドウ生成
                createFirstWindow(lpsTop, lpsLeft, lpsWidth);

                //ウインドウを生成し、終了
                return;
            }

            //ピクチャーウインドウ表示
            if (jpgUrl != null)
            {
                if (jpgUrl != "")
                {
                    try
                    {
                        //URI生成
                        jpgUri = new Uri(jpgUrl);
                    }
                    catch
                    {
                    }
                }
            }

            //ウインドウが残っていたら消しておく
            if (talkWindowList.Count > 0)
            {
                closeWindowList();
            }

            //リストの最初期化
            talkWindowList = new List<LiplisWindow>();

            //トークウインドウ生成
            nowTalkWindow = new LiplisTitleWindow(this.setting, this.skin, lpsTop, lpsLeft, lpsWidth, LiplisWindowStack.NowTalkPos, title, uri, jpgUri);

            //出現
            nowTalkWindow.Show();

            //スキッス
            nowTalkWindow.updateSkip(title);

            //追加
            talkWindowList.Add(nowTalkWindow);

            //おしゃべりウインドウ追加
            addNewWindow(lpsTop, lpsLeft, lpsWidth, lpsHeight);
        }


        /// <summary>
        /// 新しいウインドウを追加する
        /// </summary>
        public void addNewWindow(double lpsTop, double lpsLeft, double lpsWidth, double lpsHeight)
        {
            //古いウインドウを終了する
            delOldWindow();

            //なうウインドウの移動
            setPrvWindowMove(lpsTop, lpsLeft, lpsWidth, lpsHeight);

            //ランダム移動の場合は以下のようにする
            //nowTalkWindow.windowMoveRandam(lpsTop, lpsLeft, lpsWidth, lpsHeight);

            //トークウインドウ生成
            nowTalkWindow = new LiplisWindow(this.setting, this.skin, lpsTop, lpsLeft, lpsWidth, LiplisWindowStack.NowTalkPos);

            //出現
            nowTalkWindow.Show();

            //追加
            talkWindowList.Add(nowTalkWindow);
        }

        /// <summary>
        /// 古いウインドウを終了する
        /// </summary>
        public void delOldWindow()
        {
            //最大数以上なら、最古参を終了する
            if (talkWindowList.Count >= setting.lpsWindowNum)
            {
                //削除前のウインドウの高さを取得しておく
                double top = talkWindowList[0].Top;
                LiplisWindowStack windowPos = talkWindowList[0].windowPos;

                //削除する
                talkWindowList[0].endWindow();
                talkWindowList.RemoveAt(0);

                //今あるウインドウをスライドさせる
                foreach (LiplisWindow window in talkWindowList)
                {
                    window.LocationX = window.Left;
                    window.LocationY = top;
                    window.windowMove(window.windowPos);
                }
            }
        }

        /// <summary>
        /// 新規ウインドウのロケーションを取得する
        /// </summary>
        private void setPrvWindowMove(double lpsTop, double lpsLeft, double lpsWidth, double lpsHeight)
        {
            LiplisWindowStack windowPos = LiplisWindowStack.NowTalkPos;

            //ウインドウが1個だけの場合は、上に移動させる
            if (setting.lpsWindowNum == 1)
            {
                nowWindowUpperMove(lpsTop, lpsLeft, lpsWidth, lpsHeight);
            }

            //左、中、右に配置
            else if(setting.lpsWindowPos == LiplisWindowStack.RightStarck)
            {
                //右移動
                nowWindowRightMove(lpsTop, lpsLeft, lpsWidth);
                windowPos = LiplisWindowStack.RightStarck;
            }
            else if (setting.lpsWindowPos == LiplisWindowStack.LeeftStack)
            {
                //左移動
                nowWindowLeftMove(lpsTop,lpsLeft);
                windowPos = LiplisWindowStack.LeeftStack;
            }
            else
            {
                //初期
                if(centerList.Count == rightList.Count && centerList.Count == leftList.Count && rightList.Count == leftList.Count)
                {
                    //右移動
                    nowWindowRightMove(lpsTop, lpsLeft, lpsWidth);
                    windowPos = LiplisWindowStack.RightStarck;
                }
                else if(centerList.Count == leftList.Count)
                {
                    //中央移動
                    nowWindowCenterMove(lpsTop, lpsLeft, lpsWidth, lpsHeight);
                    windowPos = LiplisWindowStack.AveStack;
                }
                else
                {
                    //左移動
                    nowWindowLeftMove(lpsTop, lpsLeft);
                    windowPos = LiplisWindowStack.LeeftStack;
                }
            }

            //なうウインドウ移動
            nowTalkWindow.windowMove(windowPos);
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

            if (setting.lpsWindowNum == 2)
            {
                nowTalkWindow.LocationX = left;
                nowTalkWindow.LocationY = lpsTop;
            }

            //初期ウインドウ表示
            else if (rightList.Count == 0)
            {
                nowTalkWindow.LocationX = left;
                nowTalkWindow.LocationY = lpsTop;
            }
            else
            {
                nowTalkWindow.LocationX = left;
                nowTalkWindow.LocationY = rightList[rightList.Count - 1].LocationY + rightList[rightList.Count - 1].Height;
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

            if (setting.lpsWindowNum == 2)
            {
                nowTalkWindow.LocationX = left;
                nowTalkWindow.LocationY = lpsTop;
            }

            //初期ウインドウ表示
            else if (leftList.Count == 0)
            {
                nowTalkWindow.LocationX = left;
                nowTalkWindow.LocationY = lpsTop;
            }
            else
            {
                nowTalkWindow.LocationX = left;
                nowTalkWindow.LocationY = leftList[leftList.Count - 1].LocationY + leftList[leftList.Count - 1].Height;
            }

            //左リストに追加
            leftList.Add(nowTalkWindow);
        }

        /// <summary>
        /// なうウインドウを中央の位置に移動させる
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

            if (setting.lpsWindowNum == 2)
            {
                nowTalkWindow.LocationX = left;
                nowTalkWindow.LocationY = lpsTop + lpsHeight / 2;
            }

            else if (centerList.Count == 0)
            {
                nowTalkWindow.LocationX = left;
                nowTalkWindow.LocationY = lpsTop + lpsHeight / 2;
            }
            else
            {
                nowTalkWindow.LocationX = left;
                nowTalkWindow.LocationY = centerList[centerList.Count - 1].LocationY + centerList[centerList.Count - 1].Height;
            }

            centerList.Add(nowTalkWindow);
        }


        /// <summary>
        /// なうウインドウを上の位置に移動させる
        /// </summary>
        /// <param name="lpsTop"></param>
        /// <param name="lpsLeft"></param>
        private void nowWindowUpperMove(double lpsTop, double lpsLeft, double lpsWidth, double lpsHeight)
        {
            //左に配置
            double left = lpsLeft - nowTalkWindow.Width;

            //中心位置計算
            Int32 locationCenter = (Int32)(lpsLeft + lpsWidth / 2);

            //中央に配置
            left = locationCenter - (Int32)nowTalkWindow.Width / 2; //レフト位置


            nowTalkWindow.LocationX = left;
            nowTalkWindow.LocationY = nowTalkWindow.Top - 100;
        }
        #endregion
    }
}
