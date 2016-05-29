//=======================================================================
//  ClassName : RssEnableChecker
//  概要      : 対象のRSSURLが正しいか、または接続できるかチェックする
//
//              非同期をベーシックに作り直し
//
//  Liplis5.0
//
//  Copyright(c) 2010-2016 LipliStyle.Sachin
//=======================================================================


namespace Liplis.Xml.Rss
{
    public class RssEnableChecker
    {
        /// <summary>
        /// 有効RSSチェック
        /// </summary>
        #region 有効RSSチェック
        public static bool checkRssConnect(string url)
        {
            try
            {
                //RSS取得
                RssReader2 rr = new RssReader2(url);

                //RSSインスタンスが取得できなければ、RSSのURLとしては不正のためfalseを返す
                if(rr == null)
                {
                    return false;
                }

                //タイトルが取れなければ不正
                if (rr.title == null)
                {
                    return false;
                }

                //タイトルが取れれば有効
                return true;
            }
            catch
            {
                //エラー発生の場合は不正とみなす。
                return false;
            }
        }
        #endregion
    }
}
