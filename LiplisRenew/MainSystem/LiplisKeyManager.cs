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

using Liplis.Com;
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

            //キーの妥当性確認
            this.keyListCheck();
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
                //キーリストから削除
                keyList.Remove(pKey);

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
        /// 指定されたリストのキーを削除する
        /// </summary>
        /// <param name="pKey"></param>
        public void delKey(List<string> keyList)
        {
            foreach (var pKey in keyList)
            {
                if (keyList.Contains(pKey))
                {
                    keyList.Remove(pKey);
                }
            }

            this.saveKeyList();
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

        /// <summary>
        /// ロードしたキーの妥当性を確認する。
        /// スキンまたは保存ファイルが存在しない場合は、キーリストから削除する。
        /// </summary>
        private void keyListCheck()
        {
            List<string> fileList = new List<string>();
            List<string> delList = new List<string>();

            //ファイル名リストを取得する
            foreach (FileInfo fi in new DirectoryInfo(LpsPathController.getSettingPath()).EnumerateFiles("*", SearchOption.AllDirectories))
            {
                fileList.Add(fi.Name);
            }

            //削除対象リストを探す
            foreach (var key in keyList)
            {
                FileInfo fi = new FileInfo(key);
                if(!fileList.Contains(key))
                {
                    delList.Add(key);
                }
            }

            //削除対象となったキーを削除する
            foreach (var key in delList)
            {
                delKey(key);
            }
        }
    }
}
