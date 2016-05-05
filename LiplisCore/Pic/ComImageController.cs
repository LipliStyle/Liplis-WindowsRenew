//=======================================================================
//  ClassName : ComImageController
//  概要      : イメージコントローラー
//
//  Liplis5.0
//
//  Copyright(c) 2010-2016 LipliStyle.Sachin
//=======================================================================
using System;
using System.Drawing;

namespace Liplis.Pic
{
    public static class ComImageController
    {
        /// <summary>
        /// ファイルサイズを返す。
        /// </summary>
        /// <returns></returns>
        #region createThumbnail
        public static Image createThumbnail(Image orig)
        {
            return orig.GetThumbnailImage(
              150, 107, delegate { return false; }, IntPtr.Zero);
        }
        public static Image createThumbnail(Image orig, int hi, int wid)
        {
            return orig.GetThumbnailImage(
              wid, hi, delegate { return false; }, IntPtr.Zero);
        }
        #endregion
    }
}
