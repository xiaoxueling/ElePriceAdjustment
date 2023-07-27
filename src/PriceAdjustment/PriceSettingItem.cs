using System;
using System.Windows.Forms;
using Dr.Common.Data;

namespace PriceAdjustment
{
    public partial class PriceSettingItem : UserControl
    {
        public PriceSettingItem()
        {
            InitializeComponent();
        }

        public void SetValue(DateTime dateTime, string price)
        {
            this.Date_Time.Value = dateTime;
            this.Tbx_Price.Text = price;
        }

        public DateTime Date => Date_Time.Value;

        public decimal? Price
        {
            get
            {
                decimal? price = null;

                if (!string.IsNullOrWhiteSpace(Tbx_Price.Text))
                {
                    price = Tbx_Price.Text.ToDecimal();
                }
                return price;
            }
        }

        public int Order { get; set; }
    }
}
