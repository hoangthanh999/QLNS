using System;

namespace Quanlynhansu.Models
{
    public class Employee
    {
        public long EmployeeID { get; set; }
        public string EmployeeCode { get; set; }
        public string FullName { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string Gender { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string IdentityCard { get; set; }
        public int? DepartmentID { get; set; }
        public string DepartmentName { get; set; }
        public int? PositionID { get; set; }
        public string PositionName { get; set; }
        public DateTime HireDate { get; set; }
        public string Status { get; set; }
        public decimal? Salary { get; set; }
        public string Avatar { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public bool IsActive { get; set; }
    }
}
