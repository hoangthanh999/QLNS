namespace Quanlynhansu.Forms
{
    partial class frmSalaryReport
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Panel pnlTop;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage tabSummary;
        private System.Windows.Forms.TabPage tabDepartment;
        private System.Windows.Forms.TabPage tabEmployee;
        private System.Windows.Forms.TabPage tabChart;

        // Tab Summary
        private System.Windows.Forms.Panel pnlSummaryFilter;
        private System.Windows.Forms.Label lblSummaryMonth;
        private System.Windows.Forms.ComboBox cboSummaryMonth;
        private System.Windows.Forms.Label lblSummaryYear;
        private System.Windows.Forms.ComboBox cboSummaryYear;
        private System.Windows.Forms.Button btnSummaryLoad;
        private System.Windows.Forms.DataGridView dgvSummary;
        private System.Windows.Forms.Panel pnlSummaryStats;

        // Tab Department
        private System.Windows.Forms.Panel pnlDeptFilter;
        private System.Windows.Forms.Label lblDeptMonth;
        private System.Windows.Forms.ComboBox cboDeptMonth;
        private System.Windows.Forms.Label lblDeptYear;
        private System.Windows.Forms.ComboBox cboDeptYear;
        private System.Windows.Forms.Button btnDeptLoad;
        private System.Windows.Forms.DataGridView dgvDepartment;

        // Tab Employee
        private System.Windows.Forms.Panel pnlEmpFilter;
        private System.Windows.Forms.Label lblEmployee;
        private System.Windows.Forms.ComboBox cboEmployee;
        private System.Windows.Forms.Label lblFromMonth;
        private System.Windows.Forms.ComboBox cboFromMonth;
        private System.Windows.Forms.Label lblFromYear;
        private System.Windows.Forms.ComboBox cboFromYear;
        private System.Windows.Forms.Label lblToMonth;
        private System.Windows.Forms.ComboBox cboToMonth;
        private System.Windows.Forms.Label lblToYear;
        private System.Windows.Forms.ComboBox cboToYear;
        private System.Windows.Forms.Button btnEmpLoad;
        private System.Windows.Forms.DataGridView dgvEmployee;

        // Tab Chart
        private System.Windows.Forms.Panel pnlChartFilter;
        private System.Windows.Forms.Label lblChartYear;
        private System.Windows.Forms.ComboBox cboChartYear;
        private System.Windows.Forms.Button btnChartLoad;
        private System.Windows.Forms.Panel pnlChart;

        // Bottom buttons
        private System.Windows.Forms.Panel pnlButtons;
        private System.Windows.Forms.Button btnExport;
        private System.Windows.Forms.Button btnPrint;
        private System.Windows.Forms.Button btnClose;

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
            this.tabControl = new System.Windows.Forms.TabControl();
            this.tabSummary = new System.Windows.Forms.TabPage();
            this.dgvSummary = new System.Windows.Forms.DataGridView();
            this.pnlSummaryStats = new System.Windows.Forms.Panel();
            this.pnlSummaryFilter = new System.Windows.Forms.Panel();
            this.lblSummaryMonth = new System.Windows.Forms.Label();
            this.cboSummaryMonth = new System.Windows.Forms.ComboBox();
            this.lblSummaryYear = new System.Windows.Forms.Label();
            this.cboSummaryYear = new System.Windows.Forms.ComboBox();
            this.btnSummaryLoad = new System.Windows.Forms.Button();
            this.tabDepartment = new System.Windows.Forms.TabPage();
            this.dgvDepartment = new System.Windows.Forms.DataGridView();
            this.pnlDeptFilter = new System.Windows.Forms.Panel();
            this.lblDeptMonth = new System.Windows.Forms.Label();
            this.cboDeptMonth = new System.Windows.Forms.ComboBox();
            this.lblDeptYear = new System.Windows.Forms.Label();
            this.cboDeptYear = new System.Windows.Forms.ComboBox();
            this.btnDeptLoad = new System.Windows.Forms.Button();
            this.tabEmployee = new System.Windows.Forms.TabPage();
            this.dgvEmployee = new System.Windows.Forms.DataGridView();
            this.pnlEmpFilter = new System.Windows.Forms.Panel();
            this.lblEmployee = new System.Windows.Forms.Label();
            this.cboEmployee = new System.Windows.Forms.ComboBox();
            this.lblFromMonth = new System.Windows.Forms.Label();
            this.cboFromMonth = new System.Windows.Forms.ComboBox();
            this.lblFromYear = new System.Windows.Forms.Label();
            this.cboFromYear = new System.Windows.Forms.ComboBox();
            this.lblToMonth = new System.Windows.Forms.Label();
            this.cboToMonth = new System.Windows.Forms.ComboBox();
            this.lblToYear = new System.Windows.Forms.Label();
            this.cboToYear = new System.Windows.Forms.ComboBox();
            this.btnEmpLoad = new System.Windows.Forms.Button();
            this.tabChart = new System.Windows.Forms.TabPage();
            this.pnlChart = new System.Windows.Forms.Panel();
            this.pnlChartFilter = new System.Windows.Forms.Panel();
            this.lblChartYear = new System.Windows.Forms.Label();
            this.cboChartYear = new System.Windows.Forms.ComboBox();
            this.btnChartLoad = new System.Windows.Forms.Button();
            this.pnlButtons = new System.Windows.Forms.Panel();
            this.btnExport = new System.Windows.Forms.Button();
            this.btnPrint = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();

            this.pnlTop.SuspendLayout();
            this.tabControl.SuspendLayout();
            this.tabSummary.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSummary)).BeginInit();
            this.pnlSummaryFilter.SuspendLayout();
            this.tabDepartment.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDepartment)).BeginInit();
            this.pnlDeptFilter.SuspendLayout();
            this.tabEmployee.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvEmployee)).BeginInit();
            this.pnlEmpFilter.SuspendLayout();
            this.tabChart.SuspendLayout();
            this.pnlChartFilter.SuspendLayout();
            this.pnlButtons.SuspendLayout();
            this.SuspendLayout();

            // pnlTop
            this.pnlTop.BackColor = System.Drawing.Color.FromArgb(253, 126, 20);
            this.pnlTop.Controls.Add(this.lblTitle);
            this.pnlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTop.Location = new System.Drawing.Point(0, 0);
            this.pnlTop.Name = "pnlTop";
            this.pnlTop.Size = new System.Drawing.Size(1200, 60);
            this.pnlTop.TabIndex = 0;

            // lblTitle
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.White;
            this.lblTitle.Location = new System.Drawing.Point(12, 12);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(250, 30);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "📈 BÁO CÁO LƯƠNG";

            // tabControl
            this.tabControl.Controls.Add(this.tabSummary);
            this.tabControl.Controls.Add(this.tabDepartment);
            this.tabControl.Controls.Add(this.tabEmployee);
            this.tabControl.Controls.Add(this.tabChart);
            this.tabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.tabControl.Location = new System.Drawing.Point(0, 60);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(1200, 550);
            this.tabControl.TabIndex = 1;

            // tabSummary
            this.tabSummary.Controls.Add(this.dgvSummary);
            this.tabSummary.Controls.Add(this.pnlSummaryStats);
            this.tabSummary.Controls.Add(this.pnlSummaryFilter);
            this.tabSummary.Location = new System.Drawing.Point(4, 24);
            this.tabSummary.Name = "tabSummary";
            this.tabSummary.Padding = new System.Windows.Forms.Padding(3);
            this.tabSummary.Size = new System.Drawing.Size(1192, 522);
            this.tabSummary.TabIndex = 0;
            this.tabSummary.Text = "📊 Tổng hợp";
            this.tabSummary.UseVisualStyleBackColor = true;

            // dgvSummary
            this.dgvSummary.AllowUserToAddRows = false;
            this.dgvSummary.AllowUserToDeleteRows = false;
            this.dgvSummary.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvSummary.BackgroundColor = System.Drawing.Color.White;
            this.dgvSummary.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvSummary.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvSummary.Location = new System.Drawing.Point(3, 63);
            this.dgvSummary.Name = "dgvSummary";
            this.dgvSummary.ReadOnly = true;
            this.dgvSummary.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvSummary.Size = new System.Drawing.Size(1186, 356);
            this.dgvSummary.TabIndex = 1;

            // pnlSummaryStats
            this.pnlSummaryStats.BackColor = System.Drawing.Color.FromArgb(248, 249, 250);
            this.pnlSummaryStats.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlSummaryStats.Location = new System.Drawing.Point(3, 419);
            this.pnlSummaryStats.Name = "pnlSummaryStats";
            this.pnlSummaryStats.Size = new System.Drawing.Size(1186, 100);
            this.pnlSummaryStats.TabIndex = 2;

            // pnlSummaryFilter
            this.pnlSummaryFilter.BackColor = System.Drawing.Color.White;
            this.pnlSummaryFilter.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlSummaryFilter.Controls.Add(this.btnSummaryLoad);
            this.pnlSummaryFilter.Controls.Add(this.cboSummaryYear);
            this.pnlSummaryFilter.Controls.Add(this.lblSummaryYear);
            this.pnlSummaryFilter.Controls.Add(this.cboSummaryMonth);
            this.pnlSummaryFilter.Controls.Add(this.lblSummaryMonth);
            this.pnlSummaryFilter.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlSummaryFilter.Location = new System.Drawing.Point(3, 3);
            this.pnlSummaryFilter.Name = "pnlSummaryFilter";
            this.pnlSummaryFilter.Size = new System.Drawing.Size(1186, 60);
            this.pnlSummaryFilter.TabIndex = 0;

            // lblSummaryMonth
            this.lblSummaryMonth.AutoSize = true;
            this.lblSummaryMonth.Location = new System.Drawing.Point(15, 20);
            this.lblSummaryMonth.Name = "lblSummaryMonth";
            this.lblSummaryMonth.Size = new System.Drawing.Size(47, 15);
            this.lblSummaryMonth.TabIndex = 0;
            this.lblSummaryMonth.Text = "Tháng:";

            // cboSummaryMonth
            this.cboSummaryMonth.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboSummaryMonth.FormattingEnabled = true;
            this.cboSummaryMonth.Location = new System.Drawing.Point(75, 17);
            this.cboSummaryMonth.Name = "cboSummaryMonth";
            this.cboSummaryMonth.Size = new System.Drawing.Size(100, 23);
            this.cboSummaryMonth.TabIndex = 1;

            // lblSummaryYear
            this.lblSummaryYear.AutoSize = true;
            this.lblSummaryYear.Location = new System.Drawing.Point(190, 20);
            this.lblSummaryYear.Name = "lblSummaryYear";
            this.lblSummaryYear.Size = new System.Drawing.Size(38, 15);
            this.lblSummaryYear.TabIndex = 2;
            this.lblSummaryYear.Text = "Năm:";

            // cboSummaryYear
            this.cboSummaryYear.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboSummaryYear.FormattingEnabled = true;
            this.cboSummaryYear.Location = new System.Drawing.Point(240, 17);
            this.cboSummaryYear.Name = "cboSummaryYear";
            this.cboSummaryYear.Size = new System.Drawing.Size(100, 23);
            this.cboSummaryYear.TabIndex = 3;

            // btnSummaryLoad
            this.btnSummaryLoad.BackColor = System.Drawing.Color.FromArgb(0, 123, 255);
            this.btnSummaryLoad.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSummaryLoad.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnSummaryLoad.ForeColor = System.Drawing.Color.White;
            this.btnSummaryLoad.Location = new System.Drawing.Point(360, 15);
            this.btnSummaryLoad.Name = "btnSummaryLoad";
            this.btnSummaryLoad.Size = new System.Drawing.Size(120, 30);
            this.btnSummaryLoad.TabIndex = 4;
            this.btnSummaryLoad.Text = "📊 Xem báo cáo";
            this.btnSummaryLoad.UseVisualStyleBackColor = false;
            this.btnSummaryLoad.Click += new System.EventHandler(this.btnSummaryLoad_Click);

            // tabDepartment
            this.tabDepartment.Controls.Add(this.dgvDepartment);
            this.tabDepartment.Controls.Add(this.pnlDeptFilter);
            this.tabDepartment.Location = new System.Drawing.Point(4, 24);
            this.tabDepartment.Name = "tabDepartment";
            this.tabDepartment.Padding = new System.Windows.Forms.Padding(3);
            this.tabDepartment.Size = new System.Drawing.Size(1192, 522);
            this.tabDepartment.TabIndex = 1;
            this.tabDepartment.Text = "🏢 Theo phòng ban";
            this.tabDepartment.UseVisualStyleBackColor = true;

            // dgvDepartment
            this.dgvDepartment.AllowUserToAddRows = false;
            this.dgvDepartment.AllowUserToDeleteRows = false;
            this.dgvDepartment.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvDepartment.BackgroundColor = System.Drawing.Color.White;
            this.dgvDepartment.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDepartment.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvDepartment.Location = new System.Drawing.Point(3, 63);
            this.dgvDepartment.Name = "dgvDepartment";
            this.dgvDepartment.ReadOnly = true;
            this.dgvDepartment.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvDepartment.Size = new System.Drawing.Size(1186, 456);
            this.dgvDepartment.TabIndex = 1;

            // pnlDeptFilter
            this.pnlDeptFilter.BackColor = System.Drawing.Color.White;
            this.pnlDeptFilter.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlDeptFilter.Controls.Add(this.btnDeptLoad);
            this.pnlDeptFilter.Controls.Add(this.cboDeptYear);
            this.pnlDeptFilter.Controls.Add(this.lblDeptYear);
            this.pnlDeptFilter.Controls.Add(this.cboDeptMonth);
            this.pnlDeptFilter.Controls.Add(this.lblDeptMonth);
            this.pnlDeptFilter.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlDeptFilter.Location = new System.Drawing.Point(3, 3);
            this.pnlDeptFilter.Name = "pnlDeptFilter";
            this.pnlDeptFilter.Size = new System.Drawing.Size(1186, 60);
            this.pnlDeptFilter.TabIndex = 0;

            // lblDeptMonth
            this.lblDeptMonth.AutoSize = true;
            this.lblDeptMonth.Location = new System.Drawing.Point(15, 20);
            this.lblDeptMonth.Name = "lblDeptMonth";
            this.lblDeptMonth.Size = new System.Drawing.Size(47, 15);
            this.lblDeptMonth.TabIndex = 0;
            this.lblDeptMonth.Text = "Tháng:";

            // cboDeptMonth
            this.cboDeptMonth.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboDeptMonth.FormattingEnabled = true;
            this.cboDeptMonth.Location = new System.Drawing.Point(75, 17);
            this.cboDeptMonth.Name = "cboDeptMonth";
            this.cboDeptMonth.Size = new System.Drawing.Size(100, 23);
            this.cboDeptMonth.TabIndex = 1;

            // lblDeptYear
            this.lblDeptYear.AutoSize = true;
            this.lblDeptYear.Location = new System.Drawing.Point(190, 20);
            this.lblDeptYear.Name = "lblDeptYear";
            this.lblDeptYear.Size = new System.Drawing.Size(38, 15);
            this.lblDeptYear.TabIndex = 2;
            this.lblDeptYear.Text = "Năm:";

            // cboDeptYear
            this.cboDeptYear.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboDeptYear.FormattingEnabled = true;
            this.cboDeptYear.Location = new System.Drawing.Point(240, 17);
            this.cboDeptYear.Name = "cboDeptYear";
            this.cboDeptYear.Size = new System.Drawing.Size(100, 23);
            this.cboDeptYear.TabIndex = 3;

            // btnDeptLoad
            this.btnDeptLoad.BackColor = System.Drawing.Color.FromArgb(0, 123, 255);
            this.btnDeptLoad.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDeptLoad.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnDeptLoad.ForeColor = System.Drawing.Color.White;
            this.btnDeptLoad.Location = new System.Drawing.Point(360, 15);
            this.btnDeptLoad.Name = "btnDeptLoad";
            this.btnDeptLoad.Size = new System.Drawing.Size(120, 30);
            this.btnDeptLoad.TabIndex = 4;
            this.btnDeptLoad.Text = "📊 Xem báo cáo";
            this.btnDeptLoad.UseVisualStyleBackColor = false;
            this.btnDeptLoad.Click += new System.EventHandler(this.btnDeptLoad_Click);

            // tabEmployee
            this.tabEmployee.Controls.Add(this.dgvEmployee);
            this.tabEmployee.Controls.Add(this.pnlEmpFilter);
            this.tabEmployee.Location = new System.Drawing.Point(4, 24);
            this.tabEmployee.Name = "tabEmployee";
            this.tabEmployee.Size = new System.Drawing.Size(1192, 522);
            this.tabEmployee.TabIndex = 2;
            this.tabEmployee.Text = "👤 Theo nhân viên";
            this.tabEmployee.UseVisualStyleBackColor = true;

            // dgvEmployee
            this.dgvEmployee.AllowUserToAddRows = false;
            this.dgvEmployee.AllowUserToDeleteRows = false;
            this.dgvEmployee.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvEmployee.BackgroundColor = System.Drawing.Color.White;
            this.dgvEmployee.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvEmployee.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvEmployee.Location = new System.Drawing.Point(0, 100);
            this.dgvEmployee.Name = "dgvEmployee";
            this.dgvEmployee.ReadOnly = true;
            this.dgvEmployee.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvEmployee.Size = new System.Drawing.Size(1192, 422);
            this.dgvEmployee.TabIndex = 1;

            // pnlEmpFilter
            this.pnlEmpFilter.BackColor = System.Drawing.Color.White;
            this.pnlEmpFilter.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlEmpFilter.Controls.Add(this.btnEmpLoad);
            this.pnlEmpFilter.Controls.Add(this.cboToYear);
            this.pnlEmpFilter.Controls.Add(this.lblToYear);
            this.pnlEmpFilter.Controls.Add(this.cboToMonth);
            this.pnlEmpFilter.Controls.Add(this.lblToMonth);
            this.pnlEmpFilter.Controls.Add(this.cboFromYear);
            this.pnlEmpFilter.Controls.Add(this.lblFromYear);
            this.pnlEmpFilter.Controls.Add(this.cboFromMonth);
            this.pnlEmpFilter.Controls.Add(this.lblFromMonth);
            this.pnlEmpFilter.Controls.Add(this.cboEmployee);
            this.pnlEmpFilter.Controls.Add(this.lblEmployee);
            this.pnlEmpFilter.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlEmpFilter.Location = new System.Drawing.Point(0, 0);
            this.pnlEmpFilter.Name = "pnlEmpFilter";
            this.pnlEmpFilter.Size = new System.Drawing.Size(1192, 100);
            this.pnlEmpFilter.TabIndex = 0;

            // lblEmployee
            this.lblEmployee.AutoSize = true;
            this.lblEmployee.Location = new System.Drawing.Point(15, 20);
            this.lblEmployee.Name = "lblEmployee";
            this.lblEmployee.Size = new System.Drawing.Size(69, 15);
            this.lblEmployee.TabIndex = 0;
            this.lblEmployee.Text = "Nhân viên:";

            // cboEmployee
            this.cboEmployee.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboEmployee.FormattingEnabled = true;
            this.cboEmployee.Location = new System.Drawing.Point(100, 17);
            this.cboEmployee.Name = "cboEmployee";
            this.cboEmployee.Size = new System.Drawing.Size(400, 23);
            this.cboEmployee.TabIndex = 1;

            // lblFromMonth
            this.lblFromMonth.AutoSize = true;
            this.lblFromMonth.Location = new System.Drawing.Point(15, 60);
            this.lblFromMonth.Name = "lblFromMonth";
            this.lblFromMonth.Size = new System.Drawing.Size(63, 15);
            this.lblFromMonth.TabIndex = 2;
            this.lblFromMonth.Text = "Từ tháng:";

            // cboFromMonth
            this.cboFromMonth.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboFromMonth.FormattingEnabled = true;
            this.cboFromMonth.Location = new System.Drawing.Point(100, 57);
            this.cboFromMonth.Name = "cboFromMonth";
            this.cboFromMonth.Size = new System.Drawing.Size(80, 23);
            this.cboFromMonth.TabIndex = 3;

            // lblFromYear
            this.lblFromYear.AutoSize = true;
            this.lblFromYear.Location = new System.Drawing.Point(190, 60);
            this.lblFromYear.Name = "lblFromYear";
            this.lblFromYear.Size = new System.Drawing.Size(38, 15);
            this.lblFromYear.TabIndex = 4;
            this.lblFromYear.Text = "Năm:";

            // cboFromYear
            this.cboFromYear.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboFromYear.FormattingEnabled = true;
            this.cboFromYear.Location = new System.Drawing.Point(240, 57);
            this.cboFromYear.Name = "cboFromYear";
            this.cboFromYear.Size = new System.Drawing.Size(100, 23);
            this.cboFromYear.TabIndex = 5;

            // lblToMonth
            this.lblToMonth.AutoSize = true;
            this.lblToMonth.Location = new System.Drawing.Point(360, 60);
            this.lblToMonth.Name = "lblToMonth";
            this.lblToMonth.Size = new System.Drawing.Size(72, 15);
            this.lblToMonth.TabIndex = 6;
            this.lblToMonth.Text = "Đến tháng:";

            // cboToMonth
            this.cboToMonth.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboToMonth.FormattingEnabled = true;
            this.cboToMonth.Location = new System.Drawing.Point(445, 57);
            this.cboToMonth.Name = "cboToMonth";
            this.cboToMonth.Size = new System.Drawing.Size(80, 23);
            this.cboToMonth.TabIndex = 7;

            // lblToYear
            this.lblToYear.AutoSize = true;
            this.lblToYear.Location = new System.Drawing.Point(535, 60);
            this.lblToYear.Name = "lblToYear";
            this.lblToYear.Size = new System.Drawing.Size(38, 15);
            this.lblToYear.TabIndex = 8;
            this.lblToYear.Text = "Năm:";

            // cboToYear
            this.cboToYear.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboToYear.FormattingEnabled = true;
            this.cboToYear.Location = new System.Drawing.Point(585, 57);
            this.cboToYear.Name = "cboToYear";
            this.cboToYear.Size = new System.Drawing.Size(100, 23);
            this.cboToYear.TabIndex = 9;

            // btnEmpLoad
            this.btnEmpLoad.BackColor = System.Drawing.Color.FromArgb(0, 123, 255);
            this.btnEmpLoad.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEmpLoad.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnEmpLoad.ForeColor = System.Drawing.Color.White;
            this.btnEmpLoad.Location = new System.Drawing.Point(705, 55);
            this.btnEmpLoad.Name = "btnEmpLoad";
            this.btnEmpLoad.Size = new System.Drawing.Size(120, 30);
            this.btnEmpLoad.TabIndex = 10;
            this.btnEmpLoad.Text = "📊 Xem báo cáo";
            this.btnEmpLoad.UseVisualStyleBackColor = false;
            this.btnEmpLoad.Click += new System.EventHandler(this.btnEmpLoad_Click);

            // tabChart
            this.tabChart.Controls.Add(this.pnlChart);
            this.tabChart.Controls.Add(this.pnlChartFilter);
            this.tabChart.Location = new System.Drawing.Point(4, 24);
            this.tabChart.Name = "tabChart";
            this.tabChart.Size = new System.Drawing.Size(1192, 522);
            this.tabChart.TabIndex = 3;
            this.tabChart.Text = "📈 Biểu đồ";
            this.tabChart.UseVisualStyleBackColor = true;

            // pnlChart
            this.pnlChart.BackColor = System.Drawing.Color.White;
            this.pnlChart.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlChart.Location = new System.Drawing.Point(0, 60);
            this.pnlChart.Name = "pnlChart";
            this.pnlChart.Size = new System.Drawing.Size(1192, 462);
            this.pnlChart.TabIndex = 1;

            // pnlChartFilter
            this.pnlChartFilter.BackColor = System.Drawing.Color.White;
            this.pnlChartFilter.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlChartFilter.Controls.Add(this.btnChartLoad);
            this.pnlChartFilter.Controls.Add(this.cboChartYear);
            this.pnlChartFilter.Controls.Add(this.lblChartYear);
            this.pnlChartFilter.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlChartFilter.Location = new System.Drawing.Point(0, 0);
            this.pnlChartFilter.Name = "pnlChartFilter";
            this.pnlChartFilter.Size = new System.Drawing.Size(1192, 60);
            this.pnlChartFilter.TabIndex = 0;

            // lblChartYear
            this.lblChartYear.AutoSize = true;
            this.lblChartYear.Location = new System.Drawing.Point(15, 20);
            this.lblChartYear.Name = "lblChartYear";
            this.lblChartYear.Size = new System.Drawing.Size(38, 15);
            this.lblChartYear.TabIndex = 0;
            this.lblChartYear.Text = "Năm:";

            // cboChartYear
            this.cboChartYear.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboChartYear.FormattingEnabled = true;
            this.cboChartYear.Location = new System.Drawing.Point(65, 17);
            this.cboChartYear.Name = "cboChartYear";
            this.cboChartYear.Size = new System.Drawing.Size(120, 23);
            this.cboChartYear.TabIndex = 1;

            // btnChartLoad
            this.btnChartLoad.BackColor = System.Drawing.Color.FromArgb(0, 123, 255);
            this.btnChartLoad.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnChartLoad.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnChartLoad.ForeColor = System.Drawing.Color.White;
            this.btnChartLoad.Location = new System.Drawing.Point(200, 15);
            this.btnChartLoad.Name = "btnChartLoad";
            this.btnChartLoad.Size = new System.Drawing.Size(120, 30);
            this.btnChartLoad.TabIndex = 2;
            this.btnChartLoad.Text = "📊 Xem biểu đồ";
            this.btnChartLoad.UseVisualStyleBackColor = false;
            this.btnChartLoad.Click += new System.EventHandler(this.btnChartLoad_Click);

            // pnlButtons
            this.pnlButtons.BackColor = System.Drawing.Color.FromArgb(248, 249, 250);
            this.pnlButtons.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlButtons.Controls.Add(this.btnClose);
            this.pnlButtons.Controls.Add(this.btnPrint);
            this.pnlButtons.Controls.Add(this.btnExport);
            this.pnlButtons.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlButtons.Location = new System.Drawing.Point(0, 610);
            this.pnlButtons.Name = "pnlButtons";
            this.pnlButtons.Size = new System.Drawing.Size(1200, 60);
            this.pnlButtons.TabIndex = 2;

            // btnExport
            this.btnExport.BackColor = System.Drawing.Color.FromArgb(40, 167, 69);
            this.btnExport.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnExport.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnExport.ForeColor = System.Drawing.Color.White;
            this.btnExport.Location = new System.Drawing.Point(850, 12);
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(130, 35);
            this.btnExport.TabIndex = 0;
            this.btnExport.Text = "📊 Xuất Excel";
            this.btnExport.UseVisualStyleBackColor = false;
            this.btnExport.Click += new System.EventHandler(this.btnExport_Click);

            // btnPrint
            this.btnPrint.BackColor = System.Drawing.Color.FromArgb(0, 123, 255);
            this.btnPrint.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPrint.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnPrint.ForeColor = System.Drawing.Color.White;
            this.btnPrint.Location = new System.Drawing.Point(990, 12);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(90, 35);
            this.btnPrint.TabIndex = 1;
            this.btnPrint.Text = "🖨️ In";
            this.btnPrint.UseVisualStyleBackColor = false;
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);

            // btnClose
            this.btnClose.BackColor = System.Drawing.Color.FromArgb(108, 117, 125);
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClose.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnClose.ForeColor = System.Drawing.Color.White;
            this.btnClose.Location = new System.Drawing.Point(1090, 12);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(90, 35);
            this.btnClose.TabIndex = 2;
            this.btnClose.Text = "❌ Đóng";
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);

            // frmSalaryReport
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1200, 670);
            this.Controls.Add(this.tabControl);
            this.Controls.Add(this.pnlButtons);
            this.Controls.Add(this.pnlTop);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "frmSalaryReport";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Báo cáo lương";
            this.Load += new System.EventHandler(this.frmSalaryReport_Load);

            this.pnlTop.ResumeLayout(false);
            this.pnlTop.PerformLayout();
            this.tabControl.ResumeLayout(false);
            this.tabSummary.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvSummary)).EndInit();
            this.pnlSummaryFilter.ResumeLayout(false);
            this.pnlSummaryFilter.PerformLayout();
            this.tabDepartment.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvDepartment)).EndInit();
            this.pnlDeptFilter.ResumeLayout(false);
            this.pnlDeptFilter.PerformLayout();
            this.tabEmployee.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvEmployee)).EndInit();
            this.pnlEmpFilter.ResumeLayout(false);
            this.pnlEmpFilter.PerformLayout();
            this.tabChart.ResumeLayout(false);
            this.pnlChartFilter.ResumeLayout(false);
            this.pnlChartFilter.PerformLayout();
            this.pnlButtons.ResumeLayout(false);
            this.ResumeLayout(false);
        }

    }
}

