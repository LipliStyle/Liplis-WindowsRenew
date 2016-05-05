//=======================================================================
//  ClassName : LpsVoiceRoidInfo
//  概要      : ボイスロイド情報
//
//  Liplis5.0
//
//  Copyright(c) 2010-2016 LipliStyle.Sachin
//=======================================================================

namespace Liplis.Voc.Option
{
    public class LpsVoiceRoidInfo
    {
        public string windowTitle;
        public string voiceRoidPath;

        public LpsVoiceRoidInfo(string windowTitle, string voiceRoidPath)
        {
            this.windowTitle = windowTitle;
            this.voiceRoidPath = voiceRoidPath;
        }
    }
}
