//=======================================================================
//  ClassName : LiplisIconImage
//  概要      : アイコンイメージ
//
//Liplis5.0
//
//アップデート履歴
//   2016/05/15 ver5.0.0 作成
//
//  Copyright(c) 2010-2016 LipliStyle.Sachin
//=======================================================================
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace Liplis.Wpf
{
    public class LiplisIconImage
    {
        //=================================
        //イメージ要素
        public Image image { get; set; }
        public Int32 iconSide { get; set; }
        public Int32 xMargin { get; set; }
        public Int32 yMargin { get; set; }

        //=================================
        //操作フラグ
        public bool buttonOn { get; set; }      //ボタンONの場合、true

        public LiplisIconImage(Image image, string imageUri, Int32 iconSide, Int32 xMargin, Int32 yMargin)
        {
            //引数取得
            this.image    = image;
            this.iconSide = iconSide;
            this.xMargin  = xMargin;
            this.yMargin  = yMargin;

            //イメージ設定
            setImage(imageUri);

            //サイズ、位置設定
            this.image.Margin = new Thickness(xMargin, yMargin, 0, 0);
            this.image.Width  = iconSide;
            this.image.Height = iconSide;

            //イメージのタグに自インスタンスを入れておく(イベハンで使えるようにするため)
            this.image.Tag = this;
        }

        /// <summary>
        /// イメージを設定する
        /// </summary>
        /// <param name="imageUri"></param>
        public void setImage(string imageUri)
        {
            //イメージ設定
            this.image.Source = new BitmapImage(new Uri(imageUri));
        }



    }
}
