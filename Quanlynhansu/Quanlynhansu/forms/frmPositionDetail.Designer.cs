namespace Quanlynhansu.Forms
{
    partial class frmPositionDetail
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblPositionCode;
        private System.Windows.Forms.TextBox txtPositionCode;
        private System.Windows.Forms.Label lblPositionName;
        private System.Windows.Forms.TextBox txtPositionName;
        private System.Windows.Forms.Label lblLevel;
        private System.Windows.Forms.NumericUpDown nudLevel;
        private System.Windows.Forms.Label lblBaseSalary;
        private System.Windows.Forms.NumericUpDown nudBaseSalary;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnCancel;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblPositionCode = new System.Windows.Forms.Label();
            this.txtPositionCode = new System.Windows.Forms.TextBox();
            this.lblPositionName = new System.Windows.Forms.Label();
            this.txtPositionName = new System.Windows.Forms.TextBox();
            this.lblLevel = new System.Windows.Forms.Label();
            this.nudLevel = new System.Windows.Forms.NumericUpDown();
            this.lblBaseSalary = new System.Windows.Forms.Label();
            this.nudBaseSalary = new System.Windows.Forms.NumericUpDown();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.nudLevel)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudBaseSalary)).BeginInit();
            this.SuspendLayout();
            // 
            // lblTitle
            // 
            this.lblTitle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(122)))), ((int)(((byte)(204)))));
            this.lblTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.White;
            this.lblTitle.Location = new System.Drawing.Point(0, 0);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(534, 60);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "THÔNG TIN CHỨC VỤ";
            this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblPositionCode
            // 
            this.lblPositionCode.AutoSize = true;
            this.lblPositionCode.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblPositionCode.Location = new System.Drawing.Point(30, 90);
            this.lblPositionCode.Name = "lblPositionCode";
            this.lblPositionCode.Size = new System.Drawing.Size(95, 19);
            this.lblPositionCode.TabIndex = 1;
            this.lblPositionCode.Text = "Mã chức vụ: *";
            // 
            // txtPositionCode
            // 
            this.txtPositionCode.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtPositionCode.Location = new System.Drawing.Point(180, 87);
            this.txtPositionCode.MaxLength = 20;
            this.txtPositionCode.Name = "txtPositionCode";
            this.txtPositionCode.Size = new System.Drawing.Size(320, 25);
            this.txtPositionCode.TabIndex = 2;
            // 
            // lblPositionName
            // 
            this.lblPositionName.AutoSize = true;
            this.lblPositionName.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblPositionName.Location = new System.Drawing.Point(30, 130);
            this.lblPositionName.Name = "lblPositionName";
            this.lblPositionName.Size = new System.Drawing.Size(97, 19);
            this.lblPositionName.TabIndex = 3;
            this.lblPositionName.Text = "Tên chức vụ: *";
            // 
            // txtPositionName
            // 
            this.txtPositionName.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtPositionName.Location = new System.Drawing.Point(180, 127);
            this.txtPositionName.MaxLength = 200;
            this.txtPositionName.Name = "txtPositionName";
            this.txtPositionName.Size = new System.Drawing.Size(320, 25);
            this.txtPositionName.TabIndex = 4;
            // 
            // lblLevel
            // 
            this.lblLevel.AutoSize = true;
            this.lblLevel.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblLevel.Location = new System.Drawing.Point(30, 170);
            this.lblLevel.Name = "lblLevel";
            this.lblLevel.Size = new System.Drawing.Size(75, 19);
            this.lblLevel.TabIndex = 5;
            this.lblLevel.Text = "Cấp bậc: *";
            // 
            // nudLevel
            // 
            this.nudLevel.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.nudLevel.Location = new System.Drawing.Point(180, 167);
            this.nudLevel.Maximum = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.nudLevel.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudLevel.Name = "nudLevel";
            this.nudLevel.Size = new System.Drawing.Size(320, 25);
            this.nudLevel.TabIndex = 6;
            this.nudLevel.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // lblBaseSalary
            // 
            this.lblBaseSalary.AutoSize = true;
            this.lblBaseSalary.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblBaseSalary.Location = new System.Drawing.Point(30, 210);
            this.lblBaseSalary.Name = "lblBaseSalary";
            this.lblBaseSalary.Size = new System.Drawing.Size(104, 19);
            this.lblBaseSalary.TabIndex = 7;
            this.lblBaseSalary.Text = "Lương cơ bản:";
            // 
            // nudBaseSalary
            // 
            this.nudBaseSalary.DecimalPlaces = 0;
            this.nudBaseSalary.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.nudBaseSalary.Increment = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.nudBaseSalary.Location = new System.Drawing.Point(180, 207);
            this.nudBaseSalary.Maximum = new decimal(new int[] {
            1000000000,
            0,
            0,
            0});
            this.nudBaseSalary.Name = "nudBaseSalary";
            this.nudBaseSalary.Size = new System.Drawing.Size(320, 25);
            this.nudBaseSalary.TabIndex = 8;
            this.nudBaseSalary.ThousandsSeparator = true;
            // 
            // btnSave
            // 
            this.btnSave.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(204)))), ((int)(((byte)(113)))));
            this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSave.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnSave.ForeColor = System.Drawing.Color.White;
            this.btnSave.Location = new System.Drawing.Point(280, 260);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(100, 35);
            this.btnSave.TabIndex = 9;
            this.btnSave.Text = "Lưu";
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(149)))), ((int)(((byte)(165)))), ((int)(((byte)(166)))));
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancel.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnCancel.ForeColor = System.Drawing.Color.White;
            this.btnCancel.Location = new System.Drawing.Point(400, 260);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(100, 35);
            this.btnCancel.TabIndex = 10;
            this.btnCancel.Text = "Hủy";
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // frmPositionDetail
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(534, 321);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.nudBaseSalary);
            this.Controls.Add(this.lblBaseSalary);
            this.Controls.Add(this.nudLevel);
            this.Controls.Add(this.lblLevel);
            this.Controls.Add(this.txtPositionName);
            this.Controls.Add(this.lblPositionName);
            this.Controls.Add(this.txtPositionCode);
            this.Controls.Add(this.lblPositionCode);
            this.Controls.Add(this.lblTitle);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmPositionDetail";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Chi tiết chức vụ";
            this.Load += new System.EventHandler(this.frmPositionDetail_Load);
            ((System.ComponentModel.ISupportInitialize)(this.nudLevel)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudBaseSalary)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}
