using System;
using System.Data;
using System.Windows.Forms;

namespace Quanlynhansu.Forms
{
    public partial class frmImportPreview : Form
    {
        private DataTable dataTable;

        public frmImportPreview(DataTable dt)
        {
            InitializeComponent();
            this.dataTable = dt;
        }

        private void frmImportPreview_Load(object sender, EventArgs e)
        {
            try
            {
                // Hiển thị dữ liệu
                dgvPreview.DataSource = dataTable;

                // Cập nhật label
                lblInfo.Text = $"Xem trước dữ liệu import - Tổng: {dataTable.Rows.Count} bản ghi";

                // Format DataGridView
                FormatDataGridView();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi hiển thị dữ liệu: {ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void FormatDataGridView()
        {
            if (dgvPreview.Columns.Count > 0)
            {
                // Set header text
                dgvPreview.Columns["EmployeeCode"].HeaderText = "Mã NV";
                dgvPreview.Columns["FullName"].HeaderText = "Họ và tên";
                dgvPreview.Columns["DateOfBirth"].HeaderText = "Ngày sinh";
                dgvPreview.Columns["DateOfBirth"].DefaultCellStyle.Format = "dd/MM/yyyy";
                dgvPreview.Columns["Gender"].HeaderText = "Giới tính";
                dgvPreview.Columns["PhoneNumber"].HeaderText = "SĐT";
                dgvPreview.Columns["Email"].HeaderText = "Email";
                dgvPreview.Columns["Address"].HeaderText = "Địa chỉ";
                dgvPreview.Columns["IdentityCard"].HeaderText = "CMND/CCCD";
                dgvPreview.Columns["DepartmentID"].HeaderText = "Mã PB";
                dgvPreview.Columns["DepartmentID"].Width = 80;
                dgvPreview.Columns["PositionID"].HeaderText = "Mã CV";
                dgvPreview.Columns["PositionID"].Width = 80;
                dgvPreview.Columns["HireDate"].HeaderText = "Ngày vào";
                dgvPreview.Columns["HireDate"].DefaultCellStyle.Format = "dd/MM/yyyy";
                dgvPreview.Columns["Status"].HeaderText = "Trạng thái";
                dgvPreview.Columns["Salary"].HeaderText = "Lương";
                dgvPreview.Columns["Salary"].DefaultCellStyle.Format = "N0";
                dgvPreview.Columns["Salary"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

                // Auto size columns
                dgvPreview.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
                dgvPreview.Columns["FullName"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                dgvPreview.Columns["Email"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

                // Style
                dgvPreview.EnableHeadersVisualStyles = false;
                dgvPreview.ColumnHeadersDefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(0, 122, 204);
                dgvPreview.ColumnHeadersDefaultCellStyle.ForeColor = System.Drawing.Color.White;
                dgvPreview.ColumnHeadersDefaultCellStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold);
                dgvPreview.ColumnHeadersHeight = 35;
                dgvPreview.AlternatingRowsDefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(240, 240, 240);
            }
        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
