//=======================================================================
//  ClassName : LiplisRssCategroyManager
//  概要      : RSSのカテゴリーを管理する
//
//Liplis5.0
//
//アップデート履歴
//   2016/05/08 ver5.0.0 作成
//
//  Copyright(c) 2010-2016 LipliStyle.Sachin
//=======================================================================
using Liplis.Activity;
using Liplis.Com;
using Liplis.Gui;
using Liplis.Utl;
using Liplis.Xml;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;

namespace Liplis.MainSystem
{
    public class LiplisRssCategroyManager : SharedPreferences
    {
        ///=============================
        /// jsonファイル名
        private const string CAT_MANAGER_FILE = "rsscat";

        ///=============================
        /// キーリスト
        public List<string> catList;

        /// <summary>
        /// コンストラクター
        /// </summary>
        public LiplisRssCategroyManager():base(LpsPathController.getSettingPath() + CAT_MANAGER_FILE)
        {
            //復活させる
            this.getKeyList();
        }

        /// <summary>
        /// キーリストをプリファレンスから復元し、取得する
        /// </summary>
        /// <returns></returns>
        private void getKeyList()
        {
            //jsonのテキスト
            string jsonText = getString(CAT_MANAGER_FILE, "");

            //キーリストの取得
            this.catList = JsonConvert.DeserializeObject<List<string>>(jsonText);

            //キーリストがNullなら新規作成
            if (this.catList == null)
            {
                this.catList = new List<string>();
                saveSettings();
            }

            //デフォルトカテゴリリストの追加
            if(!this.catList.Contains(ViewLiplisRssSetting.CAT_DEFAULT))
            {
                this.catList.Add(ViewLiplisRssSetting.CAT_DEFAULT);
                saveSettings();
            }
        }

        /// <summary>
        /// キーを追加する
        /// </summary>
        /// <param name="pKey"></param>
        public void addKeyFromiUi(string pKey)
        {
            if (!catList.Contains(pKey))
            {
                catList.Add(pKey);
                this.saveKeyList();
            }
            else if(pKey == ViewLiplisRssSetting.CAT_DEFAULT)
            {
                LpsMessage.showError("その名前は使用できません。");
            }
            else
            {
                LpsMessage.showError("そのカテゴリは既に登録されています。");
            }
        }
        public void addKey(string pKey)
        {
            if (!catList.Contains(pKey))
            {
                catList.Add(pKey);
                this.saveKeyList();
            }
        }

        /// <summary>
        /// キーを削除する
        /// </summary>
        /// <param name="pKey"></param>
        public void delKey(string pKey)
        {
            if (catList.Contains(pKey))
            {
                //キーリストから削除
                catList.Remove(pKey);

                //もし保存ファイルが存在したらこちらも削除しておく
                string filePath = LpsPathController.getSettingPath() + pKey;
                if (LpsLiplisUtil.ExistsFile(filePath))
                {
                    File.Delete(filePath);
                }

                this.saveKeyList();
            }
        }

        /// <summary>
        /// キーリストを保存する
        /// </summary>
        public void saveKeyList()
        {
            string jsonText = JsonConvert.SerializeObject(this.catList);

            setString(CAT_MANAGER_FILE, jsonText);

            saveSettings();
        }


    }
}
