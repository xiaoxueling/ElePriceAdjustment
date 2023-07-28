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
        public static void Parse(bool reset, DateTime startMonth,int count,string data)
        {
            Dictionary<string, decimal?> oldDic = null;

            if (!string.IsNullOrWhiteSpace(data))
            {
                PriceList = DataConvert.JsonDeserialize<Dictionary<int, PriceItem>>(data);
            }

            if (PriceList == null)
            {
                PriceList = new Dictionary<int, PriceItem>(count);
            }

            try
            {
                if (reset)
                {
                    
                    oldDic = PriceList.Values.GroupBy(m => m.Date.ToString("yyyy-MM")).ToDictionary(k => k.Key, v => v.FirstOrDefault().Price);
                    PriceList = new Dictionary<int, PriceItem>(count);
                }
            }
            catch
            {}

            for (int i = 0; i < count; i++)
            {
                decimal? price = null;
                var date = startMonth.AddMonths(i);

                if (!reset)
                {
                    if (PriceList.ContainsKey(i))
                    {
                        continue;
                    }
                }
                else
                {
                    var key = date.ToString("yyyy-MM");
                    if (oldDic.ContainsKey(key))
                    {
                        price = oldDic[key];
                    }
                }

                var item = new PriceItem
                {
                    Date = date,
                    Price = price
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
