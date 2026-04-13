using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using ClosedXML.Excel;
using Quanlynhansu.BLL;

namespace Quanlynhansu.Forms
{
    public partial class frmLeaveStatistics : Form
    {
        public frmLeaveStatistics()
        {
            InitializeComponent();
        }

        private void frmLeaveStatistics_Load(object sender, EventArgs e)
        {
            try
            {
                LoadYears();
                LoadStatistics();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải dữ liệu: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadYears()
        {
            cboYear.Items.Clear();

            int currentYear = DateTime.Now.Year;
            for (int i = currentYear; i >= currentYear - 5; i--)
            {
                cboYear.Items.Add(i);
            }

            cboYear.SelectedIndex = 0;
        }

        private void LoadStatistics()
        {
            try
            {
                if (cboYear.SelectedItem == null) return;

                int year = Convert.ToInt32(cboYear.SelectedItem);
                DataTable dt = LeaveRequestBLL.GetLeaveStatistics(year);

                dgvStatistics.DataSource = dt;
                FormatDataGridView();
                UpdateSummary(dt);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải thống kê: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void FormatDataGridView()
        {
            if (dgvStatistics.Columns.Count == 0) return;

            dgvStatistics.Columns["EmployeeCode"].HeaderText = "Mã NV";
            dgvStatistics.Columns["FullName"].HeaderText = "Họ tên";
            dgvStatistics.Columns["DepartmentName"].HeaderText = "Phòng ban";
            dgvStatistics.Columns["TotalRequests"].HeaderText = "Số đơn";
            dgvStatistics.Columns["ApprovedDays"].HeaderText = "Ngày đã duyệt";
            dgvStatistics.Columns["PendingDays"].HeaderText = "Ngày chờ duyệt";

            dgvStatistics.Columns["EmployeeCode"].Width = 100;
            dgvStatistics.Columns["FullName"].Width = 200;
            dgvStatistics.Columns["DepartmentName"].Width = 150;
            dgvStatistics.Columns["TotalRequests"].Width = 100;
            dgvStatistics.Columns["ApprovedDays"].Width = 120;
            dgvStatistics.Columns["PendingDays"].Width = 120;

            // Căn giữa
            dgvStatistics.Columns["EmployeeCode"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvStatistics.Columns["TotalRequests"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvStatistics.Columns["ApprovedDays"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvStatistics.Columns["PendingDays"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            // Màu sắc
            foreach (DataGridViewRow row in dgvStatistics.Rows)
            {
                int approvedDays = Convert.ToInt32(row.Cells["ApprovedDays"].Value);

                if (approvedDays > 15)
                {
                    row.Cells["ApprovedDays"].Style.BackColor = Color.LightCoral;
                    row.Cells["ApprovedDays"].Style.ForeColor = Color.DarkRed;
                }
                else if (approvedDays > 10)
                {
                    row.Cells["ApprovedDays"].Style.BackColor = Color.LightYellow;
                    row.Cells["ApprovedDays"].Style.ForeColor = Color.DarkOrange;
                }
            }
        }

        private void UpdateSummary(DataTable dt)
        {
            int totalEmployees = dt.Rows.Count;
            int totalApprovedDays = 0;

            foreach (DataRow row in dt.Rows)
            {
                totalApprovedDays += Convert.ToInt32(row["ApprovedDays"]);
            }

            lblTotal.Text = $"Tổng nhân viên: {totalEmployees}";
            lblTotalApprovedDays.Text = $"Tổng ngày đã duyệt: {totalApprovedDays}";
        }

        private void btnView_Click(object sender, EventArgs e)
        {
            LoadStatistics();
        }
        private void btnExport_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvStatistics.Rows.Count == 0)
                {
                    MessageBox.Show("Không có dữ liệu để xuất!", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                SaveFileDialog sfd = new SaveFileDialog
                {
                    Filter = "Excel Files|*.xlsx",
                    FileName = $"ThongKeNghiPhep_{cboYear.SelectedItem}_{DateTime.Now:yyyyMMdd}.xlsx"
                };

                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    Cursor = Cursors.WaitCursor;

                    using (var workbook = new XLWorkbook())
                    {
                        var worksheet = workbook.Worksheets.Add("Thống kê nghỉ phép");

                        int year = Convert.ToInt32(cboYear.SelectedItem);

                        // Tiêu đề
                        worksheet.Cell(1, 1).Value = "THỐNG KÊ NGHỈ PHÉP";
                        worksheet.Cell(1, 1).Style.Font.Bold = true;
                        worksheet.Cell(1, 1).Style.Font.FontSize = 16;
                        worksheet.Range(1, 1, 1, 6).Merge();
                        worksheet.Range(1, 1, 1, 6).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

                        worksheet.Cell(2, 1).Value = $"Năm {year}";
                        worksheet.Range(2, 1, 2, 6).Merge();
                        worksheet.Range(2, 1, 2, 6).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

                        // Header
                        int row = 4;
                        worksheet.Cell(row, 1).Value = "Mã NV";
                        worksheet.Cell(row, 2).Value = "Họ tên";
                        worksheet.Cell(row, 3).Value = "Phòng ban";
                        worksheet.Cell(row, 4).Value = "Số đơn";
                        worksheet.Cell(row, 5).Value = "Ngày đã duyệt";
                        worksheet.Cell(row, 6).Value = "Ngày chờ duyệt";

                        var headerRange = worksheet.Range(row, 1, row, 6);
                        headerRange.Style.Font.Bold = true;
                        headerRange.Style.Fill.BackgroundColor = XLColor.FromHtml("#4472C4");
                        headerRange.Style.Font.FontColor = XLColor.White;
                        headerRange.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

                        // Data
                        row++;
                        DataTable dt = (DataTable)dgvStatistics.DataSource;
                        foreach (DataRow dataRow in dt.Rows)
                        {
                            worksheet.Cell(row, 1).Value = dataRow["EmployeeCode"].ToString();
                            worksheet.Cell(row, 2).Value = dataRow["FullName"].ToString();
                            worksheet.Cell(row, 3).Value = dataRow["DepartmentName"].ToString();
                            worksheet.Cell(row, 4).Value = Convert.ToInt32(dataRow["TotalRequests"]);
                            worksheet.Cell(row, 5).Value = Convert.ToInt32(dataRow["ApprovedDays"]);
                            worksheet.Cell(row, 6).Value = Convert.ToInt32(dataRow["PendingDays"]);

                            // Tô màu cảnh báo
                            int approvedDays = Convert.ToInt32(dataRow["ApprovedDays"]);
                            if (approvedDays > 15)
                            {
                                worksheet.Cell(row, 5).Style.Fill.BackgroundColor = XLColor.LightCoral;
                            }
                            else if (approvedDays > 10)
                            {
                                worksheet.Cell(row, 5).Style.Fill.BackgroundColor = XLColor.LightYellow;
                            }

                            row++;
                        }

                        worksheet.Columns().AdjustToContents();
                        workbook.SaveAs(sfd.FileName);
                    }

                    Cursor = Cursors.Default;

                    DialogResult result = MessageBox.Show(
                        "Xuất Excel thành công!\n\nBạn có muốn mở file không?",
                        "Thành công",
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
                MessageBox.Show($"Lỗi xuất Excel: {ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

    }
}
