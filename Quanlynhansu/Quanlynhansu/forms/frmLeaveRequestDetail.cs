using System;
using System.Data;
using System.Windows.Forms;
using Quanlynhansu.BLL;
using Quanlynhansu.Models;

namespace Quanlynhansu.Forms
{
    public partial class frmLeaveRequestDetail : Form
    {
        private long? leaveRequestID = null;
        private User currentUser;
        private bool isEditMode = false;

        // Constructor cho thêm mới
        public frmLeaveRequestDetail(User user)
        {
            InitializeComponent();
            this.currentUser = user;
            this.isEditMode = false;
        }

        // Constructor cho sửa
        public frmLeaveRequestDetail(long leaveRequestID, User user)
        {
            InitializeComponent();
            this.leaveRequestID = leaveRequestID;
            this.currentUser = user;
            this.isEditMode = true;
        }

        private void frmLeaveRequestDetail_Load(object sender, EventArgs e)
        {
            try
            {
                LoadEmployees();
                LoadLeaveTypes();

                if (isEditMode && leaveRequestID.HasValue)
                {
                    LoadLeaveRequestData();
                    this.Text = "Sửa đơn nghỉ phép";
                }
                else
                {
                    this.Text = "Thêm đơn nghỉ phép mới";
                    dtpFromDate.Value = DateTime.Now;
                    dtpToDate.Value = DateTime.Now;
                    CalculateTotalDays();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải dữ liệu: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadEmployees()
        {
            try
            {
                DataTable dt = LeaveRequestBLL.GetActiveEmployees();

                cboEmployee.DataSource = dt;
                cboEmployee.DisplayMember = "DisplayName";
                cboEmployee.ValueMember = "EmployeeID";
                cboEmployee.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải danh sách nhân viên: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadLeaveTypes()
        {
            cboLeaveType.Items.Clear();
            cboLeaveType.Items.AddRange(LeaveTypes.GetAll());
            cboLeaveType.SelectedIndex = 0;
        }

        private void LoadLeaveRequestData()
        {
            try
            {
                LeaveRequest leave = LeaveRequestBLL.GetLeaveRequestById(leaveRequestID.Value);

                if (leave != null)
                {
                    txtLeaveRequestID.Text = leave.LeaveRequestID.ToString();
                    cboEmployee.SelectedValue = leave.EmployeeID;
                    txtEmployeeName.Text = leave.EmployeeName;
                    cboLeaveType.SelectedItem = leave.LeaveType;
                    dtpFromDate.Value = leave.FromDate;
                    dtpToDate.Value = leave.ToDate;
                    txtReason.Text = leave.Reason;
                    txtTotalDays.Text = leave.TotalDays.ToString();

                    // Không cho sửa nhân viên khi edit
                    cboEmployee.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải thông tin đơn nghỉ phép: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cboEmployee_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboEmployee.SelectedIndex >= 0)
            {
                DataRowView row = (DataRowView)cboEmployee.SelectedItem;
                txtEmployeeName.Text = row["FullName"].ToString();
            }
            else
            {
                txtEmployeeName.Clear();
            }
        }

        private void dtpDate_ValueChanged(object sender, EventArgs e)
        {
            CalculateTotalDays();
        }

        private void CalculateTotalDays()
        {
            try
            {
                int totalDays = LeaveRequestBLL.CalculateLeaveDays(dtpFromDate.Value, dtpToDate.Value);
                txtTotalDays.Text = totalDays.ToString();
            }
            catch
            {
                txtTotalDays.Text = "0";
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                // Validation
                if (cboEmployee.SelectedIndex < 0)
                {
                    MessageBox.Show("Vui lòng chọn nhân viên!", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    cboEmployee.Focus();
                    return;
                }

                if (cboLeaveType.SelectedIndex < 0)
                {
                    MessageBox.Show("Vui lòng chọn loại nghỉ phép!", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    cboLeaveType.Focus();
                    return;
                }

                if (dtpToDate.Value < dtpFromDate.Value)
                {
                    MessageBox.Show("Ngày kết thúc phải sau ngày bắt đầu!", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    dtpToDate.Focus();
                    return;
                }

                if (string.IsNullOrWhiteSpace(txtReason.Text))
                {
                    MessageBox.Show("Vui lòng nhập lý do nghỉ phép!", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtReason.Focus();
                    return;
                }

                // Tạo object
                LeaveRequest leave = new LeaveRequest
                {
                    EmployeeID = Convert.ToInt64(cboEmployee.SelectedValue),
                    LeaveType = cboLeaveType.SelectedItem.ToString(),
                    FromDate = dtpFromDate.Value,
                    ToDate = dtpToDate.Value,
                    Reason = txtReason.Text.Trim()
                };

                bool result = false;

                if (isEditMode && leaveRequestID.HasValue)
                {
                    // Cập nhật
                    leave.LeaveRequestID = leaveRequestID.Value;
                    result = LeaveRequestBLL.UpdateLeaveRequest(leave);
                }
                else
                {
                    // Thêm mới
                    result = LeaveRequestBLL.AddLeaveRequest(leave);
                }

                if (result)
                {
                    MessageBox.Show(
                        isEditMode ? "Cập nhật đơn nghỉ phép thành công!" : "Thêm đơn nghỉ phép thành công!",
                        "Thông báo",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information
                    );

                    this.DialogResult = DialogResult.OK;
                    this.Close();
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
    }
}
