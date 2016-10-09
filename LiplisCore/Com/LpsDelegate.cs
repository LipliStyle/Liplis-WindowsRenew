//=======================================================================
//  ClassName : LpsDelegate
//  概要      : 共通デリゲート
//
//  Liplis5.0
//
//  Copyright(c) 2010-2016 LipliStyle.Sachin
//=======================================================================

using Liplis.Msg;
using System.Drawing;

namespace Liplis.Com
{
    public class LpsDelegate
    {
        ///============================
        /// 汎用デリゲート
        public delegate void dlgVoidToVoid();

        ///============================
        /// 引数bool
        public delegate void dlgBToVoid(bool flg);

        ///============================
        /// 引数string
        public delegate void dlgS1ToVoid(string str1);
        public delegate void dlgS2ToVoid(string str1, string str2);
        public delegate void dlgS3ToVoid(string str1, string str2, string str3);
        public delegate void dlgS4ToVoid(string str1, string str2, string str3, string str4);
        public delegate void dlgS5ToVoid(string str1, string str2, string str3, string str4, string str5);

        ///============================
        /// 引数int
        public delegate void dlgI1ToVoid(int val1);
        public delegate void dlgI2ToVoid(int val1, int val2);
        public delegate void dlgI3ToVoid(int val1, int val2, int val3);
        public delegate void dlgI4ToVoid(int val1, int val2, int val3, int val4);
        public delegate void dlgI5ToVoid(int val1, int val2, int val3, int val4, int val5);
        public delegate void dlgI6ToVoid(int val1, int val2, int val3, int val4, int val5, int val6);
        public delegate void dlgI7ToVoid(int val1, int val2, int val3, int val4, int val5, int val6, int val7);


        ///============================
        /// 引数MsgShortNews
        public delegate void dlgMsnToVoid(MsgTalkMessage msg, Bitmap bmp);

    }
}
