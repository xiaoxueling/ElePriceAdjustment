namespace PriceAdjustment
{
    partial class AuthForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AuthForm));
            this.Pic_Code = new System.Windows.Forms.PictureBox();
            this.授权码 = new System.Windows.Forms.GroupBox();
            this.Btn_QrcodeScan = new System.Windows.Forms.Button();
            this.Tbx_Auth = new System.Windows.Forms.TextBox();
            this.Btn_Auth = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.Btn_Show = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.Btn_hide = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.Pic_Code)).BeginInit();
            this.授权码.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // Pic_Code
            // 
            this.Pic_Code.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Pic_Code.ErrorImage = global::PriceAdjustment.Properties.Resources.gril;
            this.Pic_Code.Image = global::PriceAdjustment.Properties.Resources.gril;
            this.Pic_Code.InitialImage = null;
            this.Pic_Code.Location = new System.Drawing.Point(3, 17);
            this.Pic_Code.Name = "Pic_Code";
            this.Pic_Code.Size = new System.Drawing.Size(235, 185);
            this.Pic_Code.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.Pic_Code.TabIndex = 0;
            this.Pic_Code.TabStop = false;
            // 
            // 授权码
            // 
            this.授权码.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.授权码.Controls.Add(this.Btn_QrcodeScan);
            this.授权码.Controls.Add(this.Tbx_Auth);
            this.授权码.Location = new System.Drawing.Point(12, 221);
            this.授权码.Name = "授权码";
            this.授权码.Size = new System.Drawing.Size(479, 171);
            this.授权码.TabIndex = 1;
            this.授权码.TabStop = false;
            this.授权码.Text = "授权码";
            // 
            // Btn_QrcodeScan
            // 
            this.Btn_QrcodeScan.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.Btn_QrcodeScan.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.Btn_QrcodeScan.Location = new System.Drawing.Point(157, 132);
            this.Btn_QrcodeScan.Name = "Btn_QrcodeScan";
            this.Btn_QrcodeScan.Size = new System.Drawing.Size(168, 33);
            this.Btn_QrcodeScan.TabIndex = 2;
            this.Btn_QrcodeScan.Text = "扫描屏幕上授权二维码";
            this.Btn_QrcodeScan.UseVisualStyleBackColor = true;
            this.Btn_QrcodeScan.Click += new System.EventHandler(this.Btn_QrcodeScan_Click);
            // 
            // Tbx_Auth
            // 
            this.Tbx_Auth.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Tbx_Auth.Location = new System.Drawing.Point(7, 21);
            this.Tbx_Auth.Multiline = true;
            this.Tbx_Auth.Name = "Tbx_Auth";
            this.Tbx_Auth.Size = new System.Drawing.Size(466, 105);
            this.Tbx_Auth.TabIndex = 0;
            // 
            // Btn_Auth
            // 
            this.Btn_Auth.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.Btn_Auth.Font = new System.Drawing.Font("宋体", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Btn_Auth.ForeColor = System.Drawing.Color.ForestGreen;
            this.Btn_Auth.Location = new System.Drawing.Point(73, 398);
            this.Btn_Auth.Name = "Btn_Auth";
            this.Btn_Auth.Size = new System.Drawing.Size(347, 46);
            this.Btn_Auth.TabIndex = 1;
            this.Btn_Auth.Text = "认证";
            this.Btn_Auth.UseVisualStyleBackColor = true;
            this.Btn_Auth.Click += new System.EventHandler(this.Btn_Auth_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.label1.Location = new System.Drawing.Point(305, 27);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(144, 16);
            this.label1.TabIndex = 2;
            this.label1.Text = "请进行授权后使用";
            // 
            // Btn_Show
            // 
            this.Btn_Show.Location = new System.Drawing.Point(308, 77);
            this.Btn_Show.Name = "Btn_Show";
            this.Btn_Show.Size = new System.Drawing.Size(112, 41);
            this.Btn_Show.TabIndex = 3;
            this.Btn_Show.Text = "<<显示设备码";
            this.Btn_Show.UseVisualStyleBackColor = true;
            this.Btn_Show.Click += new System.EventHandler(this.Btn_Show_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.Pic_Code);
            this.groupBox1.Location = new System.Drawing.Point(12, 10);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(241, 205);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "设备码";
            // 
            // Btn_hide
            // 
            this.Btn_hide.Location = new System.Drawing.Point(308, 142);
            this.Btn_hide.Name = "Btn_hide";
            this.Btn_hide.Size = new System.Drawing.Size(112, 41);
            this.Btn_hide.TabIndex = 5;
            this.Btn_hide.Text = "<<隐藏设备码";
            this.Btn_hide.UseVisualStyleBackColor = true;
            this.Btn_hide.Click += new System.EventHandler(this.Btn_hide_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.ErrorImage = null;
            this.pictureBox1.Image = global::PriceAdjustment.Properties.Resources.idea;
            this.pictureBox1.InitialImage = global::PriceAdjustment.Properties.Resources.idea;
            this.pictureBox1.Location = new System.Drawing.Point(268, 21);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(37, 31);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 6;
            this.pictureBox1.TabStop = false;
            // 
            // AuthForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(501, 456);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.Btn_hide);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.Btn_Show);
            this.Controls.Add(this.Btn_Auth);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.授权码);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "AuthForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "授权";
            this.Load += new System.EventHandler(this.AuthForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.Pic_Code)).EndInit();
            this.授权码.ResumeLayout(false);
            this.授权码.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox Pic_Code;
        private System.Windows.Forms.GroupBox 授权码;
        private System.Windows.Forms.Button Btn_Auth;
        private System.Windows.Forms.TextBox Tbx_Auth;
        private System.Windows.Forms.Button Btn_QrcodeScan;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button Btn_Show;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button Btn_hide;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}