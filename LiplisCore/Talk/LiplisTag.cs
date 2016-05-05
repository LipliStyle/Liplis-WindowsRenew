//=======================================================================
//  ClassName : LiplisTag
//  概要      : リプリス用のタグを付与する
//
//  Liplis5.0
//
//  Copyright(c) 2010-2016 LipliStyle.Sachin
//=======================================================================
using Liplis.Com;
using Liplis.Msg;

namespace Liplis.Talk
{
    public static class LiplisTag
    {
        /// <summary>
        /// setTag
        /// タグを付与する
        /// </summary>
        /// <param name="target"></param>
        /// <returns></returns>
        #region setTag
        public static void setTag(MsgTalkMessage msg)
        {
            //ソースで既に振られてくるため廃止
            //HTMLリンクにタグを付与する
            msg.result = LpsRegularEx.fctReplaceHtmlLink(msg.result);

            //動画IDにタグを付与する
            msg.result = LpsRegularEx.fctReplaceNicoVideoId(msg.result);

            //myリストにタグを付与する
            msg.result = LpsRegularEx.fctReplaceNicoMyListId(msg.result);

            //BR変換
            msg.result = LpsRegularEx.fctReplaceNewLine(msg.result);

            //連続びっくりマークを統合
            LpsRegularEx.fctBikkuriTogo(msg.result);
        }
        #endregion
    }

}
