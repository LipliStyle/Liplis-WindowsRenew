//=======================================================================
//  ClassName : LpsResorceManager
//  概要      : リソース画像を使うためのユーティリティ
//
//
//Liplis5.0
//
//アップデート履歴
//   2016/05/21 ver5.0.0 作成
//
//  Copyright(c) 2010-2016 LipliStyle.Sachin
//=======================================================================
using Liplis.Properties;
using System.Drawing;
using System.Reflection;
using System.Resources;

namespace Liplis.Com
{
    public class LpsResorceManager
    {
        /// <summary>
        /// リプリス名前空間のリソース画像を名前指定で取得する
        /// 取得の型はビットマップ
        /// </summary>
        /// <param name="resourceName"></param>
        /// <returns></returns>
        public static Bitmap getResourceBitmap(Assembly assembly,string resourceName)
        {
            ResourceManager rm = new ResourceManager("Liplis.Properties.Resources", assembly);
            return (Bitmap)rm.GetObject(resourceName);
        }
    }
}
