namespace PriceAdjustment
{
    partial class CreateAuthForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CreateAuthForm));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.Btn_Scan = new System.Windows.Forms.Button();
            this.Tbx_AppCode = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.Pic_Qrcode = new System.Windows.Forms.PictureBox();
            this.Btn_Create = new System.Windows.Forms.Button();
            this.Cbx_Time = new System.Windows.Forms.CheckBox();
            this.Date_limit = new System.Windows.Forms.DateTimePicker();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.Tbx_Auth = new System.Windows.Forms.TextBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Pic_Qrcode)).BeginInit();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.Btn_Scan);
            this.groupBox1.Controls.Add(this.Tbx_AppCode);
            this.groupBox1.Location = new System.Drawing.Point(13, 13);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(604, 72);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "设备码";
            // 
            // Btn_Scan
            // 
            this.Btn_Scan.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Btn_Scan.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Btn_Scan.Location = new System.Drawing.Point(510, 20);
            this.Btn_Scan.Name = "Btn_Scan";
            this.Btn_Scan.Size = new System.Drawing.Size(75, 40);
            this.Btn_Scan.TabIndex = 1;
            this.Btn_Scan.Text = "扫描二维码";
            this.Btn_Scan.UseVisualStyleBackColor = true;
            this.Btn_Scan.Click += new System.EventHandler(this.Btn_Scan_Click);
            // 
            // Tbx_AppCode
            // 
            this.Tbx_AppCode.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Tbx_AppCode.Location = new System.Drawing.Point(6, 20);
            this.Tbx_AppCode.Multiline = true;
            this.Tbx_AppCode.Name = "Tbx_AppCode";
            this.Tbx_AppCode.Size = new System.Drawing.Size(489, 40);
            this.Tbx_AppCode.TabIndex = 0;
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.groupBox2.Controls.Add(this.Pic_Qrcode);
            this.groupBox2.Location = new System.Drawing.Point(13, 92);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(199, 183);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "授权码";
            // 
            // Pic_Qrcode
            // 
            this.Pic_Qrcode.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Pic_Qrcode.Image = global::PriceAdjustment.Properties.Resources.gril;
            this.Pic_Qrcode.Location = new System.Drawing.Point(3, 17);
            this.Pic_Qrcode.Name = "Pic_Qrcode";
            this.Pic_Qrcode.Size = new System.Drawing.Size(193, 163);
            this.Pic_Qrcode.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.Pic_Qrcode.TabIndex = 0;
            this.Pic_Qrcode.TabStop = false;
            // 
            // Btn_Create
            // 
            this.Btn_Create.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.Btn_Create.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Btn_Create.Location = new System.Drawing.Point(176, 142);
            this.Btn_Create.Name = "Btn_Create";
            this.Btn_Create.Size = new System.Drawing.Size(75, 33);
            this.Btn_Create.TabIndex = 2;
            this.Btn_Create.Text = "生成";
            this.Btn_Create.UseVisualStyleBackColor = true;
            this.Btn_Create.Click += new System.EventHandler(this.Btn_Create_Click);
            // 
            // Cbx_Time
            // 
            this.Cbx_Time.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.Cbx_Time.AutoSize = true;
            this.Cbx_Time.Location = new System.Drawing.Point(10, 153);
            this.Cbx_Time.Name = "Cbx_Time";
            this.Cbx_Time.Size = new System.Drawing.Size(72, 16);
            this.Cbx_Time.TabIndex = 3;
            this.Cbx_Time.Text = "到期日期";
            this.Cbx_Time.UseVisualStyleBackColor = true;
            // 
            // Date_limit
            // 
            this.Date_limit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.Date_limit.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.Date_limit.Location = new System.Drawing.Point(77, 148);
            this.Date_limit.Name = "Date_limit";
            this.Date_limit.Size = new System.Drawing.Size(93, 21);
            this.Date_limit.TabIndex = 4;
            // 
            // groupBox3
            // 
            this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox3.Controls.Add(this.Tbx_Auth);
            this.groupBox3.Controls.Add(this.Date_limit);
            this.groupBox3.Controls.Add(this.Btn_Create);
            this.groupBox3.Controls.Add(this.Cbx_Time);
            this.groupBox3.Location = new System.Drawing.Point(218, 92);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(400, 183);
            this.groupBox3.TabIndex = 5;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "生成";
            // 
            // Tbx_Auth
            // 
            this.Tbx_Auth.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Tbx_Auth.Location = new System.Drawing.Point(10, 21);
            this.Tbx_Auth.Multiline = true;
            this.Tbx_Auth.Name = "Tbx_Auth";
            this.Tbx_Auth.Size = new System.Drawing.Size(384, 115);
            this.Tbx_Auth.TabIndex = 5;
            // 
            // CreateAuthForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(624, 295);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "CreateAuthForm";
            this.Text = "授权码生产";
            this.Load += new System.EventHandler(this.CreateAuthForm_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.Pic_Qrcode)).EndInit();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button Btn_Scan;
        private System.Windows.Forms.TextBox Tbx_AppCode;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button Btn_Create;
        private System.Windows.Forms.CheckBox Cbx_Time;
        private System.Windows.Forms.DateTimePicker Date_limit;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.TextBox Tbx_Auth;
        private System.Windows.Forms.PictureBox Pic_Qrcode;
    }
}