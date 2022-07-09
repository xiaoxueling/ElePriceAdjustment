using PriceAdjustment.Properties;
using PriceAdjustment.QRCode;
using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PriceAdjustment
{
    public partial class AuthForm : BaseForm
    {
        public AuthForm()
        {
            InitializeComponent();
        }

        private void AuthForm_Load(object sender, EventArgs e)
        {
            if (isAuth())
            {
                this.Exit();
            }
            else
            {
                Settings.Default.APP_AUTH = string.Empty;
                Settings.Default.Save();
            }
        }

        /// <summary>
        /// 认证
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_Auth_Click(object sender, EventArgs e)
        {
            string auth =Tbx_Auth.Text.Trim();
            if (string.IsNullOrEmpty(auth))
            {
                Tbx_Auth.Focus();
                Alert("请先扫描或输入授权码",false);
                return;
            }

            Settings.Default.APP_AUTH = auth;
            Settings.Default.Save();

            if (this.isAuth())
            {
                this.Exit();
            }
            else
            {
                Tbx_Auth.Focus();
                Alert("无效的授权码哦 ~_~", false);
            }
        }

        /// <summary>
        /// 显示设备码
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_Show_Click(object sender, EventArgs e)
        {
            this.Pic_Code.Image = QRCodeHelper.CreateQrcode(getAppCode(), 5, 5, string.Empty, 25, 5, true);
        }

        /// <summary>
        /// 隐藏设备码
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_hide_Click(object sender, EventArgs e)
        {
            this.Pic_Code.Image = Resources.gril;
        }

        /// <summary>
        /// 扫描屏幕二维码
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_QrcodeScan_Click(object sender, EventArgs e)
        {
            Tbx_Auth.Clear();

            var flag = QRCodeHelper.ScanScreenQRCodeWithTip(v => {
                Tbx_Auth.Text = v;
            });

            if (!flag)
            {
                Alert("扫描二维码失败！请保证授权二维码清晰可见！");
            }
        }

        private void Exit()
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
