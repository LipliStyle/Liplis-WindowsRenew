//=======================================================================
//  ClassName : LpsMessage
//  概要      : リプリスメッセージを直接呼び出すクラス
//              実体はフォーム側だが、直接呼び出すと記述が冗長になる。
//              こちらのクラスを使って、代替的に呼び出す。
//
//  Liplis5.0
//
//  Copyright(c) 2010-2016 LipliStyle.Sachin
//=======================================================================

using Liplis.Com;

namespace Liplis.Gui
{
    public class LpsMessage
    {
        /// <summary>
        /// メッセージを表示する
        /// </summary>
        /// <param name="message"></param>
        public static void showMessage(string message)
        {
            using (_LpsMessage f = new _LpsMessage())
            {
                f.popMessage(LpsDefine.PRG_NAME_LIPLIS, message);
            }
        }

        /// <summary>
        /// エラーメッセージを表示する
        /// </summary>
        /// <param name="message"></param>
        public static void showError(string message)
        {
            using (_LpsError f = new _LpsError())
            {
                f.popMessage(LpsDefine.PRG_NAME_LIPLIS, message);
            }
        }
    }
}
