
//=======================================================================
//  ClassName : ExpWidgetInitException
//  概要      : ウィジェット初期化時エラー
//
//  Liplis5.0
//
//  Copyright(c) 2010-2016 LipliStyle.Sachin
//=======================================================================
using System;

namespace Liplis.Exp
{
    public class ExpWidgetInitException : Exception
    {
        private const string MESSAGE = "ウィジェットの初期化に失敗しました。Skinに問題がある可能性があります。";

        public ExpWidgetInitException()
        : base(MESSAGE)
        {
        }

        public ExpWidgetInitException(Exception inner)
        : base(MESSAGE + Environment.NewLine + "エラー原因:" + inner.Message, inner)
        {
        }
    }
}