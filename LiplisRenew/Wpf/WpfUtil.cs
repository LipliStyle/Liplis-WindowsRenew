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

using System.Windows;

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

    }
}
