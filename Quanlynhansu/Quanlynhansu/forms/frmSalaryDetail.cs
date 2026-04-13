using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using Quanlynhansu.BLL;
using Quanlynhansu.DAL;
using Quanlynhansu.Models;

namespace Quanlynhansu.Forms
{
    public partial class frmSalaryDetail : Form
    {
        private User currentUser;
        private long? salaryId;
        private bool isEditMode = false;

        // Constructor cho thêm mới
        public frmSalaryDetail(User user)
        {
            InitializeComponent();
            this.currentUser = user;
            this.isEditMode = false;
        }

        // Constructor cho sửa
        public frmSalaryDetail(User user, long salaryId)
        {
            InitializeComponent();
            this.currentUser = user;
            this.salaryId = salaryId;
            this.isEditMode = true;
        }

        private void frmSalaryDetail_Load(object sender, EventArgs e)
        {
            LoadComboBoxes();

            if (isEditMode && salaryId.HasValue)
            {
                lblTitle.Text = "✏️ SỬA THÔNG TIN LƯƠNG";
                LoadSalaryData();
                cboEmployee.Enabled = false;
                cboMonth.Enabled = false;
                cboYear.Enabled = false;
            }
            else
            {
                lblTitle.Text = "➕ THÊM MỚI LƯƠNG";
                cboStatus.SelectedIndex = 0; // Nháp
            }
        }

        // ========== KHỞI TẠO ==========

        private void LoadComboBoxes()
        {
            // Load nhân viên
            DataTable dtEmployees = EmployeeDAL.GetAllEmployees();
            cboEmployee.DataSource = dtEmployees;
            cboEmployee.DisplayMember = "DisplayInfo"; // EmployeeCode - FullName
            cboEmployee.ValueMember = "EmployeeID";

            // Thêm cột DisplayInfo nếu chưa có
            if (!dtEmployees.Columns.Contains("DisplayInfo"))
            {
                dtEmployees.Columns.Add("DisplayInfo", typeof(string),
                    "EmployeeCode + ' - ' + FullName");
            }

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

            // Load trạng thái
            foreach (string status in SalaryStatus.GetAll())
            {
                cboStatus.Items.Add(status);
            }
        }

        private void LoadSalaryData()
        {
            try
            {
                Salary salary = SalaryBLL.GetSalaryById(salaryId.Value);
                if (salary != null)
                {
                    cboEmployee.SelectedValue = salary.EmployeeID;
                    cboMonth.SelectedItem = salary.Month.ToString("00");
                    cboYear.SelectedItem = salary.Year.ToString();
                    txtBaseSalary.Text = salary.BaseSalary.ToString("N0");
                    txtBonus.Text = salary.Bonus.ToString("N0");
                    txtDeduction.Text = salary.Deduction.ToString("N0");
                    txtTotalSalary.Text = salary.TotalSalary.ToString("N0");
                    cboStatus.SelectedItem = salary.Status;

                    if (salary.PaymentDate.HasValue)
                    {
                        chkHasPaymentDate.Checked = true;
                        dtpPaymentDate.Value = salary.PaymentDate.Value;
                    }

                    // Load thông tin chấm công
                    LoadAttendanceInfo();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi load dữ liệu: {ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ========== XỬ LÝ CHẤM CÔNG ==========

        private void LoadAttendanceInfo()
        {
            if (cboEmployee.SelectedValue == null || cboMonth.SelectedIndex < 0 || cboYear.SelectedIndex < 0)
                return;

            try
            {
                long employeeId = Convert.ToInt64(cboEmployee.SelectedValue);
                int month = int.Parse(cboMonth.SelectedItem.ToString());
                int year = int.Parse(cboYear.SelectedItem.ToString());

                // Lấy thông tin chấm công từ database
                DataTable dt = AttendanceDAL.GetAttendanceSummary(employeeId, month, year);

                if (dt.Rows.Count > 0)
                {
                    DataRow row = dt.Rows[0];
                    txtWorkDays.Text = row["WorkDays"].ToString();
                    txtWorkHours.Text = Convert.ToDecimal(row["WorkHours"]).ToString("N1");
                }
                else
                {
                    txtWorkDays.Text = "0";
                    txtWorkHours.Text = "0";
                }

                // Lấy số ngày nghỉ phép
                DataTable dtLeave = LeaveRequestDAL.GetApprovedLeaveDays(employeeId, month, year);
                if (dtLeave.Rows.Count > 0)
                {
                    txtLeaveDays.Text = dtLeave.Rows[0]["LeaveDays"].ToString();
                }
                else
                {
                    txtLeaveDays.Text = "0";
                }

                // Tính số ngày làm việc chuẩn
                int standardDays = SalaryBLL.GetStandardWorkDays(month, year);
                txtStandardDays.Text = standardDays.ToString();

                // Load lương cơ bản từ nhân viên
                if (!isEditMode)
                {
                    DataTable dtEmp = EmployeeDAL.GetEmployeeById(employeeId);
                    if (dtEmp.Rows.Count > 0)
                    {
                        decimal baseSalary = Convert.ToDecimal(dtEmp.Rows[0]["Salary"]);
                        txtBaseSalary.Text = baseSalary.ToString("N0");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi load thông tin chấm công: {ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ========== SỰ KIỆN ==========

        private void cboEmployee_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadAttendanceInfo();
        }

        private void cboMonth_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadAttendanceInfo();
        }

        private void cboYear_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadAttendanceInfo();
        }

        private void btnCalculate_Click(object sender, EventArgs e)
        {
            try
            {
                decimal baseSalary = decimal.Parse(txtBaseSalary.Text.Replace(",", ""));
                decimal bonus = decimal.Parse(txtBonus.Text.Replace(",", ""));
                decimal deduction = decimal.Parse(txtDeduction.Text.Replace(",", ""));
                int workDays = int.Parse(txtWorkDays.Text);
                int standardDays = int.Parse(txtStandardDays.Text);

                decimal totalSalary = SalaryBLL.CalculateSalary(baseSalary, workDays, standardDays, bonus, deduction);
                txtTotalSalary.Text = totalSalary.ToString("N0");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi tính lương: {ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtSalary_TextChanged(object sender, EventArgs e)
        {
            // Auto calculate khi thay đổi giá trị
            TextBox txt = sender as TextBox;
            if (txt != null && !string.IsNullOrEmpty(txt.Text))
            {
                try
                {
                    // Format số với dấu phẩy
                    string value = txt.Text.Replace(",", "");
                    if (decimal.TryParse(value, out decimal number))
                    {
                        int cursorPosition = txt.SelectionStart;
                        int originalLength = txt.Text.Length;

                        txt.Text = number.ToString("N0");

                        int newLength = txt.Text.Length;
                        cursorPosition += (newLength - originalLength);
                        txt.SelectionStart = Math.Max(0, cursorPosition);
                    }
                }
                catch { }
            }
        }

        private void txtNumber_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Chỉ cho phép nhập số
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void chkHasPaymentDate_CheckedChanged(object sender, EventArgs e)
        {
            dtpPaymentDate.Enabled = chkHasPaymentDate.Checked;
            if (chkHasPaymentDate.Checked)
            {
                dtpPaymentDate.Value = DateTime.Now;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                // Validation
                if (cboEmployee.SelectedValue == null)
                {
                    MessageBox.Show("Vui lòng chọn nhân viên!", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    cboEmployee.Focus();
                    return;
                }

                if (cboMonth.SelectedIndex < 0)
                {
                    MessageBox.Show("Vui lòng chọn tháng!", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    cboMonth.Focus();
                    return;
                }

                if (cboYear.SelectedIndex < 0)
                {
                    MessageBox.Show("Vui lòng chọn năm!", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    cboYear.Focus();
                    return;
                }

                if (string.IsNullOrWhiteSpace(txtBaseSalary.Text) || txtBaseSalary.Text == "0")
                {
                    MessageBox.Show("Vui lòng nhập lương cơ bản!", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtBaseSalary.Focus();
                    return;
                }

                if (cboStatus.SelectedIndex < 0)
                {
                    MessageBox.Show("Vui lòng chọn trạng thái!", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    cboStatus.Focus();
                    return;
                }

                // Tạo object Salary
                Salary salary = new Salary
                {
                    EmployeeID = Convert.ToInt64(cboEmployee.SelectedValue),
                    Month = int.Parse(cboMonth.SelectedItem.ToString()),
                    Year = int.Parse(cboYear.SelectedItem.ToString()),
                    BaseSalary = decimal.Parse(txtBaseSalary.Text.Replace(",", "")),
                    Bonus = decimal.Parse(txtBonus.Text.Replace(",", "")),
                    Deduction = decimal.Parse(txtDeduction.Text.Replace(",", "")),
                    TotalSalary = decimal.Parse(txtTotalSalary.Text.Replace(",", "")),
                    Status = cboStatus.SelectedItem.ToString(),
                    PaymentDate = chkHasPaymentDate.Checked ? (DateTime?)dtpPaymentDate.Value : null
                };

                bool result;
                if (isEditMode && salaryId.HasValue)
                {
                    salary.SalaryID = salaryId.Value;
                    result = SalaryBLL.UpdateSalary(salary);
                }
                else
                {
                    result = SalaryBLL.AddSalary(salary);
                }

                if (result)
                {
                    MessageBox.Show("Lưu thành công!", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Lưu thất bại!", "Lỗi",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi: {ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
