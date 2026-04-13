using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace Quanlynhansu.Forms
{
    partial class frmEmployeeReport
    {
        private System.ComponentModel.IContainer components = null;
        private TabControl tabControl1;
        private TabPage tabOverview;
        private TabPage tabDepartment;
        private TabPage tabPosition;
        private TabPage tabStatus;
        private TabPage tabDetail;
        private Panel panelTop;
        private Label lblTitle;
        private Panel panelSearch;
        private TextBox txtSearch;
        private ComboBox cboDepartment;
        private ComboBox cboPosition;
        private ComboBox cboStatus;
        private Button btnSearch;
        private Button btnRefresh;
        private Button btnExportExcel;
        private Button btnPrint;

        // Overview tab
        private Panel panelStats;
        private Label lblTotalEmployees;
        private Label lblActiveEmployees;
        private Label lblInactiveEmployees;
        private Label lblMaleEmployees;
        private Label lblFemaleEmployees;
        private Label lblAvgSalary;
        private Chart chartDepartment;

        // Department tab
        private DataGridView dgvDepartmentReport;

        // Position tab
        private DataGridView dgvPositionReport;

        // Status tab
        private DataGridView dgvStatusReport;

        // Detail tab
        private DataGridView dgvEmployeeList;
        private Label lblTotalRecords;

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
            this.components = new System.ComponentModel.Container();

            // Initialize controls
            this.tabControl1 = new TabControl();
            this.tabOverview = new TabPage();
            this.tabDepartment = new TabPage();
            this.tabPosition = new TabPage();
            this.tabStatus = new TabPage();
            this.tabDetail = new TabPage();
            this.panelTop = new Panel();
            this.lblTitle = new Label();
            this.panelSearch = new Panel();
            this.txtSearch = new TextBox();
            this.cboDepartment = new ComboBox();
            this.cboPosition = new ComboBox();
            this.cboStatus = new ComboBox();
            this.btnSearch = new Button();
            this.btnRefresh = new Button();
            this.btnExportExcel = new Button();
            this.btnPrint = new Button();
            this.panelStats = new Panel();
            this.chartDepartment = new Chart();
            this.dgvDepartmentReport = new DataGridView();
            this.dgvPositionReport = new DataGridView();
            this.dgvStatusReport = new DataGridView();
            this.dgvEmployeeList = new DataGridView();
            this.lblTotalRecords = new Label();

            // Stats labels
            this.lblTotalEmployees = new Label();
            this.lblActiveEmployees = new Label();
            this.lblInactiveEmployees = new Label();
            this.lblMaleEmployees = new Label();
            this.lblFemaleEmployees = new Label();
            this.lblAvgSalary = new Label();

            this.tabControl1.SuspendLayout();
            this.tabOverview.SuspendLayout();
            this.tabDepartment.SuspendLayout();
            this.tabPosition.SuspendLayout();
            this.tabStatus.SuspendLayout();
            this.tabDetail.SuspendLayout();
            this.panelTop.SuspendLayout();
            this.panelSearch.SuspendLayout();
            this.panelStats.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chartDepartment)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDepartmentReport)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPositionReport)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvStatusReport)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvEmployeeList)).BeginInit();
            this.SuspendLayout();

            // 
            // panelTop
            // 
            this.panelTop.BackColor = Color.FromArgb(52, 152, 219);
            this.panelTop.Controls.Add(this.lblTitle);
            this.panelTop.Dock = DockStyle.Top;
            this.panelTop.Location = new Point(0, 0);
            this.panelTop.Name = "panelTop";
            this.panelTop.Size = new Size(1200, 60);
            this.panelTop.TabIndex = 0;

            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new Font("Segoe UI", 18F, FontStyle.Bold);
            this.lblTitle.ForeColor = Color.White;
            this.lblTitle.Location = new Point(20, 15);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new Size(280, 32);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "📊 BÁO CÁO NHÂN SỰ";

            // 
            // panelSearch
            // 
            this.panelSearch.BackColor = Color.White;
            this.panelSearch.Controls.Add(new Label { Text = "Tìm kiếm:", Location = new Point(20, 15), AutoSize = true });
            this.panelSearch.Controls.Add(this.txtSearch);
            this.panelSearch.Controls.Add(new Label { Text = "Phòng ban:", Location = new Point(320, 15), AutoSize = true });
            this.panelSearch.Controls.Add(this.cboDepartment);
            this.panelSearch.Controls.Add(new Label { Text = "Chức vụ:", Location = new Point(570, 15), AutoSize = true });
            this.panelSearch.Controls.Add(this.cboPosition);
            this.panelSearch.Controls.Add(new Label { Text = "Trạng thái:", Location = new Point(820, 15), AutoSize = true });
            this.panelSearch.Controls.Add(this.cboStatus);
            this.panelSearch.Controls.Add(this.btnSearch);
            this.panelSearch.Controls.Add(this.btnRefresh);
            this.panelSearch.Controls.Add(this.btnExportExcel);
            this.panelSearch.Controls.Add(this.btnPrint);
            this.panelSearch.Dock = DockStyle.Top;
            this.panelSearch.Location = new Point(0, 60);
            this.panelSearch.Name = "panelSearch";
            this.panelSearch.Size = new Size(1200, 80);
            this.panelSearch.TabIndex = 1;

            // 
            // txtSearch
            // 
            this.txtSearch.Font = new Font("Segoe UI", 10F);
            this.txtSearch.Location = new Point(20, 40);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new Size(250, 25);
            this.txtSearch.TabIndex = 0;
          

            this.txtSearch.GotFocus += (s, e) =>
            {
                if (txtSearch.Text == "Nhập tên hoặc mã NV...")
                {
                    txtSearch.Text = "";
                    txtSearch.ForeColor = Color.Black;
                }
            };

            this.txtSearch.LostFocus += (s, e) =>
            {
                if (string.IsNullOrWhiteSpace(txtSearch.Text))
                {
                    txtSearch.Text = "Nhập tên hoặc mã NV...";
                    txtSearch.ForeColor = Color.Gray;
                }
            };
            // 
            // cboDepartment
            // 
            this.cboDepartment.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cboDepartment.Font = new Font("Segoe UI", 10F);
            this.cboDepartment.Location = new Point(320, 40);
            this.cboDepartment.Name = "cboDepartment";
            this.cboDepartment.Size = new Size(200, 25);
            this.cboDepartment.TabIndex = 1;

            // 
            // cboPosition
            // 
            this.cboPosition.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cboPosition.Font = new Font("Segoe UI", 10F);
            this.cboPosition.Location = new Point(570, 40);
            this.cboPosition.Name = "cboPosition";
            this.cboPosition.Size = new Size(200, 25);
            this.cboPosition.TabIndex = 2;

            // 
            // cboStatus
            // 
            this.cboStatus.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cboStatus.Font = new Font("Segoe UI", 10F);
            this.cboStatus.Items.AddRange(new object[] { "", "Đang làm việc", "Nghỉ việc", "Tạm nghỉ" });
            this.cboStatus.Location = new Point(820, 40);
            this.cboStatus.Name = "cboStatus";
            this.cboStatus.Size = new Size(150, 25);
            this.cboStatus.TabIndex = 3;

            // 
            // btnSearch
            // 
            this.btnSearch.BackColor = Color.FromArgb(52, 152, 219);
            this.btnSearch.FlatStyle = FlatStyle.Flat;
            this.btnSearch.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            this.btnSearch.ForeColor = Color.White;
            this.btnSearch.Location = new Point(990, 35);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new Size(90, 35);
            this.btnSearch.TabIndex = 4;
            this.btnSearch.Text = "🔍 Tìm";
            this.btnSearch.UseVisualStyleBackColor = false;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);

            // 
            // btnRefresh
            // 
            this.btnRefresh.BackColor = Color.FromArgb(46, 204, 113);
            this.btnRefresh.FlatStyle = FlatStyle.Flat;
            this.btnRefresh.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            this.btnRefresh.ForeColor = Color.White;
            this.btnRefresh.Location = new Point(1090, 35);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new Size(90, 35);
            this.btnRefresh.TabIndex = 5;
            this.btnRefresh.Text = "🔄 Làm mới";
            this.btnRefresh.UseVisualStyleBackColor = false;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);

            // 
            // btnExportExcel
            // 
            this.btnExportExcel.BackColor = Color.FromArgb(39, 174, 96);
            this.btnExportExcel.FlatStyle = FlatStyle.Flat;
            this.btnExportExcel.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            this.btnExportExcel.ForeColor = Color.White;
            this.btnExportExcel.Location = new Point(990, 5);
            this.btnExportExcel.Name = "btnExportExcel";
            this.btnExportExcel.Size = new Size(90, 25);
            this.btnExportExcel.TabIndex = 6;
            this.btnExportExcel.Text = "📊 Excel";
            this.btnExportExcel.UseVisualStyleBackColor = false;
            this.btnExportExcel.Click += new System.EventHandler(this.btnExportExcel_Click);

            // 
            // btnPrint
            // 
            this.btnPrint.BackColor = Color.FromArgb(155, 89, 182);
            this.btnPrint.FlatStyle = FlatStyle.Flat;
            this.btnPrint.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            this.btnPrint.ForeColor = Color.White;
            this.btnPrint.Location = new Point(1090, 5);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new Size(90, 25);
            this.btnPrint.TabIndex = 7;
            this.btnPrint.Text = "🖨️ In";
            this.btnPrint.UseVisualStyleBackColor = false;
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);

            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabOverview);
            this.tabControl1.Controls.Add(this.tabDepartment);
            this.tabControl1.Controls.Add(this.tabPosition);
            this.tabControl1.Controls.Add(this.tabStatus);
            this.tabControl1.Controls.Add(this.tabDetail);
            this.tabControl1.Dock = DockStyle.Fill;
            this.tabControl1.Font = new Font("Segoe UI", 10F);
            this.tabControl1.Location = new Point(0, 140);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new Size(1200, 560);
            this.tabControl1.TabIndex = 2;

            // 
            // tabOverview - TỔNG QUAN
            // 
            this.tabOverview.BackColor = Color.FromArgb(236, 240, 241);
            this.tabOverview.Controls.Add(this.panelStats);
            this.tabOverview.Controls.Add(this.chartDepartment);
            this.tabOverview.Location = new Point(4, 26);
            this.tabOverview.Name = "tabOverview";
            this.tabOverview.Padding = new Padding(3);
            this.tabOverview.Size = new Size(1192, 530);
            this.tabOverview.TabIndex = 0;
            this.tabOverview.Text = "📈 Tổng quan";

            // 
            // panelStats
            // 
            this.panelStats.BackColor = Color.White;
            this.panelStats.Dock = DockStyle.Top;
            this.panelStats.Location = new Point(3, 3);
            this.panelStats.Name = "panelStats";
            this.panelStats.Size = new Size(1186, 150);
            this.panelStats.TabIndex = 0;

            // Thêm các label thống kê
            AddStatCard(this.panelStats, "Tổng số NV", this.lblTotalEmployees, 20, 20, Color.FromArgb(52, 152, 219));
            AddStatCard(this.panelStats, "Đang làm việc", this.lblActiveEmployees, 220, 20, Color.FromArgb(46, 204, 113));
            AddStatCard(this.panelStats, "Nghỉ việc", this.lblInactiveEmployees, 420, 20, Color.FromArgb(231, 76, 60));
            AddStatCard(this.panelStats, "Nam", this.lblMaleEmployees, 620, 20, Color.FromArgb(52, 73, 94));
            AddStatCard(this.panelStats, "Nữ", this.lblFemaleEmployees, 820, 20, Color.FromArgb(155, 89, 182));
            AddStatCard(this.panelStats, "Lương TB", this.lblAvgSalary, 1020, 20, Color.FromArgb(243, 156, 18));

            // 
            // chartDepartment
            // 
            this.chartDepartment.BackColor = Color.White;
            this.chartDepartment.Dock = DockStyle.Fill;
            this.chartDepartment.Location = new Point(3, 153);
            this.chartDepartment.Name = "chartDepartment";
            this.chartDepartment.Size = new Size(1186, 374);
            this.chartDepartment.TabIndex = 1;
            this.chartDepartment.Text = "chart1";
            ChartArea chartArea = new ChartArea();
            this.chartDepartment.ChartAreas.Add(chartArea);

            // 
            // tabDepartment - PHÒNG BAN
            // 
            this.tabDepartment.BackColor = Color.White;
            this.tabDepartment.Controls.Add(this.dgvDepartmentReport);
            this.tabDepartment.Location = new Point(4, 26);
            this.tabDepartment.Name = "tabDepartment";
            this.tabDepartment.Padding = new Padding(3);
            this.tabDepartment.Size = new Size(1192, 530);
            this.tabDepartment.TabIndex = 1;
            this.tabDepartment.Text = "🏢 Theo phòng ban";

            // 
            // dgvDepartmentReport
            // 
            this.dgvDepartmentReport.AllowUserToAddRows = false;
            this.dgvDepartmentReport.AllowUserToDeleteRows = false;
            this.dgvDepartmentReport.BackgroundColor = Color.White;
            this.dgvDepartmentReport.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDepartmentReport.Dock = DockStyle.Fill;
            this.dgvDepartmentReport.Location = new Point(3, 3);
            this.dgvDepartmentReport.Name = "dgvDepartmentReport";
            this.dgvDepartmentReport.ReadOnly = true;
            this.dgvDepartmentReport.RowHeadersWidth = 51;
            this.dgvDepartmentReport.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            this.dgvDepartmentReport.Size = new Size(1186, 524);
            this.dgvDepartmentReport.TabIndex = 0;

            // 
            // tabPosition - CHỨC VỤ
            // 
            this.tabPosition.BackColor = Color.White;
            this.tabPosition.Controls.Add(this.dgvPositionReport);
            this.tabPosition.Location = new Point(4, 26);
            this.tabPosition.Name = "tabPosition";
            this.tabPosition.Size = new Size(1192, 530);
            this.tabPosition.TabIndex = 2;
            this.tabPosition.Text = "💼 Theo chức vụ";

            // 
            // dgvPositionReport
            // 
            this.dgvPositionReport.AllowUserToAddRows = false;
            this.dgvPositionReport.AllowUserToDeleteRows = false;
            this.dgvPositionReport.BackgroundColor = Color.White;
            this.dgvPositionReport.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvPositionReport.Dock = DockStyle.Fill;
            this.dgvPositionReport.Location = new Point(0, 0);
            this.dgvPositionReport.Name = "dgvPositionReport";
            this.dgvPositionReport.ReadOnly = true;
            this.dgvPositionReport.RowHeadersWidth = 51;
            this.dgvPositionReport.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            this.dgvPositionReport.Size = new Size(1192, 530);
            this.dgvPositionReport.TabIndex = 0;

            // 
            // tabStatus - TRẠNG THÁI
            // 
            this.tabStatus.BackColor = Color.White;
            this.tabStatus.Controls.Add(this.dgvStatusReport);
            this.tabStatus.Location = new Point(4, 26);
            this.tabStatus.Name = "tabStatus";
            this.tabStatus.Size = new Size(1192, 530);
            this.tabStatus.TabIndex = 3;
            this.tabStatus.Text = "📊 Theo trạng thái";

            // 
            // dgvStatusReport
            // 
            this.dgvStatusReport.AllowUserToAddRows = false;
            this.dgvStatusReport.AllowUserToDeleteRows = false;
            this.dgvStatusReport.BackgroundColor = Color.White;
            this.dgvStatusReport.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvStatusReport.Dock = DockStyle.Fill;
            this.dgvStatusReport.Location = new Point(0, 0);
            this.dgvStatusReport.Name = "dgvStatusReport";
            this.dgvStatusReport.ReadOnly = true;
            this.dgvStatusReport.RowHeadersWidth = 51;
            this.dgvStatusReport.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            this.dgvStatusReport.Size = new Size(1192, 530);
            this.dgvStatusReport.TabIndex = 0;

            // 
            // tabDetail - CHI TIẾT
            // 
            this.tabDetail.BackColor = Color.White;
            this.tabDetail.Controls.Add(this.dgvEmployeeList);
            this.tabDetail.Controls.Add(this.lblTotalRecords);
            this.tabDetail.Location = new Point(4, 26);
            this.tabDetail.Name = "tabDetail";
            this.tabDetail.Size = new Size(1192, 530);
            this.tabDetail.TabIndex = 4;
            this.tabDetail.Text = "📋 Chi tiết";

            // 
            // dgvEmployeeList
            // 
            this.dgvEmployeeList.AllowUserToAddRows = false;
            this.dgvEmployeeList.AllowUserToDeleteRows = false;
            this.dgvEmployeeList.BackgroundColor = Color.White;
            this.dgvEmployeeList.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvEmployeeList.Dock = DockStyle.Fill;
            this.dgvEmployeeList.Location = new Point(0, 0);
            this.dgvEmployeeList.Name = "dgvEmployeeList";
            this.dgvEmployeeList.ReadOnly = true;
            this.dgvEmployeeList.RowHeadersWidth = 51;
            this.dgvEmployeeList.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            this.dgvEmployeeList.Size = new Size(1192, 500);
            this.dgvEmployeeList.TabIndex = 0;

            // 
            // lblTotalRecords
            // 
            this.lblTotalRecords.BackColor = Color.FromArgb(52, 152, 219);
            this.lblTotalRecords.Dock = DockStyle.Bottom;
            this.lblTotalRecords.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            this.lblTotalRecords.ForeColor = Color.White;
            this.lblTotalRecords.Location = new Point(0, 500);
            this.lblTotalRecords.Name = "lblTotalRecords";
            this.lblTotalRecords.Padding = new Padding(10, 0, 0, 0);
            this.lblTotalRecords.Size = new Size(1192, 30);
            this.lblTotalRecords.TabIndex = 1;
            this.lblTotalRecords.Text = "Tổng số: 0 nhân viên";
            this.lblTotalRecords.TextAlign = ContentAlignment.MiddleLeft;

            // 
            // frmEmployeeReport
            // 
            this.AutoScaleDimensions = new SizeF(7F, 17F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.ClientSize = new Size(1200, 700);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.panelSearch);
            this.Controls.Add(this.panelTop);
            this.Font = new Font("Segoe UI", 9.75F);
            this.Name = "frmEmployeeReport";
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Text = "Báo cáo nhân sự";
            this.Load += new System.EventHandler(this.frmEmployeeReport_Load);

            this.tabControl1.ResumeLayout(false);
            this.tabOverview.ResumeLayout(false);
            this.tabDepartment.ResumeLayout(false);
            this.tabPosition.ResumeLayout(false);
            this.tabStatus.ResumeLayout(false);
            this.tabDetail.ResumeLayout(false);
            this.panelTop.ResumeLayout(false);
            this.panelTop.PerformLayout();
            this.panelSearch.ResumeLayout(false);
            this.panelSearch.PerformLayout();
            this.panelStats.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.chartDepartment)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDepartmentReport)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPositionReport)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvStatusReport)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvEmployeeList)).EndInit();
            this.ResumeLayout(false);
        }

        private void AddStatCard(Panel parent, string title, Label valueLabel, int x, int y, Color color)
        {
            Panel card = new Panel
            {
                Location = new Point(x, y),
                Size = new Size(180, 110),
                BackColor = color
            };

            Label lblTitle = new Label
            {
                Text = title,
                Location = new Point(10, 10),
                Size = new Size(160, 25),
                Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                ForeColor = Color.White
            };

            valueLabel.Location = new Point(10, 45);
            valueLabel.Size = new Size(160, 50);
            valueLabel.Font = new Font("Segoe UI", 24F, FontStyle.Bold);
            valueLabel.ForeColor = Color.White;
            valueLabel.Text = "0";
            valueLabel.TextAlign = ContentAlignment.MiddleCenter;

            card.Controls.Add(lblTitle);
            card.Controls.Add(valueLabel);
            parent.Controls.Add(card);
        }
    }
}
