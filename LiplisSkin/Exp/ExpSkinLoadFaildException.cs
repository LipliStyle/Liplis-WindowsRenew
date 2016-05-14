//=======================================================================
//  ClassName :     public class ExpSkinLoadFaildException : Exception

//  概要      : Skinの読み込みに失敗したエラー
//
//  Liplis5.0
//
//  Copyright(c) 2010-2016 LipliStyle.Sachin
//=======================================================================
using System;

namespace Liplis.Exp
{
    public class ExpSkinLoadFaildException : Exception
    {
        private const string MESSAGE = "Liplisスキンの読み込みに失敗しました。";

        public ExpSkinLoadFaildException()
        : base(MESSAGE)
        {
        }

        public ExpSkinLoadFaildException(Exception inner)
        : base(MESSAGE, inner)
        {
        }
    }
}