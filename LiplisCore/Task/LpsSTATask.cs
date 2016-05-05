//=======================================================================
//  ClassName : LpsSTATask
//  概要      : STAでTaskを実行出来るクラス
//
//  Liplis5.0
//
//  Copyright(c) 2010-2016 LipliStyle.Sachin
//=======================================================================
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Liplis.Tasks
{
    public class LpsSTATask
    {
        /// <summary>
        /// Task.Runのマネ
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="func"></param>
        /// <returns></returns>
        public static Task Run<T>(Func<T> func)
        {
            //タスク
            var tcs = new TaskCompletionSource<T>();

            //スレッド実行
            var thread = new Thread(() =>
            {
                try
                {
                    tcs.SetResult(func());
                }
                catch (Exception e)
                {
                    tcs.SetException(e);
                }
            });
            thread.SetApartmentState(ApartmentState.STA);
            thread.Start();
            return tcs.Task;
        }

        /// <summary>
        /// Task.Runのマネ
        /// </summary>
        /// <param name="act"></param>
        /// <returns></returns>
        public static Task Run(Action act)
        {
            return Run(() =>
            {
                act();
                return true;
            });
        }
    }
}
