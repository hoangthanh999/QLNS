using System;
using System.Windows.Forms;
using Quanlynhansu.DAL;
using Quanlynhansu.BLL;
using Quanlynhansu.Models;

namespace Quanlynhansu.Forms
{
    public partial class frmLogin : Form
    {
        public frmLogin()
        {
            InitializeComponent();

            // Cấu hình form
            this.KeyPreview = true;
            // ❌ BỎ DÒNG NÀY: this.AcceptButton = btnLogin;
            this.CancelButton = btnExit;   // ESC = Thoát

            // ✅ Event cho Enter ở txtUsername (nhảy xuống txtPassword)
            txtUsername.KeyDown += txtUsername_KeyDown;

            // ✅ Event cho Enter ở txtPassword (thực hiện đăng nhập)
            txtPassword.KeyDown += txtPassword_KeyDown;
        }

        private void frmLogin_Load(object sender, EventArgs e)
        {
            try
            {
                // Focus vào txtUsername khi form load
                txtUsername.Focus();

                // Test kết nối database
                if (DatabaseConnection.TestConnection())
                {
                    // Kết nối thành công
                }
                else
                {
                    MessageBox.Show(
                        "Không thể kết nối đến database!\n\n" +
                        "Vui lòng kiểm tra:\n" +
                        "1. SQL Server đã chạy chưa?\n" +
                        "2. Database HRM_System đã tạo chưa?\n" +
                        "3. Connection string trong App.config đúng chưa?\n" +
                        "4. Username/Password đúng chưa?",
                        "Lỗi kết nối",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error
                    );
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi kiểm tra kết nối: " + ex.Message,
                    "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ✅ Sự kiện Enter ở txtUsername → Nhảy xuống txtPassword
        private void txtUsername_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtPassword.Focus();
                e.Handled = true;
                e.SuppressKeyPress = true; // Tắt tiếng "beep"
            }
        }

        // ✅ Sự kiện Enter ở txtPassword → Thực hiện đăng nhập
        private void txtPassword_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnLogin_Click(sender, e);
                e.Handled = true;
                e.SuppressKeyPress = true; // Tắt tiếng "beep"
            }
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                string username = txtUsername.Text.Trim();
                string password = txtPassword.Text.Trim();

                // Validate input
                if (string.IsNullOrEmpty(username))
                {
                    MessageBox.Show("Vui lòng nhập tên đăng nhập!", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtUsername.Focus();
                    return;
                }

                if (string.IsNullOrEmpty(password))
                {
                    MessageBox.Show("Vui lòng nhập mật khẩu!", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtPassword.Focus();
                    return;
                }

                // Hiển thị loading
                this.Cursor = Cursors.WaitCursor;
                btnLogin.Enabled = false;

                // Thực hiện đăng nhập
                var user = UserBLL.Login(username, password);

                if (user != null)
                {
                    // Đăng nhập thành công
                    MessageBox.Show($"Chào mừng {user.FullName}!", "Thành công",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);

                    frmMain mainForm = new frmMain(user);
                    mainForm.FormClosed += (s, args) => this.Show();
                    mainForm.Show();
                    this.Hide();

                    // Clear password
                    txtPassword.Clear();
                }
                else
                {
                    // Đăng nhập thất bại
                    MessageBox.Show("Tên đăng nhập hoặc mật khẩu không đúng!", "Lỗi",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtPassword.Clear();
                    txtPassword.Focus();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi đăng nhập: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                // Khôi phục trạng thái
                this.Cursor = Cursors.Default;
                btnLogin.Enabled = true;
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show(
                "Bạn có chắc muốn thoát ứng dụng?",
                "Xác nhận",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            if (result == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void chkShowPassword_CheckedChanged(object sender, EventArgs e)
        {
            txtPassword.PasswordChar = chkShowPassword.Checked ? '\0' : '●';
        }
    }
}
