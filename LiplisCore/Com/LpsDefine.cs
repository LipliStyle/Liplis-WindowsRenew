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
        /// デフォルトスキン名
        public const string DEFAULT_SKIN = "LiliRenew";

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

        ///=============================
        /// チャット制御コード
        #region 制御コード
        public const string CHAT_DEF_GREET = "greet";
        public const string CHAT_DEF_GOODBYE = "goodBye";
        public const string CHAT_DEF_CHANGE = "change";
        public const string CHAT_DEF_BATTERY_INFOT = "batteryInfo";

        public const string setting_Working = "setting_Working";
        public const string setting_NotFound = "setting_NotFound";
        public const string setting_AlreadyRegist = "setting_AlreadyRegist";
        public const string setting_AlreadyDelete = "setting_AlreadyDelete";
        public const string setting_RssNotValid = "setting_RssNotValid";
        public const string setting_RssValid = "setting_RssValid";
        public const string setting_Success = "setting_Success";
        public const string setting_Rss_Reg_Success = "setting_Rss_Reg_Success";
        public const string setting_Rss_Upd_Success = "setting_Rss_Upd_Success";
        public const string setting_Faild = "setting_Faild";
        public const string setting_Delete = "setting_Delete";
        public const string setting_DeleteConfirm = "setting_DeleteConfirm";
        public const string setting_DeleteSuccess = "setting_DeleteSuccess";
        public const string setting_DeleteFaild = "setting_DeleteFaild";
        public const string setting_Cancel = "setting_Cancel";
        public const string setting_UrlEmpty = "setting_UrlEmpty";
        public const string setting_NotFoundRssDelete = "setting_NotFoundRssDelete";
        public const string setting_NotFoundNotVaildRss = "setting_NotFoundNotVaildRss";
        public const string setting_FoundNotVaildRss = "setting_FoundNotVaildRss";
        public const string setting_NFRD_Success = "setting_NFRD_Success";
        public const string setting_NFRD_SAF = "setting_NFRD_SAF";
        public const string setting_RSS_ADD = "setting_RssAdd";
        public const string setting_CacheChange = "setting_CacheChange";
        public const string setting_CacheChanged = "setting_CacheChanged";
        public const string setting_updateNow = "setting_updateNow";


        public const string errNotFoundBrowzer = "errNotFoundBrowzer";
        public const string errNothingIsFound = "errNothingIsFound";
        public const string errNotFoundTopix = "errNotFoundTopix";
        public const string errNoTopixNoConnection = "errNoTopixNoConnection";

        public const string newDataSearchStart = "newDataSearchStart";
        public const string newDataSearchEnd = "newDataSearchEnd";
        public const string updateNow = "updateNow";
        public const string err_BrowzerErr = "err_BrowzerErr";
        public const string err_Commnand = "err_Commnand";

        public const string rdoFrqReticent = "rdoFrqReticent";
        public const string rdoFrqRefined = "rdoFrqRefined";
        public const string rdoFrqKeeps = "rdoFrqKeeps";
        public const string rdoFrqQuiet = "rdoFrqQuiet";
        public const string rdoFrqNormal = "rdoFrqNormal";
        public const string rdoFrqTalkative = "rdoFrqTalkative";
        public const string rdoFrqNoisy = "rdoFrqNoisy";
        public const string rdoFrqVerryNoisy = "rdoFrqVerryNoisy";
        public const string rdoFrqMachen = "rdoFrqMachen";
        public const string rdoFrqChangeable = "rdoFrqChangeable";
        public const string notConect = "notConect";
        public const string setting_DbCrean = "setting_DbCrean";
        public const string setting_DbCrean_End = "setting_DbCrean_End";

        public const string endChat = "endChat";
        #endregion

    }
}
