//=======================================================================
//  ClassName : LiplisKeyManager
//  概要      : ウィジェットごとのキーを管理する
//
//Liplis5.0
//
//アップデート履歴
//   2016/05/08 ver5.0.0 作成
//
//  Copyright(c) 2010-2016 LipliStyle.Sachin
//=======================================================================

using Liplis.Utl;
using Liplis.Xml;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Liplis.MainSystem
{
    public class LiplisKeyManager : SharedPreferences
    {
        ///=============================
        /// jsonファイル名
        private const string KEY_MANAGER_FILE = "keyman";

        ///=============================
        /// キーリスト
        public List<string> keyList;
        
        /// <summary>
        /// コンストラクター
        /// </summary>
        public LiplisKeyManager():base(LpsPathController.getSettingPath() + KEY_MANAGER_FILE)
        {
            //復活させる
            this.getKeyList();
        }

        /// <summary>
        /// キーを追加する
        /// </summary>
        /// <param name="pKey"></param>
        public void addKey(string pKey)
        {
            if(!keyList.Contains(pKey))
            {
                keyList.Add(pKey);
                this.saveKeyList();
            }
        }

        /// <summary>
        /// キーを削除する
        /// </summary>
        /// <param name="pKey"></param>
        public void delKey(string pKey)
        {
            if(keyList.Contains(pKey))
            {
                keyList.Remove(pKey);
                this.saveKeyList();
            }
        }

        /// <summary>
        /// キーをすべて削除する
        /// </summary>
        public void delAllKey()
        {
            this.keyList.Clear();
            this.saveKeyList();
        }

        /// <summary>
        /// キーリストを保存する
        /// </summary>
        private void saveKeyList()
        {
            string jsonText = JsonConvert.SerializeObject(this.keyList);

            setString(KEY_MANAGER_FILE, jsonText);

            saveSettings();
        }

        /// <summary>
        /// キーリストをプリファレンスから復元し、取得する
        /// </summary>
        /// <returns></returns>
        private void getKeyList()
        {
            //jsonのテキスト
            string jsonText = getString(KEY_MANAGER_FILE, "");

            //キーリストの取得
            this.keyList = JsonConvert.DeserializeObject<List<string>>(jsonText);

            //キーリストがNullなら新規作成
            if (this.keyList == null)
            {
                this.keyList = new List<string>();
                saveSettings();
            }
        }
    }
}
