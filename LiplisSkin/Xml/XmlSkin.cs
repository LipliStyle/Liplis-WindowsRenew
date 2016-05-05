//=======================================================================
//  ClassName : XmlSkin
//  概要      : skin.xmlの実体
//              skin.xmlを読みこませることでインスタンス化、使うことができる。
//
//  Liplis5.0
//
//  2013/07/14 NoralisEditor2.2.2 テーマカラー設定追加
//  Copyright(c) 2010-2016 LipliStyle.Sachin
//=======================================================================
using Liplis.Com;
using Liplis.Exp;
using Liplis.Utl;
using Liplis.Xml;
using System;
using System.Reflection;


namespace Liplis.Xml
{
    [Serializable]
    public class XmlSkin : XmlSetting
    {
                ///=============================
        ///プロパティ
        public string charName          { get; set; }   //キャラ名
        public string textFont          { get; set; }   //テキストフォント
        public string textColor         { get; set; }   //テキストカラー
        public string linkColor         { get; set; }   //リンクカラー
        public string titleColor        { get; set; }   //タイトルカラー  
        public string themaColor        { get; set; }   //テーマカラー    //ver2.2.2
        public string themaColorSub     { get; set; }   //テーマカラー    //ver2.2.2
        public string charIntroduction  { get; set; }   //キャラクター紹介
        public string version           { get; set; }   //バージョン
        public string toneUrl           { get; set; }   //トーンURL


        ///====================================================================
        ///
        ///                            初期化処理
        ///                         
        ///====================================================================
        #region コンストラクター
        /// <summary>
        /// コンストラクター
        /// skin.xmlのパスを指定して読み込む
        /// </summary>

        public XmlSkin(string skinSettingPath):base()
        {
            try
            {
                //設定の取得
                setting = new SharedPreferences(skinSettingPath);

                //読み込み
                getPreferenceData();

            }
            catch (Exception)
            {
                //読み込みに失敗した場合はエラー
                throw new SkinNotFoundException();
            }
        }


        /// <summary>
        /// コンストラクター
        /// 要素を直接指定し、スキン設定を作成
        /// </summary>
        public XmlSkin(string charName, int width, int height, string textFont, string textColor, string linkColor, string titleColor, string themaColor, string themaColorSub)
        {
            try
            {
                //設定
                this.charName = charName;
                this.textFont = textFont;
                this.textColor = textColor;
                this.linkColor = linkColor;
                this.titleColor = titleColor;
                this.themaColor = themaColor;
                this.themaColorSub = themaColorSub;

            }
            catch (Exception)
            {
                //読み込みに失敗した場合はエラー
                throw new SkinNotFoundException();
            }
        }
        

        /// <summary>
        /// コンストラクター
        /// </summary>
        
        public XmlSkin()
        {
        }
        #endregion

        ///====================================================================
        ///
        ///                            読み込み処理
        ///                         
        ///====================================================================
        #region 読み込み処理
        /// <summary>
        /// readResult
        /// readXmlで読み込んだ結果を自変数に読み込む
        /// </summary>

        public override void getPreferenceData()
        {
            try
            {
                charName = setting.getString(LpsDefine.SKIN_CHAR_NAME, LpsDefine.SKIN_DEF_CHAR_NAME);
                textFont = setting.getString(LpsDefine.SKIN_FONT, LpsDefine.SKIN_DEF_FONT);
                textColor = setting.getString(LpsDefine.SKIN_TEXT_COLOR, LpsDefine.SKIN_DEF_TEXT_COLOR);
                linkColor = setting.getString(LpsDefine.SKIN_LINK_COLOR, LpsDefine.SKIN_DEF_LINK_COLOR);
                titleColor = setting.getString(LpsDefine.SKIN_TITLE_COLOR, LpsDefine.SKIN_DEF_TITLE_COLOR);
                themaColor = setting.getString(LpsDefine.SKIN_THEMA_COLOR, LpsDefine.SKIN_DEF_THEMA_COLOR);     //ver2.2.2
                themaColorSub = setting.getString(LpsDefine.SKIN_THEMA_COLOR2, LpsDefine.SKIN_DEF_THEMA_COLOR2);   //ver2.2.2
                charIntroduction = setting.getString(LpsDefine.SKIN_CHAR_INTRO, LpsDefine.SKIN_DEF_CHAR_INTRO);
                version = setting.getString(LpsDefine.SKIN_VERSION, LpsDefine.SKIN_DEF_VERSION);
                toneUrl = setting.getString(LpsDefine.SKIN_TONE_URL, LpsDefine.SKIN_DEF_TONE_URL);
            }
            catch (System.Exception err)
            {
                //読み込みに失敗した場合はエラー
                throw new SkinNotFoundException(err);
            }

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
        public override void setPreferenceData()
        {

        }
        #endregion












    }
}
