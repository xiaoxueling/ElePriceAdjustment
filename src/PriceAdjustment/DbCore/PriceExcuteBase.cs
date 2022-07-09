#region PriceExcuteBase 类声明

/******************************
* 命名空间 ：PriceAdjustment.DbCore
* 类 名 称 ：PriceExcuteBase
* 创 建 人 ：XXL
* 创建时间 ：2021/7/17 11:35:36
* 版 本 号 ：V1.0
* 功能描述 ：N/A 2
******************************/

#endregion

using PriceAdjustment.Properties;
using System;
using System.Collections.Generic;
using System.IO;

namespace PriceAdjustment.DbCore
{
    /// <summary> 
    /// PriceExcuteBase 的摘要说明 
    /// </summary> 
    public abstract class PriceExcuteBase
    {

        public string _TEMP = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "TEMP");

        public Action<string, bool> ShowMsgAction = null;

        private string db_Temp = string.Empty;

        public bool Is_Delete_History { get; set; }

        public DateTime Date_Delete_History { get; set; }

        public abstract DB_TYPE dbType { get; }

        public abstract List<string> getChangePriceSql();

        public abstract string getDeleteHistoryDataSql();

        /// <summary>
        /// mdb文件复制到工作目录
        /// </summary>
        /// <returns></returns>
        public bool MoveDbFileToTemp()
        {
            try
            {
                string mdb_path = dbType == DB_TYPE.DYH ? Settings.Default.DB_DYH_PATH : Settings.Default.DB_SC_PATH;

                FileInfo mdbInfo = new FileInfo(mdb_path);

                if (!mdbInfo.Exists)
                {
                    throw new Exception("未找到mdb文件,请确认文件位置！");
                }

                ToolTip("已找到mdb文件,正在进行复制...");

                DirectoryInfo directoryInfo = new DirectoryInfo(_TEMP);
                if (!directoryInfo.Exists)
                {
                    directoryInfo.Create();
                }

                db_Temp = Path.Combine(_TEMP, dbType == DB_TYPE.DYH ? "DYH.MDB" : "SC.MDB");

                FileInfo tempInfo = mdbInfo.CopyTo(db_Temp, true);

                if (!tempInfo.Exists)
                {
                    throw new Exception("复制失败，请重试！");
                }

                ToolTip("复制mdb文件到工作目录成功！");

                return true;
            }
            catch (Exception ex)
            {
                ToolTip("复制mdb文件到工作目录失败：" + ex.Message, true);
            }
            return false;
        }


        /// <summary>
        /// 调整价格
        /// </summary>
        /// <returns></returns>
        public bool ChangePrice()
        {
            try
            {
                DbHelper.SetConnection(getConnectionString());

                DbHelper.ExecuteSqlTran(getChangePriceSql());

                ToolTip("调整价格成功！");
                return true;
            }
            catch (Exception ex)
            {
                ToolTip("调整价格失败：" + ex.Message, true);
            }
            return false;
        }

        /// <summary>
        /// 删除历史收费记录
        /// </summary>
        /// <returns></returns>
        public bool DeleteHistoryData()
        {
            if (!Is_Delete_History)
            {
                return true;
            }

            try
            {
                DbHelper.SetConnection(getConnectionString());

                DbHelper.ExecuteSql(getDeleteHistoryDataSql());

                ToolTip($"删除[{this.Date_Delete_History.ToString("yyyy年MM月dd日")}]之前历史收费记录成功！");

                return true;
            }
            catch (Exception ex)
            {
                ToolTip("删除历史收费记录失败：" + ex.Message, true);
            }
            return false;
        }


        /// <summary>
        /// temp 里 mdb文件复制到目标目录
        /// </summary>
        /// <returns></returns>
        public bool MoveTempDbToTarget()
        {
            try
            {
                FileInfo mdbInfo = new FileInfo(db_Temp);

                if (!mdbInfo.Exists)
                {
                    throw new Exception("处理失败,请重试！");
                }

                ToolTip("正在复制到目标目录...");

                string target_path = Path.Combine(Settings.Default.DB_TARGET_PATH, dbType == DB_TYPE.DYH ? "多用户" : "商场");

                if(dbType == DB_TYPE.DYH && !string.IsNullOrWhiteSpace(Settings.Default.DB_TARGET_PATH_DYH))
                {
                    target_path = Settings.Default.DB_TARGET_PATH_DYH;
                }

                if (dbType == DB_TYPE.SC && !string.IsNullOrWhiteSpace(Settings.Default.DB_TARGET_PATH_SC))
                {
                    target_path = Settings.Default.DB_TARGET_PATH_SC;
                }

                DirectoryInfo directoryInfo = new DirectoryInfo(target_path);
                if (!directoryInfo.Exists)
                {
                    directoryInfo.Create();
                }

                string target_file = Path.Combine(target_path, dbType == DB_TYPE.DYH ? "DB.MDB" : "db.mdb");

                FileInfo tempInfo = mdbInfo.CopyTo(target_file, true);

                if (!tempInfo.Exists)
                {
                    throw new Exception("复制失败，请重试！");
                }

                ToolTip("复制到目标目录成功！");

                return true;
            }
            catch (Exception ex)
            {
                ToolTip("复制到目标目录失败：" + ex.Message, true);
            }
            return false;
        }


        private string getConnectionString()
        {

            if (dbType == DB_TYPE.DYH)
            {
                return string.Format(Settings.Default.DB_DYH_STR, db_Temp, Settings.Default.DB_DYH_MDW, Settings.Default.DB_DYH_UID, Settings.Default.DB_DYH_PWD);
            }
            else
            {
                return string.Format(Settings.Default.DB_SC_STR, db_Temp, Settings.Default.DB_SC_PWD);
            }
        }

        private void ToolTip(string msg, bool error = false)
        {
            ShowMsgAction?.Invoke(msg, error);
        }


        public enum DB_TYPE
        {
            DYH,
            SC
        }
    }
}
