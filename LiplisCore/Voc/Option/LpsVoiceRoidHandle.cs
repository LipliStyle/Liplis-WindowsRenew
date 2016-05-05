//=======================================================================
//  ClassName : LpsVoiceRoidHandle
//  概要      : ボイスロイドハンドル
//
//  Liplis5.0
//
//  Copyright(c) 2010-2016 LipliStyle.Sachin
//=======================================================================
using System;

namespace Liplis.Voc.Option
{
    public class LpsVoiceRoidHandle
    {
        public IntPtr hWindowHandle;
        public IntPtr hPlayHandle;
        public IntPtr hStopHandle;
        public IntPtr hEditHandle;
        public LpsVoiceRoidInfo Info;
    }
}
