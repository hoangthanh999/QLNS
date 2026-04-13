namespace Quanlynhansu.Forms
{
    partial class frmCalculateSalary
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Panel pnlTop;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Panel pnlFilter;
        private System.Windows.Forms.Label lblMonth;
        private System.Windows.Forms.ComboBox cboMonth;
        private System.Windows.Forms.Label lblYear;
        private System.Windows.Forms.ComboBox cboYear;
        private System.Windows.Forms.Label lblDepartment;
        private System.Windows.Forms.ComboBox cboDepartment;
        private System.Windows.Forms.Button btnLoadEmployees;
        private System.Windows.Forms.GroupBox grpSettings;
        private System.Windows.Forms.Label lblStandardDays;
        private System.Windows.Forms.TextBox txtStandardDays;
        private System.Windows.Forms.CheckBox chkOverwriteExisting;
        private System.Windows.Forms.CheckBox chkAutoApprove;
        private System.Windows.Forms.DataGridView dgvEmployees;
        private System.Windows.Forms.Panel pnlBottom;
        private System.Windows.Forms.Label lblTotalEmployees;
        private System.Windows.Forms.Label lblTotalSalary;
        private System.Windows.Forms.Button btnSelectAll;
        private System.Windows.Forms.Button btnDeselectAll;
        private System.Windows.Forms.Button btnCalculate;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.ProgressBar progressBar;
        private System.Windows.Forms.Label lblProgress;

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
            this.pnlTop = new System.Windows.Forms.Panel();
            this.lblTitle = new System.Windows.Forms.Label();
            this.pnlFilter = new System.Windows.Forms.Panel();
            this.lblMonth = new System.Windows.Forms.Label();
            this.cboMonth = new System.Windows.Forms.ComboBox();
            this.lblYear = new System.Windows.Forms.Label();
            this.cboYear = new System.Windows.Forms.ComboBox();
            this.lblDepartment = new System.Windows.Forms.Label();
            this.cboDepartment = new System.Windows.Forms.ComboBox();
            this.btnLoadEmployees = new System.Windows.Forms.Button();
            this.grpSettings = new System.Windows.Forms.GroupBox();
            this.lblStandardDays = new System.Windows.Forms.Label();
            this.txtStandardDays = new System.Windows.Forms.TextBox();
            this.chkOverwriteExisting = new System.Windows.Forms.CheckBox();
            this.chkAutoApprove = new System.Windows.Forms.CheckBox();
            this.dgvEmployees = new System.Windows.Forms.DataGridView();
            this.pnlBottom = new System.Windows.Forms.Panel();
            this.lblTotalEmployees = new System.Windows.Forms.Label();
            this.lblTotalSalary = new System.Windows.Forms.Label();
            this.btnSelectAll = new System.Windows.Forms.Button();
            this.btnDeselectAll = new System.Windows.Forms.Button();
            this.btnCalculate = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.lblProgress = new System.Windows.Forms.Label();
            this.pnlTop.SuspendLayout();
            this.pnlFilter.SuspendLayout();
            this.grpSettings.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvEmployees)).BeginInit();
            this.pnlBottom.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlTop
            // 
            this.pnlTop.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(167)))), ((int)(((byte)(69)))));
            this.pnlTop.Controls.Add(this.lblTitle);
            this.pnlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTop.Location = new System.Drawing.Point(0, 0);
            this.pnlTop.Name = "pnlTop";
            this.pnlTop.Size = new System.Drawing.Size(1200, 60);
            this.pnlTop.TabIndex = 0;
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.White;
            this.lblTitle.Location = new System.Drawing.Point(12, 12);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(372, 32);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "🧮 TÍNH LƯƠNG HÀNG LOẠT";
            // 
            // pnlFilter
            // 
            this.pnlFilter.BackColor = System.Drawing.Color.White;
            this.pnlFilter.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlFilter.Controls.Add(this.lblMonth);
            this.pnlFilter.Controls.Add(this.cboMonth);
            this.pnlFilter.Controls.Add(this.lblYear);
            this.pnlFilter.Controls.Add(this.cboYear);
            this.pnlFilter.Controls.Add(this.lblDepartment);
            this.pnlFilter.Controls.Add(this.cboDepartment);
            this.pnlFilter.Controls.Add(this.btnLoadEmployees);
            this.pnlFilter.Controls.Add(this.grpSettings);
            this.pnlFilter.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlFilter.Location = new System.Drawing.Point(0, 60);
            this.pnlFilter.Name = "pnlFilter";
            this.pnlFilter.Padding = new System.Windows.Forms.Padding(10);
            this.pnlFilter.Size = new System.Drawing.Size(1200, 140);
            this.pnlFilter.TabIndex = 1;
            // 
            // lblMonth
            // 
            this.lblMonth.AutoSize = true;
            this.lblMonth.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblMonth.Location = new System.Drawing.Point(15, 15);
            this.lblMonth.Name = "lblMonth";
            this.lblMonth.Size = new System.Drawing.Size(77, 19);
            this.lblMonth.TabIndex = 0;
            this.lblMonth.Text = "Tháng: (*)";
            // 
            // cboMonth
            // 
            this.cboMonth.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboMonth.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cboMonth.FormattingEnabled = true;
            this.cboMonth.Location = new System.Drawing.Point(15, 40);
            this.cboMonth.Name = "cboMonth";
            this.cboMonth.Size = new System.Drawing.Size(120, 25);
            this.cboMonth.TabIndex = 1;
            this.cboMonth.SelectedIndexChanged += new System.EventHandler(this.cboMonth_SelectedIndexChanged);
            // 
            // lblYear
            // 
            this.lblYear.AutoSize = true;
            this.lblYear.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblYear.Location = new System.Drawing.Point(150, 15);
            this.lblYear.Name = "lblYear";
            this.lblYear.Size = new System.Drawing.Size(68, 19);
            this.lblYear.TabIndex = 2;
            this.lblYear.Text = "Năm: (*)";
            // 
            // cboYear
            // 
            this.cboYear.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboYear.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cboYear.FormattingEnabled = true;
            this.cboYear.Location = new System.Drawing.Point(150, 40);
            this.cboYear.Name = "cboYear";
            this.cboYear.Size = new System.Drawing.Size(120, 25);
            this.cboYear.TabIndex = 3;
            this.cboYear.SelectedIndexChanged += new System.EventHandler(this.cboYear_SelectedIndexChanged);
            // 
            // lblDepartment
            // 
            this.lblDepartment.AutoSize = true;
            this.lblDepartment.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblDepartment.Location = new System.Drawing.Point(285, 15);
            this.lblDepartment.Name = "lblDepartment";
            this.lblDepartment.Size = new System.Drawing.Size(85, 19);
            this.lblDepartment.TabIndex = 4;
            this.lblDepartment.Text = "Phòng ban:";
            // 
            // cboDepartment
            // 
            this.cboDepartment.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboDepartment.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cboDepartment.FormattingEnabled = true;
            this.cboDepartment.Location = new System.Drawing.Point(285, 40);
            this.cboDepartment.Name = "cboDepartment";
            this.cboDepartment.Size = new System.Drawing.Size(250, 25);
            this.cboDepartment.TabIndex = 5;
            // 
            // btnLoadEmployees
            // 
            this.btnLoadEmployees.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(123)))), ((int)(((byte)(255)))));
            this.btnLoadEmployees.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLoadEmployees.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnLoadEmployees.ForeColor = System.Drawing.Color.White;
            this.btnLoadEmployees.Location = new System.Drawing.Point(550, 38);
            this.btnLoadEmployees.Name = "btnLoadEmployees";
            this.btnLoadEmployees.Size = new System.Drawing.Size(150, 30);
            this.btnLoadEmployees.TabIndex = 6;
            this.btnLoadEmployees.Text = "📋 Tải danh sách";
            this.btnLoadEmployees.UseVisualStyleBackColor = false;
            this.btnLoadEmployees.Click += new System.EventHandler(this.btnLoadEmployees_Click);
            // 
            // grpSettings
            // 
            this.grpSettings.Controls.Add(this.lblStandardDays);
            this.grpSettings.Controls.Add(this.txtStandardDays);
            this.grpSettings.Controls.Add(this.chkOverwriteExisting);
            this.grpSettings.Controls.Add(this.chkAutoApprove);
            this.grpSettings.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.grpSettings.Location = new System.Drawing.Point(15, 80);
            this.grpSettings.Name = "grpSettings";
            this.grpSettings.Size = new System.Drawing.Size(1160, 50);
            this.grpSettings.TabIndex = 7;
            this.grpSettings.TabStop = false;
            this.grpSettings.Text = "⚙️ Cài đặt";
            // 
            // lblStandardDays
            // 
            this.lblStandardDays.AutoSize = true;
            this.lblStandardDays.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblStandardDays.Location = new System.Drawing.Point(15, 23);
            this.lblStandardDays.Name = "lblStandardDays";
            this.lblStandardDays.Size = new System.Drawing.Size(115, 15);
            this.lblStandardDays.TabIndex = 0;
            this.lblStandardDays.Text = "Số ngày chuẩn:";
            // 
            // txtStandardDays
            // 
            this.txtStandardDays.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtStandardDays.Location = new System.Drawing.Point(135, 20);
            this.txtStandardDays.Name = "txtStandardDays";
            this.txtStandardDays.ReadOnly = true;
            this.txtStandardDays.Size = new System.Drawing.Size(80, 23);
            this.txtStandardDays.TabIndex = 1;
            this.txtStandardDays.Text = "22";
            this.txtStandardDays.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // chkOverwriteExisting
            // 
            this.chkOverwriteExisting.AutoSize = true;
            this.chkOverwriteExisting.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.chkOverwriteExisting.Location = new System.Drawing.Point(250, 22);
            this.chkOverwriteExisting.Name = "chkOverwriteExisting";
            this.chkOverwriteExisting.Size = new System.Drawing.Size(188, 19);
            this.chkOverwriteExisting.TabIndex = 2;
            this.chkOverwriteExisting.Text = "Ghi đè lương đã tồn tại";
            this.chkOverwriteExisting.UseVisualStyleBackColor = true;
            // 
            // chkAutoApprove
            // 
            this.chkAutoApprove.AutoSize = true;
            this.chkAutoApprove.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.chkAutoApprove.Location = new System.Drawing.Point(470, 22);
            this.chkAutoApprove.Name = "chkAutoApprove";
            this.chkAutoApprove.Size = new System.Drawing.Size(168, 19);
            this.chkAutoApprove.TabIndex = 3;
            this.chkAutoApprove.Text = "Tự động duyệt sau khi tính";
            this.chkAutoApprove.UseVisualStyleBackColor = true;
            // 
            // dgvEmployees
            // 
            this.dgvEmployees.AllowUserToAddRows = false;
            this.dgvEmployees.AllowUserToDeleteRows = false;
            this.dgvEmployees.BackgroundColor = System.Drawing.Color.White;
            this.dgvEmployees.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvEmployees.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvEmployees.Location = new System.Drawing.Point(0, 200);
            this.dgvEmployees.Name = "dgvEmployees";
            this.dgvEmployees.RowHeadersWidth = 51;
            this.dgvEmployees.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvEmployees.Size = new System.Drawing.Size(1200, 380);
            this.dgvEmployees.TabIndex = 2;
            this.dgvEmployees.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvEmployees_CellValueChanged);
            // 
            // pnlBottom
            // 
            this.pnlBottom.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(249)))), ((int)(((byte)(250)))));
            this.pnlBottom.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlBottom.Controls.Add(this.lblTotalEmployees);
            this.pnlBottom.Controls.Add(this.lblTotalSalary);
            this.pnlBottom.Controls.Add(this.btnSelectAll);
            this.pnlBottom.Controls.Add(this.btnDeselectAll);
            this.pnlBottom.Controls.Add(this.btnCalculate);
            this.pnlBottom.Controls.Add(this.btnClose);
            this.pnlBottom.Controls.Add(this.progressBar);
            this.pnlBottom.Controls.Add(this.lblProgress);
            this.pnlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlBottom.Location = new System.Drawing.Point(0, 580);
            this.pnlBottom.Name = "pnlBottom";
            this.pnlBottom.Size = new System.Drawing.Size(1200, 120);
            this.pnlBottom.TabIndex = 3;
            // 
            // lblTotalEmployees
            // 
            this.lblTotalEmployees.AutoSize = true;
            this.lblTotalEmployees.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblTotalEmployees.Location = new System.Drawing.Point(15, 15);
            this.lblTotalEmployees.Name = "lblTotalEmployees";
            this.lblTotalEmployees.Size = new System.Drawing.Size(152, 19);
            this.lblTotalEmployees.TabIndex = 0;
            this.lblTotalEmployees.Text = "Số NV được chọn: 0";
            // 
            // lblTotalSalary
            // 
            this.lblTotalSalary.AutoSize = true;
            this.lblTotalSalary.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblTotalSalary.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(53)))), ((int)(((byte)(69)))));
            this.lblTotalSalary.Location = new System.Drawing.Point(15, 40);
            this.lblTotalSalary.Name = "lblTotalSalary";
            this.lblTotalSalary.Size = new System.Drawing.Size(180, 19);
            this.lblTotalSalary.TabIndex = 1;
            this.lblTotalSalary.Text = "Tổng lương dự kiến: 0 VNĐ";
            // 
            // btnSelectAll
            // 
            this.btnSelectAll.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(23)))), ((int)(((byte)(162)))), ((int)(((byte)(184)))));
            this.btnSelectAll.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSelectAll.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnSelectAll.ForeColor = System.Drawing.Color.White;
            this.btnSelectAll.Location = new System.Drawing.Point(720, 12);
            this.btnSelectAll.Name = "btnSelectAll";
            this.btnSelectAll.Size = new System.Drawing.Size(110, 35);
            this.btnSelectAll.TabIndex = 2;
            this.btnSelectAll.Text = "☑️ Chọn tất cả";
            this.btnSelectAll.UseVisualStyleBackColor = false;
            this.btnSelectAll.Click += new System.EventHandler(this.btnSelectAll_Click);
            // 
            // btnDeselectAll
            // 
            this.btnDeselectAll.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(108)))), ((int)(((byte)(117)))), ((int)(((byte)(125)))));
            this.btnDeselectAll.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDeselectAll.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnDeselectAll.ForeColor = System.Drawing.Color.White;
            this.btnDeselectAll.Location = new System.Drawing.Point(840, 12);
            this.btnDeselectAll.Name = "btnDeselectAll";
            this.btnDeselectAll.Size = new System.Drawing.Size(110, 35);
            this.btnDeselectAll.TabIndex = 3;
            this.btnDeselectAll.Text = "☐ Bỏ chọn";
            this.btnDeselectAll.UseVisualStyleBackColor = false;
            this.btnDeselectAll.Click += new System.EventHandler(this.btnDeselectAll_Click);
            // 
            // btnCalculate
            // 
            this.btnCalculate.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(167)))), ((int)(((byte)(69)))));
            this.btnCalculate.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCalculate.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.btnCalculate.ForeColor = System.Drawing.Color.White;
            this.btnCalculate.Location = new System.Drawing.Point(960, 12);
            this.btnCalculate.Name = "btnCalculate";
            this.btnCalculate.Size = new System.Drawing.Size(110, 35);
            this.btnCalculate.TabIndex = 4;
            this.btnCalculate.Text = "🧮 Tính lương";
            this.btnCalculate.UseVisualStyleBackColor = false;
            this.btnCalculate.Click += new System.EventHandler(this.btnCalculate_Click);
            // 
            // btnClose
            // 
            this.btnClose.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(53)))), ((int)(((byte)(69)))));
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClose.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.btnClose.ForeColor = System.Drawing.Color.White;
            this.btnClose.Location = new System.Drawing.Point(1080, 12);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(110, 35);
            this.btnClose.TabIndex = 5;
            this.btnClose.Text = "❌ Đóng";
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // progressBar
            // 
            this.progressBar.Location = new System.Drawing.Point(15, 75);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(1170, 25);
            this.progressBar.TabIndex = 6;
            this.progressBar.Visible = false;
            // 
            // lblProgress
            // 
            this.lblProgress.AutoSize = true;
            this.lblProgress.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblProgress.Location = new System.Drawing.Point(15, 55);
            this.lblProgress.Name = "lblProgress";
            this.lblProgress.Size = new System.Drawing.Size(115, 15);
            this.lblProgress.TabIndex = 7;
            this.lblProgress.Text = "Đang xử lý: 0/0";
            this.lblProgress.Visible = false;
            // 
            // frmCalculateSalary
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1200, 700);
            this.Controls.Add(this.dgvEmployees);
            this.Controls.Add(this.pnlBottom);
            this.Controls.Add(this.pnlFilter);
            this.Controls.Add(this.pnlTop);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmCalculateSalary";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Tính lương hàng loạt";
            this.Load += new System.EventHandler(this.frmCalculateSalary_Load);
            this.pnlTop.ResumeLayout(false);
            this.pnlTop.PerformLayout();
            this.pnlFilter.ResumeLayout(false);
            this.pnlFilter.PerformLayout();
            this.grpSettings.ResumeLayout(false);
            this.grpSettings.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvEmployees)).EndInit();
            this.pnlBottom.ResumeLayout(false);
            this.pnlBottom.PerformLayout();
            this.ResumeLayout(false);
        }
    }
}
