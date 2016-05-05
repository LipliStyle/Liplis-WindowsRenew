//=======================================================================
//  ClassName : MsgRssList
//  概要      : RSSリスト
//
//  Liplis5.0
//
//  Copyright(c) 2010-2016 LipliStyle.Sachin
//=======================================================================

using System;
using System.Collections.Generic;

namespace Liplis.Msg.Rss
{
    [Serializable]
    public class MsgRssList
    {
        public int maxIdx { get; set; }
        public List<MsgRssCatList> rssCatList { get; set; }

        ///====================================================================
        ///
        ///                          初期化処理
        ///                         
        ///====================================================================

        /// <summary>
        /// コンストラクター
        /// </summary>
        #region MsgRssList
        public MsgRssList()
        {
            maxIdx = 0;
            rssCatList = new List<MsgRssCatList>();
            createBasket();
        }
        #endregion

        /// <summary>
        /// searchRss
        /// rssを検索するする
        /// </summary>
        #region searchRss
        public bool searchRss(string url)
        {
            for (int idx = 0; idx < rssCatList.Count; idx++)
            {
                for (int jdx = 0; jdx < rssCatList[idx].rssList.Count; jdx++)
                {
                    if (rssCatList[idx].rssList[jdx].url == url)
                    {
                        return true;
                    }
                }
            }
            return false;
        }
        #endregion


        /// <summary>
        /// getRss
        /// rssを取得する
        /// </summary>
        #region getRss
        public MsgRss getRss(string url)
        {
            for (int idx = 0; idx < rssCatList.Count; idx++)
            {
                for (int jdx = 0; jdx < rssCatList[idx].rssList.Count; jdx++)
                {
                    if (rssCatList[idx].rssList[jdx].url == url)
                    {
                        return rssCatList[idx].rssList[jdx];
                    }
                }
            }
            return null;
        }
        #endregion

        /// <summary>
        /// searchCat
        /// カテゴリを検索する
        /// </summary>
        #region searchCat
        public bool searchCat(string cat)
        {
            //カテゴリーの存在チェック
            for (int idx = 0; idx < rssCatList.Count; idx++)
            {
                if (rssCatList[idx].cat == cat)
                {
                    return true;
                }
            }

            return false;
        }
        #endregion


        /// <summary>
        /// バスケットを作成する
        /// </summary>
        #region createBasket
        public void createBasket()
        {
            if (!searchCat("-"))
            {
                rssCatList.Add(new MsgRssCatList("-"));

            }
        }
        #endregion

        ///====================================================================
        ///
        ///                          RSS情報操作処理
        ///                         
        ///====================================================================

        /// <summary>
        /// addRss
        /// RSSを追加する
        /// </summary>
        #region addRss
        public void addRss(string url, string cat, string title)
        {
            bool mache = false;
            //カテゴリーを指定してRSS登録
            for (int idx = 0; idx < rssCatList.Count; idx++)
            {
                if (rssCatList[idx].cat == cat)
                {
                    mache = true;
                    rssCatList[idx].rssList.Add(new MsgRss(title, url, cat));
                    return;
                }
            }

            //一致しなければ、カテゴリー作成
            if (!mache)
            {
                MsgRssCatList newList = new MsgRssCatList(cat);
                newList.rssList.Add(new MsgRss(title, url, cat));
                rssCatList.Add(newList);
            }
        }
        #endregion

        /// <summary>
        /// fixRss
        /// RSSを修正する
        /// </summary>
        #region fixRss
        public void fixRss(string url, string cat, string title)
        {
            //カテゴリーを指定してRSS登録
            for (int idx = 0; idx < rssCatList.Count; idx++)
            {
                for (int jdx = 0; jdx < rssCatList[idx].rssList.Count; jdx++)
                {
                    if (rssCatList[idx].rssList[jdx].url == url)
                    {
                        //タイトルだけの変更なら、タイトルを変更する
                        rssCatList[idx].rssList[jdx].title = title;

                        //もしカテゴリーが変更されていたら、削除して追加する
                        if (rssCatList[idx].rssList[jdx].cat != cat)
                        {
                            rssCatList[idx].rssList.RemoveAt(jdx);
                            addRss(url, cat, title);
                        }
                        return;
                    }
                }
            }
        }
        #endregion


        /// <summary>
        /// delRss
        /// RSSを削除する
        /// </summary>
        #region delRss
        public void delRss(string url, string cat)
        {
            for (int idx = 0; idx < rssCatList.Count; idx++)
            {
                for (int jdx = 0; jdx < rssCatList[idx].rssList.Count; jdx++)
                {
                    if (rssCatList[idx].rssList[jdx].url == url)
                    {
                        rssCatList[idx].rssList.RemoveAt(jdx);
                        return;
                    }
                }
            }
        }
        #endregion


        ///====================================================================
        ///
        ///                         カテゴリ情報操作処理
        ///                         
        ///====================================================================

        /// <summary>
        /// addCat
        /// カテゴリを追加する
        /// </summary>
        #region addCat
        public bool addCat(string cat)
        {
            //カテゴリーの存在チェック
            for (int idx = 0; idx < rssCatList.Count; idx++)
            {
                if (rssCatList[idx].cat == cat)
                {
                    return false;
                }
            }

            //一致しなければ、カテゴリー作成
            MsgRssCatList newList = new MsgRssCatList(cat);
            rssCatList.Add(newList);

            return true;
        }
        #endregion

        /// <summary>
        /// delCat
        /// カテゴリを削除する
        /// </summary>
        #region delCat
        public void delCat(string cat)
        {
            //カテゴリーの存在チェック
            for (int idx = 0; idx < rssCatList.Count; idx++)
            {
                if (rssCatList[idx].cat == cat)
                {
                    rssCatList.RemoveAt(idx);
                }
            }
        }
        #endregion

        /// <summary>
        /// fixCat
        /// カテゴリを削除する
        /// </summary>
        #region fixCat
        public void fixCat(string befor, string after)
        {
            //カテゴリーの存在チェック
            for (int idx = 0; idx < rssCatList.Count; idx++)
            {
                if (rssCatList[idx].cat == befor)
                {
                    rssCatList[idx].cat = after;
                }
            }
        }
        #endregion
    }
}
