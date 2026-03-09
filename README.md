# 🦈 SharkTankDesktop

Ứng dụng desktop quản trị hệ thống **SharkTank ERP**
Công nghệ sử dụng: **WinForms + SQL Server**

---

# ⚙️ Yêu cầu môi trường

* 💻 Windows 10 / 11
* 🧰 Visual Studio 2022 (**.NET desktop development**)
* 🧩 .NET Framework 4.8 Developer Pack
* 🗄 SQL Server (Express / Standard)
* 📊 SQL Server Management Studio (**SSMS**)

---

# 🚀 Cài và chạy project

1️⃣ Mở solution

```
SharkTankDesktop.sln
```

2️⃣ Restore NuGet (nếu Visual Studio yêu cầu)

3️⃣ Build project

```
Build > Build Solution
```

4️⃣ Chạy ứng dụng

```
F5 hoặc Ctrl + F5
```

---

# 🗄 Cài Database

Script database nằm trong thư mục:

```
Database/
```

Bao gồm:

* `00_CreateDatabase.sql`
* `CoreSystem.sql`

### Import bằng SSMS

1️⃣ Mở **SSMS** và connect SQL Server local

2️⃣ Chạy file

```
00_CreateDatabase.sql
```

3️⃣ Sau đó chạy

```
CoreSystem.sql
```

4️⃣ Kiểm tra database đã tạo thành công (ví dụ)

```
SharkTankDB
```

---

# 🔗 Cấu hình Connection String

Mở file

```
App.config
```

Nếu dùng **SQL Login**

```
Server=.;Database=SharkTankDB;User Id=sa;Password=your_password;
```

---

# 🌿 Quy tắc làm việc với Git (Quan trọng)

⚠ **Không code trực tiếp trên `main`**

Kiểm tra branch hiện tại

```
git branch --show-current
```

Nếu đang ở `main`, chuyển sang branch của mình

```
git fetch --all --prune
git switch ten-nhanh-cua-minh
```

Push code

```
git add .
git commit -m "feat: mo ta thay doi"
git push -u origin ten-nhanh-cua-minh
```

---

# ✅ Checklist trước khi push

* ✔ Đang ở **đúng branch**
* ✔ Build **không lỗi**
* ✔ App chạy được **màn hình login**
* ❌ Không commit file rác

```
bin/
obj/
.vs/
```
