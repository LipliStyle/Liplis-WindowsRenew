//=======================================================================
//  ClassName : EnableVoiceRoid
//  概要      : 利用可能ボイスロイドリスト
////
//Liplis5.0
//
// 2016/10/29 きりたん追加
//
//
//
//  Copyright(c) 2010-2016 LipliStyle.Sachin
//=======================================================================
using Liplis.Voc.Option;
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

        //=================================
        //有効ボイスロイド名
        public const string VOICEROID_SOFTALK    = "SofTalk";
        public const string VOICEROID_YUKARI     = "VOICEROID＋ 結月ゆかり";
        public const string VOICEROID_TOMOE      = "VOICEROID＋ 民安ともえ";
        public const string VOICEROID_ZUNKO      = "VOICEROID＋ 東北ずん子";
        public const string VOICEROID_YUKARI_EX  = "VOICEROID＋ 結月ゆかり EX";
        public const string VOICEROID_TOMOE_EX   = "VOICEROID＋ 民安ともえ EX";
        public const string VOICEROID_ZUNKO_EX   = "VOICEROID＋ 東北ずん子 EX";
        public const string VOICEROID_AKANE      = "VOICEROID＋ 琴葉茜";
        public const string VOICEROID_AOI        = "VOICEROID＋ 琴葉葵";
        public const string VOICEROID_SEIKA_EX   = "VOICEROID＋ 京町セイカ EX";
        public const string VOICEROID_KIRITAN    = "VOICEROID＋ 東北きりたん";
        public const string VOICEROID_KIRITAN_EX = "VOICEROID＋ 東北きりたん EX";

        //============================================================
        //
        //シングルトン
        //
        //============================================================
        #region インスタンス

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

            enableVoiceRoidList.Add(VOICEROID_SOFTALK);
            enableVoiceRoidList.Add(VOICEROID_YUKARI);
            enableVoiceRoidList.Add(VOICEROID_TOMOE);
            enableVoiceRoidList.Add(VOICEROID_ZUNKO);
            enableVoiceRoidList.Add(VOICEROID_YUKARI_EX);
            enableVoiceRoidList.Add(VOICEROID_TOMOE_EX);
            enableVoiceRoidList.Add(VOICEROID_ZUNKO_EX);
            enableVoiceRoidList.Add(VOICEROID_AKANE);
            enableVoiceRoidList.Add(VOICEROID_AOI);
            enableVoiceRoidList.Add(VOICEROID_SEIKA_EX);
            enableVoiceRoidList.Add(VOICEROID_KIRITAN); 
            enableVoiceRoidList.Add(VOICEROID_KIRITAN_EX);
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

        /// <summary>
        /// 選択されているボイスロイドを取得する
        /// </summary>
        /// <returns></returns>
        public LpsVoiceRoid getSelectedVoiceRoid(string path)
        {
            FileVersionInfo vi;

            //パスが不正の場合失敗するので、保険をかけておく
            try
            {
                //ファイル情報取得
                vi = FileVersionInfo.GetVersionInfo(path);
            }
            catch
            {
                return new LpsVoiceRoid(new msgVoiceRoid("", ""));
            }


            //ボイスロイド名取得
            string voiceRoidName = vi.ProductName;

            //ボイスロイドインスタンス生成
            switch (voiceRoidName)
            {
                case VOICEROID_SOFTALK://ソフトーク
                    return new LpsVoiceRoid150(new msgVoiceRoid(VOICEROID_SOFTALK, path));
                case VOICEROID_YUKARI://結月ゆかり
                    return new LpsVoiceRoid150(new msgVoiceRoid(VOICEROID_YUKARI, path));
                case VOICEROID_TOMOE://民安ともえ
                    return new LpsVoiceRoid150(new msgVoiceRoid(VOICEROID_TOMOE, path));
                case VOICEROID_ZUNKO://東北ずん子
                    return new LpsVoiceRoid(new msgVoiceRoid(VOICEROID_ZUNKO, path));
                case VOICEROID_YUKARI_EX://結月ゆかり EX
                    return new LpsVoiceRoid(new msgVoiceRoid(VOICEROID_YUKARI_EX, path));
                case VOICEROID_TOMOE_EX://民安ともえ EX
                    return new LpsVoiceRoid(new msgVoiceRoid(VOICEROID_TOMOE_EX, path));
                case VOICEROID_ZUNKO_EX://東北ずん子 EX
                    return new LpsVoiceRoid(new msgVoiceRoid(VOICEROID_ZUNKO_EX, path));
                case VOICEROID_AKANE://琴葉茜
                    return new LpsVoiceRoid(new msgVoiceRoid(VOICEROID_AKANE, path));
                case VOICEROID_AOI://琴葉葵
                    return new LpsVoiceRoid(new msgVoiceRoid(VOICEROID_AOI, path));
                case VOICEROID_SEIKA_EX://京町セイカ EX
                    return new LpsVoiceRoid(new msgVoiceRoid(VOICEROID_SEIKA_EX, path));
                case VOICEROID_KIRITAN_EX://東北きりたん EX
                    return new LpsVoiceRoid(new msgVoiceRoid(VOICEROID_KIRITAN_EX, path));
                case VOICEROID_KIRITAN: //東北きりたん どうも、EXE情報を取得した場合は、EXが付かない模様。こちらでもインスタンスを生成できるようにしておく。
                    return new LpsVoiceRoid(new msgVoiceRoid(VOICEROID_KIRITAN_EX, path));
                default:
                    return new LpsVoiceRoid(new msgVoiceRoid("", ""));
            }
        }

        #endregion
    }
}
