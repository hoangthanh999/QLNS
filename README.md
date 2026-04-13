# QLNS - Quản Lý Nhân Sự (Human Resources Management System)

## 📋 Giới Thiệu
QLNS là ứng dụng quản lý nhân sự desktop được xây dựng bằng C# .NET Framework, cung cấp các công cụ quản lý toàn diện cho phòng nhân sự.

## 🎯 Tính Năng Chính
- **Quản Lý Nhân Viên** - Thêm, sửa, xóa, và quản lý thông tin nhân viên
- **Quản Lý Bộ Phận** - Quản lý các bộ phận trong công ty
- **Quản Lý Chức Vụ** - Quản lý vị trí công việc và chức vụ
- **Quản Lý Lương** - Tính toán và quản lý lương nhân viên
- **Chấm Công** - Theo dõi chấm công nhân viên
- **Quản Lý Đơn Xin Phép** - Xử lý các đơn xin phép của nhân viên
- **Báo Cáo Thống Kê** - Tạo báo cáo lương, nhân viên, thống kê phép
- **Quản Lý Người Dùng** - Quản lý tài khoản person dùng hệ thống
- **Đăng Nhập** - Hệ thống xác thực người dùng

## 🛠️ Công Nghệ Sử Dụng
- **Ngôn Ngữ**: C# (.NET Framework 4.7.2)
- **Giao Diện**: Windows Forms
- **Cơ Sở Dữ Liệu**: SQL Server
- **Thư Viện**: ClosedXML (xuất báo cáo Excel)

## 📁 Cấu Trúc Dự Án

Quanlynhansu/
├── forms/ # Giao diện người dùng (UI)
│ ├── frmLogin.cs # Form đăng nhập
│ ├── frmMain.cs # Form chính
│ ├── frmEmployeeList.cs # Danh sách nhân viên
│ ├── frmSalaryManagement.cs # Quản lý lương
│ └── ...
├── BLL/ # Business Logic Layer (Logic nghiệp vụ)
│ ├── EmployeeBLL.cs
│ ├── SalaryBLL.cs
│ ├── DepartmentBLL.cs
│ └── ...
├── DAL/ # Data Access Layer (Truy cập dữ liệu)
│ ├── EmployeeDAL.cs
│ ├── SalaryDAL.cs
│ ├── DatabaseConnection.cs
│ └── ...
├── Models/ # Các lớp model (Entity)
├── Utilities/ # Các tiện ích hỗ trợ
├── App.config # Cấu hình kết nối cơ sở dữ liệu
└── Program.cs # Entry point ứng dụng

📋 SQL Database
Database name: HRM_System
Xem chi tiết trong file SQLQuery1.sql
🚀 Hướng Dẫn Chạy Ứng Dụng
Clone hoặc mở dự án
Cập nhật chuỗi kết nối trong App.config
Đảm bảo SQL Server đang chạy
Build solution: Quanlynhansu.sln
Chạy ứng dụng (Login với tài khoản đã tạo)
👥 Tác Giả
Hoàng Thanh Hồng