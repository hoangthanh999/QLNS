using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using Quanlynhansu.DAL;
using Quanlynhansu.Utilities;

namespace Quanlynhansu.Forms
{
    public partial class frmUserManagement : Form
    {
        private DataTable dtUsers;

        public frmUserManagement()
        {
            InitializeComponent();
        }

        private void frmUserManagement_Load(object sender, EventArgs e)
        {
            LoadUsers();
            LoadRoles();
            LoadEmployees();
        }

        private void LoadUsers()
        {
            try
            {
                string query = @"
                    SELECT 
                        u.UserID,
                        u.Username,
                        u.Role,
                        u.IsActive,
                        u.LastLogin,
                        u.EmployeeID,
                        e.FullName AS EmployeeName,
                        e.Email,
                        e.PhoneNumber
                    FROM Users u
                    LEFT JOIN Employees e ON u.EmployeeID = e.EmployeeID
                    ORDER BY u.UserID DESC";

                using (SqlConnection conn = DatabaseConnection.GetConnection())
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                        {
                            dtUsers = new DataTable();
                            adapter.Fill(dtUsers);
                        }
                    }
                }

                dgvUsers.DataSource = dtUsers;

                // Định dạng DataGridView
                if (dgvUsers.Columns["UserID"] != null)
                {
                    dgvUsers.Columns["UserID"].HeaderText = "ID";
                    dgvUsers.Columns["UserID"].Width = 50;
                }
                if (dgvUsers.Columns["Username"] != null)
                {
                    dgvUsers.Columns["Username"].HeaderText = "Tên đăng nhập";
                    dgvUsers.Columns["Username"].Width = 120;
                }
                if (dgvUsers.Columns["Role"] != null)
                {
                    dgvUsers.Columns["Role"].HeaderText = "Vai trò";
                    dgvUsers.Columns["Role"].Width = 100;
                }
                if (dgvUsers.Columns["IsActive"] != null)
                {
                    dgvUsers.Columns["IsActive"].HeaderText = "Kích hoạt";
                    dgvUsers.Columns["IsActive"].Width = 80;
                }
                if (dgvUsers.Columns["LastLogin"] != null)
                {
                    dgvUsers.Columns["LastLogin"].HeaderText = "Đăng nhập cuối";
                    dgvUsers.Columns["LastLogin"].Width = 150;
                }
                if (dgvUsers.Columns["EmployeeName"] != null)
                {
                    dgvUsers.Columns["EmployeeName"].HeaderText = "Nhân viên";
                    dgvUsers.Columns["EmployeeName"].Width = 150;
                }
                if (dgvUsers.Columns["Email"] != null)
                {
                    dgvUsers.Columns["Email"].HeaderText = "Email";
                    dgvUsers.Columns["Email"].Width = 180;
                }
                if (dgvUsers.Columns["PhoneNumber"] != null)
                {
                    dgvUsers.Columns["PhoneNumber"].HeaderText = "Điện thoại";
                    dgvUsers.Columns["PhoneNumber"].Width = 100;
                }

                // Ẩn cột EmployeeID
                if (dgvUsers.Columns["EmployeeID"] != null)
                {
                    dgvUsers.Columns["EmployeeID"].Visible = false;
                }

                lblTotalUsers.Text = $"Tổng số: {dtUsers.Rows.Count} người dùng";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi tải dữ liệu: {ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadRoles()
        {
            cboRole.Items.Clear();
            cboRole.Items.Add("Admin");
            cboRole.Items.Add("Manager");
            cboRole.Items.Add("User");
            cboRole.SelectedIndex = 2; // Mặc định User
        }

        private void LoadEmployees()
        {
            try
            {
                string query = @"
                    SELECT EmployeeID, FullName AS EmployeeName
                    FROM Employees 
                    WHERE Status = N'Đang làm việc'
                    AND EmployeeID NOT IN (SELECT EmployeeID FROM Users WHERE EmployeeID IS NOT NULL)
                    ORDER BY FullName";

                using (SqlConnection conn = DatabaseConnection.GetConnection())
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                        {
                            DataTable dt = new DataTable();
                            adapter.Fill(dt);

                            cboEmployee.DataSource = dt;
                            cboEmployee.DisplayMember = "EmployeeName";
                            cboEmployee.ValueMember = "EmployeeID";
                            cboEmployee.SelectedIndex = -1;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi tải danh sách nhân viên: {ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (!ValidateInput()) return;

            try
            {
                // Kiểm tra username đã tồn tại
                string checkQuery = "SELECT COUNT(*) FROM Users WHERE Username = @Username";

                using (SqlConnection conn = DatabaseConnection.GetConnection())
                {
                    conn.Open();

                    // Kiểm tra trùng
                    using (SqlCommand checkCmd = new SqlCommand(checkQuery, conn))
                    {
                        checkCmd.Parameters.AddWithValue("@Username", txtUsername.Text.Trim());
                        int count = (int)checkCmd.ExecuteScalar();

                        if (count > 0)
                        {
                            MessageBox.Show("Tên đăng nhập đã tồn tại!", "Cảnh báo",
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            txtUsername.Focus();
                            return;
                        }
                    }

                    // Mã hóa mật khẩu
                    string hashedPassword = SecurityHelper.HashPassword(txtPassword.Text);

                    // Thêm mới - SỬA TÊN CỘT: Password -> PasswordHash
                    string query = @"
                        INSERT INTO Users (Username, PasswordHash, Role, EmployeeID, IsActive, CreatedDate)
                        VALUES (@Username, @PasswordHash, @Role, @EmployeeID, @IsActive, GETDATE())";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@Username", txtUsername.Text.Trim());
                        cmd.Parameters.AddWithValue("@PasswordHash", hashedPassword);
                        cmd.Parameters.AddWithValue("@Role", cboRole.SelectedItem.ToString());
                        cmd.Parameters.AddWithValue("@EmployeeID",
                            cboEmployee.SelectedValue != null ? cboEmployee.SelectedValue : (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@IsActive", chkIsActive.Checked);

                        int result = cmd.ExecuteNonQuery();

                        if (result > 0)
                        {
                            MessageBox.Show("Thêm người dùng thành công!", "Thông báo",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
                            LoadUsers();
                            LoadEmployees();
                            ClearInputs();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi thêm người dùng: {ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (dgvUsers.CurrentRow == null)
            {
                MessageBox.Show("Vui lòng chọn người dùng cần sửa!", "Cảnh báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!ValidateInput()) return;

            try
            {
                int userId = Convert.ToInt32(dgvUsers.CurrentRow.Cells["UserID"].Value);

                string query = @"
                    UPDATE Users 
                    SET Role = @Role,
                        EmployeeID = @EmployeeID,
                        IsActive = @IsActive
                    WHERE UserID = @UserID";

                using (SqlConnection conn = DatabaseConnection.GetConnection())
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@UserID", userId);
                        cmd.Parameters.AddWithValue("@Role", cboRole.SelectedItem.ToString());
                        cmd.Parameters.AddWithValue("@EmployeeID",
                            cboEmployee.SelectedValue != null ? cboEmployee.SelectedValue : (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@IsActive", chkIsActive.Checked);

                        int result = cmd.ExecuteNonQuery();

                        if (result > 0)
                        {
                            MessageBox.Show("Cập nhật người dùng thành công!", "Thông báo",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
                            LoadUsers();
                            ClearInputs();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi cập nhật: {ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnResetPassword_Click(object sender, EventArgs e)
        {
            if (dgvUsers.CurrentRow == null)
            {
                MessageBox.Show("Vui lòng chọn người dùng!", "Cảnh báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult result = MessageBox.Show(
                "Đặt lại mật khẩu về '123456' cho người dùng này?",
                "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                try
                {
                    int userId = Convert.ToInt32(dgvUsers.CurrentRow.Cells["UserID"].Value);
                    string hashedPassword = SecurityHelper.HashPassword("123456");

                    // SỬA TÊN CỘT: Password -> PasswordHash
                    string query = "UPDATE Users SET PasswordHash = @PasswordHash WHERE UserID = @UserID";

                    using (SqlConnection conn = DatabaseConnection.GetConnection())
                    {
                        conn.Open();
                        using (SqlCommand cmd = new SqlCommand(query, conn))
                        {
                            cmd.Parameters.AddWithValue("@UserID", userId);
                            cmd.Parameters.AddWithValue("@PasswordHash", hashedPassword);

                            int updateResult = cmd.ExecuteNonQuery();

                            if (updateResult > 0)
                            {
                                MessageBox.Show("Đặt lại mật khẩu thành công!\nMật khẩu mới: 123456", "Thông báo",
                                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Lỗi đặt lại mật khẩu: {ex.Message}", "Lỗi",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgvUsers.CurrentRow == null)
            {
                MessageBox.Show("Vui lòng chọn người dùng cần xóa!", "Cảnh báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string username = dgvUsers.CurrentRow.Cells["Username"].Value.ToString();

            if (username.ToLower() == "admin")
            {
                MessageBox.Show("Không thể xóa tài khoản Admin!", "Cảnh báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult result = MessageBox.Show(
                $"Bạn có chắc muốn xóa người dùng '{username}'?",
                "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                try
                {
                    int userId = Convert.ToInt32(dgvUsers.CurrentRow.Cells["UserID"].Value);
                    string query = "DELETE FROM Users WHERE UserID = @UserID";

                    using (SqlConnection conn = DatabaseConnection.GetConnection())
                    {
                        conn.Open();
                        using (SqlCommand cmd = new SqlCommand(query, conn))
                        {
                            cmd.Parameters.AddWithValue("@UserID", userId);
                            int deleteResult = cmd.ExecuteNonQuery();

                            if (deleteResult > 0)
                            {
                                MessageBox.Show("Xóa người dùng thành công!", "Thông báo",
                                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                                LoadUsers();
                                LoadEmployees();
                                ClearInputs();
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Lỗi xóa người dùng: {ex.Message}", "Lỗi",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void dgvUsers_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvUsers.Rows[e.RowIndex];
                txtUsername.Text = row.Cells["Username"].Value.ToString();
                cboRole.SelectedItem = row.Cells["Role"].Value.ToString();
                chkIsActive.Checked = Convert.ToBoolean(row.Cells["IsActive"].Value);

                LoadEmployeesForEdit(row.Cells["EmployeeName"].Value?.ToString());

                txtUsername.Enabled = false;
                txtPassword.Enabled = false;
                btnAdd.Enabled = false;
                btnEdit.Enabled = true;
                btnResetPassword.Enabled = true;
            }
        }

        private void LoadEmployeesForEdit(string currentEmployeeName)
        {
            try
            {
                string query = @"
                    SELECT EmployeeID, FullName AS EmployeeName
                    FROM Employees 
                    WHERE Status = N'Đang làm việc'
                    ORDER BY FullName";

                using (SqlConnection conn = DatabaseConnection.GetConnection())
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                        {
                            DataTable dt = new DataTable();
                            adapter.Fill(dt);

                            cboEmployee.DataSource = dt;
                            cboEmployee.DisplayMember = "EmployeeName";
                            cboEmployee.ValueMember = "EmployeeID";

                            if (!string.IsNullOrEmpty(currentEmployeeName))
                            {
                                cboEmployee.Text = currentEmployeeName;
                            }
                            else
                            {
                                cboEmployee.SelectedIndex = -1;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi tải danh sách nhân viên: {ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearInputs();
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            if (dtUsers == null) return;

            string searchText = txtSearch.Text.Trim();
            if (string.IsNullOrEmpty(searchText))
            {
                dgvUsers.DataSource = dtUsers;
            }
            else
            {
                DataView dv = dtUsers.DefaultView;
                dv.RowFilter = $"Username LIKE '%{searchText}%' OR Role LIKE '%{searchText}%' OR EmployeeName LIKE '%{searchText}%'";
                dgvUsers.DataSource = dv.ToTable();
            }

            lblTotalUsers.Text = $"Tổng số: {dgvUsers.Rows.Count} người dùng";
        }

        private bool ValidateInput()
        {
            if (string.IsNullOrWhiteSpace(txtUsername.Text))
            {
                MessageBox.Show("Vui lòng nhập tên đăng nhập!", "Cảnh báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtUsername.Focus();
                return false;
            }

            if (btnAdd.Enabled && string.IsNullOrWhiteSpace(txtPassword.Text))
            {
                MessageBox.Show("Vui lòng nhập mật khẩu!", "Cảnh báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtPassword.Focus();
                return false;
            }

            if (cboRole.SelectedIndex == -1)
            {
                MessageBox.Show("Vui lòng chọn vai trò!", "Cảnh báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cboRole.Focus();
                return false;
            }

            return true;
        }

        private void ClearInputs()
        {
            txtUsername.Clear();
            txtPassword.Clear();
            cboRole.SelectedIndex = 2;
            cboEmployee.SelectedIndex = -1;
            chkIsActive.Checked = true;
            txtSearch.Clear();

            txtUsername.Enabled = true;
            txtPassword.Enabled = true;
            btnAdd.Enabled = true;
            btnEdit.Enabled = false;
            btnResetPassword.Enabled = false;

            LoadEmployees();
            txtUsername.Focus();
        }
    }
}
