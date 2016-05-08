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
        /// <summary>
        /// オパシティを上げる
        /// </summary>
        /// <param name="targetWindow"></param>
        /// <param name="targetControl"></param>
        public static void opacityUp(Window targetWindow, ContentControl targetControl)
        {
            //ストーリーボード取得
            Storyboard storyboard = opacityAnimation(targetControl, 0.0, 1.0, 5000000);

            //アニメーション開始
            storyboard.Begin(targetWindow);
        }
        public static Storyboard opacityUpStoryboard(ContentControl targetControl)
        {
            return opacityAnimation(targetControl, 0.0, 1.0, 5000000);
        }
        public static Storyboard opacityUpStoryboard(ContentControl targetControl, double toOpa)
        {
            return opacityAnimation(targetControl, 0.0, toOpa, 5000000);
        }

        /// <summary>
        /// オパシティを下げる
        /// </summary>
        /// <param name="targetWindow"></param>
        /// <param name="targetControl"></param>
        public static void opacityDown(Window targetWindow, ContentControl targetControl)
        {
            //ストーリーボード取得
            Storyboard storyboard = opacityAnimation(targetControl, 1.0, 0.0, 5000000);

            //アニメーション開始
            storyboard.Begin(targetWindow);
        }
        public static Storyboard opacityDownStoryboard(ContentControl targetControl)
        {
            return opacityAnimation(targetControl, 1.0, 0.0, 5000000);
        }
        public static Storyboard opacityDownStoryboard(ContentControl targetControl, double fromOpa)
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
        public static Storyboard opacityAnimation(ContentControl targetControl, double opaFrom, double opaTo, int interval)
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
            Storyboard.SetTargetName(animation, targetControl.Name);

            //アニメーションターゲットの設定
            Storyboard.SetTargetProperty(animation, new PropertyPath(Control.OpacityProperty));

            //ストーリーボードに追加
            storyboard.Children.Add(animation);



            return storyboard;
        }

    }
}
