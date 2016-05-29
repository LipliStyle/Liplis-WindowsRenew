//=======================================================================
//  ClassName : EnableVoiceRoid
//  概要      : 利用可能ボイスロイドリスト
////
//Liplis5.0
//
//
//  Copyright(c) 2010-2016 LipliStyle.Sachin
//=======================================================================
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace Liplis.Voc
{
    public class EnableVoiceRoid
    {
        //=================================
        //Liplis要素
        public List<string> enableVoiceRoidList { get; set; }

        //============================================================
        //
        //シングルトン
        //
        //============================================================
        #region MyRegion

        //インスタンス
        private static EnableVoiceRoid _singleInstance;

        //ゲッツインスタンス
        public static EnableVoiceRoid GetInstance()
        {
            if(_singleInstance == null)
            {
                _singleInstance = new EnableVoiceRoid();
            }

            return _singleInstance;
        }

        //コンストラクター
        private EnableVoiceRoid()
        {
            initEnableVoiceRoidList();
        }

        /// <summary>
        /// ボイスロイドリストの初期化
        /// </summary>
        public void initEnableVoiceRoidList()
        {
            enableVoiceRoidList = new List<string>();

            enableVoiceRoidList.Add("SofTalk");
            enableVoiceRoidList.Add("VOICEROID＋ 結月ゆかり");
            enableVoiceRoidList.Add("VOICEROID＋ 民安ともえ");
            enableVoiceRoidList.Add("VOICEROID＋ 東北ずん子 EX");
            enableVoiceRoidList.Add("VOICEROID＋ 結月ゆかり EX");
            enableVoiceRoidList.Add("VOICEROID＋ 民安ともえ EX");
            enableVoiceRoidList.Add("VOICEROID＋ 東北ずん子 EX");
            enableVoiceRoidList.Add("VOICEROID＋ 琴葉茜");
            enableVoiceRoidList.Add("VOICEROID＋ 琴葉葵");
        }
        #endregion

        //============================================================
        //
        //パブリックメソッド
        //
        //============================================================
        #region パブリックメソッド
        
        /// <summary>
        /// ボイスロイドチェック
        /// パスに指定されたファイルがLiplisで使用できるボイスロイドかチェックする
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public bool voiceRoidCheck(string path)
        {
            //ファイル情報取得
            FileVersionInfo vi = FileVersionInfo.GetVersionInfo(path);

            //有効ボイスロイドか判定し、結果を返す
            return enableVoiceRoidList.Contains(vi.ProductName);
        }
        
        /// <summary>
        /// 有効ボイスロイドリストを返す(メッセージ表示用)
        /// </summary>
        /// <returns></returns>
        public string getEnableVoiceRoidListStr()
        {
            StringBuilder sb = new StringBuilder();

            //ボイスロイドを改行で連結して返す
            foreach (var item in enableVoiceRoidList)
            {
                sb.AppendLine(item);
            }

            //結果を返す
            return sb.ToString();
        }

        /// <summary>
        /// 指定パスのEXEのボイスロイド名を返す
        /// </summary>
        /// <returns></returns>
        public string voiceRoidName(string path)
        {
            //ファイル情報取得
            FileVersionInfo vi = FileVersionInfo.GetVersionInfo(path);

            //有効ボイスロイドか判定し、結果を返す
            return vi.ProductName;
        }

        #endregion
    }
}
