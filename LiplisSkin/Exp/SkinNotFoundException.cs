//=======================================================================
//  ClassName : SkinNotFoundException
//  概要      : Skinが見つからなかったエラー
//
//  Liplis5.0
//
//  Copyright(c) 2010-2016 LipliStyle.Sachin
//=======================================================================
using System;

namespace Liplis.Exp
{
    public class SkinNotFoundException : Exception
    {
        private const string MESSAGE = "Liplisスキンが見つからなかったか、読み込みに失敗しました。";

        public SkinNotFoundException()
        : base(MESSAGE)
        {
        }

        public SkinNotFoundException(Exception inner)
        : base(MESSAGE, inner)
        {
        }
    }
}