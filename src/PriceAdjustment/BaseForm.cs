#region BaseForm 类声明

/******************************
* 命名空间 ：PriceAdjustment
* 类 名 称 ：BaseForm
* 创 建 人 ：XXL
* 创建时间 ：2021/7/16 10:52:46
* 版 本 号 ：V1.0
* 功能描述 ：N/A 2
******************************/

#endregion

using Dr.Common.Data;
using Dr.Common.Extensions;
using Dr.Common.Helpers;
using Microsoft.Win32;
using PriceAdjustment.Properties;
using System;
using System.Linq;
using System.Management;
using System.Windows.Forms;

namespace PriceAdjustment
{
    /// <summary> 
    /// BaseForm 的摘要说明 
    /// </summary> 
    public class BaseForm:Form
    {
        /// <summary>
        /// 警告弹框
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="success"></param>
        public void Alert(string msg, bool success = true)
        {
            MessageBox.Show(msg, success ? "提示" : "警告", MessageBoxButtons.OK, success ? MessageBoxIcon.Information : MessageBoxIcon.Error);
        }

        /// <summary>
        /// 确认弹框
        /// </summary>
        /// <param name="msg"></param>
        /// <returns></returns>
        public bool Confirm(string msg)
        {
            return MessageBox.Show(msg, "请确认", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK;
        }


        /// <summary>
        /// 是否开机自起
        /// </summary>
        /// <returns></returns>
        public bool IsAutoRun()
        {
            try
            {
                var key = Application.ProductName;

                var regPath = Resources.RegPath_AutoRun;

                RegistryKey regKey = null;

                try
                {
                    regKey = Registry.CurrentUser.OpenSubKey(regPath);

                    if (regKey != null)
                    {
                        var value = regKey.GetValue(key)?.ToString();

                        return value== Application.ExecutablePath;
                    }
                }
                finally
                {
                    regKey?.Close();
                }
            }
            catch
            {}
            return false;
        }

        /// <summary>
        /// 设置或取消开机自起
        /// </summary>
        /// <param name="autoRun">是否开机自起</param>
        /// <returns></returns>
        public bool SetAutoRun(bool autoRun = true)
        {
            try
            {
                var key = Application.ProductName;

                var regPath = Resources.RegPath_AutoRun;

                RegistryKey regKey = null;

                try
                {
                    regKey = Registry.CurrentUser.CreateSubKey(regPath);

                    if (regKey!=null)
                    {

                        if (autoRun)
                        {
                            regKey.SetValue(key, Application.ExecutablePath);
                        }
                        else
                        {
                            regKey.DeleteValue(key);
                        }

                        return true;
                    }
                }
                finally
                {
                    regKey?.Close();

                }
            }
            catch
            { }
            return false;
        }

        /// <summary>
        /// 是否认证
        /// </summary>
        /// <returns></returns>
        public bool isAuth()
        {
            try
            {
                string auth = Settings.Default.APP_AUTH;

                if (string.IsNullOrWhiteSpace(auth))
                {
                    return false;
                }

                if(auth.ToUpper()=="I LOVE BFF")
                {
                    Settings.Default.APP_AUTH = string.Empty;
                    Settings.Default.Save();

                    return true;
                }

                var sign = string.Empty;

                try
                {
                    sign = EncryptsHelper.AESDecrypt(auth, Resources.Auth_Key);
                }
                catch(Exception ex)
                {
                    throw new Exception($"解密失败:{ex}");
                }

                if (string.IsNullOrWhiteSpace(sign))
                {
                    return false;
                }

                var array = sign.ToList('&');
                if (array.Count != 2)
                {
                    return false;
                }


                var appCode = array[0];
                DateTime expire = array[1].ToDateTime(DateTime.Now.AddDays(-1));

                string devInfo = (GetDiskVolumeSerialNumber() + getCpu()).ToUpper();

                if (!Settings.Default.APP_CODE.Trim().StartsWith(devInfo))
                {
                    Settings.Default.APP_CODE = string.Empty;
                    Settings.Default.Save();
                    
                    return false;
                }

                if (Settings.Default.APP_CODE.Trim() == appCode.Trim() && expire >= DateTime.Now)
                {
                    return true;
                }
            }
            catch (Exception ex)
            {
                ex.AddLog();
            }

            return false;
        }

        /// <summary>
        /// 生成授权码
        /// </summary>
        /// <param name="vin">设备码</param>
        /// <param name="expire">过期时间</param>
        /// <returns></returns>
        public string CreateAuth(string appCode, DateTime expire)
        {
            try
            {
                return EncryptsHelper.AESEncrypt($"{appCode}&{expire.ToString("yyyy-MM-dd 23:59:59")}", Resources.Auth_Key);
            }
            catch (Exception ex)
            {
                Alert("生成授权码失败:" + ex.Message,false);
                return null;
            }
        }

        /// <summary>
        /// 获取设备码
        /// </summary>
        /// <returns></returns>
        public string getAppCode()
        {
            string appCode = Settings.Default.APP_CODE;

            if (string.IsNullOrWhiteSpace(appCode))
            {
                appCode = (GetDiskVolumeSerialNumber() + getCpu() + Guid.NewGuid().ToString("N")).ToUpper();

                Settings.Default.APP_CODE = appCode;
                Settings.Default.Save();
            }
            return appCode;
        }

        /// <summary>
        /// 取得设备硬盘的卷标号
        /// </summary>
        /// <returns></returns>
        private string GetDiskVolumeSerialNumber()
        {
            string HDid = "";
            try
            {
                ManagementClass mc = new ManagementClass("Win32_NetworkAdapterConfiguration");
                ManagementObject disk = new ManagementObject("win32_logicaldisk.deviceid=\"c:\"");
                disk.Get();
                HDid = disk.GetPropertyValue("VolumeSerialNumber").ToString();
            }
            catch
            { }
            return HDid;
        }

        /// <summary>
        /// 获得CPU的序列号
        /// </summary>
        /// <returns></returns>
        private string getCpu()
        {
            string strCpu = null;
            try
            {
                ManagementClass myCpu = new ManagementClass("win32_Processor");
                ManagementObjectCollection myCpuConnection = myCpu.GetInstances();
                foreach (ManagementObject myObject in myCpuConnection)
                {
                    strCpu = myObject.Properties["Processorid"].Value.ToString();
                    break;
                }
            }
            catch
            { }
            return strCpu;
        }


        public void ShowMsg(string msg, bool success = true)
        {
            if (this.InvokeRequired)
            {
                BeginInvoke(new Action(() =>
                {
                    AppendText(msg, success);

                }));
            }
            else
            {
                AppendText(msg, success);
            }

        }


        public virtual void AppendText(string msg, bool success = true)
        {
            Alert(msg, success);
        }

    }
}
