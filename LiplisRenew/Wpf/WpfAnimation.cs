//=======================================================================
//  ClassName : WpfAnimation
//  概要      : アニメーションを定義する
//
// iOS版と同等
//
//
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
using System.Windows.Media.Animation;

namespace Liplis.Wpf
{
    public class WpfAnimation
    {


        //============================================================
        //
        //透明度操作
        //
        //============================================================
        #region 透明度操作
        /// <summary>
        /// オパシティを上げる
        /// </summary>
        /// <param name="targetWindow"></param>
        /// <param name="targetControl"></param>
        public static void opacityUp(Window targetWindow, FrameworkElement targetControl)
        {
            opacityUp(targetWindow, targetControl, 5000000);
        }
        public static void opacityUp(Window targetWindow, FrameworkElement targetControl, Int32 interval)
        {
            //ストーリーボード取得
            Storyboard storyboard = opacityAnimation(targetControl, 0.0, 1.0, interval);

            //アニメーション開始
            storyboard.Begin(targetWindow);
        }
        public static Storyboard opacityUpStoryboard(FrameworkElement targetControl)
        {
            return opacityAnimation(targetControl, 0.0, 1.0, 5000000);
        }
        public static Storyboard opacityUpStoryboard(FrameworkElement targetControl, double toOpa)
        {
            return opacityAnimation(targetControl, 0.0, toOpa, 5000000);
        }

        /// <summary>
        /// オパシティを下げる
        /// </summary>
        /// <param name="targetWindow"></param>
        /// <param name="targetControl"></param>
        public static void opacityDown(Window targetWindow, FrameworkElement targetControl)
        {
            opacityDown(targetWindow, targetControl, 5000000);
        }
        public static void opacityDown(Window targetWindow, FrameworkElement targetControl, Int32 interval)
        {
            //ストーリーボード取得
            Storyboard storyboard = opacityAnimation(targetControl, 1.0, 0.0, interval);

            //アニメーション開始
            storyboard.Begin(targetWindow);
        }
        public static Storyboard opacityDownStoryboard(FrameworkElement targetControl)
        {
            return opacityAnimation(targetControl, 1.0, 0.0, 5000000);
        }
        public static Storyboard opacityDownStoryboard(FrameworkElement targetControl, double fromOpa)
        {
            return opacityAnimation(targetControl, fromOpa, 0.0, 5000000);
        }

        /// <summary>
        /// アニメーションでオパシティを変化させる
        /// </summary>
        /// <param name="targetWindow"></param>
        /// <param name="targetControl"></param>
        /// <param name="opaFrom"></param>
        /// <param name="opaTo"></param>
        public static Storyboard opacityAnimation(FrameworkElement targetControl, double opaFrom, double opaTo, int interval)
        {
            Storyboard storyboard = new Storyboard();
            TimeSpan duration = new TimeSpan(interval);

            //ダブルアニメーションの生成
            DoubleAnimation animation = new DoubleAnimation();

            //アニメーション定義
            animation.From = opaFrom;
            animation.To = opaTo;
            animation.Duration = new Duration(duration);

            //対象コントロール設定
            Storyboard.SetTarget(animation, targetControl);

            //アニメーションターゲットの設定
            Storyboard.SetTargetProperty(animation, new PropertyPath(Control.OpacityProperty));

            //ストーリーボードに追加
            storyboard.Children.Add(animation);



            return storyboard;
        }
        #endregion






        //============================================================
        //
        //サイズ操作
        //
        //============================================================
        #region サイズ操作
        public static void imageClickDownAnimeation(Window window, Rect baseRect, Image targetControl)
        {
            //ストーリーボード取得
            Storyboard storyboard1 = WpfAnimation.sizeChangeImageAnimation(baseRect, targetControl, 1.0, 0.9, 600000);

            //アニメーション開始
            storyboard1.Begin(window);

            Storyboard storyboard2 = WpfAnimation.sizeChangeImageAnimation(baseRect, targetControl, 0.9, 0.95, 600000);

            //アニメーション開始
            storyboard2.Begin(window);
        }

        public static void imageClickUpAnimeation(Window window, Rect baseRect, Image targetControl)
        {
            //ストーリーボード取得
            Storyboard storyboard2 = WpfAnimation.sizeChangeImageAnimation(baseRect, targetControl, 0.95, 1.0, 600000);

            //アニメーション開始
            storyboard2.Begin(window);
        }


        /// <summary>
        /// アニメーションでサイズを変化させる
        /// </summary>
        /// <param name="targetWindow"></param>
        /// <param name="targetControl"></param>
        /// <param name="opaFrom"></param>
        /// <param name="opaTo"></param>
        public static Storyboard sizeChangeImageAnimation(Rect baseRect, Image targetControl, double sizeFrom, double sizeTo, int interval)
        {
            Storyboard storyboard = new Storyboard();
            TimeSpan duration = new TimeSpan(interval);

            //ダブルアニメーションの生成
            DoubleAnimation heightAnimation = new DoubleAnimation();
            DoubleAnimation widthAnimation = new DoubleAnimation();
            ThicknessAnimation marginAnimation = new ThicknessAnimation();

            Thickness tkFrom = targetControl.Margin;
            tkFrom.Left = baseRect.Left + (targetControl.Width - (targetControl.Width * sizeFrom))/2;
            tkFrom.Top = baseRect.Top + (targetControl.Height - (targetControl.Height * sizeFrom))/2;

            Thickness tkTo = targetControl.Margin;
            tkTo.Left = baseRect.Left + (targetControl.Width - (targetControl.Width * sizeTo))/ 2;
            tkTo.Top = baseRect.Top + (targetControl.Height - (targetControl.Height * sizeTo) )/ 2;


            //アニメーション定義
            heightAnimation.From = baseRect.Height * sizeFrom;
            heightAnimation.To = baseRect.Height * sizeTo;
            heightAnimation.Duration = new Duration(duration);

            widthAnimation.From = baseRect.Width * sizeFrom;
            widthAnimation.To = baseRect.Width * sizeTo;
            widthAnimation.Duration = new Duration(duration);

            marginAnimation.From = tkFrom;
            marginAnimation.To = tkTo;
            marginAnimation.Duration = new Duration(duration);


            //対象コントロール設定
            Storyboard.SetTarget(heightAnimation, targetControl);
            Storyboard.SetTarget(widthAnimation, targetControl);
            Storyboard.SetTarget(marginAnimation, targetControl);

            //アニメーションターゲットの設定
            Storyboard.SetTargetProperty(heightAnimation, new PropertyPath(Control.HeightProperty));
            Storyboard.SetTargetProperty(widthAnimation, new PropertyPath(Control.WidthProperty));
            Storyboard.SetTargetProperty(marginAnimation, new PropertyPath(Image.MarginProperty));

            //ストーリーボードに追加
            storyboard.Children.Add(heightAnimation);
            storyboard.Children.Add(widthAnimation);
            storyboard.Children.Add(marginAnimation);

            return storyboard;
        }
        #endregion


        //============================================================
        //
        //ウインドウ高さ操作
        //
        //============================================================
        #region ウインドウ高さ操作
        public static void windowHeightChange(Window targetWindow, Image targetControl, double sizeFrom, double sizeTo)
        {
            //ストーリーボード取得
            Storyboard storyboard = windowHeightChangeAnimation(targetControl, sizeFrom, sizeTo, 1000000);

            //アニメーション開始
            storyboard.Begin(targetWindow);
        }

        /// <summary>
        /// アニメーションでサイズを変化させる
        /// </summary>
        /// <param name="targetWindow"></param>
        /// <param name="targetControl"></param>
        /// <param name="opaFrom"></param>
        /// <param name="opaTo"></param>
        public static Storyboard windowHeightChangeAnimation(Image targetControl, double sizeFrom, double sizeTo, int interval)
        {
            Storyboard storyboard = new Storyboard();
            TimeSpan duration = new TimeSpan(interval);

            //ダブルアニメーションの生成
            DoubleAnimation heightAnimation = new DoubleAnimation();

            //アニメーション定義
            heightAnimation.From = sizeFrom;
            heightAnimation.To = sizeTo;
            heightAnimation.Duration = new Duration(duration);

            //対象コントロール設定
            Storyboard.SetTarget(heightAnimation, targetControl);

            //アニメーションターゲットの設定
            Storyboard.SetTargetProperty(heightAnimation, new PropertyPath(Control.HeightProperty));

            //ストーリーボードに追加
            storyboard.Children.Add(heightAnimation);

            storyboard.AccelerationRatio = 0.1;
            storyboard.DecelerationRatio = 0.1;

            return storyboard;
        }
        #endregion



        //============================================================
        //
        //ウインドウ移動
        //
        //============================================================
        #region ウインドウ移動
        /// <summary>
        /// ウインドウをアニメーションで移動させる
        /// </summary>
        /// <param name="targetWindow"></param>
        /// <param name="moveToX"></param>
        /// <param name="moveToY"></param>
        public static void windowMove(Window targetWindow, double moveToX, double moveToY)
        {
            //ストーリーボード取得
            Storyboard storyboard = windowMoveAnimation(targetWindow, moveToX, moveToY, 5000000);

            //アニメーション開始
            storyboard.Begin(targetWindow);
        }


        public static Storyboard windowMoveAnimation(Window targetWindow, double moveToX, double moveToY, int interval)
        {
            Storyboard storyboard = new Storyboard();
            TimeSpan duration = new TimeSpan(interval);

            //ダブルアニメーションの生成
            DoubleAnimation topAnimation = new DoubleAnimation();
            DoubleAnimation leftAnimation = new DoubleAnimation();

            //アニメーション定義
            topAnimation.From = targetWindow.Top;
            topAnimation.To = moveToY;
            topAnimation.Duration = new Duration(duration);

            leftAnimation.From = targetWindow.Left;
            leftAnimation.To = moveToX;
            leftAnimation.Duration = new Duration(duration);

            //対象コントロール設定
            Storyboard.SetTarget(topAnimation, targetWindow);
            Storyboard.SetTarget(leftAnimation, targetWindow);

            //アニメーションターゲットの設定
            Storyboard.SetTargetProperty(topAnimation, new PropertyPath("Top"));
            Storyboard.SetTargetProperty(leftAnimation, new PropertyPath("Left"));

            //ストーリーボードに追加
            storyboard.Children.Add(topAnimation);
            storyboard.Children.Add(leftAnimation);

            storyboard.AccelerationRatio = 0.2;
            storyboard.DecelerationRatio = 0.2;

            return storyboard;
        }
        #endregion
    }
}
