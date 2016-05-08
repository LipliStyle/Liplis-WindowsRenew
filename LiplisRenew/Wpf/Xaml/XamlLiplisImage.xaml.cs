//=======================================================================
//  ClassName : XamlLiplisImage
//  概要      : リプリスイメージ
//
// iOS版:UiImageに対応
//
//Liplis5.0
//
//アップデート履歴
//   2016/05/08 ver5.0.0 作成
//
//  Copyright(c) 2010-2016 LipliStyle.Sachin
//=======================================================================
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Liplis.Wpf.Xaml
{
    /// <summary>
    /// LiplisWidget.xaml の相互作用ロジック
    /// </summary>
    public partial class XamlLiplisImage : Window
    {
        public XamlLiplisImage()
        {
            InitializeComponent();
        }

        /// <summary>
        /// ドラッグムーブ
        /// </summary>
        /// <param name="e"></param>
        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonDown(e);
            DragMove();
        }
    }
}
