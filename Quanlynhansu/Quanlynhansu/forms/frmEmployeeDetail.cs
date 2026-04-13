using System;
using System.Data;
using System.Windows.Forms;
using Quanlynhansu.DAL;
using Quanlynhansu.Models;

namespace Quanlynhansu.Forms
{
    public partial class frmEmployeeDetail : Form
    {
        private long? employeeID = null;
        private bool isEditMode = false;

        // Constructor cho chế độ thêm mới
        public frmEmployeeDetail()
        {
            InitializeComponent();
            isEditMode = false;
            this.Text = "Thêm nhân viên mới";
        }

        // Constructor cho chế độ sửa
        public frmEmployeeDetail(long empID)
        {
            InitializeComponent();
            employeeID = empID;
            isEditMode = true;
            this.Text = "Sửa thông tin nhân viên";
        }

        private void frmEmployeeDetail_Load(object sender, EventArgs e)
        {
            LoadDepartments();
            LoadPositions();
            LoadStatus();

            if (isEditMode && employeeID.HasValue)
            {
                LoadEmployeeData();
            }
            else
            {
                // Tạo mã nhân viên tự động
                txtEmployeeCode.Text = GenerateEmployeeCode();
                dtpHireDate.Value = DateTime.Now;
                cboStatus.SelectedIndex = 0;
            }
        }

        private void LoadDepartments()
        {
            try
            {
                DataTable dt = DepartmentDAL.GetAllDepartments();

                cboDepartment.DataSource = dt;
                cboDepartment.DisplayMember = "DepartmentName";
                cboDepartment.ValueMember = "DepartmentID";
                cboDepartment.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi load phòng ban: " + ex.Message);
            }
        }

        private void LoadPositions()
        {
            try
            {
                DataTable dt = PositionDAL.GetAllPositions();

                cboPosition.DataSource = dt;
                cboPosition.DisplayMember = "PositionName";
                cboPosition.ValueMember = "PositionID";
                cboPosition.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi load chức vụ: " + ex.Message);
            }
        }

        private void LoadStatus()
        {
            cboStatus.Items.Clear();
            cboStatus.Items.Add("Đang làm việc");
            cboStatus.Items.Add("Nghỉ việc");
            cboStatus.Items.Add("Tạm nghỉ");
        }

        private void LoadEmployeeData()
        {
            try
            {
                Employee emp = EmployeeDAL.GetEmployeeByID(employeeID.Value);

                if (emp != null)
                {
                    txtEmployeeCode.Text = emp.EmployeeCode;
                    txtEmployeeCode.ReadOnly = true; // Không cho sửa mã NV
                    txtFullName.Text = emp.FullName;

                    if (emp.DateOfBirth.HasValue)
                        dtpDateOfBirth.Value = emp.DateOfBirth.Value;

                    if (emp.Gender == "Nam")
                        rdoMale.Checked = true;
                    else if (emp.Gender == "Nữ")
                        rdoFemale.Checked = true;

                    txtPhoneNumber.Text = emp.PhoneNumber;
                    txtEmail.Text = emp.Email;
                    txtAddress.Text = emp.Address;
                    txtIdentityCard.Text = emp.IdentityCard;

                    if (emp.DepartmentID.HasValue)
                        cboDepartment.SelectedValue = emp.DepartmentID.Value;

                    if (emp.PositionID.HasValue)
                        cboPosition.SelectedValue = emp.PositionID.Value;

                    dtpHireDate.Value = emp.HireDate;
                    cboStatus.Text = emp.Status;

                    if (emp.Salary.HasValue)
                        txtSalary.Text = emp.Salary.Value.ToString("N0");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi load dữ liệu: " + ex.Message);
            }
        }

        private string GenerateEmployeeCode()
        {
            return "NV" + DateTime.Now.ToString("yyyyMMddHHmmss");
        }

        private bool ValidateData()
        {
            if (string.IsNullOrWhiteSpace(txtEmployeeCode.Text))
            {
                MessageBox.Show("Vui lòng nhập mã nhân viên!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtEmployeeCode.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtFullName.Text))
            {
                MessageBox.Show("Vui lòng nhập họ tên!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtFullName.Focus();
                return false;
            }

            if (!string.IsNullOrWhiteSpace(txtEmail.Text))
            {
                if (!IsValidEmail(txtEmail.Text))
                {
                    MessageBox.Show("Email không hợp lệ!", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtEmail.Focus();
                    return false;
                }
            }

            return true;
        }

        private bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!ValidateData())
                return;

            try
            {
                Employee emp = new Employee
                {
                    EmployeeCode = txtEmployeeCode.Text.Trim(),
                    FullName = txtFullName.Text.Trim(),
                    DateOfBirth = chkDateOfBirth.Checked ? dtpDateOfBirth.Value : (DateTime?)null,
                    Gender = rdoMale.Checked ? "Nam" : (rdoFemale.Checked ? "Nữ" : "Khác"),
                    PhoneNumber = txtPhoneNumber.Text.Trim(),
                    Email = txtEmail.Text.Trim(),
                    Address = txtAddress.Text.Trim(),
                    IdentityCard = txtIdentityCard.Text.Trim(),
                    DepartmentID = cboDepartment.SelectedValue != null ?
                        (int?)Convert.ToInt32(cboDepartment.SelectedValue) : null,
                    PositionID = cboPosition.SelectedValue != null ?
                        (int?)Convert.ToInt32(cboPosition.SelectedValue) : null,
                    HireDate = dtpHireDate.Value,
                    Status = cboStatus.Text,
                    Salary = !string.IsNullOrWhiteSpace(txtSalary.Text) ?
                        (decimal?)Convert.ToDecimal(txtSalary.Text.Replace(",", "")) : null
                };

                bool result = false;

                if (isEditMode && employeeID.HasValue)
                {
                    emp.EmployeeID = employeeID.Value;
                    result = EmployeeDAL.UpdateEmployee(emp);
                }
                else
                {
                    result = EmployeeDAL.InsertEmployee(emp);
                }

                if (result)
                {
                    MessageBox.Show(isEditMode ? "Cập nhật thành công!" : "Thêm mới thành công!",
                        "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Có lỗi xảy ra!", "Lỗi",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void txtSalary_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Chỉ cho phép nhập số
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void txtSalary_Leave(object sender, EventArgs e)
        {
            // Format số tiền
            if (!string.IsNullOrWhiteSpace(txtSalary.Text))
            {
                try
                {
                    decimal value = Convert.ToDecimal(txtSalary.Text.Replace(",", ""));
                    txtSalary.Text = value.ToString("N0");
                }
                catch { }
            }
        }
    }
}
