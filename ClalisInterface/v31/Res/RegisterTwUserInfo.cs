//=======================================================================
//  ClassName : RegisterTwUserInfo
//  概要      : レスポンス ツイッターユーザー情報
//
//  Liplis5.0
//
//  Copyright(c) 2010-2016 LipliStyle.Sachin
//=======================================================================

namespace Clalis.v31.Res
{
    public class RegisterTwUserInfo
    {
        #region プロパティ
        public string name { get; set; }
        public string url { get; set; }
        public string description { get; set; }
        public string iconUrl { get; set; }
        #endregion

        #region コンストラクター
        public RegisterTwUserInfo()
        {
        }
        public RegisterTwUserInfo(string name, string url, string description, string iconUrl)
        {
            this.name = name;
            this.url = url;
            this.description = description;
            this.iconUrl = iconUrl;
        }
        #endregion
    }
}
