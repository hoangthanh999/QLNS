-- Tạo Database
CREATE DATABASE HRM_System;
GO

USE HRM_System;
GO

-- Bảng Phòng Ban
CREATE TABLE Departments (
    DepartmentID INT PRIMARY KEY IDENTITY(1,1),
    DepartmentCode NVARCHAR(20) UNIQUE NOT NULL,
    DepartmentName NVARCHAR(200) NOT NULL,
    ManagerID BIGINT NULL,
    ParentDepartmentID INT NULL,
    CreatedDate DATETIME DEFAULT GETDATE(),
    IsActive BIT DEFAULT 1,
    INDEX IX_DepartmentCode (DepartmentCode)
);

-- Bảng Chức Vụ
CREATE TABLE Positions (
    PositionID INT PRIMARY KEY IDENTITY(1,1),
    PositionCode NVARCHAR(20) UNIQUE NOT NULL,
    PositionName NVARCHAR(200) NOT NULL,
    Level INT NOT NULL,
    BaseSalary DECIMAL(18,2),
    IsActive BIT DEFAULT 1
);

-- Bảng Nhân Viên (Tối ưu cho 100 triệu records)
CREATE TABLE Employees (
    EmployeeID BIGINT PRIMARY KEY IDENTITY(1,1),
    EmployeeCode NVARCHAR(20) UNIQUE NOT NULL,
    FullName NVARCHAR(200) NOT NULL,
    DateOfBirth DATE,
    Gender NVARCHAR(10),
    PhoneNumber NVARCHAR(20),
    Email NVARCHAR(100),
    Address NVARCHAR(500),
    IdentityCard NVARCHAR(20),
    DepartmentID INT FOREIGN KEY REFERENCES Departments(DepartmentID),
    PositionID INT FOREIGN KEY REFERENCES Positions(PositionID),
    HireDate DATE NOT NULL,
    Status NVARCHAR(50) DEFAULT N'Đang làm việc',
    Salary DECIMAL(18,2),
    Avatar NVARCHAR(500),
    CreatedDate DATETIME DEFAULT GETDATE(),
    ModifiedDate DATETIME,
    IsActive BIT DEFAULT 1,
    
    -- Indexes cho tìm kiếm nhanh
    INDEX IX_EmployeeCode (EmployeeCode),
    INDEX IX_FullName (FullName),
    INDEX IX_DepartmentID (DepartmentID),
    INDEX IX_Status (Status),
    INDEX IX_Email (Email)
);

-- Bảng Chấm Công
CREATE TABLE Attendance (
    AttendanceID BIGINT PRIMARY KEY IDENTITY(1,1),
    EmployeeID BIGINT FOREIGN KEY REFERENCES Employees(EmployeeID),
    AttendanceDate DATE NOT NULL,
    CheckInTime DATETIME,
    CheckOutTime DATETIME,
    WorkHours DECIMAL(5,2),
    Status NVARCHAR(50),
    Note NVARCHAR(500),
    
    INDEX IX_EmployeeDate (EmployeeID, AttendanceDate),
    INDEX IX_AttendanceDate (AttendanceDate)
);

-- Bảng Lương
CREATE TABLE Salaries (
    SalaryID BIGINT PRIMARY KEY IDENTITY(1,1),
    EmployeeID BIGINT FOREIGN KEY REFERENCES Employees(EmployeeID),
    Month INT NOT NULL,
    Year INT NOT NULL,
    BaseSalary DECIMAL(18,2),
    Bonus DECIMAL(18,2) DEFAULT 0,
    Deduction DECIMAL(18,2) DEFAULT 0,
    TotalSalary DECIMAL(18,2),
    PaymentDate DATE,
    Status NVARCHAR(50),
    
    INDEX IX_EmployeeMonthYear (EmployeeID, Year, Month)
);

-- Bảng Nghỉ Phép
CREATE TABLE LeaveRequests (
    LeaveRequestID BIGINT PRIMARY KEY IDENTITY(1,1),
    EmployeeID BIGINT FOREIGN KEY REFERENCES Employees(EmployeeID),
    LeaveType NVARCHAR(100),
    FromDate DATE NOT NULL,
    ToDate DATE NOT NULL,
    TotalDays INT,
    Reason NVARCHAR(500),
    Status NVARCHAR(50) DEFAULT N'Chờ duyệt',
    ApprovedBy BIGINT NULL,
    ApprovedDate DATETIME,
    CreatedDate DATETIME DEFAULT GETDATE(),
    
    INDEX IX_EmployeeID (EmployeeID),
    INDEX IX_Status (Status)
);

-- Bảng Tài Khoản
CREATE TABLE Users (
    UserID INT PRIMARY KEY IDENTITY(1,1),
    Username NVARCHAR(50) UNIQUE NOT NULL,
    PasswordHash NVARCHAR(500) NOT NULL,
    EmployeeID BIGINT FOREIGN KEY REFERENCES Employees(EmployeeID),
    Role NVARCHAR(50) NOT NULL,
    IsActive BIT DEFAULT 1,
    LastLogin DATETIME,
    CreatedDate DATETIME DEFAULT GETDATE()
);

-- Stored Procedures tối ưu cho tìm kiếm
GO
CREATE PROCEDURE sp_SearchEmployees
    @SearchText NVARCHAR(200) = NULL,
    @DepartmentID INT = NULL,
    @Status NVARCHAR(50) = NULL,
    @PageNumber INT = 1,
    @PageSize INT = 50
AS
BEGIN
    SET NOCOUNT ON;
    
    SELECT 
        e.EmployeeID,
        e.EmployeeCode,
        e.FullName,
        e.DateOfBirth,
        e.Gender,
        e.PhoneNumber,
        e.Email,
        d.DepartmentName,
        p.PositionName,
        e.HireDate,
        e.Status,
        e.Salary
    FROM Employees e
    LEFT JOIN Departments d ON e.DepartmentID = d.DepartmentID
    LEFT JOIN Positions p ON e.PositionID = p.PositionID
    WHERE 
        (@SearchText IS NULL OR 
         e.FullName LIKE '%' + @SearchText + '%' OR 
         e.EmployeeCode LIKE '%' + @SearchText + '%' OR
         e.Email LIKE '%' + @SearchText + '%')
        AND (@DepartmentID IS NULL OR e.DepartmentID = @DepartmentID)
        AND (@Status IS NULL OR e.Status = @Status)
        AND e.IsActive = 1
    ORDER BY e.EmployeeID DESC
    OFFSET (@PageNumber - 1) * @PageSize ROWS
    FETCH NEXT @PageSize ROWS ONLY;
END;
GO

-- Thêm dữ liệu mẫu
INSERT INTO Departments (DepartmentCode, DepartmentName) VALUES
(N'IT', N'Công Nghệ Thông Tin'),
(N'HR', N'Nhân Sự'),
(N'ACC', N'Kế Toán'),
(N'SALE', N'Kinh Doanh'),
(N'MKT', N'Marketing');

INSERT INTO Positions (PositionCode, PositionName, Level, BaseSalary) VALUES
(N'DIR', N'Giám Đốc', 1, 50000000),
(N'MGR', N'Quản Lý', 2, 30000000),
(N'TL', N'Trưởng Nhóm', 3, 20000000),
(N'EMP', N'Nhân Viên', 4, 10000000),
(N'INT', N'Thực Tập', 5, 5000000);

-- Tạo user admin mặc định (password: admin123)
INSERT INTO Users (Username, PasswordHash, Role) VALUES
(N'admin', N'240BE518FABD2724DDB6F04EEB1DA5967448D7E831C08C8FA822809F74C720A9', N'Admin');
USE HRM_System;
GO

-- Xóa user cũ nếu có
DELETE FROM Users WHERE Username = 'admin';

-- Thêm user admin (password: admin123)
-- Hash của "admin123" = 240BE518FABD2724DDB6F04EEB1DA5967448D7E831C08C8FA822809F74C720A9
INSERT INTO Users (Username, PasswordHash, Role, IsActive) 

VALUES ('admin', '240BE518FABD2724DDB6F04EEB1DA5967448D7E831C08C8FA822809F74C720A9', 'Admin', 1);

-- Thêm user test (password: 123456)
-- Hash của "123456" = 8D969EEF6ECAD3C29A3A629280E686CF0C3F5D5A86AFF3CA12020C923ADC6C92
INSERT INTO Users (Username, PasswordHash, Role, IsActive) 
VALUES ('user1', '8D969EEF6ECAD3C29A3A629280E686CF0C3F5D5A86AFF3CA12020C923ADC6C92', 'User', 1);

SELECT * FROM Users;


SELECT COLUMN_NAME, DATA_TYPE, CHARACTER_MAXIMUM_LENGTH
FROM INFORMATION_SCHEMA.COLUMNS
WHERE TABLE_NAME = 'Departments'

-- ========== BÁO CÁO TỔNG HỢP ==========
CREATE PROCEDURE sp_GetSalarySummaryReport
    @Month INT,
    @Year INT
AS
BEGIN
    SELECT 
        e.EmployeeCode,
        e.FullName AS EmployeeName,
        d.DepartmentName,
        p.PositionName,
        s.BaseSalary,
        s.Bonus,
        s.Deduction,
        s.TotalSalary,
        s.Status
    FROM Salaries s
    INNER JOIN Employees e ON s.EmployeeID = e.EmployeeID
    LEFT JOIN Departments d ON e.DepartmentID = d.DepartmentID
    LEFT JOIN Positions p ON e.PositionID = p.PositionID
    WHERE s.Month = @Month AND s.Year = @Year
    ORDER BY d.DepartmentName, e.EmployeeCode
END
GO

-- ========== BÁO CÁO THEO PHÒNG BAN ==========
CREATE PROCEDURE sp_GetSalaryByDepartmentReport
    @Month INT,
    @Year INT
AS
BEGIN
    SELECT 
        d.DepartmentName,
        COUNT(DISTINCT s.EmployeeID) AS EmployeeCount,
        SUM(s.BaseSalary) AS TotalBaseSalary,
        SUM(s.Bonus) AS TotalBonus,
        SUM(s.Deduction) AS TotalDeduction,
        SUM(s.TotalSalary) AS TotalSalary
    FROM Salaries s
    INNER JOIN Employees e ON s.EmployeeID = e.EmployeeID
    LEFT JOIN Departments d ON e.DepartmentID = d.DepartmentID
    WHERE s.Month = @Month AND s.Year = @Year
    GROUP BY d.DepartmentName
    ORDER BY TotalSalary DESC
END
GO

-- ========== BÁO CÁO THEO NHÂN VIÊN ==========
CREATE PROCEDURE sp_GetSalaryByEmployeeReport
    @EmployeeID BIGINT,
    @FromMonth INT,
    @FromYear INT,
    @ToMonth INT,
    @ToYear INT
AS
BEGIN
    SELECT 
        s.Month,
        s.Year,
        s.BaseSalary,
        s.Bonus,
        s.Deduction,
        s.TotalSalary,
        s.Status,
        s.PaymentDate
    FROM Salaries s
    WHERE s.EmployeeID = @EmployeeID
      AND (s.Year > @FromYear OR (s.Year = @FromYear AND s.Month >= @FromMonth))
      AND (s.Year < @ToYear OR (s.Year = @ToYear AND s.Month <= @ToMonth))
    ORDER BY s.Year, s.Month
END
GO

-- ========== DỮ LIỆU CHO BIỂU ĐỒ ==========
CREATE PROCEDURE sp_GetSalaryChartData
    @Year INT
AS
BEGIN
    SELECT 
        s.Month,
        SUM(s.TotalSalary) AS TotalSalary,
        COUNT(DISTINCT s.EmployeeID) AS EmployeeCount,
        AVG(s.TotalSalary) AS AvgSalary
    FROM Salaries s
    WHERE s.Year = @Year
    GROUP BY s.Month
    ORDER BY s.Month
END
GO



-- ========== STORED PROCEDURES CHO BÁO CÁO ==========

-- 1. Báo cáo tổng hợp
IF OBJECT_ID('sp_GetSalarySummaryReport', 'P') IS NOT NULL
    DROP PROCEDURE sp_GetSalarySummaryReport;
GO

CREATE PROCEDURE sp_GetSalarySummaryReport
    @Month INT,
    @Year INT
AS
BEGIN
    SELECT 
        e.EmployeeCode,
        e.FullName AS EmployeeName,
        d.DepartmentName,
        p.PositionName,
        s.BaseSalary,
        s.Bonus,
        s.Deduction,
        s.TotalSalary,
        s.Status
    FROM Salaries s
    INNER JOIN Employees e ON s.EmployeeID = e.EmployeeID
    LEFT JOIN Departments d ON e.DepartmentID = d.DepartmentID
    LEFT JOIN Positions p ON e.PositionID = p.PositionID
    WHERE s.Month = @Month AND s.Year = @Year
    ORDER BY d.DepartmentName, e.EmployeeCode;
END
GO

-- 2. Báo cáo theo phòng ban
IF OBJECT_ID('sp_GetSalaryByDepartmentReport', 'P') IS NOT NULL
    DROP PROCEDURE sp_GetSalaryByDepartmentReport;
GO

CREATE PROCEDURE sp_GetSalaryByDepartmentReport
    @Month INT,
    @Year INT
AS
BEGIN
    SELECT 
        ISNULL(d.DepartmentName, N'Chưa phân phòng') AS DepartmentName,
        COUNT(DISTINCT s.EmployeeID) AS EmployeeCount,
        SUM(s.BaseSalary) AS TotalBaseSalary,
        SUM(s.Bonus) AS TotalBonus,
        SUM(s.Deduction) AS TotalDeduction,
        SUM(s.TotalSalary) AS TotalSalary
    FROM Salaries s
    INNER JOIN Employees e ON s.EmployeeID = e.EmployeeID
    LEFT JOIN Departments d ON e.DepartmentID = d.DepartmentID
    WHERE s.Month = @Month AND s.Year = @Year
    GROUP BY d.DepartmentName
    ORDER BY TotalSalary DESC;
END
GO

-- 3. Báo cáo theo nhân viên
IF OBJECT_ID('sp_GetSalaryByEmployeeReport', 'P') IS NOT NULL
    DROP PROCEDURE sp_GetSalaryByEmployeeReport;
GO

CREATE PROCEDURE sp_GetSalaryByEmployeeReport
    @EmployeeID BIGINT,
    @FromMonth INT,
    @FromYear INT,
    @ToMonth INT,
    @ToYear INT
AS
BEGIN
    SELECT 
        s.Month,
        s.Year,
        s.BaseSalary,
        s.Bonus,
        s.Deduction,
        s.TotalSalary,
        s.Status,
        s.PaymentDate
    FROM Salaries s
    WHERE s.EmployeeID = @EmployeeID
      AND (s.Year > @FromYear OR (s.Year = @FromYear AND s.Month >= @FromMonth))
      AND (s.Year < @ToYear OR (s.Year = @ToYear AND s.Month <= @ToMonth))
    ORDER BY s.Year, s.Month;
END
GO

-- 4. Dữ liệu cho biểu đồ
IF OBJECT_ID('sp_GetSalaryChartData', 'P') IS NOT NULL
    DROP PROCEDURE sp_GetSalaryChartData;
GO

CREATE PROCEDURE sp_GetSalaryChartData
    @Year INT
AS
BEGIN
    SELECT 
        s.Month,
        SUM(s.TotalSalary) AS TotalSalary,
        COUNT(DISTINCT s.EmployeeID) AS EmployeeCount,
        AVG(s.TotalSalary) AS AvgSalary
    FROM Salaries s
    WHERE s.Year = @Year
    GROUP BY s.Month
    ORDER BY s.Month;
END
GO

PRINT N'✅ Đã tạo thành công 4 Stored Procedures cho Báo cáo lương!';
