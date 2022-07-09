using PriceAdjustment.QRCode;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PriceAdjustment
{
    public partial class CreateAuthForm : BaseForm
    {
        public CreateAuthForm()
        {
            InitializeComponent();
        }

        private void CreateAuthForm_Load(object sender, EventArgs e)
        {
            Cbx_Time.Checked = true;
            Date_limit.Value = DateTime.Now.AddMonths(6);
        }

        private void Btn_Scan_Click(object sender, EventArgs e)
        {
            Tbx_AppCode.Clear();

            var flag = QRCodeHelper.ScanScreenQRCodeWithTip(v => {
                Tbx_AppCode.Text = v;
            });

            if (!flag)
            {
                Alert("扫描二维码失败！请保证设备码二维码清晰可见！",false);
            }
        }

        private void Btn_Create_Click(object sender, EventArgs e)
        {
            try
            {

                var appCode = Tbx_AppCode.Text.Trim();
                if (string.IsNullOrWhiteSpace(appCode))
                {
                    Alert("请先扫描屏幕二维码或输入设备码！",false);
                    return;
                }

                var auth = CreateAuth(appCode, Cbx_Time.Checked ? Date_limit.Value : DateTime.MaxValue);

                Tbx_Auth.Text = auth;

                Pic_Qrcode.Image = QRCodeHelper.CreateQrcode(auth, 10, 5, string.Empty, 20, 2, true);
            }
            catch (Exception ex)
            {
                Alert($"生成失败：{ex.Message}!",false);
            }
        }
    }
}
