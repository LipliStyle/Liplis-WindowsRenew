//=======================================================================
//  ClassName : RegisterRsCatUserInfo
//  概要      : レスポンス RSSカテゴリユーザー情報
//
//  Liplis5.0
//
//  Copyright(c) 2010-2016 LipliStyle.Sachin
//=======================================================================
using System.Collections.Generic;

namespace Clalis.v31.Res
{
    public class RegisterRsUserInfoCat
    {
        #region プロパティ
        ///=============================
        ///プロパティ
        public string cat { get; set; }
        public List<RegisterRsUserInfo> rsslist { get; set; }
        #endregion

        #region コンストラクター
        public RegisterRsUserInfoCat()
        {
            rsslist = new List<RegisterRsUserInfo>();
        }
        public RegisterRsUserInfoCat(string cat)
        {
            this.cat = cat;
            this.rsslist = new List<RegisterRsUserInfo>(); ;
        }
        public RegisterRsUserInfoCat(string cat, List<RegisterRsUserInfo> rsslist)
        {
            this.cat = cat;
            this.rsslist = rsslist;
        }
        #endregion
    }
}
