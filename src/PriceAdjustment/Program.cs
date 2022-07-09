using Dr.Common.Extensions;
using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;

namespace PriceAdjustment
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            try
            {
                Mutex mutex = new Mutex(true, Application.ProductName+"__");
                if (mutex.WaitOne(0, false))
                {
                    Run();
                }
                else
                {
                    try
                    {
                        Show();
                    }
                    catch (Exception e)
                    {
                        e.AddLog();
                        MessageBox.Show("程序已经在运行!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        Application.Exit();
                    }
                }
            }
            catch (Exception ex)
            {
                ex.AddLog();
                Run();
            }
        }

        static void Run()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            //处理未捕获的异常
            Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);
            //处理UI线程异常
            Application.ThreadException += (o, e) =>
            {
                e.Exception.AddLog();
            };
            //处理非UI线程异常
            AppDomain.CurrentDomain.UnhandledException += (o, e) => {
                (e.ExceptionObject as Exception).AddLog();
            };

            //Application.Run(new CreateAuthForm());

            AuthForm authForm = new AuthForm();
            if (authForm.isAuth())
            {
                Application.Run(new Main());
            }
            else
            {
                DialogResult result = authForm.ShowDialog();
                if (result == DialogResult.OK)
                {
                    Application.Run(new Main());
                }
            }
        }

        static void Show()
        {
            Process exist = null;
            //获取存在的进程
            Process current = Process.GetCurrentProcess();
            Process[] processes = Process.GetProcessesByName(current.ProcessName);
            foreach (Process process in processes)
            {
                if (process.Id != current.Id)
                {
                    if (process.MainModule.FileName == current.MainModule.FileName)
                    {
                        exist = process;
                    }
                }
            }
            //正常显示窗口
            ShowWindowAsync(exist.MainWindowHandle, 1);
            //将实例放置到前台
            SetForegroundWindow(exist.MainWindowHandle);
        }

        [DllImport("User32.dll")]
        private static extern bool ShowWindowAsync(IntPtr hWnd, int cmdShow);

        [DllImport("User32.dll")]
        private static extern bool SetForegroundWindow(IntPtr hWnd);
    }
}
