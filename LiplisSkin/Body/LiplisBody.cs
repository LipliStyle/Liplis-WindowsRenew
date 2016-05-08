//=======================================================================
//  ClassName : LpsBody
//  概要      : Liplisの立ち絵インスタンス
//
//  Liplis5.0
//
//  Copyright(c) 2010-2016 LipliStyle.Sachin
//=======================================================================
using Liplis.Gui;
using Liplis.Utl;
using System;
using System.Collections.Generic;
using System.Drawing;

namespace Liplis.Body
{
    public class LiplisBody : ICloneable
    {
        ///=============================
        ///プロパティ
        public string body11 { get; set; }
        public string body12 { get; set; }
        public string body21 { get; set; }
        public string body22 { get; set; }
        public string body31 { get; set; }
        public string body32 { get; set; }
        public string bodyDir { get; set; }
        public List<string> lstTouch { get; set; }

        /// <summary>
        /// コンストラクター
        /// </summary>
        /// <param name="body11">目開、口デフォ</param>
        /// <param name="body12">目開、口反転</param>
        /// <param name="body21">目半、口デフォ</param>
        /// <param name="body22">目半、口半転</param>
        /// <param name="body31">目閉、口デフォ</param>
        /// <param name="body32">目閉、口反</param>
        #region LpsBody
        public LiplisBody(string body11, string body12, string body21, string body22, string body31, string body32, string touch, string bodyDir)
        {
            this.body11 = body11;
            this.body12 = body12;
            this.body21 = body21;
            this.body22 = body22;
            this.body31 = body31;
            this.body32 = body32;
            this.bodyDir = bodyDir;
            this.lstTouch = new List<string>(touch.Split(','));
        }
        public LiplisBody(string body11, string body12, string body21, string body22, string body31, string body32, string bodyDir, List<string> lstTouch)
        {
            this.body11 = body11;
            this.body12 = body12;
            this.body21 = body21;
            this.body22 = body22;
            this.body31 = body31;
            this.body32 = body32;
            this.bodyDir = bodyDir;
            this.lstTouch = lstTouch;
        }
        #endregion

        /// <summary>
        /// ボディを取得する
        /// 目、口、方向の引数から、最適なボディを返す
        /// </summary>
        /// <param name="eye"></param>
        /// <param name="mouth"></param>
        /// <param name="direction"></param>
        /// <returns></returns>
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
        /// Bodyのゲッター
        /// </summary>
        /// <returns></returns>
        #region getBody
        public Bitmap getBody11()
        {
            return new Bitmap(bodyDir + body11);
        }
        public Bitmap getBody12()
        {
            return new Bitmap(bodyDir + body12);
        }
        public  Bitmap getBody21()
        {
            return new Bitmap(bodyDir + body21);
        }
        public Bitmap getBody22()
        {
            return new Bitmap(bodyDir + body22);
        }
        public Bitmap getBody31()
        {
            return new Bitmap(bodyDir + body31);
        }
        public Bitmap getBody32()
        {
            return new Bitmap(bodyDir + body32);
        }

        public string getBodyPath11()
        {
            return bodyDir + body11;
        }
        public string getBodyPath12()
        {
            return bodyDir + body12;
        }
        public string getBodyPath21()
        {
            return bodyDir + body21;
        }
        public string getBodyPath22()
        {
            return bodyDir + body22;
        }
        public string getBodyPath31()
        {
            return bodyDir + body31;
        }
        public string getBodyPath32()
        {
            return bodyDir + body32;
        }
        #endregion

        /// <summary>
        /// インデックスからパスを取得する
        /// </summary>
        /// <returns></returns>
        #region getPathFromIdx
        public string getPathFromIdx(int idx)
        {
            switch (idx)
            {
                case 1:
                    return this.body11;
                case 2:
                    return this.body12;
                case 3:
                    return this.body21;
                case 4:
                    return this.body22;
                case 5:
                    return this.body31;
                case 6:
                    return this.body32;
                default:
                    return "";
            }
        }
        #endregion

        /// <summary>
        /// インデックスからパスをセットする
        /// </summary>
        /// <returns></returns>
        #region setPathFromIdx
        public void setPathFromIdx(int idx, string newPath)
        {
            switch (idx)
            {
                case 1:
                    this.body11 = newPath;
                    break;
                case 2:
                    this.body12 = newPath;
                    break;
                case 3:
                    this.body21 = newPath;
                    break;
                case 4:
                    this.body22 = newPath;
                    break;
                case 5:
                    this.body31 = newPath;
                    break;
                case 6:
                    this.body32 = newPath;
                    break;
                default:
                    break;
            }
        }
        #endregion

        /// <summary>
        /// reductionBitmap
        /// 画像を縮小する
        /// </summary>
        /// <returns>ビットマップ</returns>
        #region reductionBitmap
        private Bitmap reductionBitmap(Bitmap source, int hi, int wid)
        {
            try
            {
                Bitmap canvas = new Bitmap(hi, wid);

                using (Bitmap image = source)
                {
                    using (Graphics g = Graphics.FromImage(canvas))
                    {
                        g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                        g.DrawImage(image, 0, 0, hi, wid);
                    }
                }

                return canvas;
            }
            catch
            {
                return source;
            }
        }
        #endregion

        /// <summary>
        /// 自分のインスタンスのコピーを返す
        /// </summary>
        /// <returns></returns>
        #region copy
        object ICloneable.Clone()
        {
            return new LiplisBody(this.body11, this.body12, this.body21, this.body22, this.body31, this.body32, this.bodyDir, this.lstTouch);
        }
        public LiplisBody Clone()
        {
            return new LiplisBody(this.body11, this.body12, this.body21, this.body22, this.body31, this.body32, this.bodyDir, this.lstTouch);
        }
        #endregion

        /// <summary>
        /// タッチリストを取得する
        /// </summary>
        /// <returns></returns>
        #region getLstTouch
        public List<string> getLstTouch()
        {
            return this.lstTouch;
        }
        #endregion

        /// <summary>
        /// タッチリストを取得する
        /// </summary>
        /// <returns></returns>
        #region getTouch
        public string getTouch()
        {
            string result = "";

            //コンマ区切りで設定
            foreach (string touch in lstTouch)
            {
                result = result + touch + ",";
            }

            //最後のコンマ除去
            if (result.Length > 0)
            {
                result = result.Substring(0, result.Length - 1);
            }

            return result;
        }

        #endregion

    }
}
