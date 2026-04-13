using System;
using System.Data;
using System.Windows.Forms;
using Quanlynhansu.BLL;

namespace Quanlynhansu.Forms
{
    public partial class frmAttendance : Form
    {
        public frmAttendance()
        {
            InitializeComponent();
        }

        private void frmAttendance_Load(object sender, EventArgs e)
        {
            dtpFromDate.Value = DateTime.Now.AddDays(-7);
            dtpToDate.Value = DateTime.Now;
            LoadEmployees();
            LoadAttendance();
        }

        private void LoadEmployees()
        {
            try
            {
                DataTable dt = EmployeeBLL.SearchEmployees(null, null, "Đang làm việc", 1, 1000);

                cboEmployee.DataSource = dt;
                cboEmployee.DisplayMember = "FullName";
                cboEmployee.ValueMember = "EmployeeID";
                cboEmployee.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi load nhân viên: " + ex.Message);
            }
        }

        private void LoadAttendance()
        {
            try
            {
                long? employeeID = cboEmployee.SelectedValue != null ?
                    (long?)Convert.ToInt64(cboEmployee.SelectedValue) : null;

                DataTable dt = AttendanceBLL.GetAttendanceByDateRange(
                    dtpFromDate.Value, dtpToDate.Value, employeeID);

                dgvAttendance.DataSource = dt;
                FormatDataGridView();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }
        }

        private void FormatDataGridView()
        {
            if (dgvAttendance.Columns.Count > 0)
            {
                dgvAttendance.Columns["AttendanceID"].Visible = false;
                dgvAttendance.Columns["EmployeeID"].Visible = false;
                dgvAttendance.Columns["EmployeeCode"].HeaderText = "Mã NV";
                dgvAttendance.Columns["FullName"].HeaderText = "Họ tên";
                dgvAttendance.Columns["AttendanceDate"].HeaderText = "Ngày";
                dgvAttendance.Columns["AttendanceDate"].DefaultCellStyle.Format = "dd/MM/yyyy";
                dgvAttendance.Columns["CheckInTime"].HeaderText = "Giờ vào";
                dgvAttendance.Columns["CheckInTime"].DefaultCellStyle.Format = "HH:mm:ss";
                dgvAttendance.Columns["CheckOutTime"].HeaderText = "Giờ ra";
                dgvAttendance.Columns["CheckOutTime"].DefaultCellStyle.Format = "HH:mm:ss";
                dgvAttendance.Columns["WorkHours"].HeaderText = "Giờ công";
                dgvAttendance.Columns["Status"].HeaderText = "Trạng thái";

                dgvAttendance.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            LoadAttendance();
        }

        private void btnCheckIn_Click(object sender, EventArgs e)
        {
            if (cboEmployee.SelectedValue == null)
            {
                MessageBox.Show("Vui lòng chọn nhân viên!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                long employeeID = Convert.ToInt64(cboEmployee.SelectedValue);
                if (AttendanceBLL.CheckIn(employeeID, DateTime.Now))
                {
                    MessageBox.Show("Chấm công vào thành công!", "Thành công",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadAttendance();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCheckOut_Click(object sender, EventArgs e)
        {
            if (cboEmployee.SelectedValue == null)
            {
                MessageBox.Show("Vui lòng chọn nhân viên!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                long employeeID = Convert.ToInt64(cboEmployee.SelectedValue);
                if (AttendanceBLL.CheckOut(employeeID, DateTime.Now))
                {
                    MessageBox.Show("Chấm công ra thành công!", "Thành công",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadAttendance();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
