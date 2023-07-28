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

        /// <summary>
        /// 数字判断
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Price_KeyUp(object sender, KeyEventArgs e)
        {

            try
            {
                if (sender is TextBox)
                {
                    TextBox textBox = (sender as TextBox);
                    string value = textBox.Text;

                    if (!string.IsNullOrWhiteSpace(value) && !value.EndsWith(".") && !value.IsDecimal())
                    {
                        textBox.Text = value.Substring(0, value.Length - 1).ToDecimal().ToString();
                        textBox.SelectionStart = textBox.Text.Length;
                    }
                }
            }
            catch
            { }
        }
    }
}
