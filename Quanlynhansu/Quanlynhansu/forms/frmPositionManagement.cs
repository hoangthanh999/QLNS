using System;
using System.Data;
using System.Windows.Forms;
using Quanlynhansu.BLL;

namespace Quanlynhansu.Forms
{
    public partial class frmPositionManagement : Form
    {
        public frmPositionManagement()
        {
            InitializeComponent();
        }

        private void frmPositionManagement_Load(object sender, EventArgs e)
        {
            LoadPositions();
        }

        private void LoadPositions()
        {
            try
            {
                DataTable dt = PositionBLL.GetAllPositions();
                dgvPositions.DataSource = dt;
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
            if (dgvPositions.Columns.Count > 0)
            {
                dgvPositions.Columns["PositionID"].HeaderText = "ID";
                dgvPositions.Columns["PositionID"].Width = 60;
                dgvPositions.Columns["PositionCode"].HeaderText = "Mã chức vụ";
                dgvPositions.Columns["PositionCode"].Width = 120;
                dgvPositions.Columns["PositionName"].HeaderText = "Tên chức vụ";
                dgvPositions.Columns["PositionName"].Width = 250;
                dgvPositions.Columns["Level"].HeaderText = "Cấp bậc";
                dgvPositions.Columns["Level"].Width = 100;
                dgvPositions.Columns["LevelName"].HeaderText = "Tên cấp bậc";
                dgvPositions.Columns["LevelName"].Width = 200;
                dgvPositions.Columns["BaseSalary"].HeaderText = "Lương cơ bản";
                dgvPositions.Columns["BaseSalary"].Width = 150;
                dgvPositions.Columns["BaseSalary"].DefaultCellStyle.Format = "N0";
                dgvPositions.Columns["BaseSalary"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgvPositions.Columns["IsActive"].Visible = false;
                dgvPositions.Columns["EmployeeCount"].HeaderText = "Số NV";
                dgvPositions.Columns["EmployeeCount"].Width = 100;
                dgvPositions.Columns["EmployeeCount"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            frmPositionDetail frm = new frmPositionDetail();
            if (frm.ShowDialog() == DialogResult.OK)
            {
                LoadPositions();
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (dgvPositions.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn chức vụ cần sửa!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int positionID = Convert.ToInt32(dgvPositions.SelectedRows[0].Cells["PositionID"].Value);
            frmPositionDetail frm = new frmPositionDetail(positionID);
            if (frm.ShowDialog() == DialogResult.OK)
            {
                LoadPositions();
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgvPositions.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn chức vụ cần xóa!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int positionID = Convert.ToInt32(dgvPositions.SelectedRows[0].Cells["PositionID"].Value);
            string positionName = dgvPositions.SelectedRows[0].Cells["PositionName"].Value.ToString();

            DialogResult result = MessageBox.Show(
                $"Bạn có chắc muốn xóa chức vụ '{positionName}'?",
                "Xác nhận xóa",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                try
                {
                    if (PositionBLL.DeletePosition(positionID))
                    {
                        MessageBox.Show("Xóa chức vụ thành công!", "Thành công",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadPositions();
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
            LoadPositions();
        }
    }
}
