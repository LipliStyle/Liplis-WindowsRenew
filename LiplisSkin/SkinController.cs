//=======================================================================
//  ClassName : SkinController
//  概要      : リプリス スキンコントローラー
//              プログラムと同階層にある「Skin」フォルダに配置されているスキンをすべて読み込み、
//              対象プログラムから使えるようにする。
//              
//　　　　　　　キャラクターの一覧を取得したり、キャラクター指定でスキンインスタンスを取得したりできる。
//  Liplis5.0
//
//  Copyright(c) 2010-2016 LipliStyle.Sachin
//=======================================================================
using Liplis.Utl;
using System.Collections.Generic;
using System.IO;

namespace Liplis
{
    public class SkinController
    {
        ///=============================
        ///ディクショナリ
        public Dictionary<string, Skin> dicSkin { get; set; }   //キーアクセスリスト
        public List<Skin> lstSkin { get; set; }                 //インデクサアクセスリスト
        public string skinPath { get; set; }

        ///=============================
        ///スキンファイル
        public const string SKIN_FILE_NAME = "skin.xml";

        ///=============================
        ///読込結果
        bool flgResult = true;

        /// <summary>
        /// コンストラクター
        /// </summary>
        public SkinController()
        {
            //スキンフォルダに配置されているすべてのスキンを読み込む
            loadAllSkin();

           
        }

        /// <summary>
        /// すべてのスキンを取得する
        /// </summary>
        public void loadAllSkin()
        {
            //スキンリストの初期化
            dicSkin = new Dictionary<string, Skin>();
            lstSkin = new List<Skin>();

            //スキンフォルダのチェック&取得
            skinPath = LpsPathController.getSkinPath();


            //スキンフォルダ内のフォルダ一覧を取得する
            string[] dirs = Directory.GetDirectories(skinPath);

            //スキン設定パス
            string skinSettingpath = "";

            //スキンファイルの存在確認とオブジェクトリストの生成
            foreach (string dir in dirs)
            {
                try
                {
                    //スキンファイルパス
                    skinSettingpath = dir + "\\" + SKIN_FILE_NAME;

                    //ファイルの存在チェック
                    if (LpsPathController.checkFileExist(skinSettingpath))
                    {
                        //ファイルが存在したら読み込み
                        Skin skin = new Skin(skinSettingpath);

                        //辞書に登録する
                        if (!dicSkin.ContainsKey(skin.charName))
                        {
                            dicSkin.Add(skin.charName, skin);
                            lstSkin.Add(skin);
                        }
                        else
                        {
                            LpsLogController.writingLogSt("同一キャラクターのスキンが検出されました。同一キャラクターのスキンは最初のものを除き、スキップされます。");
                            LpsLogController.writingLogSt("対象パス:" + skinSettingpath);
                            flgResult = false;  
                        }
                    }
                }
                catch
                {
                    LpsLogController.writingLogSt("スキンファイルの読み込みに失敗しました。確認してください。");
                    LpsLogController.writingLogSt("対象パス:" + skinSettingpath);
                    flgResult = false;
                }
            }
        }

        /// <summary>
        /// エラー回避のためのダミー
        /// </summary>
        private void dummy(){if (flgResult) { }
}
    }
}
