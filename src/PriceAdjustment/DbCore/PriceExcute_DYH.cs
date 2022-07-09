#region PriceExcute_DYH 类声明

/******************************
* 命名空间 ：PriceAdjustment.DbCore
* 类 名 称 ：PriceExcute_DYH
* 创 建 人 ：XXL
* 创建时间 ：2021/7/16 17:21:21
* 版 本 号 ：V1.0
* 功能描述 ：N/A 2
******************************/

#endregion

using PriceAdjustment.Model;
using PriceAdjustment.Properties;
using System;
using System.Collections.Generic;
using System.IO;

namespace PriceAdjustment.DbCore
{
    /// <summary> 
    /// 多用户数据处理
    /// </summary> 
    public class PriceExcute_DYH:PriceExcuteBase
    {
        public static PriceExcute_DYH INSTANCE => new Lazy<PriceExcute_DYH>().Value;

        public override DB_TYPE dbType => DB_TYPE.DYH;

        public override List<string> getChangePriceSql()
        {
            List<string> sqls = new List<string>();

            DateTime? beforeTime = null;


            foreach (var item in PriceSetting.PriceList.Values)
            {
                try
                {

                    if (item.Price == null || item.Price <= 0 || item.Date>DateTime.Now)
                    {
                        continue;
                    }


                    var startTime = item.Date.AddDays(-1 * item.Date.Day + 1);
                    var endTime = startTime.AddMonths(1);

                    if (beforeTime == null || startTime < beforeTime)
                    {
                        beforeTime = startTime;
                    }

                    sqls.Add($"UPDATE [SaleRecord] SET 电价 = {item.Price}, 购电量 = 购电金额/{item.Price} WHERE 购电日期 >= #{startTime.ToString("yyyy-MM-dd")}# AND 购电日期 < #{endTime.ToString("yyyy-MM-dd")}#");
                }
                catch
                {}
            }

            if (beforeTime != null)
            {
                sqls.Add($"UPDATE [SaleRecord] SET 电价 = {Settings.Default.DYH_PRICE_NEW}, 购电量 = 购电金额/{Settings.Default.DYH_PRICE_NEW} WHERE 购电日期 < #{beforeTime.Value.ToString("yyyy-MM-dd")}#");
            }

            sqls.Add($"UPDATE [User] SET 电价 = {Settings.Default.DYH_PRICE_NEW}, 末次购电量 = 末次购电量*电价/{Settings.Default.DYH_PRICE_NEW}");
            sqls.Add($"UPDATE ElecPrice SET 单价={Settings.Default.DYH_PRICE_NEW}");

            return sqls;
        }

        public override string getDeleteHistoryDataSql()
        {
            return $"DELETE * FROM [SaleRecord] WHERE 购电日期 < #{Date_Delete_History.ToString("yyyy-MM-dd")}#";
        }
    }
}
