#region PriceExcute_SC 类声明

/******************************
* 命名空间 ：PriceAdjustment.DbCore
* 类 名 称 ：PriceExcute_SC
* 创 建 人 ：XXL
* 创建时间 ：2021/7/16 17:21:30
* 版 本 号 ：V1.0
* 功能描述 ：N/A 2
******************************/

#endregion

using PriceAdjustment.Model;
using PriceAdjustment.Properties;
using System;
using System.Collections.Generic;

namespace PriceAdjustment.DbCore
{
    /// <summary> 
    /// 商场数据处理
    /// </summary> 
    public class PriceExcute_SC : PriceExcuteBase
    {
        public static PriceExcute_SC INSTANCE => new Lazy<PriceExcute_SC>().Value;

        public override DB_TYPE dbType => DB_TYPE.SC;

        public override List<string> getChangePriceSql()
        {
            List<string> sqls = new List<string>();

            DateTime? beforeTime =null;

            foreach (var item in PriceSetting.PriceList.Values)
            {
                try
                {
                    if (item.Price == null || item.Price <= 0 || item.Date > DateTime.Now)
                    {
                        continue;
                    }

                    var startTime = item.Date.AddDays(-1 * item.Date.Day + 1);
                    var endTime = startTime.AddMonths(1);

                    if (beforeTime==null || startTime < beforeTime)
                    {
                        beforeTime = startTime;
                    }

                    sqls.Add($"UPDATE T_SELL SET CURPRICE=CURPRICE/{Settings.Default.SC_PRICE_OLD}*{item.Price},BUYNUM=CINT(BUYNUM*{Settings.Default.SC_PRICE_OLD}*{item.Price}),WRNUM=CINT(BUYNUM*{Settings.Default.SC_PRICE_OLD}*{item.Price}) WHERE BUYDATETIME >= #{startTime.ToString("yyyy-MM-dd")}# AND BUYDATETIME < #{endTime.ToString("yyyy-MM-dd")}#");
                }
                catch
                { }
            }

            if (beforeTime != null)
            {
                sqls.Add($"UPDATE T_SELL SET CURPRICE=CURPRICE/{Settings.Default.SC_PRICE_OLD}*{Settings.Default.SC_PRICE_NEW},BUYNUM=CINT(BUYNUM*{Settings.Default.SC_PRICE_OLD}*{Settings.Default.SC_PRICE_NEW}),WRNUM=CINT(BUYNUM*{Settings.Default.SC_PRICE_OLD}*{Settings.Default.SC_PRICE_NEW}) WHERE BUYDATETIME < #{beforeTime.Value.ToString("yyyy-MM-dd")}#");
            }

            sqls.Add($"UPDATE T_PRICE SET PRICECONTENT=PRICECONTENT/{Settings.Default.SC_PRICE_OLD}*{Settings.Default.SC_PRICE_NEW},PRICEVALUE=PRICEVALUE/{Settings.Default.SC_PRICE_OLD}*{Settings.Default.SC_PRICE_NEW}");


            return sqls;
        }

        public override string getDeleteHistoryDataSql()
        {
            return $"DELETE FROM T_SELL WHERE BUYDATETIME < #{Date_Delete_History.ToString("yyyy-MM-dd")}#";
        }
    }
}
