
using ClosedXML.Excel;
using Quanlynhansu.forms;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Quanlynhansu.DAL;
using Quanlynhansu.BLL;
using Quanlynhansu.Models;
namespace Quanlynhansu.Forms
{
    public partial class frmEmployeeList : Form
    {
        private int currentPage = 1;
        private int pageSize = 50;
        private long totalRecords = 0;
        private int totalPages = 0;

        public frmEmployeeList()
        {
            InitializeComponent();
        }

        private void frmEmployeeList_Load(object sender, EventArgs e)
        {
            LoadDepartments();
            LoadEmployees();
        }

        private void LoadDepartments()
        {
            try
            {
                DataTable dt = DepartmentDAL.GetAllDepartments();

                cboFilterDepartment.DataSource = dt;
                cboFilterDepartment.DisplayMember = "DepartmentName";
                cboFilterDepartment.ValueMember = "DepartmentID";
                cboFilterDepartment.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }
        }

        private void LoadEmployees()
        {
            try
            {
                Cursor = Cursors.WaitCursor;

                string searchText = txtSearch.Text.Trim();
                int? departmentID = cboFilterDepartment.SelectedValue != null ?
                    (int?)Convert.ToInt32(cboFilterDepartment.SelectedValue) : null;
                string status = cboFilterStatus.SelectedItem?.ToString();

                // Lấy tổng số records
                totalRecords = EmployeeDAL.GetTotalEmployees(searchText, departmentID, status);
                totalPages = (int)Math.Ceiling((double)totalRecords / pageSize);

                // Lấy dữ liệu trang hiện tại
                DataTable dt = EmployeeDAL.SearchEmployees(searchText, departmentID, status, currentPage, pageSize);
                dgvEmployees.DataSource = dt;

                // Cập nhật thông tin phân trang
                lblPageInfo.Text = $"Trang {currentPage}/{totalPages} - Tổng: {totalRecords:N0} nhân viên";
                btnPrevPage.Enabled = currentPage > 1;
                btnNextPage.Enabled = currentPage < totalPages;

                // Format DataGridView
                FormatDataGridView();

                Cursor = Cursors.Default;
            }
            catch (Exception ex)
            {
                Cursor = Cursors.Default;
                MessageBox.Show("Lỗi: " + ex.Message);
            }
        }

        private void FormatDataGridView()
        {
            if (dgvEmployees.Columns.Count > 0)
            {
                dgvEmployees.Columns["EmployeeID"].HeaderText = "ID";
                dgvEmployees.Columns["EmployeeID"].Width = 80;
                dgvEmployees.Columns["EmployeeCode"].HeaderText = "Mã NV";
                dgvEmployees.Columns["EmployeeCode"].Width = 100;
                dgvEmployees.Columns["FullName"].HeaderText = "Họ và tên";
                dgvEmployees.Columns["FullName"].Width = 200;
                dgvEmployees.Columns["DateOfBirth"].HeaderText = "Ngày sinh";
                dgvEmployees.Columns["DateOfBirth"].DefaultCellStyle.Format = "dd/MM/yyyy";
                dgvEmployees.Columns["Gender"].HeaderText = "Giới tính";
                dgvEmployees.Columns["PhoneNumber"].HeaderText = "Số điện thoại";
                dgvEmployees.Columns["Email"].HeaderText = "Email";
                dgvEmployees.Columns["Email"].Width = 200;
                dgvEmployees.Columns["DepartmentName"].HeaderText = "Phòng ban";
                dgvEmployees.Columns["PositionName"].HeaderText = "Chức vụ";
                dgvEmployees.Columns["HireDate"].HeaderText = "Ngày vào";
                dgvEmployees.Columns["HireDate"].DefaultCellStyle.Format = "dd/MM/yyyy";
                dgvEmployees.Columns["Status"].HeaderText = "Trạng thái";
                dgvEmployees.Columns["Salary"].HeaderText = "Lương";
                dgvEmployees.Columns["Salary"].DefaultCellStyle.Format = "N0";
                dgvEmployees.Columns["Salary"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

                dgvEmployees.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                dgvEmployees.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                dgvEmployees.MultiSelect = false;
                dgvEmployees.AllowUserToAddRows = false;
                dgvEmployees.ReadOnly = true;
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            currentPage = 1;
            LoadEmployees();
        }

        private void btnPrevPage_Click(object sender, EventArgs e)
        {
            if (currentPage > 1)
            {
                currentPage--;
                LoadEmployees();
            }
        }

        private void btnNextPage_Click(object sender, EventArgs e)
        {
            if (currentPage < totalPages)
            {
                currentPage++;
                LoadEmployees();
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            frmEmployeeDetail frm = new frmEmployeeDetail();
            if (frm.ShowDialog() == DialogResult.OK)
            {
                LoadEmployees();
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (dgvEmployees.SelectedRows.Count > 0)
            {
                long employeeID = Convert.ToInt64(dgvEmployees.SelectedRows[0].Cells["EmployeeID"].Value);
                frmEmployeeDetail frm = new frmEmployeeDetail(employeeID);
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    LoadEmployees();
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn nhân viên cần sửa!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgvEmployees.SelectedRows.Count > 0)
            {
                DialogResult result = MessageBox.Show("Bạn có chắc muốn xóa nhân viên này?",
                    "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    try
                    {
                        long employeeID = Convert.ToInt64(dgvEmployees.SelectedRows[0].Cells["EmployeeID"].Value);
                        if (EmployeeDAL.DeleteEmployee(employeeID))
                        {
                            MessageBox.Show("Xóa nhân viên thành công!", "Thành công",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
                            LoadEmployees();
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Lỗi: " + ex.Message);
                    }
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn nhân viên cần xóa!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            txtSearch.Clear();
            cboFilterDepartment.SelectedIndex = -1;
            cboFilterStatus.SelectedIndex = -1;
            currentPage = 1;
            LoadEmployees();
        }

        private void dgvEmployees_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                long employeeID = Convert.ToInt64(dgvEmployees.Rows[e.RowIndex].Cells["EmployeeID"].Value);
                frmEmployeeDetail frm = new frmEmployeeDetail(employeeID);
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    LoadEmployees();
                }
            }
        }
        // ========== XUẤT EXCEL ==========
        private void btnExportExcel_Click(object sender, EventArgs e)
        {
            try
            {
                SaveFileDialog saveFileDialog = new SaveFileDialog
                {
                    Filter = "Excel Files|*.xlsx",
                    Title = "Lưu file Excel",
                    FileName = $"DanhSachNhanVien_{DateTime.Now:yyyyMMdd_HHmmss}.xlsx"
                };

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    Cursor = Cursors.WaitCursor;

                    // Lấy toàn bộ dữ liệu (không phân trang)
                    DataTable dt = EmployeeDAL.SearchEmployees(
                        txtSearch.Text.Trim(),
                        cboFilterDepartment.SelectedValue != null ? (int?)Convert.ToInt32(cboFilterDepartment.SelectedValue) : null,
                        cboFilterStatus.SelectedItem?.ToString(),
                        1,
                        int.MaxValue
                    );

                    ExportToExcel(dt, saveFileDialog.FileName);

                    Cursor = Cursors.Default;
                    MessageBox.Show("Xuất Excel thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                Cursor = Cursors.Default;
                MessageBox.Show("Lỗi xuất Excel: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ExportToExcel(DataTable dt, string filePath)
        {
            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("Danh sách nhân viên");

                // Header
                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    worksheet.Cell(1, i + 1).Value = dt.Columns[i].ColumnName;
                }

                // Style header
                var headerRange = worksheet.Range(1, 1, 1, dt.Columns.Count);
                headerRange.Style.Font.Bold = true;
                headerRange.Style.Fill.BackgroundColor = XLColor.LightBlue;
                headerRange.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

                // Data
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    for (int j = 0; j < dt.Columns.Count; j++)
                    {
                        worksheet.Cell(i + 2, j + 1).Value = dt.Rows[i][j].ToString();
                    }
                }

                worksheet.Columns().AdjustToContents();
                workbook.SaveAs(filePath);
            }
        }

        // ========== NHẬP EXCEL ==========
        private void btnImportExcel_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog openFileDialog = new OpenFileDialog
                {
                    Filter = "Excel Files|*.xlsx;*.xls",
                    Title = "Chọn file Excel"
                };

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    Cursor = Cursors.WaitCursor;

                    // Đọc file Excel
                    List<Employee> employees = ReadExcelFile(openFileDialog.FileName);

                    if (employees.Count == 0)
                    {
                        MessageBox.Show("File Excel không có dữ liệu!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    // Validate dữ liệu
                    List<string> errors = EmployeeBLL.ValidateImportData(employees);

                    if (errors.Count > 0)
                    {
                        string errorMessage = "Phát hiện lỗi:\n\n" + string.Join("\n", errors.Take(10));
                        if (errors.Count > 10)
                            errorMessage += $"\n\n... và {errors.Count - 10} lỗi khác";

                        MessageBox.Show(errorMessage, "Lỗi dữ liệu", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        Cursor = Cursors.Default;
                        return;
                    }

                    // Xác nhận import
                    DialogResult result = MessageBox.Show(
                        $"Bạn có chắc muốn import {employees.Count} nhân viên?",
                        "Xác nhận",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Question
                    );

                    if (result == DialogResult.Yes)
                    {
                        int successCount = EmployeeBLL.BulkInsertEmployees(employees);

                        MessageBox.Show(
                            $"Import thành công {successCount}/{employees.Count} nhân viên!",
                            "Thành công",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information
                        );

                        LoadEmployees();
                    }

                    Cursor = Cursors.Default;
                }
            }
            catch (Exception ex)
            {
                Cursor = Cursors.Default;
                MessageBox.Show("Lỗi import Excel: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private List<Employee> ReadExcelFile(string filePath)
        {
            List<Employee> employees = new List<Employee>();

            using (var workbook = new XLWorkbook(filePath))
            {
                var worksheet = workbook.Worksheet("Template");

                int rowCount = worksheet.RowsUsed().Count();

                for (int row = 2; row <= rowCount; row++) // Bỏ qua header
                {
                    try
                    {
                        Employee emp = new Employee
                        {
                            EmployeeCode = worksheet.Cell(row, 1).GetString().Trim(),
                            FullName = worksheet.Cell(row, 2).GetString().Trim(),
                            DateOfBirth = ParseDate(worksheet.Cell(row, 3).GetString()),
                            Gender = worksheet.Cell(row, 4).GetString().Trim(),
                            PhoneNumber = worksheet.Cell(row, 5).GetString().Trim(),
                            Email = worksheet.Cell(row, 6).GetString().Trim(),
                            Address = worksheet.Cell(row, 7).GetString().Trim(),
                            IdentityCard = worksheet.Cell(row, 8).GetString().Trim(),
                            DepartmentID = ParseInt(worksheet.Cell(row, 9).GetString()),
                            PositionID = ParseInt(worksheet.Cell(row, 10).GetString()),
                            HireDate = ParseDate(worksheet.Cell(row, 11).GetString()) ?? DateTime.Now,
                            Status = worksheet.Cell(row, 12).GetString().Trim(),
                            Salary = ParseDecimal(worksheet.Cell(row, 13).GetString())
                        };

                        if (string.IsNullOrEmpty(emp.Status))
                            emp.Status = "Đang làm việc";

                        employees.Add(emp);
                    }
                    catch
                    {
                        // Bỏ qua dòng lỗi
                    }
                }
            }

            return employees;
        }

        // ========== TẢI FILE MẪU ==========
        private void btnDownloadTemplate_Click(object sender, EventArgs e)
        {
            try
            {
                SaveFileDialog saveFileDialog = new SaveFileDialog
                {
                    Filter = "Excel Files|*.xlsx",
                    Title = "Lưu file mẫu",
                    FileName = "Template_Import_NhanVien.xlsx"
                };

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    CreateExcelTemplate(saveFileDialog.FileName);
                    MessageBox.Show("Tải file mẫu thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải file mẫu: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CreateExcelTemplate(string filePath)
        {
            using (var workbook = new XLWorkbook())
            {
                // ========== SHEET 1: TEMPLATE ==========
                var worksheet = workbook.Worksheets.Add("Template");

                // Header
                worksheet.Cell(1, 1).Value = "Mã NV";
                worksheet.Cell(1, 2).Value = "Họ và tên";
                worksheet.Cell(1, 3).Value = "Ngày sinh (dd/MM/yyyy)";
                worksheet.Cell(1, 4).Value = "Giới tính";
                worksheet.Cell(1, 5).Value = "Số điện thoại";
                worksheet.Cell(1, 6).Value = "Email";
                worksheet.Cell(1, 7).Value = "Địa chỉ";
                worksheet.Cell(1, 8).Value = "CMND/CCCD";
                worksheet.Cell(1, 9).Value = "Mã phòng ban";
                worksheet.Cell(1, 10).Value = "Mã chức vụ";
                worksheet.Cell(1, 11).Value = "Ngày vào làm (dd/MM/yyyy)";
                worksheet.Cell(1, 12).Value = "Trạng thái";
                worksheet.Cell(1, 13).Value = "Lương";

                // Style header
                var headerRange = worksheet.Range(1, 1, 1, 13);
                headerRange.Style.Font.Bold = true;
                headerRange.Style.Fill.BackgroundColor = XLColor.LightGreen;
                headerRange.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                headerRange.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;

                // Dữ liệu mẫu
                worksheet.Cell(2, 1).Value = "NV001";
                worksheet.Cell(2, 2).Value = "Nguyễn Văn A";
                worksheet.Cell(2, 3).Value = "15/01/1990";
                worksheet.Cell(2, 4).Value = "Nam";
                worksheet.Cell(2, 5).Value = "0901234567";
                worksheet.Cell(2, 6).Value = "a.nguyen@company.com";
                worksheet.Cell(2, 7).Value = "123 Lê Lợi, Q1, TP.HCM";
                worksheet.Cell(2, 8).Value = "001090123456";
                worksheet.Cell(2, 9).Value = 1;
                worksheet.Cell(2, 10).Value = 5;
                worksheet.Cell(2, 11).Value = "10/01/2020";
                worksheet.Cell(2, 12).Value = "Đang làm việc";
                worksheet.Cell(2, 13).Value = 15000000;

                worksheet.Columns().AdjustToContents();

                // ========== SHEET 2: HƯỚNG DẪN ==========
                var instructionSheet = workbook.Worksheets.Add("Hướng dẫn");
                instructionSheet.Cell(1, 1).Value = "HƯỚNG DẪN IMPORT NHÂN VIÊN";
                instructionSheet.Cell(1, 1).Style.Font.Bold = true;
                instructionSheet.Cell(1, 1).Style.Font.FontSize = 14;
                instructionSheet.Cell(1, 1).Style.Font.FontColor = XLColor.DarkBlue;

                instructionSheet.Cell(3, 1).Value = "1. Điền thông tin nhân viên vào sheet 'Template'";
                instructionSheet.Cell(4, 1).Value = "2. Ngày tháng theo định dạng: dd/MM/yyyy (VD: 15/01/1990)";
                instructionSheet.Cell(5, 1).Value = "3. Giới tính: Nam hoặc Nữ";
                instructionSheet.Cell(6, 1).Value = "4. Mã phòng ban và Mã chức vụ: Xem sheet 'Danh sách phòng ban'";
                instructionSheet.Cell(7, 1).Value = "5. Trạng thái: Đang làm việc, Nghỉ việc, Tạm nghỉ";
                instructionSheet.Cell(8, 1).Value = "6. Lương: Nhập số (VD: 15000000)";
                instructionSheet.Cell(10, 1).Value = "LƯU Ý:";
                instructionSheet.Cell(10, 1).Style.Font.Bold = true;
                instructionSheet.Cell(10, 1).Style.Font.FontColor = XLColor.Red;
                instructionSheet.Cell(11, 1).Value = "- Không xóa dòng header (dòng đầu tiên)";
                instructionSheet.Cell(12, 1).Value = "- Không thay đổi tên các cột";
                instructionSheet.Cell(13, 1).Value = "- Mã nhân viên không được trùng";

                instructionSheet.Columns().AdjustToContents();

                // ========== SHEET 3: DANH SÁCH PHÒNG BAN VÀ CHỨC VỤ ==========
                var referenceSheet = workbook.Worksheets.Add("Danh sách phòng ban");

                // Lấy danh sách phòng ban từ database
                DataTable dtDepartments = DepartmentDAL.GetAllDepartments();
                DataTable dtPositions = PositionDAL.GetAllPositions();

                // PHÒNG BAN
                referenceSheet.Cell(1, 1).Value = "DANH SÁCH PHÒNG BAN";
                referenceSheet.Cell(1, 1).Style.Font.Bold = true;
                referenceSheet.Cell(1, 1).Style.Font.FontSize = 12;
                referenceSheet.Range(1, 1, 1, 2).Merge();

                referenceSheet.Cell(2, 1).Value = "Mã PB";
                referenceSheet.Cell(2, 2).Value = "Tên phòng ban";
                referenceSheet.Range(2, 1, 2, 2).Style.Font.Bold = true;
                referenceSheet.Range(2, 1, 2, 2).Style.Fill.BackgroundColor = XLColor.LightBlue;

                int row = 3;
                foreach (DataRow dr in dtDepartments.Rows)
                {
                    referenceSheet.Cell(row, 1).Value = Convert.ToInt32(dr["DepartmentID"]);
                    referenceSheet.Cell(row, 2).Value = dr["DepartmentName"].ToString();
                    row++;
                }

                // CHỨC VỤ
                referenceSheet.Cell(1, 4).Value = "DANH SÁCH CHỨC VỤ";
                referenceSheet.Cell(1, 4).Style.Font.Bold = true;
                referenceSheet.Cell(1, 4).Style.Font.FontSize = 12;
                referenceSheet.Range(1, 4, 1, 5).Merge();

                referenceSheet.Cell(2, 4).Value = "Mã CV";
                referenceSheet.Cell(2, 5).Value = "Tên chức vụ";
                referenceSheet.Range(2, 4, 2, 5).Style.Font.Bold = true;
                referenceSheet.Range(2, 4, 2, 5).Style.Fill.BackgroundColor = XLColor.LightBlue;

                row = 3;
                foreach (DataRow dr in dtPositions.Rows)
                {
                    referenceSheet.Cell(row, 4).Value = Convert.ToInt32(dr["PositionID"]);
                    referenceSheet.Cell(row, 5).Value = dr["PositionName"].ToString();
                    row++;
                }

                referenceSheet.Columns().AdjustToContents();

                // Lưu file
                workbook.SaveAs(filePath);
            }
        }

        // ========== HÀM HỖ TRỢ ==========
        private DateTime? ParseDate(string dateString)
        {
            if (string.IsNullOrWhiteSpace(dateString))
                return null;

            try
            {
                return DateTime.ParseExact(dateString.Trim(), "dd/MM/yyyy", null);
            }
            catch
            {
                return null;
            }
        }

        private int? ParseInt(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                return null;

            if (int.TryParse(value.Trim(), out int result))
                return result;

            return null;
        }

        private decimal? ParseDecimal(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                return null;

            if (decimal.TryParse(value.Trim(), out decimal result))
                return result;

            return null;
        }

    }
}
