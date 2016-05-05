//=======================================================================
//  ClassName : ResLpsGirlsTalk
//  概要      : レスポンス ガールズトーク
//
//  Liplis5.0
//
//  Copyright(c) 2010-2016 LipliStyle.Sachin
//=======================================================================
using System;
using System.Collections.Generic;
using Clalis.v50.Msg;

namespace Clalis.v50.Res
{
    public class ResLpsGirlsTalk
    {
        ///=============================
        /// プロパティ
        public string newsId { get; set; }
        public string title { get; set; }
        public string url { get; set; }
        public string jpgUrl { get; set; }
        public List<MsgTalkData> descriptionList { get; set; }

        #region ResLpsGirlsTalk
        public ResLpsGirlsTalk()
        {
            descriptionList = new List<MsgTalkData>();
        }
        #endregion

        #region ResLpsGirlsTalk(Int64 idx, string title, string url, string jpgUrl, List<MsgTalkData> descriptionList)
        public ResLpsGirlsTalk(Int64 idx, string title, string url, string jpgUrl, List<MsgTalkData> descriptionList)
        {
            this.newsId = idx.ToString();
            this.title = title;
            this.url = url;
            this.jpgUrl = jpgUrl;
            this.descriptionList = descriptionList;
        }
        #endregion
    }
}
