//=======================================================================
//  ClassName : UserEgent
//  概要      : ユーザーエージェント
//
//  Tips      :getUserEgant()により、適当にユーザーエージェントを取得することが出来る。
//
//  Liplis5.0
//
//  Copyright(c) 2010-2016 LipliStyle.Sachin
//=======================================================================
using Liplis.Lst;

namespace Liplis.Web
{
    public class UserEgent
    {
        ///インスタンス
        private static UserEgent _singleInstance = new UserEgent();

        public static LstShufflableList<string> userEgentList;

        /// <summary>
        /// インスタンス取得
        /// </summary>
        /// <returns></returns>
        public static UserEgent GetInstance()
        {
            return _singleInstance;
        }

        /// <summary>
        /// コンストラクター
        /// </summary>
        private UserEgent()
        {
            initUserEgantList();
        }

        /// <summary>
        /// リストの初期化
        /// </summary>
        private static void initUserEgantList()
        {
            //ユーザーエージェントリストの初期化
            userEgentList = new LstShufflableList<string>();

            userEgentList.Add("Mozilla/5.0 (Windows NT 10.0) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/46.0.2486.0 Safari/537.36 Edge/13.10586");
            userEgentList.Add("Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/46.0.2486.0 Safari/537.36 Edge/13.10586");
            userEgentList.Add("Mozilla/5.0 (Windows NT 10.0; Trident/7.0; rv:11.0) like Gecko");
            userEgentList.Add("Mozilla/5.0 (Windows NT 10.0; Trident/7.0; Touch; rv:11.0) like Gecko");
            userEgentList.Add("Mozilla/5.0 (Windows NT 10.0; WOW64; Trident/7.0; rv:11.0) like Gecko");
            userEgentList.Add("Mozilla/5.0 (Windows NT 10.0; WOW64; Trident/7.0; Touch; rv:11.0) like Gecko");
            userEgentList.Add("Mozilla/5.0 (Windows NT 6.3; Trident/7.0; rv:11.0) like Gecko");
            userEgentList.Add("Mozilla/5.0 (Windows NT 6.3; Trident/7.0; Touch; rv:11.0) like Gecko");
            userEgentList.Add("Mozilla/5.0 (Windows NT 6.3; WOW64; Trident/7.0; rv:11.0) like Gecko");
            userEgentList.Add("Mozilla/5.0 (Windows NT 6.3; WOW64; Trident/7.0; Touch; rv:11.0) like Gecko");
            userEgentList.Add("Mozilla/5.0 (Windows NT 6.1; Trident/7.0; rv:11.0) like Gecko");
            userEgentList.Add("Mozilla/5.0 (Windows NT 6.1; WOW64; Trident/7.0; rv:11.0) like Gecko");
            userEgentList.Add("Mozilla/5.0 (Windows NT 10.0; rv:45.0) Gecko/20100101 Firefox/45.0");
            userEgentList.Add("Mozilla/5.0 (Windows NT 10.0; WOW64; rv:45.0) Gecko/20100101 Firefox/45.0");
            userEgentList.Add("Mozilla/5.0 (Windows NT 10.0; Win64; x64; rv:45.0) Gecko/20100101 Firefox/45.0");
            userEgentList.Add("Mozilla/5.0 (Windows NT 6.3; rv:45.0) Gecko/20100101 Firefox/45.0");
            userEgentList.Add("Mozilla/5.0 (Windows NT 6.3; WOW64; rv:45.0) Gecko/20100101 Firefox/45.0");
            userEgentList.Add("Mozilla/5.0 (Windows NT 6.3; Win64; x64; rv:45.0) Gecko/20100101 Firefox/45.0");
            userEgentList.Add("Mozilla/5.0 (Windows NT 6.2; rv:45.0) Gecko/20100101 Firefox/45.0");
            userEgentList.Add("Mozilla/5.0 (Windows NT 6.2; WOW64; rv:45.0) Gecko/20100101 Firefox/45.0");
            userEgentList.Add("Mozilla/5.0 (Windows NT 6.2; Win64; x64; rv:45.0) Gecko/20100101 Firefox/45.0");
            userEgentList.Add("Mozilla/5.0 (Windows NT 6.1; rv:45.0) Gecko/20100101 Firefox/45.0");
            userEgentList.Add("Mozilla/5.0 (Windows NT 6.1; WOW64; rv:45.0) Gecko/20100101 Firefox/45.0");
            userEgentList.Add("Mozilla/5.0 (Windows NT 10.0) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/49.0.2623.112 Safari/537.36");
            userEgentList.Add("Mozilla/5.0 (Windows NT 10.0; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/49.0.2623.112 Safari/537.36");
            userEgentList.Add("Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/49.0.2623.112 Safari/537.36");
            userEgentList.Add("Mozilla/5.0 (Windows NT 6.3) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/49.0.2623.112 Safari/537.36");
            userEgentList.Add("Mozilla/5.0 (Windows NT 6.3; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/49.0.2623.112 Safari/537.36");
            userEgentList.Add("Mozilla/5.0 (Windows NT 6.3; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/49.0.2623.112 Safari/537.36");
            userEgentList.Add("Mozilla/5.0 (Windows NT 6.2) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/49.0.2623.112 Safari/537.36");
            userEgentList.Add("Mozilla/5.0 (Windows NT 6.2; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/49.0.2623.112 Safari/537.36");
            userEgentList.Add("Mozilla/5.0 (Windows NT 6.2; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/49.0.2623.112 Safari/537.36");
            userEgentList.Add("Mozilla/5.0 (iPhone; CPU iPhone OS 9_3_1 like Mac OS X) AppleWebKit/601.1.46 (KHTML, like Gecko) Version/9.0 Mobile/13E238 Safari/601.1");
            userEgentList.Add("Mozilla/5.0 (iPhone; CPU iPhone OS 9_2_1 like Mac OS X) AppleWebKit/601.1.46 (KHTML, like Gecko) Version/9.0 Mobile/13D15 Safari/601.1");
            userEgentList.Add("Mozilla/5.0 (iPhone; CPU iPhone OS 9_1 like Mac OS X) AppleWebKit/601.1.46 (KHTML, like Gecko) Version/9.0 Mobile/13B143 Safari/601.1");
            userEgentList.Add("Mozilla/5.0 (iPhone; CPU iPhone OS 9_0_2 like Mac OS X) AppleWebKit/601.1.46 (KHTML, like Gecko) Version/9.0 Mobile/13A452 Safari/601.1");
            userEgentList.Add("Mozilla/5.0 (iPad; CPU OS 9_3_1 like Mac OS X) AppleWebKit/601.1.46 (KHTML, like Gecko) Version/9.0 Mobile/13E238 Safari/601.1");
            userEgentList.Add("Mozilla/5.0 (iPad; CPU OS 9_2_1 like Mac OS X) AppleWebKit/601.1.46 (KHTML, like Gecko) Version/9.0 Mobile/13D15 Safari/601.1");
            userEgentList.Add("Mozilla/5.0 (iPod touch; CPU iPhone OS 9_3_1 like Mac OS X) AppleWebKit/601.1.46 (KHTML, like Gecko) Version/9.0 Mobile/13E238 Safari/601.1");
            userEgentList.Add("Mozilla/5.0 (iPod touch; CPU iPhone OS 9_2_1 like Mac OS X) AppleWebKit/601.1.46 (KHTML, like Gecko) Version/9.0 Mobile/13D15 Safari/601.1");
            userEgentList.Add("Mozilla/5.0 (Linux; Android 6.0.1; Nexus 5 Build/MMB29Q) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/48.0.2564.95 Mobile Safari/537.36");
            userEgentList.Add("Mozilla/5.0 (Linux; Android 6.0.1; Nexus 5 Build/MMB29Q) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/48.0.2564.95 Mobile Safari/537.36");
            userEgentList.Add("Mozilla/5.0 (Linux; Android 6.0.1; Nexus 6P Build/MHC19I) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/49.0.2623.91 Mobile Safari/537.36");
            userEgentList.Add("Mozilla/5.0 (Linux; Android 5.1.1; HUAWEI KII-L22 Build/HUAWEIKII-L22) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/49.0.2623.105 Mobile Safari/537.36");
            userEgentList.Add("Mozilla/5.0 (Linux; Android 5.1.1; KYV37 Build/100.0.2210) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/49.0.2623.105 Mobile Safari/537.36");
            userEgentList.Add("Mozilla/5.0 (Linux; Android 5.1.1; Nexus 5 Build/LMY48I) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/49.0.2623.91 Mobile Safari/537.36");
            userEgentList.Add("Mozilla/5.0 (Linux; Android 5.1.1; Nexus 7 Build/LMY47V) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/49.0.2623.105 Safari/537.36");
            userEgentList.Add("Mozilla/5.0 (Linux; Android 5.0.2; 403SH Build/S0010) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/49.0.2623.91 Mobile Safari/537.36");
            userEgentList.Add("Mozilla/5.0 (Linux; Android 5.0.2; SHL25 Build/S1201) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/49.0.2623.91 Mobile Safari/537.36");
            userEgentList.Add("Mozilla/5.0 (Linux; Android 5.0.2; SM-T350 Build/LRX22G) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/49.0.2623.105 Safari/537.36");
            userEgentList.Add("Mozilla/5.0 (Linux; Android 5.0.2; SO-03G Build/28.0.B.1.229) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/49.0.2623.105 Mobile Safari/537.36");
            userEgentList.Add("Mozilla/5.0 (Linux; Android 4.4.4; 404KC Build/105.0.2900) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/49.0.2623.91 Mobile Safari/537.36");
            userEgentList.Add("Mozilla/5.0 (Linux; Android 4.4.4; G620S-L02 Build/HuaweiG620S-L02) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/49.0.2623.105 Mobile Safari/537.36");
            userEgentList.Add("Mozilla/5.0 (Linux; Android 4.4.4; KYV31 Build/103.0.2e00) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/49.0.2623.105 Mobile Safari/537.36");
            userEgentList.Add("Mozilla/5.0 (Linux; Android 4.4.4; SHV31 Build/SA301) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/49.0.2623.91 Mobile Safari/537.36");
        }

        /// <summary>
        /// /ユーザーエージェントを取得する
        /// </summary>
        /// <returns></returns>
        public string getUserEgant()
        {
            //シャッフルする
            userEgentList.Shuffle();

            //最初の要素を返す
            return userEgentList[0];
        }

    }
}
