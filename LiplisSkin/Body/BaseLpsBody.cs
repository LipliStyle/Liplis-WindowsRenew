//=======================================================================
//  ClassName : BaseLpsBody
//  概要      : Liplisの立ち絵インスタンスのベースクラス
//
//  Liplis5.0
//
//  Copyright(c) 2010-2016 LipliStyle.Sachin
//=======================================================================
using Liplis.Gui;
using Liplis.Utl;
using System.Collections.Generic;
using System.Drawing;

namespace Liplis.Body
{
    public abstract class BaseLpsBody
    {
        /// <summary>
        /// getBody
        /// ゲットボディ
        /// 子クラスで実装
        /// </summary>
        /// <returns></returns>
        public abstract Bitmap getBody11();
        public abstract Bitmap getBody12();
        public abstract Bitmap getBody21();
        public abstract Bitmap getBody22();
        public abstract Bitmap getBody31();
        public abstract Bitmap getBody32();
        public Bitmap getBody(int eye, int mouth, int direction)
        {
            if (direction == 0)
            {
                return getBody(eye, mouth);
            }
            else if (direction == 1)
            {
                return rotateFlip(getBody(eye, mouth));
            }
            else
            {
                return getBody(eye, mouth);
            }

        }
        private Bitmap getBody(int eye, int mouth)
        {
            if (mouth == 0)
            {
                if (eye == 1)
                {
                    return getBody11();
                }
                else if (eye == 2)
                {
                    return getBody21();
                }
                else if (eye == 3)
                {
                    return getBody31();
                }
                else
                {
                    return getBody11();
                }
            }
            else if (mouth == 1)
            {
                if (eye == 1)
                {
                    return getBody12();
                }
                else if (eye == 2)
                {
                    return getBody22();
                }
                else if (eye == 3)
                {
                    return getBody32();
                }
                else
                {
                    return getBody12();
                }
            }
            else
            {
                if (eye == 1)
                {
                    return getBody11();
                }
                else if (eye == 2)
                {
                    return getBody21();
                }
                else if (eye == 3)
                {
                    return getBody31();
                }
                else
                {
                    return getBody11();
                }
            }
        }
        
        /// <summary>
        /// ビットマップのパスをチェックした上で、
        /// 存在する場合はそのパスの画像を返す。
        /// 存在しない場合は透明１ドットのインスタンスを返す
        /// </summary>
        /// <returns></returns>
        public Bitmap getBitmap(string path)
        {
            if (LpsPathController.checkFileExist(path))
            {
                return new Bitmap(path);
            }
            else
            {
                LpsMessage.showError("画像の読み込みに失敗しました");
                return null;
            }
        }

        /// <summary>
        /// 立ち絵を反転する
        /// </summary>
        public Bitmap rotateFlip(Bitmap pic)
        {
            pic.RotateFlip(RotateFlipType.Rotate180FlipY);
            return pic;
        }

        /// <summary>
        /// タッチリストの取得
        /// </summary>
        /// <returns></returns>
        public abstract List<string> getLstTouch();
    }
}
