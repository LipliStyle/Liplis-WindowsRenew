//=======================================================================
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
    }
}
