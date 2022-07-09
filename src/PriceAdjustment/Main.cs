using Dr.Common.Data;
using PriceAdjustment.DbCore;
using PriceAdjustment.Model;
using PriceAdjustment.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PriceAdjustment
{
    public partial class Main : BaseForm
    {

        private bool run = false;
        private bool initSetting = false;

        public Main()
        {
            InitializeComponent();
        }

        private void Main_Load(object sender, EventArgs e)
        {
            try
            {
                initSetting = true;
                Date_DYH_DEL.Value = Date_SC_DEL.Value = DateTime.Now.AddMonths(-6);

                InitUserSetting();

                if (Cbx_AutoRun.Checked)
                {
                    Task.Factory.StartNew(() => {

                        for (int i = 3; i > 0; i--)
                        {
                            ShowMsg($"{i}秒后开始自动执行...");
                            Thread.Sleep(1000);
                        }
                       
                        Btn_Excute_Click(null,null);
                    });
                }
            }
            catch (Exception ex)
            {
                Alert(ex.Message);
            }
            initSetting = false;
        }

        #region 按钮操作

        /// <summary>
        /// 执行
        /// </summary>
        private void Btn_Excute_Click(object sender, EventArgs e)
        {
            if (run)
            {
                return;
            }

            run = true;

            ShowProgressBar(true);

            Task.Factory.StartNew(() =>
            {

                var flag = false;

                //保存用户配置
                SaveUserSetting();
                ShowProgressBar();

                PriceExcute_DYH dyhExcute = PriceExcute_DYH.INSTANCE;

                dyhExcute.Is_Delete_History = Cbx_DYH_DEL.Checked;
                dyhExcute.Date_Delete_History = Date_DYH_DEL.Value;

                dyhExcute.ShowMsgAction = (m, b) =>
                {
                    ShowMsg($"【多用户】{m}", !b);
                };

                flag = dyhExcute.MoveDbFileToTemp();

                if (!flag)
                {
                    run = false;
                    return;
                }
                ShowProgressBar();

                flag = dyhExcute.ChangePrice();

                if (!flag)
                {
                    run = false;
                    return;
                }

                ShowProgressBar();


                flag = dyhExcute.DeleteHistoryData();

                if (!flag)
                {
                    run = false;
                    return;
                }

                ShowProgressBar();

                flag = dyhExcute.MoveTempDbToTarget();

                if (!flag)
                {
                    run = false;
                    return;
                }

                ShowProgressBar();

                PriceExcute_SC scExcute = PriceExcute_SC.INSTANCE;

                scExcute.Is_Delete_History = Cbx_SC_DEL.Checked;
                scExcute.Date_Delete_History = Date_SC_DEL.Value;

                scExcute.ShowMsgAction = (m, b) =>
                {
                    ShowMsg($"【商场】{m}", !b);
                };

                flag = scExcute.MoveDbFileToTemp();

                if (!flag)
                {
                    run = false;
                    return;
                }

                ShowProgressBar();

                flag = scExcute.ChangePrice();

                if (!flag)
                {
                    run = false;
                    return;
                }

                ShowProgressBar();


                flag = scExcute.DeleteHistoryData();

                if (!flag)
                {
                    run = false;
                    return;
                }

                ShowProgressBar();

                flag = scExcute.MoveTempDbToTarget();

                if (!flag)
                {
                    run = false;
                    return;
                }

                ShowProgressBar();

                if (Settings.Default.AutoExit)
                {
                    for (int i = 3; i >0; i--)
                    {
                        ShowMsg($"{i}秒后开始退出程序...");
                        Thread.Sleep(1000);
                    }
                    Thread.Sleep(1000);
                    Exit();
                }
                run = false;
            });
        }

        /// <summary>
        /// 选择多用户MDB文件
        /// </summary>
        private void Btn_DYH_MDB_Click(object sender, EventArgs e)
        {
            try
            {
                using (OpenFileDialog dialog = new OpenFileDialog())
                {
                    dialog.CheckFileExists = true;
                    dialog.Filter = "MDB文件(*.mdb)|*.mdb";
                    if (dialog.ShowDialog() == DialogResult.OK)
                    {
                        Tbx_DYH_MDB.Text= dialog.FileName;
                    }
                }
            }
            catch (Exception ex)
            {
                ShowMsg("选择多用户MDB文件失败:" + ex.Message,false);
            }
        }

        /// <summary>
        /// 选择多用户MDW文件
        /// </summary>
        private void Btn_DYH_MDW_Click(object sender, EventArgs e)
        {
            try
            {
                using (OpenFileDialog dialog = new OpenFileDialog())
                {
                    dialog.CheckFileExists = true;
                    dialog.Filter = "MDW文件(*.mdw)|*.mdw";
                    if (dialog.ShowDialog() == DialogResult.OK)
                    {
                        Tbx_DYH_MDW.Text = dialog.FileName;
                    }
                }
            }
            catch (Exception ex)
            {
                ShowMsg("选择多用户MDW文件失败:" + ex.Message, false);
            }
        }

        /// <summary>
        /// 选择商场MDB文件
        /// </summary>
        private void Btn_SC_MDB_Click(object sender, EventArgs e)
        {
            try
            {
                using (OpenFileDialog dialog = new OpenFileDialog())
                {
                    dialog.CheckFileExists = true;
                    dialog.Filter = "(*.mdb)|*.mdb";
                    if (dialog.ShowDialog() == DialogResult.OK)
                    {
                        Tbx_SC_MDB.Text = dialog.FileName;
                    }
                }
            }
            catch (Exception ex)
            {
                ShowMsg("选择商场MDB文件失败:" + ex.Message, false);
            }
        }

        /// <summary>
        /// 选择目标文件夹
        /// </summary>
        private void Btn_DB_TARGET_PATH_Click(object sender, EventArgs e)
        {
            try
            {
                using (FolderBrowserDialog dialog = new FolderBrowserDialog())
                {
                    dialog.Description = "请选择目标文件夹";

                    if (dialog.ShowDialog() == DialogResult.OK)
                    {
                        Tbx_DB_TARGET_PATH.Text = dialog.SelectedPath;
                    }
                }
            }
            catch (Exception ex)
            {
                ShowMsg("选择保存位置文件夹失败:" + ex.Message, false);
            }
        }

        /// <summary>
        /// 打开目标文件夹
        /// </summary>
        private void Btn_OPEN_DB_PATH_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(Tbx_DB_TARGET_PATH.Text))
            {
                Process.Start("Explorer", "/select,"+Tbx_DB_TARGET_PATH.Text.Trim());
            }
        }

        private void Btn_SELE_DYH_Click(object sender, EventArgs e)
        {
            try
            {
                using (FolderBrowserDialog dialog = new FolderBrowserDialog())
                {
                    dialog.Description = "请选择目标文件夹";

                    if (dialog.ShowDialog() == DialogResult.OK)
                    {
                        Tbx_DB_TARGET_PATH_DYH.Text = dialog.SelectedPath;
                    }
                }
            }
            catch (Exception ex)
            {
                ShowMsg("选择保存位置文件夹失败:" + ex.Message, false);
            }
        }

        private void Btn_OPEN_DYH_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(Tbx_DB_TARGET_PATH_DYH.Text))
            {
                Process.Start("Explorer", "/select," + Tbx_DB_TARGET_PATH_DYH.Text.Trim());
            }
        }

        private void Btn_SELE_SC_Click(object sender, EventArgs e)
        {
            try
            {
                using (FolderBrowserDialog dialog = new FolderBrowserDialog())
                {
                    dialog.Description = "请选择目标文件夹";

                    if (dialog.ShowDialog() == DialogResult.OK)
                    {
                        Tbx_DB_TARGET_PATH_SC.Text = dialog.SelectedPath;
                    }
                }
            }
            catch (Exception ex)
            {
                ShowMsg("选择保存位置文件夹失败:" + ex.Message, false);
            }
        }

        private void Btn_OPEN_SC_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(Tbx_DB_TARGET_PATH_SC.Text))
            {
                Process.Start("Explorer", "/select," + Tbx_DB_TARGET_PATH_SC.Text.Trim());
            }
        }

        #endregion

        #region 用户自定义变量

        /// <summary>
        /// 初始化用户配置
        /// </summary>
        private void InitUserSetting()
        {

            try
            {
                Cbx_AutoStart.Checked = IsAutoRun();

                Settings settings = Settings.Default;

                Tbx_DB_TARGET_PATH.Text = settings.DB_TARGET_PATH;
                Tbx_DB_TARGET_PATH_DYH.Text = settings.DB_TARGET_PATH_DYH;
                Tbx_DB_TARGET_PATH_SC.Text = settings.DB_TARGET_PATH_SC;
                Cbx_AutoRun.Checked = settings.AutoRun;
                Cbx_AutoExit.Checked = settings.AutoExit;

                Tbx_DYH_MDB.Text = settings.DB_DYH_PATH;
                Tbx_DYH_MDW.Text = settings.DB_DYH_MDW;
                Tbx_DYH_UID.Text = settings.DB_DYH_UID;
                Tbx_DYH_PWD.Text = settings.DB_DYH_PWD;
                Tbx_DYH_Price_New.Text = settings.DYH_PRICE_NEW.ToString();

                Tbx_SC_MDB.Text = settings.DB_SC_PATH;
                Tbx_SC_PWD.Text = settings.DB_SC_PWD;
                Tbx_SC_Price_Old.Text = settings.SC_PRICE_OLD.ToString();
                Tbx_SC_PRICE.Text = settings.SC_PRICE_NEW.ToString();

                PriceSetting.Parse(settings.PRICE_DATA);

                //电价赋值
                foreach (var child in Table_Price.Controls)
                {
                    if(child is Panel)
                    {
                        foreach(var item in ((Panel)child).Controls)
                        {
                            try
                            {
                                if (item is DateTimePicker)
                                {
                                    var dateTimePicker = (DateTimePicker)item;

                                    if (dateTimePicker.Name.StartsWith("Date_"))
                                    {
                                        var order = dateTimePicker.Name.Replace("Date_", string.Empty).ToInt();

                                        var date = PriceSetting.PriceList[order]?.Date;
                                        if (date != null)
                                        {
                                            dateTimePicker.Value = date.Value;
                                        }
                                    }
                                }
                            }
                            catch
                            { }

                            try
                            {
                                if (item is TextBox)
                                {
                                    var textBox = (TextBox)item;

                                    if (textBox.Name.StartsWith("Tbx_Price_"))
                                    {
                                        var order = textBox.Name.Replace("Tbx_Price_", string.Empty).ToInt();

                                        var price = PriceSetting.PriceList[order]?.Price;
                                        textBox.Text = price?.ToString() ?? string.Empty;
                                    }
                                }
                            }
                            catch
                            { }
                        }
                    }
                } 

                ShowMsg("加载用户配置成功");
            }
            catch (Exception ex)
            {
                ShowMsg("加载用户配置失败："+ex.Message,false);
            }
        }

        /// <summary>
        /// 保存用户配置
        /// </summary>
        private void SaveUserSetting()
        {
            try
            {
                Settings settings = Settings.Default;

                settings.DB_TARGET_PATH=Tbx_DB_TARGET_PATH.Text;
                settings.DB_TARGET_PATH_DYH = Tbx_DB_TARGET_PATH_DYH.Text;
                settings.DB_TARGET_PATH_SC = Tbx_DB_TARGET_PATH_SC.Text;
                settings.AutoRun = Cbx_AutoRun.Checked;
                settings.AutoExit = Cbx_AutoExit.Checked;


                settings.DB_DYH_PATH = Tbx_DYH_MDB.Text;
                settings.DB_DYH_MDW = Tbx_DYH_MDW.Text;
                settings.DB_DYH_UID = Tbx_DYH_UID.Text;
                settings.DB_DYH_PWD = Tbx_DYH_PWD.Text;
                settings.SC_PRICE_OLD=Tbx_SC_Price_Old.Text.ToDecimal();
                settings.DYH_PRICE_NEW=Tbx_DYH_Price_New.Text.ToDecimal();

                settings.DB_SC_PATH= Tbx_SC_MDB.Text;
                settings.DB_SC_PWD= Tbx_SC_PWD.Text ;
                settings.SC_PRICE_NEW=Tbx_SC_PRICE.Text.ToDecimal();

                //电价赋值

                foreach (var child in Table_Price.Controls)
                {
                    if (child is Panel)
                    {
                        foreach (var item in ((Panel)child).Controls)
                        {
                            try
                            {
                                if (item is DateTimePicker)
                                {
                                    var dateTimePicker = (DateTimePicker)item;

                                    if (dateTimePicker.Name.StartsWith("Date_"))
                                    {
                                        var order = dateTimePicker.Name.Replace("Date_", string.Empty).ToInt();

                                        if (!PriceSetting.PriceList.ContainsKey(order))
                                        {
                                            PriceSetting.PriceList[order] = new PriceSetting.PriceItem()
                                            {
                                                Date = dateTimePicker.Value
                                            };
                                        }
                                        else
                                        {
                                            PriceSetting.PriceList[order].Date = dateTimePicker.Value;
                                        }
                                    }
                                }
                            }
                            catch
                            { }

                            try
                            {
                                if (item is TextBox)
                                {
                                    var textBox = (TextBox)item;

                                    if (textBox.Name.StartsWith("Tbx_Price_"))
                                    {
                                        var order = textBox.Name.Replace("Tbx_Price_", string.Empty).ToInt();

                                        decimal? price = null;

                                        if (!string.IsNullOrWhiteSpace(textBox.Text))
                                        {
                                            price = textBox.Text.ToDecimal();
                                        }

                                        if (!PriceSetting.PriceList.ContainsKey(order))
                                        {
                                            PriceSetting.PriceList[order] = new PriceSetting.PriceItem()
                                            {
                                                Price = price
                                            };
                                        }
                                        else
                                        {
                                            PriceSetting.PriceList[order].Price = price;
                                        }
                                    }
                                }
                            }
                            catch
                            { }
                        }
                    }
                }

                settings.PRICE_DATA = PriceSetting.ToData();

                settings.Save();

                ShowMsg("保存用户配置成功");
            }
            catch (Exception ex)
            {
                ShowMsg("保存用户配置失败：" + ex.Message, false);
            }
        }

        private void Cbx_AutoStart_CheckedChanged(object sender, EventArgs e)
        {
            if (initSetting)
            {
                return;
            }
            var check = Cbx_AutoStart.Checked;
            var flag = base.SetAutoRun(check);
            ShowMsg($"{(check ? "设置" : "取消")}开机自启{(flag ? "成功" : "失败")}", flag);
        }

        #endregion

        #region 消息提示
        public void ToolTip(string msg, bool success = true)
        {
            Tbx_Status.ForeColor = success ? Color.MediumSeaGreen : Color.Red;
            Tbx_Status.Text = msg;
        }

        public override void AppendText(string msg, bool success = true)
        {
            Tbx_Info.AppendText(Environment.NewLine);
            Tbx_Info.SelectionColor = success ? Color.MediumSeaGreen : Color.Red;
            Tbx_Info.AppendText($"[{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}] {msg}");
            Tbx_Info.SelectionStart = Tbx_Info.Text.Length;
            Tbx_Info.ScrollToCaret();
        } 

        /// <summary>
        /// 进度显示
        /// </summary>
        private void ShowProgressBar(bool reset=false)
        {
            try
            {
                Thread.Sleep(500);
                if (reset)
                {
                    ProgressBar.Value = 0;
                }
                else
                {
                    Task.Factory.StartNew(() =>
                    {
                        int count = 0;
                        while (count++ < 10)
                        {
                            BeginInvoke(new Action(() =>
                            {
                                if(ProgressBar.Value < ProgressBar.Maximum)
                                {
                                    ProgressBar.Value++;
                                }
                            }));
                            try
                            {
                                Thread.Sleep(50);
                            }
                            catch
                            { }
                        }
                    });
                }
            }
            catch
            {}
        }
        #endregion

        #region 窗体改变事件

        private void Main_SizeChanged(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Minimized)
            {
                ChangeFormState(false);
            }
        }

        private void Notify_DoubleClick(object sender, EventArgs e)
        {
            ChangeFormState();
        }

        private void MenuItem_Show_Click(object sender, EventArgs e)
        {
            ChangeFormState();
        }

        private void MenuItem_Help_Click(object sender, EventArgs e)
        {
            Alert(Settings.Default.Help);
        }

        private void MenuItem_About_Click(object sender, EventArgs e)
        {
            Alert(Resources.About);
        }

        private void MenuItem_Exit_Click(object sender, EventArgs e)
        {
            Notify.Visible = false;
            this.Dispose();
            this.Close();
        }

        private void ChangeFormState(bool show = true)
        {
            if (show)
            {
                if (this.WindowState == FormWindowState.Minimized)
                {
                    this.Show();
                    this.ShowInTaskbar = true;
                    this.WindowState = FormWindowState.Normal;
                    BringToFront();
                }
            }
            else
            {
                this.Hide();
                this.ShowInTaskbar = false;
                this.WindowState = FormWindowState.Minimized;
                Notify.BalloonTipText = "双击通知图标显示主页面";
                Notify.ShowBalloonTip(1000);
            }
        }

        #endregion

        /// <summary>
        /// 数字判断
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Price_KeyUp(object sender, KeyEventArgs e)
        {

            try
            {
                if (sender is TextBox)
                {
                    TextBox textBox = (sender as TextBox);
                    string value = textBox.Text;

                    if (!string.IsNullOrWhiteSpace(value)&&!value.EndsWith(".")&&!value.IsDecimal())
                    {
                        textBox.Text = value.Substring(0,value.Length-1).ToDecimal().ToString();
                        textBox.SelectionStart = textBox.Text.Length;
                    }
                }
            }
            catch
            { }
        }

        private void Exit()
        {
            try
            {
                BeginInvoke(new Action(() =>
                {
                    this.Close();
                    Application.Exit();
                }));
            }
            catch { }
        }
    }
}
