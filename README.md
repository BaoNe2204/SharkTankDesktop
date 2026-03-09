SharkTankDesktop

Ứng dụng desktop quản trị hệ thống SharkTank ERP (WinForms + SQL Server).

1. Yêu cầu môi trường

Windows 10/11

Visual Studio 2022 (.NET desktop development)

.NET Framework 4.8 Developer Pack

SQL Server (Express / Standard)

SQL Server Management Studio (SSMS)

2. Cài và chạy project

Mở solution: SharkTankDesktop.sln

Restore NuGet (nếu Visual Studio yêu cầu)

Build project: Build > Build Solution

Chạy ứng dụng: F5 hoặc Ctrl + F5

3. Cài Database

Script database nằm trong thư mục Database

00_CreateDatabase.sql

CoreSystem.sql

Import bằng SSMS

Mở SSMS và connect SQL Server local

Chạy 00_CreateDatabase.sql

Chạy CoreSystem.sql

Kiểm tra DB đã tạo (ví dụ SharkTankDB)

4. Cấu hình Connection String

Mở file App.config và chỉnh lại:

<connectionStrings>
  <add name="SharkTankDB"
       connectionString="Server=.;Database=SharkTankDB;Trusted_Connection=True;"
       providerName="System.Data.SqlClient" />
</connectionStrings>

Nếu dùng SQL Login:

Server=.;Database=SharkTankDB;User Id=sa;Password=your_password;
5. Quy tắc làm việc với Git (Quan trọng)

Không code trực tiếp trên main.

Kiểm tra branch hiện tại:

git branch --show-current

Nếu đang ở main, chuyển sang branch của mình:

git fetch --all --prune
git switch ten-nhanh-cua-minh

Push code:

git add .
git commit -m "feat: mo ta thay doi"
git push -u origin ten-nhanh-cua-minh
6. Checklist trước khi push

Đang ở đúng branch

Build không lỗi

App chạy được màn hình login

Không commit file rác:

bin/
obj/
.vs/
