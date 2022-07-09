#region QRCodeHelper 类声明

/******************************
* 命名空间 ：PriceAdjustment.QRCode
* 类 名 称 ：QRCodeHelper
* 创 建 人 ：XXL
* 创建时间 ：2021/7/16 09:07:41
* 版 本 号 ：V1.0
* 功能描述 ：N/A 2
******************************/

#endregion

using Dr.Common.Extensions;
using QRCoder;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ZXing;
using ZXing.Common;
using ZXing.QrCode;
using ZXing.QrCode.Internal;

namespace PriceAdjustment.QRCode
{
    /// <summary> 
    /// QRCodeHelper 的摘要说明 
    /// </summary> 
    public class QRCodeHelper
    {
        /// <summary>
        /// 生成二维码
        /// </summary>
        /// <param name="msg">信息</param>
        /// <param name="version">版本 1 ~ 40</param>
        /// <param name="pixel">像素点大小</param>
        /// <param name="icon_path">图标路径</param>
        /// <param name="icon_size">图标尺寸</param>
        /// <param name="icon_border">图标边框厚度</param>
        /// <param name="white_edge">二维码白边</param>
        /// <returns>位图</returns>
        public static Bitmap CreateQrcode(string msg, int version, int pixel, string icon_path, int icon_size, int icon_border, bool white_edge)
        {
            QRCodeGenerator code_generator = new QRCodeGenerator();

            QRCodeData code_data = code_generator.CreateQrCode(msg, QRCodeGenerator.ECCLevel.Q, true, false, QRCodeGenerator.EciMode.Utf8, version);

            QRCoder.QRCode code = new QRCoder.QRCode(code_data);

            Bitmap icon = null;

            if (!string.IsNullOrEmpty(icon_path))
            {
                try
                {
                    icon = new Bitmap(icon_path);
                }
                catch{}

            }
            else
            {
                icon = Properties.Resources.gril;
            }

            Bitmap bmp = code.GetGraphic(pixel, Color.Black, Color.White, icon, icon_size, icon_border, white_edge);

            return bmp;
        }


        /// <summary>
        /// 生成二维码
        /// </summary>
        /// <param name="text">内容</param>
        /// <param name="width">宽度</param>
        /// <param name="height">高度</param>
        /// <returns></returns>
        public static Bitmap CreateQrcode(string text, int width, int height)
        {
            BarcodeWriter writer = new BarcodeWriter();
            writer.Format = BarcodeFormat.QR_CODE;
            QrCodeEncodingOptions options = new QrCodeEncodingOptions()
            {
                DisableECI = true,//设置内容编码
                CharacterSet = "UTF-8",  //设置二维码的宽度和高度
                Width = width,
                Height = height,
                Margin = 1//设置二维码的边距,单位不是固定像素
            };

            writer.Options = options;
            Bitmap map = writer.Write(text);
            return map;
        }

        /// <summary>
        /// 解析二维码图片
        /// </summary>
        /// <param name="qrCodeFilePath"></param>
        /// <param name="action"></param>
        public static void QRCodeFileDecode(string qrCodeFilePath, Action<bool, string> action)
        {
            try
            {
                Bitmap target = new Bitmap(Image.FromFile(qrCodeFilePath));
                var source = new BitmapLuminanceSource(target);
                var bitmap = new BinaryBitmap(new HybridBinarizer(source));

                Dictionary<DecodeHintType, object> hints = new Dictionary<DecodeHintType, object>
                {
                    { DecodeHintType.CHARACTER_SET, "UTF-8" }
                };

                QRCodeReader reader = new QRCodeReader();
                var result = reader.decode(bitmap, hints);
                if (result != null)
                {
                    action?.Invoke(true, result.Text);
                }
            }
            catch(Exception ex)
            {
                action?.Invoke(false, ex.Message);
            }

            action?.Invoke(false, string.Empty);
        }


        /// <summary>
        /// 扫码屏幕二维码
        /// </summary>
        /// <param name="action">结果回调</param>
        public static bool ScanScreenQRCodeWithTip(Action<string> action)
        {
            bool result = false;
            try
            {
                foreach (Screen screen in Screen.AllScreens)
                {
                    using (Bitmap fullImage = new Bitmap(screen.Bounds.Width,
                                                        screen.Bounds.Height))
                    {
                        using (Graphics g = Graphics.FromImage(fullImage))
                        {
                            g.CopyFromScreen(screen.Bounds.X,
                                             screen.Bounds.Y,
                                             0, 0,
                                             fullImage.Size,
                                             CopyPixelOperation.SourceCopy);
                        }
                        for (int i = 0; i < 100; i++)
                        {
                            double stretch;
                            Rectangle cropRect = GetScanRect(fullImage.Width, fullImage.Height, i, out stretch);
                            if (cropRect.Width == 0)
                                break;

                            string url;
                            Rectangle rect;
                            if (stretch == 1 ? ScanQRCode(screen, fullImage, cropRect, out url, out rect) : ScanQRCodeStretch(screen, fullImage, cropRect, stretch, out url, out rect))
                            {
                                if (!string.IsNullOrWhiteSpace(url))
                                {
                                    result = true;

                                    QRCodeSplashForm splash = new QRCodeSplashForm();
                                    splash.FormClosed += (o, e) =>
                                    {
                                        action?.Invoke(url);
                                    };

                                    splash.Location = new Point(screen.Bounds.X, screen.Bounds.Y);
                                    double dpi = Screen.PrimaryScreen.Bounds.Width / screen.Bounds.Width;
                                    splash.TargetRect = new Rectangle(
                                        (int)(rect.Left * dpi + screen.Bounds.X),
                                        (int)(rect.Top * dpi + screen.Bounds.Y),
                                        (int)(rect.Width * dpi),
                                        (int)(rect.Height * dpi));
                                    splash.Size = new Size(fullImage.Width, fullImage.Height);
                                    splash.Show();
                                }
                            }
                        }
                    }
                }
            }
            catch { }
            return result;
        }


        private static Rectangle GetScanRect(int width, int height, int index, out double stretch)
        {
            stretch = 1;
            if (index < 5)
            {
                const int div = 5;
                int w = width * 3 / div;
                int h = height * 3 / div;
                Point[] pt = new Point[5] {
                    new Point(1, 1),

                    new Point(0, 0),
                    new Point(0, 2),
                    new Point(2, 0),
                    new Point(2, 2),
                };
                return new Rectangle(pt[index].X * width / div, pt[index].Y * height / div, w, h);
            }
            {
                const int base_index = 5;
                if (index < base_index + 6)
                {
                    double[] s = new double[] {
                        1,
                        2,
                        3,
                        4,
                        6,
                        8
                    };
                    stretch = 1 / s[index - base_index];
                    return new Rectangle(0, 0, width, height);
                }
            }
            {
                const int base_index = 11;
                if (index < base_index + 8)
                {
                    const int hdiv = 7;
                    const int vdiv = 5;
                    int w = width * 3 / hdiv;
                    int h = height * 3 / vdiv;
                    Point[] pt = new Point[8] {
                        new Point(1, 1),
                        new Point(3, 1),

                        new Point(0, 0),
                        new Point(0, 2),

                        new Point(2, 0),
                        new Point(2, 2),

                        new Point(4, 0),
                        new Point(4, 2),
                    };
                    return new Rectangle(pt[index - base_index].X * width / hdiv, pt[index - base_index].Y * height / vdiv, w, h);
                }
            }
            return new Rectangle(0, 0, 0, 0);
        }

        private static bool ScanQRCode(Screen screen, Bitmap fullImage, Rectangle cropRect, out string url, out Rectangle rect)
        {
            using (Bitmap target = new Bitmap(cropRect.Width, cropRect.Height))
            {
                using (Graphics g = Graphics.FromImage(target))
                {
                    g.DrawImage(fullImage, new Rectangle(0, 0, cropRect.Width, cropRect.Height),
                                    cropRect,
                                    GraphicsUnit.Pixel);
                }
                var source = new BitmapLuminanceSource(target);
                var bitmap = new BinaryBitmap(new HybridBinarizer(source));
                QRCodeReader reader = new QRCodeReader();
                var result = reader.decode(bitmap);
                if (result != null)
                {
                    url = result.Text;
                    double minX = Int32.MaxValue, minY = Int32.MaxValue, maxX = 0, maxY = 0;
                    foreach (ResultPoint point in result.ResultPoints)
                    {
                        minX = Math.Min(minX, point.X);
                        minY = Math.Min(minY, point.Y);
                        maxX = Math.Max(maxX, point.X);
                        maxY = Math.Max(maxY, point.Y);
                    }
                    //rect = new Rectangle((int)minX, (int)minY, (int)(maxX - minX), (int)(maxY - minY));
                    rect = new Rectangle(cropRect.Left + (int)minX, cropRect.Top + (int)minY, (int)(maxX - minX), (int)(maxY - minY));
                    return true;
                }
            }
            url = "";
            rect = new Rectangle();
            return false;
        }

        private static bool ScanQRCodeStretch(Screen screen, Bitmap fullImage, Rectangle cropRect, double mul, out string url, out Rectangle rect)
        {
            using (Bitmap target = new Bitmap((int)(cropRect.Width * mul), (int)(cropRect.Height * mul)))
            {
                using (Graphics g = Graphics.FromImage(target))
                {
                    g.DrawImage(fullImage, new Rectangle(0, 0, target.Width, target.Height),
                                    cropRect,
                                    GraphicsUnit.Pixel);
                }
                var source = new BitmapLuminanceSource(target);
                var bitmap = new BinaryBitmap(new HybridBinarizer(source));
                QRCodeReader reader = new QRCodeReader();
                var result = reader.decode(bitmap);
                if (result != null)
                {
                    url = result.Text;
                    double minX = Int32.MaxValue, minY = Int32.MaxValue, maxX = 0, maxY = 0;
                    foreach (ResultPoint point in result.ResultPoints)
                    {
                        minX = Math.Min(minX, point.X);
                        minY = Math.Min(minY, point.Y);
                        maxX = Math.Max(maxX, point.X);
                        maxY = Math.Max(maxY, point.Y);
                    }
                    rect = new Rectangle(cropRect.Left + (int)(minX / mul), cropRect.Top + (int)(minY / mul), (int)((maxX - minX) / mul), (int)((maxY - minY) / mul));
                    return true;
                }
            }
            url = "";
            rect = new Rectangle();
            return false;
        }

    }
}
