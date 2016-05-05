//=======================================================================
//  ClassName : Skin
//  概要      : リプリススキンクラス
//              スキンに含まれているxml、画像を読みだしインスタンス生成する。
//              Liplisでは、このクラスを使って、必要情報、ファイルを取得する
//
//  Liplis5.0
//
//  Copyright(c) 2010-2016 LipliStyle.Sachin
//=======================================================================

using Liplis.Xml;

namespace Liplis
{
    public class Skin
    {
        ///=============================
        ///キャラクター名
        public string charName { get; set; }

        ///=============================
        ///実データ
        public XmlSkin xmlSkin { get; set; }
        public XmlBody xmlBody { get; set; }
        public XmlChat xmlChat { get; set; }
        public XmlTouch xmlTouch { get; set; }

        /// <summary>
        /// コンストラクター
        /// </summary>
        /// <param name="loadSkinName"></param>
        public Skin(string skinSettingPath)
        {
            //各XMLのインスタンス化
            xmlSkin = new XmlSkin(skinSettingPath);
            xmlBody = new XmlBody(xmlSkin.charName);
            xmlChat = new XmlChat(xmlSkin.charName);
            xmlTouch = new XmlTouch(xmlSkin.charName);

            //キャラクター名の取得
            this.charName = xmlSkin.charName;
        }
    }
}
