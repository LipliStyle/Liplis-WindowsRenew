//=======================================================================
//  ClassName : LpsPathController
//  概要      : パスコントローラー
//
//  Liplis5.0
//
//  Copyright(c) 2010-2016 LipliStyle.Sachin
//=======================================================================
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Windows.Forms;

namespace Liplis.Utl
{
    public class LpsPathController
    {
        ///=====================================
        /// キャッシュファイルパス
        private string chacheFilePath = "";

        /// <summary>
        /// コンストラクター
        /// </summary>
        #region LpsPathController
        public LpsPathController(string cacheFilePath)
        {

        }
        #endregion

        /// <summary>
        /// セッティングパスを返す
        /// </summary>
        /// <returns>パス</returns>
        #region getSettingFilePath
        public static string getSettingFilePath()
        {
            LpsLogController lc = new LpsLogController();
            try
            {
                checkDir(getAppPath() + "\\setting");
                return getAppPath() + "\\setting\\setting.xml";
            }
            catch (System.Exception err)
            {
                LpsLogController.writingLog("LpsPathController", MethodBase.GetCurrentMethod().Name, err.ToString());
                return "";
            }
        }
        #endregion

        /// <summary>
        /// セッティングパスを返す
        /// </summary>
        /// <returns>パス</returns>
        #region getSettingPath
        public static string getSettingPath()
        {
            LpsLogController lc = new LpsLogController();
            try
            {
                checkDir(getAppPath() + "\\setting");
                return getAppPath() + "\\setting\\";
            }
            catch (System.Exception err)
            {
                LpsLogController.writingLog("LpsPathController", MethodBase.GetCurrentMethod().Name, err.ToString());
                return "";
            }
        }
        #endregion

        /// <summary>
        /// skinパスを返す
        /// </summary>
        /// <returns>パス</returns> 
        #region getSkinPath
        public static string getSkinPath()
        {
            LpsLogController lc = new LpsLogController();
            try
            {
                checkDir(getAppPath() + "\\Skin");
                return getAppPath() + "\\Skin\\";
            }
            catch (System.Exception err)
            {
                LpsLogController.writingLog("LpsPathController", MethodBase.GetCurrentMethod().Name, err.ToString());
                return "";
            }
        }
        #endregion

        /// <summary>
        /// RSSパスを返す
        /// </summary>
        /// <returns>パス</returns> 
        #region getRssSettingPath
        public static string getRssSettingPath()
        {
            LpsLogController lc = new LpsLogController();
            try
            {
                checkDir(getAppPath() + "\\setting");
                return getAppPath() + "\\setting\\rss.lps";
            }
            catch (System.Exception err)
            {
                LpsLogController.writingLog("LpsPathController", MethodBase.GetCurrentMethod().Name, err.ToString());
                return "";
            }
        }
        #endregion

        /// <summary>
        /// RSSパスを返す
        /// </summary>
        /// <returns>パス</returns> 
        #region getCatSettingPath
        public static string getCatSettingPath()
        {
            LpsLogController lc = new LpsLogController();
            try
            {
                checkDir(getAppPath() + "\\setting");
                return getAppPath() + "\\setting\\cat.lps";
            }
            catch (System.Exception err)
            {
                LpsLogController.writingLog("LpsPathController", MethodBase.GetCurrentMethod().Name, err.ToString());
                return "";
            }
        }
        #endregion

        /// <summary>
        /// LCDパスを返す
        /// </summary>
        /// <returns>パス</returns> 
        #region getLcdSettingPath
        public static string getLcdSettingPath()
        {
            LpsLogController lc = new LpsLogController();
            try
            {
                checkDir(getAppPath() + "\\setting");
                return getAppPath() + "\\setting\\lcd.lps";
            }
            catch (System.Exception err)
            {
                LpsLogController.writingLog("LpsPathController", MethodBase.GetCurrentMethod().Name, err.ToString());
                return "";
            }
        }
        #endregion

        /// <summary>
        /// ウィジェットパスを返す
        /// </summary>
        /// <returns>パス</returns> 
        #region getWidgetSettingPath
        public static string getWidgetSettingPath()
        {
            LpsLogController lc = new LpsLogController();
            try
            {
                checkDir(getAppPath() + "\\setting");
                return getAppPath() + "\\setting\\widget.lps";
            }
            catch (System.Exception err)
            {
                LpsLogController.writingLog("LpsPathController", MethodBase.GetCurrentMethod().Name, err.ToString());
                return "";
            }
        }
        #endregion

        /// <summary>
        /// RSSリーダー設定パスを返す
        /// </summary>
        /// <returns>パス</returns> 
        #region getRssReaderSettingPath
        public static string getRssReaderSettingPath()
        {
            LpsLogController lc = new LpsLogController();
            try
            {
                checkDir(getAppPath() + "\\setting");
                return getAppPath() + "\\setting\\rssReader.lps";
            }
            catch (System.Exception err)
            {
                LpsLogController.writingLog("LpsPathController", MethodBase.GetCurrentMethod().Name, err.ToString());
                return "";
            }
        }
        #endregion

        /// <summary>
        /// ウィジェットリストパスを返す
        /// </summary>
        /// <returns>パス</returns> 
        #region getWidgetListPath
        public static string getWidgetListPath()
        {
            LpsLogController lc = new LpsLogController();
            try
            {
                checkDir(getAppPath() + "\\setting");
                return getAppPath() + "\\setting\\widgetList.lps";
            }
            catch (System.Exception err)
            {
                LpsLogController.writingLog("LpsPathController", MethodBase.GetCurrentMethod().Name, err.ToString());
                return "";
            }
        }
        #endregion

        /// <summary>
        /// skinBodyパスを返す
        /// </summary>
        /// <returns>パス</returns> 
        #region getBodyDefinePath
        public static string getBodyDefinePath(string loadSkin)
        {
            LpsLogController lc = new LpsLogController();
            try
            {
                checkDir(getAppPath() + "\\Skin");
                return getAppPath() + "\\Skin\\" + loadSkin + "\\define\\body.xml";
            }
            catch (System.Exception err)
            {
                LpsLogController.writingLog("LpsPathController", MethodBase.GetCurrentMethod().Name, err.ToString());
                return "";
            }
        }
        #endregion

        /// <summary>
        /// chatパスを返す
        /// </summary>
        /// <returns>パス</returns> 
        #region getChatDefinePath
        public static string getChatDefinePath(string loadSkin)
        {
            LpsLogController lc = new LpsLogController();
            try
            {
                checkDir(getAppPath() + "\\Skin");
                return getAppPath() + "\\Skin\\" + loadSkin + "\\define\\chat.xml";
            }
            catch (System.Exception err)
            {
                LpsLogController.writingLog("LpsPathController", MethodBase.GetCurrentMethod().Name, err.ToString());
                return "";
            }
        }
        #endregion

        /// <summary>
        /// touchパスを返す
        /// Liplis4.0.2 
        /// </summary>
        /// <returns>パス</returns> 
        #region getTouchDefinePath
        public static string getTouchDefinePath(string loadSkin)
        {
            LpsLogController lc = new LpsLogController();
            try
            {
                checkDir(getAppPath() + "\\Skin");
                return getAppPath() + "\\Skin\\" + loadSkin + "\\define\\touch.xml";
            }
            catch (System.Exception err)
            {
                LpsLogController.writingLog("LpsPathController", MethodBase.GetCurrentMethod().Name, err.ToString());
                return "";
            }
        }
        #endregion

        /// <summary>
        /// skinToneパスを返す
        /// </summary>
        /// <returns>パス</returns> 
        #region getToneDefinePath
        public static string getToneDefinePath(string loadSkin)
        {
            LpsLogController lc = new LpsLogController();
            try
            {
                checkDir(getAppPath() + "\\Skin");
                return getAppPath() + "\\Skin\\" + loadSkin + "\\define\\tone.xml";
            }
            catch (System.Exception err)
            {
                LpsLogController.writingLog("LpsPathController", MethodBase.GetCurrentMethod().Name, err.ToString());
                return "";
            }
        }
        #endregion

        /// <summary>
        /// skin/windowパスを返す
        /// </summary>
        /// <returns>パス</returns> 
        #region getWindowPath
        public static string getWindowPath(string loadSkin)
        {
            LpsLogController lc = new LpsLogController();
            try
            {
                checkDir(getAppPath() + "\\Skin");
                return getAppPath() + "\\Skin\\" + loadSkin + "\\window\\";
            }
            catch (System.Exception err)
            {
                LpsLogController.writingLog("LpsPathController", MethodBase.GetCurrentMethod().Name, err.ToString());
                return "";
            }
        }
        #endregion

        /// <summary>
        /// skin/bodyパスを返す
        /// </summary>
        /// <returns>パス</returns> 
        #region getBodyPath
        public static string getBodyPath(string loadSkin)
        {
            LpsLogController lc = new LpsLogController();
            try
            {
                checkDir(getAppPath() + "\\Skin");
                return getAppPath() + "\\Skin\\" + loadSkin + "\\body\\";
            }
            catch (System.Exception err)
            {
                LpsLogController.writingLog("LpsPathController", MethodBase.GetCurrentMethod().Name, err.ToString());
                return "";
            }
        }
        #endregion

        /// <summary>
        /// skinBodyパスを返す
        /// </summary>
        /// <returns>パス</returns> 
        #region getRcmDefinePath
        public static string getRcmDefinePath(string loadSkin)
        {
            LpsLogController lc = new LpsLogController();
            try
            {
                checkDir(getAppPath() + "\\skin");
                return getAppPath() + "\\skin\\" + loadSkin + "\\rcm\\rcm.xml";
            }
            catch (System.Exception err)
            {
                LpsLogController.writingLog("LpsPathController", MethodBase.GetCurrentMethod().Name, err.ToString());
                return "";
            }
        }
        #endregion

        /// <summary>
        /// xmlファイルパスを返す
        /// </summary>
        /// <returns>パス</returns>
        #region getXmlPath
        public static string getXmlPath()
        {
            LpsLogController lc = new LpsLogController();
            try
            {
                checkDir(getAppPath() + "\\xml");
                return getAppPath() + "\\xml\\";
            }
            catch (System.Exception err)
            {
                LpsLogController.writingLog("LpsPathController", MethodBase.GetCurrentMethod().Name, err.ToString());
                return "";
            }
        }
        #endregion

        /// <summary>
        /// tempファイルパスを返す
        /// </summary>
        /// <returns>パス</returns> 
        #region getTempPath
        public static string getTempPath()
        {
            try
            {
                checkDir(getAppPath() + "\\temp");
                return getAppPath() + "\\temp\\";
            }
            catch
            {
                return "";
            }
        }
        #endregion

        /// <summary>
        /// tempファイルパスを返す
        /// </summary>
        /// <returns>パス</returns> 
        #region getTempPath2
        public static string getTempPath2()
        {
            try
            {
                checkDir(getAppPath() + "\\temp");
                return getAppPath() + "\\temp";
            }
            catch
            {
                return "";
            }
        }
        #endregion

        /// <summary>
        /// javascriptファイルパスを返す
        /// </summary>
        /// <returns>パス</returns>
        #region getJavascriptPath
        public string getJavascriptPath()
        {
            try
            {
                checkDir(getAppPath() + "\\js");
                return getAppPath() + "\\js\\";
            }
            catch (System.Exception err)
            {
                LpsLogController.writingLog("LpsPathController", MethodBase.GetCurrentMethod().Name, err.ToString());
                return "";
            }
        }
        #endregion

        /// <summary>
        /// ログファイルパスを返す
        /// </summary>
        /// <returns>パス</returns> 
        #region getLogPath
        public static string getLogPath()
        {
            try
            {
                checkDir(getAppPath() + "\\log");
                return getAppPath() + "\\log\\liplis.log";
            }
            catch (System.Exception err)
            {
                LpsLogController.writingLog("LpsPathController", MethodBase.GetCurrentMethod().Name, err.ToString());
                return "";
            }
        }
        #endregion

        /// <summary>
        /// dbファイルパスを返す
        /// </summary>
        /// <returns>パス</returns> 
        #region getDbPath
        public string getDbPath()
        {
            try
            {
                checkDir(chacheFilePath + "\\db");
                return chacheFilePath + "\\db\\Liplis.db";
            }
            catch (System.Exception err)
            {
                LpsLogController.writingLog("LpsPathController", MethodBase.GetCurrentMethod().Name, err.ToString());
                return "";
            }
        }
        public string getDbDirPath()
        {
            try
            {
                checkDir(chacheFilePath + "\\db");
                return chacheFilePath + "\\db";
            }
            catch (System.Exception err)
            {
                LpsLogController.writingLog("LpsPathController", MethodBase.GetCurrentMethod().Name, err.ToString());
                return "";
            }
        }
        #endregion

        /// <summary>
        /// 和布蕪のdicパスを返す
        /// </summary>
        /// <returns>パス</returns> 
        #region getDicPath
        public static string getDicPath()
        {
            LpsLogController lc = new LpsLogController();
            try
            {
                checkDir(getAppPath() + "\\bin\\dic");
                return getAppPath() + "\\bin\\dic\\";
            }
            catch (System.Exception err)
            {
                LpsLogController.writingLog("LpsPathController", MethodBase.GetCurrentMethod().Name, err.ToString());
                return "";
            }
        }
        #endregion 

        /// <summary>
        /// binパスを返す
        /// </summary>
        /// <returns>パス</returns>  
        #region getBinPath
        public static string getBinPath()
        {
            LpsLogController lc = new LpsLogController();
            try
            {
                checkDir(getAppPath() + "\\bin");
                return getAppPath() + "\\bin\\";
            }
            catch (System.Exception err)
            {
                LpsLogController.writingLog("LpsPathController", MethodBase.GetCurrentMethod().Name, err.ToString());
                return "";
            }
        }
        #endregion

        /// <summary>
        /// エモーションDBのパスを返す
        /// </summary>
        /// <returns>パス</returns>  
        #region getDownPath
        public static string getDownPath()
        {
            LpsLogController lc = new LpsLogController();
            try
            {
                checkDir(getAppPath() + "\\down");
                return getAppPath() + "\\down\\";
            }
            catch (System.Exception err)
            {
                LpsLogController.writingLog("LpsPathController", MethodBase.GetCurrentMethod().Name, err.ToString());
                return "";
            }
        }
        #endregion

        /// <summary>
        /// versionパスを返す
        /// </summary>
        /// <returns>パス</returns> 
        #region getParentVersionPath
        public static string getParentVersionPath(string loadSkin)
        {
            LpsLogController lc = new LpsLogController();
            try
            {
                checkDir(getParentPath() + "\\skin");
                return getParentPath() + "\\skin\\" + loadSkin + "\\define\\version.xml";
            }
            catch (System.Exception err)
            {
                LpsLogController.writingLog("LpsPathController", MethodBase.GetCurrentMethod().Name, err.ToString());
                return "";
            }
        }
        #endregion

        /// <summary>
        /// アップデートプログラムの起動場所を返す
        /// </summary>
        /// <returns></returns>
        #region getUpdatePrgPath
        public static string getUpdatePrgPath()
        {
            return getAppPath() + "\\LiplisUpdater.exe";
        }
        #endregion

        /// <summary>
        /// アプリケーションの起動パスを返す
        /// </summary>
        /// <returns>パス</returns>
        #region getAppPath
        public static string getAppPath()
        {
            return Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        }
        #endregion

        /// <summary>
        /// 親パスを返す
        /// </summary>
        /// <returns>パス</returns>
        #region getParentPath
        public static string getParentPath()
        {
            return Directory.GetParent(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)).ToString();
        }
        #endregion

        /// <summary>
        /// ディレクトリの存在チェックを行い、無かったら生成する
        /// </summary>
        /// <param name="path">ターゲットディレクトリパス</param>
        /// <returns>ディレクトリの有無</returns>
        #region checkDir
        public static bool checkDir(string path)
        {
            try
            {
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                    return true;
                }
                else
                {
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }
        #endregion

        /// <summary>
        /// ファイル存在チェック
        /// </summary>
        /// <param name="path">対象ファイルパス</param>
        /// <returns>ファイルの有無</returns>
        #region checkFileExist
        public static bool checkFileExist(string path)
        {
            return System.IO.File.Exists(path);
        }
        #endregion

        /// <summary>
        /// ファイルサイズを返す。
        /// </summary>
        /// <param name="filePath">ファイルパス</param>
        /// <returns>ファイルサイズ</returns>
        #region getFileSize
        public static long getFileSize(string filePath)
        {
            if (checkFileExist(filePath))
            {
                FileInfo fi = new System.IO.FileInfo(filePath);
                return fi.Length;
            }
            else
            {
                return 0;
            }
        }
        #endregion

        /// <summary>
        /// ファイルをリネームする
        /// </summary>
        /// <param name="filePath">ファイルパス</param>
        /// <returns>ファイルサイズ</returns>
        #region reNameFile
        public static void reNameFile(string dirPath, string oldName, string newName)
        {
            try
            {
                File.Move(dirPath + oldName, dirPath + newName);
            }
            catch
            {

            }
        }
        #endregion

        /// <summary>
        /// ファイルを消去する
        /// </summary>
        /// <param name="filePath">削除ファイルパス</param>
        /// <returns></returns>
        #region delteFile
        public static bool delteFile(string filePath)
        {
            if (checkFileExist(filePath))
            {
                try
                {
                    FileInfo fi = new System.IO.FileInfo(filePath);
                    fi.Delete();
                }
                catch
                {
                    LpsLogController lc = new LpsLogController();
                    LpsLogController.writingLog("LpsPathController", MethodBase.GetCurrentMethod().Name, "ファイルの削除に失敗しました。");
                }
                return true;
            }
            else
            {
                return false;
            }
        }
        #endregion

        /// <summary>
        /// ディレクトリ作成を試してみる
        /// </summary>
        /// <param name="path">ターゲットパス</param>
        /// <returns>作成成功可否</returns>
        #region tryCreateDir
        public static bool tryCreateDir(string path)
        {
            try
            {
                System.IO.Directory.CreateDirectory(path);
                return true;
            }
            catch
            {
                return false;
            }
        }
        #endregion

        /// <summary>
        /// ファイルリストを取得する
        /// </summary>
        /// <param name="folder"></param>
        /// <param name="searchPattern"></param>
        /// <param name="result"></param>
        #region getFileList
        public static void getFileList(string folder, string searchPattern, ref List<string> result)
        {
            //folderにあるファイルを取得する
            string[] fs = Directory.GetFiles(folder, searchPattern);
            //ArrayListに追加する
            result.AddRange(fs);

            //folderのサブフォルダを取得する
            string[] ds = Directory.GetDirectories(folder);
            //サブフォルダにあるファイルも調べる
            foreach (string d in ds)
            {
                System.Windows.Forms.Application.DoEvents();
                getFileList(d, searchPattern, ref result);
            }

        }
        #endregion

        /// <summary>
        /// 古いテンプファイルを削除する
        /// 設定時間以上アクセスの無いファイルを削除する
        /// </summary>
        /// <param name="tempDirPath">テンプダウンロードパス</param>
        /// <param name="dt">対象の削除日付</param>
        #region deleteOldTempFile
        public static void deleteOldTempFile(string tempDirPath, DateTime dt)
        {
            //ファイルリスト
            List<string> tempFileList = new List<string>();
            //テンプファイルリストを取得
            getFileList(tempDirPath, "*", ref tempFileList);

            try
            {
                //作成時刻と照らし合わせ、基準以上であれば削除する
                foreach (string path in tempFileList)
                {
                    System.Windows.Forms.Application.DoEvents();
                    FileInfo fi = new System.IO.FileInfo(path);

                    //ファイルの作成日が基準以前であれば削除する
                    if (fi.CreationTime < dt)
                    {
                        fi.Delete();
                    }
                }
            }
            catch
            {
                LpsLogController lc = new LpsLogController();
                LpsLogController.writingLog("LpsPathController", MethodBase.GetCurrentMethod().Name, "一部ファイルの削除に失敗しました。");
            }
        }
        #endregion

        /// <summary>
        /// ファイル名を取得する
        /// </summary>
        #region getSaveFileName
        public static string getSaveFileName(string extension, string downPath, string title, int downNotice)
        {
            //保存場所を聞く
            if (downNotice == 1)
            {
                return getInputFileNameDlg(extension);
            }
            else
            {
                return downPath + "\\" + title;
            }
        }
        #endregion

        /// <summary>
        /// ファイルネーム指定ダイアログ
        /// </summary>
        #region getInputFileNameDlg
        public static string getInputFileNameDlg(string extension)
        {
            string fileName;
            SaveFileDialog dialog = new SaveFileDialog();
            dialog.Title = "ファイルの保存";
            dialog.Filter = extension + " files (*." + extension + ")|*." + extension + "";
            dialog.RestoreDirectory = true;
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                fileName = dialog.FileName;
            }
            else
            {
                //ユーザーキャンセル
                fileName = "";
            }

            return fileName;
        }
        #endregion

        /// <summary>
        /// getDirPath
        /// ファイル名からディレクトリ名を取得する
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        #region getDirPath
        public static string getDirPath(string fileName)
        {
            return System.IO.Path.GetDirectoryName(fileName);
        }
        #endregion

    }
}
