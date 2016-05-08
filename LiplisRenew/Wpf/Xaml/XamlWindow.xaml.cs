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
    /// XsamlWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class XamlWindow : Window
    {
        public XamlWindow()
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

        /// <summary>
        /// サイズチェンジイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            this.image.Width = this.Width;
            this.image.Height = this.Height;
        }
    }
}
