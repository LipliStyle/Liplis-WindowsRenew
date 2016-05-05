//=======================================================================
//  ClassName : LiplisDefine
//  概要      : 各種設定定義
//
//  Liplis5.0
//
//  Copyright(c) 2010-2016 LipliStyle.Sachin
//=======================================================================

namespace Liplis.Com
{
    public class LpsDefine
    {
        ///=============================
        /// プログラム名
        public const string PRG_NAME_LIPLIS = "Liplis";

        ///=============================
        /// 設定ファイルパス定義
        #region 設定ファイルパス定義
        public const string SETTING_SKIN_FILE_NAME = "skin.xml";
        public const string SETTING_VERSION_FILE_NAME = "version.xml";
        public const string SETTING_BODY_XML_FILE = "body.xml";
        public const string SETTING_CHAT_XML_FILE = "chat.xml";
        public const string SETTING_TONE_XML_FILE = "tone.xml";
        public const string LIPLIS_NEW_EXE_FILE = "Liplis.lps";
        #endregion

        ///=============================
        /// URL定義
        #region URL定義


        #endregion

        ///=============================
        /// skinファイル定義
        #region skin設定ファイル定義
        public const string SKIN_CHAR_NAME = "charName";
        public const string SKIN_WIDTH = "width";
        public const string SKIN_HEIGHT = "height";
        public const string SKIN_FONT = "textFont";
        public const string SKIN_TEXT_COLOR = "textColor";
        public const string SKIN_LINK_COLOR = "linkColor";
        public const string SKIN_TITLE_COLOR = "titleColor";
        public const string SKIN_THEMA_COLOR = "themaColor";
        public const string SKIN_THEMA_COLOR2 = "themaColor2";
        public const string SKIN_CHAR_INTRO = "charIntroduction";
        public const string SKIN_VERSION = "version";
        public const string SKIN_TONE_URL = "tone";

        ///=============================
        /// skin設定デフォルト定義
        public const string SKIN_DEF_CHAR_NAME = "Lili";
        public const int SKIN_DEF_WIDTH = 300;
        public const int SKIN_DEF_HEIGHT = 334;
        public const string SKIN_DEF_FONT = "ＭＳ ゴシック";
        public const string SKIN_DEF_TEXT_COLOR = "0,0,0";
        public const string SKIN_DEF_LINK_COLOR = "0,0,0";
        public const string SKIN_DEF_TITLE_COLOR = "0,0,0";
        public const string SKIN_DEF_THEMA_COLOR = "255,228,96";   //ver2.2.2
        public const string SKIN_DEF_THEMA_COLOR2 = "255.140,00";   //ver2.2.2
        public const string SKIN_DEF_CHAR_INTRO = "紹介文の読み込みに失敗しました。";
        public const string SKIN_DEF_VERSION = "1.0.0";
        public const string SKIN_DEF_TONE_URL = "http://liplis.mine.nu/xml/Tone/LiplisLili.xml";
        #endregion



        ///=============================
        /// エンコード
        public const string ENCODING_SJIS         = "Shift-JIS";    //日本語
        public const string ENCODING_UTF8         = "UTF-8";        //日本語
        public const string ENCODING_GB2312       = "936";          //中国語 簡易
        public const string ENCODING_BIG5         = "950";          //中国語 繁
        public const string ENCODING_IBM860       = "860";          //ポルトガル
        public const int ENCODING_ISO_2022_KR     = 50225;          //朝鮮
        public const string ENCODING_WINDOWS_1256 = "1256";         //アラビア
        public const string ENCODING_IBM863       = "863";          //フランス語
        public const int ENCODING_UNICODE         = 1200;

        ///=============================
        /// URL定義
        #region URL定義
        public const string URL_NICO_VIDEO = "http://www.nicovideo.jp/watch/";
        public const string URL_NICO_DOMAIN = "http://www.nicovideo.jp/";
        public const string URL_NICO_LOGIN_PAGE_URL = "https://secure.nicovideo.jp/secure/login?site=niconico";
        public const string URL_NICO_INFO = "http://ext.nicovideo.jp/api/getthumbinfo/";
        #endregion

    }
}
