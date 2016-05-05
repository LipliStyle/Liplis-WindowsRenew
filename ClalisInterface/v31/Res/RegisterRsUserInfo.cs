//=======================================================================
//  ClassName : RegisterRsUserInfo
//  概要      : レスポンス Rss登録ユーザー情報
//
//  Liplis5.0
//
//  Copyright(c) 2010-2016 LipliStyle.Sachin
//=======================================================================

namespace Clalis.v31.Res
{
    public class RegisterRsUserInfo
    {
        #region プロパティ
        public string url { get; set; }
        public string title { get; set; }
        public string cat { get; set; }
        #endregion

        #region コンストラクター
        public RegisterRsUserInfo()
        {
        }
        public RegisterRsUserInfo(string url, string title, string cat)
        {
            this.url = url;
            this.title = title;
            this.cat = cat;
        }
        #endregion
    }
}
