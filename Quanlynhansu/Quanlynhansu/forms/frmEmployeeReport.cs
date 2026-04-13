using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using ClosedXML.Excel;
using System.IO;

namespace Quanlynhansu.Forms
{
    public partial class frmEmployeeReport : Form
    {
        private string  connectionString="Data Source=DESKTOP-VCP25O7\\HOANGTHANHHONG44;Initial Catalog=HRM_System;Integrated Security=False;User Id=sa;Password=123456;MultipleActiveResultSets=True;TrustServerCertificate=True";

        public frmEmployeeReport()
        {
            InitializeComponent();
        }

        private void frmEmployeeReport_Load(object sender, EventArgs e)
        {
            LoadDepartments();
            LoadPositions();
            LoadDefaultReport();
        }

        // ========== LOAD DỮ LIỆU ==========
        private void LoadDepartments()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = "SELECT DepartmentID, DepartmentName FROM Departments WHERE IsActive = 1";
                    SqlDataAdapter da = new SqlDataAdapter(query, conn);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    DataRow allRow = dt.NewRow();
                    allRow["DepartmentID"] = 0;
                    allRow["DepartmentName"] = "-- Tất cả phòng ban --";
                    dt.Rows.InsertAt(allRow, 0);

                    cboDepartment.DataSource = dt;
                    cboDepartment.DisplayMember = "DepartmentName";
                    cboDepartment.ValueMember = "DepartmentID";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi load phòng ban: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadPositions()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = "SELECT PositionID, PositionName FROM Positions WHERE IsActive = 1";
                    SqlDataAdapter da = new SqlDataAdapter(query, conn);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    DataRow allRow = dt.NewRow();
                    allRow["PositionID"] = 0;
                    allRow["PositionName"] = "-- Tất cả chức vụ --";
                    dt.Rows.InsertAt(allRow, 0);

                    cboPosition.DataSource = dt;
                    cboPosition.DisplayMember = "PositionName";
                    cboPosition.ValueMember = "PositionID";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi load chức vụ: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadDefaultReport()
        {
            LoadEmployeeStatistics();
            LoadEmployeeByDepartment();
            LoadEmployeeByPosition();
            LoadEmployeeByStatus();
            LoadDepartmentChart();
        }

        // ========== BÁO CÁO THỐNG KÊ TỔNG QUAN ==========
        private void LoadEmployeeStatistics()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = @"
                        SELECT 
                            COUNT(*) AS TotalEmployees,
                            COUNT(CASE WHEN Status = N'Đang làm việc' THEN 1 END) AS ActiveEmployees,
                            COUNT(CASE WHEN Status = N'Nghỉ việc' THEN 1 END) AS InactiveEmployees,
                            COUNT(CASE WHEN Gender = N'Nam' THEN 1 END) AS MaleEmployees,
                            COUNT(CASE WHEN Gender = N'Nữ' THEN 1 END) AS FemaleEmployees,
                            AVG(CAST(Salary AS FLOAT)) AS AvgSalary
                        FROM Employees 
                        WHERE IsActive = 1";

                    SqlCommand cmd = new SqlCommand(query, conn);
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        lblTotalEmployees.Text = reader["TotalEmployees"].ToString();
                        lblActiveEmployees.Text = reader["ActiveEmployees"].ToString();
                        lblInactiveEmployees.Text = reader["InactiveEmployees"].ToString();
                        lblMaleEmployees.Text = reader["MaleEmployees"].ToString();
                        lblFemaleEmployees.Text = reader["FemaleEmployees"].ToString();

                        decimal avgSalary = reader["AvgSalary"] != DBNull.Value
                            ? Convert.ToDecimal(reader["AvgSalary"])
                            : 0;
                        lblAvgSalary.Text = avgSalary.ToString("N0") + " VNĐ";
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi load thống kê: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ========== BÁO CÁO THEO PHÒNG BAN ==========
        private void LoadEmployeeByDepartment()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = @"
                        SELECT 
                            ISNULL(d.DepartmentName, N'Chưa phân phòng') AS DepartmentName,
                            COUNT(e.EmployeeID) AS EmployeeCount,
                            AVG(CAST(e.Salary AS FLOAT)) AS AvgSalary,
                            MAX(e.Salary) AS MaxSalary,
                            MIN(e.Salary) AS MinSalary
                        FROM Employees e
                        LEFT JOIN Departments d ON e.DepartmentID = d.DepartmentID
                        WHERE e.IsActive = 1 AND e.Status = N'Đang làm việc'
                        GROUP BY d.DepartmentName
                        ORDER BY EmployeeCount DESC";

                    SqlDataAdapter da = new SqlDataAdapter(query, conn);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    dgvDepartmentReport.DataSource = dt;
                    FormatDepartmentGrid();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi load báo cáo phòng ban: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void FormatDepartmentGrid()
        {
            dgvDepartmentReport.Columns["DepartmentName"].HeaderText = "Phòng ban";
            dgvDepartmentReport.Columns["EmployeeCount"].HeaderText = "Số nhân viên";
            dgvDepartmentReport.Columns["AvgSalary"].HeaderText = "Lương TB";
            dgvDepartmentReport.Columns["MaxSalary"].HeaderText = "Lương cao nhất";
            dgvDepartmentReport.Columns["MinSalary"].HeaderText = "Lương thấp nhất";

            dgvDepartmentReport.Columns["AvgSalary"].DefaultCellStyle.Format = "N0";
            dgvDepartmentReport.Columns["MaxSalary"].DefaultCellStyle.Format = "N0";
            dgvDepartmentReport.Columns["MinSalary"].DefaultCellStyle.Format = "N0";

            dgvDepartmentReport.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        // ========== BÁO CÁO THEO CHỨC VỤ ==========
        private void LoadEmployeeByPosition()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = @"
                        SELECT 
                            ISNULL(p.PositionName, N'Chưa có chức vụ') AS PositionName,
                            COUNT(e.EmployeeID) AS EmployeeCount,
                            AVG(CAST(e.Salary AS FLOAT)) AS AvgSalary
                        FROM Employees e
                        LEFT JOIN Positions p ON e.PositionID = p.PositionID
                        WHERE e.IsActive = 1 AND e.Status = N'Đang làm việc'
                        GROUP BY p.PositionName
                        ORDER BY EmployeeCount DESC";

                    SqlDataAdapter da = new SqlDataAdapter(query, conn);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    dgvPositionReport.DataSource = dt;
                    FormatPositionGrid();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi load báo cáo chức vụ: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void FormatPositionGrid()
        {
            dgvPositionReport.Columns["PositionName"].HeaderText = "Chức vụ";
            dgvPositionReport.Columns["EmployeeCount"].HeaderText = "Số nhân viên";
            dgvPositionReport.Columns["AvgSalary"].HeaderText = "Lương trung bình";

            dgvPositionReport.Columns["AvgSalary"].DefaultCellStyle.Format = "N0";
            dgvPositionReport.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        // ========== BÁO CÁO THEO TRẠNG THÁI ==========
        private void LoadEmployeeByStatus()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = @"
                        SELECT 
                            Status,
                            COUNT(*) AS EmployeeCount,
                            CAST(COUNT(*) * 100.0 / (SELECT COUNT(*) FROM Employees WHERE IsActive = 1) AS DECIMAL(5,2)) AS Percentage
                        FROM Employees
                        WHERE IsActive = 1
                        GROUP BY Status";

                    SqlDataAdapter da = new SqlDataAdapter(query, conn);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    dgvStatusReport.DataSource = dt;
                    FormatStatusGrid();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi load báo cáo trạng thái: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void FormatStatusGrid()
        {
            dgvStatusReport.Columns["Status"].HeaderText = "Trạng thái";
            dgvStatusReport.Columns["EmployeeCount"].HeaderText = "Số lượng";
            dgvStatusReport.Columns["Percentage"].HeaderText = "Tỷ lệ (%)";

            dgvStatusReport.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        // ========== BIỂU ĐỒ ==========
        private void LoadDepartmentChart()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = @"
                        SELECT TOP 10
                            ISNULL(d.DepartmentName, N'Chưa phân phòng') AS DepartmentName,
                            COUNT(e.EmployeeID) AS EmployeeCount
                        FROM Employees e
                        LEFT JOIN Departments d ON e.DepartmentID = d.DepartmentID
                        WHERE e.IsActive = 1 AND e.Status = N'Đang làm việc'
                        GROUP BY d.DepartmentName
                        ORDER BY EmployeeCount DESC";

                    SqlDataAdapter da = new SqlDataAdapter(query, conn);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    chartDepartment.Series.Clear();
                    Series series = new Series("Nhân viên");
                    series.ChartType = SeriesChartType.Column;
                    series.Color = Color.FromArgb(52, 152, 219);

                    foreach (DataRow row in dt.Rows)
                    {
                        series.Points.AddXY(row["DepartmentName"].ToString(), row["EmployeeCount"]);
                    }

                    chartDepartment.Series.Add(series);
                    chartDepartment.ChartAreas[0].AxisX.Title = "Phòng ban";
                    chartDepartment.ChartAreas[0].AxisY.Title = "Số nhân viên";
                    chartDepartment.ChartAreas[0].AxisX.Interval = 1;
                    chartDepartment.Titles.Clear();
                    chartDepartment.Titles.Add("BIỂU ĐỒ NHÂN VIÊN THEO PHÒNG BAN");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi load biểu đồ: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ========== TÌM KIẾM & LỌC ==========
        private void btnSearch_Click(object sender, EventArgs e)
        {
            SearchEmployees();
        }

        private void SearchEmployees()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = @"
                        SELECT 
                            e.EmployeeCode,
                            e.FullName,
                            e.DateOfBirth,
                            e.Gender,
                            e.PhoneNumber,
                            e.Email,
                            d.DepartmentName,
                            p.PositionName,
                            e.HireDate,
                            e.Status,
                            e.Salary
                        FROM Employees e
                        LEFT JOIN Departments d ON e.DepartmentID = d.DepartmentID
                        LEFT JOIN Positions p ON e.PositionID = p.PositionID
                        WHERE e.IsActive = 1
                            AND (@SearchText = '' OR e.FullName LIKE '%' + @SearchText + '%' OR e.EmployeeCode LIKE '%' + @SearchText + '%')
                            AND (@DepartmentID = 0 OR e.DepartmentID = @DepartmentID)
                            AND (@PositionID = 0 OR e.PositionID = @PositionID)
                            AND (@Status = '' OR e.Status = @Status)
                        ORDER BY e.EmployeeCode";

                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@SearchText", txtSearch.Text.Trim());
                    cmd.Parameters.AddWithValue("@DepartmentID", cboDepartment.SelectedValue ?? 0);
                    cmd.Parameters.AddWithValue("@PositionID", cboPosition.SelectedValue ?? 0);
                    cmd.Parameters.AddWithValue("@Status", cboStatus.Text);

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    dgvEmployeeList.DataSource = dt;
                    FormatEmployeeGrid();

                    lblTotalRecords.Text = $"Tổng số: {dt.Rows.Count} nhân viên";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tìm kiếm: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void FormatEmployeeGrid()
        {
            dgvEmployeeList.Columns["EmployeeCode"].HeaderText = "Mã NV";
            dgvEmployeeList.Columns["FullName"].HeaderText = "Họ tên";
            dgvEmployeeList.Columns["DateOfBirth"].HeaderText = "Ngày sinh";
            dgvEmployeeList.Columns["Gender"].HeaderText = "Giới tính";
            dgvEmployeeList.Columns["PhoneNumber"].HeaderText = "Điện thoại";
            dgvEmployeeList.Columns["Email"].HeaderText = "Email";
            dgvEmployeeList.Columns["DepartmentName"].HeaderText = "Phòng ban";
            dgvEmployeeList.Columns["PositionName"].HeaderText = "Chức vụ";
            dgvEmployeeList.Columns["HireDate"].HeaderText = "Ngày vào";
            dgvEmployeeList.Columns["Status"].HeaderText = "Trạng thái";
            dgvEmployeeList.Columns["Salary"].HeaderText = "Lương";

            dgvEmployeeList.Columns["Salary"].DefaultCellStyle.Format = "N0";
            dgvEmployeeList.Columns["DateOfBirth"].DefaultCellStyle.Format = "dd/MM/yyyy";
            dgvEmployeeList.Columns["HireDate"].DefaultCellStyle.Format = "dd/MM/yyyy";

            dgvEmployeeList.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        // ========== ✅ XUẤT EXCEL VỚI CLOSEDXML ==========
        private void btnExportExcel_Click(object sender, EventArgs e)
        {
            ExportToExcel();
        }

        private void ExportToExcel()
        {
            try
            {
                SaveFileDialog saveDialog = new SaveFileDialog();
                saveDialog.Filter = "Excel Files|*.xlsx";
                saveDialog.FileName = $"BaoCaoNhanSu_{DateTime.Now:yyyyMMdd_HHmmss}.xlsx";

                if (saveDialog.ShowDialog() == DialogResult.OK)
                {
                    using (var workbook = new XLWorkbook())
                    {
                        // ========== SHEET 1: THỐNG KÊ TỔNG QUAN ==========
                        var ws1 = workbook.Worksheets.Add("Thống kê tổng quan");

                        // Tiêu đề
                        ws1.Cell("A1").Value = "BÁO CÁO THỐNG KÊ NHÂN SỰ";
                        ws1.Range("A1:F1").Merge();
                        ws1.Cell("A1").Style.Font.FontSize = 18;
                        ws1.Cell("A1").Style.Font.Bold = true;
                        ws1.Cell("A1").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                        ws1.Cell("A1").Style.Fill.BackgroundColor = XLColor.FromArgb(52, 152, 219);
                        ws1.Cell("A1").Style.Font.FontColor = XLColor.White;
                        ws1.Row(1).Height = 30;

                        // Ngày tạo
                        ws1.Cell("A2").Value = $"Ngày tạo: {DateTime.Now:dd/MM/yyyy HH:mm:ss}";
                        ws1.Range("A2:F2").Merge();
                        ws1.Cell("A2").Style.Font.Italic = true;

                        // Dữ liệu thống kê
                        int row = 4;
                        AddStatRow(ws1, ref row, "Tổng số nhân viên:", lblTotalEmployees.Text, XLColor.FromArgb(52, 152, 219));
                        AddStatRow(ws1, ref row, "Đang làm việc:", lblActiveEmployees.Text, XLColor.FromArgb(46, 204, 113));
                        AddStatRow(ws1, ref row, "Nghỉ việc:", lblInactiveEmployees.Text, XLColor.FromArgb(231, 76, 60));
                        AddStatRow(ws1, ref row, "Nhân viên nam:", lblMaleEmployees.Text, XLColor.FromArgb(52, 73, 94));
                        AddStatRow(ws1, ref row, "Nhân viên nữ:", lblFemaleEmployees.Text, XLColor.FromArgb(155, 89, 182));
                        AddStatRow(ws1, ref row, "Lương trung bình:", lblAvgSalary.Text, XLColor.FromArgb(243, 156, 18));

                        ws1.Columns().AdjustToContents();

                        // ========== SHEET 2: THEO PHÒNG BAN ==========
                        if (dgvDepartmentReport.DataSource != null)
                        {
                            var ws2 = workbook.Worksheets.Add("Theo phòng ban");
                            DataTable dt = (DataTable)dgvDepartmentReport.DataSource;

                            // Tiêu đề
                            ws2.Cell("A1").Value = "BÁO CÁO NHÂN SỰ THEO PHÒNG BAN";
                            ws2.Range("A1:E1").Merge();
                            ws2.Cell("A1").Style.Font.FontSize = 14;
                            ws2.Cell("A1").Style.Font.Bold = true;
                            ws2.Cell("A1").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                            ws2.Cell("A1").Style.Fill.BackgroundColor = XLColor.FromArgb(52, 152, 219);
                            ws2.Cell("A1").Style.Font.FontColor = XLColor.White;

                            // Header
                            var headerRow = ws2.Row(3);
                            headerRow.Style.Font.Bold = true;
                            headerRow.Style.Fill.BackgroundColor = XLColor.FromArgb(189, 195, 199);
                            headerRow.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

                            ws2.Cell("A3").Value = "Phòng ban";
                            ws2.Cell("B3").Value = "Số nhân viên";
                            ws2.Cell("C3").Value = "Lương TB";
                            ws2.Cell("D3").Value = "Lương cao nhất";
                            ws2.Cell("E3").Value = "Lương thấp nhất";

                            // Dữ liệu
                            int startRow = 4;
                            foreach (DataRow dr in dt.Rows)
                            {
                                ws2.Cell($"A{startRow}").Value = dr["DepartmentName"].ToString();
                                ws2.Cell($"B{startRow}").Value = Convert.ToInt32(dr["EmployeeCount"]);
                                ws2.Cell($"C{startRow}").Value = Convert.ToDecimal(dr["AvgSalary"]);
                                ws2.Cell($"D{startRow}").Value = Convert.ToDecimal(dr["MaxSalary"]);
                                ws2.Cell($"E{startRow}").Value = Convert.ToDecimal(dr["MinSalary"]);

                                // Format số
                                ws2.Cell($"C{startRow}").Style.NumberFormat.Format = "#,##0";
                                ws2.Cell($"D{startRow}").Style.NumberFormat.Format = "#,##0";
                                ws2.Cell($"E{startRow}").Style.NumberFormat.Format = "#,##0";

                                startRow++;
                            }

                            // Border
                            var dataRange = ws2.Range($"A3:E{startRow - 1}");
                            dataRange.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                            dataRange.Style.Border.InsideBorder = XLBorderStyleValues.Thin;

                            ws2.Columns().AdjustToContents();
                        }

                        // ========== SHEET 3: THEO CHỨC VỤ ==========
                        if (dgvPositionReport.DataSource != null)
                        {
                            var ws3 = workbook.Worksheets.Add("Theo chức vụ");
                            DataTable dt = (DataTable)dgvPositionReport.DataSource;

                            // Tiêu đề
                            ws3.Cell("A1").Value = "BÁO CÁO NHÂN SỰ THEO CHỨC VỤ";
                            ws3.Range("A1:C1").Merge();
                            ws3.Cell("A1").Style.Font.FontSize = 14;
                            ws3.Cell("A1").Style.Font.Bold = true;
                            ws3.Cell("A1").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                            ws3.Cell("A1").Style.Fill.BackgroundColor = XLColor.FromArgb(52, 152, 219);
                            ws3.Cell("A1").Style.Font.FontColor = XLColor.White;

                            // Header
                            var headerRow = ws3.Row(3);
                            headerRow.Style.Font.Bold = true;
                            headerRow.Style.Fill.BackgroundColor = XLColor.FromArgb(189, 195, 199);
                            headerRow.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

                            ws3.Cell("A3").Value = "Chức vụ";
                            ws3.Cell("B3").Value = "Số nhân viên";
                            ws3.Cell("C3").Value = "Lương trung bình";

                            // Dữ liệu
                            int startRow = 4;
                            foreach (DataRow dr in dt.Rows)
                            {
                                ws3.Cell($"A{startRow}").Value = dr["PositionName"].ToString();
                                ws3.Cell($"B{startRow}").Value = Convert.ToInt32(dr["EmployeeCount"]);
                                ws3.Cell($"C{startRow}").Value = Convert.ToDecimal(dr["AvgSalary"]);
                                ws3.Cell($"C{startRow}").Style.NumberFormat.Format = "#,##0";
                                startRow++;
                            }

                            var dataRange = ws3.Range($"A3:C{startRow - 1}");
                            dataRange.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                            dataRange.Style.Border.InsideBorder = XLBorderStyleValues.Thin;

                            ws3.Columns().AdjustToContents();
                        }

                        // ========== SHEET 4: THEO TRẠNG THÁI ==========
                        if (dgvStatusReport.DataSource != null)
                        {
                            var ws4 = workbook.Worksheets.Add("Theo trạng thái");
                            DataTable dt = (DataTable)dgvStatusReport.DataSource;

                            ws4.Cell("A1").Value = "BÁO CÁO NHÂN SỰ THEO TRẠNG THÁI";
                            ws4.Range("A1:C1").Merge();
                            ws4.Cell("A1").Style.Font.FontSize = 14;
                            ws4.Cell("A1").Style.Font.Bold = true;
                            ws4.Cell("A1").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                            ws4.Cell("A1").Style.Fill.BackgroundColor = XLColor.FromArgb(52, 152, 219);
                            ws4.Cell("A1").Style.Font.FontColor = XLColor.White;

                            var headerRow = ws4.Row(3);
                            headerRow.Style.Font.Bold = true;
                            headerRow.Style.Fill.BackgroundColor = XLColor.FromArgb(189, 195, 199);

                            ws4.Cell("A3").Value = "Trạng thái";
                            ws4.Cell("B3").Value = "Số lượng";
                            ws4.Cell("C3").Value = "Tỷ lệ (%)";

                            int startRow = 4;
                            foreach (DataRow dr in dt.Rows)
                            {
                                ws4.Cell($"A{startRow}").Value = dr["Status"].ToString();
                                ws4.Cell($"B{startRow}").Value = Convert.ToInt32(dr["EmployeeCount"]);
                                ws4.Cell($"C{startRow}").Value = Convert.ToDecimal(dr["Percentage"]);
                                ws4.Cell($"C{startRow}").Style.NumberFormat.Format = "0.00";
                                startRow++;
                            }

                            var dataRange = ws4.Range($"A3:C{startRow - 1}");
                            dataRange.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                            dataRange.Style.Border.InsideBorder = XLBorderStyleValues.Thin;

                            ws4.Columns().AdjustToContents();
                        }

                        // ========== SHEET 5: DANH SÁCH CHI TIẾT ==========
                        if (dgvEmployeeList.DataSource != null)
                        {
                            var ws5 = workbook.Worksheets.Add("Danh sách chi tiết");
                            DataTable dt = (DataTable)dgvEmployeeList.DataSource;

                            ws5.Cell("A1").Value = "DANH SÁCH NHÂN VIÊN CHI TIẾT";
                            ws5.Range("A1:K1").Merge();
                            ws5.Cell("A1").Style.Font.FontSize = 14;
                            ws5.Cell("A1").Style.Font.Bold = true;
                            ws5.Cell("A1").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                            ws5.Cell("A1").Style.Fill.BackgroundColor = XLColor.FromArgb(52, 152, 219);
                            ws5.Cell("A1").Style.Font.FontColor = XLColor.White;

                            var table = ws5.Cell("A3").InsertTable(dt);
                            table.Theme = XLTableTheme.TableStyleMedium2;

                            // Format cột ngày
                            var dateColumns = new[] { "DateOfBirth", "HireDate" };
                            foreach (var col in dateColumns)
                            {
                                if (dt.Columns.Contains(col))
                                {
                                    int colIndex = dt.Columns[col].Ordinal + 1;
                                    ws5.Column(colIndex).Style.DateFormat.Format = "dd/MM/yyyy";
                                }
                            }

                            // Format cột lương
                            if (dt.Columns.Contains("Salary"))
                            {
                                int colIndex = dt.Columns["Salary"].Ordinal + 1;
                                ws5.Column(colIndex).Style.NumberFormat.Format = "#,##0";
                            }

                            ws5.Columns().AdjustToContents();
                        }

                        // Lưu file
                        workbook.SaveAs(saveDialog.FileName);
                    }

                    MessageBox.Show("Xuất Excel thành công!", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // Mở file
                    System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo()
                    {
                        FileName = saveDialog.FileName,
                        UseShellExecute = true
                    });
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi xuất Excel: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Helper method để thêm dòng thống kê
        private void AddStatRow(IXLWorksheet ws, ref int row, string label, string value, XLColor color)
        {
            ws.Cell($"A{row}").Value = label;
            ws.Cell($"A{row}").Style.Font.Bold = true;
            ws.Cell($"A{row}").Style.Fill.BackgroundColor = color;
            ws.Cell($"A{row}").Style.Font.FontColor = XLColor.White;
            ws.Cell($"A{row}").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Right;

            ws.Cell($"B{row}").Value = value;
            ws.Cell($"B{row}").Style.Font.Bold = true;
            ws.Cell($"B{row}").Style.Font.FontSize = 14;
            ws.Cell($"B{row}").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left;

            ws.Row(row).Height = 25;
            row++;
        }

        // ========== IN BÁO CÁO ==========
        private void btnPrint_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Chức năng in đang phát triển!", "Thông báo",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            txtSearch.Clear();
            cboDepartment.SelectedIndex = 0;
            cboPosition.SelectedIndex = 0;
            cboStatus.SelectedIndex = 0;
            LoadDefaultReport();
        }
    }
}
