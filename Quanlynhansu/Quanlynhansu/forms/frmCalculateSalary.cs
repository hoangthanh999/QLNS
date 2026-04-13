using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using Quanlynhansu.BLL;
using Quanlynhansu.DAL;
using Quanlynhansu.Models;

namespace Quanlynhansu.Forms
{
    public partial class frmCalculateSalary : Form
    {
        private User currentUser;
        private DataTable dtEmployees;

        public frmCalculateSalary(User user)
        {
            InitializeComponent();
            this.currentUser = user;
        }

        private void frmCalculateSalary_Load(object sender, EventArgs e)
        {
            LoadComboBoxes();
            SetupDataGridView();
        }

        // ========== KHỞI TẠO ==========

        private void LoadComboBoxes()
        {
            // Load tháng
            for (int i = 1; i <= 12; i++)
            {
                cboMonth.Items.Add(i.ToString("00"));
            }
            cboMonth.SelectedIndex = DateTime.Now.Month - 1;

            // Load năm
            int currentYear = DateTime.Now.Year;
            for (int i = currentYear - 2; i <= currentYear + 1; i++)
            {
                cboYear.Items.Add(i.ToString());
            }
            cboYear.SelectedItem = currentYear.ToString();

            // Load phòng ban
            cboDepartment.Items.Add("-- Tất cả --");
            DataTable dtDepts = DepartmentDAL.GetAllDepartments();
            foreach (DataRow row in dtDepts.Rows)
            {
                cboDepartment.Items.Add(new
                {
                    Text = row["DepartmentName"].ToString(),
                    Value = row["DepartmentID"]
                });
            }
            cboDepartment.DisplayMember = "Text";
            cboDepartment.ValueMember = "Value";
            cboDepartment.SelectedIndex = 0;

            // Tính số ngày làm việc chuẩn
            UpdateStandardDays();
        }

        private void SetupDataGridView()
        {
            dgvEmployees.AutoGenerateColumns = false;
            dgvEmployees.Columns.Clear();

            // Checkbox chọn
            DataGridViewCheckBoxColumn chkCol = new DataGridViewCheckBoxColumn
            {
                Name = "Selected",
                HeaderText = "☑️",
                Width = 40,
                ReadOnly = false
            };
            dgvEmployees.Columns.Add(chkCol);

            dgvEmployees.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "EmployeeID",
                DataPropertyName = "EmployeeID",
                HeaderText = "ID",
                Visible = false
            });

            dgvEmployees.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "EmployeeCode",
                DataPropertyName = "EmployeeCode",
                HeaderText = "Mã NV",
                Width = 100,
                ReadOnly = true
            });

            dgvEmployees.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "FullName",
                DataPropertyName = "FullName",
                HeaderText = "Họ tên",
                Width = 180,
                ReadOnly = true
            });

            dgvEmployees.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "DepartmentName",
                DataPropertyName = "DepartmentName",
                HeaderText = "Phòng ban",
                Width = 150,
                ReadOnly = true
            });

            dgvEmployees.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "PositionName",
                DataPropertyName = "PositionName",
                HeaderText = "Chức vụ",
                Width = 130,
                ReadOnly = true
            });

            dgvEmployees.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "BaseSalary",
                DataPropertyName = "BaseSalary",
                HeaderText = "Lương CB",
                Width = 120,
                ReadOnly = true,
                DefaultCellStyle = new DataGridViewCellStyle
                {
                    Format = "N0",
                    Alignment = DataGridViewContentAlignment.MiddleRight
                }
            });

            dgvEmployees.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "WorkDays",
                DataPropertyName = "WorkDays",
                HeaderText = "Ngày làm",
                Width = 90,
                ReadOnly = true,
                DefaultCellStyle = new DataGridViewCellStyle
                {
                    Alignment = DataGridViewContentAlignment.MiddleCenter
                }
            });

            dgvEmployees.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "WorkHours",
                DataPropertyName = "WorkHours",
                HeaderText = "Giờ làm",
                Width = 90,
                ReadOnly = true,
                DefaultCellStyle = new DataGridViewCellStyle
                {
                    Format = "N1",
                    Alignment = DataGridViewContentAlignment.MiddleCenter
                }
            });

            dgvEmployees.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "LeaveDays",
                DataPropertyName = "LeaveDays",
                HeaderText = "Ngày nghỉ",
                Width = 90,
                ReadOnly = true,
                DefaultCellStyle = new DataGridViewCellStyle
                {
                    Alignment = DataGridViewContentAlignment.MiddleCenter
                }
            });

            dgvEmployees.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Bonus",
                HeaderText = "Thưởng",
                Width = 100,
                ReadOnly = false,
                DefaultCellStyle = new DataGridViewCellStyle
                {
                    Format = "N0",
                    Alignment = DataGridViewContentAlignment.MiddleRight
                }
            });

            dgvEmployees.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Deduction",
                HeaderText = "Khấu trừ",
                Width = 100,
                ReadOnly = false,
                DefaultCellStyle = new DataGridViewCellStyle
                {
                    Format = "N0",
                    Alignment = DataGridViewContentAlignment.MiddleRight
                }
            });

            dgvEmployees.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "TotalSalary",
                HeaderText = "Tổng lương",
                Width = 130,
                ReadOnly = true,
                DefaultCellStyle = new DataGridViewCellStyle
                {
                    Format = "N0",
                    Alignment = DataGridViewContentAlignment.MiddleRight,
                    Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold)
                }
            });

            dgvEmployees.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "HasSalary",
                DataPropertyName = "HasSalary",
                HeaderText = "Đã có lương",
                Width = 100,
                ReadOnly = true,
                Visible = false
            });

            // Tô màu dòng đã có lương
            dgvEmployees.CellFormatting += (s, e) =>
            {
                if (e.RowIndex >= 0 && dgvEmployees.Rows[e.RowIndex].Cells["HasSalary"].Value != null)
                {
                    // ✅ SỬA: Kiểm tra DBNull trước khi convert
                    object hasSalaryValue = dgvEmployees.Rows[e.RowIndex].Cells["HasSalary"].Value;
                    if (hasSalaryValue != null && hasSalaryValue != DBNull.Value)
                    {
                        int hasSalary = Convert.ToInt32(hasSalaryValue);
                        if (hasSalary == 1)
                        {
                            e.CellStyle.BackColor = System.Drawing.Color.LightYellow;
                        }
                    }
                }
            };
        }

        private void UpdateStandardDays()
        {
            if (cboMonth.SelectedIndex >= 0 && cboYear.SelectedIndex >= 0)
            {
                int month = int.Parse(cboMonth.SelectedItem.ToString());
                int year = int.Parse(cboYear.SelectedItem.ToString());
                int standardDays = SalaryBLL.GetStandardWorkDays(month, year);
                txtStandardDays.Text = standardDays.ToString();
            }
        }

        // ========== SỰ KIỆN ==========

        private void cboMonth_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateStandardDays();
        }

        private void cboYear_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateStandardDays();
        }

        // ✅ HELPER METHOD - Chuyển đổi an toàn
        private decimal SafeDecimal(object value)
        {
            if (value == null || value == DBNull.Value)
                return 0;

            try
            {
                return Convert.ToDecimal(value);
            }
            catch
            {
                return 0;
            }
        }

        private int SafeInt(object value)
        {
            if (value == null || value == DBNull.Value)
                return 0;

            try
            {
                return Convert.ToInt32(value);
            }
            catch
            {
                return 0;
            }
        }

        private void btnLoadEmployees_Click(object sender, EventArgs e)
        {
            try
            {
                if (cboMonth.SelectedIndex < 0)
                {
                    MessageBox.Show("Vui lòng chọn tháng!", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (cboYear.SelectedIndex < 0)
                {
                    MessageBox.Show("Vui lòng chọn năm!", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                int month = int.Parse(cboMonth.SelectedItem.ToString());
                int year = int.Parse(cboYear.SelectedItem.ToString());
                long? departmentId = null;

                if (cboDepartment.SelectedIndex > 0)
                {
                    var selectedItem = cboDepartment.SelectedItem;
                    departmentId = Convert.ToInt64(selectedItem.GetType().GetProperty("Value").GetValue(selectedItem));
                }

                dtEmployees = SalaryDAL.GetEmployeesForSalaryCalculation(month, year, departmentId);

                // Thêm cột Bonus và Deduction mặc định = 0
                if (!dtEmployees.Columns.Contains("Bonus"))
                    dtEmployees.Columns.Add("Bonus", typeof(decimal)).DefaultValue = 0;

                if (!dtEmployees.Columns.Contains("Deduction"))
                    dtEmployees.Columns.Add("Deduction", typeof(decimal)).DefaultValue = 0;

                if (!dtEmployees.Columns.Contains("TotalSalary"))
                    dtEmployees.Columns.Add("TotalSalary", typeof(decimal)).DefaultValue = 0;

                // ✅ SỬA: Tính lương dự kiến với SafeDecimal
                int standardDays = int.Parse(txtStandardDays.Text);
                foreach (DataRow row in dtEmployees.Rows)
                {
                    decimal baseSalary = SafeDecimal(row["BaseSalary"]);
                    int workDays = SafeInt(row["WorkDays"]);
                    decimal bonus = 0;
                    decimal deduction = 0;

                    decimal totalSalary = SalaryBLL.CalculateSalary(baseSalary, workDays, standardDays, bonus, deduction);
                    row["TotalSalary"] = totalSalary;
                    row["Bonus"] = 0;
                    row["Deduction"] = 0;
                }

                dgvEmployees.DataSource = dtEmployees;

                // ✅ SỬA: Auto check các nhân viên chưa có lương
                foreach (DataGridViewRow row in dgvEmployees.Rows)
                {
                    int hasSalary = SafeInt(row.Cells["HasSalary"].Value);
                    row.Cells["Selected"].Value = hasSalary == 0;
                }

                UpdateSummary();

                MessageBox.Show($"Đã tải {dtEmployees.Rows.Count} nhân viên!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi: {ex.Message}\n\nStack Trace:\n{ex.StackTrace}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSelectAll_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in dgvEmployees.Rows)
            {
                row.Cells["Selected"].Value = true;
            }
            UpdateSummary();
        }

        private void btnDeselectAll_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in dgvEmployees.Rows)
            {
                row.Cells["Selected"].Value = false;
            }
            UpdateSummary();
        }

        private void dgvEmployees_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            // ✅ SỬA: Nếu thay đổi Bonus hoặc Deduction thì tính lại tổng lương
            if (e.ColumnIndex == dgvEmployees.Columns["Bonus"].Index ||
                e.ColumnIndex == dgvEmployees.Columns["Deduction"].Index)
            {
                DataGridViewRow row = dgvEmployees.Rows[e.RowIndex];

                decimal baseSalary = SafeDecimal(row.Cells["BaseSalary"].Value);
                int workDays = SafeInt(row.Cells["WorkDays"].Value);
                int standardDays = int.Parse(txtStandardDays.Text);

                decimal bonus = SafeDecimal(row.Cells["Bonus"].Value);
                decimal deduction = SafeDecimal(row.Cells["Deduction"].Value);

                decimal totalSalary = SalaryBLL.CalculateSalary(baseSalary, workDays, standardDays, bonus, deduction);
                row.Cells["TotalSalary"].Value = totalSalary;
            }

            // Nếu thay đổi checkbox thì cập nhật tổng
            if (e.ColumnIndex == dgvEmployees.Columns["Selected"].Index)
            {
                UpdateSummary();
            }
        }

        private void UpdateSummary()
        {
            int selectedCount = 0;
            decimal totalSalary = 0;

            foreach (DataGridViewRow row in dgvEmployees.Rows)
            {
                bool isSelected = row.Cells["Selected"].Value != null &&
                                  Convert.ToBoolean(row.Cells["Selected"].Value);

                if (isSelected)
                {
                    selectedCount++;
                    totalSalary += SafeDecimal(row.Cells["TotalSalary"].Value);
                }
            }

            lblTotalEmployees.Text = $"Số NV được chọn: {selectedCount}";
            lblTotalSalary.Text = $"Tổng lương dự kiến: {totalSalary:N0} VNĐ";
        }

        private void btnCalculate_Click(object sender, EventArgs e)
        {
            // Đếm số nhân viên được chọn
            int selectedCount = dgvEmployees.Rows.Cast<DataGridViewRow>()
                .Count(r => r.Cells["Selected"].Value != null && Convert.ToBoolean(r.Cells["Selected"].Value));

            if (selectedCount == 0)
            {
                MessageBox.Show("Vui lòng chọn ít nhất 1 nhân viên!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult result = MessageBox.Show(
                $"Bạn có chắc muốn tính lương cho {selectedCount} nhân viên?\n\n" +
                $"Tháng: {cboMonth.SelectedItem}/{cboYear.SelectedItem}\n" +
                $"Tổng lương: {lblTotalSalary.Text}",
                "Xác nhận",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                ProcessSalaryCalculation();
            }
        }

        private void ProcessSalaryCalculation()
        {
            try
            {
                // Hiển thị progress bar
                progressBar.Visible = true;
                lblProgress.Visible = true;
                btnCalculate.Enabled = false;

                int month = int.Parse(cboMonth.SelectedItem.ToString());
                int year = int.Parse(cboYear.SelectedItem.ToString());
                int standardDays = int.Parse(txtStandardDays.Text);
                bool overwriteExisting = chkOverwriteExisting.Checked;
                bool autoApprove = chkAutoApprove.Checked;

                int totalCount = 0;
                int successCount = 0;
                int skipCount = 0;
                int errorCount = 0;

                var selectedRows = dgvEmployees.Rows.Cast<DataGridViewRow>()
                    .Where(r => r.Cells["Selected"].Value != null && Convert.ToBoolean(r.Cells["Selected"].Value))
                    .ToList();

                progressBar.Maximum = selectedRows.Count;
                progressBar.Value = 0;

                foreach (DataGridViewRow row in selectedRows)
                {
                    totalCount++;
                    lblProgress.Text = $"Đang xử lý: {totalCount}/{selectedRows.Count}";
                    progressBar.Value = totalCount;
                    Application.DoEvents();

                    try
                    {
                        long employeeId = Convert.ToInt64(row.Cells["EmployeeID"].Value);
                        int hasSalary = SafeInt(row.Cells["HasSalary"].Value);

                        // Kiểm tra đã có lương chưa
                        if (hasSalary == 1 && !overwriteExisting)
                        {
                            skipCount++;
                            continue;
                        }

                        // ✅ SỬA: Sử dụng SafeDecimal và SafeInt
                        decimal baseSalary = SafeDecimal(row.Cells["BaseSalary"].Value);
                        int workDays = SafeInt(row.Cells["WorkDays"].Value);
                        decimal bonus = SafeDecimal(row.Cells["Bonus"].Value);
                        decimal deduction = SafeDecimal(row.Cells["Deduction"].Value);

                        decimal totalSalary = SalaryBLL.CalculateSalary(baseSalary, workDays, standardDays, bonus, deduction);

                        Salary salary = new Salary
                        {
                            EmployeeID = employeeId,
                            Month = month,
                            Year = year,
                            BaseSalary = baseSalary,
                            Bonus = bonus,
                            Deduction = deduction,
                            TotalSalary = totalSalary,
                            Status = autoApprove ? SalaryStatus.Approved : SalaryStatus.Calculated
                        };

                        bool saveResult;
                        if (hasSalary == 1 && overwriteExisting)
                        {
                            // Lấy SalaryID cũ để update
                            DataTable dtOldSalary = SalaryDAL.SearchSalaries(month, year, null, null);
                            DataRow[] oldRows = dtOldSalary.Select($"EmployeeID = {employeeId}");
                            if (oldRows.Length > 0)
                            {
                                salary.SalaryID = Convert.ToInt64(oldRows[0]["SalaryID"]);
                                saveResult = SalaryBLL.UpdateSalary(salary);
                            }
                            else
                            {
                                saveResult = SalaryBLL.AddSalary(salary);
                            }
                        }
                        else
                        {
                            saveResult = SalaryBLL.AddSalary(salary);
                        }

                        if (saveResult)
                        {
                            successCount++;
                            row.Cells["HasSalary"].Value = 1;
                            row.DefaultCellStyle.BackColor = System.Drawing.Color.LightGreen;
                        }
                        else
                        {
                            errorCount++;
                        }
                    }
                    catch (Exception ex)
                    {
                        errorCount++;
                        MessageBox.Show($"Lỗi xử lý nhân viên {row.Cells["EmployeeCode"].Value}: {ex.Message}",
                            "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }

                // Ẩn progress bar
                progressBar.Visible = false;
                lblProgress.Visible = false;
                btnCalculate.Enabled = true;

                // Hiển thị kết quả
                string message = $"Kết quả tính lương:\n\n" +
                                $"✅ Thành công: {successCount}\n" +
                                $"⏭️ Bỏ qua: {skipCount}\n" +
                                $"❌ Lỗi: {errorCount}\n" +
                                $"📊 Tổng: {totalCount}";

                MessageBox.Show(message, "Hoàn thành",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Reload lại danh sách
                btnLoadEmployees_Click(null, null);
            }
            catch (Exception ex)
            {
                progressBar.Visible = false;
                lblProgress.Visible = false;
                btnCalculate.Enabled = true;

                MessageBox.Show($"Lỗi: {ex.Message}\n\nStack Trace:\n{ex.StackTrace}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
