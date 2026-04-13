using System;
using System.Data;
using System.Windows.Forms;
using Quanlynhansu.BLL;
using Quanlynhansu.Models;

namespace Quanlynhansu.Forms
{
    public partial class frmDepartmentDetail : Form
    {
        private int? departmentID = null;
        private bool isEditMode = false;

        public frmDepartmentDetail()
        {
            InitializeComponent();
            isEditMode = false;
        }

        public frmDepartmentDetail(int departmentID)
        {
            InitializeComponent();
            this.departmentID = departmentID;
            isEditMode = true;
        }

        private void frmDepartmentDetail_Load(object sender, EventArgs e)
        {
            LoadComboBoxes();

            if (isEditMode && departmentID.HasValue)
            {
                lblTitle.Text = "CẬP NHẬT PHÒNG BAN";
                this.Text = "Cập nhật phòng ban";
                LoadDepartmentData();
            }
            else
            {
                lblTitle.Text = "THÊM PHÒNG BAN MỚI";
                this.Text = "Thêm phòng ban";
            }
        }

        private void LoadComboBoxes()
        {
            try
            {
                // Load Manager
                DataTable dtManagers = DepartmentBLL.GetEmployeesForManager();
                DataRow rowManager = dtManagers.NewRow();
                rowManager["EmployeeID"] = DBNull.Value;
                rowManager["EmployeeName"] = "-- Chưa chọn --";
                dtManagers.Rows.InsertAt(rowManager, 0);

                cboManager.DataSource = dtManagers;
                cboManager.DisplayMember = "EmployeeName";
                cboManager.ValueMember = "EmployeeID";

                // Load Parent Department
                DataTable dtParents = DepartmentBLL.GetParentDepartments();
                DataRow rowParent = dtParents.NewRow();
                rowParent["DepartmentID"] = DBNull.Value;
                rowParent["DepartmentName"] = "-- Không có --";
                dtParents.Rows.InsertAt(rowParent, 0);

                cboParentDepartment.DataSource = dtParents;
                cboParentDepartment.DisplayMember = "DepartmentName";
                cboParentDepartment.ValueMember = "DepartmentID";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải dữ liệu: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadDepartmentData()
        {
            try
            {
                Department dept = DepartmentBLL.GetDepartmentById(departmentID.Value);
                if (dept != null)
                {
                    txtDepartmentCode.Text = dept.DepartmentCode;
                    txtDepartmentName.Text = dept.DepartmentName;

                    if (dept.ManagerID.HasValue)
                        cboManager.SelectedValue = dept.ManagerID.Value;
                    else
                        cboManager.SelectedIndex = 0;

                    if (dept.ParentDepartmentID.HasValue)
                        cboParentDepartment.SelectedValue = dept.ParentDepartmentID.Value;
                    else
                        cboParentDepartment.SelectedIndex = 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải dữ liệu: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtDepartmentCode.Text))
                {
                    MessageBox.Show("Vui lòng nhập mã phòng ban!", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtDepartmentCode.Focus();
                    return;
                }

                if (string.IsNullOrWhiteSpace(txtDepartmentName.Text))
                {
                    MessageBox.Show("Vui lòng nhập tên phòng ban!", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtDepartmentName.Focus();
                    return;
                }

                Department dept = new Department
                {
                    DepartmentCode = txtDepartmentCode.Text.Trim(),
                    DepartmentName = txtDepartmentName.Text.Trim(),
                    ManagerID = cboManager.SelectedValue != DBNull.Value ? (long?)Convert.ToInt64(cboManager.SelectedValue) : null,
                    ParentDepartmentID = cboParentDepartment.SelectedValue != DBNull.Value ? (int?)Convert.ToInt32(cboParentDepartment.SelectedValue) : null
                };

                bool result = false;

                if (isEditMode && departmentID.HasValue)
                {
                    dept.DepartmentID = departmentID.Value;
                    result = DepartmentBLL.UpdateDepartment(dept);
                    if (result)
                    {
                        MessageBox.Show("Cập nhật phòng ban thành công!", "Thành công",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.DialogResult = DialogResult.OK;
                        this.Close();
                    }
                }
                else
                {
                    result = DepartmentBLL.AddDepartment(dept);
                    if (result)
                    {
                        MessageBox.Show("Thêm phòng ban thành công!", "Thành công",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.DialogResult = DialogResult.OK;
                        this.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Lỗi",
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
