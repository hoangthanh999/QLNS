using System;
using System.Windows.Forms;
using Quanlynhansu.BLL;
using Quanlynhansu.Models;

namespace Quanlynhansu.Forms
{
    public partial class frmPositionDetail : Form
    {
        private int? positionID = null;
        private bool isEditMode = false;

        public frmPositionDetail()
        {
            InitializeComponent();
            isEditMode = false;
        }

        public frmPositionDetail(int positionID)
        {
            InitializeComponent();
            this.positionID = positionID;
            isEditMode = true;
        }

        private void frmPositionDetail_Load(object sender, EventArgs e)
        {
            if (isEditMode && positionID.HasValue)
            {
                lblTitle.Text = "CẬP NHẬT CHỨC VỤ";
                this.Text = "Cập nhật chức vụ";
                LoadPositionData();
            }
            else
            {
                lblTitle.Text = "THÊM CHỨC VỤ MỚI";
                this.Text = "Thêm chức vụ";
            }
        }

        private void LoadPositionData()
        {
            try
            {
                Position position = PositionBLL.GetPositionById(positionID.Value);
                if (position != null)
                {
                    txtPositionCode.Text = position.PositionCode;
                    txtPositionName.Text = position.PositionName;
                    nudLevel.Value = position.Level;
                    nudBaseSalary.Value = position.BaseSalary ?? 0;
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
                if (string.IsNullOrWhiteSpace(txtPositionCode.Text))
                {
                    MessageBox.Show("Vui lòng nhập mã chức vụ!", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtPositionCode.Focus();
                    return;
                }

                if (string.IsNullOrWhiteSpace(txtPositionName.Text))
                {
                    MessageBox.Show("Vui lòng nhập tên chức vụ!", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtPositionName.Focus();
                    return;
                }

                Position position = new Position
                {
                    PositionCode = txtPositionCode.Text.Trim(),
                    PositionName = txtPositionName.Text.Trim(),
                    Level = (int)nudLevel.Value,
                    BaseSalary = nudBaseSalary.Value > 0 ? nudBaseSalary.Value : (decimal?)null
                };

                bool result = false;

                if (isEditMode && positionID.HasValue)
                {
                    position.PositionID = positionID.Value;
                    result = PositionBLL.UpdatePosition(position);
                    if (result)
                    {
                        MessageBox.Show("Cập nhật chức vụ thành công!", "Thành công",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.DialogResult = DialogResult.OK;
                        this.Close();
                    }
                }
                else
                {
                    result = PositionBLL.AddPosition(position);
                    if (result)
                    {
                        MessageBox.Show("Thêm chức vụ thành công!", "Thành công",
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
