﻿//=======================================================================
//  ClassName : ResLpsLoginRegisterInfoRssEachCat
//  概要      : レスポンス リプリス設定情報RSS
//
//  Liplis5.0
//
//  Copyright(c) 2010-2016 LipliStyle.Sachin
//=======================================================================
using System.Collections.Generic;

namespace Clalis.v31.Res
{
    public class ResLpsLoginRegisterInfoRssEachCat
    {
        ///=============================
        ///プロパティ
        public List<RegisterRsUserInfoCat> rsslist { get; set; }

        /// <summary>
        /// コンストラクター
        /// </summary>
        #region ResLpsLoginRegisterInfoRssEachCat
        public ResLpsLoginRegisterInfoRssEachCat()
        {
            this.rsslist = new List<RegisterRsUserInfoCat>();
        }
        public ResLpsLoginRegisterInfoRssEachCat(List<RegisterRsUserInfoCat> rsslist)
        {
            this.rsslist = rsslist;
        }
        #endregion

        /// <summary>
        /// RSSが登録されているかチェックする
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public bool containsRss(string url)
        {
            foreach (RegisterRsUserInfoCat item in rsslist)
            {
                foreach (var rss in item.rsslist)
                {
                    if(url == rss.url)
                    {
                        //見つかった
                        return true;
                    }
                }
            }

            //見つからなかった
            return false;   
        }

        /// <summary>
        /// カテゴリを取得する
        /// </summary>
        /// <param name="cat"></param>
        /// <returns></returns>
        public RegisterRsUserInfoCat getCatData(string cat)
        {
            foreach (RegisterRsUserInfoCat item in rsslist)
            {
                if(item.cat == cat)
                {
                    return item;
                }
            }

            //見つからなかった
            return null;
        }
    }
}
