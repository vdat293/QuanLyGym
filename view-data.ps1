# PowerShell script để xem dữ liệu database
$dbPath = Join-Path $env:LOCALAPPDATA "QuanLyPhongGym\gym.db"

if (-not (Test-Path $dbPath)) {
    Write-Host "Database không tồn tại: $dbPath" -ForegroundColor Red
    Write-Host "Hãy chạy ứng dụng trước để tạo database!" -ForegroundColor Yellow
    pause
    exit
}

Write-Host "Database: $dbPath" -ForegroundColor Green
Write-Host ("=" * 120)

# Load SQLite
Add-Type -Path "$PSScriptRoot\QuanLyPhongGym.Wpf\bin\Debug\net8.0-windows\Microsoft.Data.Sqlite.dll"

$connectionString = "Data Source=$dbPath"
$connection = New-Object Microsoft.Data.Sqlite.SqliteConnection($connectionString)
$connection.Open()

# Đếm số lượng
$countCmd = $connection.CreateCommand()
$countCmd.CommandText = "SELECT COUNT(*) FROM Members"
$count = $countCmd.ExecuteScalar()
Write-Host "`nTổng số hội viên: $count`n" -ForegroundColor Cyan

# Lấy danh sách
$command = $connection.CreateCommand()
$command.CommandText = @"
SELECT
    m.Id,
    m.FamilyName || ' ' || COALESCE(m.MiddleName || ' ', '') || m.GivenName AS FullName,
    COALESCE(m.Phone, '') AS Phone,
    COALESCE(p.Name, '') AS PlanName,
    m.JoinDate,
    m.PtStatus,
    m.TotalTrainingDays
FROM Members m
LEFT JOIN MembershipPlans p ON m.MembershipPlanId = p.Id
ORDER BY m.JoinDate DESC
"@

$reader = $command.ExecuteReader()

Write-Host ("{0,-5} {1,-30} {2,-15} {3,-35} {4,-15} {5,-15} {6,-10}" -f "ID", "Họ và tên", "SĐT", "Gói tập", "Ngày tham gia", "PT", "Ngày tập")
Write-Host ("-" * 130)

while ($reader.Read()) {
    $id = $reader.GetInt32(0)
    $fullName = $reader.GetString(1)
    $phone = $reader.GetString(2)
    $planName = $reader.GetString(3)
    $joinDate = ([DateTime]::Parse($reader.GetString(4))).ToString("dd/MM/yyyy")
    $ptStatus = $reader.GetInt32(5)
    $ptStatusText = switch ($ptStatus) {
        0 { "Không có PT" }
        1 { "Có PT" }
        2 { "PT ngoài" }
        default { "" }
    }
    $trainingDays = $reader.GetInt32(6)

    Write-Host ("{0,-5} {1,-30} {2,-15} {3,-35} {4,-15} {5,-15} {6,-10}" -f $id, $fullName, $phone, $planName, $joinDate, $ptStatusText, $trainingDays)
}

$reader.Close()
$connection.Close()

Write-Host "`n" ("=" * 120)
Write-Host "`nNhấn phím bất kỳ để thoát..." -ForegroundColor Yellow
$null = $Host.UI.RawUI.ReadKey("NoEcho,IncludeKeyDown")
