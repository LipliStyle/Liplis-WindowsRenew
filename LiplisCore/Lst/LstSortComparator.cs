//=======================================================================
//  ClassName : LstSortComparator
//  概要      : ソート時にしていするコンパレーターを定義
//              →ラムダ式による記述を推奨し、このクラスは使わないようにする！
//
//  Liplis5.0
//
//  Copyright(c) 2010-2016 LipliStyle.Sachin
//=======================================================================
using System.Collections.Generic;

namespace Liplis.Lst
{
    public class LstSortComparator
    {
        //大きい順ソート
        public static int intToIntDesc(
          KeyValuePair<int, int> kvp1,
          KeyValuePair<int, int> kvp2)
        {
            return kvp2.Value - kvp1.Value;
        }
        //小さい順ソート
        public static int intToIntAsc(
          KeyValuePair<int, int> kvp1,
          KeyValuePair<int, int> kvp2)
        {
            return kvp1.Value - kvp2.Value;
        }

        public static int strToIntDesc(
          KeyValuePair<string, int> kvp1,
          KeyValuePair<string, int> kvp2)
        {
            return kvp2.Value - kvp1.Value;
        }

        public static int strToIntAsc(
          KeyValuePair<string, int> kvp1,
          KeyValuePair<string, int> kvp2)
        {
            return kvp1.Value - kvp2.Value;
        }
    }
}

