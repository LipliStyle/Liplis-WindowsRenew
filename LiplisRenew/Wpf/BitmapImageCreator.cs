using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Windows.Media.Imaging;

namespace Liplis.Wpf
{
    public class BitmapImageCreator
    {

        public static BitmapFrame bitmap2BitmapImage(Bitmap bmp)
        {
            BitmapFrame biamge;
            IntPtr hBitmap = bmp.GetHbitmap();

            using (Stream st = new MemoryStream())
            {
                bmp.Save(st, ImageFormat.Bmp);
                st.Seek(0, SeekOrigin.Begin);
                biamge = BitmapFrame.Create(st, BitmapCreateOptions.None, BitmapCacheOption.OnLoad);
            }

            return biamge;
        }
    }
}
