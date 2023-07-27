namespace PriceAdjustment
{
    partial class PriceSettingItem
    {
        /// <summary> 
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.panel2 = new System.Windows.Forms.Panel();
            this.Date_Time = new System.Windows.Forms.DateTimePicker();
            this.Tbx_Price = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel2
            // 
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.Date_Time);
            this.panel2.Controls.Add(this.Tbx_Price);
            this.panel2.Controls.Add(this.label9);
            this.panel2.Controls.Add(this.label7);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(242, 27);
            this.panel2.TabIndex = 7;
            // 
            // Date_Time
            // 
            this.Date_Time.CustomFormat = "yyyy年MM月";
            this.Date_Time.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.Date_Time.Location = new System.Drawing.Point(38, 3);
            this.Date_Time.Name = "Date_Time";
            this.Date_Time.Size = new System.Drawing.Size(79, 21);
            this.Date_Time.TabIndex = 1;
            this.Date_Time.Value = new System.DateTime(2022, 1, 1, 0, 0, 0, 0);
            // 
            // Tbx_Price
            // 
            this.Tbx_Price.Location = new System.Drawing.Point(155, 3);
            this.Tbx_Price.Name = "Tbx_Price";
            this.Tbx_Price.Size = new System.Drawing.Size(79, 21);
            this.Tbx_Price.TabIndex = 3;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(123, 9);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(41, 12);
            this.label9.TabIndex = 2;
            this.label9.Text = "电价：";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(3, 9);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(41, 12);
            this.label7.TabIndex = 0;
            this.label7.Text = "月份：";
            // 
            // PriceSettingItem
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel2);
            this.Name = "PriceSettingItem";
            this.Size = new System.Drawing.Size(242, 27);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.DateTimePicker Date_Time;
        private System.Windows.Forms.TextBox Tbx_Price;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label7;
    }
}
