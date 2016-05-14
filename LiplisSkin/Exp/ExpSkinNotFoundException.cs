//=======================================================================
//  ClassName : ExpSkinNotFoundException
//  概要      : Skinが見つからなかったエラー
//
//  Liplis5.0
//
//  Copyright(c) 2010-2016 LipliStyle.Sachin
//=======================================================================
using System;

namespace Liplis.Exp
{
    public class ExpSkinNotFoundException : Exception
    {
        private const string MESSAGE = "Liplisスキンが見つからなかったか、読み込みに失敗しました。";

        public ExpSkinNotFoundException()
        : base(MESSAGE)
        {
        }

        public ExpSkinNotFoundException(Exception inner)
        : base(MESSAGE, inner)
        {
        }
    }
}