//=======================================================================
//  ClassName : SkinLoadFaildException
//  概要      : Skinの読み込みに失敗したエラー
//
//  Liplis5.0
//
//  Copyright(c) 2010-2016 LipliStyle.Sachin
//=======================================================================
using System;

namespace Liplis.Exp
{
    public class SkinLoadFaildException : Exception
    {
        private const string MESSAGE = "Liplisスキンの読み込みに失敗しました。";

        public SkinLoadFaildException()
        : base(MESSAGE)
        {
        }

        public SkinLoadFaildException(Exception inner)
        : base(MESSAGE, inner)
        {
        }
    }
}