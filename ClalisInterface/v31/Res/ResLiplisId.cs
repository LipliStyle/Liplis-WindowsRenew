//=======================================================================
//  ClassName : ResLiplisId
//  概要      : リプリスID
//
//  Liplis5.0
//
//  Copyright(c) 2010-2016 LipliStyle.Sachin
//=======================================================================

namespace Clalis.v31.Res
{
    public class ResLiplisId
    {
        ///=============================
        ///プロパティ
        public string userId { get; set; }

        /// <summary>
        /// コンストラクター
        /// </summary>
        #region ResLiplisId
        public ResLiplisId()
        {
            this.userId = "";
        }
        public ResLiplisId(string userId)
        {
            this.userId = userId;
        }
        #endregion
    }
}
