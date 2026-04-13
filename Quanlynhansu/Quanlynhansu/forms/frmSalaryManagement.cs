using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using Quanlynhansu.BLL;
using Quanlynhansu.Models;
using ClosedXML.Excel;

namespace Quanlynhansu.Forms
{
    public partial class frmSalaryManagement : Form
    {
        private User currentUser;

        public frmSalaryManagement(User user)
        {
            InitializeComponent();
            this.currentUser = user;
        }

        private void frmSalaryManagement_Load(object sender, EventArgs e)
        {
            LoadComboBoxes();
            SetupDataGridView();
            LoadData();
            ApplyPermissions();
        }

        // ========== KHỞI TẠO ==========

        private void LoadComboBoxes()
        {
            // Load tháng
            cboMonth.Items.Add("-- Tất cả --");
            for (int i = 1; i <= 12; i++)
            {
                cboMonth.Items.Add(i.ToString("00"));
            }
            cboMonth.SelectedIndex = DateTime.Now.Month; // Chọn tháng hiện tại

            // Load năm
            cboYear.Items.Add("-- Tất cả --");
            int currentYear = DateTime.Now.Year;
            for (int i = currentYear - 5; i <= currentYear + 1; i++)
            {
                cboYear.Items.Add(i.ToString());
            }
            cboYear.SelectedItem = currentYear.ToString(); // Chọn năm hiện tại

            // Load trạng thái
            cboStatus.Items.Add("-- Tất cả --");
            foreach (string status in SalaryStatus.GetAll())
            {
                cboStatus.Items.Add(status);
            }
            cboStatus.SelectedIndex = 0;
        }

        private void SetupDataGridView()
        {
            dgvSalaries.AutoGenerateColumns = false;
            dgvSalaries.Columns.Clear();

            dgvSalaries.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "SalaryID",
                DataPropertyName = "SalaryID",
                HeaderText = "ID",
                Width = 60,
                Visible = false
            });

            dgvSalaries.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "EmployeeCode",
                DataPropertyName = "EmployeeCode",
                HeaderText = "Mã NV",
                Width = 100
            });

            dgvSalaries.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "EmployeeName",
                DataPropertyName = "EmployeeName",
                HeaderText = "Họ tên",
                Width = 180
            });

            dgvSalaries.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "DepartmentName",
                DataPropertyName = "DepartmentName",
                HeaderText = "Phòng ban",
                Width = 150
            });

            dgvSalaries.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "PositionName",
                DataPropertyName = "PositionName",
                HeaderText = "Chức vụ",
                Width = 130
            });

            dgvSalaries.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Month",
                DataPropertyName = "Month",
                HeaderText = "Tháng",
                Width = 70,
                DefaultCellStyle = new DataGridViewCellStyle { Format = "00" }
            });

            dgvSalaries.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Year",
                DataPropertyName = "Year",
                HeaderText = "Năm",
                Width = 70
            });

            dgvSalaries.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "BaseSalary",
                DataPropertyName = "BaseSalary",
                HeaderText = "Lương cơ bản",
                Width = 120,
                DefaultCellStyle = new DataGridViewCellStyle { Format = "N0", Alignment = DataGridViewContentAlignment.MiddleRight }
            });

            dgvSalaries.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Bonus",
                DataPropertyName = "Bonus",
                HeaderText = "Thưởng",
                Width = 100,
                DefaultCellStyle = new DataGridViewCellStyle { Format = "N0", Alignment = DataGridViewContentAlignment.MiddleRight }
            });

            dgvSalaries.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Deduction",
                DataPropertyName = "Deduction",
                HeaderText = "Khấu trừ",
                Width = 100,
                DefaultCellStyle = new DataGridViewCellStyle { Format = "N0", Alignment = DataGridViewContentAlignment.MiddleRight }
            });

            dgvSalaries.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "TotalSalary",
                DataPropertyName = "TotalSalary",
                HeaderText = "Tổng lương",
                Width = 130,
                DefaultCellStyle = new DataGridViewCellStyle
                {
                    Format = "N0",
                    Alignment = DataGridViewContentAlignment.MiddleRight,
                    Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold)
                }
            });

            dgvSalaries.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Status",
                DataPropertyName = "Status",
                HeaderText = "Trạng thái",
                Width = 120
            });

            // Tô màu theo trạng thái
            dgvSalaries.CellFormatting += (s, e) =>
            {
                if (e.ColumnIndex == dgvSalaries.Columns["Status"].Index && e.Value != null)
                {
                    string status = e.Value.ToString();
                    switch (status)
                    {
                        case "Nháp":
                            e.CellStyle.BackColor = System.Drawing.Color.LightGray;
                            break;
                        case "Đã tính":
                            e.CellStyle.BackColor = System.Drawing.Color.LightBlue;
                            break;
                        case "Đã duyệt":
                            e.CellStyle.BackColor = System.Drawing.Color.LightGreen;
                            break;
                        case "Đã thanh toán":
                            e.CellStyle.BackColor = System.Drawing.Color.LightSeaGreen;
                            e.CellStyle.ForeColor = System.Drawing.Color.White;
                            break;
                        case "Đã hủy":
                            e.CellStyle.BackColor = System.Drawing.Color.LightCoral;
                            break;
                    }
                }
            };
        }

        private void ApplyPermissions()
        {
            // Chỉ Admin mới có quyền duyệt và thanh toán
            if (currentUser.Role != "Admin")
            {
                btnApprove.Enabled = false;
                btnPay.Enabled = false;
                btnDelete.Enabled = false;
            }
        }

        // ========== LOAD DỮ LIỆU ==========

        private void LoadData()
        {
            try
            {
                int? month = cboMonth.SelectedIndex > 0 ? (int?)cboMonth.SelectedIndex : null;
                int? year = cboYear.SelectedIndex > 0 ? (int?)int.Parse(cboYear.SelectedItem.ToString()) : null;
                string status = cboStatus.SelectedIndex > 0 ? cboStatus.SelectedItem.ToString() : null;
                string searchText = txtSearch.Text.Trim();

                DataTable dt = SalaryBLL.SearchSalaries(month, year, status, searchText);
                dgvSalaries.DataSource = dt;

                // Cập nhật thống kê
                lblTotal.Text = $"Tổng số bản ghi: {dt.Rows.Count}";

                if (dt.Rows.Count > 0)
                {
                    decimal totalSalary = dt.AsEnumerable().Sum(row => row.Field<decimal>("TotalSalary"));
                    lblTotalSalary.Text = $"Tổng lương: {totalSalary:N0} VNĐ";
                }
                else
                {
                    lblTotalSalary.Text = "Tổng lương: 0 VNĐ";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi load dữ liệu: {ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ========== SỰ KIỆN ==========

        private void btnSearch_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            cboMonth.SelectedIndex = DateTime.Now.Month;
            cboYear.SelectedItem = DateTime.Now.Year.ToString();
            cboStatus.SelectedIndex = 0;
            txtSearch.Clear();
            LoadData();
        }

        private void txtSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnSearch_Click(sender, e);
            }
        }

        private void btnCalculate_Click(object sender, EventArgs e)
        {
            frmCalculateSalary frm = new frmCalculateSalary(currentUser);
            frm.ShowDialog();
            LoadData(); // Refresh sau khi tính lương
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            frmSalaryDetail frm = new frmSalaryDetail(currentUser);
            if (frm.ShowDialog() == DialogResult.OK)
            {
                LoadData();
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (dgvSalaries.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn bản ghi cần sửa!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            long salaryId = Convert.ToInt64(dgvSalaries.SelectedRows[0].Cells["SalaryID"].Value);
            frmSalaryDetail frm = new frmSalaryDetail(currentUser, salaryId);
            if (frm.ShowDialog() == DialogResult.OK)
            {
                LoadData();
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgvSalaries.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn bản ghi cần xóa!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult result = MessageBox.Show("Bạn có chắc muốn xóa bản ghi này?", "Xác nhận",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                try
                {
                    long salaryId = Convert.ToInt64(dgvSalaries.SelectedRows[0].Cells["SalaryID"].Value);

                    if (SalaryBLL.DeleteSalary(salaryId))
                    {
                        MessageBox.Show("Xóa thành công!", "Thông báo",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadData();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Lỗi: {ex.Message}", "Lỗi",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnApprove_Click(object sender, EventArgs e)
        {
            if (dgvSalaries.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn bản ghi cần duyệt!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult result = MessageBox.Show("Bạn có chắc muốn duyệt bản ghi này?", "Xác nhận",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                try
                {
                    long salaryId = Convert.ToInt64(dgvSalaries.SelectedRows[0].Cells["SalaryID"].Value);

                    if (SalaryBLL.ApproveSalary(salaryId))
                    {
                        MessageBox.Show("Duyệt thành công!", "Thông báo",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadData();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Lỗi: {ex.Message}", "Lỗi",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnPay_Click(object sender, EventArgs e)
        {
            if (dgvSalaries.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn bản ghi cần thanh toán!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult result = MessageBox.Show("Xác nhận đã thanh toán lương?", "Xác nhận",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                try
                {
                    long salaryId = Convert.ToInt64(dgvSalaries.SelectedRows[0].Cells["SalaryID"].Value);

                    if (SalaryBLL.PaySalary(salaryId))
                    {
                        MessageBox.Show("Cập nhật thanh toán thành công!", "Thông báo",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadData();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Lỗi: {ex.Message}", "Lỗi",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvSalaries.Rows.Count == 0)
                {
                    MessageBox.Show("Không có dữ liệu để xuất!", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                SaveFileDialog sfd = new SaveFileDialog
                {
                    Filter = "Excel Files|*.xlsx",
                    FileName = $"DanhSachLuong_{DateTime.Now:yyyyMMdd_HHmmss}.xlsx"
                };

                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    Cursor = Cursors.WaitCursor;

                    using (var workbook = new XLWorkbook())
                    {
                        var worksheet = workbook.Worksheets.Add("Danh sách lương");

                        // Tiêu đề
                        worksheet.Cell(1, 1).Value = "DANH SÁCH LƯƠNG";
                        worksheet.Cell(1, 1).Style.Font.Bold = true;
                        worksheet.Cell(1, 1).Style.Font.FontSize = 16;
                        worksheet.Range(1, 1, 1, 10).Merge();
                        worksheet.Range(1, 1, 1, 10).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

                        // Header
                        int row = 3;
                        worksheet.Cell(row, 1).Value = "Mã NV";
                        worksheet.Cell(row, 2).Value = "Họ tên";
                        worksheet.Cell(row, 3).Value = "Phòng ban";
                        worksheet.Cell(row, 4).Value = "Chức vụ";
                        worksheet.Cell(row, 5).Value = "Tháng";
                        worksheet.Cell(row, 6).Value = "Năm";
                        worksheet.Cell(row, 7).Value = "Lương CB";
                        worksheet.Cell(row, 8).Value = "Thưởng";
                        worksheet.Cell(row, 9).Value = "Khấu trừ";
                        worksheet.Cell(row, 10).Value = "Tổng lương";
                        worksheet.Cell(row, 11).Value = "Trạng thái";

                        var headerRange = worksheet.Range(row, 1, row, 11);
                        headerRange.Style.Font.Bold = true;
                        headerRange.Style.Fill.BackgroundColor = XLColor.FromHtml("#4472C4");
                        headerRange.Style.Font.FontColor = XLColor.White;
                        headerRange.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

                        // Data
                        row++;
                        DataTable dt = (DataTable)dgvSalaries.DataSource;
                        foreach (DataRow dataRow in dt.Rows)
                        {
                            worksheet.Cell(row, 1).Value = dataRow["EmployeeCode"].ToString();
                            worksheet.Cell(row, 2).Value = dataRow["EmployeeName"].ToString();
                            worksheet.Cell(row, 3).Value = dataRow["DepartmentName"].ToString();
                            worksheet.Cell(row, 4).Value = dataRow["PositionName"].ToString();
                            worksheet.Cell(row, 5).Value = Convert.ToInt32(dataRow["Month"]);
                            worksheet.Cell(row, 6).Value = Convert.ToInt32(dataRow["Year"]);
                            worksheet.Cell(row, 7).Value = Convert.ToDecimal(dataRow["BaseSalary"]);
                            worksheet.Cell(row, 8).Value = Convert.ToDecimal(dataRow["Bonus"]);
                            worksheet.Cell(row, 9).Value = Convert.ToDecimal(dataRow["Deduction"]);
                            worksheet.Cell(row, 10).Value = Convert.ToDecimal(dataRow["TotalSalary"]);
                            worksheet.Cell(row, 11).Value = dataRow["Status"].ToString();

                            // Format tiền
                            worksheet.Cell(row, 7).Style.NumberFormat.Format = "#,##0";
                            worksheet.Cell(row, 8).Style.NumberFormat.Format = "#,##0";
                            worksheet.Cell(row, 9).Style.NumberFormat.Format = "#,##0";
                            worksheet.Cell(row, 10).Style.NumberFormat.Format = "#,##0";

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


        private void btnReport_Click(object sender, EventArgs e)
        {
            frmSalaryReport frm = new frmSalaryReport();
            frm.ShowDialog();
        }

        private void dgvSalaries_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                btnEdit_Click(sender, e);
            }
        }
    }
}
