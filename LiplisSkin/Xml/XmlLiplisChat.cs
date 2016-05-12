//=======================================================================
//  ClassName : XmlBody
//  概要      : body.xmlの実体
//              body.xmlを読みこませることでインスタンス化、立ち絵をプログラムで使えるようにする。
//
//  Liplis5.0
//
//  Copyright(c) 2010-2016 LipliStyle.Sachin
//=======================================================================
using Liplis.Exp;
using Liplis.Lst;
using Liplis.Msg;
using Liplis.Utl;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Xml;

namespace Liplis.Xml
{
    public class XmlLiplisChat : XmlReadList
    {
        ///==========================
        /// 内容
        public List<string> nameList { get; set; }
        public List<string> typeList { get; set; }
        public List<string> discriptionList { get; set; }
        public List<int> emotionList { get; set; }
        public List<string> prerewuisteList { get; set; }

        ///==========================
        /// フラグ
        public bool checkFlg { get; set; }

        ///===========================================
        /// スキンファイル読み込み完了フラグ
        public bool loadDefault;


        ///=============================
        /// chatXpath定義
        #region chatXpath定義
        public const string CHAT_NAME = "/chat/chatDiscription/name";
        public const string CHAT_TYPE = "/chat/chatDiscription/type";
        public const string CHAT_DISCRIPTION = "/chat/chatDiscription/discription";
        public const string CHAT_EMOTION = "/chat/chatDiscription/emotion";
        public const string CHAT_PREREWUISITE = "/chat/chatDiscription/prerequisite";
        #endregion



        /// <summary>
        /// RssListControllerコンストラクタ
        /// 設定ファイルを読み込む
        /// </summary>
        #region XmlChat
        public XmlLiplisChat(string loadSkin)
        {
            try
            {
                xmlDoc = new XmlDocument();
                initList();

                //キャッシュファイルの取得
                xmlFilePath = LpsPathController.getChatDefinePath(loadSkin);

                //body.xmlの存在チェック
                if (LpsPathController.checkFileExist(xmlFilePath))
                {
                    //xmlの読み込み
                    readXml();
                    readResult();
                }
                else
                {
                    //読み込みに失敗した場合はエラー
                    throw new SkinNotFoundException();
                }

            }
            catch (System.Exception err)
            {
                //読み込みに失敗した場合はエラー
                throw new SkinNotFoundException(err);
            }
        }
        #endregion

        /// <summary>
        /// リストの初期化
        /// </summary>
        #region initList
        protected void initList()
        {
            nameList = new List<string>();
            typeList = new List<string>();
            discriptionList = new List<string>();
            emotionList = new List<int>();
            prerewuisteList = new List<string>();
        }
        #endregion

        /// <summary>
        /// readResult
        /// 設定読込
        /// </summary>
        #region readResult
        public void readResult()
        {
            readXmlList(xmlDoc.SelectNodes(CHAT_NAME), nameList);
            readXmlList(xmlDoc.SelectNodes(CHAT_TYPE), typeList);
            readXmlList(xmlDoc.SelectNodes(CHAT_DISCRIPTION), discriptionList);
            readXmlListInt(xmlDoc.SelectNodes(CHAT_EMOTION), emotionList);
            readXmlList(xmlDoc.SelectNodes(CHAT_PREREWUISITE), prerewuisteList);
        }
        #endregion

        /// <summary>
        /// 挨拶メッセージを返す。
        /// タイプはLiplisDefineで指定しているタイプとする
        /// (チャット定義もそれに従うこととする)
        /// </summary>
        #region getGreetMessage
        public MsgTalkMessage getGreet(string type)
        {
            return getMacheGreet(getTargetTypeList(type));
        }
        #endregion

        /// <summary>
        /// getTargetTypelist
        /// 対象のタイプのインデックスリストを返す
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        #region getTargetTypeList
        private LstShufflableList<int> getTargetTypeList(string type)
        {
            int idx = 0;
            LstShufflableList<int> result = new LstShufflableList<int>();
            try
            {
                //登録されている定型文から、nameが一致するものを探す
                foreach (string n in typeList)
                {
                    if (n.Equals(type))
                    {
                        result.Add(idx);
                    }
                    idx++;
                }

                //シャッフルした結果から0番目のものを返す。
                return result;
            }
            catch
            {
                return new LstShufflableList<int>();
            }
        }
        #endregion

        /// <summary>
        /// getChatWord
        /// 対象のタイプのセリフを1つランダムに返す
        /// </summary>
        #region getChatWord
        public string getChatWordStr(string pType)
        {
            string result = "";
            int idx = 0;

            //対象インデックスリスト
            List<int> resList = new List<int>();

            //時間に合致する挨拶を検索
            foreach (string type in typeList)
            {
                //挨拶なら対象
                if (type.Equals(pType))
                {
                    resList.Add(idx);
                }
                idx++;
            }

            //候補が置ければ1個目を取得
            if (resList.Count > 0)
            {
                if (nameList.Count > 0)
                {
                    Random rnd = new Random();



                    int ran = rnd.Next(resList.Count);
                    int tarIdx = resList[ran];

                    try
                    {
                        result = discriptionList[tarIdx];
                    }
                    catch (Exception)
                    {
                        result = "";
                    }
                }
                else
                {
                    result = "";
                }
            }
            else
            {
                result = "";
            }

            return result;
        }
        public MsgTalkMessage getChatWord(string pType)
        {
            MsgTalkMessage result = new MsgTalkMessage();
            int idx = 0;

            //対象インデックスリスト
            List<int> resList = new List<int>();

            //時間に合致する挨拶を検索
            foreach (string type in typeList)
            {
                //挨拶なら対象
                if (type.Equals(pType))
                {
                    resList.Add(idx);
                }
                idx++;
            }

            //候補が置ければ1個目を取得
            if (resList.Count > 0)
            {
                if (nameList.Count > 0)
                {
                    Random rnd = new Random();

                    int ran = rnd.Next(resList.Count);
                    int tarIdx = resList[ran];

                    try
                    {
                        result = new MsgTalkMessage(discriptionList[tarIdx], emotionList[tarIdx], 99);
                    }
                    catch (Exception)
                    {
                        result = new MsgTalkMessage("", 0, 0);
                    }
                }
                else
                {
                    result = new MsgTalkMessage("", 0, 0);
                }
            }

            return result;
        }
        public MsgTalkMessage getChatWord(string pType, string name)
        {
            MsgTalkMessage result = new MsgTalkMessage();
            int idx = 0;

            //対象インデックスリスト
            List<int> resList = new List<int>();

            //時間に合致する挨拶を検索
            foreach (string type in typeList)
            {
                //挨拶なら対象
                if (type.Equals(pType) && nameList[idx].Equals(name))
                {
                    resList.Add(idx);
                }
                idx++;
            }

            //候補が置ければ1個目を取得
            if (resList.Count > 0)
            {
                if (nameList.Count > 0)
                {
                    Random rnd = new Random();

                    int ran = rnd.Next(resList.Count);
                    int tarIdx = resList[ran];

                    try
                    {
                        result = new MsgTalkMessage(discriptionList[tarIdx], emotionList[tarIdx], emotionList[tarIdx]);
                    }
                    catch (Exception)
                    {
                        result = new MsgTalkMessage("", 0, 0);
                    }
                }
                else
                {
                    result = new MsgTalkMessage("", 0, 0);
                }
            }

            return result;
        }
        #endregion

        /// <summary>
        /// getMacheGreet
        /// 現在時刻にマッチする挨拶を取得する
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        #region getMacheGreet
        private MsgTalkMessage getMacheGreet(LstShufflableList<int> idxList)
        {
            MsgTalkMessage result = new MsgTalkMessage();
            LstShufflableList<int> resList = new LstShufflableList<int>();
            string[] timeList;
            string[] startList;
            string[] endList;

            int nowHour = 0;
            int nowMin = 0;
            int startHour = 0;
            int startMin = 0;
            int endHour = 0;
            int endMin = 0;

            try
            {
                //インデックスリストを回してチェック
                foreach (int idx in idxList)
                {
                    Application.DoEvents();
                    try
                    {
                        if (!prerewuisteList[idx].Equals(""))
                        {
                            timeList = prerewuisteList[idx].Split(',');

                            startList = timeList[0].Split(':');
                            endList = timeList[1].Split(':');


                            if (startList.Length == 2 && endList.Length == 2)
                            {
                                DateTime cal1 = DateTime.Now;

                                nowHour = cal1.Hour;
                                nowMin = cal1.Minute;

                                startHour = int.Parse(startList[0]);
                                startMin = int.Parse(startList[1]);

                                endHour = int.Parse(endList[0]);
                                endMin = int.Parse(endList[1]);

                                //スタートアワーの場合、分を確認
                                if (nowHour == startHour && nowMin >= startMin)
                                {
                                    if (nowHour == endHour && nowMin <= endMin)
                                    {
                                        resList.Add(idx);
                                    }
                                    else if (nowHour < endHour)
                                    {
                                        resList.Add(idx);
                                    }
                                }
                                else if (nowHour >= startHour)
                                {
                                    if (nowHour == endHour && nowMin <= endMin)
                                    {
                                        resList.Add(idx);
                                    }
                                    else if (nowHour < endHour)
                                    {
                                        resList.Add(idx);
                                    }
                                }
                            }
                        }
                    }
                    catch
                    {
                        continue;
                    }

                }

                //取得したけっかから一つ選んで返す
                if (resList.Count > 0)
                {
                    if (nameList.Count > 0)
                    {
                        resList.Shuffle();
                        int tarIdx = resList[0];

                        try
                        {
                            result = new MsgTalkMessage(discriptionList[tarIdx] + " ", emotionList[tarIdx], emotionList[tarIdx]);
                        }
                        catch
                        {
                            result = new MsgTalkMessage("", 0, 0);
                        }
                    }
                    else
                    {
                        result = new MsgTalkMessage("", 0, 0);
                    }
                }

                //取得グリートメッセージのあっとマークを改行に変換しておく
                result.result = result.result.Replace("@", Environment.NewLine);

                return result;
            }
            catch
            {
                return new MsgTalkMessage("", 0, 0);
            }
        }
        #endregion

        /// <summary>
        /// saveSettings
        /// リードオンリー
        /// </summary>
        #region saveSettings
        public virtual void saveSettings()
        {
        }
        #endregion

        /// <summary>
        /// getBatteryInfo
        /// バッテリー情報を取得する
        /// </summary>
        #region getBatteryInfo
        public MsgTalkMessage getBatteryInfo(int batteryLevel, bool batteryExists)
        {

            MsgTalkMessage result;
            MsgTalkMessage batteryWord;
            String resStr = "";

            try
            {
                if (!batteryExists)
                {
                    //メッセージ作成
                    batteryWord = getChatWord("batteryNotFound");
                    return new MsgTalkMessage(batteryWord.nameList[0], batteryWord.emotionList[0], batteryWord.pointList[0]);
                }

                //電池容量のセリフを取得
                resStr = getChatWordStr("batteryInfo");

                //空だったら、電池格納用ワードを入れておく
                if (resStr.Equals(""))
                {
                    resStr = "[?]%";
                }

                //バッテリーレベルによってセリフを変える
                if (batteryLevel > 70)
                {
                    batteryWord = getChatWord("batteryHi");
                }
                else if (batteryLevel > 30)
                {
                    batteryWord = getChatWord("batteryMid");
                }
                else if (batteryLevel > 0)
                {
                    batteryWord = getChatWord("batteryLow");
                }
                else
                {
                    batteryWord = new MsgTalkMessage();
                }

                //メッセージ作成
                resStr = resStr + batteryWord.nameList[0];
                resStr = resStr.Replace("[?]", batteryLevel.ToString());
                result = new MsgTalkMessage(resStr, batteryWord.emotionList[0], batteryWord.pointList[0]);

                return result;
            }
            catch (Exception)
            {
                return new MsgTalkMessage("[?]%", 1, 1);
            }
        }
        #endregion

        /// <summary>
        /// 時刻情報を取得する
        /// </summary>
        /// <returns></returns>
        public MsgTalkMessage getClockInfo()
        {
            string resStr = "";
            DateTime nowTime = DateTime.Now;

            //時刻情報のセリフを取得
            resStr = getChatWordStr("clockInfo");
        
            //空だったら、電池格納用ワードを入れておく
            if(resStr == "")
            {
                resStr = nowTime.ToString("yyyy/MM/dd HH:mm:ss");
            }
            else
            {
                resStr = resStr.Replace("[?]", nowTime.ToString("yyyy/MM/dd HH:mm:ss"));
            }

            return new MsgTalkMessage(resStr, 1, 1);
        }

        /// <summary>
        /// getTimeSignal
        /// 時報を取得する
        /// </summary>
        #region getTimeSignal
        public MsgTalkMessage getTimeSignal(int hour)
        {
            MsgTalkMessage result = new MsgTalkMessage();
            MsgTalkMessage buf;

            try
            {
                switch (hour)
                {
                    case 1: buf = getChatWord("1Oclock"); return new MsgTalkMessage(buf.nameList[0], buf.emotionList[0], buf.pointList[0]);
                    case 2: buf = getChatWord("2Oclock"); return new MsgTalkMessage(buf.nameList[0], buf.emotionList[0], buf.pointList[0]);
                    case 3: buf = getChatWord("3Oclock"); return new MsgTalkMessage(buf.nameList[0], buf.emotionList[0], buf.pointList[0]);
                    case 4: buf = getChatWord("4Oclock"); return new MsgTalkMessage(buf.nameList[0], buf.emotionList[0], buf.pointList[0]);
                    case 5: buf = getChatWord("5Oclock"); return new MsgTalkMessage(buf.nameList[0], buf.emotionList[0], buf.pointList[0]);
                    case 6: buf = getChatWord("6Oclock"); return new MsgTalkMessage(buf.nameList[0], buf.emotionList[0], buf.pointList[0]);
                    case 7: buf = getChatWord("7Oclock"); return new MsgTalkMessage(buf.nameList[0], buf.emotionList[0], buf.pointList[0]);
                    case 8: buf = getChatWord("8Oclock"); return new MsgTalkMessage(buf.nameList[0], buf.emotionList[0], buf.pointList[0]);
                    case 9: buf = getChatWord("9Oclock"); return new MsgTalkMessage(buf.nameList[0], buf.emotionList[0], buf.pointList[0]);
                    case 10: buf = getChatWord("10Oclock"); return new MsgTalkMessage(buf.nameList[0], buf.emotionList[0], buf.pointList[0]);
                    case 11: buf = getChatWord("11Oclock"); return new MsgTalkMessage(buf.nameList[0], buf.emotionList[0], buf.pointList[0]);
                    case 12: buf = getChatWord("12Oclock"); return new MsgTalkMessage(buf.nameList[0], buf.emotionList[0], buf.pointList[0]);
                    case 13: buf = getChatWord("13Oclock"); return new MsgTalkMessage(buf.nameList[0], buf.emotionList[0], buf.pointList[0]);
                    case 14: buf = getChatWord("14Oclock"); return new MsgTalkMessage(buf.nameList[0], buf.emotionList[0], buf.pointList[0]);
                    case 15: buf = getChatWord("15Oclock"); return new MsgTalkMessage(buf.nameList[0], buf.emotionList[0], buf.pointList[0]);
                    case 16: buf = getChatWord("16Oclock"); return new MsgTalkMessage(buf.nameList[0], buf.emotionList[0], buf.pointList[0]);
                    case 17: buf = getChatWord("17Oclock"); return new MsgTalkMessage(buf.nameList[0], buf.emotionList[0], buf.pointList[0]);
                    case 18: buf = getChatWord("18Oclock"); return new MsgTalkMessage(buf.nameList[0], buf.emotionList[0], buf.pointList[0]);
                    case 19: buf = getChatWord("19Oclock"); return new MsgTalkMessage(buf.nameList[0], buf.emotionList[0], buf.pointList[0]);
                    case 20: buf = getChatWord("20Oclock"); return new MsgTalkMessage(buf.nameList[0], buf.emotionList[0], buf.pointList[0]);
                    case 21: buf = getChatWord("21Oclock"); return new MsgTalkMessage(buf.nameList[0], buf.emotionList[0], buf.pointList[0]);
                    case 22: buf = getChatWord("22Oclock"); return new MsgTalkMessage(buf.nameList[0], buf.emotionList[0], buf.pointList[0]);
                    case 23: buf = getChatWord("23Oclock"); return new MsgTalkMessage(buf.nameList[0], buf.emotionList[0], buf.pointList[0]);
                    case 0: buf = getChatWord("24Oclock"); return new MsgTalkMessage(buf.nameList[0], buf.emotionList[0], buf.pointList[0]);

                    default:
                        return result;
                }
            }
            catch (Exception)
            {
                return new MsgTalkMessage("[?]%", 1, 1);
            }
        }
        #endregion


    }
}
