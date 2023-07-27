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
        public static void Parse(DateTime startMonth,int count,string data)
        {
            if (!string.IsNullOrWhiteSpace(data))
            {
                PriceList = DataConvert.JsonDeserialize<Dictionary<int, PriceItem>>(data);
            }

            if (PriceList == null)
            {
                PriceList = new Dictionary<int, PriceItem>(12);
            }

            for (int i = 0; i < count; i++)
            {
                if (PriceList.ContainsKey(i))
                {
                    continue;
                }

                var item = new PriceItem
                {
                    Date = startMonth.AddMonths(i)
                };

                PriceList.Add(i, item);
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
