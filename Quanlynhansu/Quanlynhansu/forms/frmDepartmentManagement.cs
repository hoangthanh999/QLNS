using System;
using System.Data;
using System.Windows.Forms;
using Quanlynhansu.BLL;

namespace Quanlynhansu.Forms
{
    public partial class frmDepartmentManagement : Form
    {
        public frmDepartmentManagement()
        {
            InitializeComponent();
        }

        private void frmDepartmentManagement_Load(object sender, EventArgs e)
        {
            LoadDepartments();
        }

        private void LoadDepartments()
        {
            try
            {
                DataTable dt = DepartmentBLL.GetAllDepartments();
                dgvDepartments.DataSource = dt;
                FormatDataGridView();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải dữ liệu: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void FormatDataGridView()
        {
            if (dgvDepartments.Columns.Count > 0)
            {
                dgvDepartments.Columns["DepartmentID"].HeaderText = "ID";
                dgvDepartments.Columns["DepartmentID"].Width = 60;
                dgvDepartments.Columns["DepartmentCode"].HeaderText = "Mã PB";
                dgvDepartments.Columns["DepartmentCode"].Width = 100;
                dgvDepartments.Columns["DepartmentName"].HeaderText = "Tên phòng ban";
                dgvDepartments.Columns["DepartmentName"].Width = 200;
                dgvDepartments.Columns["ManagerID"].Visible = false;
                dgvDepartments.Columns["ManagerName"].HeaderText = "Trưởng phòng";
                dgvDepartments.Columns["ManagerName"].Width = 150;
                dgvDepartments.Columns["ParentDepartmentID"].Visible = false;
                dgvDepartments.Columns["ParentDepartmentName"].HeaderText = "PB cha";
                dgvDepartments.Columns["ParentDepartmentName"].Width = 150;
                dgvDepartments.Columns["CreatedDate"].HeaderText = "Ngày tạo";
                dgvDepartments.Columns["CreatedDate"].Width = 120;
                dgvDepartments.Columns["CreatedDate"].DefaultCellStyle.Format = "dd/MM/yyyy";
                dgvDepartments.Columns["IsActive"].Visible = false;
                dgvDepartments.Columns["EmployeeCount"].HeaderText = "Số NV";
                dgvDepartments.Columns["EmployeeCount"].Width = 80;
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            frmDepartmentDetail frm = new frmDepartmentDetail();
            if (frm.ShowDialog() == DialogResult.OK)
            {
                LoadDepartments();
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (dgvDepartments.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn phòng ban cần sửa!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int departmentID = Convert.ToInt32(dgvDepartments.SelectedRows[0].Cells["DepartmentID"].Value);
            frmDepartmentDetail frm = new frmDepartmentDetail(departmentID);
            if (frm.ShowDialog() == DialogResult.OK)
            {
                LoadDepartments();
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgvDepartments.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn phòng ban cần xóa!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int departmentID = Convert.ToInt32(dgvDepartments.SelectedRows[0].Cells["DepartmentID"].Value);
            string departmentName = dgvDepartments.SelectedRows[0].Cells["DepartmentName"].Value.ToString();

            DialogResult result = MessageBox.Show(
                $"Bạn có chắc muốn xóa phòng ban '{departmentName}'?",
                "Xác nhận xóa",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                try
                {
                    if (DepartmentBLL.DeleteDepartment(departmentID))
                    {
                        MessageBox.Show("Xóa phòng ban thành công!", "Thành công",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadDepartments();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Lỗi",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadDepartments();
        }
    }
}
