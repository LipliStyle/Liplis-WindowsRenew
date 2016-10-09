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
        #region エンコード
        public const string ENCODING_SJIS = "Shift-JIS";    //日本語
        public const string ENCODING_UTF8 = "UTF-8";        //日本語
        public const string ENCODING_GB2312 = "936";          //中国語 簡易
        public const string ENCODING_BIG5 = "950";          //中国語 繁
        public const string ENCODING_IBM860 = "860";          //ポルトガル
        public const int ENCODING_ISO_2022_KR = 50225;          //朝鮮
        public const string ENCODING_WINDOWS_1256 = "1256";         //アラビア
        public const string ENCODING_IBM863 = "863";          //フランス語
        public const int ENCODING_UNICODE = 1200;

        #endregion

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


        ///=============================
        /// ツイッター認証コード
        ///=============================
        #region ツイッター認証コード
        public const string TWITTER_OAUTH_CONSUMERKEY = "W1tQBXDr3pQu1atfIwp6A";
        public const string TWITTER_OAUTH_CONSUMERSECRET = "eTFat5surbln3MH7f0uIlwmpOcQdjlkyg7vUk90eG8";
        #endregion


        ///=============================
        /// リンク
        #region ""
        public const string LIPLIS_HELP = @"http://liplis.mine.nu/lipliswiki/webroot/?LiplisWindows%20Manual";
        public const string LIPLIS_LIPLISTYLE = @"http://liplis.mine.nu/";
        #endregion

        ///=============================
        /// ボディ画像定義
        #region ボディ画像xpath定義
        public const string BODY_HEIGHT = "/define/height";
        public const string BODY_WIDHT = "/define/width";
        public const string BODY_LOCATION_X = "/define/locationX";
        public const string BODY_LOCATION_Y = "/define/locationY";

        public const string BODY_NORMAL_11 = "/define/normal/normal11";
        public const string BODY_NORMAL_12 = "/define/normal/normal12";
        public const string BODY_NORMAL_21 = "/define/normal/normal21";
        public const string BODY_NORMAL_22 = "/define/normal/normal22";
        public const string BODY_NORMAL_31 = "/define/normal/normal31";
        public const string BODY_NORMAL_32 = "/define/normal/normal32";
        public const string BODY_NORMAL_TOUCH = "/define/normal/touch";

        public const string BODY_JOY_P_11 = "/define/joy_p/joy_p11";
        public const string BODY_JOY_P_12 = "/define/joy_p/joy_p12";
        public const string BODY_JOY_P_21 = "/define/joy_p/joy_p21";
        public const string BODY_JOY_P_22 = "/define/joy_p/joy_p22";
        public const string BODY_JOY_P_31 = "/define/joy_p/joy_p31";
        public const string BODY_JOY_P_32 = "/define/joy_p/joy_p32";
        public const string BODY_JOY_P_TOUCH = "/define/joy_p/touch";

        public const string BODY_JOY_M_11 = "/define/joy_m/joy_m11";
        public const string BODY_JOY_M_12 = "/define/joy_m/joy_m12";
        public const string BODY_JOY_M_21 = "/define/joy_m/joy_m21";
        public const string BODY_JOY_M_22 = "/define/joy_m/joy_m22";
        public const string BODY_JOY_M_31 = "/define/joy_m/joy_m31";
        public const string BODY_JOY_M_32 = "/define/joy_m/joy_m32";
        public const string BODY_JOY_M_TOUCH = "/define/joy_m/touch";

        public const string BODY_ADMIRATION_P_11 = "/define/admiration_p/admiration_p11";
        public const string BODY_ADMIRATION_P_12 = "/define/admiration_p/admiration_p12";
        public const string BODY_ADMIRATION_P_21 = "/define/admiration_p/admiration_p21";
        public const string BODY_ADMIRATION_P_22 = "/define/admiration_p/admiration_p22";
        public const string BODY_ADMIRATION_P_31 = "/define/admiration_p/admiration_p31";
        public const string BODY_ADMIRATION_P_32 = "/define/admiration_p/admiration_p32";
        public const string BODY_ADMIRATION_P_TOUCH = "/define/admiration_p/touch";

        public const string BODY_ADMIRATION_M_11 = "/define/admiration_m/admiration_m11";
        public const string BODY_ADMIRATION_M_12 = "/define/admiration_m/admiration_m12";
        public const string BODY_ADMIRATION_M_21 = "/define/admiration_m/admiration_m21";
        public const string BODY_ADMIRATION_M_22 = "/define/admiration_m/admiration_m22";
        public const string BODY_ADMIRATION_M_31 = "/define/admiration_m/admiration_m31";
        public const string BODY_ADMIRATION_M_32 = "/define/admiration_m/admiration_m32";
        public const string BODY_ADMIRATION_M_TOUCH = "/define/admiration_m/touch";

        public const string BODY_PEACE_P_11 = "/define/peace_p/peace_p11";
        public const string BODY_PEACE_P_12 = "/define/peace_p/peace_p12";
        public const string BODY_PEACE_P_21 = "/define/peace_p/peace_p21";
        public const string BODY_PEACE_P_22 = "/define/peace_p/peace_p22";
        public const string BODY_PEACE_P_31 = "/define/peace_p/peace_p31";
        public const string BODY_PEACE_P_32 = "/define/peace_p/peace_p32";
        public const string BODY_PEACE_P_TOUCH = "/define/peace_p/touch";

        public const string BODY_PEACE_M_11 = "/define/peace_m/peace_m11";
        public const string BODY_PEACE_M_12 = "/define/peace_m/peace_m12";
        public const string BODY_PEACE_M_21 = "/define/peace_m/peace_m21";
        public const string BODY_PEACE_M_22 = "/define/peace_m/peace_m22";
        public const string BODY_PEACE_M_31 = "/define/peace_m/peace_m31";
        public const string BODY_PEACE_M_32 = "/define/peace_m/peace_m32";
        public const string BODY_PEACE_M_TOUCH = "/define/peace_m/touch";

        public const string BODY_ECSTASY_P_11 = "/define/ecstasy_p/ecstasy_p11";
        public const string BODY_ECSTASY_P_12 = "/define/ecstasy_p/ecstasy_p12";
        public const string BODY_ECSTASY_P_21 = "/define/ecstasy_p/ecstasy_p21";
        public const string BODY_ECSTASY_P_22 = "/define/ecstasy_p/ecstasy_p22";
        public const string BODY_ECSTASY_P_31 = "/define/ecstasy_p/ecstasy_p31";
        public const string BODY_ECSTASY_P_32 = "/define/ecstasy_p/ecstasy_p32";
        public const string BODY_ECSTASY_P_TOUCH = "/define/ecstasy_p/touch";

        public const string BODY_ECSTASY_M_11 = "/define/ecstasy_m/ecstasy_m11";
        public const string BODY_ECSTASY_M_12 = "/define/ecstasy_m/ecstasy_m12";
        public const string BODY_ECSTASY_M_21 = "/define/ecstasy_m/ecstasy_m21";
        public const string BODY_ECSTASY_M_22 = "/define/ecstasy_m/ecstasy_m22";
        public const string BODY_ECSTASY_M_31 = "/define/ecstasy_m/ecstasy_m31";
        public const string BODY_ECSTASY_M_32 = "/define/ecstasy_m/ecstasy_m32";
        public const string BODY_ECSTASY_M_TOUCH = "/define/ecstasy_m/touch";

        public const string BODY_AMAZEMENT_P_11 = "/define/amazement_p/amazement_p11";
        public const string BODY_AMAZEMENT_P_12 = "/define/amazement_p/amazement_p12";
        public const string BODY_AMAZEMENT_P_21 = "/define/amazement_p/amazement_p21";
        public const string BODY_AMAZEMENT_P_22 = "/define/amazement_p/amazement_p22";
        public const string BODY_AMAZEMENT_P_31 = "/define/amazement_p/amazement_p31";
        public const string BODY_AMAZEMENT_P_32 = "/define/amazement_p/amazement_p32";
        public const string BODY_AMAZEMENT_P_TOUCH = "/define/amazement_p/touch";

        public const string BODY_AMAZEMENT_M_11 = "/define/amazement_m/amazement_m11";
        public const string BODY_AMAZEMENT_M_12 = "/define/amazement_m/amazement_m12";
        public const string BODY_AMAZEMENT_M_21 = "/define/amazement_m/amazement_m21";
        public const string BODY_AMAZEMENT_M_22 = "/define/amazement_m/amazement_m22";
        public const string BODY_AMAZEMENT_M_31 = "/define/amazement_m/amazement_m31";
        public const string BODY_AMAZEMENT_M_32 = "/define/amazement_m/amazement_m32";
        public const string BODY_AMAZEMENT_M_TOUCH = "/define/amazement_m/touch";

        public const string BODY_RAGE_P_11 = "/define/rage_p/rage_p11";
        public const string BODY_RAGE_P_12 = "/define/rage_p/rage_p12";
        public const string BODY_RAGE_P_21 = "/define/rage_p/rage_p21";
        public const string BODY_RAGE_P_22 = "/define/rage_p/rage_p22";
        public const string BODY_RAGE_P_31 = "/define/rage_p/rage_p31";
        public const string BODY_RAGE_P_32 = "/define/rage_p/rage_p32";
        public const string BODY_RAGE_P_TOUCH = "/define/rage_p/touch";

        public const string BODY_RAGE_M_11 = "/define/rage_m/rage_m11";
        public const string BODY_RAGE_M_12 = "/define/rage_m/rage_m12";
        public const string BODY_RAGE_M_21 = "/define/rage_m/rage_m21";
        public const string BODY_RAGE_M_22 = "/define/rage_m/rage_m22";
        public const string BODY_RAGE_M_31 = "/define/rage_m/rage_m31";
        public const string BODY_RAGE_M_32 = "/define/rage_m/rage_m32";
        public const string BODY_RAGE_M_TOUCH = "/define/rage_m/touch";

        public const string BODY_INTEREST_P_11 = "/define/interest_p/interest_p11";
        public const string BODY_INTEREST_P_12 = "/define/interest_p/interest_p12";
        public const string BODY_INTEREST_P_21 = "/define/interest_p/interest_p21";
        public const string BODY_INTEREST_P_22 = "/define/interest_p/interest_p22";
        public const string BODY_INTEREST_P_31 = "/define/interest_p/interest_p31";
        public const string BODY_INTEREST_P_32 = "/define/interest_p/interest_p32";
        public const string BODY_INTEREST_P_TOUCH = "/define/interest_p/touch";

        public const string BODY_INTEREST_M_11 = "/define/interest_m/interest_m11";
        public const string BODY_INTEREST_M_12 = "/define/interest_m/interest_m12";
        public const string BODY_INTEREST_M_21 = "/define/interest_m/interest_m21";
        public const string BODY_INTEREST_M_22 = "/define/interest_m/interest_m22";
        public const string BODY_INTEREST_M_31 = "/define/interest_m/interest_m31";
        public const string BODY_INTEREST_M_32 = "/define/interest_m/interest_m32";
        public const string BODY_INTEREST_M_TOUCH = "/define/interest_m/touch";

        public const string BODY_RESPECT_P_11 = "/define/respect_p/respect_p11";
        public const string BODY_RESPECT_P_12 = "/define/respect_p/respect_p12";
        public const string BODY_RESPECT_P_21 = "/define/respect_p/respect_p21";
        public const string BODY_RESPECT_P_22 = "/define/respect_p/respect_p22";
        public const string BODY_RESPECT_P_31 = "/define/respect_p/respect_p31";
        public const string BODY_RESPECT_P_32 = "/define/respect_p/respect_p32";
        public const string BODY_RESPECT_P_TOUCH = "/define/respect_p/touch";

        public const string BODY_RESPECT_M_11 = "/define/respect_m/respect_m11";
        public const string BODY_RESPECT_M_12 = "/define/respect_m/respect_m12";
        public const string BODY_RESPECT_M_21 = "/define/respect_m/respect_m21";
        public const string BODY_RESPECT_M_22 = "/define/respect_m/respect_m22";
        public const string BODY_RESPECT_M_31 = "/define/respect_m/respect_m31";
        public const string BODY_RESPECT_M_32 = "/define/respect_m/respect_m32";
        public const string BODY_RESPECT_M_TOUCH = "/define/respect_m/touch";

        public const string BODY_CLAMLY_P_11 = "/define/calmly_p/calmly_p11";
        public const string BODY_CLAMLY_P_12 = "/define/calmly_p/calmly_p12";
        public const string BODY_CLAMLY_P_21 = "/define/calmly_p/calmly_p21";
        public const string BODY_CLAMLY_P_22 = "/define/calmly_p/calmly_p22";
        public const string BODY_CLAMLY_P_31 = "/define/calmly_p/calmly_p31";
        public const string BODY_CLAMLY_P_32 = "/define/calmly_p/calmly_p32";
        public const string BODY_CLAMLY_P_TOUCH = "/define/calmly_p/touch";

        public const string BODY_CLAMLY_M_11 = "/define/calmly_m/calmly_m11";
        public const string BODY_CLAMLY_M_12 = "/define/calmly_m/calmly_m12";
        public const string BODY_CLAMLY_M_21 = "/define/calmly_m/calmly_m21";
        public const string BODY_CLAMLY_M_22 = "/define/calmly_m/calmly_m22";
        public const string BODY_CLAMLY_M_31 = "/define/calmly_m/calmly_m31";
        public const string BODY_CLAMLY_M_32 = "/define/calmly_m/calmly_m32";
        public const string BODY_CLAMLY_M_TOUCH = "/define/calmly_m/touch";

        public const string BODY_PROUD_P_11 = "/define/proud_p/proud_p11";
        public const string BODY_PROUD_P_12 = "/define/proud_p/proud_p12";
        public const string BODY_PROUD_P_21 = "/define/proud_p/proud_p21";
        public const string BODY_PROUD_P_22 = "/define/proud_p/proud_p22";
        public const string BODY_PROUD_P_31 = "/define/proud_p/proud_p31";
        public const string BODY_PROUD_P_32 = "/define/proud_p/proud_p32";
        public const string BODY_PROUD_P_TOUCH = "/define/proud_p/touch";

        public const string BODY_PROUD_M_11 = "/define/proud_m/proud_m11";
        public const string BODY_PROUD_M_12 = "/define/proud_m/proud_m12";
        public const string BODY_PROUD_M_21 = "/define/proud_m/proud_m21";
        public const string BODY_PROUD_M_22 = "/define/proud_m/proud_m22";
        public const string BODY_PROUD_M_31 = "/define/proud_m/proud_m31";
        public const string BODY_PROUD_M_32 = "/define/proud_m/proud_m32";
        public const string BODY_PROUD_M_TOUCH = "/define/proud_m/touch";

        public const string BODY_SITDOWN_11 = "/define/sleep/sleep_11";
        public const string BODY_SITDOWN_12 = "/define/sleep/sleep_12";
        public const string BODY_SITDOWN_21 = "/define/sleep/sleep_21";
        public const string BODY_SITDOWN_22 = "/define/sleep/sleep_22";
        public const string BODY_SITDOWN_31 = "/define/sleep/sleep_31";
        public const string BODY_SITDOWN_32 = "/define/sleep/sleep_32";

        public const string BODY_BATTERY_HI_11 = "/define/batteryHi/batteryHi_11";
        public const string BODY_BATTERY_HI_12 = "/define/batteryHi/batteryHi_12";
        public const string BODY_BATTERY_HI_21 = "/define/batteryHi/batteryHi_21";
        public const string BODY_BATTERY_HI_22 = "/define/batteryHi/batteryHi_22";
        public const string BODY_BATTERY_HI_31 = "/define/batteryHi/batteryHi_31";
        public const string BODY_BATTERY_HI_32 = "/define/batteryHi/batteryHi_32";
        public const string BODY_BATTERY_HI_TOUCH = "/define/batteryHi/touch";

        public const string BODY_BATTERY_MID_11 = "/define/batteryMid/batteryMid_11";
        public const string BODY_BATTERY_MID_12 = "/define/batteryMid/batteryMid_12";
        public const string BODY_BATTERY_MID_21 = "/define/batteryMid/batteryMid_21";
        public const string BODY_BATTERY_MID_22 = "/define/batteryMid/batteryMid_22";
        public const string BODY_BATTERY_MID_31 = "/define/batteryMid/batteryMid_31";
        public const string BODY_BATTERY_MID_32 = "/define/batteryMid/batteryMid_32";
        public const string BODY_BATTERY_MID_TOUCH = "/define/batteryMid/touch";

        public const string BODY_BATTERY_LOW_11 = "/define/batteryLow/batteryLow_11";
        public const string BODY_BATTERY_LOW_12 = "/define/batteryLow/batteryLow_12";
        public const string BODY_BATTERY_LOW_21 = "/define/batteryLow/batteryLow_21";
        public const string BODY_BATTERY_LOW_22 = "/define/batteryLow/batteryLow_22";
        public const string BODY_BATTERY_LOW_31 = "/define/batteryLow/batteryLow_31";
        public const string BODY_BATTERY_LOW_32 = "/define/batteryLow/batteryLow_32";
        public const string BODY_BATTERY_LOW_TOUCH = "/define/batteryLow/touch";

        #endregion

        ///=============================
        /// toneXpath定義
        #region toneXpath定義
        public const string TONE_NAME = "/tone/toneDiscription/name";
        public const string TONE_TYPE = "/tone/toneDiscription/type";
        public const string TONE_BEFOR = "/tone/toneDiscription/befor";
        public const string TONE_AFTER = "/tone/toneDiscription/after";
        #endregion

        ///=============================
        /// toneリソース定義
        #region toneリソース定義
        public const string TONE_RESOURCE = "tone";
        #endregion

        ///=============================
        /// アイコンリソース定義
        #region アイコンリソース定義
        public const string ICO_DEF_NEXT = "ico_next";
        public const string ICO_DEF_SLEEP = "ico_sleep";
        public const string ICO_DEF_ZZZ = "ico_zzz";
        public const string ICO_DEF_WAIKUP = "ico_waikup";
        public const string ICO_DEF_SETTING = "ico_setting";
        public const string ICO_DEF_LOG = "ico_log";
        public const string ICO_DEF_POW = "ico_pow";
        public const string ICO_DEF_RSS = "ico_rss";
        public const string ICO_DEF_CHAR = "ico_char";
        public const string ICO_DEF_TRAY = "ico_tray";
        public const string ICO_DEF_BRW = "ico_brw";
        public const string ICO_DEF_WID = "ico_wid";
        public const string ICO_DEF_DOWN = "ico_down";
        public const string BG_DEF_SETTING = "setting";

        public const string ICO_DEF_NON = "battery_non";
        public const string ICO_DEF_BATTERY_0 = "battery_0";
        public const string ICO_DEF_BATTERY_12 = "battery_12";
        public const string ICO_DEF_BATTERY_25 = "battery_25";
        public const string ICO_DEF_BATTERY_37 = "battery_37";
        public const string ICO_DEF_BATTERY_50 = "battery_50";
        public const string ICO_DEF_BATTERY_62 = "battery_62";
        public const string ICO_DEF_BATTERY_75 = "battery_75";
        public const string ICO_DEF_BATTERY_87 = "battery_87";
        public const string ICO_DEF_BATTERY_100 = "battery_100";

        public const string ICO_WID_TES = "widTest";
        public const string ICO_WID_SYS = "widSys";
        public const string ICO_WID_BRW = "widBrw";
        public const string ICO_WID_WET = "widWet";
        public const string ICO_WID_RSS = "widRss";
        public const string ICO_WID_CPU = "widCpu";
        public const string ICO_WID_MEM = "widMem";
        public const string ICO_WID_HDD = "widHdd";
        public const string ICO_WID_NET = "widNet";
        public const string ICO_EXT_FLV = "dlDoga";
        public const string ICO_EXT_MP3 = "dlMp3";

        public const string ICO_BTN_LNK = "btnLink";
        #endregion

    }
}
