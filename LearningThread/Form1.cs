using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LearningThread
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void sync_Click(object sender, EventArgs e)
        {
            Console.WriteLine();
            Console.WriteLine($"****************sync_Click Start with thread: {Thread.CurrentThread.ManagedThreadId}*********************");
            int j = 0;
            int k = 1;
            int m = j + k;
            for (int i = 0; i < 5; i++)
            {
                string name = string.Format("{0}_{1}", "sync_Click", i);
                this.DoSomethingLong(name);
            }



            Console.WriteLine($"****************sync_Click End with thread: {Thread.CurrentThread.ManagedThreadId}*********************");
        }

        #region private
        private void DoSomethingLong(string name)
        {
            Console.WriteLine($"****************DoSomethingLong Start with thread: {Thread.CurrentThread.ManagedThreadId} {name} {DateTime.Now.ToString("HHmmss:fff")}*********************");

            long result = 0;
            for (int i  =0;i < 1000000000; i++)
            {
                result += i;
            }

            Console.WriteLine($"****************DoSomethingLong End with thread: {Thread.CurrentThread.ManagedThreadId} {name} {DateTime.Now.ToString("HHmmss:fff")}*********************");
        }

        private void DoSomethingLongAdvanced(string name)
        {
            Console.WriteLine($"****************DoSomethingLong Start with thread: {Thread.CurrentThread.ManagedThreadId} {name} {DateTime.Now.ToString("HHmmss:fff")}*********************");

            long result = 0;
            for (int i = 0; i < 1000000000; i++)
            {
                result += i;
            }

            Console.WriteLine($"****************DoSomethingLong End with thread: {Thread.CurrentThread.ManagedThreadId} {name} {DateTime.Now.ToString("HHmmss:fff")}*********************");
        }

        private void ShowProgress(string progress)
        {
            Console.WriteLine(progress);
        }

        private void UploadFile(string name) 
        {
            Console.WriteLine($"****************UploadFile Start with thread: {Thread.CurrentThread.ManagedThreadId} {name} {DateTime.Now.ToString("HHmmss:fff")}*********************");
            Thread.Sleep(2000);
            Console.WriteLine($"****************UploadFile End with thread: {Thread.CurrentThread.ManagedThreadId} {name} {DateTime.Now.ToString("HHmmss:fff")}*********************");
        }

        private string RemoteServer()
        {
            Console.WriteLine($"****************UploadFile Start with thread: {Thread.CurrentThread.ManagedThreadId} {DateTime.Now.ToString("HHmmss:fff")}*********************");
            Thread.Sleep(2000);
            Console.WriteLine($"****************UploadFile End with thread: {Thread.CurrentThread.ManagedThreadId} {DateTime.Now.ToString("HHmmss:fff")}*********************");
            return "remote result";
        }

        private void Coding(string name, string project)
        {
            Console.WriteLine($"****************Coding Start {name} {project} with thread: {Thread.CurrentThread.ManagedThreadId} {DateTime.Now.ToString("HHmmss:fff")}*********************");
            long result = 0;
            for (int i = 0; i < 1000000000; i++)
            {
                result += i;
            }
            Thread.Sleep(200);
            Console.WriteLine($"****************Coding End  {name} {project} with thread: {Thread.CurrentThread.ManagedThreadId} {DateTime.Now.ToString("HHmmss:fff")}*********************");
            ;
        }
        #endregion

        private void btnAsync_Click(object sender, EventArgs e)
        {
            Console.WriteLine();
            Console.WriteLine($"****************button1_Click Start with thread: {Thread.CurrentThread.ManagedThreadId}*********************");
            Action<string> action = DoSomethingLong;
            for (int i = 0; i < 5; i++)
            {
                string name = string.Format("{0}_{1}", "button2_click", i);
                action.BeginInvoke(name, null, null);
            }

            Console.WriteLine($"****************button1_Click End with thread: {Thread.CurrentThread.ManagedThreadId}*********************");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnASyncAdvanced_Click(object sender, EventArgs e)
        {
            Console.WriteLine();
            Console.WriteLine($"****************btnASyncAdvanced_Click Start with thread: {Thread.CurrentThread.ManagedThreadId}*********************");
            #region 异步回调
            //1.执行DoSomethingLongAdvanced不能卡UI界面，
            //执行完操作后，需要写log
            //Action<string> action = DoSomethingLongAdvanced;
            //string name = string.Format("{0}_{1}", "btnASyncAdvanced_Click", 1);

            //AsyncCallback callback = ar =>
            //{
            //    Console.WriteLine(ar.AsyncState);
            //    Console.WriteLine($"DoSomethingLongAdvanced 任务完成。。。。。{Thread.CurrentThread.ManagedThreadId}");
            //};
            //action.BeginInvoke(name, callback, "Kulala");
            #endregion

            #region  IsCompleted等待线程完成会有时间误差
            //用户必须确定操作完成，才能返回上传文件，只有文件上传成功才能预览------等到任务真的完成以后才能给用户返回
            Action<string> action = UploadFile;
            Action<string> action2 = ShowProgress;
            string name = string.Format("{0}_{1}", "btnASyncAdvanced_Click", 1);

            AsyncCallback callback = ar =>
            {
                Console.WriteLine(ar.AsyncState);
                Console.WriteLine($"DoSomethingLongAdvanced 任务完成。。。。。{Thread.CurrentThread.ManagedThreadId}");
            };
            var result = action.BeginInvoke(name, callback, "Kulala");
            int i = 0;
            //while (!result.IsCompleted)
            //{
            //    if (i < 9)
            //    {
            //        string progress = $"当前文件上传进度为{++i * 10}%";
            //        action2.BeginInvoke(progress, null, null);
            //        this.textBox1.Text = progress;
            //        Application.DoEvents();
            //    }
            //    else
            //    {
            //        action2.BeginInvoke("当前文件上传进度为99.999%", null, null);
            //        this.textBox1.Text = "当前文件上传进度为99.999%";
            //        Application.DoEvents();
            //    }
            //    Thread.Sleep(200);
            //}
            while (!result.IsCompleted)
            {
                int k = i;
                if (i< 9)
                {
                    string progress = $"当前文件上传进度为{++i * 10}%";
                    action2.BeginInvoke(progress, null, null);
                    Task.Run(()=>{
                        if (this.textBox1.InvokeRequired)
                        {
                            this.Invoke(new Action(() =>
                            {
                                textBox1.Text = progress;
                                //Thread.Sleep(2000);
                                //Console.WriteLine($"当前UpdateLbl线程id{Thread.CurrentThread.ManagedThreadId}");
                            }));
                        }
                    });

                    //Task.Run(() => { this.btn.Invoke(new Action(()=> this.textBox1.Text = progress; ))});
                    //Application.DoEvents();
                }
                else
                {
                    action2.BeginInvoke("当前文件上传进度为99.999%", null, null);
                    this.textBox1.Text = "当前文件上传进度为99.999%";
                    Application.DoEvents();
                }
                Thread.Sleep(200);
            }
            Console.WriteLine("文件上传成功，现在开始预览。。。。。。。。。。");
            #endregion

            #region  信号量
            //用户必须确定操作完成，才能返回上传文件，只有文件上传成功才能预览------等到任务真的完成以后才能给用户返回
            //Action<string> action = UploadFile;
            //Action<string> action2 = ShowProgress;
            //string name = string.Format("{0}_{1}", "btnASyncAdvanced_Click", 1);

            //AsyncCallback callback = ar =>
            //{
            //    Console.WriteLine(ar.AsyncState);
            //    Console.WriteLine($"DoSomethingLongAdvanced 任务完成。。。。。{Thread.CurrentThread.ManagedThreadId}");
            //};
            //var result = action.BeginInvoke(name, callback, "Kulala");
            //Console.WriteLine("do something else here..........");
            //result.AsyncWaitHandle.WaitOne(1000);//阻塞当前线程，知道异步调用完成，才开始下面的操作，
            //                                 // -1,一致等待
            //                                 //1000，等待且最多等待1000ms，超时提前结束
            //Console.WriteLine("文件上传成功，现在开始预览。。。。。。。。。。");
            #endregion

            //#region  EndInvoke调用接口有返回值的
            ////用户必须确定操作完成，才能返回上传文件，只有文件上传成功才能预览------等到任务真的完成以后才能给用户返回
            //Func<string> func = RemoteServer;
            //var result = func.BeginInvoke(ar =>
            //{
            //    Console.WriteLine(func.EndInvoke(ar));
            //},
            //null);
            //var realResult = func.EndInvoke(result);//实际的result

            //Func<string> func1 = () => DateTime.Now.ToString();
            //var result1 = func1.EndInvoke(func1.BeginInvoke(null,null));

            //Func<string, string> func2 = (s) => $"1_{s}";
            //var result2 = func2.EndInvoke(func2.BeginInvoke("kulala", null, null));
            //#endregion

            Console.WriteLine($"****************btnASyncAdvanced_Click End with thread: {Thread.CurrentThread.ManagedThreadId}*********************");
        }
           

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnMutiple_Click(object sender, EventArgs e)
        {
            Console.WriteLine();
            Console.WriteLine($"****************btnMutiple_Click Start with thread: {Thread.CurrentThread.ManagedThreadId}*********************");
            {
                //.net framework 1.0,1.1 Thread
                //api特别丰富
                ParameterizedThreadStart threadStart = x => 
                {
                    Console.WriteLine($"This is Thread start : {Thread.CurrentThread.ManagedThreadId}");
                    Thread.Sleep(2000);
                    Console.WriteLine($"This is Thread end : {Thread.CurrentThread.ManagedThreadId}");
                };
                Thread thread = new Thread(threadStart);
                thread.Start();
                Console.WriteLine("等待thread线程结束");
                thread.Join();
                //thread.Abort();
                //thread.Suspend();
                //thread.Resume();
                //thread.IsBackground = true;
            }
            {
                //.net framework 2.0 new clr
                //线程池
                ThreadPool.QueueUserWorkItem(s => 
                {
                    Console.WriteLine($"This is ThreadPool start : {Thread.CurrentThread.ManagedThreadId}");
                    Thread.Sleep(2000);
                    Console.WriteLine($"This is ThreadPool end : {Thread.CurrentThread.ManagedThreadId}");
                }, "kulala");
            }

            {
                //.net framework3.0 3.5
                //线程池
                Task.Run(()=> 
                {
                    Console.WriteLine($"This is Task start : {Thread.CurrentThread.ManagedThreadId}");
                    Thread.Sleep(2000);
                    Console.WriteLine($"This is Task end : {Thread.CurrentThread.ManagedThreadId}");
                });

                Task task = new Task(() => Console.WriteLine("another style of task"));
                task.Start();
            }

            {
                //parallel并行编程,可以启动多个线程并行计算，主线程也会参与计算，节约一个线程。
                //可以通过ParallelOptions轻松控制最大并发数量
                //主线程也会参与计算
                Action[] actions = new Action[] { 
                () =>
                {
                    Console.WriteLine($"This is parallel1 start : {Thread.CurrentThread.ManagedThreadId}");
                    Thread.Sleep(2000);
                    Console.WriteLine($"This is parallel1 end : {Thread.CurrentThread.ManagedThreadId}");
                },
                () =>
                {
                    Console.WriteLine($"This is parallel1 start : {Thread.CurrentThread.ManagedThreadId}");
                    Thread.Sleep(2000);
                    Console.WriteLine($"This is parallel1 end : {Thread.CurrentThread.ManagedThreadId}");
                },
                () =>
                {
                    Console.WriteLine($"This is parallel1 start : {Thread.CurrentThread.ManagedThreadId}");
                    Thread.Sleep(2000);
                    Console.WriteLine($"This is parallel1 end : {Thread.CurrentThread.ManagedThreadId}");
                },
                 () =>
                {
                    Console.WriteLine($"This is parallel1 start : {Thread.CurrentThread.ManagedThreadId}");
                    Thread.Sleep(2000);
                    Console.WriteLine($"This is parallel1 end : {Thread.CurrentThread.ManagedThreadId}");
                },
                };
                Parallel.Invoke(actions);
            }

            {
                //.net Framework 4.5  引入了async await
                //await Task.Run(()=> { });
            }


            Console.WriteLine($"****************btnMutiple_Click End with thread: {Thread.CurrentThread.ManagedThreadId}*********************");
        }

        private void btnTask_Click(object sender, EventArgs e)
        {
            Console.WriteLine();
            Console.WriteLine($"****************btnTask_Click Start with thread: {Thread.CurrentThread.ManagedThreadId}*********************");
            Console.WriteLine("Eleven接了个私活儿。。。。");
            Console.WriteLine(" 沟通大概需求和标准，谈妥价钱。。。。");
            Console.WriteLine("签合同，收取50%的费用");
            Console.WriteLine("需求分析-搭建框架-模块切分。。");
            Console.WriteLine("数据库初步设计。。。");
            Console.WriteLine("挑选学员，组件开发团队");
            Console.WriteLine("开始干活");

            List<Task> taskList = new List<Task>();
           taskList.Add(Task.Run(() =>Coding("高小锐", "Portal")));
           taskList.Add(Task.Run(() => Coding("张永琴", "Client")));
           taskList.Add(Task.Run(() => Coding("高熙颜", "WebChat")));
           taskList.Add(Task.Run(() => Coding("高小兰", "Service")));
           taskList.Add(Task.Run(() => Coding("高苗苗", "SQLServer")));
           taskList.Add(Task.Run(() => Coding("马仪涵", "WebApi")));



            TaskFactory tf = new TaskFactory();
            //CancellationTokenSource cts = new CancellationTokenSource();

            //任一task完成后，启动一个新的task来做回调的内容
            tf.ContinueWhenAny(taskList.ToArray(),t => 
            {
                Console.WriteLine($"xxx 第一个完成任务，领取奖励红包 threadId: {Thread.CurrentThread.ManagedThreadId}");
            });
            //所有的task完成后，启动一个新的task来做回调的内容
            tf.ContinueWhenAll(taskList.ToArray(), tArray => Console.WriteLine($"所有项目完工，收取剩余费用threadId: {Thread.CurrentThread.ManagedThreadId}"));
            //tf.ContinueWhenAll(taskList.ToArray(), tArray => Console.WriteLine($"所有项目完工，收取剩余费用threadId: {Thread.CurrentThread.ManagedThreadId}"), cts.Token);

            //cts.Cancel();
            //cts.Token.Register(() => Console.WriteLine("任务被中途取消"));

            //continue的后续线程，可能是新线程，可能是刚完成任务的线程，还可能是同一个线程，不可能是主线程



            //可以实现不阻塞主界面的问题，1.但是尽量不要线程套线程2.有现成更好的做法
            //Task.Run(() => 
            //{
            //    Task.WaitAny(taskList.ToArray());//阻塞主线程，直到任一任务完成
            //    Console.WriteLine("xxx 第一个完成任务，领取奖励红包");

            //    Task.WaitAll(taskList.ToArray());//阻塞主线程，直到所有的任务完成
            //    Console.WriteLine("项目完成，收取剩余的费用");
            //});


            Console.WriteLine($"****************btnTask_Click End with thread: {Thread.CurrentThread.ManagedThreadId}*********************");
        }

        private void btnThreaqdSafety_Click(object sender, EventArgs e)
        {
            Console.WriteLine();
            Console.WriteLine($"****************btnThreaqdSafety_Click Start with thread: {Thread.CurrentThread.ManagedThreadId}*********************");


            //task里面的i全部是5-------因为循环很快结束，task还没有开始的时候循环已经结束，i++=5
            //如果使用k的话，其实会有五个k，分别是0 1 2 3 4， 所以k会错的。
            //容易出现outover range exception
            //for (int i = 0; i < 5; i++)
            //{
            //    int k = i;
            //    Thread.Sleep(6);
            //    Task.Run(() => 
            //    {
            //        Console.WriteLine($"This is {i} {k} start thread ID:{Thread.CurrentThread.ManagedThreadId}");
            //        Thread.Sleep(2000);
            //        Console.WriteLine($"This is {i} {k} end thread ID:{Thread.CurrentThread.ManagedThreadId}");
            //    });
            //}
            {
                Task.Delay(1000);
            }



            Console.WriteLine($"****************btnThreaqdSafety_Click End with thread: {Thread.CurrentThread.ManagedThreadId}*********************");
        }

    }
}
