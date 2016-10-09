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
        private LiplisWidget lips;
        private LiplisWidgetPreference setting;
        private Skin skin;

        //=================================
        //ウインドウインスタンス
        public List<LiplisWindow> talkWindowList { get; set; }
        public List<LiplisWindow> everyoneTitleWindowList { get; set; }
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
        public LiplisWindowManager(LiplisWidget lips, LiplisWidgetPreference setting, Skin skin)
        {
            this.lips = lips;
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
            talkWindowList          = new List<LiplisWindow>();
            everyoneTitleWindowList = new List<LiplisWindow>();

            //ロケーションリストの初期化
            centerList              = new List<LiplisWindow>();
            leftList                = new List<LiplisWindow>();
            rightList               = new List<LiplisWindow>();
            
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

            //トークウインドウリストのクリア
            talkWindowList.Clear();

            //センターリストのクリア
            centerList.Clear();

            //レフトリストのクリア
            leftList.Clear();

            //ライトリストのクリア
            rightList.Clear();
        }

        /// <summary>
        /// みんなでおしゃべりタイトルウインドウを削除する
        /// </summary>
        public void closeEveryoneTitleWindow()
        {
            foreach (var talkWindow in everyoneTitleWindowList)
            {
                talkWindow.endWindow();
            }

            //トークウインドウリストのクリア
            everyoneTitleWindowList.Clear();
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
            nowTalkWindow = new LiplisWindow(lips, this.setting, this.skin, lpsTop, lpsLeft, lpsWidth, LiplisWindowStack.NowTalkPos);

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
            //URI生成
            Uri uri = getUri(url);
            Uri jpgUri = getJpgUri(jpgUrl);

            //URIチェック
            if(uri == null)
            {
                //適切なURI出ない場合は、通常ウインドウとして開くウインドウ生成
                createFirstWindow(lpsTop, lpsLeft, lpsWidth);

                //ウインドウを生成し、終了
                return;
            }

            //ウインドウが残っていたら消しておく
            if (talkWindowList.Count > 0)
            {
                closeWindowList();
            }

            //リストの最初期化
            talkWindowList = new List<LiplisWindow>();

            //トークウインドウ生成
            nowTalkWindow = new LiplisTitleWindow(lips, this.setting, this.skin, lpsTop, lpsLeft, lpsWidth, LiplisWindowStack.NowTalkPos, title, uri, jpgUri);

            //出現
            nowTalkWindow.Show();

            //スキップ
            nowTalkWindow.updateSkip(title);

            //追加
            talkWindowList.Add(nowTalkWindow);

            //おしゃべりウインドウ追加
            addNewWindow(lpsTop, lpsLeft, lpsWidth, lpsHeight);
        }

        /// <summary>
        /// みんなでおしゃべりタイトルウインドウ追加
        /// </summary>
        /// <param name="lpsTop"></param>
        /// <param name="lpsLeft"></param>
        /// <param name="lpsWidth"></param>
        /// <param name="lpsHeight"></param>
        /// <param name="title"></param>
        /// <param name="url"></param>
        /// <param name="jpgUrl"></param>
        public void createEveryoneTitleWindow(double lpsTop, double lpsLeft, double lpsWidth, double lpsHeight, string title, string url, string jpgUrl)
        {
            //URI生成
            Uri uri = getUri(url);
            Uri jpgUri = getJpgUri(jpgUrl);

            //URIチェック
            if (uri == null)
            {
                //適切なURI出ない場合は、通常ウインドウとして開くウインドウ生成
                createFirstWindow(lpsTop, lpsLeft, lpsWidth);

                //ウインドウを生成し、終了
                return;
            }

            //ウインドウが残っていたら消しておく
            if (everyoneTitleWindowList.Count > 0)
            {
                closeEveryoneTitleWindow();
            }

            //リストの最初期化
            everyoneTitleWindowList = new List<LiplisWindow>();

            //トークウインドウ生成
            LiplisWindow evTitleWindow = new LiplisEveryoneTitleWindow(lips, this.setting, this.skin, lpsTop, lpsLeft, lpsWidth, LiplisWindowStack.NowTalkPos, title, uri, jpgUri);

            //ランダム位置
            //evTitleWindow.windowMoveRandam(lpsTop, lpsLeft, lpsWidth, lpsHeight);

            //出現
            evTitleWindow.Show();

            //スキップ
            evTitleWindow.updateSkip(title);

            //追加
            everyoneTitleWindowList.Add(evTitleWindow);

            setPrvWindowMove(evTitleWindow, lpsTop, lpsLeft, lpsWidth, lpsHeight);
        }

        /// <summary>
        /// URIを取得する
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        private Uri getUri(string url)
        {
            try
            {
                //URI生成
                return new Uri(url);
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// JPGURIを取得する
        /// </summary>
        /// <param name="jpgUrl"></param>
        /// <returns></returns>
        private Uri getJpgUri(string jpgUrl)
        {
            if (jpgUrl != null)
            {
                if (jpgUrl != "")
                {
                    try
                    {
                        //URI生成
                        return new Uri(jpgUrl);
                    }
                    catch
                    {
                        return null;
                    }
                }
            }

            return null;
        }


        /// <summary>
        /// 新しいウインドウを追加する
        /// </summary>
        public void addNewWindow(double lpsTop, double lpsLeft, double lpsWidth, double lpsHeight)
        {
            //古いウインドウを終了する
            delOldWindow(lpsTop, lpsLeft, lpsWidth);

            //なうウインドウの移動
            setPrvWindowMove(nowTalkWindow, lpsTop, lpsLeft, lpsWidth, lpsHeight);

            //ランダム移動の場合は以下のようにする
            //nowTalkWindow.windowMoveRandam(lpsTop, lpsLeft, lpsWidth, lpsHeight);

            //トークウインドウ生成
            nowTalkWindow = new LiplisWindow(lips, this.setting, this.skin, lpsTop, lpsLeft, lpsWidth, LiplisWindowStack.NowTalkPos);

            //出現
            nowTalkWindow.Show();

            //追加
            talkWindowList.Add(nowTalkWindow);
        }

        /// <summary>
        /// 古いウインドウを終了する
        /// </summary>
        public void delOldWindow(double lpsTop, double lpsLeft, double lpsWidth)
        {
            double diff = 0;

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
                switch (windowPos)
                {
                    case LiplisWindowStack.LeeftStack:
                        //レフトーリストの先頭を一つ削除
                        leftList.RemoveAt(0);

                        //リスト1つ目のウインドウと移動先との差を算出
                        diff = leftList[0].Top - top;

                        foreach (LiplisWindow window in leftList)
                        {
                            window.LocationX = lpsLeft - window.Width;
                            window.LocationY = window.Top - diff;
                            window.windowMove(window.windowPos);
                        }

                        break;
                    case LiplisWindowStack.RightStarck:
                        //ライトリストの先頭を一つ削除
                        rightList.RemoveAt(0);

                        //リスト1つ目のウインドウと移動先との差を算出
                        diff = rightList[0].Top - top;

                        foreach (LiplisWindow window in rightList)
                        {
                            window.LocationX = lpsLeft + lpsWidth;
                            window.LocationY = window.Top - diff;
                            window.windowMove(window.windowPos);
                        }


                        break;
                    case LiplisWindowStack.AveStack:
                        //センターリストの先頭を一つ削除
                        centerList.RemoveAt(0);

                        //左に配置
                        double left = lpsLeft - nowTalkWindow.Width;

                        //中心位置計算
                        Int32 locationCenter = (Int32)(lpsLeft + lpsWidth / 2);

                        //中央に配置
                        left = locationCenter - (Int32)nowTalkWindow.Width / 2; //レフト位置

                        //リスト1つ目のウインドウと移動先との差を算出
                        diff = centerList[0].Top - top;

                        foreach (LiplisWindow window in centerList)
                        {
                            window.LocationX = left;
                            window.LocationY = window.Top - diff;
                            window.windowMove(window.windowPos);
                        }

                        break;
                    default:
                        break;
                }

            }
        }

        /// <summary>
        /// 新規ウインドウのロケーションを取得する
        /// </summary>
        private void setPrvWindowMove(LiplisWindow nowTalkWindow, double lpsTop, double lpsLeft, double lpsWidth, double lpsHeight)
        {
            LiplisWindowStack windowPos = LiplisWindowStack.NowTalkPos;

            //ウインドウが1個だけの場合は、上に移動させる
            if (setting.lpsWindowNum == 1)
            {
                nowWindowUpperMove(nowTalkWindow, lpsTop, lpsLeft, lpsWidth, lpsHeight);
            }

            //左、中、右に配置
            else if(setting.lpsWindowPos == LiplisWindowStack.RightStarck)
            {
                //右移動
                nowWindowRightMove(nowTalkWindow, lpsTop, lpsLeft, lpsWidth);
                windowPos = LiplisWindowStack.RightStarck;
            }
            else if (setting.lpsWindowPos == LiplisWindowStack.LeeftStack)
            {
                //左移動
                nowWindowLeftMove(nowTalkWindow, lpsTop, lpsLeft);
                windowPos = LiplisWindowStack.LeeftStack;
            }
            else
            {
                //初期
                if(centerList.Count == rightList.Count && centerList.Count == leftList.Count && rightList.Count == leftList.Count)
                {
                    //右移動
                    nowWindowRightMove(nowTalkWindow, lpsTop, lpsLeft, lpsWidth);
                    windowPos = LiplisWindowStack.RightStarck;
                }
                else if(centerList.Count == leftList.Count)
                {
                    //中央移動
                    nowWindowCenterMove(nowTalkWindow, lpsTop, lpsLeft, lpsWidth, lpsHeight);
                    windowPos = LiplisWindowStack.AveStack;
                }
                else
                {
                    //左移動
                    nowWindowLeftMove(nowTalkWindow, lpsTop, lpsLeft);
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
        private void nowWindowRightMove(LiplisWindow targetWindow, double lpsTop, double lpsLeft, double lpsWidth)
        {
            //右に配置
            double left = lpsLeft + lpsWidth;

            if (setting.lpsWindowNum == 2)
            {
                targetWindow.LocationX = left;
                targetWindow.LocationY = lpsTop;
            }

            //初期ウインドウ表示
            else if (rightList.Count == 0)
            {
                targetWindow.LocationX = left;
                targetWindow.LocationY = lpsTop;
            }
            else
            {
                targetWindow.LocationX = left;
                targetWindow.LocationY = rightList[rightList.Count - 1].LocationY + rightList[rightList.Count - 1].Height;
            }

            //右リストに追加
            rightList.Add(targetWindow);
        }

        /// <summary>
        /// なうウインドウを左の位置に移動させる
        /// </summary>
        /// <param name="lpsTop"></param>
        /// <param name="lpsLeft"></param>
        private void nowWindowLeftMove(LiplisWindow targetWindow, double lpsTop, double lpsLeft)
        {
            //左に配置
            double left = lpsLeft - targetWindow.Width;

            if (setting.lpsWindowNum == 2)
            {
                targetWindow.LocationX = left;
                targetWindow.LocationY = lpsTop;
            }

            //初期ウインドウ表示
            else if (leftList.Count == 0)
            {
                targetWindow.LocationX = left;
                targetWindow.LocationY = lpsTop;
            }
            else
            {
                targetWindow.LocationX = left;
                targetWindow.LocationY = leftList[leftList.Count - 1].LocationY + leftList[leftList.Count - 1].Height;
            }

            //左リストに追加
            leftList.Add(targetWindow);
        }

        /// <summary>
        /// なうウインドウを中央の位置に移動させる
        /// </summary>
        /// <param name="lpsTop"></param>
        /// <param name="lpsLeft"></param>
        private void nowWindowCenterMove(LiplisWindow targetWindow, double lpsTop, double lpsLeft, double lpsWidth, double lpsHeight)
        {
            //左に配置
            double left = lpsLeft - targetWindow.Width;

            //中心位置計算
            Int32 locationCenter = (Int32)(lpsLeft + lpsWidth / 2);

            //中央に配置
            left = locationCenter - (Int32)targetWindow.Width / 2; //レフト位置

            if (setting.lpsWindowNum == 2)
            {
                targetWindow.LocationX = left;
                targetWindow.LocationY = lpsTop + lpsHeight / 2;
            }

            else if (centerList.Count == 0)
            {
                targetWindow.LocationX = left;
                targetWindow.LocationY = lpsTop + lpsHeight / 2;
            }
            else
            {
                targetWindow.LocationX = left;
                targetWindow.LocationY = centerList[centerList.Count - 1].LocationY + centerList[centerList.Count - 1].Height;
            }

            centerList.Add(targetWindow);
        }


        /// <summary>
        /// なうウインドウを上の位置に移動させる
        /// </summary>
        /// <param name="lpsTop"></param>
        /// <param name="lpsLeft"></param>
        private void nowWindowUpperMove(LiplisWindow targetWindow, double lpsTop, double lpsLeft, double lpsWidth, double lpsHeight)
        {
            //左に配置
            double left = lpsLeft - targetWindow.Width;

            //中心位置計算
            Int32 locationCenter = (Int32)(lpsLeft + lpsWidth / 2);

            //中央に配置
            left = locationCenter - (Int32)targetWindow.Width / 2; //レフト位置


            targetWindow.LocationX = left;
            targetWindow.LocationY = targetWindow.Top - 100;
        }

        /// <summary>
        /// みんなでおしゃべりのカウントセット
        /// </summary>
        public void everyoneCountSet(int val, int max)
        {
            try
            {
                if(everyoneTitleWindowList == null)
                {
                    return;
                }

                if(everyoneTitleWindowList.Count > 0)
                {
                    everyoneTitleWindowList[0].setProgress(val, max);
                }
            }
            catch
            {

            }
        }

        #endregion
    }
}
