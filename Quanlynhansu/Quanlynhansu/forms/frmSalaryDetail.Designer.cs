namespace Quanlynhansu.Forms
{
    partial class frmSalaryDetail
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Panel pnlTop;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Panel pnlMain;
        private System.Windows.Forms.Label lblEmployee;
        private System.Windows.Forms.ComboBox cboEmployee;
        private System.Windows.Forms.Label lblMonth;
        private System.Windows.Forms.ComboBox cboMonth;
        private System.Windows.Forms.Label lblYear;
        private System.Windows.Forms.ComboBox cboYear;
        private System.Windows.Forms.Label lblBaseSalary;
        private System.Windows.Forms.TextBox txtBaseSalary;
        private System.Windows.Forms.Label lblBonus;
        private System.Windows.Forms.TextBox txtBonus;
        private System.Windows.Forms.Label lblDeduction;
        private System.Windows.Forms.TextBox txtDeduction;
        private System.Windows.Forms.Label lblTotalSalary;
        private System.Windows.Forms.TextBox txtTotalSalary;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.ComboBox cboStatus;
        private System.Windows.Forms.Label lblPaymentDate;
        private System.Windows.Forms.DateTimePicker dtpPaymentDate;
        private System.Windows.Forms.CheckBox chkHasPaymentDate;
        private System.Windows.Forms.GroupBox grpAttendance;
        private System.Windows.Forms.Label lblWorkDays;
        private System.Windows.Forms.TextBox txtWorkDays;
        private System.Windows.Forms.Label lblStandardDays;
        private System.Windows.Forms.TextBox txtStandardDays;
        private System.Windows.Forms.Label lblWorkHours;
        private System.Windows.Forms.TextBox txtWorkHours;
        private System.Windows.Forms.Label lblLeaveDays;
        private System.Windows.Forms.TextBox txtLeaveDays;
        private System.Windows.Forms.Button btnCalculate;
        private System.Windows.Forms.Panel pnlButtons;
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
            this.pnlTop = new System.Windows.Forms.Panel();
            this.lblTitle = new System.Windows.Forms.Label();
            this.pnlMain = new System.Windows.Forms.Panel();
            this.lblEmployee = new System.Windows.Forms.Label();
            this.cboEmployee = new System.Windows.Forms.ComboBox();
            this.lblMonth = new System.Windows.Forms.Label();
            this.cboMonth = new System.Windows.Forms.ComboBox();
            this.lblYear = new System.Windows.Forms.Label();
            this.cboYear = new System.Windows.Forms.ComboBox();
            this.grpAttendance = new System.Windows.Forms.GroupBox();
            this.lblWorkDays = new System.Windows.Forms.Label();
            this.txtWorkDays = new System.Windows.Forms.TextBox();
            this.lblStandardDays = new System.Windows.Forms.Label();
            this.txtStandardDays = new System.Windows.Forms.TextBox();
            this.lblWorkHours = new System.Windows.Forms.Label();
            this.txtWorkHours = new System.Windows.Forms.TextBox();
            this.lblLeaveDays = new System.Windows.Forms.Label();
            this.txtLeaveDays = new System.Windows.Forms.TextBox();
            this.lblBaseSalary = new System.Windows.Forms.Label();
            this.txtBaseSalary = new System.Windows.Forms.TextBox();
            this.lblBonus = new System.Windows.Forms.Label();
            this.txtBonus = new System.Windows.Forms.TextBox();
            this.lblDeduction = new System.Windows.Forms.Label();
            this.txtDeduction = new System.Windows.Forms.TextBox();
            this.btnCalculate = new System.Windows.Forms.Button();
            this.lblTotalSalary = new System.Windows.Forms.Label();
            this.txtTotalSalary = new System.Windows.Forms.TextBox();
            this.lblStatus = new System.Windows.Forms.Label();
            this.cboStatus = new System.Windows.Forms.ComboBox();
            this.lblPaymentDate = new System.Windows.Forms.Label();
            this.dtpPaymentDate = new System.Windows.Forms.DateTimePicker();
            this.chkHasPaymentDate = new System.Windows.Forms.CheckBox();
            this.pnlButtons = new System.Windows.Forms.Panel();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.pnlTop.SuspendLayout();
            this.pnlMain.SuspendLayout();
            this.grpAttendance.SuspendLayout();
            this.pnlButtons.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlTop
            // 
            this.pnlTop.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(123)))), ((int)(((byte)(255)))));
            this.pnlTop.Controls.Add(this.lblTitle);
            this.pnlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTop.Location = new System.Drawing.Point(0, 0);
            this.pnlTop.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.pnlTop.Name = "pnlTop";
            this.pnlTop.Size = new System.Drawing.Size(1050, 92);
            this.pnlTop.TabIndex = 0;
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.White;
            this.lblTitle.Location = new System.Drawing.Point(18, 23);
            this.lblTitle.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(319, 45);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "💰 CHI TIẾT LƯƠNG";
            // 
            // pnlMain
            // 
            this.pnlMain.AutoScroll = true;
            this.pnlMain.BackColor = System.Drawing.Color.White;
            this.pnlMain.Controls.Add(this.lblEmployee);
            this.pnlMain.Controls.Add(this.cboEmployee);
            this.pnlMain.Controls.Add(this.lblMonth);
            this.pnlMain.Controls.Add(this.cboMonth);
            this.pnlMain.Controls.Add(this.lblYear);
            this.pnlMain.Controls.Add(this.cboYear);
            this.pnlMain.Controls.Add(this.grpAttendance);
            this.pnlMain.Controls.Add(this.lblBaseSalary);
            this.pnlMain.Controls.Add(this.txtBaseSalary);
            this.pnlMain.Controls.Add(this.lblBonus);
            this.pnlMain.Controls.Add(this.txtBonus);
            this.pnlMain.Controls.Add(this.lblDeduction);
            this.pnlMain.Controls.Add(this.txtDeduction);
            this.pnlMain.Controls.Add(this.btnCalculate);
            this.pnlMain.Controls.Add(this.lblTotalSalary);
            this.pnlMain.Controls.Add(this.txtTotalSalary);
            this.pnlMain.Controls.Add(this.lblStatus);
            this.pnlMain.Controls.Add(this.cboStatus);
            this.pnlMain.Controls.Add(this.lblPaymentDate);
            this.pnlMain.Controls.Add(this.dtpPaymentDate);
            this.pnlMain.Controls.Add(this.chkHasPaymentDate);
            this.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMain.Location = new System.Drawing.Point(0, 92);
            this.pnlMain.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Padding = new System.Windows.Forms.Padding(30, 31, 30, 31);
            this.pnlMain.Size = new System.Drawing.Size(1050, 642);
            this.pnlMain.TabIndex = 1;
            // 
            // lblEmployee
            // 
            this.lblEmployee.AutoSize = true;
            this.lblEmployee.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblEmployee.Location = new System.Drawing.Point(30, 31);
            this.lblEmployee.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblEmployee.Name = "lblEmployee";
            this.lblEmployee.Size = new System.Drawing.Size(129, 28);
            this.lblEmployee.TabIndex = 0;
            this.lblEmployee.Text = "Nhân viên: (*)";
            // 
            // cboEmployee
            // 
            this.cboEmployee.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboEmployee.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cboEmployee.FormattingEnabled = true;
            this.cboEmployee.Location = new System.Drawing.Point(225, 26);
            this.cboEmployee.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.cboEmployee.Name = "cboEmployee";
            this.cboEmployee.Size = new System.Drawing.Size(748, 36);
            this.cboEmployee.TabIndex = 1;
            this.cboEmployee.SelectedIndexChanged += new System.EventHandler(this.cboEmployee_SelectedIndexChanged);
            // 
            // lblMonth
            // 
            this.lblMonth.AutoSize = true;
            this.lblMonth.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblMonth.Location = new System.Drawing.Point(30, 92);
            this.lblMonth.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblMonth.Name = "lblMonth";
            this.lblMonth.Size = new System.Drawing.Size(95, 28);
            this.lblMonth.TabIndex = 2;
            this.lblMonth.Text = "Tháng: (*)";
            // 
            // cboMonth
            // 
            this.cboMonth.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboMonth.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cboMonth.FormattingEnabled = true;
            this.cboMonth.Location = new System.Drawing.Point(225, 88);
            this.cboMonth.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.cboMonth.Name = "cboMonth";
            this.cboMonth.Size = new System.Drawing.Size(223, 36);
            this.cboMonth.TabIndex = 3;
            this.cboMonth.SelectedIndexChanged += new System.EventHandler(this.cboMonth_SelectedIndexChanged);
            // 
            // lblYear
            // 
            this.lblYear.AutoSize = true;
            this.lblYear.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblYear.Location = new System.Drawing.Point(525, 92);
            this.lblYear.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblYear.Name = "lblYear";
            this.lblYear.Size = new System.Drawing.Size(83, 28);
            this.lblYear.TabIndex = 4;
            this.lblYear.Text = "Năm: (*)";
            // 
            // cboYear
            // 
            this.cboYear.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboYear.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cboYear.FormattingEnabled = true;
            this.cboYear.Location = new System.Drawing.Point(675, 88);
            this.cboYear.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.cboYear.Name = "cboYear";
            this.cboYear.Size = new System.Drawing.Size(298, 36);
            this.cboYear.TabIndex = 5;
            this.cboYear.SelectedIndexChanged += new System.EventHandler(this.cboYear_SelectedIndexChanged);
            // 
            // grpAttendance
            // 
            this.grpAttendance.Controls.Add(this.lblWorkDays);
            this.grpAttendance.Controls.Add(this.txtWorkDays);
            this.grpAttendance.Controls.Add(this.lblStandardDays);
            this.grpAttendance.Controls.Add(this.txtStandardDays);
            this.grpAttendance.Controls.Add(this.lblWorkHours);
            this.grpAttendance.Controls.Add(this.txtWorkHours);
            this.grpAttendance.Controls.Add(this.lblLeaveDays);
            this.grpAttendance.Controls.Add(this.txtLeaveDays);
            this.grpAttendance.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.grpAttendance.Location = new System.Drawing.Point(30, 154);
            this.grpAttendance.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.grpAttendance.Name = "grpAttendance";
            this.grpAttendance.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.grpAttendance.Size = new System.Drawing.Size(945, 200);
            this.grpAttendance.TabIndex = 6;
            this.grpAttendance.TabStop = false;
            this.grpAttendance.Text = "📊 Thông tin chấm công";
            // 
            // lblWorkDays
            // 
            this.lblWorkDays.AutoSize = true;
            this.lblWorkDays.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblWorkDays.Location = new System.Drawing.Point(30, 46);
            this.lblWorkDays.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblWorkDays.Name = "lblWorkDays";
            this.lblWorkDays.Size = new System.Drawing.Size(124, 28);
            this.lblWorkDays.TabIndex = 0;
            this.lblWorkDays.Text = "Số ngày làm:";
            // 
            // txtWorkDays
            // 
            this.txtWorkDays.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtWorkDays.Location = new System.Drawing.Point(195, 42);
            this.txtWorkDays.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtWorkDays.Name = "txtWorkDays";
            this.txtWorkDays.ReadOnly = true;
            this.txtWorkDays.Size = new System.Drawing.Size(223, 34);
            this.txtWorkDays.TabIndex = 1;
            this.txtWorkDays.Text = "0";
            this.txtWorkDays.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // lblStandardDays
            // 
            this.lblStandardDays.AutoSize = true;
            this.lblStandardDays.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblStandardDays.Location = new System.Drawing.Point(495, 46);
            this.lblStandardDays.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblStandardDays.Name = "lblStandardDays";
            this.lblStandardDays.Size = new System.Drawing.Size(120, 28);
            this.lblStandardDays.TabIndex = 2;
            this.lblStandardDays.Text = "Ngày chuẩn:";
            // 
            // txtStandardDays
            // 
            this.txtStandardDays.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtStandardDays.Location = new System.Drawing.Point(675, 42);
            this.txtStandardDays.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtStandardDays.Name = "txtStandardDays";
            this.txtStandardDays.ReadOnly = true;
            this.txtStandardDays.Size = new System.Drawing.Size(223, 34);
            this.txtStandardDays.TabIndex = 3;
            this.txtStandardDays.Text = "0";
            this.txtStandardDays.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // lblWorkHours
            // 
            this.lblWorkHours.AutoSize = true;
            this.lblWorkHours.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblWorkHours.Location = new System.Drawing.Point(30, 108);
            this.lblWorkHours.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblWorkHours.Name = "lblWorkHours";
            this.lblWorkHours.Size = new System.Drawing.Size(110, 28);
            this.lblWorkHours.TabIndex = 4;
            this.lblWorkHours.Text = "Số giờ làm:";
            // 
            // txtWorkHours
            // 
            this.txtWorkHours.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtWorkHours.Location = new System.Drawing.Point(195, 103);
            this.txtWorkHours.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtWorkHours.Name = "txtWorkHours";
            this.txtWorkHours.ReadOnly = true;
            this.txtWorkHours.Size = new System.Drawing.Size(223, 34);
            this.txtWorkHours.TabIndex = 5;
            this.txtWorkHours.Text = "0";
            this.txtWorkHours.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // lblLeaveDays
            // 
            this.lblLeaveDays.AutoSize = true;
            this.lblLeaveDays.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblLeaveDays.Location = new System.Drawing.Point(495, 108);
            this.lblLeaveDays.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblLeaveDays.Name = "lblLeaveDays";
            this.lblLeaveDays.Size = new System.Drawing.Size(131, 28);
            this.lblLeaveDays.TabIndex = 6;
            this.lblLeaveDays.Text = "Số ngày nghỉ:";
            // 
            // txtLeaveDays
            // 
            this.txtLeaveDays.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtLeaveDays.Location = new System.Drawing.Point(675, 103);
            this.txtLeaveDays.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtLeaveDays.Name = "txtLeaveDays";
            this.txtLeaveDays.ReadOnly = true;
            this.txtLeaveDays.Size = new System.Drawing.Size(223, 34);
            this.txtLeaveDays.TabIndex = 7;
            this.txtLeaveDays.Text = "0";
            this.txtLeaveDays.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // lblBaseSalary
            // 
            this.lblBaseSalary.AutoSize = true;
            this.lblBaseSalary.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblBaseSalary.Location = new System.Drawing.Point(30, 385);
            this.lblBaseSalary.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblBaseSalary.Name = "lblBaseSalary";
            this.lblBaseSalary.Size = new System.Drawing.Size(161, 28);
            this.lblBaseSalary.TabIndex = 7;
            this.lblBaseSalary.Text = "Lương cơ bản: (*)";
            // 
            // txtBaseSalary
            // 
            this.txtBaseSalary.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtBaseSalary.Location = new System.Drawing.Point(225, 380);
            this.txtBaseSalary.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtBaseSalary.Name = "txtBaseSalary";
            this.txtBaseSalary.Size = new System.Drawing.Size(748, 34);
            this.txtBaseSalary.TabIndex = 8;
            this.txtBaseSalary.Text = "0";
            this.txtBaseSalary.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtBaseSalary.TextChanged += new System.EventHandler(this.txtSalary_TextChanged);
            this.txtBaseSalary.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtNumber_KeyPress);
            // 
            // lblBonus
            // 
            this.lblBonus.AutoSize = true;
            this.lblBonus.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblBonus.Location = new System.Drawing.Point(30, 446);
            this.lblBonus.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblBonus.Name = "lblBonus";
            this.lblBonus.Size = new System.Drawing.Size(84, 28);
            this.lblBonus.TabIndex = 9;
            this.lblBonus.Text = "Thưởng:";
            // 
            // txtBonus
            // 
            this.txtBonus.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtBonus.Location = new System.Drawing.Point(225, 442);
            this.txtBonus.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtBonus.Name = "txtBonus";
            this.txtBonus.Size = new System.Drawing.Size(748, 34);
            this.txtBonus.TabIndex = 10;
            this.txtBonus.Text = "0";
            this.txtBonus.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtBonus.TextChanged += new System.EventHandler(this.txtSalary_TextChanged);
            this.txtBonus.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtNumber_KeyPress);
            // 
            // lblDeduction
            // 
            this.lblDeduction.AutoSize = true;
            this.lblDeduction.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblDeduction.Location = new System.Drawing.Point(30, 508);
            this.lblDeduction.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblDeduction.Name = "lblDeduction";
            this.lblDeduction.Size = new System.Drawing.Size(91, 28);
            this.lblDeduction.TabIndex = 11;
            this.lblDeduction.Text = "Khấu trừ:";
            // 
            // txtDeduction
            // 
            this.txtDeduction.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtDeduction.Location = new System.Drawing.Point(225, 503);
            this.txtDeduction.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtDeduction.Name = "txtDeduction";
            this.txtDeduction.Size = new System.Drawing.Size(748, 34);
            this.txtDeduction.TabIndex = 12;
            this.txtDeduction.Text = "0";
            this.txtDeduction.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtDeduction.TextChanged += new System.EventHandler(this.txtSalary_TextChanged);
            this.txtDeduction.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtNumber_KeyPress);
            // 
            // btnCalculate
            // 
            this.btnCalculate.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(167)))), ((int)(((byte)(69)))));
            this.btnCalculate.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCalculate.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnCalculate.ForeColor = System.Drawing.Color.White;
            this.btnCalculate.Location = new System.Drawing.Point(225, 565);
            this.btnCalculate.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnCalculate.Name = "btnCalculate";
            this.btnCalculate.Size = new System.Drawing.Size(750, 54);
            this.btnCalculate.TabIndex = 13;
            this.btnCalculate.Text = "🧮 Tính tổng lương";
            this.btnCalculate.UseVisualStyleBackColor = false;
            this.btnCalculate.Click += new System.EventHandler(this.btnCalculate_Click);
            // 
            // lblTotalSalary
            // 
            this.lblTotalSalary.AutoSize = true;
            this.lblTotalSalary.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.lblTotalSalary.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(53)))), ((int)(((byte)(69)))));
            this.lblTotalSalary.Location = new System.Drawing.Point(30, 646);
            this.lblTotalSalary.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblTotalSalary.Name = "lblTotalSalary";
            this.lblTotalSalary.Size = new System.Drawing.Size(140, 30);
            this.lblTotalSalary.TabIndex = 14;
            this.lblTotalSalary.Text = "Tổng lương:";
            // 
            // txtTotalSalary
            // 
            this.txtTotalSalary.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.txtTotalSalary.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(53)))), ((int)(((byte)(69)))));
            this.txtTotalSalary.Location = new System.Drawing.Point(225, 642);
            this.txtTotalSalary.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtTotalSalary.Name = "txtTotalSalary";
            this.txtTotalSalary.ReadOnly = true;
            this.txtTotalSalary.Size = new System.Drawing.Size(748, 37);
            this.txtTotalSalary.TabIndex = 15;
            this.txtTotalSalary.Text = "0";
            this.txtTotalSalary.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblStatus.Location = new System.Drawing.Point(30, 715);
            this.lblStatus.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(127, 28);
            this.lblStatus.TabIndex = 16;
            this.lblStatus.Text = "Trạng thái: (*)";
            // 
            // cboStatus
            // 
            this.cboStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboStatus.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cboStatus.FormattingEnabled = true;
            this.cboStatus.Location = new System.Drawing.Point(225, 711);
            this.cboStatus.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.cboStatus.Name = "cboStatus";
            this.cboStatus.Size = new System.Drawing.Size(748, 36);
            this.cboStatus.TabIndex = 17;
            // 
            // lblPaymentDate
            // 
            this.lblPaymentDate.AutoSize = true;
            this.lblPaymentDate.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblPaymentDate.Location = new System.Drawing.Point(30, 777);
            this.lblPaymentDate.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblPaymentDate.Name = "lblPaymentDate";
            this.lblPaymentDate.Size = new System.Drawing.Size(163, 28);
            this.lblPaymentDate.TabIndex = 18;
            this.lblPaymentDate.Text = "Ngày thanh toán:";
            // 
            // dtpPaymentDate
            // 
            this.dtpPaymentDate.CustomFormat = "dd/MM/yyyy";
            this.dtpPaymentDate.Enabled = false;
            this.dtpPaymentDate.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.dtpPaymentDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpPaymentDate.Location = new System.Drawing.Point(420, 772);
            this.dtpPaymentDate.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.dtpPaymentDate.Name = "dtpPaymentDate";
            this.dtpPaymentDate.Size = new System.Drawing.Size(553, 34);
            this.dtpPaymentDate.TabIndex = 20;
            // 
            // chkHasPaymentDate
            // 
            this.chkHasPaymentDate.AutoSize = true;
            this.chkHasPaymentDate.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.chkHasPaymentDate.Location = new System.Drawing.Point(225, 775);
            this.chkHasPaymentDate.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.chkHasPaymentDate.Name = "chkHasPaymentDate";
            this.chkHasPaymentDate.Size = new System.Drawing.Size(162, 32);
            this.chkHasPaymentDate.TabIndex = 19;
            this.chkHasPaymentDate.Text = "Đã thanh toán";
            this.chkHasPaymentDate.UseVisualStyleBackColor = true;
            this.chkHasPaymentDate.CheckedChanged += new System.EventHandler(this.chkHasPaymentDate_CheckedChanged);
            // 
            // pnlButtons
            // 
            this.pnlButtons.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(249)))), ((int)(((byte)(250)))));
            this.pnlButtons.Controls.Add(this.btnSave);
            this.pnlButtons.Controls.Add(this.btnCancel);
            this.pnlButtons.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlButtons.Location = new System.Drawing.Point(0, 734);
            this.pnlButtons.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.pnlButtons.Name = "pnlButtons";
            this.pnlButtons.Size = new System.Drawing.Size(1050, 83);
            this.pnlButtons.TabIndex = 2;
            // 
            // btnSave
            // 
            this.btnSave.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(123)))), ((int)(((byte)(255)))));
            this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSave.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.btnSave.ForeColor = System.Drawing.Color.White;
            this.btnSave.Location = new System.Drawing.Point(675, 18);
            this.btnSave.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(165, 54);
            this.btnSave.TabIndex = 0;
            this.btnSave.Text = "💾 Lưu";
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(108)))), ((int)(((byte)(117)))), ((int)(((byte)(125)))));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancel.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.btnCancel.ForeColor = System.Drawing.Color.White;
            this.btnCancel.Location = new System.Drawing.Point(855, 18);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(165, 54);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Text = "❌ Hủy";
            this.btnCancel.UseVisualStyleBackColor = false;
            // 
            // frmSalaryDetail
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1050, 817);
            this.Controls.Add(this.pnlMain);
            this.Controls.Add(this.pnlButtons);
            this.Controls.Add(this.pnlTop);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmSalaryDetail";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Chi tiết lương";
            this.Load += new System.EventHandler(this.frmSalaryDetail_Load);
            this.pnlTop.ResumeLayout(false);
            this.pnlTop.PerformLayout();
            this.pnlMain.ResumeLayout(false);
            this.pnlMain.PerformLayout();
            this.grpAttendance.ResumeLayout(false);
            this.grpAttendance.PerformLayout();
            this.pnlButtons.ResumeLayout(false);
            this.ResumeLayout(false);

        }
    }
}
