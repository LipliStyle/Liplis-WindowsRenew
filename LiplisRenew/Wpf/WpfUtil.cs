//=======================================================================
//  ClassName : WpfUtil
//  概要      : WPF関連ユーティリティ
//
//
//Liplis5.0
//
//アップデート履歴
//   2016/05/08 ver5.0.0 作成
//
//  Copyright(c) 2010-2016 LipliStyle.Sachins;
//=======================================================================

using System;
using System.Windows;
using System.Windows.Media;

namespace Liplis.Wpf
{
    public class WpfUtil
    {
        /// <summary>
        /// WPFウインドウが閉じられているかチェックする
        /// </summary>
        /// <param name="window"></param>
        /// <returns></returns>
        public static bool isWpfDisposed(Window window)
        {
            try
            {
                var windowState = PresentationSource.FromVisual(window);

                if(windowState == null)
                {
                    return true;
                }
                else
                {
                    return windowState.IsDisposed;
                }

                 
            }
            catch
            {
                return false;
            }

            
        }

        /// <summary>
        /// ビットマップをイメージブラシに変換する
        /// </summary>
        /// <param name="hObject"></param>
        /// <returns></returns>
        [System.Runtime.InteropServices.DllImport("gdi32.dll")]
        public static extern bool DeleteObject(IntPtr hObject);
        public static ImageBrush convertBitmapToImageBrash(System.Drawing.Bitmap bmp)
        {
            ImageBrush result = null;
            IntPtr hBitmap = bmp.GetHbitmap();
            try
            {
                result = new ImageBrush(System.Windows.Interop.Imaging.CreateBitmapSourceFromHBitmap(
                    hBitmap,
                    IntPtr.Zero,
                    Int32Rect.Empty,
                    System.Windows.Media.Imaging.BitmapSizeOptions.FromEmptyOptions()));
            }
            finally
            {
                DeleteObject(hBitmap);
            }

            return result;
        }

    }
}
