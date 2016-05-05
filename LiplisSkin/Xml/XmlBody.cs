//=======================================================================
//  ClassName : XmlBody
//  概要      : body.xmlの実体
//              body.xmlを読みこませることでインスタンス化、立ち絵をプログラムで使えるようにする。
//
//  Liplis5.0
//
//  Copyright(c) 2010-2016 LipliStyle.Sachin
//=======================================================================
using Liplis.Body;
using Liplis.Com;
using Liplis.Exp;
using Liplis.Lst;
using Liplis.Utl;
using System;
using System.Collections.Generic;
using System.Xml;

namespace Liplis.Xml
{
    public class XmlBody : XmlReadList
    {
        ///==========================
        /// スキンパス
        protected string loadSkin;

        ///==========================
        /// ボディプロパティ
        public int height { get; set; }
        public int width { get; set; }
        public int locX { get; set; }
        public int locY { get; set; }

        ///==========================
        /// リスト
        public LstShufflableList<BaseLpsBody> normalList { get; set; }
        public LstShufflableList<BaseLpsBody> joyPList { get; set; }
        public LstShufflableList<BaseLpsBody> joyMList { get; set; }
        public LstShufflableList<BaseLpsBody> admirationPList { get; set; }
        public LstShufflableList<BaseLpsBody> admirationMList { get; set; }
        public LstShufflableList<BaseLpsBody> peacePList { get; set; }
        public LstShufflableList<BaseLpsBody> peaceMList { get; set; }
        public LstShufflableList<BaseLpsBody> ecstasyPList { get; set; }
        public LstShufflableList<BaseLpsBody> ecstasyMList { get; set; }
        public LstShufflableList<BaseLpsBody> amazementPList { get; set; }
        public LstShufflableList<BaseLpsBody> amazementMList { get; set; }
        public LstShufflableList<BaseLpsBody> ragePList { get; set; }
        public LstShufflableList<BaseLpsBody> rageMList { get; set; }
        public LstShufflableList<BaseLpsBody> interestPList { get; set; }
        public LstShufflableList<BaseLpsBody> interestMList { get; set; }
        public LstShufflableList<BaseLpsBody> respectPList { get; set; }
        public LstShufflableList<BaseLpsBody> respectMList { get; set; }
        public LstShufflableList<BaseLpsBody> calmlyPList { get; set; }
        public LstShufflableList<BaseLpsBody> calmlyMList { get; set; }
        public LstShufflableList<BaseLpsBody> proudPList { get; set; }
        public LstShufflableList<BaseLpsBody> proudMList { get; set; }

        public LstShufflableList<BaseLpsBody> sitdownList { get; set; }

        ///=============================
        /// 破損ボディ
        public LstShufflableList<BaseLpsBody> batteryHiList { get; set; }           //小破 2013/10/27 ver3.2.0
        public LstShufflableList<BaseLpsBody> batteryMidList { get; set; }          //中破 2013/10/27 ver3.2.0
        public LstShufflableList<BaseLpsBody> batteryLowList { get; set; }          //大破 2013/10/27 ver3.2.0

        ///==========================
        /// インデックス
        protected int idx;

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



        ///=============================
        /// Emotion画像定義

        public const string EMOTION_NORMAL = "normal";
        public const string EMOTION_JOY_P = "joy_p";
        public const string EMOTION_JOY_M = "joy_m";
        public const string EMOTION_ADMIRATION_P = "admiration_p";
        public const string EMOTION_ADMIRATION_M = "admiration_m";
        public const string EMOTION_PEACE_P = "peace_p";
        public const string EMOTION_PEACE_M = "peace_m";
        public const string EMOTION_ECSTASY_P = "ecstasy_p";
        public const string EMOTION_ECSTASY_M = "ecstasy_m";
        public const string EMOTION_AMAZEMENT_P = "amazement_p";
        public const string EMOTION_AMAZEMENT_M = "amazement_m";
        public const string EMOTION_RAGE_P = "rage_p";
        public const string EMOTION_RAGE_M = "rage_m";
        public const string EMOTION_INTEREST_P = "interest_p";
        public const string EMOTION_INTEREST_M = "interest_m";
        public const string EMOTION_RESPECT_P = "respect_p";
        public const string EMOTION_RESPECT_M = "respect_m";
        public const string EMOTION_CLAMLY_P = "calmly_p";
        public const string EMOTION_CLAMLY_M = "calmly_m";
        public const string EMOTION_PROUD_P = "proud_p";
        public const string EMOTION_PROUD_M = "proud_m";
        public const string EMOTION_BATTERY_HI = "battery_hi";
        public const string EMOTION_BATTERY_MID = "battery_mid";
        public const string EMOTION_BATTERY_LOW = "battery_low";
        #endregion


        ///====================================================================
        ///
        ///                           コンストラクター
        ///                         
        ///====================================================================
        #region コンストラクター
        /// <summary>
        /// コンストラクター
        /// ロード中のスキンからbody.xmlの位置を特定し、読み込み
        /// </summary>
        public XmlBody(string loadSkin)
        {
            try
            {
                //ロードスキン
                this.loadSkin = loadSkin;

                //xmlドキュメント
                xmlDoc = new XmlDocument();

                //リストの初期化
                initList();

                //キャッシュファイルの取得
                xmlFilePath = LpsPathController.getBodyDefinePath(loadSkin);

                //body.xmlの存在チェック
                if (LpsPathController.checkFileExist(xmlFilePath))
                {
                    //xmlの読み込み
                    readXml();

                    //サイズとロケーションを取得する
                    getSizeAndLocation();

                    //読み込み結果の取得
                    createList();
                }
                else
                {
                    //読み込みに失敗した場合はエラー
                    throw new SkinNotFoundException();
                }
            }
            catch (System.Exception err)
            {
                throw new SkinNotFoundException(err);
            }
        }
        /// <summary>
        /// デフォルトコンストラクター
        /// </summary>
        public XmlBody()
        {
            try
            {
                this.height = 0;
                this.width = 0;
                this.locX = 0;
                this.locY = 0;
            }
            catch (Exception)
            {
                throw new SkinNotFoundException();
            }
        }
        #endregion

        ///====================================================================
        ///
        ///                            初期化処理
        ///                         
        ///====================================================================
        #region 初期化処理
        /// <summary>
        /// getSizeAndLocation
        /// ロケーションとサイズを取得する
        /// ☆Miniオーバーライド
        /// </summary>

        protected virtual void getSizeAndLocation()
        {
            this.height = rXLMSInt(xmlDoc.SelectNodes(BODY_HEIGHT));
            this.width  = rXLMSInt(xmlDoc.SelectNodes(BODY_WIDHT));
            this.locX   = rXLMSInt(xmlDoc.SelectNodes(BODY_LOCATION_X));
            this.locY   = rXLMSInt(xmlDoc.SelectNodes(BODY_LOCATION_Y));
        }

        /// <summary>
        /// リストの初期化
        /// </summary>
        protected void initList()
        {
            normalList      = new LstShufflableList<BaseLpsBody>();
            joyPList        = new LstShufflableList<BaseLpsBody>();
            joyMList        = new LstShufflableList<BaseLpsBody>();
            admirationPList = new LstShufflableList<BaseLpsBody>();
            admirationMList = new LstShufflableList<BaseLpsBody>();
            peacePList      = new LstShufflableList<BaseLpsBody>();
            peaceMList      = new LstShufflableList<BaseLpsBody>();
            ecstasyPList    = new LstShufflableList<BaseLpsBody>();
            ecstasyMList    = new LstShufflableList<BaseLpsBody>();
            amazementPList  = new LstShufflableList<BaseLpsBody>();
            amazementMList  = new LstShufflableList<BaseLpsBody>();
            ragePList       = new LstShufflableList<BaseLpsBody>();
            rageMList       = new LstShufflableList<BaseLpsBody>();
            interestPList   = new LstShufflableList<BaseLpsBody>();
            interestMList   = new LstShufflableList<BaseLpsBody>();
            respectPList    = new LstShufflableList<BaseLpsBody>();
            respectMList    = new LstShufflableList<BaseLpsBody>();
            calmlyPList     = new LstShufflableList<BaseLpsBody>();
            calmlyMList     = new LstShufflableList<BaseLpsBody>();
            proudPList      = new LstShufflableList<BaseLpsBody>();
            proudMList      = new LstShufflableList<BaseLpsBody>();
            sitdownList     = new LstShufflableList<BaseLpsBody>();
        }

        /// <summary>
        /// リストの作成
        /// </summary>
        protected virtual void createList()
        {
            normalList      = readResult(BODY_NORMAL_11, BODY_NORMAL_12, BODY_NORMAL_21, BODY_NORMAL_22, BODY_NORMAL_31, BODY_NORMAL_32, BODY_NORMAL_TOUCH);
            joyPList        = readResult(BODY_JOY_P_11, BODY_JOY_P_12, BODY_JOY_P_21, BODY_JOY_P_22, BODY_JOY_P_31, BODY_JOY_P_32, BODY_JOY_P_TOUCH);
            joyMList        = readResult(BODY_JOY_M_11, BODY_JOY_M_12, BODY_JOY_M_21, BODY_JOY_M_22, BODY_JOY_M_31, BODY_JOY_M_32, BODY_JOY_M_TOUCH);
            admirationPList = readResult(BODY_ADMIRATION_P_11, BODY_ADMIRATION_P_12, BODY_ADMIRATION_P_21, BODY_ADMIRATION_P_22, BODY_ADMIRATION_P_31, BODY_ADMIRATION_P_32, BODY_ADMIRATION_P_TOUCH);
            admirationMList = readResult(BODY_ADMIRATION_M_11, BODY_ADMIRATION_M_12, BODY_ADMIRATION_M_21, BODY_ADMIRATION_M_22, BODY_ADMIRATION_M_31, BODY_ADMIRATION_M_32, BODY_ADMIRATION_M_TOUCH);
            peacePList      = readResult(BODY_PEACE_P_11, BODY_PEACE_P_12, BODY_PEACE_P_21, BODY_PEACE_P_22, BODY_PEACE_P_31, BODY_PEACE_P_32, BODY_PEACE_P_TOUCH);
            peaceMList      = readResult(BODY_PEACE_M_11, BODY_PEACE_M_12, BODY_PEACE_M_21, BODY_PEACE_M_22, BODY_PEACE_M_31, BODY_PEACE_M_32, BODY_PEACE_M_TOUCH);
            ecstasyPList    = readResult(BODY_ECSTASY_P_11, BODY_ECSTASY_P_12, BODY_ECSTASY_P_21, BODY_ECSTASY_P_22, BODY_ECSTASY_P_31, BODY_ECSTASY_P_32, BODY_ECSTASY_P_TOUCH);
            ecstasyMList    = readResult(BODY_ECSTASY_M_11, BODY_ECSTASY_M_12, BODY_ECSTASY_M_21, BODY_ECSTASY_M_22, BODY_ECSTASY_M_31, BODY_ECSTASY_M_32, BODY_ECSTASY_M_TOUCH);
            amazementPList  = readResult(BODY_AMAZEMENT_P_11, BODY_AMAZEMENT_P_12, BODY_AMAZEMENT_P_21, BODY_AMAZEMENT_P_22, BODY_AMAZEMENT_P_31, BODY_AMAZEMENT_P_32, BODY_AMAZEMENT_P_TOUCH);
            amazementMList  = readResult(BODY_AMAZEMENT_M_11, BODY_AMAZEMENT_M_12, BODY_AMAZEMENT_M_21, BODY_AMAZEMENT_M_22, BODY_AMAZEMENT_M_31, BODY_AMAZEMENT_M_32, BODY_AMAZEMENT_M_TOUCH);
            ragePList       = readResult(BODY_RAGE_P_11, BODY_RAGE_P_12, BODY_RAGE_P_21, BODY_RAGE_P_22, BODY_RAGE_P_31, BODY_RAGE_P_32, BODY_RAGE_P_TOUCH);
            rageMList       = readResult(BODY_RAGE_M_11, BODY_RAGE_M_12, BODY_RAGE_M_21, BODY_RAGE_M_22, BODY_RAGE_M_31, BODY_RAGE_M_32, BODY_RAGE_M_TOUCH);
            interestPList   = readResult(BODY_INTEREST_P_11, BODY_INTEREST_P_12, BODY_INTEREST_P_21, BODY_INTEREST_P_22, BODY_INTEREST_P_31, BODY_INTEREST_P_32, BODY_INTEREST_P_TOUCH);
            interestMList   = readResult(BODY_INTEREST_M_11, BODY_INTEREST_M_12, BODY_INTEREST_M_21, BODY_INTEREST_M_22, BODY_INTEREST_M_31, BODY_INTEREST_M_32, BODY_INTEREST_M_TOUCH);
            respectPList    = readResult(BODY_RESPECT_P_11, BODY_RESPECT_P_12, BODY_RESPECT_P_21, BODY_RESPECT_P_22, BODY_RESPECT_P_31, BODY_RESPECT_P_32, BODY_RESPECT_P_TOUCH);
            respectMList    = readResult(BODY_RESPECT_M_11, BODY_RESPECT_M_12, BODY_RESPECT_M_21, BODY_RESPECT_M_22, BODY_RESPECT_M_31, BODY_RESPECT_M_32, BODY_RESPECT_M_TOUCH);
            calmlyPList     = readResult(BODY_CLAMLY_P_11, BODY_CLAMLY_P_12, BODY_CLAMLY_P_21, BODY_CLAMLY_P_22, BODY_CLAMLY_P_31, BODY_CLAMLY_P_32, BODY_CLAMLY_P_TOUCH);
            calmlyMList     = readResult(BODY_CLAMLY_M_11, BODY_CLAMLY_M_12, BODY_CLAMLY_M_21, BODY_CLAMLY_M_22, BODY_CLAMLY_M_31, BODY_CLAMLY_M_32, BODY_CLAMLY_M_TOUCH);
            proudPList      = readResult(BODY_PROUD_P_11, BODY_PROUD_P_12, BODY_PROUD_P_21, BODY_PROUD_P_22, BODY_PROUD_P_31, BODY_PROUD_P_32, BODY_PROUD_P_TOUCH);
            proudMList      = readResult(BODY_PROUD_M_11, BODY_PROUD_M_12, BODY_PROUD_M_21, BODY_PROUD_M_22, BODY_PROUD_M_31, BODY_PROUD_M_32, BODY_PROUD_M_TOUCH);
            sitdownList     = readResult(BODY_SITDOWN_11, BODY_SITDOWN_12, BODY_SITDOWN_21, BODY_SITDOWN_22, BODY_SITDOWN_31, BODY_SITDOWN_32, "");
            batteryHiList   = readResult(BODY_BATTERY_HI_11, BODY_BATTERY_HI_12, BODY_BATTERY_HI_21, BODY_BATTERY_HI_22, BODY_BATTERY_HI_31, BODY_BATTERY_HI_32, BODY_BATTERY_HI_TOUCH);                       //小破 2013/10/27 ver3.2.0
            batteryMidList  = readResult(BODY_BATTERY_MID_11, BODY_BATTERY_MID_12, BODY_BATTERY_MID_21, BODY_BATTERY_MID_22, BODY_BATTERY_MID_31, BODY_BATTERY_MID_32, BODY_BATTERY_MID_TOUCH);           //中破 2013/10/27 ver3.2.0
            batteryLowList  = readResult(BODY_BATTERY_LOW_11, BODY_BATTERY_LOW_12, BODY_BATTERY_LOW_21, BODY_BATTERY_LOW_22, BODY_BATTERY_LOW_31, BODY_BATTERY_LOW_32, BODY_BATTERY_LOW_TOUCH);           //大破 2013/10/27 ver3.2.0
        }
        #endregion

        ///====================================================================
        ///
        ///                            読み込み処理
        ///                         
        ///====================================================================
        #region 読み込み処理
        /// <summary>
        /// 設定読込
        /// </summary>
        protected virtual LstShufflableList<BaseLpsBody> readResult(string b11, string b12, string b21, string b22, string b31, string b32, string touch)
        {
            LstShufflableList<BaseLpsBody> result = new LstShufflableList<BaseLpsBody>();
            int idx = 0;
            List<string> b11l = new List<string>();
            List<string> b12l = new List<string>();
            List<string> b21l = new List<string>();
            List<string> b22l = new List<string>();
            List<string> b31l = new List<string>();
            List<string> b32l = new List<string>();
            List<string> tl = new List<string>();

            readXmlList(xmlDoc.SelectNodes(b11), b11l);
            readXmlList(xmlDoc.SelectNodes(b12), b12l);
            readXmlList(xmlDoc.SelectNodes(b21), b21l);
            readXmlList(xmlDoc.SelectNodes(b22), b22l);
            readXmlList(xmlDoc.SelectNodes(b31), b31l);
            readXmlList(xmlDoc.SelectNodes(b32), b32l);

            //タッチ設定のロード
            if (touch.Length > 0)
            {
                readXmlList(xmlDoc.SelectNodes(touch), tl);
            }

            //タッチ設定が無い場合は、空のタッチリスト作成
            if (tl.Count < 1)
            {
                foreach (string r11 in b11l)
                {
                    tl.Add("");
                }
            }


            //リストを回してオブジェクト生成
            foreach (string r11 in b11l)
            {
                try
                {
                    result.Add(new LpsBody(b11l[idx], b12l[idx], b21l[idx], b22l[idx], b31l[idx], b32l[idx], tl[idx], LpsPathController.getBodyPath(loadSkin)));
                }
                catch
                {
                    continue;
                }
                idx++;
            }

            return result;
        }
        #endregion


        ///====================================================================
        ///
        ///                            書き込み処理
        ///                         
        ///====================================================================
        #region 書き込み処理
        /// <summary>
        /// setPreferenceData
        /// セーブ
        /// </summary>
        public virtual void saveSettings()
        {

        }
        #endregion

        ///====================================================================
        ///
        ///                     ボディオブジェクトの取得
        ///                         
        ///====================================================================
        #region ボディオブジェクトの取得
        /// <summary>
        /// bodyの取得
        /// </summary>
        /// <param name="emotion"></param>
        /// <returns></returns>
        public BaseLpsBody getLiplisBody(int emotion, int point)
        {
            //2014/10/04 emotionが0でなく、ポイントが0の場合、エモーションの値をポイントにセットする。
            //文章の場合は、エモーションのみの設定となるため。
            if (emotion != 0 && point == 0)
            {
                point = emotion;
            }

            //絶対値をとっておく。
            emotion = Math.Abs(emotion);

            if (emotion == 0)
            {
                return selectBody(normalList);
            }
            else if (emotion == 1)
            {
                if (point >= 0)
                {
                    return selectBody(joyPList);
                }
                else
                {
                    return selectBody(joyMList);
                }
            }
            else if (emotion == 2)
            {
                if (point >= 0)
                {
                    return selectBody(admirationPList);
                }
                else
                {
                    return selectBody(admirationMList);
                }
            }
            else if (emotion == 3)
            {
                if (point >= 0)
                {
                    return selectBody(peacePList);
                }
                else
                {
                    return selectBody(peaceMList);
                }
            }
            else if (emotion == 4)
            {
                if (point >= 0)
                {
                    return selectBody(ecstasyPList);
                }
                else
                {
                    return selectBody(ecstasyMList);
                }
            }
            else if (emotion == 5)
            {
                if (point >= 0)
                {
                    return selectBody(amazementPList);
                }
                else
                {
                    return selectBody(amazementMList);
                }
            }
            else if (emotion == 6)
            {
                if (point >= 0)
                {
                    return selectBody(ragePList);
                }
                else
                {
                    return selectBody(rageMList);
                }
            }
            else if (emotion == 7)
            {
                if (point >= 0)
                {
                    return selectBody(interestPList);
                }
                else
                {
                    return selectBody(interestMList);
                }
            }
            else if (emotion == 8)
            {
                if (point >= 0)
                {
                    return selectBody(respectPList);
                }
                else
                {
                    return selectBody(respectMList);
                }
            }
            else if (emotion == 9)
            {
                if (point >= 0)
                {
                    return selectBody(calmlyPList);
                }
                else
                {
                    return selectBody(calmlyMList);
                }
            }
            else if (emotion == 10)
            {
                if (point >= 0)
                {
                    return selectBody(proudPList);
                }
                else
                {
                    return selectBody(proudMList);
                }
            }
            else if (emotion == 100)
            {
                return selectBody(sitdownList);
            }
            else
            {
                return selectBody(normalList);
            }
        }

        /// <summary>
        /// ボティをランダムに取得する
        /// </summary>
        protected BaseLpsBody selectBody(LstShufflableList<BaseLpsBody> lst)
        {
            if (lst.Count > 0)
            {
                lst.Shuffle();
                return lst[LpsLiplisUtil.getRandamInt(0, lst.Count)];
            }
            return normalList[0];
        }
            
        /// <summary>
        /// 健康状態状態からIDを取得する
        /// </summary>
        public BaseLpsBody getLiplisBodyHelth(int helth, int emotion, int point)
        {
            try
            {
                //小破以上
                if (helth > 50)
                {
                    if (batteryHiList.Count == 0)
                    {
                        return getLiplisBody(emotion, point);
                    }
                    else
                    {
                        return selectBody(batteryHiList);
                    }
                }
                //中破
                else if (helth > 25)
                {
                    if (batteryMidList.Count == 0)
                    {
                        return getLiplisBody(emotion, point);
                    }
                    else
                    {
                        return selectBody(batteryMidList);
                    }
                }
                //大破
                else
                {
                    if (batteryLowList.Count == 0)
                    {
                        return getLiplisBody(emotion, point);
                    }
                    else
                    {
                        return selectBody(batteryLowList);
                    }
                }
            }
            catch
            {
                return normalList[0];
            }
        }
        #endregion
    }
}
