using System;

namespace Quanlynhansu.Models
{
    public class Position
    {
        public int PositionID { get; set; }
        public string PositionCode { get; set; }
        public string PositionName { get; set; }
        public int Level { get; set; }
        public decimal? BaseSalary { get; set; }
        public bool IsActive { get; set; }

        // Thuộc tính bổ sung (không có trong DB)
        public int EmployeeCount { get; set; }
        public string LevelName { get; set; }
    }
}
