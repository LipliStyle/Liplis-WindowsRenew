//=======================================================================
//  ClassName : RssReader2
//  概要      : RssReader 第二世代
//
//  Liplis5.0
//
//  Copyright(c) 2010-2016 LipliStyle.Sachin
//=======================================================================
using Liplis.Tasks;
using System;
using System.Collections.Generic;
using System.Net;
using System.ServiceModel.Syndication;
using System.Text;
using System.Xml;

namespace Liplis.Xml.Rss
{
    public class RssReader2
    {
        ///============================
        /// 使用エンコード
        Encoding enc;

        ///============================
        /// RSS基本情報
        #region RssBasicData
        public string title { get; set; }
        public string rssUri { get; set; }
        public string rssDescription { get; set; }
        public string rssUpdate { get; set; }
        public bool response { get; set; }
        #endregion

        ///============================
        ///リンクラベルコレクション
        #region dataList
        public List<string> urlList { get; set; }
        public List<string> urlTitleList { get; set; }
        public List<string> discriptionList { get; set; }
        public List<string> dateList { get; set; }
        #endregion

        /// <summary>
        /// コンストラクター
        /// </summary>
        #region コンストラクター
        public RssReader2(string rssUri)
        {
            //リストの初期化
            initList();

            //RSS読込
            loadRss(rssUri);
        }
        #endregion

        /// <summary>
        /// リストの初期化
        /// </summary>
        #region initList
        private void initList()
        {
            urlList = new List<string>();
            urlTitleList = new List<string>();
            discriptionList = new List<string>();
            dateList = new List<string>();
        }
        #endregion

        /// <summary>
        /// RSSを読み込む
        /// </summary>
        /// <param name="rssUri"></param>
        #region loadRss
        private void loadRss(string rssUri)
        {
            //空は無効
            if (rssUri == "")
            {
                return;
            }

            //URL取得
            this.rssUri = rssUri;

            try
            {
                using (XmlReader rdr = XmlReader.Create(rssUri))
                {
                    //RSSフィードの取得
                    SyndicationFeed feed = getSyndicationalFeed(rdr);

                    //読み込みチェック nullなら失敗とみなす。
                    if (feed == null) { throw new XmlException(); }

                    //RSS情報の取得
                    this.title = feed.Title.Text;
                    this.rssDescription = feed.Description.Text;

                    //詳細記事の取得
                    foreach (SyndicationItem item in feed.Items)
                    {
                        urlTitleList.Add(getTextSyndicationContent(item.Title));
                        discriptionList.Add(getTextSyndicationContent(item.Summary));
                        urlList.Add((item.Links.Count > 0 ? item.Links[0].Uri.ToString() : ""));
                        dateList.Add(item.PublishDate.ToString("yyyy/MM/dd HH:mm:ss"));
                    }

                    response = true;
                }
            }
            catch (WebException)
            {
                response = false;
            }
            catch (XmlException)
            {
                //XMLエクセプションなら、バージョン1でも読込を試みる
                //本クラスでは、RDFRSSが読めないので、補完する
                response = getRssReader();
            }
            catch (Exception)
            {
                //失敗しても旧バージョンで読み込みを試してみる
                response = getRssReader();
            }
        }

        /// <summary>
        /// RSSフィードを取得する
        /// </summary>
        /// <param name="rdr"></param>
        /// <returns></returns>
        private SyndicationFeed getSyndicationalFeed(XmlReader rdr)
        {
            SyndicationFeed feed = null;

            //フィード読み込み(タイムアウト1分)
            LpsSTATask.Run(() => { feed = SyndicationFeed.Load(rdr); }).Wait(60000);

            if (feed == null) { return null; }              //タイトルチェック
            if (feed.Title == null) { return null; }        //タイトルチェック
            if (feed.Description == null) { return null; }  //ディスクリプションチェック
            if (feed.Items == null) { return null; }        //アイテム

            return feed;
        }

        /// <summary>
        /// TextSyndicationContent空文字列を取り出す
        /// </summary>
        /// <param name="tsc"></param>
        /// <returns></returns>
        private string getTextSyndicationContent(TextSyndicationContent tsc)
        {
            try
            {
                if (tsc == null)
                {
                    return "";
                }
                else
                {
                    if (tsc.Text == null)
                    {
                        return "";
                    }
                }

                return tsc.Text;
            }
            catch
            {
                return "";
            }
        }

        /// <summary>
        /// 読み取りエラーの場合は1から読み込む
        /// </summary>
        /// <returns></returns>
        private bool getRssReader()
        {
            RssReader rr = null;

            //RSS読み込み(タイムアウト1分)
            LpsSTATask.Run(() => { rr = new RssReader(this.rssUri); }).Wait(60000);

            if (rr == null) { return false; }
            if (rr.response > 0) { return false; }

            this.title = rr.title;
            this.rssUpdate = rr.rssUpdate;

            this.urlList = rr.urlList;
            this.urlTitleList = rr.urlTitleList;
            this.discriptionList = rr.discriptionList;
            this.dateList = rr.dateList;

            return true;
        }
        #endregion

        /// <summary>
        /// 最新記事の日付を取得する
        /// </summary>
        #region getLatestDate
        public DateTime getLatestDate()
        {
            try
            {
                return DateTime.Parse(dateList[0]);
            }
            catch
            {
                return DateTime.Parse("1980/01/01 00:00:00");
            }
        }
        #endregion

        /// <summary>
        /// 平均更新頻度
        /// </summary>
        #region getUpdateRate
        public double getUpdateRate()
        {
            double res = 0.0;
            TimeSpan ts = new TimeSpan();
            int idx = 0;
            double summary = 0.0;
            List<string> cDateList = new List<string>();
            cDateList.Add(DateTime.Now.ToString());
            cDateList.AddRange(dateList);

            if (cDateList.Count > 1)
            {
                try
                {
                    foreach (string str in dateList)
                    {
                        DateTime dt = DateTime.Parse(cDateList[idx]);
                        DateTime dt2 = DateTime.Parse(cDateList[idx + 1]);

                        ts = dt.Subtract(dt2);
                        summary = summary + ts.TotalHours;
                        idx++;
                    }
                    res = summary / dateList.Count;

                    return res;
                }
                catch
                {
                    return 0;
                }
            }
            else if (cDateList.Count == 2)
            {
                ts = DateTime.Parse(cDateList[0]).Subtract(DateTime.Parse(cDateList[1]));
                return ts.TotalHours;
            }
            else
            {
                return 0;
            }
        }
        #endregion 

    }
}

