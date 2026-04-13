using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using Quanlynhansu.BLL;
using Quanlynhansu.DAL;
using ClosedXML.Excel;
using System.Windows.Forms.DataVisualization.Charting;

namespace Quanlynhansu.Forms
{
    public partial class frmSalaryReport : Form
    {
        public frmSalaryReport()
        {
            InitializeComponent();
        }

        private void frmSalaryReport_Load(object sender, EventArgs e)
        {
            LoadComboBoxes();
        }

        // ========== KHOI TAO ==========

        private void LoadComboBoxes()
        {
            int currentYear = DateTime.Now.Year;
            int currentMonth = DateTime.Now.Month;

            // Load thang cho tat ca combobox
            var monthCombos = new[] { cboSummaryMonth, cboDeptMonth, cboFromMonth, cboToMonth };
            foreach (var cbo in monthCombos)
            {
                for (int i = 1; i <= 12; i++)
                {
                    cbo.Items.Add(i.ToString("00"));
                }
                cbo.SelectedIndex = currentMonth - 1;
            }

            // Load nam cho tat ca combobox
            var yearCombos = new[] { cboSummaryYear, cboDeptYear, cboFromYear, cboToYear, cboChartYear };
            foreach (var cbo in yearCombos)
            {
                for (int i = currentYear - 5; i <= currentYear; i++)
                {
                    cbo.Items.Add(i.ToString());
                }
                cbo.SelectedItem = currentYear.ToString();
            }

            // Load nhan vien
            DataTable dtEmployees = EmployeeDAL.GetAllEmployees();
            cboEmployee.DataSource = dtEmployees;
            cboEmployee.DisplayMember = "FullName";
            cboEmployee.ValueMember = "EmployeeID";
            cboEmployee.SelectedIndex = -1;
        }

        // ========== TAB TONG HOP ==========

        private void btnSummaryLoad_Click(object sender, EventArgs e)
        {
            try
            {
                int month = int.Parse(cboSummaryMonth.SelectedItem.ToString());
                int year = int.Parse(cboSummaryYear.SelectedItem.ToString());

                DataTable dt = SalaryDAL.GetSalarySummaryReport(month, year);
                dgvSummary.DataSource = dt;

                FormatSummaryGrid();
                UpdateSummaryStats(dt);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Loi: {ex.Message}", "Loi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void FormatSummaryGrid()
        {
            if (dgvSummary.Columns.Count == 0) return;

            dgvSummary.Columns["EmployeeCode"].HeaderText = "Ma NV";
            dgvSummary.Columns["EmployeeName"].HeaderText = "Ho ten";
            dgvSummary.Columns["DepartmentName"].HeaderText = "Phong ban";
            dgvSummary.Columns["PositionName"].HeaderText = "Chuc vu";
            dgvSummary.Columns["BaseSalary"].HeaderText = "Luong CB";
            dgvSummary.Columns["Bonus"].HeaderText = "Thuong";
            dgvSummary.Columns["Deduction"].HeaderText = "Khau tru";
            dgvSummary.Columns["TotalSalary"].HeaderText = "Tong luong";
            dgvSummary.Columns["Status"].HeaderText = "Trang thai";

            // Format tien
            var moneyCols = new[] { "BaseSalary", "Bonus", "Deduction", "TotalSalary" };
            foreach (var col in moneyCols)
            {
                dgvSummary.Columns[col].DefaultCellStyle.Format = "N0";
                dgvSummary.Columns[col].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            }
        }

        private void UpdateSummaryStats(DataTable dt)
        {
            pnlSummaryStats.Controls.Clear();

            int totalEmployees = dt.Rows.Count;
            decimal totalSalary = 0;
            decimal totalBonus = 0;
            decimal totalDeduction = 0;

            foreach (DataRow row in dt.Rows)
            {
                totalSalary += Convert.ToDecimal(row["TotalSalary"]);
                totalBonus += Convert.ToDecimal(row["Bonus"]);
                totalDeduction += Convert.ToDecimal(row["Deduction"]);
            }

            CreateStatLabel(pnlSummaryStats, "Tong nhan vien:", totalEmployees.ToString(), 20, 20);
            CreateStatLabel(pnlSummaryStats, "Tong luong:", $"{totalSalary:N0} VND", 20, 50);
            CreateStatLabel(pnlSummaryStats, "Tong thuong:", $"{totalBonus:N0} VND", 400, 20);
            CreateStatLabel(pnlSummaryStats, "Tong khau tru:", $"{totalDeduction:N0} VND", 400, 50);
        }

        private void CreateStatLabel(Panel panel, string label, string value, int x, int y)
        {
            Label lblLabel = new Label
            {
                Text = label,
                Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                Location = new Point(x, y),
                AutoSize = true
            };

            Label lblValue = new Label
            {
                Text = value,
                Font = new Font("Segoe UI", 11F, FontStyle.Bold),
                ForeColor = Color.FromArgb(220, 53, 69),
                Location = new Point(x + 150, y),
                AutoSize = true
            };

            panel.Controls.Add(lblLabel);
            panel.Controls.Add(lblValue);
        }

        // ========== TAB PHONG BAN ==========

        private void btnDeptLoad_Click(object sender, EventArgs e)
        {
            try
            {
                int month = int.Parse(cboDeptMonth.SelectedItem.ToString());
                int year = int.Parse(cboDeptYear.SelectedItem.ToString());

                DataTable dt = SalaryDAL.GetSalaryByDepartmentReport(month, year);
                dgvDepartment.DataSource = dt;

                FormatDepartmentGrid();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Loi: {ex.Message}", "Loi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void FormatDepartmentGrid()
        {
            if (dgvDepartment.Columns.Count == 0) return;

            dgvDepartment.Columns["DepartmentName"].HeaderText = "Phong ban";
            dgvDepartment.Columns["EmployeeCount"].HeaderText = "So NV";
            dgvDepartment.Columns["TotalBaseSalary"].HeaderText = "Tong luong CB";
            dgvDepartment.Columns["TotalBonus"].HeaderText = "Tong thuong";
            dgvDepartment.Columns["TotalDeduction"].HeaderText = "Tong khau tru";
            dgvDepartment.Columns["TotalSalary"].HeaderText = "Tong luong";

            var moneyCols = new[] { "TotalBaseSalary", "TotalBonus", "TotalDeduction", "TotalSalary" };
            foreach (var col in moneyCols)
            {
                dgvDepartment.Columns[col].DefaultCellStyle.Format = "N0";
                dgvDepartment.Columns[col].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            }
        }

        // ========== TAB NHAN VIEN ==========

        private void btnEmpLoad_Click(object sender, EventArgs e)
        {
            try
            {
                if (cboEmployee.SelectedValue == null)
                {
                    MessageBox.Show("Vui long chon nhan vien!", "Thong bao",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                long employeeId = Convert.ToInt64(cboEmployee.SelectedValue);
                int fromMonth = int.Parse(cboFromMonth.SelectedItem.ToString());
                int fromYear = int.Parse(cboFromYear.SelectedItem.ToString());
                int toMonth = int.Parse(cboToMonth.SelectedItem.ToString());
                int toYear = int.Parse(cboToYear.SelectedItem.ToString());

                DataTable dt = SalaryDAL.GetSalaryByEmployeeReport(employeeId, fromMonth, fromYear, toMonth, toYear);
                dgvEmployee.DataSource = dt;

                FormatEmployeeGrid();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Loi: {ex.Message}", "Loi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void FormatEmployeeGrid()
        {
            if (dgvEmployee.Columns.Count == 0) return;

            dgvEmployee.Columns["Month"].HeaderText = "Thang";
            dgvEmployee.Columns["Year"].HeaderText = "Nam";
            dgvEmployee.Columns["BaseSalary"].HeaderText = "Luong CB";
            dgvEmployee.Columns["Bonus"].HeaderText = "Thuong";
            dgvEmployee.Columns["Deduction"].HeaderText = "Khau tru";
            dgvEmployee.Columns["TotalSalary"].HeaderText = "Tong luong";
            dgvEmployee.Columns["Status"].HeaderText = "Trang thai";

            var moneyCols = new[] { "BaseSalary", "Bonus", "Deduction", "TotalSalary" };
            foreach (var col in moneyCols)
            {
                dgvEmployee.Columns[col].DefaultCellStyle.Format = "N0";
                dgvEmployee.Columns[col].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            }
        }

        // ========== TAB BIEU DO ==========

        private void btnChartLoad_Click(object sender, EventArgs e)
        {
            try
            {
                int year = int.Parse(cboChartYear.SelectedItem.ToString());
                DataTable dt = SalaryDAL.GetSalaryChartData(year);

                if (dt.Rows.Count == 0)
                {
                    MessageBox.Show("Khong co du lieu de ve bieu do!", "Thong bao",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                DrawSimpleChart(dt, year);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Loi: {ex.Message}", "Loi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void DrawSimpleChart(DataTable dt, int year)
        {
            pnlChart.Controls.Clear();

            try
            {
                // Tao Chart control
                Chart chart = new Chart
                {
                    Dock = DockStyle.Fill,
                    BackColor = Color.White
                };

                // Tao ChartArea
                ChartArea chartArea = new ChartArea("MainArea");
                chartArea.AxisX.Title = "Thang";
                chartArea.AxisX.Interval = 1;
                chartArea.AxisX.Minimum = 1;
                chartArea.AxisX.Maximum = 12;
                chartArea.AxisY.Title = "Tong luong (VND)";
                chartArea.AxisY.LabelStyle.Format = "#,##0";
                chartArea.BackColor = Color.WhiteSmoke;
                chart.ChartAreas.Add(chartArea);

                // Series 1: Tong luong (Cot)
                Series seriesSalary = new Series("Tong luong");
                seriesSalary.ChartType = SeriesChartType.Column;
                seriesSalary.Color = Color.FromArgb(54, 162, 235);
                seriesSalary.BorderWidth = 2;

                // Series 2: So nhan vien (Duong)
                Series seriesEmployee = new Series("So nhan vien");
                seriesEmployee.ChartType = SeriesChartType.Line;
                seriesEmployee.Color = Color.FromArgb(255, 99, 132);
                seriesEmployee.BorderWidth = 3;
                seriesEmployee.MarkerStyle = MarkerStyle.Circle;
                seriesEmployee.MarkerSize = 8;
                seriesEmployee.YAxisType = AxisType.Secondary;

                // Them du lieu
                foreach (DataRow row in dt.Rows)
                {
                    int month = Convert.ToInt32(row["Month"]);
                    decimal totalSalary = Convert.ToDecimal(row["TotalSalary"]);
                    int employeeCount = Convert.ToInt32(row["EmployeeCount"]);

                    seriesSalary.Points.AddXY(month, totalSalary);
                    seriesEmployee.Points.AddXY(month, employeeCount);
                }

                chart.Series.Add(seriesSalary);
                chart.Series.Add(seriesEmployee);

                // Cau hinh truc Y phu
                chartArea.AxisY2.Title = "So nhan vien";
                chartArea.AxisY2.Enabled = AxisEnabled.True;

                // Tieu de
                Title title = new Title();
                title.Text = $"BIEU DO LUONG NAM {year}";
                title.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
                chart.Titles.Add(title);

                // Legend
                Legend legend = new Legend("Legend");
                legend.Docking = Docking.Bottom;
                legend.Alignment = StringAlignment.Center;
                chart.Legends.Add(legend);

                // Them vao panel
                pnlChart.Controls.Add(chart);
            }
            catch (Exception ex)
            {
                // Fallback: Hien thi dang text neu Chart khong hoat dong
                Label lblError = new Label
                {
                    Text = $"Khong the ve bieu do: {ex.Message}\n\nDu lieu:",
                    Font = new Font("Segoe UI", 10F),
                    Location = new Point(20, 20),
                    AutoSize = true
                };
                pnlChart.Controls.Add(lblError);

                int y = 60;
                foreach (DataRow row in dt.Rows)
                {
                    int month = Convert.ToInt32(row["Month"]);
                    decimal totalSalary = Convert.ToDecimal(row["TotalSalary"]);
                    int employeeCount = Convert.ToInt32(row["EmployeeCount"]);

                    Label lblMonth = new Label
                    {
                        Text = $"Thang {month:00}: {employeeCount} NV - {totalSalary:N0} VND",
                        Font = new Font("Segoe UI", 9F),
                        Location = new Point(40, y),
                        AutoSize = true
                    };
                    pnlChart.Controls.Add(lblMonth);
                    y += 25;
                }
            }
        }

        // ========== XUAT EXCEL ==========

        private void btnExport_Click(object sender, EventArgs e)
        {
            try
            {
                int totalRows = 0;
                if (tabControl.SelectedTab == tabSummary)
                    totalRows = dgvSummary.Rows.Count;
                else if (tabControl.SelectedTab == tabDepartment)
                    totalRows = dgvDepartment.Rows.Count;
                else if (tabControl.SelectedTab == tabEmployee)
                    totalRows = dgvEmployee.Rows.Count;

                if (totalRows == 0)
                {
                    MessageBox.Show("Khong co du lieu de xuat!", "Thong bao",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                SaveFileDialog sfd = new SaveFileDialog
                {
                    Filter = "Excel Files|*.xlsx",
                    FileName = $"BaoCaoLuong_{DateTime.Now:yyyyMMdd_HHmmss}.xlsx",
                    Title = "Xuat bao cao Excel"
                };

                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    Cursor = Cursors.WaitCursor;

                    if (tabControl.SelectedTab == tabSummary)
                    {
                        ExportSummaryToExcel(sfd.FileName);
                    }
                    else if (tabControl.SelectedTab == tabDepartment)
                    {
                        ExportDepartmentToExcel(sfd.FileName);
                    }
                    else if (tabControl.SelectedTab == tabEmployee)
                    {
                        ExportEmployeeToExcel(sfd.FileName);
                    }

                    Cursor = Cursors.Default;

                    DialogResult result = MessageBox.Show(
                        "Xuat Excel thanh cong!\n\nBan co muon mo file khong?",
                        "Thanh cong",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Information
                    );

                    if (result == DialogResult.Yes)
                    {
                        System.Diagnostics.Process.Start(sfd.FileName);
                    }
                }
            }
            catch (Exception ex)
            {
                Cursor = Cursors.Default;
                MessageBox.Show($"Loi xuat Excel: {ex.Message}", "Loi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ExportSummaryToExcel(string filePath)
        {
            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("Tong hop luong");

                int month = int.Parse(cboSummaryMonth.SelectedItem.ToString());
                int year = int.Parse(cboSummaryYear.SelectedItem.ToString());

                worksheet.Cell(1, 1).Value = "BAO CAO TONG HOP LUONG";
                worksheet.Cell(1, 1).Style.Font.Bold = true;
                worksheet.Cell(1, 1).Style.Font.FontSize = 16;
                worksheet.Range(1, 1, 1, 9).Merge();
                worksheet.Range(1, 1, 1, 9).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

                worksheet.Cell(2, 1).Value = $"Thang {month:00}/{year}";
                worksheet.Cell(2, 1).Style.Font.Bold = true;
                worksheet.Range(2, 1, 2, 9).Merge();
                worksheet.Range(2, 1, 2, 9).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

                int row = 4;
                worksheet.Cell(row, 1).Value = "Ma NV";
                worksheet.Cell(row, 2).Value = "Ho ten";
                worksheet.Cell(row, 3).Value = "Phong ban";
                worksheet.Cell(row, 4).Value = "Chuc vu";
                worksheet.Cell(row, 5).Value = "Luong CB";
                worksheet.Cell(row, 6).Value = "Thuong";
                worksheet.Cell(row, 7).Value = "Khau tru";
                worksheet.Cell(row, 8).Value = "Tong luong";
                worksheet.Cell(row, 9).Value = "Trang thai";

                var headerRange = worksheet.Range(row, 1, row, 9);
                headerRange.Style.Font.Bold = true;
                headerRange.Style.Fill.BackgroundColor = XLColor.FromHtml("#4472C4");
                headerRange.Style.Font.FontColor = XLColor.White;
                headerRange.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

                row++;
                DataTable dt = (DataTable)dgvSummary.DataSource;
                foreach (DataRow dataRow in dt.Rows)
                {
                    worksheet.Cell(row, 1).Value = dataRow["EmployeeCode"].ToString();
                    worksheet.Cell(row, 2).Value = dataRow["EmployeeName"].ToString();
                    worksheet.Cell(row, 3).Value = dataRow["DepartmentName"].ToString();
                    worksheet.Cell(row, 4).Value = dataRow["PositionName"].ToString();
                    worksheet.Cell(row, 5).Value = Convert.ToDecimal(dataRow["BaseSalary"]);
                    worksheet.Cell(row, 6).Value = Convert.ToDecimal(dataRow["Bonus"]);
                    worksheet.Cell(row, 7).Value = Convert.ToDecimal(dataRow["Deduction"]);
                    worksheet.Cell(row, 8).Value = Convert.ToDecimal(dataRow["TotalSalary"]);
                    worksheet.Cell(row, 9).Value = dataRow["Status"].ToString();

                    worksheet.Cell(row, 5).Style.NumberFormat.Format = "#,##0";
                    worksheet.Cell(row, 6).Style.NumberFormat.Format = "#,##0";
                    worksheet.Cell(row, 7).Style.NumberFormat.Format = "#,##0";
                    worksheet.Cell(row, 8).Style.NumberFormat.Format = "#,##0";

                    row++;
                }

                int lastRow = row;
                worksheet.Cell(lastRow, 1).Value = "TONG CONG";
                worksheet.Cell(lastRow, 1).Style.Font.Bold = true;
                worksheet.Range(lastRow, 1, lastRow, 4).Merge();

                worksheet.Cell(lastRow, 5).FormulaA1 = $"SUM(E5:E{lastRow - 1})";
                worksheet.Cell(lastRow, 6).FormulaA1 = $"SUM(F5:F{lastRow - 1})";
                worksheet.Cell(lastRow, 7).FormulaA1 = $"SUM(G5:G{lastRow - 1})";
                worksheet.Cell(lastRow, 8).FormulaA1 = $"SUM(H5:H{lastRow - 1})";

                var totalRange = worksheet.Range(lastRow, 1, lastRow, 9);
                totalRange.Style.Font.Bold = true;
                totalRange.Style.Fill.BackgroundColor = XLColor.FromHtml("#D9E1F2");

                worksheet.Cell(lastRow, 5).Style.NumberFormat.Format = "#,##0";
                worksheet.Cell(lastRow, 6).Style.NumberFormat.Format = "#,##0";
                worksheet.Cell(lastRow, 7).Style.NumberFormat.Format = "#,##0";
                worksheet.Cell(lastRow, 8).Style.NumberFormat.Format = "#,##0";

                worksheet.Columns().AdjustToContents();
                workbook.SaveAs(filePath);
            }
        }

        private void ExportDepartmentToExcel(string filePath)
        {
            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("Theo phong ban");

                int month = int.Parse(cboDeptMonth.SelectedItem.ToString());
                int year = int.Parse(cboDeptYear.SelectedItem.ToString());

                worksheet.Cell(1, 1).Value = "BAO CAO LUONG THEO PHONG BAN";
                worksheet.Cell(1, 1).Style.Font.Bold = true;
                worksheet.Cell(1, 1).Style.Font.FontSize = 16;
                worksheet.Range(1, 1, 1, 6).Merge();
                worksheet.Range(1, 1, 1, 6).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

                worksheet.Cell(2, 1).Value = $"Thang {month:00}/{year}";
                worksheet.Range(2, 1, 2, 6).Merge();
                worksheet.Range(2, 1, 2, 6).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

                int row = 4;
                worksheet.Cell(row, 1).Value = "Phong ban";
                worksheet.Cell(row, 2).Value = "So NV";
                worksheet.Cell(row, 3).Value = "Tong luong CB";
                worksheet.Cell(row, 4).Value = "Tong thuong";
                worksheet.Cell(row, 5).Value = "Tong khau tru";
                worksheet.Cell(row, 6).Value = "Tong luong";

                var headerRange = worksheet.Range(row, 1, row, 6);
                headerRange.Style.Font.Bold = true;
                headerRange.Style.Fill.BackgroundColor = XLColor.FromHtml("#4472C4");
                headerRange.Style.Font.FontColor = XLColor.White;
                headerRange.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

                row++;
                DataTable dt = (DataTable)dgvDepartment.DataSource;
                foreach (DataRow dataRow in dt.Rows)
                {
                    worksheet.Cell(row, 1).Value = dataRow["DepartmentName"].ToString();
                    worksheet.Cell(row, 2).Value = Convert.ToInt32(dataRow["EmployeeCount"]);
                    worksheet.Cell(row, 3).Value = Convert.ToDecimal(dataRow["TotalBaseSalary"]);
                    worksheet.Cell(row, 4).Value = Convert.ToDecimal(dataRow["TotalBonus"]);
                    worksheet.Cell(row, 5).Value = Convert.ToDecimal(dataRow["TotalDeduction"]);
                    worksheet.Cell(row, 6).Value = Convert.ToDecimal(dataRow["TotalSalary"]);

                    worksheet.Cell(row, 3).Style.NumberFormat.Format = "#,##0";
                    worksheet.Cell(row, 4).Style.NumberFormat.Format = "#,##0";
                    worksheet.Cell(row, 5).Style.NumberFormat.Format = "#,##0";
                    worksheet.Cell(row, 6).Style.NumberFormat.Format = "#,##0";

                    row++;
                }

                worksheet.Columns().AdjustToContents();
                workbook.SaveAs(filePath);
            }
        }

        private void ExportEmployeeToExcel(string filePath)
        {
            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("Lich su luong");

                string employeeName = cboEmployee.Text;

                worksheet.Cell(1, 1).Value = "LICH SU LUONG NHAN VIEN";
                worksheet.Cell(1, 1).Style.Font.Bold = true;
                worksheet.Cell(1, 1).Style.Font.FontSize = 16;
                worksheet.Range(1, 1, 1, 7).Merge();
                worksheet.Range(1, 1, 1, 7).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

                worksheet.Cell(2, 1).Value = $"Nhan vien: {employeeName}";
                worksheet.Range(2, 1, 2, 7).Merge();
                worksheet.Range(2, 1, 2, 7).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

                int row = 4;
                worksheet.Cell(row, 1).Value = "Thang";
                worksheet.Cell(row, 2).Value = "Nam";
                worksheet.Cell(row, 3).Value = "Luong CB";
                worksheet.Cell(row, 4).Value = "Thuong";
                worksheet.Cell(row, 5).Value = "Khau tru";
                worksheet.Cell(row, 6).Value = "Tong luong";
                worksheet.Cell(row, 7).Value = "Trang thai";

                var headerRange = worksheet.Range(row, 1, row, 7);
                headerRange.Style.Font.Bold = true;
                headerRange.Style.Fill.BackgroundColor = XLColor.FromHtml("#4472C4");
                headerRange.Style.Font.FontColor = XLColor.White;
                headerRange.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

                row++;
                DataTable dt = (DataTable)dgvEmployee.DataSource;
                foreach (DataRow dataRow in dt.Rows)
                {
                    worksheet.Cell(row, 1).Value = Convert.ToInt32(dataRow["Month"]);
                    worksheet.Cell(row, 2).Value = Convert.ToInt32(dataRow["Year"]);
                    worksheet.Cell(row, 3).Value = Convert.ToDecimal(dataRow["BaseSalary"]);
                    worksheet.Cell(row, 4).Value = Convert.ToDecimal(dataRow["Bonus"]);
                    worksheet.Cell(row, 5).Value = Convert.ToDecimal(dataRow["Deduction"]);
                    worksheet.Cell(row, 6).Value = Convert.ToDecimal(dataRow["TotalSalary"]);
                    worksheet.Cell(row, 7).Value = dataRow["Status"].ToString();

                    worksheet.Cell(row, 3).Style.NumberFormat.Format = "#,##0";
                    worksheet.Cell(row, 4).Style.NumberFormat.Format = "#,##0";
                    worksheet.Cell(row, 5).Style.NumberFormat.Format = "#,##0";
                    worksheet.Cell(row, 6).Style.NumberFormat.Format = "#,##0";

                    row++;
                }

                worksheet.Columns().AdjustToContents();
                workbook.SaveAs(filePath);
            }
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Chuc nang in dang phat trien!", "Thong bao",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
