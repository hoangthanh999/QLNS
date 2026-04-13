using System;

namespace Quanlynhansu.Models
{
    public class Department
    {
        public int DepartmentID { get; set; }
        public string DepartmentCode { get; set; }
        public string DepartmentName { get; set; }
        public long? ManagerID { get; set; }
        public int? ParentDepartmentID { get; set; }
        public DateTime? CreatedDate { get; set; }
        public bool IsActive { get; set; }

        // Thuộc tính bổ sung (không có trong DB)
        public string ManagerName { get; set; }
        public string ParentDepartmentName { get; set; }
        public int EmployeeCount { get; set; }
    }
}
