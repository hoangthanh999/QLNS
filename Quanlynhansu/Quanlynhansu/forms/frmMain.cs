using System;
using System.Drawing;
using System.Windows.Forms;
using Quanlynhansu.Models;

namespace Quanlynhansu.Forms
{
    public partial class frmMain : Form
    {
        private User currentUser;

        public frmMain(User user)
        {
            InitializeComponent();
            this.currentUser = user;

            // ✅ THÊM: Thiết lập ảnh nền
            SetBackgroundImage();
        }

        // ✅ THÊM: Phương thức thiết lập ảnh nền linh hoạt
        private void SetBackgroundImage()
        {
            string imagePath = System.IO.Path.Combine(
                Application.StartupPath,
                "Resources",
                "background.jpg"
            );

            // ✅ THÊM: Hiển thị đường dẫn để kiểm tra
            MessageBox.Show($"Đang tìm ảnh tại:\n{imagePath}\n\nTồn tại: {System.IO.File.Exists(imagePath)}");

            if (System.IO.File.Exists(imagePath))
            {
                this.BackgroundImage = Image.FromFile(imagePath);
                this.BackgroundImageLayout = ImageLayout.Stretch;
            }
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            lblWelcome.Text = $"Xin chào, {currentUser.FullName} ({currentUser.Role})";
            lblDateTime.Text = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");

            // Phân quyền menu
            if (currentUser.Role != "Admin")
            {
                menuAdmin.Visible = false;
            }
        }

        // ========== MENU HỆ THỐNG ==========
        private void menuLogout_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Bạn có chắc muốn đăng xuất?",
                "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                // Đóng tất cả form con trước
                foreach (Form childForm in this.MdiChildren)
                {
                    childForm.Close();
                }

                // Hiển thị lại form login
                frmLogin loginForm = new frmLogin();
                loginForm.Show();

                // Đóng form main
                this.Close();
            }
        }

        private void menuExit_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Bạn có chắc muốn thoát ứng dụng?",
                "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        // ========== MENU QUẢN LÝ ==========
        private void menuEmployeeList_Click(object sender, EventArgs e)
        {
            OpenForm<frmEmployeeList>();
        }

        private void menuDepartment_Click(object sender, EventArgs e)
        {
            OpenForm<frmDepartmentManagement>();
        }

        private void menuPosition_Click(object sender, EventArgs e)
        {
            OpenForm<frmPositionManagement>();
        }

        // ========== MENU CHẤM CÔNG ==========
        private void menuAttendance_Click(object sender, EventArgs e)
        {
            OpenForm<frmAttendance>();
        }

        private void menuLeave_Click(object sender, EventArgs e)
        {
            OpenFormWithUser<frmLeaveManagement>();
        }

        // ========== MENU LƯƠNG ==========
        private void menuSalary_Click(object sender, EventArgs e)
        {
            OpenFormWithUser<frmSalaryManagement>();
        }

        private void menuCalculateSalary_Click(object sender, EventArgs e)
        {
            OpenFormWithUser<frmCalculateSalary>();
        }

        // ========== MENU BÁO CÁO ==========
        private void menuReportEmployee_Click(object sender, EventArgs e)
        {
            OpenForm<frmEmployeeReport>();
        }

        private void menuReportSalary_Click(object sender, EventArgs e)
        {
            OpenForm<frmSalaryReport>();
        }

        // ========== MENU ADMIN ==========
        private void menuUserManagement_Click(object sender, EventArgs e)
        {
            OpenForm<frmUserManagement>();
        }

        // ========== HELPER METHODS ==========
        private void OpenForm<T>() where T : Form, new()
        {
            foreach (Form child in this.MdiChildren)
            {
                if (child is T)
                {
                    child.Activate();
                    return;
                }
            }

            T frm = new T();
            frm.MdiParent = this;
            frm.Show();
        }

        private void OpenFormWithUser<T>() where T : Form
        {
            foreach (Form child in this.MdiChildren)
            {
                if (child is T)
                {
                    child.Activate();
                    return;
                }
            }

            T frm = (T)Activator.CreateInstance(typeof(T), currentUser);
            frm.MdiParent = this;
            frm.Show();
        }

        private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                DialogResult result = MessageBox.Show("Bạn có chắc muốn thoát ứng dụng?",
                    "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.No)
                {
                    e.Cancel = true;
                }
                else
                {
                    Application.Exit();
                }
            }
        }
    }
}
