namespace Quanlynhansu.Forms
{
    partial class frmDepartmentDetail
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblDepartmentCode;
        private System.Windows.Forms.TextBox txtDepartmentCode;
        private System.Windows.Forms.Label lblDepartmentName;
        private System.Windows.Forms.TextBox txtDepartmentName;
        private System.Windows.Forms.Label lblManager;
        private System.Windows.Forms.ComboBox cboManager;
        private System.Windows.Forms.Label lblParentDepartment;
        private System.Windows.Forms.ComboBox cboParentDepartment;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnCancel;

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
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblDepartmentCode = new System.Windows.Forms.Label();
            this.txtDepartmentCode = new System.Windows.Forms.TextBox();
            this.lblDepartmentName = new System.Windows.Forms.Label();
            this.txtDepartmentName = new System.Windows.Forms.TextBox();
            this.lblManager = new System.Windows.Forms.Label();
            this.cboManager = new System.Windows.Forms.ComboBox();
            this.lblParentDepartment = new System.Windows.Forms.Label();
            this.cboParentDepartment = new System.Windows.Forms.ComboBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lblTitle
            // 
            this.lblTitle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(122)))), ((int)(((byte)(204)))));
            this.lblTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.White;
            this.lblTitle.Location = new System.Drawing.Point(0, 0);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(534, 60);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "THÔNG TIN PHÒNG BAN";
            this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblDepartmentCode
            // 
            this.lblDepartmentCode.AutoSize = true;
            this.lblDepartmentCode.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblDepartmentCode.Location = new System.Drawing.Point(30, 90);
            this.lblDepartmentCode.Name = "lblDepartmentCode";
            this.lblDepartmentCode.Size = new System.Drawing.Size(115, 19);
            this.lblDepartmentCode.TabIndex = 1;
            this.lblDepartmentCode.Text = "Mã phòng ban: *";
            // 
            // txtDepartmentCode
            // 
            this.txtDepartmentCode.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtDepartmentCode.Location = new System.Drawing.Point(180, 87);
            this.txtDepartmentCode.MaxLength = 20;
            this.txtDepartmentCode.Name = "txtDepartmentCode";
            this.txtDepartmentCode.Size = new System.Drawing.Size(320, 25);
            this.txtDepartmentCode.TabIndex = 2;
            // 
            // lblDepartmentName
            // 
            this.lblDepartmentName.AutoSize = true;
            this.lblDepartmentName.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblDepartmentName.Location = new System.Drawing.Point(30, 130);
            this.lblDepartmentName.Name = "lblDepartmentName";
            this.lblDepartmentName.Size = new System.Drawing.Size(117, 19);
            this.lblDepartmentName.TabIndex = 3;
            this.lblDepartmentName.Text = "Tên phòng ban: *";
            // 
            // txtDepartmentName
            // 
            this.txtDepartmentName.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtDepartmentName.Location = new System.Drawing.Point(180, 127);
            this.txtDepartmentName.MaxLength = 200;
            this.txtDepartmentName.Name = "txtDepartmentName";
            this.txtDepartmentName.Size = new System.Drawing.Size(320, 25);
            this.txtDepartmentName.TabIndex = 4;
            // 
            // lblManager
            // 
            this.lblManager.AutoSize = true;
            this.lblManager.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblManager.Location = new System.Drawing.Point(30, 170);
            this.lblManager.Name = "lblManager";
            this.lblManager.Size = new System.Drawing.Size(100, 19);
            this.lblManager.TabIndex = 5;
            this.lblManager.Text = "Trưởng phòng:";
            // 
            // cboManager
            // 
            this.cboManager.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboManager.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cboManager.FormattingEnabled = true;
            this.cboManager.Location = new System.Drawing.Point(180, 167);
            this.cboManager.Name = "cboManager";
            this.cboManager.Size = new System.Drawing.Size(320, 25);
            this.cboManager.TabIndex = 6;
            // 
            // lblParentDepartment
            // 
            this.lblParentDepartment.AutoSize = true;
            this.lblParentDepartment.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblParentDepartment.Location = new System.Drawing.Point(30, 210);
            this.lblParentDepartment.Name = "lblParentDepartment";
            this.lblParentDepartment.Size = new System.Drawing.Size(108, 19);
            this.lblParentDepartment.TabIndex = 7;
            this.lblParentDepartment.Text = "Phòng ban cha:";
            // 
            // cboParentDepartment
            // 
            this.cboParentDepartment.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboParentDepartment.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cboParentDepartment.FormattingEnabled = true;
            this.cboParentDepartment.Location = new System.Drawing.Point(180, 207);
            this.cboParentDepartment.Name = "cboParentDepartment";
            this.cboParentDepartment.Size = new System.Drawing.Size(320, 25);
            this.cboParentDepartment.TabIndex = 8;
            // 
            // btnSave
            // 
            this.btnSave.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(204)))), ((int)(((byte)(113)))));
            this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSave.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnSave.ForeColor = System.Drawing.Color.White;
            this.btnSave.Location = new System.Drawing.Point(280, 260);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(100, 35);
            this.btnSave.TabIndex = 9;
            this.btnSave.Text = "Lưu";
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(149)))), ((int)(((byte)(165)))), ((int)(((byte)(166)))));
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancel.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnCancel.ForeColor = System.Drawing.Color.White;
            this.btnCancel.Location = new System.Drawing.Point(400, 260);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(100, 35);
            this.btnCancel.TabIndex = 10;
            this.btnCancel.Text = "Hủy";
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // frmDepartmentDetail
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(534, 321);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.cboParentDepartment);
            this.Controls.Add(this.lblParentDepartment);
            this.Controls.Add(this.cboManager);
            this.Controls.Add(this.lblManager);
            this.Controls.Add(this.txtDepartmentName);
            this.Controls.Add(this.lblDepartmentName);
            this.Controls.Add(this.txtDepartmentCode);
            this.Controls.Add(this.lblDepartmentCode);
            this.Controls.Add(this.lblTitle);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmDepartmentDetail";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Chi tiết phòng ban";
            this.Load += new System.EventHandler(this.frmDepartmentDetail_Load);
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}
