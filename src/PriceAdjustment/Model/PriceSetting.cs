using Dr.Common.Data;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PriceAdjustment.Model
{
    /// <summary>
    /// 电价配置
    /// </summary>
    public class PriceSetting
    {
        /// <summary>
        /// 价格字典 序号--模型
        /// </summary>
        public static Dictionary<int, PriceItem> PriceList;


        /// <summary>
        /// 解析
        /// </summary>
        public static void Parse(string data)
        {
            if (!string.IsNullOrWhiteSpace(data))
            {
                PriceList = DataConvert.JsonDeserialize<Dictionary<int, PriceItem>>(data);
            }

            if (PriceList == null)
            {
                PriceList = new Dictionary<int, PriceItem>(12);
            }

            DateTime startMonth = DateTime.Now.AddDays(-1*DateTime.Now.Day+1).AddMonths(-1 * DateTime.Now.Month + 1);

            for (int i = 0; i < 12; i++)
            {
                var order = i + 1;

                if (PriceList.ContainsKey(order))
                {
                    continue;
                }

                var item = new PriceItem
                {
                    Date = startMonth.AddMonths(i)
                };

                PriceList.Add(order, item);
            }

        }

        /// <summary>
        /// 转换
        /// </summary>
        public static string ToData()
        {
            return PriceList.ToJson();
        }

        /// <summary>
        /// 每月电价模型
        /// </summary>
        public class PriceItem
        {
            /// <summary>
            /// 时间
            /// </summary>
            public DateTime Date { get; set; }

            /// <summary>
            /// 电价
            /// </summary>
            public decimal? Price { get; set; }
        }
    }
}
