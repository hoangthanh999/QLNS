using System;
using System.Drawing;
using System.Windows.Forms;

namespace Quanlynhansu.Forms
{
    partial class frmMain
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem menuHeThong;
        private System.Windows.Forms.ToolStripMenuItem menuLogout;
        private System.Windows.Forms.ToolStripMenuItem menuExit;
        private System.Windows.Forms.ToolStripMenuItem menuQuanLy;
        private System.Windows.Forms.ToolStripMenuItem menuEmployeeList;
        private System.Windows.Forms.ToolStripMenuItem menuDepartment;
        private System.Windows.Forms.ToolStripMenuItem menuPosition;
        private System.Windows.Forms.ToolStripMenuItem menuChamCong;
        private System.Windows.Forms.ToolStripMenuItem menuAttendance;
        private System.Windows.Forms.ToolStripMenuItem menuLeave;
        private System.Windows.Forms.ToolStripMenuItem menuLuong;
        private System.Windows.Forms.ToolStripMenuItem menuSalary;
        private System.Windows.Forms.ToolStripMenuItem menuBaoCao;
        private System.Windows.Forms.ToolStripMenuItem menuReportEmployee;
        private System.Windows.Forms.ToolStripMenuItem menuReportSalary;
        private System.Windows.Forms.ToolStripMenuItem menuAdmin;
        private System.Windows.Forms.ToolStripMenuItem menuUserManagement;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel lblWelcome;
        private System.Windows.Forms.ToolStripStatusLabel lblDateTime;
        private System.Windows.Forms.Timer timer1;

        // ✅ THÊM: Panel thông tin hiện đại (THU GỌN)
        private System.Windows.Forms.Panel panelTop;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.PictureBox picLogo;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.menuHeThong = new System.Windows.Forms.ToolStripMenuItem();
            this.menuLogout = new System.Windows.Forms.ToolStripMenuItem();
            this.menuExit = new System.Windows.Forms.ToolStripMenuItem();
            this.menuQuanLy = new System.Windows.Forms.ToolStripMenuItem();
            this.menuEmployeeList = new System.Windows.Forms.ToolStripMenuItem();
            this.menuDepartment = new System.Windows.Forms.ToolStripMenuItem();
            this.menuPosition = new System.Windows.Forms.ToolStripMenuItem();
            this.menuChamCong = new System.Windows.Forms.ToolStripMenuItem();
            this.menuAttendance = new System.Windows.Forms.ToolStripMenuItem();
            this.menuLeave = new System.Windows.Forms.ToolStripMenuItem();
            this.menuLuong = new System.Windows.Forms.ToolStripMenuItem();
            this.menuSalary = new System.Windows.Forms.ToolStripMenuItem();
            this.menuBaoCao = new System.Windows.Forms.ToolStripMenuItem();
            this.menuReportEmployee = new System.Windows.Forms.ToolStripMenuItem();
            this.menuReportSalary = new System.Windows.Forms.ToolStripMenuItem();
            this.menuAdmin = new System.Windows.Forms.ToolStripMenuItem();
            this.menuUserManagement = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.lblWelcome = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblDateTime = new System.Windows.Forms.ToolStripStatusLabel();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.panelTop = new System.Windows.Forms.Panel();
            this.lblTitle = new System.Windows.Forms.Label();
            this.picLogo = new System.Windows.Forms.PictureBox();

            this.menuStrip1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.panelTop.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picLogo)).BeginInit();
            this.SuspendLayout();

            // 
            // menuStrip1 - ✅ NÂNG CẤP MENU
            // 
            this.menuStrip1.BackColor = System.Drawing.Color.FromArgb(41, 128, 185);
            this.menuStrip1.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
                this.menuHeThong,
                this.menuQuanLy,
                this.menuChamCong,
                this.menuLuong,
                this.menuBaoCao,
                this.menuAdmin});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(8, 4, 0, 4);
            this.menuStrip1.Size = new System.Drawing.Size(1200, 35);
            this.menuStrip1.TabIndex = 1;

            // ✅ STYLE CHO TẤT CẢ MENU ITEMS
            foreach (ToolStripMenuItem item in this.menuStrip1.Items)
            {
                item.ForeColor = Color.White;
                item.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
                item.Padding = new Padding(12, 4, 12, 4);
            }

            // 
            // menuHeThong
            // 
            this.menuHeThong.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
                this.menuLogout,
                this.menuExit});
            this.menuHeThong.Name = "menuHeThong";
            this.menuHeThong.Size = new System.Drawing.Size(95, 27);
            this.menuHeThong.Text = "🏠 Hệ thống";

            // 
            // menuLogout
            // 
            this.menuLogout.Name = "menuLogout";
            this.menuLogout.Size = new System.Drawing.Size(200, 26);
            this.menuLogout.Text = "🚪 Đăng xuất";
            this.menuLogout.Click += new System.EventHandler(this.menuLogout_Click);

            // 
            // menuExit
            // 
            this.menuExit.Name = "menuExit";
            this.menuExit.Size = new System.Drawing.Size(200, 26);
            this.menuExit.Text = "❌ Thoát";
            this.menuExit.Click += new System.EventHandler(this.menuExit_Click);

            // 
            // menuQuanLy
            // 
            this.menuQuanLy.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
                this.menuEmployeeList,
                this.menuDepartment,
                this.menuPosition});
            this.menuQuanLy.Name = "menuQuanLy";
            this.menuQuanLy.Size = new System.Drawing.Size(90, 27);
            this.menuQuanLy.Text = "👥 Quản lý";

            // 
            // menuEmployeeList
            // 
            this.menuEmployeeList.Name = "menuEmployeeList";
            this.menuEmployeeList.Size = new System.Drawing.Size(220, 26);
            this.menuEmployeeList.Text = "👤 Quản lý nhân viên";
            this.menuEmployeeList.Click += new System.EventHandler(this.menuEmployeeList_Click);

            // 
            // menuDepartment
            // 
            this.menuDepartment.Name = "menuDepartment";
            this.menuDepartment.Size = new System.Drawing.Size(220, 26);
            this.menuDepartment.Text = "🏢 Quản lý phòng ban";
            this.menuDepartment.Click += new System.EventHandler(this.menuDepartment_Click);

            // 
            // menuPosition
            // 
            this.menuPosition.Name = "menuPosition";
            this.menuPosition.Size = new System.Drawing.Size(220, 26);
            this.menuPosition.Text = "💼 Quản lý chức vụ";
            this.menuPosition.Click += new System.EventHandler(this.menuPosition_Click);

            // 
            // menuChamCong
            // 
            this.menuChamCong.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
                this.menuAttendance,
                this.menuLeave});
            this.menuChamCong.Name = "menuChamCong";
            this.menuChamCong.Size = new System.Drawing.Size(115, 27);
            this.menuChamCong.Text = "⏰ Chấm công";

            // 
            // menuAttendance
            // 
            this.menuAttendance.Name = "menuAttendance";
            this.menuAttendance.Size = new System.Drawing.Size(220, 26);
            this.menuAttendance.Text = "✅ Chấm công";
            this.menuAttendance.Click += new System.EventHandler(this.menuAttendance_Click);

            // 
            // menuLeave
            // 
            this.menuLeave.Name = "menuLeave";
            this.menuLeave.Size = new System.Drawing.Size(220, 26);
            this.menuLeave.Text = "🏖️ Quản lý nghỉ phép";
            this.menuLeave.Click += new System.EventHandler(this.menuLeave_Click);

            // 
            // menuLuong
            // 
            this.menuLuong.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
                this.menuSalary});
            this.menuLuong.Name = "menuLuong";
            this.menuLuong.Size = new System.Drawing.Size(80, 27);
            this.menuLuong.Text = "💰 Lương";

            // 
            // menuSalary
            // 
            this.menuSalary.Name = "menuSalary";
            this.menuSalary.Size = new System.Drawing.Size(200, 26);
            this.menuSalary.Text = "💵 Quản lý lương";
            this.menuSalary.Click += new System.EventHandler(this.menuSalary_Click);

            // 
            // menuBaoCao
            // 
            this.menuBaoCao.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
                this.menuReportEmployee,
                this.menuReportSalary});
            this.menuBaoCao.Name = "menuBaoCao";
            this.menuBaoCao.Size = new System.Drawing.Size(95, 27);
            this.menuBaoCao.Text = "📊 Báo cáo";

            // 
            // menuReportEmployee
            // 
            this.menuReportEmployee.Name = "menuReportEmployee";
            this.menuReportEmployee.Size = new System.Drawing.Size(220, 26);
            this.menuReportEmployee.Text = "📈 Báo cáo nhân sự";
            this.menuReportEmployee.Click += new System.EventHandler(this.menuReportEmployee_Click);

            // 
            // menuReportSalary
            // 
            this.menuReportSalary.Name = "menuReportSalary";
            this.menuReportSalary.Size = new System.Drawing.Size(220, 26);
            this.menuReportSalary.Text = "💹 Báo cáo lương";
            this.menuReportSalary.Click += new System.EventHandler(this.menuReportSalary_Click);

            // 
            // menuAdmin
            // 
            this.menuAdmin.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
                this.menuUserManagement});
            this.menuAdmin.Name = "menuAdmin";
            this.menuAdmin.Size = new System.Drawing.Size(85, 27);
            this.menuAdmin.Text = "⚙️ Admin";

            // 
            // menuUserManagement
            // 
            this.menuUserManagement.Name = "menuUserManagement";
            this.menuUserManagement.Size = new System.Drawing.Size(240, 26);
            this.menuUserManagement.Text = "👨‍💼 Quản lý người dùng";
            this.menuUserManagement.Click += new System.EventHandler(this.menuUserManagement_Click);

            // 
            // panelTop - ✅ THU GỌN CHỈ CÒN 50PX
            // 
            this.panelTop.BackColor = System.Drawing.Color.FromArgb(52, 152, 219);
            this.panelTop.Controls.Add(this.lblTitle);
            this.panelTop.Controls.Add(this.picLogo);
            this.panelTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelTop.Location = new System.Drawing.Point(0, 35);
            this.panelTop.Name = "panelTop";
            this.panelTop.Size = new System.Drawing.Size(1200, 50); // ✅ GIẢM TỪ 80 XUỐNG 50
            this.panelTop.TabIndex = 4;

            // 
            // picLogo
            // 
            this.picLogo.BackColor = System.Drawing.Color.White;
            this.picLogo.Location = new System.Drawing.Point(15, 8);
            this.picLogo.Name = "picLogo";
            this.picLogo.Size = new System.Drawing.Size(35, 35); // ✅ GIẢM SIZE
            this.picLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picLogo.TabIndex = 0;
            this.picLogo.TabStop = false;
            // Tạo logo đơn giản
            Bitmap logo = new Bitmap(35, 35);
            using (Graphics g = Graphics.FromImage(logo))
            {
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                g.FillEllipse(new SolidBrush(Color.FromArgb(41, 128, 185)), 2, 2, 31, 31);
                g.DrawString("HR", new Font("Arial", 12, FontStyle.Bold), Brushes.White, 6, 8);
            }
            this.picLogo.Image = logo;

            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.White;
            this.lblTitle.Location = new System.Drawing.Point(60, 12);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(290, 25);
            this.lblTitle.TabIndex = 1;
            this.lblTitle.Text = "HỆ THỐNG QUẢN LÝ NHÂN SỰ";

            // 
            // statusStrip1 - ✅ NÂNG CẤP STATUS BAR
            // 
            this.statusStrip1.BackColor = System.Drawing.Color.FromArgb(44, 62, 80);
            this.statusStrip1.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
                this.lblWelcome,
                this.lblDateTime});
            this.statusStrip1.Location = new System.Drawing.Point(0, 678);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Padding = new System.Windows.Forms.Padding(1, 0, 16, 0);
            this.statusStrip1.Size = new System.Drawing.Size(1200, 22);
            this.statusStrip1.TabIndex = 3;

            // 
            // lblWelcome
            // 
            this.lblWelcome.ForeColor = System.Drawing.Color.White;
            this.lblWelcome.Name = "lblWelcome";
            this.lblWelcome.Size = new System.Drawing.Size(70, 17);
            this.lblWelcome.Text = "👤 Xin chào,";

            // 
            // lblDateTime
            // 
            this.lblDateTime.ForeColor = System.Drawing.Color.White;
            this.lblDateTime.Name = "lblDateTime";
            this.lblDateTime.Size = new System.Drawing.Size(1113, 17);
            this.lblDateTime.Spring = true;
            this.lblDateTime.Text = "🕐 DateTime";
            this.lblDateTime.TextAlign = System.Drawing.ContentAlignment.MiddleRight;

            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 1000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);

            // 
            // frmMain - ✅ FORM CHÍNH
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(236, 240, 241);
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(1200, 700);
            this.Controls.Add(this.panelTop);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.menuStrip1);
            this.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.IsMdiContainer = true;
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "frmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Hệ thống quản lý nhân sự";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.frmMain_Load);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmMain_FormClosing);

            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.panelTop.ResumeLayout(false);
            this.panelTop.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picLogo)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private void timer1_Tick(object sender, System.EventArgs e)
        {
            lblDateTime.Text = "🕐 " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
        }
    }
}
