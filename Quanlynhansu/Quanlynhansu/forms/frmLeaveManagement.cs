using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using Quanlynhansu.BLL;
using Quanlynhansu.Models;

namespace Quanlynhansu.Forms
{
    public partial class frmLeaveManagement : Form
    {
        private User currentUser;

        public frmLeaveManagement()
        {
            InitializeComponent();
        }

        public frmLeaveManagement(User user) : this()
        {
            this.currentUser = user;
        }

        private void frmLeaveManagement_Load(object sender, EventArgs e)
        {
            try
            {
                // Load combo box
                LoadLeaveTypes();
                LoadStatuses();

                // Set ngày mặc định (tháng hiện tại)
                dtpFromDate.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
                dtpToDate.Value = DateTime.Now;

                // Load dữ liệu
                LoadData();

                // Phân quyền nút
                SetPermissions();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải dữ liệu: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadLeaveTypes()
        {
            cboLeaveType.Items.Clear();
            cboLeaveType.Items.Add("-- Tất cả --");
            cboLeaveType.Items.AddRange(LeaveTypes.GetAll());
            cboLeaveType.SelectedIndex = 0;
        }

        private void LoadStatuses()
        {
            cboStatus.Items.Clear();
            cboStatus.Items.Add("-- Tất cả --");
            cboStatus.Items.AddRange(LeaveStatus.GetAll());
            cboStatus.SelectedIndex = 0;
        }

        private void LoadData()
        {
            try
            {
                DataTable dt = LeaveRequestBLL.GetAllLeaveRequests();
                dgvLeaveRequests.DataSource = dt;

                FormatDataGridView();
                UpdateStatistics(dt);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải dữ liệu: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void FormatDataGridView()
        {
            if (dgvLeaveRequests.Columns.Count == 0) return;

            // Ẩn các cột không cần thiết
            dgvLeaveRequests.Columns["LeaveRequestID"].Visible = false;
            dgvLeaveRequests.Columns["EmployeeID"].Visible = false;
            dgvLeaveRequests.Columns["ApprovedBy"].Visible = false;

            // Đặt tiêu đề
            dgvLeaveRequests.Columns["EmployeeCode"].HeaderText = "Mã NV";
            dgvLeaveRequests.Columns["EmployeeName"].HeaderText = "Họ tên";
            dgvLeaveRequests.Columns["DepartmentName"].HeaderText = "Phòng ban";
            dgvLeaveRequests.Columns["PositionName"].HeaderText = "Chức vụ";
            dgvLeaveRequests.Columns["LeaveType"].HeaderText = "Loại nghỉ";
            dgvLeaveRequests.Columns["FromDate"].HeaderText = "Từ ngày";
            dgvLeaveRequests.Columns["ToDate"].HeaderText = "Đến ngày";
            dgvLeaveRequests.Columns["TotalDays"].HeaderText = "Số ngày";
            dgvLeaveRequests.Columns["Reason"].HeaderText = "Lý do";
            dgvLeaveRequests.Columns["Status"].HeaderText = "Trạng thái";
            dgvLeaveRequests.Columns["ApprovedByName"].HeaderText = "Người duyệt";
            dgvLeaveRequests.Columns["ApprovedDate"].HeaderText = "Ngày duyệt";
            dgvLeaveRequests.Columns["CreatedDate"].HeaderText = "Ngày tạo";

            // Định dạng cột
            dgvLeaveRequests.Columns["EmployeeCode"].Width = 80;
            dgvLeaveRequests.Columns["EmployeeName"].Width = 150;
            dgvLeaveRequests.Columns["DepartmentName"].Width = 120;
            dgvLeaveRequests.Columns["PositionName"].Width = 120;
            dgvLeaveRequests.Columns["LeaveType"].Width = 120;
            dgvLeaveRequests.Columns["FromDate"].Width = 100;
            dgvLeaveRequests.Columns["ToDate"].Width = 100;
            dgvLeaveRequests.Columns["TotalDays"].Width = 70;
            dgvLeaveRequests.Columns["Reason"].Width = 200;
            dgvLeaveRequests.Columns["Status"].Width = 100;
            dgvLeaveRequests.Columns["ApprovedByName"].Width = 120;
            dgvLeaveRequests.Columns["ApprovedDate"].Width = 120;
            dgvLeaveRequests.Columns["CreatedDate"].Width = 120;

            // Định dạng ngày tháng
            dgvLeaveRequests.Columns["FromDate"].DefaultCellStyle.Format = "dd/MM/yyyy";
            dgvLeaveRequests.Columns["ToDate"].DefaultCellStyle.Format = "dd/MM/yyyy";
            dgvLeaveRequests.Columns["ApprovedDate"].DefaultCellStyle.Format = "dd/MM/yyyy HH:mm";
            dgvLeaveRequests.Columns["CreatedDate"].DefaultCellStyle.Format = "dd/MM/yyyy HH:mm";

            // Căn giữa
            dgvLeaveRequests.Columns["EmployeeCode"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvLeaveRequests.Columns["FromDate"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvLeaveRequests.Columns["ToDate"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvLeaveRequests.Columns["TotalDays"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvLeaveRequests.Columns["Status"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            // Màu sắc cho trạng thái
            foreach (DataGridViewRow row in dgvLeaveRequests.Rows)
            {
                string status = row.Cells["Status"].Value?.ToString();
                switch (status)
                {
                    case "Chờ duyệt":
                        row.Cells["Status"].Style.BackColor = Color.LightYellow;
                        row.Cells["Status"].Style.ForeColor = Color.DarkOrange;
                        break;
                    case "Đã duyệt":
                        row.Cells["Status"].Style.BackColor = Color.LightGreen;
                        row.Cells["Status"].Style.ForeColor = Color.DarkGreen;
                        break;
                    case "Từ chối":
                        row.Cells["Status"].Style.BackColor = Color.LightCoral;
                        row.Cells["Status"].Style.ForeColor = Color.DarkRed;
                        break;
                    case "Đã hủy":
                        row.Cells["Status"].Style.BackColor = Color.LightGray;
                        row.Cells["Status"].Style.ForeColor = Color.Gray;
                        break;
                }
            }
        }

        private void UpdateStatistics(DataTable dt)
        {
            int total = dt.Rows.Count;
            int pending = dt.Select("Status = 'Chờ duyệt'").Length;
            int approved = dt.Select("Status = 'Đã duyệt'").Length;
            int rejected = dt.Select("Status = 'Từ chối'").Length;

            lblTotal.Text = $"Tổng số: {total}";
            lblPending.Text = $"Chờ duyệt: {pending}";
            lblApproved.Text = $"Đã duyệt: {approved}";
            lblRejected.Text = $"Từ chối: {rejected}";
        }

        private void SetPermissions()
        {
            // Admin có full quyền
            if (currentUser?.Role == "Admin")
            {
                btnApprove.Enabled = true;
                btnReject.Enabled = true;
                btnDelete.Enabled = true;
            }
            else
            {
                // User thường chỉ xem và tạo đơn
                btnApprove.Enabled = false;
                btnReject.Enabled = false;
                btnDelete.Enabled = false;
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                string searchText = txtSearch.Text.Trim();
                string status = cboStatus.SelectedIndex > 0 ? cboStatus.SelectedItem.ToString() : null;
                string leaveType = cboLeaveType.SelectedIndex > 0 ? cboLeaveType.SelectedItem.ToString() : null;
                DateTime? fromDate = dtpFromDate.Checked ? dtpFromDate.Value : (DateTime?)null;
                DateTime? toDate = dtpToDate.Checked ? dtpToDate.Value : (DateTime?)null;

                DataTable dt = LeaveRequestBLL.SearchLeaveRequests(searchText, status, fromDate, toDate, leaveType);
                dgvLeaveRequests.DataSource = dt;

                FormatDataGridView();
                UpdateStatistics(dt);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tìm kiếm: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            txtSearch.Clear();
            cboLeaveType.SelectedIndex = 0;
            cboStatus.SelectedIndex = 0;
            dtpFromDate.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            dtpToDate.Value = DateTime.Now;
            LoadData();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            frmLeaveRequestDetail frm = new frmLeaveRequestDetail(currentUser);
            if (frm.ShowDialog() == DialogResult.OK)
            {
                LoadData();
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (dgvLeaveRequests.CurrentRow == null)
            {
                MessageBox.Show("Vui lòng chọn đơn nghỉ phép cần sửa!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            long leaveRequestID = Convert.ToInt64(dgvLeaveRequests.CurrentRow.Cells["LeaveRequestID"].Value);
            string status = dgvLeaveRequests.CurrentRow.Cells["Status"].Value.ToString();

            if (status != "Chờ duyệt")
            {
                MessageBox.Show("Chỉ có thể sửa đơn đang chờ duyệt!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            frmLeaveRequestDetail frm = new frmLeaveRequestDetail(leaveRequestID, currentUser);
            if (frm.ShowDialog() == DialogResult.OK)
            {
                LoadData();
            }
        }

        private void btnApprove_Click(object sender, EventArgs e)
        {
            if (dgvLeaveRequests.CurrentRow == null)
            {
                MessageBox.Show("Vui lòng chọn đơn nghỉ phép cần duyệt!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            long leaveRequestID = Convert.ToInt64(dgvLeaveRequests.CurrentRow.Cells["LeaveRequestID"].Value);
            string employeeName = dgvLeaveRequests.CurrentRow.Cells["EmployeeName"].Value.ToString();
            string status = dgvLeaveRequests.CurrentRow.Cells["Status"].Value.ToString();

            if (status != "Chờ duyệt")
            {
                MessageBox.Show("Đơn này đã được xử lý trước đó!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult result = MessageBox.Show($"Bạn có chắc muốn DUYỆT đơn nghỉ phép của {employeeName}?",
                "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                try
                {
                    // Giả sử currentUser.EmployeeID là ID người duyệt
                    long approverID = 1; // Thay bằng currentUser.EmployeeID

                    if (LeaveRequestBLL.ApproveLeaveRequest(leaveRequestID, approverID))
                    {
                        MessageBox.Show("Duyệt đơn nghỉ phép thành công!", "Thông báo",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadData();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi: " + ex.Message, "Lỗi",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnReject_Click(object sender, EventArgs e)
        {
            if (dgvLeaveRequests.CurrentRow == null)
            {
                MessageBox.Show("Vui lòng chọn đơn nghỉ phép cần từ chối!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            long leaveRequestID = Convert.ToInt64(dgvLeaveRequests.CurrentRow.Cells["LeaveRequestID"].Value);
            string employeeName = dgvLeaveRequests.CurrentRow.Cells["EmployeeName"].Value.ToString();
            string status = dgvLeaveRequests.CurrentRow.Cells["Status"].Value.ToString();

            if (status != "Chờ duyệt")
            {
                MessageBox.Show("Đơn này đã được xử lý trước đó!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult result = MessageBox.Show($"Bạn có chắc muốn TỪ CHỐI đơn nghỉ phép của {employeeName}?",
                "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (result == DialogResult.Yes)
            {
                try
                {
                    long approverID = 1; // Thay bằng currentUser.EmployeeID

                    if (LeaveRequestBLL.RejectLeaveRequest(leaveRequestID, approverID))
                    {
                        MessageBox.Show("Từ chối đơn nghỉ phép thành công!", "Thông báo",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadData();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi: " + ex.Message, "Lỗi",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgvLeaveRequests.CurrentRow == null)
            {
                MessageBox.Show("Vui lòng chọn đơn nghỉ phép cần xóa!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            long leaveRequestID = Convert.ToInt64(dgvLeaveRequests.CurrentRow.Cells["LeaveRequestID"].Value);
            string employeeName = dgvLeaveRequests.CurrentRow.Cells["EmployeeName"].Value.ToString();
            string status = dgvLeaveRequests.CurrentRow.Cells["Status"].Value.ToString();

            if (status != "Chờ duyệt")
            {
                MessageBox.Show("Chỉ có thể xóa đơn đang chờ duyệt!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult result = MessageBox.Show($"Bạn có chắc muốn XÓA đơn nghỉ phép của {employeeName}?",
                "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (result == DialogResult.Yes)
            {
                try
                {
                    if (LeaveRequestBLL.DeleteLeaveRequest(leaveRequestID))
                    {
                        MessageBox.Show("Xóa đơn nghỉ phép thành công!", "Thông báo",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadData();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi: " + ex.Message, "Lỗi",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnStatistics_Click(object sender, EventArgs e)
        {
            frmLeaveStatistics frm = new frmLeaveStatistics();
            frm.ShowDialog();
        }

        private void dgvLeaveRequests_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                btnEdit_Click(sender, e);
            }
        }

        private void dgvLeaveRequests_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvLeaveRequests.CurrentRow != null)
            {
                string status = dgvLeaveRequests.CurrentRow.Cells["Status"].Value?.ToString();

                // Chỉ cho phép sửa/xóa đơn chờ duyệt
                btnEdit.Enabled = (status == "Chờ duyệt");

                if (currentUser?.Role == "Admin")
                {
                    btnDelete.Enabled = (status == "Chờ duyệt");
                    btnApprove.Enabled = (status == "Chờ duyệt");
                    btnReject.Enabled = (status == "Chờ duyệt");
                }
            }
        }

        private void txtSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnSearch_Click(sender, e);
            }
        }
    }
}
