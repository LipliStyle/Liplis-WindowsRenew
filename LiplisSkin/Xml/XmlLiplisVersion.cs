//=======================================================================
//  ClassName : XmlVersion
//  概要      : version.xmlの実体
//              version.xmlを読みこませることでインスタンス化、使うことができる。
//
//  Liplis5.0
//
//  Copyright(c) 2010-2016 LipliStyle.Sachin
//=======================================================================
using Liplis.Exp;
using System.Reflection;

namespace Liplis.Xml
{
    public class XmlLiplisVersion : XmlSetting
    {
        ///=============================
        ///プロパティ
        public string version { get; set; }

        ///=============================
        /// 設定ファイル定義
        #region 設定ファイル定義
        public const string PREFS_VERSION = "version";
        #endregion



        /// <summary>
        /// 
        /// </summary>
        public XmlLiplisVersion(string versionFilePath)
        {
            //設定の取得
            setting = new SharedPreferences(versionFilePath);

            getPreferenceData();

        }

        /// <summary>
        /// getPreferenceData
        /// プリファレンスデータの取得
        /// </summary>
        #region getPreferenceData
        public override void getPreferenceData()
        {
            try
            {
                //メイン設定の読込
                version = setting.getString(PREFS_VERSION, Assembly.GetExecutingAssembly().GetName().Version.ToString());
            }
            catch (System.Exception err)
            {
                //読み込みに失敗した場合はエラー
                throw new SkinNotFoundException(err);
            }

        }
        #endregion

        /// <summary>
        /// setPreferenceData
        /// セーブ
        /// </summary>
        #region setPreferenceData
        public override void setPreferenceData()
        {
            setting.setString(PREFS_VERSION, this.version);
            setting.saveSettings();
        }
        #endregion
    }
}
